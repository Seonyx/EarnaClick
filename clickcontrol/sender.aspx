<%@ Page MasterPageFile="~/App_MasterPages/layout.Master" Language="vb" AutoEventWireup="false" CodeBehind="sender.aspx.vb" Inherits="earnaclick.UI.sender" %>
 <asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" >
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" EnableViewState="false"  />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper ">
<portal:HeadingControl ID="heading" runat="server" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">

<div class="settingrow">
        <script>
            var url = '<%=linkurl%>';

            function lose_in_ie() {
                // ie loses referer in window.open()
                if (url.indexOf("?") != -1) {
                    window.open(url + '&_=' + Math.random());
                }
                else {
                    window.open(url + '?_=' + Math.random());
                }
                report();
            }

            function lose_in_ff() {
                var newWindow = window.open();
                newWindow.document.write('<html><meta http-equiv="refresh" content="0; url=' + url + '"></html>');
                newWindow.document.close();
                report();
            }


            function report() {
                var pathname = '<%=siteurl%><%=Sitefolder%><%=modulepath%>/sender.aspx';

                $.ajax({
                    type: "POST",
                    url: pathname + '/positiveoutcome',

                    data: "{'clickstatus': '<%=linkID%>'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: window.location.replace('<%=siteurl%><%=modulepath%>/receiver.aspx?code=<%=strrandomcode%>&pageid=<%=pageId%>&mid=<%=moduleId%>'),
            failure: function (response) {
                alert(response.d);
            },
            error: function (xhr, status, error) {
                // Display a generic error for now.
                window.location.replace('<%=siteurl%><%=modulepath%>/error.aspx?code=100&pageid=<%=pageId%>&mid=<%=moduleId%>')
        }
                });
    }

</script>
    <asp:Label ID="Label1" runat="server" Visible="true" Text="Label"></asp:Label>
    <div id="linkpanel" runat="server">
      
        <a href="#" class="art-button"  onclick="<%= browser %>";">Click here</a> 
    </div>
</div><!-- end settingrow-->



</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel> 
<mp:CornerRounderBottom id="cbottom1" runat="server" EnableViewState="false" />	
</portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" >
</asp:Content>
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />