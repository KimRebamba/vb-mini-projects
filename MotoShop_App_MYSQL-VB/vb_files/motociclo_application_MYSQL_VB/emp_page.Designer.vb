<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Partial Class emp_page
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

    ' Form controls
    Private WithEvents btnPayroll As System.Windows.Forms.Button
    Private WithEvents btnViewPayslip As System.Windows.Forms.Button
    Private WithEvents btnMonthSelection As System.Windows.Forms.Button

    Public Sub New()
        InitializeComponent()
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblLogout = New System.Windows.Forms.Label()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.lblEmployee = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnAccountManagement = New System.Windows.Forms.Button()
        Me.btnPayroll = New System.Windows.Forms.Button()
        Me.btnProcessReturns = New System.Windows.Forms.Button()
        Me.btnViewOrders = New System.Windows.Forms.Button()
        Me.btnDashboard = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnViewPayslip = New System.Windows.Forms.Button()
        Me.btnMonthSelection = New System.Windows.Forms.Button()
        Me.lblInstructions = New System.Windows.Forms.Label()
        Me.dgvOrders = New System.Windows.Forms.DataGridView()
        Me.lblOrders = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblCompletedOrders = New System.Windows.Forms.Label()
        Me.lblPendingReturns = New System.Windows.Forms.Label()
        Me.lblTotalOrders = New System.Windows.Forms.Label()
        Me.lblStats = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dgvOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Controls.Add(Me.lblLogout)
        Me.Panel1.Controls.Add(Me.lblWelcome)
        Me.Panel1.Controls.Add(Me.lblEmployee)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1200, 80)
        Me.Panel1.TabIndex = 0
        '
        'lblLogout
        '
        Me.lblLogout.AutoSize = True
        Me.lblLogout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblLogout.Font = New System.Drawing.Font("Futura-Medium", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblLogout.ForeColor = System.Drawing.Color.White
        Me.lblLogout.Location = New System.Drawing.Point(1000, 30)
        Me.lblLogout.Name = "lblLogout"
        Me.lblLogout.Size = New System.Drawing.Size(103, 25)
        Me.lblLogout.TabIndex = 2
        Me.lblLogout.Text = "LOGOUT"
        '
        'lblWelcome
        '
        Me.lblWelcome.AutoSize = True
        Me.lblWelcome.Font = New System.Drawing.Font("Futura-Medium", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblWelcome.ForeColor = System.Drawing.Color.White
        Me.lblWelcome.Location = New System.Drawing.Point(200, 25)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(234, 29)
        Me.lblWelcome.TabIndex = 1
        Me.lblWelcome.Text = "WELCOME, "
        '
        'lblEmployee
        '
        Me.lblEmployee.AutoSize = True
        Me.lblEmployee.Font = New System.Drawing.Font("Futura_Book-Bold", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblEmployee.ForeColor = System.Drawing.Color.White
        Me.lblEmployee.Location = New System.Drawing.Point(20, 25)
        Me.lblEmployee.Name = "lblEmployee"
        Me.lblEmployee.Size = New System.Drawing.Size(170, 31)
        Me.lblEmployee.TabIndex = 0
        Me.lblEmployee.Text = "EMPLOYEE"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(64, 64, 64)
        Me.Panel2.Controls.Add(Me.btnAccountManagement)
        Me.Panel2.Controls.Add(Me.btnPayroll)
        Me.Panel2.Controls.Add(Me.btnProcessReturns)
        Me.Panel2.Controls.Add(Me.btnViewOrders)
        Me.Panel2.Controls.Add(Me.btnDashboard)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 80)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 520)
        Me.Panel2.TabIndex = 1
        '
        'btnAccountManagement
        '
        Me.btnAccountManagement.BackColor = System.Drawing.Color.Black
        Me.btnAccountManagement.FlatAppearance.BorderSize = 0
        Me.btnAccountManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAccountManagement.Font = New System.Drawing.Font("Futura-Medium", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnAccountManagement.ForeColor = System.Drawing.Color.White
        Me.btnAccountManagement.Location = New System.Drawing.Point(0, 300)
        Me.btnAccountManagement.Name = "btnAccountManagement"
        Me.btnAccountManagement.Size = New System.Drawing.Size(200, 60)
        Me.btnAccountManagement.TabIndex = 4
        Me.btnAccountManagement.Text = "ACCOUNT MANAGEMENT"
        Me.btnAccountManagement.UseVisualStyleBackColor = False
        '
        'btnProcessReturns
        '
        Me.btnProcessReturns.BackColor = System.Drawing.Color.Black
        Me.btnProcessReturns.FlatAppearance.BorderSize = 0
        Me.btnProcessReturns.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProcessReturns.Font = New System.Drawing.Font("Futura-Medium", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnProcessReturns.ForeColor = System.Drawing.Color.White
        Me.btnProcessReturns.Location = New System.Drawing.Point(0, 240)
        Me.btnProcessReturns.Name = "btnProcessReturns"
        Me.btnProcessReturns.Size = New System.Drawing.Size(200, 60)
        Me.btnProcessReturns.TabIndex = 3
        Me.btnProcessReturns.Text = "PROCESS RETURNS"
        Me.btnProcessReturns.UseVisualStyleBackColor = False
        '
        'btnPayroll
        '
        Me.btnPayroll.BackColor = System.Drawing.Color.Black
        Me.btnPayroll.FlatAppearance.BorderSize = 0
        Me.btnPayroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPayroll.Font = New System.Drawing.Font("Futura-Medium", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnPayroll.ForeColor = System.Drawing.Color.White
        Me.btnPayroll.Location = New System.Drawing.Point(0, 180)
        Me.btnPayroll.Name = "btnPayroll"
        Me.btnPayroll.Size = New System.Drawing.Size(200, 60)
        Me.btnPayroll.TabIndex = 2
        Me.btnPayroll.Text = "PAYROLL"
        Me.btnPayroll.UseVisualStyleBackColor = False
        '
        'btnViewOrders
        '
        Me.btnViewOrders.BackColor = System.Drawing.Color.Black
        Me.btnViewOrders.FlatAppearance.BorderSize = 0
        Me.btnViewOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewOrders.Font = New System.Drawing.Font("Futura-Medium", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewOrders.ForeColor = System.Drawing.Color.White
        Me.btnViewOrders.Location = New System.Drawing.Point(0, 120)
        Me.btnViewOrders.Name = "btnViewOrders"
        Me.btnViewOrders.Size = New System.Drawing.Size(200, 60)
        Me.btnViewOrders.TabIndex = 1
        Me.btnViewOrders.Text = "VIEW ORDERS"
        Me.btnViewOrders.UseVisualStyleBackColor = False
        '
        'btnDashboard
        '
        Me.btnDashboard.BackColor = System.Drawing.Color.Black
        Me.btnDashboard.FlatAppearance.BorderSize = 0
        Me.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDashboard.Font = New System.Drawing.Font("Futura-Medium", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnDashboard.ForeColor = System.Drawing.Color.White
        Me.btnDashboard.Location = New System.Drawing.Point(0, 60)
        Me.btnDashboard.Name = "btnDashboard"
        Me.btnDashboard.Size = New System.Drawing.Size(200, 60)
        Me.btnDashboard.TabIndex = 0
        Me.btnDashboard.Text = "DASHBOARD"
        Me.btnDashboard.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.lblTitle)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(200, 80)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1000, 60)
        Me.Panel3.TabIndex = 2
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Futura_Book-Bold", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.Black
        Me.lblTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(196, 31)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "DASHBOARD"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel4.Controls.Add(Me.btnViewPayslip)
        Me.Panel4.Controls.Add(Me.btnMonthSelection)
        Me.Panel4.Controls.Add(Me.lblInstructions)
        Me.Panel4.Controls.Add(Me.dgvOrders)
        Me.Panel4.Controls.Add(Me.lblOrders)
        Me.Panel4.Location = New System.Drawing.Point(220, 160)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(960, 300)
        Me.Panel4.TabIndex = 3
        '
        'dgvOrders
        '
        Me.dgvOrders.AllowUserToAddRows = False
        Me.dgvOrders.AllowUserToDeleteRows = False
        Me.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None
        Me.dgvOrders.BackgroundColor = System.Drawing.Color.White
        Me.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOrders.Location = New System.Drawing.Point(20, 80)
        Me.dgvOrders.Name = "dgvOrders"
        Me.dgvOrders.ReadOnly = True
        Me.dgvOrders.RowHeadersWidth = 51
        Me.dgvOrders.RowTemplate.Height = 24
        Me.dgvOrders.Size = New System.Drawing.Size(920, 200)
        Me.dgvOrders.TabIndex = 1
        Me.dgvOrders.ScrollBars = System.Windows.Forms.ScrollBars.Both
        '
        'lblInstructions
        '
        Me.lblInstructions.AutoSize = True
        Me.lblInstructions.Font = New System.Drawing.Font("Futura-Medium", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstructions.ForeColor = System.Drawing.Color.Gray
        Me.lblInstructions.Location = New System.Drawing.Point(15, 50)
        Me.lblInstructions.Name = "lblInstructions"
        Me.lblInstructions.Size = New System.Drawing.Size(400, 18)
        Me.lblInstructions.TabIndex = 2
        Me.lblInstructions.Text = "EDITING DONE IN VIEW ORDERS BUTTON!"
        '
        'lblOrders
        '
        Me.lblOrders.AutoSize = True
        Me.lblOrders.Font = New System.Drawing.Font("Futura-Medium", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrders.ForeColor = System.Drawing.Color.Black
        Me.lblOrders.Location = New System.Drawing.Point(15, 15)
        Me.lblOrders.Name = "lblOrders"
        Me.lblOrders.Size = New System.Drawing.Size(234, 29)
        Me.lblOrders.TabIndex = 0
        Me.lblOrders.Text = "RECENT ORDERS"
        '
        'btnViewPayslip
        '
        Me.btnViewPayslip.BackColor = System.Drawing.Color.FromArgb(0, 122, 204)
        Me.btnViewPayslip.FlatAppearance.BorderSize = 0
        Me.btnViewPayslip.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewPayslip.Font = New System.Drawing.Font("Futura-Medium", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewPayslip.ForeColor = System.Drawing.Color.White
        Me.btnViewPayslip.Location = New System.Drawing.Point(800, 15)
        Me.btnViewPayslip.Name = "btnViewPayslip"
        Me.btnViewPayslip.Size = New System.Drawing.Size(140, 35)
        Me.btnViewPayslip.TabIndex = 3
        Me.btnViewPayslip.Text = "VIEW PAYSLIP"
        Me.btnViewPayslip.UseVisualStyleBackColor = False
        '
        'btnMonthSelection
        '
        Me.btnMonthSelection.BackColor = System.Drawing.Color.FromArgb(0, 150, 136)
        Me.btnMonthSelection.FlatAppearance.BorderSize = 0
        Me.btnMonthSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMonthSelection.Font = New System.Drawing.Font("Futura-Medium", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnMonthSelection.ForeColor = System.Drawing.Color.White
        Me.btnMonthSelection.Location = New System.Drawing.Point(650, 15)
        Me.btnMonthSelection.Name = "btnMonthSelection"
        Me.btnMonthSelection.Size = New System.Drawing.Size(140, 35)
        Me.btnMonthSelection.TabIndex = 4
        Me.btnMonthSelection.Text = "SELECT MONTH"
        Me.btnMonthSelection.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.Controls.Add(Me.lblCompletedOrders)
        Me.Panel5.Controls.Add(Me.lblPendingReturns)
        Me.Panel5.Controls.Add(Me.lblTotalOrders)
        Me.Panel5.Controls.Add(Me.lblStats)
        Me.Panel5.Location = New System.Drawing.Point(220, 480)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(960, 100)
        Me.Panel5.TabIndex = 4
        '
        'lblCompletedOrders
        '
        Me.lblCompletedOrders.AutoSize = True
        Me.lblCompletedOrders.Font = New System.Drawing.Font("Futura-Medium", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompletedOrders.ForeColor = System.Drawing.Color.Black
        Me.lblCompletedOrders.Location = New System.Drawing.Point(400, 50)
        Me.lblCompletedOrders.Name = "lblCompletedOrders"
        Me.lblCompletedOrders.Size = New System.Drawing.Size(165, 20)
        Me.lblCompletedOrders.TabIndex = 3
        Me.lblCompletedOrders.Text = "Completed Orders: 0"
        '
        'lblPendingReturns
        '
        Me.lblPendingReturns.AutoSize = True
        Me.lblPendingReturns.Font = New System.Drawing.Font("Futura-Medium", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPendingReturns.ForeColor = System.Drawing.Color.Black
        Me.lblPendingReturns.Location = New System.Drawing.Point(200, 50)
        Me.lblPendingReturns.Name = "lblPendingReturns"
        Me.lblPendingReturns.Size = New System.Drawing.Size(152, 20)
        Me.lblPendingReturns.TabIndex = 2
        Me.lblPendingReturns.Text = "Pending Returns: 0"
        '
        'lblTotalOrders
        '
        Me.lblTotalOrders.AutoSize = True
        Me.lblTotalOrders.Font = New System.Drawing.Font("Futura-Medium", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalOrders.ForeColor = System.Drawing.Color.Black
        Me.lblTotalOrders.Location = New System.Drawing.Point(20, 50)
        Me.lblTotalOrders.Name = "lblTotalOrders"
        Me.lblTotalOrders.Size = New System.Drawing.Size(122, 20)
        Me.lblTotalOrders.TabIndex = 1
        Me.lblTotalOrders.Text = "Total Orders: 0"
        '
        'lblStats
        '
        Me.lblStats.AutoSize = True
        Me.lblStats.Font = New System.Drawing.Font("Futura_Book-Bold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblStats.ForeColor = System.Drawing.Color.Black
        Me.lblStats.Location = New System.Drawing.Point(15, 15)
        Me.lblStats.Name = "lblStats"
        Me.lblStats.Size = New System.Drawing.Size(142, 25)
        Me.lblStats.TabIndex = 0
        Me.lblStats.Text = "STATISTICS"
        '      
        'emp_page
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 600)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "emp_page"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Employee Dashboard"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.dgvOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents Panel1 As Panel
    Public WithEvents lblLogout As Label
    Public WithEvents lblWelcome As Label
    Public WithEvents lblEmployee As Label
    Public WithEvents Panel2 As Panel
    Public WithEvents btnAccountManagement As Button
    Public WithEvents btnProcessReturns As Button
    Public WithEvents btnViewOrders As Button
    Public WithEvents btnDashboard As Button
    Public WithEvents Panel3 As Panel
    Public WithEvents lblTitle As Label
    Public WithEvents Panel4 As Panel
    Public WithEvents lblInstructions As Label
    Public WithEvents dgvOrders As DataGridView
    Public WithEvents lblOrders As Label
    Public WithEvents Panel5 As Panel
    Public WithEvents lblCompletedOrders As Label
    Public WithEvents lblPendingReturns As Label
    Public WithEvents lblTotalOrders As Label
    Public WithEvents lblStats As Label
End Class
