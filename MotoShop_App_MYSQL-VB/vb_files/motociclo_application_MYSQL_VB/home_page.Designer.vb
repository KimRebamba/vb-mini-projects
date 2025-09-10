<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class home_page
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(home_page))
        Me.home_flow = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblOrder = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lblCart = New System.Windows.Forms.Label()
        Me.lblVoucher = New System.Windows.Forms.Label()
        Me.lblHome = New System.Windows.Forms.Label()
        Me.pbCart = New System.Windows.Forms.PictureBox()
        Me.lblLogOut = New System.Windows.Forms.Label()
        Me.lblMoto = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.home_panel = New System.Windows.Forms.Panel()
        Me.lblCountBranches = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblun = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cboBrand = New System.Windows.Forms.ComboBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.cboBranch = New System.Windows.Forms.ComboBox()
        Me.txtKeyword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.lblCountModels = New System.Windows.Forms.Label()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.home_panel.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'home_flow
        '
        Me.home_flow.AutoScroll = True
        Me.home_flow.BackColor = System.Drawing.Color.White
        Me.home_flow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.home_flow.Location = New System.Drawing.Point(0, 106)
        Me.home_flow.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.home_flow.Name = "home_flow"
        Me.home_flow.Size = New System.Drawing.Size(1075, 504)
        Me.home_flow.TabIndex = 0
        Me.home_flow.Visible = False
        '
        'lblWelcome
        '
        Me.lblWelcome.BackColor = System.Drawing.Color.Transparent
        Me.lblWelcome.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWelcome.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWelcome.ForeColor = System.Drawing.Color.White
        Me.lblWelcome.Location = New System.Drawing.Point(793, 17)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(200, 25)
        Me.lblWelcome.TabIndex = 24
        Me.lblWelcome.Text = "WELCOME, USER!"
        Me.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.Black
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold)
        Me.lblStatus.ForeColor = System.Drawing.Color.White
        Me.lblStatus.Location = New System.Drawing.Point(0, 76)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(1075, 31)
        Me.lblStatus.TabIndex = 27
        Me.lblStatus.Text = "SELECT YOUR BRANCH:"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Black
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold)
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(932, 548)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(101, 39)
        Me.btnBack.TabIndex = 28
        Me.btnBack.Text = "BACK"
        Me.btnBack.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnBack.UseVisualStyleBackColor = False
        Me.btnBack.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Red
        Me.Panel1.Controls.Add(Me.lblOrder)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.lblCart)
        Me.Panel1.Controls.Add(Me.lblVoucher)
        Me.Panel1.Controls.Add(Me.lblHome)
        Me.Panel1.Controls.Add(Me.pbCart)
        Me.Panel1.Controls.Add(Me.lblLogOut)
        Me.Panel1.Controls.Add(Me.lblMoto)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.lblWelcome)
        Me.Panel1.Location = New System.Drawing.Point(0, -5)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1075, 81)
        Me.Panel1.TabIndex = 31
        '
        'lblOrder
        '
        Me.lblOrder.AutoSize = True
        Me.lblOrder.BackColor = System.Drawing.Color.Black
        Me.lblOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrder.ForeColor = System.Drawing.Color.White
        Me.lblOrder.Location = New System.Drawing.Point(800, 43)
        Me.lblOrder.Name = "lblOrder"
        Me.lblOrder.Size = New System.Drawing.Size(87, 22)
        Me.lblOrder.TabIndex = 49
        Me.lblOrder.Text = "ORDERS"
        Me.lblOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PictureBox2
        '
        Me.PictureBox2.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureBox2.Image = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.profile_01
        Me.PictureBox2.Location = New System.Drawing.Point(1001, 11)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(65, 62)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 48
        Me.PictureBox2.TabStop = False
        '
        'lblCart
        '
        Me.lblCart.AutoSize = True
        Me.lblCart.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblCart.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCart.ForeColor = System.Drawing.Color.White
        Me.lblCart.Location = New System.Drawing.Point(627, 30)
        Me.lblCart.Name = "lblCart"
        Me.lblCart.Size = New System.Drawing.Size(90, 32)
        Me.lblCart.TabIndex = 44
        Me.lblCart.Text = "CART"
        '
        'lblVoucher
        '
        Me.lblVoucher.AutoSize = True
        Me.lblVoucher.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblVoucher.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVoucher.ForeColor = System.Drawing.Color.White
        Me.lblVoucher.Location = New System.Drawing.Point(447, 30)
        Me.lblVoucher.Name = "lblVoucher"
        Me.lblVoucher.Size = New System.Drawing.Size(173, 32)
        Me.lblVoucher.TabIndex = 47
        Me.lblVoucher.Text = "VOUCHERS"
        '
        'lblHome
        '
        Me.lblHome.AutoSize = True
        Me.lblHome.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblHome.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHome.ForeColor = System.Drawing.Color.White
        Me.lblHome.Location = New System.Drawing.Point(97, 30)
        Me.lblHome.Name = "lblHome"
        Me.lblHome.Size = New System.Drawing.Size(98, 32)
        Me.lblHome.TabIndex = 46
        Me.lblHome.Text = "HOME"
        '
        'pbCart
        '
        Me.pbCart.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbCart.Image = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.cart_01_01
        Me.pbCart.Location = New System.Drawing.Point(711, 25)
        Me.pbCart.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.pbCart.Name = "pbCart"
        Me.pbCart.Size = New System.Drawing.Size(49, 47)
        Me.pbCart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbCart.TabIndex = 45
        Me.pbCart.TabStop = False
        '
        'lblLogOut
        '
        Me.lblLogOut.AutoSize = True
        Me.lblLogOut.BackColor = System.Drawing.Color.Black
        Me.lblLogOut.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblLogOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogOut.ForeColor = System.Drawing.Color.White
        Me.lblLogOut.Location = New System.Drawing.Point(896, 43)
        Me.lblLogOut.Name = "lblLogOut"
        Me.lblLogOut.Size = New System.Drawing.Size(87, 22)
        Me.lblLogOut.TabIndex = 43
        Me.lblLogOut.Text = "LOGOUT"
        Me.lblLogOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMoto
        '
        Me.lblMoto.AutoSize = True
        Me.lblMoto.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblMoto.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoto.ForeColor = System.Drawing.Color.White
        Me.lblMoto.Location = New System.Drawing.Point(205, 30)
        Me.lblMoto.Name = "lblMoto"
        Me.lblMoto.Size = New System.Drawing.Size(231, 32)
        Me.lblMoto.TabIndex = 42
        Me.lblMoto.Text = "MOTORCYCLES"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.square_01
        Me.PictureBox1.Location = New System.Drawing.Point(11, 12)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(80, 62)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 39
        Me.PictureBox1.TabStop = False
        '
        'home_panel
        '
        Me.home_panel.BackColor = System.Drawing.Color.Transparent
        Me.home_panel.Controls.Add(Me.lblCountBranches)
        Me.home_panel.Controls.Add(Me.Label5)
        Me.home_panel.Controls.Add(Me.PictureBox4)
        Me.home_panel.Controls.Add(Me.PictureBox5)
        Me.home_panel.Controls.Add(Me.Label9)
        Me.home_panel.Controls.Add(Me.PictureBox3)
        Me.home_panel.Controls.Add(Me.Panel2)
        Me.home_panel.Controls.Add(Me.lblCountModels)
        Me.home_panel.Controls.Add(Me.PictureBox6)
        Me.home_panel.Location = New System.Drawing.Point(0, 106)
        Me.home_panel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.home_panel.Name = "home_panel"
        Me.home_panel.Size = New System.Drawing.Size(1075, 505)
        Me.home_panel.TabIndex = 0
        Me.home_panel.Visible = False
        '
        'lblCountBranches
        '
        Me.lblCountBranches.AutoSize = True
        Me.lblCountBranches.BackColor = System.Drawing.Color.Transparent
        Me.lblCountBranches.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.2!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountBranches.ForeColor = System.Drawing.Color.Black
        Me.lblCountBranches.Location = New System.Drawing.Point(126, 128)
        Me.lblCountBranches.Name = "lblCountBranches"
        Me.lblCountBranches.Size = New System.Drawing.Size(40, 42)
        Me.lblCountBranches.TabIndex = 12
        Me.lblCountBranches.Text = "1"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(128, 170)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(122, 50)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "DEALER " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "BRANCHES"
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.branch_01
        Me.PictureBox4.Location = New System.Drawing.Point(37, 128)
        Me.PictureBox4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(95, 84)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 19
        Me.PictureBox4.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox5.BackgroundImage = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.mtoor_01
        Me.PictureBox5.Image = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.mtoor_01
        Me.PictureBox5.Location = New System.Drawing.Point(265, 128)
        Me.PictureBox5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(91, 84)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 22
        Me.PictureBox5.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(353, 170)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 50)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "BRAND " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "MODELS"
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.Image = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.newestlogo
        Me.PictureBox3.Location = New System.Drawing.Point(37, 22)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(275, 88)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 17
        Me.PictureBox3.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Black
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.lblun)
        Me.Panel2.Controls.Add(Me.btnSearch)
        Me.Panel2.Controls.Add(Me.cboBrand)
        Me.Panel2.Controls.Add(Me.cboType)
        Me.Panel2.Controls.Add(Me.cboBranch)
        Me.Panel2.Controls.Add(Me.txtKeyword)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.PictureBox7)
        Me.Panel2.Location = New System.Drawing.Point(0, 295)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1075, 208)
        Me.Panel2.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(608, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 25)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "BRAND"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(429, 97)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 25)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "TYPE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(251, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 25)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "BRANCH"
        '
        'lblun
        '
        Me.lblun.AutoSize = True
        Me.lblun.BackColor = System.Drawing.Color.Transparent
        Me.lblun.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblun.ForeColor = System.Drawing.Color.White
        Me.lblun.Location = New System.Drawing.Point(35, 97)
        Me.lblun.Name = "lblun"
        Me.lblun.Size = New System.Drawing.Size(115, 25)
        Me.lblun.TabIndex = 8
        Me.lblun.Text = "KEYWORD"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.Red
        Me.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.btnSearch.Location = New System.Drawing.Point(797, 117)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(239, 49)
        Me.btnSearch.TabIndex = 7
        Me.btnSearch.Text = "SEARCH"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'cboBrand
        '
        Me.cboBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBrand.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!)
        Me.cboBrand.FormattingEnabled = True
        Me.cboBrand.Location = New System.Drawing.Point(612, 126)
        Me.cboBrand.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboBrand.Name = "cboBrand"
        Me.cboBrand.Size = New System.Drawing.Size(173, 37)
        Me.cboBrand.TabIndex = 5
        Me.cboBrand.TabStop = False
        '
        'cboType
        '
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!)
        Me.cboType.FormattingEnabled = True
        Me.cboType.Location = New System.Drawing.Point(433, 126)
        Me.cboType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(173, 37)
        Me.cboType.TabIndex = 4
        Me.cboType.TabStop = False
        '
        'cboBranch
        '
        Me.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBranch.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!)
        Me.cboBranch.FormattingEnabled = True
        Me.cboBranch.Location = New System.Drawing.Point(253, 126)
        Me.cboBranch.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cboBranch.Name = "cboBranch"
        Me.cboBranch.Size = New System.Drawing.Size(173, 37)
        Me.cboBranch.TabIndex = 3
        Me.cboBranch.TabStop = False
        '
        'txtKeyword
        '
        Me.txtKeyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKeyword.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.txtKeyword.Location = New System.Drawing.Point(37, 124)
        Me.txtKeyword.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtKeyword.MaxLength = 20
        Me.txtKeyword.Name = "txtKeyword"
        Me.txtKeyword.Size = New System.Drawing.Size(202, 34)
        Me.txtKeyword.TabIndex = 2
        Me.txtKeyword.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(217, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(651, 36)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "FIND THE RIGHT MOTORCYCLE FOR YOU!"
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = Global.motociclo_application_MYSQL_VB.My.Resources.Resources._2
        Me.PictureBox7.Location = New System.Drawing.Point(0, -329)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(1075, 536)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox7.TabIndex = 12
        Me.PictureBox7.TabStop = False
        '
        'lblCountModels
        '
        Me.lblCountModels.AutoSize = True
        Me.lblCountModels.BackColor = System.Drawing.Color.Transparent
        Me.lblCountModels.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.2!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountModels.ForeColor = System.Drawing.Color.Black
        Me.lblCountModels.Location = New System.Drawing.Point(351, 128)
        Me.lblCountModels.Name = "lblCountModels"
        Me.lblCountModels.Size = New System.Drawing.Size(40, 42)
        Me.lblCountModels.TabIndex = 20
        Me.lblCountModels.Text = "1"
        '
        'PictureBox6
        '
        Me.PictureBox6.BackgroundImage = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.Yellow_And_White_Modern_Motorcycle_Services_Facebook_Cover
        Me.PictureBox6.Image = Global.motociclo_application_MYSQL_VB.My.Resources.Resources.Yellow_And_White_Modern_Motorcycle_Services_Facebook_Cover
        Me.PictureBox6.Location = New System.Drawing.Point(40, -46)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(1038, 345)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox6.TabIndex = 23
        Me.PictureBox6.TabStop = False
        '
        'home_page
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1075, 609)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.home_panel)
        Me.Controls.Add(Me.home_flow)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "home_page"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Home Page (Customer)"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.home_panel.ResumeLayout(False)
        Me.home_panel.PerformLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents home_flow As FlowLayoutPanel
    Friend WithEvents lblWelcome As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblMoto As Label
    Friend WithEvents lblCart As Label
    Friend WithEvents lblLogOut As Label
    Friend WithEvents pbCart As PictureBox
    Friend WithEvents lblVoucher As Label
    Friend WithEvents lblHome As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents lblOrder As Label
    Friend WithEvents home_panel As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents txtKeyword As TextBox
    Friend WithEvents cboBrand As ComboBox
    Friend WithEvents cboType As ComboBox
    Friend WithEvents cboBranch As ComboBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents lblun As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents lblCountBranches As Label
    Friend WithEvents lblCountModels As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents PictureBox7 As PictureBox
End Class
