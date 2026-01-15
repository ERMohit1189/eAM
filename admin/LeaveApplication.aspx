<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LeaveApplication.aspx.cs" Inherits="admin_LeaveApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Leave Application</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <asp:UpdatePanel ID="upMain" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-12 no-padding" id="tblInsert" runat="server">

                                            <div class="col-sm-12 no-padding">
                                                <div class="col-sm-4 half-width-50 mgbt-xs-15" id="Div1" runat="server">
                                                    <label class="control-label">Emp ID&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtEmpID" AutoPostBack="true" CssClass="form-control-blue" OnTextChanged="txtEmpID_TextChanged" runat="server"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 ">
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="gvEmp" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="EmpID" HeaderText="Emp ID" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" HeaderStyle-Width="70px" />
                                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                            <asp:BoundField DataField="EMobileNo" HeaderText="Mobile No." HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                            <asp:BoundField DataField="EFatherName" HeaderText="Father Name" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                            <asp:BoundField DataField="EmpCategory" HeaderText="Employee Category" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                            <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                            <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                                <label class="control-label">Is Half Day&nbsp;<span class="vd_red">*</span></label>

                                                <div class="">
                                                    <asp:RadioButtonList ID="rblIsHalfDay" RepeatDirection="Horizontal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblIsHalfDay_SelectedIndexChanged">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4 half-width-50 mgbt-xs-15" id="dvHalfType" runat="server">
                                                <label class="control-label">Half Day Type&nbsp;<span class="vd_red">*</span></label>
                                                <div class=" ">
                                                    <asp:DropDownList ID="ddlHaltType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHaltType_SelectedIndexChanged">
                                                        <asp:ListItem Text="1st Half" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="2nd Half" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4 half-width-50 mgbt-xs-15" id="dvNoOfDays" runat="server">
                                                <label class="control-label">No. Of Day(s)&nbsp;<span class="vd_red">*</span></label>
                                                <div class=" ">
                                                    <asp:DropDownList ID="ddlNoOfDays" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNoOfDays_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4 half-width-50 mgbt-xs-15" id="dvFromDate" runat="server">
                                                <label class="control-label">
                                                    <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label><span class="vd_red">*</span></label>
                                                <div class=" ">
                                                    <script>
                                                        Sys.Application.add_load(datetime);
                                                    </script>
                                                    <asp:TextBox ID="txtFromDate" runat="server" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4 half-width-50 mgbt-xs-15" id="dvToDate" runat="server">
                                                <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                                <div class=" ">
                                                    <script>
                                                        Sys.Application.add_load(datetime);
                                                    </script>
                                                    <asp:TextBox ID="txtToDate" ReadOnly="true" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4 half-width-50 mgbt-xs-15" id="dvReason" runat="server">
                                                <label class="control-label">Reason&nbsp;<span class="vd_red">*</span></label>
                                                <div class=" ">
                                                    <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4 half-width-50 mgbt-xs-15" id="Div2" runat="server">
                                                <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>
                                                <div class=" ">
                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4 half-width-50 mgbt-xs-15" id="dvContact1" runat="server">
                                                <label class="control-label">Contact 1&nbsp;<span class="vd_red">*</span></label>
                                                <div class=" ">
                                                    <asp:TextBox ID="txtContact1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4 half-width-50 mgbt-xs-15" id="dvContact2" runat="server">
                                                <label class="control-label">Contact 2&nbsp;<span class="vd_red">*</span></label>
                                                <div class=" ">
                                                    <asp:TextBox ID="txtContact2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 no-padding text-center mgbt-xs-15">
                                                <asp:LinkButton ID="btnInsert" runat="server" CssClass="button" OnClick="btnInsert_Click">Submit</asp:LinkButton>
                                                <asp:LinkButton ID="btnReset" runat="server" CssClass="button" OnClick="btnReset_Click">Reset</asp:LinkButton>
                                                <div id="dvMSG" runat="server"></div>
                                            </div>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

