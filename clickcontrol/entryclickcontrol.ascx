<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="entryclickcontrol.ascx.vb" Inherits="earnaclick.UI.entryclickcontrol" %>
<portal:outerwrapperpanel ID="pnlOuterWrap" runat="server">
<mp:CornerRounderTop id="ctop1" runat="server" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper Browserfeature">
<portal:ModuleTitleControl EditText="Edit" EditUrl="~/Browserfeature/BrowserfeatureEdit.aspx" runat="server" id="TitleControl" />
<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
<div class="art-postcontent art-postcontent-0 clearfix">
	<div class="art-content-layout-wrapper layout-item-0">
		<div class="art-content-layout">
			<div class="art-content-layout-row">
				<div class="art-layout-cell layout-item-1" style="width: 100%">
                    <asp:Label ID="Label1" runat="server" Visible="true" Text="Label"></asp:Label>
                    <div  id="senderdiv" runat="server" >
                        <a href="<%= senderURL %>" class="art-button">Got it...</a>
                    </div><!-- end senderdiv-->
                </div><!-- end art-layout-cell layout-item-1 -->
            </div><!-- end art-content-layout-row -->
        </div><!-- end art-content-layout -->
    </div><!-- end art-content-layout-wrapper layout-item-0-->
    <article class="art-post art-article">

<div class="art-content-layout-wrapper layout-item-0">
<div class="art-content-layout">
<div class="art-content-layout-row">
<div class="art-layout-cell layout-item-1" style="width: 33%;">
<h3 style="border-bottom: 1px dotted #9DC4D7; padding-bottom: 5px;">Need Traffic?</h3>

<p><span style="font-weight: bold;">There are often times as a developer or marketer that you wish you had traffic.</span></p>

<p>Not your own clicks but real unsimulated traffic from remote IP addresses, perhaps to test geolocation or reporting software.</p>

<p>With Earnaclick you can participate in a system that just does that.</p>

<div class="image-caption-wrapper" style="width: 100%;"><img alt="Image by Fotolia" class="art-lightbox" src="data/sites/1/skins/earnaclick_V2/images/54fbb.jpg" style="width: 100%; max-width: 450px;" />
</div>
</div>

<div class="art-layout-cell layout-item-1" style="width: 34%;">
<h3 style="border-bottom: 1px dotted #9DC4D7; padding-bottom: 5px;">How does it work?</h3>

<p><span style="font-weight: bold;">To earn a click you must click on a link that we give you to visit someone else's site.</span></p>

<p>Once you have done that you have earned the right to have your desired link entered into our database for someone else to click.</p>

<p>In that way you receive but traffic from a completely different IP address.</p>

<div class="image-caption-wrapper" style="width: 100%;"><img alt="Image by Fotolia" class="art-lightbox" src="data/sites/1/skins/earnaclick_V2/images/6a5be.jpg" style="width: 100%; max-width: 400px;" />
</div>
</div>

<div class="art-layout-cell layout-item-2" style="width: 33%;">
<h3 style="border-bottom: 1px dotted #9DC4D7; padding-bottom: 5px;">Warning!</h3>

<p><span style="font-weight: bold;">Note that Earnaclick do not condone click-fraud.</span></p>

<p>This service is provided as a tool for professional use.</p>

<p>Anyone found using the service to deliberately defraud will have their account terminated.</p>

