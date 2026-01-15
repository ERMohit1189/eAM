<%@ Page Title="Change Password | eAM" Language="C#" MasterPageFile="stuRootManager.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="admin_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-lg-2 no-padding"></div>
                                <div class="col-lg-10 no-padding">
                                    <div class="col-sm-8 no-padding">
                                        <div class="form-group">
                                            <asp:Label ID="lblVendor" runat="server" class="col-sm-4 text-right txt-middle-l txt-bold " Text="Old Password *"></asp:Label>
                                            <div class="col-sm-8  mgbt-xs-20">
                                                <asp:TextBox ID="txtOldPanel" runat="server" AutoPostBack="True"
                                                    OnTextChanged="txtOldPanel_TextChanged"
                                                    TextMode="Password" CssClass="form-control-blue"></asp:TextBox>
                                                <asp:Label ID="LblMatch" runat="server" Style="color:#cc0000;"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-8 no-padding">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" class="col-sm-4 text-right txt-middle-l txt-bold " Text="New Password *"></asp:Label>
                                            <div class="col-sm-8  mgbt-xs-20">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNewPanel" runat="server" TabIndex="1" TextMode="Password" CssClass="form-control-blue"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-8 no-padding">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" class="col-sm-4 text-right txt-middle-l txt-bold " Text="Confirm New Password *"></asp:Label>
                                            <div class="col-sm-8  mgbt-xs-20">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TextConfNewPassw" runat="server"
                                                            TextMode="Password" TabIndex="3" CssClass="form-control-blue ValidateTextBox"></asp:TextBox>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                            ControlToCompare="txtNewPanel" ControlToValidate="TextConfNewPassw"
                                                            ErrorMessage="Password not matched!" Style="color: #CC0000" Display="Dynamic"></asp:CompareValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 no-padding">
                                        <div class="form-group">
                                            <div class="col-sm-4">
                                            </div>
                                            <div class="col-sm-8  mgbt-xs-20">
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return ValidateTextBox('.ValidateTextBox');"
                                                    OnClick="LinkButton1_Click" CssClass="button" TabIndex="4">Submit</asp:LinkButton>
                                                &nbsp;<asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                <div id="msgbox" runat="server" style="left: 75px"></div>
                                            </div>
                                        </div>
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

