<%@ Page Title="Planner | eAM" Language="C#" MasterPageFile="~/root-manager.master" AutoEventWireup="true" CodeFile="Planner.aspx.cs" Inherits="Planner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <style>
                .fc-event-skin {
    border-color: none !important;
    background-color: none !important;
    color: none !important;
}
                .fc-event-inner {
                    visibility: visible !important;
                }
            </style>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel widget light-widget panel-bd-top">
                           <%-- <div class="panel-heading vd_bg-red">
                                <h3 class="panel-title"><span class="menu-icon"><i class="fa  fa-calendar"></i></span>Planner</h3>
                                <div class="vd_panel-menu">
                                    <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                                    <div data-action="refresh" onclick="return refresh();" title="Refresh"
                                        data-placement="bottom" class=" menu entypo-icon smaller-font">
                                        <i class="icon-cycle"></i>
                                    </div>
                                    <div data-action="close" title="Close" data-placement="bottom" class=" menu entypo-icon"><i class="icon-cross"></i></div>
                                </div>
                            </div>--%>
                            <div class="panel-body">
                                <div class="col-md-12">
                                    <%-- <label class="control-label">Chart Show As: &nbsp;<span class="vd_red"></span></label>--%>
                                    <asp:RadioButtonList ID="rbList" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbList_SelectedIndexChanged"
                                        RepeatLayout="Flow" CssClass="vd_radio radio-success">
                                        <asp:ListItem Selected="True">Calendar View</asp:ListItem>
                                        <asp:ListItem>Tabular View</asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>
                                <div class="col-md-12">
                                    <iframe id="FullCalendarFrame" src="Planner1.aspx" runat="server"  scrolling="no" class="mgtp-10" style="width: 100%; border:none; height: 900px;"></iframe>
                                </div>
                            </div>
                        </div>
                        <!-- Panel Widget -->
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



