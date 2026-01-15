<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="FeeGroupMaster.aspx.cs" Inherits="admin_FeeGroupMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    
    <%-- ==== in aspx file   --%>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12  ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12   no-padding " id="table" runat="server">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtFeeGroup" runat="server" CssClass="form-control-blue txtBox"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
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

                                <div class="col-sm-12   ">
                                    <div class=" table-responsive  table-responsive2">
                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <HeaderTemplate>
                                                    <tr>
                                                        <th class="vd_bg-blue text-center vd_white" style="width: 40px;">#</th>
                                                        <th class="vd_bg-blue text-center vd_white">Fee Category</th>
                                                        <%--<th class="vd_bg-blue text-center vd_white">Remark</th>--%>
                                                        <th class="vd_bg-blue text-center vd_white" style="width: 40px;">Edit</th>
                                                        <th class="vd_bg-blue text-center vd_white" style="width: 40px;">Delete</th>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("FeeGroupName") %>'></asp:Label></td>
                                                        <%--<td>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Remark") %>'></asp:Label></td>--%>
                                                        <td class="menu-action">

                                                            <asp:LinkButton ID="lnkEdit" runat="server" title="Edit" 
                                                                OnClick="lnkEdit_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </td>
                                                        <td class="menu-action">

                                                            <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" CausesValidation="False"
                                                                title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
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
                            <td>Fee Category<span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFeeGroupPanel" runat="server" CssClass="form-control-blue txtBoxPanel"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="hide">
                            <td>Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="lnkUpdate" runat="server" OnClientClick="return ValidateTextBox('.txtBoxPanel');" CssClass="button-y" OnClick="lnkUpdate_Click">Update</asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n">Cancel</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="Button1" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="lnkCancel" PopupControlID="Panel1"
                        TargetControlID="Button1" BackgroundCssClass="popup_bg" PopupDragHandleControlID="Panel1">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?
                                </h4>
                            </td>
                        </tr>

                        <tr>

                            <td>

                                <asp:LinkButton ID="lnkNo" runat="server" CssClass="button-n">No</asp:LinkButton>
                                &nbsp;&nbsp;  
                                                        <asp:LinkButton ID="lnkDeleteYes" runat="server" CssClass="button-y" OnClick="lnkDeleteYes_Click">Yes</asp:LinkButton>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Style="display: none" />
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" Enabled="True" TargetControlID="Button2"
                    PopupControlID="Panel2" CancelControlID="Button2" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

