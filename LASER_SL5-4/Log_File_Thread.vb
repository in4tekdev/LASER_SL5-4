Imports System.Threading
Imports System.IO
Imports System.Collections.Specialized.BitVector32


Public Class Log_EventArgs
        Inherits System.EventArgs

        Public log_item As String

        Public Sub New(argu As String)
            MyBase.New
            log_item = argu
        End Sub
    End Class



Public Class Log_File_Thread

    Public Event Log_event(ByVal sender As Object, ByVal e As Log_EventArgs)


    'COSTRUTTORI------------------------------------------------------------------
    Public Sub New()

        fname = Station_settings.Path & "/" & Now.Year.ToString("D4") & "_" & Now.Month.ToString("D2") & "_" & Now.Day.ToString("D2") & ".log"
    End Sub



    'COSTRUTTORI END ------------------------------------------------------------------



    'VARIABILI PRIVATE------------------------------------------------------------------
    Private Exit_log_thread As Boolean = False
    Dim fname As String
    'VARIABILI PRIVATE END------------------------------------------------------------------



    'VARIABILI PUBBLICHE------------------------------------------------------------------
    Public Log_loop As Thread
    'VARIABILI PUBBLICHE END------------------------------------------------------------------



    'PROPRIETA------------------------------------------------------------------
    Property Log_exit_thread() As Boolean



        Get
            Return Exit_log_thread
        End Get
        Set(exit_log As Boolean)
            Exit_log_thread = exit_log
        End Set



    End Property
    'PROPRIETA END------------------------------------------------------------------



    'FUNZIONI------------------------------------------------------------------
    Public Sub Log_thread()
        Try

            While (1)

                Thread.Sleep(100)

                If (Log_queue.Count > 0) Then   'se ci sono eventi da loggare, apro il file di log
                    'Dim fname As String = Station_settings.ApplicationLogPath & "/" & Now.Year.ToString("D4") & "_" & Now.Month.ToString("D2") & "_" & Now.Day.ToString("D2") & ".log"
                    Dim lg As String

                    Try
                        Using logFile As New StreamWriter(fname, True)

                            While (Log_queue.Count > 0)
                                lg = Log_queue.Peek
                                lg = lg.Replace(Chr(&H2), "")
                                lg = lg.Replace(Chr(&H17), " ")
                                lg = lg.Replace(Chr(&H3), "")

                                logFile.WriteLine(lg)
                                Message_for_log.Enqueue(lg)
                                Log_queue.Dequeue()
                            End While

                        End Using

                    Catch ex As Exception

                        'MsgBox("Errore di log " & ex.Message)
                    End Try

                End If

                If Exit_log_thread Then
                    Exit While
                End If

            End While

        Catch ex As Exception

        End Try
    End Sub



    Public Sub Add_log_element(msg_1 As String, msg_2 As String)

        Dim elem As String
        elem = (Now.ToString + ":" + Now.Millisecond.ToString + " :" + msg_1 + "; " + msg_2)

        Log_queue.Enqueue(elem)


    End Sub



    Protected Overridable Sub On_new_log_event(ByVal e As Log_EventArgs)
        RaiseEvent Log_event(Me, e)
    End Sub



    'FUNZIONI END------------------------------------------------------------------
End Class


