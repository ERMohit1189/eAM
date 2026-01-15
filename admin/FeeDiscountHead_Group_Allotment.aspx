<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="FeeDiscountHead_Group_Allotment.aspx.cs" Inherits="admin_FeeDiscountHead_Group_Allotment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        .hideborder {
            border-top: 0px !important;
            border-left: 0px !important;
            border-right: 0px !important;
        }
    </style>
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
                                    <fieldset>
                                        <legend>Select Group Name<asp:DropDownList ID="drpGroupName" runat="server"></asp:DropDownList></legend>

                                        <table id="tblFeeDiscountHead" class="table table-striped table-hover no-bm no-head-border table-bordered text-center pro-table">
                                            <asp:Repeater ID="rptFeeDiscountHead" runat="server">
                                                <HeaderTemplate>
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkAll" runat="server" Text="Select All" onclick="checkAll(this);" Style="float: left;" /></th>
                                                        <th>Fee Discount Head</th>
                                                        <th>Preference</th>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkFeeDiscountHead" runat="server" onclick="uncheckAll(this);" Style="float: left;" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblFeeDiscountHead" runat="server" Text='<%# Eval("FeeHead") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPreference" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>

                                    </fieldset>
                                </div>
                                <div class="col-sm-6  " style="position: relative; min-height: 250px;">
                                    <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click" 
                                        CssClass="button" OnClientClick="return checkEmptyTextbox();"
                                        Style="position: absolute; bottom: 0px; left: 0;">Submit</asp:LinkButton>
                                    <div id="msgbox" runat="server"></div>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table table-striped table-hover no-bm no-head-border table-bordered text-center pro-table">
                                        <asp:Repeater ID="rpt" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Group Name</th>
                                                    <th>Fee Discount Head</th>
                                                    <th>Preference</th>
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
                                                        <%# Eval("GroupName") %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblFeeDiscountHead" runat="server" Text='<%# Eval("FeeHead") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Preference") %>
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
                                    <asp:Label ID="lblGroupNamePanel" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Fee Discount Head <span class="vd_red">*</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblFeeDiscountHeadPanel" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Preference <span class="vd_red">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPreferencePanel" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="Button5" runat="server" Style="display: none"></asp:LinkButton>
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
                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
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
                    function checkAll(chk) {
                        var input = document.getElementById('tblFeeDiscountHead').getElementsByTagName('input');
                        if (chk.checked) {
                            for (var i = 1; i < input.length; i++) {
                                if (input[i].type === "checkbox") {
                                    input[i].checked = "Checked";
                                }
                            }
                        }
                        else {
                            for (var i = 1; i < input.length; i++) {
                                if (input[i].type === "checkbox") {
                                    input[i].checked = "";
                                }
                            }
                        }
                    }

                    function uncheckAll(chk) {
                        var input = document.getElementById('tblFeeDiscountHead').getElementsByTagName('input');
                        if (chk.checked === false) {
                            input[0].checked = "";

                        }
                    }


                    function checkEmptyTextbox() {
                        var flag = true;
                        var focuson = 0;
                        var input = document.getElementById('tblFeeDiscountHead').getElementsByTagName('input');
                    
                            for (var i = 1; i < input.length; i++) {
                                if (input[i].type === "checkbox") {
                                    if (input[i].checked) {
                                        if (input[i + 1].type === "text") {
                                            if (input[i + 1].value === "") {
                                                if (focuson === 0) {
                                                    focuson = i + 1;
                                                }
                                                input[i + 1].style.borderColor = "#da4448";
                                                flag = false;
                                            }
                                        }
                                    }
                                    else {
                                        if (input[i + 1].type === "text") {
                                            input[i + 1].style.borderColor = "#D5D5D5;";
                                        }
                                    }
                                }
                            }

                            input[focuson].focus();
                       
                        return flag;
                    }
                </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

