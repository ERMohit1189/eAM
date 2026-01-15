<%@ Control Language="C#" AutoEventWireup="true" CodeFile="attendance.ascx.cs" Inherits="admin_usercontrol_widgets_Wid2" %>
<div class="col-md-12  mgbt-xs-10 dash-b-w">
  <div class="panel widget panel-bd-top  vd_bdt-green vd_todo-widget light-widget">
        <div class="panel-heading no-title ">
            <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-pie-chart"></i></span>
                <asp:LinkButton ID="lnkStdAtt" runat="server" PostBackUrl="~/11/AttendanceReport.aspx?check=StudenceAttendanceDayWiseReport">Today's Student Attendance</asp:LinkButton>
            </h3>
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
                <asp:Chart ID="Chart1" runat="server" BorderlineWidth="0">
                    <Titles>
                        <asp:Title Docking="Bottom" Name="Title1" Text="Subject wise Marks Report" />
                    </Titles>
                    <Legends>
                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                    </Legends>
                    <Series>
                        <asp:Series Name="Default" XValueMember="Marks" YValueMembers="Subjects" IsValueShownAsLabel="false" />
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                    </ChartAreas>
                </asp:Chart>
            </div>

            <div class="col-md-4 col-xs-4 no-padding ">
                <table style="width: 100%" class="mp-table2">

                    <tr class="vd_green">
                        <td class="tab-b-45 " style="color:#1FAE66 !important">Present 
                        </td>
                        <td class="text-center tab-b-10">:
                        </td>
                        <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                            <asp:LinkButton ID="lblStdTotalPresent" ForeColor="#1FAE66" runat="server"
                                PostBackUrl="~/11/AttendanceReport.aspx?Sts=P&check=StudenceAttendanceDayWiseReport"></asp:LinkButton>

                        </td>
                    </tr>
                     <tr class="vd_yellow">
                        <td class="tab-b-45" style="color:#F89C2C !important">Late
                        </td>
                        <td class="text-center tab-b-10">:
                        </td>
                        <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                            <asp:LinkButton ID="lblStdTotalLate" ForeColor="#F89C2C" runat="server"
                                PostBackUrl="~/11/AttendanceReport.aspx?Sts=Lt&check=StudenceAttendanceDayWiseReport"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr class=" vd_red">
                        <td class="tab-b-45 " style="color:#E91E63 !important">Absent 
                        </td>
                        <td class="text-center tab-b-10">:
                        </td>
                        <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                            <asp:LinkButton ID="lblStdTotalAbsent" ForeColor="#E91E63" runat="server"
                                PostBackUrl="~/11/AttendanceReport.aspx?Sts=A&check=StudenceAttendanceDayWiseReport"></asp:LinkButton>
                        </td>
                    </tr>
                   
                    <tr class=" vd_red">
                        <td class="tab-b-45 ">
                            <asp:Label ID="lblNF" runat="server" Text="Not Mark" style="color:#DA4448 !important"></asp:Label>
                        </td>
                        <td class="text-center tab-b-10">:
                        </td>
                        <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                            <asp:LinkButton ID="lblNotFilled" ForeColor="#DA4448" runat="server"
                                PostBackUrl="~/11/AttendanceReport.aspx?Sts=N&check=StudenceAttendanceDayWiseReport"></asp:LinkButton>
                        </td>

                    </tr>
                    
                    <tr class="vd_blue">
                        <td class="tab-b-45" style="color:#23709E !important">Other 
                        </td>
                        <td class="text-center tab-b-10">:
                        </td>
                        <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                            <asp:LinkButton ID="lblStdOther" ForeColor="#23709E" runat="server"
                                PostBackUrl="~/11/AttendanceReport.aspx?Sts=O&check=StudenceAttendanceDayWiseReport"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="vd_info br">
                <h5 class="text-right font-semibold"><strong>Total Students </strong></h5>
                <h3 class="text-center  vd_red font-s-14">
                    <asp:LinkButton ID="lblStdTotalAttendence" runat="server" PostBackUrl="~/11/AttendanceReport.aspx?check=StudenceAttendanceDayWiseReport"></asp:LinkButton>
                </h3>
            </div>
 </div>
        </div>
    </div>
</div>
