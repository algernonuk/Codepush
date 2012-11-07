Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.ServiceProcess

Public Class ProjectInstaller

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent

    End Sub

    Private Sub ServiceInstaller1_AfterInstall(sender As System.Object, e As System.Configuration.Install.InstallEventArgs) Handles ServiceInstaller1.AfterInstall
        Dim controller As New ServiceController("MiddlemanService")
        Dim appName As String = "Login"
        Dim logName = "ASLoginRecord"

        If Not EventLog.Exists(logName) Then
            System.Diagnostics.EventLog.CreateEventSource(appName, logName)
            Dim el As EventLog = New EventLog()
            el.Log = logName
            el.MaximumKilobytes = 1024
            el.ModifyOverflowPolicy(OverflowAction.OverwriteOlder, 90)
            el.Close()
        End If
        controller.Start()

    End Sub
End Class
