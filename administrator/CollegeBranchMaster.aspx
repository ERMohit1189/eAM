<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/administrato_root-manager.master" AutoEventWireup="true" CodeFile="CollegeBranchMaster.aspx.cs"
    Inherits="SuperAdmin_CollegeBranchMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>


    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-lg-12 no-padding">
                                    <div class="col-sm-3   half-width-50 mgbt-xs-9">
                                        <asp:Label ID="Label1" runat="server" class="control-label" Text="Branch Name"></asp:Label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtCaste" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3   half-width-50 mgbt-xs-9">
                                        <asp:Label ID="Label4" runat="server" class="control-label" Text="Branch Code"></asp:Label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtShortcode" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3   half-width-50 mgbt-xs-9">
                                        <asp:Label ID="Labels2" runat="server" class="control-label" Text="Remark"></asp:Label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button">Submit</asp:LinkButton>
                                         <div id="msgbox" runat="server" style="left: 75px;"></div>

                                    </div>
                                   
                                </div>

                                <div class="col-lg-12 no-padding">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branch Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branch Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2s" runat="server" Text='<%# Bind("ShortCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3s" runat="server" Text='<%# Bind("BranchRemark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("BranchId") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit " 
                                                            OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("BranchId") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
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
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup text-center">

                        <tr>
                            <td>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="Button7" runat="server" Style="display: none" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" />

                                &nbsp; &nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="button-y" Text="Yes" OnClick="btnDelete_Click" CausesValidation="False" />

                            </td>
                        </tr>



                    </table>

                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
            </div>
            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup">

                        <tr>
                            <td align="right" width="35%">Branch Name
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPanelCategory" runat="server" CssClass="form-control-blue validatetxt1" Width="200px"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnPanelCategory" />
                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="LinkButton5" PopupControlID="Panel1"
                                    TargetControlID="Button5" BackgroundCssClass="popup_bg">
                                </asp:ModalPopupExtender>
                            </td>
                        </tr>
                         <tr>
                            <td align="right" width="35%">Branch Code
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtShortcodePanel" runat="server" CssClass="form-control-blue validatetxt1" Enabled="false" Width="200px"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnShortcodePanel" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top !important">Remark&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRemarkPanel" runat="server" CssClass="form-control-blue" TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;
                            </td>
                            <td align="left" style="padding-left: 5px;">
                                <asp:LinkButton ID="LinkButton4" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" OnClick="LinkButton4_Click" CssClass="button-y">Update</asp:LinkButton>
                                &nbsp;
                                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" CssClass="button-n">Cancel</asp:LinkButton>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
