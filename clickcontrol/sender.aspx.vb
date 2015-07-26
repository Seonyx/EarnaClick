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

Public Class sender
    Inherits mojoBasePage
    Public browser As String = ""
    Public linkID As Integer = 0
    Public userid As Integer = -1
    Public PageId As Integer = -1
    Public moduleId As Integer = -1
    Public loginName As String = ""
    Public strrandomcode As String = ""
    Public siteurl As String = ConfigurationManager.AppSettings("siteurl")
    Public sitefolder As String = ConfigurationManager.AppSettings("sitefolder")
    Public modulepath As String = ConfigurationManager.AppSettings("modulepath")
    Private currentUser As SiteUser = SiteUtils.GetCurrentSiteUser()
    Public linkurl As String = ""

    Private Sub _default_Init(sender As Object, e As EventArgs) Handles Me.Init
        REM check is this is a free user and how many clicks he had today

        If (HttpContext.Current.Request.IsAuthenticated) Then
            userid = currentUser.UserId
            validateuser()
            LoadSettings()

            Dim agent As String = Request.ServerVariables("HTTP_USER_AGENT")
            Dim strposition As Integer = InStr(agent, "MSIE")
            If strposition > 0 Then
                browser = "lose_in_ie();"
            Else
                browser = "lose_in_ff();"
            End If

            linkurl = getlink()
            'Dim rndnumber = New Random
            Dim randomcode As String = GetRandomString(9)
            strrandomcode = randomcode
            REM save this so we can validate the ID of the user in the receiver page.
            REM note we don't have a user id without the Mojo framework so use 1 for now then add it later
            Dim con As New SqlConnection
            Dim cmd As New SqlCommand
            Dim strSQL As String = "INSERT INTO receivervalidation (clickdatetime,userid,code ) VALUES(SYSDATETIME(), " & userid & ",'" & randomcode & "')"
            Try
                con.ConnectionString = ConfigurationManager.AppSettings("MSSQLConnectionString")
                con.Open()
                cmd.Connection = con
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                HttpContext.Current.Response.Write(strSQL & "<strong>URL addition fail :</strong><br>" & _
ex.StackTrace & "<br><br>" & ex.Message & "<br><br><p>Please <a href='mailto:mail@earnalink.com'>e-mail us</a> providing as much detail as possible including the error message, what page you were viewing and what you were trying to achieve.<p>")
                HttpContext.Current.Response.Flush()
                HttpContext.Current.Response.End()
            Finally
                con.Close()
            End Try
        Else
            Response.Redirect("/Secure/Login.aspx")
        End If
        PopulateLabels()
    End Sub

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        
    End Sub

    Sub PopulateLabels()
        Dim labeltext As String = ""
        If Context.User.Identity.IsAuthenticated = True Then
            labeltext = "<h3>Let&rsquo;s Get Earning</h3>"
            labeltext = labeltext & "<p>It's very easy. Just <strong>click</strong> on the link you see below and visit the website. It belongs to another person like you who wants traffic.</p>"
            labeltext = labeltext & "<p>When the other pages loads, close it down and, you'll be able to enter your own web address for someone else to click. It's that simple!</p>"
            Label1.Text = labeltext
            linkpanel.Visible = True
        Else
            labeltext = "<h3>Do you want clicks?</h3>"
            labeltext = labeltext & "<p>If the answer is yes then you've come to the right place."
            labeltext = labeltext & "Just<a href=" & Chr(34) & siteurl & sitefolder & "/Secure/Login.aspx?returnurl=%2fdefault.aspx%3f" & Chr(34) & "> sign in</a>/<a href=" & Chr(34) & "/Secure/register.aspx?returnurl=%2fdefault.aspx%3f" & Chr(34) & SiteRoot & Sitefolder & ">register </a>to get started earning clicks from people just like you.</p>"
            Label1.Text = labeltext
            linkpanel.Visible = False
        End If
    End Sub

    Sub LoadSettings()
        PageId = WebUtils.ParseInt32FromQueryString("pageid", PageId)
        moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId)
        heading.Text = "Click to Make Someone Else Happy"
    End Sub

    Function getlink() As String
        Dim myreader As SqlDataReader
        Dim passw As String = ""
        Dim regmsg As String = ""
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim strsql As String = ""
        Dim successflag As Boolean = False
        Dim linkURI As String = "http://www.seonyx.com/"
        Try
            strsql = "SELECT top 1 links.Id, links.link, links.userid FROM links LEFT OUTER JOIN clickhistory ON links.Id=clickhistory.linkid WHERE clickhistory.Id IS NULL and links.userid <> " & userid
            cn.ConnectionString = ConfigurationManager.AppSettings("MSSQLConnectionString")
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = strsql
            myreader = cmd.ExecuteReader
            If myreader.HasRows = True Then
                While myreader.Read
                    linkURI = myreader("link")
                    linkID = myreader("Id")
                    successflag = True
                End While
            End If
            cmd.Parameters.Clear()
            myreader.Close()
            If successflag = False Then 'there were no childless links, so find the one with the least hits
                strsql = "SELECT top 1 links.id, Links.link, links.dateEntered, links.usertype,"
                strsql = strsql & "COUNT(clickhistory.Id) Child_Count  FROM links JOIN clickhistory"
                strsql = strsql & "	ON links.Id = clickhistory.linkid "
                strsql = strsql & "	where links.userid <> " & userid
                strsql = strsql & " GROUP BY links.id, Links.link, links.dateEntered, links.usertype "
                strsql = strsql & " ORDER BY  Child_Count ASC "
                cmd.CommandText = strsql
                myreader = cmd.ExecuteReader
                While myreader.Read
                    linkURI = myreader("link")
                    linkID = myreader("Id")
                    successflag = True
                End While
            End If
            cn.Close()
        Catch ex As Exception
            Label1.Text = ex.ToString
        End Try
        'Return linkURI
        linkURI = checkhasprotocol(linkURI)
        Dim result As Uri
        If Uri.TryCreate(linkURI, UriKind.RelativeOrAbsolute, result) Then
            If Not result.IsAbsoluteUri Then
                Dim siteuri2 As New Uri(ToAbsoluteUrl(linkURI))
                Return siteuri2.ToString
            Else
                Return result.ToString
            End If
        Else
            Return "http://www.seonyx.com/"
        End If
    End Function

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

    <System.Web.Services.WebMethod()> _
    Public Shared Function positiveoutcome(clickstatus As String) As String
        REM update database
        REM Need to add a record to the click history
        REM Get the location and IP of the clicker

        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim strSQL As String = "INSERT INTO clickhistory(clickdatetime, linkId) VALUES(SYSDATETIME(), " & clickstatus & ")"

        Try
            con.ConnectionString = ConfigurationManager.AppSettings("MSSQLConnectionString")
            con.Open()
            cmd.Connection = con
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

        Catch ex As Exception

        Finally
            con.Close()
        End Try

    End Function

    Sub validateuser()
        REM Check the user is entitled to a click. If not redirect to a signup page.
        Dim myreader As System.Data.SqlClient.SqlDataReader
        Dim cn As New System.Data.SqlClient.SqlConnection
        Dim cmd As New System.Data.SqlClient.SqlCommand
        Dim currenttotal As Integer = 0
        Dim maxhits As Integer = Convert.ToInt32(ConfigurationManager.AppSettings("MaximumDailyHits"))
        Dim currentUser As SiteUser = SiteUtils.GetCurrentSiteUser()
        Try
            cn.ConnectionString = ConfigurationManager.AppSettings("MSSQLConnectionString")
            cn.Open()

            cmd.Connection = cn
            cmd.CommandText = "select links.userId, COUNT(links.userid) Link_count from links where userid = " & userid & " and dateEntered between DATEADD(DAY, -1, SYSDATETIME()) and SYSDATETIME() GROUP BY links.userid"
            myreader = cmd.ExecuteReader



            If myreader.HasRows = True Then
                While myreader.Read
                    currenttotal = myreader.GetValue(1)
                End While
            End If
            cn.Close()

        Catch ex As Exception
            ' = ex.ToString
            Response.Write(ex.ToString)
        End Try
        Dim userstatus As String = "free"
        If Not System.Web.HttpContext.Current.Session("purchasestatus") Is Nothing Then
            userstatus = System.Web.HttpContext.Current.Session("purchasestatus").ToString
        End If
        If userstatus = "paid" Then
            REM do nothing
        Else
            REM no evidence of the user being paid in the current session. 
            REM so check the db in case the session timed out
            Dim myreader2 As System.Data.SqlClient.SqlDataReader
            Dim cn2 As New System.Data.SqlClient.SqlConnection
            Dim cmd2 As New System.Data.SqlClient.SqlCommand
            Dim usertype As String = "free"

            Try
                cn2.ConnectionString = ConfigurationManager.AppSettings("MSSQLConnectionString")
                cn2.Open()

                cmd2.Connection = cn2
                cmd2.CommandText = "select usertype from userinformation where userid = " & userid
                myreader2 = cmd2.ExecuteReader



                If myreader2.HasRows = True Then
                    Dim UserTypeGuidPOS = myreader2.GetOrdinal("usertype")
                    While myreader2.Read
                        usertype = myreader2.GetValue(UserTypeGuidPOS)
                    End While
                End If
                cn2.Close()

            Catch ex As Exception
                ' = ex.ToString
                Response.Write(ex.ToString)
            End Try
            If usertype = "paid" Then
                REM do nothing
            Else
                REM there is no evidence in the db that a product is current so redirect to the shop
                If currenttotal >= maxhits Then Response.Redirect(siteurl & sitefolder & "/shop?pageid=" & PageId & "&mid=" & moduleId)
            End If

        End If

    End Sub

    Public Function GetRandomString(ByVal iLength As Integer) As String
        Dim rdm As New Random()
        Dim allowChrs() As Char = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray()
        Dim sResult As String = ""

        For i As Integer = 0 To iLength - 1
            sResult += allowChrs(rdm.Next(0, allowChrs.Length))
        Next

        Return sResult
    End Function

    Function checkhasprotocol(strURL As String)
        REM could be this url does not have the prefix http://
        Select Case LCase(Left(strURL, 7))
            Case "http://"
                Return strURL
            Case "https://"
                Return strURL
            Case Else
                Return ("http://") & strURL
        End Select
    End Function
End Class