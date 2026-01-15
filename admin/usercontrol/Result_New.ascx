<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Result_New.ascx.cs" Inherits="admin_Examination" %>


<style>
    .append-icon {
        margin-right: 2px;
    }
</style>
<div class="text-left col-lg-2 col-md-2 col-xs-2 col-sm-2 no-padding" style="padding-left: 0px">
    <div runat="server" id="ctrl13" class="mgbt-xs-5 p-mgbt-xs-5">
        <asp:Image ID="Image2" runat="server" class="logo-size" ImageUrl="~/img/cbse-logo.png" style="Width: 80px; Height: 80px;" />
    </div>
    <div runat="server" id="ctrl14" class="mgbt-xs-5 p-mgbt-xs-5" visible="false">
        <asp:Image ID="Image3" runat="server" class="logo-size" />
    </div>
</div>

<div class=" text-center col-lg-8 col-md-8 col-xs-8 col-sm-8 no-padding " id="ctrlmain" runat="server">
    <div class="main-titel-box">
        <h1 id="ctrl2" runat="server" class="main-name">
            <asp:Label ID="lblInstitute" runat="server" style="font-weight: bold; font-size: 21px !important;"></asp:Label>
        </h1>
        <h3 id="ctrl3" runat="server" class="sub-adds ">
            <asp:Label ID="lblAddress" runat="server"></asp:Label>
        </h3>
        <h3 id="ctrl4" runat="server" class="sub-adds hidden">
            <asp:Label ID="lblBranchandCity" runat="server"></asp:Label>
        </h3>
        <h3 id="ctrl5" runat="server" class="sub-adds hidden">
            <asp:Label ID="lblCity" runat="server"></asp:Label>
        </h3>
        <h3 id="ctrl6" runat="server" class="sub-adds ">
            <i class="append-icon fa fa-phone-square"></i> 
            <asp:Label ID="lblContactnoandemail" runat="server"></asp:Label>
            <i class="append-icon fa fa-envelope-o"></i>
            <asp:Label ID="lblContactnoandemail1" runat="server"></asp:Label>
        </h3>
        <h3 id="ctrl7" runat="server" class="sub-adds hidden">
            <i class="append-icon fa fa-phone-square"></i>
            <asp:Label ID="lblPhoneNo" runat="server"></asp:Label>
        </h3>

        <h3 id="ctrl8" runat="server" class="sub-adds hidden">
            <i class="append-icon fa fa-envelope-o"></i>
            <asp:Label ID="lblEmail" runat="server"></asp:Label>
        </h3>

        <h3 id="ctrl10" runat="server" class="sub-adds ">
            <i class="append-icon fa fa-globe"></i>
            <%--     <asp:HyperLink ID="hylWebsite" runat="server" Target="_blank">--%>
            <asp:Label ID="lblWebsite" runat="server"></asp:Label>
            <%--    </asp:HyperLink>--%>

        </h3>
        <h3 id="ctrl9" runat="server" class="sub-adds hidden" style="text-transform:lowercase !important;">
            <i class="append-icon fa fa-envelope-o"></i>
            <asp:Label ID="lblWebsiteandEmail" runat="server"></asp:Label>
            <i class="append-icon fa fa-globe"></i>
            <asp:HyperLink ID="hylWebsiteandEmail1" runat="server" Target="_blank">
                <asp:Label ID="lblWebsiteandEmail1" runat="server" Text=""></asp:Label>
            </asp:HyperLink>
        </h3>
        <h3 id="ctrl11" runat="server" class="sub-adds hidden">
            <asp:Label ID="Label1" runat="server" Text="Affiliated to"></asp:Label>
            <asp:Label ID="lblAffilation" runat="server"></asp:Label>
        </h3>
        <h3 id="ctrl12" runat="server" class="sub-adds hidden">
            <asp:Label ID="lblSlogan" runat="server"></asp:Label>
        </h3>
        <h3 runat="server" id="ctrl15" class="sub-adds hidden">Affiliation No. 
            <asp:Label ID="lblAffilationNo" runat="server" Text=""></asp:Label>
            &nbsp;
           <%-- School No. :
            <asp:Label ID="lblschoolno" runat="server" Text="139"> </asp:Label>--%>
        </h3>
        <h3 class="form-name">
            <span class="span">PRE BOARD MARK DETAILS FOR SESSION 
                <asp:Label ID="lblSession" runat="server" Text=""></asp:Label></span>
        </h3>


    </div>
</div>

<div class=" text-left col-lg-2 col-md-2 col-xs-2 col-sm-2 no-padding" id="ctrl1" runat="server" style="margin-right:0px !important;">

    <div class="mgbt-xs-5 p-mgbt-xs-5">
        <asp:Image ID="Image1" runat="server" class="logo-size" />
    </div>

</div>









