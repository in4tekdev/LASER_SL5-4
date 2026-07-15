Imports System.Security.Cryptography.X509Certificates
Imports Opc.UaFx
Imports Opc.UaFx.Client

Public Class OPCClass
    Public command As OpcSubscribeDataChange()
    Public client As OpcClient
    Public subscription As OpcSubscription
    Public IsConnected As Boolean = False

    Public Sub New(ip As String, port As String)
        Try
            Opc.UaFx.Client.Licenser.LicenseKey = "AALOERR5OO7EKFNQCABINGCH6TYOVHPLFC2QCUEAULJ635JH4VDJ4QAJYCEU6H2SO2Z7BJWGHUWWXQ5HKWI7OFVYYMERDPQDC7ZW7ZPKO3MPWJYY2JO3D2AR4WXCEJW2YBBPBRGK6SBRI4DBXF4NGVKZUATMW3VI7EALG5FQNKFTYIDJGPOBOCH2GPO5NTO5BUHPZQSXZNGAUSILQX56E6NCBRCDX35VJBWLQDD4QANXIWUKO7D3Q7SWDDL55ZZCSN7NLHKB3W5O524VIXFPLVJIYKM5E5KPN7VW3YA3AM6LFRE7DMOEYZDTRZIMJAECFDX6FSTN35KY7IHF6YTSOBJQKN3WAK3WP73IGCJTPE3WCZLD3EJBCPIAFRVTMSZ6FKB3CB6UQA5WHHF4SEH3X3LCDQK33TH4D4OUTREFWGGJ5QIILEVAMONY3KVQG7AFUTLKGTLH3L4TPKOP5ITEBY36XPV6PHM6QLUPEKROXB2L65WBVXNTKNMHMLHTWWLQOZDE4LMN6VL2KTQLV6HZCPUYAEOEZLM4QQHA4LDVQCUXNRFW5GCYXEJMNCWRZ6JNHXNAOCSVYBDJ4YT2JV5NMQO2YM756MOUJNI5AVEM2YTDTLACIIOKI3BM7HXYJZBTHFME2O7ONQLRMFIQQF72YOWHVCRLLWGZOJUTWDOCM54BJ33JJSH47PQR2KXHT7ODF7PJ4X5MEOWVBH2TUMRAGMPGVQ42QIAR4B5FG6DXO3P6USHICVPFSCNSIELNCZEHKGUZTVQ42ADLKW4GDACQ4ADF6VD42TEYUXGOTW4DX7VH5IZ65IWSKWK7AGBNSSYHHJVPIM3F7YWTRM2BONX6E7MOJ3Q2QYN72ZRSONDSURQ2EF4VCOQH27IKSG6FPVWQOTTTY6RGGMLGSL2L5E7VOAJJKD63NPWEYB4WMIYBHNVSROYPWDRGKEEPRHIYJADAGK33P4HP3J5EOBEQWJ5ACJICDLGXEC3CSOXIJD25U5MYWYPLSTXO4KQPBSVD3HXA5PWD737ZGYOWZB54LHUQNIJDYDQPMBZDFWXKXI5F7YXC62RGXH6YGQSVYJH4CG7SNI47YKIKKRM6PQQKDPFH4ZTDOJTHYZLOZPCBLLXPVS6ZIFNMH6YIBR3IYZ2LL4J73PEEYHHKDUUJKC3GH4FSQQRK4RVYMFV6WHK46K73VPRXCK3EROC54KGAE74PJOK4UTOPPHZWT6PMLPFIDXWI3T6VNV7YJQK5AXQ7B3AA6VHTERJ7W6UL4ADMFQUQEV3367BPNNQNQ5KB"
            client = New OpcClient("opc.tcp://" + ip + ":" + port + "/")
            client.Connect()
            IsConnected = True
        Catch ex As Exception
            IsConnected = False
            AddOpcLog("ERRORE OPC CONNECT", ex.Message)
        End Try
    End Sub

    Public Function OPC_Write(node As String, Value As String) As Boolean
        Try
            If IsConnected = False OrElse client Is Nothing Then
                Return False
            End If

            Dim result As OpcStatus = client.WriteNode("ns=3;s=" + node, Value)
            Return result.IsGood
        Catch ex As Exception
            AddOpcLog("ERRORE SCRITTURA OPC", ex.Message)
            IsConnected = False
            Return False
        End Try
    End Function

    Public Function OPC_Write(node As String, Value As Boolean) As Boolean
        Try
            AddOpcLog("SCRITTURA OPC:", "NODO: " + node + " VALORE: " + Value.ToString)
            If IsConnected = False OrElse client Is Nothing Then
                Return False
            End If

            Dim result As OpcStatus = client.WriteNode("ns=3;s=" + node, Value)
            Return result.IsGood
        Catch ex As Exception
            AddOpcLog("ERRORE SCRITTURA OPC", ex.Message)
            IsConnected = False
            Return False
        End Try
    End Function

    Public Function OPC_Read(node As String) As OpcValue
        Try
            If IsConnected = False OrElse client Is Nothing Then
                Return Nothing
            End If

            Dim res As OpcValue = client.ReadNode("ns=3;s=" + node)
            If res.Status.IsGood Then
                Return res
            End If
        Catch ex As Exception
            AddOpcLog("ERRORE LETTURA OPC", ex.Message)
            IsConnected = False
        End Try

        Return Nothing
    End Function

    Public Function OPC_Read_String(node As String, ByRef value As String) As Boolean
        Dim res As OpcValue = OPC_Read(node)
        If res Is Nothing OrElse res.Value Is Nothing Then
            value = ""
            Return False
        End If

        value = res.Value.ToString
        Return True
    End Function

    Public Sub Subscribe()
        Try
            If IsConnected = False OrElse client Is Nothing Then
                AddOpcLog("SOTTOSCRIZIONE OPC NON ATTIVA", "Client OPC non connesso")
                Exit Sub
            End If

            Me.subscription = Me.client.SubscribeDataChange("ns=3;s=" + NODO_START_LASER, AddressOf Me.HandleDataChangeReceived)
            'Me.subscription = Me.client.SubscribeDataChange("ns=2;i=" + Nodo_seriale, AddressOf Me.HandleDataChangeReceived)
            subs_cnt = (subscription.MonitoredItems.Count)
            Dim monitoredItem = Me.subscription.MonitoredItems(0)

        Catch ex As Exception
            IsConnected = False
            AddOpcLog("ERRORE SOTTOSCRIZIONE OPC", ex.Message)
        End Try
    End Sub

    Private Sub HandleDataChangeReceived(ByVal sender As Object, ByVal e As OpcDataChangeReceivedEventArgs)
        Try
            If subs_cnt > 0 Then
                subs_cnt -= 1
            Else
                If e.MonitoredItem.NodeId.ValueAsString = NODO_START_LASER And e.Item.Value.ToString = "True" Then
                    AddOpcLog("COMANDO RICEVUTO", "START LASERATURA")
                    Plc_status.pronto_per_laser_1 = True
                    OPC_Read_String(NODO_RICETTA_LASER, las1.recipe)
                    OPC_Read_String(NODO_ID_LASER, las1.TrackID)
                    OPC_Read_String(NODO_COMMESSA_LASER, las1.batch)
                End If
            End If
        Catch ex As Exception
            AddOpcLog("ERRORE EVENTO OPC", ex.Message)
            IsConnected = False
        End Try
    End Sub


    Public Sub OPC_Disconnect()
        Try
            If client IsNot Nothing AndAlso IsConnected Then
                client.Disconnect()
            End If
        Catch ex As Exception
            AddOpcLog("ERRORE DISCONNESSIONE OPC", ex.Message)
        Finally
            IsConnected = False
        End Try
    End Sub

    Private Sub AddOpcLog(msg1 As String, msg2 As String)
        Try
            If Logs IsNot Nothing Then
                Logs.Add_log_element(msg1, msg2)
            End If
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub
End Class
