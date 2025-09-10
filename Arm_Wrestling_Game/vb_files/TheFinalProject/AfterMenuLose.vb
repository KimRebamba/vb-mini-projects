Public Class AfterMenuLose
    Private Sub pbTry_Click(sender As Object, e As EventArgs) Handles pbTry.Click
        Me.Hide()
        LoadingScreen.Show()
    End Sub

    Private Sub pbQuit_Click(sender As Object, e As EventArgs) Handles pbQuit.Click
        Application.Exit()
    End Sub

    Private Sub AfterMenuLose_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.BackgroundImage = My.Resources.YOULOSENeutral_01
    End Sub

    Private Sub pbQuit_MouseEnter(sender As Object, e As EventArgs) Handles pbQuit.MouseEnter
        Me.BackgroundImage = My.Resources.YOULOSEQuit_01
    End Sub

    Private Sub pbTry_MouseEnter(sender As Object, e As EventArgs) Handles pbTry.MouseEnter
        Me.BackgroundImage = My.Resources.YOULOSEtry_01
    End Sub
End Class