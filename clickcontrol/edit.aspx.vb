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

Public Class edit
    Inherits mojoBasePage

    Public userid As Integer = -1

    Private Sub edit_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim currentUser As SiteUser = SiteUtils.GetCurrentSiteUser()
        If (HttpContext.Current.Request.IsAuthenticated) Then
            userid = currentUser.UserId
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim myCommand = New SqlDataAdapter("select * from links where userid =  " & userid & "order by dateEntered", ConfigurationManager.AppSettings("MSSQLConnectionString"))
        Dim strSQL As String = "SELECT  links.id, links.userid, Links.link, links.dateEntered, links.usertype,"
        strSQL = strSQL & "COUNT(clickhistory.Id) Child_Count  FROM links left JOIN clickhistory "
        strSQL = strSQL & "ON links.Id = clickhistory.linkid "
        strSQL = strSQL & "where links.userid = " & userid
        strSQL = strSQL & "GROUP BY links.userid, links.id, Links.link, links.dateEntered, links.usertype"
        strSQL = strSQL & "ORDER BY  Child_Count ASC order by dateEntered"
        Dim myCommand = New SqlDataAdapter(strSQL, ConfigurationManager.AppSettings("MSSQLConnectionString"))
        Dim ds As Data.DataSet = New Data.DataSet
        myCommand.Fill(ds)
        GridView1.DataSource = ds
        GridView1.DataBind()
        myCommand.Dispose()
    End Sub

End Class