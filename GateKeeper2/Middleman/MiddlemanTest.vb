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

Public Class MiddlemanTest
    Private reg As RegControl = New regControl
    Private Delegate Sub SetTextCallback(s As String)
    Private Delegate Sub DoRefreshCallback()

    Private Const port As Integer = 27429
    Dim listening As Boolean = True
    Dim commandBuffer As String
    Private ReadOnly Property UTF8() As Encoding
        Get
            Return System.Text.Encoding.UTF8
        End Get
    End Property

    Private Sub MiddlemanTest_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        BG.RunWorkerAsync()
    End Sub

    Private Sub listener()
        Me.DoRefresh()
        Dim Listener As New TcpListener(System.Net.IPAddress.Loopback, port)
        listening = True
        Listener.Start()
        Do Until listening = False
            Me.DoRefresh()
            Dim sb As New SocketAndBuffer()
            sb.Socket = Listener.AcceptSocket()
            Try
                sb.Socket.BeginReceive(sb.Buffer, 0, sb.Buffer.Length, SocketFlags.None, AddressOf ReceiveCallBack, sb)
            Catch ex As Exception
                Utils.WriteToEventLog("Listener: " & ex.Message, EventLogEntryType.Error)
            End Try
        Loop
    End Sub

    Private Sub ReceiveCallBack(ByVal ar As IAsyncResult)
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
        Dim theHost As host
        Dim retVal As String = ""
        Dim printArray As String()
        theHost = New host(My.Resources.HostsLocation, paramList(1))
        Dim PrinterList As List(Of String) = theHost.PrinterList

        For Each printer In PrinterList
            printArray = printer.Split(vbTab)
            If printArray(0).StartsWith("=") Then
                retVal = retVal & "=" & printArray(1) & ","
            Else
                retVal = retVal & printArray(1) & ","
            End If

        Next
        Me.AppendText(vbCrLf & "PrinterList : " & retVal & vbCrLf)
        Return retVal
    End Function

    Private Sub processCommand(ByVal sb As SocketAndBuffer, ByVal cb As String)
        Me.AppendText(vbCrLf & "Rec: " & cb)

        If cb.StartsWith("RR") Then
            sb.Buffer = regRead(cb)

        ElseIf cb.StartsWith("RA") Then
            sb.Buffer = regAdd(cb)

        ElseIf cb.StartsWith("GETPRINTERS") Then
            sb.Buffer = UTF8.GetBytes(getPrinters(cb))

        ElseIf cb.StartsWith("NETSTAT") Then
            If My.Computer.Network.IsAvailable Then
                sb.Buffer = UTF8.GetBytes("connected")
            Else
                sb.Buffer = UTF8.GetBytes("standalone")
            End If
        Else
            sb.Buffer = System.Text.Encoding.UTF8.GetBytes("OK")

        End If
            sb.Socket.Send(sb.Buffer)
            Array.Clear(sb.Buffer, 0, sb.Buffer.Length)
    End Sub

    Private Function regRead(ByVal cb As String) As Byte()
        Me.AppendText(vbCrLf & "regRead: " & cb & vbCrLf)
        Dim commandBits As String() = cb.Split("|")
        Dim retVal As String = reg.readReg(commandBits(1))
        Me.AppendText("Reg: " & retVal & vbCrLf)
        'placeholder 
        Return UTF8.GetBytes(retVal)
    End Function

    Private Function regAdd(cb As String) As Byte()
        Dim commandBits As String() = cb.Split("|")
        'commandBits(0) = command, (1) = hive#regpath#key, (2) = value, (3) = type
        Me.AppendText(vbCrLf & "Regadd: " & cb & vbCrLf)
        'reg.addReg(commandBits(1), commandBits(2), commandBits(3))
        Return UTF8.GetBytes("OK")
    End Function 

    Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BG.DoWork
        Me.AppendText("Starting work" & vbCrLf)
        listener()
    End Sub

    Private Sub DoRefresh()
        If Me.InvokeRequired Then
            Dim d As New DoRefreshCallback(AddressOf DoRefresh)
            Me.Invoke(d, New Object() {})
        Else
            Me.Refresh()
        End If
    End Sub

    Private Sub AppendText(ByVal [text] As String)
        If Me.Outbox.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf AppendText)
            Me.Invoke(d, New Object() {[text]})
        Else
            Me.Outbox.AppendText([text])
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
