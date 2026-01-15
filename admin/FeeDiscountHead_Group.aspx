<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="FeeDiscountHead_Group.aspx.cs" Inherits="admin_FeeDiscountHead_Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-6  ">
                                    Group Name
                                    <asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-6  ">
                                    <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click"
                                        CssClass="button" OnClientClick="return checkEmptyTextbox();">Submit</asp:LinkButton>
                                    <div id="msgbox" runat="server"></div>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table table-striped table-hover no-bm no-head-border table-bordered text-center pro-table">
                                        <asp:Repeater ID="rpt" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Group Name</th>
                                                    <th>Edit</th>
                                                    <th>Delete</th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Container.ItemIndex+1 %>
                                                    </td>
                                                    <td id="GroupName" runat="server">
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                        <%# Eval("GroupName") %>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" title="Edit City" 
                                                            OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red">
                                                            <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
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


                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                        <table class="tab-popup">
                            <tr>
                                <td>Group Name <span class="vd_red">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGroupNamePanel" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="text-center">
                                    <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button-y " OnClick="lnkUpdate_Click">Update</asp:LinkButton>
                                    &nbsp;&nbsp;
                                <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n ">Cancel</asp:LinkButton>
                                    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:LinkButton ID="lnk1" runat="server" style="display:none"></asp:LinkButton>
                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="lnk1" PopupControlID="Panel1"
                        CancelControlID="lnkCancel" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </div>

                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                        <table class="tab-popup text-center">
                            <tr>
                                <td class="text-center">
                                    <h4>Do you really want to delete this record?
                                    <asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                        <asp:LinkButton ID="Button7" runat="server" Style="display: none"></asp:LinkButton>
                                    </h4>
                                </td>
                            </tr>

                            <tr>
                                <td class="text-center">
                                    <asp:LinkButton ID="lnkNo" runat="server" CssClass="button-n">No</asp:LinkButton>
                                    &nbsp; &nbsp;
                                <asp:LinkButton ID="lnkYes" runat="server" CssClass="button-y" OnClick="lnkYes_Click">Yes</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" Enabled="True" TargetControlID="Button7"
                        PopupControlID="Panel2" CancelControlID="lnkNo" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </div>

                <script>
                    function checkEmptyTextbox() {
                        var flag = true;
                        var focuson = 0;

                        var txtBox = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_txtGroupName');

                        if (txtBox.value === "") {

                            txtBox.style.borderColor = "#da4448";
                            txtBox.focus();
                            flag = false;
                        }
                        else {
                            txtBox.style.borderColor = "#D5D5D5;";
                        }
                        return flag;
                    }
                </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

