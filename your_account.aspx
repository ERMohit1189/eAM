<%@ Page Title="" Language="C#" MasterPageFile="~/root-manager.master" AutoEventWireup="true" CodeFile="your_account.aspx.cs" Inherits="your_account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
      <div class ="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                     <p class="text-justify" id="para1" runat="server">
                                <strong>Name of Organization: </strong>
                                <asp:Label ID="lblNameOrg" runat="server" Text=""></asp:Label>
                            </p>
                            <p class="text-justify" id="para2" runat="server">
                                <strong>Customer ID: </strong>
                                <asp:Label ID="lblCid" runat="server" Text=""></asp:Label>
                            </p>
                            <h4 style="font-size: 16px;">Subscription Information</h4>
                            <p class="text-justify"><strong>Product Name: </strong>eAM&reg;</p>
                            <p class="text-justify" id="para3" runat="server">
                                <strong>Subscribed Modules: </strong>
                                <asp:Label ID="lblSubModule" runat="server" Text=""></asp:Label>
                            </p>
                            <p class="text-justify" id="para4" runat="server">
                                <strong>Subscription Status: </strong>
                                <asp:Label ID="lblSubStatus" runat="server" Text=""></asp:Label>
                            </p>
                            <p class="text-justify" id="para5" runat="server">
                                <strong>Billing Frequency: </strong>
                                <asp:Label ID="lblBillingFrequency" runat="server" Text=""></asp:Label>
                            </p>
                            <p class="text-justify" id="para6" runat="server">
                                <strong>Billing Currency: </strong>
                                <asp:Label ID="lblBillingCurrency" runat="server" Text=""></asp:Label>
                            </p>
                            <p class="text-justify" id="para7" runat="server">
                                <strong>Next Due Date: </strong>
                                <asp:Label ID="lblNextDueDate" runat="server" Text=""></asp:Label>
                            </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


</asp:Content>

