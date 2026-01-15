<%@ Control Language="C#" AutoEventWireup="true" CodeFile="planner.ascx.cs" Inherits="admin_usercontrol_widgets_planner" %>
<div class="col-lg-6  mgbt-xs-10 half-w-1280">
    <div class="panel widget panel-bd-top vd_bdt-grey vd_todo-widget light-widget ">
        <div class="panel-heading no-title">
            <h3 class="panel-title"><span class="menu-icon"><i class="fa  fa-calendar-alt"></i></span>
                <a href="../Planner.aspx" target="_blank">Planner</a></h3>
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
            <div class=" no-padding ">
                <div id="calendar"></div>
            </div>
        </div>
    </div>
    <script>
        function refresh() {

            $('#calendar').fullCalendar('refetchEvents');

            return false;
        }
    </script>
    <!-- Panel Widget -->
</div>




