<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AfterMenuLose
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
        pbTry = New PictureBox()
        pbQuit = New PictureBox()
        CType(pbTry, ComponentModel.ISupportInitialize).BeginInit()
        CType(pbQuit, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pbTry
        ' 
        pbTry.BackColor = Color.Transparent
        pbTry.Location = New Point(87, 238)
        pbTry.Name = "pbTry"
        pbTry.Size = New Size(180, 47)
        pbTry.TabIndex = 0
        pbTry.TabStop = False
        ' 
        ' pbQuit
        ' 
        pbQuit.BackColor = Color.Transparent
        pbQuit.Location = New Point(343, 238)
        pbQuit.Name = "pbQuit"
        pbQuit.Size = New Size(180, 47)
        pbQuit.TabIndex = 1
        pbQuit.TabStop = False
        ' 
        ' AfterMenuLose
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.YOULOSENeutral_01
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(607, 396)
        Controls.Add(pbQuit)
        Controls.Add(pbTry)
        DoubleBuffered = True
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "AfterMenuLose"
        StartPosition = FormStartPosition.CenterScreen
        Text = "YOU LOSE!"
        CType(pbTry, ComponentModel.ISupportInitialize).EndInit()
        CType(pbQuit, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pbTry As PictureBox
    Friend WithEvents pbQuit As PictureBox
End Class
