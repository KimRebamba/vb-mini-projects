Imports MySql.Data.MySqlClient

Public Class account_page
    Dim loginConnStr As String =
        "server=localhost;user=vb_login;password=login123;database=motociclo_db;"

#Region "Login"
    Private Sub btnsignup_Click(sender As Object, e As EventArgs) Handles btnsignup.Click
        Me.Hide()
        account_signup.Show()
    End Sub

    Private Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit.Click
        Dim uname As String = txtusername.Text.Trim()
        Dim pw As String = txtpassword.Text.Trim()

        If String.IsNullOrWhiteSpace(uname) OrElse String.IsNullOrWhiteSpace(pw) Then
            MessageBox.Show("Please enter both username and password.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn As New MySqlConnection(loginConnStr)
            Try
                conn.Open()

                Dim query As String = "SELECT user_id, username, role FROM accounts WHERE username = @username AND password = @password"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@username", uname)
                cmd.Parameters.AddWithValue("@password", pw)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim userId As Integer = CInt(reader("user_id"))
                        Dim userName As String = reader("username").ToString()
                        Dim role As String = reader("role").ToString()

                        Try
                            Select Case role
                                Case "admin"
                                    Dim adminForm As New admin_page
                                    adminForm.connStr = "server=localhost;user=root;password=;database=motociclo_db;"
                                    adminForm.currentUserId = userId
                                    adminForm.currentUsername = userName
                                    adminForm.Show()
                                Case "employee"
                                    Dim empForm As New emp_page
                                    empForm.connStr = "server=localhost;user=mc_vbemployee;password=emp123;database=motociclo_db;"
                                    empForm.currentUsername = userName
                                    empForm.currentUserId = userId
                                    empForm.Show()
                                Case Else
                                    Dim homeForm As New home_page
                                    homeForm.connStr = "server=localhost;user=mc_vbcustomer;password=cust123;database=motociclo_db;"
                                    homeForm.currentUsername = userName
                                    homeForm.currentUserId = userId
                                    homeForm.Show()
                            End Select

                            Me.Hide()
                        Catch ex As Exception
                            MessageBox.Show("Error creating application form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Else
                        MessageBox.Show("Invalid credentials. Please check your username and password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Database connection error: " & ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        txtusername.Clear()
        txtpassword.Clear()
    End Sub

    Private Sub btnPWsee_Click(sender As Object, e As EventArgs) Handles btnPWsee.Click
        If txtpassword.UseSystemPasswordChar Then
            txtpassword.UseSystemPasswordChar = False
            btnPWsee.Text = "HIDE"
        Else
            txtpassword.UseSystemPasswordChar = True
            btnPWsee.Text = "SHOW"
        End If
    End Sub
#End Region

#Region "Design/asttik"
    Dim promoMessages As New List(Of String) From {
    "Get up to 0% off on selected models!",
    "Electric scooters now available!",
    "Visit our Quezon City branch today!",
    "Optional returns with every motorcycle purchase!",
    "Installment plans with 100% interest!",
    "Need help? Contact your selected branch!"
}

    Dim currentMessageIndex As Integer = 0
    Dim messageTimer As New Timer With {.Interval = 10000}

    Private Sub account_page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpassword.UseSystemPasswordChar = True
        lblDescription.Text = promoMessages(currentMessageIndex)

        AddHandler messageTimer.Tick, AddressOf RotateMessages
        messageTimer.Start()
    End Sub

    Private Sub RotateMessages(sender As Object, e As EventArgs)
        currentMessageIndex += 1
        If currentMessageIndex >= promoMessages.Count Then
            currentMessageIndex = 0
        End If
        lblDescription.Text = promoMessages(currentMessageIndex)
    End Sub

    Private Sub account_page_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If messageTimer IsNot Nothing Then
            messageTimer.Stop()
            messageTimer.Dispose()
        End If
    End Sub

#End Region

End Class
