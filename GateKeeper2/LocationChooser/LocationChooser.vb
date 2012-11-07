Imports PetesControls
Imports System.Windows.Forms
Imports System.Net.NetworkInformation
Imports System.Net
Imports System.Drawing.Printing
Imports System

Public Class LocationChooser
    Declare Function AddPrinterConnection Lib "winspool.drv" Alias "AddPrinterConnectionA" (ByVal pName As String) As Long
    Declare Function DeletePrinterConnection Lib "winspool.drv" Alias "DeletePrinterConnectionA" (ByVal pName As String) As Long
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal pszPrinter As String) As Boolean
    Declare Function GetDefaultPrinter Lib "winspool.drv" Alias "GetDefaultPrinterA" (ByVal pszBuffer() As String, ByVal pcchBuffer As Integer) As Boolean

    Event sendPrinters(ByVal plist As List(Of String), ByVal location As String)
    Dim _locList As List(Of host) = New List(Of host)
    Dim defPrinter As String = ""

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Location Services"
    Private Sub Location_Chooser_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'check for network
        If Not Networked() Then
            MessageBox.Show("No network available")
            Me.Hide()
        Else
            If Locations.Items.Count = 0 Then
                loadLocations()
            End If
        End If
    End Sub

    Private Sub loadLocations()
        Dim theHost As host
        Try
            For Each item As String In FileOps.getFiles(My.Settings.DEPLOYSHARE & My.Settings.HOSTSLOCATION)
                If Not item.Contains("-") Then
                    item = item.Remove(item.Length - 4)
                    theHost = New host(item)
                    If theHost.Name.Equals("No Printers") Then
                        _locList.Add(theHost)
                        Locations.Items.Add(theHost.Name)
                    End If
                    If theHost.PrinterList.Count > 0 Then
                        _locList.Add(theHost)
                        Locations.Items.Add(theHost.Name)
                    End If
                End If
            Next
        Catch ex As Exception
            Locations.Items.Add("No Locations Found")
        End Try

    End Sub

    Public Function getFirstLocation() As String
        If Locations.Items.Count = 0 then
            loadLocations
        End If
        Return Locations.Items.Item(0)
    End Function

    Private Function GetHostItem() As host
        If IsNothing(Locations.SelectedItem) then
            Locations.SelectedIndex = 0
        End If
        Dim location As String = Locations.SelectedItem
        Dim hostItem As host
        hostItem = _locList.Find(Function(p) p.Name.Equals(location))
        Return hostItem
    End Function

    Public Sub sendPrintersRoutine()
        Dim plist As List(Of String) = New List(Of String)
        Dim hostItem As host = GetHostItem()
        plist = hostItem.PrinterList
        RaiseEvent sendPrinters(plist, Locations.SelectedItem)
    End Sub


    Private Sub Locations_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles Locations.MouseDoubleClick
        Dim hostItem As host = GetHostItem()
        Dim info As New InfoDisplay()
        info.Title = "Location Details for " & hostItem.Name
        info.addLine(System.Drawing.FontStyle.Bold + System.Drawing.FontStyle.Underline, "Printers at this location")
        For Each printer In hostItem.PrinterList
            info.addLine(System.Drawing.FontStyle.Regular, vbTab & printer)
        Next
        info.Size = New System.Drawing.Size(300, 360)
        info.LeftButtonVis = False
        info.RightButton = "Ok"
        info.ShowDialog()
    End Sub

    Private Sub SetButton_Click(sender As System.Object, e As System.EventArgs) Handles SetButton.Click
        Me.Hide()
        sendPrintersRoutine()
    End Sub

    Public Sub setDefault(ByVal location As String)
        If Locations.Items.Count = 0 Then
            loadLocations()
        End If

        Locations.SelectedIndex = Locations.Items.IndexOf(location)
        Me.Text = Locations.SelectedItem
    End Sub

    Private Function Networked() As Boolean
        If Not System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() Then
            Return False
        End If
        Dim nics As NetworkInterface()
        Dim dnsIPs As IPAddressCollection

        nics = NetworkInterface.GetAllNetworkInterfaces()

        For Each nic As NetworkInterface In nics
            If (nic.OperationalStatus = OperationalStatus.Up) Then
                dnsIPs = nic.GetIPProperties().DnsAddresses
                For Each dnsIp As IPAddress In dnsIPs
                    If GetHostNameFromIP(dnsIp.ToString).ToLower.Contains(My.Settings.DomainID) Then
                        Return True
                    End If
                Next
            End If
        Next

        Return False
    End Function

    Public Function GetHostNameFromIP(ByRef IP As String) As String
        Dim hoste As IPHostEntry = New IPHostEntry()
        hoste.HostName = "NonExistent"
        Try
            hoste = System.Net.Dns.GetHostEntry(IP)
        Catch ex As Exception

        End Try

        Return hoste.HostName
    End Function
#End Region

#Region "Printer Functions"
    Public Sub addPrinter(ByVal name As String)
        Dim res As Boolean = True
        Try
            If name.StartsWith("=") Then
                name = name.Substring(1)
                AddPrinterConnection(name)
                setDefaultPrinter(name)
                'Utils.WriteToEventLog("Mapped default " & name, EventLogEntryType.Information)
            Else
                AddPrinterConnection(name)
                'Utils.WriteToEventLog("Mapped " & name, EventLogEntryType.Information)
            End If
        Catch ex As Exception
            Throw New PrinterException("Error adding printer")
            'Utils.WriteToEventLog("Error Adding Printer", EventLogEntryType.Error)
        End Try

    End Sub

    Public Sub deleteAllRemotePrinters()
        Dim res As Boolean = True
        Try
            For Each strPrinterName As String In PrinterSettings.InstalledPrinters
                If strPrinterName.StartsWith("\\") Then
                    deletePrinter(strPrinterName)
                End If
                If strPrinterName.StartsWith("XPS") then
                    deletePrinter(strPrinterName)
                End If
            Next
        Catch ex As Exception
            Throw New PrinterException("Error deleting printers")
        End Try
    End Sub

    Public Sub setTheDefaultPrinter()
        setDefaultPrinter(defPrinter)
    End Sub

    Public Sub setTheDefaultPrinter(ByVal name As String)
        setDefaultPrinter(name)
    End Sub

    Public Sub setLocalDefault()
        Try
            For Each prn As String In PrinterSettings.InstalledPrinters
                If Not prn.StartsWith("\\") Then
                    setDefaultPrinter(prn)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    'Delete printer by name
    Public Sub deletePrinter(ByVal name As String)
        Try
            DeletePrinterConnection(name)
        Catch ex As Exception
            Throw New PrinterException("Error deleting printer")
        End Try
    End Sub

#End Region

End Class

Public Class PrinterException
    Inherits ApplicationException

    Public Sub New()
        MyBase.New("General Printer Exception")
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class
