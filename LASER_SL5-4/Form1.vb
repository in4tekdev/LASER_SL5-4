Imports System.Collections.Specialized.BitVector32
Imports System.ComponentModel
Imports System.Drawing.Text
Imports System.Reflection.Emit
Imports System.Threading
Imports System.Windows.Controls
Imports System.Windows.Forms.VisualStyles
Imports System.Windows.Input
Imports Laser_Interface.Log_File_Thread
Imports Microsoft.VisualBasic.Logging
Imports MS.Internal

Public Class Form1

    Private current_child_form As Form
    Public Event Show_My_Message(ByVal message As Graphics_update)
    Public WithEvents cycle_1 As Main_Cycle
    Public opc1 As OPCClass
    'Public WithEvents Logs As Log_File_Thread
    'Public WithEvents Mqtt_comm As Mqtt_class
    'Public mqtt_client_struct As Mqtt_client_id_struct

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Get_Last_Config() = False Then
            MsgBox("ERRORE CARICAMENTO ULTIMA CONFIG")
        End If

        Open_Child_Form(New Laser(Me))

        Me.Text = "SL5-4 LASER V. " & Application.ProductVersion
        com_ch1.Tcp_server_running = False
        com_ch1.Tcp_client_connected = False
        com_ch1.Plc_connected = False


        'TcpServ = New TCPSrv
        'TcpServ.server_loop = New Thread(AddressOf TcpServ.Tcp_Thread)
        'TcpServ.server_loop.Start()


        Timer1.Enabled = True



        opc1 = New OPCClass(Station_settings.DeviceIp, Station_settings.DevicePort)
        opc1.Subscribe()
        com_ch1.Plc_connected = True


        cycle_1 = New Main_Cycle()
        cycle_1.main_loop = New Thread(AddressOf cycle_1.main_thread)
        cycle_1.main_loop.Start()

        'inizializzazione thread dei log
        Logs = New Log_File_Thread()
        Logs.Log_loop = New Thread(AddressOf Logs.Log_thread)
        Logs.Log_loop.Start()


        form_selected = "LASER"
    End Sub

    Private Sub Form1_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        'opc1.OPC_Disconnect()
        End
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'If TcpServ.server_loop.IsAlive = False Then
        '    TcpServ.server_loop = New Thread(AddressOf TcpServ.Tcp_Thread)
        '    TcpServ.server_loop.Start()
        'End If
    End Sub



    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        form_to_open = "LASER"
        Open_Child_Form(New Laser(Me))
        form_selected = "LASER"
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Open_Child_Form(New Plc_dlg)
        form_selected = "PLC"
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        If IconButton4.Visible = False Then
            IconButton4.Visible = True
        Else
            IconButton4.Visible = False
        End If

        If IconButton5.Visible = False Then
            IconButton5.Visible = True
        Else
            IconButton5.Visible = False
        End If
        Open_Child_Form(New Log_dlg())
        form_selected = "LOG"
    End Sub



    'DESIGN FORM

    Private Sub Open_Child_Form(child_form As Form)
        Me.Invoke(Sub()
                      If form_selected = "LASER" Then
                          current_child_form.Visible = False
                          hidden_form = current_child_form
                      ElseIf form_to_open = "LASER" And IsNothing(hidden_form) = False Then
                          current_child_form.Close()
                          hidden_form.Visible = True
                          Label4.Text = child_form.Text
                          form_to_open = ""
                          hidden_form = Nothing
                      ElseIf IsNothing(current_child_form) = False Then

                          current_child_form.Close()

                          current_child_form.Close()
                      End If
                      current_child_form = child_form
                      child_form.TopLevel = False
                      child_form.FormBorderStyle = FormBorderStyle.None
                      child_form.Dock = DockStyle.Fill
                      Panel1.Controls.Add(child_form)
                      Panel1.Tag = child_form
                      child_form.BringToFront()
                      child_form.Show()
                      Label4.Text = child_form.Text
                  End Sub)
    End Sub

    Public Sub error_received(ByVal sender As Object, ByVal e As Error_comm) Handles cycle_1.error_msg_exchange
        UpdateLaserResult(e.dm_text, las1.SN, las1.TrackID)
        If InsertLaserSerial(las1.recipe, las1.batch, las1.SN, las1.TrackID) = False Then
            MsgBox("ERRORE INSERIMENTO DATI IN TABELLA LASER SERIAL")
        End If
        Dim resWrite As Boolean = False
        If e.err_to_send = "NO ERROR" Then
            opc1.OPC_Write(NODO_RISULTATO_LASER, True)
            opc1.OPC_Write(NODO_LASERATO_LASER, e.dm_text)
            opc1.OPC_Write(NODO_FINE_LASER, True)
            Logs.Add_log_element("COMANDO INVIATO", "FINE LASERATURA OK")
            While (1)
                Thread.Sleep(100)

                Dim n_try = 0
                While resWrite = False
                    Thread.Sleep(100)
                    If n_try <= MAX_TRY_WRITE_OPC Then
                        resWrite = opc1.OPC_Write(NODO_RISULTATO_LASER, False)
                        Logs.Add_log_element("COMANDO INVIATO", "RESET RISULTATO LASER OK")
                        n_try += 1
                    Else
                        n_try = 0
                        Logs.Add_log_element("COMANDO NON INVIATO", "IMPOSSIBILE CONTATTATARE SERVER OPCUA ESITO BUONO")
                        Exit While
                    End If
                End While

                resWrite = False
                n_try = 0
                While resWrite = False
                    Thread.Sleep(100)
                    If n_try <= MAX_TRY_WRITE_OPC Then
                        resWrite = opc1.OPC_Write(NODO_FINE_LASER, False)
                        Logs.Add_log_element("COMANDO INVIATO", "RESET FINITO LASER OK")
                        n_try += 1
                    Else
                        n_try = 0
                        Logs.Add_log_element("COMANDO NON INVIATO", "IMPOSSIBILE CONTATTATARE SERVER OPCUA RESET FINE LASER")
                        Exit While
                    End If
                End While
                Exit While
            End While

        Else
            Logs.Add_log_element("COMANDO INVIATO", "FINE LASERATURA KO")
            opc1.OPC_Write(NODO_RISULTATO_LASER, False)
            opc1.OPC_Write(NODO_LASERATO_LASER, e.dm_text)
            opc1.OPC_Write(NODO_FINE_LASER, True)
            While (1)
                Thread.Sleep(100)
                If opc1.OPC_Read(NODO_START_LASER).ToString = "False" Then
                    opc1.OPC_Write(NODO_RISULTATO_LASER, False)
                    Logs.Add_log_element("COMANDO INVIATO", "RESET RISULTATO LASER KO")
                    opc1.OPC_Write(NODO_FINE_LASER, False)
                    Logs.Add_log_element("COMANDO INVIATO", "RESET FINITO LASER KO")
                    Exit While
                End If
            End While
            'Open_Child_Form(New Error_dlg())
            'form_selected = "ERROR"
        End If


    End Sub
    '    Public Sub Mqtt_update(ByVal sender As Object, ByVal e As MqttEventArgs) Handles Mqtt_comm.mqtt_msg_received

    '        'ricevuto un messaggio MQTT: discriminiamo il topic di provenienza, facciamo il parsing del messaggio e impostiamole azioni da fare
    '        Dim str As String
    '        Dim separators As Char() = New Char() {"$"c, "#"c, "%"c}
    '        Dim str_arr() As String
    '        Dim serial_ok As Integer
    '        Dim serial_scrap As Integer

    '        Select Case e.Mqtt_out.topic

    '            Case "com/startlaser"

    '                'parsing della stringa ricevuta
    '                If e.Mqtt_out.msg = "1" Then
    '                    Plc_status.pronto_per_laser_1 = True
    '                End If
    '            Case "com/seriale"
    '                Dim chr() As Char = e.Mqtt_out.msg.ToCharArray()
    '                Dim n_str As String
    '                For Each ch As Char In chr
    '                    If ch <> vbNullChar Then
    '                        n_str = n_str + ch
    '                    Else
    '                        GoTo End_of_for
    '                    End If
    '                Next
    'End_of_for:
    '                las1.SN = Convert.ToInt32(n_str)
    '            Case "sup/laser/commessa"
    '                las1.batch = e.Mqtt_out.msg
    '                Open_Child_Form(New Laser)
    '            Case "sup/laser/ricetta"
    '                las1.recipe = e.Mqtt_out.msg
    '                Open_Child_Form(New Laser)
    '        End Select

    '    End Sub


    Public Sub Send_to(ByVal sender As Object, ByVal e As Graphics_update) Handles cycle_1.Graphics_update
        RaiseEvent Show_My_Message(e)
    End Sub

    Private Sub IconButton5_Click(sender As Object, e As EventArgs) Handles IconButton5.Click
        'opc1.OPC_Write(Nodo_fine_las1, True)
        'opc1.OPC_Write(Nodo_fine_las2, True)
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles IconButton6.Click
        Open_Child_Form(New Recipe_management_dlg())
        form_selected = "RECIPE"
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'opc1.OPC_Disconnect()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'opc1.OPC_Disconnect()
    End Sub

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        opc1.OPC_Write(NODO_FINE_LASER, False)
        opc1.OPC_Write(NODO_RISULTATO_LASER, False)
    End Sub

End Class
