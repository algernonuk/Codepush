Imports System.IO
Imports System.Xml
Imports System.Xml.XmlNode
Imports System.ComponentModel

public Class host
    Private _name As String
    Private _profile As New List(Of String)
    Private _privList As New List(Of String)
    Private _printerList As New List(Of String)

    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property PrinterList() As List(Of String)
        Get
            Return _printerList
        End Get
        Set(ByVal value As List(Of String))
            _printerList = value
        End Set
    End Property
    Public Property PrivList() As List(Of String)
        Get
            Return _privList
        End Get
        Set(ByVal value As List(Of String))
            _privList = value
        End Set
    End Property
    Public Property Profile() As List(Of String)
        Get
            Return _profile
        End Get
        Set(ByVal value As List(Of String))
            _profile = value
        End Set
    End Property

    Public Sub New(ByVal thisName As String)
        Me.Name = thisName
        loadHost()
    End Sub

    Public Sub addPrinter(ByVal pname As String)
        If pname.Contains(vbTab) then
            Dim splitPrinter() As String = pname.Split(vbTab)
            pname = splitPrinter(1)
        End If
        If Not Me.PrinterList.Contains(pname) then
            Me.PrinterList.Add(pname)
        End If
    End Sub

    Public Sub addPrivUser(ByVal user As String)
        If Not Me.PrivList.Contains(user) then
            Me.PrivList.Add(user)
        End If
    End Sub

    Public Sub addProfile(ByVal profileName As String)
        If Not Me.Profile.Contains(profileName) Then
            Me.Profile.Add(profileName)
        End If

    End Sub


    Public Sub removeProfile(ByVal profileName As String)
        Me.Profile.Remove(profileName)
    End Sub


    Public Sub loadHost()
        Me.Profile.Clear()
        Dim HN As String = My.Settings.DEPLOYSHARE & My.Settings.HOSTSLOCATION & Name

        If Not HN.ToLower.EndsWith(".xml") then
            HN = HN & ".xml"
        End If

        Dim hostFile As XmlDocument = New XmlDocument
        Try
            hostFile.Load(HN)
        Catch ex As FileNotFoundException
            createWPKGHost(Name)
            Return
        Catch ex As Exception

            Return
        End Try

        For Each Item As XmlNode In hostFile.SelectSingleNode("/wpkg/host")

            Dim NamedItem As XmlNode = Item.Attributes.GetNamedItem("id")
            If Not NamedItem is Nothing
                Dim userAttr As String = NamedItem.Value
                If Not userAttr Is Nothing Then
                    addProfile(userAttr)
                End If
            End If


            NamedItem = Item.Attributes.GetNamedItem("user")
            If Not NamedItem is Nothing
                Dim userAttr As String = NamedItem.Value
                If Not userAttr Is Nothing Then
                    addPrivUser(userAttr)
                End If
            End If


            NamedItem = Item.Attributes.GetNamedItem("printer")
            If Not NamedItem is Nothing
                Dim userAttr As String = NamedItem.Value
                If Not userAttr Is Nothing Then
                    addPrinter(userAttr)
                End If
            End If

        Next

    End Sub

    Private Sub saveWPKG()
        Dim Doc As New XmlDocument()
        Dim newAtt As XmlAttribute

        ' Use the XmlDeclaration class to place the
        ' <?xml version="1.0"?> declaration at the top of our XML file
        Dim dec As XmlDeclaration = Doc.CreateXmlDeclaration("1.0", Nothing, Nothing)
        Doc.AppendChild(dec)
        Dim DocRoot As XmlElement = Doc.CreateElement("wpkg")
        Doc.AppendChild(DocRoot)

        Dim HN As XmlNode = Doc.CreateElement("host")
        newAtt = Doc.CreateAttribute("name")
        newAtt.Value = Name
        HN.Attributes.Append(newAtt)
        newAtt = Doc.CreateAttribute("profile-id")
        newAtt.Value = "Dummy"
        HN.Attributes.Append(newAtt)

        saveWPKGExtracted(Doc, HN, "profile", "id", Profile)
        saveWPKGExtracted(Doc, HN, "privileged", "user", PrivList)
        saveWPKGExtracted(Doc, HN, "printers", "printer", PrinterList)

        DocRoot.AppendChild(HN)
        Doc.Save(My.Settings.DEPLOYSHARE & My.Settings.HOSTSLOCATION & Name & ".xml")
    End Sub

    Private Sub saveWPKGExtracted(ByVal Doc As XmlDocument, ByVal HN As XmlNode, ByVal Element As String, ByVal Attrib As String, ByVal theList As List(Of String))
        For Each Item As String In theList
            Dim newPriv As XmlNode = Doc.CreateElement(Element)
            Dim thisAtt As XmlAttribute = Doc.CreateAttribute(Attrib)
            thisAtt.Value = Item
            newPriv.Attributes.Append(thisAtt)
            HN.AppendChild(newPriv)
        Next
    End Sub

    Public Sub writeFile()
        saveWPKG()
    End Sub

    Private Sub createWPKGHost(Name As String)
        Dim Doc As New XmlDocument()
        Dim newAtt As XmlAttribute

        ' Use the XmlDeclaration class to place the
        ' <?xml version="1.0"?> declaration at the top of our XML file
        Dim dec As XmlDeclaration = Doc.CreateXmlDeclaration("1.0", Nothing, Nothing)
        Doc.AppendChild(dec)
        Dim DocRoot As XmlElement = Doc.CreateElement("wpkg")
        Doc.AppendChild(DocRoot)

        Dim HN As XmlNode = Doc.CreateElement("host")
        newAtt = Doc.CreateAttribute("name")
        newAtt.Value = Name
        HN.Attributes.Append(newAtt)

        newAtt = Doc.CreateAttribute("profile-id")
        newAtt.Value = "Dummy"
        HN.Attributes.Append(newAtt)
        'Me.PrinterList.Add("DUMMY")
        'Me.PrivList.Add("Domain Admins")
        Me.Profile.Add("Core")

        saveWPKGExtracted(Doc, HN, "profile", "id", Profile)
        saveWPKGExtracted(Doc, HN, "privileged", "user", PrivList)
        saveWPKGExtracted(Doc, HN, "printers", "printer", PrinterList)
        DocRoot.AppendChild(HN)

        Doc.Save(My.Settings.DEPLOYSHARE & My.Settings.HOSTSLOCATION & Name & ".xml")
    End Sub
End Class

