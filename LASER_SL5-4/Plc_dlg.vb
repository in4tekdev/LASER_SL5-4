Imports System.Threading
Imports System.Windows.Controls

Public Class Plc_dlg
    Private Sub plc_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox1.Text = Station_settings.DeviceIp
        TextBox2.Text = Station_settings.DevicePort
        ComboBox1.Items.Add(Station_settings.CommunicationType)
        ComboBox1.SelectedText = Station_settings.CommunicationType
    End Sub

End Class