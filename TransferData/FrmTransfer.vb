Imports System.Globalization
Imports System.Threading


Public Class FrmTransfer
    Private Const CP_NOCLOSE_BUTTON As Integer = &H200

    Dim MyMarquee As New Marquee
    Private MouseIsDown As Boolean = False
    Private MouseIsDownLoc As Point = Nothing
    Public fupload As Boolean
    Dim TargetDT As DateTime
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get
    End Property
    Private m_icoHappy As Icon
    Private m_icoSad As Icon

    Dim reader As New System.Configuration.AppSettingsReader
    Dim myInterval As String = reader.GetValue("INTERVAL", GetType(String))
    Dim myIntervalProses As String = reader.GetValue("INTERVALPROSES", GetType(String))
    Dim myIntervalAktif As String = reader.GetValue("INTERVALAKTIF", GetType(String))
    Dim myStart As String = reader.GetValue("START", GetType(String))
    Private Sub JedaTrf(ByVal n As Integer) '--milisecond
        Dim x As Long
        x = Environment.TickCount
        While (Math.Abs(Environment.TickCount - x) < n)
            Application.DoEvents()
        End While
    End Sub
    Private Sub FrmTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''Control.CheckForIllegalCrossThreadCalls = False
        fupload = True
        Call GetJamSvr()
        j = Format(jamSvr, "HH")
        m = Format(jamSvr, "mm")
        d = Format(Now, "ss")
        Timer3.Start()
        Call CBLIST()

        Call RunInterval()

        MarqueeTimer.Enabled = True

        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        Cursor = Cursors.AppStarting

        Try
            Dim file_path As String = My.Application.Info.DirectoryPath
            m_icoHappy = New Icon(file_path & "\Happy.ico")
            m_icoSad = New Icon(file_path & "\Sad.ico")

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


        'lbtime.Text = j & ":" & m & ":" & d
        ''   Call CheckInterval()

        Cursor = Cursors.Default
    End Sub
    Private Sub CheckInterval()
        Dim interval As Int64 = CLng(myInterval)
        If interval > 0 Then
            Timer1.Interval = interval
            Timer1.Enabled = True

        ElseIf myStart.Length > 0 Then
            Timer2.Enabled = True

        End If
    End Sub
    Private Sub RadHappy_CheckedChangeds(sender As Object, e As EventArgs) Handles RadHappy.CheckedChanged
        NotifyIcon1.Icon = m_icoHappy
        Icon = m_icoHappy

        NotifyIcon1.BalloonTipTitle = "Tigas"
        If RadHappy.Checked = True Then
            Call RunInterval()
        Else
            NotifyIcon1.BalloonTipText = "Transfer Data was stopped"
        End If

    End Sub

    Private Sub RadSad_CheckedChanged(sender As Object, e As EventArgs) Handles RadSad.CheckedChanged
        If RadSad.Checked = True Then
            Timer1.Enabled = False
            Timer2.Enabled = False
            MsgBox("Transfer Data was stopped")
        End If
        Me.NotifyIcon1.Icon = m_icoSad
        Me.Icon = m_icoSad
    End Sub
    Private Sub ProsesTransfer()
        fupload = False
        NotifyIcon1.BalloonTipText = "Proses Transfer"

        WriteLog("Start Download Data" & Environment.NewLine)
        Call DownloadData()
        WriteLog("Finish Download Data" & Environment.NewLine & Environment.NewLine)

        WriteLog("Start Upload Data" & Environment.NewLine)
        Call UploadData()
        WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

    End Sub

    Private Sub BtnFlush_Click(sender As Object, e As EventArgs) Handles BtnFlush.Click
        Call AmbilidJenis()
        LblStatus.ForeColor = Color.White
        BtnFlush.Enabled = False
        Application.DoEvents()
        If idJenis = 0 Then
            Call CekStatusTransfer()
            If stsTrf = "Running" Then
                GroupBox2.Enabled = False
                GroupBox4.Enabled = False
                Call ProsesTransfer()
            Else
                Call CekTransferAktif()
                LblStatus.Text = "Saat Ini DC " & DCaktif & " Sedang Proses Sync"
                LblStatus.ForeColor = Color.Yellow
                BtnFlush.Enabled = True
                Call RunInterval()
                Exit Sub
            End If

            Call AmbilidJenis()
            ComboBox1.Enabled = False
        ElseIf idJenis = 1 Then
            NotifyIcon1.BalloonTipText = "Proses Transfer"
            LblStatus.Text = "Start Download Data" & ComboBox1.Text
            WriteLog("Start Download Data" & Environment.NewLine)
            Call DownloadData()
            WriteLog("Finish Download Data" & Environment.NewLine & Environment.NewLine)
            LblStatus.Text = "Finish Download Data" & ComboBox1.Text
            BarDetail.Value = 0
            BtnFlush.Enabled = True
        ElseIf idJenis = 2 Then
            LblStatus.Text = "Start Upload Data" & ComboBox1.Text
            WriteLog("Start Upload Data" & Environment.NewLine)
            BtnFlush.Enabled = False
            Cursor = Cursors.AppStarting
            'Data Master
            Call UploadMaster()
            Cursor = Cursors.Default
            WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

            LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
            BarDetail.Value = 0
            BtnFlush.Enabled = True

        ElseIf idJenis = 3 Then
            LblStatus.Text = "Start Upload Data" & ComboBox1.Text
            WriteLog("Start Upload Data" & Environment.NewLine)
            BtnFlush.Enabled = False
            Cursor = Cursors.AppStarting
            'TO dan TI
            Call UploadTrans()
            Cursor = Cursors.Default
            WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

            LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
            BarDetail.Value = 0
            BtnFlush.Enabled = True
        ElseIf idJenis = 4 Then
            LblStatus.Text = "Start Upload Data" & ComboBox1.Text
            WriteLog("Start Upload Data" & Environment.NewLine)
            BtnFlush.Enabled = False
            Cursor = Cursors.AppStarting
            'Sales
            Call UploadSales()
            Cursor = Cursors.Default
            WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

            LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
            BarDetail.Value = 0
            BtnFlush.Enabled = True
        ElseIf idJenis = 5 Then
            LblStatus.Text = "Start Upload Data" & ComboBox1.Text
            WriteLog("Start Upload Data" & Environment.NewLine)
            BtnFlush.Enabled = False
            Cursor = Cursors.AppStarting
            'TO & TI FRC
            Call UploadTransFrc()
            Cursor = Cursors.Default
            WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

            LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
            BarDetail.Value = 0
            BtnFlush.Enabled = True

        ElseIf idJenis = 6 Then
            LblStatus.Text = "Start Upload Data" & ComboBox1.Text
            WriteLog("Start Upload Data" & Environment.NewLine)
            BtnFlush.Enabled = False
            Cursor = Cursors.AppStarting
            'Stock Opname DC
            Call UploadSODC()
            Cursor = Cursors.Default
            WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

            LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
            BarDetail.Value = 0
            BtnFlush.Enabled = True

        ElseIf idJenis = 7 Then
            LblStatus.Text = "Start Upload Data" & ComboBox1.Text
            WriteLog("Start Upload Data" & Environment.NewLine)
            BtnFlush.Enabled = False

            Cursor = Cursors.AppStarting
            'Stock Opname TOKO
            Call UploadSOTOKO()
            Cursor = Cursors.Default
            WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

            LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
            BarDetail.Value = 0
            BtnFlush.Enabled = True
        ElseIf idJenis = 8 Then
            LblStatus.Text = "Start Upload Data" & ComboBox1.Text
            WriteLog("Start Upload Data" & Environment.NewLine)
            BtnFlush.Enabled = False
            Cursor = Cursors.AppStarting
            'Invoice Matching DC
            Call UploadMatching()
            Cursor = Cursors.Default
            WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

            LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
            BarDetail.Value = 0
            BtnFlush.Enabled = True

        End If

        LblStatus.Text = "Sending log to email..."
        Call EmailLog()
        LblStatus.Text = "Finish..."
        LblStatus.ForeColor = Color.MediumAquamarine
        BtnFlush.Enabled = True
        ComboBox1.Enabled = True
        'Call CBLIST()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If RadHappy.Checked = True Then

            Call CekStatusTransfer()

            If stsTrf = "Running" Then
                Call ProsesTransfer()
            Else
                Call CekTransferAktif()
                LblStatus.Text = "Saat Ini DC " & DCaktif & " Sedang Proses Sync"

            End If
        End If

    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        ShowInTaskbar = True
        NotifyIcon1.Visible = False
        WindowState = FormWindowState.Normal
    End Sub


    Private Sub FrmTransfer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If WindowState = FormWindowState.Minimized Then
            ShowInTaskbar = False
            NotifyIcon1.Visible = True

        End If
    End Sub

    Private Sub MnuHappy_Click(sender As Object, e As EventArgs) Handles MnuHappy.Click
        NotifyIcon1.Icon = m_icoHappy
        Icon = m_icoHappy
    End Sub

    Private Sub MnuSad_Click(sender As Object, e As EventArgs) Handles MnuSad.Click
        NotifyIcon1.Icon = m_icoSad
        Icon = m_icoSad
    End Sub
    Private Sub UploadData()

        ''  Call JedaTrf(myIntervalProses)
        BtnFlush.Enabled = False
        Cursor = Cursors.AppStarting
        CheckinKestaging()
        'Data Master
        Call UploadMaster()
        Cursor = Cursors.Default

        '' Call JedaTrf(myIntervalProses)
        Cursor = Cursors.AppStarting
        CheckinKestaging()
        'TO dan TI
        Call UploadTrans()
        Cursor = Cursors.Default

        ''  Call JedaTrf(myIntervalProses)
        Cursor = Cursors.AppStarting
        CheckinKestaging()
        'Sales
        Call UploadSales()
        Cursor = Cursors.Default

        '' Call JedaTrf(myIntervalProses)
        Cursor = Cursors.AppStarting
        CheckinKestaging()
        'TO & TI FRC
        Call UploadTransFrc()
        Cursor = Cursors.Default

        ''  Call JedaTrf(myIntervalProses)
        Cursor = Cursors.AppStarting
        CheckinKestaging()
        'Stock Opname DC
        Call UploadSODC()
        Cursor = Cursors.Default

        Cursor = Cursors.AppStarting
        CheckinKestaging()
        'Stock Opname TOKO
        Call UploadSOTOKO()
        Cursor = Cursors.Default

        Cursor = Cursors.AppStarting
        CheckinKestaging()
        'Invoice Matching DC
        Call UploadMatching()
        Cursor = Cursors.Default
        CheckinKestaging()
        Call UpdateStatusNormal()
        Call RunInterval()
        LblStatus.Text = "Finish..."
        fupload = True
        BarDetail.Value = 0
        BtnFlush.Enabled = True
        GroupBox2.Enabled = True
        GroupBox4.Enabled = True
    End Sub

    Private Sub DownloadData()

        BtnFlush.Enabled = False
        Cursor = Cursors.AppStarting
        Call DownloadMaster()
        Cursor = Cursors.Default


        LblStatus.Text = "Finish..."
        BarDetail.Value = 0
        BtnFlush.Enabled = True
    End Sub



    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'If RadHappy.Checked = True Then
        'Dim timeStart As String = myStart
        'Dim dtTime As DateTime = Convert.ToDateTime(timeStart)

        Dim ts As TimeSpan = TargetDT.Subtract(DateTime.Now)
            If ts.TotalMilliseconds <= 0 Then
                Timer2.Stop()

                Call CekStatusTransfer()

            If stsTrf = "Running" Then
                If fupload = True Then
                    If j >= 3 AndAlso j <= 9 Then
                        LblStatus.Text = "ODOO Sedang Sync"
                        LblStatus.ForeColor = Color.Red
                        GroupBox2.Enabled = False
                        GroupBox4.Enabled = False
                        Call CheckinKestaging()
                        Call RunInterval()
                        GroupBox2.Enabled = False
                        GroupBox4.Enabled = False
                    ElseIf j >= 18 AndAlso j <= 22 Then
                        GroupBox2.Enabled = False
                        GroupBox4.Enabled = False
                        LblStatus.Text = "Toko Sedang Transfer Data"
                        LblStatus.ForeColor = Color.Red
                        Call CheckinKestaging()
                        Call RunInterval()

                    Else
                        GroupBox2.Enabled = False
                        GroupBox4.Enabled = False
                        Call ProsesTransfer()
                    End If
                End If
            Else
                If j >= 3 AndAlso j <= 8 Then
                    GroupBox2.Enabled = False
                    GroupBox4.Enabled = False
                    LblStatus.Text = "ODOO Sedang Sync"
                    LblStatus.ForeColor = Color.Red
                    Call CheckinKestaging()
                    Call RunInterval()

                ElseIf j >= 18 AndAlso j <= 22 Then
                    GroupBox2.Enabled = False
                    GroupBox4.Enabled = False
                    LblStatus.Text = "Toko Sedang Transfer Data"
                    LblStatus.ForeColor = Color.Red
                    Call CheckinKestaging()
                    Call RunInterval()
                Else
                    GroupBox2.Enabled = True
                    GroupBox4.Enabled = True
                    Call CekTransferAktif()
                    LblStatus.Text = "Saat Ini DC " & DCaktif & " Sedang Proses Sync"
                    LblStatus.ForeColor = Color.Yellow
                    Call CheckinKestaging()
                    Call RunInterval()
                End If
            End If
        End If

        'If Hour(Now) = Hour(dtTime) And Minute(Now) = Minute(dtTime) Then

        '    Thread.Sleep(60000)

        'End If


        'If Hour(Now) = 4 And Minute(Now) = 59 And Second(Now) > 57 Then
        '    Call EmailLog()
        'End If

        'End If
    End Sub

    Private Sub MarqueeTimer_Tick(sender As Object, e As EventArgs) Handles MarqueeTimer.Tick
        MyMarquee.Tick()
        LblTitle.Text = MyMarquee.MarqueeText
    End Sub

    Private Sub FrmTransfer_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        MyMarquee.Text = "Solusi Sukses Sempurna" & " ".PadRight(7, " ")
        MyMarquee.ScrollDirection = Marquee.Direction.Left

    End Sub

    Private Sub LblStatus_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles LblStatus.MouseDoubleClick
        Dim str As String = InputBox("Karakter!")
        If str.Length > 0 Then
            InputBox("Encrypt", CryptOrg("E", str), CryptOrg("E", str))

        End If
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick

        Timer3.Interval = 1000
        d = Format(Now, "ss")
        If d = 0 Then
            m = m + 1
        End If
        If m = 59 AndAlso d = 59 Then
            m = 0
            j = j + 1
            If j >= 23 AndAlso m = 0 AndAlso d = 59 Then
                j = 0
                m = 0
            End If
        End If


        lbtime.Text = j & ":" & m & ":" & d
    End Sub

    Private Sub FrmTransfer_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        MouseIsDown = False
    End Sub

    Private Sub LblTitle_MouseMove(sender As Object, e As MouseEventArgs) Handles LblTitle.MouseMove
        If e.Button = MouseButtons.Left Then
            If MouseIsDown = False Then
                MouseIsDown = True
                MouseIsDownLoc = New Point(e.X, e.Y)
            End If

            Me.Location = New Point(Me.Location.X + e.X - MouseIsDownLoc.X, Me.Location.Y + e.Y - MouseIsDownLoc.Y)
        End If
    End Sub

    Private Sub LblTitle_Click(sender As Object, e As EventArgs) Handles LblTitle.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        namaJnenis = ComboBox1.Text
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        End
    End Sub



    Private Sub BackgroundWorker1_DoWork_1(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'LblStatus.ForeColor = Color.White
        'BtnFlush.Enabled = False
        'Application.DoEvents()
        'If idJenis = 0 Then
        '    Call CekStatusTransfer()
        '    If stsTrf = "Run" Then
        '        Call ProsesTransfer()
        '    Else
        '        Call CekTransferAktif()
        '        LblStatus.Text = "Saat Ini DC " & DCaktif & " Sedang Proses Sync"
        '        LblStatus.ForeColor = Color.Yellow
        '        BtnFlush.Enabled = True

        '    End If

        '    Call AmbilidJenis()
        '    ComboBox1.Enabled = False
        'ElseIf idJenis = 1 Then
        '    NotifyIcon1.BalloonTipText = "Proses Transfer"
        '    LblStatus.Text = "Start Download Data" & ComboBox1.Text
        '    WriteLog("Start Download Data" & Environment.NewLine)
        '    Call DownloadData()
        '    WriteLog("Finish Download Data" & Environment.NewLine & Environment.NewLine)
        '    LblStatus.Text = "Finish Download Data" & ComboBox1.Text
        '    BarDetail.Value = 0
        '    BtnFlush.Enabled = True
        'ElseIf idJenis = 2 Then
        '    LblStatus.Text = "Start Upload Data" & ComboBox1.Text
        '    WriteLog("Start Upload Data" & Environment.NewLine)
        '    BtnFlush.Enabled = False
        '    Cursor = Cursors.AppStarting
        '    'Data Master
        '    Call UploadMaster()
        '    Cursor = Cursors.Default
        '    WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

        '    LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
        '    BarDetail.Value = 0
        '    BtnFlush.Enabled = True

        'ElseIf idJenis = 3 Then
        '    LblStatus.Text = "Start Upload Data" & ComboBox1.Text
        '    WriteLog("Start Upload Data" & Environment.NewLine)
        '    BtnFlush.Enabled = False
        '    Cursor = Cursors.AppStarting
        '    'TO dan TI
        '    Call UploadTrans()
        '    Cursor = Cursors.Default
        '    WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

        '    LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
        '    BarDetail.Value = 0
        '    BtnFlush.Enabled = True
        'ElseIf idJenis = 4 Then
        '    LblStatus.Text = "Start Upload Data" & ComboBox1.Text
        '    WriteLog("Start Upload Data" & Environment.NewLine)
        '    BtnFlush.Enabled = False
        '    Cursor = Cursors.AppStarting
        '    'Sales
        '    Call UploadSales()
        '    Cursor = Cursors.Default
        '    WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

        '    LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
        '    BarDetail.Value = 0
        '    BtnFlush.Enabled = True
        'ElseIf idJenis = 5 Then
        '    LblStatus.Text = "Start Upload Data" & ComboBox1.Text
        '    WriteLog("Start Upload Data" & Environment.NewLine)
        '    BtnFlush.Enabled = False
        '    Cursor = Cursors.AppStarting
        '    'TO & TI FRC
        '    Call UploadTransFrc()
        '    Cursor = Cursors.Default
        '    WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

        '    LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
        '    BarDetail.Value = 0
        '    BtnFlush.Enabled = True

        'ElseIf idJenis = 6 Then
        '    LblStatus.Text = "Start Upload Data" & ComboBox1.Text
        '    WriteLog("Start Upload Data" & Environment.NewLine)
        '    BtnFlush.Enabled = False
        '    Cursor = Cursors.AppStarting
        '    'Stock Opname DC
        '    Call UploadSODC()
        '    Cursor = Cursors.Default
        '    WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

        '    LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
        '    BarDetail.Value = 0
        '    BtnFlush.Enabled = True

        'ElseIf idJenis = 7 Then
        '    LblStatus.Text = "Start Upload Data" & ComboBox1.Text
        '    WriteLog("Start Upload Data" & Environment.NewLine)
        '    BtnFlush.Enabled = False

        '    Cursor = Cursors.AppStarting
        '    'Stock Opname TOKO
        '    Call UploadSOTOKO()
        '    Cursor = Cursors.Default
        '    WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

        '    LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
        '    BarDetail.Value = 0
        '    BtnFlush.Enabled = True
        'ElseIf idJenis = 8 Then
        '    LblStatus.Text = "Start Upload Data" & ComboBox1.Text
        '    WriteLog("Start Upload Data" & Environment.NewLine)
        '    BtnFlush.Enabled = False
        '    Cursor = Cursors.AppStarting
        '    'Invoice Matching DC
        '    Call UploadMatching()
        '    Cursor = Cursors.Default
        '    WriteLog("Finish Upload Data" & Environment.NewLine & Environment.NewLine)

        '    LblStatus.Text = "Finish Upload Data" & ComboBox1.Text
        '    BarDetail.Value = 0
        '    BtnFlush.Enabled = True

        'End If

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        'LblStatus.Text = "Sending log to email..."
        'Call EmailLog()
        'LblStatus.Text = "Finish..."
        'LblStatus.ForeColor = Color.MediumAquamarine
        'BtnFlush.Enabled = True
        'ComboBox1.Enabled = True
        'Call CBLIST()
    End Sub
    Public Sub RunInterval()
        Dim CountDownFrom As TimeSpan = TimeSpan.FromMinutes(myIntervalAktif)
        Timer2.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)
        Timer2.Start()
    End Sub

    Private Sub LblStatus_TextChanged(sender As Object, e As EventArgs) Handles LblStatus.TextChanged

    End Sub
End Class
