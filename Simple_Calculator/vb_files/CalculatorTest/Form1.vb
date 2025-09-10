Imports System.CodeDom.Compiler

Public Class Form1

    Dim decFirst As Decimal
    Dim decSecond As Decimal
    Dim temp As Decimal
    Dim operation_select As Integer
    Dim blnOperation As Boolean = False
    Dim blnPoint As Boolean = False

    Dim blnPercent As Boolean = False

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        If lblResult.Text <> "0" Then
            lblResult.Text += "1"
        Else
            lblResult.Text = "1"
        End If
    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
        If lblResult.Text <> "0" Then
            lblResult.Text += "2"
        Else
            lblResult.Text = "2"
        End If
    End Sub

    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        If lblResult.Text <> "0" Then
            lblResult.Text += "3"
        Else
            lblResult.Text = "3"
        End If
    End Sub

    Private Sub btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click
        If lblResult.Text <> "0" Then
            lblResult.Text += "4"
        Else
            lblResult.Text = "4"
        End If
    End Sub

    Private Sub btn5_Click(sender As Object, e As EventArgs) Handles btn5.Click
        If lblResult.Text <> "0" Then
            lblResult.Text += "5"
        Else
            lblResult.Text = "5"
        End If
    End Sub

    Private Sub btn6_Click(sender As Object, e As EventArgs) Handles btn6.Click
        If lblResult.Text <> "0" Then
            lblResult.Text += "6"
        Else
            lblResult.Text = "6"
        End If
    End Sub

    Private Sub btn7_Click(sender As Object, e As EventArgs) Handles btn7.Click
        If lblResult.Text <> "0" Then
            lblResult.Text += "7"
        Else
            lblResult.Text = "7"
        End If
    End Sub

    Private Sub btn8_Click(sender As Object, e As EventArgs) Handles btn8.Click
        If lblResult.Text <> "0" Then
            lblResult.Text += "8"
        Else
            lblResult.Text = "8"
        End If
    End Sub
    Private Sub btn9_Click(sender As Object, e As EventArgs) Handles btn9.Click
        If lblResult.Text <> "0" Then
            lblResult.Text += "9"
        Else
            lblResult.Text = "9"
        End If
    End Sub

    Private Sub btnAC_Click(sender As Object, e As EventArgs) Handles btnAC.Click
        lblResult.Text = "0"
        blnPoint = False
        blnOperation = False
        blnPercent = False
    End Sub

    Private Sub btn0_Click(sender As Object, e As EventArgs) Handles btn0.Click
        If lblResult.Text <> "0" Then
            lblResult.Text += "0"
        End If

    End Sub

    Private Sub btn00_Click(sender As Object, e As EventArgs) Handles btn00.Click
        If lblResult.Text <> "0" Then
            lblResult.Text += "00"
        End If
    End Sub

    Private Sub btnDecimal_Click(sender As Object, e As EventArgs) Handles btnDecimal.Click
        If blnPoint = False Then
            lblResult.Text += "."
            blnPoint = True
        Else
            If blnPercent = False Then
                lblResult.Text = CInt(lblResult.Text)
                blnPoint = False
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If blnOperation = False Then
            decFirst = Val(lblResult.Text)
            lblResult.Text = "0"
            operation_select = 1
            blnOperation = True
            blnPoint = False
            blnPercent = False
        End If
    End Sub

    Private Sub btnSubtract_Click(sender As Object, e As EventArgs) Handles btnSubtract.Click
        If blnOperation = False Then
            decFirst = Val(lblResult.Text)
            lblResult.Text = "0"
            operation_select = 2
            blnOperation = True
            blnPoint = False
            blnPercent = False
        End If
    End Sub

    Private Sub btnMultiply_Click(sender As Object, e As EventArgs) Handles btnMultiply.Click
        If blnOperation = False Then
            decFirst = Val(lblResult.Text)
            lblResult.Text = "0"
            operation_select = 3
            blnOperation = True
            blnPoint = False
            blnPercent = False
        End If
    End Sub

    Private Sub btnDivide_Click(sender As Object, e As EventArgs) Handles btnDivide.Click
        If blnOperation = False Then
            decFirst = Val(lblResult.Text)
            lblResult.Text = "0"
            operation_select = 4
            blnOperation = True
            blnPoint = False
            blnPercent = False
        End If
    End Sub

    Private Sub btnTotal_Click(sender As Object, e As EventArgs) Handles btnTotal.Click

        If blnOperation = True Then
            decSecond = Val(lblResult.Text)
            Select Case operation_select
                Case 1
                    lblResult.Text = decFirst + decSecond
                Case 2
                    lblResult.Text = decFirst - decSecond
                Case 3
                    lblResult.Text = decFirst * decSecond
                Case 4
                    If decSecond <> 0 Then
                        lblResult.Text = decFirst / decSecond
                    End If
            End Select

            operation_select = 0
            blnOperation = False
            blnPoint = False
            blnPercent = False
        End If

    End Sub

    Private Sub btnSign_Click(sender As Object, e As EventArgs) Handles btnSign.Click

        If CInt(lblResult.Text) > 0 Then
            temp = lblResult.Text
            lblResult.Text = "-" & lblResult.Text
        Else
            lblResult.Text = CInt(lblResult.Text) * -1
        End If

    End Sub

    Private Sub btnPercent_Click(sender As Object, e As EventArgs) Handles btnPercent.Click


        If blnPercent = False Then
            temp = CDec(lblResult.Text)
            lblResult.Text = lblResult.Text & "%"
            blnPercent = True
            blnPoint = True
        Else
            lblResult.Text = temp
            blnPercent = False
        End If


    End Sub
End Class
