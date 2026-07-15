Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports System.Xml
Imports System.Xml.Serialization

Public Class Recipe_management_dlg

    Private selected_recipe_idx As Integer
    Private selected_recipe_text As String
    Private new_Recipe As recipe_data
    Private import_Recipe As recipe_data

    'Private new_Line_items(9) As single_item_struct
    'Private new_Logo_items(9) As single_item_struct
    'Private new_Datamatrix_items(9) As single_item_struct
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Recipe_management_dlg_Load(sender As Object, e As EventArgs) Handles Me.Load

        ReDim new_Recipe.Line_items(9)
        ReDim new_Recipe.Logo_items(9)
        ReDim new_Recipe.Datamatrix_items(9)

        'popola l'elenco delle ricette
        Update_recipe_list()
    End Sub

    Private Sub Update_recipe_list()
        'popola l'elenco delle ricette
        Dim recipe_list As String() = Get_all_recipes()
        ListBox1.Items.Clear()
        For Each rec As String In recipe_list
            ListBox1.Items.Add(rec)
        Next rec
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'nuova ricetta
        Dim str As String
        Using new_recipe_dlg = New Input_string_dlg(0, "NEW RECIPE")
            If new_recipe_dlg.ShowDialog() = DialogResult.OK Then
                str = new_recipe_dlg.Get_recipe
                If (Check_recipe_existing(str) = True) Then
                    MsgBox("Recipe existing - unable to proceed")
                Else
                    Create_new_recipe_from_scratch(str)
                End If
            End If
        End Using
        'popola l'elenco delle ricette
        Update_recipe_list()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'clona ricetta
        Dim str As String
        Using new_recipe_dlg = New Input_string_dlg(0, selected_recipe_text)
            If new_recipe_dlg.ShowDialog() = DialogResult.OK Then
                str = new_recipe_dlg.Get_recipe
                If (Check_recipe_existing(str) = True) Then
                    MsgBox("Recipe existing - unable to proceed")
                Else
                    edit_Recipe.Recipe_name = str
                    Create_new_recipe_from_existing(edit_Recipe)
                End If
            End If
        End Using

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        selected_recipe_idx = ListBox1.SelectedIndex
        selected_recipe_text = ListBox1.SelectedItem
        edit_Recipe.Recipe_name = selected_recipe_text

        Carica_ricetta(edit_Recipe, 99)
        Update_UI()

    End Sub

    Private Sub Create_new_recipe_from_scratch(rec As String)

        Dim i As Integer

        For i = 0 To 7
            Create_item_from_scratch(new_Recipe.Line_items(i), 1, i)
            Insert_new_item(new_Recipe.Line_items(i))
        Next

        Create_item_from_scratch(new_Recipe.Line_items(9), 1, 9)
        Insert_new_item(new_Recipe.Line_items(9))

        For i = 0 To 3
            Create_item_from_scratch(new_Recipe.Logo_items(i), 2, i)
            Insert_new_item(new_Recipe.Logo_items(i))
        Next

        For i = 0 To 1
            Create_item_from_scratch(new_Recipe.Datamatrix_items(i), 3, i)
            Insert_new_item(new_Recipe.Datamatrix_items(i))
        Next

        new_Recipe.Recipe_name = rec
        new_Recipe.Recipe_description = "This is a new recipe"
        new_Recipe.Recipe_active = True
        new_Recipe.Associated_station = 99
        new_Recipe.Line_1 = new_Recipe.Line_items(0).idx
        new_Recipe.Line_2 = new_Recipe.Line_items(1).idx
        new_Recipe.Line_3 = new_Recipe.Line_items(2).idx
        new_Recipe.Line_4 = new_Recipe.Line_items(3).idx
        new_Recipe.Line_5 = new_Recipe.Line_items(4).idx
        new_Recipe.Line_6 = new_Recipe.Line_items(5).idx
        new_Recipe.Line_7 = new_Recipe.Line_items(6).idx
        new_Recipe.Line_8 = new_Recipe.Line_items(7).idx
        new_Recipe.Serial = new_Recipe.Line_items(9).idx
        new_Recipe.Logo_1 = new_Recipe.Logo_items(0).idx
        new_Recipe.Logo_2 = new_Recipe.Logo_items(1).idx
        new_Recipe.Logo_3 = new_Recipe.Logo_items(2).idx
        new_Recipe.Logo_4 = new_Recipe.Logo_items(3).idx
        new_Recipe.Datamatrix_1 = new_Recipe.Datamatrix_items(0).idx
        new_Recipe.Datamatrix_2 = new_Recipe.Datamatrix_items(1).idx
        new_Recipe.Cloning = 1
        new_Recipe.X_cloning = 0.0
        new_Recipe.Y_cloning = 0.0
        new_Recipe.Enable_laser_on_back = False

        Insert_new_recipe(new_Recipe)

    End Sub

    Private Sub Create_item_from_scratch(ByRef my_item As single_item_struct, my_type As Integer, index As Integer)
        'my_type: 0-->disabled, 1-->text, 2-->logo, 3-->DataMatrix

        'Idx
        my_item.item_type = my_type
        my_item.laser_on_back = False
        my_item.t_font = IpgMarkingGraphicsLibrary.HersheyFont.Sans
        my_item.t_fontName = ""
        my_item.t_text = "Line " & (index + 1).ToString
        my_item.t_before = "Before "
        my_item.t_after = "After "
        my_item.t_x_pos = 0.0
        my_item.t_y_pos = 0.0
        my_item.t_x_scale = 1.0
        my_item.t_y_scale = 1.0
        my_item.t_z_scale = 1.0
        my_item.t_x_rotate = 0.0
        my_item.t_y_rotate = 0.0
        my_item.t_z_rotate = 0.0
        my_item.t_height = 5.0
        my_item.t_energy = 0.0007
        my_item.t_pitch = 0.005
        my_item.l_path = "C.\nofile.dxf"
        my_item.l_x_pos = 0.0
        my_item.l_y_pos = 0.0
        my_item.l_x_scale = 1.0
        my_item.l_y_scale = 1.0
        my_item.l_z_scale = 1.0
        my_item.l_x_rotate = 0.0
        my_item.l_y_rotate = 0.0
        my_item.l_z_rotate = 0.0
        my_item.l_energy = 0.0004
        my_item.l_pitch = 0.003
        my_item.dm_text = "Datamatrix " & index.ToString
        my_item.dm_size = 8.8
        my_item.dm_inverted = False
        my_item.dm_border = 1.0
        my_item.dm_x_pos = 0.0
        my_item.dm_y_pos = 0.0
        my_item.dm_z_rotate = 0.0
        my_item.dm_fill_angle = 45.0
        my_item.dm_fill_spacing = 0.07
        my_item.dm_beam_diameter = 0.04
        my_item.dm_direction = IpgMarkingGraphicsLibrary.FillDirection.BiDirection
        my_item.dm_grid_type = IpgMarkingGraphicsLibrary.FillType.EdgeToEdge
        my_item.dm_pitch = 0.02
        my_item.dm_energy = 0.00015
        my_item.dm_optimization = IpgMarkingGraphicsLibrary.Optimization.Quality
        my_item.txt_rounded = 0
        my_item.txt_round_dim = 0

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Recipe_edit_dlg.Height = Screen.PrimaryScreen.Bounds.Height
        If (Recipe_edit_dlg.ShowDialog = DialogResult.OK) Then

        End If

    End Sub

    Private Sub Update_UI()
        RichTextBox1.Text = edit_Recipe.Recipe_description
        CheckBox1.Checked = edit_Recipe.Recipe_active
        CheckBox2.Checked = edit_Recipe.Enable_laser_on_back
        NumericUpDown1.Value = edit_Recipe.Associated_station
        NumericUpDown2.Value = edit_Recipe.Cloning
        NumericUpDown3.Value = edit_Recipe.X_cloning
        NumericUpDown4.Value = edit_Recipe.Y_cloning

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        edit_Recipe.Recipe_description = RichTextBox1.Text
        edit_Recipe.Recipe_active = CheckBox1.Checked
        edit_Recipe.Enable_laser_on_back = CheckBox2.Checked
        edit_Recipe.Associated_station = NumericUpDown1.Value
        edit_Recipe.Cloning = NumericUpDown2.Value
        edit_Recipe.X_cloning = NumericUpDown3.Value
        edit_Recipe.Y_cloning = NumericUpDown4.Value
        Update_recipe(edit_Recipe)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'export data to xml

        Dim f_name As String

        SFD1.Filter = "Recipe data (*.xml)|*.xml"

        If SFD1.ShowDialog = DialogResult.OK Then
            f_name = SFD1.FileName
        Else
            Exit Sub
        End If

        Dim serializer As New XmlSerializer(GetType(recipe_data), New XmlRootAttribute("Recipe_serializer"))
        Using file As System.IO.FileStream = System.IO.File.Open(f_name, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
            serializer.Serialize(file, edit_Recipe)
        End Using

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'import data from xml

        Dim f_name As String
        Dim str As String

        OFD1.Filter = "Recipe data (*.xml)|*.xml"

        If OFD1.ShowDialog = DialogResult.OK Then
            f_name = OFD1.FileName
        Else
            Exit Sub
        End If

        Dim serializer As New XmlSerializer(GetType(recipe_data), New XmlRootAttribute("Recipe_serializer"))
        Using file = System.IO.File.OpenRead(f_name)
            import_Recipe = serializer.Deserialize(file)
        End Using

        If (Check_recipe_existing(import_Recipe.Recipe_name) = True) Then
            'il nome della ricetta esiste già, l'utente deve cambiarlo

            Using new_recipe_dlg = New Input_string_dlg(0, "IMPORTED RECIPE")
                If new_recipe_dlg.ShowDialog() = DialogResult.OK Then
                    str = new_recipe_dlg.Get_recipe
                    If (Check_recipe_existing(str) = True) Then
                        MsgBox("Recipe existing - unable to proceed")
                        Exit Sub
                    Else
                        import_Recipe.Recipe_name = str
                    End If
                End If
            End Using

        End If

        Create_new_recipe_from_existing(import_Recipe)

    End Sub

    Private Sub Create_new_recipe_from_existing(ByRef rec As recipe_data)
        'crea una nuova ricetta da una esistente
        'il nome deve già essere univoco; devono essere aggiornati gli indici ed i riferimenti

        Insert_new_item(rec.Line_items(0))
        Insert_new_item(rec.Line_items(1))
        Insert_new_item(rec.Line_items(2))
        Insert_new_item(rec.Line_items(3))
        Insert_new_item(rec.Line_items(4))
        Insert_new_item(rec.Line_items(5))
        Insert_new_item(rec.Line_items(6))
        Insert_new_item(rec.Line_items(7))
        Insert_new_item(rec.Line_items(9))

        Insert_new_item(rec.Logo_items(0))
        Insert_new_item(rec.Logo_items(1))
        Insert_new_item(rec.Logo_items(2))
        Insert_new_item(rec.Logo_items(3))

        Insert_new_item(rec.Datamatrix_items(0))
        Insert_new_item(rec.Datamatrix_items(1))

        rec.Line_1 = rec.Line_items(0).idx
        rec.Line_2 = rec.Line_items(1).idx
        rec.Line_3 = rec.Line_items(2).idx
        rec.Line_4 = rec.Line_items(3).idx
        rec.Line_5 = rec.Line_items(4).idx
        rec.Line_6 = rec.Line_items(5).idx
        rec.Line_7 = rec.Line_items(6).idx
        rec.Line_8 = rec.Line_items(7).idx
        rec.Serial = rec.Line_items(9).idx
        rec.Logo_1 = rec.Logo_items(0).idx
        rec.Logo_2 = rec.Logo_items(1).idx
        rec.Logo_3 = rec.Logo_items(2).idx
        rec.Logo_4 = rec.Logo_items(3).idx
        rec.Datamatrix_1 = rec.Datamatrix_items(0).idx
        rec.Datamatrix_2 = rec.Datamatrix_items(1).idx

        Insert_new_recipe(rec)
        'popola l'elenco delle ricette
        Update_recipe_list()

    End Sub

End Class
