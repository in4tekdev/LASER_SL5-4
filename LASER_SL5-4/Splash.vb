Imports Microsoft.VisualBasic.Devices
Imports System.IO
Imports Microsoft.VisualBasic.FileIO


Public Class Splash

    Dim out_str As String = ""

    Private Sub Splash_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Load_settings() Then

            MsgBox("Unable to load config file")
            End

        End If


        If (Not Startup_sequence()) Then
            GroupBox1.Visible = True
            TextBox1.Text = Station_settings.Db_conn_string
            Exit Sub
        End If

        Timer1.Enabled = True

    End Sub

    Private Function Load_settings() As Boolean

        Dim ans As Boolean = False

        If (File.Exists(Application.StartupPath & "\config.txt")) Then  'il file esiste

            Dim fileReader As String
            fileReader = FileSystem.ReadAllText(Application.StartupPath & "/config.txt")

            Try
                'la prima riga è la stringa di connessione al database, la seconda è la station number
                Dim str() As String = fileReader.Split(ControlChars.CrLf)
                Station_settings.Db_conn_string = str(0)
                Station_settings.StationNumber = str(1)
                Station_settings.StationNumber_m = str(2)
                Station_settings.Hw_enable = str(3)
                ans = True
            Catch ex As Exception
                ans = False
            End Try

        Else
            ans = False
        End If

        Return ans

    End Function

    Private Function Check_for_db() As Boolean

        Dim my_db_ver As New db_ver
        Dim ans As Boolean = False

        Get_db_version(my_db_ver)

        If (my_db_ver.valid) Then
            out_str = "Ver. " & my_db_ver.version & ControlChars.CrLf & "Date: " & my_db_ver.date_time.ToString & ControlChars.CrLf & my_db_ver.note & ControlChars.CrLf
            ans = True
            GroupBox1.Visible = False
        Else
            out_str = "Unable to connect to database"
            GroupBox1.Visible = True
            TextBox1.Text = Station_settings.Db_conn_string
            Label2.Text = my_db_ver.exception
            ans = False
        End If

        Return ans

    End Function

    Private Function Startup_sequence() As Boolean

        Dim ans As Boolean = True

        If (Check_for_db()) Then
            RTB1.Text = out_str
            If Not (Load_station(Station_settings.StationNumber) Or Not (Load_station_m(Station_settings.StationNumber_m))) Then
                MsgBox("Unable to load station settings")
                ans = False
            End If

            'esegue il ping a tutti gli indirizzi caricati per questa stazione
            out_str = out_str & ControlChars.CrLf

            'laser
            'If (My.Computer.Network.Ping(Station_settings.DeviceIp, 1000)) Then
            '    out_str = out_str & Station_settings.DeviceIp & " OK" & ControlChars.CrLf
            'Else
            '    out_str = out_str & Station_settings.DeviceIp & " FAIL CONNECTING PLC !!!!!!!!!!!!!!!!!!!" & ControlChars.CrLf
            '    ans = False
            'End If

            If (My.Computer.Network.Ping(Station_settings.LaserIP, 1000)) Then
                out_str = out_str & Station_settings.LaserIP & " OK" & ControlChars.CrLf
            Else
                out_str = out_str & Station_settings.LaserIP & " FAIL CONNECTING LASER !!!!!!!!!!!!!!!!!!!" & ControlChars.CrLf
                ans = False
            End If
            RTB1.Text = out_str

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'forzatura per debug
            ans = True
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Else
            RTB1.Text = out_str
            ans = False
        End If

        Return ans

        Return ans

    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False

        Me.Hide()

        Form1.Show()

        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'esci dal programma
        Application.Exit()
    End Sub

End Class