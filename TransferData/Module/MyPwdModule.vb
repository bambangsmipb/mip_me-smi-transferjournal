Module MyPwdModule
    Public Function CryptOrg(ByVal Aksi As String, ByVal Src As String) As String

        On Error Resume Next
        'VERSI 2
        Dim Tujuan As String, i As Integer
        Dim Kunci(3) As String, L As String, K As String, M As String

        K = "SOLUSI"
        L = "SUKSES"
        M = "SEMPURNA"

        Kunci(0) = K
        For i = 1 To Len(Src) / Len(K) + 1
            Kunci(0) = Kunci(0) & K

        Next

        K = Kunci(0)
        Kunci(0) = L
        For i = 1 To Len(Src) / Len(L) + 1
            Kunci(0) = Kunci(0) & L

        Next

        L = Kunci(0)
        Kunci(0) = M
        For i = 1 To Len(Src) / Len(M) + 1
            Kunci(0) = Kunci(0) & M

        Next

        M = Kunci(0)
        If Aksi = "E" Then
            Tujuan = Chr((Rnd() * 10000 Mod 255))
            Kunci(0) = Tujuan
            Tujuan = ""

            Kunci(2) = Hex(Asc(Kunci(0)))
            If Len(Kunci(2)) = 1 Then Kunci(2) = "0" & Kunci(2)
            Tujuan = Kunci(2) : Kunci(2) = ""
            For i = 1 To Len(Src)
                Kunci(1) = Chr((((Asc(Mid(Src, i, 1)) Xor Asc(Mid(K, i, 1))) Xor Asc(Mid(L, i, 1))) Xor Asc(Mid(M, i, 1))) Xor Asc(Kunci(0)))
                Kunci(2) = "" : Kunci(2) = Hex(Asc(Kunci(1)))

                If Len(Kunci(2)) = 1 Then Kunci(2) = "0" & Kunci(2)
                Tujuan = Tujuan & Kunci(2)
                Kunci(0) = Kunci(1) : Kunci(1) = ""
            Next

            Kunci(0) = ""
            Kunci(1) = ""
            For i = 1 To Len(Tujuan)
                If i Mod 2 = 0 Then
                    Kunci(0) = Kunci(0) & Mid(Tujuan, i, 1)

                Else
                    Kunci(1) = Kunci(1) & Mid(Tujuan, i, 1)

                End If
            Next
            Tujuan = Kunci(0) & Kunci(1)

        Else

            Kunci(0) = Mid(Src, 1, Len(Src) / 2)
            Kunci(1) = Mid(Src, Len(Src) / 2 + 1)

            Dim src1 As String
            src1 = ""
            For i = 1 To Len(Kunci(0))
                src1 = src1 & Mid(Kunci(1), i, 1) & Mid(Kunci(0), i, 1)

            Next

            Tujuan = "" : Kunci(2) = ""
            For i = 1 To Len(src1) / 2
                Kunci(2) = Kunci(2) & Chr(Convert.ToInt32(Mid(src1, i * 2 - 1, 2), 16))

            Next

            For i = 1 To Len(Kunci(2)) - 1
                Kunci(1) = Chr(((Asc(Mid(Kunci(2), Len(Kunci(2)) - i + 1, 1)) Xor Asc(Mid(Kunci(2), Len(Kunci(2)) - i, 1))) Xor Asc(Mid(K, Len(Kunci(2)) - i, 1)) Xor Asc(Mid(L, Len(Kunci(2)) - i, 1)) Xor Asc(Mid(M, Len(Kunci(2)) - i, 1))))
                Tujuan = Tujuan & Kunci(1)

                Kunci(0) = Kunci(1)
                Kunci(1) = ""
            Next

            Kunci(0) = ""
            For i = 1 To Len(Tujuan)
                Kunci(0) = Kunci(0) & Mid(Tujuan, Len(Tujuan) - i + 1, 1)

            Next
            Tujuan = Kunci(0)

        End If
        CryptOrg = Tujuan

    End Function
End Module
