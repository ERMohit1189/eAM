<%@ Control Language="C#" AutoEventWireup="true" CodeFile="staffattendance.ascx.cs" Inherits="admin_usercontrol_widgets_Wid3" %>
<div class="col-md-4  mgbt-xs-10 dash-b-w">
    <div class="panel widget panel-bd-top vd_bdt-yellow vd_todo-widget light-widget">
        <div class="panel-heading no-title ">
            <h3 class="panel-title"><span class="menu-icon"><i class=" fa  fa-user"></i></span>
                <asp:LinkButton ID="lnkStdAtt" runat="server" PostBackUrl="~/8/EmpAttendanceReport.aspx?print=1">Staff Attendance</asp:LinkButton></h3>
            <div class="vd_panel-menu">
                <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                <div data-action="refresh" title="Refresh" data-placement="bottom" class=" menu entypo-icon smaller-font"><i class="icon-cycle"></i></div>
                <div data-action="close" title="Close" data-placement="bottom" class=" menu entypo-icon"><i class="icon-cross"></i></div>
            </div>
        </div>
        <div class="panel-body no-padding">
            <!-- vd_panel-menu -->
            <div class=" pad-lr-15-1024-260">
                <div class="col-md-8 col-xs-8 no-padding ">
                    <asp:Chart ID="Chart2" runat="server">
                        <Titles>
                            <asp:Title ShadowOffset="6" Name="Title1" />
                        </Titles>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom"
                                 Name="Default"
                                LegendStyle="Row" />
                        </Legends>
                        <Series>
                            <asp:Series Name="Default"/>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"
                                BorderWidth="1" />
                        </ChartAreas>
                    </asp:Chart>
                </div>

                <div class="col-md-4 col-xs-4 no-padding ">
                    <table style="width: 100%" class="mp-table2">

                        <tr class="vd_green">
                            <td class="tab-b-45" style="color:#1FAE66 !important">Present 
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                                <asp:LinkButton ID="lblEmpTotalPresent" ForeColor="#1FAE66" runat="server" PostBackUrl="~/8/EmpAttendanceReport.aspx?Sts=P&print=1"></asp:LinkButton>
                        </tr>
                        <tr class="vd_yellow">
                            <td class="tab-b-45 " style="color:#F89C2C !important">Late 
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                                <asp:LinkButton ID="lblEmpTotalLate" ForeColor="#F89C2C" runat="server" PostBackUrl="~/8/EmpAttendanceReport.aspx?Sts=LT&print=1"></asp:LinkButton>
                        </tr>
                        <tr class=" vd_red">
                            <td class="tab-b-45 " style="color:#E91E63 !important">Absent 
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                                <asp:LinkButton ID="lblEmpTotalAbsent" ForeColor="#E91E63" runat="server" PostBackUrl="~/8/EmpAttendanceReport.aspx?Sts=A&print=1"></asp:LinkButton>
                        </tr>
                        
                        <tr class=" vd_red">
                            <td class="tab-b-45 " style="color:#2196f3 !important">
                                Not Mark
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                                <%-- ReSharper disable once Asp.Entity --%>
                                <asp:LinkButton ID="lblNotFilled" ForeColor="#2196f3" runat="server" PostBackUrl="~/8/EmpAttendanceReport.aspx?Sts=N&print=1"></asp:LinkButton>
                            </td>

                        </tr>
                        <tr class="vd_blue">
                            <td class="tab-b-45" style="color:#673ab7 !important">Half Day 
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                                <asp:LinkButton ID="lblEmpHD" ForeColor="#673ab7" runat="server" PostBackUrl="~/8/EmpAttendanceReport.aspx?Sts=HD&print=1"></asp:LinkButton>
                        </tr>
                        <tr class="vd_blue">
                            <td class="tab-b-45" style="color:#000 !important">Short Leave 
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                                <asp:LinkButton ID="lblEmpSL" ForeColor="#000" runat="server" PostBackUrl="~/8/EmpAttendanceReport.aspx?Sts=SL&print=1"></asp:LinkButton>
                        </tr>

                    </table>
                </div>

                <div class="vd_info br">
                    <h5 class="text-right font-semibold"><strong>Total Staff </strong></h5>
                    <h3 class="text-center font-s-14 vd_red">
                    <asp:LinkButton ID="lblTotalAttendence" runat="server" PostBackUrl="~/8/EmpAttendanceReport.aspx?Sts=-1&print=1"></asp:LinkButton></h3>
                </div>
            </div>
        </div>
    </div>
</div>
