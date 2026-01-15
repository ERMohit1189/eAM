<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmpAttendenceUpdate.aspx.cs" Inherits="_8.EmpAttendanceUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                try {
                    Sys.Application.add_load(datetime);
                }
                catch (err) {

                }

            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" id="table1" runat="server">
                                    <div class="col-sm-3 half-width-50 ">
                                        <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:TextBox ID="txtDate" CssClass="form-control-blue datepicker-normal" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 half-width-50">
                                        <label class="control-label">Shift Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlShiftCat" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <%--Display Order--%>
                                    <div class="col-sm-4 half-width-50">
                                        <label class="control-label">Display Order&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlDisplayOrder" runat="server" CssClass="form-control-blue validatedrp">
                                                <asp:ListItem Enabled="true" Text="Alphabetical Order" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Designation Wise" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:LinkButton ID="lnkView" runat="server" CssClass="button" OnClick="lnkView_OnClick">View</asp:LinkButton>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="table-responsive table-responsive2">
                                            <div id="divExport" runat="server">
                                                <table class="table table-striped table-hover no-bm no-head-border table-bordered table-header-group text-left">
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Emp. Id</th>
                                                        <th>Machine Id</th>
                                                        <th>Emp Code</th>
                                                        <th>Name</th>
                                                        <th>Designation</th>
                                                        <th>In
                                                            <asp:Label ID="lblShiftIn" runat="server" Text='<%# "ShiftIn" %>'></asp:Label></th>
                                                        <th>Out
                                                            <asp:Label ID="lblShiftOut" runat="server" Text='<%# "ShiftOut" %>'></asp:Label></th>
                                                        <th>Attendance</th>
                                                        <th>Calculated Attendance</th>
                                                        <th>
                                                            <asp:CheckBox ID="chkEditAll" runat="server" OnCheckedChanged="chkEditAll_OnCheckedChanged" AutoPostBack="True" /></th>
                                                    </tr>
                                                    <asp:Repeater runat="server" ID="rptPunch" OnItemDataBound="rptPunch_OnItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex+1 %></td>
                                                                <td>
                                                                    <asp:Label ID="lblEmpId" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblMachineId" runat="server" Text='<%# Eval("MachineId") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("EmpCode") %>'></asp:Label></td>
                                                                <td><%# Eval("Name") %></td>
                                                                <td><%# Eval("Designation") %></td>
                                                                <td>
                                                                    <asp:Label ID="lblPunchInTime" runat="server" Text='<%# Eval("PunchInTime") %>'></asp:Label>
                                                                    &nbsp;
                                                                    <asp:Label ID="lblAttendenceIn" runat="server" Text='<%# Eval("AttendenceIn") %>' Font-Bold="True"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblPunchOutTime" runat="server" Text='<%# Eval("PunchOutTime") %>'></asp:Label>
                                                                    &nbsp;
                                                                    <asp:Label ID="lblAttendenceOut" runat="server" Text='<%# Eval("AttendenceOut") %>' Font-Bold="True"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblSavedAttendence" runat="server" Text='<%# Eval("SavedAttendence") %>' Font-Bold="True"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblCalcAttendence" runat="server" Text='<%# Eval("CalcAttendance") %>' Font-Bold="True"></asp:Label></td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkEdit" runat="server" OnCheckedChanged="chkEdit_OnCheckedChanged" AutoPostBack="True" /></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" OnClick="lnkSubmit_OnClick">Update</asp:LinkButton>
                                        <div runat="server" id="msgbox" style="left: 75px;"></div>
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

