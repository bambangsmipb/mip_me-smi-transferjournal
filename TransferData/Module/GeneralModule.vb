Imports System.Net.Mail
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System.Net

Module GeneralModule
    Function CertificateValidationCallBack(
        ByVal sender As Object,
        ByVal certificate As X509Certificate,
        ByVal chain As X509Chain,
        ByVal sslPolicyErrors As SslPolicyErrors
    ) As Boolean
        Return True
    End Function
    Dim reader As New System.Configuration.AppSettingsReader
    Dim mySMTP As String = reader.GetValue("SMTP", GetType(String))
    Dim myEmail As String = reader.GetValue("EMAIL", GetType(String))
    Dim myPassword As String = GetEmailPWD()
    Dim myPort As String = reader.GetValue("PORT", GetType(String))
    Dim toAddress As String = reader.GetValue("TO", GetType(String))

    Private Function GetEmailPWD() As String
        Dim emailPWD As String = reader.GetValue("PASSWORD", GetType(String))
        Try
            Dim i As Integer = emailPWD.IndexOf("[")
            Dim f As String = emailPWD.Substring(i + 1, emailPWD.IndexOf("]", i + 1) - i - 1)
            emailPWD = CryptOrg("D", f)

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        Return emailPWD
    End Function
    Private Function IsSendLog() As Boolean
        Dim kirim As Boolean = False

        Try
            Dim sendLog As String = reader.GetValue("SENDLOG", GetType(String))
            If sendLog = "1" Then kirim = True
        Catch ex As Exception

        End Try

        Return kirim
    End Function
    Public Sub EmailLog()
        Try
            If IsSendLog() = False Then Exit Sub

            ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf CertificateValidationCallBack)

            Dim Mail As New MailMessage
            Dim SMTP As New SmtpClient(mySMTP)

            Dim strSubject As String = "Log - Transfer Journal " & Format(Now, "yyyyMMdd")
            Mail.Subject = strSubject
            Mail.From = New MailAddress(myEmail)
            SMTP.Credentials = New System.Net.NetworkCredential(myEmail, myPassword)

            Dim addr As String() = toAddress.Split(";")
            If addr.Length > 0 Then
                For Each ad As String In addr
                    Mail.To.Add(ad)

                Next

            Else
                Mail.To.Add(toAddress)

            End If

            Dim file_path As String = My.Application.Info.DirectoryPath & "\" & Format(Now, "yyyyMM")
            Dim strFile As String = String.Format(file_path & "\" & Format(Now, "yyyyMMdd") & ".log")
            Mail.Body = IO.File.ReadAllText(strFile)

            SMTP.EnableSsl = True
            SMTP.Port = myPort
            SMTP.Send(Mail)
            WriteLog("Email: " & strSubject & Environment.NewLine)

        Catch ex As SmtpException
            WriteLog(ex.Message & Environment.NewLine)

        End Try


    End Sub
End Module
