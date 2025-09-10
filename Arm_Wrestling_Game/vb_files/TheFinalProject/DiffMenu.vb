Public Class DiffMenu
    Private Sub pbEasy_Click(sender As Object, e As EventArgs) Handles pbEasy.Click
        Me.Hide()
        difficulty = 15
        Form1.Show()
    End Sub

    Private Sub pbEasy_MouseEnter(sender As Object, e As EventArgs) Handles pbEasy.MouseEnter
        Me.BackgroundImage = My.Resources.easy_01
    End Sub

    Private Sub DiffMenu_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.BackgroundImage = My.Resources.DiffNeutral_01
    End Sub

    Private Sub pbHard_MouseEnter(sender As Object, e As EventArgs) Handles pbHard.MouseEnter
        Me.BackgroundImage = My.Resources.hard_01
    End Sub

    Private Sub pbMed_MouseEnter(sender As Object, e As EventArgs) Handles pbMed.MouseEnter
        Me.BackgroundImage = My.Resources.medium_01
    End Sub

    Private Sub pbMed_Click(sender As Object, e As EventArgs) Handles pbMed.Click
        Me.Hide()
        difficulty = 25
        Form1.Show()
    End Sub

    Private Sub pbHard_Click(sender As Object, e As EventArgs) Handles pbHard.Click
        Me.Hide()
        difficulty = 35
        Form1.Show()
    End Sub
End Class