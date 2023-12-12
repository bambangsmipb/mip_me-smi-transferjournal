Imports Newtonsoft.Json
Module DownloadModule
    Public Sub DownloadMaster()
        Application.DoEvents()
        'cek list tabel yg ada di file xml
        Dim ListTbl As DataTable = GetListTable("DownloadMaster")
        If ListTbl.Rows.Count = 0 Then MsgBox("Tidak ada list table di file DownloadMaster") : Exit Sub
        For Each row As DataRow In ListTbl.Rows
            Dim srcTable As String = row("srcTable")
            WriteLog("Checking Odoo Table " & srcTable & Environment.NewLine)
            Dim tbl As DataTable = SelectOdoo(srcTable)
            If tbl.Rows.Count > 0 Then
                WriteLog("Download Odoo " & srcTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)
                Dim dstTable As String = row("dstTable")
                Dim i As Int64 = InsertHO(dstTable, tbl)
                Dim view As New DataView(tbl)
                Dim dt As DataTable = view.ToTable(True, "code")
                Dim strJson As String = JsonConvert.SerializeObject(dt)
                WriteLog(strJson & Environment.NewLine)

                If i > 0 Then
                    WriteLog("Success Sync HO " & dstTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)

                Else
                    WriteLog("Gagal Sync " & dstTable & " " & tbl.Rows.Count & " row(s)" & Environment.NewLine)

                End If
            Else

                WriteLog("--- " & srcTable & " have no row updated ---" & Environment.NewLine)

            End If
        Next


    End Sub

End Module
