Imports System.IO
Imports System.Linq

Public Class Form1

    Dim myFile As String = "numbers.txt"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnWrite.Click
        Dim textInput As String = TextBox1.Text.Trim()
        If String.IsNullOrEmpty(textInput) Then
            MessageBox.Show("Gotta put a number in first!", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TextBox1.Focus()
            Return
        End If

        Try
            Dim fileWriter As StreamWriter = New StreamWriter(myFile, True)
            fileWriter.WriteLine(textInput)
            fileWriter.Close()

            MessageBox.Show("Number saved!", "Done", MessageBoxButtons.OK, MessageBoxIcon.None)
            TextBox1.Clear()

        Catch ex As Exception
            MessageBox.Show("ERROR! Could not save.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnRead.Click
        ListBox1.Items.Clear()

        Try
            Dim sr As StreamReader = New StreamReader(myFile)

            Dim textLine As String
            textLine = sr.ReadLine()
            Do While (textLine IsNot Nothing)
                ListBox1.Items.Add(textLine)
                textLine = sr.ReadLine()
            Loop

            sr.Close()

            MessageBox.Show("Loaded numbers into the list!", "Read Done", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("ERROR! Could not read file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnSort.Click

        If ListBox1.Items.Count = 0 Then
            MessageBox.Show("Read the numbers first by clicking READ!", "Wait", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim numbersList As New List(Of Integer)

        For Each item In ListBox1.Items
            Try
                numbersList.Add(Convert.ToInt32(item.ToString()))
            Catch ex As FormatException
                MessageBox.Show("Warning! Skipped bad data: " & item.ToString(), "Bad Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        Next

        Console.WriteLine("--- Sorting numbers using LINQ OrderBy! ---")
        Dim sortedNumbers = numbersList.OrderBy(Function(n) n)
        Console.WriteLine(vbCrLf & "--- SORTED RESULTS in Console ---")
        For Each num In sortedNumbers
            Console.WriteLine(num)
        Next
        ListBox1.Items.Clear()
        For Each num In sortedNumbers
            ListBox1.Items.Add(num.ToString())
        Next
        MessageBox.Show("Sorted results are now in the ListBox AND the Console!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            Using writer As New StreamWriter(myFile, False)
            End Using
            ListBox1.Items.Clear()
            MessageBox.Show($"Data in {myFile} has been cleared ", "File Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"Error Clearing file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
