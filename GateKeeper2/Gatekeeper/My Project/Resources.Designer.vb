﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.269
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Gatekeeper.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        Friend ReadOnly Property amberball1() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("amberball1", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property banner() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("banner", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to DEVICETYPE^.
        '''</summary>
        Friend ReadOnly Property DeviceType() As String
            Get
                Return ResourceManager.GetString("DeviceType", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to RA|HKLM#Software\microsoft\windows nt\currentversion\winlogon#Shell|c:\program files\ashby school\gatekeepersuite\gatekeeper.exe|REG_SZ^.
        '''</summary>
        Friend ReadOnly Property ExplorerCommand() As String
            Get
                Return ResourceManager.GetString("ExplorerCommand", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to RA|HKLM#Software\microsoft\windows nt\currentversion\winlogon#Shell|Explorer.exe|REG_SZ^.
        '''</summary>
        Friend ReadOnly Property ExplorerLocation() As String
            Get
                Return ResourceManager.GetString("ExplorerLocation", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property greenball1() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("greenball1", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to motd.rtf.
        '''</summary>
        Friend ReadOnly Property LocalMOTD() As String
            Get
                Return ResourceManager.GetString("LocalMOTD", resourceCulture)
            End Get
        End Property
        
        Friend ReadOnly Property redball1() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("redball1", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to \\svr-deploy1\deploy$\motd.rtf.
        '''</summary>
        Friend ReadOnly Property RemoteMOTD() As String
            Get
                Return ResourceManager.GetString("RemoteMOTD", resourceCulture)
            End Get
        End Property
    End Module
End Namespace