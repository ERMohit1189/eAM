<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="payStack.aspx.cs" Inherits="admin.payStack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        .mondatory {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding ">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Service Provider<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtSP" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Success Url<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtSU" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Failure  Url<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtFU" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Pseudo-unique reference<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtPUnique" runat="server" CssClass="form-control-blue" Text="1000000000"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Live Secret Key<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtLSK" runat="server" CssClass="form-control-blue" Text="sk_test_777b695a8d4693b7105b7bd896c08a7b9a8f23dd"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Live Public Key<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtLPK" runat="server" CssClass="form-control-blue" Text="pk_test_7daaa6985c14039c9a22a9a67cb15aa53e68de7c"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Email<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-blue" Text="customer@email.com"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Base URL<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtBU" runat="server" CssClass="form-control-blue" Text="https://js.paystack.co/v1/inline.js"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Upload Logo<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:FileUpload ID="fpUploadPhoto" runat="server" CssClass="form-control-blue " />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Logo<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:Image runat="server" ID="imgLogo" Width="100" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 no-padding ">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rbActivate" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lnkSubmit" CssClass="button form-control-blue" runat="server" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkSubmit" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class=" " style="width: 100%;">
                                        <div class="text-box-msg" style="width: 100%;">
                                            <div id="msgbox" runat="server"></div>
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

