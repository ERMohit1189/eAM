<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SetStudentAttendenceRange.aspx.cs" Inherits="admin_SetStudentAttendenceRange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-lg-2">
                                    <div class="form-group ">
                                        <asp:Label ID="lblClass" runat="server" class="txt-middle-l txt-bold no-padding" Text="Class"></asp:Label>
                                        <div class="controls  mgbt-xs-20">
                                            <asp:DropDownList ID="drpClass" runat="server" OnSelectedIndexChanged="drpClass_SelectedIndexChanged" CssClass="form-control-blue validatedrp"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group ">
                                        <asp:Label ID="Label1" runat="server" class="txt-middle-l txt-bold no-padding" Text="Evaluation"></asp:Label>
                                        <div class="controls  mgbt-xs-20">
                                            <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drpEval_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group ">
                                        <asp:Label ID="Label2" runat="server" class="txt-middle-l txt-bold no-padding" Text="Exam Start Date"></asp:Label>
                                        <div class="controls  mgbt-xs-20">
                                            <asp:TextBox ID="txtDate1" runat="server" CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group ">
                                        <asp:Label ID="Label3" runat="server" class="txt-middle-l txt-bold no-padding" Text="Exam End Date"></asp:Label>
                                        <div class="controls  mgbt-xs-20">
                                            <asp:TextBox ID="txtDate2" runat="server" CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 hide">
                                    <div class="form-group ">
                                        <asp:Label ID="Label4" runat="server" class="txt-middle-l txt-bold no-padding" Text="Attendence count from"></asp:Label>
                                        <div class="col-sm-5 controls no-padding">
                                            <asp:RadioButtonList ID="rbcountStart" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_radio radio-success"
                                                AutoPostBack="True" OnSelectedIndexChanged="rbcountStart_SelectedIndexChanged">
                                                <asp:ListItem Value="FromSession" Selected="True">From School Session Date</asp:ListItem>
                                                <asp:ListItem Value="Manualy">Manualy</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-2 controls  mgbt-xs-20" runat="server" id="divdate" visible="false">
                                            <asp:TextBox ID="txtCountStartDate" runat="server" CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 hide">
                                    <div class="form-group ">
                                        <asp:Label ID="Label5" runat="server" class="col-sm-3  txt-middle-l txt-bold no-padding" Text="New Admission Attendence count from"></asp:Label>
                                        <div class="col-sm-5 controls no-padding">
                                            <asp:CheckBox ID="chkAdmission" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_checkBox checkBox-success" Text="From Date of admission">
                                            </asp:CheckBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <br />
                                    <div class="form-group ">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" OnClick="lnkSubmit_Click"
                                            OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 120px;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12 no-padding">
                                    <br />
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ClassName" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Evaluation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="EvalName" runat="server" Text='<%# Bind("EvalName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Exam Start Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExamStartDate" runat="server" Text='<%# Bind("ExamStartDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Exam End Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExamEndDate" runat="server" Text='<%# Bind("ExamEndDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Attendence count from" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CountStartFrom" runat="server" Text='<%# Bind("CountStartFrom") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Count from Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CountStartDateDate" runat="server" Text='<%# Bind("CountStartDateDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Count from Admission Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CountStartFromAdmission" runat="server" Text='<%# Bind("CountStartFromAdmission") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br />
                                                        (<asp:Label ID="RecordDate" runat="server" Text='<%# Bind("RecordDate") %>'></asp:Label>)
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Labelid" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkEdit_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </div>
                                </div>


                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                        <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always auto-set-height">
                                            <div class="col-sm-12 ">
                                                <table class="tab-popup">
                                                    <tr>
                                                        <td>Exam Start Date <span class="vd_red">*</span>
                                                            <asp:Label ID="lblClassid" runat="server" CssClass="hide"></asp:Label>
                                                            <asp:Label ID="lblEval" runat="server" CssClass="hide"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDate10" runat="server" CssClass="form-control-blue datepicker-normal validatetxt1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Exam End Date <span class="vd_red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDate20" runat="server" CssClass="form-control-blue datepicker-normal validatetxt1"></asp:TextBox>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Attendence count from<span class="vd_red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rbcountStart0" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_radio radio-success"
                                                                AutoPostBack="True" OnSelectedIndexChanged="rbcountStart0_SelectedIndexChanged">
                                                                <asp:ListItem Value="FromSession">From School Session Date</asp:ListItem>
                                                                <asp:ListItem Value="FromAdmission">From Date of admission</asp:ListItem>
                                                                <asp:ListItem Value="Manualy">Manualy</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" visible="false" id="divdate0">
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCountStartDate0" runat="server" CssClass="form-control-blue  datepicker-normal"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>New Admission Attendence count from</td>
                                                        <td>
                                                                <asp:CheckBox ID="chkAdmission0" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_checkBox checkBox-success" Text="From Date of admission">
                                                                </asp:CheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Button ID="Button3" runat="server" CssClass="button-y" OnClick="Button3_Click" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" Text="Update" />
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