<div class="image-caption-wrapper" style="width: 100%;"><img alt="Image by Wikimedia" class="art-lightbox" src="data/sites/1/skins/earnaclick_V2/images/71506.jpg" style="width: 100%; max-width: 450px;" />
</div>
</div>
</div>
</div>
</div>
<!-- 
	<div class="art-content-layout-wrapper layout-item-0">
		<div class="art-content-layout">
			<div class="art-content-layout-row">
				<div class="art-layout-cell layout-item-1" style="width: 33%;">
					<h3 style="border-bottom: 1px dotted #9DC4D7; padding-bottom: 5px;">Gallery</h3>
					<p>
						<span style="font-weight: bold;"><span style="font-style: italic;">Mattis ipsum vestibulum libero quisque leo erat phasellus suspendisse. Aenean maecenas ipsum congue justo tempus tortor orci in nullam lobortis pretium.</span></span>
					</p>
					<ul>
						<li><span style="font-weight: bold;">Nunc dictum rutrum. </span><br/>
						Dui neque a consectetuer a ac orci. Suspendisse ligula leo.</li>
						<li><span style="font-weight: bold;">Ipsum quis eros sit.</span><br/>
						Blandit donec mi ut felis eu at nec. Cras aliquam suscipit.</li>
						<li><span style="font-weight: bold;">Pellentesque tempus.</span><br/>
						Urna egestas faucibus ornare mattis. Imperdiet donec tortor.</li>
						<li><span style="font-weight: bold;">Nunc accumsan augue.</span><br/>
						Leo viverra laoreet erat etiam mi. Magna parturient duis.</li>
					</ul>
					<p>
						<a class="art-button" href="#">Read more</a>
					</p>
				</div>
				<div class="art-layout-cell layout-item-1" style="width: 34%;">
					<h3 style="border-bottom: 1px dotted #9DC4D7; padding-bottom: 5px;">Subscription</h3>
					<p>
						<span style="font-weight: bold;"><span style="font-style: italic;">Libero metus enim elit lacus aenean donec quis mi curabitur taciti nunc. Ut non a morbi et consectetuer gravida iaculis mauris cursus facilisi sodales ac.</span></span>
					</p>
					<div class="image-caption-wrapper" style="width: 60%; float: left;">
						<img alt="an image" class="art-lightbox" src="data/sites/1/skins/earnaclick_V2/images/64427.jpg" style="width: 100%; max-width: 450px;"/>
						<p>
							Image by Flickr/Jan Tik
						</p>
					</div>
					<p>
						<span style="font-weight: bold;">Faucibus phasellus varius et consectetuer nisi ligula tempus felis.</span>
					</p>
					<p>
						Et fusce suscipit non nullam curabitur morbi eu nonummy nunc fusce dolor lectus consectetuer pellentesque. Dapibus tortor pellentesque quam nam class amet sapien sed in nulla egestas orci. Eleifend leo vivamus neque.
					</p>
				</div>
				<div class="art-layout-cell layout-item-2" style="width: 33%;">
					<h3 style="border-bottom: 1px dotted #9DC4D7; padding-bottom: 5px;">Follow Us</h3>
					<p>
						<span style="font-weight: bold;"><span style="font-style: italic;">Ultrices pretium nunc hendrerit eros curabitur et malesuada vestibulum. Quis quam enim lectus felis turpis nam porta varius pellentesque porttitor urna. </span></span>
					</p>
					<div class="image-caption-wrapper" style="width: 60%; float: left;">
						<img alt="an image" class="art-lightbox" src="data/sites/1/skins/earnaclick_V2/images/9c609.jpg" style="width: 100%; max-width: 406px;"/>
						<p>
							Image by Flickr/HiggySTFC
						</p>
					</div>
					<p>
						<span style="font-weight: bold;">Aliquam augue enim at pellentesque libero molestie nisl nibh nulla.</span>
					</p>
					<p>
						Fusce proin hymenaeos libero ante et volutpat in risus aliquam sem tempor cum condimentum varius ultricies. Lacus jnteger arcu eleifend a luctus purus mi luctus mollis pellentesque dolor. Lectus libero eget mattis et.
					</p>
				</div>
			</div>
		</div>
	</div> --></div>
</article>


</portal:InnerBodyPanel>
</portal:OuterBodyPanel>
<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
</portal:InnerWrapperPanel>
<mp:CornerRounderBottom id="cbottom1" runat="server" />
</portal:outerwrapperpanel>

