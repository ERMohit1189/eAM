<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="PrintFeeCard.aspx.cs" Inherits="PrintFeeCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
   
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/jspdf/0.9.0rc1/jspdf.min.js"></script>
    <style>
        @import "compass/css3";
        .term1 > tbody > tr > td {
            padding:5px;
        }
        @page {             
             margin:6px;  /* Safe value */
         }      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <script>
        Sys.Application.add_load(datetime);
    </script>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12 ">

                            <div id="Div1" class="col-sm-12  no-padding" runat="server">
                                <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                    <div class="" id="div_drpclass">
                                        <select id="drpclass" class="form-control-blue"></select>
                                    </div>
                                </div>
                                <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                    <div class="" id="div_section">
                                        <select id="drpsection" class="form-control-blue"></select>
                                    </div>
                                </div>
                                <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Select Stream&nbsp;<span class="vd_red">*</span></label>
                                    <div class="" id="div_Branch">
                                        <select id="drpBranch" class="form-control-blue"></select>
                                    </div>
                                </div>
                                 <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                    <div class="" id="div_ddlFeeType">
                                        <select id="ddlFeeType" class="form-control-blue"></select>
                                    </div>
                                </div>
                                <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Status</label>
                                    <div class="">
                                        <select id="drpStatus" class="form-control-blue">
                                           <option value="">All</option>
                                            <option value="A">Active</option>
                                            <option value="B">Inactive</option>
                                            <option value="W">Withdrwal</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Select S.R. No.</label>
                                    <div class="" id="div_srno">
                                        <select id="drpsrno" class="form-control-blue"></select>
                                    </div>
                                </div>
                                
                                <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Remark</label>
                                    <div class="" id="">
                                        <textarea id="txtRemark"  class="form-control-blue" placeholder="Maximum 250 Charectors" maxlength="250"></textarea>
                                    </div>
                                </div>
                                <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                    <div class="">
                                        <input type="button" id="btnView" class="button" value="view" />
                                        <div class="text-box-msg"></div>
                                        <div id="headerDiv" runat="server" class="hide"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12  no-padding hide" id="icons" style="border-bottom: 2px solid #000; margin-bottom: 20px; padding-bottom: 5px !important;">
                                <div style="float: right; font-size: 19px;">
                                    <a onclick="PrintDiv();" class="btn btn-sm"><i class="fa fa-print text-primary"></i> Print</a>
                                </div>
                            </div>

                            <div id="divExport" class=""></div>
                            <div id="editor"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style>
        @import "compass/css3";

        #drpPromoToManual {
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
        }


        .borders {
            border: 1px solid red !important;
        }

        .borders2 {
            border: 1px solid #D5D5D5;
        }
    </style>
    <script>
        
        $(document).ready(function () {
            $(document).on('change', '#DrpSessionName', function () {
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("~/admin/Server/SessionChange.aspx") %>',
                    data: { 'SessionName': $("[id*=DrpSessionName]").val() },
                    dataType: "json",
                    async: true,
                    success: function (result) {
                        window.location.href = location.href;
                    },
                    error: function (result) {
                        window.location.href = location.href;
                    }
                });
            });

            $(document).on('click', '#btnView', function () {
                LoadData();
            });
            $(document).on('change', '#drpclass', function () {
                LoadSection();
            });
           
            $(document).on('change', '#drpsection', function () {
                LoadBranch();
            });
            $(document).on('change', '#drpBranch', function () {
                loadSrno();
            });
            $(document).on('change', '#drpStatus', function () {
                loadSrno();
            });
            $(document).on('change', '#ddlFeeType', function () {
                loadSrno();
            });
            LoadClass();
            LoadFeeMedium();
        });

        function LoadClass() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlClass.aspx") %>',
                data: {},
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_drpclass").html("");
                    $("#div_drpclass").html(result.responseText);
                    HideLoader();
                }
            });
        }
        function LoadFeeMedium() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlFeeMedium.aspx") %>',
                data: {  },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_ddlFeeType").html("");
                    $("#div_ddlFeeType").html(result.responseText);
                    HideLoader();
                }
            });
        }
        function LoadSection() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSection.aspx") %>',
                data: { 'classId': $("#drpclass").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_section").html("");
                    $("#div_section").html(result.responseText);
                    HideLoader();
                }
            });
        }
        function LoadBranch() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlBranch.aspx") %>',
                data: { 'classId': $("#drpclass").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_Branch").html("");
                    $("#div_Branch").html(result.responseText);
                    HideLoader();
                }
            });
        }
        function loadSrno() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSrNo.aspx") %>',
                data: { 'classId': $("#drpclass").val(), 'SectionId': $("#drpsection").val(), 'BranchId': $("#drpBranch").val(), 'Status': $("#drpStatus").val(), 'Medium': $("#ddlFeeType").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_srno").html("");
                    $("#div_srno").html(result.responseText);
                    HideLoader();
                }
            });
        }

        function PrintDiv() {
            var headContent = document.getElementsByTagName('head')[0].innerHTML;

            var divContents = document.getElementById("divExport").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000, class="tbls"');
            printWindow.document.write('<html><head><title>Fee Card ' + $("#classsecton").html() + '</title>' + headContent + '</head>');
            printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');

            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 1500);
            return false;
            printWindow.close();

        }
        
        $.noConflict();
        function LoadData() {
            $("[id*=icons]").addClass("hide");
            $("[id*=divExport]").html("");
            if ($("#drpclass").val() == "") {
                $("#drpclass").addClass("borders");
                return;
            }
            else {
                $("#drpclass").removeClass("borders");
            }
            if ($("#drpsection").val() == "") {
                $("#drpsection").addClass("borders");
                return;
            }
            else {
                $("#drpsection").removeClass("borders");
            }
            if ($("#drpBranch").val() == "") {
                $("#drpBranch").addClass("borders");
                return;
            }
            else {
                $("#drpBranch").removeClass("borders");
            }
            var headerDiv = $("[id*=headerDiv]").html();
            ShowLoader();
            
            var session = '<%=HttpContext.Current.Session["SessionName"] %>';
            var SrNo = $("#drpsrno").val();
            var Section = $("#drpsection option:selected").text();
            var ClassId = $("#drpclass").val();
            var ClassName = $("#drpclass option:selected").text();
            var BranchId = $("#drpBranch").val();
            var Remark = $("#txtRemark").val();
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var status = $("#drpStatus").val();
            var feemedium = $("#ddlFeeType").val();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/PrintfeeCardServer.aspx") %>',
                data: {
                    'session': session,
                    'SrNo': SrNo,
                    'Section': Section,
                    'ClassId': ClassId,
                    'BranchId':BranchId,
                    'BranchCode': BranchCode,
                    'ClassName': ClassName,
                    'Remark':Remark,
                    'status': status,
                    'feemedium': feemedium,
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    HideLoader();
                    $("[id*=divExport]").html(result.responseText);
                    //for (var i = 0; i < parseInt($(".spnCount").html()) ; i++) {
                    //    $("#Qr" + i).barcode("213sads3", "ean13");
                    //    //$("#Qr" + i).barcode($("#QrData" + i).html(), "ean13");
                    //}
                },
                error: function (result) {
                    HideLoader();
                    $("[id*=divExport]").html(result.responseText);
                    //for (var i = 0; i < parseInt($(".spnCount").html()) ; i++) {
                    //    $("#Qr" + i).barcode("213sads3", "ean13");
                    //}
                    $("[id=icons]").removeClass("hide");
                }
            });
        }
    </script>
    
</asp:Content>
