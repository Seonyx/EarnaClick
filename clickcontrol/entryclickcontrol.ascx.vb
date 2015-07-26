Imports System
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient

Imports mojoPortal.Business
Imports mojoPortal.Business.WebHelpers
Imports mojoPortal.Features.UI
Imports mojoPortal.Web
Imports mojoPortal.Web.Controls
Imports mojoPortal.Web.UI
Imports mojoPortal.Web.Editor
Imports mojoPortal.Net
Imports mojoPortal.Web.Framework


Public Class entryclickcontrol
    Inherits SiteModuleControl

    Public userid As Integer = -1
    Private PageId As Integer = -1
    Private moduleId As Integer = -1

    Public siteurl As String = ConfigurationManager.AppSettings("siteurl")
    Public Sitefolder As String = ConfigurationManager.AppSettings("sitefolder")
    Private currentUser As SiteUser = SiteUtils.GetCurrentSiteUser()
    Public modulepath As String = ConfigurationManager.AppSettings("modulepath")
    Public linkurl As String = ""
    Public senderURL As String = ""

    Private Sub _default_Init(sender As Object, e As EventArgs) Handles Me.Init
        REM check is this is a free user and how many clicks he had today

        If (HttpContext.Current.Request.IsAuthenticated) Then
            userid = currentUser.UserId
            LoadSettings()
        End If
        PopulateLabels()
    End Sub

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Sub PopulateLabels()
        Dim labeltext As String = ""
        ' Dim returnURL As String = Sitefolder & "/sender.aspx?pageid=" & PageId & "&mid=" & moduleId
        Dim returnURL As String = Sitefolder & "/Default.aspx"
        If Context.User.Identity.IsAuthenticated = True Then
            If Not (Session("pageid") Is Nothing) Then
                If Not (Session("mid") Is Nothing) Then
                    labeltext = "<h3>Let&rsquo;s Get Earning</h3>"
                    labeltext = labeltext & "<p>Before we start just a reminder that this is a tool for generating traffic for professional use."
                    labeltext = labeltext & "<p>It should NOT be used for making money through click fraud or any other nefarious purpose. Misusers will be banned.</p>"
                    Label1.Text = labeltext
                    'linktosender.PostBackUrl = siteurl & modulepath & "/sender.aspx?pageid=" & Session("pageid").ToString & "&mid=" & Session("mid").ToString
                    'linktosender.Visible = True
                    Dim UriIn As Uri = New Uri(siteurl)
                    Dim strPage As String = modulepath & "/sender.aspx?pageid=" & Session("pageid").ToString & "&mid=" & Session("mid").ToString
                    Dim UriOut As Uri = Nothing
                    If Uri.TryCreate(UriIn, strPage, UriOut) Then
                        senderURL = siteurl & modulepath & "/sender.aspx?pageid=" & Session("pageid").ToString & "&mid=" & Session("mid").ToString
                    Else
                        Response.Redirect(siteurl & modulepath & "/error.aspx?code=33&pageid=" & PageId & "&mid=" & moduleId)
                    End If
                    'senderURL = siteurl & modulepath & "/sender.aspx?pageid=" & Session("pageid").ToString & "&mid=" & Session("mid").ToString

                    senderdiv.Visible = True
                Else
                    Response.Redirect(siteurl & modulepath & "/error.aspx?code=99&pageid=" & PageId & "&mid=" & moduleId)
                End If
            Else
                Response.Redirect(siteurl & modulepath & "/error.aspx?code=99&pageid=" & PageId & "&mid=" & moduleId)

            End If
        Else
            labeltext = "<h3>Do you want clicks?</h3>"
            labeltext = labeltext & "<p>If the answer is yes then you've come to the right place. "
            'labeltext = labeltext & "Just<a href=" & Chr(34) & "/Secure/Login.aspx?returnurl=%2fdefault.aspx%3f" & Chr(34) & "> sign in</a>/<a href=" & Chr(34) & "/Secure/register.aspx?returnurl=%2fdefault.aspx%3f" & Chr(34) & SiteRoot & Sitefolder & ">register </a>to get started earning clicks from people just like you.</p>"
            labeltext = labeltext & "Just<a href=" & Chr(34) & SiteRoot & Sitefolder & "/Secure/Login.aspx?returnurl=" & Server.UrlEncode(returnURL) & Chr(34) & "> sign in</a>/<a href=" & Chr(34) & SiteRoot & Sitefolder & "/Secure/register.aspx?returnurl=" & Server.UrlEncode(returnURL) & Chr(34) & ">register </a>to get started earning clicks from people just like you.</p>"
            Label1.Text = labeltext
            'linktosender.Visible = False
            senderdiv.Visible = False
        End If


    End Sub

    Sub LoadSettings()
        PageId = WebUtils.ParseInt32FromQueryString("pageid", PageId)
        moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId)
        Session("pageid") = PageId
        Session("mid") = moduleId
    End Sub

    Public Shared Function ToAbsoluteUrl(relativeUrl As String) As String
        If String.IsNullOrEmpty(relativeUrl) Then
            Return relativeUrl
        End If

        If HttpContext.Current Is Nothing Then
            Return relativeUrl
        End If

        If relativeUrl.StartsWith("/") Then
            relativeUrl = relativeUrl.Insert(0, "~")
        End If
        If Not relativeUrl.StartsWith("~/") Then
            relativeUrl = relativeUrl.Insert(0, "~/")
        End If

        Dim url = HttpContext.Current.Request.Url
        Dim port = If(url.Port <> 80, (":" + url.Port), [String].Empty)

        Return [String].Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl))
    End Function

End Class