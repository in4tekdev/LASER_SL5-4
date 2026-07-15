Imports System.Windows.Forms

Public Class Input_string_dlg

    Private rcp_name As String
    Private tipo As Integer '0-->nuova, 1-->modifica

    Public Sub New(my_type As Integer, str As String)

        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().
        rcp_name = str
        tipo = my_type

    End Sub

    Public ReadOnly Property Get_recipe() As String
        Get
            Return rcp_name
        End Get
    End Property

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        rcp_name = TextBox1.Text

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Input_string_dlg_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox1.Text = rcp_name
        If tipo = 0 Then
            Me.Text = "New recipe name"
        ElseIf (tipo = 1) Then
            Me.Text = "Modify recipe name"
        End If
    End Sub
End Class
