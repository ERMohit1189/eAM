<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="FeeHeadCategoryMaster.aspx.cs" Inherits="admin_FeeHeadCategoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
             <script>
                Sys.Application.add_load(tooltip);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding " id="table1" runat="server">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Head Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtFeeHeadCategory" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2  mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="return ValidateTextBox('.txtBox');" CssClass="button form-control-blue" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12  ">
                                    <div class=" table-responsive  table-responsive2">
                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <HeaderTemplate>
                                                    <tr>
                                                        <th class="vd_bg-blue text-center vd_white" style="width: 40px;">#</th>
                                                        <th class="vd_bg-blue text-center vd_white">Fee Head Category</th>
                                                        <th class="vd_bg-blue text-center vd_white">Remark</th>
                                                        <th class="vd_bg-blue text-center vd_white" style="width: 40px;">Edit</th>
                                                        <th class="vd_bg-blue text-center vd_white" style="width: 40px;">Delete</th>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label><%# Container.ItemIndex+1 %></td>
                                                        <td><%# Eval("FeeHeadCategory") %></td>
                                                        <td><%# Eval("Remark") %></td>
                                                        <td class="menu-action">

                                                            <asp:LinkButton ID="lnkEdit" runat="server" title="Edit" data-toggle="tooltip" data-placement="top"
                                                                OnClick="lnkEdit_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>

                                                        </td>
                                                        <td class="menu-action">
                                                            <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" CausesValidation="False"
                                                                title="Delete" data-toggle="tooltip" data-placement="top" class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>Fee Head Category
                            </td>
                            <td>
                                <asp:TextBox ID="txtFeeHeadCategoryPanel" runat="server" CssClass="form-control-blue mgbt-xs-15"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>Remark</td>
                            <td>
                                <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="lnkUpdate" CssClass="button-y" OnClientClick="return ValidateTextBox('.txtBoxPanel');" runat="server" OnClick="lnkUpdate_Click">Submit</asp:LinkButton>
                                &nbsp;&nbsp;
                        <asp:LinkButton ID="lnkCancel" CssClass="button-n" runat="server">Cancel</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button1" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" TargetControlID="Button1"
                    CancelControlID="lnkCancel" PopupControlID="Panel1" runat="server" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?</h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:LinkButton ID="lnkNo" CssClass="button-n" runat="server">No</asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lnkYes" CssClass="button-y" runat="server" OnClick="lnkYes_Click">Yes</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" TargetControlID="Button2"
                    CancelControlID="lnkNo" PopupControlID="Panel2" runat="server" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

