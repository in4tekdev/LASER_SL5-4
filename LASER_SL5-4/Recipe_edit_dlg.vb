Imports System.Drawing.Text
Imports System.Drawing.Drawing2D
Imports System.IO

Public Class Recipe_edit_dlg

    Private actual_type_idx As type_and_idx
    Private loadingUi As Boolean = False

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Recipe_edit_dlg_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WindowState = WindowState.Maximized
        actual_type_idx.idx = -1
        actual_type_idx.type = type_to_show.NO_TYPE

        'forzo la carica dei parametri del primo elemento        
        actual_type_idx = Get_id_from_combo(0)
        Show_hide_controls(actual_type_idx.type)
        Populate_UI(actual_type_idx.type, actual_type_idx.idx)

        ComboBox5.SelectedIndex = 0
        GroupBox1.Text = ComboBox5.SelectedItem

    End Sub

    Private Sub Show_hide_controls(show_type As type_to_show)

        Select Case show_type
            Case type_to_show.TEXT_LINE
                'text
                Label3.Visible = True
                Label3.Text = "Text"
                TextBox1.Visible = True
                'font
                Label2.Visible = True
                ComboBox1.Visible = True
				'fontName
                Label24.Visible = True
                ComboBox6.Visible = True
                'height
                Label12.Visible = True
                NumericUpDown9.Visible = True
                'after
                Label15.Visible = False
                TextBox2.Visible = False
                'path
                Label16.Visible = False
                Button1.Visible = False
                'x-scale, y-scale, z-scale
                Label6.Visible = True
                Label7.Visible = True
                Label8.Visible = True
                NumericUpDown3.Visible = True
                NumericUpDown4.Visible = True
                NumericUpDown5.Visible = True
                'x-rotation, y-rotation, z-rotation
                Label9.Visible = True
                Label10.Visible = True
                Label11.Visible = True
                NumericUpDown6.Visible = True
                NumericUpDown7.Visible = True
                NumericUpDown8.Visible = True
                ' curved text
                CheckBox4.Visible = True
                NumericUpDown16.Visible = True
                'Datamatrix controls only
                CheckBox2.Visible = False
                Label17.Visible = False
                Label18.Visible = False
                Label19.Visible = False
                Label20.Visible = False
                Label21.Visible = False
                Label22.Visible = False
                Label23.Visible = False
                Label27.Visible = False
                Label1.Visible = False
                TextBox3.Visible = False
                NumericUpDown12.Visible = False
                NumericUpDown13.Visible = False
                NumericUpDown14.Visible = False
                NumericUpDown15.Visible = False
                NumericUpDown19.Visible = False
                ComboBox2.Visible = False
                ComboBox3.Visible = False
                ComboBox4.Visible = False


            Case type_to_show.SERIAL_LINE
                'text
                Label3.Visible = True
                Label3.Text = "Before"
                TextBox1.Visible = True
                'font
                Label2.Visible = True
                ComboBox1.Visible = True
                Label24.Visible = True
                ComboBox6.Visible = True
                'height
                Label12.Visible = True
                NumericUpDown9.Visible = True
                'after
                Label15.Visible = True
                TextBox2.Visible = True
                'path
                Label16.Visible = False
                Button1.Visible = False
                'x-scale, y-scale, z-scale
                Label6.Visible = True
                Label7.Visible = True
                Label8.Visible = True
                NumericUpDown3.Visible = True
                NumericUpDown4.Visible = True
                NumericUpDown5.Visible = True
                'x-rotation, y-rotation, z-rotation
                Label9.Visible = True
                Label10.Visible = True
                Label11.Visible = True
                NumericUpDown6.Visible = True
                NumericUpDown7.Visible = True
                NumericUpDown8.Visible = True
                'curved text
                CheckBox4.Visible = True
                NumericUpDown16.Visible = True
                'Datamatrix controls only
                CheckBox2.Visible = False
                Label17.Visible = False
                Label18.Visible = False
                Label19.Visible = False
                Label20.Visible = False
                Label21.Visible = False
                Label22.Visible = False
                Label23.Visible = False
                Label27.Visible = False
                Label1.Visible = False
                TextBox3.Visible = False
                NumericUpDown12.Visible = False
                NumericUpDown13.Visible = False
                NumericUpDown14.Visible = False
                NumericUpDown15.Visible = False
                NumericUpDown19.Visible = False
                ComboBox2.Visible = False
                ComboBox3.Visible = False
                ComboBox4.Visible = False

            Case type_to_show.LOGO
                'text
                Label3.Visible = False
                TextBox1.Visible = False
                'font
                Label2.Visible = False
                ComboBox1.Visible = False
                Label24.Visible = False
                ComboBox6.Visible = False
                'height
                Label12.Visible = False
                NumericUpDown9.Visible = False
                'after
                Label15.Visible = False
                TextBox2.Visible = False
                'path
                Label16.Visible = True
                Button1.Visible = True
                'x-scale, y-scale, z-scale
                Label6.Visible = True
                Label7.Visible = True
                Label8.Visible = True
                NumericUpDown3.Visible = True
                NumericUpDown4.Visible = True
                NumericUpDown5.Visible = True
                'x-rotation, y-rotation, z-rotation
                Label9.Visible = True
                Label10.Visible = True
                Label11.Visible = True
                NumericUpDown6.Visible = True
                NumericUpDown7.Visible = True
                NumericUpDown8.Visible = True
                'curved text
                CheckBox4.Visible = False
                NumericUpDown16.Visible = False
                'Datamatrix controls only
                CheckBox2.Visible = False
                Label17.Visible = False
                Label18.Visible = False
                Label19.Visible = False
                Label20.Visible = False
                Label21.Visible = False
                Label22.Visible = False
                Label23.Visible = False
                Label27.Visible = False
                Label1.Visible = False
                TextBox3.Visible = False
                NumericUpDown12.Visible = False
                NumericUpDown13.Visible = False
                NumericUpDown14.Visible = False
                NumericUpDown15.Visible = False
                NumericUpDown19.Visible = False
                ComboBox2.Visible = False
                ComboBox3.Visible = False
                ComboBox4.Visible = False

            Case type_to_show.DATAMATRIX
                'text
                Label3.Visible = True
                Label3.Text = "Before"
                TextBox1.Visible = True
                'font
                Label2.Visible = False
                ComboBox1.Visible = False
                Label24.Visible = False
                ComboBox6.Visible = False
                'height
                Label12.Visible = False
                NumericUpDown9.Visible = False
                'after
                Label15.Visible = True
                TextBox2.Visible = True
                'path
                Label16.Visible = False
                Button1.Visible = False
                'x-scale, y-scale, z-scale
                Label6.Visible = False
                Label7.Visible = False
                Label8.Visible = False
                NumericUpDown3.Visible = False
                NumericUpDown4.Visible = False
                NumericUpDown5.Visible = False
                'x-rotation, y-rotation, z-rotation
                Label9.Visible = False
                Label10.Visible = False
                Label11.Visible = True
                NumericUpDown6.Visible = False
                NumericUpDown7.Visible = False
                NumericUpDown8.Visible = True
                'curved text
                CheckBox4.Visible = False
                NumericUpDown16.Visible = False
                'Datamatrix controls only
                CheckBox2.Visible = True
                Label17.Visible = True
                Label18.Visible = True
                Label19.Visible = True
                Label20.Visible = True
                Label21.Visible = True
                Label22.Visible = True
                Label23.Visible = True
                Label27.Visible = True
                Label1.Visible = True
                TextBox3.Visible = True
                NumericUpDown12.Visible = True
                NumericUpDown13.Visible = True
                NumericUpDown14.Visible = True
                NumericUpDown15.Visible = True
                NumericUpDown19.Visible = True
                ComboBox2.Visible = True
                ComboBox3.Visible = True
                ComboBox4.Visible = True

        End Select

    End Sub

    ' salva i parametri modificati o non nelle variabili interne, NON NEL DB
    Private Sub Get_data_from_UI(item_type As type_to_show, item_idx As Integer)

        Select Case item_type

            Case type_to_show.SERIAL_LINE, type_to_show.TEXT_LINE

                If (CheckBox1.Checked) Then
                    edit_Recipe.Line_items(item_idx).item_type = 1
                Else
                    edit_Recipe.Line_items(item_idx).item_type = 0
                End If
                edit_Recipe.Line_items(item_idx).laser_on_back = CheckBox3.Checked

                'text
                If (item_type = type_to_show.TEXT_LINE) Then
                    edit_Recipe.Line_items(item_idx).t_text = TextBox1.Text
                ElseIf (item_type = type_to_show.SERIAL_LINE) Then
                    edit_Recipe.Line_items(item_idx).t_before = TextBox1.Text
                End If

                'rounded
                edit_Recipe.Line_items(item_idx).txt_rounded = CheckBox4.Checked
                edit_Recipe.Line_items(item_idx).txt_round_dim = NumericUpDown16.Value

                'font
                edit_Recipe.Line_items(item_idx).t_font = ComboBox1.SelectedIndex
				'fontName
                edit_Recipe.Line_items(item_idx).t_fontName = comboBox6.Text

                'height
                edit_Recipe.Line_items(item_idx).t_height = NumericUpDown9.Value

                'after
                If (item_type = type_to_show.SERIAL_LINE) Then
                    edit_Recipe.Line_items(item_idx).t_after = TextBox2.Text
                End If

                'X-pos, Y-pos
                edit_Recipe.Line_items(item_idx).t_x_pos = NumericUpDown1.Value
                edit_Recipe.Line_items(item_idx).t_y_pos = NumericUpDown2.Value

                'X-scale, Y-scale, Z-scale
                edit_Recipe.Line_items(item_idx).t_x_scale = NumericUpDown3.Value
                edit_Recipe.Line_items(item_idx).t_y_scale = NumericUpDown4.Value
                edit_Recipe.Line_items(item_idx).t_z_scale = NumericUpDown5.Value

                'X-rot, Y-rot, Z-rot
                edit_Recipe.Line_items(item_idx).t_x_rotate = NumericUpDown6.Value
                edit_Recipe.Line_items(item_idx).t_y_rotate = NumericUpDown7.Value
                edit_Recipe.Line_items(item_idx).t_z_rotate = NumericUpDown8.Value

                'Energy, pitch
                edit_Recipe.Line_items(item_idx).t_energy = NumericUpDown10.Value
                edit_Recipe.Line_items(item_idx).t_pitch = NumericUpDown11.Value

            Case type_to_show.LOGO

                'path
                'edit_Logo_items(item_idx).l_path = Label16.Text

                If (CheckBox1.Checked) Then
                    edit_Recipe.Logo_items(item_idx).item_type = 2
                Else
                    edit_Recipe.Logo_items(item_idx).item_type = 0
                End If
                edit_Recipe.Logo_items(item_idx).laser_on_back = CheckBox3.Checked

                'X-pos, Y-pos
                edit_Recipe.Logo_items(item_idx).l_x_pos = NumericUpDown1.Value
                edit_Recipe.Logo_items(item_idx).l_y_pos = NumericUpDown2.Value

                'X-scale, Y-scale, Z-scale
                edit_Recipe.Logo_items(item_idx).l_x_scale = NumericUpDown3.Value
                edit_Recipe.Logo_items(item_idx).l_y_scale = NumericUpDown4.Value
                edit_Recipe.Logo_items(item_idx).l_z_scale = NumericUpDown5.Value

                'X-rot, Y-rot, Z-rot
                edit_Recipe.Logo_items(item_idx).l_x_rotate = NumericUpDown6.Value
                edit_Recipe.Logo_items(item_idx).l_y_rotate = NumericUpDown7.Value
                edit_Recipe.Logo_items(item_idx).l_z_rotate = NumericUpDown8.Value

                'Energy, pitch
                edit_Recipe.Logo_items(item_idx).l_energy = NumericUpDown10.Value
                edit_Recipe.Logo_items(item_idx).l_pitch = NumericUpDown11.Value

            Case type_to_show.DATAMATRIX

                If (CheckBox1.Checked) Then
                    edit_Recipe.Datamatrix_items(item_idx).item_type = 3
                Else
                    edit_Recipe.Datamatrix_items(item_idx).item_type = 0
                End If
                edit_Recipe.Datamatrix_items(item_idx).laser_on_back = CheckBox3.Checked

                'text
                edit_Recipe.Datamatrix_items(item_idx).dm_text = TextBox3.Text

                'before
                edit_Recipe.Datamatrix_items(item_idx).dm_before = TextBox1.Text

                'after
                edit_Recipe.Datamatrix_items(item_idx).dm_after = TextBox2.Text

                'X-pos, Y-pos
                edit_Recipe.Datamatrix_items(item_idx).dm_x_pos = NumericUpDown1.Value
                edit_Recipe.Datamatrix_items(item_idx).dm_y_pos = NumericUpDown2.Value

                'Z-rot
                edit_Recipe.Datamatrix_items(item_idx).dm_z_rotate = NumericUpDown8.Value

                'inverted
                edit_Recipe.Datamatrix_items(item_idx).dm_inverted = CheckBox2.Checked

                'border
                edit_Recipe.Datamatrix_items(item_idx).dm_border = NumericUpDown15.Value

                'size
                edit_Recipe.Datamatrix_items(item_idx).dm_size = NumericUpDown19.Value

                'fill angle
                edit_Recipe.Datamatrix_items(item_idx).dm_fill_angle = NumericUpDown12.Value

                'fill spacing
                edit_Recipe.Datamatrix_items(item_idx).dm_fill_spacing = NumericUpDown13.Value

                'beam diameter
                edit_Recipe.Datamatrix_items(item_idx).dm_beam_diameter = NumericUpDown14.Value

                ''fill direction
                'ComboBox2.Items.Clear()
                'For i = 0 To (Fill_direction_items.Length - 1)
                '    ComboBox2.Items.Add(Fill_direction_items(i))
                'Next
                'ComboBox2.SelectedIndex = edit_Datamatrix_items(item_idx).dm_direction
                edit_Recipe.Datamatrix_items(item_idx).dm_direction = ComboBox2.SelectedIndex
                ''fill type
                'ComboBox3.Items.Clear()
                'For i = 0 To (Fill_type_items.Length - 1)
                '    ComboBox3.Items.Add(Fill_type_items(i))
                'Next
                'ComboBox3.SelectedIndex = edit_Datamatrix_items(item_idx).dm_grid_type
                edit_Recipe.Datamatrix_items(item_idx).dm_grid_type = ComboBox3.SelectedIndex
                ''optimization
                'ComboBox4.Items.Clear()
                'For i = 0 To (Optimization_items.Length - 1)
                '    ComboBox4.Items.Add(Optimization_items(i))
                'Next
                'ComboBox4.SelectedIndex = edit_Datamatrix_items(item_idx).dm_optimization
                edit_Recipe.Datamatrix_items(item_idx).dm_optimization = ComboBox4.SelectedIndex
                'Energy, pitch
                edit_Recipe.Datamatrix_items(item_idx).dm_energy = NumericUpDown10.Value
                edit_Recipe.Datamatrix_items(item_idx).dm_pitch = NumericUpDown11.Value

        End Select

    End Sub

    Private Sub Populate_UI(item_type As type_to_show, item_idx As Integer)

        Dim i As Integer
        loadingUi = True

        Select Case item_type

            Case type_to_show.SERIAL_LINE, type_to_show.TEXT_LINE

                If (edit_Recipe.Line_items(item_idx).item_type <> 0) Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If

                CheckBox3.Checked = edit_Recipe.Line_items(item_idx).laser_on_back

                'text
                If (item_type = type_to_show.TEXT_LINE) Then
                    TextBox1.Text = edit_Recipe.Line_items(item_idx).t_text
                    drawText(item_idx).text = edit_Recipe.Line_items(item_idx).t_text
                ElseIf (item_type = type_to_show.SERIAL_LINE) Then
                    TextBox1.Text = edit_Recipe.Line_items(item_idx).t_before
                End If

                'rounded
                CheckBox4.Checked = edit_Recipe.Line_items(item_idx).txt_rounded
                NumericUpDown16.Value = edit_Recipe.Line_items(item_idx).txt_round_dim

                'font
                ComboBox1.Items.Clear()
                For i = 0 To (Font_items.Length - 1)
                    ComboBox1.Items.Add(Font_items(i))
                Next
                ComboBox1.SelectedIndex = edit_Recipe.Line_items(item_idx).t_font
                'drawText(item_idx).font = edit_Recipe.Line_items(item_idx).t_font

                'fontName
                Dim installedFonts As New InstalledFontCollection()
                ComboBox6.Items.Clear()
                ComboBox6.Items.Add(String.Empty) 'meglio string.empty ??????
                ' Iterate through the installed fonts
                For Each fontFamily As FontFamily In installedFonts.Families
                    ComboBox6.Items.Add(fontFamily.Name)
                Next
                ComboBox6.SelectedValue = edit_Recipe.Line_items(item_idx).t_fontName
                ComboBox6.Text = edit_Recipe.Line_items(item_idx).t_fontName
                'drawText(item_idx).font = edit_Recipe.Line_items(item_idx).t_fontName
                If String.IsNullOrEmpty(edit_Recipe.Line_items(item_idx).t_fontName) Then
                    drawText(item_idx).font = edit_Recipe.Line_items(item_idx).t_font
                Else
                    drawText(item_idx).font = edit_Recipe.Line_items(item_idx).t_fontName
                End If

                'height
                NumericUpDown9.Value = edit_Recipe.Line_items(item_idx).t_height
                drawText(item_idx).textSize = edit_Recipe.Line_items(item_idx).t_height
                'after
                If (item_type = type_to_show.SERIAL_LINE) Then
                    TextBox2.Text = edit_Recipe.Line_items(item_idx).t_after
                End If

                'X-pos, Y-pos
                NumericUpDown1.Value = edit_Recipe.Line_items(item_idx).t_x_pos
                drawText(item_idx).x_pos = edit_Recipe.Line_items(item_idx).t_x_pos
                NumericUpDown2.Value = edit_Recipe.Line_items(item_idx).t_y_pos
                drawText(item_idx).y_pos = edit_Recipe.Line_items(item_idx).t_y_pos

                'X-scale, Y-scale, Z-scale
                NumericUpDown3.Value = edit_Recipe.Line_items(item_idx).t_x_scale
                NumericUpDown4.Value = edit_Recipe.Line_items(item_idx).t_y_scale
                NumericUpDown5.Value = edit_Recipe.Line_items(item_idx).t_z_scale
                drawText(item_idx).x_scale = edit_Recipe.Line_items(item_idx).t_x_scale
                drawText(item_idx).y_scale = edit_Recipe.Line_items(item_idx).t_y_scale

                'X-rot, Y-rot, Z-rot
                NumericUpDown6.Value = edit_Recipe.Line_items(item_idx).t_x_rotate
                NumericUpDown7.Value = edit_Recipe.Line_items(item_idx).t_y_rotate
                NumericUpDown8.Value = edit_Recipe.Line_items(item_idx).t_z_rotate
                drawText(item_idx).x_rotate = edit_Recipe.Line_items(item_idx).t_x_rotate

                'Energy, pitch
                NumericUpDown10.Value = edit_Recipe.Line_items(item_idx).t_energy
                NumericUpDown11.Value = edit_Recipe.Line_items(item_idx).t_pitch

            Case type_to_show.LOGO

                If (edit_Recipe.Logo_items(item_idx).item_type <> 0) Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If

                CheckBox3.Checked = edit_Recipe.Logo_items(item_idx).laser_on_back

                'path
                Label16.Text = edit_Recipe.Logo_items(item_idx).l_path

                'X-pos, Y-pos
                NumericUpDown1.Value = edit_Recipe.Logo_items(item_idx).l_x_pos
                NumericUpDown2.Value = edit_Recipe.Logo_items(item_idx).l_y_pos

                'X-scale, Y-scale, Z-scale
                NumericUpDown3.Value = edit_Recipe.Logo_items(item_idx).l_x_scale
                NumericUpDown4.Value = edit_Recipe.Logo_items(item_idx).l_y_scale
                NumericUpDown5.Value = edit_Recipe.Logo_items(item_idx).l_z_scale

                'X-rot, Y-rot, Z-rot
                NumericUpDown6.Value = edit_Recipe.Logo_items(item_idx).l_x_rotate
                NumericUpDown7.Value = edit_Recipe.Logo_items(item_idx).l_y_rotate
                NumericUpDown8.Value = edit_Recipe.Logo_items(item_idx).l_z_rotate

                'Energy, pitch
                NumericUpDown10.Value = edit_Recipe.Logo_items(item_idx).l_energy
                NumericUpDown11.Value = edit_Recipe.Logo_items(item_idx).l_pitch

            Case type_to_show.DATAMATRIX

                If (edit_Recipe.Datamatrix_items(item_idx).item_type <> 0) Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If

                CheckBox3.Checked = edit_Recipe.Datamatrix_items(item_idx).laser_on_back

                'text
                TextBox3.Text = edit_Recipe.Datamatrix_items(item_idx).dm_text

                'before
                TextBox1.Text = edit_Recipe.Datamatrix_items(item_idx).dm_before

                'after
                TextBox2.Text = edit_Recipe.Datamatrix_items(item_idx).dm_after

                'X-pos, Y-pos
                NumericUpDown1.Value = edit_Recipe.Datamatrix_items(item_idx).dm_x_pos
                NumericUpDown2.Value = edit_Recipe.Datamatrix_items(item_idx).dm_y_pos

                'Z-rot
                NumericUpDown8.Value = edit_Recipe.Datamatrix_items(item_idx).dm_z_rotate

                'inverted
                CheckBox2.Checked = edit_Recipe.Datamatrix_items(item_idx).dm_inverted

                'border
                NumericUpDown15.Value = edit_Recipe.Datamatrix_items(item_idx).dm_border

                'size
                NumericUpDown19.Value = edit_Recipe.Datamatrix_items(item_idx).dm_size

                'fill angle
                NumericUpDown12.Value = edit_Recipe.Datamatrix_items(item_idx).dm_fill_angle

                'fill spacing
                NumericUpDown13.Value = edit_Recipe.Datamatrix_items(item_idx).dm_fill_spacing

                'beam diameter
                NumericUpDown14.Value = edit_Recipe.Datamatrix_items(item_idx).dm_beam_diameter

                'fill direction
                ComboBox2.Items.Clear()
                For i = 0 To (Fill_direction_items.Length - 1)
                    ComboBox2.Items.Add(Fill_direction_items(i))
                Next
                ComboBox2.SelectedIndex = edit_Recipe.Datamatrix_items(item_idx).dm_direction

                'fill type
                ComboBox3.Items.Clear()
                For i = 0 To (Fill_type_items.Length - 1)
                    ComboBox3.Items.Add(Fill_type_items(i))
                Next
                ComboBox3.SelectedIndex = edit_Recipe.Datamatrix_items(item_idx).dm_grid_type

                'optimization
                ComboBox4.Items.Clear()
                For i = 0 To (Optimization_items.Length - 1)
                    ComboBox4.Items.Add(Optimization_items(i))
                Next
                ComboBox4.SelectedIndex = edit_Recipe.Datamatrix_items(item_idx).dm_optimization

                'Energy, pitch
                NumericUpDown10.Value = edit_Recipe.Datamatrix_items(item_idx).dm_energy
                NumericUpDown11.Value = edit_Recipe.Datamatrix_items(item_idx).dm_pitch

        End Select

        loadingUi = False
        createDesign()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        OpenFileDialog1.InitialDirectory = "c:\"
        OpenFileDialog1.Filter = "dxf files (*.dxf)|*.dxf"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.RestoreDirectory = False

        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            edit_Recipe.Logo_items(actual_type_idx.idx).l_path = OpenFileDialog1.FileName
            Label16.Text = edit_Recipe.Logo_items(actual_type_idx.idx).l_path
            PictureBox1.Invalidate()
        End If

    End Sub

    'ComboBox selezione campo "ITEM"
    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged

        Dim sel_id As type_and_idx

        If (actual_type_idx.idx <> -1) Then     'And (actual_type = type_to_show.NO_TYPE))
            'alla prima entrata nell'evento non c'è nulla da salvare
            sel_id = Get_id_from_combo(actual_type_idx.idx)
            Get_data_from_UI(actual_type_idx.type, actual_type_idx.idx)
        End If

        actual_type_idx = Get_id_from_combo(ComboBox5.SelectedIndex)
        Show_hide_controls(actual_type_idx.type)
        Populate_UI(actual_type_idx.type, actual_type_idx.idx)
        GroupBox1.Text = ComboBox5.SelectedItem

    End Sub

    Private Function Get_id_from_combo(idx As Integer) As type_and_idx

        Dim t_i As type_and_idx

        If ((idx >= 0) And (idx <= 7)) Then
            t_i.type = type_to_show.TEXT_LINE
            t_i.idx = idx
        ElseIf (idx = 8) Then
            t_i.type = type_to_show.SERIAL_LINE
            t_i.idx = 9
        ElseIf ((idx >= 9) And (idx <= 12)) Then
            t_i.type = type_to_show.LOGO
            t_i.idx = idx - 9
        ElseIf ((idx >= 13) And (idx <= 14)) Then
            t_i.type = type_to_show.DATAMATRIX
            t_i.idx = idx - 13
        Else
            t_i.type = type_to_show.NO_TYPE
            t_i.idx = -1
        End If

        Return t_i

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles cmd_SaveParams.Click
        'update item
        actual_type_idx = Get_id_from_combo(ComboBox5.SelectedIndex)
        Get_data_from_UI(actual_type_idx.type, actual_type_idx.idx)
        If (actual_type_idx.type = type_to_show.TEXT_LINE) Then
            Update_item(edit_Recipe.Line_items(actual_type_idx.idx))
        ElseIf (actual_type_idx.type = type_to_show.SERIAL_LINE) Then
            Update_item(edit_Recipe.Line_items(9))
        ElseIf (actual_type_idx.type = type_to_show.LOGO) Then
            Update_item(edit_Recipe.Logo_items(actual_type_idx.idx))
        ElseIf (actual_type_idx.type = type_to_show.DATAMATRIX) Then
            Update_item(edit_Recipe.Datamatrix_items(actual_type_idx.idx))
        End If
        PictureBox1.Invalidate()

    End Sub


    Public Sub createDesign()
        PictureBox1.Invalidate()
    End Sub

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
        g.Clear(Color.White)

        Dim scale As Single = GetPreviewScale()
        Dim origin As New PointF(PictureBox1.ClientSize.Width / 2.0F, PictureBox1.ClientSize.Height / 2.0F)

        DrawLaserField(g, origin, scale)

        If edit_Recipe.Line_items IsNot Nothing Then
            For i As Integer = 0 To edit_Recipe.Line_items.Length - 1
                DrawTextItem(g, edit_Recipe.Line_items(i), i, origin, scale)
            Next
        End If

        If edit_Recipe.Datamatrix_items IsNot Nothing Then
            For i As Integer = 0 To edit_Recipe.Datamatrix_items.Length - 1
                DrawDataMatrixItem(g, edit_Recipe.Datamatrix_items(i), i, origin, scale)
            Next
        End If

        If edit_Recipe.Logo_items IsNot Nothing Then
            For i As Integer = 0 To edit_Recipe.Logo_items.Length - 1
                DrawLogoItem(g, edit_Recipe.Logo_items(i), i, origin, scale)
            Next
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles cmd_RefreshGraph.Click
        SaveSelectedItemFromUi()
        PictureBox1.Invalidate()
    End Sub

    Private Sub PreviewControlChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged, CheckBox2.CheckedChanged, CheckBox3.CheckedChanged, CheckBox4.CheckedChanged,
        TextBox1.TextChanged, TextBox2.TextChanged, TextBox3.TextChanged, ComboBox1.SelectedIndexChanged, ComboBox2.SelectedIndexChanged, ComboBox3.SelectedIndexChanged, ComboBox4.SelectedIndexChanged, ComboBox6.SelectedIndexChanged,
        NumericUpDown1.ValueChanged, NumericUpDown2.ValueChanged, NumericUpDown3.ValueChanged, NumericUpDown4.ValueChanged, NumericUpDown5.ValueChanged, NumericUpDown6.ValueChanged, NumericUpDown7.ValueChanged,
        NumericUpDown8.ValueChanged, NumericUpDown9.ValueChanged, NumericUpDown10.ValueChanged, NumericUpDown11.ValueChanged, NumericUpDown12.ValueChanged, NumericUpDown13.ValueChanged, NumericUpDown14.ValueChanged,
        NumericUpDown15.ValueChanged, NumericUpDown16.ValueChanged, NumericUpDown19.ValueChanged

        If loadingUi OrElse actual_type_idx.idx < 0 Then
            Exit Sub
        End If

        SaveSelectedItemFromUi()
        PictureBox1.Invalidate()
    End Sub

    Private Sub SaveSelectedItemFromUi()
        If loadingUi OrElse actual_type_idx.idx < 0 Then
            Exit Sub
        End If

        Get_data_from_UI(actual_type_idx.type, actual_type_idx.idx)
    End Sub

    Private Function GetPreviewScale() As Single
        Dim margin As Single = 24.0F
        Dim w As Single = Math.Max(1.0F, PictureBox1.ClientSize.Width - (margin * 2.0F))
        Dim h As Single = Math.Max(1.0F, PictureBox1.ClientSize.Height - (margin * 2.0F))
        Return Math.Min(w, h) / focaleLaser
    End Function

    Private Function ToPreviewPoint(x As Single, y As Single, origin As PointF, scale As Single) As PointF
        Return New PointF(origin.X + (x * scale), origin.Y - (y * scale))
    End Function

    Private Sub DrawLaserField(g As Graphics, origin As PointF, scale As Single)
        Dim fieldSize As Single = focaleLaser * scale
        Dim field As New RectangleF(origin.X - (fieldSize / 2.0F), origin.Y - (fieldSize / 2.0F), fieldSize, fieldSize)

        Using borderPen As New Pen(Color.Firebrick, 2.0F),
              axisPen As New Pen(Color.LightSkyBlue, 1.0F),
              gridPen As New Pen(Color.Gainsboro, 1.0F)

            For mm As Integer = -50 To 50 Step 10
                Dim x1 = ToPreviewPoint(mm, -focaleLaser / 2.0F, origin, scale)
                Dim x2 = ToPreviewPoint(mm, focaleLaser / 2.0F, origin, scale)
                g.DrawLine(gridPen, x1, x2)

                Dim y1 = ToPreviewPoint(-focaleLaser / 2.0F, mm, origin, scale)
                Dim y2 = ToPreviewPoint(focaleLaser / 2.0F, mm, origin, scale)
                g.DrawLine(gridPen, y1, y2)
            Next

            g.DrawRectangle(borderPen, field.X, field.Y, field.Width, field.Height)
            g.DrawLine(axisPen, origin.X, field.Top, origin.X, field.Bottom)
            g.DrawLine(axisPen, field.Left, origin.Y, field.Right, origin.Y)
        End Using
    End Sub

    'disegna il testo nella PictureBox
    Private Sub DrawTextItem(g As Graphics, item As single_item_struct, itemIndex As Integer, origin As PointF, scale As Single)
        If item.item_type = 0 Then
            Exit Sub
        End If

        Dim textToDraw As String = GetTextPreview(item, itemIndex)
        If String.IsNullOrEmpty(textToDraw) Then
            Exit Sub
        End If

        Dim fontName As String = item.t_fontName
        If String.IsNullOrWhiteSpace(fontName) OrElse FontExists(fontName) = False Then
            fontName = "Arial"
        End If

        Dim fontSize As Single = Math.Max(1.0F, item.t_height * scale * Math.Max(0.1F, Math.Abs(item.t_y_scale)))
        Using font As New Font(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel),
              textBrush As New SolidBrush(If(item.laser_on_back, Color.DarkSlateBlue, Color.Black))

            Dim state As GraphicsState = g.Save()
            Dim p = ToPreviewPoint(item.t_x_pos, item.t_y_pos, origin, scale)
            g.TranslateTransform(p.X, p.Y)
            g.RotateTransform(-item.t_z_rotate)
            g.ScaleTransform(Math.Max(0.1F, Math.Abs(item.t_x_scale)), 1.0F)

            Dim textSize As SizeF = g.MeasureString(textToDraw, font)
            g.DrawString(textToDraw, font, textBrush, -textSize.Width / 2.0F, -textSize.Height / 2.0F)
            g.Restore(state)
        End Using
    End Sub

    Private Function GetTextPreview(item As single_item_struct, itemIndex As Integer) As String
        If itemIndex = 9 Then
            Return item.t_before & "000001" & item.t_after
        End If

        Return item.t_text
    End Function

    Private Sub DrawDataMatrixItem(g As Graphics, item As single_item_struct, itemIndex As Integer, origin As PointF, scale As Single)
        If item.item_type = 0 Then
            Exit Sub
        End If

        Dim side As Single = Math.Max(1.0F, item.dm_size * scale)
        Dim state As GraphicsState = g.Save()
        Dim p = ToPreviewPoint(item.dm_x_pos, item.dm_y_pos, origin, scale)
        g.TranslateTransform(p.X, p.Y)
        g.RotateTransform(-item.dm_z_rotate)

        Dim rect As New RectangleF(-side / 2.0F, -side / 2.0F, side, side)
        Using fillBrush As New SolidBrush(If(item.dm_inverted, Color.Black, Color.White)),
              borderPen As New Pen(Color.DarkGreen, 2.0F),
              modulePen As New Pen(Color.DarkGreen, 1.0F),
              labelBrush As New SolidBrush(Color.DarkGreen)

            g.FillRectangle(fillBrush, rect)
            g.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width, rect.Height)

            For i As Integer = 1 To 4
                Dim stepSize As Single = side / 5.0F
                g.DrawLine(modulePen, rect.Left + (i * stepSize), rect.Top, rect.Left + (i * stepSize), rect.Bottom)
                g.DrawLine(modulePen, rect.Left, rect.Top + (i * stepSize), rect.Right, rect.Top + (i * stepSize))
            Next

            Using labelFont As New Font("Arial", Math.Max(8.0F, side / 5.0F), FontStyle.Bold, GraphicsUnit.Pixel)
                g.DrawString("DM" & (itemIndex + 1).ToString, labelFont, labelBrush, rect.Left, rect.Bottom + 2.0F)
            End Using
        End Using

        g.Restore(state)
    End Sub

    Private Sub DrawLogoItem(g As Graphics, item As single_item_struct, itemIndex As Integer, origin As PointF, scale As Single)
        If item.item_type = 0 Then
            Exit Sub
        End If

        Dim width As Single = Math.Max(4.0F, 20.0F * Math.Max(0.1F, Math.Abs(item.l_x_scale)) * scale)
        Dim height As Single = Math.Max(4.0F, 12.0F * Math.Max(0.1F, Math.Abs(item.l_y_scale)) * scale)
        Dim state As GraphicsState = g.Save()
        Dim p = ToPreviewPoint(item.l_x_pos, item.l_y_pos, origin, scale)
        g.TranslateTransform(p.X, p.Y)
        g.RotateTransform(-item.l_z_rotate)

        Dim rect As New RectangleF(-width / 2.0F, -height / 2.0F, width, height)
        Using logoPen As New Pen(Color.SteelBlue, 2.0F),
              logoBrush As New SolidBrush(Color.FromArgb(35, Color.SteelBlue)),
              labelBrush As New SolidBrush(Color.SteelBlue),
              labelFont As New Font("Arial", 9.0F, FontStyle.Regular, GraphicsUnit.Pixel)

            g.FillRectangle(logoBrush, rect)
            g.DrawRectangle(logoPen, rect.X, rect.Y, rect.Width, rect.Height)
            g.DrawLine(logoPen, rect.Left, rect.Top, rect.Right, rect.Bottom)
            g.DrawLine(logoPen, rect.Right, rect.Top, rect.Left, rect.Bottom)

            Dim label As String = "Logo " & (itemIndex + 1).ToString
            If String.IsNullOrWhiteSpace(item.l_path) = False Then
                label = Path.GetFileNameWithoutExtension(item.l_path)
            End If
            g.DrawString(label, labelFont, labelBrush, rect.Left, rect.Bottom + 2.0F)
        End Using

        g.Restore(state)
    End Sub

End Class

Public Enum type_to_show
    NO_TYPE = 0
    TEXT_LINE
    SERIAL_LINE
    LOGO
    DATAMATRIX
End Enum
Public Structure type_and_idx
    Public type As type_to_show
    Public idx As Integer
End Structure
