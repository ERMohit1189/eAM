<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EntranceExamMaster.aspx.cs" Inherits="_1.AdminEntranceExamMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Exam Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtExamName" onFocus="javascript:this.select();" CssClass="form-control-blue validatetxt" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');"
                                            CssClass="button form-control-blue " OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <div class="table-responsive2  table-responsive">
                                        <asp:HiddenField ID="hfid" runat="server" />

                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <HeaderTemplate>
                                                    <tr>
                                                        <th class="vd_bg-blue vd_white text-center" style="width: 40px;">#</th>
                                                        <th class="vd_bg-blue vd_white ">Exam Name</th>
                                                        <th class="vd_bg-blue vd_white text-center" style="width: 40px;">Edit</th>
                                                        <th class="vd_bg-blue vd_white text-center" style="width: 40px;">Delete</th>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center">
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblExamName" runat="server" Text='<%# Eval("ExamName") %>'></asp:Label></td>
                                                        <td class="text-center menu-action">

                                                            <asp:LinkButton ID="lnkEdit" runat="server" title="Edit" 
                                                                OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </td>
                                                        <td class="text-center menu-action">

                                                            <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click" CausesValidation="False"
                                                                title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
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
                            <td>Exam Name</td>
                            <td>
                                <asp:TextBox ID="txtExamNamePanel" CssClass="form-control-blue validatetxt1" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="lnkUpdate" runat="server" OnClientClick="return ValidateTextBox('.validatetxt1');" CssClass="button-y" OnClick="lnkUpdate_Click">Submit</asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n">Cancel</asp:LinkButton></td>
                        </tr>


                    </table>
                </asp:Panel>
                <asp:LinkButton ID="lnkTargetControl1" runat="server" Style="display: none"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                    BackgroundCssClass="popup_bg" Enabled="True" CancelControlID="lnkCancel"
                    PopupControlID="Panel1" TargetControlID="lnkTargetControl1">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td class="text-center">
                                <h4>Are you sure you want to delete this?</h4>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-center">
                                <asp:LinkButton ID="lnkNo" runat="server" CssClass="button-n">No</asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lnkDeleteyes" runat="server" CssClass="button-y" OnClick="lnkDeleteyes_Click">Yes</asp:LinkButton>
                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:LinkButton ID="lnkTargetControl2" runat="server" Style="display: none"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server"
                    BackgroundCssClass="popup_bg" Enabled="True"
                    PopupControlID="Panel2" TargetControlID="lnkTargetControl2" CancelControlID="lnkNo">
                </ajaxToolkit:ModalPopupExtender>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

