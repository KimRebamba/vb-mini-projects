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
        PictureBox1 = New PictureBox()
        PictureBox2 = New PictureBox()
        rbBluePen = New RadioButton()
        rbRedPen = New RadioButton()
        rbRuler = New RadioButton()
        rbPencil = New RadioButton()
        Panel1 = New Panel()
        rbNotebook = New RadioButton()
        Panel2 = New Panel()
        rbNone = New RadioButton()
        rbBag = New RadioButton()
        rbFolder = New RadioButton()
        rbEraser = New RadioButton()
        lblTitle = New Label()
        lblPaymentTitle = New Label()
        btnConfirm = New Button()
        lblTotalPrice = New Label()
        tbPayment = New TextBox()
        btnEnter = New Button()
        Panel3 = New Panel()
        Panel4 = New Panel()
        Panel5 = New Panel()
        Panel6 = New Panel()
        Panel7 = New Panel()
        Panel8 = New Panel()
        Panel9 = New Panel()
        btnReset = New Button()
        lbCart = New ListBox()
        Label1 = New Label()
        PictureBox3 = New PictureBox()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        Panel4.SuspendLayout()
        Panel5.SuspendLayout()
        Panel6.SuspendLayout()
        Panel7.SuspendLayout()
        Panel8.SuspendLayout()
        Panel9.SuspendLayout()
        CType(PictureBox3, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = My.Resources.Resources.Sign_01
        PictureBox1.Location = New Point(25, -8)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(588, 190)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = My.Resources.Resources.itemslol_01
        PictureBox2.Location = New Point(18, 214)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(601, 504)
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.TabIndex = 1
        PictureBox2.TabStop = False
        ' 
        ' rbBluePen
        ' 
        rbBluePen.AutoSize = True
        rbBluePen.BackColor = Color.Transparent
        rbBluePen.Cursor = Cursors.Hand
        rbBluePen.Location = New Point(-2, 2)
        rbBluePen.Name = "rbBluePen"
        rbBluePen.Size = New Size(17, 16)
        rbBluePen.TabIndex = 2
        rbBluePen.UseVisualStyleBackColor = False
        ' 
        ' rbRedPen
        ' 
        rbRedPen.AutoSize = True
        rbRedPen.Cursor = Cursors.Hand
        rbRedPen.Location = New Point(-2, 1)
        rbRedPen.Name = "rbRedPen"
        rbRedPen.Size = New Size(17, 16)
        rbRedPen.TabIndex = 3
        rbRedPen.UseVisualStyleBackColor = True
        ' 
        ' rbRuler
        ' 
        rbRuler.AutoSize = True
        rbRuler.Cursor = Cursors.Hand
        rbRuler.Location = New Point(-2, 1)
        rbRuler.Name = "rbRuler"
        rbRuler.Size = New Size(17, 16)
        rbRuler.TabIndex = 4
        rbRuler.UseVisualStyleBackColor = True
        ' 
        ' rbPencil
        ' 
        rbPencil.AutoSize = True
        rbPencil.Cursor = Cursors.Hand
        rbPencil.Location = New Point(-2, 1)
        rbPencil.Name = "rbPencil"
        rbPencil.Size = New Size(17, 16)
        rbPencil.TabIndex = 6
        rbPencil.UseVisualStyleBackColor = True
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.DarkGreen
        Panel1.BorderStyle = BorderStyle.Fixed3D
        Panel1.Controls.Add(rbBluePen)
        Panel1.ForeColor = Color.DarkGoldenrod
        Panel1.Location = New Point(153, 293)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(17, 24)
        Panel1.TabIndex = 10
        ' 
        ' rbNotebook
        ' 
        rbNotebook.AutoSize = True
        rbNotebook.Cursor = Cursors.Hand
        rbNotebook.Location = New Point(-2, 1)
        rbNotebook.Name = "rbNotebook"
        rbNotebook.Size = New Size(17, 16)
        rbNotebook.TabIndex = 7
        rbNotebook.UseVisualStyleBackColor = True
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.DarkGreen
        Panel2.BorderStyle = BorderStyle.Fixed3D
        Panel2.Controls.Add(rbNone)
        Panel2.ForeColor = Color.DarkGoldenrod
        Panel2.Location = New Point(461, 323)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(16, 25)
        Panel2.TabIndex = 11
        ' 
        ' rbNone
        ' 
        rbNone.AutoSize = True
        rbNone.BackColor = Color.Transparent
        rbNone.Cursor = Cursors.Hand
        rbNone.Location = New Point(-2, 2)
        rbNone.Name = "rbNone"
        rbNone.Size = New Size(17, 16)
        rbNone.TabIndex = 2
        rbNone.UseVisualStyleBackColor = False
        ' 
        ' rbBag
        ' 
        rbBag.AutoSize = True
        rbBag.Cursor = Cursors.Hand
        rbBag.Location = New Point(-2, 2)
        rbBag.Name = "rbBag"
        rbBag.Size = New Size(17, 16)
        rbBag.TabIndex = 7
        rbBag.UseVisualStyleBackColor = True
        ' 
        ' rbFolder
        ' 
        rbFolder.AutoSize = True
        rbFolder.Cursor = Cursors.Hand
        rbFolder.Location = New Point(-2, 2)
        rbFolder.Name = "rbFolder"
        rbFolder.Size = New Size(17, 16)
        rbFolder.TabIndex = 3
        rbFolder.UseVisualStyleBackColor = True
        ' 
        ' rbEraser
        ' 
        rbEraser.AutoSize = True
        rbEraser.Cursor = Cursors.Hand
        rbEraser.Location = New Point(-2, 2)
        rbEraser.Name = "rbEraser"
        rbEraser.Size = New Size(17, 16)
        rbEraser.TabIndex = 4
        rbEraser.UseVisualStyleBackColor = True
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Futura ICG XBold", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(18, 745)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(184, 50)
        lblTitle.TabIndex = 12
        lblTitle.Text = "TO PAY:"
        ' 
        ' lblPaymentTitle
        ' 
        lblPaymentTitle.AutoSize = True
        lblPaymentTitle.Font = New Font("Futura ICG XBold", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblPaymentTitle.ForeColor = Color.White
        lblPaymentTitle.Location = New Point(18, 795)
        lblPaymentTitle.Name = "lblPaymentTitle"
        lblPaymentTitle.Size = New Size(226, 50)
        lblPaymentTitle.TabIndex = 13
        lblPaymentTitle.Text = "PAYMENT:"
        ' 
        ' btnConfirm
        ' 
        btnConfirm.BackColor = Color.White
        btnConfirm.Font = New Font("Futura ICG XBold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnConfirm.Location = New Point(453, 744)
        btnConfirm.Name = "btnConfirm"
        btnConfirm.Size = New Size(166, 59)
        btnConfirm.TabIndex = 14
        btnConfirm.Text = "CONFIRM"
        btnConfirm.UseVisualStyleBackColor = False
        ' 
        ' lblTotalPrice
        ' 
        lblTotalPrice.BackColor = Color.White
        lblTotalPrice.BorderStyle = BorderStyle.Fixed3D
        lblTotalPrice.Font = New Font("Futura ICG XBold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTotalPrice.Location = New Point(246, 754)
        lblTotalPrice.Name = "lblTotalPrice"
        lblTotalPrice.Size = New Size(191, 33)
        lblTotalPrice.TabIndex = 15
        lblTotalPrice.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tbPayment
        ' 
        tbPayment.Font = New Font("Futura ICG XBold", 11.999999F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        tbPayment.Location = New Point(246, 803)
        tbPayment.Name = "tbPayment"
        tbPayment.Size = New Size(191, 37)
        tbPayment.TabIndex = 16
        ' 
        ' btnEnter
        ' 
        btnEnter.BackColor = Color.White
        btnEnter.Font = New Font("Futura ICG XBold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnEnter.Location = New Point(477, 744)
        btnEnter.Name = "btnEnter"
        btnEnter.Size = New Size(119, 59)
        btnEnter.TabIndex = 17
        btnEnter.Text = "ENTER"
        btnEnter.UseVisualStyleBackColor = False
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.DarkGreen
        Panel3.BorderStyle = BorderStyle.Fixed3D
        Panel3.Controls.Add(rbRedPen)
        Panel3.ForeColor = Color.DarkGoldenrod
        Panel3.Location = New Point(154, 380)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(17, 24)
        Panel3.TabIndex = 11
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.DarkGreen
        Panel4.BorderStyle = BorderStyle.Fixed3D
        Panel4.Controls.Add(rbRuler)
        Panel4.ForeColor = Color.DarkGoldenrod
        Panel4.Location = New Point(153, 465)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(17, 24)
        Panel4.TabIndex = 11
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.DarkGreen
        Panel5.BorderStyle = BorderStyle.Fixed3D
        Panel5.Controls.Add(rbNotebook)
        Panel5.ForeColor = Color.DarkGoldenrod
        Panel5.Location = New Point(154, 551)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(17, 24)
        Panel5.TabIndex = 11
        ' 
        ' Panel6
        ' 
        Panel6.BackColor = Color.DarkGreen
        Panel6.BorderStyle = BorderStyle.Fixed3D
        Panel6.Controls.Add(rbPencil)
        Panel6.ForeColor = Color.DarkGoldenrod
        Panel6.Location = New Point(154, 633)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(17, 24)
        Panel6.TabIndex = 12
        ' 
        ' Panel7
        ' 
        Panel7.BackColor = Color.DarkGreen
        Panel7.BorderStyle = BorderStyle.Fixed3D
        Panel7.Controls.Add(rbFolder)
        Panel7.ForeColor = Color.DarkGoldenrod
        Panel7.Location = New Point(461, 399)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(16, 25)
        Panel7.TabIndex = 12
        ' 
        ' Panel8
        ' 
        Panel8.BackColor = Color.DarkGreen
        Panel8.BorderStyle = BorderStyle.Fixed3D
        Panel8.Controls.Add(rbEraser)
        Panel8.ForeColor = Color.DarkGoldenrod
        Panel8.Location = New Point(460, 505)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(16, 25)
        Panel8.TabIndex = 12
        ' 
        ' Panel9
        ' 
        Panel9.BackColor = Color.DarkGreen
        Panel9.BorderStyle = BorderStyle.Fixed3D
        Panel9.Controls.Add(rbBag)
        Panel9.ForeColor = Color.DarkGoldenrod
        Panel9.Location = New Point(461, 606)
        Panel9.Name = "Panel9"
        Panel9.Size = New Size(16, 25)
        Panel9.TabIndex = 12
        ' 
        ' btnReset
        ' 
        btnReset.BackColor = Color.White
        btnReset.Font = New Font("Futura ICG XBold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnReset.Location = New Point(477, 812)
        btnReset.Name = "btnReset"
        btnReset.Size = New Size(119, 59)
        btnReset.TabIndex = 18
        btnReset.Text = "RESET"
        btnReset.UseVisualStyleBackColor = False
        ' 
        ' lbCart
        ' 
        lbCart.BackColor = Color.MintCream
        lbCart.BorderStyle = BorderStyle.None
        lbCart.Font = New Font("Futura Std Book", 10.7999992F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbCart.FormattingEnabled = True
        lbCart.Location = New Point(667, 274)
        lbCart.Name = "lbCart"
        lbCart.Size = New Size(239, 506)
        lbCart.TabIndex = 19
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(646, 223)
        Label1.Name = "Label1"
        Label1.Size = New Size(0, 20)
        Label1.TabIndex = 20
        ' 
        ' PictureBox3
        ' 
        PictureBox3.Image = My.Resources.Resources.itemscart_01
        PictureBox3.Location = New Point(651, 214)
        PictureBox3.Name = "PictureBox3"
        PictureBox3.Size = New Size(270, 663)
        PictureBox3.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox3.TabIndex = 21
        PictureBox3.TabStop = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.DarkOliveGreen
        ClientSize = New Size(949, 921)
        Controls.Add(lbCart)
        Controls.Add(PictureBox3)
        Controls.Add(Label1)
        Controls.Add(btnReset)
        Controls.Add(Panel9)
        Controls.Add(Panel8)
        Controls.Add(Panel7)
        Controls.Add(Panel6)
        Controls.Add(Panel5)
        Controls.Add(Panel4)
        Controls.Add(Panel3)
        Controls.Add(btnEnter)
        Controls.Add(tbPayment)
        Controls.Add(lblTotalPrice)
        Controls.Add(btnConfirm)
        Controls.Add(lblPaymentTitle)
        Controls.Add(lblTitle)
        Controls.Add(Panel2)
        Controls.Add(PictureBox1)
        Controls.Add(Panel1)
        Controls.Add(PictureBox2)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "Form1"
        Text = "Kim's Merchandise Store"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        Panel8.ResumeLayout(False)
        Panel8.PerformLayout()
        Panel9.ResumeLayout(False)
        Panel9.PerformLayout()
        CType(PictureBox3, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents rbBluePen As RadioButton
    Friend WithEvents rbRedPen As RadioButton
    Friend WithEvents rbRuler As RadioButton
    Friend WithEvents rbPencil As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents rbNotebook As RadioButton
    Friend WithEvents Panel2 As Panel
    Friend WithEvents rbBag As RadioButton
    Friend WithEvents rbNone As RadioButton
    Friend WithEvents rbFolder As RadioButton
    Friend WithEvents rbEraser As RadioButton
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblPaymentTitle As Label
    Friend WithEvents btnConfirm As Button
    Friend WithEvents lblTotalPrice As Label
    Friend WithEvents tbPayment As TextBox
    Friend WithEvents btnEnter As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents btnReset As Button
    Friend WithEvents lbCart As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox3 As PictureBox

End Class
