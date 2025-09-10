<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        progressBar = New ProgressBar()
        cpuTimer = New Timer(components)
        pbStatus = New PictureBox()
        CType(pbStatus, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' progressBar
        ' 
        progressBar.Location = New Point(164, 271)
        progressBar.Name = "progressBar"
        progressBar.Size = New Size(312, 34)
        progressBar.TabIndex = 0
        progressBar.Value = 50
        ' 
        ' cpuTimer
        ' 
        cpuTimer.Interval = 1000
        ' 
        ' pbStatus
        ' 
        pbStatus.BackgroundImageLayout = ImageLayout.Stretch
        pbStatus.Image = My.Resources.Resources.Neutral_01
        pbStatus.Location = New Point(158, 90)
        pbStatus.Name = "pbStatus"
        pbStatus.Size = New Size(320, 167)
        pbStatus.SizeMode = PictureBoxSizeMode.StretchImage
        pbStatus.TabIndex = 1
        pbStatus.TabStop = False
        ' 
        ' Form1
        ' 
        BackgroundImage = My.Resources.Resources.FightScreen_01
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(648, 384)
        Controls.Add(pbStatus)
        Controls.Add(progressBar)
        DoubleBuffered = True
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ARM by kim (WRESTLING!)"
        CType(pbStatus, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Private WithEvents progressBar As ProgressBar
    Private WithEvents cpuTimer As Timer
    Friend WithEvents pbStatus As PictureBox
End Class
