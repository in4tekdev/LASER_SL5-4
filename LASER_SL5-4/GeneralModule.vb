Imports System.Drawing.Text
Imports System.Drawing
Imports IpgMarkingGraphicsLibrary
Imports System.Security.Policy

Public Module GeneralModule
    Public las1 As laser_str
    Public msg_ans_queue As Queue = New Queue()
    Public com_ch1 As com_status
    Public Plc_command As plc_bit_send
    Public Plc_status As Plc_bit_receive
    Public plc_send As plc_bit_send
    Public las_answ As laser_results
    Public WithEvents Plc_1 As PLC
    Public Message_for_log As New Queue
    'Public log As New Queue
    Public mqtt_topics(5) As String
    Public temp_string As String
    Public Mqtt_queue As Queue
    Public cycle_enum As cycle_status
    Public previous_status As cycle_status
    Public start_manuale As Boolean = False
    Public form_selected As String
    Public hidden_form As Form
    Public form_to_open As String


    ' DATI GRAFICA
    Public focaleLaser As Int16 = 110
    Public magnifier As Int16 = 8
    Public center As Int16 = CInt(focaleLaser * magnifier / 2)

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~v
    'VARIABILI OPCUA
    Public subs_cnt As Int16
    'Public Nodo_fine_las1 As String = "GVL_Laser_Opcua.MARCATURA_FATTA_OPCUA"
    'Public Nodo_fine_las2 As String = "GVL_Opcua.LASERATURA_2_ESEGUITA"
    'Public Nodo_las1_ready As String = "GVL_Laser_Opcua.PRONTA_PER_LASER_OPCUA"
    'Public Nodo_las2_ready As String = "GVL_Opcua.PRONTO_PER_LASERATURA_2"
    'Public Nodo_seriale As String = "575294216"

    Public NODO_START_LASER As String = Chr(34) + "ScambioDati" + Chr(34) + "." + Chr(34) + "ST11_Marcatura" + Chr(34) + "." + Chr(34) + "OUT" + Chr(34) + "." + Chr(34) + "Start" + Chr(34)
    Public NODO_RICETTA_LASER As String = Chr(34) + "ScambioDati" + Chr(34) + "." + Chr(34) + "ST11_Marcatura" + Chr(34) + "." + Chr(34) + "OUT" + Chr(34) + "." + Chr(34) + "Ricetta" + Chr(34)
    Public NODO_ID_LASER As String = Chr(34) + "ScambioDati" + Chr(34) + "." + Chr(34) + "ST11_Marcatura" + Chr(34) + "." + Chr(34) + "OUT" + Chr(34) + "." + Chr(34) + "ID_Pz" + Chr(34)
    Public NODO_SCARTO As String = Chr(34) + "ScambioDati" + Chr(34) + "." + Chr(34) + "ST11_Marcatura" + Chr(34) + "." + Chr(34) + "OUT" + Chr(34) + "." + Chr(34) + "RichPzScarto" + Chr(34)
    Public NODO_COMMESSA_LASER As String = Chr(34) + "ScambioDati" + Chr(34) + "." + Chr(34) + "ST11_Marcatura" + Chr(34) + "." + Chr(34) + "OUT" + Chr(34) + "." + Chr(34) + "Commessa" + Chr(34)


    Public NODO_RISULTATO_LASER As String = Chr(34) + "ScambioDati" + Chr(34) + "." + Chr(34) + "ST11_Marcatura" + Chr(34) + "." + Chr(34) + "IN" + Chr(34) + "." + Chr(34) + "EsitoBuo" + Chr(34)
    Public NODO_LASERATO_LASER As String = Chr(34) + "ScambioDati" + Chr(34) + "." + Chr(34) + "ST11_Marcatura" + Chr(34) + "." + Chr(34) + "IN" + Chr(34) + "." + Chr(34) + "StringaMarcata" + Chr(34)
    Public NODO_FINE_LASER As String = Chr(34) + "ScambioDati" + Chr(34) + "." + Chr(34) + "ST11_Marcatura" + Chr(34) + "." + Chr(34) + "IN" + Chr(34) + "." + Chr(34) + "Terminato" + Chr(34)

    Public Const MAX_TRY_WRITE_OPC As Int16 = 3

    ' Public WithEvents Ipg_laser As Ipg_laser_class

    Public Function Start_Laser_String(Recipe As String, Laser_type As String, Batch As String, SN As Int32, Position As Int32, String_infos As String)
        Dim str_to_send As String
        Dim str_frm As String
        str_frm = "000000"
        str_to_send = str_to_send + Chr(2)
        str_to_send = str_to_send + Recipe
        str_to_send = str_to_send + Chr(23)
        str_to_send = str_to_send + Laser_type
        str_to_send = str_to_send + Chr(23)
        If (IsNothing(Batch) = True Or Batch = "") Then
            Batch = " "
            str_to_send = str_to_send + Batch
        Else
            str_to_send = str_to_send + Batch
        End If
        str_to_send = str_to_send + Chr(23)
        str_to_send = str_to_send + SN.ToString(str_frm)
        str_to_send = str_to_send + Chr(23)
        If (IsNothing(String_infos) = False Or String_infos <> "") Then
            str_to_send = str_to_send + Chr(23)
            str_to_send = str_to_send + Position.ToString
            str_to_send = str_to_send + Chr(23)
            str_to_send = str_to_send + String_infos
        Else
            str_to_send = str_to_send + Chr(23)
            str_to_send = str_to_send + " "
            str_to_send = str_to_send + Chr(23)
            str_to_send = str_to_send + las1.position.ToString()
        End If
        str_to_send = str_to_send + Chr(3).ToString

        Dim msg As Byte() = System.Text.Encoding.ASCII.GetBytes(str_to_send)
        Return msg
    End Function


    Structure laser_str
        Dim recipe As String
        Dim laser_type As Boolean
        Dim batch As String
        Dim SN As Int32
        Dim string_infos As String
        Dim position As Int32
        Dim start_laser As Boolean
        Dim laser_pointer As Boolean
        Dim TrackID As String
        Dim DMtext As String
    End Structure

    Structure com_status
        Dim Tcp_server_running As Boolean
        Dim Tcp_client_connected As Boolean
        Dim Plc_connected As Boolean
    End Structure

    Public Enum Laser_st
        LASER_IDLE = 0
        LASER_READY_1
        LASER_WAIT_END_1
        LASER_WAIT_2
        LASER_READY_2
        LASER_WAIT_END_2
        LASER_END
        LASER_WAIT_STATION_FREE
        LASER_PREPARE_NEXT
    End Enum



    Public Structure plc_bit_send



        Public fine_laser_1 As Boolean
        Public fine_laser_2 As Boolean



    End Structure

    Public Enum plc_send_bit_enum
        FINE_LASER_1 = &H40
        FINE_LASER_2 = &H80
    End Enum

    Public Structure Plc_bit_receive
        Public pronto_per_laser_1 As Boolean
        Public pronto_per_laser_2 As Boolean
        Public fine_laseratura As Boolean
    End Structure

    Public Enum plc_bit_receive_enum
        PRONTO_PER_LASER_1 = &H10
        PRONTO_PER_LASER_2 = &H20
    End Enum

    Public Enum laser_results
        LASER_OK = &H41
        INITIALIZAION_ERROR = &H42
        EXCEPTION_ERROR = &H43
        COMMUNICATION_ERROR = &H44
        COMMAND_RECEIVED = &H52
    End Enum
    Public Enum cycle_status
        IDLE
        START_LAS_1
        WAIT_END_LAS_1
        WAIT_START_LAS_2
        START_LAS_2
        WAIT_END_LAS_2
        INITIALIZE
        ERROR_EXIT
    End Enum


