Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Reflection

'// USE REGIONS FOR ORGANIZATIONNN!!
Public Class admin_page

    Public connStr As String
    Public currentUserId As Integer
    Public currentUsername As String

    Private profitsReportActive As Boolean = False
    Private profitsReportFromDate As Date = New DateTime(Now.Year, Now.Month, 1)
    Private profitsReportToDate As Date = DateTime.Now

#Region "Form"

    Sub allPanelsClose()
        dashPanel.Hide()
        ACpanel.Hide()
        MR_panel.Hide()
        EXpanel.Hide()
        add_SUP_panel.Hide()
        SALpanel.Hide()
        ORPanel.Hide()
        RETPanel.Hide()
        Vpanel.Hide()
        REPmenu.Hide()
        REPpanel.Hide()
        home_flow.Hide()
        manage_panel.Hide()
        emp_panel.Hide()
        BR_panel.Hide()
        btnBack.Hide()
        home_panel.Hide()

    End Sub

    Private Sub lblLogOut_Click(sender As Object, e As EventArgs) Handles lblLogOut.Click
        Me.Hide()
        account_page.Show()
    End Sub

    Private Sub admin_page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        allPanelsClose()
        LoadAdminDashboard()
        btnREPBack.BringToFront()
    End Sub
#End Region

#Region "Dashboard"

    Private Sub CountStats()
        Using conn As New MySqlConnection(connStr)
            conn.Open()

            ' Count branches
            Dim cmdBranches As New MySqlCommand("SELECT COUNT(*) FROM branches", conn)
            Dim branchCount As Integer = Convert.ToInt32(cmdBranches.ExecuteScalar())
            lblCountBranches.Text = branchCount

            ' Count models
            Dim cmdModels As New MySqlCommand("SELECT COUNT(*) FROM models", conn)
            Dim modelCount As Integer = Convert.ToInt32(cmdModels.ExecuteScalar())
            lblCountModels.Text = modelCount

            'Count Employees
            Dim cmdEmp As New MySqlCommand("SELECT COUNT(*) FROM employees", conn)
            Dim empcount As Integer = Convert.ToInt32(cmdEmp.ExecuteScalar())
            lblEmployeesCount.Text = empcount
        End Using
    End Sub

    Private Sub LoadAdminDashboard()
        CountStats()
        home_panel.Show()
        dashPanel.Controls.Clear()
        dashPanel.Padding = New Padding(20)
        dashPanel.AutoScroll = True
        dashPanel.Show()

        Dim yOffset As Integer = 10
        Dim cardWidth As Integer = 200
        Dim cardHeight As Integer = 70
        Dim xCard As Integer = 10
        Dim yCard As Integer = yOffset
        Dim cardSpacing As Integer = 20
        Dim fontTitle As New Font("Futura-Medium", 10, FontStyle.Bold)
        Dim fontValue As New Font("Futura-Medium", 16, FontStyle.Bold)

        ' 1. Total Sales (All Time)
        Dim totalSales As Decimal = 0
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT SUM((oi.unit_price * oi.quantity) * (1 - IFNULL(v.percent_sale, 0)/100)) FROM orders o JOIN order_items oi ON o.order_id = oi.order_id LEFT JOIN vouchers v ON o.voucher_code = v.voucher_code WHERE o.order_status = 'delivered'", conn)
            totalSales = Convert.ToDecimal(cmd.ExecuteScalar())
        End Using
        Dim cardSales As New Panel With {.BackColor = Color.Black, .BorderStyle = BorderStyle.FixedSingle, .Location = New Point(xCard, yCard), .Size = New Size(cardWidth, cardHeight)}
        cardSales.Controls.Add(New Label With {.Text = "Total Sales", .Font = fontTitle, .Location = New Point(10, 5), .AutoSize = True, .ForeColor = Color.White})
        cardSales.Controls.Add(New Label With {.Text = $"₱{totalSales:N2}", .Font = fontValue, .Location = New Point(10, 30), .AutoSize = True, .ForeColor = Color.Red})
        dashPanel.Controls.Add(cardSales)
        xCard += cardWidth + cardSpacing

        ' 2. Total Orders
        Dim totalOrders As Integer = 0
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT COUNT(*) FROM orders", conn)
            totalOrders = Convert.ToInt32(cmd.ExecuteScalar())
        End Using
        Dim cardOrders As New Panel With {.BackColor = Color.Black, .BorderStyle = BorderStyle.FixedSingle, .Location = New Point(xCard, yCard), .Size = New Size(cardWidth, cardHeight)}
        cardOrders.Controls.Add(New Label With {.Text = "Total Orders", .Font = fontTitle, .Location = New Point(10, 5), .AutoSize = True, .ForeColor = Color.White})
        cardOrders.Controls.Add(New Label With {.Text = totalOrders.ToString(), .Font = fontValue, .Location = New Point(10, 30), .AutoSize = True, .ForeColor = Color.Red})
        dashPanel.Controls.Add(cardOrders)
        xCard += cardWidth + cardSpacing

        ' 3. Total Customers
        Dim totalCustomers As Integer = 0
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT COUNT(*) FROM accounts WHERE role = 'customer'", conn)
            totalCustomers = Convert.ToInt32(cmd.ExecuteScalar())
        End Using
        Dim cardCustomers As New Panel With {.BackColor = Color.Black, .BorderStyle = BorderStyle.FixedSingle, .Location = New Point(xCard, yCard), .Size = New Size(cardWidth, cardHeight)}
        cardCustomers.Controls.Add(New Label With {.Text = "Total Customers", .Font = fontTitle, .Location = New Point(10, 5), .AutoSize = True, .ForeColor = Color.White})
        cardCustomers.Controls.Add(New Label With {.Text = totalCustomers.ToString(), .Font = fontValue, .Location = New Point(10, 30), .AutoSize = True, .ForeColor = Color.Red})
        dashPanel.Controls.Add(cardCustomers)
        xCard += cardWidth + cardSpacing

        ' 4. Net Profit (All Time)
        Dim totalExpenses As Decimal = 0
        Dim operationalExpenses As Decimal = 0
        Dim motorcycleCosts As Decimal = 0
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            ' Get operational expenses
            Dim cmd As New MySqlCommand("SELECT SUM(fee) FROM expenses", conn)
            operationalExpenses = Convert.ToDecimal(cmd.ExecuteScalar())

            ' Get motorcycle costs (cost_price * quantity sold)
            Dim cmdMotorcycle As New MySqlCommand("SELECT SUM(m.cost_price * oi.quantity) FROM orders o JOIN order_items oi ON o.order_id = oi.order_id JOIN models m ON oi.model_id = m.model_id WHERE o.order_status = 'delivered'", conn)
            motorcycleCosts = Convert.ToDecimal(cmdMotorcycle.ExecuteScalar())

            totalExpenses = operationalExpenses + motorcycleCosts
        End Using
        Dim netProfit As Decimal = totalSales - totalExpenses
        Dim cardProfit As New Panel With {.BackColor = Color.Black, .BorderStyle = BorderStyle.FixedSingle, .Location = New Point(450, 100), .Size = New Size(cardWidth, cardHeight)}
        cardProfit.Controls.Add(New Label With {.Text = "Net Profit", .Font = fontTitle, .Location = New Point(10, 5), .AutoSize = True, .ForeColor = Color.White})
        cardProfit.Controls.Add(New Label With {.Text = $"₱{netProfit:N2}", .Font = fontValue, .Location = New Point(10, 30), .AutoSize = True, .ForeColor = Color.Red})
        dashPanel.Controls.Add(cardProfit)

        xCard = 10
        yCard += cardHeight + cardSpacing

        ' 5. Total Expenses
        Dim cardExp As New Panel With {.BackColor = Color.Black, .BorderStyle = BorderStyle.FixedSingle, .Location = New Point(xCard, yCard), .Size = New Size(cardWidth, cardHeight)}
        cardExp.Controls.Add(New Label With {.Text = "Total Expenses", .Font = fontTitle, .Location = New Point(10, 5), .AutoSize = True, .ForeColor = Color.White})
        cardExp.Controls.Add(New Label With {.Text = $"₱{totalExpenses:N2}", .Font = fontValue, .Location = New Point(10, 30), .AutoSize = True, .ForeColor = Color.Red})
        dashPanel.Controls.Add(cardExp)
        xCard += cardWidth + cardSpacing

        ' 6. Inventory Value
        Dim inventoryValue As Decimal = 0
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT SUM(bm.stock * m.cost_price) FROM branch_model bm JOIN models m ON bm.model_id = m.model_id", conn)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                inventoryValue = Convert.ToDecimal(result)
            End If
        End Using
        Dim cardInv As New Panel With {.BackColor = Color.Black, .BorderStyle = BorderStyle.FixedSingle, .Location = New Point(xCard, yCard), .Size = New Size(cardWidth, cardHeight)}
        cardInv.Controls.Add(New Label With {.Text = "Inventory Cost", .Font = fontTitle, .Location = New Point(10, 5), .AutoSize = True, .ForeColor = Color.White})
        cardInv.Controls.Add(New Label With {.Text = $"₱{inventoryValue:N2}", .Font = fontValue, .Location = New Point(10, 30), .AutoSize = True, .ForeColor = Color.Red})
        dashPanel.Controls.Add(cardInv)
        xCard += cardWidth + cardSpacing

        ' 10. Refresh Button
        Dim btnRefresh As New Button With {
            .Text = "REFRESH" & vbCrLf & "DATA",
            .Font = New Font("Futura-Medium", 12, FontStyle.Bold),
            .BackColor = Color.Black,
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Location = New Point(672, 10),
            .Size = New Size(120, 160)
        }
        AddHandler btnRefresh.Click, Sub() LoadAdminDashboard()
        dashPanel.Controls.Add(btnRefresh)
    End Sub
    Private Sub lblDashboard_Click(sender As Object, e As EventArgs) Handles lblDashboard.Click
        allPanelsClose()
        LoadAdminDashboard()
        lblStatus.Text = "ADMIN HOME PAGE"
    End Sub

#End Region

#Region "Management"

#Region "Buttons"
    Private Sub btnVback_Click(sender As Object, e As EventArgs) Handles btnVback.Click
        Vpanel.Hide()
        manage_panel.Show()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        manage_panel.Hide()
        LoadRoles()
        LoadAccounts()
        ACpanel.Show()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        allPanelsClose()
        lblStatus.Text = "YOUR OPTIONS:"
        manage_panel.Show()
        home_flow.Hide()
        btnBack.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        manage_panel.Hide()

        LoadBranches()

        BR_panel.Show()
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        manage_panel.Hide()
        LoadComboBoxes()
        LoadEmployees()
        emp_panel.Show()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        manage_panel.Hide()

        LoadMotorcycles()
        LoadMRComboBoxes()
        LoadSuppliers()
        MR_panel.Show()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        manage_panel.Hide()
        LoadExpenseCombos()
        LoadExpenses()
        LoadBranchesToCboEXBranch()
        EXpanel.Show()
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        manage_panel.Hide()

        LoadOrderStatus()
        LoadPaymentStatus()
        LoadPaymentOptions()
        LoadOrders()
        ORPanel.Show()

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        manage_panel.Hide()

        LoadReturns()
        LoadReturnStatuses()
        LoadReturnConditions()
        RETPanel.Show()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        manage_panel.Hide()

        LoadSalaries()
        LoadSelectedSalaryDetails()
        LoadBranches_sal()
        LoadSalaryStatus()
        SALpanel.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        manage_panel.Hide()
        LoadSuppliers()
        add_SUP_panel.Show()

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        manage_panel.Hide()
        LoadVoucherStatusCombo()
        LoadVouchers()
        Vpanel.Show()

    End Sub

    Private Sub btnBRBack_MouseClick(sender As Object, e As MouseEventArgs) Handles btnBRBack.MouseClick
        BR_panel.Hide()
        manage_panel.Show()
    End Sub

    Private Sub btnMRBack_MouseClick(sender As Object, e As MouseEventArgs) Handles btnMRBack.MouseClick
        MR_panel.Hide()
        manage_panel.Show()

    End Sub

    Private Sub btnRETBack_MouseClick(sender As Object, e As MouseEventArgs) Handles btnRETBack.MouseClick
        manage_panel.Show()
        RETPanel.Hide()

    End Sub

    Private Sub btnSALBack_MouseClick(sender As Object, e As MouseEventArgs) Handles btnSALBack.MouseClick
        manage_panel.Show()
        SALpanel.Hide()

    End Sub

    Private Sub btnSUPBACK_MouseClick(sender As Object, e As MouseEventArgs) Handles btnSUPBACK.MouseClick
        manage_panel.Show()
        add_SUP_panel.Hide()

    End Sub

    Private Sub btnEMPBack_MouseClick(sender As Object, e As MouseEventArgs) Handles btnEMPBack.MouseClick
        manage_panel.Show()
        emp_panel.Hide()

    End Sub

    Private Sub btnORBack_MouseClick(sender As Object, e As MouseEventArgs) Handles btnORBack.MouseClick
        manage_panel.Show()
        ORPanel.Hide()

    End Sub

#End Region

