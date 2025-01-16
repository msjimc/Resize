Option Strict On
Imports Resize.FileAccessClass

Public Class Form1
    Private Original, Final As String

    Private Sub btnOriginal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOriginal.Click
        Dim f As String = FileString(FileJob.Open, "Select the original file", "Screen shot (*.png; *.jpg; *.bmp)|*.png;*.jpg;*.bmp")
        If f = "Cancel" OrElse f = "" Then Exit Sub

        Me.Original = f
        Me.lblOriginal.text = f.Substring(f.LastIndexOf("\") + 1)

        If String.IsNullOrEmpty(Final) = False Then
            Me.btnCreate.Enabled = True
        Else
            Me.btnCreate.Enabled = False
        End If

    End Sub

    Private Sub btnFinal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinal.Click
        Dim f As String = FileString(FileJob.SaveAs, "Enter the name of the final file", "Final file (*.bmp; *.jpg)|*.bmp;*.jpg")
        If f = "Cancel" OrElse f = "" Then Exit Sub

        Me.Final = f
        Me.lblFinal.Text = f.Substring(f.LastIndexOf("\") + 1)

        If String.IsNullOrEmpty(Original) = False Then
            Me.btnCreate.Enabled = True
        Else
            Me.btnCreate.Enabled = False
        End If

    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try

            Dim original As Bitmap = CType(Bitmap.FromFile(Me.Original), Bitmap)
            Dim dpi As Single = original.VerticalResolution

            Dim TheInteger As Integer = CInt(Me.cboMultiplier.Text)
            Dim FinalDPI As Integer = CInt(Me.cboDPI.Text) + 1

            Dim final As Bitmap = New Bitmap(original.Width * TheInteger, original.Height * TheInteger, System.Drawing.Imaging.PixelFormat.Format32bppRgb)

            final.SetResolution(FinalDPI, FinalDPI)
            Dim g As Graphics = Graphics.FromImage(final)
            g.Clear(Color.White)

            Dim pixelColor As Color
            Dim rec As Rectangle = New Rectangle(0, 0, TheInteger, TheInteger)

            For x As Integer = 0 To original.Width - 1
                For y As Integer = 0 To original.Height - 1
                    pixelColor = original.GetPixel(x, y)
                    If Not (pixelColor.R = 255 AndAlso pixelColor.G = 255 AndAlso pixelColor.B = 255) Then
                        g.FillRectangle(New SolidBrush(pixelColor), rec)
                    End If
                    rec.Y += TheInteger
                Next
                rec.Y = 0
                rec.X += TheInteger
            Next

            final.Save(Me.Final)
            original = Nothing

            MsgBox("Done", MsgBoxStyle.Information, "Done")
        Catch ex As Exception
            MsgBox("An error occured", MsgBoxStyle.Information, "Error")
        End Try

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        For DPI As Integer = 100 To 1200 Step 100
            Me.cboDPI.Items.Add(DPI.ToString)
        Next

        Me.cboDPI.SelectedIndex = 0
        Me.cboMultiplier.SelectedIndex = 0

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim OriginalBTM, newPic As Bitmap

        OriginalBTM = CType(Bitmap.FromFile(Me.Original), Bitmap)
        newPic = New Bitmap(OriginalBTM.Width, OriginalBTM.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb)
        newPic.SetResolution(600, 600)

        Dim g As Graphics = Graphics.FromImage(newPic)
        g.Clear(Color.White)

        Dim pixelColor As Color

        For x As Integer = 0 To OriginalBTM.Width - 1
            For y As Integer = 0 To OriginalBTM.Height - 1
                pixelColor = OriginalBTM.GetPixel(x, y)
                If Not (pixelColor.R = 255 AndAlso pixelColor.G = 255 AndAlso pixelColor.B = 255) Then
                    newPic.SetPixel(x, y, pixelColor)
                End If
            Next
        Next

        newPic.Save(Me.Final)

        MsgBox("Done", MsgBoxStyle.Information, "Done")


    End Sub
End Class
