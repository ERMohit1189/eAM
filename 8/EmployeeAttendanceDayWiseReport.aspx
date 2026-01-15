<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmployeeAttendanceDayWiseReport.aspx.cs" Inherits="admin_EmployeeAttendanceDayWiseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
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

                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpDDEmpYY" runat="server" OnSelectedIndexChanged="DrpDDEmpYY_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDDEmpMM" runat="server" OnSelectedIndexChanged="DrpDDEmpMM_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDDEmpDD" runat="server"
                                                        CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Department&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DrpDepartment" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 47px;"></div>
                                    </div>

                                </div>

                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

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

                                <div class="col-sm-12  " id="divExport" runat="server">
                                    <div class=" table-responsive  table-responsive2" id="abc" runat="server">
                                        <asp:GridView ID="Grd1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered table-header-group">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("EFirstName") %>'></asp:Label>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("EMiddleName") %>'></asp:Label>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("ELastName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("EMobileNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Attendance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("AttendanceValue") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  />
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
            


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

