<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LeaveAppReport.aspx.cs" Inherits="admin_LeaveAppReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Leave Application Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From Date</label>
                                        <div class="">
                                            <script>
                                                Sys.Application.add_load(datetime);
                                            </script>
                                            <asp:TextBox ID="txtFromDate" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date</label>
                                        <div class="">
                                            <script>
                                                Sys.Application.add_load(datetime);
                                            </script>
                                            <asp:TextBox ID="txtToDate" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Status&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control-blue">
                                                <asp:ListItem Text="--All--" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Canceled" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Emp ID&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlEmpID" runat="server" OnSelectedIndexChanged="ddlEmpID_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                            <div id="dvMSG" runat="server"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  mgbt-xs-10">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" data-toggle="tooltip" data-placement="top"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" data-toggle="tooltip" data-placement="top"><i class="fa fa-print "></i></asp:LinkButton>

                                                    <script>
                                                        Sys.Application.add_load(tooltip);
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
                                        <div class=" table-responsive  table-responsive2" id="abc" runat="server">
                                            <asp:GridView ID="gvLeaveApp" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center ">
                                                <Columns>
                                                    <asp:BoundField DataField="SrNo" HeaderText="#" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" HeaderStyle-Width="40px" />
                                                    <asp:BoundField DataField="EmpId" HeaderText="Emp ID" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="EmpName" HeaderText="Emp Name" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="AppDate" HeaderText="Application Date" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="LeaveDate" HeaderText="Leave Date" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="varStatus" HeaderText="Status" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="ApproveEmpName" HeaderText="Approved By" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />

                                                    <asp:BoundField DataField="AppReason" HeaderText="Reason" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="Address" HeaderText="Address" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="ContactNo1" HeaderText="Contact No." HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="ContactNo2" HeaderText="Alt. Contact No." HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                </Columns>
                                            </asp:GridView>
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
