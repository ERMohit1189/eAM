<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ReceiptSrNoMaster.aspx.cs" Inherits="ReceiptSrNoMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Receipt No. Text&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:Label ID="lblReceiptNoText" runat="server" CssClass="form-control-blue validatetxt" style="color:#f00; font-weight:bold;"></asp:Label>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Receipt No. Starting From&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                    <asp:RadioButtonList runat="server" ID="rdoStart" RepeatDirection="Horizontal" CssClass="vd_radio radio-success">
                                        <asp:ListItem Value="1">Start Every Year From 1</asp:ListItem>
                                        <asp:ListItem Value ="2">Start Serialwise</asp:ListItem>
                                    </asp:RadioButtonList>
                                            </div>
                                         </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button form-control-blue ">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px;"></div>
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
