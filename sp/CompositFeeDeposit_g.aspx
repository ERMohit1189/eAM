<%@ Page Title="Composite Fee | eAM&#174;" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="false" CodeFile="CompositFeeDeposit_g.aspx.cs" Inherits="CompositFeeDeposit_g" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script src="../js/jquery-1.4.3.min.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="https://js.paystack.co/v1/inline.js"></script>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSearch]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetStudents") %>',
                            data: "{ 'studentId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {

                                response($.map(data.d,
                                    function (item) {
                                        return {
                                            label: item.split('@')[0],
                                            val: item.split('@')[1]
                                        }
                                    }));
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfStudentId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
     <script>
        function disablebtn() {
            $('#ContentPlaceHolder1_ContentPlaceHolderMainBox_lnkSubmit').click(function () {
                $('#ContentPlaceHolder1_ContentPlaceHolderMainBox_lnkSubmit').hide();
            });
        }
    </script>
    <style>
        .pro-table1 > tbody > tr > th:nth-child(n) {
            background-color: #ddd;
            color: #000000;
        }

        .pro-table1 > tbody > tr > td:nth-child(n) {
            background-color: #eee;
            color: #000000;
        }

        input[type=radio], input[type=checkbox] {
            width: 17px;
            height: 17px;
            margin: 0px 0px;
            line-height: normal;
            vertical-align: text-bottom;
        }

        .rowYellow {
            background: #ff8c00 !important;
            color: #000 !important;
            height: 30px !important;
        }

        .rowGreen {
            background: #4e9057 !important;
            color: #eaeaea !important;
            height: 30px !important;
        }

        .rowGreenchild {
            background: #7fa284 !important;
            color: #eaeaea !important;
            height: 30px !important;
        }


        .box {
            max-width: 240px;
            display: block;
            position: absolute;
            background: #808080;
            color: white;
            /*padding: 5px;*/
            /*border: 1px solid black;*/
            text-align: left;
            z-index: 99999;
            border-radius: 10px;
            vertical-align: top !important;
        }

            .box .col-sm-6 {
                padding-left: 5px !important;
                padding-right: 5px !important;
                padding-bottom: 5px !important;
                padding-top: 5px !important;
            }

        input[disabled], textarea[disabled] {
            cursor: default;
            background: #e6e6e6 !important;
        }

        @media print {
            .no-print, .no-print * {
                display: none !important;
            }
        }
    </style>
    <script src="https://psa.atomtech.in/staticdata/ots/js/atomcheckout.js" type="text/javascript"></script>
<script type="text/javascript">
    function openAtomPay() {

        //alert('openPay called');
        var TokenId = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_hdn_atomTokenId').value;
        var merchTxnIds = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_hdn_merchTxnIds').value;
        var MerchentId = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_hdn_merchId').value;
        var CustEmail = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_hdn_custEmail').value;
        var CustMobile = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_hdn_custMobile').value;
        var ReturnUrl = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_hdn_returnUrl').value;
        var Amt = document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_hdn_Amt').value;
        //alert("mesage:"+rb);

        const options = {
            "atomTokenId": TokenId,
            "merchId": MerchentId,
            "custEmail": CustEmail,
            "custMobile": CustMobile,
            "returnUrl": ReturnUrl + "?txnid="+merchTxnIds + "$$$" + Amt
        }
        let atom = new AtomPaynetz(options, 'uat');
    }
