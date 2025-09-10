Public Class LoadingScreen
    Dim count As Short = 0
    Dim blnTip As Boolean = False

    Private Sub pbStart_Click(sender As Object, e As EventArgs) Handles pbStart.Click
        Me.Hide()

        If playername = "" Then
            Do
                playername = InputBox("What's your name, player?", "Asking name...")
            Loop While String.IsNullOrWhiteSpace(playername)
        End If
        DiffMenu.Show()
    End Sub

    Private Sub pbQuit_Click(sender As Object, e As EventArgs) Handles pbQuit.Click
        Application.Exit()
    End Sub

    Private Sub pbQuit_MouseEnter(sender As Object, e As EventArgs) Handles pbQuit.MouseEnter
        Me.BackgroundImage = My.Resources.LoadingScreenExit_01
    End Sub

    Private Sub pbStart_MouseEnter(sender As Object, e As EventArgs) Handles pbStart.MouseEnter
        Me.BackgroundImage = My.Resources.LoadingScreenStart_01
    End Sub

    Private Sub LoadingScreen_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        If blnTip = False Then
            Me.BackgroundImage = My.Resources.LoadingScreenNeutral_01
        End If
    End Sub

    Private Sub LoadingScreen_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub pbEaster_MouseEnter(sender As Object, e As EventArgs) Handles pbEaster.MouseEnter
        Beep()
        Me.BackgroundImage = My.Resources.easter_01
    End Sub

    Private Sub pbEaster_Click(sender As Object, e As EventArgs) Handles pbEaster.Click
        If count = 7 Then
            MessageBox.Show("Stop clicking the logo.. what exactly do you want to happen? Just why?")
        ElseIf count = 19 Then
            MessageBox.Show("Credits: " & vbCrLf & "Custom Drawings: Me" & vbCrLf & "Coding: Me" & vbCrLf & "Everything: Me......")
            MessageBox.Show("Now stop clicking the logo.")
            count = 0
        End If

        count += 1
    End Sub

    Private Sub btnTip_Click(sender As Object, e As EventArgs) Handles btnTip.Click
        If blnTip = False Then
            pbEaster.Hide()
            pbStart.Hide()
            pbQuit.Hide()
            Me.BackgroundImage = My.Resources.ARMTIPS_01
            btnTip.Text = "BACK"
            blnTip = True
        Else
            Me.BackgroundImage = My.Resources.LoadingScreenNeutral_01
            btnTip.Text = "TIP?"
            blnTip = False
            pbEaster.Show()
            pbStart.Show()
            pbQuit.Show()
        End If
    End Sub
End Class