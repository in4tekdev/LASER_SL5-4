<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Splash
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RTB1 = New System.Windows.Forms.RichTextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(12, 337)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(776, 101)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Database connection problem"
        Me.GroupBox1.Visible = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Black
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(677, 43)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(93, 50)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Exit"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Location = New System.Drawing.Point(248, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(423, 50)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Label2"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBox1.Location = New System.Drawing.Point(6, 19)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(764, 20)
        Me.TextBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(776, 62)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Laser"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RTB1
        '
        Me.RTB1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.RTB1.ForeColor = System.Drawing.Color.White
        Me.RTB1.Location = New System.Drawing.Point(12, 74)
        Me.RTB1.Name = "RTB1"
        Me.RTB1.Size = New System.Drawing.Size(776, 257)
        Me.RTB1.TabIndex = 2
        Me.RTB1.Text = ""
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'Splash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.RTB1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Splash"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Splash"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents RTB1 As RichTextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label2 As Label
    Friend WithEvents Button2 As Button
End Class
