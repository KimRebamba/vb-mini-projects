Imports System.Xml.Schema
Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient


Public Class home_page

    Private navigationStack As New Stack(Of Action)
    Public currentUsername As String
    Public currentUserId As Integer
    Public currentUserRole As String
    Private cartItems As New List(Of CartItem)
    Public connStr As String ' This is now the role-based connection
    Dim modelId As Integer
    Dim branchId As Integer

#Region "Menu"
    Private Sub lblLogOut_Click(sender As Object, e As EventArgs) Handles lblLogOut.Click
        Me.Hide()
        account_page.Show()
    End Sub
    Private Sub lblMoto_Click(sender As Object, e As EventArgs) Handles lblMoto.Click
        home_panel.Hide()
        home_flow.Show()
        LoadBranches()
        messageTimer.Stop()
    End Sub
    Private Sub lblHome_Click(sender As Object, e As EventArgs) Handles lblHome.Click
        CountStats()
        home_flow.Hide()
        btnBack.Hide()
        home_panel.Show()
        lblStatus.Text = "PROMO: " & promoMessages(currentMessageIndex)

        AddHandler messageTimer.Tick, AddressOf RotateMessages
        messageTimer.Start()
    End Sub
    Private Sub lblVoucher_Click(sender As Object, e As EventArgs) Handles lblVoucher.Click
        lblStatus.Text = "AVAILABLE VOUCHERS:"
        messageTimer.Stop()
        home_panel.Hide()
        home_flow.Show()
        btnBack.Hide()
        home_flow.Controls.Clear()
        navigationStack.Clear()

        Dim query As String = "
        SELECT voucher_code, percent_sale, from_date, to_date, status 
        FROM vouchers 
        WHERE CURDATE() BETWEEN from_date AND to_date 
    "

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim code As String = reader("voucher_code").ToString()
                Dim percent As String = reader("percent_sale").ToString() & "% OFF"
                Dim fromDate As String = Format(CDate(reader("from_date")), "MMM dd, yyyy")
                Dim toDate As String = Format(CDate(reader("to_date")), "MMM dd, yyyy")
                Dim status As String = reader("status").ToString()

                Dim accentColor As Color = If(status.ToLower() = "active", Color.Red, Color.Black)

                Dim card As New Panel With {
                .Size = New Size(260, 150),
                .BackColor = Color.White,
                .Margin = New Padding(12),
                .BorderStyle = BorderStyle.FixedSingle,
                .Cursor = Cursors.Hand
            }

                ' Accent bar
                Dim accentBar As New Panel With {
                .Size = New Size(8, 150),
                .BackColor = accentColor,
                .Location = New Point(0, 0)
            }
                card.Controls.Add(accentBar)

                ' Voucher code (big, bold)
                Dim lblCode As New Label With {
                .Text = code,
                .Font = New Font("FuturaBookCTT", 20, FontStyle.Bold),
                .ForeColor = Color.Black,
                .Location = New Point(20, 10),
                .AutoSize = True
            }
                card.Controls.Add(lblCode)

                ' Discount
                Dim lblPercent As New Label With {
                .Text = percent,
                .Font = New Font("FuturaBookCTT", 15, FontStyle.Bold),
                .ForeColor = Color.Red,
                .Location = New Point(20, 45),
                .AutoSize = True
            }
                card.Controls.Add(lblPercent)

                ' Validity dates
                Dim lblDates As New Label With {
                .Text = "Valid: " & fromDate & " – " & toDate,
                .Font = New Font("FuturaBookCTT", 11, FontStyle.Italic),
                .ForeColor = Color.DimGray,
                .Location = New Point(20, 75),
                .AutoSize = True
            }
                card.Controls.Add(lblDates)

                ' Status
                Dim lblStatusV As New Label With {
                .Text = "Status: " & status,
                .Font = New Font("FuturaBookCTT", 11, FontStyle.Bold),
                .ForeColor = accentColor,
                .Location = New Point(20, 100),
                .AutoSize = True
            }
                card.Controls.Add(lblStatusV)

                ' Tap to copy
                Dim lblCopy As New Label With {
                .Text = "Click to copy code",
                .Font = New Font("FuturaBookCTT", 9, FontStyle.Italic),
                .ForeColor = Color.Gray,
                .Location = New Point(20, 120),
                .AutoSize = True
            }
                card.Controls.Add(lblCopy)

                ' Click handler
                Dim clickAction As EventHandler = Sub(sender2, e2)
                                                      Clipboard.SetText(code)
                                                      MessageBox.Show("Voucher code copied!", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                  End Sub
                AddHandler card.Click, clickAction
                AddHandler lblCode.Click, clickAction
                AddHandler lblPercent.Click, clickAction
                AddHandler lblDates.Click, clickAction
                AddHandler lblStatusV.Click, clickAction
                AddHandler lblCopy.Click, clickAction

                home_flow.Controls.Add(card)
            End While
        End Using
    End Sub
    Private Sub lblOrder_Click(sender As Object, e As EventArgs) Handles lblOrder.Click
        lblStatus.Text = "YOUR ORDERS:"
        home_panel.Hide()
        home_flow.Show()
        btnBack.Hide()
        home_flow.Controls.Clear()
        navigationStack.Clear()


        Dim ordersQuery As String = "SELECT o.order_id, o.date_ordered, o.est_delivery, o.order_status, o.voucher_code, o.payment_status, o.payment_option " &
            "FROM orders o " &
            "WHERE o.user_id = @uid " &
            "ORDER BY o.date_ordered DESC"

        Dim ordersList As New List(Of Object)
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(ordersQuery, conn)
            cmd.Parameters.AddWithValue("@uid", currentUserId)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                ordersList.Add(New With {
                    .OrderId = CInt(reader("order_id")),
                    .OrderDate = CDate(reader("date_ordered")),
                    .DeliveryDate = CDate(reader("est_delivery")),
                    .OrderStatus = reader("order_status").ToString(),
                    .PaymentStatus = reader("payment_status").ToString(),
                    .PaymentOption = reader("payment_option").ToString(),
                    .VoucherCode = If(IsDBNull(reader("voucher_code")), "", reader("voucher_code").ToString())
                })
            End While
        End Using

        ' Now process each order
        For Each orderData In ordersList
            Dim orderId As Integer = orderData.OrderId
            Dim orderDate As Date = orderData.OrderDate
            Dim deliveryDate As Date = orderData.DeliveryDate
            Dim orderStatus As String = orderData.OrderStatus
            Dim paymentStatus As String = orderData.PaymentStatus
            Dim paymentOption As String = orderData.PaymentOption
            Dim voucherCode As String = orderData.VoucherCode

            ' Get all items for this order
            Dim itemsQuery As String = "SELECT oi.model_id, oi.quantity, oi.unit_price, " &
                    "m.model_name, m.photo, b.branch_name, b.address AS branch_address, r.return_id, r.return_status, r.quantity_returned " &
                    "FROM order_items oi " &
                    "JOIN models m ON oi.model_id = m.model_id " &
                    "JOIN branches b ON oi.branch_id = b.branch_id " &
                    "LEFT JOIN returns r ON oi.order_id = r.order_id " &
                    "WHERE oi.order_id = @orderId" ' @paramater = orderid

            Dim orderItems As New List(Of Object)
            Dim orderTotal As Decimal = 0
            Dim firstItemPhoto As Byte() = Nothing
            Dim firstModelName As String = ""
            Dim firstBranchName As String = ""
            Dim firstBranchAddress As String = ""

            Using connItems As New MySqlConnection(connStr)
                connItems.Open()
                Dim cmdItems As New MySqlCommand(itemsQuery, connItems)
                cmdItems.Parameters.AddWithValue("@orderId", orderId)
                Dim readerItems As MySqlDataReader = cmdItems.ExecuteReader()

                While readerItems.Read()
                    Dim modelName As String = readerItems("model_name").ToString()
                    Dim branchName As String = readerItems("branch_name").ToString()
                    Dim branchAddress As String = readerItems("branch_address").ToString()
                    Dim quantity As Integer = CInt(readerItems("quantity"))
                    Dim unitPrice As Decimal = CDec(readerItems("unit_price"))
                    Dim photoBytes As Byte() = CType(readerItems("photo"), Byte())
                    Dim returnId As Object = readerItems("return_id")
                    Dim returnStatus As String = If(IsDBNull(readerItems("return_status")), "", readerItems("return_status").ToString())
                    Dim quantityReturned As Integer = If(IsDBNull(readerItems("quantity_returned")), 0, CInt(readerItems("quantity_returned")))
                    Dim itemTotal As Decimal = unitPrice * quantity


                    If firstItemPhoto Is Nothing Then
                        firstItemPhoto = photoBytes
                        firstModelName = modelName
                        firstBranchName = branchName
                        firstBranchAddress = branchAddress
                    End If

                    orderItems.Add(New With {
                            .ModelName = modelName,
                            .BranchName = branchName,
                            .BranchAddress = branchAddress,
                            .Quantity = quantity,
                            .UnitPrice = unitPrice,
                            .ItemTotal = itemTotal,
                            .PhotoBytes = photoBytes,
                            .ReturnId = returnId,
                            .ReturnStatus = returnStatus,
                            .QuantityReturned = quantityReturned
                        })

                    orderTotal += itemTotal
                End While
            End Using

            ' Calculate dynamic height based on number of items
            Dim cardHeight As Integer = Math.Max(300, 250 + (orderItems.Count * 25))

            ' --- Order Card Panel ---
            Dim card As New Panel With {
                    .Size = New Size(750, cardHeight),
                    .BackColor = Color.White,
                    .Margin = New Padding(10),
                    .BorderStyle = BorderStyle.FixedSingle
                }

            ' Show first item's photo
            Dim pic As New PictureBox With {
                    .Size = New Size(120, 120),
                    .Location = New Point(15, 15),
                    .SizeMode = PictureBoxSizeMode.Zoom,
                    .Image = GetImageFromBytes(firstItemPhoto),
                    .BorderStyle = BorderStyle.FixedSingle
                }
            card.Controls.Add(pic)

            Dim xLeft As Integer = 150
            Dim yTop As Integer = 15
            Dim futuraMed As New Font("Futura-Medium", 13.8, FontStyle.Bold)
            Dim futuraSmall As New Font("Futura-Medium", 11, FontStyle.Regular)

            ' Show order info
            Dim lblOrderInfo As New Label With {.Text = $"Order #{orderId}", .Font = futuraMed, .Location = New Point(xLeft, yTop), .AutoSize = True}
            card.Controls.Add(lblOrderInfo)
            yTop += 28
            Dim lblBranch As New Label With {.Text = $"Branch: {firstBranchName}", .Font = futuraSmall, .Location = New Point(xLeft, yTop), .AutoSize = True}
            card.Controls.Add(lblBranch)
            yTop += 22
            Dim lblBranchAddr As New Label With {.Text = $"Address: {firstBranchAddress}", .Font = futuraSmall, .Location = New Point(xLeft, yTop), .AutoSize = True}
            card.Controls.Add(lblBranchAddr)
            yTop += 22
            Dim lblOrderDate As New Label With {.Text = $"Ordered: {orderDate:MMM dd, yyyy}", .Font = futuraSmall, .Location = New Point(xLeft, yTop), .AutoSize = True}
            card.Controls.Add(lblOrderDate)
            yTop += 22
            Dim lblDeliveryDate As New Label With {.Text = $"Delivery: {deliveryDate:MMM dd, yyyy}", .Font = futuraSmall, .Location = New Point(xLeft, yTop), .AutoSize = True}
            card.Controls.Add(lblDeliveryDate)
            yTop += 22

            ' Show items list
            yTop += 10
            Dim lblItemsTitle As New Label With {.Text = "Items:", .Font = futuraSmall, .Location = New Point(xLeft, yTop), .AutoSize = True, .ForeColor = Color.DarkBlue}
            card.Controls.Add(lblItemsTitle)
            yTop += 20

            For Each item In orderItems
                Dim lblItem As New Label With {
                        .Text = $"• {item.ModelName} (Qty: {item.Quantity}) - ₱{item.ItemTotal:N2}",
                        .Font = futuraSmall,
                        .Location = New Point(xLeft + 10, yTop),
                        .AutoSize = True
                    }
                card.Controls.Add(lblItem)
                yTop += 18
            Next

            ' Show order total
            yTop += 5
            Dim lblOrderTotal As New Label With {
                    .Text = $"Order Total: ₱{orderTotal:N2}",
                    .Font = New Font(futuraMed.FontFamily, 12, FontStyle.Bold),
                    .Location = New Point(xLeft, yTop),
                    .AutoSize = True,
                    .ForeColor = Color.DarkBlue
                }
            card.Controls.Add(lblOrderTotal)
            yTop += 22
            Dim lblOrderStatus As New Label With {.Text = $"Order Status: {orderStatus}", .Font = futuraSmall, .Location = New Point(xLeft + 250, 15), .AutoSize = True}
            card.Controls.Add(lblOrderStatus)
            Dim lblPayStatus As New Label With {.Text = $"Payment: {paymentStatus} ({paymentOption})", .Font = futuraSmall, .Location = New Point(xLeft + 250, 37), .AutoSize = True}
            card.Controls.Add(lblPayStatus)

            ' --- Return and Cancel Buttons ---
            ' Check if any item in the order can be returned
            Dim canReturn As Boolean = (orderStatus.ToLower() = "delivered" Or orderStatus.ToLower() = "completed") AndAlso
                                          orderItems.Any(Function(item) IsDBNull(item.ReturnId) OrElse item.ReturnId Is Nothing)
            Dim canCancel As Boolean = (orderStatus.ToLower() = "pending" Or orderStatus.ToLower() = "processing")

            Dim btnReturn As New Button With {
                    .Text = "Return",
                    .BackColor = If(canReturn, Color.Red, Color.LightGray),
                    .ForeColor = Color.White,
                    .Font = futuraSmall,
                    .Location = New Point(570, 100),
                    .Size = New Size(100, 35),
                    .Enabled = canReturn
                }
            card.Controls.Add(btnReturn)

            Dim btnCancel As New Button With {
                    .Text = "Cancel",
                    .BackColor = If(canCancel, Color.Black, Color.LightGray),
                    .ForeColor = Color.White,
                    .Font = futuraSmall,
                    .Location = New Point(570, 60),
                    .Size = New Size(100, 35),
                    .Enabled = canCancel
                }
            card.Controls.Add(btnCancel)

            ' Show return status if any items were returned
            Dim returnedItems = orderItems.Where(Function(item) Not IsDBNull(item.ReturnId) AndAlso item.ReturnId IsNot Nothing).ToList()
            If returnedItems.Any() Then
                Dim lblReturned As New Label With {
                        .Text = $"Returns: {returnedItems.Count} item(s) returned",
                        .Font = futuraSmall,
                        .Location = New Point(570, 140),
                        .AutoSize = True,
                        .ForeColor = Color.ForestGreen
                    }
                card.Controls.Add(lblReturned)
            End If

            AddHandler btnReturn.Click, Sub()
                                            If orderStatus.ToLower() = "pending" Or orderStatus.ToLower() = "processing" Then
                                                MessageBox.Show("You can only return after delivery is completed.")
                                                Return
                                            End If

                                            ' Show list of returnable items
                                            Dim returnableItems = orderItems.Where(Function(item) IsDBNull(item.ReturnId) OrElse item.ReturnId Is Nothing).ToList()
                                            If Not returnableItems.Any() Then
                                                MessageBox.Show("No items in this order can be returned.")
                                                Return
                                            End If

                                            Dim itemList As String = "Select item to return:" & vbCrLf
                                            For i As Integer = 0 To returnableItems.Count - 1
                                                itemList += $"{i + 1}. {returnableItems(i).ModelName} (Qty: {returnableItems(i).Quantity})" & vbCrLf
                                            Next

                                            Dim itemChoice As String = InputBox(itemList & vbCrLf & "Enter item number:", "Return Item", "1")
                                            Dim itemIndex As Integer
                                            If Not Integer.TryParse(itemChoice, itemIndex) OrElse itemIndex < 1 OrElse itemIndex > returnableItems.Count Then
                                                MessageBox.Show("Invalid item selection.")
                                                Return
                                            End If

                                            Dim selectedItem = returnableItems(itemIndex - 1)

                                            Dim reason As String = InputBox("Reason for return:", "Return Item")
                                            If String.IsNullOrWhiteSpace(reason) Then Return

                                            Dim allowedConditions As String() = {"Damaged", "Defect", "Others"}
                                            Dim condition As String = InputBox("Condition (Damaged, Defect, Others):", "Return Item", "Damaged")
                                            If String.IsNullOrWhiteSpace(condition) Then Return
                                            condition = condition.Trim()
                                            If Not allowedConditions.Contains(condition) Then
                                                MessageBox.Show("Invalid condition. Please enter one of: Damaged, Defect, Others.")
                                                Return
                                            End If

                                            Dim qtyStr As String = InputBox($"Quantity to return (max {selectedItem.Quantity}):", "Return Item", "1")
                                            Dim qtyReturn As Integer = 1
                                            If Not Integer.TryParse(qtyStr, qtyReturn) OrElse qtyReturn < 1 OrElse qtyReturn > selectedItem.Quantity Then
                                                MessageBox.Show("Invalid quantity.")
                                                Return
                                            End If

                                            Try
                                                Using connR As New MySqlConnection(connStr)
                                                    connR.Open()
                                                    Dim cmdR As New MySqlCommand("INSERT INTO Returns (order_id, return_date, reason, condition_, return_status, quantity_returned) VALUES (@oid, NOW(), @reason, @cond, 'Pending', @qty)", connR)
                                                    cmdR.Parameters.AddWithValue("@oid", orderId)
                                                    cmdR.Parameters.AddWithValue("@reason", reason)
                                                    cmdR.Parameters.AddWithValue("@cond", condition)
                                                    cmdR.Parameters.AddWithValue("@qty", qtyReturn)
                                                    cmdR.ExecuteNonQuery()
                                                End Using
                                                MessageBox.Show("Return request submitted!", "Return", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                lblOrder_Click(Nothing, Nothing)
                                            Catch ex As Exception
                                                MessageBox.Show("Failed to submit return: " & ex.Message)
                                            End Try
                                        End Sub

            AddHandler btnCancel.Click, Sub()
                                            If MessageBox.Show("Are you sure you want to cancel this order?", "Cancel Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                                Try
                                                    Using connC As New MySqlConnection(connStr)
                                                        connC.Open()
                                                        Dim transaction As MySqlTransaction = connC.BeginTransaction()
                                                        Try
                                                            ' checks how many items are there in order for updating stock again (cancellation)
                                                            Dim cmdGet As New MySqlCommand("SELECT model_id, branch_id, quantity FROM order_items WHERE order_id = @oid", connC, transaction)
                                                            cmdGet.Parameters.AddWithValue("@oid", orderId)
                                                            Dim orderReader As MySqlDataReader = cmdGet.ExecuteReader()

                                                            Dim itemsToReturn As New List(Of Tuple(Of Integer, Integer, Integer))()
                                                            While orderReader.Read()
                                                                Dim modelId As Integer = orderReader.GetInt32("model_id")
                                                                Dim branchId As Integer = orderReader.GetInt32("branch_id")
                                                                Dim orderQuantity As Integer = orderReader.GetInt32("quantity")
                                                                itemsToReturn.Add(New Tuple(Of Integer, Integer, Integer)(modelId, branchId, orderQuantity))
                                                            End While
                                                            orderReader.Close()

                                                            ' Update the order status to cancelled
                                                            Dim cmdC As New MySqlCommand("UPDATE orders SET order_status = 'cancelled' WHERE order_id = @oid", connC, transaction)
                                                            cmdC.Parameters.AddWithValue("@oid", orderId)
                                                            cmdC.ExecuteNonQuery()

                                                            ' Return all items to stock
                                                            For Each item In itemsToReturn
                                                                Dim cmdStock As New MySqlCommand("UPDATE branch_model SET stock = stock + @qty WHERE branch_id = @bid AND model_id = @mid", connC, transaction)
                                                                cmdStock.Parameters.AddWithValue("@qty", item.Item3)
                                                                cmdStock.Parameters.AddWithValue("@bid", item.Item2)
                                                                cmdStock.Parameters.AddWithValue("@mid", item.Item1)
                                                                cmdStock.ExecuteNonQuery()
                                                            Next

                                                            transaction.Commit()
                                                            MessageBox.Show("Order cancelled and stock returned.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                            lblOrder_Click(Nothing, Nothing)

                                                        Catch ex As Exception
                                                            transaction.Rollback()
                                                            MessageBox.Show("Failed to cancel order: " & ex.Message)
                                                        End Try
                                                    End Using
                                                Catch ex As Exception
                                                    MessageBox.Show("Failed to cancel order: " & ex.Message)
                                                End Try
                                            End If
                                        End Sub

            ' Add View Receipt button
            Dim btnReceipt As New Button With {
                    .Text = "View Receipt",
                    .BackColor = Color.Blue,
                    .ForeColor = Color.White,
                    .Font = futuraSmall,
                    .Location = New Point(570, 180),
                    .Size = New Size(100, 35)
                }
            card.Controls.Add(btnReceipt)

            AddHandler btnReceipt.Click, Sub()
                                             Try 'TERMTEST 
                                                 Dim receiptGen As New SimpleReceiptGenerator(connStr)
                                                 receiptGen.ShowTextReceipt(orderId)
                                             Catch ex As Exception
                                                 MessageBox.Show("Error viewing receipt: " & ex.Message, "Receipt Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             End Try
                                         End Sub

            home_flow.Controls.Add(card)
        Next
    End Sub

#Region "Buying/Adding-To-Cart"
    Private Sub lblCart_Click(sender As Object, e As EventArgs) Handles lblCart.Click
        lblStatus.Text = "YOUR CART:"
        home_panel.Hide()
        home_flow.Show()
        btnBack.Hide()
        home_flow.Controls.Clear()
        navigationStack.Clear()
        cartItems.Clear()

        Using conn As New MySqlConnection(connStr)
            conn.Open()

            Dim query As String = "
        SELECT m.model_name, m.photo, m.type, m.transmission, m.price, 
               c.quantity, c.model_id, c.branch_id, m.year_model, bm.stock
        FROM cart c
        JOIN models m ON c.model_id = m.model_id
        JOIN branch_model bm ON c.model_id = bm.model_id AND c.branch_id = bm.branch_id
        WHERE c.user_id = @uid"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@uid", currentUserId)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim modelId As Integer = reader("model_id")
                Dim branchId As Integer = reader("branch_id")
                Dim qty As Integer = reader("quantity")
                Dim unitPrice As Decimal = reader("price")
                Dim subtotal As Decimal = unitPrice * qty

                Dim modelName As String = reader("model_name").ToString()
                Dim modelType As String = reader("type").ToString()
                Dim transmission As String = reader("transmission").ToString()
                Dim yearModel As String = reader("year_model").ToString()
                Dim stock As Integer = CInt(reader("stock"))

                Dim photoBytes As Byte() = CType(reader("photo"), Byte())

                ' Add to cart items list
                cartItems.Add(New CartItem With {
                    .ModelId = modelId,
                    .BranchId = branchId,
                    .Quantity = qty,
                    .UnitPrice = unitPrice,
                    .ModelName = modelName,
                    .ModelType = modelType,
                    .Transmission = transmission,
                    .YearModel = yearModel,
                    .Stock = stock,
                    .PhotoBytes = photoBytes
                })

                Dim panel As New Panel With {
                    .Size = New Size(700, 125),
                    .BackColor = Color.White,
                    .Margin = New Padding(10),
                    .BorderStyle = BorderStyle.FixedSingle
                }

                ' Add checkbox for selection
                Dim chkSelect As New CheckBox With {
                    .Location = New Point(10, 50),
                    .Size = New Size(20, 20),
                    .Checked = True ' Default to selected
                }
                panel.Controls.Add(chkSelect)

                Dim pic As New PictureBox With {
                    .Size = New Size(100, 100),
                    .Location = New Point(40, 10),
                    .SizeMode = PictureBoxSizeMode.StretchImage,
                    .Image = GetImageFromBytes(photoBytes)
                }
                panel.Controls.Add(pic)

                Dim lblName As New Label With {
                    .Text = modelName,
                    .Font = New Font("Futura-Medium", 16, FontStyle.Bold),
                    .Location = New Point(150, 10),
                    .AutoSize = True
                }
                panel.Controls.Add(lblName)

                Dim lblInfo As New Label With {
                    .Text = $"{modelType} | {transmission} | {yearModel}",
                    .Location = New Point(150, 40),
                    .AutoSize = True,
                    .Font = New Font("Futura-Medium", 13.8, FontStyle.Bold),
                    .ForeColor = Color.DimGray
                }
                panel.Controls.Add(lblInfo)

                Dim lblSubtotal As New Label With {
                    .Text = $"Subtotal: ₱{subtotal:N2}",
                    .Location = New Point(150, 75),
                    .AutoSize = True,
                    .Font = New Font("Futura-Medium", 12, FontStyle.Bold)
                }
                panel.Controls.Add(lblSubtotal)

                home_flow.Controls.Add(panel)
            End While
        End Using

        ' Add checkout and remove buttons at the bottom
        If cartItems.Count > 0 Then
            Dim buttonPanel As New Panel With {
                .Size = New Size(700, 60),
                .BackColor = Color.LightGray,
                .Margin = New Padding(10),
                .BorderStyle = BorderStyle.FixedSingle
            }

            Dim btnCheckout As New Button With {
                .Text = "Checkout Selected Items",
                .Location = New Point(20, 15),
                .Size = New Size(200, 30),
                .BackColor = Color.Green,
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Futura-Medium", 12, FontStyle.Bold)
            }
            buttonPanel.Controls.Add(btnCheckout)

            Dim btnRemove As New Button With {
                .Text = "Remove Selected",
                .Location = New Point(240, 15),
                .Size = New Size(200, 30),
                .BackColor = Color.Red,
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Futura-Medium", 12, FontStyle.Bold)
            }
            buttonPanel.Controls.Add(btnRemove)

            AddHandler btnCheckout.Click, Sub()
                                              ' Get selected items
                                              Dim selectedItems As New List(Of CartItem)
                                              Dim itemIndex As Integer = 0

                                              For i As Integer = 0 To home_flow.Controls.Count - 2 ' -2 to exclude button panel
                                                  Dim panel As Panel = TryCast(home_flow.Controls(i), Panel)
                                                  If panel IsNot Nothing Then
                                                      Dim chk As CheckBox = TryCast(panel.Controls(0), CheckBox)
                                                      If chk IsNot Nothing AndAlso chk.Checked Then
                                                          If itemIndex < cartItems.Count Then
                                                              selectedItems.Add(cartItems(itemIndex))
                                                          End If
                                                      End If
                                                      itemIndex += 1
                                                  End If
                                              Next

                                              If selectedItems.Count = 0 Then
                                                  MessageBox.Show("Please select at least one item to checkout.", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                  Return
                                              End If

                                              ' Show checkout form
                                              ShowCheckoutForm(selectedItems)
                                          End Sub

            AddHandler btnRemove.Click, Sub()
                                            ' Remove selected items
                                            Dim itemsToRemove As New List(Of Integer)
                                            For i As Integer = 0 To home_flow.Controls.Count - 2 ' -2 to exclude button panel
                                                Dim panel As Panel = TryCast(home_flow.Controls(i), Panel)
                                                If panel IsNot Nothing Then
                                                    Dim chk As CheckBox = TryCast(panel.Controls(0), CheckBox)
                                                    If chk IsNot Nothing AndAlso chk.Checked Then
                                                        itemsToRemove.Add(i)
                                                    End If
                                                End If
                                            Next

                                            If itemsToRemove.Count = 0 Then
                                                MessageBox.Show("Please select at least one item to remove.", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                Return
                                            End If

                                            If MessageBox.Show($"Remove {itemsToRemove.Count} selected item(s)?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                                ' Remove from database
                                                Using conn2 As New MySqlConnection(connStr)
                                                    conn2.Open()
                                                    For Each index In itemsToRemove
                                                        Dim item = cartItems(index)
                                                        Dim delCmd As New MySqlCommand("DELETE FROM cart WHERE user_id=@uid AND model_id=@model AND branch_id=@branch", conn2)
                                                        delCmd.Parameters.AddWithValue("@uid", currentUserId)
                                                        delCmd.Parameters.AddWithValue("@model", item.ModelId)
                                                        delCmd.Parameters.AddWithValue("@branch", item.BranchId)
                                                        delCmd.ExecuteNonQuery()
                                                    Next
                                                End Using

                                                ' Refresh cart
                                                lblCart_Click(Nothing, Nothing)
                                            End If
                                        End Sub

            home_flow.Controls.Add(buttonPanel)
        End If
    End Sub

    Private Sub ShowCheckoutForm(selectedItems As List(Of CartItem))
        lblStatus.Text = "CHECKOUT:"
        home_flow.Controls.Clear()
        btnBack.Show()

        ' Get user info
        Dim userName As String = "", userAddress As String = "", userFirst As String = "", userLast As String = "", userEmail As String = "", userPhone As String = ""
        Using connUser As New MySqlConnection(connStr)
            connUser.Open()
            Dim cmdUser As New MySqlCommand("SELECT username, address, first_name, last_name, email, phone_number FROM accounts WHERE user_id=@uid", connUser)
            cmdUser.Parameters.AddWithValue("@uid", currentUserId)
            Using readerU = cmdUser.ExecuteReader()
                If readerU.Read() Then
                    userName = readerU("username").ToString()
                    userAddress = readerU("address").ToString()
                    userFirst = readerU("first_name").ToString()
                    userLast = readerU("last_name").ToString()
                    userEmail = readerU("email").ToString()
                    userPhone = readerU("phone_number").ToString()
                End If
            End Using
        End Using

        Dim futuraBig As New Font("Futura-Medium", 16, FontStyle.Bold)
        Dim futuraMed As New Font("Futura-Medium", 13.8, FontStyle.Bold)
        Dim futuraSmall As New Font("Futura-Medium", 11, FontStyle.Regular)

        Dim checkoutPanel As New Panel With {.Width = 780, .Height = 800, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle}
        Dim y As Integer = 10

        ' Title
        Dim lblTitle As New Label With {.Text = "Checkout", .Font = futuraBig, .Location = New Point(20, y), .AutoSize = True}
        checkoutPanel.Controls.Add(lblTitle)
        y += 40

        ' User Info GroupBox
        Dim grpUser As New GroupBox With {.Text = "Your Info", .Location = New Point(20, y), .Size = New Size(740, 90), .Font = futuraMed}
        Dim lblUserInfo As New Label With {
            .Text = $"Username: {userName}    Name: {userFirst} {userLast}" & vbCrLf &
                    $"Email: {userEmail}    Phone: {userPhone}" & vbCrLf &
                    $"Address: {userAddress}",
            .Font = futuraSmall,
            .Location = New Point(15, 25),
            .AutoSize = True
        }
        grpUser.Controls.Add(lblUserInfo)
        checkoutPanel.Controls.Add(grpUser)
        y += 100

        ' Selected Items GroupBox with FlowLayoutPanel for better item display
        Dim grpItems As New GroupBox With {.Text = "Selected Items", .Location = New Point(20, y), .Size = New Size(740, 300), .Font = futuraMed}

        ' Create FlowLayoutPanel for items
        Dim flowItems As New FlowLayoutPanel With {
            .Location = New Point(10, 25),
            .Size = New Size(720, 270),
            .AutoScroll = True,
            .FlowDirection = FlowDirection.TopDown,
            .WrapContents = False
        }
        grpItems.Controls.Add(flowItems)

        Dim totalAmount As Decimal = 0
        For Each item In selectedItems
            Dim itemPanel As New Panel With {.Size = New Size(700, 60), .BackColor = Color.LightGray, .Margin = New Padding(5)}

            Dim pic As New PictureBox With {
                .Size = New Size(50, 50),
                .Location = New Point(10, 5),
                .SizeMode = PictureBoxSizeMode.Zoom,
                .Image = GetImageFromBytes(item.PhotoBytes)
            }
            itemPanel.Controls.Add(pic)

            Dim lblItemName As New Label With {
                .Text = item.ModelName,
                .Font = futuraSmall,
                .Location = New Point(70, 5),
                .AutoSize = True
            }
            itemPanel.Controls.Add(lblItemName)

            Dim lblItemInfo As New Label With {
                .Text = $"{item.ModelType} | {item.Transmission} | {item.YearModel}",
                .Font = New Font(futuraSmall.FontFamily, 9),
                .Location = New Point(70, 25),
                .AutoSize = True,
                .ForeColor = Color.DimGray
            }
            itemPanel.Controls.Add(lblItemInfo)

            Dim lblQty As New Label With {
                .Text = $"Qty: {item.Quantity}",
                .Font = futuraSmall,
                .Location = New Point(400, 5),
                .AutoSize = True
            }
            itemPanel.Controls.Add(lblQty)

            Dim itemSubtotal As Decimal = item.UnitPrice * item.Quantity
            totalAmount += itemSubtotal
            Dim lblSubtotal As New Label With {
                .Text = $"₱{itemSubtotal:N2}",
                .Font = futuraSmall,
                .Location = New Point(500, 5),
                .AutoSize = True,
                .ForeColor = Color.DarkGreen
            }
            itemPanel.Controls.Add(lblSubtotal)

            flowItems.Controls.Add(itemPanel)
        Next
        checkoutPanel.Controls.Add(grpItems)
        y += 320

        ' Payment Section - Improved with better layout and more payment options
        Dim grpPay As New GroupBox With {.Text = "Payment Method", .Location = New Point(20, y), .Size = New Size(740, 120), .Font = futuraMed}

        ' Payment method selection with radio buttons for better UX
        Dim lblPay As New Label With {.Text = "Select Payment Method:", .Location = New Point(15, 25), .AutoSize = True, .Font = futuraSmall, .ForeColor = Color.DarkBlue}
        grpPay.Controls.Add(lblPay)

        Dim rbCash As New RadioButton With {.Text = "Cash on Delivery", .Location = New Point(15, 50), .Font = futuraSmall, .Checked = True}
        Dim rbATM As New RadioButton With {.Text = "ATM/Bank Transfer", .Location = New Point(200, 50), .Font = futuraSmall}
        Dim rbCredit As New RadioButton With {.Text = "Credit Card", .Location = New Point(400, 50), .Font = futuraSmall}
        grpPay.Controls.Add(rbCash)
        grpPay.Controls.Add(rbATM)
        grpPay.Controls.Add(rbCredit)

        ' Voucher section
        Dim lblVoucher As New Label With {.Text = "Voucher Code (Optional):", .Location = New Point(15, 85), .AutoSize = True, .Font = futuraSmall}
        Dim txtVoucher As New TextBox With {.Width = 120, .Location = New Point(200, 82), .Font = futuraSmall, .Text = "Enter voucher code"}
        ' Add placeholder behavior
        AddHandler txtVoucher.GotFocus, Sub()
                                            If txtVoucher.Text = "Enter voucher code" Then
                                                txtVoucher.Text = ""
                                                txtVoucher.ForeColor = Color.Black
                                            End If
                                        End Sub
        AddHandler txtVoucher.LostFocus, Sub()
                                             If String.IsNullOrWhiteSpace(txtVoucher.Text) Then
                                                 txtVoucher.Text = "Enter voucher code"
                                                 txtVoucher.ForeColor = Color.Gray
                                             End If
                                         End Sub
        txtVoucher.ForeColor = Color.Gray
        Dim btnCheckVoucher As New Button With {.Text = "Apply", .Location = New Point(330, 80), .Width = 60, .Height = 25, .Font = futuraSmall, .BackColor = Color.DarkGreen, .ForeColor = Color.White}
        grpPay.Controls.Add(lblVoucher)
        grpPay.Controls.Add(txtVoucher)
        grpPay.Controls.Add(btnCheckVoucher)
        checkoutPanel.Controls.Add(grpPay)
        y += 130

        ' Order Summary
        Dim grpSummary As New GroupBox With {.Text = "Order Summary", .Location = New Point(20, y), .Size = New Size(740, 100), .Font = futuraMed}
        Dim lblTotal As New Label With {.Text = $"Total Amount: ₱{totalAmount:N2}", .Location = New Point(15, 25), .AutoSize = True, .Font = futuraSmall}
        Dim lblDiscount As New Label With {.Text = "", .Location = New Point(15, 45), .AutoSize = True, .ForeColor = Color.ForestGreen, .Font = futuraSmall}
        Dim lblFinal As New Label With {.Text = "", .Location = New Point(15, 65), .AutoSize = True, .Font = futuraMed}
        grpSummary.Controls.Add(lblTotal)
        grpSummary.Controls.Add(lblDiscount)
        grpSummary.Controls.Add(lblFinal)
        checkoutPanel.Controls.Add(grpSummary)
        y += 120

        ' Buttons
        Dim btnConfirm As New Button With {
            .Text = "Confirm Order",
            .BackColor = Color.Green,
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Width = 360,
            .Height = 40,
            .Location = New Point(20, y),
            .Font = futuraMed
        }
        checkoutPanel.Controls.Add(btnConfirm)

        Dim btnCancel As New Button With {
            .Text = "Cancel",
            .BackColor = Color.Red,
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Width = 360,
            .Height = 40,
            .Location = New Point(400, y),
            .Font = futuraMed
        }
        checkoutPanel.Controls.Add(btnCancel)

        home_flow.Controls.Add(checkoutPanel)

        ' Voucher logic
        Dim discountPercent As Integer = 0
        Dim finalAmount As Decimal = totalAmount
        AddHandler btnCheckVoucher.Click, Sub()
                                              If String.IsNullOrWhiteSpace(txtVoucher.Text) Then
                                                  lblDiscount.Text = "Enter a voucher code."
                                                  lblFinal.Text = ""
                                                  Return
                                              End If
                                              Using connV As New MySqlConnection(connStr)
                                                  connV.Open()
                                                  Dim cmdV As New MySqlCommand("SELECT percent_sale, from_date, to_date, status FROM vouchers WHERE voucher_code=@code", connV)
                                                  cmdV.Parameters.AddWithValue("@code", txtVoucher.Text.Trim())
                                                  Using readerV = cmdV.ExecuteReader()
                                                      If readerV.Read() Then
                                                          Dim percent As Integer = Convert.ToInt32(readerV("percent_sale"))
                                                          Dim fromDate As Date = Convert.ToDateTime(readerV("from_date"))
                                                          Dim toDate As Date = Convert.ToDateTime(readerV("to_date"))
                                                          Dim statusV As String = readerV("status").ToString()
                                                          If statusV = "active" AndAlso Date.Now >= fromDate AndAlso Date.Now <= toDate Then
                                                              discountPercent = percent
                                                              Dim discountAmount As Decimal = totalAmount * (percent / 100D)
                                                              finalAmount = totalAmount - discountAmount
                                                              lblDiscount.Text = $"Voucher valid! {percent}% off (-₱{discountAmount:N2})"
                                                              lblFinal.Text = $"Final Amount: ₱{finalAmount:N2}"
                                                          Else
                                                              discountPercent = 0
                                                              finalAmount = totalAmount
                                                              lblDiscount.Text = "Voucher is not active or not valid for this date."
                                                              lblFinal.Text = ""
                                                          End If
                                                      Else
                                                          discountPercent = 0
                                                          finalAmount = totalAmount
                                                          lblDiscount.Text = "Voucher not found."
                                                          lblFinal.Text = ""
                                                      End If
                                                  End Using
                                              End Using
                                          End Sub

        ' Button handlers
        AddHandler btnCancel.Click, Sub()
                                        lblCart_Click(Nothing, Nothing)
                                    End Sub

        AddHandler btnConfirm.Click, Sub()
                                         ' Get selected payment method from radio buttons
                                         Dim paymentOpt As String = ""
                                         If rbCash.Checked Then
                                             paymentOpt = "cash"
                                         ElseIf rbATM.Checked Then
                                             paymentOpt = "ATM"
                                         ElseIf rbCredit.Checked Then
                                             paymentOpt = "credit"
                                         Else
                                             MessageBox.Show("Please select a payment method.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                             Return
                                         End If
                                         ' Handle voucher code properly - check if it's placeholder text or empty
                                         Dim voucherCode As String = Nothing
                                         If Not String.IsNullOrWhiteSpace(txtVoucher.Text) AndAlso txtVoucher.Text <> "Enter voucher code" Then
                                             voucherCode = txtVoucher.Text.Trim()
                                         End If
                                         Dim deliveryDate As Date = DateTime.Now.AddDays(5)

                                         Try
                                             Using connOrder As New MySqlConnection(connStr)
                                                 connOrder.Open()

                                                 ' Validate voucher code if provided
                                                 If Not String.IsNullOrEmpty(voucherCode) Then
                                                     Dim cmdValidateVoucher As New MySqlCommand("SELECT COUNT(*) FROM vouchers WHERE voucher_code = @code", connOrder)
                                                     cmdValidateVoucher.Parameters.AddWithValue("@code", voucherCode)
                                                     Dim voucherExists As Integer = Convert.ToInt32(cmdValidateVoucher.ExecuteScalar())
                                                     If voucherExists = 0 Then
                                                         MessageBox.Show($"Voucher code '{voucherCode}' does not exist. Please enter a valid voucher code or leave it empty.", "Invalid Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                         Return
                                                     End If
                                                 End If

                                                 Dim transaction As MySqlTransaction = connOrder.BeginTransaction() 'MP2 - Customer CRUD
                                                 Try
                                                     ' Create the main order
                                                     Dim cmdInsertOrder As New MySqlCommand("
INSERT INTO orders (user_id, date_ordered, est_delivery, order_status, voucher_code, payment_status, payment_option)
VALUES (@user, NOW(), @deliv, 'pending', @voucher, 'pending', @pay)", connOrder, transaction)
                                                     cmdInsertOrder.Parameters.AddWithValue("@user", currentUserId)
                                                     cmdInsertOrder.Parameters.AddWithValue("@deliv", deliveryDate)
                                                     cmdInsertOrder.Parameters.AddWithValue("@voucher", If(voucherCode Is Nothing, DBNull.Value, voucherCode))
                                                     cmdInsertOrder.Parameters.AddWithValue("@pay", paymentOpt)
                                                     cmdInsertOrder.ExecuteNonQuery()

                                                     ' Get the order ID
                                                     Dim orderId As Integer = 0
                                                     Dim cmdGetOrder As New MySqlCommand("SELECT LAST_INSERT_ID()", connOrder, transaction)
                                                     orderId = Convert.ToInt32(cmdGetOrder.ExecuteScalar())

                                                     ' Create order items
                                                     For Each item In selectedItems
                                                         Dim cmdInsertItem As New MySqlCommand("
INSERT INTO order_items (order_id, model_id, branch_id, quantity, unit_price)
VALUES (@orderId, @modelId, @branchId, @qty, @unitPrice)", connOrder, transaction)
                                                         cmdInsertItem.Parameters.AddWithValue("@orderId", orderId)
                                                         cmdInsertItem.Parameters.AddWithValue("@modelId", item.ModelId)
                                                         cmdInsertItem.Parameters.AddWithValue("@branchId", item.BranchId)
                                                         cmdInsertItem.Parameters.AddWithValue("@qty", item.Quantity)
                                                         cmdInsertItem.Parameters.AddWithValue("@unitPrice", item.UnitPrice)
                                                         cmdInsertItem.ExecuteNonQuery()

                                                         ' Update stock
                                                         Dim updateStockCmd As New MySqlCommand("UPDATE branch_model SET stock = stock - @qty WHERE branch_id = @branch AND model_id = @model", connOrder, transaction)
                                                         updateStockCmd.Parameters.AddWithValue("@qty", item.Quantity)
                                                         updateStockCmd.Parameters.AddWithValue("@branch", item.BranchId)
                                                         updateStockCmd.Parameters.AddWithValue("@model", item.ModelId)
                                                         updateStockCmd.ExecuteNonQuery()

                                                         ' Remove from cart
                                                         Dim delCart As New MySqlCommand("DELETE FROM cart WHERE user_id = @uid AND model_id = @model AND branch_id = @branch", connOrder, transaction)
                                                         delCart.Parameters.AddWithValue("@uid", currentUserId)
                                                         delCart.Parameters.AddWithValue("@model", item.ModelId)
                                                         delCart.Parameters.AddWithValue("@branch", item.BranchId)
                                                         delCart.ExecuteNonQuery()
                                                     Next

                                                     transaction.Commit()

                                                     ' Show payment form
                                                     Dim paymentForm As New PaymentForm(connStr, orderId, finalAmount, paymentOpt)
                                                     If paymentForm.ShowDialog() = DialogResult.OK Then
                                                         MessageBox.Show("Order placed and payment processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                     Else
                                                         MessageBox.Show("Order placed successfully! Payment was cancelled.", "Success with Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                     End If

                                                     lblCart_Click(Nothing, Nothing)
                                                 Catch ex As Exception
                                                     transaction.Rollback()
                                                     MessageBox.Show("Failed to place order: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                 End Try
                                             End Using
                                         Catch ex As Exception
                                             MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                         End Try
                                     End Sub
    End Sub

    Private Sub pbCart_Click(sender As Object, e As EventArgs) Handles pbCart.Click
        lblCart_Click(sender, e)
    End Sub
    Private Sub LoadModels(branchId As Integer, brand As String)
        lblStatus.Text = "SELECT YOUR MODEL:"
        navigationStack.Push(Sub() LoadBrands(branchId))
        home_flow.Controls.Clear()
        btnBack.Show()

        Dim query As String = "SELECT m.model_id, m.model_name, m.photo, m.year_model, m.type, m.transmission, m.price, bm.stock
                           FROM models m
                           JOIN branch_model bm ON m.model_id = bm.model_id
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
                .Size = New Size(260, 400),
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

                ' --- Buy Now Button ---
                Dim btnBuy As New Button With {
                .Text = "Buy Now",
                .BackColor = Color.Red,
                .ForeColor = Color.White,
                .Dock = DockStyle.Bottom,
                .Height = 38,
                .Font = New Font("Futura-Medium", 13.8, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat
            }

                ' --- Add to Cart Button ---
                Dim btnCart As New Button With {
                .Text = "Add to Cart",
                .BackColor = Color.Black,
                .ForeColor = Color.White,
                .Dock = DockStyle.Bottom,
                .Height = 38,
                .Font = New Font("Futura-Medium", 13.8, FontStyle.Bold),
                .FlatStyle = FlatStyle.Flat
            }

                ' Add controls (bottom buttons first)
                card.Controls.Add(btnBuy)
                card.Controls.Add(btnCart)
                card.Controls.Add(lblStock)
                card.Controls.Add(lblPrice)
                card.Controls.Add(lblTrans)
                card.Controls.Add(lblType)
                card.Controls.Add(lblYear)
                card.Controls.Add(lblName)
                card.Controls.Add(pic)
                home_flow.Controls.Add(card)

                ' --- Add to Cart Handler ---
                AddHandler btnCart.Click, Sub()
                                              If stock <= 0 Then
                                                  MessageBox.Show("Sorry, this model is out of stock.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                  Return
                                              End If

                                              ' Show quantity selection dialog
                                              Dim quantityForm As New Form()
                                              quantityForm.Text = "Select Quantity"
                                              quantityForm.Size = New Size(300, 200)
                                              quantityForm.StartPosition = FormStartPosition.CenterParent
                                              quantityForm.FormBorderStyle = FormBorderStyle.FixedDialog
                                              quantityForm.MaximizeBox = False
                                              quantityForm.MinimizeBox = False

                                              Dim lblTitle As New Label With {
                                                  .Text = $"Add {modelName} to Cart",
                                                  .Font = New Font("Arial", 12, FontStyle.Bold),
                                                  .Location = New Point(20, 20),
                                                  .AutoSize = True
                                              }
                                              quantityForm.Controls.Add(lblTitle)

                                              Dim lblQty As New Label With {
                                                  .Text = "Quantity:",
                                                  .Font = New Font("Arial", 10),
                                                  .Location = New Point(20, 60),
                                                  .AutoSize = True
                                              }
                                              quantityForm.Controls.Add(lblQty)

                                              Dim numQty As New NumericUpDown With {
                                                  .Minimum = 1,
                                                  .Maximum = stock,
                                                  .Value = 1,
                                                  .Location = New Point(100, 58),
                                                  .Width = 80
                                              }
                                              quantityForm.Controls.Add(numQty)

                                              Dim lblStockInfo As New Label With {
                                                  .Text = $"Available: {stock}",
                                                  .Font = New Font("Arial", 9),
                                                  .Location = New Point(20, 85),
                                                  .AutoSize = True,
                                                  .ForeColor = Color.DarkGray
                                              }
                                              quantityForm.Controls.Add(lblStockInfo)

                                              Dim btnAdd As New Button With {
                                                  .Text = "Add to Cart",
                                                  .DialogResult = DialogResult.OK,
                                                  .Location = New Point(50, 120),
                                                  .Width = 80,
                                                  .BackColor = Color.Green,
                                                  .ForeColor = Color.White
                                              }
                                              quantityForm.Controls.Add(btnAdd)

                                              Dim btnCancel As New Button With {
                                                  .Text = "Cancel",
                                                  .DialogResult = DialogResult.Cancel,
                                                  .Location = New Point(150, 120),
                                                  .Width = 80
                                              }
                                              quantityForm.Controls.Add(btnCancel)

                                              If quantityForm.ShowDialog() = DialogResult.OK Then
                                                  Dim selectedQty As Integer = CInt(numQty.Value)

                                                  Using conn2 As New MySqlConnection(connStr)
                                                      conn2.Open()
                                                      Dim checkCmd As New MySqlCommand("SELECT quantity FROM cart WHERE user_id = @uid AND model_id = @model AND branch_id = @branch", conn2)
                                                      checkCmd.Parameters.AddWithValue("@uid", currentUserId)
                                                      checkCmd.Parameters.AddWithValue("@model", thisModelId)
                                                      checkCmd.Parameters.AddWithValue("@branch", branchId)
                                                      Dim existingQty As Object = checkCmd.ExecuteScalar()

                                                      If existingQty IsNot Nothing Then
                                                          Dim currentQty As Integer = Convert.ToInt32(existingQty)
                                                          Dim newQty As Integer = currentQty + selectedQty
                                                          If newQty > stock Then
                                                              MessageBox.Show($"Cannot add {selectedQty} more items. You already have {currentQty} in cart, and only {stock} are available.", "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                              Return
                                                          End If

                                                          ' Update existing cart item
                                                          Dim updateCmd As New MySqlCommand("UPDATE Cart SET quantity = quantity + @qty WHERE user_id = @uid AND model_id = @model AND branch_id = @branch", conn2)
                                                          updateCmd.Parameters.AddWithValue("@qty", selectedQty)
                                                          updateCmd.Parameters.AddWithValue("@uid", currentUserId)
                                                          updateCmd.Parameters.AddWithValue("@model", thisModelId)
                                                          updateCmd.Parameters.AddWithValue("@branch", branchId)
                                                          updateCmd.ExecuteNonQuery()
                                                      Else
                                                          ' Insert new cart item
                                                          Dim insertCmd As New MySqlCommand("INSERT INTO Cart (user_id, model_id, branch_id, quantity) VALUES (@uid, @model, @branch, @qty)", conn2)
                                                          insertCmd.Parameters.AddWithValue("@uid", currentUserId)
                                                          insertCmd.Parameters.AddWithValue("@model", thisModelId)
                                                          insertCmd.Parameters.AddWithValue("@branch", branchId)
                                                          insertCmd.Parameters.AddWithValue("@qty", selectedQty)
                                                          insertCmd.ExecuteNonQuery()
                                                      End If
                                                  End Using
                                                  MessageBox.Show($"Added {selectedQty} item(s) to cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                              End If
                                          End Sub

                ' --- Buy Now Handler ---
                AddHandler btnBuy.Click, Sub()
                                             btnBack.Hide()
                                             If stock <= 0 Then
                                                 MessageBox.Show("Sorry, this model is out of stock.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                 Return
                                             End If

                                             ' --- Show Buy Panel ---
                                             home_flow.Controls.Clear()
                                             lblStatus.Text = "ORDERING..."

                                             Dim futuraBig As New Font("Futura-Medium", 18, FontStyle.Bold)
                                             Dim futuraMed As New Font("Futura-Medium", 14, FontStyle.Bold)
                                             Dim futuraSmall As New Font("Futura-Medium", 12, FontStyle.Regular)

                                             Dim buyPanel As New Panel With {.Width = 780, .Height = 600, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle}
                                             Dim y As Integer = 10

                                             ' Product Image
                                             Dim pbModel As New PictureBox With {
                    .Image = GetImageFromBytes(photoBytes),
                    .SizeMode = PictureBoxSizeMode.Zoom,
                    .Location = New Point(20, y),
                    .Size = New Size(120, 120),
                    .BorderStyle = BorderStyle.FixedSingle
                }
                                             buyPanel.Controls.Add(pbModel)

                                             Dim lblTitle As New Label With {.Text = modelName, .Font = futuraBig, .Location = New Point(160, y), .AutoSize = True}
                                             buyPanel.Controls.Add(lblTitle)
                                             y += 40

                                             Dim lblProductInfo As New Label With {.Text = $"{brand} | {type} | {transmission} | {yearModel}", .Location = New Point(160, y), .AutoSize = True, .ForeColor = Color.Black, .Font = futuraMed}
                                             buyPanel.Controls.Add(lblProductInfo)
                                             y += 40

                                             ' Stock
                                             Dim lblStockBuy As New Label With {.Text = $"Stock: {stock}", .Location = New Point(160, y), .AutoSize = True, .ForeColor = Color.Black, .Font = futuraSmall}
                                             buyPanel.Controls.Add(lblStockBuy)
                                             y += 30

                                             ' Quantity selector
                                             Dim lblQty As New Label With {.Text = "Quantity:", .Location = New Point(160, y), .AutoSize = True, .Font = futuraSmall}
                                             Dim btnMinus As New Button With {.Text = "-", .Location = New Point(240, y - 3), .Size = New Size(30, 30), .Font = futuraMed}
                                             Dim txtQty As New TextBox With {.Text = "1", .Location = New Point(275, y), .Size = New Size(40, 30), .Font = futuraMed, .TextAlign = HorizontalAlignment.Center, .ReadOnly = True}
                                             Dim btnPlus As New Button With {.Text = "+", .Location = New Point(320, y - 3), .Size = New Size(30, 30), .Font = futuraMed}
                                             buyPanel.Controls.Add(lblQty)
                                             buyPanel.Controls.Add(btnMinus)
                                             buyPanel.Controls.Add(txtQty)
                                             buyPanel.Controls.Add(btnPlus)
                                             y += 25

                                             ' User info
                                             Dim userName As String = "", userAddress As String = "", userFirst As String = "", userLast As String = "", userEmail As String = "", userPhone As String = ""
                                             Using connUser As New MySqlConnection(connStr)
                                                 connUser.Open()
                                                 Dim cmdUser As New MySqlCommand("SELECT username, address, first_name, last_name, email, phone_number FROM accounts WHERE user_id=@uid", connUser)
                                                 cmdUser.Parameters.AddWithValue("@uid", currentUserId)
                                                 Using readerU = cmdUser.ExecuteReader()
                                                     If readerU.Read() Then
                                                         userName = readerU("username").ToString()
                                                         userAddress = readerU("address").ToString()
                                                         userFirst = readerU("first_name").ToString()
                                                         userLast = readerU("last_name").ToString()
                                                         userEmail = readerU("email").ToString()
                                                         userPhone = readerU("phone_number").ToString()
                                                     End If
                                                 End Using
                                             End Using

                                             Dim grpUser As New GroupBox With {.Text = "Your Info", .Location = New Point(20, 160), .Size = New Size(740, 90), .Font = futuraMed}
                                             Dim lblUserInfo As New Label With {
                    .Text = $"Username: {userName}    Name: {userFirst} {userLast}" & vbCrLf &
                            $"Email: {userEmail}    Phone: {userPhone}" & vbCrLf &
                            $"Address: {userAddress}",
                    .Font = futuraSmall,
                    .Location = New Point(15, 25),
                    .AutoSize = True
                }
                                             grpUser.Controls.Add(lblUserInfo)
                                             buyPanel.Controls.Add(grpUser)
                                             y = 265

                                             ' --- Price Section ---
                                             Dim grpPrice As New GroupBox With {.Text = "Order Summary", .Location = New Point(20, y), .Size = New Size(740, 70), .Font = futuraMed}
                                             Dim lblOrig As New Label With {.Text = $"Subtotal: ₱{price:N2} (1x)", .Location = New Point(15, 25), .AutoSize = True, .Font = futuraSmall}
                                             Dim lblDiscount As New Label With {.Text = "", .Location = New Point(15, 45), .AutoSize = True, .ForeColor = Color.ForestGreen, .Font = futuraSmall}
                                             Dim lblFinal As New Label With {.Text = "", .Location = New Point(15, 65), .AutoSize = True, .Font = futuraMed}
                                             grpPrice.Controls.Add(lblOrig)
                                             grpPrice.Controls.Add(lblDiscount)
                                             grpPrice.Controls.Add(lblFinal)
                                             buyPanel.Controls.Add(grpPrice)
                                             y += 80

                                             ' --- Payment Section ---
                                             Dim grpPay As New GroupBox With {.Text = "Payment", .Location = New Point(20, y), .Size = New Size(740, 90), .Font = futuraMed}
                                             Dim lblPay As New Label With {.Text = "Payment Option:", .Location = New Point(15, 25), .AutoSize = True, .Font = futuraSmall}
                                             Dim cbPay As New ComboBox With {.DropDownStyle = ComboBoxStyle.DropDownList, .Width = 200, .Location = New Point(200, 22), .Font = futuraSmall}
                                             cbPay.Items.AddRange(New String() {"cash", "ATM"})
                                             grpPay.Controls.Add(lblPay)
                                             grpPay.Controls.Add(cbPay)

                                             Dim lblVoucher As New Label With {.Text = "Voucher Code:", .Location = New Point(15, 60), .AutoSize = True, .Font = futuraSmall}
                                             Dim txtVoucher As New TextBox With {.Width = 90, .Location = New Point(200, 57), .Font = futuraSmall}
                                             Dim btnCheckVoucher As New Button With {.Text = "Check", .Location = New Point(300, 55), .Width = 60, .Height = 25, .Font = futuraSmall}
                                             grpPay.Controls.Add(lblVoucher)
                                             grpPay.Controls.Add(txtVoucher)
                                             grpPay.Controls.Add(btnCheckVoucher)
                                             buyPanel.Controls.Add(grpPay)
                                             y += 60

                                             ' --- Voucher logic ---
                                             Dim discountPercent As Integer = 0
                                             Dim finalPrice As Decimal = price
                                             AddHandler btnCheckVoucher.Click, Sub()
                                                                                   Dim qtyBuy As Integer = Integer.Parse(txtQty.Text)
                                                                                   Dim subtotal As Decimal = price * qtyBuy
                                                                                   If String.IsNullOrWhiteSpace(txtVoucher.Text) Then
                                                                                       lblDiscount.Text = "Enter a voucher code."
                                                                                       lblFinal.Text = ""
                                                                                       Return
                                                                                   End If
                                                                                   Using connV As New MySqlConnection(connStr)
                                                                                       connV.Open()
                                                                                       Dim cmdV As New MySqlCommand("SELECT percent_sale, from_date, to_date, status FROM vouchers WHERE voucher_code=@code", connV)
                                                                                       cmdV.Parameters.AddWithValue("@code", txtVoucher.Text.Trim())
                                                                                       Using readerV = cmdV.ExecuteReader()
                                                                                           If readerV.Read() Then
                                                                                               Dim percent As Integer = Convert.ToInt32(readerV("percent_sale"))
                                                                                               Dim fromDate As Date = Convert.ToDateTime(readerV("from_date"))
                                                                                               Dim toDate As Date = Convert.ToDateTime(readerV("to_date"))
                                                                                               Dim statusV As String = readerV("status").ToString()
                                                                                               If statusV = "active" AndAlso Date.Now >= fromDate AndAlso Date.Now <= toDate Then
                                                                                                   discountPercent = percent
                                                                                                   Dim discountAmount As Decimal = subtotal * (percent / 100D)
                                                                                                   finalPrice = subtotal - discountAmount
                                                                                                   lblDiscount.Text = $"Voucher valid! {percent}% off (-₱{discountAmount:N2})"
                                                                                                   lblFinal.Text = $"Final Price: ₱{finalPrice:N2}"
                                                                                               Else
                                                                                                   discountPercent = 0
                                                                                                   finalPrice = subtotal
                                                                                                   lblDiscount.Text = "Voucher is not active or not valid for this date."
                                                                                                   lblFinal.Text = ""
                                                                                               End If
                                                                                           Else
                                                                                               discountPercent = 0
                                                                                               finalPrice = subtotal
                                                                                               lblDiscount.Text = "Voucher not found."
                                                                                               lblFinal.Text = ""
                                                                                           End If
                                                                                       End Using
                                                                                   End Using
                                                                               End Sub

                                             ' Confirm and Cancel buttons
                                             Dim btnConfirm As New Button With {
                    .Text = "Confirm Purchase",
                    .BackColor = Color.ForestGreen,
                    .ForeColor = Color.White,
                    .FlatStyle = FlatStyle.Flat,
                    .Width = 360,
                    .Height = 40,
                    .Location = New Point(20, 440),
                    .Font = futuraMed
                }
                                             buyPanel.Controls.Add(btnConfirm)

                                             Dim btnCancel As New Button With {
                    .Text = "Cancel",
                    .BackColor = Color.LightGray,
                    .ForeColor = Color.Black,
                    .FlatStyle = FlatStyle.Flat,
                    .Width = 360,
                    .Height = 40,
                    .Location = New Point(400, 440),
                    .Font = futuraMed
                }
                                             buyPanel.Controls.Add(btnCancel)

                                             home_flow.Controls.Add(buyPanel)
                                             Dim lblPriceBuy As New Label With {.Text = $"Subtotal: ₱{price:N2}", .Location = New Point(160, y), .AutoSize = True, .Font = futuraMed, .ForeColor = Color.Red}

                                             ' Quantity logic
                                             AddHandler btnPlus.Click, Sub()
                                                                           Dim currentQty As Integer = Integer.Parse(txtQty.Text)
                                                                           If currentQty < stock Then
                                                                               txtQty.Text = (currentQty + 1).ToString()
                                                                               lblPriceBuy.Text = $"Subtotal: ₱{price * (currentQty + 1):N2}"
                                                                               lblOrig.Text = $"Subtotal: ₱{price * (currentQty + 1):N2} ({currentQty + 1}x)"
                                                                           End If
                                                                       End Sub

                                             AddHandler btnMinus.Click, Sub()
                                                                            Dim currentQty As Integer = Integer.Parse(txtQty.Text)
                                                                            If currentQty > 1 Then
                                                                                txtQty.Text = (currentQty - 1).ToString()
                                                                                lblPriceBuy.Text = $"Subtotal: ₱{price * (currentQty - 1):N2}"
                                                                                lblOrig.Text = $"Subtotal: ₱{price * (currentQty - 1):N2} ({currentQty - 1}x)"
                                                                            End If
                                                                        End Sub

                                             ' Cancel logic
                                             AddHandler btnCancel.Click, Sub()
                                                                             LoadModels(branchId, brand)
                                                                             btnBack.Show()
                                                                         End Sub

                                             ' Confirm logic (insert into Orders, remove from stock, etc.)
                                             AddHandler btnConfirm.Click, Sub()
                                                                              Dim qtyBuy As Integer = Integer.Parse(txtQty.Text)
                                                                              If qtyBuy > stock Then
                                                                                  MessageBox.Show("Not enough stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                                  Return
                                                                              End If
                                                                              If cbPay.SelectedItem Is Nothing Then
                                                                                  MessageBox.Show("Please select a payment option.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                                  Return
                                                                              End If
                                                                              Dim paymentOpt As String = cbPay.SelectedItem.ToString()
                                                                              ' Handle voucher code properly - check if it's empty
                                                                              Dim voucherCode As String = Nothing
                                                                              If Not String.IsNullOrWhiteSpace(txtVoucher.Text) Then
                                                                                  voucherCode = txtVoucher.Text.Trim()
                                                                              End If
                                                                              Dim deliveryDate As Date = DateTime.Now.AddDays(5)
                                                                              Dim paymentStatus As String = "pending"
                                                                              ' Payment form will be shown after order creation
                                                                              Try
                                                                                  Using connOrder As New MySqlConnection(connStr)
                                                                                      connOrder.Open()

                                                                                      ' Validate voucher code if provided
                                                                                      If Not String.IsNullOrEmpty(voucherCode) Then
                                                                                          Dim cmdValidateVoucher As New MySqlCommand("SELECT COUNT(*) FROM vouchers WHERE voucher_code = @code", connOrder)
                                                                                          cmdValidateVoucher.Parameters.AddWithValue("@code", voucherCode)
                                                                                          Dim voucherExists As Integer = Convert.ToInt32(cmdValidateVoucher.ExecuteScalar())
                                                                                          If voucherExists = 0 Then
                                                                                              MessageBox.Show($"Voucher code '{voucherCode}' does not exist. Please enter a valid voucher code or leave it empty.", "Invalid Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                                              Return
                                                                                          End If
                                                                                      End If

                                                                                      Dim transaction As MySqlTransaction = connOrder.BeginTransaction() 'MP2 - Customer CRUD
                                                                                      Try
                                                                                          ' Calculate final price with voucher discount if applicable
                                                                                          Dim orderPrice As Decimal = price * qtyBuy
                                                                                          Dim finalOrderPrice As Decimal = orderPrice
                                                                                          If Not String.IsNullOrWhiteSpace(voucherCode) Then
                                                                                              Dim cmdVoucher As New MySqlCommand("SELECT percent_sale FROM vouchers WHERE voucher_code = @code AND status = 'active' AND CURDATE() BETWEEN from_date AND to_date", connOrder, transaction)
                                                                                              cmdVoucher.Parameters.AddWithValue("@code", voucherCode)
                                                                                              Dim voucherDiscount As Object = cmdVoucher.ExecuteScalar()
                                                                                              If voucherDiscount IsNot Nothing AndAlso Not IsDBNull(voucherDiscount) Then
                                                                                                  Dim discountValue As Integer = Convert.ToInt32(voucherDiscount)
                                                                                                  finalOrderPrice = orderPrice - (orderPrice * (discountValue / 100D))
                                                                                              End If
                                                                                          End If


                                                                                          ' Create the main order
                                                                                          Dim cmdInsert As New MySqlCommand("
INSERT INTO Orders (user_id, date_ordered, est_delivery,
    order_status, voucher_code, payment_status, payment_option)
VALUES (@user, NOW(), @deliv, 'pending', @voucher, @status, @pay)", connOrder, transaction)
                                                                                          cmdInsert.Parameters.AddWithValue("@user", currentUserId)
                                                                                          cmdInsert.Parameters.AddWithValue("@deliv", deliveryDate)
                                                                                          cmdInsert.Parameters.AddWithValue("@voucher", If(voucherCode Is Nothing, DBNull.Value, voucherCode))
                                                                                          cmdInsert.Parameters.AddWithValue("@pay", paymentOpt)
                                                                                          cmdInsert.Parameters.AddWithValue("@status", "pending")
                                                                                          cmdInsert.ExecuteNonQuery()

                                                                                          ' Get the order ID that was just created
                                                                                          Dim orderId As Integer = 0
                                                                                          Dim cmdGetOrder As New MySqlCommand("SELECT LAST_INSERT_ID()", connOrder, transaction)
                                                                                          orderId = Convert.ToInt32(cmdGetOrder.ExecuteScalar())

                                                                                          ' Insert into order_items table
                                                                                          Dim cmdInsertItem As New MySqlCommand("
INSERT INTO order_items (order_id, model_id, branch_id, quantity, unit_price)
VALUES (@orderId, @model, @branch, @qty, @unitPrice)", connOrder, transaction)
                                                                                          cmdInsertItem.Parameters.AddWithValue("@orderId", orderId)
                                                                                          cmdInsertItem.Parameters.AddWithValue("@model", thisModelId)
                                                                                          cmdInsertItem.Parameters.AddWithValue("@branch", branchId)
                                                                                          cmdInsertItem.Parameters.AddWithValue("@qty", qtyBuy)
                                                                                          cmdInsertItem.Parameters.AddWithValue("@unitPrice", price)
                                                                                          cmdInsertItem.ExecuteNonQuery()

                                                                                          ' Update stock
                                                                                          Dim updateStock As New MySqlCommand("UPDATE branch_model SET stock = stock - @qty WHERE branch_id = @branch AND model_id = @model", connOrder, transaction)
                                                                                          updateStock.Parameters.AddWithValue("@qty", qtyBuy)
                                                                                          updateStock.Parameters.AddWithValue("@branch", branchId)
                                                                                          updateStock.Parameters.AddWithValue("@model", thisModelId)
                                                                                          updateStock.ExecuteNonQuery()

                                                                                          transaction.Commit()

                                                                                          ' Show payment form after order is created
                                                                                          Dim paymentForm As New PaymentForm(connStr, orderId, finalOrderPrice, paymentOpt)
                                                                                          If paymentForm.ShowDialog() = DialogResult.OK Then
                                                                                              MessageBox.Show("Order placed and payment processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                                                          Else
                                                                                              MessageBox.Show("Order placed successfully! Payment was cancelled.", "Success with Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                                          End If
                                                                                      Catch ex As Exception
                                                                                          transaction.Rollback()
                                                                                          MessageBox.Show("Failed to place order: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                                      End Try
                                                                                  End Using
                                                                              Catch ex As Exception
                                                                                  MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                              End Try
                                                                          End Sub
                                         End Sub
            End While
        End Using
    End Sub

#End Region

#End Region

#Region "Form"
    Private Sub home_page_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        LoadBranchesToComboBox()
        home_panel.Show()
        CountStats()
        lblWelcome.Text = "WELCOME, " + currentUsername.ToUpper + "!"
        lblStatus.Text = "PROMO: " & promoMessages(currentMessageIndex)
        AddHandler messageTimer.Tick, AddressOf RotateMessages
        messageTimer.Start()
    End Sub

#End Region

#Region "Motorcycles"
    Private Sub LoadBranches()
        lblStatus.Text = "SELECT YOUR BRANCH:"
        btnBack.Hide()
        home_flow.Controls.Clear()
        navigationStack.Clear() ' Clear all history 

        Dim query As String = "SELECT branch_id, branch_name, address, phone_number, photo FROM branches"

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
                    Using cmdModels As New MySqlCommand("SELECT COUNT(*) FROM branch_model WHERE branch_id = @bid", conn2)
                        cmdModels.Parameters.AddWithValue("@bid", branchId)
                        modelCount = Convert.ToInt32(cmdModels.ExecuteScalar())
                    End Using
                    ' Count employees - only for admin/employee users
                    If currentUserRole = "admin" OrElse currentUserRole = "employee" Then
                        Using cmdEmp As New MySqlCommand("SELECT COUNT(*) FROM employees WHERE emp_id IN (SELECT emp_id FROM employment_records WHERE branch_id = @bid)", conn2)
                            cmdEmp.Parameters.AddWithValue("@bid", branchId)
                            empCount = Convert.ToInt32(cmdEmp.ExecuteScalar())
                        End Using
                    Else
                        empCount = 0 ' Customer users don't need to see employee count
                    End If
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

                ' Number of employees label - only show for admin/employee users
                If currentUserRole = "admin" OrElse currentUserRole = "employee" Then
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
                End If

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
        navigationStack.Push(Sub() LoadBranches())
        home_flow.Controls.Clear()

        Dim query As String = "SELECT m.brand, COUNT(*) AS model_count " &
                          "FROM models m " &
                          "JOIN branch_model bm ON m.model_id = bm.model_id " &
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

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim goBack = navigationStack.Pop()
        goBack.Invoke()
    End Sub

    Private Function GetImageFromBytes(bytes As Byte()) As Image
        Using ms As New IO.MemoryStream(bytes)
            Return Image.FromStream(ms)
        End Using
    End Function

#End Region

#Region "Design/asttik"
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
        End Using
    End Sub

    Private Sub lblLogOut_MouseEnter(sender As Object, e As EventArgs) Handles lblLogOut.MouseEnter
        lblLogOut.BackColor = Color.White
        lblLogOut.ForeColor = Color.Black
        lblLogOut.Font = New Font(lblLogOut.Font, FontStyle.Underline)
    End Sub

    Private Sub lblLogOut_MouseLeave(sender As Object, e As EventArgs) Handles lblLogOut.MouseLeave
        lblLogOut.BackColor = Color.Black
        lblLogOut.ForeColor = Color.White
        lblLogOut.Font = New Font(lblLogOut.Font, FontStyle.Regular)
    End Sub

    Private Sub lblHome_MouseEnter(sender As Object, e As EventArgs) Handles lblHome.MouseEnter
        lblHome.Font = New Font(lblHome.Font, FontStyle.Underline)
    End Sub

    Private Sub lblHome_MouseLeave(sender As Object, e As EventArgs) Handles lblHome.MouseLeave
        lblHome.Font = New Font(lblHome.Font, FontStyle.Regular)
    End Sub

    Private Sub lblMoto_MouseEnter(sender As Object, e As EventArgs) Handles lblMoto.MouseLeave
        lblMoto.Font = New Font(lblMoto.Font, FontStyle.Regular)
    End Sub

    Private Sub lblMoto_MouseHover(sender As Object, e As EventArgs) Handles lblMoto.MouseEnter
        lblMoto.Font = New Font(lblMoto.Font, FontStyle.Underline)
    End Sub

    Private Sub lblVoucher_MouseEnter(sender As Object, e As EventArgs) Handles lblVoucher.MouseEnter
        lblVoucher.Font = New Font(lblVoucher.Font, FontStyle.Underline)
    End Sub
    Private Sub lblVoucher_MouseLeave(sender As Object, e As EventArgs) Handles lblVoucher.MouseLeave
        lblVoucher.Font = New Font(lblVoucher.Font, FontStyle.Regular)
    End Sub

    Private Sub lblCart_MouseEnter(sender As Object, e As EventArgs) Handles lblCart.MouseEnter
        lblCart.Font = New Font(lblCart.Font, FontStyle.Underline)
    End Sub

    Private Sub lblCart_MouseLeave(sender As Object, e As EventArgs) Handles lblCart.MouseLeave
        lblCart.Font = New Font(lblCart.Font, FontStyle.Regular)
    End Sub
    Private Sub lblOrder_MouseEnter(sender As Object, e As EventArgs) Handles lblOrder.MouseEnter
        lblOrder.BackColor = Color.White
        lblOrder.ForeColor = Color.Black
        lblOrder.Font = New Font(lblOrder.Font, FontStyle.Underline)
    End Sub

    Private Sub lblOrder_MouseLeave(sender As Object, e As EventArgs) Handles lblOrder.MouseLeave
        lblOrder.BackColor = Color.Black
        lblOrder.ForeColor = Color.White
        lblOrder.Font = New Font(lblOrder.Font, FontStyle.Regular)
    End Sub

    Dim promoMessages As New List(Of String) From {
    "Get up to 0% off on selected models!",
    "Electric scooters now available!",
    "Visit our Quezon City branch today!",
    "Optional returns with every motorcycle purchase!",
    "Installment plans with 100% interest!",
    "Need help? Contact your selected branch!",
    "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z!"
}

    Dim currentMessageIndex As Integer = 0
    Dim messageTimer As New Timer With {.Interval = 10000}

    Private Sub RotateMessages(sender As Object, e As EventArgs)
        currentMessageIndex += 1
        If currentMessageIndex >= promoMessages.Count Then
            currentMessageIndex = 0
        End If
        lblStatus.Text = "PROMO: " & promoMessages(currentMessageIndex)
    End Sub

#End Region

#Region "Search"
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If cboBranch.SelectedIndex = -1 Then
            MessageBox.Show("Please select a branch.")
            Exit Sub
        End If

        If cboType.SelectedIndex = -1 Then
            MessageBox.Show("Please select a type.")
            Exit Sub
        End If

        If cboBrand.SelectedIndex = -1 Then
            MessageBox.Show("Please select a brand.")
            Exit Sub
        End If

        home_panel.Hide()
        home_flow.Show()
        messageTimer.Stop()

        Dim keyword As String = txtKeyword.Text.Trim()
        Dim branchId As Integer = CInt(cboBranch.SelectedValue)
        Dim selectedType As String = cboType.SelectedItem.ToString()
        Dim selectedBrand As String = cboBrand.SelectedItem.ToString()

        txtKeyword.Clear()
        cboBranch.SelectedIndex = -1
        cboType.SelectedIndex = -1
        cboBrand.SelectedIndex = -1
        LoadFilteredModels(keyword, branchId, selectedType, selectedBrand)
    End Sub

    Private Sub LoadTypesForBranch(branchId As Integer)
        Dim query As String = "SELECT DISTINCT m.type FROM models m JOIN branch_model bm ON m.model_id = bm.model_id WHERE bm.branch_id = @branchId"
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@branchId", branchId)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Dim types As New List(Of String)
            While reader.Read()
                types.Add(reader("type").ToString())
            End While
            cboType.DataSource = types
            cboType.SelectedIndex = -1
        End Using
    End Sub

    Private Sub cboType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboType.SelectedIndexChanged
        If cboType.SelectedIndex <> -1 AndAlso cboBranch.SelectedIndex <> -1 Then
            Dim branchId As Integer = CInt(cboBranch.SelectedValue)
            Dim selectedType As String = cboType.SelectedItem.ToString()
            LoadBrandsForBranchAndType(branchId, selectedType)
            cboBrand.Enabled = True
        Else
            cboBrand.DataSource = Nothing
            cboBrand.Enabled = False
        End If
    End Sub

    Private Sub LoadBrandsForBranchAndType(branchId As Integer, modelType As String)
        Dim query As String = "SELECT DISTINCT m.brand FROM models m JOIN branch_model bm ON m.model_id = bm.model_id WHERE bm.branch_id = @branchId AND m.type = @type"
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@branchId", branchId)
            cmd.Parameters.AddWithValue("@type", modelType)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Dim brands As New List(Of String)
            While reader.Read()
                brands.Add(reader("brand").ToString())
            End While
            cboBrand.DataSource = brands
            cboBrand.SelectedIndex = -1
        End Using
    End Sub

    Private Sub LoadBranchesToComboBox()
        Dim query As String = "SELECT branch_id, branch_name FROM branches"
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            With cboBranch
                .DisplayMember = "branch_name"
                .ValueMember = "branch_id"
                .DataSource = dt ' DataTable with branch_id and branch_name
                .SelectedIndex = -1
            End With
        End Using
    End Sub
    Private Sub LoadFilteredModels(keyword As String, branchId As Integer, type As String, brand As String)
        lblStatus.Text = "SEARCH RESULTS:"
        home_flow.Controls.Clear()
        btnBack.Show()
        navigationStack.Push(Sub() LoadFilteredModels(keyword, branchId, type, brand))

        Dim query As String = "SELECT m.model_id, m.model_name, m.photo, m.year_model, m.type, m.transmission, m.price, bm.stock, m.brand FROM models m JOIN branch_model bm ON m.model_id = bm.model_id WHERE bm.branch_id = @branchId AND m.type = @type AND m.brand = @brand AND m.model_name LIKE @keyword"

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@branchId", branchId)
            cmd.Parameters.AddWithValue("@type", type)
            cmd.Parameters.AddWithValue("@brand", brand)
            If Not String.IsNullOrWhiteSpace(keyword) Then
                cmd.Parameters.AddWithValue("@keyword", "%" & keyword & "%")
            Else
                cmd.Parameters.AddWithValue("@keyword", "%%")
            End If

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim thisModelId As Integer = CInt(reader("model_id"))
                Dim modelName As String = reader("model_name").ToString()
                Dim yearModel As String = reader("year_model").ToString()
                Dim modelType As String = reader("type").ToString()
                Dim transmission As String = reader("transmission").ToString()
                Dim price As Decimal = CDec(reader("price"))
                Dim stock As Integer = If(IsDBNull(reader("stock")), 0, CInt(reader("stock")))
                Dim photoBytes As Byte() = CType(reader("photo"), Byte())
                Dim brandName As String = reader("brand").ToString()

                ' --- Card Panel ---
                Dim card As New Panel With {
                    .Size = New Size(260, 400),
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
                    .ForeColor = Color.Black,
                    .Height = 22,
                    .TextAlign = ContentAlignment.MiddleCenter
                }
                Dim lblType As New Label With {
                    .Text = $"Type: {modelType}",
                    .Dock = DockStyle.Top,
                    .Font = New Font("Futura-Medium", 12, FontStyle.Italic),
                    .ForeColor = Color.Black,
                    .Height = 22,
                    .TextAlign = ContentAlignment.MiddleCenter
                }
                Dim lblTrans As New Label With {
                    .Text = $"Transmission: {transmission}",
                    .Dock = DockStyle.Top,
                    .Font = New Font("Futura-Medium", 12, FontStyle.Italic),
                    .ForeColor = Color.Black,
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

                ' --- Buy Now Button ---
                Dim btnBuy As New Button With {
                    .Text = "Buy Now",
                    .BackColor = Color.Red,
                    .ForeColor = Color.White,
                    .Dock = DockStyle.Bottom,
                    .Height = 38,
                    .Font = New Font("Futura-Medium", 13.8, FontStyle.Bold),
                    .FlatStyle = FlatStyle.Flat
                }

                ' --- Add to Cart Button ---
                Dim btnCart As New Button With {
                    .Text = "Add to Cart",
                    .BackColor = Color.Black,
                    .ForeColor = Color.White,
                    .Dock = DockStyle.Bottom,
                    .Height = 38,
                    .Font = New Font("Futura-Medium", 13.8, FontStyle.Bold),
                    .FlatStyle = FlatStyle.Flat
                }

                ' Add controls (bottom buttons first)
                card.Controls.Add(btnBuy)
                card.Controls.Add(btnCart)
                card.Controls.Add(lblStock)
                card.Controls.Add(lblPrice)
                card.Controls.Add(lblTrans)
                card.Controls.Add(lblType)
                card.Controls.Add(lblYear)
                card.Controls.Add(lblName)
                card.Controls.Add(pic)
                home_flow.Controls.Add(card)

                ' --- Add to Cart Handler ---
                AddHandler btnCart.Click, Sub()
                                              If stock <= 0 Then
                                                  MessageBox.Show("Sorry, this model is out of stock.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                  Return
                                              End If

                                              ' Show quantity selection dialog
                                              Dim quantityForm As New Form()
                                              quantityForm.Text = "Select Quantity"
                                              quantityForm.Size = New Size(300, 200)
                                              quantityForm.StartPosition = FormStartPosition.CenterParent
                                              quantityForm.FormBorderStyle = FormBorderStyle.FixedDialog
                                              quantityForm.MaximizeBox = False
                                              quantityForm.MinimizeBox = False

                                              Dim lblTitle As New Label With {
                                                  .Text = $"Add {modelName} to Cart",
                                                  .Font = New Font("Arial", 12, FontStyle.Bold),
                                                  .Location = New Point(20, 20),
                                                  .AutoSize = True
                                              }
                                              quantityForm.Controls.Add(lblTitle)

                                              Dim lblQty As New Label With {
                                                  .Text = "Quantity:",
                                                  .Font = New Font("Arial", 10),
                                                  .Location = New Point(20, 60),
                                                  .AutoSize = True
                                              }
                                              quantityForm.Controls.Add(lblQty)

                                              Dim numQty As New NumericUpDown With {
                                                  .Minimum = 1,
                                                  .Maximum = stock,
                                                  .Value = 1,
                                                  .Location = New Point(100, 58),
                                                  .Width = 80
                                              }
                                              quantityForm.Controls.Add(numQty)

                                              Dim lblStockInfo As New Label With {
                                                  .Text = $"Available: {stock}",
                                                  .Font = New Font("Arial", 9),
                                                  .Location = New Point(20, 85),
                                                  .AutoSize = True,
                                                  .ForeColor = Color.DarkGray
                                              }
                                              quantityForm.Controls.Add(lblStockInfo)

                                              Dim btnAdd As New Button With {
                                                  .Text = "Add to Cart",
                                                  .DialogResult = DialogResult.OK,
                                                  .Location = New Point(50, 120),
                                                  .Width = 80,
                                                  .BackColor = Color.Green,
                                                  .ForeColor = Color.White
                                              }
                                              quantityForm.Controls.Add(btnAdd)

                                              Dim btnCancel As New Button With {
                                                  .Text = "Cancel",
                                                  .DialogResult = DialogResult.Cancel,
                                                  .Location = New Point(150, 120),
                                                  .Width = 80
                                              }
                                              quantityForm.Controls.Add(btnCancel)

                                              If quantityForm.ShowDialog() = DialogResult.OK Then
                                                  Dim selectedQty As Integer = CInt(numQty.Value)

                                                  Using conn2 As New MySqlConnection(connStr)
                                                      conn2.Open()
                                                      Dim transaction As MySqlTransaction = conn2.BeginTransaction()
                                                      Try
                                                          ' Check if item already exists in cart
                                                          Dim checkCmd As New MySqlCommand("SELECT quantity FROM cart WHERE user_id = @uid AND model_id = @model AND branch_id = @branch FOR UPDATE", conn2, transaction)
                                                          checkCmd.Parameters.AddWithValue("@uid", currentUserId)
                                                          checkCmd.Parameters.AddWithValue("@model", thisModelId)
                                                          checkCmd.Parameters.AddWithValue("@branch", branchId)
                                                          Dim existingQty As Object = checkCmd.ExecuteScalar()

                                                          If existingQty IsNot Nothing Then
                                                              Dim currentQty As Integer = Convert.ToInt32(existingQty)
                                                              Dim newQty As Integer = currentQty + selectedQty
                                                              If newQty > stock Then
                                                                  MessageBox.Show($"Cannot add {selectedQty} more items. You already have {currentQty} in cart, and only {stock} are available.", "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                  transaction.Rollback()
                                                                  Return
                                                              End If

                                                              ' Update existing cart item
                                                              Dim updateCmd As New MySqlCommand("UPDATE Cart SET quantity = quantity + @qty WHERE user_id = @uid AND model_id = @model AND branch_id = @branch", conn2, transaction)
                                                              updateCmd.Parameters.AddWithValue("@qty", selectedQty)
                                                              updateCmd.Parameters.AddWithValue("@uid", currentUserId)
                                                              updateCmd.Parameters.AddWithValue("@model", thisModelId)
                                                              updateCmd.Parameters.AddWithValue("@branch", branchId)
                                                              updateCmd.ExecuteNonQuery()
                                                          Else
                                                              ' Insert new cart item
                                                              Dim insertCmd As New MySqlCommand("INSERT INTO Cart (user_id, model_id, branch_id, quantity) VALUES (@uid, @model, @branch, @qty)", conn2, transaction)
                                                              insertCmd.Parameters.AddWithValue("@uid", currentUserId)
                                                              insertCmd.Parameters.AddWithValue("@model", thisModelId)
                                                              insertCmd.Parameters.AddWithValue("@branch", branchId)
                                                              insertCmd.Parameters.AddWithValue("@qty", selectedQty)
                                                              insertCmd.ExecuteNonQuery()
                                                          End If

                                                          transaction.Commit()
                                                      Catch ex As Exception
                                                          transaction.Rollback()
                                                          MessageBox.Show("Error adding to cart: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                          Return
                                                      End Try
                                                  End Using
                                                  MessageBox.Show($"Added {selectedQty} item(s) to cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                              End If
                                          End Sub

                AddHandler btnBuy.Click, Sub()
                                             btnBack.Hide()
                                             If stock <= 0 Then
                                                 MessageBox.Show("Sorry, this model is out of stock.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                 Return
                                             End If

                                             ' --- Show Buy Panel ---
                                             home_flow.Controls.Clear()
                                             lblStatus.Text = "ORDERING..."

                                             Dim futuraBig As New Font("Futura-Medium", 18, FontStyle.Bold)
                                             Dim futuraMed As New Font("Futura-Medium", 14, FontStyle.Bold)
                                             Dim futuraSmall As New Font("Futura-Medium", 12, FontStyle.Regular)

                                             Dim buyPanel As New Panel With {.Width = 780, .Height = 600, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle}
                                             Dim y As Integer = 10

                                             ' Product Image
                                             Dim pbModel As New PictureBox With {
                    .Image = GetImageFromBytes(photoBytes),
                    .SizeMode = PictureBoxSizeMode.Zoom,
                    .Location = New Point(20, y),
                    .Size = New Size(120, 120),
                    .BorderStyle = BorderStyle.FixedSingle
                }
                                             buyPanel.Controls.Add(pbModel)

                                             Dim lblTitle As New Label With {.Text = modelName, .Font = futuraBig, .Location = New Point(160, y), .AutoSize = True}
                                             buyPanel.Controls.Add(lblTitle)
                                             y += 40

                                             Dim lblProductInfo As New Label With {.Text = $"{brand} | {type} | {transmission} | {yearModel}", .Location = New Point(160, y), .AutoSize = True, .ForeColor = Color.Black, .Font = futuraMed}
                                             buyPanel.Controls.Add(lblProductInfo)
                                             y += 40

                                             ' Stock
                                             Dim lblStockBuy As New Label With {.Text = $"Stock: {stock}", .Location = New Point(160, y), .AutoSize = True, .ForeColor = Color.Black, .Font = futuraSmall}
                                             buyPanel.Controls.Add(lblStockBuy)
                                             y += 30

                                             ' Quantity selector
                                             Dim lblQty As New Label With {.Text = "Quantity:", .Location = New Point(160, y), .AutoSize = True, .Font = futuraSmall}
                                             Dim btnMinus As New Button With {.Text = "-", .Location = New Point(240, y - 3), .Size = New Size(30, 30), .Font = futuraMed}
                                             Dim txtQty As New TextBox With {.Text = "1", .Location = New Point(275, y), .Size = New Size(40, 30), .Font = futuraMed, .TextAlign = HorizontalAlignment.Center, .ReadOnly = True}
                                             Dim btnPlus As New Button With {.Text = "+", .Location = New Point(320, y - 3), .Size = New Size(30, 30), .Font = futuraMed}
                                             buyPanel.Controls.Add(lblQty)
                                             buyPanel.Controls.Add(btnMinus)
                                             buyPanel.Controls.Add(txtQty)
                                             buyPanel.Controls.Add(btnPlus)
                                             y += 25

                                             ' User info
                                             Dim userName As String = "", userAddress As String = "", userFirst As String = "", userLast As String = "", userEmail As String = "", userPhone As String = ""
                                             Using connUser As New MySqlConnection(connStr)
                                                 connUser.Open()
                                                 Dim cmdUser As New MySqlCommand("SELECT username, address, first_name, last_name, email, phone_number FROM accounts WHERE user_id=@uid", connUser)
                                                 cmdUser.Parameters.AddWithValue("@uid", currentUserId)
                                                 Using readerU = cmdUser.ExecuteReader()
                                                     If readerU.Read() Then
                                                         userName = readerU("username").ToString()
                                                         userAddress = readerU("address").ToString()
                                                         userFirst = readerU("first_name").ToString()
                                                         userLast = readerU("last_name").ToString()
                                                         userEmail = readerU("email").ToString()
                                                         userPhone = readerU("phone_number").ToString()
                                                     End If
                                                 End Using
                                             End Using

                                             Dim grpUser As New GroupBox With {.Text = "Your Info", .Location = New Point(20, 160), .Size = New Size(740, 90), .Font = futuraMed}
                                             Dim lblUserInfo As New Label With {
                    .Text = $"Username: {userName}    Name: {userFirst} {userLast}" & vbCrLf &
                            $"Email: {userEmail}    Phone: {userPhone}" & vbCrLf &
                            $"Address: {userAddress}",
                    .Font = futuraSmall,
                    .Location = New Point(15, 25),
                    .AutoSize = True
                }
                                             grpUser.Controls.Add(lblUserInfo)
                                             buyPanel.Controls.Add(grpUser)
                                             y = 265

                                             ' --- Price Section ---
                                             Dim grpPrice As New GroupBox With {.Text = "Order Summary", .Location = New Point(20, y), .Size = New Size(740, 70), .Font = futuraMed}
                                             Dim lblOrig As New Label With {.Text = $"Subtotal: ₱{price:N2} (1x)", .Location = New Point(15, 25), .AutoSize = True, .Font = futuraSmall}
                                             Dim lblDiscount As New Label With {.Text = "", .Location = New Point(15, 45), .AutoSize = True, .ForeColor = Color.ForestGreen, .Font = futuraSmall}
                                             Dim lblFinal As New Label With {.Text = "", .Location = New Point(15, 65), .AutoSize = True, .Font = futuraMed}
                                             grpPrice.Controls.Add(lblOrig)
                                             grpPrice.Controls.Add(lblDiscount)
                                             grpPrice.Controls.Add(lblFinal)
                                             buyPanel.Controls.Add(grpPrice)
                                             y += 80

                                             ' --- Payment Section ---
                                             Dim grpPay As New GroupBox With {.Text = "Payment", .Location = New Point(20, y), .Size = New Size(740, 90), .Font = futuraMed}
                                             Dim lblPay As New Label With {.Text = "Payment Option:", .Location = New Point(15, 25), .AutoSize = True, .Font = futuraSmall}
                                             Dim cbPay As New ComboBox With {.DropDownStyle = ComboBoxStyle.DropDownList, .Width = 200, .Location = New Point(200, 22), .Font = futuraSmall}
                                             cbPay.Items.AddRange(New String() {"cash", "ATM"})
                                             grpPay.Controls.Add(lblPay)
                                             grpPay.Controls.Add(cbPay)

                                             Dim lblVoucher As New Label With {.Text = "Voucher Code:", .Location = New Point(15, 60), .AutoSize = True, .Font = futuraSmall}
                                             Dim txtVoucher As New TextBox With {.Width = 90, .Location = New Point(200, 57), .Font = futuraSmall}
                                             Dim btnCheckVoucher As New Button With {.Text = "Check", .Location = New Point(300, 55), .Width = 60, .Height = 25, .Font = futuraSmall}
                                             grpPay.Controls.Add(lblVoucher)
                                             grpPay.Controls.Add(txtVoucher)
                                             grpPay.Controls.Add(btnCheckVoucher)
                                             buyPanel.Controls.Add(grpPay)
                                             y += 60

                                             ' --- Voucher logic ---
                                             Dim discountPercent As Integer = 0
                                             Dim finalPrice As Decimal = price
                                             AddHandler btnCheckVoucher.Click, Sub()
                                                                                   Dim qtyBuy As Integer = Integer.Parse(txtQty.Text)
                                                                                   Dim subtotal As Decimal = price * qtyBuy
                                                                                   If String.IsNullOrWhiteSpace(txtVoucher.Text) Then
                                                                                       lblDiscount.Text = "Enter a voucher code."
                                                                                       lblFinal.Text = ""
                                                                                       Return
                                                                                   End If
                                                                                   Using connV As New MySqlConnection(connStr)
                                                                                       connV.Open()
                                                                                       Dim cmdV As New MySqlCommand("SELECT percent_sale, from_date, to_date, status FROM vouchers WHERE voucher_code=@code", connV)
                                                                                       cmdV.Parameters.AddWithValue("@code", txtVoucher.Text.Trim())
                                                                                       Using readerV = cmdV.ExecuteReader()
                                                                                           If readerV.Read() Then
                                                                                               Dim percent As Integer = Convert.ToInt32(readerV("percent_sale"))
                                                                                               Dim fromDate As Date = Convert.ToDateTime(readerV("from_date"))
                                                                                               Dim toDate As Date = Convert.ToDateTime(readerV("to_date"))
                                                                                               Dim statusV As String = readerV("status").ToString()
                                                                                               If statusV = "active" AndAlso Date.Now >= fromDate AndAlso Date.Now <= toDate Then
                                                                                                   discountPercent = percent
                                                                                                   Dim discountAmount As Decimal = subtotal * (percent / 100D)
                                                                                                   finalPrice = subtotal - discountAmount
                                                                                                   lblDiscount.Text = $"Voucher valid! {percent}% off (-₱{discountAmount:N2})"
                                                                                                   lblFinal.Text = $"Final Price: ₱{finalPrice:N2}"
                                                                                               Else
                                                                                                   discountPercent = 0
                                                                                                   finalPrice = subtotal
                                                                                                   lblDiscount.Text = "Voucher is not active or not valid for this date."
                                                                                                   lblFinal.Text = ""
                                                                                               End If
                                                                                           Else
                                                                                               discountPercent = 0
                                                                                               finalPrice = subtotal
                                                                                               lblDiscount.Text = "Voucher not found."
                                                                                               lblFinal.Text = ""
                                                                                           End If
                                                                                       End Using
                                                                                   End Using
                                                                               End Sub

                                             ' Confirm and Cancel buttons
                                             Dim btnConfirm As New Button With {
                    .Text = "Confirm Purchase",
                    .BackColor = Color.ForestGreen,
                    .ForeColor = Color.White,
                    .FlatStyle = FlatStyle.Flat,
                    .Width = 360,
                    .Height = 40,
                    .Location = New Point(20, 440),
                    .Font = futuraMed
                }
                                             buyPanel.Controls.Add(btnConfirm)

                                             Dim btnCancel As New Button With {
                    .Text = "Cancel",
                    .BackColor = Color.LightGray,
                    .ForeColor = Color.Black,
                    .FlatStyle = FlatStyle.Flat,
                    .Width = 360,
                    .Height = 40,
                    .Location = New Point(400, 440),
                    .Font = futuraMed
                }
                                             buyPanel.Controls.Add(btnCancel)

                                             home_flow.Controls.Add(buyPanel)
                                             Dim lblPriceBuy As New Label With {.Text = $"Subtotal: ₱{price:N2}", .Location = New Point(160, y), .AutoSize = True, .Font = futuraMed, .ForeColor = Color.Red}

                                             ' Quantity logic
                                             AddHandler btnPlus.Click, Sub()
                                                                           Dim currentQty As Integer = Integer.Parse(txtQty.Text)
                                                                           If currentQty < stock Then
                                                                               txtQty.Text = (currentQty + 1).ToString()
                                                                               lblPriceBuy.Text = $"Subtotal: ₱{price * (currentQty + 1):N2}"
                                                                               lblOrig.Text = $"Subtotal: ₱{price * (currentQty + 1):N2} ({currentQty + 1}x)"
                                                                           End If
                                                                       End Sub

                                             AddHandler btnMinus.Click, Sub()
                                                                            Dim currentQty As Integer = Integer.Parse(txtQty.Text)
                                                                            If currentQty > 1 Then
                                                                                txtQty.Text = (currentQty - 1).ToString()
                                                                                lblPriceBuy.Text = $"Subtotal: ₱{price * (currentQty - 1):N2}"
                                                                                lblOrig.Text = $"Subtotal: ₱{price * (currentQty - 1):N2} ({currentQty - 1}x)"
                                                                            End If
                                                                        End Sub

                                             ' Cancel logic
                                             AddHandler btnCancel.Click, Sub()
                                                                             LoadModels(branchId, brand)
                                                                             btnBack.Show()
                                                                         End Sub

                                             ' Confirm logic (insert into Orders, remove from stock, etc.)
                                             AddHandler btnConfirm.Click, Sub()
                                                                              Dim qtyBuy As Integer = Integer.Parse(txtQty.Text)
                                                                              If qtyBuy > stock Then
                                                                                  MessageBox.Show("Not enough stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                                  Return
                                                                              End If
                                                                              If cbPay.SelectedItem Is Nothing Then
                                                                                  MessageBox.Show("Please select a payment option.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                                  Return
                                                                              End If
                                                                              Dim paymentOpt As String = cbPay.SelectedItem.ToString()
                                                                              ' Handle voucher code properly - check if it's empty
                                                                              Dim voucherCode As String = Nothing
                                                                              If Not String.IsNullOrWhiteSpace(txtVoucher.Text) Then
                                                                                  voucherCode = txtVoucher.Text.Trim()
                                                                              End If
                                                                              Dim deliveryDate As Date = DateTime.Now.AddDays(5)
                                                                              Dim paymentStatus As String = "pending"
                                                                              ' Payment form will be shown after order creation
                                                                              Try
                                                                                  Using connOrder As New MySqlConnection(connStr)
                                                                                      connOrder.Open()

                                                                                      ' Validate voucher code if provided
                                                                                      If Not String.IsNullOrEmpty(voucherCode) Then
                                                                                          Dim cmdValidateVoucher As New MySqlCommand("SELECT COUNT(*) FROM vouchers WHERE voucher_code = @code", connOrder)
                                                                                          cmdValidateVoucher.Parameters.AddWithValue("@code", voucherCode)
                                                                                          Dim voucherExists As Integer = Convert.ToInt32(cmdValidateVoucher.ExecuteScalar())
                                                                                          If voucherExists = 0 Then
                                                                                              MessageBox.Show($"Voucher code '{voucherCode}' does not exist. Please enter a valid voucher code or leave it empty.", "Invalid Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                                              Return
                                                                                          End If
                                                                                      End If

                                                                                      Dim transaction As MySqlTransaction = connOrder.BeginTransaction()
                                                                                      Try
                                                                                          ' Calculate final price with voucher discount if applicable
                                                                                          Dim orderPrice As Decimal = price * qtyBuy
                                                                                          Dim finalOrderPrice As Decimal = orderPrice
                                                                                          If Not String.IsNullOrWhiteSpace(voucherCode) Then
                                                                                              Dim cmdVoucher As New MySqlCommand("SELECT percent_sale FROM vouchers WHERE voucher_code = @code AND status = 'active' AND CURDATE() BETWEEN from_date AND to_date", connOrder, transaction)
                                                                                              cmdVoucher.Parameters.AddWithValue("@code", voucherCode)
                                                                                              Dim voucherDiscount As Object = cmdVoucher.ExecuteScalar()
                                                                                              If voucherDiscount IsNot Nothing AndAlso Not IsDBNull(voucherDiscount) Then
                                                                                                  Dim discountValue As Integer = Convert.ToInt32(voucherDiscount)
                                                                                                  finalOrderPrice = orderPrice - (orderPrice * (discountValue / 100D))
                                                                                              End If
                                                                                          End If

                                                                                          ' Create the main order
                                                                                          Dim cmdInsert As New MySqlCommand("
INSERT INTO Orders (user_id, date_ordered, est_delivery,
    order_status, voucher_code, payment_status, payment_option)
VALUES (@user, NOW(), @deliv, 'pending', @voucher, @status, @pay)", connOrder, transaction)
                                                                                          cmdInsert.Parameters.AddWithValue("@user", currentUserId)
                                                                                          cmdInsert.Parameters.AddWithValue("@deliv", deliveryDate)
                                                                                          cmdInsert.Parameters.AddWithValue("@voucher", If(voucherCode Is Nothing, DBNull.Value, voucherCode))
                                                                                          cmdInsert.Parameters.AddWithValue("@pay", paymentOpt)
                                                                                          cmdInsert.Parameters.AddWithValue("@status", "pending")
                                                                                          cmdInsert.ExecuteNonQuery()

                                                                                          ' Get the order ID that was just created
                                                                                          Dim orderId As Integer = 0
                                                                                          Dim cmdGetOrder As New MySqlCommand("SELECT LAST_INSERT_ID()", connOrder, transaction)
                                                                                          orderId = Convert.ToInt32(cmdGetOrder.ExecuteScalar())

                                                                                          ' Insert into order_items table
                                                                                          Dim cmdInsertItem As New MySqlCommand("
INSERT INTO order_items (order_id, model_id, branch_id, quantity, unit_price)
VALUES (@orderId, @model, @branch, @qty, @unitPrice)", connOrder, transaction)
                                                                                          cmdInsertItem.Parameters.AddWithValue("@orderId", orderId)
                                                                                          cmdInsertItem.Parameters.AddWithValue("@model", thisModelId)
                                                                                          cmdInsertItem.Parameters.AddWithValue("@branch", branchId)
                                                                                          cmdInsertItem.Parameters.AddWithValue("@qty", qtyBuy)
                                                                                          cmdInsertItem.Parameters.AddWithValue("@unitPrice", price)
                                                                                          cmdInsertItem.ExecuteNonQuery()

                                                                                          ' Update stock
                                                                                          Dim updateStock As New MySqlCommand("UPDATE branch_model SET stock = stock - @qty WHERE branch_id = @branch AND model_id = @model", connOrder, transaction)
                                                                                          updateStock.Parameters.AddWithValue("@qty", qtyBuy)
                                                                                          updateStock.Parameters.AddWithValue("@branch", branchId)
                                                                                          updateStock.Parameters.AddWithValue("@model", thisModelId)
                                                                                          updateStock.ExecuteNonQuery()

                                                                                          transaction.Commit()

                                                                                          ' Show payment form after order is created
                                                                                          Dim paymentForm As New PaymentForm(connStr, orderId, finalOrderPrice, paymentOpt)
                                                                                          If paymentForm.ShowDialog() = DialogResult.OK Then
                                                                                              MessageBox.Show("Order placed and payment processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                                                          Else
                                                                                              MessageBox.Show("Order placed successfully! Payment was cancelled.", "Success with Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                                                          End If
                                                                                      Catch ex As Exception
                                                                                          transaction.Rollback()
                                                                                          MessageBox.Show("Failed to place order: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                                      End Try
                                                                                  End Using
                                                                              Catch ex As Exception
                                                                                  MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                              End Try
                                                                          End Sub
                                         End Sub
            End While
        End Using
    End Sub
    Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
        If cboBranch.SelectedIndex <> -1 Then
            Dim branchId As Integer = CInt(cboBranch.SelectedValue)
            LoadTypesForBranch(branchId)
            cboType.Enabled = True
        Else
            cboType.DataSource = Nothing
            cboType.Enabled = False
            cboBrand.DataSource = Nothing
            cboBrand.Enabled = False
        End If
    End Sub

    Private Sub lblStatus_Click(sender As Object, e As EventArgs) Handles lblStatus.Click

    End Sub





#End Region

End Class


Public Class CartItem
    Public Property ModelId As Integer
    Public Property BranchId As Integer
    Public Property Quantity As Integer
    Public Property UnitPrice As Decimal
    Public Property ModelName As String
    Public Property ModelType As String
    Public Property Transmission As String
    Public Property YearModel As String
    Public Property Stock As Integer
    Public Property PhotoBytes As Byte()
End Class
