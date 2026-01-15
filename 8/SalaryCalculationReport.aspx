<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SalaryCalculationReport.aspx.cs" Inherits="admin_SalaryCalculationReport" %>

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
                                        <label class="control-label">Department</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDepartment" CssClass="form-control-blue" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Designation</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDesignation" CssClass="form-control-blue" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpDesignation_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Month</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control-blue" AutoPostBack="true">
                                                <asp:ListItem Value="1">Jan</asp:ListItem>
                                                <asp:ListItem Value="2">Feb</asp:ListItem>
                                                <asp:ListItem Value="3">Mar</asp:ListItem>
                                                <asp:ListItem Value="4">Apr</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">Jun</asp:ListItem>
                                                <asp:ListItem Value="7">Jul</asp:ListItem>
                                                <asp:ListItem Value="8">Aug</asp:ListItem>
                                                <asp:ListItem Value="9">Sep</asp:ListItem>
                                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                                <asp:ListItem Value="12">Dec</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Year</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control-blue" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Employee</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpEmp" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpEmp_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click" CssClass="button">Submit</asp:LinkButton>
                                        <div id="divMsg" runat="server" style="left: 47px;"></div>
                                    </div>

                                    <div class="col-sm-12  mgbt-xs-10">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float: right; font-size: 19px;" id="Panel2" runat="server">
                                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click1" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
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
                                    <div class="col-sm-12  ">
                                        <div runat="server" id="divExport">
                                            <div class=" table-responsive  table-responsive2 ">
                                                <div runat="server" id="header"></div>
                                                <div class="text-center">
                                                    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                                                </div>
                                                <table class="table table-striped table-hover no-bm no-head-border table-bordered">
                                                    <asp:Repeater ID="rptEmp" runat="server" OnItemDataBound="rptEmp_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Name</th>
                                                                <th>Emp Id</th>
                                                                <th style="display: none">Department</th>
                                                                <th>Designation</th>
                                                                <th style="display: none">Month</th>
                                                                <th style="display: none">Year</th>
                                                                <th>Basic Salary</th>
                                                                <th>Other Salary</th>
                                                                <th>CTC</th>
                                                                <th>Month Days</th>
                                                                <th>Total Working Days</th>
                                                                <th>On Time</th>
                                                                <th>Half Day</th>
                                                                <th>Late</th>
                                                                <th>SL</th>
                                                                <th style="display: none">HD+Lt</th>
                                                                <th>Absent</th>
                                                                <th>Payable Days</th>

                                                                <th>Net Basic Salary</th>
                                                                <th>Net Other Salary</th>
                                                                <th>Net Salary</th>

                                                                <th>Net PF</th>

                                                                <th>Deduction</th>
                                                                <th>Bonus</th>

                                                                <th>Gross Salary</th>

                                                                <th>Used CL</th>
                                                                <th>Salary Encash</th>
                                                                <th>Total Salary</th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="text-center">
                                                                <td><%# Container.ItemIndex+1 %></td>
                                                                <td>
                                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblEmpid" runat="server" Text='<%# Eval("Empid") %>'></asp:Label>
                                                                    <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("EmpCode") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td style="display: none">
                                                                    <asp:Label ID="lblDepartMent" runat="server" Text='<%# Eval("DepartMent") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation") %>'></asp:Label></td>
                                                                <td style="display: none">
                                                                    <asp:Label ID="lblMonthsName" runat="server" Text='<%# Eval("MonthsName") %>'></asp:Label></td>
                                                                <td style="display: none">
                                                                    <asp:Label ID="lblYearsName" runat="server" Text='<%# Eval("YearsName") %>'></asp:Label></td>


                                                                <td>
                                                                    <asp:Label ID="lblBasicSalary" runat="server" Text='<%# Eval("BasicSal") %>'></asp:Label></td>
                                                                <td>

                                                                    <asp:Label ID="lblOtherSalary" runat="server" Text='<%# Eval("OtherSal") %>'></asp:Label>
                                                                </td>
                                                                <td>

                                                                    <asp:Label ID="lblCTC" runat="server" Text='<%# Eval("CTC") %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblmonthlyday" runat="server" Text='<%# Eval("MonthDays") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbltotalWorkingday" runat="server" Text='<%# Eval("TotalWorkingDays") %>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblontime" runat="server" Text='<%# Eval("OnTime") %>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblhalfday" runat="server" Text='<%# Eval("HalfDay") %>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbllate" runat="server" Text='<%# Eval("Late") %>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSL" runat="server" Text='<%# Eval("SL") %>'></asp:Label>

                                                                </td>

                                                                <td style="display: none">
                                                                    <asp:Label ID="lblHdLt" runat="server" Text='<%# Eval("halfdaylatesl") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblabsent" runat="server" Text='<%# Eval("Absent") %>'></asp:Label>

                                                                </td>
                                                                <td>

                                                                    <asp:Label ID="lblSalaryday" runat="server" Text='<%# Eval("Payabledays") %>'></asp:Label></td>

                                                                <td>
                                                                    <asp:Label ID="lblNetBasicSalary" runat="server" Text='<%# Eval("NetBasicSal") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblNetOtherSalary" runat="server" Text='<%# Eval("NetOtherSal") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblNetSalary" runat="server" Text='<%# Eval("NetSalary") %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblPf" runat="server" Text='<%# Eval("PF") %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblDeduction" runat="server" Text='<%# Eval("Deduction") %>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblBonus" runat="server" Text='<%# Eval("Bonus") %>'></asp:Label>

                                                                </td>


                                                                <td>
                                                                    <asp:Label ID="lblGrossSalary" runat="server" Text='<%# Eval("GrossSalary") %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblCL" runat="server" Text='<%# Eval("usedCl") %>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCLinCash" runat="server" Text='<%# Eval("SalaryEncash") %>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTotalSalary" runat="server" Text='<%# Eval("TotalSalary") %>'></asp:Label></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td style="font-weight: bold" class="text-right">Total:</td>
                                                                <td>
                                                                    <asp:Label ID="lblTotalCTC" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="lblTotalNetBasicSalary" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="lblTotalNetOtherSalary" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="lblTotalNetSalary" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="lblTotalPf" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="lblTotalDeduction" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="lblTotalBonus" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="lblTotalGrossSalary" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                                <td></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="lblTotalSalEncash" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblTotalSalFooter" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                            </tr>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
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

