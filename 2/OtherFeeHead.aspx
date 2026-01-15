<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="OtherFeeHead.aspx.cs" Inherits="OtherFeeHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
    <style>
        @media print
        {    
            .no-print, .no-print *
            {
                display: none !important;
            }
        }
        @import "compass/css3";

        #drpPromoToManual {
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
        }

        .term1 > tbody > tr > td {
            padding: 1.1px 5px !important;
        }

        .term1 > tbody > tr > th {
            padding: 1.1px 5px !important;
        }

        .txt-rep-title-12-b {
            margin: 0px !important;
            font-size: 11px !important;
            font-weight: 600 !important;
            color: #000 !important;
        }
        /*.mp-table > tbody > tr > td, .mp-table > tfoot > tr > td {
            font-size: 20px !important;
            padding: 2px 10px !important;
        }*/
        .term1 > .p-pad-25 > span {
            padding: 1.1px 5px !important;
            font-size: 11px !important;
            color: #000 !important;
        }

        .term1 > .mp-table > tbody > tr > td span {
            font-size: 11px !important;
            padding: 1.1px 5px !important;
            color: #000 !important;
        }


        .term1 > .mp-table > tbody > tr > td, .mp-table > tfoot > tr > td {
            font-size: 12px !important;
            padding: 1px 5px !important;
            color: #000 !important;
        }


        .term2 > .p-pad-25 > span {
            padding: 1.5px 5px !important;
            font-size: 11px !important;
            color: #000 !important;
        }

        .term2 > .mp-table > tbody > tr > td span {
            font-size: 11px !important;
            color: #000 !important;
        }

        .term2 > .mp-table > tbody > tr > td, .mp-table > tfoot > tr > td {
            font-size: 11px !important;
            color: #000 !important;
        }

        .term2 > tbody > tr > td {
            padding: 1.5px 5px !important;
        }

        .term2 > tbody > tr > th {
            padding: 1.5px 5px !important;
        }
        .sentence-case {    text-transform: initial !important;
        }
        .table-responsive2 {
            border:0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top ">
                    <div class="panel-body ">
                        <div class="col-sm-12  no-padding ">
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">From Class&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:DropDownList ID="drpFromClass" runat="server" CssClass="form-control-blue">
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">To Class&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:DropDownList ID="drpToClass" runat="server" CssClass="form-control-blue">
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:DropDownList ID="ddlGender" runat="server">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                        <asp:ListItem Value="1">Male</asp:ListItem>
                                        <asp:ListItem Value="2">Female</asp:ListItem>
                                        <asp:ListItem Value="3">Transgender</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                 <label class="control-label">Fee Head&nbsp;<span class="vd_red">*</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label class="checkbox-inline"><input type="checkbox" id="IsSingleHead" onchange="IsSingleHeadFn(this)" style="width: 16px; height: 16px; margin: 3px -14px;">&nbsp;Is Single Head For Class</label></label>
                                <div class="">
                                    <asp:TextBox ID="txtFeeHead" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Amount&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control-blue validatetxt" onkeypress="return isNumber(event)"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15 hide" id="divRemark">
                                <label class="control-label">Remark&nbsp;<span class="vd_red"></span></label>
                                <div class="">
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12  half-width-50  btn-a-devices-6-p6 mgbt-xs-15" style="margin-top: 26px;">
                                <div class='col -sm-12 '><input type='button' id='lnkSubmit' class='button form-control-blue' value='Submit' />&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='lnkclear' class='button form-control-blue hide' value='Clear' /></div>
                                <asp:HiddenField runat="server" ID="hdnId" />
                                <asp:HiddenField runat="server" ID="hdnClassId" />
                            </div>
                            <div class="col-sm-4  half-width-50  btn-a-devices-6-p6 mgbt-xs-15" style="margin-top: 26px;">
                                <div id="msgbox" runat="server" style="left: 155px;"></div>
                            </div>
                            
                        </div>
                        
                        <div class="col-sm-12 ">
                            <div class="table-responsive  table-responsive2" id="divList">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="width: 100%; height:200px; position:absolute; top: 15%;" id="popup" class="hide">
                <asp:Panel ID="Panel1" runat="server" CssClass="animated2 fadeInDown" style="max-width: 500px; margin:0 auto; border: 1px solid; border-radius: 5px;
    background-color: #fff;
    -moz-box-shadow: 1px 2px 10px #222;
    -webkit-box-shadow: 1px 2px 10px #222;
    -o-box-shadow: 1px 2px 10px #222;
    box-shadow: 1px 2px 10px #222;">
                    <div data-rel="scroll" data-scrollheight="150" class="scroll-show-always auto-set-height">
                        <div class="col-sm-12 ">
                            <table class="tab-popup">
                                <tr>
                                    <td>Amount <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmountPanel" runat="server" CssClass="form-control-blue validatetxt1" onkeypress="return isNumber(event);" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Remark <span class="vd_red"></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRemarkPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td></td>
                                    <td>
                                        <input type='button' id='lnkUpdate'  class="button-y" value='Update' onclick="ValidateDropdown('.validatedrp1'); ValidateTextBox('.validatetxt1'); UpdateHead(); return validationReturn();" />
                                        
                                        &nbsp;&nbsp;
                                        <input type='button' id='Button4'  class="button-n" value='Cancel' onclick="hidePopup();" />
                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                
            </div>
    <style>
        .borders {
            border: 1px solid red !important;
        }

        .mp-table > thead > tr > th
        {
            font-size: 14px !important;

            padding:5px !important;
        }
    </style>
    <script>
        function hidePopup() {
            $("#popup").addClass("hide");
        }
        $(document).ready(function () {
            LoadData();
            
            $(document).on('click', '#lnkSubmit', function () {
                $("[id*=divList]").html("");
                if ($("[id*=lnkSubmit]").val() == "Submit") {
                    SaveHead();
                }
                
                
            });
            
            $(document).on('click', '#lnkclear', function () {
                ClearData();
                $("[id*=divList]").html("");
                LoadData();
            });
            $(document).on('change', '[id*=drpFromClass]', function () {
                $("[id*=divList]").html("");
                LoadData();
            });
            $(document).on('change', '[id*=drpToClass]', function () {
                $("[id*=divList]").html("");
                LoadData();
            });
            $(document).on('change', '[id*=ddlGender]', function () {
                $("[id*=divList]").html("");
                LoadData();
            });
        });

        function IsSingleHeadFn(tis) {
            $("[id*=txtRemark]").val('');
            if ($(tis).prop("checked") == true) {
                $("#divRemark").removeClass('hide');
            }
            else {
                $("#divRemark").addClass('hide');
            }
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function SaveHead() {
           
            if ($("[id*=txtFeeHead]").val() == "") {
                $("[id*=txtFeeHead]").addClass("borders");
                return true;
            }
            else {
                $("[id*=txtFeeHead]").removeClass("borders");
            }

            if ($("[id*=txtAmount]").val() == "") {
                $("[id*=txtAmount]").addClass("borders");
                return true;
            }
            else {
                $("[id*=txtAmount]").removeClass("borders");
            }

            if ($("#IsSingleHead").prop("checked") == true && $("[id*=txtRemark]").val() == "") {
                $("[id*=txtRemark]").addClass("borders");
                return true;
            }
            else {
                $("[id*=txtRemark]").removeClass("borders");
            }

            var FromClassId = $("[id*=drpFromClass]").val();
            var ToClassId = $("[id*=drpToClass]").val();
            var Gender = $("[id*=ddlGender]").val();
            var IsSingleHead = 0; var Remark = "";
            if ($("#IsSingleHead").prop("checked")==true) {
                IsSingleHead = 1; Remark = $("[id*=txtRemark]").val();
            }
            var FeeHead = $("[id*=txtFeeHead]").val();
            var Amount = $("[id*=txtAmount]").val();
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var LoginName = '<%=HttpContext.Current.Session["LoginName"] %>';
           
            var HeadData = "";
            var counts = $("[id*=divList] table tbody tr").length;
            for (var i = parseInt(FromClassId) ; i <= parseInt(ToClassId) ; i++) {
                if (Gender == "0") {
                    for (var j = 0; j <3; j++) {
                        HeadData += i + "##";// classid
                        HeadData += (j+1) + "##";
                        HeadData += IsSingleHead + "##";
                        HeadData += FeeHead + "##";
                        HeadData += Amount + "##";
                        HeadData += Remark + "##";
                        HeadData += SessionName + "##";
                        HeadData += BranchCode + "##";
                        HeadData += LoginName + "$";
                    }
                    
                }
                else {
                    HeadData += i + "##";// classid
                    HeadData += Gender + "##";
                    HeadData += IsSingleHead + "##";
                    HeadData += FeeHead + "##";
                    HeadData += Amount + "##";
                    HeadData += Remark + "##";
                    HeadData += SessionName + "##";
                    HeadData += BranchCode + "##";
                    HeadData += LoginName + "$";
                }
            }
            if (HeadData=="") {
                alert("No record(s) found!");
                return true;
            }
            ShowLoader();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/SaveOtherFeeHeadServer.aspx") %>',
                data: {
                    'HeadData': HeadData
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    HideLoader();
                    LoadData();
                    ClearData();
                    $("[id*=msgbox]").html(result.responseText);
                    setTimeout(function () {
                        $("[id*=msgbox] span").hide('slide', { direction: 'left' }, 3000);
                    }, 5000);
                },
                error: function (result) {
                    HideLoader();
                    LoadData();
                    ClearData();
                    $("[id*=msgbox]").html(result.responseText);
                    setTimeout(function () {
                        $("[id*=msgbox] span").hide('slide', { direction: 'left' }, 3000);
                    }, 5000);
                }
            });
        }

        function UpdateHead() {
           
            if ($("[id*=txtAmountPanel]").val() == "") {
                return true;
            }
            var IsSingleHead = 0; var Remark = "";
            Remark = $("[id*=txtRemarkPanel]").val();
            var Amount = $("[id*=txtAmountPanel]").val();
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
           
            var HeadData = "";
            HeadData += $("[id*=hdnId]").val() + "##";// id
            HeadData += $("[id*=hdnClassId]").val() + "##";// classid
            HeadData += IsSingleHead + "##";
            HeadData += Amount + "##";
            HeadData += Remark + "##";
            HeadData += SessionName + "##";
            HeadData += BranchCode + "$";

            if (HeadData=="") {
                alert("No record(s) found!");
                return true;
            }
            ShowLoader();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/UpdateOtherFeeHeadServer.aspx") %>',
                data: {
                    'HeadData': HeadData
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    HideLoader();
                    LoadData();
                    hidePopup();
                    $("[id*=msgbox]").html(result.responseText);
                    setTimeout(function () {
                        $("[id*=msgbox] span").hide('slide', { direction: 'left' }, 3000);
                    }, 5000);
                },
                error: function (result) {
                    HideLoader();
                    LoadData();
                    hidePopup();
                    $("[id*=msgbox]").html(result.responseText);
                    setTimeout(function () {
                        $("[id*=msgbox] span").hide('slide', { direction: 'left' }, 3000);
                    }, 5000);
                }
            });
        }

        function LoadData() {
            $("[id*=icons]").addClass("hide");
            $("[id*=divList]").html("");

            ShowLoader();
            var FromClassName = $("[id*=drpFromClass] option:selected").text();
            var ToClassName = $("[id*=drpToClass] option:selected").text();
            var FromClassId = $("[id*=drpFromClass]").val();
            var ToClassId = $("[id*=drpToClass]").val();
            var Gender = $("[id*=ddlGender]").val();
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';

            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/OtherFeeHeadListServer.aspx") %>',
                data: {
                    'FromClassName': FromClassName,
                    'ToClassName': ToClassName,
                    'FromClassId': FromClassId,
                    'ToClassId': ToClassId,
                    'Gender': Gender,
                    'SessionName': SessionName,
                    'BranchCode': BranchCode
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    HideLoader();
                    $("[id*=divList]").html(result.responseText);
                    $("[id=icons]").removeClass("hide");
                },
                error: function (result) {
                    HideLoader();
                    $("[id*=divList]").html(result.responseText);
                    $("[id=icons]").removeClass("hide");
                }
            });
        }

        function editHead(tis) {
            var valdata = $(tis).closest('tr').attr('valdata').split('##');
            $("[id*=hdnId]").val(valdata[0]);
            $("[id*=hdnClassId]").val(valdata[1]);
            $("[id*=txtAmountPanel]").val(valdata[5]);
            $("[id*=txtRemarkPanel]").val(valdata[6]);
            $("#popup").removeClass("hide");
            
        }

        function deleteHead(tis) {
            if (confirm("Are you sure you want to delete this?")) {
                var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
                var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
                var valdata = $(tis).closest('tr').attr('valdata').split('##');
                $("[id*=hdnId]").val(valdata[0]);
                $("[id*=hdnClassId]").val(valdata[1]);
                ShowLoader();
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("Server/DeleteOtherFeeHeadServer.aspx") %>',
                    data: {
                        'SessionName': SessionName,
                        'BranchCode': BranchCode,
                        'id': $("[id*=hdnId]").val(),
                        'ClassId': $("[id*=hdnClassId]").val(),
                    },
                    dataType: "json",
                    async: true,
                    success: function (result) {
                        HideLoader();
                        LoadData();
                        ClearData();
                        $("[id*=msgbox]").html(result.responseText);
                        setTimeout(function () {
                            $("[id*=msgbox] span").hide('slide', { direction: 'left' }, 3000);
                        }, 5000);
                    },
                    error: function (result) {
                        HideLoader();
                        LoadData();
                        ClearData();
                        $("[id*=msgbox]").html(result.responseText);
                        setTimeout(function () {
                            $("[id*=msgbox] span").hide('slide', { direction: 'left' }, 3000);
                        }, 5000);
                    }
                });
            }
        }

        function ClearData() {
            $("[id*=drpFromClass]").prop('disabled', false);
            $("[id*=drpToClass]").prop('disabled', false);
            $("[id*=ddlGender]").prop('disabled', false);
            $("[id*=IsSingleHead]").prop('disabled', false);
            $("[id*=txtFeeHead]").prop('disabled', false);

            $("#IsSingleHead").prop("checked", false);

            $("[id*=drpFromClass]").val("1");
            $("[id*=drpToClass]").val("1");

            $("#divRemark").addClass('hide');
            $("[id*=txtRemark]").val('');

            $("[id*=ddlGender]").val("0");
            $("[id*=txtFeeHead]").val("");
            $("[id*=txtAmount]").val("");
            $("[id*=hdnId]").val("");
            $("[id*=hdnClassId]").val("");
            $("[id*=lnkSubmit]").val("Submit");
        }

        function PrintDiv() {
            var headContent = document.getElementsByTagName('head')[0].innerHTML;

            var divContents = document.getElementById("printList").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000, class="tbls"');
            printWindow.document.write('<html><head><title>REPORT CARD FOR Nur to Prep</title>' + headContent + '</head>');
            var TermNmae = $("[id*=drpEval]").val();
            printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 1500);
            return false;
            printWindow.close();

        }
    </script>
</asp:Content>

