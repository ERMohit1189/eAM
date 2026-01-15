<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ReriodAllotmentReport.aspx.cs" Inherits="ReriodAllotmentReport" %>

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
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlBranch" CssClass="form-control-blue" runat="server">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSection" CssClass="form-control-blue" runat="server">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:Button ID="btnInsert" runat="server" CssClass="button form-control-blue" OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();" Text="View" OnClick="btnViewt_Click" />
                                        <div id="msgbox" runat="server"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12 mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server" visible="false">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                    title="Export to Word" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                    title="Export to Excel" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                    title="Export to PDF" data-toggle="tooltip" data-placement="top"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print" data-toggle="tooltip" data-placement="top"><i class="fa fa-print "></i></asp:LinkButton>

                                                <script>
                                                    Sys.Application.add_load(tooltip);
                                                </script>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ImageButton1" />
                                            <asp:PostBackTrigger ControlID="ImageButton2" />
                                            <asp:PostBackTrigger ControlID="ImageButton3" />
                                            <asp:PostBackTrigger ControlID="ImageButton4" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-12">
                                    <div class="table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div id="gdv1" runat="server">
                                                    <table align="center" id="abc" runat="server" visible="false" width="100%" class="table no-p-b-table">
                                                        <tr>
                                                            <td>
                                                                <div id="header" runat="server" class="col-md-12 no-padding"></div>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                    <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                    <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                    <asp:GridView ID="Grid" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-head-border table-bordered">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label3dd" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Class">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LabelClass" runat="server" Text='<%#Eval("class") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Medium">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LabelMedium" runat="server" Text='<%#Eval("Medium") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Subject (Code)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LabelSubjectName" runat="server" Text='<%#Eval("SubjectName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Paper (Code)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LabelPaper" runat="server" Text='<%#Eval("PaperName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Period">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Period" runat="server" Text='<%#Eval("Period") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Staff">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="EmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Duration">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="duration" runat="server" Text='<%#Eval("duration") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Username">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LoginName" runat="server" Text='<%#Eval("LoginName") %>'></asp:Label><br />
                                                                                    (<asp:Label ID="RecordedDate" runat="server" Text='<%#Eval("RecordedDate") %>'></asp:Label>)
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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

