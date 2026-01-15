<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="UniformFeeHead.aspx.cs" Inherits="UniformFeeHead" %>

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
                                <div class="txt-middle">
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
                                <label class="control-label">Remark&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50  btn-a-devices-6-p6 mgbt-xs-15" style="margin-top: 26px;">
                                <div class='col -sm-12 '><input type='button' id='lnkSubmit' class='button form-control-blue' value='Submit' />&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' id='lnkclear' class='button form-control-blue' value='Clear' /></div>
                                <div id="msgbox" runat="server" style="left: 155px;"></div>
                                <asp:HiddenField runat="server" ID="hdnId" />
                                <asp:HiddenField runat="server" ID="hdnClassId" />
                            </div>
                        </div>
                        
                        <div class="col-sm-12 ">
                            <div class=" table-responsive  table-responsive2" id="divList">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
        $(document).ready(function () {
            LoadData();
            $(document).on('click', '#lnkSubmit', function () {
                $("[id*=divList]").html("");
                if ($("[id*=lnkSubmit]").val() == "Submit") {
                    SaveHead();
                }
                if ($("[id*=lnkSubmit]").val() == "Update") {
                    UpdateHead();
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
            alert(FromClassId + "---" + ToClassId);
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
                url: '<%=ResolveUrl("Server/SaveUniformFeeHeadServer.aspx") %>',
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

            var IsSingleHead = 0; var Remark = "";
            if ($("#IsSingleHead").prop("checked")==true) {
                IsSingleHead = 1; Remark = $("[id*=txtRemark]").val();
            }
            var Amount = $("[id*=txtAmount]").val();
            var Qty = $("[id*=txtQty]").val();
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
                url: '<%=ResolveUrl("Server/UpdateUniformFeeHeadServer.aspx") %>',
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
                url: '<%=ResolveUrl("Server/UniformFeeHeadListServer.aspx") %>',
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
            $("[id*=drpFromClass]").val(valdata[1]);
            $("[id*=drpToClass]").val(valdata[1]);
            $("[id*=hdnClassId]").val(valdata[1]);
            if (valdata[2] == "True") {
                $("#IsSingleHead").prop("checked", true);
                $("#divRemark").removeClass('hide');
                $("[id*=txtRemark]").val(valdata[6]);
            }
            else {
                $("#IsSingleHead").prop("checked", false);
                $("#divRemark").addClass('hide');
                $("[id*=txtRemark]").val('');
            }
            $("[id*=ddlGender]").val(valdata[3]);
            $("[id*=txtFeeHead]").val(valdata[4]);
            $("[id*=txtAmount]").val(valdata[5]);

            $("[id*=drpFromClass]").prop('disabled', true);
            $("[id*=drpToClass]").prop('disabled', true);
            $("[id*=ddlGender]").prop('disabled', true);
            $("[id*=IsSingleHead]").prop('disabled', true);
            $("[id*=txtFeeHead]").prop('disabled', true);
            $("[id*=lnkSubmit]").val("Update");
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
                    url: '<%=ResolveUrl("Server/DeleteUniformFeeHeadServer.aspx") %>',
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
            printWindow.document.write('<html><head><title>Product Heads</title>' + headContent + '</head>');
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

