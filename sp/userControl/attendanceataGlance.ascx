<%@ Control Language="C#" AutoEventWireup="true" CodeFile="attendanceataGlance.ascx.cs" Inherits="sp_userControl_attendanceataGlance" %>
<div class="col-sm-12" id="div11" runat="server">
     <div class="panel widget panel-bd-top vd_bdt-grey vd_todo-widget light-widget ">
        <div class="panel-heading no-title">
              <h3 class="panel-title"><a href="MonthwiseStudentAttendenceReport.aspx" target="_blank"><span class="menu-icon"><i class="fa fa-address-book"></i></span>Attendance at a Glance</a></h3>
            <div class="vd_panel-menu">
                <div data-action="minimize" title="Minimize" data-placement="bottom"
                    class=" menu entypo-icon">
                    <i class="icon-minus3"></i>
                </div>
                <div data-action="refresh" title="Refresh" data-placement="bottom"
                    class=" menu entypo-icon smaller-font">
                    <i class="icon-cycle"></i>
                </div>
                <div data-action="close" title="Close" data-placement="bottom" class=" menu entypo-icon"><i class="icon-cross"></i></div>
            </div>
        </div>
        <div class="panel-body">
            <!-- vd_panel-menu -->
            <div class=" no-padding">
                <div class="col-sm-6 " id="todayAtt" runat="server">                    
                    <fieldset>
                        <legend>Today</legend> 
                         <span style="font-size: 11px;" id="viewInTime" runat="server" visible="false">IN:</span><asp:Label ID="lblInTime" runat="server" Text="" style="font-size: 11px;"></asp:Label>     
                         <span style="font-size: 11px;" id="viewOutTime" runat="server" visible="false">|OUT:</span><asp:Label ID="lblOutTime" runat="server" Text="" style="font-size: 11px;"></asp:Label>
                        <span style="font-size: 11px;" id="viewLine" runat="server" visible="false">|</span> 
                        <span style="font-size: 11px;">Attendance:</span>
                         <asp:Label ID="lblAbsent" runat="server" Text="" style="font-size: 11px;"></asp:Label>
                         <asp:Label ID="lblTodaydate" runat="server" Text="" style="font-size: 11px;"></asp:Label>
                    </fieldset>
                </div>
                <div class="col-sm-6  tab-red" id="divlastAb" runat="server">
                    <fieldset>
                        <legend>Last Absent</legend>
                        <span>
                            <asp:Label ID="lbllastAbsentDate" runat="server" style="font-size: 12px;"></asp:Label>
                            <asp:Label ID="lblLastDayName" runat="server" style="font-size: 12px;"></asp:Label></span>
                        <div class="col-md-12  no-padding " id="divreason" runat="server">
                            <asp:Label ID="lblReason" runat="server" style="font-size: 12px;"></asp:Label>
                        </div>
                    </fieldset>
                </div>


                <div class="col-sm-12  tab-green" id="divoverAllAtt" runat="server">
                    <fieldset>
                        <legend>Graphical Presentation 
                            <asp:Label ID="lblheading" runat="server" Text=""></asp:Label></legend>
                        <div class="col-md-8 no-padding ">
                            <div class="set-box-in-center img-box-150" style="width: 150px; height: 150px;">
                                <asp:Chart ID="Chart1" runat="server">
                                    <Titles>
                                        <asp:Title ShadowOffset="4" Name="Title1" />
                                    </Titles>
                                    <Legends>
                                        <asp:Legend Alignment="Center" Docking="bottom"
                                            IsTextAutoFit="False" Name="Default"
                                            LegendStyle="Row" />
                                    </Legends>
                                    <Series>
                                        <asp:Series Name="Default" />
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1"
                                            BorderWidth="0" />
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        </div>
                        <div class="col-md-4 col-xs-4 no-padding ">
                <table style="width: 100%" class="mp-table2">

                    <tr class="vd_green">
                        <td class="tab-b-45 " style="color:#1FAE66 !important">Total Days 
                        </td>
                        <td class="text-center tab-b-10">:
                        </td>
                        <td class="tab-b-45 text-right">
                            <asp:Label ID="lblStdTotaldays" ForeColor="#1FAE66" runat="server"></asp:Label>

                        </td>
                    </tr>
                     <tr class="vd_yellow">
                        <td class="tab-b-45" style="color:#3f51b5 !important">Total Present
                        </td>
                        <td class="text-center tab-b-10">:
                        </td>
                        <td class="tab-b-45 text-right">
                            <asp:Label ID="lblStdTotalPresent" ForeColor="#3f51b5" runat="server"></asp:Label>
                        </td>
                    </tr>
                   <tr class="vd_yellow">
                        <td class="tab-b-45" style="color:#2196f3 !important">Percentage
                        </td>
                        <td class="text-center tab-b-10">:
                        </td>
                        <td class="tab-b-45 text-right">
                            <asp:Label ID="lblAttper" ForeColor="#2196f3" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
                    </fieldset>
                </div>

            </div>

        </div>
    </div>
</div>
