Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Specialized.BitVector32

Module Database_module

    Public Sub Get_db_version(ByRef versione As db_ver)

        'recupera la versione del database
        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            cn.Open()

            ' Set up a data set command object.
            Dim strSelect As String = "SELECT * FROM DB_VERSION"

            Dim cmd As New SqlCommand(strSelect, cn)

            Dim rdr As SqlDataReader

            rdr = cmd.ExecuteReader
            If (rdr.HasRows) Then
                While rdr.Read()    'scorriamo fino all'ultima

                    versione.version = rdr("db_version")
                    versione.date_time = rdr("release_date")
                    versione.note = rdr("note")

                End While
                versione.valid = True
                versione.exception = "Ok"

            Else    'non ci sono righe; non deve succedere, ma per sicurezza mettiamo dei valori
                versione.version = "No row available"
                versione.exception = "No row available"
                versione.date_time = New DateTime(1900, 1, 1, 23, 59, 59)
                versione.note = "No row available"
                versione.valid = False
            End If

            cn.Close()

        Catch ex As Exception
            versione.valid = False
            versione.exception = ex.Message
            versione.date_time = Now
            versione.note = "No data available"
        End Try

    End Sub

    Public Function Load_station(station_number As Integer) As Boolean
        'ritorna true se la stazione è stata trovata ed è attiva, false altrimenti
        Dim ans As Boolean = False

        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            cn.Open()

            ' Set up a data set command object.
            Dim strSelect As String = "SELECT * FROM [COM_TYPE_LASER] WHERE StationNumber= @stat_num"

            Dim cmd As New SqlCommand(strSelect, cn)
            cmd.Parameters.AddWithValue("@stat_num", station_number)

            Dim rdr As SqlDataReader

            rdr = cmd.ExecuteReader
            If (rdr.HasRows) Then

                While rdr.Read()    'scorriamo fino all'ultima

                    If (rdr("CommunicationActive") = True) Then
                        Station_settings.StationNumber = rdr("StationNumber") - 50
                        Station_settings.CommunicationActive = rdr("CommunicationActive")
                        Station_settings.CommunicationType = rdr("CommunicationType")
                        'Station_settings.DeviceName = rdr("DeviceName")
                        'Station_settings.DevicePassword = rdr("DevicePassword")
                        Station_settings.DeviceIp = rdr("DeviceIp")
                        Station_settings.DevicePort = rdr("DevicePort")
                        Station_settings.Rotation = rdr("Rotation")
                        Station_settings.Path = rdr("Path")

                        'Station_settings.DeviceName = Station_settings.DeviceName.Trim
                        Station_settings.DeviceIp = Station_settings.DeviceIp.Trim
                        Station_settings.DevicePort = Station_settings.DevicePort.Trim
                        Station_settings.Path = Station_settings.Path.Trim


                        ans = True
                        Exit While
                    Else    'stazione non attiva
                        ans = False
                    End If
                End While
            Else    'non ci sono righe; non dovrebbe succedere.....
                ans = False
            End If

            cn.Close()

        Catch ex As Exception
            ans = False
        End Try

        Return ans
    End Function

    Public Function Load_station_m(station_number As Integer) As Boolean
        'ritorna true se la stazione è stata trovata ed è attiva, false altrimenti
        Dim ans As Boolean = False

        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            cn.Open()

            ' Set up a data set command object.
            Dim strSelect As String = "SELECT * FROM STATION_LASER WHERE StationNumber= @stat_num"

            Dim cmd As New SqlCommand(strSelect, cn)
            cmd.Parameters.AddWithValue("@stat_num", station_number)

            Dim rdr As SqlDataReader

            rdr = cmd.ExecuteReader
            If (rdr.HasRows) Then

                While rdr.Read()    'scorriamo fino all'ultima

                    If (rdr("StationActive") = True) Then
                        Station_settings.StationNumber = rdr("StationNumber")
                        Station_settings.StationActive = rdr("StationActive")
                        Station_settings.LaserName = rdr("LaserName")
                        Station_settings.LaserIP = rdr("LaserIP")
                        Station_settings.ServerIP = rdr("ServerIP")
                        Station_settings.ServerPort = rdr("ServerPort")
                        Station_settings.ApplicationLogPath = rdr("ApplicationLogPath")
                        Station_settings.Description = rdr("Description")
                        Station_settings.ProductionSite = rdr("ProductionSite")
                        Station_settings.LaserName = Station_settings.LaserName.Trim
                        Station_settings.LaserIP = Station_settings.LaserIP.Trim
                        Station_settings.ServerIP = Station_settings.ServerIP.Trim
                        Station_settings.ApplicationLogPath = Station_settings.ApplicationLogPath.Trim
                        Station_settings.Description = Station_settings.Description.Trim

                        ans = True
                        Exit While
                    Else    'stazione non attiva
                        ans = False
                    End If
                End While
            Else    'non ci sono righe; non dovrebbe succedere.....
                ans = False
            End If

            cn.Close()

        Catch ex As Exception
            ans = False
        End Try

        Return ans
    End Function

    Public Function Load_recipe(ByRef rec As recipe_data, station_num As Integer) As Boolean
        'carica la ricetta
        'restituisce true se tutto OK, 0 se c'è stata eccezione

        Dim ans As Boolean = False
        Dim strSelect As String

        Try

            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            cn.Open()

            ' Set up a data set command object.
            If (station_num = 99) Then
                strSelect = "SELECT * FROM RECIPE_LASER WHERE Recipe_name=@m0"
            Else
                strSelect = "SELECT * FROM RECIPE_LASER WHERE Recipe_name=@m0 AND Associated_station = @stat"
            End If

            Dim cmd As New SqlCommand(strSelect, cn)
            cmd.Parameters.AddWithValue("@m0", rec.Recipe_name)
            If (station_num <> 99) Then
                cmd.Parameters.AddWithValue("@stat", station_num)
            End If


            Dim rdr As SqlDataReader
            rdr = cmd.ExecuteReader
            If (rdr.HasRows) Then
                rdr.Read()

                rec.Recipe_name = rdr("Recipe_name")
                rec.Recipe_description = rdr("Recipe_description")
                rec.Recipe_active = rdr("Recipe_active")
                rec.Associated_station = rdr("Associated_station")

                rec.Line_1 = rdr("Line_1")
                rec.Line_2 = rdr("Line_2")
                rec.Line_3 = rdr("Line_3")
                rec.Line_4 = rdr("Line_4")
                rec.Line_5 = rdr("Line_5")
                rec.Line_6 = rdr("Line_6")
                rec.Line_7 = rdr("Line_7")
                rec.Line_8 = rdr("Line_8")
                rec.Serial = rdr("Serial")
                rec.Logo_1 = rdr("Logo_1")
                rec.Logo_2 = rdr("Logo_2")
                rec.Logo_3 = rdr("Logo_3")
                rec.Logo_4 = rdr("Logo_4")
                rec.Datamatrix_1 = rdr("Datamatrix_1")
                rec.Datamatrix_2 = rdr("Datamatrix_2")
                rec.Cloning = rdr("Cloning")
                rec.X_cloning = rdr("X_spacing")
                rec.Y_cloning = rdr("Y_spacing")
                rec.Enable_laser_on_back = rdr("Enable_laser_on_back")

                ans = True

            Else    'in realtà questo caso non deve mai verificarsi
                ans = False
            End If

            cn.Close()

        Catch ex As Exception
            MsgBox("Load_recipe: " & ex.Message)
            ans = False
        End Try

        Return ans

    End Function

    Public Function Load_recipe_item(index As Integer, ByRef item As single_item_struct) As Boolean

        'ritorna true se l'item è stato trovato e caricato correttamente, false altrimenti
        Dim ans As Boolean = False

        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            cn.Open()

            ' Set up a data set command object.
            Dim strSelect As String = "SELECT * FROM ITEMS_LASER WHERE Idx= @item_index"

            Dim cmd As New SqlCommand(strSelect, cn)
            cmd.Parameters.AddWithValue("@item_index", index)

            Dim rdr As SqlDataReader

            rdr = cmd.ExecuteReader
            If (rdr.HasRows) Then

                rdr.Read()

                item.idx = rdr("Idx")
                item.item_type = rdr("Item_type")
                item.laser_on_back = rdr("laser_on_back")
                item.t_font = rdr("t_font")
                If IsDBNull(rdr("t_fontName")) Then
                    item.t_fontName = ""
                Else
                    item.t_fontName = rdr("t_fontName")
                End If
                item.t_text = rdr("t_text")
                item.t_before = rdr("t_before")
                item.t_after = rdr("t_after")
                item.t_x_pos = rdr("t_x_pos")
                item.t_y_pos = rdr("t_y_pos")
                item.t_x_scale = rdr("t_x_scale")
                item.t_y_scale = rdr("t_y_scale")
                item.t_z_scale = rdr("t_z_scale")
                item.t_x_rotate = rdr("t_x_rotate")
                item.t_y_rotate = rdr("t_y_rotate")
                item.t_z_rotate = rdr("t_z_rotate")
                item.t_height = rdr("t_height")
                item.t_energy = rdr("t_energy")
                item.t_pitch = rdr("t_pitch")
                item.l_path = rdr("l_path")
                item.l_x_pos = rdr("l_x_pos")
                item.l_y_pos = rdr("l_y_pos")
                item.l_x_scale = rdr("l_x_scale")
                item.l_y_scale = rdr("l_y_scale")
                item.l_z_scale = rdr("l_z_scale")
                item.l_x_rotate = rdr("l_x_rotate")
                item.l_y_rotate = rdr("l_y_rotate")
                item.l_z_rotate = rdr("l_z_rotate")
                item.l_energy = rdr("l_energy")
                item.l_pitch = rdr("l_pitch")
                item.dm_text = rdr("dm_text")
                item.dm_size = rdr("dm_size")
                item.dm_inverted = rdr("dm_inverted")
                item.dm_border = rdr("dm_border")
                item.dm_x_pos = rdr("dm_x_pos")
                item.dm_y_pos = rdr("dm_y_pos")
                item.dm_z_rotate = rdr("dm_z_rotate")
                item.dm_fill_angle = rdr("dm_fill_angle")
                item.dm_fill_spacing = rdr("dm_fill_spacing")
                item.dm_beam_diameter = rdr("dm_beam_diameter")
                item.dm_direction = rdr("dm_direction")
                item.dm_grid_type = rdr("dm_grid_type")
                item.dm_pitch = rdr("dm_pitch")
                item.dm_energy = rdr("dm_energy")
                item.dm_optimization = rdr("dm_optimization")
                item.txt_rounded = rdr("t_rounded")
                item.txt_round_dim = rdr("t_rounded_size")

                ans = True

            Else    'non ci sono righe; non dovrebbe succedere.....
                ans = False
            End If

            cn.Close()

        Catch ex As Exception
            ans = False
        End Try

        Return ans
    End Function

    Public Function Load_topics() As Boolean
        'ritorna true se la stazione è stata trovata ed è attiva, false altrimenti
        Dim ans As Boolean = False

        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            cn.Open()

            ' Set up a data set command object.
            Dim strSelect As String = "SELECT * FROM MQTT_TOPICS"

            Dim cmd As New SqlCommand(strSelect, cn)

            Dim rdr As SqlDataReader

            rdr = cmd.ExecuteReader
            If (rdr.HasRows) Then
                Dim i = 0
                While rdr.Read()    'scorriamo fino all'ultima
                    mqtt_topics(i) = rdr("Topic")
                    i += 1
                    ans = True
                End While
            Else    'non ci sono righe; non dovrebbe succedere.....
                ans = False
            End If

            cn.Close()

        Catch ex As Exception
            ans = False
        End Try
        Return ans
    End Function

    Public Function Get_Last_Config() As Boolean
        'ritorna true se la stazione è stata trovata ed è attiva, false altrimenti
        Dim ans As Boolean = False

        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            cn.Open()

            ' Set up a data set command object.
            Dim strSelect As String = "SELECT * FROM LAST_CONFIGURATION_LASER"

            Dim cmd As New SqlCommand(strSelect, cn)

            Dim rdr As SqlDataReader

            rdr = cmd.ExecuteReader
            If (rdr.HasRows) Then
                Dim i = 0
                While rdr.Read()    'scorriamo fino all'ultima
                    las1.recipe = rdr("Recipe")
                    las1.batch = rdr("Batch")
                    las1.laser_pointer = False
                    i += 1
                    ans = True
                End While
            Else    'non ci sono righe; non dovrebbe succedere.....
                ans = False
            End If

            cn.Close()

        Catch ex As Exception
            ans = False
        End Try
        Return ans
    End Function
    Public Function Get_active_recipes() As String()



        Dim ret_val As String() = {""}
        Dim n_recipe As Integer = 0



        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn



            cn.Open()



            ' Set up a data set command object.
            Dim strSelect As String = "SELECT * FROM RECIPE_LASER WHERE Recipe_active = @active AND Associated_station = @stat"



            Dim cmd As New SqlCommand(strSelect, cn)
            cmd.Parameters.AddWithValue("@active", True)
            cmd.Parameters.AddWithValue("@stat", Station_settings.StationNumber)



            Dim rdr As SqlDataReader



            rdr = cmd.ExecuteReader



            If (rdr.HasRows) Then
                While rdr.Read()    'scorriamo fino all'ultima
                    ReDim Preserve ret_val(n_recipe)
                    ret_val(n_recipe) = rdr("Recipe_name")
                    n_recipe += 1



                End While



            End If



            cn.Close()



        Catch ex As Exception
            'Add_log_element("DB Get_active_recipes EXCEPTION:", ex.Message)
        End Try



        Return ret_val



    End Function

    '-----------------------------------------------------------------------------------
    'FUNZIONI EDITING RICETTA

    Public Function Check_recipe_existing(rec As String) As Boolean
        'ritorna true se il nome di ricetta è già esistente, false altrimenti
        Dim ret_val As Boolean = False

        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            cn.Open()

            ' Set up a data set command object.
            Dim strSelect As String = "SELECT * FROM RECIPE_LASER WHERE Recipe_name = @r_name"

            Dim cmd As New SqlCommand(strSelect, cn)
            cmd.Parameters.AddWithValue("@r_name", rec)

            Dim rdr As SqlDataReader

            rdr = cmd.ExecuteReader

            If (rdr.HasRows) Then

                ret_val = True

            End If

            cn.Close()

        Catch ex As Exception
            'Add_log_element("DB Get_active_recipes EXCEPTION:", ex.Message)
        End Try

        Return ret_val

    End Function

    Public Function Get_all_recipes() As String()

        Dim ret_val As String() = {""}
        Dim n_recipe As Integer = 0

        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            cn.Open()

            ' Set up a data set command object.
            Dim strSelect As String = "SELECT * FROM RECIPE_LASER"

            Dim cmd As New SqlCommand(strSelect, cn)
            'cmd.Parameters.AddWithValue("@active", True)
            'cmd.Parameters.AddWithValue("@stat", Station_settings.StationNumber)

            Dim rdr As SqlDataReader

            rdr = cmd.ExecuteReader

            If (rdr.HasRows) Then
                While rdr.Read()    'scorriamo fino all'ultima
                    ReDim Preserve ret_val(n_recipe)
                    ret_val(n_recipe) = rdr("Recipe_name")
                    n_recipe += 1

                End While

            End If

            cn.Close()

        Catch ex As Exception
            'Add_log_element("DB Get_active_recipes EXCEPTION:", ex.Message)
        End Try

        Return ret_val

    End Function

    Public Function Insert_new_item(ByRef my_item As single_item_struct) As Boolean

        Dim ans As Boolean = False

        Try

            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            command.CommandText = "INSERT INTO ITEMS_LASER (Item_type, laser_on_back, t_font, t_text, t_before, t_after, t_x_pos, t_y_pos, t_x_scale, t_y_scale, " &
                                "t_z_scale, t_x_rotate, t_y_rotate, t_z_rotate, t_height, t_energy, t_pitch, l_path, l_x_pos, l_y_pos, " &
                                "l_x_scale, l_y_scale, l_z_scale, l_x_rotate, l_y_rotate, l_z_rotate, l_energy, l_pitch, dm_text, dm_size, " &
                                "dm_inverted, dm_border, dm_x_pos, dm_y_pos, dm_z_rotate, dm_fill_angle, dm_fill_spacing, dm_beam_diameter, dm_direction, dm_grid_type, " &
                                "dm_pitch, dm_energy, dm_optimization,t_rounded,t_rounded_size, t_fontName) values " &
                                "(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, " &
                                "@p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, @p20, " &
                                "@p21, @p22, @p23, @p24, @p25, @p26, @p27, @p28, @p29, @p30, " &
                                "@p31, @p32, @p33, @p34, @p35, @p36, @p37, @p38, @p39, @p40, " &
                                "@p41, @p42, @p43, @p44, @p45, @p46); SELECT SCOPE_IDENTITY();"

            Dim p1 As SqlParameter = New SqlParameter
            Dim p2 As SqlParameter = New SqlParameter
            Dim p3 As SqlParameter = New SqlParameter
            Dim p4 As SqlParameter = New SqlParameter
            Dim p5 As SqlParameter = New SqlParameter
            Dim p6 As SqlParameter = New SqlParameter
            Dim p7 As SqlParameter = New SqlParameter
            Dim p8 As SqlParameter = New SqlParameter
            Dim p9 As SqlParameter = New SqlParameter
            Dim p10 As SqlParameter = New SqlParameter
            Dim p11 As SqlParameter = New SqlParameter
            Dim p12 As SqlParameter = New SqlParameter
            Dim p13 As SqlParameter = New SqlParameter
            Dim p14 As SqlParameter = New SqlParameter
            Dim p15 As SqlParameter = New SqlParameter
            Dim p16 As SqlParameter = New SqlParameter
            Dim p17 As SqlParameter = New SqlParameter
            Dim p18 As SqlParameter = New SqlParameter
            Dim p19 As SqlParameter = New SqlParameter
            Dim p20 As SqlParameter = New SqlParameter
            Dim p21 As SqlParameter = New SqlParameter
            Dim p22 As SqlParameter = New SqlParameter
            Dim p23 As SqlParameter = New SqlParameter
            Dim p24 As SqlParameter = New SqlParameter
            Dim p25 As SqlParameter = New SqlParameter
            Dim p26 As SqlParameter = New SqlParameter
            Dim p27 As SqlParameter = New SqlParameter
            Dim p28 As SqlParameter = New SqlParameter
            Dim p29 As SqlParameter = New SqlParameter
            Dim p30 As SqlParameter = New SqlParameter
            Dim p31 As SqlParameter = New SqlParameter
            Dim p32 As SqlParameter = New SqlParameter
            Dim p33 As SqlParameter = New SqlParameter
            Dim p34 As SqlParameter = New SqlParameter
            Dim p35 As SqlParameter = New SqlParameter
            Dim p36 As SqlParameter = New SqlParameter
            Dim p37 As SqlParameter = New SqlParameter
            Dim p38 As SqlParameter = New SqlParameter
            Dim p39 As SqlParameter = New SqlParameter
            Dim p40 As SqlParameter = New SqlParameter
            Dim p41 As SqlParameter = New SqlParameter
            Dim p42 As SqlParameter = New SqlParameter
            Dim p43 As SqlParameter = New SqlParameter
            Dim p44 As SqlParameter = New SqlParameter
            Dim p45 As SqlParameter = New SqlParameter
            Dim p46 As SqlParameter = New SqlParameter

            p1.DbType = DbType.Int32
            p2.DbType = DbType.Int32
            p3.DbType = DbType.Int32
            p4.DbType = DbType.String
            p5.DbType = DbType.String
            p6.DbType = DbType.String
            p7.DbType = DbType.Double
            p8.DbType = DbType.Double
            p9.DbType = DbType.Double
            p10.DbType = DbType.Double
            p11.DbType = DbType.Double
            p12.DbType = DbType.Double
            p13.DbType = DbType.Double
            p14.DbType = DbType.Double
            p15.DbType = DbType.Double
            p16.DbType = DbType.Double
            p17.DbType = DbType.Double
            p18.DbType = DbType.String
            p19.DbType = DbType.Double
            p20.DbType = DbType.Double
            p21.DbType = DbType.Double
            p22.DbType = DbType.Double
            p23.DbType = DbType.Double
            p24.DbType = DbType.Double
            p25.DbType = DbType.Double
            p26.DbType = DbType.Double
            p27.DbType = DbType.Double
            p28.DbType = DbType.Double
            p29.DbType = DbType.String
            p30.DbType = DbType.Double
            p31.DbType = DbType.Boolean
            p32.DbType = DbType.Double
            p33.DbType = DbType.Double
            p34.DbType = DbType.Double
            p35.DbType = DbType.Double
            p36.DbType = DbType.Double
            p37.DbType = DbType.Double
            p38.DbType = DbType.Double
            p39.DbType = DbType.Int32
            p40.DbType = DbType.Int32
            p41.DbType = DbType.Double
            p42.DbType = DbType.Double
            p43.DbType = DbType.Int32
            p44.DbType = DbType.Int32
            p45.DbType = DbType.Double
            p46.DbType = DbType.String

            p1.Value = my_item.item_type
            p2.Value = my_item.laser_on_back
            p3.Value = my_item.t_font
            p4.Value = my_item.t_text
            p5.Value = my_item.t_before
            p6.Value = my_item.t_after
            p7.Value = my_item.t_x_pos
            p8.Value = my_item.t_y_pos
            p9.Value = my_item.t_x_scale
            p10.Value = my_item.t_y_scale
            p11.Value = my_item.t_z_scale
            p12.Value = my_item.t_x_rotate
            p13.Value = my_item.t_y_rotate
            p14.Value = my_item.t_z_rotate
            p15.Value = my_item.t_height
            p16.Value = my_item.t_energy
            p17.Value = my_item.t_pitch
            p18.Value = my_item.l_path
            p19.Value = my_item.l_x_pos
            p20.Value = my_item.l_y_pos
            p21.Value = my_item.l_x_scale
            p22.Value = my_item.l_y_scale
            p23.Value = my_item.l_z_scale
            p24.Value = my_item.l_x_rotate
            p25.Value = my_item.l_y_rotate
            p26.Value = my_item.l_z_rotate
            p27.Value = my_item.l_energy
            p28.Value = my_item.l_pitch
            p29.Value = my_item.dm_text
            p30.Value = my_item.dm_size
            p31.Value = my_item.dm_inverted
            p32.Value = my_item.dm_border
            p33.Value = my_item.dm_x_pos
            p34.Value = my_item.dm_y_pos
            p35.Value = my_item.dm_z_rotate
            p36.Value = my_item.dm_fill_angle
            p37.Value = my_item.dm_fill_spacing
            p38.Value = my_item.dm_beam_diameter
            p39.Value = my_item.dm_direction
            p40.Value = my_item.dm_grid_type
            p41.Value = my_item.dm_pitch
            p42.Value = my_item.dm_energy
            p43.Value = my_item.dm_optimization
            p44.Value = my_item.txt_rounded
            p45.Value = my_item.txt_round_dim
            p46.Value = my_item.t_fontName

            p1.ParameterName = "p1"
            p2.ParameterName = "p2"
            p3.ParameterName = "p3"
            p4.ParameterName = "p4"
            p5.ParameterName = "p5"
            p6.ParameterName = "p6"
            p7.ParameterName = "p7"
            p8.ParameterName = "p8"
            p9.ParameterName = "p9"
            p10.ParameterName = "p10"
            p11.ParameterName = "p11"
            p12.ParameterName = "p12"
            p13.ParameterName = "p13"
            p14.ParameterName = "p14"
            p15.ParameterName = "p15"
            p16.ParameterName = "p16"
            p17.ParameterName = "p17"
            p18.ParameterName = "p18"
            p19.ParameterName = "p19"
            p20.ParameterName = "p20"
            p21.ParameterName = "p21"
            p22.ParameterName = "p22"
            p23.ParameterName = "p23"
            p24.ParameterName = "p24"
            p25.ParameterName = "p25"
            p26.ParameterName = "p26"
            p27.ParameterName = "p27"
            p28.ParameterName = "p28"
            p29.ParameterName = "p29"
            p30.ParameterName = "p30"
            p31.ParameterName = "p31"
            p32.ParameterName = "p32"
            p33.ParameterName = "p33"
            p34.ParameterName = "p34"
            p35.ParameterName = "p35"
            p36.ParameterName = "p36"
            p37.ParameterName = "p37"
            p38.ParameterName = "p38"
            p39.ParameterName = "p39"
            p40.ParameterName = "p40"
            p41.ParameterName = "p41"
            p42.ParameterName = "p42"
            p43.ParameterName = "p43"
            p44.ParameterName = "p44"
            p45.ParameterName = "p45"
            p46.ParameterName = "p46"

            p1.SourceColumn = "Item_type"
            p2.SourceColumn = "laser_on_back"
            p3.SourceColumn = "t_font"
            p4.SourceColumn = "t_text"
            p5.SourceColumn = "t_before"
            p6.SourceColumn = "t_after"
            p7.SourceColumn = "t_x_pos"
            p8.SourceColumn = "t_y_pos"
            p9.SourceColumn = "t_x_scale"
            p10.SourceColumn = "t_y_scale"
            p11.SourceColumn = "t_z_scale"
            p12.SourceColumn = "t_x_rotate"
            p13.SourceColumn = "t_y_rotate"
            p14.SourceColumn = "t_z_rotate"
            p15.SourceColumn = "t_height"
            p16.SourceColumn = "t_energy"
            p17.SourceColumn = "t_pitch"
            p18.SourceColumn = "l_path"
            p19.SourceColumn = "l_x_pos"
            p20.SourceColumn = "l_y_pos"
            p21.SourceColumn = "l_x_scale"
            p22.SourceColumn = "l_y_scale"
            p23.SourceColumn = "l_z_scale"
            p24.SourceColumn = "l_x_rotate"
            p25.SourceColumn = "l_y_rotate"
            p26.SourceColumn = "l_z_rotate"
            p27.SourceColumn = "l_energy"
            p28.SourceColumn = "l_pitch"
            p29.SourceColumn = "dm_text"
            p30.SourceColumn = "dm_size"
            p31.SourceColumn = "dm_inverted"
            p32.SourceColumn = "dm_border"
            p33.SourceColumn = "dm_x_pos"
            p34.SourceColumn = "dm_y_pos"
            p35.SourceColumn = "dm_z_rotate"
            p36.SourceColumn = "dm_fill_angle"
            p37.SourceColumn = "dm_fill_spacing"
            p38.SourceColumn = "dm_beam_diameter"
            p39.SourceColumn = "dm_direction"
            p40.SourceColumn = "dm_grid_type"
            p41.SourceColumn = "dm_pitch"
            p42.SourceColumn = "dm_energy"
            p43.SourceColumn = "dm_optimization"
            p44.SourceColumn = "t_rounded"
            p45.SourceColumn = "t_rounded_size"
            p46.SourceColumn = "t_fontName"

            command.Parameters.Add(p1)
            command.Parameters.Add(p2)
            command.Parameters.Add(p3)
            command.Parameters.Add(p4)
            command.Parameters.Add(p5)
            command.Parameters.Add(p6)
            command.Parameters.Add(p7)
            command.Parameters.Add(p8)
            command.Parameters.Add(p9)
            command.Parameters.Add(p10)
            command.Parameters.Add(p11)
            command.Parameters.Add(p12)
            command.Parameters.Add(p13)
            command.Parameters.Add(p14)
            command.Parameters.Add(p15)
            command.Parameters.Add(p16)
            command.Parameters.Add(p17)
            command.Parameters.Add(p18)
            command.Parameters.Add(p19)
            command.Parameters.Add(p20)
            command.Parameters.Add(p21)
            command.Parameters.Add(p22)
            command.Parameters.Add(p23)
            command.Parameters.Add(p24)
            command.Parameters.Add(p25)
            command.Parameters.Add(p26)
            command.Parameters.Add(p27)
            command.Parameters.Add(p28)
            command.Parameters.Add(p29)
            command.Parameters.Add(p30)
            command.Parameters.Add(p31)
            command.Parameters.Add(p32)
            command.Parameters.Add(p33)
            command.Parameters.Add(p34)
            command.Parameters.Add(p35)
            command.Parameters.Add(p36)
            command.Parameters.Add(p37)
            command.Parameters.Add(p38)
            command.Parameters.Add(p39)
            command.Parameters.Add(p40)
            command.Parameters.Add(p41)
            command.Parameters.Add(p42)
            command.Parameters.Add(p43)
            command.Parameters.Add(p44)
            command.Parameters.Add(p45)
            command.Parameters.Add(p46)

            cn.Open()

            my_item.idx = command.ExecuteScalar

            cn.Close()
            ans = True

        Catch ex As Exception

            MsgBox(ex.Message & " Insert_new_item")
            ans = False
        End Try

        Return ans

    End Function

    Public Function Insert_new_recipe(rec As recipe_data) As Boolean

        Dim ans As Boolean = False

        Try

            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            command.CommandText = "INSERT INTO RECIPE_LASER (Recipe_name, Recipe_description, Recipe_active, Associated_station, Line_1, Line_2" &
                                ", Line_3, Line_4, Line_5, Line_6" &
                                ", Line_7, Line_8, Serial, Logo_1, Logo_2, Logo_3" &
                                ", Logo_4, Datamatrix_1, Datamatrix_2, Cloning, X_spacing, Y_spacing, Enable_laser_on_back) values " &
                                "(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, @p20, @p21, @p22, @p23)"

            Dim p1 As SqlParameter = New SqlParameter
            Dim p2 As SqlParameter = New SqlParameter
            Dim p3 As SqlParameter = New SqlParameter
            Dim p4 As SqlParameter = New SqlParameter
            Dim p5 As SqlParameter = New SqlParameter
            Dim p6 As SqlParameter = New SqlParameter
            Dim p7 As SqlParameter = New SqlParameter
            Dim p8 As SqlParameter = New SqlParameter
            Dim p9 As SqlParameter = New SqlParameter
            Dim p10 As SqlParameter = New SqlParameter
            Dim p11 As SqlParameter = New SqlParameter
            Dim p12 As SqlParameter = New SqlParameter
            Dim p13 As SqlParameter = New SqlParameter
            Dim p14 As SqlParameter = New SqlParameter
            Dim p15 As SqlParameter = New SqlParameter
            Dim p16 As SqlParameter = New SqlParameter
            Dim p17 As SqlParameter = New SqlParameter
            Dim p18 As SqlParameter = New SqlParameter
            Dim p19 As SqlParameter = New SqlParameter
            Dim p20 As SqlParameter = New SqlParameter
            Dim p21 As SqlParameter = New SqlParameter
            Dim p22 As SqlParameter = New SqlParameter
            Dim p23 As SqlParameter = New SqlParameter

            p1.DbType = DbType.String
            p2.DbType = DbType.String
            p3.DbType = DbType.Boolean
            p4.DbType = DbType.Int32
            p5.DbType = DbType.Int32
            p6.DbType = DbType.Int32
            p7.DbType = DbType.Int32
            p8.DbType = DbType.Int32
            p9.DbType = DbType.Int32
            p10.DbType = DbType.Int32
            p11.DbType = DbType.Int32
            p12.DbType = DbType.Int32
            p13.DbType = DbType.Int32
            p14.DbType = DbType.Int32
            p15.DbType = DbType.Int32
            p16.DbType = DbType.Int32
            p17.DbType = DbType.Int32
            p18.DbType = DbType.Int32
            p19.DbType = DbType.Int32
            p20.DbType = DbType.Int32
            p21.DbType = DbType.Int32
            p22.DbType = DbType.Double
            p22.DbType = DbType.Double
            p23.DbType = DbType.Boolean

            p1.Value = rec.Recipe_name                  'Recipe_name
            p2.Value = rec.Recipe_description           'Recipe_description
            p3.Value = rec.Recipe_active                'Recipe_active
            p4.Value = rec.Associated_station           'Associated_station
            p5.Value = rec.Line_1                       'Line_1
            p6.Value = rec.Line_2                       'Line_2
            p7.Value = rec.Line_3                       'Line_3
            p8.Value = rec.Line_4                       'Line_4
            p9.Value = rec.Line_5                       'Line_5
            p10.Value = rec.Line_6                      'Line_6
            p11.Value = rec.Line_7                      'Line_7
            p12.Value = rec.Line_8                      'Line_8
            p13.Value = rec.Serial                      'Serial
            p14.Value = rec.Logo_1                      'Logo_1
            p15.Value = rec.Logo_2                      'Logo_2
            p16.Value = rec.Logo_3                      'Logo_3
            p17.Value = rec.Logo_4                      'Logo_4
            p18.Value = rec.Datamatrix_1                'Datamatrix_1
            p19.Value = rec.Datamatrix_2                'Datamatrix_2
            p20.Value = rec.Cloning                     'Cloning
            p21.Value = rec.X_cloning                   'X_spacing
            p22.Value = rec.Y_cloning                   'Y_spacing
            p23.Value = rec.Enable_laser_on_back        'Enable_laser_on_back

            p1.ParameterName = "p1"
            p2.ParameterName = "p2"
            p3.ParameterName = "p3"
            p4.ParameterName = "p4"
            p5.ParameterName = "p5"
            p6.ParameterName = "p6"
            p7.ParameterName = "p7"
            p8.ParameterName = "p8"
            p9.ParameterName = "p9"
            p10.ParameterName = "p10"
            p11.ParameterName = "p11"
            p12.ParameterName = "p12"
            p13.ParameterName = "p13"
            p14.ParameterName = "p14"
            p15.ParameterName = "p15"
            p16.ParameterName = "p16"
            p17.ParameterName = "p17"
            p18.ParameterName = "p18"
            p19.ParameterName = "p19"
            p20.ParameterName = "p20"
            p21.ParameterName = "p21"
            p22.ParameterName = "p22"
            p23.ParameterName = "p23"

            p1.SourceColumn = "Recipe_name"
            p2.SourceColumn = "Recipe_description"
            p3.SourceColumn = "Recipe_active"
            p4.SourceColumn = "Associated_station"
            p5.SourceColumn = "Line_1"
            p6.SourceColumn = "Line_2"
            p7.SourceColumn = "Line_3"
            p8.SourceColumn = "Line_4"
            p9.SourceColumn = "Line_5"
            p10.SourceColumn = "Line_6"
            p11.SourceColumn = "Line_7"
            p12.SourceColumn = "Line_8"
            p13.SourceColumn = "Serial"
            p14.SourceColumn = "Logo_1"
            p15.SourceColumn = "Logo_2"
            p16.SourceColumn = "Logo_3"
            p17.SourceColumn = "Logo_4"
            p18.SourceColumn = "Datamatrix_1"
            p19.SourceColumn = "Datamatrix_2"
            p20.SourceColumn = "Cloning"
            p21.SourceColumn = "X_spacing"
            p22.SourceColumn = "Y_spacing"
            p23.SourceColumn = "Enable_laser_on_back"


            command.Parameters.Add(p1)
            command.Parameters.Add(p2)
            command.Parameters.Add(p3)
            command.Parameters.Add(p4)
            command.Parameters.Add(p5)
            command.Parameters.Add(p6)
            command.Parameters.Add(p7)
            command.Parameters.Add(p8)
            command.Parameters.Add(p9)
            command.Parameters.Add(p10)
            command.Parameters.Add(p11)
            command.Parameters.Add(p12)
            command.Parameters.Add(p13)
            command.Parameters.Add(p14)
            command.Parameters.Add(p15)
            command.Parameters.Add(p16)
            command.Parameters.Add(p17)
            command.Parameters.Add(p18)
            command.Parameters.Add(p19)
            command.Parameters.Add(p20)
            command.Parameters.Add(p21)
            command.Parameters.Add(p22)
            command.Parameters.Add(p23)

            cn.Open()

            command.ExecuteNonQuery()

            cn.Close()
            ans = True

        Catch ex As Exception

            MsgBox(ex.Message & " Insert_new_recipe")
            ans = False
        End Try

        Return ans

    End Function

    Public Function Update_item(my_item As single_item_struct) As Boolean

        Dim ans As Boolean = False

        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim selectCommand As New SqlCommand
            selectCommand.Connection = cn

            Dim strSelect As String = "SELECT * FROM ITEMS_LASER WHERE Idx= @itm"
            selectCommand.CommandText = strSelect

            Dim par0 As New SqlParameter()
            selectCommand.Parameters.AddWithValue("@itm", my_item.idx)

            Dim dscmd As SqlDataAdapter = New SqlDataAdapter(selectCommand)
            Dim autogen As New SqlCommandBuilder(dscmd)

            cn.Open()

            ' Load a data set.
            Dim ds As New DataSet()
            dscmd.Fill(ds, "mod_rec")

            Dim dt As DataTable = ds.Tables.Item("mod_rec")
            Dim rowCustomer As DataRow
            rowCustomer = dt.Rows(0)

            rowCustomer.Item("Item_type") = my_item.item_type
            rowCustomer.Item("laser_on_back") = my_item.laser_on_back
            rowCustomer.Item("t_font") = my_item.t_font
            rowCustomer.Item("t_fontName") = my_item.t_fontName
            rowCustomer.Item("t_text") = my_item.t_text
            rowCustomer.Item("t_before") = my_item.t_before
            rowCustomer.Item("t_after") = my_item.t_after
            rowCustomer.Item("t_x_pos") = my_item.t_x_pos
            rowCustomer.Item("t_y_pos") = my_item.t_y_pos
            rowCustomer.Item("t_x_scale") = my_item.t_x_scale
            rowCustomer.Item("t_y_scale") = my_item.t_y_scale
            rowCustomer.Item("t_z_scale") = my_item.t_z_scale
            rowCustomer.Item("t_x_rotate") = my_item.t_x_rotate
            rowCustomer.Item("t_y_rotate") = my_item.t_y_rotate
            rowCustomer.Item("t_z_rotate") = my_item.t_z_rotate
            rowCustomer.Item("t_height") = my_item.t_height
            rowCustomer.Item("t_energy") = my_item.t_energy
            rowCustomer.Item("t_pitch") = my_item.t_pitch
            rowCustomer.Item("l_path") = my_item.l_path
            rowCustomer.Item("l_x_pos") = my_item.l_x_pos
            rowCustomer.Item("l_y_pos") = my_item.l_y_pos
            rowCustomer.Item("l_x_scale") = my_item.l_x_scale
            rowCustomer.Item("l_y_scale") = my_item.l_y_scale
            rowCustomer.Item("l_z_scale") = my_item.l_z_scale
            rowCustomer.Item("l_x_rotate") = my_item.l_x_rotate
            rowCustomer.Item("l_y_rotate") = my_item.l_y_rotate
            rowCustomer.Item("l_z_rotate") = my_item.l_z_rotate
            rowCustomer.Item("l_energy") = my_item.l_energy
            rowCustomer.Item("l_pitch") = my_item.l_pitch
            rowCustomer.Item("dm_text") = my_item.dm_text
            rowCustomer.Item("dm_size") = my_item.dm_size
            rowCustomer.Item("dm_inverted") = my_item.dm_inverted
            rowCustomer.Item("dm_border") = my_item.dm_border
            rowCustomer.Item("dm_x_pos") = my_item.dm_x_pos
            rowCustomer.Item("dm_y_pos") = my_item.dm_y_pos
            rowCustomer.Item("dm_z_rotate") = my_item.dm_z_rotate
            rowCustomer.Item("dm_fill_angle") = my_item.dm_fill_angle
            rowCustomer.Item("dm_fill_spacing") = my_item.dm_fill_spacing
            rowCustomer.Item("dm_beam_diameter") = my_item.dm_beam_diameter
            rowCustomer.Item("dm_direction") = my_item.dm_direction
            rowCustomer.Item("dm_grid_type") = my_item.dm_grid_type
            rowCustomer.Item("dm_pitch") = my_item.dm_pitch
            rowCustomer.Item("dm_energy") = my_item.dm_energy
            rowCustomer.Item("dm_optimization") = my_item.dm_optimization
            rowCustomer.Item("t_rounded") = my_item.txt_rounded
            rowCustomer.Item("t_rounded_size") = my_item.txt_round_dim

            dscmd.Update(ds, "mod_rec")

            cn.Close()

            ans = True

        Catch ex As Exception
            ans = False
        End Try

        Return ans

    End Function

    Public Function Update_recipe(rec As recipe_data) As Boolean

        Dim ans As Boolean = False

        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim selectCommand As New SqlCommand
            selectCommand.Connection = cn

            Dim strSelect As String = "SELECT * FROM RECIPE_LASER WHERE Recipe_name= @reci"
            selectCommand.CommandText = strSelect

            Dim par0 As New SqlParameter()
            selectCommand.Parameters.AddWithValue("@reci", rec.Recipe_name)

            Dim dscmd As SqlDataAdapter = New SqlDataAdapter(selectCommand)
            Dim autogen As New SqlCommandBuilder(dscmd)

            cn.Open()

            ' Load a data set.
            Dim ds As New DataSet()
            dscmd.Fill(ds, "mod_rec")     'pal_track

            Dim dt As DataTable = ds.Tables.Item("mod_rec")
            Dim rowCustomer As DataRow
            rowCustomer = dt.Rows(0)

            rowCustomer.Item("Recipe_description") = rec.Recipe_description
            rowCustomer.Item("Recipe_active") = rec.Recipe_active
            rowCustomer.Item("Associated_station") = rec.Associated_station
            rowCustomer.Item("Line_1") = rec.Line_1
            rowCustomer.Item("Line_2") = rec.Line_2
            rowCustomer.Item("Line_3") = rec.Line_3
            rowCustomer.Item("Line_4") = rec.Line_4
            rowCustomer.Item("Line_5") = rec.Line_5
            rowCustomer.Item("Line_6") = rec.Line_6
            rowCustomer.Item("Line_7") = rec.Line_7
            rowCustomer.Item("Line_8") = rec.Line_8
            rowCustomer.Item("Serial") = rec.Serial
            rowCustomer.Item("Logo_1") = rec.Logo_1
            rowCustomer.Item("Logo_2") = rec.Logo_2
            rowCustomer.Item("Logo_3") = rec.Logo_3
            rowCustomer.Item("Logo_4") = rec.Logo_4
            rowCustomer.Item("Datamatrix_1") = rec.Datamatrix_1
            rowCustomer.Item("Datamatrix_2") = rec.Datamatrix_2
            rowCustomer.Item("Cloning") = rec.Cloning
            rowCustomer.Item("X_spacing") = rec.X_cloning
            rowCustomer.Item("Y_spacing") = rec.Y_cloning
            rowCustomer.Item("Enable_laser_on_back") = rec.Enable_laser_on_back

            dscmd.Update(ds, "mod_rec")

            cn.Close()

            ans = True

        Catch ex As Exception
            ans = False
        End Try

        Return ans

    End Function

    Public Function GetSerial(ricetta As String, commessa As String) As Int32

        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn

            cn.Open()

            ' Set up a data set command object.
            Dim strSelect As String = "SELECT TOP (1) SerialLaser FROM [SL5-4].[dbo].[LASER_SERIAL_INCREMENT] WHERE Ricetta =@ricetta AND Commessa =@commessa ORDER BY SerialLaser DESC"

            Dim cmd As New SqlCommand(strSelect, cn)
            cmd.Parameters.AddWithValue("@ricetta", ricetta)
            cmd.Parameters.AddWithValue("@commessa", commessa)

            Dim rdr As SqlDataReader

            rdr = cmd.ExecuteReader
            If (rdr.HasRows) Then

                While rdr.Read()    'scorriamo fino all'ultima
                    Dim lastSerial As String = rdr("SerialLaser")
                    '# IN4TEK - 26112025 -
                    'corretto formato n.seriale in Int32 (era Int16 = max 32767
                    'Dim newSerial As Int16 = Convert.ToInt32(lastSerial) + 1
                    Dim newSerial As Int32 = Convert.ToInt32(lastSerial) + 1
                    cn.Close()
                    Return newSerial

                End While
            Else    'non ci sono righe; e il primo seriale quindi assegno 1.....

                Return 1
            End If

            cn.Close()

        Catch ex As Exception
            Return 1
        End Try

    End Function

    Public Sub UpdateLaserResult(dmtxt As String, sn As Int32, id As Int32)

        Try

            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim selectCommand As New SqlCommand
            selectCommand.Connection = cn

            Dim strSelect As String = "SELECT * FROM DTRE WHERE IdPezzo= @id"
            selectCommand.CommandText = strSelect

            Dim par0 As New SqlParameter()
            par0.ParameterName = "@id"
            par0.SqlDbType = SqlDbType.Int
            par0.Direction = ParameterDirection.Input
            par0.Value = id
            selectCommand.Parameters.Add(par0)

            Dim dscmd As SqlDataAdapter = New SqlDataAdapter(selectCommand)
            Dim autogen As New SqlCommandBuilder(dscmd)

            cn.Open()

            ' Load a data set.
            Dim ds As New DataSet()
            dscmd.Fill(ds, "FinalUpdate")

            Dim dt As DataTable = ds.Tables.Item("FinalUpdate")
            Dim rowCustomer As DataRow
            rowCustomer = dt.Rows(0)
            rowCustomer.Item("TimeStampLaser") = Now
            rowCustomer.Item("SerialLaser") = sn.ToString("00000")
            rowCustomer.Item("DMWrite") = dmtxt


            dscmd.Update(ds, "FinalUpdate")

            cn.Close()


        Catch ex As Exception
            'Add_log_element("DB", "FinalDbUpdate: exception " & ex.Message & " ptc=" & ptc_n.get_suffix & " i=" & i, 0)  'max livello di log
        End Try
    End Sub

    Public Function InsertLaserSerial(recipe As String, batch As String, sn As Int32, id As Int32) As Boolean
        Try
            Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
            Dim command As New SqlCommand
            command.Connection = cn
            Dim strSelect As String = "INSERT INTO [SL5-4].[dbo].[LASER_SERIAL_INCREMENT] (IdPezzo,Ricetta, Commessa, SerialLaser) VALUES (@IdPezzo, @ricetta, @commessa, @serial)"

            command.CommandText = strSelect
            cn.Open()

            ' Set up a data set command object.

            command.Parameters.AddWithValue("@IdPezzo", id)
            command.Parameters.AddWithValue("@ricetta", recipe)
            command.Parameters.AddWithValue("@commessa", batch)
            command.Parameters.AddWithValue("@serial", sn)


            Dim rowsAffected As Integer = command.ExecuteNonQuery()

            cn.Close()
            If rowsAffected > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

