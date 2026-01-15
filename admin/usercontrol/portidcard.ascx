<%@ Control Language="C#" AutoEventWireup="true" CodeFile="portidcard.ascx.cs" Inherits="admin_Report" %>


<style>
    .append-icon {
        margin-right: 2px;
    }
</style>
<div class="text-left col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding">
    <div runat="server" id="ctrl13" class="mgbt-xs-5 p-mgbt-xs-5" style="text-align: center !important;">
        <asp:Image ID="Image1" runat="server" class="port-logo-size" style="margin-top: 4px;" />
    </div>
   
</div>

<div class=" text-center col-lg-9 col-md-9 col-xs-9 col-sm-9 no-padding " id="ctrlmain" runat="server">
    <div class="main-titel-box" style="margin-top:  5px;">
        <h1 id="ctrl2" runat="server" class="main-name" style="font-size: 11px; color: #cf251e;     font-weight: bold;">
            <asp:Label ID="lblInstitute" runat="server"></asp:Label>
        </h1>
        <h3 id="ctrl3" runat="server" class="sub-adds " style="font-size: 10px;     font-weight: bold;">
            <asp:Label ID="lblAddress" runat="server" style="color: #000;"></asp:Label>
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










