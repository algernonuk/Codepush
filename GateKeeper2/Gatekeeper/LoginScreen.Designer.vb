﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginScreen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginScreen))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Login = New System.Windows.Forms.Button()
        Me.Logout = New System.Windows.Forms.Button()
        Me.AUPBox = New System.Windows.Forms.RichTextBox()
        Me.LogoBox = New System.Windows.Forms.PictureBox()
        Me.ClientSocket1 = New PetesControls.ClientSocket(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.BallStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusBox = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Timeout = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.LogoBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Login, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Logout, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.AUPBox, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LogoBox, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(632, 468)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(319, 447)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Label1"
        '
        'Login
        '
        Me.Login.Dock = System.Windows.Forms.DockStyle.Right
        Me.Login.Location = New System.Drawing.Point(554, 414)
        Me.Login.Name = "Login"
        Me.Login.Size = New System.Drawing.Size(75, 30)
        Me.Login.TabIndex = 0
        Me.Login.Text = "&Accept"
        Me.Login.UseVisualStyleBackColor = True
        '
        'Logout
        '
        Me.Logout.Dock = System.Windows.Forms.DockStyle.Left
        Me.Logout.Location = New System.Drawing.Point(3, 414)
        Me.Logout.Name = "Logout"
        Me.Logout.Size = New System.Drawing.Size(75, 30)
        Me.Logout.TabIndex = 1
        Me.Logout.Text = "&Decline"
        Me.Logout.UseVisualStyleBackColor = True
        '
        'AUPBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.AUPBox, 2)
        Me.AUPBox.DetectUrls = False
        Me.AUPBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AUPBox.Location = New System.Drawing.Point(3, 126)
        Me.AUPBox.Name = "AUPBox"
        Me.AUPBox.ReadOnly = True
        Me.AUPBox.Size = New System.Drawing.Size(626, 282)
        Me.AUPBox.TabIndex = 2
        Me.AUPBox.Text = "Welcome"
        '
        'LogoBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.LogoBox, 2)
        Me.LogoBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LogoBox.Image = Global.Gatekeeper.My.Resources.Resources.banner
        Me.LogoBox.InitialImage = Nothing
        Me.LogoBox.Location = New System.Drawing.Point(3, 3)
        Me.LogoBox.Name = "LogoBox"
        Me.LogoBox.Size = New System.Drawing.Size(626, 117)
        Me.LogoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoBox.TabIndex = 3
        Me.LogoBox.TabStop = False
        '
        'ClientSocket1
        '
        Me.ClientSocket1.EOLChar = Global.Microsoft.VisualBasic.ChrW(10)
        Me.ClientSocket1.LineMode = True
        Me.ClientSocket1.PacketSize = 4096
        Me.ClientSocket1.SynchronizingObject = Me
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BallStatus, Me.StatusBox, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 446)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(632, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'BallStatus
        '
        Me.BallStatus.Image = Global.Gatekeeper.My.Resources.Resources.amberball1
        Me.BallStatus.Name = "BallStatus"
        Me.BallStatus.Size = New System.Drawing.Size(16, 17)
        '
        'StatusBox
        '
        Me.StatusBox.Name = "StatusBox"
        Me.StatusBox.Size = New System.Drawing.Size(25, 17)
        Me.StatusBox.Text = "......"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(545, 17)
        Me.ToolStripStatusLabel2.Spring = True
        Me.ToolStripStatusLabel2.Text = "V1.5.1b"
        Me.ToolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(23, 23)
        '
        'Timeout
        '
        Me.Timeout.Interval = 3
        '
        'LoginScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 468)
        Me.ControlBox = False
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginScreen"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ashby School"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.LogoBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(false)
        Me.StatusStrip1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Login As System.Windows.Forms.Button
    Friend WithEvents Logout As System.Windows.Forms.Button
    Friend WithEvents AUPBox As System.Windows.Forms.RichTextBox
    Friend WithEvents LogoBox As System.Windows.Forms.PictureBox
    Friend WithEvents ClientSocket1 As PetesControls.ClientSocket
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Timeout As System.Windows.Forms.Timer
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BallStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusBox As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel

End Class