#Region "Motorcycles"
    Private uploadedMotorImage As Byte()

    Private Sub btnMRUpload_Click(sender As Object, e As EventArgs) Handles btnMRUpload.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            If ofd.ShowDialog() = DialogResult.OK Then
                uploadedMotorImage = File.ReadAllBytes(ofd.FileName)
                pbMotor.Image = Image.FromFile(ofd.FileName)
                MessageBox.Show("Motorcycle photo uploaded.", "Success", MessageBoxButtons.OK)
            End If
        End Using
    End Sub

    Private Sub btnMRInsert_Click(sender As Object, e As EventArgs) Handles btnMRInsert.Click
        If Not ValidateMotorFields() Then Exit Sub
        If uploadedMotorImage Is Nothing Then
            MessageBox.Show("Please upload a motorcycle photo.", "Missing Photo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            Dim modelId As Integer
            Dim branchId As Integer = Convert.ToInt32(cboMRBranch.SelectedValue)
            Dim quantity As Integer = Integer.Parse(txtMRStock.Text)
            Dim costPrice As Decimal = Decimal.Parse(txtMRCost.Text)
            Dim totalCost As Decimal = costPrice * quantity
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim transaction = conn.BeginTransaction() 'MP1 - Product CRUD
                Try

                    ' Insert into Models
                    Dim insertModelquery As String = "INSERT INTO models (model_name, type, brand, year_model, transmission, supplier_id, price, cost_price, photo) VALUES (@model_name, @type, @brand, @year_model, @transmission, @supplier_id, @price, @cost_price, @photo); SELECT LAST_INSERT_ID();"
                    Using cmd As New MySqlCommand(insertModelquery, conn, transaction)
                        cmd.Parameters.AddWithValue("@model_name", txtMRName.Text.Trim())
                        cmd.Parameters.AddWithValue("@type", cboMRType.SelectedItem.ToString())
                        cmd.Parameters.AddWithValue("@brand", txtMRBRand.Text.Trim())
                        cmd.Parameters.AddWithValue("@year_model", txtMRYear.Text.Trim())
                        cmd.Parameters.AddWithValue("@transmission", cboMRTrans.SelectedItem.ToString())
                        cmd.Parameters.AddWithValue("@supplier_id", Convert.ToInt32(cboMRSupplier.SelectedValue))
                        cmd.Parameters.AddWithValue("@price", Decimal.Parse(txtMRSelling.Text))
                        cmd.Parameters.AddWithValue("@cost_price", costPrice)
                        cmd.Parameters.AddWithValue("@photo", uploadedMotorImage)
                        modelId = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using
                    ' Insert into Branch_Model
                    Dim insertBranchModelQuery As String = "INSERT INTO branch_model (branch_id, model_id, stock) VALUES (@branch_id, @model_id, @stock)"
                    Using cmd As New MySqlCommand(insertBranchModelQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@branch_id", branchId)
                        cmd.Parameters.AddWithValue("@model_id", modelId)
                        cmd.Parameters.AddWithValue("@stock", quantity)
                        cmd.ExecuteNonQuery()
                    End Using
                    ' Insert into expenses
                    Dim insertExpenseQuery As String = "INSERT INTO expenses (branch_id, expense_type, fee, status, from_date, due_date) VALUES (@branch, 'Supplies', @fee, 'Paid', @date, @date)"
                    Using cmd As New MySqlCommand(insertExpenseQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@branch", branchId)
                        cmd.Parameters.AddWithValue("@fee", totalCost)
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.Date)
                        cmd.ExecuteNonQuery()
                    End Using
                    transaction.Commit()
                    MessageBox.Show("Motorcycle added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearMotorFields()
                    LoadMotorcycles()
                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show("Error saving motorcycle: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving motorcycle: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnMRUpdate_Click(sender As Object, e As EventArgs) Handles btnMRUpdate.Click
        If String.IsNullOrWhiteSpace(txtMRID.Text) Then
            MessageBox.Show("Please select a motorcycle to update.")
            Exit Sub
        End If

        If Not ValidateMotorFields() Then Exit Sub

        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                ' Update Models table
                Dim query As String = "UPDATE models SET model_name=@name, brand=@brand, year_model=@year, type=@type, transmission=@trans, price=@price, cost_price=@cost, supplier_id=@supplier, photo=@photo WHERE model_id=@id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", txtMRName.Text.Trim())
                    cmd.Parameters.AddWithValue("@brand", txtMRBRand.Text.Trim())
                    cmd.Parameters.AddWithValue("@year", txtMRYear.Text.Trim())
                    cmd.Parameters.AddWithValue("@type", cboMRType.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@trans", cboMRTrans.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@price", Decimal.Parse(txtMRSelling.Text))
                    cmd.Parameters.AddWithValue("@cost", Decimal.Parse(txtMRCost.Text))
                    cmd.Parameters.AddWithValue("@supplier", Convert.ToInt32(cboMRSupplier.SelectedValue))

                    If uploadedMotorImage Is Nothing Then
                        ' Load the existing image from the DB
                        Dim getImageCmd As New MySqlCommand("SELECT photo FROM models WHERE model_id=@id", conn)
                        getImageCmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtMRID.Text))
                        Dim result As Object = getImageCmd.ExecuteScalar()
                        If result IsNot DBNull.Value Then
                            uploadedMotorImage = CType(result, Byte())
                        End If
                    End If

                    cmd.Parameters.AddWithValue("@photo", uploadedMotorImage)
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtMRID.Text))
                    cmd.ExecuteNonQuery()
                End Using

                Dim originalBranchId As Integer = Convert.ToInt32(dgvMR.SelectedRows(0).Cells("branch_id").Value)
                Dim newBranchId As Integer = Convert.ToInt32(cboMRBranch.SelectedValue)
                Dim modelId As Integer = Convert.ToInt32(txtMRID.Text)
                Dim newStock As Integer = Integer.Parse(txtMRStock.Text)
                Dim costPrice As Decimal = Decimal.Parse(txtMRCost.Text)

                ' Check if branch is being changed
                If originalBranchId <> newBranchId Then
                    ' Delete the old branch_model entry
                    Dim deleteOldBranchQuery As String = "DELETE FROM branch_model WHERE model_id=@model_id AND branch_id=@branch_id"
                    Using cmdDeleteOld As New MySqlCommand(deleteOldBranchQuery, conn)
                        cmdDeleteOld.Parameters.AddWithValue("@model_id", modelId)
                        cmdDeleteOld.Parameters.AddWithValue("@branch_id", originalBranchId)
                        cmdDeleteOld.ExecuteNonQuery()
                    End Using

                    ' Insert new branch_model entry
                    Dim insertNewBranchQuery As String = "INSERT INTO branch_model (model_id, branch_id, stock) VALUES (@model_id, @branch_id, @stock)"
                    Using cmdInsertNew As New MySqlCommand(insertNewBranchQuery, conn)
                        cmdInsertNew.Parameters.AddWithValue("@model_id", modelId)
                        cmdInsertNew.Parameters.AddWithValue("@branch_id", newBranchId)
                        cmdInsertNew.Parameters.AddWithValue("@stock", newStock)
                        cmdInsertNew.ExecuteNonQuery()
                    End Using

                    ' Add expense for the new stock in the new branch
                    If newStock > 0 Then
                        Dim additionalCost As Decimal = newStock * costPrice
                        Dim insertExpenseQuery As String = "INSERT INTO expenses (branch_id, expense_type, fee, status, from_date, due_date) VALUES (@branch, 'Supplies', @fee, 'Paid', @date, @date)"
                        Using cmdExpense As New MySqlCommand(insertExpenseQuery, conn)
                            cmdExpense.Parameters.AddWithValue("@branch", newBranchId)
                            cmdExpense.Parameters.AddWithValue("@fee", additionalCost)
                            cmdExpense.Parameters.AddWithValue("@date", DateTime.Now.Date)
                            cmdExpense.ExecuteNonQuery()
                        End Using
                    End If
                Else
                    ' Same branch, just update stock
                    ' Get current stock to calculate the difference
                    Dim currentStock As Integer = 0
                    Dim getCurrentStockQuery As String = "SELECT stock FROM branch_model WHERE model_id=@model_id AND branch_id=@branch_id"
                    Using cmdCurrentStock As New MySqlCommand(getCurrentStockQuery, conn)
                        cmdCurrentStock.Parameters.AddWithValue("@model_id", modelId)
                        cmdCurrentStock.Parameters.AddWithValue("@branch_id", originalBranchId)
                        Dim result = cmdCurrentStock.ExecuteScalar()
                        If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                            currentStock = Convert.ToInt32(result)
                        End If
                    End Using

                    ' Calculate stock difference and cost
                    Dim stockDifference As Integer = newStock - currentStock
                    If stockDifference > 0 Then
                        ' Stock is being increased, calculate cost and add expense
                        Dim additionalCost As Decimal = stockDifference * costPrice

                        ' Insert expense for additional stock
                        Dim insertExpenseQuery As String = "INSERT INTO expenses (branch_id, expense_type, fee, status, from_date, due_date) VALUES (@branch, 'Supplies', @fee, 'Paid', @date, @date)"
                        Using cmdExpense As New MySqlCommand(insertExpenseQuery, conn)
                            cmdExpense.Parameters.AddWithValue("@branch", originalBranchId)
                            cmdExpense.Parameters.AddWithValue("@fee", additionalCost)
                            cmdExpense.Parameters.AddWithValue("@date", DateTime.Now.Date)
                            cmdExpense.ExecuteNonQuery()
                        End Using
                    End If

                    ' Update only the stock for that branch/model
                    Dim updateBranchModelQuery As String = "UPDATE Branch_Model SET stock=@stock WHERE model_id=@model_id AND branch_id=@branch_id"
                    Using cmdBranchModel As New MySqlCommand(updateBranchModelQuery, conn)
                        cmdBranchModel.Parameters.AddWithValue("@stock", newStock)
                        cmdBranchModel.Parameters.AddWithValue("@model_id", modelId)
                        cmdBranchModel.Parameters.AddWithValue("@branch_id", originalBranchId)
                        cmdBranchModel.ExecuteNonQuery()
                    End Using
                End If
            End Using

            MessageBox.Show("Motorcycle updated successfully.")
            ClearMotorFields()
            LoadMotorcycles()

        Catch ex As Exception
            MessageBox.Show("Error updating motorcycle: " & ex.Message)
        End Try
    End Sub

    Private Sub btnMRDelete_Click(sender As Object, e As EventArgs) Handles btnMRDelete.Click
        If String.IsNullOrWhiteSpace(txtMRID.Text) Then
            MessageBox.Show("Please select a motorcycle to delete.")
            Exit Sub
        End If
        If MessageBox.Show("Are you sure you want to delete this motorcycle?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Try
                Using conn As New MySqlConnection(connStr)
                    conn.Open()
                    Dim transaction = conn.BeginTransaction()
                    Try
                        Dim modelId As Integer = Convert.ToInt32(txtMRID.Text)
                        ' Delete from order_items first (due to FK)
                        Dim deleteOrderItemsQuery As String = "DELETE FROM order_items WHERE model_id = @id"
                        Using cmdOrderItems As New MySqlCommand(deleteOrderItemsQuery, conn, transaction)
                            cmdOrderItems.Parameters.AddWithValue("@id", modelId)
                            cmdOrderItems.ExecuteNonQuery()
                        End Using
                        ' Delete from Orders
                        Dim deleteOrdersQuery As String = "DELETE FROM orders WHERE order_id NOT IN (SELECT order_id FROM order_items)"
                        Using cmdOrders As New MySqlCommand(deleteOrdersQuery, conn, transaction)
                            cmdOrders.ExecuteNonQuery()
                        End Using
                        ' Delete from Branch_Model
                        Dim deleteBranchModelQuery As String = "DELETE FROM branch_model WHERE model_id = @id"
                        Using cmdBranchModel As New MySqlCommand(deleteBranchModelQuery, conn, transaction)
                            cmdBranchModel.Parameters.AddWithValue("@id", modelId)
                            cmdBranchModel.ExecuteNonQuery()
                        End Using
                        ' Delete from Cart
                        Dim deleteCartQuery As String = "DELETE FROM cart WHERE model_id = @id"
                        Using cmdCart As New MySqlCommand(deleteCartQuery, conn, transaction)
                            cmdCart.Parameters.AddWithValue("@id", modelId)
                            cmdCart.ExecuteNonQuery()
                        End Using
                        ' Delete from Models
                        Dim deleteModelQuery As String = "DELETE FROM models WHERE model_id = @id"
                        Using cmdModel As New MySqlCommand(deleteModelQuery, conn, transaction)
                            cmdModel.Parameters.AddWithValue("@id", modelId)
                            cmdModel.ExecuteNonQuery()
                        End Using
                        transaction.Commit()
                        MessageBox.Show("Motorcycle deleted successfully.")
                        ClearMotorFields()
                        LoadMotorcycles()
                    Catch ex As Exception
                        transaction.Rollback()
                        MessageBox.Show("Error deleting motorcycle: " & ex.Message)
                    End Try
                End Using
            Catch ex As Exception
                MessageBox.Show("Error deleting motorcycle: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnMRUp_Click(sender As Object, e As EventArgs) Handles btnMRUP.Click
        If dgvMR.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvMR.CurrentRow.Index
            If index > 0 Then
                index -= 1
                dgvMR.ClearSelection()
                dgvMR.Rows(index).Selected = True
                dgvMR.CurrentCell = dgvMR.Rows(index).Cells(0)
                LoadMRFieldsFromRow(index)
            End If
        End If
    End Sub

    Private Sub btnMRDown_Click(sender As Object, e As EventArgs) Handles btnMRDown.Click
        If dgvMR.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvMR.CurrentRow.Index
            If index < dgvMR.Rows.Count - 2 Then ' -1 for new row, -1 more for zero-based
                index += 1
                dgvMR.ClearSelection()
                dgvMR.Rows(index).Selected = True
                dgvMR.CurrentCell = dgvMR.Rows(index).Cells(0)
                LoadMRFieldsFromRow(index)
            End If
        End If
    End Sub

    Private Sub LoadMRFieldsFromRow(rowIndex As Integer)
        Try
            With dgvMR.Rows(rowIndex)
                txtMRID.Text = .Cells("model_id").Value?.ToString()
                txtMRName.Text = .Cells("model_name").Value?.ToString()
                txtMRBRand.Text = .Cells("brand").Value?.ToString()
                txtMRYear.Text = .Cells("year_model").Value?.ToString()
                cboMRType.SelectedItem = .Cells("type").Value?.ToString()
                cboMRTrans.SelectedItem = .Cells("transmission").Value?.ToString()
                txtMRSelling.Text = .Cells("price").Value?.ToString()
                txtMRCost.Text = .Cells("cost_price").Value?.ToString()
                txtMRStock.Text = .Cells("stock").Value?.ToString()

                ' Set Supplier by value (ID)
                Dim supplierName As String = .Cells("supplier_name").Value?.ToString()
                If Not String.IsNullOrEmpty(supplierName) Then
                    For i As Integer = 0 To cboMRSupplier.Items.Count - 1
                        Dim item As DataRowView = DirectCast(cboMRSupplier.Items(i), DataRowView)
                        If item("supplier_name").ToString() = supplierName Then
                            cboMRSupplier.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End If

                ' Set Branch by value (ID)
                Dim branchName As String = .Cells("branch_name").Value?.ToString()
                If Not String.IsNullOrEmpty(branchName) Then
                    For i As Integer = 0 To cboMRBranch.Items.Count - 1
                        Dim item As DataRowView = DirectCast(cboMRBranch.Items(i), DataRowView)
                        If item("branch_name").ToString() = branchName Then
                            cboMRBranch.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End If

                ' Load BLOB image
                If Not IsDBNull(.Cells("photo").Value) Then
                    Try
                        Dim imageData As Byte() = CType(.Cells("photo").Value, Byte())
                        Using ms As New MemoryStream(imageData)
                            pbMotor.Image = Image.FromStream(ms)
                        End Using
                    Catch ex As Exception
                        pbMotor.Image = Nothing
                        MessageBox.Show("Error loading motorcycle image: " & ex.Message, "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Try
                Else
                    pbMotor.Image = Nothing
                End If
            End With
        Catch ex As Exception
            MessageBox.Show("Error loading motorcycle fields: " & ex.Message)
        End Try
    End Sub

    Private Sub ClearMotorFields()
        txtMRID.Clear()
        txtMRName.Clear()
        txtMRBRand.Clear()
        txtMRYear.Clear()
        cboMRType.SelectedIndex = -1
        cboMRTrans.SelectedIndex = -1
        txtMRSelling.Clear()
        txtMRCost.Clear()
        txtMRStock.Clear()
        cboMRSupplier.SelectedIndex = -1
        cboMRBranch.SelectedIndex = -1
        pbMotor.Image = Nothing
        uploadedMotorImage = Nothing
    End Sub

    Private Function ValidateMotorFields() As Boolean
        If String.IsNullOrWhiteSpace(txtMRName.Text) OrElse
       String.IsNullOrWhiteSpace(txtMRYear.Text) OrElse
       String.IsNullOrWhiteSpace(txtMRBRand.Text) OrElse
       cboMRType.SelectedItem Is Nothing OrElse
       cboMRTrans.SelectedItem Is Nothing OrElse
       String.IsNullOrWhiteSpace(txtMRSelling.Text) OrElse
       String.IsNullOrWhiteSpace(txtMRCost.Text) OrElse
       String.IsNullOrWhiteSpace(txtMRStock.Text) OrElse
       cboMRSupplier.SelectedItem Is Nothing OrElse
       cboMRBranch.SelectedItem Is Nothing Then

            MessageBox.Show("Please complete all fields.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Sub LoadMotorcycles()

        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                Dim query As String = "
                SELECT 
                    m.model_id, 
                    m.model_name, 
                    m.brand, 
                    m.year_model, 
                    m.type, 
                    m.transmission, 
                    m.price, 
                    m.cost_price, 
                    s.supplier_name,
                    bm.branch_id,
                    b.branch_name,
                    bm.stock,
                    m.photo
                FROM models m
                LEFT JOIN suppliers s ON m.supplier_id = s.supplier_id
                LEFT JOIN branch_model bm ON m.model_id = bm.model_id
                LEFT JOIN branches b ON bm.branch_id = b.branch_id
            "


                Dim da As New MySqlDataAdapter(query, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvMR.DataSource = dt ' ✅ Only do this
            End Using


        Catch ex As Exception
            MessageBox.Show("Failed to load motorcycles: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadMRComboBoxes()
        ' Load Branches properly with ID binding
        Using conn As New MySqlConnection(connStr)
            conn.Open()

            ' --- Load Branches ---
            Dim branchDT As New DataTable()
            Dim branchDA As New MySqlDataAdapter("SELECT branch_id, branch_name FROM branches", conn)
            branchDA.Fill(branchDT)

            cboMRBranch.DataSource = branchDT
            cboMRBranch.DisplayMember = "branch_name"
            cboMRBranch.ValueMember = "branch_id"

            ' --- Load Suppliers ---
            Dim supplierDT As New DataTable()
            Dim supplierDA As New MySqlDataAdapter("SELECT supplier_id, supplier_name FROM suppliers", conn)
            supplierDA.Fill(supplierDT)

            cboMRSupplier.DataSource = supplierDT
            cboMRSupplier.DisplayMember = "supplier_name"
            cboMRSupplier.ValueMember = "supplier_id"
        End Using

        ' --- Motorcycle Types ---
        cboMRType.Items.Clear()
        cboMRType.Items.AddRange({"Scooter", "Sportbike", "Off-road", "Electric", "Moped", "Other"})

        ' --- Transmissions ---
        cboMRTrans.Items.Clear()
        cboMRTrans.Items.AddRange({"Manual", "Automatic", "CVT"})

        cboMRBranch.SelectedIndex = -1
        cboMRSupplier.SelectedIndex = -1
        cboMRType.SelectedIndex = -1
        cboMRTrans.SelectedIndex = -1
    End Sub

    Private Sub btnMRSearch_Click(sender As Object, e As EventArgs) Handles btnMRSearch.Click
        Dim searchTerm As String = txtMRSearch.Text.Trim()
        Dim query As String = "SELECT m.model_id, m.model_name, m.brand, m.year_model, m.type, m.transmission, m.price, m.cost_price, s.supplier_name, bm.branch_id, b.branch_name, bm.stock, m.photo FROM models m LEFT JOIN suppliers s ON m.supplier_id = s.supplier_id LEFT JOIN branch_model bm ON m.model_id = bm.model_id LEFT JOIN branches b ON bm.branch_id = b.branch_id WHERE m.model_id = @id OR m.model_name LIKE @term OR m.brand LIKE @term OR m.year_model LIKE @term OR m.type LIKE @term OR m.transmission LIKE @term OR s.supplier_name LIKE @term ORDER BY m.model_id LIMIT 50"
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                Dim idVal As Integer
                If Integer.TryParse(searchTerm, idVal) Then
                    cmd.Parameters.AddWithValue("@id", idVal)
                Else
                    cmd.Parameters.AddWithValue("@id", -1)
                End If
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        dgvMR.DataSource = dt
    End Sub

    Private Sub dgvMR_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMR.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        dgvMR.ClearSelection()
        dgvMR.Rows(e.RowIndex).Selected = True
        dgvMR.CurrentCell = dgvMR.Rows(e.RowIndex).Cells(e.ColumnIndex)
        LoadMRFieldsFromRow(e.RowIndex)
    End Sub

#End Region

#Region "Branches (CHECK!)"

    Private uploadedImage As Byte() = Nothing 'para sa BLOB haynako

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Image Files (*.jpg;*.png;*.jpeg)|*.jpg;*.png;*.jpeg"

            If ofd.ShowDialog() = DialogResult.OK Then
                uploadedImage = File.ReadAllBytes(ofd.FileName)
                pbBranch.Image = Image.FromFile(ofd.FileName)
                MessageBox.Show("Photo uploaded successfully.", "Upload", MessageBoxButtons.OK)
            End If
        End Using
    End Sub

    Private Sub LoadSelectedBranchDetails()
        If dgvBR.CurrentRow IsNot Nothing Then
            Dim row As DataGridViewRow = dgvBR.CurrentRow

            txtBRName.Text = row.Cells("branch_name").Value.ToString()
            txtBRAddress.Text = row.Cells("address").Value.ToString()
            txtBRPhone.Text = row.Cells("phone_number").Value.ToString()

            ' Load image if available
            If Not IsDBNull(row.Cells("photo").Value) Then
                Dim photoData As Byte() = CType(row.Cells("photo").Value, Byte())
                Using ms As New MemoryStream(photoData)
                    pbBranch.Image = Image.FromStream(ms)
                End Using
            Else
                pbBranch.Image = Nothing
            End If
        End If
    End Sub

    Private Sub btnBRInsert_Click(sender As Object, e As EventArgs) Handles btnBRInsert.Click

        If String.IsNullOrWhiteSpace(txtBRName.Text) OrElse
           String.IsNullOrWhiteSpace(txtBRAddress.Text) OrElse
           String.IsNullOrWhiteSpace(txtBRPhone.Text) Then
            MessageBox.Show("Please fill out all fields.", "Missing Info", MessageBoxButtons.OK)
            Return
        End If

        If uploadedImage Is Nothing Then
            MessageBox.Show("Please upload a photo before submitting.", "Missing Photo", MessageBoxButtons.OK)
            Return
        End If

        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                Dim query As String = "INSERT INTO branches (branch_name, address, phone_number, photo) VALUES (@name, @address, @phone, @photo)"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", txtBRName.Text.Trim())
                    cmd.Parameters.AddWithValue("@address", txtBRAddress.Text.Trim())
                    cmd.Parameters.AddWithValue("@phone", txtBRPhone.Text.Trim())

                    If uploadedImage IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@photo", uploadedImage)
                    Else
                        cmd.Parameters.AddWithValue("@photo", DBNull.Value)
                    End If

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Branch added successfully!", "Success", MessageBoxButtons.OK)
            txtBRName.Clear()
            txtBRAddress.Clear()
            txtBRPhone.Clear()
            uploadedImage = Nothing
            LoadBranches()
        Catch ex As Exception
            MessageBox.Show("Error adding branch: " & ex.Message, "Database Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btnBRUpdate_Click(sender As Object, e As EventArgs) Handles btnBRUpdate.Click
        If dgvBR.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a branch to update.")
            Return
        End If

        Dim selectedRow As DataGridViewRow = dgvBR.SelectedRows(0)
        Dim branchId As Integer = Convert.ToInt32(selectedRow.Cells("branch_id").Value)

        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                Dim query As String
                If uploadedImage IsNot Nothing Then
                    query = "UPDATE branches SET branch_name=@name, address=@address, phone_number=@phone, photo=@photo WHERE branch_id=@id"
                Else
                    query = "UPDATE branches SET branch_name=@name, address=@address, phone_number=@phone WHERE branch_id=@id"
                End If

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", txtBRName.Text.Trim())
                    cmd.Parameters.AddWithValue("@address", txtBRAddress.Text.Trim())
                    cmd.Parameters.AddWithValue("@phone", txtBRPhone.Text.Trim())
                    If uploadedImage IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@photo", uploadedImage)
                    End If
                    cmd.Parameters.AddWithValue("@id", branchId)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Branch updated successfully!")
            LoadBranches()
            uploadedImage = Nothing ' reset
        Catch ex As Exception
            MessageBox.Show("Error updating branch: " & ex.Message)
        End Try
    End Sub

    Private Sub btnBRDelete_Click(sender As Object, e As EventArgs) Handles btnBRDelete.Click
        If dgvBR.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a branch to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim branchId As Integer = CInt(dgvBR.SelectedRows(0).Cells("branch_id").Value)

#Region "TRANSACTION PART"
        'SEQUENCE: Orders -> Expenses -> Branch_Model -> Employees (to NULL) -> Branches

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim transaction = conn.BeginTransaction()
            Try

                Dim deleteOrders As New MySqlCommand("DELETE FROM orders WHERE order_id IN (SELECT order_id FROM order_items WHERE branch_id = @id)", conn, transaction)
                deleteOrders.Parameters.AddWithValue("@id", branchId)
                deleteOrders.ExecuteNonQuery()
                Dim deleteOrderItems As New MySqlCommand("DELETE FROM order_items WHERE branch_id = @id", conn, transaction)
                deleteOrderItems.Parameters.AddWithValue("@id", branchId)
                deleteOrderItems.ExecuteNonQuery()
                Dim deleteExpenses As New MySqlCommand("DELETE FROM expenses WHERE branch_id = @id", conn, transaction)
                deleteExpenses.Parameters.AddWithValue("@id", branchId)
                deleteExpenses.ExecuteNonQuery()
                Dim deleteBranchModels As New MySqlCommand("DELETE FROM branch_model WHERE branch_id = @id", conn, transaction)
                deleteBranchModels.Parameters.AddWithValue("@id", branchId)
                deleteBranchModels.ExecuteNonQuery()

                Dim updateEmployees As New MySqlCommand("UPDATE employees SET current_branch_id = NULL WHERE current_branch_id = @id", conn, transaction)
                updateEmployees.Parameters.AddWithValue("@id", branchId)
                updateEmployees.ExecuteNonQuery()

                Dim deleteBranch As New MySqlCommand("DELETE FROM branches WHERE branch_id = @id", conn, transaction)
                deleteBranch.Parameters.AddWithValue("@id", branchId)
                deleteBranch.ExecuteNonQuery()
                transaction.Commit()
                MessageBox.Show("Branch and related data deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadBranches()
            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Error while deleting branch: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
#End Region

    End Sub


    Private Sub btnBRUP_Click(sender As Object, e As EventArgs) Handles btnBRUP.Click
        If dgvBR.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvBR.CurrentRow.Index
            If index > 0 Then
                dgvBR.ClearSelection()
                dgvBR.Rows(index - 1).Selected = True
                dgvBR.CurrentCell = dgvBR.Rows(index - 1).Cells(0)
                LoadSelectedBranchDetails()
            End If
        End If
    End Sub

    Private Sub btnBRDown_Click(sender As Object, e As EventArgs) Handles btnBRDown.Click
        If dgvBR.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvBR.CurrentRow.Index
            If index < dgvBR.Rows.Count - 2 Then ' -2 because of the "new row" at bottom
                dgvBR.ClearSelection()
                dgvBR.Rows(index + 1).Selected = True
                dgvBR.CurrentCell = dgvBR.Rows(index + 1).Cells(0)
                LoadSelectedBranchDetails()
            End If
        End If
    End Sub

    Private Sub LoadBranches()
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT branch_id, branch_name, address, phone_number, photo FROM branches"
                Using adapter As New MySqlDataAdapter(query, conn)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgvBR.DataSource = dt
                End Using
            End Using
            LoadSelectedBranchDetails()
        Catch ex As Exception
            MessageBox.Show("Error loading branches: " & ex.Message)
        End Try
    End Sub

    Private Sub btnBRSearch_Click(sender As Object, e As EventArgs) Handles btnBRSearch.Click
        Dim searchTerm As String = txtBRSearch.Text.Trim()
        Dim query As String = "SELECT branch_id, branch_name, address, phone_number, photo FROM branches WHERE branch_id = @id OR branch_name LIKE @term OR address LIKE @term OR phone_number LIKE @term ORDER BY branch_id LIMIT 50"
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                Dim idVal As Integer
                If Integer.TryParse(searchTerm, idVal) Then
                    cmd.Parameters.AddWithValue("@id", idVal)
                Else
                    cmd.Parameters.AddWithValue("@id", -1)
                End If
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        dgvBR.DataSource = dt
    End Sub

    Private Sub dgvBR_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBR.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        dgvBR.ClearSelection()
        dgvBR.Rows(e.RowIndex).Selected = True
        dgvBR.CurrentCell = dgvBR.Rows(e.RowIndex).Cells(e.ColumnIndex)
        LoadSelectedBranchDetails()
    End Sub

#End Region

#Region "Suppliers (CHECK!)"
    Private Sub btnSUPSubmit_Click(sender As Object, e As EventArgs) Handles btnSUPSubmit.Click
        If String.IsNullOrWhiteSpace(txtSUPName.Text) Then
            MessageBox.Show("Supplier name is required.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim conn As New MySqlConnection(connStr)

        If Not String.IsNullOrWhiteSpace(txtSUP_ID.Text) Then
            Dim checkQuery As String = "SELECT COUNT(*) FROM suppliers WHERE supplier_id = @id"
            Dim checkCmd As New MySqlCommand(checkQuery, conn)
            checkCmd.Parameters.AddWithValue("@id", txtSUP_ID.Text.Trim)

            Try
                conn.Open()
                Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                conn.Close()

                If exists > 0 Then
                    MessageBox.Show("Supplier ID already exists. Use Update instead.", "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtSUP_ID.Clear()
                    Exit Sub
                End If
            Catch ex As MySqlException
                MessageBox.Show("Error checking ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                conn.Close()
                Exit Sub
            End Try
        End If

        Dim query As String = "INSERT INTO suppliers (supplier_name, contact_person, contact_number, address) VALUES (@name, @contact, @number, @address)"
        Dim cmd As New MySqlCommand(query, conn)

        cmd.Parameters.AddWithValue("@name", txtSUPName.Text.Trim)
        cmd.Parameters.AddWithValue("@contact", txtSUPContact.Text.Trim)
        cmd.Parameters.AddWithValue("@number", txtSUPNumber.Text.Trim)
        cmd.Parameters.AddWithValue("@address", txtSupAddress.Text.Trim)

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            MessageBox.Show("Supplier added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            txtSUPName.Clear()
            txtSUPContact.Clear()
            txtSUPNumber.Clear()
            txtSupAddress.Clear()
            txtSUP_ID.Clear()

        Catch ex As MySqlException
            MessageBox.Show("Error while adding supplier: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
            LoadSuppliers()
        End Try
    End Sub

    Private Sub LoadSuppliers()
        Dim conn As New MySqlConnection(connStr)
        Dim query As String = "SELECT * FROM suppliers"
        Dim adapter As New MySqlDataAdapter(query, conn)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)
            dgvSUP.DataSource = table
        Catch ex As MySqlException
            MessageBox.Show("Error loading suppliers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnSUP_Update_Click(sender As Object, e As EventArgs) Handles btnSUP_Update.Click
        If String.IsNullOrWhiteSpace(txtSUP_ID.Text) Then
            MessageBox.Show("Please enter a Supplier ID to update.", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim conn As New MySqlConnection(connStr)
        Dim checkQuery As String = "SELECT COUNT(*) FROM suppliers WHERE supplier_id = @id"
        Dim checkCmd As New MySqlCommand(checkQuery, conn)
        checkCmd.Parameters.AddWithValue("@id", txtSUP_ID.Text.Trim)

        Try
            conn.Open()
            Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
            conn.Close()

            If exists = 0 Then
                MessageBox.Show("Supplier ID does not exist.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Catch ex As MySqlException
            MessageBox.Show("Error checking ID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            conn.Close()
            Exit Sub
        End Try

        ' Proceed to update
        Dim updateQuery As String = "UPDATE suppliers SET supplier_name = @name, contact_person = @contact, contact_number = @number, address = @address WHERE supplier_id = @id"
        Dim cmd As New MySqlCommand(updateQuery, conn)

        cmd.Parameters.AddWithValue("@name", txtSUPName.Text.Trim)
        cmd.Parameters.AddWithValue("@contact", txtSUPContact.Text.Trim)
        cmd.Parameters.AddWithValue("@number", txtSUPNumber.Text.Trim)
        cmd.Parameters.AddWithValue("@address", txtSupAddress.Text.Trim)
        cmd.Parameters.AddWithValue("@id", txtSUP_ID.Text.Trim)

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            MessageBox.Show("Supplier updated successfully.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Clear fields
            txtSUP_ID.Clear()
            txtSUPName.Clear()
            txtSUPContact.Clear()
            txtSUPNumber.Clear()
            txtSupAddress.Clear()
        Catch ex As MySqlException
            MessageBox.Show("Error while updating supplier: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
            LoadSuppliers()
        End Try
    End Sub

    Private Sub btnSUP_UP_Click(sender As Object, e As EventArgs) Handles btnSUP_UP.Click
        Dim dgv As DataGridView = dgvSUP

        If dgv.CurrentRow IsNot Nothing AndAlso dgv.CurrentRow.Index > 0 Then
            Dim prevRowIndex As Integer = dgv.CurrentRow.Index - 1
            dgv.ClearSelection()
            dgv.Rows(prevRowIndex).Selected = True
            dgv.CurrentCell = dgv.Rows(prevRowIndex).Cells(0)
            LoadSUPFieldsFromRow(prevRowIndex)
        End If
    End Sub

    Private Sub btnSUP_Down_Click(sender As Object, e As EventArgs) Handles btnSUP_Down.Click
        Dim dgv As DataGridView = dgvSUP

        If dgv.CurrentRow IsNot Nothing AndAlso dgv.CurrentRow.Index < dgv.Rows.Count - 2 Then
            Dim nextRowIndex As Integer = dgv.CurrentRow.Index + 1
            dgv.ClearSelection()
            dgv.Rows(nextRowIndex).Selected = True
            dgv.CurrentCell = dgv.Rows(nextRowIndex).Cells(0)
            LoadSUPFieldsFromRow(nextRowIndex)
        End If
    End Sub

    Private Sub LoadSUPFieldsFromRow(rowIndex As Integer)
        Try
            With dgvSUP.Rows(rowIndex)
                txtSUP_ID.Text = .Cells("supplier_id").Value?.ToString() ' <-- Add this line
                txtSUPName.Text = .Cells("supplier_name").Value?.ToString()
                txtSUPContact.Text = .Cells("contact_person").Value?.ToString()
                txtSUPNumber.Text = .Cells("contact_number").Value?.ToString()
                txtSupAddress.Text = .Cells("address").Value?.ToString()
            End With
        Catch ex As Exception
            MessageBox.Show("Error loading supplier data: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSUP_Delete_Click(sender As Object, e As EventArgs) Handles btnSUP_Delete.Click
        Dim conn As New MySqlConnection(connStr)
        Dim supplierId As String = txtSUP_ID.Text.Trim()

        If String.IsNullOrEmpty(supplierId) Then
            MessageBox.Show("Please select a supplier to delete.")
            Return
        End If

        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo)
        If confirm = DialogResult.Yes Then
            Try

                conn.Open()
                Dim query As String = "DELETE FROM suppliers WHERE supplier_id = @id"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", supplierId)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                conn.Close()

                If rowsAffected > 0 Then
                    MessageBox.Show("Supplier deleted successfully.")
                    LoadSuppliers()
                    txtSUP_ID.Clear()
                    txtSUPName.Clear()
                    txtSUPContact.Clear()
                    txtSUPNumber.Clear()
                    txtSupAddress.Clear()
                Else
                    MessageBox.Show("Supplier not found.")
                End If
            Catch ex As MySqlException
                MessageBox.Show("Error deleting supplier: " & ex.Message)
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End If
    End Sub

    Private Sub btnSUPSearch_Click(sender As Object, e As EventArgs) Handles btnSUPSearch.Click
        Dim searchTerm As String = txtSUPSearch.Text.Trim()
        Dim query As String = "SELECT * FROM suppliers WHERE supplier_id = @id OR supplier_name LIKE @term OR contact_person LIKE @term OR contact_number LIKE @term OR address LIKE @term ORDER BY supplier_id LIMIT 50"
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                Dim idVal As Integer
                If Integer.TryParse(searchTerm, idVal) Then
                    cmd.Parameters.AddWithValue("@id", idVal)
                Else
                    cmd.Parameters.AddWithValue("@id", -1)
                End If
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        dgvSUP.DataSource = dt
    End Sub

    Private Sub dgvSUP_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSUP.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        dgvSUP.ClearSelection()
        dgvSUP.Rows(e.RowIndex).Selected = True
        dgvSUP.CurrentCell = dgvSUP.Rows(e.RowIndex).Cells(e.ColumnIndex)
        LoadSUPFieldsFromRow(e.RowIndex)
    End Sub

#End Region

#Region "Employees (CHECK!)"

    Private Sub LoadPositionsToCboEMPPosition()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT position_id, position_name FROM positions"
            Using cmd As New MySqlCommand(query, conn)
                Dim dt As New DataTable()
                dt.Load(cmd.ExecuteReader())
                cboEMPPosition.DataSource = dt
                cboEMPPosition.DisplayMember = "position_name"
                cboEMPPosition.ValueMember = "position_id"
            End Using
        End Using
    End Sub

    Private Sub btnEmpSearch_Click_1(sender As Object, e As EventArgs) Handles btnEmpSearch.Click
        LoadPositionsToCboEMPPosition()
        Dim searchTerm As String = txtEmpSearch.Text.Trim()
        Dim query As String = "
    SELECT 
        e.emp_id,
        a.first_name,
        a.last_name,
        e.birth_date,
        e.gender,
        a.phone_number,
        a.email,
        a.date_created,
        e.employment_status,
        e.user_id,
        a.username,
        a.role as account_role,
        b.branch_name as current_branch,
        p.position_name as current_position,
        p.monthly_rate as current_rate
    FROM employees e
    LEFT JOIN accounts a ON e.user_id = a.user_id
    LEFT JOIN positions p ON e.current_position_id = p.position_id
    WHERE e.emp_id = @id OR a.first_name LIKE @term OR a.last_name LIKE @term OR a.email LIKE @term OR a.phone_number LIKE @term
    ORDER BY e.emp_id LIMIT 50"
        Dim dt As New DataTable
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                Dim idVal As Integer
                If Integer.TryParse(searchTerm, idVal) Then
                    cmd.Parameters.AddWithValue("@id", idVal)
                Else
                    cmd.Parameters.AddWithValue("@id", -1)
                End If
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        dgvEMP.DataSource = dt
    End Sub

    Private Sub LoadComboBoxes()

        Using conn As New MySqlConnection(connStr)
            conn.Open()

            Dim branchCmd As New MySqlCommand("SELECT branch_id, branch_name FROM branches", conn)
            Using reader = branchCmd.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)
                cboEMPBranch.DataSource = dt
                cboEMPBranch.DisplayMember = "branch_name"
                cboEMPBranch.ValueMember = "branch_id"
            End Using

            Dim posCmd As New MySqlCommand("SELECT position_id, position_name FROM positions", conn)
            Using reader = posCmd.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)
                cboEMPPosition.DataSource = dt
                cboEMPPosition.DisplayMember = "position_name"
                cboEMPPosition.ValueMember = "position_id"
            End Using

            conn.Close()
        End Using

        cboEMPGender.Items.Clear()
        cboEMPGender.Items.Add("M")
        cboEMPGender.Items.Add("F")

        cboEMPStatus.Items.Clear()
        cboEMPStatus.Items.Add("active")
        cboEMPStatus.Items.Add("on_leave")
        cboEMPStatus.Items.Add("fired")
    End Sub

    Private Sub btnEMPUP_Click(sender As Object, e As EventArgs) Handles btnEMPUP.Click
        If dgvEMP.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvEMP.CurrentRow.Index
            If index > 0 Then
                dgvEMP.ClearSelection()
                dgvEMP.Rows(index - 1).Selected = True
                dgvEMP.CurrentCell = dgvEMP.Rows(index - 1).Cells(0)
                LoadSelectedEmployeeDetails()
            End If
        End If
    End Sub

    Private Sub btnEMPDown_Click(sender As Object, e As EventArgs) Handles btnEMPDown.Click
        If dgvEMP.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvEMP.CurrentRow.Index
            If index < dgvEMP.Rows.Count - 2 Then ' accounting for new row
                dgvEMP.ClearSelection()
                dgvEMP.Rows(index + 1).Selected = True
                dgvEMP.CurrentCell = dgvEMP.Rows(index + 1).Cells(0)
                LoadSelectedEmployeeDetails()
            End If
        End If
    End Sub

    Private Sub LoadEmployees()
        Try
            LoadComboBoxes() ' Load both branch and position combo boxes

            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "
    SELECT 
        e.emp_id,
        a.first_name,
        a.last_name,
        e.birth_date,
        e.gender,
        a.phone_number,
        a.email,
        a.date_created,
        e.employment_status,
        e.user_id,
        a.username,
        a.role as account_role,
        b.branch_name as current_branch,
        p.position_name as current_position,
        p.monthly_rate as current_rate
    FROM employees e
    LEFT JOIN accounts a ON e.user_id = a.user_id
    LEFT JOIN branches b ON e.current_branch_id = b.branch_id
    LEFT JOIN positions p ON e.current_position_id = p.position_id
    ORDER BY e.emp_id
"
                Using adapter As New MySqlDataAdapter(query, conn)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgvEMP.DataSource = dt
                End Using
            End Using
            LoadSelectedEmployeeDetails()
        Catch ex As Exception
            MessageBox.Show("Error loading employees: " & ex.Message)
        End Try
    End Sub



    Private Sub LoadSelectedEmployeeDetails()
        If dgvEMP.CurrentRow IsNot Nothing Then
            Dim row As DataGridViewRow = dgvEMP.CurrentRow

            txtEMPFN.Text = row.Cells("first_name").Value.ToString()
            txtEMPLN.Text = row.Cells("last_name").Value.ToString()
            txtEMPEmail.Text = row.Cells("email").Value.ToString()
            txtEMPPhone.Text = row.Cells("phone_number").Value.ToString()

            ' Set branch and position combo boxes based on current values
            If row.Cells("current_branch").Value IsNot DBNull.Value Then
                cboEMPBranch.Text = row.Cells("current_branch").Value.ToString()
            End If

            If row.Cells("current_position").Value IsNot DBNull.Value Then
                cboEMPPosition.Text = row.Cells("current_position").Value.ToString()
            End If

            cboEMPGender.SelectedItem = row.Cells("gender").Value.ToString()
            cboEMPStatus.SelectedItem = row.Cells("employment_status").Value.ToString()

            dtEMPBDate.Value = Convert.ToDateTime(row.Cells("birth_date").Value)
        End If
    End Sub

    Private Sub btnEMPInsert_Click(sender As Object, e As EventArgs) Handles btnEMPInsert.Click
        ' Ensure combo boxes are loaded
        LoadComboBoxes()
        LoadPositionsToCboEMPPosition()

        ' Debug: Check which field is causing the validation to fail
        Dim missingFields As New List(Of String)

        If String.IsNullOrWhiteSpace(txtEMPFN.Text) Then
            missingFields.Add("First Name")
        End If
        If String.IsNullOrWhiteSpace(txtEMPLN.Text) Then
            missingFields.Add("Last Name")
        End If
        If String.IsNullOrWhiteSpace(txtEMPEmail.Text) Then
            missingFields.Add("Email")
        End If
        If String.IsNullOrWhiteSpace(txtEMPPhone.Text) Then
            missingFields.Add("Phone Number")
        End If
        If cboEMPBranch.SelectedIndex = -1 AndAlso String.IsNullOrWhiteSpace(cboEMPBranch.Text) Then
            missingFields.Add("Branch")
        End If
        If cboEMPPosition.SelectedIndex = -1 AndAlso String.IsNullOrWhiteSpace(cboEMPPosition.Text) Then
            missingFields.Add("Position")
        End If
        If String.IsNullOrWhiteSpace(cboEMPGender.Text) Then
            missingFields.Add("Gender")
        End If
        If String.IsNullOrWhiteSpace(cboEMPStatus.Text) Then
            missingFields.Add("Employment Status")
        End If

        If missingFields.Count > 0 Then
            Dim missingFieldsText As String = String.Join(", ", missingFields)
            Dim debugInfo As String = $"Missing Fields: {missingFieldsText}" & vbCrLf & vbCrLf &
                                     $"Debug Info:" & vbCrLf &
                                     $"Branch SelectedIndex: {cboEMPBranch.SelectedIndex}, Text: '{cboEMPBranch.Text}'" & vbCrLf &
                                     $"Position SelectedIndex: {cboEMPPosition.SelectedIndex}, Text: '{cboEMPPosition.Text}'" & vbCrLf &
                                     $"Gender SelectedIndex: {cboEMPGender.SelectedIndex}, Text: '{cboEMPGender.Text}'" & vbCrLf &
                                     $"Status SelectedIndex: {cboEMPStatus.SelectedIndex}, Text: '{cboEMPStatus.Text}'" & vbCrLf &
                                     $"Branch Items Count: {cboEMPBranch.Items.Count}" & vbCrLf &
                                     $"Position Items Count: {cboEMPPosition.Items.Count}" & vbCrLf &
                                     $"Gender Items Count: {cboEMPGender.Items.Count}" & vbCrLf &
                                     $"Status Items Count: {cboEMPStatus.Items.Count}"
            MessageBox.Show(debugInfo, "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not txtEMPEmail.Text.Contains("@") OrElse Not txtEMPEmail.Text.Contains(".") Then
            MessageBox.Show("Please enter a valid email address.")
            Return
        End If
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                Dim username As String = txtEMPFN.Text.Trim().ToLower() & "." & txtEMPLN.Text.Trim().ToLower()
                Dim checkQuery As String = "SELECT COUNT(*) FROM accounts WHERE username = @username OR email = @email"
                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@username", username)
                    checkCmd.Parameters.AddWithValue("@email", txtEMPEmail.Text.Trim())
                    Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                    If exists > 0 Then
                        MessageBox.Show("An account with this username or email already exists.", "Duplicate Account", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

#Region "TRANSACTION PART"
                'SEQUENCE: Employees -> Accounts (for payslip)

                Dim transaction = conn.BeginTransaction()
                Try
                    Dim empId As Integer

                    ' First create the account
                    Dim accountQuery As String = "INSERT INTO accounts (username, password, email, address, phone_number, role, date_created, first_name, last_name) VALUES (@username, @password, @email, @address, @phone, 'employee', @date_created, @firstname, @lastname); SELECT LAST_INSERT_ID();"
                    Dim userId As Integer
                    Using cmdAccount As New MySqlCommand(accountQuery, conn, transaction)
                        cmdAccount.Parameters.AddWithValue("@username", txtEMPFN.Text.Trim().ToLower() & "." & txtEMPLN.Text.Trim().ToLower())
                        cmdAccount.Parameters.AddWithValue("@password", "emp123")
                        cmdAccount.Parameters.AddWithValue("@email", txtEMPEmail.Text.Trim())
                        cmdAccount.Parameters.AddWithValue("@address", "Employee Address")
                        cmdAccount.Parameters.AddWithValue("@phone", txtEMPPhone.Text.Trim())
                        cmdAccount.Parameters.AddWithValue("@date_created", Now())
                        cmdAccount.Parameters.AddWithValue("@firstname", txtEMPFN.Text.Trim())
                        cmdAccount.Parameters.AddWithValue("@lastname", txtEMPLN.Text.Trim())
                        userId = Convert.ToInt32(cmdAccount.ExecuteScalar())
                    End Using

                    ' Then create the employee record with current branch and position
                    Dim empQuery As String = "INSERT INTO employees (birth_date, gender, employment_status, user_id, current_branch_id, current_position_id) VALUES (@bdate, @gender, @status, @user_id, @branch_id, @position_id); SELECT LAST_INSERT_ID();"
                    Using cmd As New MySqlCommand(empQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@bdate", dtEMPBDate.Value.Date)
                        cmd.Parameters.AddWithValue("@gender", cboEMPGender.Text)
                        cmd.Parameters.AddWithValue("@status", cboEMPStatus.Text)
                        cmd.Parameters.AddWithValue("@user_id", userId)
                        cmd.Parameters.AddWithValue("@branch_id", cboEMPBranch.SelectedValue)
                        cmd.Parameters.AddWithValue("@position_id", cboEMPPosition.SelectedValue)
                        empId = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using

                    Dim positionName As String = cboEMPPosition.Text
                    Dim username_ As String = txtEMPFN.Text.Trim().ToLower() & "." & txtEMPLN.Text.Trim().ToLower()
                    Dim defaultPassword As String = "emp123"

                    transaction.Commit()

                    Dim successMessage As String = $"Employee added successfully!" & vbCrLf &
                             $"Position: {positionName}" & vbCrLf &
                             $"Login credentials:" & vbCrLf &
                             $"Username: {username_}" & vbCrLf &
                             $"Password: {defaultPassword}"

                    MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadEmployees()
                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show("Error: " & ex.Message)
                End Try
#End Region

            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEMPUpdate_Click(sender As Object, e As EventArgs) Handles btnEMPUpdate.Click
        If dgvEMP.SelectedRows.Count = 0 Then
            MessageBox.Show("Select an employee to update.")
            Return
        End If

        Dim empId As Integer = Convert.ToInt32(dgvEMP.SelectedRows(0).Cells("emp_id").Value)
        Dim newPositionId As Integer = Convert.ToInt32(cboEMPPosition.SelectedValue)
        Dim newBranchId As Integer = Convert.ToInt32(cboEMPBranch.SelectedValue)

        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()

                ' Update employee basic info
                Dim updateEmp As String = "
                UPDATE employees SET
                    birth_date=@bdate, gender=@gender, employment_status=@status,
                    current_branch_id=@branch_id, current_position_id=@position_id
                WHERE emp_id=@id"
                Using cmd As New MySqlCommand(updateEmp, conn)
                    cmd.Parameters.AddWithValue("@bdate", dtEMPBDate.Value.Date)
                    cmd.Parameters.AddWithValue("@gender", cboEMPGender.Text)
                    cmd.Parameters.AddWithValue("@status", cboEMPStatus.Text)
                    cmd.Parameters.AddWithValue("@branch_id", newBranchId)
                    cmd.Parameters.AddWithValue("@position_id", newPositionId)
                    cmd.Parameters.AddWithValue("@id", empId)

                    Dim rowsAffected = cmd.ExecuteNonQuery()
                    Console.WriteLine($"Updated employee {empId}: Branch={newBranchId}, Position={newPositionId}, Rows affected: {rowsAffected}")
                End Using

                ' Update account information
                Dim updateAccount As String = "
                UPDATE accounts SET
                    first_name=@fn, last_name=@ln, email=@email, phone_number=@phone
                WHERE user_id=(SELECT user_id FROM employees WHERE emp_id=@id)"
                Using cmdAccount As New MySqlCommand(updateAccount, conn)
                    cmdAccount.Parameters.AddWithValue("@fn", txtEMPFN.Text.Trim())
                    cmdAccount.Parameters.AddWithValue("@ln", txtEMPLN.Text.Trim())
                    cmdAccount.Parameters.AddWithValue("@email", txtEMPEmail.Text.Trim())
                    cmdAccount.Parameters.AddWithValue("@phone", txtEMPPhone.Text.Trim())
                    cmdAccount.Parameters.AddWithValue("@id", empId)
                    cmdAccount.ExecuteNonQuery()
                End Using

            End Using

            MessageBox.Show("Employee updated successfully!")
            LoadEmployees()

        Catch ex As Exception
            MessageBox.Show("Update failed: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEMPDelete_Click(sender As Object, e As EventArgs) Handles btnEMPDelete.Click
        LoadPositionsToCboEMPPosition()

        If dgvEMP.SelectedRows.Count = 0 Then
            MessageBox.Show("Select an employee to delete.")
            Return
        End If
        Dim empId As Integer = Convert.ToInt32(dgvEMP.SelectedRows(0).Cells("emp_id").Value)
        If MessageBox.Show("Are you sure you want to delete this employee?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then
            Return
        End If
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim transaction = conn.BeginTransaction()
                Try
                    ' Get user_id before deleting employee
                    Dim getUserIdQuery As String = "SELECT user_id FROM employees WHERE emp_id = @empId"
                    Dim userId As Integer? = Nothing
                    Using cmdGetUserId As New MySqlCommand(getUserIdQuery, conn, transaction)
                        cmdGetUserId.Parameters.AddWithValue("@empId", empId)
                        Dim result = cmdGetUserId.ExecuteScalar()
                        If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                            userId = Convert.ToInt32(result)
                        End If
                    End Using

                    ' Delete in correct order to avoid FK violations:
                    ' 1. Delete from salaries (FK to employees)
                    Dim deleteSalariesQuery As String = "DELETE FROM salaries WHERE emp_id = @emp_id"
                    Using cmdSalaries As New MySqlCommand(deleteSalariesQuery, conn, transaction)
                        cmdSalaries.Parameters.AddWithValue("@emp_id", empId)
                        cmdSalaries.ExecuteNonQuery()
                    End Using

                    ' 2. Delete from employees (main table)
                    Dim delEmp As New MySqlCommand("DELETE FROM employees WHERE emp_id=@id", conn, transaction)
                    delEmp.Parameters.AddWithValue("@id", empId)
                    delEmp.ExecuteNonQuery()

                    ' 4. Delete from accounts if user_id exists (FK from employees)
                    If userId.HasValue Then
                        Dim delAccount As New MySqlCommand("DELETE FROM accounts WHERE user_id=@userId", conn, transaction)
                        delAccount.Parameters.AddWithValue("@userId", userId.Value)
                        delAccount.ExecuteNonQuery()
                    End If

                    transaction.Commit()
                    MessageBox.Show("Employee deleted.")
                    LoadEmployees()
                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show("Delete failed: " & ex.Message)
                End Try
            End Using
        Catch ex As Exception
            MessageBox.Show("Delete failed: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvEMP_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEMP.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        dgvEMP.ClearSelection()
        dgvEMP.Rows(e.RowIndex).Selected = True
        dgvEMP.CurrentCell = dgvEMP.Rows(e.RowIndex).Cells(e.ColumnIndex)
        LoadSelectedEmployeeDetails()
    End Sub

    ' New function to link existing employee to user account
    Private Sub LinkEmployeeToUserAccount(empId As Integer, userId As Integer)
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "UPDATE employees SET user_id = @userId WHERE emp_id = @empId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@userId", userId)
                    cmd.Parameters.AddWithValue("@empId", empId)
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        MessageBox.Show("Employee successfully linked to user account!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadEmployees()
                    Else
                        MessageBox.Show("Failed to link employee to user account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error linking employee to user account: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' New function to unlink employee from user account
    Private Sub UnlinkEmployeeFromUserAccount(empId As Integer)
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "UPDATE employees SET user_id = NULL WHERE emp_id = @empId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@empId", empId)
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        MessageBox.Show("Employee successfully unlinked from user account!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadEmployees()
                    Else
                        MessageBox.Show("Failed to unlink employee from user account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error unlinking employee from user account: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Accounts (CHECK!)"
    Private Sub LoadRoles()
        cboACRole.Items.Clear()
        cboACRole.Items.Add("Admin")
        cboACRole.Items.Add("Employee")
        cboACRole.Items.Add("Customer")
    End Sub

    Private Sub LoadAccounts()

        Dim query As String = "
        SELECT *
        FROM accounts
    "

        Dim adapter As New MySqlDataAdapter(query, connStr)
        Dim dt As New DataTable()
        adapter.Fill(dt)

        dgvAC.DataSource = dt
        dgvAC.ClearSelection()
    End Sub
    Private Sub LoadSelectedAccountDetails()
        If dgvAC.CurrentRow IsNot Nothing Then
            With dgvAC.CurrentRow
                txtACUsername.Text = .Cells("username").Value.ToString()
                txtACPS.Text = .Cells("password").Value.ToString()
                txtACMaila.Text = .Cells("email").Value.ToString()
                txtACPhone.Text = .Cells("phone_number").Value.ToString()
                txtACAdd.Text = .Cells("address").Value.ToString()
                cboACRole.Text = .Cells("role").Value.ToString()
                txtFN.Text = .Cells("first_name").Value.ToString()
                txtLN.Text = .Cells("last_name").Value.ToString()
            End With
        End If
    End Sub

    Private Sub btnACUp_Click(sender As Object, e As EventArgs) Handles btnACUP.Click
        If dgvAC.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvAC.CurrentRow.Index
            If index > 0 Then
                dgvAC.ClearSelection()
                dgvAC.Rows(index - 1).Selected = True
                dgvAC.CurrentCell = dgvAC.Rows(index - 1).Cells(0)
                LoadSelectedAccountDetails()
            End If
        End If
    End Sub

    Private Sub btnACDown_Click(sender As Object, e As EventArgs) Handles btnACDown.Click
        If dgvAC.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvAC.CurrentRow.Index
            If index < dgvAC.Rows.Count - 2 Then ' skip new row
                dgvAC.ClearSelection()
                dgvAC.Rows(index + 1).Selected = True
                dgvAC.CurrentCell = dgvAC.Rows(index + 1).Cells(0)
                LoadSelectedAccountDetails()
            End If
        End If
    End Sub

    Private Sub btnACInsert_Click(sender As Object, e As EventArgs) Handles btnACInsert.Click

        If String.IsNullOrEmpty(txtACUsername.Text) OrElse String.IsNullOrEmpty(txtACPS.Text) OrElse
           String.IsNullOrEmpty(txtACMaila.Text) OrElse String.IsNullOrEmpty(txtACPhone.Text) OrElse
           String.IsNullOrEmpty(txtACAdd.Text) OrElse String.IsNullOrEmpty(txtFN.Text) OrElse
           String.IsNullOrEmpty(txtLN.Text) Then
            MessageBox.Show("Please fill in all required fields.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Not IsValidEmail(txtACMaila.Text.Trim()) Then
            MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Not IsValidPhoneNumber(txtACPhone.Text.Trim()) Then
            MessageBox.Show("Please enter a valid phone number (10-11 digits).", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim checkQuery As String = "SELECT COUNT(*) FROM accounts WHERE (username = @username OR email = @email OR phone_number = @phone)"
        Using conn As New MySqlConnection(connStr)
            Using checkCmd As New MySqlCommand(checkQuery, conn)
                checkCmd.Parameters.AddWithValue("@username", txtACUsername.Text.Trim())
                checkCmd.Parameters.AddWithValue("@email", txtACMaila.Text.Trim())
                checkCmd.Parameters.AddWithValue("@phone", txtACPhone.Text.Trim())

                conn.Open()
                Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                conn.Close()

                If count > 0 Then
                    MessageBox.Show("Username, email, or phone number already exists. Please use different values.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using
        End Using

        Dim query As String = "INSERT INTO accounts (username, password, email, address, phone_number, role, date_created, first_name, last_name) " &
                      "VALUES (@username, @password, @email, @address, @phone, @role, @date_created, @firstname, @lastname)"

        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@username", txtACUsername.Text)
                cmd.Parameters.AddWithValue("@password", txtACPS.Text)
                cmd.Parameters.AddWithValue("@email", txtACMaila.Text)
                cmd.Parameters.AddWithValue("@address", txtACAdd.Text)
                cmd.Parameters.AddWithValue("@phone", txtACPhone.Text)
                cmd.Parameters.AddWithValue("@role", cboACRole.Text.ToLower())
                cmd.Parameters.AddWithValue("@date_created", DateTime.Now.Date)
                cmd.Parameters.AddWithValue("@firstname", txtFN.Text)
                cmd.Parameters.AddWithValue("@lastname", txtLN.Text)

                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()

                MessageBox.Show("Account added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ClearAccountFields()
            End Using
        End Using

        LoadAccounts()
    End Sub

    Private Sub ClearAccountFields()
        txtACUsername.Clear()
        txtACPS.Clear()
        txtACMaila.Clear()
        txtACPhone.Clear()
        txtACAdd.Clear()
        txtFN.Clear()
        txtLN.Clear()
        cboACRole.SelectedIndex = -1
    End Sub

    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim addr As New System.Net.Mail.MailAddress(email)
            Return addr.Address = email
        Catch
            Return False
        End Try
    End Function

    Private Function IsValidPhoneNumber(phone As String) As Boolean
        ' Remove any non-digit characters for validation
        Dim digitsOnly As String = System.Text.RegularExpressions.Regex.Replace(phone, "[^\d]", "")
        Return digitsOnly.Length >= 10 AndAlso digitsOnly.Length <= 11
    End Function

    Private Sub btnACUpdate_Click(sender As Object, e As EventArgs) Handles btnACUpdate.Click
        If dgvAC.CurrentRow Is Nothing Then
            MessageBox.Show("Select an account to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If String.IsNullOrEmpty(txtACUsername.Text) OrElse String.IsNullOrEmpty(txtACPS.Text) OrElse
           String.IsNullOrEmpty(txtACMaila.Text) OrElse String.IsNullOrEmpty(txtACPhone.Text) OrElse
           String.IsNullOrEmpty(txtACAdd.Text) OrElse String.IsNullOrEmpty(txtFN.Text) OrElse
           String.IsNullOrEmpty(txtLN.Text) Then
            MessageBox.Show("Please fill in all required fields.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Not IsValidEmail(txtACMaila.Text.Trim()) Then
            MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Not IsValidPhoneNumber(txtACPhone.Text.Trim()) Then
            MessageBox.Show("Please enter a valid phone number (10-11 digits).", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim userId As Integer = Convert.ToInt32(dgvAC.CurrentRow.Cells("user_id").Value)

        Dim checkQuery As String = "SELECT COUNT(*) FROM accounts WHERE (username = @username OR email = @email OR phone_number = @phone) AND user_id != @userId"
        Using conn As New MySqlConnection(connStr)
            Using checkCmd As New MySqlCommand(checkQuery, conn)
                checkCmd.Parameters.AddWithValue("@username", txtACUsername.Text.Trim())
                checkCmd.Parameters.AddWithValue("@email", txtACMaila.Text.Trim())
                checkCmd.Parameters.AddWithValue("@phone", txtACPhone.Text.Trim())
                checkCmd.Parameters.AddWithValue("@userId", userId)

                conn.Open()
                Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                conn.Close()

                If count > 0 Then
                    MessageBox.Show("Username, email, or phone number already exists in another account. Please use different values.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using
        End Using

        ' Proceed with update
        Dim query As String = "UPDATE accounts SET username=@username, password=@password, email=@email, " &
                      "address=@address, phone_number=@phone, role=@role, first_name=@firstname, last_name=@lastname " &
                      "WHERE user_id=@id"

        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@username", txtACUsername.Text.Trim())
                cmd.Parameters.AddWithValue("@password", txtACPS.Text.Trim())
                cmd.Parameters.AddWithValue("@email", txtACMaila.Text.Trim())
                cmd.Parameters.AddWithValue("@address", txtACAdd.Text.Trim())
                cmd.Parameters.AddWithValue("@phone", txtACPhone.Text.Trim())
                cmd.Parameters.AddWithValue("@role", cboACRole.Text.ToLower())
                cmd.Parameters.AddWithValue("@firstname", txtFN.Text.Trim())
                cmd.Parameters.AddWithValue("@lastname", txtLN.Text.Trim())
                cmd.Parameters.AddWithValue("@id", userId)

                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()

                MessageBox.Show("Account updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        End Using

        LoadAccounts()
    End Sub

    Private Sub btnACDelete_Click(sender As Object, e As EventArgs) Handles btnACDelete.Click
        If Convert.ToInt32(dgvAC.CurrentRow.Cells("user_id").Value) = currentUserId Then
            MessageBox.Show("You cannot delete your own account while logged in.")
            Exit Sub
        End If

        Dim userId As Integer = Convert.ToInt32(dgvAC.CurrentRow.Cells("user_id").Value)

        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this account?", "Confirm", MessageBoxButtons.YesNo)
        If confirm = DialogResult.No Then Exit Sub

#Region "TRANSACTION PART"
        'SEQUENCE: Returns -> Orders -> Cart -> Accounts

        Using conn As New MySqlConnection(connStr)
            conn.Open()




            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try

                Dim delReturnsQuery As String = "DELETE r FROM returns r INNER JOIN orders o ON r.order_id = o.order_id WHERE o.user_id = @id"
                Using delReturnsCmd As New MySqlCommand(delReturnsQuery, conn, transaction)
                    delReturnsCmd.Parameters.AddWithValue("@id", userId)
                    delReturnsCmd.ExecuteNonQuery()
                End Using

                Dim delOrderItemsQuery As String = "DELETE FROM order_items WHERE order_id IN (SELECT order_id FROM orders WHERE user_id = @id)"
                Using delOrderItemsCmd As New MySqlCommand(delOrderItemsQuery, conn, transaction)
                    delOrderItemsCmd.Parameters.AddWithValue("@id", userId)
                    delOrderItemsCmd.ExecuteNonQuery()
                End Using

                Dim delOrdersQuery As String = "DELETE FROM orders WHERE user_id = @id"
                Using delOrdersCmd As New MySqlCommand(delOrdersQuery, conn, transaction)
                    delOrdersCmd.Parameters.AddWithValue("@id", userId)
                    delOrdersCmd.ExecuteNonQuery()
                End Using

                Dim delCartQuery As String = "DELETE FROM cart WHERE user_id = @id"
                Using delCartCmd As New MySqlCommand(delCartQuery, conn, transaction)
                    delCartCmd.Parameters.AddWithValue("@id", userId)
                    delCartCmd.ExecuteNonQuery()
                End Using

                Dim delAccQuery As String = "DELETE FROM accounts WHERE user_id = @id"
                Using delAccCmd As New MySqlCommand(delAccQuery, conn, transaction)
                    delAccCmd.Parameters.AddWithValue("@id", userId)
                    delAccCmd.ExecuteNonQuery()
                End Using

                transaction.Commit()
                MessageBox.Show("Account and all related data (returns, orders, carts) deleted successfully.")

            Catch ex As Exception
                ' Rollback the transaction if any operation fails
                transaction.Rollback()
                MessageBox.Show("Error deleting account: " & ex.Message & vbCrLf & "All changes have been rolled back.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            conn.Close()
        End Using
#End Region

        LoadAccounts()
    End Sub

    Private Sub btnACBack_MouseClick(sender As Object, e As MouseEventArgs) Handles btnACBack.MouseClick
        ACpanel.Hide()
        manage_panel.Show()
    End Sub

    Private Sub btnACSearch_Click(sender As Object, e As EventArgs) Handles btnACSearch.Click
        Dim searchTerm As String = txtACSearch.Text.Trim()
        Dim query As String = "SELECT * FROM accounts WHERE user_id = @id OR username LIKE @term OR email LIKE @term OR phone_number LIKE @term OR role LIKE @term or address like @term ORDER BY user_id DESC LIMIT 50"
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                Dim idVal As Integer
                If Integer.TryParse(searchTerm, idVal) Then
                    cmd.Parameters.AddWithValue("@id", idVal)
                Else
                    cmd.Parameters.AddWithValue("@id", -1)
                End If
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        dgvAC.DataSource = dt
    End Sub

    Private Sub dgvAC_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAC.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        dgvAC.ClearSelection()
        dgvAC.Rows(e.RowIndex).Selected = True
        dgvAC.CurrentCell = dgvAC.Rows(e.RowIndex).Cells(e.ColumnIndex)
        LoadSelectedAccountDetails()
    End Sub

#End Region

#Region "Salaries"
    Private Sub LoadSalaryStatus()
        cboSALStatus.Items.Clear()
        cboSALStatus.Items.Add("pending")
        cboSALStatus.Items.Add("paid")
        cboSALStatus.Items.Add("overdue")
        cboSALStatus.SelectedIndex = 0 ' Optional: select the first by default
    End Sub
    Private Sub LoadBranches_sal()
        Dim query As String = "SELECT branch_id, branch_name FROM branches"
        Dim dt As New DataTable()

        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        cboSALBranch.DisplayMember = "branch_name"
        cboSALBranch.ValueMember = "branch_id"
        cboSALBranch.DataSource = dt
    End Sub

    Private Sub cboSALBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSALBranch.SelectedIndexChanged
        If cboSALBranch.SelectedIndex = -1 OrElse cboSALBranch.SelectedValue Is Nothing Then
            cboSALID.DataSource = Nothing
            Return
        End If

        Dim branchId As Integer = Convert.ToInt32(cboSALBranch.SelectedValue)

        Dim query As String = "SELECT e.emp_id, CONCAT(a.first_name, ' ', a.last_name) AS full_name FROM employees e JOIN accounts a ON e.user_id = a.user_id WHERE e.current_branch_id = @branchId"
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@branchId", branchId)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                cboSALID.DataSource = dt
                cboSALID.DisplayMember = "full_name"
                cboSALID.ValueMember = "emp_id"
            End Using
        End Using
    End Sub

    Private Function GetBranchIdForEmployee(empId As Integer) As Integer
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT current_branch_id FROM employees WHERE emp_id = @empId", conn)
            cmd.Parameters.AddWithValue("@empId", empId)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return Convert.ToInt32(result)
            End If
        End Using
        Return -1 ' Not found
    End Function

    ' Insert new salary
    Private Sub btnSALInsert_Click(sender As Object, e As EventArgs) Handles btnSALInsert.Click
        If cboSALID.SelectedItem Is Nothing Then
            MessageBox.Show("Please select an employee.")
            Return
        End If

        ' Validate date range
        If dtSALFrom.Value.Date >= dtSALTO.Value.Date Then
            MessageBox.Show("From date must be earlier than To date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Calculate number of months between dates
        Dim numMonths As Integer = CalculateMonthsBetweenDates(dtSALFrom.Value.Date, dtSALTO.Value.Date)

        ' Debug information
        Dim debugInfo As String = $"Date Range Debug:" & vbCrLf &
                                 $"From Date: {dtSALFrom.Value.Date:yyyy-MM-dd}" & vbCrLf &
                                 $"To Date: {dtSALTO.Value.Date:yyyy-MM-dd}" & vbCrLf &
                                 $"Days Difference: {(dtSALTO.Value.Date - dtSALFrom.Value.Date).Days}" & vbCrLf &
                                 $"Calculated Months: {numMonths}"
        Console.WriteLine(debugInfo) ' This will show in the debug output

        ' Validate minimum 1 month
        If numMonths < 1 Then
            MessageBox.Show($"The date range must be at least 1 month.{vbCrLf}{vbCrLf}{debugInfo}", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim empId As Integer = Convert.ToInt32(cboSALID.SelectedValue)

        Dim monthlyRate As Decimal = 0

        ' Get the current position for the employee and its monthly rate
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT p.monthly_rate FROM employees e " &
                              "JOIN positions p ON e.current_position_id = p.position_id " &
                              "WHERE e.emp_id = @empId"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@empId", empId)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    monthlyRate = Convert.ToDecimal(result)
                Else
                    MessageBox.Show("No position found for this employee.")
                    Return
                End If
            End Using
        End Using

        Dim totalSalary As Decimal = monthlyRate * numMonths

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "INSERT INTO salaries (emp_id, pay_date, rate_used, status, from_date, to_date) " &
                              "VALUES (@emp_id, @pay_date, @rate_used, @status, @from_date, @to_date)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@emp_id", empId)
                cmd.Parameters.AddWithValue("@pay_date", dtSALPayDate.Value.Date)
                cmd.Parameters.AddWithValue("@rate_used", monthlyRate)
                cmd.Parameters.AddWithValue("@status", cboSALStatus.Text)
                cmd.Parameters.AddWithValue("@from_date", dtSALFrom.Value.Date)
                cmd.Parameters.AddWithValue("@to_date", dtSALTO.Value.Date)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim branchId As Integer = -1
            Dim branchQuery As String = "SELECT current_branch_id FROM employees WHERE emp_id = @empId"
            Using branchCmd As New MySqlCommand(branchQuery, conn)
                branchCmd.Parameters.AddWithValue("@empId", empId)
                Dim result = branchCmd.ExecuteScalar()
                If result IsNot Nothing Then
                    branchId = Convert.ToInt32(result)
                End If
            End Using

            If branchId <> -1 Then
                Dim expenseStatus As String = MapSalaryStatusToExpenseStatus(cboSALStatus.Text)

                Dim expQuery As String = "INSERT INTO expenses (branch_id, expense_type, fee, status, from_date, due_date) VALUES (@branch_id, 'Salaries', @fee, @status, @from, @due)"
                Using expCmd As New MySqlCommand(expQuery, conn)
                    expCmd.Parameters.AddWithValue("@branch_id", branchId)
                    expCmd.Parameters.AddWithValue("@fee", totalSalary)
                    expCmd.Parameters.AddWithValue("@status", expenseStatus)
                    expCmd.Parameters.AddWithValue("@from", dtSALFrom.Value.Date)
                    expCmd.Parameters.AddWithValue("@due", dtSALTO.Value.Date)
                    expCmd.ExecuteNonQuery()
                End Using
            End If
        End Using

        MessageBox.Show("Salary added successfully.")
        LoadSalaries()
    End Sub

    Private Function MapSalaryStatusToExpenseStatus(salaryStatus As String) As String
        Select Case salaryStatus.ToLower()
            Case "pending"
                Return "Processing"
            Case "paid"
                Return "Paid"
            Case "overdue"
                Return "Overdue"
            Case Else
                Return "Processing"
        End Select
    End Function

    Private Function CalculateMonthsBetweenDates(fromDate As DateTime, toDate As DateTime) As Integer
        ' Calculate the difference in years and months
        Dim years As Integer = toDate.Year - fromDate.Year
        Dim months As Integer = toDate.Month - fromDate.Month

        ' Calculate total months
        Dim totalMonths As Integer = years * 12 + months

        ' Adjust for day of month - if end day is before start day, subtract 1 month
        ' unless it's the last day of the month
        If toDate.Day < fromDate.Day Then
            Dim lastDayOfMonth As Integer = DateTime.DaysInMonth(toDate.Year, toDate.Month)
            If toDate.Day < lastDayOfMonth Then
                totalMonths -= 1
            End If
        End If

        ' For salary calculations, we want to be generous with month counting
        ' If the date range is at least 20 days, count it as at least 1 month
        Dim daysDifference As Integer = (toDate - fromDate).Days
        If daysDifference >= 20 AndAlso totalMonths = 0 Then
            totalMonths = 1
        End If

        Return Math.Max(0, totalMonths)
    End Function

    Private Sub btnSALUpdate_Click(sender As Object, e As EventArgs) Handles btnSALUpdate.Click
        If dgvSAL.CurrentRow Is Nothing Then
            MessageBox.Show("Select a salary record to update.")
            Return
        End If

        ' Validate date range
        If dtSALFrom.Value.Date >= dtSALTO.Value.Date Then
            MessageBox.Show("From date must be earlier than To date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Calculate number of months between dates
        Dim numMonths As Integer = CalculateMonthsBetweenDates(dtSALFrom.Value.Date, dtSALTO.Value.Date)

        ' Debug information
        Dim debugInfo As String = $"Date Range Debug:" & vbCrLf &
                                 $"From Date: {dtSALFrom.Value.Date:yyyy-MM-dd}" & vbCrLf &
                                 $"To Date: {dtSALTO.Value.Date:yyyy-MM-dd}" & vbCrLf &
                                 $"Days Difference: {(dtSALTO.Value.Date - dtSALFrom.Value.Date).Days}" & vbCrLf &
                                 $"Number of Months: {numMonths}"
        Console.WriteLine(debugInfo) ' This will show in the debug output

        ' Validate minimum 1 month
        If numMonths < 1 Then
            MessageBox.Show($"The number of months must be at least 1.{vbCrLf}{vbCrLf}{debugInfo}", "Invalid Number of Months", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim salaryId As Integer = Convert.ToInt32(dgvSAL.CurrentRow.Cells("salary_id").Value)
        Dim empId As Integer = Convert.ToInt32(dgvSAL.CurrentRow.Cells("emp_id").Value)

        ' Get the rate from the textbox or use the current rate
        Dim rateUsed As Decimal = 0
        If Not String.IsNullOrEmpty(txtSALRate.Text) Then
            If Decimal.TryParse(txtSALRate.Text, rateUsed) Then
                ' Rate is valid
            Else
                MessageBox.Show("Please enter a valid rate amount.")
                Return
            End If
        Else
            ' Get the current rate from the database
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT rate_used FROM salaries WHERE salary_id = @salaryId"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@salaryId", salaryId)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing Then
                        rateUsed = Convert.ToDecimal(result)
                    Else
                        MessageBox.Show("No rate found for this salary record.")
                        Return
                    End If
                End Using
            End Using
        End If

        ' Now update the salary record with the rate_used field
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "UPDATE salaries SET pay_date=@pay_date, rate_used=@rate_used, status=@status, from_date=@from_date, to_date=@to_date WHERE salary_id=@id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", salaryId)
                cmd.Parameters.AddWithValue("@pay_date", dtSALPayDate.Value.Date)
                cmd.Parameters.AddWithValue("@rate_used", rateUsed)
                cmd.Parameters.AddWithValue("@status", cboSALStatus.Text)
                cmd.Parameters.AddWithValue("@from_date", dtSALFrom.Value.Date)
                cmd.Parameters.AddWithValue("@to_date", dtSALTO.Value.Date)
                cmd.ExecuteNonQuery()
            End Using

            ' Update the corresponding expense record
            Dim branchId As Integer = -1
            ' Get the branch_id for the employee
            Dim branchQuery As String = "SELECT current_branch_id FROM employees WHERE emp_id = @empId"
            Using branchCmd As New MySqlCommand(branchQuery, conn)
                branchCmd.Parameters.AddWithValue("@empId", empId)
                Dim result = branchCmd.ExecuteScalar()
                If result IsNot Nothing Then
                    branchId = Convert.ToInt32(result)
                End If
            End Using

            If branchId <> -1 Then
                ' Map salary status to expense status
                Dim expenseStatus As String = MapSalaryStatusToExpenseStatus(cboSALStatus.Text)

                ' Calculate total salary amount
                Dim numMonths_ As Integer = CalculateMonthsBetweenDates(dtSALFrom.Value.Date, dtSALTO.Value.Date)
                Dim totalSalary As Decimal = rateUsed * numMonths_

                Dim expQuery As String = "UPDATE expenses SET fee=@fee, status=@status, from_date=@from, due_date=@due WHERE expense_type='Salaries' AND branch_id=@branch_id AND from_date=@old_from AND due_date=@old_to"
                Using expCmd As New MySqlCommand(expQuery, conn)
                    expCmd.Parameters.AddWithValue("@fee", totalSalary)
                    expCmd.Parameters.AddWithValue("@status", expenseStatus)
                    expCmd.Parameters.AddWithValue("@from", dtSALFrom.Value.Date)
                    expCmd.Parameters.AddWithValue("@due", dtSALTO.Value.Date)
                    expCmd.Parameters.AddWithValue("@branch_id", branchId)
                    expCmd.Parameters.AddWithValue("@old_from", Convert.ToDateTime(dgvSAL.CurrentRow.Cells("from_date").Value))
                    expCmd.Parameters.AddWithValue("@old_to", Convert.ToDateTime(dgvSAL.CurrentRow.Cells("to_date").Value))
                    expCmd.ExecuteNonQuery()
                End Using
            End If
        End Using

        MessageBox.Show("Salary updated successfully.")
        LoadSalaries()
    End Sub

    Private Sub btnSALDelete_Click(sender As Object, e As EventArgs) Handles btnSALDelete.Click
        If dgvSAL.CurrentRow Is Nothing Then
            MessageBox.Show("Select a salary record to delete.")
            Return
        End If

        Dim id As Integer = Convert.ToInt32(dgvSAL.CurrentRow.Cells("salary_id").Value)

        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this salary record?", "Confirm", MessageBoxButtons.YesNo)
        If confirm = DialogResult.No Then Return

        ' Delete the related expense record
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            ' Get the salary details first
            Dim empId As Integer = Convert.ToInt32(dgvSAL.CurrentRow.Cells("emp_id").Value)
            Dim rateUsed As Decimal = Convert.ToDecimal(dgvSAL.CurrentRow.Cells("rate_used").Value)
            Dim fromDate As Date = Convert.ToDateTime(dgvSAL.CurrentRow.Cells("from_date").Value)
            Dim toDate As Date = Convert.ToDateTime(dgvSAL.CurrentRow.Cells("to_date").Value)
            Dim numMonths As Integer = Convert.ToInt32(dgvSAL.CurrentRow.Cells("num_months").Value)
            Dim totalSalary As Decimal = rateUsed * numMonths

            Dim delExpenseQuery As String = "DELETE FROM expenses WHERE expense_type = 'Salaries' AND fee = @amount AND from_date = @from AND due_date = @to AND branch_id = (SELECT current_branch_id FROM employees WHERE emp_id = @empId)"
            Using delExpenseCmd As New MySqlCommand(delExpenseQuery, conn)
                delExpenseCmd.Parameters.AddWithValue("@amount", totalSalary)
                delExpenseCmd.Parameters.AddWithValue("@from", fromDate)
                delExpenseCmd.Parameters.AddWithValue("@to", toDate)
                delExpenseCmd.Parameters.AddWithValue("@empId", empId)
                delExpenseCmd.ExecuteNonQuery()
            End Using

            ' Now delete the salary
            Dim delSalaryQuery As String = "DELETE FROM salaries WHERE salary_id = @id"
            Using delSalaryCmd As New MySqlCommand(delSalaryQuery, conn)
                delSalaryCmd.Parameters.AddWithValue("@id", Convert.ToInt32(dgvSAL.CurrentRow.Cells("salary_id").Value))
                delSalaryCmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Salary record deleted.")
        LoadSalaries()
    End Sub

    Private Sub LoadSalaries()
        Dim query As String = "SELECT s.salary_id, e.emp_id, a.first_name, a.last_name, s.pay_date, s.rate_used, s.status, s.from_date, s.to_date, " &
                          "e.current_branch_id AS branch_id, " &
                          "CASE " &
                          "  WHEN DATEDIFF(s.to_date, s.from_date) >= 20 THEN " &
                          "    GREATEST(1, (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date))) " &
                          "  ELSE " &
                          "    (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date)) " &
                          "END AS num_months, " &
                          "s.rate_used * CASE " &
                          "  WHEN DATEDIFF(s.to_date, s.from_date) >= 20 THEN " &
                          "    GREATEST(1, (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date))) " &
                          "  ELSE " &
                          "    (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date)) " &
                          "END AS total_salary " &
                          "FROM salaries s INNER JOIN employees e ON s.emp_id = e.emp_id INNER JOIN accounts a ON e.user_id = a.user_id ORDER BY branch_id"

        Dim dt As New DataTable
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        dgvSAL.DataSource = dt
    End Sub

    Private Sub LoadSelectedSalaryDetails()
        If dgvSAL.CurrentRow Is Nothing Then Return

        Dim row = dgvSAL.CurrentRow

        dtSALPayDate.Value = Convert.ToDateTime(row.Cells("pay_date").Value)
        cboSALStatus.Text = row.Cells("status").Value.ToString()
        dtSALFrom.Value = Convert.ToDateTime(row.Cells("from_date").Value)
        dtSALTO.Value = Convert.ToDateTime(row.Cells("to_date").Value)

        ' Load the rate_used value
        If Not IsDBNull(row.Cells("rate_used").Value) Then
            txtSALRate.Text = Convert.ToDecimal(row.Cells("rate_used").Value).ToString("N2")
        Else
            txtSALRate.Text = "0.00"
        End If


    End Sub

    Private Sub btnSALUP_Click(sender As Object, e As EventArgs) Handles btnSALUP.Click
        cboSALBranch.SelectedIndex = -1
        cboSALID.SelectedIndex = -1

        If dgvSAL.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvSAL.CurrentRow.Index
            If index > 0 Then
                dgvSAL.ClearSelection()
                dgvSAL.Rows(index - 1).Selected = True
                dgvSAL.CurrentCell = dgvSAL.Rows(index - 1).Cells(0)
                LoadSelectedSalaryDetails()
            End If
        End If
    End Sub

    Private Sub btnSALDOWN_Click(sender As Object, e As EventArgs) Handles btnSALDOWN.Click
        cboSALBranch.SelectedIndex = -1
        cboSALID.SelectedIndex = -1

        If dgvSAL.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvSAL.CurrentRow.Index
            If index < dgvSAL.Rows.Count - 2 Then ' -2 in case of new row
                dgvSAL.ClearSelection()
                dgvSAL.Rows(index + 1).Selected = True
                dgvSAL.CurrentCell = dgvSAL.Rows(index + 1).Cells(0)
                LoadSelectedSalaryDetails()
            End If

        End If
    End Sub

    Private Sub admin_page_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        lblWelcome.Text = "ADMIN, " & currentUsername & "!"
    End Sub

    Private Sub btnSALSearch_Click(sender As Object, e As EventArgs) Handles btnSALSearch.Click
        Dim searchTerm As String = txtSALSearch.Text.Trim()
        Dim query As String = "SELECT s.salary_id, e.emp_id, a.first_name, a.last_name, s.pay_date, s.rate_used, s.status, s.from_date, s.to_date, " &
                          "e.current_branch_id AS branch_id, " &
                          "CASE " &
                          "  WHEN DATEDIFF(s.to_date, s.from_date) >= 20 THEN " &
                          "    GREATEST(1, (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date))) " &
                          "  ELSE " &
                          "    (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date)) " &
                          "END AS num_months, " &
                          "s.rate_used * CASE " &
                          "  WHEN DATEDIFF(s.to_date, s.from_date) >= 20 THEN " &
                          "    GREATEST(1, (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date))) " &
                          "  ELSE " &
                          "    (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date)) " &
                          "END AS total_salary " &
                          "FROM salaries s INNER JOIN employees e ON s.emp_id = e.emp_id INNER JOIN accounts a ON e.user_id = a.user_id " &
                          "WHERE s.salary_id = @id OR s.emp_id = @id OR s.status LIKE @term OR a.first_name LIKE @term OR a.last_name LIKE @term ORDER BY s.salary_id DESC LIMIT 50"
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                Dim idVal As Integer
                If Integer.TryParse(searchTerm, idVal) Then
                    cmd.Parameters.AddWithValue("@id", idVal)
                Else
                    cmd.Parameters.AddWithValue("@id", -1)
                End If
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        dgvSAL.DataSource = dt
    End Sub

    Private Sub dgvSAL_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSAL.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        dgvSAL.ClearSelection()
        dgvSAL.Rows(e.RowIndex).Selected = True
        dgvSAL.CurrentCell = dgvSAL.Rows(e.RowIndex).Cells(e.ColumnIndex)
        LoadSelectedSalaryDetails()
    End Sub

    Private Sub txtSALRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSALRate.KeyPress
        ' Allow only numbers, decimal point, and control characters
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If

        ' Allow only one decimal point
        If e.KeyChar = "." AndAlso txtSALRate.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtSALRate_TextChanged(sender As Object, e As EventArgs) Handles txtSALRate.TextChanged
        ' Simple validation - only allow numbers and one decimal point
        If Not String.IsNullOrEmpty(txtSALRate.Text) Then
            Dim filteredText As String = ""
            Dim decimalCount As Integer = 0

            For Each c As Char In txtSALRate.Text
                If Char.IsDigit(c) Then
                    filteredText += c
                ElseIf c = "." AndAlso decimalCount = 0 Then
                    filteredText += c
                    decimalCount += 1
                End If
            Next

            If filteredText <> txtSALRate.Text Then
                txtSALRate.Text = filteredText
                txtSALRate.SelectionStart = txtSALRate.Text.Length
            End If
        End If
    End Sub



#End Region

#Region "Orders"
    Private Sub LoadOrderStatus()
        cboORstatus.Items.Clear()
        cboORstatus.Items.AddRange({"pending", "processing", "shipped", "delivered", "cancelled", "returned"})
    End Sub

    Private Sub LoadPaymentStatus()
        cboORpaystatus.Items.Clear()
        cboORpaystatus.Items.AddRange({"pending", "paid", "refunded", "failed"})
    End Sub

    Private Sub LoadPaymentOptions()
        cboORpayopt.Items.Clear()
        cboORpayopt.Items.AddRange({"cash", "ATM"})
    End Sub

    Private Sub LoadOrders()
        Dim query As String = "
        SELECT 
            o.order_id,
            o.date_ordered,
            o.est_delivery,
            o.order_status,
            o.payment_status,
            o.payment_option,
            c.first_name,
            c.last_name,
            m.model_name,
            m.brand,
            oi.quantity,
            oi.unit_price,
            (oi.quantity * oi.unit_price) as subtotal,
            b.branch_name,
            oi.model_id,
            oi.branch_id
        FROM orders o
        JOIN accounts c ON o.user_id = c.user_id
        JOIN order_items oi ON o.order_id = oi.order_id
        JOIN models m ON oi.model_id = m.model_id
        JOIN branches b ON oi.branch_id = b.branch_id
        ORDER BY o.order_id DESC, m.model_name"

        Dim dt As New DataTable
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        dgvOR.DataSource = dt

        ' Set column headers for better readability
        If dgvOR.Columns.Count > 0 Then
            dgvOR.Columns("order_id").HeaderText = "Order ID"
            dgvOR.Columns("date_ordered").HeaderText = "Date Ordered"
            dgvOR.Columns("est_delivery").HeaderText = "Est. Delivery"
            dgvOR.Columns("order_status").HeaderText = "Order Status"
            dgvOR.Columns("payment_status").HeaderText = "Payment Status"
            dgvOR.Columns("payment_option").HeaderText = "Payment Method"
            dgvOR.Columns("first_name").HeaderText = "First Name"
            dgvOR.Columns("last_name").HeaderText = "Last Name"
            dgvOR.Columns("model_name").HeaderText = "Model Name"
            dgvOR.Columns("brand").HeaderText = "Brand"
            dgvOR.Columns("quantity").HeaderText = "Quantity"
            dgvOR.Columns("unit_price").HeaderText = "Unit Price"
            dgvOR.Columns("subtotal").HeaderText = "Subtotal"
            dgvOR.Columns("branch_name").HeaderText = "Branch"

            ' Format currency columns
            dgvOR.Columns("unit_price").DefaultCellStyle.Format = "N2"
            dgvOR.Columns("subtotal").DefaultCellStyle.Format = "N2"

            ' Set column widths
            dgvOR.Columns("order_id").Width = 80
            dgvOR.Columns("date_ordered").Width = 120
            dgvOR.Columns("est_delivery").Width = 100
            dgvOR.Columns("order_status").Width = 100
            dgvOR.Columns("payment_status").Width = 100
            dgvOR.Columns("payment_option").Width = 100
            dgvOR.Columns("first_name").Width = 100
            dgvOR.Columns("last_name").Width = 100
            dgvOR.Columns("model_name").Width = 150
            dgvOR.Columns("brand").Width = 100
            dgvOR.Columns("quantity").Width = 80
            dgvOR.Columns("unit_price").Width = 100
            dgvOR.Columns("subtotal").Width = 100
            dgvOR.Columns("branch_name").Width = 120

            ' Hide internal columns
            dgvOR.Columns("model_id").Visible = False
            dgvOR.Columns("branch_id").Visible = False
        End If
    End Sub

    Private Sub LoadSelectedOrderDetails()
        If dgvOR.CurrentRow Is Nothing Then Exit Sub
        Dim row = dgvOR.CurrentRow

        dtORdelivery.Value = If(IsDBNull(row.Cells("est_delivery").Value), Date.Today, Convert.ToDateTime(row.Cells("est_delivery").Value))
        cboORstatus.Text = row.Cells("order_status").Value.ToString()
        cboORpaystatus.Text = row.Cells("payment_status").Value.ToString()
        cboORpayopt.Text = row.Cells("payment_option").Value.ToString()

        ' Load quantity and unit price from the selected row
        If Not IsDBNull(row.Cells("quantity").Value) Then
            txtORQuantity.Text = Convert.ToInt32(row.Cells("quantity").Value).ToString()
        Else
            txtORQuantity.Text = "0"
        End If

        If Not IsDBNull(row.Cells("unit_price").Value) Then
            txtORUNITPRICE.Text = Convert.ToDecimal(row.Cells("unit_price").Value).ToString("N2")
        Else
            txtORUNITPRICE.Text = "0.00"
        End If
    End Sub

    Private Sub LoadOrderQuantity(orderId As Integer)
        Try
            Dim query As String = "SELECT SUM(quantity) as total_quantity FROM order_items WHERE order_id = @orderId"
            Using conn As New MySqlConnection(connStr)
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@orderId", orderId)
                    conn.Open()
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        txtORQuantity.Text = Convert.ToInt32(result).ToString()
                    Else
                        txtORQuantity.Text = "0"
                    End If
                End Using
            End Using
        Catch ex As Exception
            txtORQuantity.Text = "0"
        End Try
    End Sub

    Private Sub LoadOrderUnitPrice(orderId As Integer)
        Try
            Dim query As String = "SELECT AVG(unit_price) as avg_unit_price FROM order_items WHERE order_id = @orderId"
            Using conn As New MySqlConnection(connStr)
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@orderId", orderId)
                    conn.Open()
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        txtORUNITPRICE.Text = Convert.ToDecimal(result).ToString("N2")
                    Else
                        txtORUNITPRICE.Text = "0.00"
                    End If
                End Using
            End Using
        Catch ex As Exception
            txtORUNITPRICE.Text = "0.00"
        End Try
    End Sub
    Private Sub btnORUp_Click(sender As Object, e As EventArgs) Handles btnORUp.Click
        If dgvOR.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvOR.CurrentRow.Index
            If index > 0 Then
                dgvOR.ClearSelection()
                dgvOR.Rows(index - 1).Selected = True
                dgvOR.CurrentCell = dgvOR.Rows(index - 1).Cells(0)
                LoadSelectedOrderDetails()
            End If
        End If
    End Sub

    Private Sub btnORDown_Click(sender As Object, e As EventArgs) Handles btnORDown.Click
        If dgvOR.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvOR.CurrentRow.Index
            If index < dgvOR.Rows.Count - 2 Then
                dgvOR.ClearSelection()
                dgvOR.Rows(index + 1).Selected = True
                dgvOR.CurrentCell = dgvOR.Rows(index + 1).Cells(0)
                LoadSelectedOrderDetails()
            End If
        End If
    End Sub

    Private Sub btnORUpdate_Click(sender As Object, e As EventArgs) Handles btnORUpdate.Click
        If dgvOR.CurrentRow Is Nothing Then
            MessageBox.Show("Select an order to update.")
            Return
        End If

        Dim orderId As Integer = Convert.ToInt32(dgvOR.CurrentRow.Cells("order_id").Value)
        Dim newStatus As String = cboORstatus.Text

        ' Validate quantity and unit price inputs
        Dim newQuantity As Integer
        Dim newUnitPrice As Decimal

        If Not Integer.TryParse(txtORQuantity.Text, newQuantity) OrElse newQuantity <= 0 Then
            MessageBox.Show("Please enter a valid quantity (must be a positive number).", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not Decimal.TryParse(txtORUNITPRICE.Text, newUnitPrice) OrElse newUnitPrice <= 0 Then
            MessageBox.Show("Please enter a valid unit price (must be a positive number).", "Invalid Unit Price", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check if the new quantity exceeds available stock
        Dim currentQuantity As Integer = Convert.ToInt32(dgvOR.CurrentRow.Cells("quantity").Value)
        Dim quantityDifference As Integer = newQuantity - currentQuantity

        If quantityDifference > 0 Then
            ' Need to check if we have enough stock
            Dim modelId As Integer = Convert.ToInt32(dgvOR.CurrentRow.Cells("model_id").Value)
            Dim branchId As Integer = Convert.ToInt32(dgvOR.CurrentRow.Cells("branch_id").Value)

            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim stockCmd As New MySqlCommand("SELECT stock FROM branch_model WHERE model_id = @modelId AND branch_id = @branchId", conn)
                stockCmd.Parameters.AddWithValue("@modelId", modelId)
                stockCmd.Parameters.AddWithValue("@branchId", branchId)
                Dim availableStock As Integer = Convert.ToInt32(stockCmd.ExecuteScalar())

                If quantityDifference > availableStock Then
                    MessageBox.Show($"Cannot increase quantity by {quantityDifference}. Only {availableStock} units available in stock.", "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Confirm with user when increasing quantity
                Dim confirmResult = MessageBox.Show($"You are increasing the quantity by {quantityDifference} units. This will reduce available stock from {availableStock} to {availableStock - quantityDifference}. Continue?", "Confirm Quantity Increase", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirmResult = DialogResult.No Then
                    Return
                End If
            End Using
        End If

        Dim query As String = "
        UPDATE orders 
        SET est_delivery = @delivery, 
            order_status = @status,
            payment_status = @paystat,
            payment_option = @payopt
        WHERE order_id = @id"

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim transaction = conn.BeginTransaction()
            Try
                ' Update order status and payment status
                Using cmd As New MySqlCommand(query, conn, transaction)
                    cmd.Parameters.AddWithValue("@delivery", dtORdelivery.Value.Date)
                    cmd.Parameters.AddWithValue("@status", newStatus)
                    cmd.Parameters.AddWithValue("@paystat", cboORpaystatus.Text)
                    cmd.Parameters.AddWithValue("@payopt", cboORpayopt.Text)
                    cmd.Parameters.AddWithValue("@id", orderId)
                    cmd.ExecuteNonQuery()
                End Using

                ' Update quantity and unit price for the specific order item
                Dim updateOrderItemsQuery As String = "
                UPDATE order_items 
                SET quantity = @quantity, 
                    unit_price = @unitPrice 
                WHERE order_id = @orderId 
                AND model_id = @modelId 
                AND branch_id = @branchId"

                Using cmdOrderItems As New MySqlCommand(updateOrderItemsQuery, conn, transaction)
                    cmdOrderItems.Parameters.AddWithValue("@quantity", newQuantity)
                    cmdOrderItems.Parameters.AddWithValue("@unitPrice", newUnitPrice)
                    cmdOrderItems.Parameters.AddWithValue("@orderId", orderId)
                    cmdOrderItems.Parameters.AddWithValue("@modelId", Convert.ToInt32(dgvOR.CurrentRow.Cells("model_id").Value))
                    cmdOrderItems.Parameters.AddWithValue("@branchId", Convert.ToInt32(dgvOR.CurrentRow.Cells("branch_id").Value))
                    cmdOrderItems.ExecuteNonQuery()
                End Using

                ' Update stock based on quantity change
                If quantityDifference <> 0 Then
                    Dim stockUpdateQuery As String = "
                    UPDATE branch_model 
                    SET stock = stock - @quantityDifference 
                    WHERE model_id = @modelId AND branch_id = @branchId"

                    Using cmdStock As New MySqlCommand(stockUpdateQuery, conn, transaction)
                        cmdStock.Parameters.AddWithValue("@quantityDifference", quantityDifference)
                        cmdStock.Parameters.AddWithValue("@modelId", Convert.ToInt32(dgvOR.CurrentRow.Cells("model_id").Value))
                        cmdStock.Parameters.AddWithValue("@branchId", Convert.ToInt32(dgvOR.CurrentRow.Cells("branch_id").Value))
                        cmdStock.ExecuteNonQuery()
                    End Using
                End If

                ' If order is cancelled, restock within the same transaction
                If newStatus.ToLower() = "cancelled" Then
                    Dim fetchCmd As New MySqlCommand("SELECT branch_id, model_id, quantity FROM order_items WHERE order_id = @orderId", conn, transaction)
                    fetchCmd.Parameters.AddWithValue("@orderId", orderId)
                    Using reader = fetchCmd.ExecuteReader()
                        While reader.Read()
                            Dim branchId As Integer = Convert.ToInt32(reader("branch_id"))
                            Dim modelId As Integer = Convert.ToInt32(reader("model_id"))
                            Dim qty As Integer = Convert.ToInt32(reader("quantity"))
                            ' Update stock in branch_model within transaction
                            Dim stockCmd As New MySqlCommand("UPDATE branch_model SET stock = stock + @qty WHERE branch_id = @branchId AND model_id = @modelId", conn, transaction)
                            stockCmd.Parameters.AddWithValue("@qty", qty)
                            stockCmd.Parameters.AddWithValue("@branchId", branchId)
                            stockCmd.Parameters.AddWithValue("@modelId", modelId)
                            stockCmd.ExecuteNonQuery()
                        End While
                    End Using
                End If

                transaction.Commit()
            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Error updating order: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try
        End Using

        MessageBox.Show("Order updated successfully including quantity and unit price.")
        LoadOrders()

        ' Refresh profits report if it's currently active and order was changed to delivered
        If newStatus.ToLower() = "delivered" Then
            RefreshProfitsReportIfActive()
        End If
    End Sub

    Private Sub btnORDelete_Click(sender As Object, e As EventArgs) Handles btnORDelete.Click
        If dgvOR.CurrentRow Is Nothing Then
            MessageBox.Show("Select an order to delete.")
            Return
        End If
        Dim result = MessageBox.Show("Are you sure you want to delete this order?", "Confirm", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then Exit Sub
        Dim orderId As Integer = Convert.ToInt32(dgvOR.CurrentRow.Cells("order_id").Value)
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim transaction = conn.BeginTransaction()
            Try
                ' Delete returns first
                Dim deleteReturnsQuery As String = "DELETE FROM returns WHERE order_id = @id"
                Using cmdReturns As New MySqlCommand(deleteReturnsQuery, conn, transaction)
                    cmdReturns.Parameters.AddWithValue("@id", orderId)
                    cmdReturns.ExecuteNonQuery()
                End Using
                ' Delete order_items first (due to FK)
                Dim deleteOrderItemsQuery As String = "DELETE FROM order_items WHERE order_id = @id"
                Using cmdOrderItems As New MySqlCommand(deleteOrderItemsQuery, conn, transaction)
                    cmdOrderItems.Parameters.AddWithValue("@id", orderId)
                    cmdOrderItems.ExecuteNonQuery()
                End Using
                ' Delete order
                Dim deleteOrderQuery As String = "DELETE FROM orders WHERE order_id = @id"
                Using cmdOrder As New MySqlCommand(deleteOrderQuery, conn, transaction)
                    cmdOrder.Parameters.AddWithValue("@id", orderId)
                    cmdOrder.ExecuteNonQuery()
                End Using
                transaction.Commit()
                MessageBox.Show("Order deleted.")
                LoadOrders()
            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Error deleting order: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btnOrderSearch_Click(sender As Object, e As EventArgs) Handles btnOrderSearch.Click
        Dim searchTerm As String = txtOrderSearch.Text.Trim()
        Dim query As String = "
        SELECT o.order_id, o.user_id, c.first_name, c.last_name,
               o.date_ordered, o.est_delivery,
               o.order_status, o.payment_status, o.payment_option,
               COUNT(oi.model_id) as item_count,
               SUM(oi.quantity * oi.unit_price) as total_amount
        FROM orders o
        JOIN accounts c ON o.user_id = c.user_id
        LEFT JOIN order_items oi ON o.order_id = oi.order_id
        WHERE
            o.order_id = @id
            OR o.user_id = @id
            OR c.first_name LIKE @term
            OR c.last_name LIKE @term
            OR o.order_status LIKE @term
            OR o.payment_status Like @term
            OR  o.payment_option like @term
        GROUP BY o.order_id, o.user_id, c.first_name, c.last_name,
                 o.date_ordered, o.est_delivery, o.order_status, 
                 o.payment_status, o.payment_option
        ORDER BY o.order_id DESC
        LIMIT 50"
        Dim dt As New DataTable
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                Dim idVal As Integer
                If Integer.TryParse(searchTerm, idVal) Then
                    cmd.Parameters.AddWithValue("@id", idVal)
                Else
                    cmd.Parameters.AddWithValue("@id", -1)
                End If
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        dgvOR.DataSource = dt
    End Sub

    Private Sub dgvOR_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOR.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        dgvOR.ClearSelection()
        dgvOR.Rows(e.RowIndex).Selected = True
        dgvOR.CurrentCell = dgvOR.Rows(e.RowIndex).Cells(e.ColumnIndex)
        LoadSelectedOrderDetails()
    End Sub

    Private Sub txtORQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtORQuantity.TextChanged
        ' Simple validation - only allow numbers
        If Not String.IsNullOrEmpty(txtORQuantity.Text) Then
            Dim filteredText As String = ""
            For Each c As Char In txtORQuantity.Text
                If Char.IsDigit(c) Then
                    filteredText += c
                End If
            Next
            If filteredText <> txtORQuantity.Text Then
                txtORQuantity.Text = filteredText
                txtORQuantity.SelectionStart = txtORQuantity.Text.Length
            End If
        End If

        ' Calculate subtotal when quantity changes
        CalculateSubtotal()
    End Sub

    Private Sub txtORQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtORQuantity.KeyPress
        ' Allow only numbers and control characters
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtORUNITPRICE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtORUNITPRICE.KeyPress
        ' Allow only numbers, decimal point, and control characters
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If

        ' Allow only one decimal point
        If e.KeyChar = "." AndAlso txtORUNITPRICE.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtORUNITPRICE_TextChanged(sender As Object, e As EventArgs) Handles txtORUNITPRICE.TextChanged
        ' Simple validation - only allow numbers and one decimal point
        If Not String.IsNullOrEmpty(txtORUNITPRICE.Text) Then
            Dim filteredText As String = ""
            Dim decimalCount As Integer = 0

            For Each c As Char In txtORUNITPRICE.Text
                If Char.IsDigit(c) Then
                    filteredText += c
                ElseIf c = "." AndAlso decimalCount = 0 Then
                    filteredText += c
                    decimalCount += 1
                End If
            Next

            If filteredText <> txtORUNITPRICE.Text Then
                txtORUNITPRICE.Text = filteredText
                txtORUNITPRICE.SelectionStart = txtORUNITPRICE.Text.Length
            End If
        End If

        ' Calculate subtotal when unit price changes
        CalculateSubtotal()
    End Sub

    Private Sub dgvOR_SelectionChanged(sender As Object, e As EventArgs) Handles dgvOR.SelectionChanged
        ' Update the quantity and unit price fields when a different row is selected
        If dgvOR.CurrentRow IsNot Nothing Then
            LoadSelectedOrderDetails()
        End If
    End Sub

    Private Sub CalculateSubtotal()
        Try
            Dim quantity As Integer
            Dim unitPrice As Decimal

            If Integer.TryParse(txtORQuantity.Text, quantity) AndAlso Decimal.TryParse(txtORUNITPRICE.Text, unitPrice) Then
                Dim subtotal As Decimal = quantity * unitPrice
                ' You can add a label to display the subtotal if needed
                ' lblSubtotal.Text = subtotal.ToString("N2")
            End If
        Catch ex As Exception
            ' Handle any calculation errors
        End Try
    End Sub
#End Region

#Region "Returns"
    Private Sub btnRETUp_Click(sender As Object, e As EventArgs) Handles btnRETUp.Click
        If dgvRET.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvRET.CurrentRow.Index
            If index > 0 Then
                dgvRET.ClearSelection()
                dgvRET.Rows(index - 1).Selected = True
                dgvRET.CurrentCell = dgvRET.Rows(index - 1).Cells(0)
                LoadSelectedReturnDetails()
            End If
        End If
    End Sub

    Private Sub btnRETDown_Click(sender As Object, e As EventArgs) Handles btnRETDown.Click
        If dgvRET.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvRET.CurrentRow.Index
            If index < dgvRET.Rows.Count - 2 Then ' exclude the new row
                dgvRET.ClearSelection()
                dgvRET.Rows(index + 1).Selected = True
                dgvRET.CurrentCell = dgvRET.Rows(index + 1).Cells(0)
                LoadSelectedReturnDetails()
            End If
        End If
    End Sub
    Private Sub LoadSelectedReturnDetails()
        If dgvRET.CurrentRow IsNot Nothing Then
            Dim row As DataGridViewRow = dgvRET.CurrentRow

            dtRETDate.Value = Convert.ToDateTime(row.Cells("return_date").Value)
            txtRETQuan.Text = row.Cells("quantity_returned").Value.ToString()
            cboRETStatus.SelectedItem = row.Cells("return_status").Value.ToString()

            ' Set the condition if it exists in the row
            If Not IsDBNull(row.Cells("condition_").Value) Then
                cboRETCondition.SelectedItem = row.Cells("condition_").Value.ToString()
            End If
        End If
    End Sub
    Private Sub btnRETUpdate_Click(sender As Object, e As EventArgs) Handles btnRETUpdate.Click
        If dgvRET.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a return to update.")
            Exit Sub
        End If

        Dim row As DataGridViewRow = dgvRET.CurrentRow
        Dim returnId As Integer = Convert.ToInt32(row.Cells("return_id").Value)
        Dim newStatus As String = cboRETStatus.Text

        Dim query As String = "UPDATE returns SET return_date = @return_date, return_status = @return_status, quantity_returned = @quantity_returned, condition_ = @condition WHERE return_id = @return_id"

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@return_date", dtRETDate.Value)
                cmd.Parameters.AddWithValue("@return_status", newStatus)
                cmd.Parameters.AddWithValue("@quantity_returned", Integer.Parse(txtRETQuan.Text))
                cmd.Parameters.AddWithValue("@condition", If(cboRETCondition.SelectedItem IsNot Nothing, cboRETCondition.SelectedItem.ToString(), "Others"))

                cmd.Parameters.AddWithValue("@return_id", returnId)
                cmd.ExecuteNonQuery()
            End Using

            ' If return is approved, restock
            If newStatus.ToLower() = "approved" Then
                Dim fetchCmd As New MySqlCommand("SELECT oi.branch_id, oi.model_id, r.quantity_returned FROM returns r JOIN order_items oi ON r.order_id = oi.order_id WHERE r.return_id = @returnId LIMIT 1", conn)
                fetchCmd.Parameters.AddWithValue("@returnId", returnId)
                Using reader = fetchCmd.ExecuteReader()
                    If reader.Read() Then
                        Dim branchId As Integer = Convert.ToInt32(reader("branch_id"))
                        Dim modelId As Integer = Convert.ToInt32(reader("model_id"))
                        Dim qtyReturned As Integer = Convert.ToInt32(reader("quantity_returned"))
                        reader.Close()
                        ' Update stock in branch_model
                        Dim stockCmd As New MySqlCommand("UPDATE branch_model SET stock = stock + @qty WHERE branch_id = @branchId AND model_id = @modelId", conn)
                        stockCmd.Parameters.AddWithValue("@qty", qtyReturned)
                        stockCmd.Parameters.AddWithValue("@branchId", branchId)
                        stockCmd.Parameters.AddWithValue("@modelId", modelId)
                        stockCmd.ExecuteNonQuery()
                    End If
                End Using
            End If
        End Using

        MessageBox.Show("Return record updated successfully.")
        LoadReturns()
    End Sub

    Private Sub btnRETDelete_Click(sender As Object, e As EventArgs) Handles btnRETDelete.Click
        If dgvRET.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a return to delete.")
            Exit Sub
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this return?", "Confirm Delete", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then Exit Sub

        Dim returnId As Integer = Convert.ToInt32(dgvRET.CurrentRow.Cells("return_id").Value)
        Dim query As String = "DELETE FROM returns WHERE return_id = @return_id"

        Using conn As New MySqlConnection(connStr),
          cmd As New MySqlCommand(query, conn)

            cmd.Parameters.AddWithValue("@return_id", returnId)

            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()

            MessageBox.Show("Return record deleted successfully.")
            LoadReturns()

        End Using
    End Sub

    Private Sub LoadReturnStatuses()
        cboRETStatus.Items.Clear()
        cboRETStatus.Items.Add("Approved")
        cboRETStatus.Items.Add("Rejected")
        cboRETStatus.Items.Add("Pending")
    End Sub

    Private Sub LoadReturnConditions()
        cboRETCondition.Items.Clear()
        cboRETCondition.Items.Add("Damaged")
        cboRETCondition.Items.Add("Defect")
        cboRETCondition.Items.Add("Others")
    End Sub

    Private Sub LoadReturns()
        Dim query As String = "SELECT r.return_id, r.order_id, r.return_date, r.reason, r.condition_, r.return_status, r.quantity_returned, " &
                              "a.first_name, a.last_name, m.model_name, b.branch_name " &
                              "FROM returns r " &
                              "LEFT JOIN orders o ON r.order_id = o.order_id " &
                              "LEFT JOIN accounts a ON o.user_id = a.user_id " &
                              "LEFT JOIN order_items oi ON o.order_id = oi.order_id " &
                              "LEFT JOIN models m ON oi.model_id = m.model_id " &
                              "LEFT JOIN branches b ON oi.branch_id = b.branch_id " &
                              "ORDER BY r.return_date DESC"
        Dim dt As New DataTable()

        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        dgvRET.DataSource = dt

        ' Set column headers
        If dgvRET.Columns.Count > 0 Then
            dgvRET.Columns("return_id").HeaderText = "Return ID"
            dgvRET.Columns("order_id").HeaderText = "Order ID"
            dgvRET.Columns("return_date").HeaderText = "Return Date"
            dgvRET.Columns("reason").HeaderText = "Reason"
            dgvRET.Columns("condition_").HeaderText = "Condition"
            dgvRET.Columns("return_status").HeaderText = "Status"
            dgvRET.Columns("quantity_returned").HeaderText = "Qty Returned"
            dgvRET.Columns("first_name").HeaderText = "Customer First Name"
            dgvRET.Columns("last_name").HeaderText = "Customer Last Name"
            dgvRET.Columns("model_name").HeaderText = "Model"
            dgvRET.Columns("branch_name").HeaderText = "Branch"
        End If
    End Sub

    Private Function GetBranchIDFromOrder(orderId As Integer) As Integer
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT DISTINCT branch_id FROM order_items WHERE order_id = @orderId LIMIT 1"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@orderId", orderId)

                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    Return Convert.ToInt32(result)
                End If
            End Using
        Catch ex As Exception
            MsgBox("Failed to get branch_id: " & ex.Message)
        End Try

        Return -1 ' Invalid branch
    End Function

    Private Sub dgvRET_SelectionChanged(sender As Object, e As EventArgs) Handles dgvRET.SelectionChanged
        If dgvRET.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = dgvRET.SelectedRows(0)
            Dim orderId As Integer = Convert.ToInt32(selectedRow.Cells("order_id").Value)
            LoadSelectedReturnDetails()
        End If
    End Sub

    Private Sub dgvRET_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRET.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        dgvRET.ClearSelection()
        dgvRET.Rows(e.RowIndex).Selected = True
        dgvRET.CurrentCell = dgvRET.Rows(e.RowIndex).Cells(e.ColumnIndex)
        LoadSelectedReturnDetails()
    End Sub

    Private Sub btnRETSearch_Click(sender As Object, e As EventArgs) Handles btnRETSearch.Click
        Dim searchTerm As String = txtRETSearch.Text.Trim()
        Dim query As String = "SELECT r.return_id, r.order_id, r.return_date, r.reason, r.condition_, r.return_status, r.quantity_returned, " &
                              "a.first_name, a.last_name, m.model_name, b.branch_name " &
                              "FROM returns r " &
                              "LEFT JOIN orders o ON r.order_id = o.order_id " &
                              "LEFT JOIN accounts a ON o.user_id = a.user_id " &
                              "LEFT JOIN order_items oi ON o.order_id = oi.order_id " &
                              "LEFT JOIN models m ON oi.model_id = m.model_id " &
                              "LEFT JOIN branches b ON oi.branch_id = b.branch_id " &
                              "WHERE r.return_id = @id OR r.order_id = @id OR r.return_status LIKE @term OR r.condition_ LIKE @term OR r.reason LIKE @term OR a.first_name LIKE @term OR a.last_name LIKE @term OR m.model_name LIKE @term " &
                              "ORDER BY r.return_date DESC LIMIT 50"
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                Dim idVal As Integer
                If Integer.TryParse(searchTerm, idVal) Then
                    cmd.Parameters.AddWithValue("@id", idVal)
                Else
                    cmd.Parameters.AddWithValue("@id", -1)
                End If
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        dgvRET.DataSource = dt
    End Sub


#End Region

#Region "Expenses"
    Private Sub LoadExpenseCombos()
        ' Expense Types
        cboEXtype.Items.Clear()
        cboEXtype.Items.AddRange(New String() {"Electricity", "Water", "Rent", "Salaries", "Supplies", "Maintenance", "Marketing", "Others"})
        cboEXtype.SelectedIndex = -1

        ' Statuses
        cboEXStatus.Items.Clear()
        cboEXStatus.Items.AddRange(New String() {"Paid", "Overdue", "Processing", "Cancelled"})
        cboEXStatus.SelectedIndex = -1
    End Sub

    Private Sub btnEXUp_Click(sender As Object, e As EventArgs) Handles btnEXUp.Click
        If dgvEX.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvEX.CurrentRow.Index
            If index > 0 Then
                dgvEX.ClearSelection()
                dgvEX.Rows(index - 1).Selected = True
                dgvEX.CurrentCell = dgvEX.Rows(index - 1).Cells(0)
            End If
        End If
    End Sub

    Private Sub btnEXDown_Click(sender As Object, e As EventArgs) Handles btnEXDown.Click
        If dgvEX.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvEX.CurrentRow.Index
            If index < dgvEX.Rows.Count - 2 Then ' -2 in case of new row
                dgvEX.ClearSelection()
                dgvEX.Rows(index + 1).Selected = True
                dgvEX.CurrentCell = dgvEX.Rows(index + 1).Cells(0)
            End If
        End If
    End Sub

    Private Sub btnEXInsert_Click(sender As Object, e As EventArgs) Handles btnEXInsert.Click
        ' Validation
        If cboEXtype.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(txtEXfee.Text) OrElse
           cboEXStatus.SelectedIndex = -1 OrElse cboEXBranch.SelectedIndex = -1 Then
            MessageBox.Show("Please fill in all fields.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim branchId As Integer = CInt(cboEXBranch.SelectedValue)

        Try
            Using conn As New MySqlConnection(connStr) 'MP3 - Expense CRUD
                conn.Open()
                Dim query As String = "INSERT INTO expenses (branch_id, expense_type, fee, status, from_date, due_date) VALUES (@branch_id, @type, @fee, @status, @from, @due)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@branch_id", branchId)
                    cmd.Parameters.AddWithValue("@type", cboEXtype.Text)
                    cmd.Parameters.AddWithValue("@fee", Decimal.Parse(txtEXfee.Text))
                    cmd.Parameters.AddWithValue("@status", cboEXStatus.Text)
                    cmd.Parameters.AddWithValue("@from", dtEXfrom.Value.Date)
                    cmd.Parameters.AddWithValue("@due", dtEXto.Value.Date)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("Expense inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadExpenses()
        Catch ex As Exception
            MessageBox.Show("Error inserting expense: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEXUpdate_Click(sender As Object, e As EventArgs) Handles btnEXUpdate.Click
        If dgvEX.SelectedRows.Count = 0 Then
            MessageBox.Show("Select an expense to update.")
            Return
        End If

        If cboEXBranch.SelectedIndex = -1 Then
            MessageBox.Show("Please select a valid branch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim expId As Integer = CInt(dgvEX.SelectedRows(0).Cells("exp_id").Value)
        Dim branchId As Integer = CInt(cboEXBranch.SelectedValue)

        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "UPDATE expenses SET branch_id=@branch_id, expense_type=@type, fee=@fee, status=@status, from_date=@from, due_date=@due WHERE exp_id=@id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@branch_id", branchId)
                    cmd.Parameters.AddWithValue("@type", cboEXtype.Text)
                    cmd.Parameters.AddWithValue("@fee", Decimal.Parse(txtEXfee.Text))
                    cmd.Parameters.AddWithValue("@status", cboEXStatus.Text)
                    cmd.Parameters.AddWithValue("@from", dtEXfrom.Value.Date)
                    cmd.Parameters.AddWithValue("@due", dtEXto.Value.Date)
                    cmd.Parameters.AddWithValue("@id", expId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("Expense updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadExpenses()
        Catch ex As Exception
            MessageBox.Show("Error updating expense: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEXDelete_Click(sender As Object, e As EventArgs) Handles btnEXDelete.Click
        If dgvEX.SelectedRows.Count = 0 Then
            MessageBox.Show("Select an expense to delete.")
            Return
        End If

        Dim expId As Integer = CInt(dgvEX.SelectedRows(0).Cells("exp_id").Value)
        If MessageBox.Show("Are you sure you want to delete this expense?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then
            Return
        End If

        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "DELETE FROM expenses WHERE exp_id=@id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", expId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("Expense deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadExpenses()
        Catch ex As Exception
            MessageBox.Show("Error deleting expense: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadExpenses()
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT e.exp_id, b.branch_name, e.expense_type, e.fee, e.status, e.from_date, e.due_date, e.branch_id " &
                                  "FROM expenses e " &
                                  "JOIN branches b ON e.branch_id = b.branch_id " &
                                  "ORDER BY e.due_date DESC"
                Dim cmd As New MySqlCommand(query, conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                dgvEX.DataSource = dt
                If dgvEX.Columns.Count > 0 Then
                    dgvEX.Columns("exp_id").HeaderText = "Expense ID"
                    dgvEX.Columns("branch_name").HeaderText = "Branch"
                    dgvEX.Columns("expense_type").HeaderText = "Type"
                    dgvEX.Columns("fee").HeaderText = "Amount"
                    dgvEX.Columns("status").HeaderText = "Status"
                    dgvEX.Columns("from_date").HeaderText = "From"
                    dgvEX.Columns("due_date").HeaderText = "Due"
                    dgvEX.Columns("branch_id").Visible = False ' Hide branch_id column
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading expenses: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadBranchesToCboEXBranch()
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT branch_id, branch_name FROM branches"
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                cboEXBranch.DataSource = dt
                cboEXBranch.DisplayMember = "branch_name"
                cboEXBranch.ValueMember = "branch_id"
                cboEXBranch.SelectedIndex = -1
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading branches: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvEX_SelectionChanged(sender As Object, e As EventArgs) Handles dgvEX.SelectionChanged
        If dgvEX.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = dgvEX.SelectedRows(0)
            cboEXBranch.SelectedValue = row.Cells("branch_id").Value
            cboEXtype.Text = row.Cells("expense_type").Value.ToString()
            txtEXfee.Text = row.Cells("fee").Value.ToString()
            cboEXStatus.Text = row.Cells("status").Value.ToString()
            dtEXfrom.Value = Convert.ToDateTime(row.Cells("from_date").Value)
            dtEXto.Value = Convert.ToDateTime(row.Cells("due_date").Value)
        End If
    End Sub

    Private Sub btnEXBack_MouseClick(sender As Object, e As MouseEventArgs) Handles btnEXBack.MouseClick
        EXpanel.Hide()
        manage_panel.Show()
    End Sub

    Private Sub btnEXSearch_Click(sender As Object, e As EventArgs) Handles btnEXSearch.Click
        Dim searchTerm As String = txtEXSearch.Text.Trim()
        Dim query As String = "SELECT e.exp_id, b.branch_name, e.expense_type, e.fee, e.status, e.from_date, e.due_date, e.branch_id FROM expenses e JOIN branches b ON e.branch_id = b.branch_id WHERE e.exp_id = @id OR b.branch_name LIKE @term OR e.expense_type LIKE @term OR e.fee LIKE @term OR e.status LIKE @term ORDER BY e.due_date DESC LIMIT 50"
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                Dim idVal As Integer
                If Integer.TryParse(searchTerm, idVal) Then
                    cmd.Parameters.AddWithValue("@id", idVal)
                Else
                    cmd.Parameters.AddWithValue("@id", -1)
                End If
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        dgvEX.DataSource = dt
    End Sub

    Private Sub dgvEX_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEX.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        dgvEX.ClearSelection()
        dgvEX.Rows(e.RowIndex).Selected = True
        dgvEX.CurrentCell = dgvEX.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If dgvEX.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = dgvEX.Rows(e.RowIndex)
            cboEXBranch.SelectedValue = row.Cells("branch_id").Value
            cboEXtype.Text = row.Cells("expense_type").Value.ToString()
            txtEXfee.Text = row.Cells("fee").Value.ToString()
            cboEXStatus.Text = row.Cells("status").Value.ToString()
            dtEXfrom.Value = Convert.ToDateTime(row.Cells("from_date").Value)
            dtEXto.Value = Convert.ToDateTime(row.Cells("due_date").Value)
        End If
    End Sub
#End Region

#Region "Vouchers"
    Private Sub btnVSearch_Click(sender As Object, e As EventArgs) Handles btnVSearch.Click
        Dim searchTerm As String = txtVSearch.Text.Trim()
        Dim query As String = "SELECT * FROM vouchers WHERE voucher_code LIKE @term OR status LIKE @term OR percent_sale LIKE @term ORDER BY to_date DESC LIMIT 50"
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connStr)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
                conn.Open()
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        dgvV.DataSource = dt
    End Sub

    Private Sub dgvV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvV.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        dgvV.ClearSelection()
        dgvV.Rows(e.RowIndex).Selected = True
        dgvV.CurrentCell = dgvV.Rows(e.RowIndex).Cells(e.ColumnIndex)
        ' Load voucher details into controls
        If dgvV.CurrentRow IsNot Nothing Then
            Dim row As DataGridViewRow = dgvV.Rows(e.RowIndex)
            txtVcode.Text = row.Cells("voucher_code").Value.ToString()
            txtVsale.Text = row.Cells("percent_sale").Value.ToString()
            dtVFrom.Value = Convert.ToDateTime(row.Cells("from_date").Value)
            dtVto.Value = Convert.ToDateTime(row.Cells("to_date").Value)
            cboVStatus.Text = row.Cells("status").Value.ToString()
        End If
    End Sub

    Private Sub LoadVoucherStatusCombo()
        cboVStatus.Items.Clear()
        cboVStatus.Items.AddRange(New String() {"active", "expired", "used", "inactive"})
        cboVStatus.SelectedIndex = -1
    End Sub
    Private Sub LoadVouchers()
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT voucher_code, percent_sale, from_date, to_date, status FROM vouchers ORDER BY to_date DESC"
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                dgvV.DataSource = dt
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading vouchers: " & ex.Message)
        End Try
    End Sub

    Private Sub btnVoucherInsert_Click(sender As Object, e As EventArgs) Handles bntVinsert.Click
        If String.IsNullOrWhiteSpace(txtVcode.Text) OrElse cboVStatus.SelectedIndex = -1 Then
            MessageBox.Show("Please fill in all required fields.")
            Return
        End If

        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "INSERT INTO vouchers (voucher_code, percent_sale, from_date, to_date, status) VALUES (@code, @percent, @from, @to, @status)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@code", txtVcode.Text.Trim())
                    cmd.Parameters.AddWithValue("@percent", txtVsale.Text.Trim())
                    cmd.Parameters.AddWithValue("@from", dtVFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@to", dtVto.Value.Date)
                    cmd.Parameters.AddWithValue("@status", cboVStatus.Text)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("Voucher inserted successfully!")
            LoadVouchers()
        Catch ex As Exception
            MessageBox.Show("Error inserting voucher: " & ex.Message)
        End Try
    End Sub
    Private Sub btnVoucherUpdate_Click(sender As Object, e As EventArgs) Handles btnVupdate.Click
        If dgvV.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a voucher to update.")
            Return
        End If

        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "UPDATE vouchers SET percent_sale=@percent, from_date=@from, to_date=@to, status=@status WHERE voucher_code=@code"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@code", txtVcode.Text.Trim())
                    cmd.Parameters.AddWithValue("@percent", txtVsale.Text.Trim())
                    cmd.Parameters.AddWithValue("@from", dtVFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@to", dtVto.Value.Date)
                    cmd.Parameters.AddWithValue("@status", cboVStatus.Text)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("Voucher updated successfully!")
            LoadVouchers()
        Catch ex As Exception
            MessageBox.Show("Error updating voucher: " & ex.Message)
        End Try
    End Sub

    Private Sub btnVoucherDelete_Click(sender As Object, e As EventArgs) Handles btnVdelete.Click
        If dgvV.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a voucher to delete.")
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete this voucher?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then
            Return
        End If

        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "DELETE FROM vouchers WHERE voucher_code=@code"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@code", txtVcode.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("Voucher deleted successfully!")
            LoadVouchers()
        Catch ex As Exception
            MessageBox.Show("Error deleting voucher: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvVouchers_SelectionChanged(sender As Object, e As EventArgs) Handles dgvV.SelectionChanged
        If dgvV.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = dgvV.SelectedRows(0)
            txtVcode.Text = row.Cells("voucher_code").Value.ToString()
            txtVsale.Text = Convert.ToInt32(row.Cells("percent_sale").Value)
            dtVFrom.Value = Convert.ToDateTime(row.Cells("from_date").Value)
            dtVto.Value = Convert.ToDateTime(row.Cells("to_date").Value)
            cboVStatus.Text = row.Cells("status").Value.ToString()
        End If
    End Sub

    Private Sub btnVup_Click(sender As Object, e As EventArgs) Handles btnVUp.Click
        If dgvV.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvV.CurrentRow.Index
            If index > 0 Then
                dgvV.ClearSelection()
                dgvV.Rows(index - 1).Selected = True
                dgvV.CurrentCell = dgvV.Rows(index - 1).Cells(0)
            End If
        End If
    End Sub

    Private Sub btnVdown_Click(sender As Object, e As EventArgs) Handles btnVdown.Click
        If dgvV.CurrentRow IsNot Nothing Then
            Dim index As Integer = dgvV.CurrentRow.Index
            If index < dgvV.Rows.Count - 2 Then ' -2 in case of new row
                dgvV.ClearSelection()
                dgvV.Rows(index + 1).Selected = True
                dgvV.CurrentCell = dgvV.Rows(index + 1).Cells(0)
            End If
        End If
    End Sub

#End Region

#Region "Position"

    ' Show POSPanel when btnPosition is clicked
    Private Sub btnPosition_Click(sender As Object, e As EventArgs) Handles btnPosition.Click
        allPanelsClose()
        POSPanel.Show()
        LoadPositions()
    End Sub

    ' Hide POSPanel and go back
    Private Sub btnPOSBack_Click(sender As Object, e As EventArgs) Handles btnPOSBack.Click
        POSPanel.Hide()
        manage_panel.Show()
    End Sub

    ' Load all positions into dgvPOS
    Private Sub LoadPositions(Optional search As String = "")
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT position_id, position_name, monthly_rate FROM Positions"
                If Not String.IsNullOrWhiteSpace(search) Then
                    query &= " WHERE position_name LIKE @search"
                End If
                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrWhiteSpace(search) Then
                        cmd.Parameters.AddWithValue("@search", "%" & search & "%")
                    End If
                    Dim dt As New DataTable()
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                    dgvPOS.DataSource = dt
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading positions: " & ex.Message)
        End Try
    End Sub

    ' Search positions
    Private Sub btnPOSSearch_Click(sender As Object, e As EventArgs) Handles btnPOSSEarch.Click
        LoadPositions(txtPOSSearch.Text)
    End Sub

    ' Insert new position
    Private Sub btnPOSInsert_Click(sender As Object, e As EventArgs) Handles btnPOSInsert.Click
        If String.IsNullOrWhiteSpace(txtPOSName.Text) OrElse String.IsNullOrWhiteSpace(txtPOSRate.Text) Then
            MessageBox.Show("Please fill in all fields.")
            Return
        End If
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "INSERT INTO Positions (position_name, monthly_rate) VALUES (@name, @rate)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", txtPOSName.Text)
                    cmd.Parameters.AddWithValue("@rate", Decimal.Parse(txtPOSRate.Text))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("Position added.")
            LoadPositions()
            txtPOSName.Clear()
            txtPOSRate.Clear()
        Catch ex As Exception
            MessageBox.Show("Error inserting position: " & ex.Message)
        End Try
    End Sub

    ' Update selected position
    Private Sub btnPOSUpdate_Click(sender As Object, e As EventArgs) Handles btnPOSUpdate.Click
        If dgvPOS.CurrentRow Is Nothing Then
            MessageBox.Show("Select a position to update.")
            Return
        End If
        If String.IsNullOrWhiteSpace(txtPOSName.Text) OrElse String.IsNullOrWhiteSpace(txtPOSRate.Text) Then
            MessageBox.Show("Please fill in all fields.")
            Return
        End If
        Dim posId As Integer = Convert.ToInt32(dgvPOS.CurrentRow.Cells("position_id").Value)
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                Dim query As String = "UPDATE Positions SET position_name=@name, monthly_rate=@rate WHERE position_id=@id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", txtPOSName.Text)
                    cmd.Parameters.AddWithValue("@rate", Decimal.Parse(txtPOSRate.Text))
                    cmd.Parameters.AddWithValue("@id", posId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("Position updated.")
            LoadPositions()
        Catch ex As Exception
            MessageBox.Show("Error updating position: " & ex.Message)
        End Try
    End Sub

    Private Sub btnPOSDelete_Click(sender As Object, e As EventArgs) Handles btnPOSDelete.Click
        If dgvPOS.CurrentRow Is Nothing Then
            MessageBox.Show("Select a position to delete.")
            Return
        End If
        Dim posId As Integer = Convert.ToInt32(dgvPOS.CurrentRow.Cells("position_id").Value)
        If MessageBox.Show("Are you sure you want to delete this position?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then Return
        Try
            Using conn As New MySqlConnection(connStr)
                conn.Open()
                ' First, update employees to set current_position_id to NULL for this position
                Dim updateEmpPos As New MySqlCommand("UPDATE employees SET current_position_id = NULL WHERE current_position_id = @id", conn)
                updateEmpPos.Parameters.AddWithValue("@id", posId)
                updateEmpPos.ExecuteNonQuery()
                ' Then, delete from positions
                Dim delPos As New MySqlCommand("DELETE FROM Positions WHERE position_id=@id", conn)
                delPos.Parameters.AddWithValue("@id", posId)
                delPos.ExecuteNonQuery()
            End Using
            MessageBox.Show("Position deleted.")
            LoadPositions()
            txtPOSName.Clear()
            txtPOSRate.Clear()
        Catch ex As Exception
            MessageBox.Show("Error deleting position: " & ex.Message)
        End Try
    End Sub

    ' Populate fields when selecting a row
    Private Sub dgvPOS_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPOS.SelectionChanged
        If dgvPOS.CurrentRow IsNot Nothing Then
            txtPOSName.Text = dgvPOS.CurrentRow.Cells("position_name").Value.ToString()
            txtPOSRate.Text = dgvPOS.CurrentRow.Cells("monthly_rate").Value.ToString()
        End If
    End Sub

    ' Navigate up
    Private Sub btnPOSUP_Click(sender As Object, e As EventArgs) Handles btnPOSUP.Click
        Dim dgv As DataGridView = dgvPOS
        If dgv.CurrentRow IsNot Nothing AndAlso dgv.CurrentRow.Index > 0 Then
            Dim prevRowIndex As Integer = dgv.CurrentRow.Index - 1
            dgv.ClearSelection()
            dgv.Rows(prevRowIndex).Selected = True
            dgv.CurrentCell = dgv.Rows(prevRowIndex).Cells(0)
            LoadPOSFieldsFromRow(prevRowIndex)
        End If
    End Sub

    ' Navigate down
    Private Sub btnPOSDown_Click(sender As Object, e As EventArgs) Handles btnPOSDown.Click
        Dim dgv As DataGridView = dgvPOS
        ' Use Rows.Count - 1 because the last row is often the blank "new row"
        If dgv.CurrentRow IsNot Nothing AndAlso dgv.CurrentRow.Index < dgv.Rows.Count - 2 Then
            Dim nextRowIndex As Integer = dgv.CurrentRow.Index + 1
            dgv.ClearSelection()
            dgv.Rows(nextRowIndex).Selected = True
            dgv.CurrentCell = dgv.Rows(nextRowIndex).Cells(0)
            LoadPOSFieldsFromRow(nextRowIndex)
        End If
    End Sub

    Private Sub LoadPOSFieldsFromRow(rowIndex As Integer)
        If rowIndex >= 0 AndAlso rowIndex < dgvPOS.Rows.Count Then
            Dim row = dgvPOS.Rows(rowIndex)
            If Not row.IsNewRow Then
                txtPOSName.Text = row.Cells("position_name").Value.ToString()
                txtPOSRate.Text = row.Cells("monthly_rate").Value.ToString()
            End If
        End If
    End Sub



#End Region

#End Region

#Region "Inventory"
    Private navigationStack As New Stack(Of Action) 'AI-Code
    Private Sub lblInventory_Click(sender As Object, e As EventArgs) Handles lblInventory.Click
        allPanelsClose()
        manage_panel.Hide()
        home_flow.Show()
        LoadBranches2()
    End Sub

    Private Function GetImageFromBytes(bytes As Byte()) As Image
        Using ms As New IO.MemoryStream(bytes)
            Return Image.FromStream(ms)
        End Using
    End Function
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If navigationStack.Count > 0 Then
            Dim goBack = navigationStack.Pop()
            goBack.Invoke()
        Else
            ' Optionally, hide the back button or go to a default panel
            btnBack.Hide()
            ' Or show a message, or navigate to home
            ' home_panel.Show()
        End If
    End Sub

    Private Sub LoadBranches2()
        lblStatus.Text = "SELECT YOUR BRANCH:"
        btnBack.Hide()
        home_flow.Controls.Clear()
        navigationStack.Clear() ' Clear all history 

        Dim query As String = "SELECT branch_id, branch_name, address, phone_number, photo FROM Branches"

        Using conn As New MySqlConnection(Me.connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim branchId As Integer = CInt(reader("branch_id"))
                ' Query number of models for this branch
                Dim modelCount As Integer = 0
                Dim empCount As Integer = 0
                Using conn2 As New MySqlConnection(Me.connStr)
                    conn2.Open()
                    ' Count models
                    Using cmdModels As New MySqlCommand("SELECT COUNT(*) FROM Branch_Model WHERE branch_id = @bid", conn2)
                        cmdModels.Parameters.AddWithValue("@bid", branchId)
                        modelCount = Convert.ToInt32(cmdModels.ExecuteScalar())
                    End Using
                    ' Count employees
                    Using cmdEmp As New MySqlCommand("SELECT COUNT(*) FROM employees WHERE current_branch_id = @bid", conn2)
                        cmdEmp.Parameters.AddWithValue("@bid", branchId)
                        empCount = Convert.ToInt32(cmdEmp.ExecuteScalar())
                    End Using
                End Using

                Dim card As New Panel With {
                    .Size = New Size(220, 250),
                    .BackColor = Color.White,
                    .Margin = New Padding(12),
                    .BorderStyle = BorderStyle.FixedSingle,
                    .Cursor = Cursors.Hand,
                    .Padding = New Padding(0, 0, 0, 0)
                }



                ' Branch name
                Dim lblName As New Label With {
                .Text = reader("branch_name").ToString(),
                .Dock = DockStyle.Top,
                .Font = New Font("FuturaBookCTT", 15.5F, FontStyle.Bold),
                .TextAlign = ContentAlignment.MiddleCenter,
                .ForeColor = Color.Black,
                .Height = 32,
                .Padding = New Padding(0, 8, 0, 0)
            }

                card.Controls.Add(lblName)
                ' Address with icon
                Dim lblAddr As New Label With {
                .Text = "📍 " & reader("address").ToString(),
                .Dock = DockStyle.Bottom,
                .Font = New Font("FuturaBookCTT", 11.8F, FontStyle.Italic),
                .TextAlign = ContentAlignment.MiddleLeft,
                .ForeColor = Color.DimGray,
                .Height = 25,
                .Padding = New Padding(8, 0, 0, 0)
            }
                card.Controls.Add(lblAddr)

                ' Phone with icon
                Dim lblNumber As New Label With {
                .Text = "☎ " & reader("phone_number").ToString(),
                .Dock = DockStyle.Bottom,
                .Font = New Font("FuturaBookCTT", 11.8F, FontStyle.Italic),
                .TextAlign = ContentAlignment.MiddleLeft,
                .ForeColor = Color.DimGray,
                .Height = 25,
                .Padding = New Padding(8, 0, 0, 0)
            }
                card.Controls.Add(lblNumber)

                ' Number of models label
                Dim lblModels As New Label With {
                    .Text = "🏍 Models: " & modelCount.ToString(),
                    .Dock = DockStyle.Bottom,
                    .Font = New Font("FuturaBookCTT", 11.8F, FontStyle.Italic),
                    .ForeColor = Color.DimGray,
                    .Height = 25,
                    .TextAlign = ContentAlignment.MiddleLeft,
                    .Padding = New Padding(8, 0, 0, 0)
                }
                card.Controls.Add(lblModels)

                ' Number of employees label
                Dim lblEmps As New Label With {
                    .Text = "👤 Employees: " & empCount.ToString(),
                    .Dock = DockStyle.Bottom,
                    .Font = New Font("FuturaBookCTT", 11.8F, FontStyle.Italic),
                    .ForeColor = Color.DimGray,
                    .Height = 25,
                    .TextAlign = ContentAlignment.MiddleLeft,
                    .Padding = New Padding(8, 0, 0, 0)
                }


                card.Controls.Add(lblEmps)

                ' Branch image
                Dim picBox As New PictureBox With {
                .Size = New Size(220, 110),
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Image = GetImageFromBytes(CType(reader("photo"), Byte())),
                .Dock = DockStyle.Top,
                .BorderStyle = BorderStyle.FixedSingle
            }

                card.Controls.Add(picBox)
                ' Click handler
                Dim thisBranchId As Integer = CInt(reader("branch_id"))
                Dim clickHandler As EventHandler = Sub(sender2, e2) LoadBrands(thisBranchId)
                AddHandler card.Click, clickHandler
                AddHandler picBox.Click, clickHandler
                AddHandler lblName.Click, clickHandler
                AddHandler lblAddr.Click, clickHandler
                AddHandler lblNumber.Click, clickHandler

                home_flow.Controls.Add(card)
            End While
        End Using
    End Sub
    Private Sub LoadBrands(branchId As Integer)
        lblStatus.Text = "SELECT YOUR BRAND:"
        btnBack.Show()
        navigationStack.Push(Sub() LoadBranches2())
        home_flow.Controls.Clear()

        Dim query As String = "SELECT m.brand, COUNT(*) AS model_count " &
                          "FROM Models m " &
                          "JOIN Branch_Model bm ON m.model_id = bm.model_id " &
                          "WHERE bm.branch_id = @branchId " &
                          "GROUP BY m.brand"

        Using conn As New MySqlConnection(Me.connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@branchId", branchId)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim brandName As String = reader("brand").ToString()
                Dim modelCount As Integer = Convert.ToInt32(reader("model_count"))

                Dim card As New Panel With {
                .Size = New Size(180, 80),
                .BackColor = Color.White,
                .BorderStyle = BorderStyle.FixedSingle,
                .Margin = New Padding(14),
                .Cursor = Cursors.Hand
            }

                Dim lblBrand As New Label With {
                .Text = brandName,
                .Font = New Font("FuturaBookCTT", 16, FontStyle.Bold),
                .ForeColor = Color.Black,
                .Location = New Point(0, 8),
                .Width = 180,
                .Height = 32,
                .TextAlign = ContentAlignment.MiddleCenter
            }
                card.Controls.Add(lblBrand)

                Dim lblCount As New Label With {
                .Text = "🏍 Models: " & modelCount.ToString(),
                .Font = New Font("FuturaBookCTT", 12, FontStyle.Italic),
                .ForeColor = Color.DimGray,
                .Location = New Point(0, 44),
                .Width = 180,
                .Height = 24,
                .TextAlign = ContentAlignment.MiddleCenter
            }
                card.Controls.Add(lblCount)

                ' Hover effect
                AddHandler card.MouseEnter, Sub() card.BackColor = Color.Gainsboro
                AddHandler card.MouseLeave, Sub() card.BackColor = Color.White

                ' Click handler
                AddHandler card.Click, Sub() LoadModels(branchId, brandName)
                AddHandler lblBrand.Click, Sub() LoadModels(branchId, brandName)
                AddHandler lblCount.Click, Sub() LoadModels(branchId, brandName)

                home_flow.Controls.Add(card)
            End While
        End Using
    End Sub

    Private Sub LoadModels(branchId As Integer, brand As String)
        lblStatus.Text = "SELECT YOUR MODEL:"
        navigationStack.Push(Sub() LoadBrands(branchId))
        home_flow.Controls.Clear()
        btnBack.Show()

        Dim query As String = "SELECT m.model_id, m.model_name, m.photo, m.year_model, m.type, m.transmission, m.price, bm.stock
                           FROM Models m
                           JOIN Branch_Model bm ON m.model_id = bm.model_id
                           WHERE bm.branch_id = @branchId AND m.brand = @brand"

        Using conn As New MySqlConnection(Me.connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@branchId", branchId)
            cmd.Parameters.AddWithValue("@brand", brand)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim thisModelId As Integer = CInt(reader("model_id"))
                Dim modelName As String = reader("model_name").ToString()
                Dim yearModel As String = reader("year_model").ToString()
                Dim type As String = reader("type").ToString()
                Dim transmission As String = reader("transmission").ToString()
                Dim price As Decimal = CDec(reader("price"))
                Dim stock As Integer = If(IsDBNull(reader("stock")), 0, CInt(reader("stock")))
                Dim photoBytes As Byte() = CType(reader("photo"), Byte())

                ' --- Card Panel ---
                Dim card As New Panel With {
                .Size = New Size(260, 320),
                .BackColor = Color.White,
                .Margin = New Padding(15),
                .BorderStyle = BorderStyle.FixedSingle,
                .Padding = New Padding(10)
            }

                ' --- Product Image ---
                Dim pic As New PictureBox With {
                .Height = 140,
                .Width = 240,
                .Dock = DockStyle.Top,
                .Image = GetImageFromBytes(photoBytes),
                .SizeMode = PictureBoxSizeMode.Zoom,
                .BorderStyle = BorderStyle.FixedSingle,
                .BackColor = Color.White
            }

                ' --- Model Name ---
                Dim lblName As New Label With {
                .Text = modelName,
                .Dock = DockStyle.Top,
                .Font = New Font("Futura-Medium", 16, FontStyle.Bold),
                .ForeColor = Color.Black,
                .Height = 32,
                .TextAlign = ContentAlignment.MiddleCenter
            }

                ' --- Year, Type, Transmission ---
                Dim lblYear As New Label With {
                .Text = $"Year: {yearModel}",
                .Dock = DockStyle.Top,
                .Font = New Font("Futura-Medium", 12, FontStyle.Italic),
                .ForeColor = Color.DimGray,
                .Height = 22,
                .TextAlign = ContentAlignment.MiddleCenter
            }
                Dim lblType As New Label With {
                .Text = $"Type: {type}",
                .Dock = DockStyle.Top,
                .Font = New Font("Futura-Medium", 12, FontStyle.Italic),
                .ForeColor = Color.DimGray,
                .Height = 22,
                .TextAlign = ContentAlignment.MiddleCenter
            }
                Dim lblTrans As New Label With {
                .Text = $"Transmission: {transmission}",
                .Dock = DockStyle.Top,
                .Font = New Font("Futura-Medium", 12, FontStyle.Italic),
                .ForeColor = Color.DimGray,
                .Height = 22,
                .TextAlign = ContentAlignment.MiddleCenter
            }

                ' --- Price ---
                Dim lblPrice As New Label With {
                .Text = $"₱{price:N2}",
                .Dock = DockStyle.Top,
                .Font = New Font("Futura-Medium", 15, FontStyle.Bold),
                .ForeColor = Color.Red,
                .Height = 30,
                .TextAlign = ContentAlignment.MiddleCenter
            }

                ' --- Stock ---
                Dim lblStock As New Label With {
                .Text = $"Stock: {stock}",
                .Dock = DockStyle.Top,
                .Font = New Font("Futura-Medium", 11, FontStyle.Bold),
                .ForeColor = Color.Black,
                .Height = 22,
                .TextAlign = ContentAlignment.MiddleCenter
            }
                card.Controls.Add(lblStock)
                card.Controls.Add(lblPrice)
                card.Controls.Add(lblTrans)
                card.Controls.Add(lblType)
                card.Controls.Add(lblYear)
                card.Controls.Add(lblName)
                card.Controls.Add(pic)
                home_flow.Controls.Add(card)
            End While
        End Using
    End Sub

#End Region

#Region "Reports"
    Private Sub btnREPBack_Click(sender As Object, e As EventArgs) Handles btnREPBack.Click
        btnREPBack.Hide()
        allPanelsClose()
        REPmenu.Hide()
        REPpanel.Show()

        profitsReportActive = False
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        allPanelsClose()
        REPmenu.Hide()
        REPpanel.Show()
        btnREPBack.Hide()
        lblStatus.Text = "SUMMARIES/REPORTS"
    End Sub

    Private Sub btnREPExp_Click(sender As Object, e As EventArgs) Handles btnREPExp.Click
        allPanelsClose()
        btnREPBack.Show()
        REPmenu.Controls.Clear()
        REPmenu.Padding = New Padding(20)
        REPmenu.AutoScroll = True
        REPpanel.Hide()
        REPmenu.Show()

        Dim x As Integer = 0
        Dim y As Integer = 0

        ' From label
        Dim lblFrom As New Label With {
    .Text = "From-To:",
    .Font = New Font("Futura-Medium", 11, FontStyle.Bold),
    .Location = New Point(x, y),
    .AutoSize = True
}
        REPmenu.Controls.Add(lblFrom)
        x += lblFrom.Width + 5

        ' From picker
        Dim dtpFrom As New DateTimePicker With {
    .Value = New DateTime(Now.Year, Now.Month, 1),
    .Location = New Point(x, y),
    .Width = 120
}
        REPmenu.Controls.Add(dtpFrom)
        x += dtpFrom.Width + 10

        ' To picker
        Dim dtpTo As New DateTimePicker With {
    .Value = DateTime.Now,
    .Location = New Point(x, y),
    .Width = 120
}
        REPmenu.Controls.Add(dtpTo)
        dtpTo.BringToFront()
        x += dtpTo.Width + 10

        ' Filter button
        Dim btnFilter As New Button With {
    .Text = "Filter",
    .Font = New Font("Futura-Medium", 10, FontStyle.Bold),
    .Location = New Point(x, y),
    .Size = New Size(80, 28)
}
        REPmenu.Controls.Add(btnFilter)
        btnFilter.BringToFront()


        ' Subroutine to build the report for a date range
        Dim buildReport As Action(Of Date, Date) = Sub(fromDate As Date, toDate As Date) 'MP3 - Expense CRUD
                                                       ' Remove all controls except the date pickers and filter button
                                                       For i = REPmenu.Controls.Count - 1 To 4 Step -1
                                                           REPmenu.Controls.RemoveAt(i)
                                                       Next
                                                       Dim yOffset As Integer = 40

                                                       ' 1. Total Expenses
                                                       Dim totalExpenses As Decimal = 0
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim cmd As New MySqlCommand("SELECT SUM(fee) FROM expenses WHERE from_date >= @from AND from_date <= @to", conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)

                                                           totalExpenses = Convert.ToDecimal(cmd.ExecuteScalar())
                                                       End Using
                                                       Dim lblTotal As New Label With {
                                                           .Text = $"Total Expenses: ₱{totalExpenses:N2}",
                                                           .Font = New Font("Futura-Medium", 16, FontStyle.Bold),
                                                           .Location = New Point(0, yOffset),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblTotal)

                                                       ' 2. Expenses by Type
                                                       Dim dtType As New DataTable()
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim query As String = "SELECT expense_type, SUM(fee) AS total FROM expenses WHERE from_date >= @from AND from_date <= @to GROUP BY expense_type"
                                                           Dim cmd As New MySqlCommand(query, conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)

                                                           Dim adapter As New MySqlDataAdapter(cmd)
                                                           adapter.Fill(dtType)
                                                       End Using
                                                       Dim lblType As New Label With {
                                                           .Text = "Expenses by Type:",
                                                           .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
                                                           .Location = New Point(0, yOffset + 40),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblType)
                                                       Dim dgvType As New DataGridView With {
                                                           .DataSource = dtType,
                                                           .Location = New Point(0, yOffset + 70),
                                                           .Width = 300,
                                                           .Height = 120,
                                                           .ReadOnly = True,
                                                           .AllowUserToAddRows = False,
                                                           .AllowUserToDeleteRows = False,
                                                           .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                                       }
                                                       dgvType.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                                       REPmenu.Controls.Add(dgvType)

                                                       ' 3. Expenses by Branch
                                                       Dim dtBranch As New DataTable()
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim query As String = "SELECT b.branch_name, SUM(e.fee) AS total FROM expenses e JOIN branches b ON e.branch_id = b.branch_id WHERE e.from_date >= @from AND e.from_date <= @to GROUP BY b.branch_id, b.branch_name"
                                                           Dim cmd As New MySqlCommand(query, conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Dim adapter As New MySqlDataAdapter(cmd)
                                                           adapter.Fill(dtBranch)
                                                       End Using
                                                       Dim lblBranch As New Label With {
                                                           .Text = "Expenses by Branch:",
                                                           .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
                                                           .Location = New Point(320, yOffset + 40),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblBranch)
                                                       Dim dgvBranch As New DataGridView With {
                                                           .DataSource = dtBranch,
                                                           .Location = New Point(320, yOffset + 70),
                                                           .Width = 300,
                                                           .Height = 120,
                                                           .ReadOnly = True,
                                                           .AllowUserToAddRows = False,
                                                           .AllowUserToDeleteRows = False,
                                                           .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                                       }
                                                       dgvBranch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                                       REPmenu.Controls.Add(dgvBranch)

                                                       ' 4. Status Breakdown
                                                       Dim dtStatus As New DataTable()
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim query As String = "SELECT status, SUM(fee) AS total FROM expenses WHERE from_date >= @from AND from_date <= @to GROUP BY status"
                                                           Dim cmd As New MySqlCommand(query, conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Dim adapter As New MySqlDataAdapter(cmd)
                                                           adapter.Fill(dtStatus)
                                                       End Using
                                                       Dim lblStatus As New Label With {
                                                           .Text = "Expense Status:",
                                                           .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
                                                           .Location = New Point(640, yOffset + 40),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblStatus)
                                                       Dim dgvStatus As New DataGridView With {
                                                           .DataSource = dtStatus,
                                                           .Location = New Point(640, yOffset + 70),
                                                           .Width = 200,
                                                           .Height = 120,
                                                           .ReadOnly = True,
                                                           .AllowUserToAddRows = False,
                                                           .AllowUserToDeleteRows = False,
                                                           .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                                       }
                                                       dgvStatus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                                       REPmenu.Controls.Add(dgvStatus)

                                                       ' 5. Recent Expenses
                                                       Dim dtRecent As New DataTable()
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim query As String = "SELECT e.from_date, b.branch_name, e.expense_type, e.fee, e.status FROM expenses e JOIN branches b ON e.branch_id = b.branch_id WHERE e.from_date >= @from AND e.from_date <= @to ORDER BY e.from_date DESC LIMIT 20"
                                                           Dim cmd As New MySqlCommand(query, conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Dim adapter As New MySqlDataAdapter(cmd)
                                                           adapter.Fill(dtRecent)
                                                       End Using
                                                       Dim lblRecent As New Label With {
                                                           .Text = "Recent Expenses:",
                                                           .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
                                                           .Location = New Point(0, yOffset + 210),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblRecent)
                                                       Dim dgvRecent As New DataGridView With {
                                                           .DataSource = dtRecent,
                                                           .Location = New Point(0, yOffset + 240),
                                                           .Width = 840,
                                                           .Height = 220,
                                                           .ReadOnly = True,
                                                           .AllowUserToAddRows = False,
                                                           .AllowUserToDeleteRows = False,
                                                           .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                                       }
                                                       dgvRecent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                                       REPmenu.Controls.Add(dgvRecent)
                                                   End Sub

        ' Initial build
        buildReport(dtpFrom.Value.Date, dtpTo.Value.Date)
        ' Filter button handler
        AddHandler btnFilter.Click, Sub()
                                        buildReport(dtpFrom.Value.Date, dtpTo.Value.Date)
                                    End Sub

    End Sub

    Private Sub btnREPInventory_Click(sender As Object, e As EventArgs) Handles btnREPInventory.Click
        allPanelsClose()
        btnREPBack.Show()
        REPmenu.Controls.Clear()
        REPmenu.Padding = New Padding(20)
        REPmenu.AutoScroll = True
        REPpanel.Hide()
        REPmenu.Show()

        Dim yOffset As Integer = 0

        ' 1. Total Inventory Value
        Dim totalValue As Decimal = 0
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT SUM(bm.stock * m.cost_price) AS total_value FROM branch_model bm JOIN models m ON bm.model_id = m.model_id", conn)
            Dim result = cmd.ExecuteScalar()
            If result IsNot DBNull.Value Then totalValue = Convert.ToDecimal(result)
        End Using
        Dim lblTotal As New Label With {
            .Text = $"Total Inventory Value: ₱{totalValue:N2}",
            .Font = New Font("Futura-Medium", 16, FontStyle.Bold),
            .Location = New Point(0, yOffset),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblTotal)
        yOffset += 40

        ' 2. Stock by Branch
        Dim dtBranch As New DataTable()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT b.branch_name, SUM(bm.stock) AS total_stock, SUM(bm.stock * m.cost_price) AS total_value FROM branch_model bm JOIN branches b ON bm.branch_id = b.branch_id JOIN models m ON bm.model_id = m.model_id GROUP BY b.branch_id, b.branch_name"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.Fill(dtBranch)
        End Using
        Dim lblBranch As New Label With {
            .Text = "Stock by Branch:",
            .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
            .Location = New Point(0, yOffset),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblBranch)
        Dim dgvBranch As New DataGridView With {
            .DataSource = dtBranch,
            .Location = New Point(0, yOffset + 30),
            .Width = 350,
            .Height = 120,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        }
        dgvBranch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        REPmenu.Controls.Add(dgvBranch)

        ' 3. Low Stock Alerts
        Dim dtLowStock As New DataTable()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT b.branch_name, m.model_name, bm.stock FROM branch_model bm JOIN branches b ON bm.branch_id = b.branch_id JOIN models m ON bm.model_id = m.model_id WHERE bm.stock < 5 ORDER BY bm.stock ASC"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.Fill(dtLowStock)
        End Using
        Dim lblLowStock As New Label With {
            .Text = "Low Stock Alerts (< 5):",
            .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
            .Location = New Point(370, yOffset),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblLowStock)
        Dim dgvLowStock As New DataGridView With {
            .DataSource = dtLowStock,
            .Location = New Point(370, yOffset + 30),
            .Width = 350,
            .Height = 120,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        }
        dgvLowStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        REPmenu.Controls.Add(dgvLowStock)

        ' 4. Out of Stock
        Dim dtOutStock As New DataTable()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT b.branch_name, m.model_name FROM branch_model bm JOIN branches b ON bm.branch_id = b.branch_id JOIN models m ON bm.model_id = m.model_id WHERE bm.stock = 0"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.Fill(dtOutStock)
        End Using
        Dim lblOutStock As New Label With {
            .Text = "Out of Stock:",
            .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
            .Location = New Point(740, yOffset),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblOutStock)
        Dim dgvOutStock As New DataGridView With {
            .DataSource = dtOutStock,
            .Location = New Point(740, yOffset + 30),
            .Width = 220,
            .Height = 120,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        }
        dgvOutStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        REPmenu.Controls.Add(dgvOutStock)

        yOffset += 170

        ' 5. Stock by Model
        Dim dtModel As New DataTable()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT m.model_name, m.brand, SUM(bm.stock) AS total_stock, m.cost_price, m.price FROM branch_model bm JOIN models m ON bm.model_id = m.model_id GROUP BY m.model_id, m.model_name, m.brand, m.cost_price, m.price ORDER BY total_stock ASC"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.Fill(dtModel)
        End Using
        Dim lblModel As New Label With {
            .Text = "Stock by Model:",
            .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
            .Location = New Point(0, yOffset),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblModel)
        Dim dgvModel As New DataGridView With {
            .DataSource = dtModel,
            .Location = New Point(0, yOffset + 30),
            .Width = 960,
            .Height = 220,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        }
        dgvModel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        REPmenu.Controls.Add(dgvModel)

    End Sub

    Private Sub btnREPCustomers_Click(sender As Object, e As EventArgs) Handles btnREPCustomers.Click
        btnREPBack.Show()
        REPmenu.Controls.Clear()
        REPmenu.Show()
        REPpanel.Hide()
        manage_panel.Hide()

        ' 1. Total number of customers
        Dim totalCustomers As Integer = 0
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT COUNT(*) FROM accounts WHERE role = 'customer'", conn)
            totalCustomers = Convert.ToInt32(cmd.ExecuteScalar())
        End Using
        Dim lblTotal As New Label With {
            .Text = "Total Customers: " & totalCustomers.ToString(),
            .Font = New Font("Futura-Medium", 16, FontStyle.Bold),
            .Location = New Point(20, 20),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblTotal)

        ' 2. Top customers by number of orders and total spent
        Dim dtTop As New DataTable()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT a.user_id, a.username, a.first_name, a.last_name, COUNT(DISTINCT o.order_id) AS order_count, SUM((oi.unit_price * oi.quantity) * (1 - IFNULL(v.percent_sale, 0)/100)) AS total_spent " &
                "FROM accounts a LEFT JOIN orders o ON a.user_id = o.user_id LEFT JOIN order_items oi ON o.order_id = oi.order_id LEFT JOIN models m ON oi.model_id = m.model_id LEFT JOIN vouchers v ON o.voucher_code = v.voucher_code " &
                "WHERE a.role = 'customer' GROUP BY a.user_id, a.username, a.first_name, a.last_name ORDER BY total_spent DESC LIMIT 10"
            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            adapter.Fill(dtTop)
        End Using
        Dim lblTop As New Label With {
            .Text = "Top Customers (by Total Spent):",
            .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
            .Location = New Point(20, 60),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblTop)
        Dim dgvTop As New DataGridView With {
            .DataSource = dtTop,
            .Location = New Point(20, 90),
            .Width = 600,
            .Height = 200,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        }
        REPmenu.Controls.Add(dgvTop)

        ' 3. All customers with order count and total spent
        Dim dtAll As New DataTable()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT a.user_id, a.username, a.first_name, a.last_name, COUNT(DISTINCT o.order_id) AS order_count, SUM((oi.unit_price * oi.quantity) * (1 - IFNULL(v.percent_sale, 0)/100)) AS total_spent " &
                "FROM accounts a LEFT JOIN orders o ON a.user_id = o.user_id LEFT JOIN order_items oi ON o.order_id = oi.order_id LEFT JOIN models m ON oi.model_id = m.model_id LEFT JOIN vouchers v ON o.voucher_code = v.voucher_code " &
                "WHERE a.role = 'customer' GROUP BY a.user_id, a.username, a.first_name, a.last_name ORDER BY a.username"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            adapter.Fill(dtAll)
        End Using
        Dim lblAll As New Label With {
            .Text = "All Customers:",
            .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
            .Location = New Point(20, 310),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblAll)
        Dim dgvAll As New DataGridView With {
            .DataSource = dtAll,
            .Location = New Point(20, 340),
            .Width = 600,
            .Height = 250,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        }
        REPmenu.Controls.Add(dgvAll)
    End Sub

    Private Sub btnREPEmployees_Click(sender As Object, e As EventArgs) Handles btnREPEmployees.Click

        REPmenu.Controls.Clear()
        REPmenu.Padding = New Padding(20)
        REPmenu.AutoScroll = True
        REPmenu.Show()
        REPpanel.Hide()
        btnREPBack.Show()

        ' 1. Total employees and by status
        Dim totalEmp As Integer = 0, activeEmp As Integer = 0, onLeaveEmp As Integer = 0, firedEmp As Integer = 0
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            totalEmp = Convert.ToInt32(New MySqlCommand("SELECT COUNT(*) FROM employees", conn).ExecuteScalar())
            activeEmp = Convert.ToInt32(New MySqlCommand("SELECT COUNT(*) FROM employees WHERE employment_status = 'active'", conn).ExecuteScalar())
            onLeaveEmp = Convert.ToInt32(New MySqlCommand("SELECT COUNT(*) FROM employees WHERE employment_status = 'on_leave'", conn).ExecuteScalar())
            firedEmp = Convert.ToInt32(New MySqlCommand("SELECT COUNT(*) FROM employees WHERE employment_status = 'fired'", conn).ExecuteScalar())
        End Using
        Dim lblTotal As New Label With {
            .Text = $"Total Employees: {totalEmp}   (Active: {activeEmp}, On Leave: {onLeaveEmp}, Fired: {firedEmp})",
            .Font = New Font("Futura-Medium", 16, FontStyle.Bold),
            .Location = New Point(0, 0),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblTotal)

        ' 2. Employees by branch (left column)
        Dim dtBranch As New DataTable()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT b.branch_name, COUNT(e.emp_id) AS employee_count " &
                                  "FROM branches b LEFT JOIN employees e ON b.branch_id = e.current_branch_id " &
                                  "GROUP BY b.branch_id, b.branch_name ORDER BY employee_count DESC"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.Fill(dtBranch)
        End Using
        Dim lblBranch As New Label With {
            .Text = "Employees by Branch:",
            .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
            .Location = New Point(0, 40),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblBranch)
        Dim dgvBranch As New DataGridView With {
            .DataSource = dtBranch,
            .Location = New Point(0, 70),
            .Width = 400,
            .Height = 150,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        }
        dgvBranch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        REPmenu.Controls.Add(dgvBranch)

        ' 3. Employee list (left column, below)
        Dim dtList As New DataTable()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT e.emp_id, a.first_name, a.last_name, b.branch_name, e.employment_status, a.date_created as hire_date, a.email, a.phone_number " &
                                   "FROM employees e LEFT JOIN accounts a ON e.user_id = a.user_id LEFT JOIN branches b ON e.current_branch_id = b.branch_id " &
                                   "ORDER BY a.first_name, a.last_name"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.Fill(dtList)
        End Using
        Dim lblList As New Label With {
            .Text = "Employee List:",
            .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
            .Location = New Point(0, 230),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblList)
        Dim dgvList As New DataGridView With {
            .DataSource = dtList,
            .Location = New Point(0, 260),
            .Width = 400,
            .Height = 250,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        }
        dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        REPmenu.Controls.Add(dgvList)

        ' 4. Salary Summary (right column)
        Dim totalSalary As Decimal = 0
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT SUM(s.rate_used * CASE WHEN DATEDIFF(s.to_date, s.from_date) >= 20 THEN GREATEST(1, (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date))) ELSE (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date)) END) FROM salaries s", conn)
            totalSalary = Convert.ToDecimal(cmd.ExecuteScalar())
        End Using
        Dim lblSalary As New Label With {
            .Text = $"Total Salary Expenses: ₱{totalSalary:N2}",
            .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
            .Location = New Point(420, 40),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblSalary)

        ' Salary by branch (right column)
        Dim dtSalBranch As New DataTable()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT b.branch_name, SUM(s.rate_used * CASE WHEN DATEDIFF(s.to_date, s.from_date) >= 20 THEN GREATEST(1, (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date))) ELSE (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date)) END) AS total_salary " &
                                  "FROM salaries s " &
                                  "JOIN employees e ON s.emp_id = e.emp_id " &
                                  "JOIN branches b ON e.current_branch_id = b.branch_id " &
                                  "GROUP BY b.branch_id, b.branch_name ORDER BY total_salary DESC"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.Fill(dtSalBranch)
        End Using
        Dim lblSalBranch As New Label With {
            .Text = "Salary Expenses by Branch:",
            .Font = New Font("Futura-Medium", 11, FontStyle.Bold),
            .Location = New Point(420, 70),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblSalBranch)
        Dim dgvSalBranch As New DataGridView With {
            .DataSource = dtSalBranch,
            .Location = New Point(420, 100),
            .Width = 350,
            .Height = 120,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        }
        dgvSalBranch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        REPmenu.Controls.Add(dgvSalBranch)

        ' Top 10 highest paid employees (right column, below)
        Dim dtTopSal As New DataTable()
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "SELECT a.first_name, a.last_name, b.branch_name, SUM(s.rate_used * CASE WHEN DATEDIFF(s.to_date, s.from_date) >= 20 THEN GREATEST(1, (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date))) ELSE (YEAR(s.to_date) - YEAR(s.from_date)) * 12 + (MONTH(s.to_date) - MONTH(s.from_date)) END) AS total_salary " &
                                  "FROM salaries s " &
                                  "JOIN employees e ON s.emp_id = e.emp_id " &
                                  "JOIN accounts a ON e.user_id = a.user_id " &
                                  "JOIN branches b ON e.current_branch_id = b.branch_id " &
                                  "GROUP BY e.emp_id, a.first_name, a.last_name, b.branch_name " &
                                  "ORDER BY total_salary DESC LIMIT 10"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.Fill(dtTopSal)
        End Using
        Dim lblTopSal As New Label With {
            .Text = "Top 10 Highest Paid Employees:",
            .Font = New Font("Futura-Medium", 11, FontStyle.Bold),
            .Location = New Point(420, 230),
            .AutoSize = True
        }
        REPmenu.Controls.Add(lblTopSal)
        Dim dgvTopSal As New DataGridView With {
            .DataSource = dtTopSal,
            .Location = New Point(420, 260),
            .Width = 350,
            .Height = 250,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        }
        dgvTopSal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        REPmenu.Controls.Add(dgvTopSal)
    End Sub


    '// if meron, it returns 0. (e.g. sum(no price * no quantity) -> 0)
    Private Sub btnREPProfits_Click(sender As Object, e As EventArgs) Handles btnREPProfits.Click
        allPanelsClose()
        btnREPBack.Show()
        REPmenu.Controls.Clear()
        REPmenu.Padding = New Padding(20)
        REPmenu.AutoScroll = True
        REPpanel.Hide()
        REPmenu.Show()

        ' Set flag to indicate profits report is active
        profitsReportActive = True

        ' Date range controls
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim lblFrom As New Label With {.Text = "From-To:", .Font = New Font("Futura-Medium", 11, FontStyle.Bold), .Location = New Point(x, y), .AutoSize = True}
        REPmenu.Controls.Add(lblFrom)
        x += lblFrom.Width + 5
        Dim dtpFrom As New DateTimePicker With {.Value = New DateTime(Now.Year, Now.Month, 1), .Location = New Point(x, y), .Width = 120}
        REPmenu.Controls.Add(dtpFrom)
        x += dtpFrom.Width + 10
        Dim dtpTo As New DateTimePicker With {.Value = DateTime.Now, .Location = New Point(x, y), .Width = 120}
        REPmenu.Controls.Add(dtpTo)
        x += dtpTo.Width + 10
        Dim btnFilter As New Button With {.Text = "Filter", .Font = New Font("Futura-Medium", 10, FontStyle.Bold), .Location = New Point(x, y), .Size = New Size(80, 28)}
        REPmenu.Controls.Add(btnFilter)

        ' Subroutine to build the report for a date range
        Dim buildReport As Action(Of Date, Date) = Sub(fromDate As Date, toDate As Date) 'MP5 - Profits CRUD
                                                       ' Remove all controls except the date pickers and filter button
                                                       For i = REPmenu.Controls.Count - 1 To 4 Step -1
                                                           REPmenu.Controls.RemoveAt(i)
                                                       Next
                                                       Dim yOffset As Integer = 40

                                                       ' 1. Total Sales
                                                       Dim totalSales As Decimal = 0
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim cmd As New MySqlCommand("SELECT SUM((oi.unit_price * oi.quantity) * (1 - IFNULL(v.percent_sale, 0)/100)) AS total_sales FROM orders o JOIN order_items oi ON o.order_id = oi.order_id LEFT JOIN vouchers v ON o.voucher_code = v.voucher_code WHERE o.order_status = 'delivered' AND o.date_ordered >= @from AND o.date_ordered <= @to", conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           totalSales = Convert.ToDecimal(cmd.ExecuteScalar())
                                                       End Using

                                                       ' 2. Total Expenses (including motorcycle costs)
                                                       Dim totalExpenses As Decimal = 0
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           ' Get operational expenses
                                                           Dim cmd As New MySqlCommand("SELECT SUM(fee) FROM expenses WHERE from_date >= @from AND from_date <= @to", conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Dim operationalExpenses As Decimal = Convert.ToDecimal(cmd.ExecuteScalar())

                                                           ' Get motorcycle costs (cost_price * quantity sold)
                                                           Dim cmdMotorcycle As New MySqlCommand("SELECT SUM(m.cost_price * oi.quantity) AS motorcycle_costs FROM orders o JOIN order_items oi ON o.order_id = oi.order_id JOIN models m ON oi.model_id = m.model_id WHERE o.order_status = 'delivered' AND o.date_ordered >= @from AND o.date_ordered <= @to", conn)
                                                           cmdMotorcycle.Parameters.AddWithValue("@from", fromDate)
                                                           cmdMotorcycle.Parameters.AddWithValue("@to", toDate)
                                                           Dim motorcycleCosts As Decimal = Convert.ToDecimal(cmdMotorcycle.ExecuteScalar())

                                                           totalExpenses = operationalExpenses + motorcycleCosts
                                                       End Using
                                                       Dim netProfit As Decimal = totalSales - totalExpenses

                                                       ' Show summary
                                                       Dim lblSummary As New Label With {
                                                           .Text = $"Total Sales: ₱{totalSales:N2}    Total Expenses: ₱{totalExpenses:N2}    Net Profit: ₱{netProfit:N2}",
                                                           .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
                                                           .Location = New Point(0, yOffset),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblSummary)
                                                       yOffset += 40

                                                       ' 3. Profit by Branch
                                                       Dim dtBranch As New DataTable()
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim query As String = "SELECT b.branch_name, SUM((oi.unit_price * oi.quantity) * (1 - IFNULL(v.percent_sale, 0)/100)) AS sales, SUM(m.cost_price * oi.quantity) AS costs, SUM((oi.unit_price * oi.quantity) * (1 - IFNULL(v.percent_sale, 0)/100)) - SUM(m.cost_price * oi.quantity) AS profit FROM orders o JOIN order_items oi ON o.order_id = oi.order_id JOIN models m ON oi.model_id = m.model_id LEFT JOIN vouchers v ON o.voucher_code = v.voucher_code JOIN branches b ON oi.branch_id = b.branch_id WHERE o.order_status = 'delivered' AND o.date_ordered >= @from AND o.date_ordered <= @to GROUP BY b.branch_id, b.branch_name"

                                                           Dim cmd As New MySqlCommand(query, conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Dim adapter As New MySqlDataAdapter(cmd)
                                                           adapter.Fill(dtBranch)
                                                       End Using
                                                       Dim lblBranch As New Label With {
                                                           .Text = "Profit by Branch:",
                                                           .Font = New Font("Futura-Medium", 12, FontStyle.Bold),
                                                           .Location = New Point(0, yOffset),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblBranch)
                                                       Dim dgvBranch As New DataGridView With {
                                                           .DataSource = dtBranch,
                                                           .Location = New Point(0, yOffset + 30),
                                                           .Width = 500,
                                                           .Height = 120,
                                                           .ReadOnly = True,
                                                           .AllowUserToAddRows = False,
                                                           .AllowUserToDeleteRows = False,
                                                           .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                                       }
                                                       dgvBranch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                                       REPmenu.Controls.Add(dgvBranch)

                                                       ' 4. Profit by Model
                                                       Dim dtModel As New DataTable()
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim query As String = "SELECT m.model_name, SUM((oi.unit_price * oi.quantity) * (1 - IFNULL(v.percent_sale, 0)/100)) AS sales, SUM(m.cost_price * oi.quantity) AS costs, SUM((oi.unit_price * oi.quantity) * (1 - IFNULL(v.percent_sale, 0)/100)) - SUM(m.cost_price * oi.quantity) AS profit FROM orders o JOIN order_items oi ON o.order_id = oi.order_id JOIN models m ON oi.model_id = m.model_id LEFT JOIN vouchers v ON o.voucher_code = v.voucher_code WHERE o.order_status = 'delivered' AND o.date_ordered >= @from AND o.date_ordered <= @to GROUP BY m.model_id, m.model_name"
                                                           Dim cmd As New MySqlCommand(query, conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Dim adapter As New MySqlDataAdapter(cmd)
                                                           adapter.Fill(dtModel)
                                                       End Using
                                                       Dim lblModel As New Label With {
                                                           .Text = "Profit by Model:",
                                                           .Font = New Font("Futura-Medium", 12, FontStyle.Bold),
                                                           .Location = New Point(520, yOffset),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblModel)
                                                       Dim dgvModel As New DataGridView With {
                                                           .DataSource = dtModel,
                                                           .Location = New Point(520, yOffset + 30),
                                                           .Width = 400,
                                                           .Height = 120,
                                                           .ReadOnly = True,
                                                           .AllowUserToAddRows = False,
                                                           .AllowUserToDeleteRows = False,
                                                           .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                                       }
                                                       dgvModel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                                       REPmenu.Controls.Add(dgvModel)

                                                       Dim descText As String =
            "Description: " & vbCrLf &
            "- Total Sales: Revenue from delivered orders." & vbCrLf &
            "- Total Expenses: All business costs including motorcycle costs (cost_price * quantity sold) and operational expenses (salaries, rent, supplies, etc.)." & vbCrLf &
            "- Net Profit: Net Profit = Total Sales - Total Expenses" & vbCrLf &
            "- Profit by Branch: Breakdown of profit per branch." & vbCrLf &
            "- Profit by Model: Breakdown of profit per product/model" & vbCrLf &
            "All numbers and tables show only orders and expenses between the selected From and To dates."
                                                       Dim lblDesc As New Label With {
                                                           .Text = descText,
                                                           .Font = New Font("Futura-Medium", 10, FontStyle.Regular),
                                                           .Location = New Point(0, 250),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblDesc)

                                                   End Sub

        ' Initial build
        profitsReportFromDate = dtpFrom.Value.Date
        profitsReportToDate = dtpTo.Value.Date
        buildReport(dtpFrom.Value.Date, dtpTo.Value.Date)
        ' Filter button handler
        AddHandler btnFilter.Click, Sub()
                                        profitsReportFromDate = dtpFrom.Value.Date
                                        profitsReportToDate = dtpTo.Value.Date
                                        buildReport(dtpFrom.Value.Date, dtpTo.Value.Date)
                                    End Sub
    End Sub

    ' Function to refresh profits report if it's currently active
    Private Sub RefreshProfitsReportIfActive()
        If profitsReportActive AndAlso REPmenu.Visible Then
            ' Find the filter button and trigger its click event to refresh the report
            For Each control As Control In REPmenu.Controls
                If TypeOf control Is Button AndAlso control.Text = "Filter" Then
                    DirectCast(control, Button).PerformClick()
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnREPReturns_Click(sender As Object, e As EventArgs) Handles btnREPReturns.Click
        allPanelsClose()
        btnREPBack.Show()
        REPmenu.Controls.Clear()
        REPmenu.Padding = New Padding(20)
        REPmenu.AutoScroll = True
        REPpanel.Hide()
        REPmenu.Show()

        ' Date range controls
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim lblFrom As New Label With {.Text = "From-To:", .Font = New Font("Futura-Medium", 11, FontStyle.Bold), .Location = New Point(x, y), .AutoSize = True}
        REPmenu.Controls.Add(lblFrom)
        x += lblFrom.Width + 5
        Dim dtpFrom As New DateTimePicker With {.Value = New DateTime(Now.Year, Now.Month, 1), .Location = New Point(x, y), .Width = 120}
        REPmenu.Controls.Add(dtpFrom)
        x += dtpFrom.Width + 10
        Dim dtpTo As New DateTimePicker With {.Value = DateTime.Now, .Location = New Point(x, y), .Width = 120}
        REPmenu.Controls.Add(dtpTo)
        x += dtpTo.Width + 10
        Dim btnFilter As New Button With {.Text = "Filter", .Font = New Font("Futura-Medium", 10, FontStyle.Bold), .Location = New Point(x, y), .Size = New Size(80, 28)}
        REPmenu.Controls.Add(btnFilter)

        ' Subroutine to build the report for a date range
        Dim buildReport As Action(Of Date, Date) = Sub(fromDate As Date, toDate As Date)
                                                       For i = REPmenu.Controls.Count - 1 To 4 Step -1
                                                           REPmenu.Controls.RemoveAt(i)
                                                       Next
                                                       Dim yOffset As Integer = 40

                                                       ' 1. Total Returns and Amount
                                                       Dim totalReturns As Integer = 0
                                                       Dim totalAmount As Decimal = 0
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim cmd As New MySqlCommand("SELECT COUNT(*) AS total_returns FROM returns WHERE return_date >= @from AND return_date <= @to", conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Using reader = cmd.ExecuteReader()
                                                               If reader.Read() Then
                                                                   totalReturns = Convert.ToInt32(reader("total_returns"))
                                                               End If
                                                           End Using
                                                       End Using
                                                       Dim lblSummary As New Label With {
                                                           .Text = $"Total Returns: {totalReturns}",
                                                           .Font = New Font("Futura-Medium", 13, FontStyle.Bold),
                                                           .Location = New Point(0, yOffset),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblSummary)
                                                       yOffset += 40

                                                       ' 2. Returns by Status
                                                       Dim dtStatus As New DataTable()
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim query As String = "SELECT return_status, COUNT(*) AS count FROM returns WHERE return_date >= @from AND return_date <= @to GROUP BY return_status"
                                                           Dim cmd As New MySqlCommand(query, conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Dim adapter As New MySqlDataAdapter(cmd)
                                                           adapter.Fill(dtStatus)
                                                       End Using
                                                       Dim lblStatus As New Label With {
                                                           .Text = "Returns by Status:",
                                                           .Font = New Font("Futura-Medium", 12, FontStyle.Bold),
                                                           .Location = New Point(0, yOffset),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblStatus)
                                                       Dim dgvStatus As New DataGridView With {
                                                           .DataSource = dtStatus,
                                                           .Location = New Point(0, yOffset + 30),
                                                           .Width = 300,
                                                           .Height = 100,
                                                           .ReadOnly = True,
                                                           .AllowUserToAddRows = False,
                                                           .AllowUserToDeleteRows = False,
                                                           .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                                       }
                                                       dgvStatus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                                       REPmenu.Controls.Add(dgvStatus)

                                                       ' 3. Returns by Reason
                                                       Dim dtReason As New DataTable()
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim query As String = "SELECT condition_, COUNT(*) AS count FROM returns WHERE return_date >= @from AND return_date <= @to GROUP BY condition_"
                                                           Dim cmd As New MySqlCommand(query, conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Dim adapter As New MySqlDataAdapter(cmd)
                                                           adapter.Fill(dtReason)
                                                       End Using
                                                       Dim lblReason As New Label With {
                                                           .Text = "Returns by Condition:",
                                                           .Font = New Font("Futura-Medium", 12, FontStyle.Bold),
                                                           .Location = New Point(320, yOffset),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblReason)
                                                       Dim dgvReason As New DataGridView With {
                                                           .DataSource = dtReason,
                                                           .Location = New Point(320, yOffset + 30),
                                                           .Width = 300,
                                                           .Height = 100,
                                                           .ReadOnly = True,
                                                           .AllowUserToAddRows = False,
                                                           .AllowUserToDeleteRows = False,
                                                           .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                                       }
                                                       dgvReason.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                                       REPmenu.Controls.Add(dgvReason)

                                                       ' 4. Returns by Branch
                                                       Dim dtBranch As New DataTable()
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim query As String = "SELECT b.branch_name, COUNT(*) AS count FROM returns r JOIN orders o ON r.order_id = o.order_id JOIN branches b ON o.branch_id = b.branch_id WHERE r.return_date >= @from AND r.return_date <= @to GROUP BY b.branch_id, b.branch_name"
                                                           Dim cmd As New MySqlCommand(query, conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Dim adapter As New MySqlDataAdapter(cmd)
                                                           adapter.Fill(dtBranch)
                                                       End Using
                                                       Dim lblBranch As New Label With {
                                                           .Text = "Returns by Branch:",
                                                           .Font = New Font("Futura-Medium", 12, FontStyle.Bold),
                                                           .Location = New Point(640, yOffset),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblBranch)
                                                       Dim dgvBranch As New DataGridView With {
                                                           .DataSource = dtBranch,
                                                           .Location = New Point(640, yOffset + 30),
                                                           .Width = 300,
                                                           .Height = 100,
                                                           .ReadOnly = True,
                                                           .AllowUserToAddRows = False,
                                                           .AllowUserToDeleteRows = False,
                                                           .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                                       }
                                                       dgvBranch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                                       REPmenu.Controls.Add(dgvBranch)

                                                       yOffset += 140

                                                       ' 5. Returns by Model
                                                       Dim dtModel As New DataTable()
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim query As String = "SELECT m.model_name, COUNT(*) AS count FROM returns r JOIN orders o ON r.order_id = o.order_id JOIN models m ON o.model_id = m.model_id WHERE r.return_date >= @from AND r.return_date <= @to GROUP BY m.model_id, m.model_name"
                                                           Dim cmd As New MySqlCommand(query, conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Dim adapter As New MySqlDataAdapter(cmd)
                                                           adapter.Fill(dtModel)
                                                       End Using
                                                       Dim lblModel As New Label With {
                                                           .Text = "Returns by Model:",
                                                           .Font = New Font("Futura-Medium", 12, FontStyle.Bold),
                                                           .Location = New Point(0, yOffset),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblModel)
                                                       Dim dgvModel As New DataGridView With {
                                                           .DataSource = dtModel,
                                                           .Location = New Point(0, yOffset + 30),
                                                           .Width = 500,
                                                           .Height = 100,
                                                           .ReadOnly = True,
                                                           .AllowUserToAddRows = False,
                                                           .AllowUserToDeleteRows = False,
                                                           .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                                       }
                                                       dgvModel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                                       REPmenu.Controls.Add(dgvModel)

                                                       ' 6. Recent Returns
                                                       Dim dtRecent As New DataTable()
                                                       Using conn As New MySqlConnection(connStr)
                                                           conn.Open()
                                                           Dim query As String = "SELECT r.return_date, b.branch_name, m.model_name, a.username, r.reason, r.condition_, r.return_status, r.quantity_returned FROM returns r JOIN orders o ON r.order_id = o.order_id JOIN branches b ON o.branch_id = b.branch_id JOIN models m ON o.model_id = m.model_id JOIN accounts a ON o.user_id = a.user_id WHERE r.return_date >= @from AND r.return_date <= @to ORDER BY r.return_date DESC LIMIT 20"
                                                           Dim cmd As New MySqlCommand(query, conn)
                                                           cmd.Parameters.AddWithValue("@from", fromDate)
                                                           cmd.Parameters.AddWithValue("@to", toDate)
                                                           Dim adapter As New MySqlDataAdapter(cmd)
                                                           adapter.Fill(dtRecent)
                                                       End Using
                                                       Dim lblRecent As New Label With {
                                                           .Text = "Recent Returns:",
                                                           .Font = New Font("Futura-Medium", 12, FontStyle.Bold),
                                                           .Location = New Point(520, yOffset),
                                                           .AutoSize = True
                                                       }
                                                       REPmenu.Controls.Add(lblRecent)
                                                       Dim dgvRecent As New DataGridView With {
                                                           .DataSource = dtRecent,
                                                           .Location = New Point(520, yOffset + 30),
                                                           .Width = 500,
                                                           .Height = 100,
                                                           .ReadOnly = True,
                                                           .AllowUserToAddRows = False,
                                                           .AllowUserToDeleteRows = False,
                                                           .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                                       }
                                                       dgvRecent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                                       REPmenu.Controls.Add(dgvRecent)
                                                   End Sub
        buildReport(dtpFrom.Value.Date, dtpTo.Value.Date)

        AddHandler btnFilter.Click, Sub()
                                        buildReport(dtpFrom.Value.Date, dtpTo.Value.Date)
                                    End Sub
    End Sub



#End Region


End Class

