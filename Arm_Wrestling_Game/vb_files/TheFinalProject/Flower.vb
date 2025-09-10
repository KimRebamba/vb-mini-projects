Public Class Flower
    Private Sub pbTry_Click(sender As Object, e As EventArgs) Handles pbTry.Click
        Me.Hide()
        LoadingScreen.Show()
    End Sub

    Private Sub pbQuit_Click(sender As Object, e As EventArgs) Handles pbQuit.Click
        Application.Exit()
    End Sub

    Private Sub pbQuit_MouseEnter(sender As Object, e As EventArgs) Handles pbQuit.MouseEnter
        Me.BackgroundImage = My.Resources.Flower2_01
    End Sub

    Private Sub Flower_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.BackgroundImage = My.Resources.Flowers1_01
    End Sub

    Private Sub pbTry_MouseEnter(sender As Object, e As EventArgs) Handles pbTry.MouseEnter
        Me.BackgroundImage = My.Resources.Flower3_01
    End Sub

    Private Sub Flower_MdiChildActivate(sender As Object, e As EventArgs) Handles Me.MdiChildActivate
        If Me.Visible = True Then
            lblname.Text = playername.ToUpper
        End If
    End Sub

    Private Sub Flower_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblname.Text = playername.ToUpper
    End Sub
End Class