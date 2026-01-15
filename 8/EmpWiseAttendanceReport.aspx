<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmpWiseAttendanceReport.aspx.cs" Inherits="_8_EmpWiseAttendanceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" Runat="Server">
    <div class="row">
    <div class="col-md-12">
        <div class="panel widget panel-bd-top vd_bdt-grey vd_todo-widget light-widget">
            <div class="panel-heading"></div>
            <div class="panel-body">
                <div class=" no-padding mgtp-10">
                    <%--<div class="col-sm-5">
                        <div class="col-md-2">
                            <strong>Year:&nbsp;</strong>
                        </div>
                        <div class="col-md-10">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                            <div class="text-box-msg">
                            </div>
                        </div>
                    </div>--%>
                    <div class="col-sm-6">
                        <div class="col-md-2">
                            <strong>Month:&nbsp;</strong>
                        </div>
                        <div class="col-md-10">
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                            <div class="text-box-msg">
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2 mgbt-xs-10">
                        <div class="">
                            <asp:LinkButton ID="lnkView" runat="server" CssClass="button" OnClick="lnkView_OnClick">View</asp:LinkButton>
                            <div class="text-box-msg">
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="pro-content-box text-center" style="padding:10px;font-weight:bolder;">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="table-responsive table-responsive2">
                            <div id="divExport" runat="server">
                                <table class="table table-striped table-hover no-bm no-head-border table-bordered table-header-group text-left">
                                    <tr>
                                        <th>#</th>
                                        <th>Date</th>
                                        <th>Day</th>
                                        <th>In</th>
                                        <th>Out</th>
                                        <th>Attendance</th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptPunch" OnItemDataBound="rptPunch_OnItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.ItemIndex+1 %></td>
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Day") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblPunchInTime" runat="server" Text='<%# Eval("PunchInTime") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblPunchOutTime" runat="server" Text='<%# Eval("PunchOutTime") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblSavedAttendence" runat="server" Text='<%# Eval("SavedAttendence") %>'  Font-Bold="True"></asp:Label></td>
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
</div>
</asp:Content>

