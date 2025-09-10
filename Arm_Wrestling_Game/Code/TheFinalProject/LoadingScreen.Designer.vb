<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoadingScreen
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
        pbStart = New PictureBox()
        pbQuit = New PictureBox()
        pbEaster = New PictureBox()
        btnTip = New Button()
        CType(pbStart, ComponentModel.ISupportInitialize).BeginInit()
        CType(pbQuit, ComponentModel.ISupportInitialize).BeginInit()
        CType(pbEaster, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pbStart
        ' 
        pbStart.BackColor = Color.Transparent
        pbStart.Location = New Point(247, 188)
        pbStart.Name = "pbStart"
        pbStart.Size = New Size(110, 48)
        pbStart.TabIndex = 0
        pbStart.TabStop = False
        ' 
        ' pbQuit
        ' 
        pbQuit.BackColor = Color.Transparent
        pbQuit.Location = New Point(260, 254)
        pbQuit.Name = "pbQuit"
        pbQuit.Size = New Size(87, 50)
        pbQuit.TabIndex = 1
        pbQuit.TabStop = False
        ' 
        ' pbEaster
        ' 
        pbEaster.BackColor = Color.Transparent
        pbEaster.Location = New Point(233, 65)
        pbEaster.Name = "pbEaster"
        pbEaster.Size = New Size(142, 64)
        pbEaster.TabIndex = 2
        pbEaster.TabStop = False
        ' 
        ' btnTip
        ' 
        btnTip.BackColor = SystemColors.ActiveCaptionText
        btnTip.Cursor = Cursors.Hand
        btnTip.FlatStyle = FlatStyle.Flat
        btnTip.ForeColor = SystemColors.ControlLightLight
        btnTip.Location = New Point(528, 340)
        btnTip.Name = "btnTip"
        btnTip.Size = New Size(55, 29)
        btnTip.TabIndex = 3
        btnTip.Text = "TIP?"
        btnTip.UseVisualStyleBackColor = False
        ' 
        ' LoadingScreen
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.LoadingScreenNeutral_01
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(607, 396)
        Controls.Add(btnTip)
        Controls.Add(pbEaster)
        Controls.Add(pbQuit)
        Controls.Add(pbStart)
        DoubleBuffered = True
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "LoadingScreen"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ARM by kim"
        CType(pbStart, ComponentModel.ISupportInitialize).EndInit()
        CType(pbQuit, ComponentModel.ISupportInitialize).EndInit()
        CType(pbEaster, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pbStart As PictureBox
    Friend WithEvents pbQuit As PictureBox
    Friend WithEvents pbEaster As PictureBox
    Friend WithEvents btnTip As Button
End Class
