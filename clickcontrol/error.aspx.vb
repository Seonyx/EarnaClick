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

Public Class _error
    Inherits mojoBasePage
    Private PageId As Integer = -1
    Private moduleId As Integer = -1
    Public siteurl As String = ConfigurationManager.AppSettings("siteurl")
    Public sitefolder As String = ConfigurationManager.AppSettings("sitefolder")
    Public modulepath As String = ConfigurationManager.AppSettings("modulepath")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim errorcode As Integer = 0
        errorcode = Convert.ToInt32(Request.QueryString("code"))
        LinkButton1.PostBackUrl = SiteRoot  & modulepath & "/sender.aspx?pageid=" & PageId & "&mid=" & moduleId
        Select Case errorcode
            Case 0
                errorlabel.Text = "There has been a general error."
            Case 100
                errorlabel.Text = "There has been a problem updating the database with your information. This is likely to be a timeout or connection problem. Perhaps the server is very busy. Please try again later."
            Case 1
                errorlabel.Text = "We send a code and check that it is valid when we receive your information on the following page. This code was missing. It could be a security issue or problem of access. Please try again and if it happens again, report the issue using our contact page."
            Case 2
                errorlabel.Text = "We send a code and check that it is valid when we receive your information on the following page. The code we received was different to the one sent. It could be a security issue or problem of access. Please try again and if it happens again, report the issue using our contact page."
            Case 3
                errorlabel.Text = "We send a code and check that it is valid when we receive your information on the following page. This code was the wrong length. It could be a security issue or problem of access. Please try again and if it happens again, report the issue using our contact page."
            Case 40
                errorlabel.Text = "There was an error with the web address you supplied. We did a check and the address either contained characters that should not be in a web address or the address was incorrectly formed. Perhaps it was missing the http://"
            Case 99
                errorlabel.Text = "There was a problem receiving values from your login provider. Please try again and if the problem persists, report it to us using our contact page."
            Case 33
                errorlabel.Text = "There was a problem resolving a website address. If you see this error something has gone very wrong indeed. Please let us know via our contact page."
        End Select
    End Sub

    Sub LoadSettings()
        PageId = WebUtils.ParseInt32FromQueryString("pageid", PageId)
        moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId)
    End Sub
End Class