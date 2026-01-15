<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Studentwisecumulative_IXtoX.aspx.cs" Inherits="common_G5_Studentwisecumulative_IXtoX" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
     <script src="../../js/jquery.min.js"></script>
    <style>
        .trBg{
            background: #f3f3f3 !important;
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
                                    <label class="control-label">Select S.R. NO.</label>
                                    <div class="" id="div_srno">
                                        <select id="drpsrno" class="form-control-blue"></select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
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
                                <div style="float: right; font-size: 19px;">
                                    <a onclick="PrintDiv();" class="btn btn-sm"><i class="fa fa-print text-primary"></i> Print</a>
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

            $(document).on('change', '#drpclass', function () {
                LoadSection($("#drpclass").val());
            });
            $(document).on('change', '#drpsection', function () {
                
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("Server/ddlSrNo_5.aspx") %>',
                    data: { 'classId': $("#drpclass").val(), 'SectionId': $("#drpsection").val(), 'Status': $("#drpStatus").val() },
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
                    url: '<%=ResolveUrl("Server/ddlSrNo_5.aspx") %>',
                    data: { 'classId': $("#drpclass").val(), 'SectionId': $("#drpsection").val(), 'Status': $("#drpStatus").val() },
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
            LoadClass();
        });

        function LoadClass() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlClass_5.aspx") %>',
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

        function LoadSection(classId) {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSection_5.aspx") %>',
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
            $(".spnPromotedtoclassManula").val($("[id*=txtPromotedtoclass]").val());
            var headContent = document.getElementsByTagName('head')[0].innerHTML;

            var divContents = document.getElementById("divExport").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000, class="tbls"');
            printWindow.document.write('<html><head><title>STUDENT WISE CUMLATIVE FOR IXtoX</title>' + headContent + '</head>');
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
            $("[id*=icons]").addClass("hide");
            $("[id*=divExport]").html("");
            if ($("[id*=drpsection]").val() == "") {
                $("[id*=drpsection]").addClass("borders");
                return;
            }
            else {
                $("[id*=drpsection]").removeClass("borders");
            }

            var headerDiv = $("[id*=headerDiv]").html();
            ShowLoader();
            
            var session = '<%=HttpContext.Current.Session["SessionName"] %>';
            var SrNo = $("[id*=drpsrno]").val();
            var SectionName = $("[id*=drpsection] option:selected").text();
            var SectionID = $("[id*=drpsection]").val();
            var TermNmae = $("[id*=drpEval]").val();
            var ClassId = $("#drpclass").val();
            var ClassName = $("[id*=drpclass] option:selected").text();
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var status = $("#drpStatus").val();
            var AttendanceTyp = $("[id*=drpAttendance]").val();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/Studentwisecumulative_IXtoX_1718Server.aspx") %>',
                data: {
                    'ClassId': ClassId,
                    'ClassName': ClassName,
                    'SectionID': SectionID,
                    'SectionName': SectionName,
                    'session': session,
                    'BranchCode': BranchCode,
                    'status': status,
                    'AttendanceTyp':AttendanceTyp,
                    'SrNo': SrNo
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    HideLoader();
                    $("[id*=divExport]").html(result.responseText);
                },
                error: function (result) {
                    HideLoader();
                    $("[id*=divExport]").html(result.responseText);
                    $("[id=icons]").removeClass("hide");
                    $(".divHeader").append(headerDiv);
                }
            });
        }
    </script>
</asp:Content>
