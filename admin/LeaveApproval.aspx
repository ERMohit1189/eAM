<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LeaveApproval.aspx.cs" Inherits="admin_LeaveApproval" %>

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
                                                <asp:ListItem Text="Pending" Value="2" Selected="True"></asp:ListItem>
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


                                    <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2" id="abc" runat="server">
                                            <asp:GridView ID="gvLeaveApp" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center" OnRowDataBound="gvLeaveApp_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="SrNo" HeaderText="#" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" HeaderStyle-Width="40px" />
                                                    <asp:BoundField DataField="EmpId" HeaderText="Emp ID" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="EmpName" HeaderText="Emp Name" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="AppDate" HeaderText="Application Date" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="LeaveDate" HeaderText="Leave Date" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="varStatus" HeaderText="Status" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="AppReason" HeaderText="Reason" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblA03ID" runat="server" Visible="false" Text='<%# Eval("A03ID") %>'></asp:Label>
                                                            <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("Status") %>'></asp:Label>
                                                            <asp:LinkButton ID="lbltnApproved" runat="server" title="Approve" OnClick="lbltnApproved_Click" data-toggle="tooltip" data-placement="top" class="btn menu-icon vd_bd-green vd_green"> <i class="fa fa-thumbs-up"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnCencelled" runat="server" title="Cancel" OnClick="lbtnCencelled_Click" data-toggle="tooltip" data-placement="top" class="btn menu-icon vd_bd-red vd_red"> <i class="fa fa-times"></i></asp:LinkButton>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="110px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>

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


