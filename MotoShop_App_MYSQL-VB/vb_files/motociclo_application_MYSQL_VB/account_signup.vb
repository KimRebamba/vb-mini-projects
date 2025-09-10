Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Class account_signup

#Region "Buttons"
    Private Sub btnsignup_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Me.Hide()
        account_page.Show()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Check for empty fields
        If txtaddress.Text = "" Or txtemail.Text = "" Or txtpassword.Text = "" Or txtusername.Text = "" Or txtphone.Text = "" Or txtFN.Text = "" Or txtLN.Text = "" Then
            MessageBox.Show("EMPTY FIELDS / ANSWER ALL TEXTBOXES!")
            Return
        End If

        ' Check password strength
        If GetPasswordStrength(txtpassword.Text) = "Strong" Or GetPasswordStrength(txtpassword.Text) = "Medium" Then
            Dim connStr As String = "server=localhost;user=vb_login;password=login123;database=motociclo_db;"
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                ' Check if username or email already exists
                Dim checkCmd As New MySqlCommand("SELECT COUNT(*) FROM accounts WHERE username=@u OR email=@e", conn)
                checkCmd.Parameters.AddWithValue("@u", txtusername.Text.Trim())
                checkCmd.Parameters.AddWithValue("@e", txtemail.Text.Trim())

                Dim exists As Integer = CInt(checkCmd.ExecuteScalar())
                If exists > 0 Then
                    MessageBox.Show("Username or Email already taken.")
                    Return
                End If

                ' Insert new account using plain INSERT
                Dim insertQuery As String = "INSERT INTO accounts (username, password, email, address, phone_number, role, date_created, first_name, last_name) " &
                                        "VALUES (@username, @password, @email, @address, @phone, 'customer', CURDATE(), @firstname, @lastname)"

                Dim cmd As New MySqlCommand(insertQuery, conn)
                cmd.Parameters.AddWithValue("@username", txtusername.Text.Trim())
                cmd.Parameters.AddWithValue("@password", txtpassword.Text.Trim())
                cmd.Parameters.AddWithValue("@email", txtemail.Text.Trim())
                cmd.Parameters.AddWithValue("@address", txtaddress.Text.Trim())
                cmd.Parameters.AddWithValue("@phone", txtphone.Text.Trim())
                cmd.Parameters.AddWithValue("@firstname", txtFN.Text.Trim())
                cmd.Parameters.AddWithValue("@lastname", txtLN.Text.Trim())

                Try
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Signup successful!")
                    Me.Hide()
                    account_page.Show()
                Catch ex As MySqlException
                    MessageBox.Show("Signup failed: " & ex.Message)
                End Try

                txtusername.Clear()
                txtpassword.Clear()
                txtemail.Clear()
                txtaddress.Clear()
                txtphone.Clear()
                txtFN.Clear()
                txtLN.Clear()
                lblemail1.Text = ""
                lblpw.Text = ""
                lblvalid.Text = ""
            End Using

            messageTimer.Stop()
        Else
            MsgBox("PASSWORD TOO WEAK!")
        End If
    End Sub
#End Region

#Region "Checkers"
    'REGEX class - for checking of input/expression
    Function phoneChecker()
        Dim pattern As String = "^09\d{9}$|^09\d{2}-\d{3}-\d{4}$"
        Dim input As String = txtphone.Text.Trim()

        If Not Regex.IsMatch(input, pattern) Then
            Return False
        End If

        Return True
    End Function

    Private Function IsValidEmail(email As String) As Boolean
        Dim pattern As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        Return Regex.IsMatch(email, pattern)
    End Function

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtpassword.TextChanged
        If lblpw.Visible = False Then
            lblpw.Show()
        End If

        Dim password As String = txtpassword.Text
        Dim strength As String = GetPasswordStrength(password)

        lblpw.Text = strength

        Select Case strength
            Case "Weak"
                lblpw.ForeColor = Color.Red
            Case "Medium"
                lblpw.ForeColor = Color.Orange
            Case "Strong"
                lblpw.ForeColor = Color.Green
        End Select
    End Sub

    Private Function GetPasswordStrength(pw As String) As String
        Dim lengthValid As Boolean = pw.Length >= 8
        Dim hasUpper As Boolean = pw.Any(AddressOf Char.IsUpper)
        Dim hasLower As Boolean = pw.Any(AddressOf Char.IsLower)
        Dim hasDigit As Boolean = pw.Any(AddressOf Char.IsDigit)
        Dim hasSpecial As Boolean = pw.Any(Function(c) Not Char.IsLetterOrDigit(c))

        Dim score As Integer = 0
        If lengthValid Then score += 1
        If hasUpper Then score += 1
        If hasLower Then score += 1
        If hasDigit Then score += 1
        If hasSpecial Then score += 1

        Select Case score
            Case >= 5
                Return "Strong"
            Case 3 To 4
                Return "Medium"
            Case Else
                Return "Weak"
        End Select
    End Function

    Private Sub txtemail_TextChanged(sender As Object, e As EventArgs) Handles txtemail.TextChanged
        Dim emailInput As String = txtemail.Text.Trim()

        If IsValidEmail(emailInput) Then
            lblemail1.Text = "Valid email"
            lblemail1.ForeColor = Color.Green
        Else
            lblemail1.Text = "Invalid email"
            lblemail1.ForeColor = Color.Red
            txtemail.Focus()
        End If
    End Sub

    Private Sub txtphone_TextChanged(sender As Object, e As EventArgs) Handles txtphone.TextChanged
        If Not phoneChecker() Then
            lblvalid.ForeColor = Color.Red
            lblvalid.Text = "09XX-XXX-YYYY Format!"
            txtphone.Focus()
        Else
            lblvalid.ForeColor = Color.Green
            lblvalid.Text = "Valid Format!"
        End If
    End Sub

#End Region

#Region "Design/Asttk"
    Dim promoMessages As New List(Of String) From {
    "To get instant database updates!",
    "Better security & access on accounts!",
    "Add-to-cart feature!",
    "Clean UI and less hassle, perd!",
    "Easter eggs? Maybe!",
    "Managable for staff and admins!"
}

    Dim currentMessageIndex As Integer = 0
    Dim messageTimer As New Timer With {.Interval = 10000}

    Private Sub RotateMessages(sender As Object, e As EventArgs)
        currentMessageIndex += 1
        If currentMessageIndex >= promoMessages.Count Then

            currentMessageIndex = 0
        End If
        lblDescription.Text = "+ " & promoMessages(currentMessageIndex)
    End Sub

#End Region

#Region "Form"
    Private Sub account_signin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblDescription.Text = "+ " & promoMessages(currentMessageIndex) & vbCrLf
        AddHandler messageTimer.Tick, AddressOf RotateMessages
        messageTimer.Start()
    End Sub
    Private Sub account_signup_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        messageTimer.Start()
    End Sub
#End Region

End Class