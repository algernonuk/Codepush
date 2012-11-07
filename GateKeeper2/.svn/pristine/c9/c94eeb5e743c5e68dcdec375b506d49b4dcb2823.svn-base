Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports Microsoft.Win32
Imports PetesControls
Imports System.Environment

Public Class PrivilegeFunctions

    'secret keys to decrypt privsettings.ini
    Private Const KEY As String = "ASHBY^&*"
    Private Const IV As String = "*&^YBHSA"
    Private pfile As String = getPrivFile

    Private function getPrivFile() As String
        ' work out if we need to process a priv file on this machine
        Dim thePath As String = System.Reflection.Assembly.GetExecutingAssembly.Location()
        Dim pos As Integer = thePath.LastIndexOf("\")
        thePath = thePath.Substring(0,pos)
        Return thePath & "\privfile.ini"
    End Function


    ''' <summary>
    ''' Processes the privFile, making registry changes based on commands in the file
    ''' the file consists of entries made from 4 lines, and is encrypted with DES encryption
    ''' KeyName: name of registry key 
    ''' ValueName: name of the registry setting  or **del. name of setting to delete 
    ''' ValueType: one of REG_SZ for String, DWORD for hex etc 
    ''' Value: value of setting, or blank in case of delete 
    ''' </summary>
    ''' <param name="sid">The sid of the user hive to modify.</param>
    Public Sub processPriv(ByVal sid As String)
        Dim inLine As String
        Dim keyName As String = ""
        Dim valueName As String = ""
        Dim valueType As String = ""
        Dim value As String = ""

        Dim commandArray() As String
        Try
            If (System.IO.File.Exists(pFile)) Then

                Dim myDESProvider As DESCryptoServiceProvider = New DESCryptoServiceProvider()

                myDESProvider.Key = ASCIIEncoding.ASCII.GetBytes(KEY)
                myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes(IV)

                Dim DecryptedFile As FileStream = New FileStream(pFile, FileMode.Open, FileAccess.Read)
                Dim myICryptoTransform As ICryptoTransform = myDESProvider.CreateDecryptor(myDESProvider.Key, myDESProvider.IV)
                Dim myCryptoStream As CryptoStream = New CryptoStream(DecryptedFile, myICryptoTransform, CryptoStreamMode.Read)

                Dim fileReader As New StreamReader(myCryptoStream)

                Do While fileReader.Peek() <> -1
                    inLine = fileReader.ReadLine()
                    commandArray = inLine.Split(":")
                    Select Case commandArray(0)
                        Case "KeyName"
                            keyName = rebuildString(commandArray)
                        Case "ValueName"
                            valueName = rebuildString(commandArray)
                        Case "ValueType"
                            valueType = commandArray(1)
                        Case "Value"
                            If commandArray.GetUpperBound(0) > 0 Then
                                value = commandArray(1)
                            Else
                                value = ""
                            End If
                            processReg(sid, keyName, valueName, valueType, value)
                    End Select
                Loop
                myCryptoStream.Close()
                fileReader.Close()
                fileReader = Nothing
            Else
                Utils.WriteToEventLog("Privilege file not found " & pFile, EventLogEntryType.Error)
            End If

        Catch ex As Exception
            Utils.WriteToEventLog(ex.Message & " - " & ex.StackTrace, EventLogEntryType.Error)

        End Try
    End Sub

    ''' <summary>
    ''' Processes the reg entries in the userhive.
    ''' </summary>
    ''' <param name="sid">The sid of the user to modify.</param>
    ''' <param name="keyName">Name of the key.</param>
    ''' <param name="valueName">Name of the value.</param>
    ''' <param name="valueType">Type of the value.</param>
    ''' <param name="value">The value.</param>
    Public Sub processReg(ByVal sid As String, ByVal keyName As String, ByVal valueName As String, ByVal valueType As String, ByVal value As String)
        Dim type As RegistryValueKind = RegistryValueKind.Unknown
        Dim regUtil As regControl = New regControl()

        If valueName.StartsWith("**del.") Then
            value = valueName.Substring(6)
            regUtil.regDelVal("HKCU#" & sid & "\" & keyName & "#" & value)
            Return
        ElseIf valueName.StartsWith("**delvals.") Then
            regUtil.delvals("HKCU#" & sid & "\" & keyName)
            Return
        End If

        regUtil.addReg("HKCU#" & sid & "\" & keyName & "#" & valueName, value, valueType)
    End Sub

    ''' <summary>
    ''' Rebuilds the string.
    ''' Takes an array that was split on : and puts elements from 1 to the end back into 1 string with :'s
    ''' </summary>
    ''' <param name="carray">The carray.</param><returns></returns>
    Private Function rebuildString(ByVal carray() As String) As String
        Dim ostr As String = ""

        If carray.GetUpperBound(0) = 1 Then
            Return carray(1)
        End If

        For x As Integer = 1 To carray.GetUpperBound(0) Step 1
            ostr = String.Concat(ostr, carray(x))
            If x < carray.GetUpperBound(0) Then
                ostr = String.Concat(ostr, ":")
            End If
        Next

        Return ostr
    End Function

End Class
