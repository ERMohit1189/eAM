<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmpAttendanceReport.aspx.cs" Inherits="_8.AdminEmpAttendanceReport" %>

<%@ Register Src="~/admin/usercontrol/loader.ascx" TagPrefix="uc1" TagName="loader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Staff Attendance Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <uc1:loader runat="server" ID="loader" />
    <asp:UpdatePanel ID="upMaun" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <asp:HiddenField ID="hidSQL" runat="server" />
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Date</label>
                                    <div class="">
                                        <asp:TextBox ID="txtFromDate" placeholder="" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15" id="pnl2" runat="server" visible="false">
                                    <label class="control-label">To Date</label>
                                    <div class="">
                                        <asp:UpdatePanel ID="upDate" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtToDate" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="ddlAttendance" />
                                                <asp:PostBackTrigger ControlID="txtToDate" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Attendance</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlAttendance" AutoPostBack="true" OnSelectedIndexChanged="ddlAttendance_SelectedIndexChanged" runat="server" CssClass="form-control-blue validatedrp">
                                            <asp:ListItem Text="--All--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Present" Value="P"></asp:ListItem>
                                            <asp:ListItem Text="Late" Value="LT"></asp:ListItem>
                                            <asp:ListItem Text="Absent" Value="A"></asp:ListItem>
                                            <asp:ListItem Text="Not Mark" Value="N"></asp:ListItem>
                                            <asp:ListItem Text="Half Day" Value="HD"></asp:ListItem>
                                            <asp:ListItem Text="Short Leave" Value="SL"></asp:ListItem>
                                            <asp:ListItem Text="Sandwitch Leave" Value="SWL"></asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                     <label class="control-label">Designation</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpDesignation" AutoPostBack="true" OnSelectedIndexChanged="drpDesignation_SelectedIndexChanged" runat="server" CssClass="form-control-blue validatedrp" ></asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>


                                <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15" style="display: none;">
                                    <asp:LinkButton ID="lbtnSearch" Visible="false" OnClick="lbtnSearch_Click" ValidationGroup="A" CssClass="btn vd_btn vd_bg-blue" runat="server">  <i class="fa fa-paper-plane"></i> Search </asp:LinkButton>
                                </div>

                                <div class="col-sm-12   mgbt-xs-15 btn-a-devices-1-p4-p2">
                                    <h4>
                                        <asp:Label ID="lblHeading" Width="100%" runat="server"></asp:Label></h4>
                                </div>

                                <div class="col-sm-12  ">
                                    <div class="table-responsive2  table-responsive">
                                        <asp:GridView ID="gvAttendance" AutoGenerateColumns="false" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="#" DataField="Count" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-center" ItemStyle-CssClass="text-center" />--%>
                                                <asp:BoundField HeaderText="Emp. ID." DataField="EmpId" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />
                                                <asp:BoundField HeaderText="Name" DataField="EmpName" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />
                                                <asp:BoundField HeaderText="Designation" DataField="Designation" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left"
                                                    ItemStyle-CssClass="text-left" />
                                                <asp:BoundField HeaderText="Mobile No." DataField="EMobileNo" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />
                                                <asp:BoundField HeaderText="Attendance" DataField="AttendanceValue"
                                                    HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />

                                                <asp:BoundField HeaderText="In" DataField="InTime"
                                                    HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />

                                                <asp:BoundField HeaderText="Out" DataField="OutTime"
                                                    HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />
                                            </Columns>
                                        </asp:GridView>
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



