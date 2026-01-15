<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmpAttendanceAtGlance.ascx.cs" Inherits="staff_userControl_EmpAttendanceAtGlance" %>

<div class="mgbt-xs-10 col-md-8 sp-d-box1">
    <div clss="row">
        <div class="panel widget panel-bd-top vd_bdt-grey vd_todo-widget light-widget ">
            <div class="panel-heading no-title">
                <h3 class="panel-title text-center"><span class="menu-icon"><i class="fa fa-clock-o"></i></span>
                    <a href="8/EmpWiseAttendanceReport.aspx" target="_blank">Attendance at a Glance</a>
                </h3>
            </div>
            <div class="panel-body">
                <div class=" no-padding mgtp-10">
                    <table class="table pro-tab no-bm  table-hover">
                        <tbody>
                            <tr>
                                <td>
                                    <div class="pro-content-box">
                                        <asp:Label ID="lblTodayDate" runat="server" Text=""></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="pro-content-box">
                                        <asp:Label ID="lblTodayIn" runat="server" Text=""></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="pro-content-box">
                                        <asp:Label ID="lblTodayOut" runat="server" Text=""></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="pro-content-box">
                                        <asp:Label ID="lblTodayAttendance" runat="server" Text=""></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>