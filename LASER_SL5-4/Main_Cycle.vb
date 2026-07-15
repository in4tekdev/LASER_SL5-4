Imports System.Reflection.Emit
Imports System.Threading
Imports System.Web.UI.WebControls
Imports System.Windows.Controls
Imports System.Windows.Interop
Imports System.Collections.Specialized.BitVector32
Imports IpgMarkingGraphicsLibrary

Public Class Error_comm
    Inherits System.EventArgs
    Public err_to_send As String
    Public dm_text As String
    Public Sub New(err As String, dm As String)
        MyBase.New
        err_to_send = err
        dm_text = dm
    End Sub
End Class

Public Class msg_to_plc
    Inherits System.EventArgs
    Public msg_to_send As plc_message
    Public Sub New(msg As plc_message)
        MyBase.New
        msg_to_send.fine_las_1 = msg.fine_las_1
        msg_to_send.fine_las_2 = msg.fine_las_2
    End Sub
End Class

Public Class Laser_Update
    Inherits System.EventArgs

    Public graphics As Graphics_update

    Public Sub New(argu As Graphics_update)
        MyBase.New
        graphics.recipe = argu.recipe
        graphics.serial = argu.serial
        graphics.batch = argu.batch
        graphics.opt = argu.opt
        graphics.last = argu.last
        graphics.laser_stat = argu.laser_stat
        graphics.fine_las_1 = argu.fine_las_1
        graphics.fine_las_2 = argu.fine_las_2
    End Sub
End Class


