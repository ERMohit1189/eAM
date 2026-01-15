<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PeriodAllotToStaff.aspx.cs" Inherits="PeriodAllotToStaff" %>

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
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlBranch" CssClass="form-control-blue validatedrp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSection" CssClass="form-control-blue validatedrp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlMedium" CssClass="form-control-blue validatedrp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSubject" CssClass="form-control-blue validatedrp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlPaper" CssClass="form-control-blue validatedrp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaper_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Periods&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlPeriod" CssClass="form-control-blue validatedrp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Staff&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlStaff" CssClass="form-control-blue validatedrp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg" style="color:red" runat="server" id="div_isClassteacher"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Is Class Teacher&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:RadioButtonList ID="chkIsClassTeacher" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-9  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:Button ID="btnInsert" runat="server" Visible="false" CssClass="button form-control-blue" OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();" Text="Submit" OnClick="btnInsert_Click" />
                                        <div id="msgbox" runat="server"></div>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <div class="table-responsive table-responsive2">
                                            <asp:GridView ID="gvTimeTableRule" runat="server" CssClass="table table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="false">
                                                <Columns>
                                                     <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="sno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ClassName" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                                                            <asp:Label ID="classid" runat="server" Text='<%# Eval("classid") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="BranchId" runat="server" Text='<%# Eval("BranchId") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="SectionId" runat="server" Text='<%# Eval("SectionId") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Medium">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Medium" runat="server" Text='<%# Eval("Medium") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SubjectName" runat="server" Text='<%# Eval("SubjectName") %>'></asp:Label>
                                                            <asp:Label ID="SubjectId" runat="server" Text='<%# Eval("SubjectId") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Paper">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("PaperName") %>'></asp:Label>
                                                            <asp:Label ID="PaperId" runat="server" Text='<%# Eval("PaperId") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Period">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Period" runat="server" Text='<%# Eval("Period") %>'></asp:Label>
                                                            <asp:Label ID="PeriodId" runat="server" Text='<%# Eval("PeriodId") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Duration">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Deuration" runat="server" Text='<%# Eval("Deuration") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Teacher">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                            &nbsp(<asp:Label ID="EmpCode" runat="server" Text='<%# Eval("EmpCode") %>'></asp:Label>)
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Class Teacher">
                                                        <ItemTemplate>
                                                            <asp:Label ID="IsClassTeacher" runat="server" Text='<%# Eval("IsClassTeacher") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Username">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LoginName" runat="server" Text='<%# Eval("LoginName") %>'></asp:Label><br />
                                                            (<asp:Label ID="RecordedDate" runat="server" Text='<%# Eval("RecordedDate") %>'></asp:Label>)
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
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
                                                            <asp:Label ID="Label37" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" title="Delete" data-toggle="tooltip" data-placement="top" class="btn menu-icon vd_bd-red vd_red">
                                                                <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
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
                                                            <td>Staff <span class="vd_red">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlStaffEdit" CssClass="form-control-blue validatedrp1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStaffEdit_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr class="hide">
                                                            <td>Is Class Teacher <span class="vd_red">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="chkIsClassTeacherEdit" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" runat="server">
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Button ID="Button3" runat="server" CssClass="button-y" OnClick="Button3_Click" OnClientClick="ValidateDropdown('.validatedrp1');ValidateTextBox('.validatetxt1');return validationReturn();" Text="Update" />
                                                                &nbsp;&nbsp;
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                                <asp:Label ID="classidEdit" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="BranchIdEdit" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="SubjectIdEdit" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="PaperIdEdit" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="PeriodIdEdit" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="EmpCodeOldEdit" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="SectionIdEdit" runat="server" Visible="false"></asp:Label>
                                                                <div id="msgbox2" runat="server"></div>
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
                                                    <td style="text-align: center;" height="50">
                                                        <h4>Do you really want to delete this record?
                                                            <asp:Label ID="classidDelete" runat="server" Visible="False"></asp:Label>
                                                            <asp:Label ID="PeriodIdDelete" runat="server" Visible="False"></asp:Label>
                                                            <asp:Label ID="EmpCodeDelete" runat="server" Visible="False"></asp:Label>
                                                            <asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                                            <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                                        </h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;">
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

