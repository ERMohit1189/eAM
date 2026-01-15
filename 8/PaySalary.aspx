<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PaySalary.aspx.cs" Inherits="PaySalary" %>

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
                                        <label class="control-label">Designation&nbsp;<span class="vd_red">* </span></label>
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
                                    
                                    
                                    <div class="col-sm-3  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkCalculated" runat="server" OnClick="lnkView_Click" CssClass="button">View</asp:LinkButton>
                                        <div id="divMsg" runat="server" style="left: 47px;"></div>
                                    </div>


                                    <div class="col-sm-12">
                                        <div runat="server" id="divExport">
                                            <div class=" table-responsive  table-responsive2 ">
                                                <table class="table table-striped table-hover no-bm no-head-border table-bordered" id="tbl">
                                                    <asp:Repeater ID="rptEmp" runat="server">
                                                         <HeaderTemplate>
                                                            <tr class="itemrow">
                                                                <th># <asp:CheckBox ID="chkAll" runat="server" CssClass="chkAll" onchange="chkAll(this)"></asp:CheckBox></th>
                                                                <th>Emp. Id.</th>
                                                                <th>Name</th>
                                                                <th>Mobile No.</th>
                                                                <th>Days/Attendance</th>
                                                                <th>Head Details</th>
                                                                <th>CTC</th>
                                                                <th>Earning</th>
                                                                <th>Deductions</th>
                                                                <th>Advance/Loan</th>
                                                                <th>Net Pay</th>
                                                                <th>Generated By</th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="text-center itemrow">
                                                                <td style="white-space:nowrap !important;"><%# Container.ItemIndex+1 %>
                                                                    <asp:CheckBox ID="chk" runat="server" CssClass="chk" onchange="chk(this)"></asp:CheckBox> 
                                                                   </td>
                                                                <td class="">
                                                                    <asp:Label ID="EmpId" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label></td>
                                                                <td style="text-align:left;">
                                                                    <asp:Label ID="Name" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align:left;">
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
                                                                            <div class="col-sm-12" style="min-width: 120px;">
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
                                                                    <asp:Label ID="AdvanceSalary" runat="server" Text='<%# Eval("AdvanceSalary") %>'></asp:Label><br />
                                                                </td>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="NetPay" runat="server" Text='<%# Eval("NetPay") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("GeneratedBy") %>'></asp:Label><br />
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("GeneratedDates") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-12 hide" id="divSubmit">
                                        <div class="col-sm-3 mgbt-xs-15" style="padding-left:0;">
                                        <label class="control-label">Mode</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlMode" runat="server" CssClass="form-control-blue" onchange="changeMode(this)">
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
                                         <div class="col-sm-3 mgbt-xs-15 hide" style="padding-left: 0;" id="divInstrumentNo">
                                             <label class="control-label">Instrument No.</label>
                                             <div class="">
                                                 <asp:TextBox runat="server" ID="txtInstrument" CssClass="form-control-blue"></asp:TextBox>
                                             </div>
                                         </div>
                                         <div class="col-sm-3 mgbt-xs-15 hide" style="padding-left: 0;" id="divBankName">
                                             <label class="control-label">Bank Name</label>
                                             <div class="">
                                                 <asp:TextBox runat="server" ID="txtBankName" CssClass="form-control-blue"></asp:TextBox>
                                             </div>
                                         </div>
                                         <div class="col-sm-3  btn-a-devices-2-p2 mgbt-xs-15">
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
            <script>
                function changeMode(tis) {
                    $('#txtInstrument').val('');
                    $('#txtBankName').val('');
                    if ($(tis).val() == "Cash") {
                        $('#divInstrumentNo').addClass('hide');
                        $('#divBankName').addClass('hide');
                    }
                    else {
                        $('#divInstrumentNo').removeClass('hide');
                        $('#divBankName').removeClass('hide');
                    }
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

