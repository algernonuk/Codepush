<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MiddlemanTest
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Outbox = New System.Windows.Forms.RichTextBox()
        Me.ClientSocket1 = New PetesControls.ClientSocket(Me.components)
        Me.BG = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout
        '
        'Outbox
        '
        Me.Outbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Outbox.Location = New System.Drawing.Point(0, 0)
        Me.Outbox.Name = "Outbox"
        Me.Outbox.Size = New System.Drawing.Size(661, 575)
        Me.Outbox.TabIndex = 0
        Me.Outbox.Text = ""
        '
        'ClientSocket1
        '
        Me.ClientSocket1.EOLChar = Global.Microsoft.VisualBasic.ChrW(10)
        Me.ClientSocket1.LineMode = true
        Me.ClientSocket1.PacketSize = 4096
        Me.ClientSocket1.SynchronizingObject = Me
        '
        'BG
        '
        '
        'MiddlemanTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(661, 575)
        Me.Controls.Add(Me.Outbox)
        Me.Name = "MiddlemanTest"
        Me.Text = "Middleman Server Echobox"
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents Outbox As System.Windows.Forms.RichTextBox
    Friend WithEvents ClientSocket1 As PetesControls.ClientSocket
    Friend WithEvents BG As System.ComponentModel.BackgroundWorker

End Class
