<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EntranceDateAllotment.aspx.cs" Inherits="EntranceDateAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                Sys.Application.add_load(datetime);
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">

                                        <label class="control-label">Form Date</label>
                                        <div class="">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control-blue datepicker-normal validatetxtss"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date</label>
                                        <div class="">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-blue datepicker-normal validatetxtss"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  half-width-50" style="padding-top: 5px;">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:LinkButton ID="LinkView" runat="server" OnClientClick="ValidateTextBox('.validatetxtss');ValidateDropdown('.validatedrpss');return validationReturn(this);" class="button form-control-blue" OnClick="LinkView_Click">View</asp:LinkButton>
                                        </div>
                                        <div id="msgbox" runat="server" style="left: 75px"></div>
                                    </div>
                                    <div class=" col-sm-12 " id="divdatabind" runat="server">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-head-border table-bordered">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label31" runat="server" Text='<%# Bind("sno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40px" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receipt No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelRecieptNo" runat="server" Text='<%# Bind("RecieptNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("AdmissionFromDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type of Admission">
                                                        <ItemTemplate>
                                                            <asp:Label ID="AdmissionType" runat="server" Text='<%# Bind("AdmissionType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Student's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label32" runat="server" Text='<%# Bind("StudentNames") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                                            <asp:Label ID="lblbranch" runat="server" Text='<%# Bind("Branch") %>'></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label33" runat="server" Text='<%# Bind("ReceivedAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entrance Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelEntrenceDate" runat="server" Text='<%# Bind("EntrenceDates") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField><asp:TemplateField HeaderText="Entrance Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelEnteranceStatus" runat="server" Text='<%# Bind("EnteranceStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Allot Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkAllotDate" runat="server" title="Allot Test Date" data-toggle="tooltip"
                                                                data-placement="top" OnClick="LinkAllotDate_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="80px" />
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
                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                        <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always auto-set-height">
                            <div class="col-sm-12 ">
                                <table class="tab-popup">
                                    <tr>
                                        <td>Allot Date <span class="vd_red">*</span>
                                        </td>
                                        <td>
                                            <div class="col-sm-6" style="padding: 0;">
                                                <asp:TextBox ID="txtAllotDate" runat="server" CssClass="form-control-blue validatetxt1 datepicker-normal" style="height: 34px !important;"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2" style="padding: 0;">
                                                <asp:DropDownList ID="ddlHH" runat="server" CssClass="form-control-blue validatedrp1">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2" style="padding: 0;">
                                                <asp:DropDownList ID="ddlMM" runat="server" CssClass="form-control-blue validatedrp1">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2" style="padding: 0;">
                                                <asp:DropDownList ID="ddlTT" runat="server" CssClass="form-control-blue">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Status <span class="vd_red">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEntranceStatus" runat="server" CssClass="form-control-blue validatedrp1">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                <asp:ListItem Value="Passed">Passed</asp:ListItem>
                                                <asp:ListItem Value="Failed">Failed</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="Button3" runat="server" CssClass="button-y"  OnClick="Button3_Click" OnClientClick="ValidateDropdown('.validatedrp1');ValidateTextBox('.validatetxt1');return validationReturn();" Text="Update"
                                             />
                                        &nbsp;&nbsp;
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                        <asp:Label ID="lblRecieptNo" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblIDs" runat="server" Visible="False"></asp:Label>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

