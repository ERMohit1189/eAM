<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmployeeAttendanceUpdate.aspx.cs" Inherits="_8.EmployeeAttendanceUpdate" %>

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
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:TextBox ID="txtDate" CssClass="form-control-blue datepicker-normal" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:LinkButton ID="lnkView" runat="server" CssClass="button" OnClick="lnkView_OnClick">View</asp:LinkButton>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2">
                                            <div id="divExport" runat="server">
                                                <table class="table table-striped table-hover no-bm no-head-border table-bordered table-header-group text-left">
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Emp. Id</th>
                                                        <th>Machine Id</th>
                                                        <th>Name</th>
                                                        <th>In Punch</th>
                                                        <th>In Attendance</th>
                                                        <th>Out Punch</th>
                                                        <th>Out Attendance</th>
                                                        <th>Final Attendance</th>
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
                                                                <td><%# Eval("Name") %></td>
                                                                <td>
                                                                    <asp:Label ID="lblPunchInTime" runat="server" Text='<%# Eval("PunchInTime") %>'></asp:Label>
                                                                    <asp:TextBox ID="txtPunchInTime" runat="server" CssClass="form-control-blue"
                                                                         AutoPostBack="True" OnTextChanged="txtPunchTime_OnTextChanged"
                                                                         Text='<%# Eval("PunchInTime") %>' ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAttendenceIn" runat="server" Text='<%# Eval("AttendenceIn") %>' Font-Bold="True"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblPunchOutTime" runat="server" Text='<%# Eval("PunchOutTime") %>'></asp:Label>
                                                                    <asp:TextBox ID="txtPunchOutTime" runat="server" CssClass="form-control-blue"
                                                                                 AutoPostBack="True" OnTextChanged="txtPunchTime_OnTextChanged"
                                                                         Text='<%# Eval("PunchOutTime") %>' ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAttendenceOut" runat="server" Text='<%# Eval("AttendenceOut") %>' Font-Bold="True"></asp:Label></td>
                                                                
                                                                <td>
                                                                    <asp:Label ID="lblFinalAttendenceOut" runat="server" Text='<%# Eval("FinalAttendance") %>' Font-Bold="True"></asp:Label></td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkEdit" runat="server" OnCheckedChanged="chkEdit_OnCheckedChanged" AutoPostBack="True" /></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-12 ">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" OnClick="lnkSubmit_OnClick">Submit</asp:LinkButton>
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

