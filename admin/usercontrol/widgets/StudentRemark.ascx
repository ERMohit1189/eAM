<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StudentRemark.ascx.cs" Inherits="admin.usercontrol.widgets.AdminUsercontrolWidgetsWid2" %>

<div class="col-sm-4 col-md-4  " style="    float: none;
    margin: 0 auto;">
    <%-- ReSharper disable once UnknownCssClass --%>
   
  <div class="panel widget panel-bd-top  vd_bdt-green vd_todo-widget light-widget" style="border: 1px solid #393d41!important;">
      
        <div class="panel-body no-padding">

            <!-- vd_panel-menu -->
             <div class=" pad-lr-15-1024-260">
            <div class="col-md-8 col-xs-8 no-padding ">
                <asp:Chart ID="Chart1" runat="server" style="height: 253px !important; width: 300px !important;">
                    <Titles>
                        <asp:Title ShadowOffset="5" Name="Title1" />
                    </Titles>
                    <Legends>
                        <asp:Legend Alignment="Center" Docking="Bottom"  IsTextAutoFit="False" Name="Default"  LegendStyle="Row" />
                    </Legends>
                    <Series>
                        <asp:Series Name="Default" />
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"  BorderWidth="0" />
                    </ChartAreas>
                </asp:Chart>
            </div>

            <div class="col-md-4 col-xs-4 no-padding ">
                <br/><br/><br/><br/><br/><br/>
                <table style="width: 100%" class="mp-table2">

                    <tr class="vd_green">
                        <td class="tab-b-45 ">Positive 
                        </td>
                        <td class="text-center tab-b-10">:
                        </td>
                        <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                            <asp:Label ID="lblStdTotalPresent" ForeColor="#1FAE66" runat="server"></asp:Label>

                        </td>
                    </tr>
                    <tr class=" vd_red">
                        <td class="tab-b-45 ">Negative
                        </td>
                        <td class="text-center tab-b-10">:
                        </td>
                        <td class="tab-b-45 text-right">
                            <%-- ReSharper disable once Asp.Entity --%>
                            <asp:Label ID="lblStdTotalAbsent" ForeColor="#DA4448" runat="server"></asp:Label>
                        </td>
                    </tr>
                
                </table>
            </div>

            <div class="vd_info br" runat="server" Visible="False">
                <h5 class="text-right font-semibold"><strong>Total Students </strong></h5>
                <h3 class="text-center  vd_red font-s-14">
                    <asp:LinkButton ID="lblStdTotalAttendence" runat="server" PostBackUrl="#"></asp:LinkButton>
                </h3>
            </div>
 </div>
        </div>
    </div>
        </div>

