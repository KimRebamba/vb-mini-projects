<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        btnEasy = New Button()
        lblTITLE = New Label()
        btnMedium = New Button()
        btnHard = New Button()
        btnExtreme = New Button()
        Label1 = New Label()
        lblInstructions = New Label()
        Button1 = New Button()
        SuspendLayout()
        ' 
        ' btnEasy
        ' 
        btnEasy.BackColor = Color.White
        btnEasy.FlatStyle = FlatStyle.Flat
        btnEasy.Location = New Point(25, 125)
        btnEasy.Name = "btnEasy"
        btnEasy.Size = New Size(148, 31)
        btnEasy.TabIndex = 0
        btnEasy.Text = "EASY"
        btnEasy.UseVisualStyleBackColor = False
        ' 
        ' lblTITLE
        ' 
        lblTITLE.BackColor = Color.MintCream
        lblTITLE.BorderStyle = BorderStyle.FixedSingle
        lblTITLE.Cursor = Cursors.Hand
        lblTITLE.Font = New Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTITLE.Location = New Point(25, 26)
        lblTITLE.Name = "lblTITLE"
        lblTITLE.Size = New Size(403, 53)
        lblTITLE.TabIndex = 2
        lblTITLE.Text = "DRAW THE FLAG"
        lblTITLE.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnMedium
        ' 
        btnMedium.BackColor = Color.White
        btnMedium.FlatStyle = FlatStyle.Flat
        btnMedium.Location = New Point(25, 162)
        btnMedium.Name = "btnMedium"
        btnMedium.Size = New Size(148, 34)
        btnMedium.TabIndex = 3
        btnMedium.Text = "MEDIUM"
        btnMedium.UseVisualStyleBackColor = False
        ' 
        ' btnHard
        ' 
        btnHard.BackColor = Color.White
        btnHard.FlatStyle = FlatStyle.Flat
        btnHard.Location = New Point(25, 202)
        btnHard.Name = "btnHard"
        btnHard.Size = New Size(148, 35)
        btnHard.TabIndex = 4
        btnHard.Text = "HARD"
        btnHard.UseVisualStyleBackColor = False
        ' 
        ' btnExtreme
        ' 
        btnExtreme.BackColor = Color.White
        btnExtreme.FlatStyle = FlatStyle.Flat
        btnExtreme.Location = New Point(25, 243)
        btnExtreme.Name = "btnExtreme"
        btnExtreme.Size = New Size(148, 32)
        btnExtreme.TabIndex = 5
        btnExtreme.Text = "EXTREME"
        btnExtreme.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.White
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(25, 91)
        Label1.Name = "Label1"
        Label1.Size = New Size(148, 20)
        Label1.TabIndex = 6
        Label1.Text = "SELECT A DIFFICULTY"
        ' 
        ' lblInstructions
        ' 
        lblInstructions.BackColor = Color.MintCream
        lblInstructions.BorderStyle = BorderStyle.FixedSingle
        lblInstructions.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblInstructions.Location = New Point(179, 91)
        lblInstructions.Name = "lblInstructions"
        lblInstructions.Size = New Size(249, 224)
        lblInstructions.TabIndex = 7
        lblInstructions.Text = "Instructions:" & vbCrLf & "1) Select your Difficulty" & vbCrLf & "2) Draw the Flag Shown" & vbCrLf & "3) Have Fun with the Chaotic Mess!" & vbCrLf & vbCrLf
        lblInstructions.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.White
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Location = New Point(25, 281)
        Button1.Name = "Button1"
        Button1.Size = New Size(148, 34)
        Button1.TabIndex = 8
        Button1.Text = "IMPOSSIBLE"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Black
        ClientSize = New Size(452, 342)
        Controls.Add(Button1)
        Controls.Add(lblInstructions)
        Controls.Add(Label1)
        Controls.Add(btnExtreme)
        Controls.Add(btnHard)
        Controls.Add(btnMedium)
        Controls.Add(lblTITLE)
        Controls.Add(btnEasy)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Draw The Flag by Kim"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnEasy As Button
    Friend WithEvents lblTITLE As Label
    Friend WithEvents btnMedium As Button
    Friend WithEvents btnHard As Button
    Friend WithEvents btnExtreme As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lblInstructions As Label
    Friend WithEvents Button1 As Button

End Class
