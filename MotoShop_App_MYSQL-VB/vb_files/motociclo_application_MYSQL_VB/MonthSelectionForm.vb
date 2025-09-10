Public Class MonthSelectionForm
    Inherits Form

    Private cboMonth As ComboBox
    Private cboYear As ComboBox
    Private btnOK As Button
    Private btnCancel As Button
    Private lblTitle As Label
    Private lblMonth As Label
    Private lblYear As Label

    Public Property SelectedMonth As Integer
    Public Property SelectedYear As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Select Month and Year"
        Me.Size = New Size(350, 250)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        ' Title
        lblTitle = New Label()
        lblTitle.Text = "SELECT PAYROLL PERIOD"
        lblTitle.Font = New Font("Futura_Book-Bold", 14, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(0, 122, 204)
        lblTitle.Location = New Point(20, 20)
        lblTitle.Size = New Size(300, 25)
        lblTitle.TextAlign = ContentAlignment.MiddleCenter

        ' Month selection
        lblMonth = New Label()
        lblMonth.Text = "Month:"
        lblMonth.Font = New Font("Futura-Medium", 12, FontStyle.Bold)
        lblMonth.Location = New Point(30, 70)
        lblMonth.Size = New Size(80, 20)

        cboMonth = New ComboBox()
        cboMonth.Font = New Font("Futura-Medium", 11)
        cboMonth.Location = New Point(120, 70)
        cboMonth.Size = New Size(200, 25)
        cboMonth.DropDownStyle = ComboBoxStyle.DropDownList

        ' Year selection
        lblYear = New Label()
        lblYear.Text = "Year:"
        lblYear.Font = New Font("Futura-Medium", 12, FontStyle.Bold)
        lblYear.Location = New Point(30, 110)
        lblYear.Size = New Size(80, 20)

        cboYear = New ComboBox()
        cboYear.Font = New Font("Futura-Medium", 11)
        cboYear.Location = New Point(120, 110)
        cboYear.Size = New Size(200, 25)
        cboYear.DropDownStyle = ComboBoxStyle.DropDownList

        ' Buttons
        btnOK = New Button()
        btnOK.Text = "OK"
        btnOK.BackColor = Color.FromArgb(0, 122, 204)
        btnOK.ForeColor = Color.White
        btnOK.FlatStyle = FlatStyle.Flat
        btnOK.Font = New Font("Futura-Medium", 10, FontStyle.Bold)
        btnOK.Location = New Point(80, 160)
        btnOK.Size = New Size(80, 35)
        btnOK.DialogResult = DialogResult.OK
        AddHandler btnOK.Click, AddressOf btnOK_Click

        btnCancel = New Button()
        btnCancel.Text = "CANCEL"
        btnCancel.BackColor = Color.Gray
        btnCancel.ForeColor = Color.White
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Futura-Medium", 10, FontStyle.Bold)
        btnCancel.Location = New Point(180, 160)
        btnCancel.Size = New Size(80, 35)
        btnCancel.DialogResult = DialogResult.Cancel

        ' Add controls to form
        Me.Controls.Add(lblTitle)
        Me.Controls.Add(lblMonth)
        Me.Controls.Add(cboMonth)
        Me.Controls.Add(lblYear)
        Me.Controls.Add(cboYear)
        Me.Controls.Add(btnOK)
        Me.Controls.Add(btnCancel)

        ' Load month and year data
        LoadMonthYearData()
    End Sub

    Private Sub LoadMonthYearData()
        ' Load months
        cboMonth.Items.Clear()
        cboMonth.Items.Add("January")
        cboMonth.Items.Add("February")
        cboMonth.Items.Add("March")
        cboMonth.Items.Add("April")
        cboMonth.Items.Add("May")
        cboMonth.Items.Add("June")
        cboMonth.Items.Add("July")
        cboMonth.Items.Add("August")
        cboMonth.Items.Add("September")
        cboMonth.Items.Add("October")
        cboMonth.Items.Add("November")
        cboMonth.Items.Add("December")

        ' Load years (current year and 5 years back)
        cboYear.Items.Clear()
        Dim currentYear As Integer = DateTime.Now.Year
        For i As Integer = currentYear - 5 To currentYear + 1
            cboYear.Items.Add(i.ToString())
        Next

        ' Set default values
        cboMonth.SelectedIndex = DateTime.Now.Month - 1 ' Current month (0-based index)
        cboYear.SelectedItem = currentYear.ToString()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs)
        If cboMonth.SelectedIndex = -1 OrElse cboYear.SelectedIndex = -1 Then
            MessageBox.Show("Please select both month and year.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Set the selected values
        SelectedMonth = cboMonth.SelectedIndex + 1 ' Convert to 1-based month
        SelectedYear = Convert.ToInt32(cboYear.SelectedItem)

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class 