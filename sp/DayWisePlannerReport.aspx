<%@ Page Title="" Language="C#" MasterPageFile="~/sp/sp_root-manager.master" AutoEventWireup="true" 
CodeFile="DayWisePlannerReport.aspx.cs" Inherits="sp_DayWisePlannerReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtFromDate" placeholder="Select Date" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtToDate" placeholder="Select Date" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">

                                        <asp:LinkButton ID="lnkShow" runat="server" OnClick="lnkShow_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px;"></div>
                                    </div>





                                    <div class="col-sm-12  mgbt-xs-10">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float: right; font-size: 19px;" id="Panel1" runat="server">

                                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                                    <script>
                                                        
                                                    </script>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="ImageButton1" />
                                                <asp:PostBackTrigger ControlID="ImageButton2" />
                                                <asp:PostBackTrigger ControlID="ImageButton3" />
                                                <asp:PostBackTrigger ControlID="ImageButton4" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <div class="table-responsive2  table-responsive" id="mainDiv" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <div id="header" runat="server" style="width: 80%;"></div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <table class="table table-hover no-bm table-striped table-bordered pro-table">
                                                            <asp:Repeater ID="Repeater1" runat="server">
                                                                <HeaderTemplate>
                                                                    <tr>
                                                                        <td colspan="4" style="text-align: center">
                                                                            <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th class="text-center" style="width: 40px;">#</th>
                                                                        <th class="text-left" style="width: 85px;">Day</th>
                                                                        <th class="text-center" style="width: 150px;">Date</th>
                                                                        <th class="text-left">Title</th>
                                                                    </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="text-center"><%# Eval("Srno") %></td>
                                                                        <td class="text-left"><%# Eval("Day") %></td>
                                                                        <td class="text-center"><%# Eval("Startdate") %></td>
                                                                        <td class="text-left"><%# Eval("PlannerName") %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

