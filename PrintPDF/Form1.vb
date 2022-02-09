Option Explicit On

Public Class Form1

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Public Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim filewithext As String
        Dim i As Long
        Dim swtime As Integer
        Dim sw As New Stopwatch

        'Get files from user input in Textbox 1
        Dim files1 As String = (TextBox1.Text)

        'Set file location and file extension
        Dim workingDir As String = ("C:\SWPDM\PDF's\")
        Dim fileExt As String = (".pdf")

        'Split user input into array, seperated by line break
        Dim stringText = files1.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
        Dim stringArray(stringText.Length - 1) As String

        'Set form size to autofit height based upon number of lines input
        Dim x As Integer = Me.ClientSize.Width
        Dim y As Integer = Me.ClientSize.Height
        y = ((stringText.Length + 1) * 13)
        Me.TextBox1.Size = New Size(x, y - 10)
        Me.ClientSize = New Size(x + 50, y + 40)

        'Start new process to print PDF's
        Dim psi As New ProcessStartInfo
        psi.UseShellExecute = True
        psi.Verb = "print"
        psi.WindowStyle = ProcessWindowStyle.Hidden        'IF NEEDED, ADD TO NEXT LINE: (psi.Arguments = PrintDialog1.PrinterSettings.PrinterName.ToString()) 

        'Assign full path to each file and execute print process
        For i = 0 To UBound(stringArray)
            filewithext = (workingDir + stringText(i) & fileExt)
            stringArray(i) = filewithext

            'OPTIONAL METHOD OF PRINTING, USES PDFTOPRINTER APPLICATION
            'cmdHandle(filewithext)


            psi.FileName = (filewithext) 'document to be printed
            Process.Start(psi)

            'Set text to show user current print info
            Label2.Text = ("Now Printing")
            Label3.Text = psi.FileName

            'set time delay for print info display
            swtime = 3000
            sw.Start()
            Do While sw.ElapsedMilliseconds < swtime
                Application.DoEvents()
            Loop
            sw.Stop()
        Next

        'Clear User input text box and show job is complete
        TextBox1.Text = ""
        Label2.Text = "Print Job Complete"
        Label3.Text = ""
    End Sub

    'Public Sub cmdHandle(ByVal pstringArray As String)
    'Dim myProcess As New Process
    'Dim quote As String = ("""")

    'myProcess.StartInfo.UseShellExecute = True
    'myProcess.StartInfo.WorkingDirectory = ("C:\SWPDM\PDF'S\")
    'myProcess.StartInfo.FileName = ("cmd /b")
    'myProcess.StartInfo.Arguments = ("/c  \\bmedc-01\common\PrintPDFPackageTemp\PrintApp\PDFtoPrinter " + quote + pstringArray + quote)
    'Debug.Print(myProcess.StartInfo.Arguments)

    'myProcess = Process.Start(myProcess.StartInfo)
    'myProcess.WaitForExit()
    'System.Threading.Thread.Sleep(1500)
    'Exit Sub
    'End Sub
End Class

