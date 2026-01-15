<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PeriodMaster.aspx.cs" Inherits="PeriodMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
   <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>   
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Period Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtPeriod" CssClass="form-control-blue validatetxt" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Period Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlPeriodType" CssClass="form-control-blue" runat="server">
                                                 <asp:ListItem>Assembly</asp:ListItem>
                                            <asp:ListItem>Zero Period</asp:ListItem>
                                            <asp:ListItem Selected="True">Normal Period</asp:ListItem>
                                            <asp:ListItem>Lunch Break</asp:ListItem>
                                            <asp:ListItem>Diary Period</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-6  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:Button ID="btnInsert" runat="server" CssClass="button form-control-blue"  OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();"  Text="Submit" OnClick="btnInsert_Click" />
                                        <div id="msgbox" runat="server"></div>
                                    </div>





                                    <div class="col-sm-12 ">
                                        <div class="table-responsive table-responsive2">
                                            <asp:GridView ID="gvTimeTableRule" runat="server" CssClass="table table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsr" runat="server" Text='<%# Container.DataItemIndex+1 %>' style="padding-left:10px;"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Period">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelPeriod" runat="server" Text='<%# Bind("Period") %>' style="padding-left:10px;"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Period Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PeriodType" runat="server" Text='<%# Bind("PeriodType") %>' style="padding-left:10px;"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" data-toggle="tooltip" data-placement="top"
                                                            OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label37" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" title="Delete" data-toggle="tooltip" data-placement="top" class="btn menu-icon vd_bd-red vd_red">
                                                                <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"  Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always auto-set-height">
                        <div class="col-sm-12 ">
                            <table class="tab-popup">
                                <tr>
                                    <td>Period <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPeriod0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Period Type <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPeriodType0" CssClass="form-control-blue" runat="server">
                                            <asp:ListItem>Assembly</asp:ListItem>
                                            <asp:ListItem>Zero Period</asp:ListItem>
                                            <asp:ListItem Selected="True">Normal Period</asp:ListItem>
                                            <asp:ListItem>Lunch Break</asp:ListItem>
                                            <asp:ListItem>Diary Period</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="Button3" runat="server" CssClass="button-y" OnClick="Button3_Click" OnClientClick="ValidateDropdown('.validatedrp1');ValidateTextBox('.validatetxt1');return validationReturn();" Text="Update" />
                                        &nbsp;&nbsp;
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button4" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>
                                    <div style="overflow: auto; width: 2px; height: 1px">
                                        <asp:Panel ID="pnlDelete" runat="server" CssClass="popup animated2 fadeInDown">
                                            <table class="tab-popup text-center">
                                                <tr>
                                                    <td style="text-align:center;" height="50">
                                                        <h4>Do you really want to delete this record?<asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                                            <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                                        </h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:center;">
                                                        <asp:Button ID="btnNo" runat="server" CausesValidation="False" CssClass="button-n" Text="No" OnClick="btnNo_Click" />
                                                        &nbsp; &nbsp;
                                                        <asp:Button ID="btnYes" runat="server" CausesValidation="False" CssClass="button-y" Text="Yes" OnClick="btnYes_Click" />


                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                                                Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                                            </asp:ModalPopupExtender>
                                        </asp:Panel>


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

