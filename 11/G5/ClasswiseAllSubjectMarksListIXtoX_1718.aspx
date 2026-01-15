<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="ClasswiseAllSubjectMarksListIXtoX_1718.aspx.cs" Inherits="common_ClasswiseAllSubjectMarksListIXtoX_1718" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../../js/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/jspdf/0.9.0rc1/jspdf.min.js"></script>
    
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
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                    <div class="" id="div_drpclass">
                                        <select id="drpclass" class="form-control-blue"></select>
                                    </div>
                                </div>
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                    <div class="" id="div_section">
                                        <select id="drpsection" class="form-control-blue"></select>
                                    </div>
                                </div>

                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Evaluation</label>
                                    <div class="">
                                        <select id="drpEval" class="form-control-blue">
                                            <option value=""><-Select-></option>
                                            <option value="Term1">Term1</option>
                                            <option value="Term2">Term2</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
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
                            </div>
                            <div class="col-sm-12  no-padding hide" id="icons" style="border-bottom: 2px solid #000; margin-bottom: 6px; padding-bottom: 5px !important;">
                                <div style="float: right; font-size: 19px;">
                                    <a onclick="PrintDiv();" class="btn btn-sm"><i class="fa fa-print text-primary"></i> Print</a>
                                    <a onclick="exportexcel();" class="btn btn-sm"><i class="fa fa-file-excel-o text-primary"></i> Execl</a>
                                </div>
                            </div>
                            <div id="headerDiv" runat="server" class="hide"></div>
                            <div id="divExport"></div>
                            <div id="editor"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style>
        @import "compass/css3";

        .borders {
            border: 1px solid red !important;
        }

        .borders2 {
            border: 1px solid #D5D5D5;
        }
    </style>
    <style>
      

        .table-fixed > thead {
            width: 100%;
        }

        .table-fixed > tbody {
            height: 500px;
            overflow-y: scroll;
            width: 100%;
        }

        .table-fixed > thead, .table-fixed > tbody, .table-fixed > tr, .table-fixed > td, .table-fixed > th {
            display: block;
        }
    </style>
    <script type="text/javascript">  
        function exportexcel() {
            
            var contents = $("[id*=divExport]").html();
            window.open('data:application/vnd.ms-excel,' + encodeURIComponent(contents));
        }
</script>  
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
            LoadClass();

            $(document).on('change', '#drpclass', function () {
                LoadSection($("#drpclass").val());
            });

            $(document).on('change', '#drpEval', function () {
                if ($("#drpEval").val() != "") {
                    LoadData();
                }
            });
            $(document).on('change', '#drpStatus', function () {
                if ($("#drpStatus").val() != "") {
                    LoadData();
                }
            });
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
            var headContent = document.getElementsByTagName('head')[0].innerHTML;
            var divContents = document.getElementById("break_div").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000, class="tbls"');
            printWindow.document.write('<html><head><title>Class wise Cumulative (IX Ito X)</title>' + headContent + '</head>');
            printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 1500);
            return false;
            printWindow.close();

        }
        var doc = new jsPDF();
        var specialElementHandlers = {
            '#editor': function (element, renderer) {
                return true;
            }
        };

        function downloadPDF() {
            var pageURL = $(location).attr("html");

            doc.fromHTML(pageURL, 15, 15, {
                'width': 170,
                'elementHandlers': specialElementHandlers
            });
            doc.save('sample-content.pdf');
        }

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

            var ClassId = $("#drpclass").val();
            var ClassName = $("[id*=drpclass] option:selected").text();
            var SectionID = $("[id*=drpsection]").val();
            var SectionName = $("[id*=drpsection] option:selected").text();
            var TermNmae = $("[id*=drpEval]").val();
            var session = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var status = $("#drpStatus").val();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ClassWiseCumulativeReport_IXtoX_1718Server.aspx") %>',
                data: {
                    'ClassId': ClassId,
                    'ClassName': ClassName,
                    'SectionID': SectionID,
                    'SectionName': SectionName,
                    'TermNmae': TermNmae,
                    'session': session,
                    'BranchCode': BranchCode,
                    'status': status
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    console.log(result.responseText);
                    HideLoader();
                    $("[id*=divExport]").html(result.responseText);
                    $("[id=icons]").removeClass("hide");
                    $(".divHeader").append(headerDiv);
                },
                error: function (result) {
                    console.log(result.responseText);
                    HideLoader();
                    $("[id*=divExport]").html(result.responseText);
                    $("[id=icons]").removeClass("hide");
                    $(".divHeader").append(headerDiv);
                }
            });
        }
    </script>
</asp:Content>