</script>
     <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                try {
                    Sys.Application.add_load(getStudentsList);
                    Sys.Application.add_load(datetime);
                    
                    Sys.Application.add_load(prettyphoto);
                    Sys.Application.add_load(disablebtn);
                }
                catch (ex) {

                }
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12" id="divstd" runat="server">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body" id="printdiv">
                                 <div style="background: #ff0000; color: #fff; padding: 6px; font-size: 17px;" visible="false" runat="server" id="divMess">
                                            <asp:Label runat="server" ID="mess"></asp:Label>
                                        </div>
                                 <div id="msgs" runat="server" style="color: #FF0000"></div>
                                <asp:TextBox runat="server" ID="txtDepositDate" CssClass="datepicker-normal hide"></asp:TextBox>
                                <div class="col-sm-6  no-padding hidden-print">
                                    <div id="msgbox" runat="server" style="left: 60px;"></div>
                                </div>
                                <div class="col-sm-12" runat="server" id="divTutionFee" visible="false">
                                    <span id="btnPrint" runat="server" visible="false" class="btn-print-box" onclick="printDiv();"><a><i class="icon-printer"></i></a></span>
                                       
                                    <fieldset style="padding: 5px !important;">
                                        <legend style="width: 100%"><i class="fa fa-book"></i>&nbsp;Fee Details
                                                <asp:CheckBox runat="server" ID="chkCompleteFee" Text="&nbsp;Complete Fee Payment" AutoPostBack="true" OnCheckedChanged="chkCompleteFee_CheckedChanged" Style="float: right; font-size: 13px; margin: 0; padding: 0; height: 16px; color: #ff8c00;" />
                                        </legend>
                                        <div class="table table-responsive">
                                            <table class="table no-bm no-head-border table-bordered pro-table1 table-header-group" id="mainTable">
                                                <asp:Repeater ID="rptFeeStructure" runat="server">
                                                    <HeaderTemplate>
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 3%;">#</th>
                                                                <th class="text-left " style="width: 27%;">Installment</th>
                                                                <th class="text-right" style="width: 10%">Fee</th>
                                                                <th class="text-right" style="width: 10%">Discount</th>
                                                                <th class="text-right" style="width: 10%">Total</th>
                                                                <th class="text-right" style="width: 10%">Paid/ Payable</th>
                                                                <th class="text-right" style="width: 10%">Balance/ Due</th>
                                                                <th class="text-right" style="width: 10%">Consolidated</th>
                                                            </tr>
                                                        </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr id='instalBtn_<%# Container.ItemIndex+1 %>' style="height: 30px !important; line-height: 1;" class="mainRow">
                                                            <td class="text-center" style="height: 30px !important; width: 36px; line-height: 1;">
                                                                <span id="lblInstallmentSrNo" class="vl"><%# Container.ItemIndex+1 %></span>
                                                                <asp:Label runat="server" ID="lblDueDate" Text='<%# Eval("DueDate") %>' CssClass="hide"></asp:Label>
                                                                <asp:Label runat="server" ID="lblStsMain" CssClass="hide"></asp:Label>
                                                            </td>
                                                            <td class="text-left" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblIcon" title="History" onclick="openClode(this)" CssClass="chks_h btn menu-icon vd_bd-grey-n vd_black-n iconhistory" Style="top: 0 !important; padding: 1px 6px"><i class="togalIcon fa fa-sort-amount-desc"></i></asp:Label>
                                                                <asp:CheckBox runat="server" ID="chkInstallment" CssClass="vd_check check-success" Style="height: 17px;" onchange="MainHeadCheck(this);" />
                                                                <asp:Label runat="server" ID="lblInstallmentName" CssClass="vl" Style="vertical-align: middle !important; margin-left: 4px !important;" Text='<%# Eval("MonthName") %>'></asp:Label>
                                                            </td>
                                                            <td class="text-right" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblInstallmentAmount" CssClass="calfee vl" Style="cursor: pointer !important" onmouseover="showfee(this)" onmouseout="hidefee(this)" Text='<%# Eval("headAmount") %>'></asp:Label>&nbsp;<asp:Label runat="server" Style="cursor: pointer !important" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" ID="HeadAmon" class="fa fa-info-circle"></asp:Label>
                                                                <div class="box hide row">
                                                                    <asp:Repeater ID="RepeaterHeadAmon" runat="server">
                                                                        <ItemTemplate>
                                                                            <div class="text-left col-sm-6" style="white-space: nowrap;">
                                                                                <asp:Label runat="server" ID="lblhNameh" Text='<%# Eval("Feehead") %>'> </asp:Label>
                                                                            </div>
                                                                            <div class="text-right col-sm-6">
                                                                                :&nbsp;&nbsp;<asp:Label runat="server" ID="lblhAmth" Text='<%# Bind("headAmount") %>' Style="text-align: right;"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </td>
                                                            <td class="text-right" style="cursor: pointer; line-height: 1; padding-top: 4px !important; padding-bottom: 4px !important;">
                                                                <asp:Label runat="server" ID="lblInstallmentDiscountPaid" CssClass="calDisc vl hide" Text='<%# Eval("PaidDiscount") %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblInstallmentDiscount" CssClass="calDisc vl" Style="cursor: pointer !important" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" Text='<%# Eval("Discount") %>'></asp:Label>&nbsp;<asp:Label runat="server" ID="HeadDiscount" Style="cursor: pointer !important" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" class="fa fa-info-circle"></asp:Label><br />
                                                                <asp:TextBox runat="server" ID="txtInstallmentdiscount" class="form-control-blue" placeholder="0.00" Style="text-align: right !important; height: 20px;" ReadOnly="true"></asp:TextBox>
                                                                <div class="box hide row">
                                                                    <asp:Repeater ID="RepeaterHeadDiscount" runat="server">
                                                                        <ItemTemplate>
                                                                            <div class="text-left col-sm-6" style="white-space: nowrap;">
                                                                                <asp:Label runat="server" ID="lblDiscNameh"
                                                                                    Text='<%# Eval("DiscountName") %>'>
                                                                                </asp:Label>
                                                                            </div>
                                                                            <div class="text-right col-sm-6">
                                                                                :&nbsp;&nbsp;<asp:Label runat="server" ID="lblDiscAmth" Text='<%# Bind("DiscountAmount") %>' Style="text-align: right;"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblPaidDiscAmth" CssClass="hide" Text='<%# Bind("PaidDiscAmount") %>' Style="text-align: right;"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </td>
                                                            <td class="text-right" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblInstallmentTotal" class="vl" Text='<%# Eval("PaybleAmount") %>'></asp:Label>
                                                            </td>
                                                            <td class="text-right" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lbInstallmentlPaid" class="vl" Text='<%# Eval("PaidAmount") %>'></asp:Label>
                                                                <asp:TextBox runat="server" ID="txtInstallmentPayable" class="form-control-blue" placeholder="0.00" Text='<%# Eval("DueAmount").ToString()=="0.00"?"":Eval("DueAmount") %>' Style="text-align: right !important; height: 20px;"  ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="text-right lasttd" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblInstallmentDue" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblhideInstallmentDue" class="hide" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                            </td>
                                                            <td class="text-right lasttd" style="line-height: 1;">
                                                                <asp:Label runat="server" ID="lblrecuring" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                            </td>
                                                            
                                                        </tr>
                                                        <tr class="hide" id='instalDeails_<%# Container.ItemIndex+1 %>'>
                                                            <td colspan="8" style="padding: 10px !important; box-shadow: 0px 16px 233px #a0a0a0  inset;">
                                                                <table class="table no-bm no-head-border table-bordered pro-table table-header-group" border="1" style="border-collapse: collapse; margin-top: 2px !important;">
                                                                    <asp:Repeater ID="rptFee" runat="server">
                                                                        <HeaderTemplate>
                                                                            <thead>
                                                                                <tr>
                                                                                    <th style="text-align: center !important; vertical-align: middle !important; width: 4%; padding: 2px 0px !important;" scope="col">#</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important; width: 29%;" scope="col">Fee Head</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 11.2%;" scope="col">Amount</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 11.2%;" scope="col">Discount</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 11.2%;" scope="col">Total</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 11.2%;" scope="col">Paid/ Payable</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important; width: 22.2%;" scope="col">Balance/ Due</th>
                                                                                </tr>
                                                                            </thead>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr runat="server" id="feeheadrow" class="childRow">
                                                                                <td style="text-align: center !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <span id="lblFeeheadSrNo"><%# Container.ItemIndex+1 %></span>
                                                                                    <asp:CheckBox runat="server" ID="chkInstallmentFee" CssClass="vd_check check-success chks_c" Style="height: 17px;" onchange="ChildCheck(this);" />
                                                                                    <asp:Label runat="server" ID="lblisFineFeeApply" CssClass="hide" Text='<%# Eval("isFineFee") %>'></asp:Label>

                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" CssClass="hide" ID="lblInstallmentId" Text='<%# Eval("MonthId") %>'></asp:Label>
                                                                                    <asp:Label runat="server" CssClass="hide" ID="lblFeeheadId" Text='<%# Eval("Feeheadid") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblFeehead" Text='<%# Eval("Feehead") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblStsChild" CssClass="hide"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" ID="lblFeeheadAmount" Text='<%# Eval("headAmount") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" ID="lblFeeHeadDiscount" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)" Text='<%# Eval("Discount") %>'></asp:Label>&nbsp;<asp:Label runat="server" ID="ChildDiscount" class="fa fa-info-circle" onmouseover="showdiscount(this)" onmouseout="hidediscount(this)"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblFeeHeadDiscountPaid" class="hide" Text='<%# Eval("PaidDiscount") %>'></asp:Label>
                                                                                    <asp:TextBox runat="server" ID="txtFeeHeadDiscount" class="form-control-blue" placeholder="0.00"  ReadOnly="true" Style="cursor: pointer !important; text-align: right !important; height: 20px;"></asp:TextBox>
                                                                                    <div class="box hide row">
                                                                                        <asp:Repeater ID="RepeaterChildDiscount" runat="server">
                                                                                            <ItemTemplate>
                                                                                                <div class="text-left col-sm-6" style="white-space: nowrap;">
                                                                                                    <asp:Label runat="server" ID="lblDiscNameC"
                                                                                                        Text='<%# Eval("DiscountName") %>'>
                                                                                                    </asp:Label>
                                                                                                </div>
                                                                                                <div class="text-right col-sm-6">
                                                                                                    :&nbsp;&nbsp;<asp:Label runat="server" ID="lblDiscAmtC" Text='<%# Bind("DiscountAmount") %>' Style="text-align: right;"></asp:Label>
                                                                                                    <asp:Label runat="server" ID="lblPaidDiscAmtC" CssClass="hide" Text='<%# Bind("PaidDiscAmount") %>' Style="text-align: right;"></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </div>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" ID="lblFeeHeadTotal" Text='<%# Eval("PaybleAmount") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" ID="lblFeeHeadPaid" Text='<%# Eval("PaidAmount") %>'></asp:Label><br>
                                                                                    <asp:TextBox runat="server" ID="txtFeeHeadPayable_h" class="form-control-blue" placeholder="0.00" Text='<%# Eval("DueAmount") %>' Style="text-align: right !important; height: 20px;"></asp:TextBox>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important; line-height: 1;">
                                                                                    <asp:Label runat="server" ID="lblFeeHeadBalanceShow" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblFeeHeadBalance" class="hide vl" Text='<%# Eval("DueAmount") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>

                                                                    </asp:Repeater>
                                                                </table>
                                                                <table class="table table-hover no-bm no-head-border table-bordered pro-table table-header-group" border="1" style="border-collapse: collapse; margin-top: 20px !important;">

                                                                    <asp:Repeater ID="rptHistory" runat="server">
                                                                        <HeaderTemplate>
                                                                            <thead>

                                                                                <tr>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important; width: 30px;" scope="col">#</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important;" scope="col">Date</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important;" scope="col">Receipt No.</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important;" scope="col">Installment</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important;" scope="col">Mode</th>
                                                                                    <th style="text-align: left !important; vertical-align: middle !important;" scope="col">Status</th>
                                                                                    <th style="text-align: right !important; vertical-align: middle !important;" scope="col">Paid Amount</th>
                                                                                </tr>
                                                                            </thead>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <span id="lblHstorySrNo"><%# Container.ItemIndex+1 %></span>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="lblHstoryDepositDate" Text='<%# Eval("DepositDate") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="Label2" Text='<%# Bind("ReceiptNo") %>' CssClass="onprint2 hide"></asp:Label>
                                                                                    <span class="hidden-print">
                                                                                        <asp:HyperLink ID="HylnkReceiptNo" runat="server" CssClass="onprint" Style="color: #0019cc;" Text='<%# Bind("ReceiptNo") %>'
                                                                                            NavigateUrl='<%# "FeeReceiptAllDuplicate.aspx?RecieptSrNo="+Eval("ReceiptNo").ToString().Replace("/","__")+"$"+Eval("SessionName").ToString()+"$"+Eval("BranchCode").ToString() %>' Target="_blank">
                                                                                        </asp:HyperLink>
                                                                                    </span>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="lbllHstoryInstallment" Text='<%# Eval("installmentName") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="lblHstoryMode" Text='<%# Eval("ModeOfPayment") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="lblHstorySatus" Text='<%# Eval("receiptStatus") %>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right !important; vertical-align: middle !important;">
                                                                                    <asp:Label runat="server" ID="lblHstorPaid" Text='<%# Eval("PaidAmount") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:Repeater>
                                                <tr>
                                                                <th class="text-right " style="width: 27%;" colspan="2">Total</th>
                                                                <th class="text-right" style="width: 10%"><asp:Label runat="server" ID="lblInstallmentFee_total"></asp:Label></th>
                                                                <th class="text-right" style="width: 10%"><asp:Label runat="server" ID="lblDisvount_total"></asp:Label></th>
                                                                <th class="text-right" style="width: 10%"><asp:Label runat="server" ID="lblTotal_total"></asp:Label></th>
                                                                <th class="text-right" style="width: 10%"><asp:Label runat="server" ID="lblPaid_total"></asp:Label></th>
                                                                <th class="text-right" style="width: 10%"><asp:Label runat="server" ID="lblBalance_total"></asp:Label></th>
                                                                <th class="text-right" style="width: 10%"><asp:Label runat="server" ID="lblConsolidated_total"></asp:Label></th>
                                                            </tr>
                                            </table>
                                        </div>
                                    </fieldset>

                                    <div class="col-sm-12 hidden-print">
                                        <div><i class="fa fa-credit-card"></i>&nbsp;Payment Details</div>
                                        <div class="col-sm-3" style="border: 1px solid" id="divFeeDetails">
                                            <div style="padding: 5px !important;">
                                                <div style="width: 100%; font-size: 12px !important; margin-top: 6px; padding: 0 0;">
                                                    <div class="col-sm-12  text-left" style="background: #fafafa; font-weight: 700; color: #dc4448; padding: 5px;">
                                                        <label>Total Payable </label>
                                                        <asp:Label ID="txtTotalPaid" runat="server" CssClass="validateTextMode" Text="0.00" Style="float: right; color: #000; width: 100px; text-align: right;" onblur="TotalPayable(this)"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnTotalPaid" />
                                                    </div>
                                                    <div class="col-sm-12  mgbt-xs-15 text-center" id="divSubmit">
                                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" OnClick="lnkSubmit_Click" OnClientClick="ValidateTextBox('.validateTextMode');ValidateDropdown('.validatedrpMode');return validationReturn();RemoveHideFromPayment(1);" Style="margin-top: 7px;"><i class="fa fa-floppy-o"></i> Submit</asp:LinkButton>
                                                        <button id="lnkSubmitPayStack" class="button form-control-blue hide" onclick="payWithPaystack()" style="margin-top: 7px;"><i class="fa fa-floppy-o"></i>Submit</button>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-sm-6" id="divOnlineDetails">
                                                <div class="col-sm-12  text-center" style="background: #fafafa; font-weight: 700; color: #dc4448; padding: 5px;">
                                                    <asp:DropDownList runat="server" ID="ddlPaymentGateway" onchange="PaymentGatewayChange()">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-12 mgbt-xs-15" style="background: #fafafa; font-weight: 700; font-size: 12px; padding-right: 5px; padding-left: 2px; margin-top: 10px; padding-top: 7px; border: solid 1px #ccc;">
                                                    <div class="col-sm-12  text-left" style="padding: 0; padding-right: 3px;">
                                                        <img id="imgLogo" alt="" style="height: 100px;" />
                                                    </div>
                                                    <div class="col-sm-12 text-left" id="divCharges" style="padding: 0; padding-left: 5px; min-height: 142px; overflow: auto;">
                                                    </div>
                                                </div>
                                            </div>
                                        <div id="Div1" runat="server" style="left: 75px">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="display: none; visibility: hidden">
                        <asp:Label runat="server" ID="data_key"></asp:Label>
                        <asp:Label runat="server" ID="data_email"></asp:Label>
                        <asp:Label runat="server" ID="data_PseudoUniqueReference"></asp:Label>
                        <asp:Label runat="server" ID="data_txnNo"></asp:Label>
                        <asp:HiddenField runat="server" ID="hdnFirstName" />
                        <asp:HiddenField runat="server" ID="hdnLastName" />
                        <asp:HiddenField runat="server" ID="hdnTxtNos" />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnPayStack" runat="server" OnClick="btnPayStack_Click" Text="Submit"></asp:Button>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnPayStack" />
                            </Triggers>
                        </asp:UpdatePanel>

                         <%--For Atom--%>
                         <asp:HiddenField runat="server" ID="hdn_merchTxnIds" />
                         <asp:HiddenField runat="server" ID="hdn_atomTokenId" />
                         <asp:HiddenField runat="server" ID="hdn_merchId" />
                         <asp:HiddenField runat="server" ID="hdn_custEmail" />
                         <asp:HiddenField runat="server" ID="hdn_custMobile" />
                         <asp:HiddenField runat="server" ID="hdn_returnUrl" />
                         <asp:HiddenField runat="server" ID="hdn_Amt" />
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                
        
        function PaymentGatewayChange() {
            if ($("[id*=ddlPaymentGateway]").val() == '') {
                $("#imgLogo").addClass("hide");
                $("#divCharges").html('');
                $("[id*=lnkSubmit]").addClass("hide");
                $("#lnkSubmitPayStack").addClass("hide");
                $("[id*=ddlPaymentGateway]").addClass('validatedrpMode');
            }
            else {
                if ($("[id*=ddlPaymentGateway]").val() == 'PayStack') {
                    $("[id*=lnkSubmit]").addClass("hide");
                    $("#lnkSubmitPayStack").removeClass("hide");
                }
                else {
                    $("[id*=lnkSubmit]").removeClass("hide");
                    $("#lnkSubmitPayStack").addClass("hide");
                }
                if ($("[id*=ddlPaymentGateway]").val() == 'PayUMoney') {
                    $("#imgLogo").removeClass("hide");
                    var maindata = '<%=HttpContext.Current.Session["PayUMoney"] %>';
                    var data = maindata.split("##");
                    $("#imgLogo").attr("src", "../uploads/CollegeLogo/" + data[0]);
                    $("#divCharges").html(data[1]);
                }
                if ($("[id*=ddlPaymentGateway]").val() == 'Eazypay') {
                    $("#imgLogo").removeClass("hide");
                    var maindata = '<%=HttpContext.Current.Session["Eazypay"] %>';
                    var data = maindata.split("##");
                    $("#imgLogo").attr("src", "../uploads/CollegeLogo/" + data[0]);
                    $("#divCharges").html(data[1]);
                }
                if ($("[id*=ddlPaymentGateway]").val() == 'PayStack') {
                    $("#imgLogo").removeClass("hide");
                    var maindata = '<%=HttpContext.Current.Session["PayStack"] %>';
                    alert(maindata);
                    var data = maindata.split("##");
                    $("#imgLogo").attr("src", "../uploads/CollegeLogo/" + data[0]);
                    $("#divCharges").html(data[1]);
                }
            }
        }
        function payWithPaystack() {
            $("[id*=hdnTxtNos]").val("");
            var data_key = $("[id*=data_key]").html();
            var data_email = $("[id*=data_email]").html();
            var data_amount = (parseFloat($("[id*=HdnTotalPaidAmount]").val()) * 100);
            var data_full_name = $("[id*=grdStRecord] tr:eq(1)").find("td:eq(1) span").html();
            var data_firstname = $("#hdnFirstName").val();
            var data_lastname = $("#hdnLastName").val();
            var data_value = $("[id*=grdStRecord] tr:eq(1)").find("td:eq(6) span").html();
            var data_PseudoUniqueReference = parseInt($("[id*=data_PseudoUniqueReference]").html());
            //alert(data_key+" | "+data_email+" | "+data_amount+" | "+data_full_name+" | "+data_firstname+" | "+data_lastname+" | "+data_value+" | "+data_PseudoUniqueReference);

            var handler = PaystackPop.setup({
                key: data_key,
                email: data_email,
                amount: data_amount,
                ref: '' + Math.floor((Math.random() * data_PseudoUniqueReference) + 1), // generates a pseudo-unique reference. Please replace with a reference you generated. Or remove the line entirely so our API will generate one for you
                firstname: data_firstname,
                lastname: data_lastname,
                // label: "Optional string that replaces customer email"
                metadata: {
                    custom_fields: [
                        {
                            display_name: "Mobile Number",
                            variable_name: "mobile_number",
                            value: data_value
                        }
                    ]
                },
                callback: function (response) {
                    $("[id*=hdnTxtNos]").val(response.reference);
                    $("#<%=btnPayStack.ClientID %>").click();
                },
                onClose: function () {
                    alert('window closed');
                }
            });
            handler.openIframe();
        }
            </script>
            <script>
                function printDiv() {
                    $(".iconhistory").each(function () {
                        openClode(this);
                    });
                    $(".onprint").addClass('hide');
                    $(".onprint2").removeClass('hide');
                    var headContent = document.getElementsByTagName('head')[0].innerHTML;
                    var divContents = document.getElementById("printdiv").innerHTML;
                    var printWindow = window.open('', '', 'height=700,width=1300, class="tbls"');
                    printWindow.document.write('<html><head><title"CompositFee"</title>' + headContent + '</head>');
                    printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');
                    printWindow.document.close();
                    setTimeout(function () {
                        printWindow.print();
                        printWindow.close();
                        $(".iconhistory").each(function () {
                            openClode(this);
                        });
                        $(".onprint").removeClass('hide');
                        $(".onprint2").addClass('hide');
                    }, 1500);
                    return false;
                }
                function ChangeRowColor() {
                    $(".mainRow").each(function () {
                        var due1 = $(this).find('td:eq(6) span:eq(1)').html();
                        var Pending = $(this).find('td:eq(0) span:eq(2)').html();
                        var balance1 = parseFloat(due1 == "" ? "0" : due1);
                        if (balance1 == 0 && Pending != "Pending") {
                            var tdlen1 = $(this).find('td').length;
                            for (var i = 0; i < tdlen1; i++) {
                                $(this).find('td:eq(' + i + ')').addClass('rowGreen');
                            }
                            $(this).find('td:eq(3) input[type=text]').addClass('hide');
                            $(this).find('td:eq(5) input[type=text]').addClass('hide');
                            $(this).find('td:eq(8) textarea').addClass('hide');
                        }
                        else if (balance1 == 0 || balance1 != 0 && Pending == "Pending") {
                            var tdlen1 = $(this).find('td').length;
                            for (var i = 0; i < tdlen1; i++) {
                                $(this).find('td:eq(' + i + ')').addClass('rowYellow');
                            }
                            $(this).find('td:eq(3) input[type=text]').addClass('hide');
                            $(this).find('td:eq(5) input[type=text]').addClass('hide');
                            $(this).find('td:eq(8) textarea').addClass('hide');
                        }
                        else {
                            var tdlen1 = $(this).find('td').length;
                            for (var i = 0; i < tdlen1; i++) {
                                $(this).find('td:eq(' + i + ')').removeClass('rowGreenChild');
                            }
                        }

                    });
                    $(".childRow").each(function () {
                        var due = $(this).find('td:eq(6) span:eq(1)').html();
                        var Pending = $(this).find('td:eq(1) span:eq(3)').html();
                        var balance = parseFloat(due == "" ? "0" : due);
                        if (balance == 0 && Pending != "Pending") {
                            var tdlen = $(this).find('td').length;
                            for (var i = 0; i < tdlen; i++) {
                                $(this).find('td:eq(' + i + ')').addClass('rowGreenchild');
                            }
                            $(this).find('td:eq(3) input[type=text]').addClass('hide');
                            $(this).find('td:eq(5) input[type=text]').addClass('hide');
                            $(this).find('td:eq(7) textarea').addClass('hide');
                        }
                        else if (balance == 0 || balance != 0 && Pending == "Pending") {
                            var tdlen = $(this).find('td').length;
                            for (var i = 0; i < tdlen; i++) {
                                $(this).find('td:eq(' + i + ')').addClass('rowYellow');
                            }
                            $(this).find('td:eq(3) input[type=text]').addClass('hide');
                            $(this).find('td:eq(5) input[type=text]').addClass('hide');
                            $(this).find('td:eq(7) textarea').addClass('hide');
                        }
                        else {
                            var tdlen = $(this).find('td').length;
                            for (var i = 0; i < tdlen; i++) {
                                $(this).find('td:eq(' + i + ')').removeClass('rowGreenchild');
                            }
                        }
                    });
                }
                function totalsAmt() {
                    var totalamt = 0;
                    var len = $(".chks_h").length;
                    for (var i = 0; i < len; i++) {
                        var sts = $("#instalBtn_" + (i + 1)).find('td:eq(1) input[type=checkbox]').prop("checked");
                        if (sts) {
                            var val = $("#instalBtn_" + (i + 1)).find('td:eq(5) input[type=text]').val();
                            totalamt += parseFloat(val == "" ? "0" : val);
                        }
                    }
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtTotalPaid").html(totalamt.toFixed(2));
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnTotalPaid").val(totalamt.toFixed(2));
                }
                
                function MainHeadCheck(tis) {

                    var headRowid = $(tis).closest('tr').attr('id');
                    var ischecked = $("#" + headRowid).find('td:eq(1) input[type=checkbox]').prop("checked");

                    $("#" + headRowid).find('td:eq(3) input[type=text]').val('');
                    $("#" + headRowid).find('td:eq(5) input[type=text]').val('');

                    var lblInstallmentAmt_h = parseFloat($("#" + headRowid).find('td:eq(2) span:eq(0)').html());
                    var lblDiscount_h = parseFloat($("#" + headRowid).find('td:eq(3) span:eq(1)').html());
                    var total_h = (lblInstallmentAmt_h - lblDiscount_h).toFixed(2);
                    $("#" + headRowid).find('td:eq(4) span:eq(0)').html(total_h);
                    var lblpaid_h = parseFloat($("#" + headRowid).find('td:eq(5) span:eq(0)').html());
                    var payable = (total_h - lblpaid_h).toFixed(2);

                    if (ischecked) {
                        $("#" + headRowid).find('td:eq(6) span:eq(0)').html("0.00");
                        $("#" + headRowid).find('td:eq(5) input[type=text]').val(payable);
                    }
                    else {
                        $("#" + headRowid).find('td:eq(6) span:eq(0)').html(payable);
                        $("#" + headRowid).find('td:eq(5) input[type=text]').val("0.00");
                    }


                    var rowid = $(tis).closest('tr').attr('id');
                    var totalrec = 0; var idmached = 0;
                    $(".mainRow").each(function () {
                        var childRowid = "instalDeails_" + $(this).attr('id').split("_")[1];
                        var len = $("#" + childRowid).find('td table tbody tr').length;
                        if (rowid == $(this).attr('id')) {
                            idmached = 1;
                        }
                        var ischecked2 = false;
                        if (idmached > 0) {
                            if (ischecked && rowid == $(this).attr('id')) {
                                $(this).find('td:eq(1) input[type=checkbox]').prop("checked", true);
                                var fee = parseFloat($(this).find('td:eq(2) span:eq(0)').html());
                                var paidDisc = parseFloat($(this).find('td:eq(3) span:eq(1)').html());
                                $(this).find('td:eq(4) span:eq(0)').html((fee - paidDisc).toFixed(2));
                                var totals = parseFloat($(this).find('td:eq(4) span:eq(0)').html());
                                var paid = parseFloat($(this).find('td:eq(5) span:eq(0)').html());
                                $(this).find('td:eq(5) input[type=text]').val((totals - paid).toFixed(2));
                                $(this).find('td:eq(6) span:eq(0)').html("0.00");
                                ischecked2 = true;
                            }
                            else {
                                $(this).find('td:eq(1) input[type=checkbox]').prop("checked", false);
                                $(this).find('td:eq(3) input[type=text]').val('');
                                $(this).find('td:eq(5) input[type=text]').val('');
                                $(this).find('td:eq(8) textarea').val('');
                                var fee = parseFloat($(this).find('td:eq(2) span:eq(0)').html());
                                var paidDisc = parseFloat($(this).find('td:eq(3) span:eq(1)').html());
                                $(this).find('td:eq(4) span:eq(0)').html((fee - paidDisc).toFixed(2));
                                var totals = parseFloat($(this).find('td:eq(4) span:eq(0)').html());
                                var paid = parseFloat($(this).find('td:eq(5) span:eq(0)').html());
                                $(this).find('td:eq(5) input[type=text]').val('');
                                $(this).find('td:eq(6) span:eq(0)').html((totals - paid).toFixed(2));
                                ischecked2 = false;
                            }

                            
                        }
                        else {

                            $(this).find('td:eq(1) input[type=checkbox]').prop("checked", true);
                            var fee = parseFloat($(this).find('td:eq(2) span:eq(0)').html());
                            var paidDisc = parseFloat($(this).find('td:eq(3) span:eq(1)').html());
                            $(this).find('td:eq(4) span:eq(0)').html((fee - paidDisc).toFixed(2));
                            var totals = parseFloat($(this).find('td:eq(4) span:eq(0)').html());
                            var paid = parseFloat($(this).find('td:eq(5) span:eq(0)').html());
                            $(this).find('td:eq(5) input[type=text]').val((totals - paid).toFixed(2));
                            $(this).find('td:eq(6) span:eq(0)').html("0.00");
                            ischecked2 = true;
                            
                        }
                        for (var i = 0; i < len; i++) {
                            $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val('');
                            $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val('');
                            var dues = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(1)').html());
                            var discount = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                            var paiddiscount = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(2)').html());
                            var ApplyFine = $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(0) span:eq(2)').html();
                            if (isNaN(discount)) {
                                discount = 0;
                            }
                            if (isNaN(paiddiscount)) {
                                paiddiscount = 0;
                            }
                            if (dues == 0 && discount != paiddiscount) {
                                if (ischecked2) {
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", true);
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').removeAttr('disabled');;
                                    var feeh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                                    var paidDisch = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((feeh - paidDisch).toFixed(2));
                                    var totalsh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html());
                                    var paidh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html("0.00");
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val((totalsh - paidh).toFixed(2));
                                    if (ApplyFine == "ApplyFine") {
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').attr('disabled', 'disabled');
                                    }
                                }
                                else {
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", false);
                                    var feeh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                                    var paidDisch = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((feeh - paidDisch).toFixed(2));
                                    var totalsh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html());
                                    var paidh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html((totalsh - paidh).toFixed(2));
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val('');
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').attr('disabled', 'disabled');
                                }
                            }
                            else if (dues > 0) {
                                if (ischecked2) {
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", true);
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').removeAttr('disabled');;
                                    var feeh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                                    var paidDisch = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((feeh - paidDisch).toFixed(2));
                                    var totalsh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html());
                                    var paidh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html("0.00");
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val((totalsh - paidh).toFixed(2));
                                    if (ApplyFine == "ApplyFine") {
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').attr('disabled', 'disabled');
                                    }
                                }
                                else {
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", false);
                                    var feeh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                                    var paidDisch = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html((feeh - paidDisch).toFixed(2));
                                    var totalsh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html());
                                    var paidh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html((totalsh - paidh).toFixed(2));
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val('');
                                    $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').attr('disabled', 'disabled');
                                }
                            }
                            else {
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked", false);
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val('');
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val('');
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(7) textarea').val('');
                            }
                        }
                    });
                    
                    totalsAmt();
                }
                function ChildCheck(tis) {

                    var childRowid = $(tis).closest('tr').attr('id');
                    $("#" + childRowid).find('td:eq(3) input[type=text]').val('');
                    $("#" + childRowid).find('td:eq(5) input[type=text]').val('');
                    var ischk = $("#" + childRowid).find('td:eq(0) input[type=checkbox]').prop("checked");
                    var lblHeadAmt = parseFloat($("#" + childRowid).find('td:eq(2) span:eq(0)').html());
                    var lblDiscount = parseFloat($("#" + childRowid).find('td:eq(3) span:eq(0)').html());
                    var total = (lblHeadAmt - lblDiscount).toFixed(2);
                    $("#" + childRowid).find('td:eq(3) input[type=text]').val('');
                    if (parseFloat($(tis).val()) > total) {
                        $(tis).val('');
                    }
                    var txtDiscount = 0;
                    var lbltotal = (total - txtDiscount).toFixed(2);
                    $("#" + childRowid).find('td:eq(4) span:eq(0)').html(lbltotal);
                    var lblpaid = parseFloat($("#" + childRowid).find('td:eq(5) span:eq(0)').html());
                    var lbltotaldue = (lbltotal - lblpaid).toFixed(2);
                    if (ischk) {
                        $("#" + childRowid).find('td:eq(5) input[type=text]').val(lbltotaldue);
                        $("#" + childRowid).find('td:eq(6) span:eq(0)').html("0.00");
                    }
                    else {
                        $("#" + childRowid).find('td:eq(6) span:eq(0)').html(lbltotaldue);
                        $("#" + childRowid).find('td:eq(5) input[type=text]').val('');
                    }

                    var trid = $(tis).closest('table').closest('tr').attr('id');
                    var len = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr').length;
                    var discountPls = 0; var cnt = 0;
                    var fee = 0; var lblDiscount = 0; var txtDiscount = 0; var lbltotal = 0; var lblPaid = 0; var txtPaid = 0; var lblDue = 0;
                    for (var i = 0; i < len; i++) {
                        discountPls = discountPls + 0;
                        var ff = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(0) input[type=checkbox]').prop("checked");
                        if (ff) {

                            cnt++;
                        }
                        fee += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(2) span:eq(0)').html());
                        lblDiscount += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(3) span:eq(0)').html());
                        var dsc = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(3) input[type=text]').val();
                        txtDiscount += parseFloat(dsc == "" ? "0" : dsc);
                        lbltotal += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(4) span:eq(0)').html());
                        lblPaid += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(5) span:eq(0)').html());
                        var payble = $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(5) input[type=text]').val();
                        lblDue += parseFloat($("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + i + ')').find('td:eq(6) span:eq(0)').html());
                        txtPaid += parseFloat(payble == "" ? "0" : payble);
                    }
                    var HeadtrId = "instalBtn_" + $(tis).closest('table').closest('tr').attr('id').split('_')[1];
                    if (cnt > 0) {
                        $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]').prop("checked", true);
                    }
                    else {
                        $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]').prop("checked", false);
                        for (var k = 0; k < len; k++) {
                            $("#" + trid).find('td:eq(0) table:eq(0) tbody tr:eq(' + k + ')').find('td:eq(0) input[type=checkbox]').attr('readonly', 'readonly');
                        }
                        var tiss = $("#" + HeadtrId).find('td:eq(1) input[type=checkbox]');
                        MainHeadCheck(tiss);
                    }
                    
                    $("#" + HeadtrId).find('td:eq(3) input[type=text]').val(txtDiscount == 0 ? "" : txtDiscount.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(4) span:eq(0)').html(lbltotal.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(5) span:eq(0)').html(lblPaid.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(5) input[type=text]').val(txtPaid == 0 ? "" : txtPaid.toFixed(2));
                    $("#" + HeadtrId).find('td:eq(6) span:eq(0)').html(lblDue.toFixed(2));
                    totalsAmt();
                }
                function TotalPayable(tis) {
                    if (isNaN(parseFloat($(tis).val()))) {
                        $(tis).val('');
                    }
                    else if (parseFloat($(tis).val()) == 0) {
                        $(tis).val('');
                    }
                    var maivval1 = parseFloat($(tis).val() == "" ? "0" : $(tis).val());
                    $(".mainRow").each(function () {
                        var headRowid = $(this).attr('id');
                        $("#" + headRowid).find('td:eq(3) input[type=text]').val('');
                        $("#" + headRowid).find('td:eq(5) input[type=text]').val('');
                        $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', false);

                        var payable_h = 0; var due_h = 0; var thisval = 0;
                        var lblfee_h = parseFloat($("#" + headRowid).find('td:eq(2) span:eq(0)').html());
                        var lbldiscount_h = parseFloat($("#" + headRowid).find('td:eq(3) span:eq(1)').html());
                        var lblTotal_h = (lblfee_h.toFixed(2) - lbldiscount_h.toFixed(2)).toFixed(2);
                        var lblpaid_h = parseFloat($("#" + headRowid).find('td:eq(5) span:eq(0)').html());
                        var actualDue = parseFloat($("#" + headRowid).find('td:eq(6) span:eq(1)').html());
                        payable_h = (lblTotal_h - lblpaid_h).toFixed(2);
                        if (actualDue > 0) {
                            var txttotal_h = 0;

                            if (maivval1 >= payable_h) {
                                $("#" + headRowid).find('td:eq(6) span:eq(0)').html('0.00');
                                $("#" + headRowid).find('td:eq(5) input[type=text]').val(payable_h);
                                maivval1 = maivval1 - payable_h;
                                txttotal_h = payable_h;
                                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', true);
                            }
                            else if (maivval1 < payable_h && maivval1 > 0) {
                                $("#" + headRowid).find('td:eq(6) span:eq(0)').html((payable_h - maivval1).toFixed(2));
                                $("#" + headRowid).find('td:eq(5) input[type=text]').val(maivval1);
                                txttotal_h = maivval1;
                                maivval1 = 0;
                                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', true);
                            }
                            else {
                                $("#" + headRowid).find('td:eq(6) span:eq(0)').html(payable_h);
                                $("#" + headRowid).find('td:eq(5) input[type=text]').val('');
                                $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', false);
                                txttotal_h = 0;
                                maivval1 = 0;
                            }
                            var childRowid = "instalDeails_" + $(this).closest('tr').attr('id').split("_")[1];
                            var len = $("#" + childRowid).find('td table tbody tr').length;
                            if (txttotal_h > 0) {
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) input[type=text]').val('');
                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val('');
                                var finetype = 0; var totalDuef = 0;
                                for (var j = 0; j < len; j++) {
                                    var ApplyFine = $("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(0) span:eq(2)').html();
                                    if (ApplyFine == "ApplyFine") {
                                        var lblPayablef = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(4) span:eq(0)').html());
                                        var lblPaidf = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(5) span:eq(0)').html());
                                        var duh = (lblPayablef - lblPaidf).toFixed(2);
                                        if (duh > 0) {
                                            finetype = finetype + 1;
                                            totalDuef = parseFloat(totalDuef) + parseFloat(duh);
                                        }
                                    }

                                }
                                if (txttotal_h < totalDuef) {
                                    var boxValue = parseFloat($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtTotalPaid").html());
                                    boxValue = boxValue - txttotal_h;
                                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtTotalPaid").html(boxValue.toFixed(2));
                                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnTotalPaid").val(boxValue.toFixed(2));

                                    $("#" + headRowid).find('td:eq(6) span:eq(0)').html(payable_h);
                                    $("#" + headRowid).find('td:eq(5) input[type=text]').val('');
                                    $("#" + headRowid).find('td:eq(1)  input[type=checkbox]').prop('checked', false);

                                    txttotal_h = 0;
                                    maivval1 = 0;
                                    for (var i = 0; i < len; i++) {
                                        var lblfee = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(2) span:eq(0)').html());
                                        var lbldisc = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(3) span:eq(0)').html());
                                        var lblPayablefh = (lblfee - lbldisc).toFixed(2);
                                        $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html(lblPayablefh);
                                        var lblPaidfh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                                        var tDuefh = (lblPayablefh - lblPaidfh).toFixed(2);
                                        $("#" + childRowid).find('td table tbody tr:eq(' + 2 + ') td:eq(6) span:eq(0)').html(tDuefh);
                                        $("#" + childRowid).find('td table tbody tr:eq(' + 2 + ') td:eq(5) input[type=text]').val('');
                                        $("#" + childRowid).find('td table tbody tr:eq(' + 2 + ') td:eq(0)  input[type=checkbox]').prop('checked', false);
                                    }
                                }
                                else {
                                    if (totalDuef > 0 && txttotal_h >= totalDuef) {
                                        for (var j = (len - finetype) ; j < len; j++) {
                                            var lblPayablefh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(4) span:eq(0)').html());
                                            var lblPaidfh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(5) span:eq(0)').html());
                                            var due2 = (lblPayablefh - lblPaidfh).toFixed(2);


                                            $("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(0)  input[type=checkbox]').prop('checked', true);
                                            if (txttotal_h >= due2) {
                                                $("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(5) input[type=text]').val(due2);
                                                $("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(6) span:eq(0)').html("0.00")
                                                txttotal_h = txttotal_h - due2;
                                            }
                                        }
                                        len = (len - finetype);
                                    }

                                    for (var i = 0; i < len; i++) {
                                        var lblPayable = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(4) span:eq(0)').html());
                                        var lblPaid = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) span:eq(0)').html());
                                        var lblActualDue = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(1)').html());
                                        if (lblActualDue > 0) {
                                            var totalDue = (lblPayable - lblPaid).toFixed(2);
                                            if (txttotal_h >= totalDue) {
                                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val(totalDue);
                                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html("0.00");
                                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(0)  input[type=checkbox]').prop('checked', true);
                                                txttotal_h = txttotal_h - totalDue;
                                            }
                                            else if (txttotal_h < totalDue && txttotal_h > 0) {
                                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val(txttotal_h);
                                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html((totalDue - txttotal_h).toFixed(2));
                                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(0)  input[type=checkbox]').prop('checked', true);
                                                txttotal_h = txttotal_h - totalDue;
                                                if (txttotal_h < 0) {
                                                    txttotal_h = 0;
                                                }
                                            }
                                            else if (txttotal_h == 0) {
                                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(6) span:eq(0)').html(totalDue);
                                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(5) input[type=text]').val('');
                                                $("#" + childRowid).find('td table tbody tr:eq(' + i + ') td:eq(0)  input[type=checkbox]').prop('checked', false);
                                                txttotal_h = 0;
                                            }
                                        }
                                    }

                                }
                            }
                            else {
                                for (var j = 0; j < len; j++) {
                                    var lblfee = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(2) span:eq(0)').html());
                                    var lbldisc = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(3) span:eq(0)').html());
                                    var lblPayablefh = (lblfee - lbldisc).toFixed(2);
                                    $("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(4) span:eq(0)').html(lblPayablefh);
                                    var lblPaidfh = parseFloat($("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(5) span:eq(0)').html());
                                    var tDuefh = (lblPayablefh - lblPaidfh).toFixed(2);
                                    $("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(6) span:eq(0)').html(tDuefh);
                                    $("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(5) input[type=text]').val('');
                                    $("#" + childRowid).find('td table tbody tr:eq(' + j + ') td:eq(0)  input[type=checkbox]').prop('checked', false);
                                }
                            }
                        }
                    });
                }



                function showfee(tis) {
                    $(tis).closest('td').find('div:eq(0)').removeClass('hide');
                }
                function hidefee(tis) {
                    $(tis).closest('td').find('div:eq(0)').addClass('hide');
                }
                function showdiscount(tis) {
                    $(tis).closest('td').find('div:eq(0)').removeClass('hide');
                }
                function hidediscount(tis) {
                    $(tis).closest('td').find('div:eq(0)').addClass('hide');
                }
                function openClode(tis) {
                    if ($(tis).closest('tr').find("td:eq(1) i.togalIcon").hasClass("fa fa-sort-amount-desc")) {
                        $(tis).closest('tr').find("td:eq(1) i.togalIcon").removeClass("fa fa-sort-amount-desc")
                        $(tis).closest('tr').find("td:eq(1) i.togalIcon").addClass("fa fa-sort-amount-asc")
                    }
                    else {
                        $(tis).closest('tr').find("td:eq(1) i.togalIcon").removeClass("fa fa-sort-amount-asc")
                        $(tis).closest('tr').find("td:eq(1) i.togalIcon").addClass("fa fa-sort-amount-desc")
                    }
                    var tisId = $(tis).closest('tr').attr('id');
                    tisId = tisId.split('_')[1];
                    var bodyId = "instalDeails_" + tisId;
                    if ($("#" + bodyId).hasClass('hide')) {
                        $("#" + bodyId).removeClass('hide');
                    }
                    else {
                        $("#" + bodyId).addClass('hide');
                    }
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkSubmit" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
