Public Class AfterMenu
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles pbClaim.Click
        Me.Hide()
        Flower.Show()
    End Sub

    Private Sub AfterMenu_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.BackgroundImage = My.Resources.YOUWONNeutral_01
    End Sub

    Private Sub pbClaim_MouseEnter(sender As Object, e As EventArgs) Handles pbClaim.MouseEnter
        Me.BackgroundImage = My.Resources.YOUWONIN_01
    End Sub
End Class