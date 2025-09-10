Public Class Form1

    Sub callForm()
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub btnEasy_Click(sender As Object, e As EventArgs) Handles btnEasy.Click
        diff_number = 1
        callForm()
    End Sub

    Private Sub btnMedium_Click(sender As Object, e As EventArgs) Handles btnMedium.Click
        diff_number = 2
        callForm()
    End Sub

    Private Sub btnHard_Click(sender As Object, e As EventArgs) Handles btnHard.Click
        diff_number = 3
        callForm()
    End Sub

    Private Sub btnExtreme_Click(sender As Object, e As EventArgs) Handles btnExtreme.Click
        diff_number = 4
        callForm()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        diff_number = 5
        callForm()
    End Sub

    Private Sub lblTITLE_Click(sender As Object, e As EventArgs) Handles lblTITLE.Click
        Me.Hide()
    End Sub
End Class
