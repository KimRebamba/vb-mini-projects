<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Flower
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
        lblname = New Label()
        CType(pbTry, ComponentModel.ISupportInitialize).BeginInit()
        CType(pbQuit, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pbTry
        ' 
        pbTry.BackColor = Color.Transparent
        pbTry.Location = New Point(441, 267)
        pbTry.Name = "pbTry"
        pbTry.Size = New Size(135, 34)
        pbTry.TabIndex = 0
        pbTry.TabStop = False
        ' 
        ' pbQuit
        ' 
        pbQuit.BackColor = Color.Transparent
        pbQuit.Location = New Point(438, 320)
        pbQuit.Name = "pbQuit"
        pbQuit.Size = New Size(139, 36)
        pbQuit.TabIndex = 1
        pbQuit.TabStop = False
        ' 
        ' lblname
        ' 
        lblname.BackColor = Color.Transparent
        lblname.Font = New Font("Futura ICG XBold", 11.999999F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblname.ForeColor = Color.White
        lblname.Location = New Point(197, 314)
        lblname.Name = "lblname"
        lblname.Size = New Size(115, 28)
        lblname.TabIndex = 2
        lblname.Text = "asfw"
        lblname.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Flower
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Control
        BackgroundImage = My.Resources.Resources.Flowers1_01
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(607, 396)
        Controls.Add(lblname)
        Controls.Add(pbQuit)
        Controls.Add(pbTry)
        DoubleBuffered = True
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "Flower"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Flowers!"
        CType(pbTry, ComponentModel.ISupportInitialize).EndInit()
        CType(pbQuit, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pbTry As PictureBox
    Friend WithEvents pbQuit As PictureBox
    Friend WithEvents lblname As Label
End Class
