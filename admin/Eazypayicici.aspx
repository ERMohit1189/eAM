<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Eazypayicici.aspx.cs" Inherits="admin.Eazypayicici" %>

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
                                <div class="col-sm-12 no-padding ">
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
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rbActivate" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:RadioButtonList>
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
                                    
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
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

