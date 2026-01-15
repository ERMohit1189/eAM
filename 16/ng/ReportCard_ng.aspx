<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ReportCard_ng.aspx.cs" Inherits="ReportCard_ng" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../../js/jquery.min.js"></script>
    <script>
        function currdateTime() {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
            var today = dd + '-' + mm + '-' + yyyy;
            today = today + time;
        }
        ///===============Print Area
        function PrintDiv() {
            var headContent = document.getElementsByTagName('head')[0].innerHTML;
            var divContents = document.getElementById("divExport").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000, class="term1"');
            printWindow.document.write('<html><head><title>' + "ReportCard" + $("#drpclass option:selected").text() + '</title>' + headContent + '</head>');
            printWindow.document.write('<body>' + divContents + '</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 1500);
            return false;
            printWindow.close();
        };
    </script>

    <script type="text/javascript">
        ///===============Download Excel
        function ExportToExcel() {
            var htmltable = document.getElementById('divExport');
            var html = htmltable.outerHTML;
            window.open('data:application/vnd.ms-excel,' + encodeURIComponent(html));
        }
    </script>
    <script type="text/javascript">
        function export2Word() {
            ///===============Download Word

            var html, link, blob, url, css;
            css = (
              '<style>' +
              '@page WordSection1{size: 841.95pt 595.35pt;mso-page-orientation: landscape;}' +
              'div.WordSection1 {page: WordSection1;}' +
              '</style>'
            );
            var htmltable = document.getElementById('divExport');
            html = htmltable.innerHTML;
            blob = new Blob(['\ufeff', css + html], {
                type: 'application/msword'
            });
            url = URL.createObjectURL(blob);
            link = document.createElement('A');
            link.href = url;
            link.download = "ReportCard" + $("#drpclass option:selected").text() + currdateTime();  // default name without extension 
            document.body.appendChild(link);
            if (navigator.msSaveOrOpenBlob) navigator.msSaveOrOpenBlob(blob, ("ReportCard" + $("#drpclass option:selected").text() + currdateTime() + '.doc')); // IE10-11
            else link.click();  // other browsers
            document.body.removeChild(link);
        };

    </script>
    <%--<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.22/pdfmake.min.js"></script>--%>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>
    <script type="text/javascript">
        function export2Pdf() {
            ///===============Download PDF
            html2canvas($('#divExport')[0], {
                onrendered: function (canvas) {
                    var data = canvas.toDataURL();
                    var docDefinition = {
                        content: [{
                            image: data,
                            width: 500
                        }]
                    };
                    pdfMake.createPdf(docDefinition).download($("[id*=ContentPlaceHolder1_ContentPlaceHolderMainBox_lblTitle]").html() + ".pdf");
                }
            });
        }
    </script>
    <style>
        @import "compass/css3";

        #drpPromoToManual {
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
        }

        .term1 > tbody > tr > td {
            padding: 1.1px 5px !important;
        }

        .term1 .scalTable > tbody > tr > td{
            border: 0px !important;
        }
          .term1 .scalTable > tbody > tr > th{
              border: 0px !important;
        }
        .term1 .scalTable > tbody > tr > td.scal{
            border-right: 1px solid !important;
            text-align: right !important;
        }
          .term1 .scalTable > tbody > tr > th.scal{
              text-align: right !important;
              border-right: 1px solid !important;
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
       
        h3.form-name  {
            font-size: 15px !important;
            margin-left: -70px;
        }
         h3.form-name > span  {
            border: 1px solid;
    padding: 0px 5px 0px 5px;
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
         .vertical-text {
	transform: rotate(270deg);
	/*transform-origin: left top 0;*/
    font-size:12px !important;
    
    white-space: nowrap;
    width: 30px !important;
    margin: auto;
       margin-top: 102px !important;
        }
        .th_tdDesign {
            background: #ccc;
    color: #ca0000;
        }
        .tdDesign {
            padding: 3px !important;
    font-size: 11px !important;
    text-align:center !important;
        }
        .tdDesign2 {
            padding: 3px !important;
    font-size: 11px !important;
    text-align:left !important;
        }
        .scal {
                text-align: right !important;
    border-right: 1px solid;
    padding-right: 3px;
    font-size: 11px !important;
    white-space:nowrap;
        }
        .keys {
                text-align: left;
    padding-left: 3px;
    font-size: 11px !important;
     white-space:nowrap;
        }
        .tdDesign2 table > tbody > tr > th, td {
            text-align:left !important;
        }
        .mp-table > tbody > tr > th {
           font-size: 11px !important;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    

    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12 ">

                            <div id="Div1" class="col-sm-12  no-padding" runat="server">
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                    <div class="" id="div_drpclass">
                                        <select id="drpclass" class="form-control-blue"></select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                    <div class="" id="div_section">
                                        <select id="drpsection" class="form-control-blue"></select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_drpBranch">
                                        <select id="drpBranch" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">S.R. No.</label>
                                    <div class="" id="div_srno">
                                        <select id="drpsrno" class="form-control-blue"></select>
                                    </div>
                                </div>
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Term&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_terms">
                                        <select id="ddlTerm" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>

                                <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Attendance</label>
                                    <div class="">
                                        <select id="drpAttendance" class="form-control-blue">
                                            <option value="0">Auto</option>
                                            <option value="1" selected="selected">Manual</option>
                                        </select>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel62" runat="server">
                                    <ContentTemplate>
                                        
                                        <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Date </label>
                                            <div class="">
                                                
                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control-blue datepicker-normal currDate"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                            
                                            
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-sm-1  half-width-50 mgbt-xs-15">
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
                                        <%--<a onclick="ExportToExcel();" class="btn btn-sm"><i class="fa fa-file-excel-o text-primary"></i> Excel</a>
                                        <a onclick="export2Word();" class="btn btn-sm"><i class="fa fa-file-word-o text-primary"></i> Word</a>
                                        <a onclick="export2Pdf();" class="btn btn-sm"><i class="fa fa-file-pdf-o text-primary"></i> PDF</a>--%>
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
    <link href="../../css/jquery-ui.css?p=1" rel="stylesheet" />
    <script src="../../js/jquery-ui.js?p=1"></script>
    <script>
        $(document).ready(function () {

            $("[id*=txtDate]").datepicker({ dateFormat: 'dd-M-yy', changeYear: true, changeMonth: true, yearRange: '-50:+10' });
            if ($('[id*=txtDate]').val() == "" && !$('.datepicker-normal').hasClass("dateblank")) {
                $('[id*=txtDate]').datepicker('setDate', new Date());
            }
            else {
                $("[id*=txtDate]").datepicker({ dateFormat: 'dd-M-yy', changeYear: true, changeMonth: true, yearRange: '-50:+10' });
            }
            if ($('[id*=txtDate]').hasClass("currDate")) {
                $('.currDate').datepicker('setDate', new Date());
            }
        });
      </script>
    <script>

        $(document).ready(function () {

            $(document).on('click', '#btnView', function () {
                LoadData();
            });
            LoadClass();
            $(document).on('change', '#drpclass', function () {
                $("[id*=divList]").html("");
                LoadSection();
            });
            $(document).on('change', '#drpsection', function () {
                $("[id*=divList]").html("");
                LoadBranch();
            });

            $(document).on('change', '#drpBranch', function () {
                $("[id*=divList]").html("");
                LoadTerm();
                LoadsrNO();
            });
            
        });

        function LoadClass() {
            ShowLoader();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlClass_MarkEntry.aspx") %>',
                data: {},
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    $("#div_drpclass").html("");
                    $("#div_drpclass").html(result.responseText);
                    
                }
            });
            HideLoader();
        }

        function LoadSection() {
            ShowLoader();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSection_MarkEntry.aspx") %>',
                data: { 'classId': $("#drpclass").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    $("#div_section").html("");
                    $("#div_section").html(result.responseText);
                   
                }
            });
            HideLoader();
        }
        function LoadBranch() {
            ShowLoader();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlBranch_MarkEntry.aspx") %>',
                data: { 'classId': $("#drpclass").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    $("#div_drpBranch").html("");
                    $("#div_drpBranch").html(result.responseText);
                   
                }
            });
            HideLoader();
        }
        function LoadsrNO() {
            ShowLoader();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSrNo.aspx") %>',
                data: { 'classId': $("#drpclass").val(), 'SectionId': $("#drpsection").val(), 'BranchId': $("#drpBranch").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    $("#div_srno").html("");
                    $("#div_srno").html(result.responseText);
                   
                }
            });
            HideLoader();
        }
        function LoadTerm() {
            ShowLoader();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlterms_MarkEntry.aspx") %>',
                data: {},
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    $("#div_terms").html("");
                    $("#div_terms").html(result.responseText);
                    
                }
            });
            HideLoader();
        }


        function LoadData() {
            $("[id*=icons]").addClass("hide");
            $("[id*=divExport]").html("");
            
            

            var ClassId = $("#drpclass").val();
            var SectionId = $("#drpsection").val();
            var BranchId = $("#drpBranch").val();
            var srno = $("#drpsrno").val();
            var TermId = $("#ddlTerm").val();
            var TermName = $("#ddlTerm option:selected").text();
            var AttendanceType = $("#drpAttendance").val();
            var Currdate = $("[id*=txtDate]").val();
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            if (ClassId == "") { alert("Select Class"); return true; }
            else if (SectionId == "") { alert("Select Section"); return true; }
            else if (BranchId == "") { alert("Select Branch"); return true; }
            else if (TermId == "") { alert("select Exam Term"); return true; }
            else {
                var headerDiv = $("[id*=headerDiv]").html();
                ShowLoader();
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("server/ReportCardServer.aspx") %>',
                    data: {
                        'ClassId': ClassId,
                        'SectionId': SectionId,
                        'BranchId': BranchId,
                        'srno': srno,
                        'TermId': TermId,
                        'TermName': TermName,
                        'AttendanceType': AttendanceType,
                        'SessionName': SessionName,
                        'BranchCode': BranchCode,
                        'Currdate': Currdate
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
        }
    </script>
    
</asp:Content>
