<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class account_page
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(account_page))
        Me.txtusername = New System.Windows.Forms.TextBox()
        Me.lblun = New System.Windows.Forms.Label()
        Me.lblpw = New System.Windows.Forms.Label()
        Me.txtpassword = New System.Windows.Forms.TextBox()
        Me.btnsubmit = New System.Windows.Forms.Button()
        Me.btnsignup = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnPWsee = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtusername
        '
        Me.txtusername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtusername.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.txtusername.Location = New System.Drawing.Point(23, 49)
        Me.txtusername.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtusername.MaxLength = 20
        Me.txtusername.Name = "txtusername"
        Me.txtusername.Size = New System.Drawing.Size(298, 34)
        Me.txtusername.TabIndex = 1
        '
        'lblun
        '
        Me.lblun.AutoSize = True
        Me.lblun.BackColor = System.Drawing.Color.Transparent
        Me.lblun.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblun.ForeColor = System.Drawing.Color.Black
        Me.lblun.Location = New System.Drawing.Point(19, 22)
        Me.lblun.Name = "lblun"
        Me.lblun.Size = New System.Drawing.Size(124, 25)
        Me.lblun.TabIndex = 2
        Me.lblun.Text = "USERNAME"
        '
        'lblpw
        '
        Me.lblpw.AutoSize = True
        Me.lblpw.BackColor = System.Drawing.Color.Transparent
        Me.lblpw.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.lblpw.ForeColor = System.Drawing.Color.Black
        Me.lblpw.Location = New System.Drawing.Point(19, 101)
        Me.lblpw.Name = "lblpw"
        Me.lblpw.Size = New System.Drawing.Size(130, 25)
        Me.lblpw.TabIndex = 4
        Me.lblpw.Text = "PASSWORD"
        '
        'txtpassword
        '
        Me.txtpassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpassword.Location = New System.Drawing.Point(23, 128)
        Me.txtpassword.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtpassword.MaxLength = 40
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.Size = New System.Drawing.Size(298, 34)
        Me.txtpassword.TabIndex = 3
        '
        'btnsubmit
        '
        Me.btnsubmit.BackColor = System.Drawing.Color.Red
        Me.btnsubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsubmit.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold)
        Me.btnsubmit.ForeColor = System.Drawing.Color.Black
        Me.btnsubmit.Location = New System.Drawing.Point(24, 183)
        Me.btnsubmit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnsubmit.Name = "btnsubmit"
        Me.btnsubmit.Size = New System.Drawing.Size(423, 49)
        Me.btnsubmit.TabIndex = 6
        Me.btnsubmit.Text = "LOG IN"
        Me.btnsubmit.UseVisualStyleBackColor = False
        '
        'btnsignup
        '
        Me.btnsignup.BackColor = System.Drawing.Color.Silver
        Me.btnsignup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsignup.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold)
        Me.btnsignup.ForeColor = System.Drawing.Color.Black
        Me.btnsignup.Location = New System.Drawing.Point(24, 44)
        Me.btnsignup.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnsignup.Name = "btnsignup"
        Me.btnsignup.Size = New System.Drawing.Size(423, 42)
        Me.btnsignup.TabIndex = 7
        Me.btnsignup.Text = "SIGN UP"
        Me.btnsignup.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnsubmit)
        Me.Panel1.Controls.Add(Me.btnPWsee)
        Me.Panel1.Controls.Add(Me.txtusername)
        Me.Panel1.Controls.Add(Me.lblun)
        Me.Panel1.Controls.Add(Me.txtpassword)
        Me.Panel1.Controls.Add(Me.lblpw)
        Me.Panel1.Location = New System.Drawing.Point(577, 103)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(469, 251)
        Me.Panel1.TabIndex = 9
        '
        'btnPWsee
        '
        Me.btnPWsee.BackColor = System.Drawing.Color.Black
        Me.btnPWsee.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPWsee.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPWsee.ForeColor = System.Drawing.Color.White
        Me.btnPWsee.Location = New System.Drawing.Point(335, 128)
        Me.btnPWsee.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnPWsee.Name = "btnPWsee"
        Me.btnPWsee.Size = New System.Drawing.Size(111, 37)
        Me.btnPWsee.TabIndex = 10
        Me.btnPWsee.Text = "SHOW"
        Me.btnPWsee.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(20, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(217, 25)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Don't have an account?"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.btnsignup)
        Me.Panel2.Location = New System.Drawing.Point(577, 377)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(469, 100)
        Me.Panel2.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(35, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(151, 25)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "WELCOME TO"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(61, 530)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(235, 44)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "VB - MYSQL APPLICATION" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "AGUILA, REBAMBA"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.newestlogo
        Me.PictureBox1.Location = New System.Drawing.Point(40, 48)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(401, 128)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'lblDescription
        '
        Me.lblDescription.BackColor = System.Drawing.Color.Transparent
        Me.lblDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblDescription.ForeColor = System.Drawing.Color.White
        Me.lblDescription.Location = New System.Drawing.Point(684, 512)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(347, 62)
        Me.lblDescription.TabIndex = 14
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(428, 530)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(220, 22)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "MESSAGE-OF-THE-DAY:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'account_page
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.ducatiii
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1075, 609)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "account_page"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Account Login/Sign-up"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtusername As TextBox
    Friend WithEvents lblun As Label
    Friend WithEvents lblpw As Label
    Friend WithEvents txtpassword As TextBox
    Friend WithEvents btnsubmit As Button
    Friend WithEvents btnsignup As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnPWsee As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblDescription As Label
    Friend WithEvents Label3 As Label
End Class
