<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmpAttendanceStatusUpdate.aspx.cs" Inherits="_8.AdminEmpAttendanceReport" %>

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
                                                <asp:PostBackTrigger ControlID="ddlEmp" />
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
                                            <asp:ListItem Text="Absent" Value="A"></asp:ListItem>
                                            <asp:ListItem Text="Not Mark" Value="N"></asp:ListItem>
                                            <asp:ListItem Text="Leave" Value="L"></asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Staff</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlEmp" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
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
                                        <asp:GridView ID="gvAttendance" AutoGenerateColumns="false"
                                            CssClass="table mp-table no-tb no-bm p-table-bordered table-bordered vd_bg-green form-control-blue vd_white" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="#" DataField="Count" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-center" ItemStyle-CssClass="text-center" />--%>
                                                <%--<asp:BoundField HeaderText="Emp. ID." DataField="EmpId" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />--%>
                                                <asp:TemplateField HeaderText="Emp. ID.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblempcode"  Visible="False"  runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                        <asp:Label ID="lblempid" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                        <asp:Label ID="lbldepartment"  Visible="False"  runat="server" Text='<%# Bind("DepartmentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np text-center" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblempname" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-3" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                </asp:TemplateField>
                                                

                                                <%--<asp:BoundField HeaderText="Name" DataField="EmpName" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />--%>

                                                 <%--<asp:BoundField HeaderText="Designation" DataField="Designation" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" 
                                                     ItemStyle-CssClass="text-left" />--%>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesignation" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-3" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                </asp:TemplateField>

                                                <%--<asp:BoundField HeaderText="Mobile No." DataField="EMobileNo" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />--%>

                                                <asp:TemplateField HeaderText="Mobile No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmobileno" runat="server" Text='<%# Bind("EMobileNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-3" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                </asp:TemplateField>

                                                <%--<asp:BoundField HeaderText="Attendance" DataField="AttendanceValue" 
                                                    HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />--%>

                                                <asp:TemplateField HeaderText="Attendance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("AttendanceValue") %>'></asp:Label>
                                                        <asp:Label ID="Label22" runat="server" Visible="False" Text='<%# Bind("AttendanceValue") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-3"  />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Attendance">
                                                    <HeaderTemplate>
                                                        <asp:DropDownList ID="drpAttendance" AutoPostBack="true" 
                                                                          OnSelectedIndexChanged="drpAttendance_OnSelectedIndexChanged" 
                                                                          CssClass="form-control-blue"  Font-Size="12px"
                                                                          ForeColor="Black" runat="server">
                                                        </asp:DropDownList>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"
                                                             Font-Size="12px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" 
                                                                          CssClass="form-control-blue vd_bg-green vd_white attendence_ddl" >
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-2 form-group" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 text-center no-padding mgbt-xs-15" runat="server" id="btnshowsubmit" Visible="False">
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_OnClick" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                       <div id="msgbox" runat="server" style="left:75px"></div>
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



