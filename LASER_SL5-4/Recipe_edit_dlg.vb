Imports System.Drawing.Text

Public Class Recipe_edit_dlg

    Private actual_type_idx As type_and_idx
    'Private actual_idx As Integer = -1
    'Private actual_type As type_to_show = type_to_show.NO_TYPE

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Recipe_edit_dlg_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WindowState = WindowState.Maximized
        actual_type_idx.idx = -1
        actual_type_idx.type = type_to_show.NO_TYPE
        ComboBox5.SelectedIndex = 0

        'TreeView1.ExpandAll()
        'GroupBox1.Visible = True

    End Sub

    'Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick

    '    Dim data_idx As Integer = 0

    '    Select Case e.Node.Tag

    '        Case 1, 2, 3     'è stato selezionato un nodo padre, nascondo tutto il group box
    '            GroupBox1.Visible = False

    '        Case 10 To 15       'è stato selezionato un nodo di linea di testo
    '            GroupBox1.Visible = True
    '            data_idx = e.Node.Tag - 10
    '            GroupBox1.Text = "Line " & (data_idx + 1).ToString
    '            actual_type = type_to_show.TEXT_LINE
    '            Show_hide_controls(type_to_show.TEXT_LINE)
    '            Populate_UI(type_to_show.TEXT_LINE, data_idx)

    '        Case 19             'è stato selezionato il nodo del serial number
    '            GroupBox1.Visible = True
    '            data_idx = e.Node.Tag - 10
    '            GroupBox1.Text = "Serial number line"
    '            actual_type = type_to_show.SERIAL_LINE
    '            Show_hide_controls(type_to_show.SERIAL_LINE)
    '            Populate_UI(type_to_show.SERIAL_LINE, data_idx)

    '        Case 20 To 22       'è stato selezionato il nodo di un logo
    '            GroupBox1.Visible = True
    '            data_idx = e.Node.Tag - 20
    '            GroupBox1.Text = "Logo " & (data_idx + 1).ToString
    '            actual_type = type_to_show.LOGO
    '            Show_hide_controls(type_to_show.LOGO)
    '            Populate_UI(type_to_show.LOGO, data_idx)

    '        Case 30 To 31       'è stato selezionato il nodo di un datamatrix
    '            GroupBox1.Visible = True
    '            data_idx = e.Node.Tag - 30
    '            GroupBox1.Text = "Data matrix " & (data_idx + 1).ToString
    '            actual_type = type_to_show.DATAMATRIX
    '            Show_hide_controls(type_to_show.DATAMATRIX)
    '            Populate_UI(type_to_show.DATAMATRIX, data_idx)

    '    End Select

    '    actual_idx = data_idx

    'End Sub

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
                edit_Recipe.Line_items(item_idx).t_fontName = ComboBox6.SelectedItem.ToString

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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
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

    End Sub


    Public Sub createDesign()
        For i As Integer = 0 To edit_Recipe.Line_items.Count - 1
            If edit_Recipe.Line_items(i).item_type <> 0 Then
                drawText(i).enable = True
                drawText(i).text = edit_Recipe.Line_items(i).t_text
                If String.IsNullOrEmpty(edit_Recipe.Line_items(i).t_font) Then
                    drawText(i).font = edit_Recipe.Line_items(i).t_font
                Else
                    drawText(i).font = edit_Recipe.Line_items(i).t_fontName
                End If

                drawText(i).textSize = edit_Recipe.Line_items(i).t_height
                drawText(i).x_pos = edit_Recipe.Line_items(i).t_x_pos
                drawText(i).y_pos = edit_Recipe.Line_items(i).t_y_pos
                drawText(i).x_scale = edit_Recipe.Line_items(i).t_x_scale
                drawText(i).y_scale = edit_Recipe.Line_items(i).t_y_scale
                drawText(i).x_rotate = edit_Recipe.Line_items(i).t_z_rotate
            Else
                drawText(i).enable = False
            End If
        Next
    End Sub

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        createDesign()
        Dim g As Graphics = e.Graphics
        g.DrawRectangle(Pens.Red, 0, 0, focaleLaser * magnifier, focaleLaser * magnifier)
        Dim p1 As New System.Drawing.Point(focaleLaser * magnifier / 2, 0)
        Dim p2 As New System.Drawing.Point(focaleLaser * magnifier / 2, focaleLaser * magnifier)
        g.DrawLine(Pens.LightBlue, p1, p2)
        Dim p3 As New System.Drawing.Point(0, focaleLaser * magnifier / 2)
        Dim p4 As New System.Drawing.Point(focaleLaser * magnifier, focaleLaser * magnifier / 2)
        g.DrawLine(Pens.LightBlue, p3, p4)
        ' Disegna del testo

        For Each las In drawText
            If las.enable = True Then
                Dim font As New Font(las.font, CInt((las.textSize / 3) * magnifier))

                ' Salva lo stato grafico
                Dim state = g.Save()
                'PER SETTARE ROTAZIONE

                ' Disegna la stringa (posizionata localmente a 0, 0 dopo la trasformazione)
                'g.Transform = matrix


                g.ScaleTransform(las.x_scale, las.y_scale)

                Dim textSize As SizeF = g.MeasureString(las.text, font)

                ' MODIFICA PERCHE IL PROGRAMMA DELLA IPG IL PUNTO PASSATO E IL CENTRO
                las.x_pos = las.x_pos - (textSize.Width / 2)
                las.y_pos = las.y_pos + (textSize.Height / 2)

                If las.x_rotate < 0 Then
                    ' Applica una trasformazione di scala relativa
                    g.TranslateTransform(center - las.x_pos, center + las.y_pos)
                Else
                    g.TranslateTransform(center + las.x_pos, center - las.y_pos)
                End If

                g.DrawString(las.text, font, Brushes.Black, 0, 0)

                ' Ripristina lo stato grafico originale
                g.Restore(state)

            End If
        Next

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