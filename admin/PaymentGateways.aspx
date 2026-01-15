<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PaymentGateways.aspx.cs" Inherits="PaymentGateways" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
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
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Provider</label>
                                    <asp:DropDownList ID="ddlGatewayname" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlGatewayname_SelectedIndexChanged">
                                        <asp:ListItem Value="PayUMoney">PayU</asp:ListItem>
                                        <asp:ListItem Value="Eazypay">Eazypay</asp:ListItem>
                                        <asp:ListItem Value="PayStack">Paystack</asp:ListItem>
                                        <asp:ListItem Value="Razorpay">Razorpay</asp:ListItem>
                                        <asp:ListItem Value="Atom">Atom</asp:ListItem>
                                    </asp:DropDownList>
                                        </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">For</label>
                                        <asp:DropDownList ID="ddlGatewayFor" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlGatewayFor_SelectedIndexChanged">
                                        <asp:ListItem Value="Fee">Fee Management</asp:ListItem>
                                        <asp:ListItem Value="Admission">Admission Portal</asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-12 no-padding" runat="server" id="divPayumoney">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Success URL</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtSU" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Failure URL</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtFU" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Service Provider</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtSP" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Product Info</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtPI" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Merchant Key</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtMK" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Merchant Salt</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtMS" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Base URL</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtBU" runat="server" CssClass="form-control-blue" ></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Hash Sequence</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtHS" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Authorization Header</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtAuthHeader" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 no-padding" runat="server" id="divEazypay" visible="false">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Return URL</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtGatwayURL" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Merchant ID</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtMerchantID" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">AES Key</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtAESKey" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 no-padding" runat="server" id="divPayStack" visible="false">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Pseudo-unique reference<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtPUnique" runat="server" CssClass="form-control-blue" Text=""></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Live Secret Key<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtLSK" runat="server" CssClass="form-control-blue" Text=""></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Live Public Key<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtLPK" runat="server" CssClass="form-control-blue" Text=""></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Email<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-blue" Text=""></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Base URL<span class="mondatory">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtbaseUrl" runat="server" CssClass="form-control-blue" Text=""></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rbActivate" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:RadioButtonList>
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
                                <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Payment Remark<span class="mondatory"></span></label>
                                    <asp:TextBox runat="server" ID="paymentCharges"></asp:TextBox>
                                </div>
                                 
                                <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                    <asp:LinkButton ID="lnkSubmit" CssClass="button form-control-blue" runat="server" OnClick="lnkSubmit_Click" ValidationGroup="a">Submit</asp:LinkButton>
                                    </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkSubmit" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                            <div class=" ">

                                        <div class="text-box-msg">
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

