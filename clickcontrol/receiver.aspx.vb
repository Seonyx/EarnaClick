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

Public Class receiver
    Inherits mojoBasePage
    Public referrer
    Private PageId As Integer = -1
    Private moduleId As Integer = -1
    Public userid As Integer
    Public siteurl As String = ConfigurationManager.AppSettings("siteurl")
    Public sitefolder As String = ConfigurationManager.AppSettings("Sitefolder")
    Public modulepath As String = ConfigurationManager.AppSettings("modulepath")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim currentUser As SiteUser = SiteUtils.GetCurrentSiteUser()
        If (HttpContext.Current.Request.IsAuthenticated) Then
            userid = currentUser.UserId
        Else
            Response.Redirect("/Secure/Login.aspx")
        End If
        LoadSettings()

        referrer = Request.ServerVariables("HTTP_REFERER")

        validaterequest()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        REM We have our receiver value. Validate it as a genuine url then add to db else 
        REM How do I know the request was genuine. How about pushing out a random coded string in default
        REM checking for it's return as a querystring variable.
        REM By reaching this event we passed the validation in page_load
        REM we also have to update the user's daily maximum count
        REM That implies we need a user information table (that will have to link in with the Mojoportal userid later)
        REM Use the user validation table to check whether the daily hit count is reached for free users.
        If CheckInput(receiver.Text) = True Then
            REM add to the db, increment daily count by one then redirect user back to homepage
            Dim strSQL As String = ""
            Dim conn As New SqlClient.SqlConnection
            conn.ConnectionString = ConfigurationManager.AppSettings("MSSQLConnectionString")
            Dim cmd As New SqlCommand
            Dim testing As String = receiver.Text.ToString
            Try
                conn.Open()
                strSQL = "insert into links(userid,link,dateentered,usertype) values(@userid,@link,SYSDATETIME(),@usertype)"
                cmd.Parameters.AddWithValue("@userid", userid) 'rem fix later with Mojo userid 
                cmd.Parameters.AddWithValue("@link", receiver.Text.ToString)
                cmd.Parameters.AddWithValue("@dateentered", "SYSDATETIME()")
                cmd.Parameters.AddWithValue("@usertype", "free") REM fixup type when where back in Mojo
                cmd.Connection = conn
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()
                'Close database
                conn.Close()
            Catch ex As Exception
                HttpContext.Current.Response.Write("<strong>URL addition fail :</strong><br>" & _
                ex.StackTrace & "<br><br>" & ex.Message & "<br><br><p>Please <a href='mailto:mail@earnalink.com'>e-mail us</a> providing as much detail as possible including the error message, what page you were viewing and what you were trying to achieve.<p>")
                HttpContext.Current.Response.Flush()
                HttpContext.Current.Response.End()
            Finally
                conn.Close()
            End Try
        Else
            REM The web address failed validation send back with message to try again. 
            Response.Redirect(siteurl & modulepath & "/error.aspx?code=40&pageid=" & PageId & "&mid=" & moduleId)
        End If
        Response.Redirect(SiteRoot & modulepath & "/sender.aspx?pageid=" & PageId & "&mid=" & moduleId)
    End Sub

    Protected Sub validaterequest()
        REM I use the userid value 1 here outside mojo - change to the mojo userid later
        Dim incomingcode As String = Request.QueryString("code")
        Dim storedcode As String = ""
        Dim strSQL As String = ""
        If incomingcode Is Nothing Then Response.Redirect("error.aspx?code=1&pageid=" & PageId & "&mid=" & moduleId)
        If Len(incomingcode) > 0 AndAlso Len(incomingcode) < 10 Then
            REM we're OK to check with DB
            Dim conn As New SqlClient.SqlConnection
            conn.ConnectionString = ConfigurationManager.AppSettings("MSSQLConnectionString")
            Dim cmd As New SqlCommand
            Try
                conn.Open()
                strSQL = "SELECT top 1 code FROM receivervalidation where userid = " & userid & " order by clickdatetime desc"

                cmd.Connection = conn
                cmd.CommandText = strSQL
                Dim objDataReader As SqlDataReader
                objDataReader = cmd.ExecuteReader()

                If objDataReader.HasRows = True Then
                    While objDataReader.Read
                        storedcode = objDataReader("code")
                    End While
                End If
                'Close database connection 
                objDataReader.Close()
                'Close database
                conn.Close()
            Catch ex As Exception
                HttpContext.Current.Response.Write("<strong>AN ERROR OCCURRED:</strong><br>" & _
              ex.StackTrace & "<br><br>" & ex.Message & "<br><br><p>Please <a href='mailto:mail@earnaclick.com'>e-mail us</a> providing as much detail as possible including the error message, what page you were viewing and what you were trying to achieve.<p>")
                HttpContext.Current.Response.Flush()
                HttpContext.Current.Response.End()
            Finally
                conn.Close()
            End Try
            If storedcode <> incomingcode Then Response.Redirect("error.aspx?code=2&pageid=" & PageId & "&mid=" & moduleId)
        Else
            Response.Redirect("error.aspx?code=3&pageid=" & PageId & "&mid=" & moduleId)
        End If
        REM If we have not redirected at this point then the code stored in the page was correct and we're valid.
    End Sub

    Function CheckInput(strIn As String) As Boolean
        Dim pattern As String = ""
        Dim message As Boolean = False
        Dim hasprotocol As Boolean = False
        If Left(LCase(strIn), 7) = "http://" Then
            hasprotocol = True
        End If
        If Left(LCase(strIn), 7) = "https://" Then
            hasprotocol = True
        End If
        If hasprotocol = False Then
            strIn = "http://" & strIn
        End If
        pattern = "http(s)?://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?"
        If Regex.IsMatch(strIn, pattern) Then
            message = True
        Else
            message = False
        End If
        Return message
    End Function

    Sub LoadSettings()
        PageId = WebUtils.ParseInt32FromQueryString("pageid", PageId)
        moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId)
        heading.Text = "Send Us Your Link"
        'If Not ModuleConfiguration Is Nothing Then

        'Title = ModuleConfiguration.ModuleTitle
        'Description = ModuleConfiguration.FeatureName

        'End If

    End Sub
End Class