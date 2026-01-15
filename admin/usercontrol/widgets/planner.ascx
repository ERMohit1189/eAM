<%@ Control Language="C#" AutoEventWireup="true" CodeFile="planner.ascx.cs" Inherits="admin_usercontrol_widgets_planner" %>
<div class="col-md-4  mgbt-xs-10 dash-b-w">
    <div class="panel widget panel-bd-top vd_bdt-black vd_todo-widget light-widget">
        <div class="panel-heading no-title ">
            <h3 class="panel-title"><span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                <asp:LinkButton ID="lnkPlanner" runat="server" PostBackUrl="~/11/PlannerReport.aspx?print=1">Planner</asp:LinkButton></h3>
            <div class="vd_panel-menu">
                <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                <%--  <div data-action="refresh" title="Refresh" data-placement="bottom" class=" menu entypo-icon smaller-font"><i class="icon-cycle"></i></div>--%>
                <asp:LinkButton ID="lnkRefresh" OnClientClick="return refresh();" runat="server"
                    data-action="refresh" title="Refresh" data-placement="bottom"
                    class="menu entypo-icon smaller-font"><i class="icon-cycle"></i></asp:LinkButton>
                <div data-action="close" title="Close" data-placement="bottom" class=" menu entypo-icon"><i class="icon-cross"></i></div>
            </div>
        </div>

        <div class="panel-body">
            <div class=" no-padding pad-lr-15-1024-260">
                <div id="calendar"></div>
            </div>
        </div>
    </div>
    <!-- Panel Widget -->
</div>
<style>
    .fc .fc-header-title h2 {
            font-size: 13px !important;
    }
    #ContentPlaceHolder1_ContentPlaceHolderMainBox_w6 .panel .panel-body {
    padding: 8px 15px 7px 15px !important;
}
</style>
<script>
    function refresh() {

        $('#calendar').fullCalendar('refetchEvents');

        return false;
    }
</script>
