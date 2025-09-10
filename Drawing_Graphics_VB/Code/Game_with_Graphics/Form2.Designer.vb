<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Label1 = New Label()
        pbFlag = New PictureBox()
        Label2 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Label3 = New Label()
        btnRed = New Button()
        btnBlack = New Button()
        Button6 = New Button()
        Button7 = New Button()
        btnYellow = New Button()
        btnWhite = New Button()
        Label4 = New Label()
        Button4 = New Button()
        Button5 = New Button()
        Label5 = New Label()
        Label6 = New Label()
        pbCanvas = New PictureBox()
        btnTriangle = New Button()
        CType(pbFlag, ComponentModel.ISupportInitialize).BeginInit()
        CType(pbCanvas, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BorderStyle = BorderStyle.FixedSingle
        Label1.Location = New Point(9, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(57, 22)
        Label1.TabIndex = 0
        Label1.Text = "DRAW:"
        ' 
        ' pbFlag
        ' 
        pbFlag.BorderStyle = BorderStyle.FixedSingle
        pbFlag.Image = My.Resources.Resources.china
        pbFlag.Location = New Point(9, 40)
        pbFlag.Name = "pbFlag"
        pbFlag.Size = New Size(189, 122)
        pbFlag.SizeMode = PictureBoxSizeMode.StretchImage
        pbFlag.TabIndex = 1
        pbFlag.TabStop = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BorderStyle = BorderStyle.FixedSingle
        Label2.Location = New Point(12, 175)
        Label2.Name = "Label2"
        Label2.Size = New Size(58, 22)
        Label2.TabIndex = 2
        Label2.Text = "TOOLS:"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(9, 207)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 35)
        Button1.TabIndex = 3
        Button1.Text = "LINE"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(9, 249)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 35)
        Button2.TabIndex = 4
        Button2.Text = "CIRCLE"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(9, 295)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 35)
        Button3.TabIndex = 5
        Button3.Text = "REC"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BorderStyle = BorderStyle.FixedSingle
        Label3.Location = New Point(12, 345)
        Label3.Name = "Label3"
        Label3.Size = New Size(68, 22)
        Label3.TabIndex = 6
        Label3.Text = "COLORS:"
        ' 
        ' btnRed
        ' 
        btnRed.Location = New Point(12, 380)
        btnRed.Name = "btnRed"
        btnRed.Size = New Size(75, 35)
        btnRed.TabIndex = 7
        btnRed.Text = "RED"
        btnRed.UseVisualStyleBackColor = True
        ' 
        ' btnBlack
        ' 
        btnBlack.Location = New Point(93, 380)
        btnBlack.Name = "btnBlack"
        btnBlack.Size = New Size(75, 35)
        btnBlack.TabIndex = 8
        btnBlack.Text = "BLACK"
        btnBlack.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(93, 250)
        Button6.Name = "Button6"
        Button6.Size = New Size(156, 35)
        Button6.TabIndex = 9
        Button6.Text = "FILL CIRCLE"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Button7
        ' 
        Button7.Location = New Point(93, 295)
        Button7.Name = "Button7"
        Button7.Size = New Size(156, 35)
        Button7.TabIndex = 10
        Button7.Text = "FILL REC"
        Button7.UseVisualStyleBackColor = True
        ' 
        ' btnYellow
        ' 
        btnYellow.Location = New Point(12, 421)
        btnYellow.Name = "btnYellow"
        btnYellow.Size = New Size(75, 35)
        btnYellow.TabIndex = 11
        btnYellow.Text = "YELLOW"
        btnYellow.UseVisualStyleBackColor = True
        ' 
        ' btnWhite
        ' 
        btnWhite.Location = New Point(93, 421)
        btnWhite.Name = "btnWhite"
        btnWhite.Size = New Size(75, 35)
        btnWhite.TabIndex = 12
        btnWhite.Text = "WHITE"
        btnWhite.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BorderStyle = BorderStyle.FixedSingle
        Label4.Location = New Point(269, 9)
        Label4.Name = "Label4"
        Label4.Size = New Size(97, 22)
        Label4.TabIndex = 14
        Label4.Text = "DRAW HERE:"
        ' 
        ' Button4
        ' 
        Button4.BackColor = SystemColors.ButtonShadow
        Button4.Location = New Point(174, 508)
        Button4.Name = "Button4"
        Button4.Size = New Size(75, 35)
        Button4.TabIndex = 15
        Button4.Text = "CLEAR"
        Button4.UseVisualStyleBackColor = False
        ' 
        ' Button5
        ' 
        Button5.BackColor = SystemColors.ActiveCaption
        Button5.Location = New Point(174, 549)
        Button5.Name = "Button5"
        Button5.Size = New Size(75, 35)
        Button5.TabIndex = 16
        Button5.Text = "HOME"
        Button5.UseVisualStyleBackColor = False
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.BorderStyle = BorderStyle.FixedSingle
        Label5.Location = New Point(12, 468)
        Label5.Name = "Label5"
        Label5.Size = New Size(115, 22)
        Label5.TabIndex = 17
        Label5.Text = "INSTRUCTIONS:"
        ' 
        ' Label6
        ' 
        Label6.BorderStyle = BorderStyle.FixedSingle
        Label6.Location = New Point(12, 508)
        Label6.Name = "Label6"
        Label6.Size = New Size(156, 150)
        Label6.TabIndex = 18
        Label6.Text = "To place a shape or line:" & vbCrLf & vbCrLf & "1) Click to place" & vbCrLf & "    1st Point" & vbCrLf & "2) Click to place" & vbCrLf & "    2nd Point"
        ' 
        ' pbCanvas
        ' 
        pbCanvas.BorderStyle = BorderStyle.FixedSingle
        pbCanvas.Location = New Point(269, 40)
        pbCanvas.Name = "pbCanvas"
        pbCanvas.Size = New Size(922, 618)
        pbCanvas.TabIndex = 19
        pbCanvas.TabStop = False
        ' 
        ' btnTriangle
        ' 
        btnTriangle.Location = New Point(93, 207)
        btnTriangle.Name = "btnTriangle"
        btnTriangle.Size = New Size(156, 35)
        btnTriangle.TabIndex = 20
        btnTriangle.Text = "FILL TRIANGLE"
        btnTriangle.UseVisualStyleBackColor = True
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1207, 673)
        Controls.Add(btnTriangle)
        Controls.Add(pbCanvas)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Button5)
        Controls.Add(Button4)
        Controls.Add(Label4)
        Controls.Add(btnWhite)
        Controls.Add(btnYellow)
        Controls.Add(Button7)
        Controls.Add(Button6)
        Controls.Add(btnBlack)
        Controls.Add(btnRed)
        Controls.Add(Label3)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label2)
        Controls.Add(pbFlag)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "Form2"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Drawing..."
        CType(pbFlag, ComponentModel.ISupportInitialize).EndInit()
        CType(pbCanvas, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents pbFlag As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents btnRed As Button
    Friend WithEvents btnBlack As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents btnYellow As Button
    Friend WithEvents btnWhite As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents pbCanvas As PictureBox
    Friend WithEvents btnTriangle As Button
End Class
