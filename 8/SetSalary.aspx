<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SetSalary.aspx.cs" Inherits="SetSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script type="text/javascript">

        function getEmployeeList() {
            $(function () {
                $("[id$=txtHeaderEmpId]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetEmployee") %>',
                            data: "{ 'empId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d,
                                    function(item) {
                                        return {
                                            label: item.split('@')[0],
                                            val: item.split('@')[1]
                                        }
                                    }));
                            },
                            error: function (request, status, error) { alert(request); alert(status); alert(error); },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfEmployeeId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
    <script src="../js/jquery.min.js"></script>
    <script>
        function calculate(tis) {
            $('.errr').html('');
            $('.erorDiv').addClass('hide');
            $(tis).addClass('txtModify');
            $(tis).removeClass('txtModify3');
            $('.txtCTC').css('border', '1px solid #D5D5D5 !important');
            let str = $(tis).val();
            let strCTC = $('.txtCTC').val();
            const regExp = /^(\d+(\.\d+)?)$/
            if (!regExp.test(str)) {
                $(tis).val('0');
                $(tis).removeClass('txtModify');
                $(tis).addClass('txtModify3');
                $('.errr').html('Invalid amount!');
                $('.erorDiv').removeClass('hide');
            }
            else if (!regExp.test(strCTC)) {
                $('.txtCTC').val('');
                $(tis).val('0');
                $('.txtCTC').css('border', '1px solid red !important');
                $('.errr').html("Invalid CTC amount!");
                $('.erorDiv').removeClass('hide');
            }
            
            else {
                var totalAmtE = 0;
                var lenE = $('.valueE').length;
                for (var i = 0; i < lenE; i++) {
                    var valueE = parseFloat($('.valueE:eq(' + i + ')').val());
                    if (valueE > 0) {
                        totalAmtE = totalAmtE + valueE;
                    }
                }
                var ctc = parseFloat($('.txtCTC').val());
                if (totalAmtE > ctc) {
                    totalAmtE = 0;
                    $(tis).val('0');
                    for (var i = 0; i < lenE; i++) {
                        var valueE = parseFloat($('.valueE:eq(' + i + ')').val());
                        if (valueE > 0) {
                            totalAmtE = totalAmtE + valueE;
                        }
                    }
                    $(tis).removeClass('txtModify');
                    $(tis).addClass('txtModify3');
                    $('.errr').html("Total gross amount exceeded from above gross salary!");
                    $('.erorDiv').removeClass('hide');
                }
                var totalAmtD = 0;
                var lenD = $('.valueD').length;
                for (var j = 0; j < lenD; j++) {
                    var valueD = parseFloat($('.valueD:eq(' + j + ')').val());
                    if (valueD > 0) {
                        totalAmtD = totalAmtD + valueD;
                    }
                }
                var netPay = 0;
                netPay = (totalAmtE - totalAmtD);
                $(".totalGross").html(totalAmtE);
                $(".totalDeductions").html(totalAmtD);
                $(".totalNetPay").html(netPay);
            }
        }
    </script>
    <style>
        .txtModify {
            width: 35% !important;
    border: none !important;
    outline: none !important;
    border-bottom: 2px dotted #000000 !important;
    text-align: right !important;
    padding: 0px !important;
        }
        .txtModify3 {
            width: 35% !important;
    border: none !important;
    outline: none !important;
    border-bottom: 2px dotted red !important;
    text-align: right !important;
    padding: 0px !important;
        }
        .txtModify2 {
            width: 35% !important;
    border: none !important;
    outline: none !important;
    background: #e9e8e8 !important;
    text-align: right !important;
    padding: 0px !important;
        }
        input[type=checkbox] {
    width: 15px !important;
    height: 17px !important;
    margin: 0px 0px;
    line-height: normal;
    vertical-align: text-bottom;
}
    </style>
    <div id="loader" runat="server"></div>
            <script>
                try {
                    Sys.Application.add_load(datetime);
                    
                }
                catch (ex) {
                }
            </script>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getEmployeeList);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">
                                    <div class="col-sm-4 mgbt-xs-15">
                                        <asp:TextBox ID="txtHeaderEmpId" placeholder="Enter Emp. ID/ Emp. Code/ Name" runat="server" AutoPostBack="true" OnTextChanged="txtHeaderEmpId_TextChanged" CssClass="form-control-blue"></asp:TextBox>
                                        <div class="text-box-msg">
                                            <asp:HiddenField ID="hfEmployeeId" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkShow" runat="server" CssClass="button" OnClick="lnkShow_Click" ValidationGroup="a">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                    <div class="erorDiv col-sm-6 mgbt-xs-15 hide">
                                            <span class="alert alert-danger" style="padding: 5px;"><i class="fa fa-exclamation-circle"></i>&nbsp;<asp:Label runat="server" ID="errr" CssClass="errr" style="padding: 0px;"></asp:Label></span>
                                        </div>
                                    <div class="col-sm-12" runat="server" id="divRecord" visible="false">
                                       <table class="table table-bordered">
                                           <thead class="well">
                                               <tr>
                                                   <th style="text-align:left;">Employee ID.</th>
                                                   <td style="text-align:left;"><asp:Label runat="server" ID="lblEmployeeId"></asp:Label></td>
                                                   <th style="text-align:left;">Designation</th>
                                                   <td style="text-align:left;"><asp:Label runat="server" ID="lblDesignation"></asp:Label></td>
                                               </tr>
                                               <tr>
                                                   <th style="text-align:left;">Name</th>
                                                   <td style="text-align:left;"><asp:Label runat="server" ID="lblName"></asp:Label></td>
                                                   <th style="text-align:left;">Date of Joining</th>
                                                   <td style="text-align:left;"><asp:Label runat="server" ID="lblDateofJoining"></asp:Label></td>
                                               </tr>
                                               <tr>
                                                   <th style="text-align:left;">Father's Name</th>
                                                   <td style="text-align:left;"><asp:Label runat="server" ID="lblFathersName"></asp:Label></td>
                                                   <th style="text-align:left;">Contact No.</th>
                                                   <td style="text-align:left;"><asp:Label runat="server" ID="lblContactNo"></asp:Label></td>
                                               </tr>
                                               <tr>
                                                   <th style="text-align:left;">PF NO.</th>
                                                   <td style="text-align:left;"><asp:Label runat="server" ID="lblPFNo"></asp:Label></td>
                                                   <th style="text-align:left;">E.S.I. NO.</th>
                                                   <td style="text-align:left;"><asp:Label runat="server" ID="lblESICNo"></asp:Label></td>
                                               </tr>
                                               <tr>
                                                   <th style="text-align:left;">Gross Salary</th>
                                                   <td style="text-align:left;"><asp:TextBox runat="server" ID="txtCTC" onblur="Validate(this)" CssClass="txtCTC"></asp:TextBox></td>
                                                   <td colspan="2"></td>
                                               </tr>
                                           </thead>
                                           <tbody class="well">
                                               <tr>
                                                   <td colspan="4" style="height:30px;"></td>
                                               </tr>
                                               <tr style="background:#ccc;">
                                                   <th style="text-align:center;" colspan="2">EARNINGS</th>
                                                   <th style="text-align:center;" colspan="2">DEDUCTIONS</th>
                                               </tr>
                                               <tr>
                                                   <td colspan="2">
                                                       <table class="table table-bordered">
                                                           <tr>
                                                               <th style="text-align: left;background: #ccc;" colspan="2"><b>Fixed Pay</b></th>

                                                           </tr>
                                                           <asp:Repeater runat="server" ID="rptEarningFix">
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <th style="text-align: left;">
                                                                           <asp:Label runat="server" ID="lblCmpId" class="hide" Text='<%# Eval("ComponentId") %>'></asp:Label>
                                                                           <asp:Label runat="server" ID="lblCmpName" Text='<%# Eval("ComponentName") %>'></asp:Label></th>
                                                                       <td style="text-align: right;">
                                                                           <i class="fa fa-inr"></i><asp:TextBox runat="server" ID="txtCmVal" Text='<%# Eval("ComponentValue") %>' onblur="calculate(this)" CssClass="valueE txtModify"></asp:TextBox>
                                                                       </td>
                                                                   </tr>
                                                               </ItemTemplate>
                                                           </asp:Repeater>
                                                           <tr>
                                                               <th style="text-align: left;background: #ccc;" colspan="2"><b>Flexible Benefit Plan (FBP)</b></th>

                                                           </tr>
                                                           <asp:Repeater runat="server" ID="rptEarningFlaxiable">
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <th style="text-align: left;">
                                                                           <asp:Label runat="server" ID="lblCmpId1" class="hide" Text='<%# Eval("ComponentId") %>'></asp:Label>
                                                                           <asp:Label runat="server" ID="lblCmpName1" Text='<%# Eval("ComponentName") %>'></asp:Label></th>
                                                                       <td style="text-align: right;">
                                                                           <i class="fa fa-inr"></i><asp:TextBox runat="server" ID="txtCmVal1" Text='<%# Eval("ComponentValue") %>' onblur="calculate(this)" CssClass="valueE txtModify"></asp:TextBox>
                                                                       </td>
                                                                   </tr>
                                                               </ItemTemplate>
                                                           </asp:Repeater>

                                                       </table>
                                                   </td>
                                                   <td colspan="2">
                                                       <table class="table table-bordered">
                                                           <asp:Repeater runat="server" ID="rptDeduction">
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <th style="text-align:left;">
                                                                           <asp:Label runat="server" ID="lblCmpId2" class="hide" Text='<%# Eval("ComponentId") %>'></asp:Label>
                                                                           <asp:Label runat="server" ID="lblCmpName2" Text='<%# Eval("ComponentName") %>'></asp:Label></th>
                                                                       <td style="text-align:right;">
                                                                           
                                                                           <i class="fa fa-inr"></i><asp:TextBox runat="server" ID="txtCmVal2" Text='<%# Eval("ComponentValue") %>' Enabled='<%# Eval("ComponentCategory").ToString()=="Salary Advance" || Eval("ComponentCategory").ToString()=="Other Deduction"?false:true %>' CssClass='<%# Eval("ComponentCategory").ToString()=="Salary Advance" || Eval("ComponentCategory").ToString()=="Other Deduction"?"valueD txtModify2":"valueD txtModify" %>' onblur="calculate(this)"></asp:TextBox>
                                                                       </td>
                                                                   </tr>
                                                               </ItemTemplate>
                                                           </asp:Repeater>
                                                       </table>
                                                   </td>
                                               </tr>
                                               <tr style="background:#ccc;">
                                                   <th style="text-align:left;">Gross Salary</th>
                                                   <th style="text-align:left;"><i class="fa fa-inr"></i>&nbsp;<asp:Label runat="server" ID="totalGross" CssClass="totalGross"></asp:Label></th>
                                                   <th style="text-align:left;">Total Deductions</th>
                                                   <th style="text-align:left;"><i class="fa fa-inr"></i>&nbsp;<asp:Label runat="server" ID="totalDeductions" CssClass="totalDeductions"></asp:Label></th>
                                               </tr>
                                                <tr style="background:#ccc;">
                                                   <th style="text-align:center;" colspan="3">Net Pay</th>
                                                   <th style="text-align:left;"><i class="fa fa-inr"></i>&nbsp;<asp:Label runat="server" ID="totalNetPay" CssClass="totalNetPay"></asp:Label></th>
                                               </tr>
                                           </tbody>
                                       </table>
                                        <div class="col-sm-12 well" id="divAppraisal" runat="server">
                                            <div class="col-sm-3">
                                                <asp:CheckBox runat="server" ID="chkAppraisal" Text="&nbsp;Is Appraisal" CssClass="vd_check check-success" /><br />
                                                <span style="font-size:10px; color:red;">(Note:- Please keep uncheck if record update only.)</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Label runat="server" ID="Label2" CssClass="form-label">Appraisal Date</asp:Label>
                                                <asp:TextBox runat="server" ID="txtAppraisalDate" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Label runat="server" ID="Label3" CssClass="form-label">Appraisal Remark</asp:Label>
                                                <asp:TextBox runat="server" ID="txtAppraisalRemark" CssClass="form-control-blue"></asp:TextBox>
                                            </div>
                                            
                                        </div>
                                        <div class="col-sm-12 text-center well">
                                                <asp:LinkButton ID="btnInsert" runat="server" CssClass="button form-control-blue" OnClick="btnInsert_Click" OnClientClick="ValidateTextBox('.validatetxtE');ValidateDropdown('.validatedrpE');return validationReturn();">Submit</asp:LinkButton>
                                                <div id="Div1" runat="server" style="left: 140px;"></div>
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

