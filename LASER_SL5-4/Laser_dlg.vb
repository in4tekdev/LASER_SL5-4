Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets
Imports System.Text.UTF7Encoding
Imports System.Threading
Imports System.ComponentModel

Public Class Laser
    Public Sub New(event_provider As Form1)

        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()
        AddHandler event_provider.Show_My_Message, AddressOf aggiorna_grafica
        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().

    End Sub
    Private Sub Laser_Load(sender As Object, e As EventArgs) Handles Me.Load

        Update_recipe_combo()
        TextBox1.Text = las1.batch
        If (las1.SN <> vbNull) Then
            TextBox2.Text = las1.SN
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'las1.batch = TextBox1.Text
        'las1.start_laser = True
        ''Dim msg = Start_Laser_String(las1.recipe, las1.laser_type, las1.batch, las1.SN, las1.position, las1.string_infos)
        'msg_ans_queue.Enqueue(msg)
        'las1.SN = las1.SN + 1
        'TextBox2.Text = las1.SN
        'las1.start_laser = False
    End Sub

    Private Sub Update_recipe_combo()
        Dim recipe_list As String() = Get_active_recipes()
        ComboBox1.Items.Clear()
        For Each rec As String In recipe_list
            ComboBox1.Items.Add(rec)
        Next rec
        ComboBox1.SelectedItem = las1.recipe

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        las1.batch = TextBox1.Text
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        las1.recipe = ComboBox1.SelectedItem
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If (TextBox2.Text <> vbNull And TextBox2.Text <> "") Then
            las1.SN = TextBox2.Text
        End If

    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If (las1.SN.ToString <> TextBox2.Text) Then
            TextBox2.Text = las1.SN
        End If

    End Sub

    Public Sub aggiorna_grafica(ByVal message As Graphics_update)
        Try

        If form_selected = "LASER" Then


            Label4.Invoke(Sub() 'stato connessione
                              Label4.Text = message.laser_stat
                              If message.laser_stat = "LASER FREE ..." Then
                                  Label4.BackColor = Color.Green
                              ElseIf message.laser_stat = "LASER RUNNING ..." Then
                                  Label4.BackColor = Color.Orange
                              ElseIf message.laser_stat = "LASER INITIALIZED ..." Then
                                  Label4.BackColor = Color.Yellow
                              End If
                              'Label2.BackColor = Color.Yellow
                          End Sub)
            Label10.Invoke(Sub() 'stato connessione
                               Label10.Text = message.recipe
                               'Label2.BackColor = Color.Yellow
                           End Sub)
            Label11.Invoke(Sub() 'stato connessione
                               Label11.Text = message.serial
                               'Label2.BackColor = Color.Yellow
                           End Sub)
            Label12.Invoke(Sub() 'stato connessione
                               Label12.Text = message.batch
                               'Label2.BackColor = Color.Yellow
                           End Sub)
            Label13.Invoke(Sub() 'stato connessione
                               Label13.Text = message.opt
                               'Label2.BackColor = Color.Yellow
                           End Sub)
            Label14.Invoke(Sub() 'stato connessione
                               Label14.Text = message.last
                               'Label2.BackColor = Color.Yellow
                           End Sub)
        End If
Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            las1.laser_pointer = True
        Else
            las1.laser_pointer = False
        End If

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        start_manuale = True
    End Sub
End Class
