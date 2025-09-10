Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO

Public Class SimpleReceiptGenerator
    Private connStr As String

    Public Sub New(connectionString As String)
        connStr = connectionString
    End Sub

    Public Sub GenerateTextReceipt(orderId As Integer, savePath As String)
        'CHECKER NANAMAN
        Try
            Dim orderData As DataTable = GetOrderData(orderId)

            If orderData.Rows.Count = 0 Then
                MessageBox.Show("Order not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim receiptContent As String = CreateReceiptContent(orderData)

            File.WriteAllText(savePath, receiptContent)

            MessageBox.Show($"Receipt saved as: {savePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error generating receipt: " & ex.Message, "Receipt Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ShowTextReceipt(orderId As Integer)
        Try
            Dim orderData As DataTable = GetOrderData(orderId) '2

            If orderData.Rows.Count = 0 Then
                MessageBox.Show("Order not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim receiptContent As String = CreateReceiptContent(orderData) 'JUMPS TO FORM (AI CODE)

            Dim receiptForm As New Form()
            receiptForm.Text = "Order Receipt - Order #" & orderId
            receiptForm.Size = New Size(500, 600)
            receiptForm.StartPosition = FormStartPosition.CenterScreen

            Dim textBox As New TextBox()
            textBox.Multiline = True
            textBox.ScrollBars = ScrollBars.Vertical
            textBox.Dock = DockStyle.Fill
            textBox.Font = New Font("Courier New", 10)
            textBox.Text = receiptContent
            textBox.ReadOnly = True

            receiptForm.Controls.Add(textBox)
            receiptForm.Show()

        Catch ex As Exception
            MessageBox.Show("Error showing receipt: " & ex.Message, "Receipt Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'MYSQL QUERY!!!!
    Private Function GetOrderData(orderId As Integer) As DataTable 'TERMTEST 
        Dim dt As New DataTable()

        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Dim query As String = "
                SELECT 
                    o.order_id, o.date_ordered, o.est_delivery, o.order_status, 
                    o.payment_status, o.payment_option, o.final_price,
                    o.voucher_code, oi.quantity, oi.unit_price,
                    m.model_name, m.brand, m.price as model_price,
                    b.branch_name, b.address as branch_address, b.phone_number,
                    a.first_name, a.last_name, a.username
                FROM Orders o
                JOIN order_items oi ON o.order_id = oi.order_id
                JOIN Models m ON oi.model_id = m.model_id
                JOIN Branches b ON oi.branch_id = b.branch_id
                JOIN accounts a ON o.user_id = a.user_id
                WHERE o.order_id = @orderId
                ORDER BY oi.order_item_id"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@orderId", orderId)

            Dim adapter As New MySqlDataAdapter(cmd)
            adapter.Fill(dt)
        End Using

        Return dt
    End Function

    Private Function CreateReceiptContent(orderData As DataTable) As String
        'PRE, ITO YUNG CONTENT NG RECEIPT

        Dim receipt As String = ""

        ' Header
        receipt &= "=".PadRight(50, "=") & vbCrLf
        receipt &= "           MOTOCICLO MOTORCYCLE SHOP" & vbCrLf
        receipt &= "=".PadRight(50, "=") & vbCrLf
        receipt &= vbCrLf

        ' Get first row for order details
        Dim firstRow As DataRow = orderData.Rows(0)

        ' Receipt Details
        receipt &= $"Receipt #: {firstRow("order_id")}" & vbCrLf
        receipt &= $"Date: {Convert.ToDateTime(firstRow("date_ordered")).ToString("MMM dd, yyyy")}" & vbCrLf
        receipt &= $"Time: {Convert.ToDateTime(firstRow("date_ordered")).ToString("HH:mm:ss")}" & vbCrLf
        receipt &= vbCrLf

        ' Customer Info
        receipt &= "CUSTOMER INFORMATION:" & vbCrLf
        receipt &= $"-".PadRight(30, "-") & vbCrLf
        receipt &= $"Name: {firstRow("first_name")} {firstRow("last_name")}" & vbCrLf
        receipt &= $"Username: {firstRow("username")}" & vbCrLf
        receipt &= vbCrLf

        ' Branch Info
        receipt &= "BRANCH INFORMATION:" & vbCrLf
        receipt &= $"-".PadRight(30, "-") & vbCrLf
        receipt &= $"Branch: {firstRow("branch_name")}" & vbCrLf
        receipt &= $"Address: {firstRow("branch_address")}" & vbCrLf
        receipt &= $"Phone: {firstRow("phone_number")}" & vbCrLf
        receipt &= vbCrLf

        ' Order Details
        receipt &= "ORDER DETAILS:" & vbCrLf
        receipt &= $"-".PadRight(30, "-") & vbCrLf

        Dim totalSubtotal As Decimal = 0
        Dim itemCount As Integer = 1

        ' Loop through all items
        For Each row As DataRow In orderData.Rows
            receipt &= $"Item {itemCount}:" & vbCrLf
            receipt &= $"  Model: {row("model_name")}" & vbCrLf
            receipt &= $"  Brand: {row("brand")}" & vbCrLf
            receipt &= $"  Quantity: {row("quantity")}" & vbCrLf
            receipt &= $"  Unit Price: ₱{Convert.ToDecimal(row("unit_price")).ToString("N2")}" & vbCrLf

            ' Calculate subtotal for this item
            Dim itemSubtotal As Decimal = Convert.ToDecimal(row("unit_price")) * Convert.ToInt32(row("quantity"))
            totalSubtotal += itemSubtotal
            receipt &= $"  Subtotal: ₱{itemSubtotal.ToString("N2")}" & vbCrLf
            receipt &= vbCrLf
            itemCount += 1
        Next

        ' Total before discount
        receipt &= $"TOTAL BEFORE DISCOUNT: ₱{totalSubtotal.ToString("N2")}" & vbCrLf

        ' Voucher discount
        If Not IsDBNull(firstRow("voucher_code")) AndAlso Not String.IsNullOrEmpty(firstRow("voucher_code").ToString()) Then
            receipt &= $"Voucher Code: {firstRow("voucher_code")}" & vbCrLf
            Dim discount As Decimal = totalSubtotal - Convert.ToDecimal(firstRow("final_price"))
            receipt &= $"Discount: ₱{discount.ToString("N2")}" & vbCrLf
        End If

        receipt &= vbCrLf

        ' Final Total
        receipt &= $"=".PadRight(30, "=") & vbCrLf
        receipt &= $"FINAL TOTAL: ₱{Convert.ToDecimal(firstRow("final_price")).ToString("N2")}" & vbCrLf
        receipt &= $"=".PadRight(30, "=") & vbCrLf
        receipt &= vbCrLf

        ' Payment Info
        receipt &= "PAYMENT INFORMATION:" & vbCrLf
        receipt &= $"-".PadRight(30, "-") & vbCrLf
        receipt &= $"Payment Method: {firstRow("payment_option")}" & vbCrLf
        receipt &= $"Payment Status: {firstRow("payment_status")}" & vbCrLf
        receipt &= $"Order Status: {firstRow("order_status")}" & vbCrLf
        receipt &= $"Estimated Delivery: {Convert.ToDateTime(firstRow("est_delivery")).ToString("MMM dd, yyyy")}" & vbCrLf
        receipt &= vbCrLf

        ' Footer
        receipt &= "Thank you for your purchase!" & vbCrLf
        receipt &= "Please keep this receipt for your records." & vbCrLf
        receipt &= vbCrLf
        receipt &= "=".PadRight(50, "=") & vbCrLf

        Return receipt
    End Function

    Public Sub PrintTextReceipt(orderId As Integer)
        Try
            Dim orderData As DataTable = GetOrderData(orderId)

            If orderData.Rows.Count = 0 Then
                MessageBox.Show("Order not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim receiptContent As String = CreateReceiptContent(orderData)

            Dim printDialog As New PrintDialog() 'AI CODE  - COMPLEX NA MASYADO, PRE
            If printDialog.ShowDialog() = DialogResult.OK Then
                Dim printDocument As New Printing.PrintDocument()
                AddHandler printDocument.PrintPage, Sub(sender As Object, e As Printing.PrintPageEventArgs)
                                                        Dim font As New Font("Courier New", 10)
                                                        Dim brush As New SolidBrush(Color.Black)
                                                        Dim lines() As String = receiptContent.Split(vbCrLf)
                                                        Dim y As Single = 10

                                                        For Each line As String In lines
                                                            e.Graphics.DrawString(line, font, brush, 10, y)
                                                            y += font.GetHeight()

                                                            If y > e.PageBounds.Height - 50 Then
                                                                e.HasMorePages = True
                                                                Return
                                                            End If
                                                        Next
                                                    End Sub

                printDocument.Print()
            End If

        Catch ex As Exception
            MessageBox.Show("Error printing receipt: " & ex.Message, "Receipt Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub GenerateTextReceiptWithPayment(orderId As Integer, savePath As String, amountPaid As Decimal, change As Decimal)
        Try
            Dim orderData As DataTable = GetOrderData(orderId)

            If orderData.Rows.Count = 0 Then
                MessageBox.Show("Order not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim receiptContent As String = CreateReceiptContentWithPayment(orderData, amountPaid, change)

            File.WriteAllText(savePath, receiptContent)

            MessageBox.Show($"Receipt saved as: {savePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error generating receipt: " & ex.Message, "Receipt Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ShowTextReceiptWithPayment(orderId As Integer, amountPaid As Decimal, change As Decimal)
        'CHECKER NATIN
        Try
            Dim orderData As DataTable = GetOrderData(orderId)

            If orderData.Rows.Count = 0 Then
                MessageBox.Show("Order not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim receiptContent As String = CreateReceiptContentWithPayment(orderData, amountPaid, change)

            Dim receiptForm As New Form With {
                .Text = $"Receipt - Order #{orderId}",
                .Size = New Size(600, 700),
                .StartPosition = FormStartPosition.CenterParent,
                .FormBorderStyle = FormBorderStyle.FixedDialog,
                .MaximizeBox = False
            }

            Dim txtReceipt As New TextBox With {
                .Multiline = True,
                .ScrollBars = ScrollBars.Vertical,
                .ReadOnly = True,
                .Font = New Font("Courier New", 10),
                .Dock = DockStyle.Fill,
                .Text = receiptContent
            }

            receiptForm.Controls.Add(txtReceipt)
            receiptForm.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Error showing receipt: " & ex.Message, "Receipt Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function CreateReceiptContentWithPayment(orderData As DataTable, amountPaid As Decimal, change As Decimal) As String
        'PRE, ITO YUNG CONTENT NG RECEIPT

        Dim receipt As String = ""

        ' Header
        receipt &= "=".PadRight(50, "=") & vbCrLf
        receipt &= "           MOTOCICLO MOTORCYCLE SHOP" & vbCrLf
        receipt &= "=".PadRight(50, "=") & vbCrLf
        receipt &= vbCrLf

        ' Get first row for order details
        Dim firstRow As DataRow = orderData.Rows(0)

        ' Receipt Details
        receipt &= $"Receipt #: {firstRow("order_id")}" & vbCrLf
        receipt &= $"Date: {Convert.ToDateTime(firstRow("date_ordered")).ToString("MMM dd, yyyy")}" & vbCrLf
        receipt &= $"Time: {Convert.ToDateTime(firstRow("date_ordered")).ToString("HH:mm:ss")}" & vbCrLf
        receipt &= vbCrLf
        
        ' Customer Info
        receipt &= "CUSTOMER INFORMATION:" & vbCrLf
        receipt &= $"-".PadRight(30, "-") & vbCrLf
        receipt &= $"Name: {firstRow("first_name")} {firstRow("last_name")}" & vbCrLf
        receipt &= $"Username: {firstRow("username")}" & vbCrLf
        receipt &= vbCrLf
        
        ' Branch Info
        receipt &= "BRANCH INFORMATION:" & vbCrLf
        receipt &= $"-".PadRight(30, "-") & vbCrLf
        receipt &= $"Branch: {firstRow("branch_name")}" & vbCrLf
        receipt &= $"Address: {firstRow("branch_address")}" & vbCrLf
        receipt &= $"Phone: {firstRow("phone_number")}" & vbCrLf
        receipt &= vbCrLf
        
        ' Order Details
        receipt &= "ORDER DETAILS:" & vbCrLf
        receipt &= $"-".PadRight(30, "-") & vbCrLf

        Dim totalSubtotal As Decimal = 0
        Dim itemCount As Integer = 1

        ' Loop through all items
        For Each row As DataRow In orderData.Rows
            receipt &= $"Item {itemCount}:" & vbCrLf
            receipt &= $"  Model: {row("model_name")}" & vbCrLf
            receipt &= $"  Brand: {row("brand")}" & vbCrLf
            receipt &= $"  Quantity: {row("quantity")}" & vbCrLf
            receipt &= $"  Unit Price: ₱{Convert.ToDecimal(row("unit_price")).ToString("N2")}" & vbCrLf

            ' Calculate subtotal for this item
            Dim itemSubtotal As Decimal = Convert.ToDecimal(row("unit_price")) * Convert.ToInt32(row("quantity"))
            totalSubtotal += itemSubtotal
            receipt &= $"  Subtotal: ₱{itemSubtotal.ToString("N2")}" & vbCrLf
            receipt &= vbCrLf
            itemCount += 1
        Next

        ' Total before discount
        receipt &= $"TOTAL BEFORE DISCOUNT: ₱{totalSubtotal.ToString("N2")}" & vbCrLf
        
        ' Voucher discount
        If Not IsDBNull(firstRow("voucher_code")) AndAlso Not String.IsNullOrEmpty(firstRow("voucher_code").ToString()) Then
            receipt &= $"Voucher Code: {firstRow("voucher_code")}" & vbCrLf
            Dim discount As Decimal = totalSubtotal - Convert.ToDecimal(firstRow("final_price"))
            receipt &= $"Discount: ₱{discount.ToString("N2")}" & vbCrLf
        End If
        
        receipt &= vbCrLf
        
        ' Final Total
        receipt &= $"=".PadRight(30, "=") & vbCrLf
        receipt &= $"FINAL TOTAL: ₱{Convert.ToDecimal(firstRow("final_price")).ToString("N2")}" & vbCrLf
        receipt &= $"=".PadRight(30, "=") & vbCrLf
        receipt &= vbCrLf
        
        ' Payment Information
        receipt &= "PAYMENT INFORMATION:" & vbCrLf
        receipt &= $"-".PadRight(30, "-") & vbCrLf
        receipt &= $"Payment Method: {firstRow("payment_option")}" & vbCrLf
        receipt &= $"Payment Status: {firstRow("payment_status")}" & vbCrLf
        receipt &= $"Amount Paid: ₱{amountPaid.ToString("N2")}" & vbCrLf
        receipt &= $"Change: ₱{change.ToString("N2")}" & vbCrLf
        receipt &= $"Order Status: {firstRow("order_status")}" & vbCrLf
        receipt &= $"Estimated Delivery: {Convert.ToDateTime(firstRow("est_delivery")).ToString("MMM dd, yyyy")}" & vbCrLf
        receipt &= vbCrLf
        
        ' Footer
        receipt &= "Thank you for your purchase!" & vbCrLf
        receipt &= "Please keep this receipt for your records." & vbCrLf
        receipt &= vbCrLf
        receipt &= "=".PadRight(50, "=") & vbCrLf
        
        Return receipt
    End Function
End Class 