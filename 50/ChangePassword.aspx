<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="admin_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <script>

        function showPassword1(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_txtOldPanel");
            textbox.type = "text";
        }
        function hidePassword1(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_txtOldPanel");
            textbox.type = "password";
        }
        function showPassword2(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_txtNewPanel");
            textbox.type = "text";
        }
        function hidePassword2(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_txtNewPanel");
            textbox.type = "password";
        }
        function showPassword3(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_TextConfNewPassw");
            textbox.type = "text";
        }
        function hidePassword3(element) {
            var textbox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_TextConfNewPassw");
            textbox.type = "password";
        }
    </script>
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

                                            <asp:Label ID="old" runat="server" class="col-sm-4 text-right txt-middle-l txt-bold " Text="Old Password *"></asp:Label>
                                            <div class="col-sm-8  mgbt-xs-20">
                                                <div class="vd_input-wrapper controls">
                                                    <span class="menu-icon cursor-p" id="eye" title="Show Password/Hide Password"
                                                        data-placement="left" runat="server" onmousedown="showPassword1(this.id)"
                                                        onmouseup="hidePassword1(this.id)"><i class="fa fa-eye"></i></span>

                                                    <asp:TextBox ID="txtOldPanel" runat="server" AutoPostBack="True"
                                                        OnTextChanged="txtOldPanel_TextChanged"
                                                        TextMode="Password" CssClass="form-control-blue" ToolTip="Show Password/Hide Password"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="LblMatch" runat="server" Style="color: #cc0000;"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-8 no-padding">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" class="col-sm-4 text-right txt-middle-l txt-bold " Text="New Password *"></asp:Label>
                                            <div class="col-sm-8  mgbt-xs-20">
                                                <div class="vd_input-wrapper controls">
                                                    <span class="menu-icon cursor-p" id="Span1" title="Show Password/Hide Password"
                                                        data-placement="left" runat="server" onmousedown="showPassword2(this.id)"
                                                        onmouseup="hidePassword2(this.id)"><i class="fa fa-eye"></i></span>
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNewPanel" runat="server" TabIndex="1" TextMode="Password" CssClass="form-control-blue"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-8 no-padding">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" class="col-sm-4 text-right txt-middle-l txt-bold " Text="Confirm New Password *"></asp:Label>
                                            <div class="col-sm-8  mgbt-xs-20">
                                                <div class="vd_input-wrapper controls">
                                                    <span class="menu-icon cursor-p" id="Span2" title="Show Password/Hide Password"
                                                        data-placement="left" runat="server" onmousedown="showPassword3(this.id)"
                                                        onmouseup="hidePassword3(this.id)"><i class="fa fa-eye"></i></span>
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

