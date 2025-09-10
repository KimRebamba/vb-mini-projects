Imports System.Threading.Tasks.Dataflow

Public Class Form1

    Dim bLoop As Boolean = False
    Dim pRedPen As Decimal = 9.3
    Dim pBluePen As Decimal = 9.3
    Dim pRuler As Decimal = 30
    Dim pNotebook As Decimal = 40
    Dim pPencil As Decimal = 8.0

    Dim pFolder As Decimal = 6.5
    Dim pEraser As Decimal = 5.0
    Dim pBag As Decimal = 190.99

    Dim pToPay As Decimal = 0
    Dim pPayment As Decimal = 0
    Dim pChange As Decimal = 0

    Dim Qbluepen As Short = 0
    Dim Qredpen As Short = 0
    Dim Qruler As Short = 0
    Dim Qnotebook As Short = 0
    Dim Qpencil As Short = 0

    Dim Qfolder As Short = 0
    Dim Qeraser As Short = 0
    Dim Qbag As Short = 0


    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click

        btnReset.Hide()
        tbPayment.Clear()

        rbFolder.Hide()
        rbNotebook.Hide()
        rbPencil.Hide()
        rbRedPen.Hide()
        rbNone.Hide()
        rbBag.Hide()
        rbRuler.Hide()
        rbBluePen.Hide()
        rbEraser.Hide()

        If rbRedPen.Checked = True Then
            pToPay += pRedPen * Qredpen

            rbRedPen.Show()
        End If
        If rbBluePen.Checked = True Then
            pToPay += pBluePen * Qbluepen

            rbBluePen.Show()
        End If
        If rbRuler.Checked = True Then
            pToPay += pRuler * Qruler

            rbRuler.Show()
        End If
        If rbNotebook.Checked = True Then
            pToPay += pNotebook * Qnotebook

            rbNotebook.Show()
        End If
        If rbPencil.Checked = True Then

            pToPay += pPencil * Qpencil

            rbPencil.Show()
        End If

        ' Check accessory selection
        If rbNone.Checked = True Then
            pToPay += 0
            rbNone.Show()
        End If
        If rbFolder.Checked = True Then

            pToPay += pFolder * Qfolder

            rbFolder.Show()
        End If
        If rbEraser.Checked = True Then

            pToPay += pEraser * Qeraser

            rbEraser.Show()
        End If
        If rbBag.Checked = True Then

            pToPay += pBag * Qbag

            rbBag.Show()
        End If

        If pToPay <> 0 Then
            lblTotalPrice.Text = pToPay
            btnConfirm.Hide()
            btnEnter.Show()
            lblPaymentTitle.Show()
            tbPayment.Show()
        Else
            rbNone.Checked = False
            btnReset.Hide()
            rbFolder.Show()
            rbNotebook.Show()
            rbPencil.Show()
            rbRedPen.Show()
            rbNone.Show()
            rbBag.Show()
            rbRuler.Show()
            rbBluePen.Show()
            rbEraser.Show()
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        btnEnter.Hide()
        lblPaymentTitle.Hide()
        tbPayment.Hide()
        btnReset.Hide()

    End Sub

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click

        pPayment = Val(tbPayment.Text)
        pChange = pPayment - pToPay

        If Not pPayment >= pToPay And IsNumeric(pPayment) Then
            Beep()
            MsgBox("Invalid Value!")
            tbPayment.Clear()

        ElseIf pPayment > pToPay Then

            MsgBox(("Your change is:" & vbCrLf & "$" & pChange), 0, "ORDER SUCCESSFUL!")
            Beep()
            MsgBox("Thank you for shopping at Kim's Merchandise Store!", 0, "THANK YOU!")

            lblTotalPrice.Text = ""

            rbBluePen.Checked = False
            rbRedPen.Checked = False
            rbRuler.Checked = False
            rbNotebook.Checked = False
            rbPencil.Checked = False

            rbNone.Checked = False
            rbFolder.Checked = False
            rbEraser.Checked = False
            rbBag.Checked = False

            pToPay = 0
            pPayment = 0
            pChange = 0

            lbCart.Items.Clear()

            btnEnter.Hide()
            lblPaymentTitle.Hide()
            tbPayment.Hide()
            btnConfirm.Show()

            btnReset.Hide()
            rbFolder.Show()
            rbNotebook.Show()
            rbPencil.Show()
            rbRedPen.Show()
            rbNone.Show()
            rbBag.Show()
            rbRuler.Show()
            rbBluePen.Show()
            rbEraser.Show()
        Else
            MsgBox("Exact Amount!", 0, "ORDER SUCCESSFUL!")
            Beep()
            MsgBox("Thank you for shopping at Kim's Merchandise Store!", 0, "THANK YOU!")

            rbBluePen.Checked = False
            rbRedPen.Checked = False
            rbRuler.Checked = False
            rbNotebook.Checked = False
            rbPencil.Checked = False

            rbNone.Checked = False
            rbFolder.Checked = False
            rbEraser.Checked = False
            rbBag.Checked = False

            pToPay = 0
            pPayment = 0
            pChange = 0
            lbCart.Items.Clear()
            btnEnter.Hide()
            lblPaymentTitle.Hide()
            tbPayment.Hide()
            btnConfirm.Show()

            lblTotalPrice.Text = ""

            btnReset.Hide()
            rbFolder.Show()
            rbNotebook.Show()
            rbPencil.Show()
            rbRedPen.Show()
            rbNone.Show()
            rbBag.Show()
            rbRuler.Show()
            rbBluePen.Show()
            rbEraser.Show()
        End If

    End Sub

    Private Sub rbBluePen_CheckedChanged(sender As Object, e As EventArgs) Handles rbBluePen.CheckedChanged

        If rbBluePen.Checked = True Then

            Do
                Qbluepen = Val(InputBox("How many Blue Pens?", "Quantity"))
                If Qbluepen <> 0 And Qbluepen >= 0 Then
                    bLoop = True
                End If
            Loop While bLoop = False
            lbCart.Items.Add("Blue Pen - $" & pBluePen & " x " & vbCrLf & Qbluepen)
        End If

        bloop = False
        btnReset.Show()
    End Sub

    Private Sub rbNone_CheckedChanged(sender As Object, e As EventArgs) Handles rbNone.CheckedChanged

        If rbNone.Checked = True Then
            rbFolder.Checked = False
            rbEraser.Checked = False
            rbBag.Checked = False
            btnReset.Show()
        End If

    End Sub

    Private Sub rbFolder_CheckedChanged(sender As Object, e As EventArgs) Handles rbFolder.CheckedChanged

        If rbFolder.Checked = True Then
            rbNone.Checked = False
            Do
                Qfolder = Val(InputBox("How many Folders?", "Quantity"))
                If Qfolder <> 0 And Qfolder >= 0 Then
                    bLoop = True
                End If
            Loop While bLoop = False
            lbCart.Items.Add("Folder - $" & pFolder & " x " & Qfolder)
        End If

        bLoop = False

        btnReset.Show()

    End Sub

    Private Sub rbEraser_CheckedChanged(sender As Object, e As EventArgs) Handles rbEraser.CheckedChanged

        If rbEraser.Checked = True Then

            Do
                Qeraser = Val(InputBox("How many Erasers?", "Quantity"))
                If Qeraser <> 0 And Qeraser >= 0 Then
                    bLoop = True
                End If
            Loop While bLoop = False
            lbCart.Items.Add("Eraser - $" & pEraser & " x " & Qeraser)
        End If

        bLoop = False
        rbNone.Checked = False
        btnReset.Show()
    End Sub

    Private Sub rbBag_CheckedChanged(sender As Object, e As EventArgs) Handles rbBag.CheckedChanged

        If rbBag.Checked = True Then

            Do
                Qbag = Val(InputBox("How many Bags?", "Quantity"))
                If Qbag <> 0 And Qbag >= 0 Then
                    bLoop = True
                End If
            Loop While bLoop = False
            lbCart.Items.Add("Bag - $" & pBag & " x " & Qbag)
        End If

        bLoop = False
        rbNone.Checked = False
        btnReset.Show()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

        lbCart.Items.Clear()
        pToPay = 0
        Qbluepen = 0
        Qredpen = 0
        Qruler = 0
        Qnotebook = 0
        Qpencil = 0

        Qfolder = 0
        Qeraser = 0
        Qbag = 0
        rbBluePen.Checked = False
        rbRedPen.Checked = False
        rbRuler.Checked = False
        rbNotebook.Checked = False
        rbPencil.Checked = False

        rbNone.Checked = False
        rbFolder.Checked = False
        rbEraser.Checked = False
        rbBag.Checked = False

        btnReset.Hide()
    End Sub

    Private Sub rbNotebook_CheckedChanged(sender As Object, e As EventArgs) Handles rbNotebook.CheckedChanged

        If rbNotebook.Checked = True Then

            Do
                Qnotebook = Val(InputBox("How many Notebooks?", "Quantity"))
                If Qnotebook <> 0 And Qnotebook >= 0 Then
                    bLoop = True
                End If
            Loop While bLoop = False
            lbCart.Items.Add("Notebook - $" & pNotebook & " x " & Qnotebook)
        End If

        bLoop = False
        btnReset.Show()

    End Sub

    Private Sub rbPencil_CheckedChanged(sender As Object, e As EventArgs) Handles rbPencil.CheckedChanged

        If rbPencil.Checked = True Then

            Do
                Qpencil = Val(InputBox("How many Pencils?", "Quantity"))
                If Qpencil <> 0 And Qpencil >= 0 Then
                    bLoop = True
                End If
            Loop While bLoop = False
            lbCart.Items.Add("Pencil - $" & pPencil & " x " & Qpencil)
        End If

        bLoop = False
        btnReset.Show()
    End Sub

    Private Sub rbRuler_CheckedChanged(sender As Object, e As EventArgs) Handles rbRuler.CheckedChanged

        If rbRuler.Checked = True Then

            Do
                Qruler = Val(InputBox("How many Rulers?", "Quantity"))
                If Qruler <> 0 And Qruler >= 0 Then
                    bLoop = True
                End If
            Loop While bLoop = False
            lbCart.Items.Add("Ruler - $" & pNotebook & " x " & Qruler)
        End If

        bLoop = False
        btnReset.Show()
    End Sub

    Private Sub rbRedPen_CheckedChanged(sender As Object, e As EventArgs) Handles rbRedPen.CheckedChanged

        If rbRedPen.Checked = True Then
            Do
                Qredpen = Val(InputBox("How many Red Pens?", "Quantity"))
                If Qredpen <> 0 And Qredpen >= 0 Then
                    bLoop = True
                End If
            Loop While bLoop = False
            lbCart.Items.Add("Red Pen - $" & pRedPen & " x " & Qredpen)
        End If

        bLoop = False
        btnReset.Show()
    End Sub


End Class