Public Class Main_Cycle

    Public Event Graphics_update(ByVal sender As Object, ByVal e As Graphics_update)
    Public Event Comm_to_plc(ByVal sender As Object, ByVal e As plc_message)
    Public WithEvents Ipg_laser As Ipg_laser_class
    Public Event error_msg_exchange(ByVal sender As Object, ByVal e As Error_comm)
    Public laserStartTime As DateTime
    Public nRetry As Int16

    Public main_loop As Thread

    Public Sub New()
    	try
        cycle_enum = cycle_status.INITIALIZE
        If (Station_settings.Hw_enable = 1) Then
            Ipg_laser = New Ipg_laser_class(Station_settings.LaserName)
            Ipg_laser.Client_ipg = New Thread(AddressOf Ipg_laser.Client_thread_ipg)
            Ipg_laser.Client_ipg.Start()

            ReDim Recipe.Line_items(9)
            ReDim Recipe.Logo_items(9)
            ReDim Recipe.Datamatrix_items(9)

            ReDim edit_Recipe.Line_items(9)
            ReDim edit_Recipe.Logo_items(9)
            ReDim edit_Recipe.Datamatrix_items(9)


            Font_items_idx = System.Enum.GetValues(GetType(HersheyFont))
            Font_items = System.Enum.GetNames(GetType(HersheyFont))

            Fill_direction_items = System.Enum.GetValues(GetType(FillDirection))
            Fill_direction_items_idx = System.Enum.GetNames(GetType(FillDirection))

            Fill_type_items = System.Enum.GetValues(GetType(FillType))
            Fill_type_items_idx = System.Enum.GetNames(GetType(FillType))

            Optimization_items = System.Enum.GetValues(GetType(Optimization))
            Optimization_items_idx = System.Enum.GetNames(GetType(Optimization))
        End If
Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub

    Protected Overridable Sub on_new_error(ByVal e As Error_comm)
        RaiseEvent error_msg_exchange(Me, e)
    End Sub
    Public Sub main_thread()
    	try
        cycle_enum = cycle_status.INITIALIZE
        previous_status = cycle_status.INITIALIZE
        While (True)
            Thread.Sleep(150)
            If start_manuale = True Then
                start_manuale = False
                las1.laser_type = False
                Recipe.Recipe_name = las1.recipe
                If (Carica_ricetta(Recipe, Station_settings.StationNumber) = True) Then
                    las1.SN = GetSerial(las1.recipe, las1.batch)
                    If las1.SN > 0 Then
                        '# composizione stringa e datamatrix
                        Dim Anno_produzione As String = (Now.Year - 2000).ToString("D2")
                        Dim Giorno_giuliano As String = Now.DayOfYear.ToString("D3")
                        Dim datamatrix As String
                            las1.position = 2
                            'productionSite viene letto dal DB tabella STATION_LASER solo 1 volta all'avvio dell'interfaccia!
                            las1.string_infos = Station_settings.ProductionSite + Anno_produzione + Giorno_giuliano + las1.SN.ToString("000000")
                        Recipe.Line_items(las1.position - 1).t_text = las1.string_infos
                        datamatrix = ":" + Station_settings.ProductionSite + Anno_produzione + Giorno_giuliano + las1.SN.ToString("000000")

                        '' STRINGA PER LASER PT DI TESLA SU POSIZONE 6
                        las1.position = 6
                        las1.string_infos = "ITALY_" + Anno_produzione + Giorno_giuliano
                            Recipe.Line_items(las1.position - 1).t_text = las1.string_infos
                            Ipg_laser.Add_ipg_cmd(las1.laser_type, las1.laser_pointer, las1.batch + las1.SN.ToString("00000"), datamatrix)
                            cycle_enum = cycle_status.WAIT_END_LAS_1
                    Else
                        cycle_enum = cycle_status.ERROR_EXIT
                    End If
                End If
            End If

            If las_answ = laser_results.INITIALIZAION_ERROR Then
                on_new_error(New Error_comm("INIT ERROR", ""))
                las_answ = vbNull
            End If

            '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            If cycle_enum = cycle_status.INITIALIZE Then
                'LASER INITIALIZE METTO TUTTO A FALSE
                If previous_status = cycle_status.WAIT_END_LAS_1 Or previous_status = cycle_status.WAIT_END_LAS_2 Then
                    cycle_enum = cycle_status.IDLE
                End If
                cycle_enum = cycle_status.IDLE
                previous_status = cycle_status.INITIALIZE
                '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            ElseIf cycle_enum = cycle_status.IDLE Then

                If Plc_status.pronto_per_laser_1 = True Then
                    cycle_enum = cycle_status.START_LAS_1
                    Plc_status.pronto_per_laser_1 = False
                    previous_status = cycle_status.IDLE
                End If

                '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            ElseIf cycle_enum = cycle_status.START_LAS_1 Then
                'LASERO DAVANTI
                If (las1.recipe = "" Or IsNothing(las1.recipe)) Then
                    'ERRORE RICETTA NON PRESENTA
                    Continue While
                End If
                las1.laser_type = False
                Recipe.Recipe_name = las1.recipe


                If (Carica_ricetta(Recipe, Station_settings.StationNumber) = True) Then

                    ' STR AGGIUNTIVA DA INIZIALIZZARE SE SERVE
                    las1.SN = GetSerial(las1.recipe, las1.batch)

                    If las1.SN > 0 Then

                        ' STRINGA PER LASER PT DI TESLA SU POSIZONE 2
                        Dim Anno_produzione As String = (Now.Year - 2000).ToString("D2")
                        Dim Giorno_giuliano As String = Now.DayOfYear.ToString("D3")
                        Dim datamatrix As String
                        las1.position = 2
                        las1.string_infos = Station_settings.ProductionSite + Anno_produzione + Giorno_giuliano + las1.SN.ToString("000000")
                        Recipe.Line_items(las1.position - 1).t_text = las1.string_infos
                        datamatrix = ":" + Station_settings.ProductionSite + Anno_produzione + Giorno_giuliano + las1.SN.ToString("000000")


                        '' STRINGA PER LASER PT DI TESLA SU POSIZONE 6
                        las1.position = 6
                        las1.string_infos = "ITALY_" + Anno_produzione + Giorno_giuliano
                            Recipe.Line_items(las1.position - 1).t_text = las1.string_infos
                            Ipg_laser.Add_ipg_cmd(las1.laser_type, las1.laser_pointer, las1.batch + las1.SN.ToString("00000"), datamatrix)
                            cycle_enum = cycle_status.WAIT_END_LAS_1
                    Else
                        cycle_enum = cycle_status.ERROR_EXIT
                    End If

                Else
                    cycle_enum = cycle_status.ERROR_EXIT
                End If
                '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            ElseIf cycle_enum = cycle_status.WAIT_END_LAS_1 Then
                If las_answ = vbNull Or las_answ = 0 Then
                    Continue While
                ElseIf (las_answ = laser_results.LASER_OK) Then
                    If Station_settings.Rotation = True Then

                    ElseIf Station_settings.Rotation = False Then
                        on_new_error(New Error_comm("NO ERROR", las1.DMtext))
                        'on_plc_status_change(New plc_message With {.fine_las_1 = True, .fine_las_2 = False})
                        previous_status = cycle_status.WAIT_END_LAS_1
                        cycle_enum = cycle_status.IDLE
                    End If
                    las_answ = vbNull
                ElseIf (las_answ = laser_results.EXCEPTION_ERROR) Then
                    on_new_error(New Error_comm("EX ERROR", ""))
                    cycle_enum = cycle_status.ERROR_EXIT
                End If

                '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


                '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


                '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            ElseIf cycle_enum = cycle_status.ERROR_EXIT Then
                on_new_error(New Error_comm("COM ERROR", ""))

                on_plc_status_change(New plc_message With {.fine_las_1 = True, .fine_las_2 = True})
                las_answ = vbNull
                cycle_enum = cycle_status.INITIALIZE
            End If
        End While

Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub

    Public Sub Laser_msg(ByVal sender As Object, ByVal e As Laser_EventArgs) Handles Ipg_laser.Ipg_laser_event

        If (e.Laser_stat.msg_target = laser_target_enu.INIZIATO) Then
            Logs.Add_log_element(e.Laser_stat.msg_str, "")
            on_graphics_update(New Graphics_update With {.recipe = las1.recipe, .serial = las1.SN, .batch = las1.batch, .opt = "", .last = Now.ToString, .laser_stat = "LASER RUNNING ..."})
        ElseIf (e.Laser_stat.msg_target = laser_target_enu.FINITO) Then
            las_answ = laser_results.LASER_OK
            on_graphics_update(New Graphics_update With {.recipe = "---", .serial = "---", .batch = "---", .opt = "---", .last = "---", .laser_stat = "LASER FREE ..."})
            Logs.Add_log_element(e.Laser_stat.msg_str, "")
        ElseIf (e.Laser_stat.msg_target = laser_target_enu.INIZIALIZZATO) Then
            on_graphics_update(New Graphics_update With {.recipe = "---", .serial = "---", .batch = "---", .opt = "---", .last = "---", .laser_stat = "LASER INITIALIZED ..."})
            Logs.Add_log_element(e.Laser_stat.msg_str, "")
        ElseIf (e.Laser_stat.msg_target = laser_target_enu.ECCEZIONE) Then
            las_answ = laser_results.EXCEPTION_ERROR
            Logs.Add_log_element(e.Laser_stat.msg_str, "")
        ElseIf (e.Laser_stat.msg_target = laser_target_enu.ERRORE_INIZIALIZZAZIONE) Then
            'Communication.Add_msg_to_send(LASER_INIT_ERROR)
            Logs.Add_log_element(e.Laser_stat.msg_str, "")
        End If
    End Sub

    Public Overridable Sub on_graphics_update(ByVal e As Graphics_update)
        RaiseEvent Graphics_update(Me, e)
    End Sub

    Public Overridable Sub on_plc_status_change(ByVal e As plc_message)
        RaiseEvent Comm_to_plc(Me, e)
    End Sub
End Class


Public Structure Graphics_update
    Public recipe As String
    Public serial As String
    Public batch As String
    Public opt As String
    Public last As String
    Public laser_stat As String
    Public fine_las_1 As Boolean
    Public fine_las_2 As Boolean
End Structure

Public Structure plc_message
    Public fine_las_1 As Boolean
    Public fine_las_2 As Boolean
End Structure