<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="OptSubjectMngnt.aspx.cs" Inherits="admin_Subject_OptSubjectMngnt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Optional Subject Management</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
             <script>
                Sys.Application.add_load(tooltip);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding " id="tblInsert" runat="server">
                                    <div class="col-sm-12  no-padding ">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlClass" CssClass="form-control-blue" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Stream</label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlClassBranch" CssClass="form-control-blue" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClassBranch_SelectedIndexChanged"></asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Section</label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlClassSection" CssClass="form-control-blue" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClassSection_SelectedIndexChanged"></asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Optional Subject Group</label>
                                            <div class="">
                                                <asp:DropDownList CssClass="form-control-blue" ID="ddlOptionalSubject" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOptionalSubject_SelectedIndexChanged"></asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-4  " id="relsubgroup" runat="server" visible="false">
                                            <label class="control-label">Relative Optional Subject Group</label>
                                            <div class="txt-middle">
                                                <asp:CheckBoxList ID="cblCorrespondOptnSubject" RepeatDirection="Horizontal" CssClass="vd_checkbox checkbox-success" RepeatLayout="Flow" runat="server"></asp:CheckBoxList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2  mgbt-xs-15">
                                        <asp:Button ID="btnInsert" runat="server" CssClass="button" Text="Submit" OnClick="btnInsert_Click" />
                                        <asp:Button ID="btnUpdate" runat="server" CssClass="button" Text="Update" OnClick="btnUpdate_Click1" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="button" Text="Reset" OnClick="btnReset_Click" />
                                        <div id="msgbox" runat="server" style="left: 125px;"></div>
                                    </div>
                                    </div>

                                    

                                    <div class="col-sm-12  ">

                                        <div class="table-responsive  table-responsive2">
                                            <asp:GridView ID="gvOptnSubject" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered ">
                                                <Columns>
                                                    <asp:BoundField DataField="SrNo" HeaderText="#" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" ItemStyle-CssClass="text-center" HeaderStyle-Width="40px" />
                                                    <asp:BoundField DataField="ClassName" HeaderText="Class" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" ItemStyle-CssClass="text-center" />
                                                    <asp:BoundField DataField="SectionName" HeaderText="Section" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" ItemStyle-CssClass="text-center" />
                                                    <asp:BoundField DataField="BranchName" HeaderText="Stream" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" ItemStyle-CssClass="text-center" />
                                                    <asp:BoundField DataField="SubjectGroupMaster" HeaderText="Optional Subject Group" HeaderStyle-CssClass="vd_bg-blue vd_white text-left" ItemStyle-CssClass="text-left" />
                                                    <asp:BoundField DataField="SubjectGroupMaster_Correspond" HeaderText="Relative Subject Group" HeaderStyle-CssClass="vd_bg-blue vd_white text-left" ItemStyle-CssClass="text-left" />
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" Text='<%# Eval("SubjectGroupMasterID") %>' CssClass="edit" Font-Size="0pt" Height="16px" Width="16px"
                                                                 Visible="false" OnClick="lbtnEdit_Click"></asp:LinkButton>
                                                            <%--<asp:LinkButton ID="lbtnDelete" runat="server" Text='<%# Eval("SubjectGroupMasterID") %>'  OnClick="lbtnDelete_Click"></asp:LinkButton>--%>
                                                            <asp:Label ID="Label37" runat="server" Text='<%# Eval("SubjectGroupMasterID") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" title="Delete" data-toggle="tooltip" CausesValidation="False"
                                                                data-placement="top" class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
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




            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="pnlDelete" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                                <asp:Button ID="btnNo" runat="server" CssClass="button-n" CausesValidation="False" Text="No" OnClick="btnNo_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnYes" runat="server" CssClass="button-y" CausesValidation="False" Text="Yes" OnClick="btnYes_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                        Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

