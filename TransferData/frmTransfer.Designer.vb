<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmTransfer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTransfer))
        Me.BarDetail = New System.Windows.Forms.ProgressBar()
        Me.RadSad = New System.Windows.Forms.RadioButton()
        Me.RadHappy = New System.Windows.Forms.RadioButton()
        Me.BtnFlush = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuHappy = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSad = New System.Windows.Forms.ToolStripMenuItem()
        Me.LblStatus = New System.Windows.Forms.TextBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.MarqueeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.btnminimize = New System.Windows.Forms.Button()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lbtime = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'BarDetail
        '
        Me.BarDetail.Location = New System.Drawing.Point(8, 229)
        Me.BarDetail.Name = "BarDetail"
        Me.BarDetail.Size = New System.Drawing.Size(600, 10)
        Me.BarDetail.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.BarDetail.TabIndex = 1
        '
        'RadSad
        '
        Me.RadSad.AutoSize = True
        Me.RadSad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadSad.Location = New System.Drawing.Point(397, 133)
        Me.RadSad.Name = "RadSad"
        Me.RadSad.Size = New System.Drawing.Size(51, 17)
        Me.RadSad.TabIndex = 3
        Me.RadSad.Text = "Stop"
        Me.RadSad.UseVisualStyleBackColor = True
        '
        'RadHappy
        '
        Me.RadHappy.AutoSize = True
        Me.RadHappy.Checked = True
        Me.RadHappy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadHappy.Location = New System.Drawing.Point(319, 133)
        Me.RadHappy.Name = "RadHappy"
        Me.RadHappy.Size = New System.Drawing.Size(72, 17)
        Me.RadHappy.TabIndex = 4
        Me.RadHappy.TabStop = True
        Me.RadHappy.Text = "Transfer"
        Me.RadHappy.UseVisualStyleBackColor = True
        '
        'BtnFlush
        '
        Me.BtnFlush.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFlush.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BtnFlush.ForeColor = System.Drawing.Color.White
        Me.BtnFlush.Location = New System.Drawing.Point(5, 115)
        Me.BtnFlush.Name = "BtnFlush"
        Me.BtnFlush.Size = New System.Drawing.Size(112, 35)
        Me.BtnFlush.TabIndex = 5
        Me.BtnFlush.Text = "FLUSH"
        Me.BtnFlush.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 10000
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Tigas Transfer Data"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuHappy, Me.MnuSad})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(116, 48)
        '
        'MnuHappy
        '
        Me.MnuHappy.Name = "MnuHappy"
        Me.MnuHappy.Size = New System.Drawing.Size(115, 22)
        Me.MnuHappy.Text = "Transfer"
        '
        'MnuSad
        '
        Me.MnuSad.Name = "MnuSad"
        Me.MnuSad.Size = New System.Drawing.Size(115, 22)
        Me.MnuSad.Text = "Stop"
        '
        'LblStatus
        '
        Me.LblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LblStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStatus.ForeColor = System.Drawing.Color.White
        Me.LblStatus.Location = New System.Drawing.Point(312, 247)
        Me.LblStatus.Multiline = True
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.ReadOnly = True
        Me.LblStatus.Size = New System.Drawing.Size(299, 20)
        Me.LblStatus.TabIndex = 6
        Me.LblStatus.Text = "Status"
        Me.LblStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Timer2
        '
        Me.Timer2.Interval = 20000
        '
        'LblTitle
        '
        Me.LblTitle.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.LblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.LblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTitle.ForeColor = System.Drawing.Color.White
        Me.LblTitle.Location = New System.Drawing.Point(0, 0)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Size = New System.Drawing.Size(636, 62)
        Me.LblTitle.TabIndex = 7
        Me.LblTitle.Text = "Solusi Sukses Sempurna"
        Me.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MarqueeTimer
        '
        '
        'btnminimize
        '
        Me.btnminimize.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnminimize.FlatAppearance.BorderSize = 0
        Me.btnminimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnminimize.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnminimize.ForeColor = System.Drawing.Color.White
        Me.btnminimize.Location = New System.Drawing.Point(565, 1)
        Me.btnminimize.Margin = New System.Windows.Forms.Padding(2)
        Me.btnminimize.Name = "btnminimize"
        Me.btnminimize.Size = New System.Drawing.Size(26, 28)
        Me.btnminimize.TabIndex = 17
        Me.btnminimize.Text = "-"
        Me.btnminimize.UseVisualStyleBackColor = False
        '
        'btnclose
        '
        Me.btnclose.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnclose.FlatAppearance.BorderSize = 0
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(592, 1)
        Me.btnclose.Margin = New System.Windows.Forms.Padding(2)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(26, 28)
        Me.btnclose.TabIndex = 16
        Me.btnclose.Text = "x"
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.RadHappy)
        Me.GroupBox2.Controls.Add(Me.RadSad)
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(9, 64)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(464, 163)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pemilihan Job"
        '
        'ComboBox1
        '
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(59, 24)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(221, 23)
        Me.ComboBox1.TabIndex = 1
        Me.ComboBox1.Text = "Semua Transaksi"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.Label2.Location = New System.Drawing.Point(16, 24)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 19)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Task :"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox4.Controls.Add(Me.lbtime)
        Me.GroupBox4.Controls.Add(Me.BtnFlush)
        Me.GroupBox4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GroupBox4.Location = New System.Drawing.Point(477, 64)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Size = New System.Drawing.Size(131, 163)
        Me.GroupBox4.TabIndex = 20
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Command"
        '
        'lbtime
        '
        Me.lbtime.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.lbtime.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbtime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lbtime.Location = New System.Drawing.Point(10, 28)
        Me.lbtime.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbtime.Name = "lbtime"
        Me.lbtime.Size = New System.Drawing.Size(112, 19)
        Me.lbtime.TabIndex = 10
        Me.lbtime.Text = "Jam"
        Me.lbtime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Panel1.Location = New System.Drawing.Point(-2, 270)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(622, 10)
        Me.Panel1.TabIndex = 21
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Panel2.Location = New System.Drawing.Point(618, 59)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(10, 213)
        Me.Panel2.TabIndex = 22
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Panel3.Location = New System.Drawing.Point(-9, 62)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(10, 213)
        Me.Panel3.TabIndex = 23
        '
        'Timer3
        '
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(6, 246)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(199, 20)
        Me.TextBox1.TabIndex = 24
        Me.TextBox1.TabStop = False
        Me.TextBox1.Text = "PT. Surganya Motor Indonesia"
        '
        'Panel4
        '
        Me.Panel4.Location = New System.Drawing.Point(1, 50)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(617, 10)
        Me.Panel4.TabIndex = 25
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Panel5.Location = New System.Drawing.Point(1, 48)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(617, 10)
        Me.Panel5.TabIndex = 26
        '
        'BackgroundWorker1
        '
        '
        'Panel6
        '
        Me.Panel6.Location = New System.Drawing.Point(2, 225)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(617, 10)
        Me.Panel6.TabIndex = 26
        '
        'FrmTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(636, 291)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.LblStatus)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnminimize)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.LblTitle)
        Me.Controls.Add(Me.BarDetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer Journal v1.0.2"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BarDetail As ProgressBar
    Friend WithEvents RadSad As RadioButton
    Friend WithEvents RadHappy As RadioButton
    Friend WithEvents BtnFlush As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents MnuHappy As ToolStripMenuItem
    Friend WithEvents MnuSad As ToolStripMenuItem
    Friend WithEvents LblStatus As TextBox
    Friend WithEvents Timer2 As Timer
    Friend WithEvents LblTitle As Label
    Friend WithEvents MarqueeTimer As Timer
    Friend WithEvents btnminimize As Button
    Friend WithEvents btnclose As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents lbtime As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Timer3 As Timer
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel6 As Panel
End Class
