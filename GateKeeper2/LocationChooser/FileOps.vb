Imports System.IO

Public Enum fileOpsReturn
    OK = 1
    FileNotFound = 2
    Exception = 3
    FileExists = 4

End Enum

Public Class FileOps
    Private Shared _thisInstance As FileOps

    Protected Sub New()

    End Sub

    Public Shared Function getFileOps() As FileOps
        If _thisInstance Is Nothing Then
            _thisInstance = New FileOps
        End If
        Return _thisInstance
    End Function

    Public Shared Function delete(ByVal fn As String) As fileOpsReturn
        Try
            If File.Exists(fn) Then
                File.Delete(fn)
                Return fileOpsReturn.OK
            Else
                Return fileOpsReturn.FileNotFound
            End If
        Catch ex As Exception
            return fileOpsReturn.Exception
        End Try

    End Function

    Public Shared Function getFiles(path As String) As List(Of String)
        Dim fList = New List(Of String)
        Dim di As DirectoryInfo = New DirectoryInfo(path)
        Dim dia As FileInfo() = di.GetFiles()

        For Each f As FileInfo In dia
            fList.Add(f.Name)
        Next

        Return fList
    End Function

End Class

