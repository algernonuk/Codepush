Imports Microsoft.Win32

' Simple class to store a registry entry as hive, key, setting and value
Public Class regControl
    Private depth As Integer = 0
    Public Sub New()

    End Sub


    ''' <summary>
    ''' Reads the regkey specified by hive:path:key
    ''' </summary>
    ''' <param name="path">The path.</param><returns>the value of the reg key as a string</returns>
    Public Function readReg(ByVal path As String)
        Dim parray() As String
        Dim regkey As RegistryKey
        parray = path.Split("#")
        Try
            regkey = getRegKey(path, False)
            Return regkey.GetValue(parray(2)).ToString
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    ''' <summary>
    ''' Adds a value into the registry.
    ''' </summary>
    ''' <param name="path">The path of the desired key (Hive#Path#Key).</param>
    ''' <param name="value">The value.</param>
    ''' <param name="type">The type ("String", "DWord").</param>
    Public Sub addReg(ByVal path As String, ByVal value As String, ByVal type As String)
        Dim parray() As String
        Dim regkey As RegistryKey
        parray = path.Split("#")
        regkey = getRegKey(path, True)
        Select Case type.ToUpper
            Case "REG_SZ"
                regkey.SetValue(parray(2), value)
            Case "REG_DWORD"
                value = value.Substring(2)
                regkey.SetValue(parray(2), Integer.Parse(value, Globalization.NumberStyles.HexNumber))
        End Select
        regkey.Flush()


    End Sub

    ''' <summary>
    ''' Check if the valuename exists within the registry key
    ''' </summary>
    ''' <param name="key">The key.</param>
    ''' <param name="valueName">Name of the value.</param><returns>true if found</returns>
    Private Function valueExists(ByVal key As RegistryKey, ByVal valueName As String) As Boolean
        Dim vNames() As String = key.GetValueNames
        For Each vname In vNames
            If vname.Equals(valueName) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Creates a registry key in HKLM.
    ''' </summary>
    ''' <param name="path">The path.</param><returns></returns>
    Private Function createHKLM(ByVal path)
        Dim regKey As RegistryKey = Nothing
        Dim parray() As String
        Dim pcount As Integer
        Dim oldPath As String = ""
        Dim fullPath As String = ""

        parray = path.split("\")
        pcount = parray.GetUpperBound(0)

        For Each pelement As String In parray
            fullPath = fullPath & pelement
            regKey = Registry.LocalMachine.OpenSubKey(fullPath)
            If regKey Is Nothing Then
                regKey = Registry.LocalMachine.OpenSubKey(oldPath)
                regKey.CreateSubKey(pelement)
            End If
            oldPath = String.Copy(fullPath)
            fullPath = fullPath & "\"
        Next

        Return regKey
    End Function

    ''' <summary>
    ''' Creates a registry key in HKCU.
    ''' </summary>
    ''' <param name="path">The path.</param><returns></returns>
    Private Function createHKCU(ByVal path)
        Dim regKey As RegistryKey = Nothing
        Dim parray() As String
        Dim pcount As Integer
        Dim oldPath As String = ""
        Dim fullPath As String = ""

        parray = path.split("\")
        pcount = parray.GetUpperBound(0)

        For Each pelement As String In parray
            fullPath = fullPath & pelement
            regKey = Registry.Users.OpenSubKey(fullPath)
            If regKey Is Nothing Then
                regKey = Registry.Users.OpenSubKey(oldPath, True)
                regKey.CreateSubKey(pelement)
            End If
            oldPath = String.Copy(fullPath)
            fullPath = fullPath & "\"
        Next
        Return regKey
    End Function

    ''' <summary>
    ''' Delete the specified value from a registry path.
    ''' </summary>
    ''' <param name="path">The path.</param>
    Public Sub regDelVal(ByVal path As String)
        Dim parray() As String
        Dim regkey As RegistryKey

        parray = path.Split("#")
        Try
            regkey = getRegKey(path, False)
            regkey.DeleteValue(parray(2))
        Catch ex As Exception
            'nothing to delete
        End Try

    End Sub

    ''' <summary>
    ''' Gets the key referenced by path.
    ''' </summary>
    ''' <param name="path">The path.</param><returns>RegistryKey</returns>
    Private Function getRegKey(ByVal path As String, ByVal create As Boolean)
        Dim regkey As RegistryKey
        Dim parray() As String
        regkey = Nothing
        parray = path.Split("#")

        'recurse if I cant open the key initially, create it, and call again
        If depth < 2 Then
            Try
                Select Case parray(0)
                    Case "HKLM"
                        regkey = Registry.LocalMachine.OpenSubKey(parray(1), True)

                        If (regkey Is Nothing And create) Then
                            regkey = createHKLM(parray(1))
                            regkey.Flush()
                            depth = depth + 1
                            regkey = Registry.Users.OpenSubKey(parray(1), True)
                        End If
                    Case "HKCU"
                        regkey = Registry.Users.OpenSubKey(parray(1), True)
                        If regkey Is Nothing Then

                        End If
                        If (regkey Is Nothing And create) Then
                            regkey = createHKCU(parray(1))
                            regkey.Flush()
                            depth = depth + 1
                            regkey = Registry.Users.OpenSubKey(parray(1), True)
                        End If
                End Select

            Catch ex As Exception
                'Utils.WriteToEventLog("Error while opening " & parray(1) & " - " & ex.Message, EventLogEntryType.Error)
            End Try
        End If

        If regkey Is Nothing Then
            depth = 0
            Dim e As New Exception("Cannot open Key " & parray(1))
            Throw e
        End If
        depth = 0
        Return regkey
    End Function

    ''' <summary>
    ''' delete all values under key specified in path
    ''' </summary>
    ''' <param name="path">The path.</param>
    Public Sub delvals(ByVal path As String)
        Dim regkey As RegistryKey
        Try
            regkey = getRegKey(path, False)
            Dim valuenames() As String = regkey.GetValueNames
            For Each valuename In valuenames
                regkey.DeleteValue(valuename)
            Next
        Catch ex As Exception

        End Try

    End Sub

    Public Sub deletevalues(ByVal path As String)
        'delete ; delimited list of values under key
    End Sub

    Public Sub deletekeys(ByVal path As String)
        'delete  ; demlimited list of entire keys
    End Sub
End Class
