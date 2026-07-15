Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports ModbusTCP

Public Class PLC
    Public value_readed As String

    Friend WithEvents MBmaster As Master
    Public plc_loop As Thread
    Public Sub New(plc_ip_addr As String, plc_port As Int16)
        MBmaster = New Master(plc_ip_addr, plc_port, True)
        com_ch1.Plc_connected = True
        AddHandler MBmaster.OnResponseData, AddressOf MBmaster_OnResponseData
        AddHandler MBmaster.OnException, AddressOf MBmaster_OnException
    End Sub

    Private Sub MBmaster_OnException(ID As UInt16, unit As Byte, funct As Byte, exception As Byte)
        Dim err_str As String = ""
        Select Case exception

            Case Master.excIllegalFunction

                err_str = "Illegal function!"

            Case Master.excIllegalDataAdr

                err_str = "Illegal data adress!"

            Case Master.excIllegalDataVal

                err_str = "Illegal data value!"

            Case Master.excSlaveDeviceFailure

                err_str = "Slave device failure!"

            Case Master.excAck

                err_str = "Acknoledge!"

            Case Master.excGatePathUnavailable

                err_str = "Gateway path unavailbale!"

            Case Master.excExceptionTimeout

                err_str = "Slave timed out!"
                com_ch1.Plc_connected = False
            Case Master.excExceptionConnectionLost

                err_str = "Connection is lost!"
                com_ch1.Plc_connected = False
            Case Master.excExceptionNotConnected

                err_str = "Not connected!"
                com_ch1.Plc_connected = False

        End Select



        'RichTextBox2.Text = err_str
    End Sub

    Private Sub MBmaster_OnResponseData(ID As UInt16, unit As Byte, funct As Byte, values As Byte())

        Dim w0 As UInt16
        'Dim i As Integer
        Dim str As String
        ', w2 As Int16
        'Dim w10 As UInt16
        ', w3 As UInt16

        ' Seperate calling thread
        '---------------------------------------------------------------

        ' Identify requested data
        Select Case ID
            Case 1      'read coils

            Case 2      'Read discrete inputs


            Case 3      'Read holding register

                w0 = values(1) * 256 + values(0)
                Debug.Print(w0)
                str = ""

                If (w0 And plc_bit_receive_enum.PRONTO_PER_LASER_1) Then
                    Plc_status.pronto_per_laser_1 = True
                    If value_readed <> values(0).ToString("X2") Then
                        Log_queue.Enqueue("FROM PLC; " + Now.ToString + ":" + Now.Millisecond.ToString + " :" + values(0).ToString("X2"))
                        value_readed = values(0).ToString("X2")
                    End If
                Else
                        Plc_status.pronto_per_laser_1 = False
                End If

                If (w0 And plc_bit_receive_enum.PRONTO_PER_LASER_2) Then
                    Plc_status.pronto_per_laser_2 = True
                    If value_readed <> values(0).ToString("X2") Then
                        Log_queue.Enqueue("FROM PLC; " + Now.ToString + ":" + Now.Millisecond.ToString + " :" + values(0).ToString("X2"))
                        value_readed = values(0).ToString("X2")
                    End If
                Else
                    Plc_status.pronto_per_laser_2 = False
                End If

            Case 4      'Read input register

            Case 5      'Write single coil

            Case 6      'Write multiple coils

            Case 7      'Write single register

            Case 8      'Write multiple register

        End Select
    End Sub

    Public Sub Write_register(bit_to_send As plc_bit_send)

        Dim ID As UInt16 = 7
        Dim unit As Byte = 1
        Dim StartAddress As UInt16 = 0
        Dim data_byte(1) As Byte

        data_byte(0) = 0
        data_byte(1) = 0

        If (Plc_command.fine_laser_1) Then
            data_byte(0) = data_byte(0) Or plc_send_bit_enum.FINE_LASER_1
        Else
            data_byte(0) = data_byte(0) And Not (plc_send_bit_enum.FINE_LASER_1)
        End If

        If (Plc_command.fine_laser_2) Then
            data_byte(0) = data_byte(0) Or plc_send_bit_enum.FINE_LASER_2
        Else
            data_byte(0) = data_byte(0) And Not (plc_send_bit_enum.FINE_LASER_2)
        End If
        Log_queue.Enqueue("TO PLC; " + Now.ToString + ":" + Now.Millisecond.ToString + " :" + data_byte(0).ToString("X2"))
        MBmaster.WriteSingleRegister(ID, unit, StartAddress, data_byte)

    End Sub
    Public Sub Plc_Thread()
        Dim ID As UShort = 3
        Dim unit As Byte = 1
        Dim start_address As UShort = 0
        Dim lenght As UInt16 = 1
        plc_send.fine_laser_1 = False
        plc_send.fine_laser_2 = False
        Me.Write_register(plc_send)
        While (True)
            Thread.Sleep(100)

            'Dim start_address As UShort = 0
            'Dim lenght As UInt16 = 10
            Try
                MBmaster.ReadHoldingRegister(ID, unit, start_address, lenght)
            Catch ex As Exception

                'TODO: gestire eccezione di comunicazione con PLC
                'Add_log_element("Timer1 ReadHoldingRegister EXCEPTION:", ex.Message)
            End Try

            'Laser_SM()

        End While
    End Sub


End Class
