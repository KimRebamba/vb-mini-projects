Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing

Public Class PayslipForm
    Inherits Form

    Private lblTitle As Label
    Private lblEmployeeName As Label
    Private lblEmployeeID As Label
    Private lblPosition As Label
    Private lblBranch As Label
    Private lblPayDate As Label
    Private lblPayPeriod As Label
    Private lblNumMonths As Label
    Private lblBasicSalary As Label
    Private lblAllowances As Label
    Private lblDeductions As Label
    Private lblNetPay As Label
    Private lblStatus As Label
    Private btnPrint As Button
    Private btnClose As Button
    Private txtPayslipDetails As TextBox

    Private payslipData As MySqlDataReader

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Payslip Details"
        Me.Size = New Size(600, 750)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        lblTitle = New Label()
        lblTitle.Text = "PAYSLIP"
        lblTitle.Font = New Font("Futura_Book-Bold", 18, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(0, 122, 204)
        lblTitle.Location = New Point(20, 20)
        lblTitle.Size = New Size(200, 30)
        lblTitle.TextAlign = ContentAlignment.MiddleCenter

        lblEmployeeName = New Label()
        lblEmployeeName.Font = New Font("Futura-Medium", 12, FontStyle.Bold)
        lblEmployeeName.Location = New Point(20, 70)
        lblEmployeeName.Size = New Size(300, 20)

        lblEmployeeID = New Label()
        lblEmployeeID.Font = New Font("Futura-Medium", 10)
        lblEmployeeID.Location = New Point(20, 95)
        lblEmployeeID.Size = New Size(300, 20)

        lblPosition = New Label()
        lblPosition.Font = New Font("Futura-Medium", 10)
        lblPosition.Location = New Point(20, 120)
        lblPosition.Size = New Size(300, 20)

        lblBranch = New Label()
        lblBranch.Font = New Font("Futura-Medium", 10)
        lblBranch.Location = New Point(20, 145)
        lblBranch.Size = New Size(300, 20)

        lblPayDate = New Label()
        lblPayDate.Font = New Font("Futura-Medium", 10)
        lblPayDate.Location = New Point(20, 170)
        lblPayDate.Size = New Size(300, 20)

        lblPayPeriod = New Label()
        lblPayPeriod.Font = New Font("Futura-Medium", 10)
        lblPayPeriod.Location = New Point(20, 195)
        lblPayPeriod.Size = New Size(300, 20)

        lblNumMonths = New Label()
        lblNumMonths.Font = New Font("Futura-Medium", 10)
        lblNumMonths.Location = New Point(20, 220)
        lblNumMonths.Size = New Size(300, 20)

        lblStatus = New Label()
        lblStatus.Font = New Font("Futura-Medium", 12, FontStyle.Bold)
        lblStatus.Location = New Point(400, 70)
        lblStatus.Size = New Size(150, 20)
        lblStatus.TextAlign = ContentAlignment.MiddleCenter

        Dim lblBreakdownTitle As New Label()
        lblBreakdownTitle.Text = "SALARY BREAKDOWN"
        lblBreakdownTitle.Font = New Font("Futura-Medium", 14, FontStyle.Bold)
        lblBreakdownTitle.ForeColor = Color.FromArgb(0, 122, 204)
        lblBreakdownTitle.Location = New Point(20, 255)
        lblBreakdownTitle.Size = New Size(200, 25)

        lblBasicSalary = New Label()
        lblBasicSalary.Font = New Font("Futura-Medium", 11)
        lblBasicSalary.Location = New Point(20, 290)
        lblBasicSalary.Size = New Size(300, 20)

        lblAllowances = New Label()
        lblAllowances.Font = New Font("Futura-Medium", 11)
        lblAllowances.Location = New Point(20, 315)
        lblAllowances.Size = New Size(300, 20)

        lblDeductions = New Label()
        lblDeductions.Font = New Font("Futura-Medium", 11)
        lblDeductions.Location = New Point(20, 340)
        lblDeductions.Size = New Size(300, 20)

        lblNetPay = New Label()
        lblNetPay.Font = New Font("Futura-Medium", 14, FontStyle.Bold)
        lblNetPay.ForeColor = Color.FromArgb(0, 150, 136)
        lblNetPay.Location = New Point(20, 375)
        lblNetPay.Size = New Size(300, 25)


        txtPayslipDetails = New TextBox()
        txtPayslipDetails.Multiline = True
        txtPayslipDetails.ScrollBars = ScrollBars.Vertical
        txtPayslipDetails.ReadOnly = True
        txtPayslipDetails.Font = New Font("Consolas", 9)
        txtPayslipDetails.Location = New Point(20, 415)
        txtPayslipDetails.Size = New Size(540, 250)


        btnPrint = New Button()
        btnPrint.Text = "PRINT PAYSLIP"
        btnPrint.BackColor = Color.FromArgb(0, 122, 204)
        btnPrint.ForeColor = Color.White
        btnPrint.FlatStyle = FlatStyle.Flat
        btnPrint.Font = New Font("Futura-Medium", 10, FontStyle.Bold)
        btnPrint.Location = New Point(20, 685)
        btnPrint.Size = New Size(150, 35)
        AddHandler btnPrint.Click, AddressOf btnPrint_Click

        btnClose = New Button()
        btnClose.Text = "CLOSE"
        btnClose.BackColor = Color.Gray
        btnClose.ForeColor = Color.White
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font = New Font("Futura-Medium", 10, FontStyle.Bold)
        btnClose.Location = New Point(190, 685)
        btnClose.Size = New Size(100, 35)
        AddHandler btnClose.Click, AddressOf btnClose_Click

        Me.Controls.Add(lblTitle)
        Me.Controls.Add(lblEmployeeName)
        Me.Controls.Add(lblEmployeeID)
        Me.Controls.Add(lblPosition)
        Me.Controls.Add(lblBranch)
        Me.Controls.Add(lblPayDate)
        Me.Controls.Add(lblPayPeriod)
        Me.Controls.Add(lblNumMonths)
        Me.Controls.Add(lblStatus)
        Me.Controls.Add(lblBreakdownTitle)
        Me.Controls.Add(lblBasicSalary)
        Me.Controls.Add(lblAllowances)
        Me.Controls.Add(lblDeductions)
        Me.Controls.Add(lblNetPay)
        Me.Controls.Add(txtPayslipDetails)
        Me.Controls.Add(btnPrint)
        Me.Controls.Add(btnClose)
    End Sub

    Public Sub LoadPayslipData(reader As MySqlDataReader)
        payslipData = reader

        Try

            Dim firstName As String = If(IsDBNull(reader("first_name")), "", reader("first_name").ToString())
            Dim lastName As String = If(IsDBNull(reader("last_name")), "", reader("last_name").ToString())
            Dim fullName As String = $"{firstName} {lastName}".Trim()

            lblEmployeeName.Text = $"Employee: {fullName}"
            lblEmployeeID.Text = $"Employee ID: {If(IsDBNull(reader("salary_id")), "N/A", reader("salary_id").ToString())}"
            lblPosition.Text = $"Position: {If(IsDBNull(reader("position_name")), "N/A", reader("position_name").ToString())}"
            lblBranch.Text = $"Branch: {If(IsDBNull(reader("branch_name")), "N/A", reader("branch_name").ToString())}"
            lblPayDate.Text = $"Pay Date: {If(IsDBNull(reader("pay_date")), "N/A", Convert.ToDateTime(reader("pay_date")).ToString("MMMM dd, yyyy"))}"
            lblPayPeriod.Text = $"Period: {If(IsDBNull(reader("from_date")), "N/A", Convert.ToDateTime(reader("from_date")).ToString("MMM dd"))} - {If(IsDBNull(reader("to_date")), "N/A", Convert.ToDateTime(reader("to_date")).ToString("MMM dd, yyyy"))}"


            Dim fromDate As DateTime = Convert.ToDateTime(reader("from_date"))
            Dim toDate As DateTime = Convert.ToDateTime(reader("to_date"))
            Dim numMonths As Integer = CalculateMonths(fromDate, toDate)
            lblNumMonths.Text = $"Number of Months: {numMonths}"


            Dim status As String = If(IsDBNull(reader("status")), "Unknown", reader("status").ToString().ToUpper())
            lblStatus.Text = status
            Select Case status.ToLower()
                Case "paid"
                    lblStatus.BackColor = Color.LightGreen
                    lblStatus.ForeColor = Color.DarkGreen
                Case "pending"
                    lblStatus.BackColor = Color.LightYellow
                    lblStatus.ForeColor = Color.DarkOrange
                Case "overdue"
                    lblStatus.BackColor = Color.LightCoral
                    lblStatus.ForeColor = Color.DarkRed
                Case Else
                    lblStatus.BackColor = Color.LightGray
                    lblStatus.ForeColor = Color.Black
            End Select


            Dim totalAmount As Decimal = If(IsDBNull(reader("rate_used")), 0, Convert.ToDecimal(reader("rate_used")))
            Dim monthlyRate As Decimal = If(IsDBNull(reader("monthly_rate")), 0, Convert.ToDecimal(reader("monthly_rate")))


            Dim basicSalary As Decimal = monthlyRate * numMonths


            Dim transportAllowance As Decimal = CalculateTransportAllowance(monthlyRate, numMonths)
            Dim mealAllowance As Decimal = CalculateMealAllowance(monthlyRate, numMonths)
            Dim housingAllowance As Decimal = CalculateHousingAllowance(monthlyRate, numMonths)
            Dim totalAllowances As Decimal = transportAllowance + mealAllowance + housingAllowance


            Dim taxDeduction As Decimal = CalculateTaxDeduction(basicSalary, numMonths)
            Dim sssDeduction As Decimal = CalculateSSSDeduction(basicSalary)
            Dim philhealthDeduction As Decimal = CalculatePhilHealthDeduction(basicSalary)
            Dim totalDeductions As Decimal = taxDeduction + sssDeduction + philhealthDeduction


            Dim grossPay As Decimal = basicSalary + totalAllowances
            Dim netPay As Decimal = grossPay - totalDeductions

            lblBasicSalary.Text = $"Basic Salary ({numMonths} months): ₱{basicSalary:N2}"
            lblAllowances.Text = $"Total Allowances: ₱{totalAllowances:N2}"
            lblDeductions.Text = $"Total Deductions: ₱{totalDeductions:N2}"
            lblNetPay.Text = $"NET PAY: ₱{netPay:N2}"


            GenerateDetailedPayslip(reader, numMonths, basicSalary, totalAllowances, totalDeductions, netPay, transportAllowance, mealAllowance, housingAllowance, taxDeduction, sssDeduction, philhealthDeduction)

        Catch ex As Exception
            MessageBox.Show("Error loading payslip data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function CalculateMonths(fromDate As DateTime, toDate As DateTime) As Integer

        Dim years As Integer = toDate.Year - fromDate.Year
        Dim months As Integer = toDate.Month - fromDate.Month


        Dim totalMonths As Integer = years * 12 + months

        If toDate.Day < fromDate.Day Then
            totalMonths -= 1
        End If


        Dim daysDifference As Integer = (toDate - fromDate).Days
        If daysDifference < 30 Then
            totalMonths = 0
        End If
        
        Return Math.Max(0, totalMonths)
    End Function

    Private Function CalculateTransportAllowance(monthlyRate As Decimal, numMonths As Integer) As Decimal
        ' Transport allowance: 5% of monthly rate per month
        Return (monthlyRate * 0.05D) * numMonths
    End Function

    Private Function CalculateMealAllowance(monthlyRate As Decimal, numMonths As Integer) As Decimal
        ' Meal allowance: 3% of monthly rate per month
        Return (monthlyRate * 0.03D) * numMonths
    End Function

    Private Function CalculateHousingAllowance(monthlyRate As Decimal, numMonths As Integer) As Decimal
        ' Housing allowance: 2% of monthly rate per month
        Return (monthlyRate * 0.02D) * numMonths
    End Function

    Private Function CalculateTaxDeduction(basicSalary As Decimal, numMonths As Integer) As Decimal

        Dim monthlySalary As Decimal = basicSalary / numMonths

        Dim monthlyTax As Decimal = 0
        
        If monthlySalary <= 25000 Then
            monthlyTax = 0
        ElseIf monthlySalary <= 33333.33D Then
            monthlyTax = (monthlySalary - 25000) * 0.2D
        ElseIf monthlySalary <= 66666.67D Then
            monthlyTax = 1666.67D + (monthlySalary - 33333.33D) * 0.25D
        ElseIf monthlySalary <= 166666.67D Then
            monthlyTax = 10000D + (monthlySalary - 66666.67D) * 0.3D
        ElseIf monthlySalary <= 666666.67D Then
            monthlyTax = 40000D + (monthlySalary - 166666.67D) * 0.32D
        Else
            monthlyTax = 200000D + (monthlySalary - 666666.67D) * 0.35D
        End If


        Return monthlyTax * numMonths
    End Function

    Private Function CalculateSSSDeduction(basicSalary As Decimal) As Decimal

        Return basicSalary * 0.03D
    End Function

    Private Function CalculatePhilHealthDeduction(basicSalary As Decimal) As Decimal

        Return basicSalary * 0.02D
    End Function

    Private Sub GenerateDetailedPayslip(reader As MySqlDataReader, numMonths As Integer, basicSalary As Decimal, totalAllowances As Decimal, totalDeductions As Decimal, netPay As Decimal, transportAllowance As Decimal, mealAllowance As Decimal, housingAllowance As Decimal, taxDeduction As Decimal, sssDeduction As Decimal, philhealthDeduction As Decimal)
        Try
            Dim payslipText As String = ""
            payslipText &= "=".PadRight(60, "=") & vbCrLf
            payslipText &= "                    MOTOCICLO PAYSLIP" & vbCrLf
            payslipText &= "=".PadRight(60, "=") & vbCrLf & vbCrLf


            payslipText &= "EMPLOYEE INFORMATION:" & vbCrLf
            payslipText &= "-".PadRight(30, "-") & vbCrLf
            payslipText &= $"Name: {If(IsDBNull(reader("first_name")), "", reader("first_name").ToString())} {If(IsDBNull(reader("last_name")), "", reader("last_name").ToString())}" & vbCrLf
            payslipText &= $"Email: {If(IsDBNull(reader("email")), "N/A", reader("email").ToString())}" & vbCrLf
            payslipText &= $"Phone: {If(IsDBNull(reader("phone_number")), "N/A", reader("phone_number").ToString())}" & vbCrLf
            payslipText &= $"Position: {If(IsDBNull(reader("position_name")), "N/A", reader("position_name").ToString())}" & vbCrLf
            payslipText &= $"Branch: {If(IsDBNull(reader("branch_name")), "N/A", reader("branch_name").ToString())}" & vbCrLf
            payslipText &= $"Hire Date: {If(IsDBNull(reader("hire_date")), "N/A", Convert.ToDateTime(reader("hire_date")).ToString("MMMM dd, yyyy"))}" & vbCrLf & vbCrLf


            payslipText &= "PAY INFORMATION:" & vbCrLf
            payslipText &= "-".PadRight(30, "-") & vbCrLf
            payslipText &= $"Salary ID: {If(IsDBNull(reader("salary_id")), "N/A", reader("salary_id").ToString())}" & vbCrLf
            payslipText &= $"Pay Date: {If(IsDBNull(reader("pay_date")), "N/A", Convert.ToDateTime(reader("pay_date")).ToString("MMMM dd, yyyy"))}" & vbCrLf
            payslipText &= $"Period: {If(IsDBNull(reader("from_date")), "N/A", Convert.ToDateTime(reader("from_date")).ToString("MMM dd"))} - {If(IsDBNull(reader("to_date")), "N/A", Convert.ToDateTime(reader("to_date")).ToString("MMM dd, yyyy"))}" & vbCrLf
            payslipText &= $"Number of Months: {numMonths}" & vbCrLf
            payslipText &= $"Status: {If(IsDBNull(reader("status")), "N/A", reader("status").ToString().ToUpper())}" & vbCrLf & vbCrLf

            Dim monthlyRate As Decimal = If(IsDBNull(reader("monthly_rate")), 0, Convert.ToDecimal(reader("monthly_rate")))

            payslipText &= "SALARY BREAKDOWN:" & vbCrLf
            payslipText &= "-".PadRight(30, "-") & vbCrLf
            payslipText &= $"Monthly Rate:".PadRight(25) & $"₱{monthlyRate:N2}" & vbCrLf
            payslipText &= $"Basic Salary ({numMonths} months):".PadRight(25) & $"₱{basicSalary:N2}" & vbCrLf & vbCrLf

            payslipText &= "ALLOWANCES:" & vbCrLf
            payslipText &= "-".PadRight(30, "-") & vbCrLf
            payslipText &= $"Transport Allowance:".PadRight(25) & $"₱{transportAllowance:N2}" & vbCrLf
            payslipText &= $"Meal Allowance:".PadRight(25) & $"₱{mealAllowance:N2}" & vbCrLf
            payslipText &= $"Housing Allowance:".PadRight(25) & $"₱{housingAllowance:N2}" & vbCrLf
            payslipText &= "".PadRight(25) & "-".PadRight(15, "-") & vbCrLf
            payslipText &= $"Total Allowances:".PadRight(25) & $"₱{totalAllowances:N2}" & vbCrLf & vbCrLf

            payslipText &= "GROSS PAY:" & vbCrLf
            payslipText &= "-".PadRight(30, "-") & vbCrLf
            payslipText &= $"Gross Pay:".PadRight(25) & $"₱{basicSalary + totalAllowances:N2}" & vbCrLf & vbCrLf

            payslipText &= "DEDUCTIONS:" & vbCrLf
            payslipText &= "-".PadRight(30, "-") & vbCrLf
            payslipText &= $"Tax:".PadRight(25) & $"₱{taxDeduction:N2}" & vbCrLf
            payslipText &= $"SSS:".PadRight(25) & $"₱{sssDeduction:N2}" & vbCrLf
            payslipText &= $"PhilHealth:".PadRight(25) & $"₱{philhealthDeduction:N2}" & vbCrLf
            payslipText &= "".PadRight(25) & "-".PadRight(15, "-") & vbCrLf
            payslipText &= $"Total Deductions:".PadRight(25) & $"₱{totalDeductions:N2}" & vbCrLf & vbCrLf

            payslipText &= "NET PAY:" & vbCrLf
            payslipText &= "-".PadRight(30, "-") & vbCrLf
            payslipText &= $"Net Pay:".PadRight(25) & $"₱{netPay:N2}" & vbCrLf & vbCrLf

            payslipText &= "=".PadRight(60, "=") & vbCrLf
            payslipText &= "Generated on: " & DateTime.Now.ToString("MMMM dd, yyyy 'at' hh:mm tt") & vbCrLf
            payslipText &= "This is a computer-generated document." & vbCrLf
            payslipText &= "=".PadRight(60, "=")

            txtPayslipDetails.Text = payslipText

        Catch ex As Exception
            txtPayslipDetails.Text = "Error generating detailed payslip: " & ex.Message
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        Try
            Dim printDocument As New PrintDocument()
            AddHandler printDocument.PrintPage, AddressOf PrintPayslip
            printDocument.Print()
        Catch ex As Exception
            MessageBox.Show("Error printing payslip: " & ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintPayslip(sender As Object, e As PrintPageEventArgs)
        Try
            Dim font As New Font("Consolas", 9)
            Dim brush As New SolidBrush(Color.Black)
            Dim y As Integer = 100

            Dim lines() As String = txtPayslipDetails.Text.Split(vbCrLf)
            For Each line As String In lines
                If y > e.MarginBounds.Bottom Then
                    e.HasMorePages = True
                    Return
                End If
                e.Graphics.DrawString(line, font, brush, 100, y)
                y += 15
            Next

        Catch ex As Exception
            MessageBox.Show("Error during printing: " & ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class 