Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Module DBModule

    Public strDownload, strUpload, DCaktif As String
    Public idDC, idJenis As Integer
    Public dr As SqlDataReader
    Public jamSvr As DateTime
    Public j, m, d As Double
    Public nmDc, stsTrf, namaJnenis As String
    Private Function GetConnString(strServer As String)
        Dim reader As New System.Configuration.AppSettingsReader
        Dim strConn As String = ""

        Select Case strServer
            Case "HO"
                strConn = ConfigurationManager.ConnectionStrings("HO").ConnectionString

            Case "DC"
                strConn = ConfigurationManager.ConnectionStrings("DC").ConnectionString

            Case "ODOO"
                strConn = ConfigurationManager.ConnectionStrings("ODOO").ConnectionString

        End Select

        If strConn.Length > 0 Then
            Try
                Dim i As Integer = strConn.IndexOf("[")
                Dim f As String = strConn.Substring(i + 1, strConn.IndexOf("]", i + 1) - i - 1)
                Dim strPwd As String = CryptOrg("D", f)

                strConn = strConn.Replace("[" & f & "]", strPwd)
            Catch ex As Exception
                WriteLog("FunctionGetConnString => " & strServer & " " & ex.Message & Environment.NewLine)
                MsgBox(ex.Message)

            End Try

        End If

        Return strConn
    End Function
    Public Function SelectOdoo(tableName As String) As DataTable
        Dim dt As New DataTable
        Dim connString As String = GetConnString("ODOO")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("SELECT * FROM " + tableName + " WITH (NOLOCK)")
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                cmd.CommandTimeout = 720000

                Try
                    con.Open()
                    Dim myReader As SqlDataReader = cmd.ExecuteReader()
                    dt.Load(myReader)
                    con.Close()

                Catch ex As SqlException
                    WriteLog("Function SelectOdoo => " & tableName & " " & ex.Message & Environment.NewLine)

                End Try


            End Using
        End Using

        Return dt

    End Function
    Public Function InsertOdoo(tableName As String, dt As DataTable) As Int64
        Dim rowAffected As Int64 = 0
        Dim connString As String = GetConnString("ODOO")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("Insert_" + tableName)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = con
                cmd.CommandTimeout = 720000
                cmd.Parameters.AddWithValue("@tbl" + tableName, dt)
                Try
                    con.Open()
                    rowAffected = cmd.ExecuteNonQuery()
                    con.Close()

                Catch ex As SqlException
                    WriteLog("Function InsertOdoo => " & tableName & " " & ex.Message & Environment.NewLine)

                End Try
            End Using
        End Using

        Return rowAffected
    End Function
    Public Function InsertHO(tableName As String, dt As DataTable) As Int64
        Dim rowAffected As Int64 = 0
        Dim connString As String = GetConnString("HO")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("Insert_" + tableName)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = con
                cmd.CommandTimeout = 720000
                cmd.Parameters.AddWithValue("@tbl" + tableName, dt)
                Try
                    con.Open()
                    rowAffected = cmd.ExecuteNonQuery()
                    con.Close()

                Catch ex As SqlException
                    WriteLog("Function InsertHO =>" & tableName & " " & ex.Message & Environment.NewLine)

                End Try

            End Using
        End Using

        Return rowAffected
    End Function
    Public Function InsertJournal(tableName As String, dt As DataTable) As DataTable
        Dim tbl As New DataTable
        Dim connString As String = GetConnString("ODOO")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("Insert_" + tableName)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = con
                cmd.CommandTimeout = 720000
                cmd.Parameters.AddWithValue("@tbl" + tableName, dt)
                Try
                    con.Open()
                    Dim myReader As SqlDataReader = cmd.ExecuteReader()
                    tbl.Load(myReader)
                    con.Close()

                Catch ex As SqlException
                    WriteLog("Function InsertOdoo => " & tableName & " " & ex.Message & Environment.NewLine)

                End Try
            End Using
        End Using

        Return tbl
    End Function
    Public Function SelectDC(tableName As String) As DataTable
        Dim dt As New DataTable
        Dim connString As String = GetConnString("DC")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("Select_" + tableName)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = con
                cmd.CommandTimeout = 720000

                Try
                    con.Open()
                    Dim myReader As SqlDataReader = cmd.ExecuteReader()
                    dt.Load(myReader)
                    con.Close()

                Catch ex As SqlException
                    WriteLog("Function SelectDC => " & tableName & " " & ex.Message & Environment.NewLine)

                End Try


            End Using
        End Using

        Return dt

    End Function
    Public Function FlagDC(tableName As String, dt As DataTable) As Int64
        Dim rowAffected As Int64 = 0
        Dim connString As String = GetConnString("DC")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("Flag_" + tableName)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = con
                cmd.CommandTimeout = 720000
                cmd.Parameters.AddWithValue("@flag" + tableName, dt)
                Try
                    con.Open()
                    rowAffected = cmd.ExecuteNonQuery()
                    con.Close()

                Catch ex As SqlException
                    WriteLog("Function FlagDC => " & tableName & " " & ex.Message & Environment.NewLine)

                End Try

            End Using
        End Using

        Return rowAffected
    End Function
    Public Function FlagSales(strTable As String, dt As DataTable) As Int64
        Dim rowAffected As Int64 = 0
        Dim connString As String = GetConnString("DC")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("Flag_SalesToko")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = con
                cmd.CommandTimeout = 720000
                cmd.Parameters.AddWithValue("@flagSalesToko", dt)
                Try
                    con.Open()
                    rowAffected = cmd.ExecuteNonQuery()
                    con.Close()

                Catch ex As SqlException
                    WriteLog("Function FlagDC => SalesToko:" & strTable & " " & ex.Message & Environment.NewLine)

                End Try

            End Using
        End Using

        Return rowAffected
    End Function

    Public Function GetListTable(strList As String) As DataTable

        Dim Tbl As New DataTable

        Try
            Dim myFolder As String = My.Application.Info.DirectoryPath
            Dim myXMLSh As String = myFolder & "\" & strList & "_Sh.xml"
            Dim myXMLDt As String = myFolder & "\" & strList & "_Dt.xml"
            With Tbl
                .ReadXmlSchema(myXMLSh)
                .ReadXml(myXMLDt)
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return Tbl
    End Function
    Public Sub WriteLog(ByVal strMessage As String)
        Dim file_path As String = My.Application.Info.DirectoryPath & "\" & Format(Now, "yyyyMM")
        If (Not System.IO.Directory.Exists(file_path)) Then
            System.IO.Directory.CreateDirectory(file_path)
        End If
        Dim strFile As String = String.Format(file_path & "\" & Format(Now, "yyyyMMdd") & ".log")
        File.AppendAllText(strFile, Now.ToString & ": " & strMessage)
    End Sub

    Public Sub CekDCAktif()

        Dim connString As String = GetConnString("DC")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("select idDC from MstDC where statusData=1", con)

                Try
                    con.Open()
                    dr = cmd.ExecuteReader
                    dr.Read()
                    idDC = dr.Item("idDC")
                    dr.Close()
                    con.Close()

                Catch ex As SqlException

                End Try

            End Using
        End Using

    End Sub

    Public Sub GetJamSvr()
        Dim connString As String = GetConnString("ODOO")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("select GETDATE() as Jam", con)
                Try
                    con.Open()
                    dr = cmd.ExecuteReader
                    dr.Read()

                    jamSvr = dr.Item("Jam")
                    dr.Close()
                    con.Close()
                Catch ex As SqlException
                    MsgBox("Tidak dapat Sync Jam Server")
                End Try

            End Using
        End Using

    End Sub

    Public Sub CekStatusTransfer()
        Call CekDCAktif()
        stsTrf = ""
        Dim connString As String = GetConnString("ODOO")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("exec spSMICheckerTransfer 'CekRunning'," & idDC & "")
                cmd.Connection = con
                cmd.CommandTimeout = 720000

                Try
                    con.Open()
                    dr = cmd.ExecuteReader()
                    dr.Read()

                    nmDc = dr.Item("namaDc")
                    stsTrf = dr.Item("StatusTrf")
                    dr.Close()
                    con.Close()

                Catch ex As SqlException
                    FrmTransfer.LblStatus.Text = "Query Check Error"
                End Try
            End Using
        End Using

    End Sub
    Public Sub CBLIST()
        FrmTransfer.ComboBox1.Items.Clear()
        Dim connString As String = GetConnString("ODOO")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("select namaJenis from mstJenisTransfer where statusData=1")
                cmd.Connection = con
                cmd.CommandTimeout = 720000

                Try
                    con.Open()
                    dr = cmd.ExecuteReader()

                    While dr.Read()
                        FrmTransfer.ComboBox1.Items.Add(dr(0))
                    End While
                    dr.Close()
                    con.Close()

                Catch ex As SqlException
                    FrmTransfer.LblStatus.Text = "Jenis Transfer Tidak ditemukan"
                End Try
            End Using
        End Using

    End Sub
    Public Sub AmbilidJenis()
        If namaJnenis = "" Then
            namaJnenis = "Semua Transaksi"
        End If
        Dim connString As String = GetConnString("ODOO")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("select idJenis from mstJenisTransfer where namaJenis='" & namaJnenis & "'")
                cmd.Connection = con
                cmd.CommandTimeout = 720000

                Try
                    con.Open()
                    dr = cmd.ExecuteReader()
                    dr.Read()

                    idJenis = dr.Item(0)
                    dr.Close()
                    con.Close()

                Catch ex As SqlException
                    FrmTransfer.LblStatus.Text = "Tidak Ditemukan Jenis Transfer"
                End Try
            End Using
        End Using

    End Sub
    Public Sub UpdateStatusNormal()
        Call CekDCAktif()
        'If idDC = 1 Then
        '    strDownload = "update scedule_transfer set statusTrf=1,chekcIn=getdate() where idDc=1"
        'ElseIf idDC = 2 Then
        '    strDownload = "update scedule_transfer set statusTrf=1,chekcIn=getdate() where idDc=2"
        'ElseIf idDC = 3 Then
        '    strDownload = "update scedule_transfer set statusTrf=1,chekcIn=getdate() where idDc=3"
        'ElseIf idDC = 4 Then
        '    strDownload = "update scedule_transfer set statusTrf=1,chekcIn=getdate() where idDc=4"
        'ElseIf idDC = 5 Then
        '    strDownload = 

        'End If
        Dim connString As String = GetConnString("ODOO")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("exec spSMICheckerTransfer 'DoneUpdate'," & idDC & "")
                cmd.Connection = con
                'cmd.CommandTimeout = 720000
                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()

                Catch ex As SqlException
                    FrmTransfer.LblStatus.Text = "Data Gagal Update"
                End Try
            End Using
        End Using

    End Sub
    Public Sub CekTransferAktif()

        Dim connString As String = GetConnString("ODOO")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("select top 1 namaDc from scedule_transfer where statusTrf=1 order by noUrut asc")
                cmd.Connection = con
                cmd.CommandTimeout = 720000

                Try
                    con.Open()
                    dr = cmd.ExecuteReader()
                    dr.Read()

                    DCaktif = dr.Item(0)
                    dr.Close()
                    con.Close()

                Catch ex As SqlException
                    FrmTransfer.LblStatus.Text = "Tidak Ditemukan Untuk DC Aktif"
                End Try
            End Using
        End Using

    End Sub
    Public Sub CheckinKestaging()
        CekDCAktif()
        Dim connString As String = GetConnString("ODOO")
        Using con As New SqlConnection(connString)
            Using cmd As New SqlCommand("update scedule_transfer set chekcIn=getdate() where idDc=" & idDC & "")
                cmd.Connection = con
                'cmd.CommandTimeout = 720000
                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()

                Catch ex As SqlException

                End Try
            End Using
        End Using

    End Sub
End Module