End Module





'Public Function Update_last_config(last_recipe, last_batch, last_serial) As String()



'    Dim ret_val As String() = {""}
'    Dim n_recipe As Integer = 0



'    Try
'        Dim cn As SqlConnection = New SqlConnection(Station_settings.Db_conn_string)
'        Dim command As New SqlCommand
'        command.Connection = cn



'        cn.Open()



'        ' Set up a data set command object.
'        Dim strSelect As String = "UPDATE Last_configuration SET Recipe= @recipe, Batch = @batch, Serial = @serial"



'        Dim cmd As New SqlCommand(strSelect, cn)
'        cmd.Parameters.AddWithValue("@recipe", last_recipe)
'        cmd.Parameters.AddWithValue("@batch", last_batch)
'        cmd.Parameters.AddWithValue("@serial", last_serial)



'        Dim rdr As SqlDataReader



'        rdr = cmd.ExecuteReader



'        If (rdr.HasRows) Then
'            While rdr.Read()    'scorriamo fino all'ultima
'                ReDim Preserve ret_val(n_recipe)
'                ret_val(n_recipe) = rdr("Recipe_name")
'                n_recipe += 1



'            End While



'        End If



'        cn.Close()



'    Catch ex As Exception
'        'Add_log_element("DB Get_active_recipes EXCEPTION:", ex.Message)
'    End Try



'    Return ret_val



'End Function

Public Structure db_ver
    Public version As String
    Public date_time As DateTime
    Public note As String
    Public valid As Boolean
    Public exception As String
End Structure
