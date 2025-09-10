<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AfterMenu
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
        pbClaim = New PictureBox()
        CType(pbClaim, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pbClaim
        ' 
        pbClaim.BackColor = Color.Transparent
        pbClaim.Location = New Point(202, 247)
        pbClaim.Name = "pbClaim"
        pbClaim.Size = New Size(201, 49)
        pbClaim.SizeMode = PictureBoxSizeMode.StretchImage
        pbClaim.TabIndex = 0
        pbClaim.TabStop = False
        ' 
        ' AfterMenu
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.YOUWONNeutral_01
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(607, 396)
        Controls.Add(pbClaim)
        DoubleBuffered = True
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "AfterMenu"
        StartPosition = FormStartPosition.CenterScreen
        Text = "YOU WIN!"
        CType(pbClaim, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pbClaim As PictureBox
End Class
