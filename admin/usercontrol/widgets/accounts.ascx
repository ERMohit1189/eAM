<%@ Control Language="C#" AutoEventWireup="true" CodeFile="accounts.ascx.cs" Inherits="admin_usercontrol_widgets_Wid2" %>
<div class="col-md-4  mgbt-xs-10 dash-b-w">

    <div class="panel widget panel-bd-top vd_bdt-grey vd_todo-widget light-widget">
        <div class="panel-heading no-title ">
            <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-money"></i></span>
                <asp:LinkButton ID="lnkStdAtt" runat="server" PostBackUrl="~/7/DayBookReport.aspx?Type=1">Accounts at a Glance</asp:LinkButton></h3>
            <div class="vd_panel-menu">
                <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                <div data-action="refresh" title="Refresh" data-placement="bottom" class=" menu entypo-icon smaller-font"><i class="icon-cycle"></i></div>
                <div data-action="close" title="Close" data-placement="bottom" class=" menu entypo-icon"><i class="icon-cross"></i></div>
            </div>
        </div>
        <div class="panel-body no-padding">

            <!-- vd_panel-menu -->
            <div class=" pad-lr-15-1024-260" style="    height: 286px;">
                 <div class="col-md-5 col-xs-5 no-padding">
                    
                </div>
                <div class="col-md-7 col-xs-7 no-padding">
                    <table style="width: 100%" class="mp-table2">

                        <tr class="vd_blue">
                            <td class="tab-b-45 ">Cash Opening 
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                                <asp:Label ID="Label1" runat="server" Text="0"></asp:Label></td>
                        </tr>
                        <tr class="vd_green">
                            <td class="tab-b-45 ">Income (Cash)
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                            <asp:Label ID="Label2" runat="server" Text="0"></asp:Label></td>
                        </tr>
                        <tr class="vd_green">
                            <td class="tab-b-45 ">Income (Other)
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                            <asp:Label ID="Label5" runat="server" Text="0"></asp:Label></td>
                        </tr>
                        <tr class="vd_red">
                            <td class="tab-b-45">Cash Expense
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                                <asp:Label ID="Label3" runat="server" Text="0"></asp:Label></td>
                        </tr>
                         <tr class="vd_blue">
                            <td class="tab-b-45">Discount
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                                <asp:Label ID="lblDiscount" runat="server" Text="0"></asp:Label></td>
                        </tr>
                        <tr class="vd_yellow">
                            <td class="tab-b-45">Cash Closing 
                            </td>
                            <td class="text-center tab-b-10">:
                            </td>
                            <td class="tab-b-45 text-right">
                                <asp:Label ID="Label4" runat="server" Text="0"></asp:Label></td>
                        </tr>
                        

                    </table>
                </div>
            </div>
        </div>
    </div>
</div>
