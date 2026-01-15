<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="GuardianLoginDetailsPrint.aspx.cs"
    Inherits="SuperAdmin_StaffLoginDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Course&nbsp;</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlCourse" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlClass" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSection"  runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Stream</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlBranch"  runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtNote"  runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8  half-width-50 btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="button" OnClick="lnkView_Click">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 92px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding">
                                    <div style="float: right; font-size: 19px;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="false">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                    title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                    title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                    title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="ImageButton1" />
                                                <asp:PostBackTrigger ControlID="ImageButton2" />
                                                <asp:PostBackTrigger ControlID="ImageButton3" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-12 " id="divExport" runat="server">
                                    <div class="table-responsive2 table-responsive">
                                        <table class="table no-bm no-head-border pro-table table-header-group">
                                            <asp:Repeater ID="rptLoginDetails" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <table class="table no-bm no-head-border pro-table table-header-group">
                                                                <tr>
                                                                    <%--<td>S.No.: <%# Container.ItemIndex+1 %></td>--%>
                                                                    <td>S.R. No.:
                                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label></td>
                                                                    <td>Student's Name:
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label></td>
                                                                    <td>Class:
                                                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                            
                                                                    </td>

                                                                </tr>
                                                                <tr>

                                                                    <td colspan="3">Website:
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Website") %>'></asp:Label>
                                                                        and click on Parent Portal.</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Username:
                                                                    <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("UserName") %>'></asp:Label></td>
                                                                    <td colspan="2">Password:
                                                                    <asp:Label ID="lblPassword" runat="server" Text='<%# Bind("Password") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2"><asp:Label ID="lblNote" runat="server"></asp:Label></tdc>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <div class=" col-lg-12 no-padding print-row">
                                                                            <div class="cut-line-box">
                                                                                <h4><i class="fa fa-scissors"></i></h4>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
