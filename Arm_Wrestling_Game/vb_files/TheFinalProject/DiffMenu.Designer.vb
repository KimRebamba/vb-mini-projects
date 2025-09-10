<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiffMenu
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
        pbEasy = New PictureBox()
        pbMed = New PictureBox()
        pbHard = New PictureBox()
        CType(pbEasy, ComponentModel.ISupportInitialize).BeginInit()
        CType(pbMed, ComponentModel.ISupportInitialize).BeginInit()
        CType(pbHard, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pbEasy
        ' 
        pbEasy.BackColor = Color.Transparent
        pbEasy.Location = New Point(254, 127)
        pbEasy.Name = "pbEasy"
        pbEasy.Size = New Size(103, 47)
        pbEasy.TabIndex = 0
        pbEasy.TabStop = False
        ' 
        ' pbMed
        ' 
        pbMed.BackColor = Color.Transparent
        pbMed.Location = New Point(235, 206)
        pbMed.Name = "pbMed"
        pbMed.Size = New Size(137, 47)
        pbMed.TabIndex = 1
        pbMed.TabStop = False
        ' 
        ' pbHard
        ' 
        pbHard.BackColor = Color.Transparent
        pbHard.Location = New Point(254, 284)
        pbHard.Name = "pbHard"
        pbHard.Size = New Size(103, 47)
        pbHard.TabIndex = 2
        pbHard.TabStop = False
        ' 
        ' DiffMenu
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.DiffNeutral_01
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(607, 396)
        Controls.Add(pbHard)
        Controls.Add(pbMed)
        Controls.Add(pbEasy)
        DoubleBuffered = True
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "DiffMenu"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Difficulty...."
        CType(pbEasy, ComponentModel.ISupportInitialize).EndInit()
        CType(pbMed, ComponentModel.ISupportInitialize).EndInit()
        CType(pbHard, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pbEasy As PictureBox
    Friend WithEvents pbMed As PictureBox
    Friend WithEvents pbHard As PictureBox
End Class