#Region "costanti"

    Public Const VIRGOLA As Char = ","c
    Public Const PUNTO As Char = "."c

#End Region

#Region "log"
    Public WithEvents Logs As Log_File_Thread
    Public Log_queue As New Queue
#End Region

#Region "configurazione"

    Public Separatore_decimale As Char
    Public Station_settings As station_struct
    Public Last_recipe As String
    Public Topics As String()

#End Region


    Public Structure station_struct

        Public Db_conn_string As String
        Public StationNumber As Integer
        Public ProductionSite As String
        Public CommunicationActive As Boolean
        Public CommunicationType As String
        Public DeviceName As String
        Public DevicePassword As String
        Public DeviceIp As String
        Public DevicePort As String
        Public Rotation As Boolean
        Public Path As String
        Public StationNumber_m As Integer

        Public StationActive As Boolean
        Public LaserName As String
        Public LaserIP As String
        Public ServerIP As String
        Public ServerPort As Integer
        Public ApplicationLogPath As String
        Public Description As String
        Public Hw_enable As Integer

    End Structure





    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''             FINE DICHIARAZIONE VARIABILI PROGRAMMA LUCA       ''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '


#Region "ack"
    'Public Ended_sent_time As DateTime
    Public N_try As Integer = 0
    Public Ack_received As Boolean = True

