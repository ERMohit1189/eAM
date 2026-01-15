<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PaidSalaryReport.aspx.cs" Inherits="PaidSalaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <style>
        input[type=checkbox] {
            width: 14px !important; height: 17px !important
        }
    </style>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Designation&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDesignation" CssClass="form-control-blue" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpDesignation_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Month&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged">
                                                <asp:ListItem>Jan</asp:ListItem>
                                                <asp:ListItem>Feb</asp:ListItem>
                                                <asp:ListItem>Mar</asp:ListItem>
                                                <asp:ListItem>Apr</asp:ListItem>
                                                <asp:ListItem>May</asp:ListItem>
                                                <asp:ListItem>Jun</asp:ListItem>
                                                <asp:ListItem>Jul</asp:ListItem>
                                                <asp:ListItem>Aug</asp:ListItem>
                                                <asp:ListItem>Sep</asp:ListItem>
                                                <asp:ListItem>Oct</asp:ListItem>
                                                <asp:ListItem>Nov</asp:ListItem>
                                                <asp:ListItem>Dec</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Year&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Status</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                <asp:ListItem><--Select--></asp:ListItem>
                                                <asp:ListItem>Paid</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Mode</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlMode" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged">
                                                <asp:ListItem><--Select--></asp:ListItem>
                                                <asp:ListItem>Cash</asp:ListItem>
                                                <asp:ListItem>Cheque</asp:ListItem>
                                                <asp:ListItem>DD</asp:ListItem>
                                                <asp:ListItem>Card</asp:ListItem>
                                                <asp:ListItem>Online Transfer</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkCalculated" runat="server" OnClick="lnkView_Click" CssClass="button">View</asp:LinkButton>
                                        <div id="divMsg" runat="server" style="left: 47px;"></div>
                                    </div>

                                    <div class="col-sm-12  mgbt-xs-10" runat="server" id="div1" visible="false">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                    title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                    title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                    title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

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
                                    <div class="col-sm-12">
                                        <div runat="server" id="divExport">
                                            <div class="col-sm-12 text-center" runat="server" id="divGeading" visible="false">
                                            <div id="header" runat="server" class="col-md-12 no-padding"></div>
                                            <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;">Salary Report</asp:Label><asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                </div>
                                            <div class=" table-responsive  table-responsive2 ">
                                                <table class="table table-striped table-hover no-bm no-head-border table-bordered" id="tbl">
                                                    <asp:Repeater ID="rptEmp" runat="server">
                                                         <HeaderTemplate>
                                                            <tr class="itemrow">
                                                                <th>#</th>
                                                                <th>Emp. Id.</th>
                                                                <th>Name</th>
                                                                <th class="hide">Mobile No.</th>
                                                                <th>Days/Attendance</th>
                                                                <th>Head Details</th>
                                                                <th>CTC</th>
                                                                <th>Earning</th>
                                                                <th>Deductions</th>
                                                                <th>Total</th>
                                                                <th>Advance/Loan</th>
                                                                <th>Net Pay</th>
                                                                <th>Status</th>
                                                                <th>Generated By</th>
                                                                <th>Mode</th>
                                                                <th>Reference No.</th>
                                                                <th>Paid By</th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="text-center itemrow">
                                                                <td style="white-space:nowrap !important;"><%# Container.ItemIndex+1 %> 
                                                                   </td>
                                                                <td class="">
                                                                    <asp:Label ID="EmpId" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label></td>
                                                                <td style="text-align:left;">
                                                                    <asp:Label ID="Name" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align:left;" class="hide">
                                                                    <asp:Label ID="EMobileNo" runat="server" Text='<%# Eval("EMobileNo") %>'></asp:Label>
                                                                </td>
                                                                <td class="text-left" style="vertical-align:top !important; font-size: 10px;font-weight: bold;">
                                                                    <div class="col-sm-12">
                                                                    <div style="float:left;">
                                                                        Total Days 
                                                                    </div>
                                                                    <div style="float:right;">
                                                                        <asp:Label ID="TotalDaysOfMonth" runat="server" Text='<%# Eval("TotalDaysOfMonth") %>'></asp:Label>
                                                                    </div>
                                                                        </div>
                                                                    <div class="col-sm-12">
                                                                     <div style="float:left;">
                                                                        Leave/Absent
                                                                    </div>
                                                                    <div style="float:right;">
                                                                        <asp:Label ID="DeductedDaysOfMonth" runat="server" Text='<%# Eval("DeductedDaysOfMonth") %>'></asp:Label>
                                                                    </div>
                                                                        </div>
                                                                    <div class="col-sm-12">
                                                                    <div style="float:left;">
                                                                        Paid Days
                                                                    </div>
                                                                    <div style="float:right;">
                                                                        <asp:Label ID="TotalAttendance" runat="server" Text='<%# Eval("TotalAttendance") %>'></asp:Label>
                                                                    </div>
                                                                    </div>
                                                                    
                                                                </td>
                                                                <td class="text-left" style="vertical-align:top !important; font-size: 10px;font-weight: bold;">
                                                                    <asp:Repeater ID="rptHead" runat="server">
                                                                        <ItemTemplate>
                                                                            <div class="col-sm-12" style="min-width: 175px;">
                                                                                <div style="float: left;">
                                                                                    <asp:Label ID="ComponentName" runat="server" Text='<%# Eval("ComponentName") %>'></asp:Label>
                                                                                </div>
                                                                                <div style="float: right;">
                                                                                    <asp:Label ID="Value" runat="server" Text='<%# Eval("ComponentValue") %>'></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="CTC" runat="server" Text='<%# Eval("CTC") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="Earning" runat="server" Text='<%# Eval("Earning") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="Deduction" runat="server" Text='<%# Eval("Deduction") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="total" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="AdvanceSalary" runat="server" Text='<%# Eval("AdvanceSalary") %>'></asp:Label><br />
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="NetPay" runat="server" Text='<%# Eval("NetPay") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("GeneratedBy") %>'></asp:Label><br />
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("GeneratedDates") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Mode") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("InstumentNo") %>'></asp:Label><br />
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
                                                                </td>
                                                                
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("PaidBy") %>'></asp:Label><br />
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("PaidDates") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr>
                                                        <th colspan="5"></th>
                                                        <th style="text-align:right;"><asp:Label ID="lbltotalCTC" runat="server"></asp:Label></th>
                                                        <th style="text-align:right;"><asp:Label ID="lbltotalEarning" runat="server"></asp:Label></th>
                                                        <th style="text-align:right;"><asp:Label ID="lbltotalDeduction" runat="server"></asp:Label></th>
                                                        <th style="text-align:right;"><asp:Label ID="lbltotalTotal" runat="server"></asp:Label></th>
                                                        <th style="text-align:right;"><asp:Label ID="lbltotalAdvance" runat="server"></asp:Label></th>
                                                        <th style="text-align:right;"><asp:Label ID="lbltotalNetpay" runat="server"></asp:Label></th>
                                                        <th colspan="5"></th>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

