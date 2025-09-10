Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Drawing

Public Class PaymentForm
    Inherits Form

    Private connStr As String
    Private orderId As Integer
    Private totalAmount As Decimal
    Private paymentMethod As String
    Private amountPaid As Decimal = 0
    Private change As Decimal = 0

    Public Sub New(connectionString As String, orderIdParam As Integer, amount As Decimal, paymentMethodParam As String)
        connStr = connectionString
        orderId = orderIdParam
        totalAmount = amount
        paymentMethod = paymentMethodParam
        InitializeForm()
    End Sub

    Private Sub InitializeForm()

        Me.Text = "Payment - Order #" & If(orderId > 0, orderId.ToString(), "New Order")
        Me.Size = New Size(400, 400)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        CreateControls()
    End Sub

    Private Sub CreateControls()
        Dim lblTitle As New Label With {
            .Text = "PAYMENT DETAILS",
            .Font = New Font("Arial", 16, FontStyle.Bold),
            .Location = New Point(20, 20),
            .AutoSize = True,
            .ForeColor = Color.DarkBlue
        }
        Me.Controls.Add(lblTitle)

        Dim lblOrderId As New Label With {
            .Text = If(orderId > 0, "Order #: " & orderId, "New Order"),
            .Font = New Font("Arial", 12),
            .Location = New Point(20, 60),
            .AutoSize = True
        }
        Me.Controls.Add(lblOrderId)

        Dim lblTotal As New Label With {
            .Text = "Total Amount: ₱" & totalAmount.ToString("N2"),
            .Font = New Font("Arial", 14, FontStyle.Bold),
            .Location = New Point(20, 90),
            .AutoSize = True,
            .ForeColor = Color.DarkGreen
        }
        Me.Controls.Add(lblTotal)

        Dim lblPaymentMethod As New Label With {
            .Text = "Payment Method: " & paymentMethod,
            .Font = New Font("Arial", 12),
            .Location = New Point(20, 120),
            .AutoSize = True
        }
        Me.Controls.Add(lblPaymentMethod)

        Dim lblAmountPaid As New Label With {
            .Text = "Amount Paid:",
            .Font = New Font("Arial", 12, FontStyle.Bold),
            .Location = New Point(20, 160),
            .AutoSize = True
        }
        Me.Controls.Add(lblAmountPaid)

        Dim txtAmountPaid As New TextBox With {
            .Name = "txtAmountPaid",
            .Location = New Point(20, 185),
            .Size = New Size(200, 25),
            .Font = New Font("Arial", 12),
            .Text = totalAmount.ToString("N2")
        }
        Me.Controls.Add(txtAmountPaid)

        Dim lblChange As New Label With {
            .Text = "Change:",
            .Font = New Font("Arial", 12, FontStyle.Bold),
            .Location = New Point(20, 220),
            .AutoSize = True
        }
        Me.Controls.Add(lblChange)

        Dim lblChangeAmount As New Label With {
            .Name = "lblChangeAmount",
            .Text = "₱0.00",
            .Font = New Font("Arial", 14, FontStyle.Bold),
            .Location = New Point(20, 245),
            .AutoSize = True,
            .ForeColor = Color.DarkBlue
        }
        Me.Controls.Add(lblChangeAmount)

        Dim btnProcessPayment As New Button With {
            .Text = "Process Payment",
            .Location = New Point(20, 290),
            .Size = New Size(150, 40),
            .Font = New Font("Arial", 12, FontStyle.Bold),
            .BackColor = Color.Green,
            .ForeColor = Color.White
        }
        AddHandler btnProcessPayment.Click, AddressOf ProcessPayment
        Me.Controls.Add(btnProcessPayment)

        Dim btnCancel As New Button With {
            .Text = "Cancel",
            .Location = New Point(190, 290),
            .Size = New Size(100, 40),
            .Font = New Font("Arial", 12),
            .BackColor = Color.LightGray
        }
        AddHandler btnCancel.Click, AddressOf CancelPayment
        Me.Controls.Add(btnCancel)

        AddHandler txtAmountPaid.TextChanged, AddressOf AmountPaidChanged
        AddHandler txtAmountPaid.KeyPress, AddressOf AmountPaidKeyPress

        txtAmountPaid.Focus()
        txtAmountPaid.SelectAll()
    End Sub

    Private Sub AmountPaidKeyPress(sender As Object, e As KeyPressEventArgs)

        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        If e.KeyChar = "."c AndAlso DirectCast(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub AmountPaidChanged(sender As Object, e As EventArgs)
        CalculateChange()
    End Sub

    Private Sub CalculateChange()
        Try
            Dim txtAmountPaid As TextBox = DirectCast(Me.Controls("txtAmountPaid"), TextBox)
            Dim lblChangeAmount As Label = DirectCast(Me.Controls("lblChangeAmount"), Label)

            Dim amountPaid As Decimal
            If Decimal.TryParse(txtAmountPaid.Text, amountPaid) Then
                Dim change As Decimal = amountPaid - totalAmount
                lblChangeAmount.Text = "₱" & change.ToString("N2")

                If change < 0 Then
                    lblChangeAmount.ForeColor = Color.Red
                    lblChangeAmount.Text = "₱" & change.ToString("N2") & " (Insufficient)"
                Else
                    lblChangeAmount.ForeColor = Color.DarkBlue
                End If
            Else
                lblChangeAmount.Text = "₱0.00"
                lblChangeAmount.ForeColor = Color.DarkBlue
            End If
        Catch ex As Exception
            MessageBox.Show("ERROR")
        End Try
    End Sub

    Private Sub ProcessPayment()
        Try
            Dim txtAmountPaid As TextBox = DirectCast(Me.Controls("txtAmountPaid"), TextBox)

            Dim amountPaid As Decimal
            If Not Decimal.TryParse(txtAmountPaid.Text, amountPaid) Then
                MessageBox.Show("Please enter a valid amount.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If amountPaid < totalAmount Then
                MessageBox.Show("Amount paid is less than the total amount. Please enter sufficient payment.", "Insufficient Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If


            Me.amountPaid = amountPaid
            Me.change = amountPaid - totalAmount

            Dim message As String = $"Payment processed successfully!" & vbCrLf & vbCrLf
            message &= $"Amount Paid: ₱{amountPaid:N2}" & vbCrLf
            message &= $"Total Amount: ₱{totalAmount:N2}" & vbCrLf
            message &= $"Change: ₱{change:N2}"

            MessageBox.Show(message, "Payment Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetPaymentResult() As (Success As Boolean, AmountPaid As Decimal, Change As Decimal)
        Return (DialogResult = DialogResult.OK, amountPaid, change)
    End Function

    Private Sub CancelPayment()
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class