<%@ Control Language="C#" AutoEventWireup="true" CodeFile="fee.ascx.cs" Inherits="admin_usercontrol_widgets_Wid2" %>
<div class="col-md-4  mgbt-xs-10 dash-b-w">
   <div class="panel widget panel-bd-top vd_bdt-blue  vd_todo-widget light-widget">
        <div class="panel-heading no-title ">
            <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-rupee"></i></span>Fee at a Glance</h3>
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
                    <asp:Chart ID="Chart4" runat="server">
                        <Titles>
                            <asp:Title ShadowOffset="4" Name="Title1" />
                        </Titles>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom"
                                IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" />
                        </Legends>
                      <%--  <Series>
                            <asp:Series Name="Default" />
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"
                                BorderWidth="0" />
                        </ChartAreas>--%>
                    </asp:Chart>
                </div>
                <div class="col-md-4 col-xs-4 no-padding">
                    <table style="width: 100%" class="mp-table2">

                        <tr class="vd_red">
                            <td class="tab-b-45 ">Total Due 
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                                <asp:Label ID="Label6" runat="server" Text="0"></asp:Label></td>
                        </tr>
                        <tr class="vd_green">
                            <td class="tab-b-45 ">Deposited 
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                                <asp:Label ID="Label7" runat="server" Text="0"></asp:Label></td>
                        </tr>
                        <tr class="vd_yellow">
                            <td class="tab-b-45">Balance
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                                <asp:Label ID="Label8" runat="server" Text="0"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <div class="vd_info br">
                    <h5 class="text-right font-semibold"><strong><%--Due Installment--%></strong></h5>
                    <h3 class="text-center font-s-14  vd_red">
                        <asp:Label ID="Label10" runat="server" Text=""></asp:Label></h3>
                </div>
            </div>
        </div>
    </div>
</div>
