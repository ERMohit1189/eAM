<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SalaryCalculation.aspx.cs" Inherits="admin_SalaryCalculation" %>

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
                                        <label class="control-label">Designation</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDesignation" CssClass="form-control-blue" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpDesignation_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 mgbt-xs-15">
                                        <label class="control-label">Month</label>
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
                                    <div class="col-sm-2 mgbt-xs-15">
                                        <label class="control-label">Year</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-3 mgbt-xs-15">
                                         <label class="control-label">Salary Calculation</label>
                                         <div class="">
                                             <asp:DropDownList ID="ddlSalary" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlSalary_SelectedIndexChanged">
                                                 <asp:ListItem>All</asp:ListItem>
                                                 <asp:ListItem>Not Calculated</asp:ListItem>
                                                 <asp:ListItem>Calculated</asp:ListItem>
                                             </asp:DropDownList>
                                             <div class="text-box-msg">
                                             </div>
                                         </div>
                                     </div>

                                    <div class="col-sm-2  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkCalculated" runat="server" OnClick="lnkCalculated_Click" CssClass="button">Calculate</asp:LinkButton>
                                        <div id="divMsg" runat="server" style="left: 47px;"></div>
                                    </div>


                                    <div class="col-sm-12  ">
                                        <div runat="server" id="divExport">
                                            <div class=" table-responsive  table-responsive2 ">
                                                <table class="table table-striped table-hover no-bm no-head-border table-bordered" id="tbl">
                                                    <asp:Repeater ID="rptEmp" runat="server">
                                                         <HeaderTemplate>
                                                            <tr class="itemrow">
                                                                <th># <asp:CheckBox ID="chkAll" runat="server" CssClass="chkAll" onchange="chkAll(this)"></asp:CheckBox></th>
                                                                <th class="">Emp. Id.</th>
                                                                <th>Name</th>
                                                                <th class="hide">Mobile No.</th>
                                                                <th>Days/Attendance</th>
                                                                <th>Head Details</th>
                                                                <th>CTC</th>
                                                                <th>Earning</th>
                                                                <th>Deductions</th>
                                                                <th style="white-space:nowrap !important;">Total</th>
                                                                <th>Advance/Loan</th>
                                                                <th>Net Pay</th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="text-center itemrow">
                                                                <td style="white-space:nowrap !important;"><%# Container.ItemIndex+1 %> 
                                                                    <asp:Label ID="lblcolor" runat="server"></asp:Label>
                                                                    <asp:CheckBox ID="chk" runat="server" CssClass="chk" onchange="chk(this)"></asp:CheckBox></td>
                                                                <td class="">
                                                                    <asp:Label ID="EmpId" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label></td>
                                                                <td style="text-align:left;">
                                                                    <asp:Label ID="Name" runat="server" Text='<%# Eval("name") %>' style="white-space:nowrap !important;"></asp:Label>
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
                                                                        <asp:Label ID="TotalDaysOfMonth" runat="server"></asp:Label>
                                                                    </div>
                                                                        </div>
                                                                    <div class="col-sm-12">
                                                                     <div style="float:left;">
                                                                        LWP/Absent/SWL
                                                                    </div>
                                                                    <div style="float:right;">
                                                                        <asp:Label ID="DeductedDaysOfMonth" runat="server"></asp:Label>
                                                                    </div>
                                                                        </div>
                                                                    <div class="col-sm-12">
                                                                    <div style="float:left;">
                                                                        Paid Days
                                                                    </div>
                                                                    <div style="float:right;">
                                                                        <asp:Label ID="TotalAttendance" runat="server"></asp:Label>
                                                                    </div>
                                                                    </div>
                                                                     
                                                                </td>
                                                                <td class="text-left" style="vertical-align:top !important; font-size: 10px;font-weight: bold;">
                                                                    <asp:Repeater ID="rptHead" runat="server">
                                                                        <ItemTemplate>
                                                                            <div class="col-sm-12">
                                                                                <div style="float: left;">
                                                                                    <asp:Label ID="ComponentId" CssClass="hide" runat="server" Text='<%# Eval("ComponentId") %>'></asp:Label>
                                                                                    <asp:Label ID="ComponentName" runat="server" Text='<%# Eval("ComponentName") %>'></asp:Label>
                                                                                </div>
                                                                                <div style="float: right;">
                                                                                    <asp:Label ID="Value" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="CTC" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="Earning" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="Deduction" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="Total" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="AdvanceSalary" runat="server"></asp:Label><br />
                                                                    <asp:TextBox ID="txtAdvanceSalary" runat="server" Visible="false" style="width:50%; text-align:right;" onblur="cHKdecimalOrNumeric(this)"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="Netpay" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                            <br />
                                            <div class="col-sm-12 hide" id="divSubmit">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click" CssClass="button">Submit</asp:LinkButton>
                                    </div>
                                        </div>
                                        <div id="divMsg2" runat="server" style="left: 47px;"></div>
                                    </div>

                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script>
                function setColor() {
                    //$('.NotEqual').closest('tr').children('td').css('background', 'rgb(250 225 152)')
                }
               function cHKdecimalOrNumeric(tis) {
                   var $this = $(tis);
                   $this.val($this.val().replace(/[^\d.]/g, ''));
                   var amount = ($(tis).val() == "" ? "" : $(tis).val());
                   $(tis).val(amount);
                   var lblNet = $(tis).closest('tr').find('td:eq(9) span').html();
                   var lbl = $(tis).closest('tr').find('td:eq(10) span').html();
                   if (parseFloat(amount) > parseFloat(lblNet)) {
                       $(tis).closest('tr').find('td:eq(9) span').css("color", "red");
                       $(tis).val("0.00");
                       $(tis).closest('tr').find('td:eq(10) span').css("color", "#333");
                       $(tis).closest('tr').find('td:eq(11) span').html(parseFloat(lblNet).toFixed(2));
                       return;
                   }
                   else{
                       $(tis).closest('tr').find('td:eq(9) span').css("color", "#333");
                   }
                   if (parseFloat(amount) > parseFloat(lbl)) {
                       $(tis).closest('tr').find('td:eq(10) span').css("color", "red");
                       $(tis).closest('tr').find('td:eq(9) span').css("color", "#333");
                       $(tis).val("0.00");
                       $(tis).closest('tr').find('td:eq(11) span').html(parseFloat(lblNet).toFixed(2));
                       return;
                   }
                   else {
                       $(tis).closest('tr').find('td:eq(10) span').css("color", "#333");
                   }
                   var total = (parseFloat(lblNet) - parseFloat(amount))
                   $(tis).closest('tr').find('td:eq(11) span').html(total.toFixed(2));

               }

               function chkAll(tis) {
                   var len = $('.chk').length;
                   if ($('.chkAll input[type=checkbox]').prop("checked") == true) {
                       for (var i = 0; i < len; i++) {
                           $('.chk:eq(' + i + ') input[type=checkbox]').prop("checked", true);
                       }
                       $("#divSubmit").removeClass('hide');
                   }
                   else {
                       for (var i = 0; i < len; i++) {
                           $('.chk:eq(' + i + ') input[type=checkbox]').prop("checked", false);
                       }
                       $("#divSubmit").addClass('hide');
                   }
               }
               function chk(tis) {
                   var len = $('.chk').length;
                   var cnt = 0;
                   for (var i = 0; i < len; i++) {
                       if ($('.chk:eq(' + i + ') input[type=checkbox]').prop("checked") == true) {
                           cnt = cnt + 1;
                       }
                   }
                   if (cnt == len) {
                       $('.chkAll input[type=checkbox]').prop("checked", true);
                   }
                   else {
                       $('.chkAll input[type=checkbox]').prop("checked", false);
                   }
                   if (cnt > 0) {
                       $("#divSubmit").removeClass('hide');
                   }
                   else {
                       $("#divSubmit").addClass('hide');
                   }
               }
           </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

