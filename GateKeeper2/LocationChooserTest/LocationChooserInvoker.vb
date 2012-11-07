Imports LocationChooser

Public Class LocationChooserInvoker
    Private WithEvents Dim lc As LocationChooser.LocationChooser = New LocationChooser.LocationChooser

    Private Sub gotStuff(ByVal plist As List(Of String), ByVal loc As String) Handles lc.sendPrinters
        lc.deleteAllRemotePrinters

        For Each item In plist
            lc.addPrinter(item)
        Next
        Application.exit
    End Sub

Private Sub Form1_Load( sender As System.Object,  e As System.EventArgs) Handles MyBase.Load
        
        lc.Show
        lc.setDefault("Main Site")
End Sub
End Class
