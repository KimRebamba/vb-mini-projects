<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class account_signup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(account_signup))
        Me.btnlogin = New System.Windows.Forms.Button()
        Me.txtpassword = New System.Windows.Forms.TextBox()
        Me.lblun = New System.Windows.Forms.Label()
        Me.txtusername = New System.Windows.Forms.TextBox()
        Me.txtemail = New System.Windows.Forms.TextBox()
        Me.txtaddress = New System.Windows.Forms.TextBox()
        Me.txtphone = New System.Windows.Forms.TextBox()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.lblvalid = New System.Windows.Forms.Label()
        Me.lblpw = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblemail1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.txtFN = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtLN = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnlogin
        '
        Me.btnlogin.BackColor = System.Drawing.Color.White
        Me.btnlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnlogin.Font = New System.Drawing.Font("Futura-Medium", 13.8!, System.Drawing.FontStyle.Bold)
        Me.btnlogin.ForeColor = System.Drawing.Color.Black
        Me.btnlogin.Location = New System.Drawing.Point(408, 480)
        Me.btnlogin.Name = "btnlogin"
        Me.btnlogin.Size = New System.Drawing.Size(129, 42)
        Me.btnlogin.TabIndex = 8
        Me.btnlogin.Text = "CANCEL"
        Me.btnlogin.UseVisualStyleBackColor = False
        '
        'txtpassword
        '
        Me.txtpassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpassword.Font = New System.Drawing.Font("Futura-Medium", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpassword.Location = New System.Drawing.Point(19, 135)
        Me.txtpassword.MaxLength = 40
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.Size = New System.Drawing.Size(254, 37)
        Me.txtpassword.TabIndex = 12
        '
        'lblun
        '
        Me.lblun.AutoSize = True
        Me.lblun.BackColor = System.Drawing.Color.Transparent
        Me.lblun.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.lblun.ForeColor = System.Drawing.Color.Black
        Me.lblun.Location = New System.Drawing.Point(16, 23)
        Me.lblun.Name = "lblun"
        Me.lblun.Size = New System.Drawing.Size(115, 24)
        Me.lblun.TabIndex = 11
        Me.lblun.Text = "USERNAME"
        '
        'txtusername
        '
        Me.txtusername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtusername.Font = New System.Drawing.Font("Futura-Medium", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtusername.Location = New System.Drawing.Point(19, 50)
        Me.txtusername.MaxLength = 20
        Me.txtusername.Name = "txtusername"
        Me.txtusername.Size = New System.Drawing.Size(254, 37)
        Me.txtusername.TabIndex = 10
        '
        'txtemail
        '
        Me.txtemail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtemail.Font = New System.Drawing.Font("Futura-Medium", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtemail.Location = New System.Drawing.Point(18, 216)
        Me.txtemail.MaxLength = 100
        Me.txtemail.Name = "txtemail"
        Me.txtemail.Size = New System.Drawing.Size(254, 37)
        Me.txtemail.TabIndex = 15
        '
        'txtaddress
        '
        Me.txtaddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtaddress.Font = New System.Drawing.Font("Futura-Medium", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaddress.Location = New System.Drawing.Point(19, 301)
        Me.txtaddress.MaxLength = 255
        Me.txtaddress.Multiline = True
        Me.txtaddress.Name = "txtaddress"
        Me.txtaddress.Size = New System.Drawing.Size(518, 78)
        Me.txtaddress.TabIndex = 17
        '
        'txtphone
        '
        Me.txtphone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtphone.Font = New System.Drawing.Font("Futura-Medium", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtphone.Location = New System.Drawing.Point(18, 427)
        Me.txtphone.MaxLength = 11
        Me.txtphone.Name = "txtphone"
        Me.txtphone.Size = New System.Drawing.Size(254, 37)
        Me.txtphone.TabIndex = 19
        '
        'btnSubmit
        '
        Me.btnSubmit.BackColor = System.Drawing.Color.Red
        Me.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSubmit.Font = New System.Drawing.Font("Futura-Medium", 13.8!, System.Drawing.FontStyle.Bold)
        Me.btnSubmit.ForeColor = System.Drawing.Color.Black
        Me.btnSubmit.Location = New System.Drawing.Point(18, 480)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(376, 42)
        Me.btnSubmit.TabIndex = 21
        Me.btnSubmit.Text = "SUBMIT"
        Me.btnSubmit.UseVisualStyleBackColor = False
        '
        'lblvalid
        '
        Me.lblvalid.BackColor = System.Drawing.Color.White
        Me.lblvalid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblvalid.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.lblvalid.ForeColor = System.Drawing.Color.Red
        Me.lblvalid.Location = New System.Drawing.Point(288, 427)
        Me.lblvalid.Name = "lblvalid"
        Me.lblvalid.Size = New System.Drawing.Size(250, 37)
        Me.lblvalid.TabIndex = 22
        Me.lblvalid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblpw
        '
        Me.lblpw.BackColor = System.Drawing.Color.White
        Me.lblpw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblpw.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.lblpw.ForeColor = System.Drawing.Color.Red
        Me.lblpw.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblpw.Location = New System.Drawing.Point(292, 136)
        Me.lblpw.Name = "lblpw"
        Me.lblpw.Size = New System.Drawing.Size(102, 37)
        Me.lblpw.TabIndex = 23
        Me.lblpw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Futura-Medium", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(902, 371)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 25)
        Me.Label3.TabIndex = 26
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label3.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(16, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(118, 24)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "PASSWORD"
        '
        'lblemail1
        '
        Me.lblemail1.BackColor = System.Drawing.Color.White
        Me.lblemail1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblemail1.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.lblemail1.ForeColor = System.Drawing.Color.Red
        Me.lblemail1.Location = New System.Drawing.Point(292, 216)
        Me.lblemail1.Name = "lblemail1"
        Me.lblemail1.Size = New System.Drawing.Size(203, 38)
        Me.lblemail1.TabIndex = 29
        Me.lblemail1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(288, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 24)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "CHECKER:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(16, 189)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 24)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "EMAIL"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(14, 274)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 24)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "ADDRESS"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(14, 397)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 24)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "PHONE #"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(284, 397)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 24)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "CHECKER:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(288, 189)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(106, 24)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "CHECKER:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtLN)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtFN)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.btnlogin)
        Me.Panel1.Controls.Add(Me.btnSubmit)
        Me.Panel1.Controls.Add(Me.txtpassword)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtusername)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.lblun)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtemail)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtaddress)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtphone)
        Me.Panel1.Controls.Add(Me.lblemail1)
        Me.Panel1.Controls.Add(Me.lblvalid)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.lblpw)
        Me.Panel1.Location = New System.Drawing.Point(457, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(552, 548)
        Me.Panel1.TabIndex = 36
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Futura Bk", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(46, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(156, 26)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "WHY CREATE A"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.square_01
        Me.PictureBox1.Location = New System.Drawing.Point(39, 84)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(75, 65)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 38
        Me.PictureBox1.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Futura Bk", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(111, 108)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(121, 26)
        Me.Label10.TabIndex = 39
        Me.Label10.Text = "ACCOUNT?"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("Futura Bk", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(51, 188)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 26)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "Benefits:"
        '
        'lblDescription
        '
        Me.lblDescription.BackColor = System.Drawing.Color.Transparent
        Me.lblDescription.Font = New System.Drawing.Font("a_FuturaOrto", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblDescription.ForeColor = System.Drawing.Color.Black
        Me.lblDescription.Location = New System.Drawing.Point(51, 224)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(346, 61)
        Me.lblDescription.TabIndex = 40
        '
        'txtFN
        '
        Me.txtFN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFN.Font = New System.Drawing.Font("Futura-Medium", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFN.Location = New System.Drawing.Point(291, 50)
        Me.txtFN.MaxLength = 20
        Me.txtFN.Name = "txtFN"
        Me.txtFN.Size = New System.Drawing.Size(103, 37)
        Me.txtFN.TabIndex = 36
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(288, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(35, 24)
        Me.Label12.TabIndex = 37
        Me.Label12.Text = "FN"
        '
        'txtLN
        '
        Me.txtLN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLN.Font = New System.Drawing.Font("Futura-Medium", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLN.Location = New System.Drawing.Point(412, 50)
        Me.txtLN.MaxLength = 20
        Me.txtLN.Name = "txtLN"
        Me.txtLN.Size = New System.Drawing.Size(103, 37)
        Me.txtLN.TabIndex = 38
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("a_FuturaOrtoLt", 12.0!)
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(409, 23)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 24)
        Me.Label13.TabIndex = 39
        Me.Label13.Text = "LN"
        '
        'account_signup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1074, 603)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBox1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "account_signup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Account Sign-up"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnlogin As Button
    Friend WithEvents txtpassword As TextBox
    Friend WithEvents lblun As Label
    Friend WithEvents txtusername As TextBox
    Friend WithEvents txtemail As TextBox
    Friend WithEvents txtaddress As TextBox
    Friend WithEvents txtphone As TextBox
    Friend WithEvents btnSubmit As Button
    Friend WithEvents lblvalid As Label
    Friend WithEvents lblpw As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblemail1 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents lblDescription As Label
    Friend WithEvents txtLN As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtFN As TextBox
    Friend WithEvents Label12 As Label
End Class
