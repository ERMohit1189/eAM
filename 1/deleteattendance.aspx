<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="deleteattendance.aspx.cs" Inherits="deleteattendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="thy" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">
                                    

                                    <div class="col-sm-2">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpAtteClass" runat="server" OnSelectedIndexChanged="DrpAtteClass_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpBranch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpsection" runat="server" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label8" runat="server" class="control-label" Text="From"></asp:Label>&nbsp;
                                        <span class="vd_red">*</span>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpSaal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpSaal_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpMahina" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMahina_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDin" runat="server" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label4" runat="server" class="control-label" Text="To"></asp:Label>&nbsp;
                                        <span class="vd_red">*</span>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpSaalTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpSaalTo_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpMahinaTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMahinaTo_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDinTo" runat="server" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">Delete</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 64px"></div>
                                    </div>

                                </div>

                                
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to delete attendance between date range?
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" OnClick="Button8_Click" CausesValidation="False" />
                                &nbsp;&nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

