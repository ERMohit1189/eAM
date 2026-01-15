<%@ Page Title="Change Password | eAM ®" Language="C#" MasterPageFile="main-ap.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ChangePassword.aspx.cs" Inherits="admin_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        .x-navigation li.active2 > a .fa,
        .x-navigation li.active2 > a .glyphicon {
            color: #ffd559;
        }

        .x-navigation li.active21 > a .fa,
        .x-navigation li.active21 > a .glyphicon {
            color: #ffd559;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                                        <div class="vd_login-page">
                                            <div class="panel widget">
                                                <div class="panel-body">
                                                    <div class="col-sm-12 no-padding ">
                                                        <div class="col-md-4 col-sm-6  ">
                                                            <label class="control-label">Old Password  &nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:TextBox ID="txtOldPanelss" runat="server" CssClass="form-control-blue validatetxt" TextMode="Password"></asp:TextBox>
                                                                <asp:Label ID="LblMatch" runat="server" Style="color: #CC0000"></asp:Label>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-4 col-sm-6 ">
                                                            <label class="control-label">New Password  &nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtNewPanel" runat="server" CssClass="form-control-blue validatetxt" TextMode="Password"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPanel" ErrorMessage="*" ForeColor="#CC3300" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-4 col-sm-6  ">
                                                            <label class="control-label">Confirm Password &nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:TextBox ID="TextConfNewPassw" runat="server" CssClass="form-control-blue validatetxt" TextMode="Password"></asp:TextBox>
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPanel" ControlToValidate="TextConfNewPassw" ErrorMessage="Password not matched!" SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="a"></asp:CompareValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4 col-sm-6   btn-a-devices-2-p2 mgbt-xs-15">
                                                            <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn(this);" OnClick="Button1_OnClick" ValidationGroup="a" />
                                                            <asp:Button ID="Button2" runat="server" Text="Reset" CssClass="button" OnClick="Button2_OnClick" CausesValidation="False" />
                                                            <div id="msgbox" runat="server" style="left: 150px !important;"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