#End Region



#Region "costanti"

    Public Const PORTA_COMUNICAZIONE As Integer = 13000
    Public Const STX As Byte = &H2
    Public Const ETX As Byte = &H3
    Public Const ACK As Byte = &H6
    Public Const NAK As Byte = &H15
    Public Const ETB As Byte = &H17
    Public Const LASER_ENDED As Byte = &H41
    Public Const LASER_INIT_ERROR As Byte = &H42
    Public Const LASER_EXCEPTION As Byte = &H43
    Public Const COMMUNICATION_ERROR As Byte = &H44
    Public Const RECEIVED_START As Byte = &H52

#End Region


#Region "recipe"

    Public Recipe As recipe_data
    Public edit_Recipe As recipe_data

#End Region

#Region "log"

    'Public Log_queue As New Queue

#End Region

#Region "laser_enum"

    Public Font_items As Array
    Public Font_items_idx As Array

    Public Fill_direction_items As Array
    Public Fill_direction_items_idx As Array

    Public Fill_type_items As Array
    Public Fill_type_items_idx As Array

    Public Optimization_items As Array
    Public Optimization_items_idx As Array

#End Region

    Public Function Carica_ricetta(ByRef rec As recipe_data, station_num As Integer) As Boolean

        Dim ans As Boolean = False

        If (Load_recipe(rec, station_num) = True) Then
            ans = True
            ans = ans And Load_recipe_item(rec.Line_1, rec.Line_items(0))
            ans = ans And Load_recipe_item(rec.Line_2, rec.Line_items(1))
            ans = ans And Load_recipe_item(rec.Line_3, rec.Line_items(2))
            ans = ans And Load_recipe_item(rec.Line_4, rec.Line_items(3))
            ans = ans And Load_recipe_item(rec.Line_5, rec.Line_items(4))
            ans = ans And Load_recipe_item(rec.Line_6, rec.Line_items(5))
            ans = ans And Load_recipe_item(rec.Line_7, rec.Line_items(6))
            ans = ans And Load_recipe_item(rec.Line_8, rec.Line_items(7))
            ans = ans And Load_recipe_item(rec.Serial, rec.Line_items(9))
            ans = ans And Load_recipe_item(rec.Logo_1, rec.Logo_items(0))
            ans = ans And Load_recipe_item(rec.Logo_2, rec.Logo_items(1))
            ans = ans And Load_recipe_item(rec.Logo_3, rec.Logo_items(2))
            ans = ans And Load_recipe_item(rec.Logo_4, rec.Logo_items(3))
            ans = ans And Load_recipe_item(rec.Datamatrix_1, rec.Datamatrix_items(0))
            ans = ans And Load_recipe_item(rec.Datamatrix_2, rec.Datamatrix_items(1))

        Else
            ans = False
        End If

        Return ans

    End Function

    <Serializable()> Public Structure recipe_data

        Public Recipe_name As String
        Public Recipe_description As String
        Public Recipe_active As Boolean
        Public Associated_station As Integer

        Public Line_1 As Integer
        Public Line_2 As Integer
        Public Line_3 As Integer
        Public Line_4 As Integer
        Public Line_5 As Integer
        Public Line_6 As Integer
        Public Line_7 As Integer
        Public Line_8 As Integer
        Public Serial As Integer
        Public Logo_1 As Integer
        Public Logo_2 As Integer
        Public Logo_3 As Integer
        Public Logo_4 As Integer
        Public Datamatrix_1 As Integer
        Public Datamatrix_2 As Integer
        Public Cloning As Integer
        Public X_cloning As Single
        Public Y_cloning As Single
        Public Enable_laser_on_back As Boolean

        Public Line_items() As single_item_struct
        Public Logo_items() As single_item_struct
        Public Datamatrix_items() As single_item_struct

    End Structure

    Public Structure single_item_struct
        Public idx As Integer
        Public item_type As Integer     '0-->disabled, 1-->text, 2-->logo, 3-->DataMatrix
        Public laser_on_back As Boolean
        'parametri per il testo
        Public t_font As HersheyFont
        Public t_fontName As String
        Public t_text As String
        Public t_before As String
        Public t_after As String
        Public t_x_pos As Single
        Public t_y_pos As Single
        Public t_x_scale As Single
        Public t_y_scale As Single
        Public t_z_scale As Single
        Public t_x_rotate As Single
        Public t_y_rotate As Single
        Public t_z_rotate As Single
        Public t_height As Single
        Public t_energy As Single
        Public t_pitch As Single
        'parametri per il logo
        Public l_path As String
        Public l_x_pos As Single
        Public l_y_pos As Single
        Public l_x_scale As Single
        Public l_y_scale As Single
        Public l_z_scale As Single
        Public l_x_rotate As Single
        Public l_y_rotate As Single
        Public l_z_rotate As Single
        Public l_energy As Single
        Public l_pitch As Single
        'parametri per il DataMatrix
        Public dm_text As String
        Public dm_before As String
        Public dm_after As String
        Public dm_inverted As Boolean
        Public dm_size As Single
        Public dm_border As Single  'spessore bordo per dm invertito
        Public dm_x_pos As Single
        Public dm_y_pos As Single
        Public dm_z_rotate As Single
        Public dm_fill_angle As Single
        Public dm_fill_spacing As Single
        Public dm_beam_diameter As Single
        Public dm_direction As FillDirection
        Public dm_grid_type As FillType
        Public dm_pitch As Single
        Public dm_energy As Single
        Public dm_optimization As Optimization
        Public txt_rounded As Boolean
        Public txt_round_dim As Single
    End Structure


    Public Structure LaserStringRecipe
        Dim text As String
        Dim x_pos As Int16
        Dim y_pos As Int16
        Dim x_scale As Double
        Dim y_scale As Double
        Dim textSize As Double
        Dim x_rotate As Double
        Dim rounded_text As Double
        Dim font As String
        Dim enable As Boolean
    End Structure

    Public drawText(9) As LaserStringRecipe


#Region "Fontcheck"

    ' Function to check if a font exists on the system
    Public Function FontExists(ByVal fontName As String) As Boolean
        ' Create a collection of installed fonts
        Dim installedFonts As New InstalledFontCollection()

        ' Iterate through the installed fonts
        For Each fontFamily As FontFamily In installedFonts.Families
            If fontFamily.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase) Then
                Return True ' Font found
            End If
        Next

        Return False ' Font not found
    End Function

#End Region

End Module
