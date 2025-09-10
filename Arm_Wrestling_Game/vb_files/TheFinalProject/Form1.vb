Imports System.Threading.Tasks

Public Class Form1

    Private Sub cpuTimer_Tick(sender As Object, e As EventArgs) Handles cpuTimer.Tick
        If progressBar.Value + difficulty <= progressBar.Maximum Then
            progressBar.Value += difficulty
        Else
            progressBar.Value = progressBar.Maximum
        End If
        CheckWinner()
    End Sub

    Private Async Sub CheckWinner()
        UpdateStatusPicture()
        If progressBar.Value <= 0 Then
            cpuTimer.Stop()
            Beep()
            Await Task.Delay(2000)
            Me.Hide()
            AfterMenu.Show()
        ElseIf progressBar.Value >= progressBar.Maximum Then
            cpuTimer.Stop()
            Beep()
            Await Task.Delay(2000)
            Me.Hide()
            AfterMenuLose.Show()
        End If
    End Sub

    Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If progressBar.Value <= 0 Then
            Exit Sub
        End If

        If progressBar.Value >= progressBar.Maximum Then
            Exit Sub
        End If

        If progressBar.Value > 0 Then
            progressBar.Value -= 5
        End If
        CheckWinner()
    End Sub

    Private Sub UpdateStatusPicture()
        Dim halfValue As Integer = progressBar.Maximum / 2
        Dim quarterValue As Integer = progressBar.Maximum / 4
        Dim threeQuarterValue As Integer = 3 * quarterValue

        If progressBar.Value <= 0 Then
            pbStatus.Image = My.Resources.PlayerWins_01
        ElseIf progressBar.Value >= progressBar.Maximum Then
            pbStatus.Image = My.Resources.CPUWins_01
        ElseIf progressBar.Value <= quarterValue Then
            pbStatus.Image = My.Resources.PlayerWinning_01
        ElseIf progressBar.Value >= threeQuarterValue Then
            pbStatus.Image = My.Resources.CPUWinning_01
        ElseIf progressBar.Value > halfValue Then
            pbStatus.Image = My.Resources.CPUNeutral_01
        Else
            pbStatus.Image = My.Resources.Neutral_01
        End If
    End Sub

    Private Sub pbStatus_Click(sender As Object, e As EventArgs) Handles pbStatus.Click
        If progressBar.Value <= 0 Then
            Exit Sub
        End If

        If progressBar.Value >= progressBar.Maximum Then
            Exit Sub
        End If

        If progressBar.Value > 0 Then
            progressBar.Value -= 5
        End If
        CheckWinner()
    End Sub

    Private Sub Form1_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            pbStatus.Image = My.Resources.Neutral_01
            progressBar.Value = progressBar.Maximum / 2
            cpuTimer.Start()
        End If
    End Sub

    Private Sub progressBar_Click(sender As Object, e As EventArgs) Handles progressBar.Click
        If progressBar.Value <= 0 Then
            Exit Sub
        End If

        If progressBar.Value >= progressBar.Maximum Then
            Exit Sub
        End If

        If progressBar.Value > 0 Then
            progressBar.Value -= 5
        End If
        CheckWinner()
    End Sub
End Class