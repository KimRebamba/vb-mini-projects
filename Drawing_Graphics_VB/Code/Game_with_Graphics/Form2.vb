Public Class Form2
    'EASY - JAPAN
    'MEDIUM - QATAR
    'HARD - GERMANY
    'EXTREME - CHINA
    'IMPOSSIBLE - CANADA

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        num_tool = 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        num_tool = 2
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        num_tool = 3
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        num_tool = 4
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        num_tool = 5
    End Sub

    Private Sub btnRed_Click(sender As Object, e As EventArgs) Handles btnRed.Click
        num_color = 1
    End Sub

    Private Sub btnYellow_Click(sender As Object, e As EventArgs) Handles btnYellow.Click
        num_color = 2
    End Sub

    Private Sub btnBlack_Click(sender As Object, e As EventArgs) Handles btnBlack.Click
        num_color = 3
    End Sub

    Private Sub btnWhite_Click(sender As Object, e As EventArgs) Handles btnWhite.Click
        num_color = 4
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Refresh()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Refresh()
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub Form2_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        Me.Refresh()

        Select Case diff_number
            Case 1
                pbFlag.Image = My.Resources.japan

            Case 2
                pbFlag.Image = My.Resources.qatar
            Case 3
                pbFlag.Image = My.Resources.germany
            Case 4
                pbFlag.Image = My.Resources.china
            Case 5
                pbFlag.Image = My.Resources.canada
        End Select

    End Sub

    Dim myPen As Pen
    Dim myBrush As Brush
    Dim myRectangle As New Rectangle

    Dim num_tool As Integer = 0
    Dim num_color As Integer = 0

    Dim line_num As Integer = 0
    Dim circle_num As Integer = 0
    Dim rec_num As Integer = 0
    Dim tri_num As Integer = 0

    Dim line_POne As Point
    Dim line_Ptwo As Point

    Dim circle_POne As Point
    Dim circle_PTwo As Point

    Dim rec_POne As Point
    Dim rec_PTwo As Point

    Dim tri_POne As Point
    Dim tri_PTwo As Point
    Dim tri_PThree As Point

    Private Sub pbCanvas_MouseClick(sender As Object, e As MouseEventArgs) Handles pbCanvas.MouseClick
        'e As MouseEventArgs (pre-defined) will be our mouse position thing..
        '  -> It's the mouse position within the pictureBox
        'First point will be our x1 and x2, Second point will be our x2 and y2

        Dim CreateGraphics As Graphics = pbCanvas.CreateGraphics

        If num_color <> 0 And num_tool <> 0 Then
            Select Case num_tool
                Case 1
                    If line_num = 0 Then
                        line_POne = New Point(e.X, e.Y)
                        line_num += 1
                    Else

                        Select Case num_color
                            Case 1
                                myPen = New Pen(Brushes.Red, 10)
                            Case 2
                                myPen = New Pen(Brushes.Yellow, 10)
                            Case 3
                                myPen = New Pen(Brushes.Black, 10)
                            Case 4
                                myPen = New Pen(Brushes.White, 10)
                        End Select

                        line_Ptwo = New Point(e.X, e.Y)
                        Dim lines = {line_POne, line_Ptwo}
                        pbCanvas.CreateGraphics.DrawLines(myPen, lines)
                        line_num = 0
                    End If
                Case 2
                    If circle_num = 0 Then
                        circle_POne = New Point(e.X, e.Y - 30)
                        circle_num += 1
                    Else

                        Select Case num_color
                            Case 1
                                myPen = New Pen(Brushes.Red, 10)
                            Case 2
                                myPen = New Pen(Brushes.Yellow, 10)
                            Case 3
                                myPen = New Pen(Brushes.Black, 10)
                            Case 4
                                myPen = New Pen(Brushes.White, 10)
                        End Select

                        circle_PTwo = New Point(e.X, e.Y - 30)

                        'logic -> to prevent width and height being negative
                        If circle_POne.X < circle_PTwo.X Then
                            myRectangle.X = circle_POne.X
                            myRectangle.Width = circle_PTwo.X - circle_POne.X
                        Else
                            myRectangle.X = circle_PTwo.X
                            myRectangle.Width = circle_POne.X - circle_PTwo.X
                        End If

                        If circle_POne.Y < circle_PTwo.Y Then
                            myRectangle.Y = circle_POne.Y
                            myRectangle.Height = circle_PTwo.Y - circle_POne.Y
                        Else
                            myRectangle.Y = circle_PTwo.Y
                            myRectangle.Height = circle_POne.Y - circle_PTwo.Y
                        End If

                        pbCanvas.CreateGraphics.DrawEllipse(myPen, myRectangle)
                        circle_num = 0

                    End If
                Case 3
                    If rec_num = 0 Then
                        rec_POne = New Point(e.X, e.Y - 30)
                        rec_num += 1

                    Else
                        Select Case num_color
                            Case 1
                                myPen = New Pen(Brushes.Red, 10)
                            Case 2
                                myPen = New Pen(Brushes.Yellow, 10)
                            Case 3
                                myPen = New Pen(Brushes.Black, 10)
                            Case 4
                                myPen = New Pen(Brushes.White, 10)
                        End Select

                        rec_PTwo = New Point(e.X, e.Y - 30)

                        If rec_POne.X < rec_PTwo.X Then
                            myRectangle.X = rec_POne.X
                            myRectangle.Width = rec_PTwo.X - rec_POne.X
                        Else
                            myRectangle.X = rec_PTwo.X
                            myRectangle.Width = rec_POne.X - rec_PTwo.X
                        End If

                        If rec_POne.Y < rec_PTwo.Y Then
                            myRectangle.Y = rec_POne.Y
                            myRectangle.Height = rec_PTwo.Y - rec_POne.Y
                        Else
                            myRectangle.Y = rec_PTwo.Y
                            myRectangle.Height = rec_POne.Y - rec_PTwo.Y
                        End If

                        pbCanvas.CreateGraphics.DrawRectangle(myPen, myRectangle)
                        rec_num = 0
                    End If
                Case 4
                    If circle_num = 0 Then
                        circle_POne = New Point(e.X, e.Y - 30)
                        circle_num += 1

                    Else

                        Select Case num_color
                            Case 1
                                myBrush = New SolidBrush(Color.Red)
                            Case 2
                                myBrush = New SolidBrush(Color.Yellow)
                            Case 3
                                myBrush = New SolidBrush(Color.Black)
                            Case 4
                                myBrush = New SolidBrush(Color.White)
                        End Select

                        circle_PTwo = New Point(e.X, e.Y - 30)

                        If circle_POne.X < circle_PTwo.X Then
                            myRectangle.X = circle_POne.X
                            myRectangle.Width = circle_PTwo.X - circle_POne.X
                        Else
                            myRectangle.X = circle_PTwo.X
                            myRectangle.Width = circle_POne.X - circle_PTwo.X
                        End If

                        If circle_POne.Y < circle_PTwo.Y Then
                            myRectangle.Y = circle_POne.Y
                            myRectangle.Height = circle_PTwo.Y - circle_POne.Y
                        Else
                            myRectangle.Y = circle_PTwo.Y
                            myRectangle.Height = circle_POne.Y - circle_PTwo.Y
                        End If

                        pbCanvas.CreateGraphics.FillEllipse(myBrush, myRectangle)
                        circle_num = 0

                    End If

                Case 5
                    If rec_num = 0 Then
                        rec_POne = New Point(e.X, e.Y - 30)
                        rec_num += 1

                    Else

                        Select Case num_color
                            Case 1
                                myBrush = New SolidBrush(Color.Red)
                            Case 2
                                myBrush = New SolidBrush(Color.Yellow)
                            Case 3
                                myBrush = New SolidBrush(Color.Black)
                            Case 4
                                myBrush = New SolidBrush(Color.White)
                        End Select

                        rec_PTwo = New Point(e.X, e.Y - 30)

                        If rec_POne.X < rec_PTwo.X Then
                            myRectangle.X = rec_POne.X
                            myRectangle.Width = rec_PTwo.X - rec_POne.X
                        Else
                            myRectangle.X = rec_PTwo.X
                            myRectangle.Width = rec_POne.X - rec_PTwo.X
                        End If

                        If rec_POne.Y < rec_PTwo.Y Then
                            myRectangle.Y = rec_POne.Y
                            myRectangle.Height = rec_PTwo.Y - rec_POne.Y
                        Else
                            myRectangle.Y = rec_PTwo.Y
                            myRectangle.Height = rec_POne.Y - rec_PTwo.Y
                        End If

                        pbCanvas.CreateGraphics.FillRectangle(myBrush, myRectangle)
                        rec_num = 0
                    End If
                Case 6
                    If tri_num = 0 Then
                        tri_POne = New Point(e.X, e.Y)
                        tri_num += 1
                    ElseIf tri_num = 1 Then
                        tri_PTwo = New Point(e.X, e.Y)
                        tri_num += 1
                    Else
                        Select Case num_color
                            Case 1
                                myBrush = New SolidBrush(Color.Red)
                            Case 2
                                myBrush = New SolidBrush(Color.Yellow)
                            Case 3
                                myBrush = New SolidBrush(Color.Black)
                            Case 4
                                myBrush = New SolidBrush(Color.White)
                        End Select

                        tri_PThree = New Point(e.X, e.Y)
                        Dim lines = {tri_POne, tri_PTwo, tri_PThree}
                        pbCanvas.CreateGraphics.FillPolygon(myBrush, lines)
                        tri_num = 0
                    End If
            End Select
        End If
    End Sub

    Private Sub btnTriangle_Click(sender As Object, e As EventArgs) Handles btnTriangle.Click
        num_tool = 6
    End Sub
End Class