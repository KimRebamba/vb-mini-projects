Imports MySql.Data.MySqlClient

Partial Public Class emp_page

    Public Property connStr As String
    Public Property currentUsername As String
    Public Property currentUserId As Integer
    Public Property currentUserRole As String
    Public Property currentUserBranchId As Integer

    Private Shadows contextMenu As New ContextMenuStrip()

    Private Sub emp_page_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not String.IsNullOrEmpty(currentUsername) Then
            lblWelcome.Text = "EMPLOYEE WELCOME, " + currentUsername.ToUpper + "!"
        End If

        Me.Text = "Employee Dashboard - " + currentUsername

        InitializeContextMenu()

        LoadDashboard()
    End Sub

    Private Sub InitializeContextMenu()
        contextMenu = New ContextMenuStrip()

        Dim statuses() As String = {"pending", "processing", "shipped", "delivered", "cancelled"}

        For Each status As String In statuses
            Dim menuItem As New ToolStripMenuItem("Mark as " & status.ToUpper())
            menuItem.Tag = status
            AddHandler menuItem.Click, AddressOf UpdateOrderStatus
            contextMenu.Items.Add(menuItem)
        Next

        contextMenu.Items.Add(New ToolStripSeparator())

        Dim viewDetailsItem As New ToolStripMenuItem("View Order Details")
        AddHandler viewDetailsItem.Click, AddressOf ViewOrderDetails
        contextMenu.Items.Add(viewDetailsItem)

        dgvOrders.ContextMenuStrip = contextMenu
    End Sub

    Private Sub UpdateOrderStatus(sender As Object, e As EventArgs)
        If lblTitle.Text <> "ORDER MANAGEMENT" Then
            MessageBox.Show("Order status can only be updated from the Order Management section.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If dgvOrders.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an order to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim selectedRow As DataGridViewRow = dgvOrders.SelectedRows(0)
        Dim orderId As Integer = Convert.ToInt32(selectedRow.Cells("order_id").Value)
        Dim newStatus As String = DirectCast(sender, ToolStripMenuItem).Tag.ToString()
        Dim currentStatus As String = selectedRow.Cells("order_status").Value.ToString()
        Dim accountName As String = selectedRow.Cells("first_name").Value.ToString() & " " & selectedRow.Cells("last_name").Value.ToString()
        Dim modelName As String = selectedRow.Cells("model_name").Value.ToString()

        Dim result As DialogResult = MessageBox.Show(
            $"Are you sure you want to update Order #{orderId} status from '{currentStatus.ToUpper()}' to '{newStatus.ToUpper()}'?" & vbCrLf & vbCrLf &
            $"Account: {accountName}" & vbCrLf &
            $"Model: {modelName}",
            "Confirm Status Update",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Try
                Using conn As New MySqlConnection(connStr)
                    conn.Open()

                    Dim updateQuery As String = "UPDATE Orders SET order_status = @status WHERE order_id = @orderId"
                    Dim cmd As New MySqlCommand(updateQuery, conn)
                    cmd.Parameters.AddWithValue("@status", newStatus)
                    cmd.Parameters.AddWithValue("@orderId", orderId)

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show($"Order #{orderId} status successfully updated to '{newStatus.ToUpper()}'", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        LoadAllOrders()

                        LoadStatistics(conn)
                    Else
                        MessageBox.Show("Failed to update order status. Order may not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error updating order status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub ViewOrderDetails(sender As Object, e As EventArgs)
        If dgvOrders.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an order to view details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim selectedRow As DataGridViewRow = dgvOrders.SelectedRows(0)

        Select Case lblTitle.Text
            Case "ORDER MANAGEMENT"
                Dim orderId As Integer = Convert.ToInt32(selectedRow.Cells("order_id").Value)
                Dim customerName As String = selectedRow.Cells("first_name").Value.ToString() & " " & selectedRow.Cells("last_name").Value.ToString()
                Dim phoneNumber As String = selectedRow.Cells("phone_number").Value.ToString()
                Dim modelName As String = selectedRow.Cells("model_name").Value.ToString()
                Dim orderStatus As String = selectedRow.Cells("order_status").Value.ToString()
                Dim paymentStatus As String = selectedRow.Cells("payment_status").Value.ToString()
                Dim dateOrdered As String = selectedRow.Cells("date_ordered").Value.ToString()
                Dim quantity As String = selectedRow.Cells("quantity").Value.ToString()

                Dim details As String = $"ORDER DETAILS" & vbCrLf & vbCrLf &
                                       $"Order ID: {orderId}" & vbCrLf &
                                       $"Customer: {customerName}" & vbCrLf &
                                       $"Phone: {phoneNumber}" & vbCrLf &
                                       $"Model: {modelName}" & vbCrLf &
                                       $"Quantity: {quantity}" & vbCrLf &
                                       $"Order Status: {orderStatus.ToUpper()}" & vbCrLf &
                                       $"Payment Status: {paymentStatus.ToUpper()}" & vbCrLf &
                                       $"Date Ordered: {dateOrdered}" & vbCrLf
                MessageBox.Show(details, "Order Details", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Case "RETURNS MANAGEMENT"
                Dim returnId As Integer = Convert.ToInt32(selectedRow.Cells("return_id").Value)
                Dim orderId As Integer = Convert.ToInt32(selectedRow.Cells("order_id").Value)
                Dim accountName As String = selectedRow.Cells("first_name").Value.ToString() & " " & selectedRow.Cells("last_name").Value.ToString()
                Dim returnDate As String = selectedRow.Cells("return_date").Value.ToString()
                Dim reason As String = selectedRow.Cells("reason").Value.ToString()
                Dim condition As String = selectedRow.Cells("condition_").Value.ToString()
                Dim returnStatus As String = selectedRow.Cells("return_status").Value.ToString()
                Dim quantityReturned As String = selectedRow.Cells("quantity_returned").Value.ToString()
                Dim details As String = $"RETURN DETAILS" & vbCrLf & vbCrLf &
                                       $"Return ID: {returnId}" & vbCrLf &
                                       $"Order ID: {orderId}" & vbCrLf &
                                       $"Account: {accountName}" & vbCrLf &
                                       $"Return Date: {returnDate}" & vbCrLf &
                                       $"Quantity Returned: {quantityReturned}" & vbCrLf &
                                       $"Reason: {reason}" & vbCrLf &
                                       $"Condition: {condition}" & vbCrLf &
                                       $"Status: {returnStatus.ToUpper()}"
                MessageBox.Show(details, "Return Details", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Case "ACCOUNT MANAGEMENT"
                Dim userId As Integer = Convert.ToInt32(selectedRow.Cells("user_id").Value)
                Dim accountName As String = selectedRow.Cells("first_name").Value.ToString() & " " & selectedRow.Cells("last_name").Value.ToString()
                Dim email As String = selectedRow.Cells("email").Value.ToString()
                Dim phoneNumber As String = selectedRow.Cells("phone_number").Value.ToString()
                Dim address As String = selectedRow.Cells("address").Value.ToString()
                Dim username As String = selectedRow.Cells("username").Value.ToString()
                Dim role As String = selectedRow.Cells("role").Value.ToString()
                Dim dateCreated As String = selectedRow.Cells("date_created").Value.ToString()
                Dim details As String = $"ACCOUNT DETAILS" & vbCrLf & vbCrLf &
                                       $"User ID: {userId}" & vbCrLf &
                                       $"Name: {accountName}" & vbCrLf &
                                       $"Email: {email}" & vbCrLf &
                                       $"Username: {username}" & vbCrLf &
                                       $"Phone: {phoneNumber}" & vbCrLf &
                                       $"Address: {address}" & vbCrLf &
                                       $"Role: {role}" & vbCrLf &
                                       $"Date Created: {dateCreated}"
                MessageBox.Show(details, "Account Details", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Case "PAYROLL"
                Dim salaryId As Integer = Convert.ToInt32(selectedRow.Cells("salary_id").Value)
                Dim payDate As String = selectedRow.Cells("pay_date").Value.ToString()
                Dim amount As String = selectedRow.Cells("rate_used").Value.ToString()
                Dim status As String = selectedRow.Cells("status").Value.ToString()
                Dim fromDate As String = selectedRow.Cells("from_date").Value.ToString()
                Dim toDate As String = selectedRow.Cells("to_date").Value.ToString()

                Dim employeeName As String = ""
                Try
                    Using conn As New MySqlConnection(connStr)
                        conn.Open()
                        Dim empQuery As String = "SELECT CONCAT(a.first_name, ' ', a.last_name) AS full_name FROM employees e JOIN accounts a ON e.user_id = a.user_id WHERE e.emp_id = @empId"
                        Dim cmd As New MySqlCommand(empQuery, conn)
                        cmd.Parameters.AddWithValue("@empId", currentUserId)
                        Dim result = cmd.ExecuteScalar()
                        If result IsNot Nothing Then
                            employeeName = result.ToString()
                        End If
                    End Using
                Catch ex As Exception
                    employeeName = "Employee"
                End Try

                Dim details As String = $"PAYSLIP DETAILS" & vbCrLf & vbCrLf &
                                       $"Employee: {employeeName}" & vbCrLf &
                                       $"Salary ID: {salaryId}" & vbCrLf &
                                       $"Pay Date: {payDate}" & vbCrLf &
                                       $"Amount: ₱{amount:N2}" & vbCrLf &
                                       $"Status: {status.ToUpper()}" & vbCrLf &
                                       $"Period: {fromDate} to {toDate}" & vbCrLf & vbCrLf &
                                       $"Click 'View Payslip' to see detailed breakdown."
                MessageBox.Show(details, "Payslip Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select
    End Sub

    Private contextMenuReturns As ContextMenuStrip

    Private Sub InitializeReturnsContextMenu()
        contextMenuReturns = New ContextMenuStrip()
        Dim statuses() As String = {"Approved", "Rejected", "Pending"}
        For Each status As String In statuses
            Dim menuItem As New ToolStripMenuItem("Mark as " & status)
            menuItem.Tag = status
            AddHandler menuItem.Click, AddressOf UpdateReturnStatus
            contextMenuReturns.Items.Add(menuItem)
        Next
        contextMenuReturns.Items.Add(New ToolStripSeparator())
        Dim viewDetailsItem As New ToolStripMenuItem("View Return Details")
        AddHandler viewDetailsItem.Click, AddressOf ViewOrderDetails
        contextMenuReturns.Items.Add(viewDetailsItem)
    End Sub

    Private Sub UpdateReturnStatus(sender As Object, e As EventArgs)
        If dgvOrders.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a return to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim selectedRow As DataGridViewRow = dgvOrders.SelectedRows(0)
        Dim returnId As Integer = Convert.ToInt32(selectedRow.Cells("return_id").Value)
        Dim newStatus As String = DirectCast(sender, ToolStripMenuItem).Tag.ToString()
        Dim currentStatus As String = selectedRow.Cells("return_status").Value.ToString()
        Dim accountName As String = selectedRow.Cells("first_name").Value.ToString() & " " & selectedRow.Cells("last_name").Value.ToString()
        Dim result As DialogResult = MessageBox.Show(
            $"Are you sure you want to update Return #{returnId} status from '{currentStatus}' to '{newStatus}'?" & vbCrLf &
            $"Account: {accountName}",
            "Confirm Status Update",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Try
                Using conn As New MySqlConnection(connStr)
                    conn.Open()
                    Dim updateQuery As String = "UPDATE Returns SET return_status = @status WHERE return_id = @returnId"
                    Dim cmd As New MySqlCommand(updateQuery, conn)
                    cmd.Parameters.AddWithValue("@status", newStatus)
                    cmd.Parameters.AddWithValue("@returnId", returnId)
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        MessageBox.Show($"Return #{returnId} status successfully updated to '{newStatus}'", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadReturns()
                    Else
                        MessageBox.Show("Failed to update return status. Return may not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error updating return status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub LoadDashboard()
        Me.dgvOrders.ScrollBars = ScrollBars.Both
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                If currentUserBranchId = 0 Then
                    currentUserBranchId = GetEmployeeBranchId(conn)
                End If

                LoadRecentOrders(conn)

                LoadStatistics(conn)

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading dashboard: " & ex.Message)
        End Try
    End Sub

    Private Function GetEmployeeBranchId(conn As MySqlConnection) As Integer
        Try
            ' Get branch_id using the new user_id relationship and current_branch_id column
            Dim query As String = "SELECT e.current_branch_id FROM employees e WHERE e.user_id = @userId"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@userId", currentUserId)
            Dim result = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return Convert.ToInt32(result)
            End If

            ' Fallback to username matching with accounts table
            query = "SELECT e.current_branch_id FROM employees e JOIN accounts a ON e.user_id = a.user_id WHERE a.username = @username"
            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@username", currentUsername)
            result = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return Convert.ToInt32(result)
            End If

        Catch ex As Exception
            MessageBox.Show("Error getting employee branch: " & ex.Message)
        End Try

        Return 0
    End Function

    Private Sub LoadRecentOrders(conn As MySqlConnection)
        dgvOrders.ContextMenuStrip = Nothing
        Dim query As String = "
        SELECT 
            o.order_id,
            o.date_ordered,
            o.est_delivery,
            o.order_status,
            o.payment_status,
            o.payment_option,
            a.first_name,
            a.last_name,
            a.phone_number,
            m.model_name,
            m.brand,
            oi.quantity,
            oi.unit_price,
            (oi.quantity * oi.unit_price) as subtotal
        FROM orders o
        JOIN accounts a ON o.user_id = a.user_id
        JOIN order_items oi ON o.order_id = oi.order_id
        JOIN models m ON oi.model_id = m.model_id
        WHERE oi.branch_id = @branchId
        ORDER BY o.order_id DESC, m.model_name
        LIMIT 10"
        Dim cmd As New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@branchId", currentUserBranchId)
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim dt As New DataTable()
        adapter.Fill(dt)
        dgvOrders.DataSource = dt

        ' Set column headers for better readability
        If dgvOrders.Columns.Count > 0 Then
            dgvOrders.Columns("order_id").HeaderText = "Order ID"
            dgvOrders.Columns("date_ordered").HeaderText = "Date Ordered"
            dgvOrders.Columns("est_delivery").HeaderText = "Est. Delivery"
            dgvOrders.Columns("order_status").HeaderText = "Order Status"
            dgvOrders.Columns("payment_status").HeaderText = "Payment Status"
            dgvOrders.Columns("payment_option").HeaderText = "Payment Method"
            dgvOrders.Columns("first_name").HeaderText = "First Name"
            dgvOrders.Columns("last_name").HeaderText = "Last Name"
            dgvOrders.Columns("phone_number").HeaderText = "Phone"
            dgvOrders.Columns("model_name").HeaderText = "Model Name"
            dgvOrders.Columns("brand").HeaderText = "Brand"
            dgvOrders.Columns("quantity").HeaderText = "Quantity"
            dgvOrders.Columns("unit_price").HeaderText = "Unit Price"
            dgvOrders.Columns("subtotal").HeaderText = "Subtotal"

            ' Format currency columns
            dgvOrders.Columns("unit_price").DefaultCellStyle.Format = "N2"
            dgvOrders.Columns("subtotal").DefaultCellStyle.Format = "N2"

            ' Set column widths
            dgvOrders.Columns("order_id").Width = 80
            dgvOrders.Columns("date_ordered").Width = 120
            dgvOrders.Columns("est_delivery").Width = 100
            dgvOrders.Columns("order_status").Width = 100
            dgvOrders.Columns("payment_status").Width = 100
            dgvOrders.Columns("payment_option").Width = 100
            dgvOrders.Columns("first_name").Width = 100
            dgvOrders.Columns("last_name").Width = 100
            dgvOrders.Columns("phone_number").Width = 100
            dgvOrders.Columns("model_name").Width = 150
            dgvOrders.Columns("brand").Width = 100
            dgvOrders.Columns("quantity").Width = 80
            dgvOrders.Columns("unit_price").Width = 100
            dgvOrders.Columns("subtotal").Width = 100

            ' Style order status column
            dgvOrders.Columns("order_status").DefaultCellStyle.Font = New Font(dgvOrders.Font, FontStyle.Bold)
            dgvOrders.Columns("order_status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If
    End Sub

    Private Sub LoadStatistics(conn As MySqlConnection)
        Try
            Dim cmdTotal As New MySqlCommand("SELECT COUNT(DISTINCT o.order_id) FROM orders o JOIN order_items oi ON o.order_id = oi.order_id WHERE oi.branch_id = @branchId", conn)
            cmdTotal.Parameters.AddWithValue("@branchId", currentUserBranchId)
            Dim totalOrders As Integer = Convert.ToInt32(cmdTotal.ExecuteScalar())
            Me.lblTotalOrders.Text = "Total Orders: " & totalOrders

            Dim cmdReturns As New MySqlCommand("SELECT COUNT(*) FROM returns r JOIN orders o ON r.order_id = o.order_id JOIN order_items oi ON o.order_id = oi.order_id WHERE r.return_status = 'Processing' AND oi.branch_id = @branchId", conn)
            cmdReturns.Parameters.AddWithValue("@branchId", currentUserBranchId)
            Dim pendingReturns As Integer = Convert.ToInt32(cmdReturns.ExecuteScalar())
            Me.lblPendingReturns.Text = "Pending Returns: " & pendingReturns

            Dim cmdCompleted As New MySqlCommand("SELECT COUNT(DISTINCT o.order_id) FROM orders o JOIN order_items oi ON o.order_id = oi.order_id WHERE o.order_status = 'delivered' AND oi.branch_id = @branchId", conn)
            cmdCompleted.Parameters.AddWithValue("@branchId", currentUserBranchId)
            Dim completedOrders As Integer = Convert.ToInt32(cmdCompleted.ExecuteScalar())
            Me.lblCompletedOrders.Text = "Completed Orders: " & completedOrders

            ' Get employee welcome message using the accounts table for name information
            Dim cmdEmployee As New MySqlCommand("SELECT a.username, a.first_name, a.last_name FROM accounts a WHERE a.user_id = @userId", conn)
            cmdEmployee.Parameters.AddWithValue("@userId", currentUserId)
            Using reader As MySqlDataReader = cmdEmployee.ExecuteReader()
                If reader.Read() Then
                    Dim username As String = reader("username").ToString()
                    Dim firstName As String = If(reader("first_name") IsNot DBNull.Value, reader("first_name").ToString(), "")
                    Dim lastName As String = If(reader("last_name") IsNot DBNull.Value, reader("last_name").ToString(), "")
                    
                    If Not String.IsNullOrEmpty(firstName) AndAlso Not String.IsNullOrEmpty(lastName) Then
                        Me.lblWelcome.Text = "EMPLOYEE WELCOME, " & firstName.ToUpper() & " " & lastName.ToUpper() & "!"
                    Else
                        Me.lblWelcome.Text = "EMPLOYEE WELCOME, " & username.ToUpper() & "!"
                    End If
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading statistics: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        ResetButtonColors()
        btnDashboard.BackColor = Color.FromArgb(192, 0, 0)

        lblTitle.Text = "DASHBOARD"
        lblOrders.Text = "RECENT ORDERS"
        lblInstructions.Text = "EDITING DONE IN VIEW ORDERS BUTTON!"
        LoadDashboard()
    End Sub

    Private Sub btnViewOrders_Click(sender As Object, e As EventArgs) Handles btnViewOrders.Click
        ResetButtonColors()
        InitializeContextMenu()
        btnViewOrders.BackColor = Color.FromArgb(192, 0, 0)

        lblTitle.Text = "ORDER MANAGEMENT"
        lblOrders.Text = "ALL ORDERS"
        lblInstructions.Text = "Right-click on an order to edit status or view details"
        dgvOrders.ContextMenuStrip = contextMenu
        LoadAllOrders()
    End Sub

    Private Sub LoadAllOrders()
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                If currentUserBranchId = 0 Then
                    currentUserBranchId = GetEmployeeBranchId(conn)
                End If

                Dim query As String = "
                SELECT 
                    o.order_id,
                    o.date_ordered,
                    o.est_delivery,
                    o.order_status,
                    o.payment_status,
                    o.payment_option,
                    a.first_name,
                    a.last_name,
                    a.phone_number,
                    m.model_name,
                    m.brand,
                    oi.quantity,
                    oi.unit_price,
                    (oi.quantity * oi.unit_price) as subtotal
                FROM orders o
                JOIN accounts a ON o.user_id = a.user_id
                JOIN order_items oi ON o.order_id = oi.order_id
                JOIN models m ON oi.model_id = m.model_id
                WHERE oi.branch_id = @branchId
                ORDER BY o.order_id DESC, m.model_name"

                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@branchId", currentUserBranchId)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                dgvOrders.DataSource = dt

                ' Set column headers for better readability
                If dgvOrders.Columns.Count > 0 Then
                    dgvOrders.Columns("order_id").HeaderText = "Order ID"
                    dgvOrders.Columns("date_ordered").HeaderText = "Date Ordered"
                    dgvOrders.Columns("est_delivery").HeaderText = "Est. Delivery"
                    dgvOrders.Columns("order_status").HeaderText = "Order Status"
                    dgvOrders.Columns("payment_status").HeaderText = "Payment Status"
                    dgvOrders.Columns("payment_option").HeaderText = "Payment Method"
                    dgvOrders.Columns("first_name").HeaderText = "First Name"
                    dgvOrders.Columns("last_name").HeaderText = "Last Name"
                    dgvOrders.Columns("phone_number").HeaderText = "Phone"
                    dgvOrders.Columns("model_name").HeaderText = "Model Name"
                    dgvOrders.Columns("brand").HeaderText = "Brand"
                    dgvOrders.Columns("quantity").HeaderText = "Quantity"
                    dgvOrders.Columns("unit_price").HeaderText = "Unit Price"
                    dgvOrders.Columns("subtotal").HeaderText = "Subtotal"

                    ' Format currency columns
                    dgvOrders.Columns("unit_price").DefaultCellStyle.Format = "N2"
                    dgvOrders.Columns("subtotal").DefaultCellStyle.Format = "N2"

                    ' Set column widths
                    dgvOrders.Columns("order_id").Width = 80
                    dgvOrders.Columns("date_ordered").Width = 120
                    dgvOrders.Columns("est_delivery").Width = 100
                    dgvOrders.Columns("order_status").Width = 100
                    dgvOrders.Columns("payment_status").Width = 100
                    dgvOrders.Columns("payment_option").Width = 100
                    dgvOrders.Columns("first_name").Width = 100
                    dgvOrders.Columns("last_name").Width = 100
                    dgvOrders.Columns("phone_number").Width = 100
                    dgvOrders.Columns("model_name").Width = 150
                    dgvOrders.Columns("brand").Width = 100
                    dgvOrders.Columns("quantity").Width = 80
                    dgvOrders.Columns("unit_price").Width = 100
                    dgvOrders.Columns("subtotal").Width = 100

                    ' Style order status column
                    dgvOrders.Columns("order_status").DefaultCellStyle.Font = New Font(dgvOrders.Font, FontStyle.Bold)
                    dgvOrders.Columns("order_status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading orders: " & ex.Message)
        End Try
    End Sub

    Private Sub btnProcessReturns_Click(sender As Object, e As EventArgs) Handles btnProcessReturns.Click
        InitializeReturnsContextMenu()
        ResetButtonColors()
        btnProcessReturns.BackColor = Color.FromArgb(192, 0, 0)

        lblTitle.Text = "RETURNS MANAGEMENT"
        lblOrders.Text = "PENDING RETURNS"
        dgvOrders.ContextMenuStrip = Nothing
        lblInstructions.Text = "Right-click on a return to update status or view details"
        dgvOrders.ContextMenuStrip = contextMenuReturns
        LoadReturns()
    End Sub

    Private Sub LoadReturns()
        Try

            Using conn As New MySqlConnection(connStr)
                conn.Open()

                If currentUserBranchId = 0 Then
                    currentUserBranchId = GetEmployeeBranchId(conn)
                End If

                Dim query As String = "SELECT r.return_id, o.order_id, a.first_name, a.last_name, " &
                                     "r.return_date, r.reason, r.condition_, r.return_status, r.quantity_returned " &
                                     "FROM Returns r " &
                                     "JOIN Orders o ON r.order_id = o.order_id " &
                                     "JOIN order_items oi ON o.order_id = oi.order_id " &
                                     "JOIN accounts a ON o.user_id = a.user_id " &
                                     "WHERE oi.branch_id = @branchId " &
                                     "ORDER BY r.return_date DESC"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@branchId", currentUserBranchId)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                dgvOrders.DataSource = dt
                If dgvOrders.Columns.Count > 0 Then
                    dgvOrders.Columns("return_id").HeaderText = "Return ID"
                    dgvOrders.Columns("order_id").HeaderText = "Order ID"
                    dgvOrders.Columns("first_name").HeaderText = "First Name"
                    dgvOrders.Columns("last_name").HeaderText = "Last Name"
                    dgvOrders.Columns("return_date").HeaderText = "Return Date"
                    dgvOrders.Columns("reason").HeaderText = "Reason"
                    dgvOrders.Columns("condition_").HeaderText = "Condition"
                    dgvOrders.Columns("return_status").HeaderText = "Status"
                    dgvOrders.Columns("quantity_returned").HeaderText = "Quantity"
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading returns: " & ex.Message)
        End Try
    End Sub

    Private Sub btnAccountManagement_Click(sender As Object, e As EventArgs) Handles btnAccountManagement.Click
        ResetButtonColors()
        btnAccountManagement.BackColor = Color.FromArgb(192, 0, 0)
        lblTitle.Text = "ACCOUNT MANAGEMENT"
        lblOrders.Text = "ACCOUNT LIST"
        lblInstructions.Text = "Double-click on an account to view details"
        dgvOrders.ContextMenuStrip = Nothing
        LoadAccounts()
    End Sub

    Private Sub LoadAccounts()
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                Dim query As String = "SELECT user_id, first_name, last_name, address, phone_number, email, username, role, date_created FROM accounts WHERE role <> 'admin' ORDER BY user_id"
                Dim cmd As New MySqlCommand(query, conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                dgvOrders.DataSource = dt
                If dgvOrders.Columns.Count > 0 Then
                    dgvOrders.Columns("user_id").HeaderText = "User ID"
                    dgvOrders.Columns("first_name").HeaderText = "First Name"
                    dgvOrders.Columns("last_name").HeaderText = "Last Name"
                    dgvOrders.Columns("address").HeaderText = "Address"
                    dgvOrders.Columns("phone_number").HeaderText = "Phone"
                    dgvOrders.Columns("email").HeaderText = "Email"
                    dgvOrders.Columns("username").HeaderText = "Username"
                    dgvOrders.Columns("role").HeaderText = "Role"
                    dgvOrders.Columns("date_created").HeaderText = "Date Created"
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading accounts: " & ex.Message)
        End Try
    End Sub

    Private Sub btnPayroll_Click(sender As Object, e As EventArgs) Handles btnPayroll.Click
        ResetButtonColors()
        btnPayroll.BackColor = Color.FromArgb(192, 0, 0)
        lblTitle.Text = "PAYROLL"
        lblOrders.Text = "MY PAYSLIPS"
        lblInstructions.Text = "Double-click on a payslip to view details or click 'View Payslip' to see detailed breakdown"
        dgvOrders.ContextMenuStrip = Nothing
        LoadPayroll()
    End Sub

    Private Sub LoadPayroll()
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                Dim empId As Integer = GetEmployeeId(currentUserId)

                If empId = -1 Then
                    MessageBox.Show("Employee record not found for current user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                Dim query As String = "SELECT salary_id, pay_date, rate_used, status, from_date, to_date " &
                                     "FROM Salaries " &
                                     "WHERE emp_id = @empId " &
                                     "ORDER BY pay_date DESC"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@empId", empId)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                dgvOrders.DataSource = dt

                If dgvOrders.Columns.Count > 0 Then
                    dgvOrders.Columns("salary_id").HeaderText = "Salary ID"
                    dgvOrders.Columns("pay_date").HeaderText = "Pay Date"
                    dgvOrders.Columns("rate_used").HeaderText = "Rate (₱)"
                    dgvOrders.Columns("status").HeaderText = "Status"
                    dgvOrders.Columns("from_date").HeaderText = "From Date"
                    dgvOrders.Columns("to_date").HeaderText = "To Date"

                    dgvOrders.Columns("rate_used").DefaultCellStyle.Format = "N2"
                    dgvOrders.Columns("rate_used").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading payroll: " & ex.Message)
        End Try
    End Sub

    Private Function GetEmployeeId(userId As Integer) As Integer
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                ' Get emp_id using the new user_id relationship
                Dim query As String = "SELECT emp_id FROM employees WHERE user_id = @userId"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@userId", userId)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return Convert.ToInt32(result)
                End If
            End Using
        Catch ex As Exception
            ' If user_id relationship fails, try username matching
            Try
                Using conn As New MySqlConnection(connStr)
                    conn.Open()
                    Dim query As String = "SELECT e.emp_id FROM employees e JOIN accounts a ON e.user_id = a.user_id WHERE a.username = @username"
                    Dim cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@username", currentUsername)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        Return Convert.ToInt32(result)
                    End If
                End Using
            Catch ex2 As Exception
                ' Log error but don't show to user
            End Try
        End Try
        Return -1
    End Function

    Private Sub btnViewPayslip_Click(sender As Object, e As EventArgs) Handles btnViewPayslip.Click
        If lblTitle.Text <> "PAYROLL" Then
            MessageBox.Show("Payslip can only be viewed from the Payroll section.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If dgvOrders.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a payslip to view.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim selectedRow As DataGridViewRow = dgvOrders.SelectedRows(0)
        Dim salaryId As Integer = Convert.ToInt32(selectedRow.Cells("salary_id").Value)

        ShowDetailedPayslip(salaryId)
    End Sub

    Private Sub ShowDetailedPayslip(salaryId As Integer)
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                'payslip query
                Dim query As String = "SELECT s.salary_id, s.pay_date, s.rate_used, s.status, s.from_date, s.to_date, " &
                                     "a.first_name, a.last_name, a.email, a.phone_number, a.date_created as hire_date, " &
                                     "p.position_name, p.monthly_rate, b.branch_name " &
                                     "FROM Salaries s " &
                                     "JOIN Employees e ON s.emp_id = e.emp_id " &
                                     "JOIN accounts a ON e.user_id = a.user_id " &
                                     "LEFT JOIN Positions p ON e.current_position_id = p.position_id " &
                                     "LEFT JOIN Branches b ON e.current_branch_id = b.branch_id " &
                                     "WHERE s.salary_id = @salaryId"

                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@salaryId", salaryId)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim payslipForm As New PayslipForm()
                        payslipForm.LoadPayslipData(reader)
                        payslipForm.ShowDialog()
                    Else
                        MessageBox.Show("Payslip not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading payslip: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnMonthSelection_Click(sender As Object, e As EventArgs) Handles btnMonthSelection.Click
        If lblTitle.Text <> "PAYROLL" Then
            MessageBox.Show("Month selection is only available in the Payroll section.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim monthForm As New MonthSelectionForm()
        If monthForm.ShowDialog() = DialogResult.OK Then
            LoadPayrollByMonth(monthForm.SelectedYear, monthForm.SelectedMonth)
        End If
    End Sub

    Private Sub LoadPayrollByMonth(year As Integer, month As Integer)
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                Dim empId As Integer = GetEmployeeId(currentUserId)

                If empId = -1 Then
                    MessageBox.Show("Employee record not found for current user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                Dim query As String = "SELECT salary_id, pay_date, rate_used, status, from_date, to_date " &
                                     "FROM Salaries " &
                                     "WHERE emp_id = @empId " &
                                     "AND YEAR(pay_date) = @year " &
                                     "AND MONTH(pay_date) = @month " &
                                     "ORDER BY pay_date DESC"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@empId", empId)
                cmd.Parameters.AddWithValue("@year", year)
                cmd.Parameters.AddWithValue("@month", month)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                dgvOrders.DataSource = dt

                If dgvOrders.Columns.Count > 0 Then
                    dgvOrders.Columns("salary_id").HeaderText = "Salary ID"
                    dgvOrders.Columns("pay_date").HeaderText = "Pay Date"
                    dgvOrders.Columns("rate_used").HeaderText = "Rate (₱)"
                    dgvOrders.Columns("status").HeaderText = "Status"
                    dgvOrders.Columns("from_date").HeaderText = "From Date"
                    dgvOrders.Columns("to_date").HeaderText = "To Date"

                    dgvOrders.Columns("rate_used").DefaultCellStyle.Format = "N2"
                    dgvOrders.Columns("rate_used").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If

                lblOrders.Text = $"PAYSLIPS - {month}/{year}"
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading payroll by month: " & ex.Message)
        End Try
    End Sub

    Private Sub ResetButtonColors()
        btnDashboard.BackColor = Color.Black
        btnViewOrders.BackColor = Color.Black
        btnProcessReturns.BackColor = Color.Black
        btnAccountManagement.BackColor = Color.Black
        btnPayroll.BackColor = Color.Black
    End Sub

    ' New function to link employee to user account
    Private Function LinkEmployeeToUser(empId As Integer, userId As Integer) As Boolean
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "UPDATE employees SET user_id = @userId WHERE emp_id = @empId"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@userId", userId)
                cmd.Parameters.AddWithValue("@empId", empId)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected > 0
            End Using
        Catch ex As Exception
            MessageBox.Show("Error linking employee to user account: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ' New function to unlink employee from user account
    Private Function UnlinkEmployeeFromUser(empId As Integer) As Boolean
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "UPDATE employees SET user_id = NULL WHERE emp_id = @empId"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@empId", empId)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected > 0
            End Using
        Catch ex As Exception
            MessageBox.Show("Error unlinking employee from user account: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ' New function to get employee details including user account info
    Private Function GetEmployeeDetails(empId As Integer) As DataRow
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT e.*, a.username, a.email as account_email, a.role " &
                                     "FROM employees e " &
                                     "LEFT JOIN accounts a ON e.user_id = a.user_id " &
                                     "WHERE e.emp_id = @empId"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@empId", empId)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error getting employee details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Private Sub lblLogout_Click(sender As Object, e As EventArgs) Handles lblLogout.Click
        Me.Hide()
        account_page.Show()
    End Sub

    Private Sub lblLogout_MouseEnter(sender As Object, e As EventArgs) Handles lblLogout.MouseEnter
        lblLogout.BackColor = Color.White
        lblLogout.ForeColor = Color.Black
        lblLogout.Font = New Font(lblLogout.Font, FontStyle.Underline)
    End Sub

    Private Sub lblLogout_MouseLeave(sender As Object, e As EventArgs) Handles lblLogout.MouseLeave
        lblLogout.BackColor = Color.Transparent
        lblLogout.ForeColor = Color.White
        lblLogout.Font = New Font(lblLogout.Font, FontStyle.Regular)
    End Sub

    Private Sub dgvOrders_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrders.CellDoubleClick

        If e.RowIndex >= 0 Then
            ViewOrderDetails(Nothing, Nothing)
        End If
    End Sub

    Private Sub dgvOrders_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvOrders.CellFormatting

        If e.ColumnIndex >= 0 AndAlso dgvOrders.Columns(e.ColumnIndex).Name = "order_status" AndAlso e.Value IsNot Nothing Then
            Dim status As String = e.Value.ToString().ToLower()

            Select Case status
                Case "pending"
                    e.CellStyle.BackColor = Color.LightYellow
                    e.CellStyle.ForeColor = Color.DarkOrange
                Case "processing"
                    e.CellStyle.BackColor = Color.LightBlue
                    e.CellStyle.ForeColor = Color.DarkBlue
                Case "shipped"
                    e.CellStyle.BackColor = Color.LightGreen
                    e.CellStyle.ForeColor = Color.DarkGreen
                Case "delivered"
                    e.CellStyle.BackColor = Color.LightGreen
                    e.CellStyle.ForeColor = Color.Green
                Case "cancelled"
                    e.CellStyle.BackColor = Color.LightCoral
                    e.CellStyle.ForeColor = Color.DarkRed
                Case Else
                    e.CellStyle.BackColor = Color.White
                    e.CellStyle.ForeColor = Color.Black
            End Select
        End If


        If e.ColumnIndex >= 0 AndAlso dgvOrders.Columns(e.ColumnIndex).Name = "status" AndAlso e.Value IsNot Nothing Then
            Dim status As String = e.Value.ToString().ToLower()

            Select Case status
                Case "paid"
                    e.CellStyle.BackColor = Color.LightGreen
                    e.CellStyle.ForeColor = Color.DarkGreen
                Case "pending"
                    e.CellStyle.BackColor = Color.LightYellow
                    e.CellStyle.ForeColor = Color.DarkOrange
                Case "overdue"
                    e.CellStyle.BackColor = Color.LightCoral
                    e.CellStyle.ForeColor = Color.DarkRed
                Case Else
                    e.CellStyle.BackColor = Color.White
                    e.CellStyle.ForeColor = Color.Black
            End Select
        End If
    End Sub

End Class