Imports System.ServiceProcess
Imports PetesControls
Imports MachineManager
Imports System
Imports Microsoft.Win32
Imports System.Threading
Imports System.Net.Sockets
Imports System.Text
Imports System.Net
Imports System.Security
Imports System.Security.AccessControl
Imports System.Security.Cryptography
Imports System.IO
Imports System.Net.NetworkInformation
Imports System.Collections.Specialized

Public Class MiddlemanService
    Private reg As regControl = New regControl
    Private priv As PrivilegeFunctions = New PrivilegeFunctions
    Private adTool As New ADTools

    Private machineName As String = My.Computer.Name
    Private theHost As host = New host(My.Resources.HostsLocation, machineName, True)
    Private devicetype As String

    Private Const port As Integer = 27429
    Dim listening As Boolean = True
    Dim commandBuffer As String

    Dim debugs As String = ""
    Dim debug As Boolean = False
    Private ReadOnly Property UTF8() As Encoding
        Get
            Return System.Text.Encoding.UTF8
        End Get
    End Property

    ' To access the constructor in Visual Basic, select New from the
    ' method name drop-down list. 
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        'EventLog1.WriteEntry("Starting Up", EventLogEntryType.Information)
        devicetype = reg.readReg("HKLM#Software\asGatekeeper#deviceType")
        debugs = reg.readReg("HKLM#Software\asGatekeeper#debug")
        EventLog1.WriteEntry("Debug status = " & debug, EventLogEntryType.Information)
        If debugs.ToLower.Equals("true") Then
            debug = True
        End If

        'setup whitelist
        theHost.addPrivUser("administrator")
        theHost.addPrivUser("setup")

        'remove old admins

        debuglog("Removing old admins", EventLogEntryType.Information)

        For Each user In Utils.getAdmins(machineName)
            If Not theHost.PrivList.Contains(user) Then
                debuglog("Removing - " & user, EventLogEntryType.Information)
                Utils.RemovelocalAdmin(machineName, user)
            End If
        Next

        ' Add priv users to local admin group
        debuglog("Adding local admins", EventLogEntryType.Information)
        For Each user In theHost.PrivList
            debuglog("Adding - " & user, EventLogEntryType.Information)
            Utils.localAdmin(machineName, user)
        Next
        debuglog("GP Update", EventLogEntryType.Information)
        Utils.gpupdate()
        debuglog("Policies refreshed", EventLogEntryType.Information)

        ' Enumerate Domain Admins and store in Local Machine Registry
        Try
            Dim unames As StringCollection = adTool.GetUsersFromGroup("Domain Admins")
            Dim userList As StringBuilder = New StringBuilder
            For Each name As String In unames
                userList.Append(name)
                userList.Append(",")
            Next

            reg.addReg("HKLM#Software\asGatekeeper#CachedAdmins", userList.ToString, "REG_SZ")
        Catch ex As Exception

        End Try
        BG.RunWorkerAsync()
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        'regAdd("RA|HKLM#Software\microsoft\windows nt\currentversion\winlogon#Shell|explorer.exe|REG_SZ^")
        'Utils.WriteToEventLog("Stopped", EventLogEntryType.Information)
        'BG.CancelAsync()

        MyBase.Stop()
        MyBase.ExitCode = 0
    End Sub

    Protected Overrides Sub OnShutdown()
        'BG.CancelAsync()
        MyBase.OnShutdown()
        MyBase.ExitCode = 0
    End Sub


    Private Sub BG_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BG.DoWork
        listener()
    End Sub

    Private Sub listener()

        Dim Listener As New TcpListener(System.Net.IPAddress.Loopback, port)
        listening = True
        Listener.Start()
        Do Until listening = False
            Dim sb As New SocketAndBuffer()
            While Not Listener.Pending()
                Threading.Thread.Sleep(250)
            End While
            sb.Socket = Listener.AcceptSocket()
            Try
                sb.Socket.BeginReceive(sb.Buffer, 0, sb.Buffer.Length, SocketFlags.None, AddressOf ReceiveCallBack, sb)
            Catch ex As Exception
                'EventLog1.WriteEntry("Listener: " & ex.Message, EventLogEntryType.Error)
            End Try
        Loop
    End Sub

    Private Sub ReceiveCallBack(ByVal ar As IAsyncResult)
        'EventLog1.WriteEntry("Got Callback",EventLogEntryType.Information)
        Try
            Dim processIt As Boolean = False
            Dim sb As SocketAndBuffer = CType(ar.AsyncState, SocketAndBuffer)
            Dim numbytes As Int32 = sb.Socket.EndReceive(ar)
            If numbytes > 0 Then
                '-- Convert the buffer to a string
                Dim Receive As String = UTF8.GetString(sb.Buffer, 0, numbytes)
                ' ^ is the command delimiter
                If Receive.EndsWith("^") Then
                    Receive = Receive.Trim("^")
                    processIt = True
                End If
                commandBuffer = commandBuffer + Receive
                If (processIt) Then
                    processCommand(sb, commandBuffer)
                    commandBuffer = ""
                    processIt = False
                End If
                Array.Clear(sb.Buffer, 0, sb.Buffer.Length)
                '-- Receive again
                sb.Socket.BeginReceive(sb.Buffer, 0, sb.Buffer.Length, SocketFlags.None, AddressOf ReceiveCallBack, sb)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function getPrinters(ByVal param As String) As String
        Dim paramList As String() = param.Split("|")
        Dim retVal As String = ""
        Dim printArray As String()
        Dim PrinterList As List(Of String) = theHost.PrinterList
        If theHost.PrinterList.Count = 0 Then
            Return "NOPRINTERS"
        End If

        For Each printer In PrinterList
            printArray = printer.Split(vbTab)
            If printArray(0).StartsWith("=") Then
                If Not retVal.Contains(printArray(1)) Then
                    retVal = retVal & "=" & printArray(1) & ","
                End If
            Else
                If Not retVal.Contains(printArray(1)) Then
                    retVal = retVal & printArray(1) & ","
                End If
            End If

        Next
        'EventLog1.WriteEntry("PrinterList : " & retVal, EventLogEntryType.Information)
        Return retVal
    End Function

    Private Function reload()
        theHost = New host(My.Resources.HostsLocation, machineName, True)

        Return UTF8.GetBytes("OK")
    End Function

    Private Sub processCommand(ByVal sb As SocketAndBuffer, ByVal cb As String)
        debuglog("processCommand - " & cb, EventLogEntryType.Information)
        If cb.StartsWith("RR") Then
            sb.Buffer = regRead(cb)

        ElseIf cb.StartsWith("RA") Then
            sb.Buffer = regAdd(cb)

        ElseIf cb.StartsWith("RELOAD") Then
            sb.Buffer = reload()

        ElseIf cb.StartsWith("DEVICETYPE") Then
            sb.Buffer = UTF8.GetBytes(devicetype)

        ElseIf cb.StartsWith("GETPRINTERS") Then
            sb.Buffer = UTF8.GetBytes(getPrinters(cb))

        ElseIf cb.StartsWith("NETSTAT") Then
            sb.Buffer = UTF8.GetBytes(getNetStat)

        ElseIf cb.StartsWith("DISADMIN") Then
            sb.Buffer = UTF8.GetBytes(disadmin(cb))

        ElseIf cb.StartsWith("SETPRIV") Then
            sb.Buffer = setPriv(cb)

        ElseIf cb.StartsWith("GPUPDATE") Then
            sb.Buffer = UTF8.GetBytes(Utils.gpupdate)

        ElseIf cb.StartsWith("GETPRIV") Then
            sb.Buffer = getPriv(cb)

        ElseIf cb.StartsWith("GETDEBUG") Then
            sb.Buffer = UTF8.GetBytes(debugs)

        Else
            sb.Buffer = System.Text.Encoding.UTF8.GetBytes("What?")

        End If
        sb.Socket.Send(sb.Buffer)
        Array.Clear(sb.Buffer, 0, sb.Buffer.Length)
    End Sub

    Private Function getDNSSuffix() As String
        Dim adapters As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
        Dim adapter As NetworkInterface
        Dim retval As String = "standalone"
        For Each adapter In adapters
            Dim properties As IPInterfaceProperties = adapter.GetIPProperties()
            If properties.DnsSuffix.Equals(My.Resources.DNSSuffix) Then
                retval = "connected"
            Else
                retval = "standalone"
            End If
        Next
        Return retval
    End Function

    Private Function getNetStat() As String
        'Return getDNSSuffix
        Try
            If My.Computer.Network.IsAvailable Then
                If My.Computer.Network.Ping(My.Resources.DomainController) Then
                    Return "connected"
                Else
                    Return "standalone"
                End If
            Else
                Return "standalone"
            End If
        Catch ex As Exception
            Return "standalone"
        End Try
    End Function

    Private Function getPriv(ByVal cb As String) As Byte()
        Dim paramList As String() = cb.Split("|")
        Dim retVal As String = ""
        theHost = New host(My.Resources.HostsLocation, machineName)
        Dim userList As List(Of String) = theHost.PrivList

        EventLog1.WriteEntry("getPriv:" & cb, EventLogEntryType.Information)
        For Each usern In userList
            EventLog1.WriteEntry("privUser:" & usern, EventLogEntryType.Information)
        Next

        If userList.Contains(paramList(2).ToLower) Then
            retVal = "Privileged"
            EventLog1.WriteEntry("PrivUserFound " & paramList(2), EventLogEntryType.SuccessAudit)
        Else
            retVal = "Standard User"
            EventLog1.WriteEntry("StandardUserFound " & paramList(2), EventLogEntryType.SuccessAudit)
        End If

        Return UTF8.GetBytes(retVal)
    End Function

    Private Function disadmin(ByVal cb As String) As String
        Dim paramList As String() = cb.Split("|")
        Dim sid As String = paramList(3)
        Dim retVal As String = ""
        Utils.RemovelocalAdmin(paramList(1), paramList(2))
        Return "Done"
    End Function

    Private Function setPriv(ByVal cb As String) As Byte()
        Dim paramList As String() = cb.Split("|")
        Dim sid As String = paramList(3)
        Dim retVal As String = ""
        priv.processPriv(sid)
        Return UTF8.GetBytes("SetPriv")
    End Function

    Private Function regRead(ByVal cb As String) As Byte()
        Dim commandBits As String() = cb.Split("|")
        Dim retVal As String = reg.readReg(commandBits(1))
        debuglog("regRead() - " & commandBits(1) & " = " & retVal, EventLogEntryType.Information)
        Return UTF8.GetBytes(retVal)
    End Function

    Private Function regAdd(cb As String) As Byte()
        Dim commandBits As String() = cb.Split("|")
        'commandBits(0) = command, (1) = hive#regpath#key, (2) = value, (3) = type
        reg.addReg(commandBits(1), commandBits(2), commandBits(3))
        Return UTF8.GetBytes("OK")
    End Function


    Private Sub debuglog(ByVal message As String, et As EventLogEntryType)
        If debug Then
            EventLog1.WriteEntry(message, et)
        End If
    End Sub


End Class

''' <summary>
''' Encapsulate the socket definition and a buffer to communicate across it.
''' </summary>
Public Class SocketAndBuffer
    Public Socket As System.Net.Sockets.Socket
    Public Buffer(1023) As Byte
End Class

