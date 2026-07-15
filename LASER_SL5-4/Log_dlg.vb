Imports System.Threading
Imports System.Windows
Imports System.Windows.Documents

Public Class Log_dlg
    Private lenght_1 As Int16
    Private lenght_2 As Int16
    Private Sub Log_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (com_ch1.Tcp_server_running = True) Then
            Button1.BackColor = Color.Green
            Button1.Text = "RUNNING"
        Else
            Button1.BackColor = Color.Red
            Button1.Text = ""
        End If
        If (com_ch1.Plc_connected = True) Then
            Button3.BackColor = Color.Green
            Button3.Text = "CONNECTED"
        Else
            Button3.BackColor = Color.Red
            Button3.Text = ""
        End If
        Timer1.Enabled = True
        MessageLog.Enabled = True

        If temp_string <> "" And IsNothing(temp_string) = False Then
            RichTextBox1.Text = temp_string
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If (com_ch1.Tcp_server_running = True) Then
            Button1.BackColor = Color.Green
            Button1.Text = "RUNNING"
        Else
            Button1.BackColor = Color.Red
            Button1.Text = ""
        End If
        If (com_ch1.Plc_connected = True) Then
            Button3.BackColor = Color.Green
            Button3.Text = "CONNECTED"
        Else
            Button3.BackColor = Color.Red
            Button3.Text = ""
        End If
    End Sub

    Private Sub update_log(ByRef lng As Int16, queue As Queue, textbx As RichTextBox)
        If queue.Count <> 0 Then
            Dim el As String
            el = queue.Peek
            'el = el.Replace(Chr(&H17), " ")
            'el = el.Replace(Chr(&H3), "")
            textbx.Text = textbx.Text + el + ControlChars.CrLf
            temp_string = textbx.Text
            queue.Dequeue()
        End If
    End Sub

    Private Sub MessageLog_Tick(sender As Object, e As EventArgs) Handles MessageLog.Tick
        update_log(lenght_1, Message_for_log, RichTextBox1)
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class