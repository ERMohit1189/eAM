<%@ Page Title="" Language="C#" MasterPageFile="~/14/comman_root_manager.master" AutoEventWireup="true" CodeFile="ReportCard_ItoVIII.aspx.cs" Inherits="ReportCard_ItoVIII" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
    <style>
        @import "compass/css3";

        .drpPromoToManual {
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
        }

        tables > tbody > tr > td {
            padding: 5px 5px !important;
        }

        tables > tbody > tr > th {
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
        tables > .p-pad-25 > span {
            padding: 1.1px 5px !important;
            font-size: 11px !important;
            color: #000 !important;
        }

        tables > .mp-table > tbody > tr > td span {
            font-size: 11px !important;
            padding: 1.1px 5px !important;
            color: #000 !important;
        }


        tables > .mp-table > tbody > tr > td, .mp-table > tfoot > tr > td {
            font-size: 12px !important;
            padding: 1px 5px !important;
            color: #000 !important;
        }


        tables > .p-pad-25 > span {
            padding: 1.5px 5px !important;
            font-size: 11px !important;
            color: #000 !important;
        }

        tables > .mp-table > tbody > tr > td span {
            font-size: 11px !important;
            color: #000 !important;
        }

        tables > .mp-table > tbody > tr > td, .mp-table > tfoot > tr > td {
            font-size: 11px !important;
            color: #000 !important;
        }

        tables > tbody > tr > td {
            padding: 1.5px 5px !important;
        }

        tables > tbody > tr > th {
            padding: 1.5px 5px !important;
        }

        .sentence-case {
            text-transform: initial !important;
        }

        .table > tbody > tr > td, th {
            padding: 1px !important;
            font-size: 10px !important;
        }

        .table > thead > tr > th {
            padding: 1px !important;
            font-size: 10px !important;
            padding: 1px 3px !important;
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
                            <asp:Label runat="server" ID="lblClassid" CssClass="hide"></asp:Label>
                            <asp:Label runat="server" ID="lblClassName" CssClass="hide"></asp:Label>
                            <asp:Label runat="server" ID="lblSectionId" CssClass="hide"></asp:Label>
                            <asp:Label runat="server" ID="lblSectionName" CssClass="hide"></asp:Label>
                            <asp:Label runat="server" ID="lblError" Visible="false" Style="color: red"></asp:Label>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15 no-padding" runat="server" id="divEval" visible="false">
                                <label class="control-label">Select Term</label>
                                <div class="">
                                    <asp:DropDownList ID="ddlEval" runat="server" CssClass="form-control-blue " onchange="LoadReportCardForG()">
                                        <asp:ListItem>Term1</asp:ListItem>
                                        <asp:ListItem>Term2</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div id="Div1" class="col-sm-12  no-padding" runat="server">
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                    <div class="" id="div_drpClass">
                                        <select id="drpClass" class="form-control-blue"></select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                    <div class="" id="div_section">
                                        <select id="drpSection" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
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
                                    <label class="control-label">S.R. No.</label>
                                    <div class="" id="div_srno">
                                        <select id="drpsrno" class="form-control-blue"></select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Evaluation</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue ">
                                            <asp:ListItem>Term1</asp:ListItem>
                                            <asp:ListItem>Term2</asp:ListItem>
                                        </asp:DropDownList>

                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>


                                <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Attendance</label>
                                    <div class="">
                                        <asp:DropDownList ID="drpAttendance" runat="server" CssClass="form-control-blue ">
                                            <asp:ListItem Value="0">Auto</asp:ListItem>
                                            <asp:ListItem Value="1">Manual</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                    <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                        <input type="button" id="btnView" class="button" value="view" />
                                        <div class="text-box-msg"></div>
                                        <div id="headerDiv" runat="server" class="hide"></div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12  no-padding hide" id="icons" style="border-bottom: 2px solid #000; margin-bottom: 20px; padding-bottom: 5px !important;">
                                <div style="float: right; font-size: 19px;" id="printbtn" class="hide">
                                    <a onclick="PrintDiv();" class="btn btn-sm"><i class="fa fa-print text-primary"></i>Print</a>
                                </div>
                            </div>

                            <div id="divExport" class=""></div>

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

            LoadClass();
            $(document).on('click', '#btnView', function () {
                LoadData();
            });


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

            $(document).on('change', '#drpClass', function () {
                LoadSection($("#drpClass").val());
            });
            $(document).on('change', '#drpSection', function () {
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("Server/ddlSrNo.aspx") %>',
                    data: { 'classId': $("#drpClass").val(), 'SectionId': $("#drpSection").val(), 'Status': $("#drpStatus").val() },
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

            });
            $(document).on('change', '#drpStatus', function () {
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("Server/ddlSrNo.aspx") %>',
                    data: { 'classId': $("#drpClass").val(), 'SectionId': $("#drpSection").val(), 'Status': $("#drpStatus").val() },
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
            });


        });

        function LoadClass() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlClassG2.aspx") %>',
                data: {},
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_drpClass").html("");
                    $("#div_drpClass").html(result.responseText);
                    HideLoader();
                }
            });
        }

        function LoadSection(classId) {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSection.aspx") %>',
                data: { 'classId': classId },
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

        function PrintDiv() {

            var headContent = document.getElementsByTagName('head')[0].innerHTML;

            var divContents = document.getElementById("divExport").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000, class="tbls"');
            printWindow.document.write('<html><head><title>REPORT CARD FOR I to VIII</title>' + headContent + '</head>');
            var TermNmae = $("[id*=drpEval]").val();
            if (TermNmae == 'Term1') {
                printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');
            }
            else {
                printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');
            }

            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 1500);
            return false;
            printWindow.close();

        }
        $.noConflict();
        function LoadData() {
            $("#printbtn").removeClass('hide');
            $("[id*=icons]").addClass("hide");
            $("[id*=divExport]").html("");
            if ($("[id*=drpSection]").val() == "") {
                $("[id*=drpSection]").addClass("borders");
                return;
            }
            else {
                $("[id*=drpSection]").removeClass("borders");
            }

            var headerDiv = $("[id*=headerDiv]").html();
            ShowLoader();


            var SrNo = $("#drpsrno").val();
            var SectionName = $("#drpSection option:selected").text();
            var SectionId = $("#drpSection").val();
            var Term = $("[id*=drpEval]").val();
            var ClassId = $("#drpClass").val();
            var ClassName = $("#drpClass option:selected").text();
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            var Status = $("#drpStatus").val();
            var AttendanceType = $("[id*=drpAttendance]").val();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ReportCard_ItoVIIIServer.aspx") %>',
                data: {
                    'SrNo': SrNo,
                    'SectionName': SectionName,
                    'SectionId': SectionId,
                    'Term': Term,
                    'ClassId': ClassId,
                    'ClassName': ClassName,
                    'BranchCode': BranchCode,
                    'SessionName': SessionName,
                    'Status': Status,
                    'AttendanceType': AttendanceType

                },
                dataType: "json",
                async: true,
                success: function (result) {
                    HideLoader();
                    $("[id*=divExport]").html(result.responseText);
                    $("[id=icons]").removeClass("hide");
                    $(".divHeader").append(headerDiv);
                },
                error: function (result) {
                    HideLoader();
                    $("[id*=divExport]").html(result.responseText);
                    $("[id=icons]").removeClass("hide");
                    $(".divHeader").append(headerDiv);
                }
            });
        }

        function LoadReportCardForG() {
            $("#printbtn").addClass('hide');
            $("[id*=icons]").addClass("hide");
            $("[id*=divExport]").html("");

            var headerDiv = $("[id*=headerDiv]").html();
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            var SrNo = '<%=HttpContext.Current.Session["Srno"] %>'
            var SectionName = $("[id*=lblSectionName]").html();
            var SectionId = $("[id*=lblSectionId]").html();
            var Term = $("[id*=ddlEval]").val();
            var ClassId = $("[id*=lblClassid]").html();
            var ClassName = $("[id*=lblClassName]").html();
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var Status = "A";
            var AttendanceType = "1";
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ReportCard_ItoVIIIServer.aspx") %>',
                data: {
                    'SrNo': SrNo,
                    'SectionName': SectionName,
                    'SectionId': SectionId,
                    'Term': Term,
                    'ClassId': ClassId,
                    'ClassName': ClassName,
                    'BranchCode': BranchCode,
                    'SessionName': SessionName,
                    'Status': Status,
                    'AttendanceType': AttendanceType
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    $("[id*=divExport]").html(result.responseText);
                    $("[id=icons]").removeClass("hide");
                    $(".divHeader").append(headerDiv);
                },
                error: function (result) {
                    $("[id*=divExport]").html(result.responseText);
                    $("[id=icons]").removeClass("hide");
                    $(".divHeader").append(headerDiv);
                }
            });
        }
    </script>
</asp:Content>

