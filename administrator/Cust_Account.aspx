<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/administrato_root-manager.master"
     EnableEventValidation="false" AutoEventWireup="true" CodeFile="Cust_Account.aspx.cs" Inherits="your_account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12  mgbt-xs-15">
                                        <strong>Name of Organization : </strong>
                                        <asp:Label ID="lblNameOrg" CssClass="vd_red" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Customer ID&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtCid" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  no-padding">
                                        <fieldset>
                                            <legend>
                                                <span class="font-s-17">Subscription Information</span>
                                            </legend>

                                            <div class="col-sm-12  mgbt-xs-15">
                                                <strong>Product Name : </strong><span >eAM&reg;</span>
                                                <asp:Label ID="Label1" CssClass="vd_red" runat="server" Text=""></asp:Label>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Subscribed Modules</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtSubModule" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Subscription Status</label>
                                                <div class="">
                                                    <asp:DropDownList ID="drpSubStatus" CssClass="form-control-blue" runat="server">
                                                        <asp:ListItem>Active</asp:ListItem>
                                                        <asp:ListItem>Inactive</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Billing Frequency</label>
                                                <div class="">
                                                    <asp:DropDownList ID="drpBillingFrequency" CssClass="form-control-blue" runat="server">
                                                        <asp:ListItem>Yearly</asp:ListItem>
                                                        <asp:ListItem>Monthly</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Billing Currency</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtBillingCurrency" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Next Due Date</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtNextDueDate" runat="server" CssClass="datepicker-normal form-control-blue validatetxt"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                                <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');" CssClass="button form-control-blue" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="width: 74px"></div>
                                            </div>


                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

