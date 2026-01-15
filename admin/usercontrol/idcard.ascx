<%@ Control Language="C#" AutoEventWireup="true" CodeFile="idcard.ascx.cs" Inherits="admin_Report" %>

<style>
    .append-icon {
        margin-right: 2px;
    }
</style>

<div class="text-left col-lg-2 col-md-2 col-xs-2 col-sm-2" style="padding-left: 2px">
    <div runat="server" id="ctrl13" class="mgbt-xs-5 p-mgbt-xs-5" style="text-align: right !important;">
        <asp:Image ID="Images" runat="server"  style="max-width:50px !important; height: inherit !important; text-align: center; margin-top: 0px;" />
    </div>
   
</div>

<div class=" text-center col-lg-10 col-md-10 col-xs-10 col-sm-10 no-padding " id="ctrlmain" runat="server">
    <div class="main-titel-box" style="margin-top:4px;">
        <h1 id="ctrl2" runat="server" class="main-name" style="font-size: 11px; line-height:14px; font-weight: 900; color: #cf251e; padding: 0 0px 0 0px !important;">
            <asp:Label ID="lblInstitute" runat="server"></asp:Label>
        </h1>
        <h3 id="ctrl3" runat="server" class="sub-adds " style="font-size:10px;letter-spacing: -.5px; line-height:12px; font-weight: bold; color: #000; margin: 5px 0px 5px 0px  !important;">
            <asp:Label ID="lblAddress" runat="server"></asp:Label>
        </h3>
        <h3 id="ctrl4" runat="server" class="sub-adds ">
            <asp:Label ID="lblBranchandCity" runat="server"></asp:Label>
        </h3>
        <h3 id="ctrl5" runat="server" class="sub-adds " style="font-size: 8px;">
            <asp:Label ID="lblCity" runat="server"></asp:Label>
        </h3>
           <h3 id="phonectrl6" runat="server" class="sub-adds ">
           <span style="font-size: 10px;     font-weight: bold;">Phone No.:</span> <asp:Label ID="lblPhoneNumber" runat="server" style="color: #000;font-size: 10px;     font-weight: bold;"></asp:Label>
        </h3>
       <%-- <h3 id="ctrl6" runat="server" class="sub-adds ">
            <i class="append-icon fa fa-phone-square"></i>+91 
            <asp:Label ID="lblContactnoandemail" runat="server"></asp:Label>
            <i class="append-icon fa fa-envelope-o"></i>
            <asp:Label ID="lblContactnoandemail1" runat="server"></asp:Label>
        </h3>
        <h3 id="ctrl7" runat="server" class="sub-adds ">
            <i class="append-icon fa fa-phone-square"></i>+91 
            <asp:Label ID="lblPhoneNo" runat="server"></asp:Label>
        </h3>

        <h3 id="ctrl8" runat="server" class="sub-adds ">
            <i class="append-icon fa fa-envelope-o"></i>
            <asp:Label ID="lblEmail" runat="server"></asp:Label>
        </h3>

        <h3 id="ctrl10" runat="server" class="sub-adds ">
            <i class="append-icon fa fa-globe"></i>
            <asp:Label ID="lblWebsite" runat="server"></asp:Label>
        </h3>
        <h3 id="ctrl9" runat="server" class="sub-adds ">
            <i class="append-icon fa fa-envelope-o"></i>
            <asp:Label ID="lblWebsiteandEmail" runat="server"></asp:Label>
            <i class="append-icon fa fa-globe"></i>
            <asp:HyperLink ID="hylWebsiteandEmail1" runat="server" Target="_blank">
                <asp:Label ID="lblWebsiteandEmail1" runat="server" Text=""></asp:Label>
            </asp:HyperLink>
        </h3>
        <h3 id="ctrl11" runat="server" class="sub-adds ">
            <asp:Label ID="Label1" runat="server" Text="Affiliated to"></asp:Label>
            <asp:Label ID="lblAffilation" runat="server"></asp:Label>
        </h3>
        <h3 id="ctrl12" runat="server" class="sub-adds ">
            <asp:Label ID="lblSlogan" runat="server"></asp:Label>
        </h3>
        <h3 runat="server" id="ctrl15" class="sub-adds ">Affiliation No. 
            <asp:Label ID="lblAffilationNo" runat="server" Text=""></asp:Label>
            &nbsp;
        </h3>--%>
    </div>
</div>










