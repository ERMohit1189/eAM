<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Templates.aspx.cs" Inherits="Templates" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">
                                    <div class="col-sm-12  no-padding ">
                                        <div class="col-sm-12   mgbt-xs-15">
                                            <asp:RadioButtonList ID="reportType" runat="server" class="form-control-blue vd_radio radio-success" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="reportType_SelectedIndexChanged">
                                                <asp:ListItem Value="TC Templet" Selected="True">TC Template</asp:ListItem>
                                                <asp:ListItem Value="Receipt Templet">Receipt Template</asp:ListItem>
                                                <asp:ListItem Value="Admission Form Template">Admission Form Template</asp:ListItem>
                                                <asp:ListItem Value="Fee Card Template">Fee Card Template</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 no-padding" runat="server" id="TCTemplate">
                                        <%--<div class="col-sm-3 mgbt-xs-15">
                                            <label class="control-label">Board&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlBoard" runat="server" class="form-control-blue validatedrp1">
                                            </asp:DropDownList>
                                        </div>--%>
                                        <div class="col-sm-4 mgbt-xs-15">
                                            <label class="control-label">Templates&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlTemplate" runat="server" class="form-control-blue validatedrp1" AutoPostBack="true" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="Template 1">CBSE-English</asp:ListItem>
                                                <asp:ListItem Value="Template 2">CBSE-Hindi</asp:ListItem>
                                                <asp:ListItem Value="Template 3">ICSE/ISC</asp:ListItem>
                                                <asp:ListItem Value="Template 4">UPBoard-Hindi</asp:ListItem>
                                                <asp:ListItem Value="Template 5">UPBoard-English</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3 mgbt-xs-15" style="margin-top: 30px;">
                                            <asp:CheckBox ID="chkisLock" runat="server" class="form-control-blue vd_checkbox checkbox-success" Text="Do you want lock Template list?" />
                                        </div>
                                        <div class="col-sm-4   mgbt-xs-15">
                                            <div class="" style="margin-top: 25px;">
                                                <asp:LinkButton ID="LinkSubmit1" runat="server" CssClass="button form-control-blue" OnClick="LinkSubmit1_Click" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn(this);">Submit</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 74px;"></div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" runat="server" id="link" style="padding-bottom:10px;"> </div>
                                        <div class="col-sm-12">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Board">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Board") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Default Template">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Text") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Lock">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabeCopy" runat="server" Text='<%# Bind("IsLockList") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>

                                                            <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"
                                                                title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 no-padding" runat="server" id="ReceiptTemplate" visible="false">
                                        <%--<div class="col-sm-3 mgbt-xs-15">
                                            <label class="control-label">Board&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlBoardReceipt" runat="server" class="form-control-blue validatedrpRe">
                                            </asp:DropDownList>
                                        </div>--%>
                                        <div class="col-sm-4 mgbt-xs-15">
                                            <label class="control-label">Templates&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlTemplateReceipt" runat="server" class="form-control-blue validatedrpRe" AutoPostBack="true" OnSelectedIndexChanged="ddlTemplateReceipt_SelectedIndexChanged">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="Template 1">Template 1</asp:ListItem>
                                                <asp:ListItem Value="Template 2">Template 2</asp:ListItem>
                                                 <asp:ListItem Value="Template 3">Template 3</asp:ListItem>
                                                <asp:ListItem Value="Template 4">Template 4</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3 mgbt-xs-15 hide" style="margin-top: 30px;">
                                            <asp:CheckBox ID="chkisLockReceipt" runat="server" class="form-control-blue vd_checkbox checkbox-success" Text="Do you want lock Template list?" />
                                        </div>
                                        <div class="col-sm-4   mgbt-xs-15">
                                            <div class="" style="margin-top: 25px;">
                                                <asp:LinkButton ID="LinkReceipt" runat="server" CssClass="button form-control-blue" OnClick="LinkReceipt_Click" OnClientClick="ValidateTextBox('.validatetxtRe');ValidateDropdown('.validatedrpRe');return validationReturn(this);">Submit</asp:LinkButton>
                                                <div id="msgbox3" runat="server" style="left: 74px;"></div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" runat="server" id="linkreceipts" style="padding-bottom:10px;"> </div>
                                        <div class="col-sm-12">
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1s" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Board">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2s" runat="server" Text='<%# Bind("Board") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Default Template">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5s" runat="server" Text='<%# Bind("Text") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Lock" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabeCopys" runat="server" Text='<%# Bind("IsLockList") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>

                                                            <asp:Label ID="Label377s" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkrcpt" runat="server" OnClick="lnkrcptDelete_Click"
                                                                title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 no-padding" runat="server" id="AdmissionFormTemplate" visible="false">
                                       <%-- <div class="col-sm-3 mgbt-xs-15">
                                            <label class="control-label">Board&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlBoardAdmission" runat="server" class="form-control-blue validatedrp2">
                                            </asp:DropDownList>
                                        </div>--%>
                                        <div class="col-sm-4 mgbt-xs-15">
                                            <label class="control-label">Templates&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlTemplateAdmission" runat="server" class="form-control-blue validatedrp2" AutoPostBack="true" OnSelectedIndexChanged="ddlTemplateAdmission_SelectedIndexChanged">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="Template 1">Hindi Version</asp:ListItem>
                                                <asp:ListItem Value="Template 2">English Version</asp:ListItem>
                                                <asp:ListItem Value="Template 3">Extended Version</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3 mgbt-xs-15" style="margin-top: 30px;">
                                            <asp:CheckBox ID="chkisLockAdmission" runat="server" class="form-control-blue vd_checkbox checkbox-success" Text="Do you want lock Template list?" />
                                        </div>
                                        <div class="col-sm-4   mgbt-xs-15">
                                            <div class="" style="margin-top: 25px;">
                                                <asp:LinkButton ID="LinkAdmission" runat="server" CssClass="button form-control-blue" OnClick="LinkAdmission_Click" OnClientClick="ValidateTextBox('.validatetxt2');ValidateDropdown('.validatedrp2');return validationReturn(this);">Submit</asp:LinkButton>
                                                <div id="msgbox2" runat="server" style="left: 74px;"></div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" runat="server" id="linkAdmissions" style="padding-bottom:10px;"> </div>
                                        <div class="col-sm-12">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1a" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                   <%-- <asp:TemplateField HeaderText="Board">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2a" runat="server" Text='<%# Bind("Board") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Default Template">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5a" runat="server" Text='<%# Bind("Text") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Lock">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabeCopya" runat="server" Text='<%# Bind("IsLockList") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>

                                                            <asp:Label ID="Label377a" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkAdmissionDelete" runat="server" OnClick="lnkAdmissionDelete_Click"
                                                                title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 no-padding" runat="server" id="FeeCardTemplate" visible="false">
                                       <%-- <div class="col-sm-3 mgbt-xs-15">
                                            <label class="control-label">Board&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlBoardFeeCard" runat="server" class="form-control-blue validatedrpRe">
                                            </asp:DropDownList>
                                        </div>--%>
                                        <div class="col-sm-4 mgbt-xs-15">
                                            <label class="control-label">Templates&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlTemplateFeeCard" runat="server" class="form-control-blue validatedrpRe" AutoPostBack="true" OnSelectedIndexChanged="ddlTemplateFeeCard_SelectedIndexChanged">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="Template 1">Template 1</asp:ListItem>
                                                <asp:ListItem Value="Template 2">Template 2</asp:ListItem>
                                                <asp:ListItem Value="Template 3">Template 3</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3 mgbt-xs-15 hide" style="margin-top: 30px;">
                                            <asp:CheckBox ID="chkisLockFeeCard" runat="server" class="form-control-blue vd_checkbox checkbox-success" Text="Do you want lock Template list?" />
                                        </div>
                                        <div class="col-sm-4   mgbt-xs-15">
                                            <div class="" style="margin-top: 25px;">
                                                <asp:LinkButton ID="LinkFeeCard" runat="server" CssClass="button form-control-blue" OnClick="LinkFeeCard_Click" OnClientClick="ValidateTextBox('.validatetxtRe');ValidateDropdown('.validatedrpRe');return validationReturn(this);">Submit</asp:LinkButton>
                                                <div id="msgbox4" runat="server" style="left: 74px;"></div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" runat="server" id="linkFeeCards" style="padding-bottom:10px;"> </div>
                                        <div class="col-sm-12">
                                            <asp:GridView ID="GridViewFeeCard" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1s" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Board">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2s" runat="server" Text='<%# Bind("Board") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Default Template">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5s" runat="server" Text='<%# Bind("Text") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Lock" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabeCopys" runat="server" Text='<%# Bind("IsLockList") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>

                                                            <asp:Label ID="LabelFeeCard" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkFeeCard" runat="server" OnClick="lnkFeeCardDelete_Click"
                                                                title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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
                                <h4>Are you sure you want to delete this?</h4>
                                <asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="Button8" runat="server" CausesValidation="False" Text="No" CssClass="button-n" />
                                &nbsp;&nbsp;  
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" CssClass="button-y" />
                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
                <%-- ReSharper disable once Asp.InvalidControlType --%>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button8"
                    BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True"
                    PopupControlID="Panel2" TargetControlID="Button2"></asp:ModalPopupExtender>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2a" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?</h4>
                                <asp:Label ID="lblvalue2a" runat="server" Visible="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="Button1a" runat="server" CausesValidation="False" Text="No" CssClass="button-n" />
                                &nbsp;&nbsp;  
                                <asp:Button ID="Button3a" runat="server" CausesValidation="False" OnClick="btnAdmissionDelete_Click" Text="Yes" CssClass="button-y" />
                            </td>
                        </tr>s
                    </table>

                </asp:Panel>
                <asp:Button ID="Button4a" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="Panel3a_ModalPopupExtender" runat="server" CancelControlID="Button1a"
                    BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True"
                    PopupControlID="Panel2a" TargetControlID="Button4a"></asp:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2Feecard" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?</h4>
                                <asp:Label ID="lblvalue2aFeecard" runat="server" Visible="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="Button1Feecard" runat="server" CausesValidation="False" Text="No" CssClass="button-n" />
                                &nbsp;&nbsp;  
                                <asp:Button ID="Button3Feecard" runat="server" CausesValidation="False" OnClick="btnFeecardDelete_Click" Text="Yes" CssClass="button-y" />
                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:Button ID="Button4aFeecard" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="Panel2Feecard_ModalPopupExtender" runat="server" CancelControlID="Button1Feecard"
                    BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True"
                    PopupControlID="Panel2Feecard" TargetControlID="Button4aFeecard"></asp:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?</h4>
                                <asp:Label ID="lblvalue2s" runat="server" Visible="False"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="Button1s" runat="server" CausesValidation="False" Text="No" CssClass="button-n" />
                                &nbsp;&nbsp;  
                                <asp:Button ID="Button3s" runat="server" CausesValidation="False" OnClick="btnrecDelete_Click" Text="Yes" CssClass="button-y" />
                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:Button ID="Button4s" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="Panel4s_ModalPopupExtender" runat="server" CancelControlID="Button1s"
                    BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True"
                    PopupControlID="Panel3" TargetControlID="Button4s"></asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
