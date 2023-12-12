Imports Newtonsoft.Json
Module UploadModule
    Public Sub UploadMaster()
        Application.DoEvents()

        Dim ListTbl As DataTable = GetListTable("UploadMaster")
        If ListTbl.Rows.Count = 0 Then MsgBox("Tidak ada list table di file UploadMaster") : Exit Sub
        For Each row As DataRow In ListTbl.Rows
            Dim srcTable As String = row("srcTable")
            WriteLog("Checking DC Table " & srcTable & Environment.NewLine)
            Dim tbl As DataTable = SelectDC(srcTable)
            If tbl.Rows.Count > 0 Then
                WriteLog("Download DC " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)
                Dim dstTable As String = row("dstTable")
                Dim i As Int64 = InsertOdoo(dstTable, tbl)
                If i > 0 Then
                    Dim strJson As String = JsonConvert.SerializeObject(tbl)
                    WriteLog(strJson & Environment.NewLine)

                    WriteLog("Success Sync ODOO " & dstTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)
                    Dim view As New DataView(tbl)
                    Dim dt As DataTable = view.ToTable(True, "code")

                    If srcTable <> "MstPerusahaan" Then
                        Dim j As Int64 = FlagDC(srcTable, dt)
                        If j > 0 Then
                            WriteLog("Success flag DC TglSync " & srcTable & Environment.NewLine)
                        End If
                    End If

                Else
                    WriteLog("Gagal Sync " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)

                End If
            Else
                WriteLog("--- " & srcTable & " have no row updated ---" & Environment.NewLine)

            End If
        Next


    End Sub

    Public Sub UploadTrans()
        Application.DoEvents()

        Dim ListTbl As DataTable = GetListTable("UploadTrans")
        If ListTbl.Rows.Count = 0 Then MsgBox("Tidak ada list table di file UploadTrans") : Exit Sub
        For Each row As DataRow In ListTbl.Rows
            Dim srcTable As String = row("srcTable")
            WriteLog("Checking DC Table " & srcTable & Environment.NewLine)
            Dim tbl As DataTable = SelectDC(srcTable)
            If tbl.Rows.Count > 0 Then
                WriteLog("Download DC " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)

                Dim strJson As String = JsonConvert.SerializeObject(tbl)
                WriteLog(strJson & Environment.NewLine)

                Dim dstTable As String = row("dstTable")
                Dim tblJournal As DataTable = InsertJournal(dstTable, tbl)

                If tblJournal.Rows.Count > 0 Then
                    Try
                        WriteLog("Success Sync ODOO " & dstTable & " " & tblJournal.Rows.Count & " row(s)" & Environment.NewLine)
                        Dim view As New DataView(tblJournal)
                        Dim dt As DataTable = view.ToTable(True, "description")

                        strJson = JsonConvert.SerializeObject(dt)
                        WriteLog(strJson & Environment.NewLine)

                        Dim j As Int64 = FlagDC(srcTable, dt)
                        If j > 0 Then
                            WriteLog("Success flag DC TglSync " & srcTable & Environment.NewLine)
                        End If

                        Dim selisih As Int32 = tbl.Rows.Count - tblJournal.Rows.Count
                        If selisih > 0 Then
                            WriteLog("Warning sebanyak " & selisih & " gagal diupload " & Environment.NewLine)
                        End If
                    Catch ex As Exception
                        WriteLog(tblJournal.Rows(0)(5).ToString & Environment.NewLine)
                        WriteLog(ex.Message & Environment.NewLine)

                    End Try

                Else
                    WriteLog("Gagal Sync " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)

                End If
            Else
                WriteLog("--- " & srcTable & " have no row updated ---" & Environment.NewLine)

            End If
        Next


    End Sub

    Public Sub UploadSales()
        Application.DoEvents()

        Dim ListTbl As DataTable = GetListTable("UploadSales")
        If ListTbl.Rows.Count = 0 Then MsgBox("Tidak ada list table di file UploadSales") : Exit Sub
        For Each row As DataRow In ListTbl.Rows
            Dim srcTable As String = row("srcTable")
            WriteLog("Checking DC Table " & srcTable & Environment.NewLine)
            Dim tbl As DataTable = SelectDC(srcTable)
            If tbl.Rows.Count > 0 Then
                WriteLog("Download DC " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)
                Dim strJson As String = JsonConvert.SerializeObject(tbl)
                WriteLog(strJson & Environment.NewLine)

                Dim dstTable As String = row("dstTable")
                Dim tblJournal As DataTable = InsertJournal(dstTable, tbl)
                If tblJournal.Rows.Count > 0 Then
                    Try

                        WriteLog("Success Sync ODOO " & dstTable & " " & tblJournal.Rows.Count & " row(s)" & Environment.NewLine)
                        Dim view As New DataView(tblJournal)
                        Dim dt As DataTable = view.ToTable(True, {"journal_group", "location_code", "reference"})

                        strJson = JsonConvert.SerializeObject(dt)
                        WriteLog(strJson & Environment.NewLine)

                        Dim j As Int64 = FlagSales(srcTable, dt)
                        If j > 0 Then
                            WriteLog("Success flag DC TglSync " & srcTable & Environment.NewLine)
                        End If

                        Dim selisih As Int32 = tbl.Rows.Count - tblJournal.Rows.Count
                        If selisih > 0 Then
                            WriteLog("Warning sebanyak " & selisih & " gagal diupload " & Environment.NewLine)
                        End If
                    Catch ex As Exception
                        WriteLog(tblJournal.Rows(0)(5).ToString & Environment.NewLine)
                        WriteLog(ex.Message & Environment.NewLine)


                    End Try

                Else
                    WriteLog("Gagal Sync " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)

                End If
            Else
                WriteLog("--- " & srcTable & " have no row updated ---" & Environment.NewLine)

            End If
        Next


    End Sub


    Public Sub UploadTransFrc()
        Application.DoEvents()

        Dim ListTbl As DataTable = GetListTable("UploadTransFrc")
        If ListTbl.Rows.Count = 0 Then MsgBox("Tidak ada list table di file UploadTransFrc") : Exit Sub
        'source tabel FRC_JRN	destiniton tabel journal_entry
        For Each row As DataRow In ListTbl.Rows
            Dim srcTable As String = row("srcTable")
            WriteLog("Checking DC Table " & srcTable & Environment.NewLine)

            Dim tbl As DataTable = SelectDC(srcTable) 'panggil codingan berikutnya
            If tbl.Rows.Count > 0 Then
                WriteLog("Download DC " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)
                Dim strJson As String = JsonConvert.SerializeObject(tbl)
                WriteLog(strJson & Environment.NewLine)

                Dim dstTable As String = row("dstTable")
                Dim tblJournal As DataTable = InsertJournal(dstTable, tbl)
                If tblJournal.Rows.Count > 0 Then

                    Try

                        WriteLog("Success Sync ODOO " & dstTable & " " & tblJournal.Rows.Count & " row(s)" & Environment.NewLine)
                        Dim view As New DataView(tblJournal)
                        Dim dt As DataTable = view.ToTable(True, {"location_code", "description"})

                        strJson = JsonConvert.SerializeObject(dt)
                        WriteLog(strJson & Environment.NewLine)
                        'UpdateStatusNormal flag yang sdh di insert jurnal, panggil function flag guna update tgl sync
                        Dim j As Int64 = FlagDC(srcTable, dt)
                        If j > 0 Then
                            'tulis log
                            WriteLog("Success flag DC TglSync " & srcTable & Environment.NewLine)
                        End If

                        Dim selisih As Int32 = tbl.Rows.Count - tblJournal.Rows.Count
                        If selisih > 0 Then
                            WriteLog("Warning sebanyak " & selisih & " gagal diupload " & Environment.NewLine)
                        End If
                    Catch ex As Exception
                        WriteLog(tblJournal.Rows(0)(5).ToString & Environment.NewLine)
                        WriteLog(ex.Message & Environment.NewLine)


                    End Try



                Else
                    WriteLog("Gagal Sync " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)

                End If
            Else
                WriteLog("--- " & srcTable & " have no row updated ---" & Environment.NewLine)

            End If
        Next


    End Sub

    Public Sub UploadSODC()
        Application.DoEvents()

        Dim ListTbl As DataTable = GetListTable("UploadSODC") '---------
        If ListTbl.Rows.Count = 0 Then MsgBox("Tidak ada list table di file UploadSODC") : Exit Sub
        For Each row As DataRow In ListTbl.Rows
            Dim srcTable As String = row("srcTable")
            WriteLog("Checking DC Table " & srcTable & Environment.NewLine)
            Dim tbl As DataTable = SelectDC(srcTable) '---------
            If tbl.Rows.Count > 0 Then
                WriteLog("Download DC " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)
                Dim strJson As String = JsonConvert.SerializeObject(tbl)
                WriteLog(strJson & Environment.NewLine)

                Dim dstTable As String = row("dstTable")
                Dim tblJournal As DataTable = InsertJournal(dstTable, tbl)
                If tblJournal.Rows.Count > 0 Then
                    Try

                        WriteLog("Success Sync ODOO " & dstTable & " " & tblJournal.Rows.Count & " row(s)" & Environment.NewLine)
                        Dim view As New DataView(tblJournal)
                        Dim dt As DataTable = view.ToTable(True, "reference")

                        strJson = JsonConvert.SerializeObject(dt)
                        WriteLog(strJson & Environment.NewLine)

                        Dim j As Int64 = FlagDC(srcTable, dt)
                        If j > 0 Then
                            WriteLog("Success flag DC TglSync " & srcTable & Environment.NewLine)
                        End If

                        Dim selisih As Int32 = tbl.Rows.Count - tblJournal.Rows.Count
                        If selisih > 0 Then
                            WriteLog("Warning sebanyak " & selisih & " gagal diupload " & Environment.NewLine)
                        End If
                    Catch ex As Exception
                        WriteLog(tblJournal.Rows(0)(5).ToString & Environment.NewLine)
                        WriteLog(ex.Message & Environment.NewLine)


                    End Try

                Else
                    WriteLog("Gagal Sync " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)

                End If
            Else
                WriteLog("--- " & srcTable & " have no row updated ---" & Environment.NewLine)

            End If
        Next


    End Sub
    Public Sub UploadSOTOKO()
        Application.DoEvents()

        Dim ListTbl As DataTable = GetListTable("UploadSOTOKO")
        If ListTbl.Rows.Count = 0 Then MsgBox("Tidak ada list table di file UploadSOTOKO") : Exit Sub
        For Each row As DataRow In ListTbl.Rows
            Dim srcTable As String = row("srcTable")
            WriteLog("Checking DC Table " & srcTable & Environment.NewLine)
            Dim tbl As DataTable = SelectDC(srcTable)
            If tbl.Rows.Count > 0 Then
                WriteLog("Download DC " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)
                Dim strJson As String = JsonConvert.SerializeObject(tbl)
                WriteLog(strJson & Environment.NewLine)

                Dim dstTable As String = row("dstTable")
                Dim tblJournal As DataTable = InsertJournal(dstTable, tbl)
                If tblJournal.Rows.Count > 0 Then
                    Try
                        WriteLog("Success Sync ODOO " & dstTable & " " & tblJournal.Rows.Count & " row(s)" & Environment.NewLine)

                        Dim view As New DataView(tblJournal)
                        Dim dt As DataTable = view.ToTable(True, {"location_code", "reference"})
                        strJson = JsonConvert.SerializeObject(dt)
                        WriteLog(strJson & Environment.NewLine)

                        Dim j As Int64 = FlagDC(srcTable, dt)
                        If j > 0 Then
                            WriteLog("Success flag DC TglSync " & srcTable & Environment.NewLine)
                        End If

                        Dim selisih As Int32 = tbl.Rows.Count - tblJournal.Rows.Count
                        If selisih > 0 Then
                            WriteLog("Warning sebanyak " & selisih & " gagal diupload " & Environment.NewLine)
                        End If
                    Catch ex As Exception
                        WriteLog(tblJournal.Rows(0)(5).ToString & Environment.NewLine)
                        WriteLog(ex.Message & Environment.NewLine)


                    End Try

                Else
                    WriteLog("Gagal Sync " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)

                End If
            Else
                WriteLog("--- " & srcTable & " have no row updated ---" & Environment.NewLine)

            End If
        Next


    End Sub

    Public Sub UploadMatching()
        Application.DoEvents()
        'List tabel atau sp-- baca tabel xml
        Dim ListTbl As DataTable = GetListTable("UploadMatching")
        If ListTbl.Rows.Count = 0 Then MsgBox("Tidak ada list table di file UploadMatching") : Exit Sub
        For Each row As DataRow In ListTbl.Rows
            Dim srcTable As String = row("srcTable")
            WriteLog("Checking DC Table " & srcTable & Environment.NewLine)
            'List tabel atau sp
            Dim tbl As DataTable = SelectDC(srcTable)
            If tbl.Rows.Count > 0 Then
                WriteLog("Download DC " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)
                Dim strJson As String = JsonConvert.SerializeObject(tbl)
                WriteLog(strJson & Environment.NewLine)

                Dim dstTable As String = row("dstTable")
                'List tabel atau sp--insert ke jurnal
                Dim tblJournal As DataTable = InsertJournal(dstTable, tbl)
                If tblJournal.Rows.Count > 0 Then
                    Try

                        WriteLog("Success Sync ODOO " & dstTable & " " & tblJournal.Rows.Count & " row(s)" & Environment.NewLine)

                        Dim view As New DataView(tblJournal)
                        Dim dt As DataTable = view.ToTable(True, "reference")
                        strJson = JsonConvert.SerializeObject(dt)
                        WriteLog(strJson & Environment.NewLine)
                        'Update flag setelah di jurnal
                        Dim j As Int64 = FlagDC(srcTable, dt)
                        If j > 0 Then
                            WriteLog("Success flag DC TglSync " & srcTable & Environment.NewLine)
                        End If

                        Dim selisih As Int32 = tbl.Rows.Count - tblJournal.Rows.Count
                        If selisih > 0 Then
                            WriteLog("Warning sebanyak " & selisih & " gagal diupload " & Environment.NewLine)
                        End If
                    Catch ex As Exception
                        WriteLog(tblJournal.Rows(0)(5).ToString & Environment.NewLine)
                        WriteLog(ex.Message & Environment.NewLine)


                    End Try

                Else
                    WriteLog("Gagal Sync " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)

                End If
            Else
                WriteLog("--- " & srcTable & " have no row updated ---" & Environment.NewLine)

            End If
        Next


    End Sub

End Module
