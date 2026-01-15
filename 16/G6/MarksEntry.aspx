<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarksEntry.aspx.cs" Inherits="MarksEntry" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (charCode === 46) {
                var inputValue = $("#inputfield").val();
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode !== 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <div id="loader" runat="server"></div>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12 no-padding" id="div1" runat="server">
                            <div class="col-sm-12  no-padding ">
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_drpclass">
                                        <select id="drpclass" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_branch">
                                        <select id="drpBranch" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Group&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Stream">
                                        <select id="drpStream" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_section">
                                        <select id="drpsection" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Medium">
                                        <select id="drpMedium" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Evaluation&nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <select id="drpEval" class="form-control-blue">
                                            <option value="TERM1">TERM1</option>
                                            <option value="TERM2">TERM2</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Subject">
                                        <select id="drpSubject" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Paper">
                                        <select id="drpPaper" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                 <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Status&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Status">
                                        <select id="drpStatus" class="form-control-blue">
                                            <option value='0'>All</option>
                                             <option value='A' selected="selected">Active</option>
                                             <option value='AB'>Active & Blocked</option>
                                             <option value='W'>Withdrawal</option>
                                             <option value='B'>Blocked</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Tab Index&nbsp;<span class="vd_red">*</span></label>
                                    <div id="">
                                        <select id="ddlTabIndex" class="form-control-blue">
                                            <option value='Vertical'>Vertical</option>
                                            <option value='Horizontal'>Horizontal</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                             <div class="col-sm-12 mgbt-xs-15">
                                <span class="txt-bold txt-middle-l text-primary">Note:- </span><span class="txt-bold txt-middle-l text-danger blink">Type ML For Medical Leave, NAD for New Admission and AB for Absent.</span>
                            </div>
                            <div class="col-sm-12  ">
                                <div class=" table-responsive  table-responsive2 " id="divList" runat="server">
                                </div>
                                <div class="col -sm-12  text-center"><div id="msgbox" runat="server" style="left: 155px;"></div></div>
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

        .borders2 {
            border: 1px solid #D5D5D5;
        }

        .blink {
            animation-duration: 1200ms;
            animation-name: blink;
            animation-iteration-count: infinite;
            animation-direction: alternate;
            -webkit-animation: blink 1200ms infinite; /* Safari and Chrome */
        }

        @keyframes blink {
            from {
                color: yellow;
            }

            to {
                color: red;
            }
        }

        @-webkit-keyframes blink {
            from {
                color: yellow;
            }

            to {
                color: red;
            }
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
            LoadMedium();
            $(document).on('blur', 'input[type=text]', function () {
                $(this).removeClass('borders');
                $(this).val(this.value.toUpperCase());
                var values = $.trim($(this).val());
               // var strss = /^(\+|-)?(\d*\.?\d*)$/;
                var strss = /^(\+|-)?(\d+)$/;
                if (values.match(strss))
                {
                    //return true;
                }
                else {
                    if (values == "" || values == "AB" || values == "NAD" || values == "ML")
                    {
                       // alert("dh")
                    }
                    else {
                       // alert("2")
                        $(this).val('');
                        $(this).focus();
                        return false;
                    }
                }
                var total = 0;
                var thisVal = 0;
                if (values == "" || values != 'AB' && values != 'NAD' && values != 'ML' && isNaN(parseFloat($(this).val())))
                {
                    $(this).val('');
                    if (values != 'AB' && values != 'NAD' && values != 'ML' && !isNaN(parseFloat($(this).val()))) {
                        thisVal = parseFloat($(this).val());
                    }
                    var UT1 = 0, UT2 = 0, UT3 = 0, HYTH = 0, HYPrac = 0;
                    var UT1m = 0, UT2m = 0, UT3m = 0, HYTHm = 0, HYPracm = 0;
                    if (!isNaN(parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]').val()))) {
                        UT1m = parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]').val()))) {
                        UT2m = parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]').val()))) {
                        UT3m = parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($('[id*=divList] table tbody tr:eq(1) th:eq(0) input[type=text]').val()))) {
                        HYTHm = parseFloat($('[id*=divList] table tbody tr:eq(1) th:eq(0) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($('[id*=divList] table tbody tr:eq(1) th:eq(1) input[type=text]').val()))) {
                        HYPracm = parseFloat($('[id*=divList] table tbody tr:eq(1) th:eq(1) input[type=text]').val());
                    }


                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val()))) {
                        UT1 = parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val()))) {
                        UT2 = parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(6) input[type=text]').val()))) {
                        UT3 = parseFloat($(this).closest('tr').find('td:eq(6) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(7) input[type=text]').val()))) {
                        HYTH = parseFloat($(this).closest('tr').find('td:eq(7) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(8) input[type=text]').val()))) {
                        HYPrac = parseFloat($(this).closest('tr').find('td:eq(8) input[type=text]').val());
                    }

                    let testtotal1 = UT1 + UT2 + UT3;

                    // Find the minimum score among UT1, UT2, UT3
                    let minScore = Math.min(UT1, UT2, UT3);

                    // Subtract the minimum score from total to get sum of best two UTs
                    let sumOfBestTwo = testtotal1 - minScore;
                    total = parseInt(sumOfBestTwo + HYTH + HYPrac);
                    //  total = parseFloat(UT1 + UT2+UT3 + HYTH + HYPrac);
                    var grade1 = "";
                    if (total >= 0) {

                        if (total <= 53) {
                            grade1 = "D";
                        }
                        else if (total > 53 && total <= 76) {
                            grade1 = "C";
                        }
                        else if (total > 76 && total <= 91) {
                            grade1 = "B";
                        }
                        else if (total > 91 && total <= 106) {
                            grade1 = "B+";
                        }
                        else if (total > 106 && total <= 128) {
                            grade1 = "A";
                        }
                        else if (total > 128 && total <= 150) {
                            grade1 = "A+";
                        }
                        else {
                            grade1 = "";
                        }
                    }

                    var isMax = "";
                    if ($(this).attr('name') == "1") {
                        if (parseFloat(thisVal) > parseFloat(UT1m)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    if ($(this).attr('name') == "2") {
                        if (parseFloat(thisVal) > parseFloat(UT2m)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    if ($(this).attr('name') == "3") {
                        if (parseFloat(thisVal) > parseFloat(UT3m)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    if ($(this).attr('name') == "4") {
                        if (parseFloat(thisVal) > parseFloat(HYTHm)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    if ($(this).attr('name') == "5") {
                        if (parseFloat(thisVal) > parseFloat(HYPracm)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }

                    if (isMax != "Yes") {
                        $(this).closest('tr').find('td:eq(9) span').html('');
                        $(this).closest('tr').find('td:eq(10) span').html('');

                        $(this).closest('tr').find('td:eq(9) span').html(total.toFixed(2));
                        $(this).closest('tr').find('td:eq(10) span').html(grade1);
                    }
                    if (isMax == "Yes") {
                        $(this).val('');
                    }
                }
                else {
                    if (values != 'AB' && values != 'NAD' && values != 'ML' && !isNaN(parseFloat($(this).val()))) {
                        thisVal = parseFloat($(this).val());
                    }
                    var UT1 = 0, UT2 = 0, UT3 = 0, HYTH = 0, HYPrac = 0;
                    var UT1m = 0, UT2m = 0, UT3m = 0, HYTHm = 0, HYPracm = 0;
                    if (!isNaN(parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]').val()))) {
                        UT1m = parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]').val()))) {
                        UT2m = parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]').val()))) {
                        UT3m = parseFloat($('[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($('[id*=divList] table tbody tr:eq(1) th:eq(0) input[type=text]').val()))) {
                        HYTHm = parseFloat($('[id*=divList] table tbody tr:eq(1) th:eq(0) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($('[id*=divList] table tbody tr:eq(1) th:eq(1) input[type=text]').val()))) {
                        HYPracm = parseFloat($('[id*=divList] table tbody tr:eq(1) th:eq(1) input[type=text]').val());
                    }


                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val()))) {
                        UT1 = parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val()))) {
                        UT2 = parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(6) input[type=text]').val()))) {
                        UT3 = parseFloat($(this).closest('tr').find('td:eq(6) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(7) input[type=text]').val()))) {
                        HYTH = parseFloat($(this).closest('tr').find('td:eq(7) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(8) input[type=text]').val()))) {
                        HYPrac = parseFloat($(this).closest('tr').find('td:eq(8) input[type=text]').val());
                    }

                    let testtotal1 = UT1 + UT2 + UT3;

                    // Find the minimum score among UT1, UT2, UT3
                    let minScore = Math.min(UT1, UT2, UT3);

                    // Subtract the minimum score from total to get sum of best two UTs
                    let sumOfBestTwo = testtotal1 - minScore;
                    total = parseInt(sumOfBestTwo + HYTH + HYPrac);
                  //  total = parseFloat(UT1 + UT2+UT3 + HYTH + HYPrac);
                    var grade1 = "";
                    if (total >= 0) {

                        if (total <= 53)
                        {
                            grade1 = "D";
                        }
                        else if (total > 53 && total <= 76) {
                            grade1 = "C";
                        }
                        else if (total > 76 && total <= 91) {
                            grade1 = "B";
                        }
                        else if (total > 91 && total <= 106) {
                            grade1 = "B+";
                        }
                        else if (total > 106 && total <= 128) {
                            grade1 = "A";
                        }
                        else if (total > 128 && total <= 150) {
                            grade1 = "A+";
                        }
                        else {
                            grade1 = "";
                        }
                    }

                    var isMax = "";
                    if ($(this).attr('name') == "1") {
                        if (parseFloat(thisVal) > parseFloat(UT1m)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    if ($(this).attr('name') == "2") {
                        if (parseFloat(thisVal) > parseFloat(UT2m)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    if ($(this).attr('name') == "3") {
                        if (parseFloat(thisVal) > parseFloat(UT3m)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    if ($(this).attr('name') == "4") {
                        if (parseFloat(thisVal) > parseFloat(HYTHm)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    if ($(this).attr('name') == "5") {
                        if (parseFloat(thisVal) > parseFloat(HYPracm)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    
                    if (isMax != "Yes") {
                        $(this).closest('tr').find('td:eq(9) span').html('');
                        $(this).closest('tr').find('td:eq(10) span').html('');

                        $(this).closest('tr').find('td:eq(9) span').html(total.toFixed(2));
                        $(this).closest('tr').find('td:eq(10) span').html(grade1);
                    }
                    if (isMax == "Yes") {
                        $(this).val('');
                    }
                }
            });
            $(document).on('click', '#lnkSubmit', function () {
                $("[id*=divList]").attr("disabled");
               
                SaveMarks();
            });
            LoadClass();

            $(document).on('change', '#drpclass', function () {
                $("[id*=divList]").html("");
                LoadBranch();
            });
            $(document).on('change', '#drpBranch', function () {
                $("[id*=divList]").html("");
                LoadStream();
                LoadSection();
            });
            
            $(document).on('change', '#drpsection', function () {
                $("[id*=divList]").html("");
                LoadSubject();
            });
            $(document).on('change', '#drpEval', function () {
                $("[id*=divList]").html("");
                $('#drpSubject').val("")
                $('#drpPaper').html("<option value=''><--Select--></option>")
                LoadSubject();
            });
            $(document).on('change', '#drpMedium', function () {
                $("[id*=divList]").html("");
                $('#drpSubject').val("")
                $('#drpPaper').html("<option value=''><--Select--></option>")
                LoadSubject();
            });
            $(document).on('change', '#drpSubject', function () {
                $("[id*=divList]").html("");
                LoadPaper();
            });
            $(document).on('change', '#drpPaper', function () {
                if ($("[id*=drpPaper]").val() == "") {
                    $("[id*=divList]").html("");
                }
                else {
                    LoadFillList();
                }
            });
            
            $(document).on('change', '#ddlTabIndex', function () {
                LoadFillList();
            });
            $(document).on('change', '#drpStatus', function () {
                LoadFillList();
            });
        });
        function LoadMedium() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlMedium_MarkEntry_6.aspx") %>',
                data: {},
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_Medium").html("");
                    $("#div_Medium").html(result.responseText);
                    HideLoader();
                }
            });
        }
        function LoadClass() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlClass_MarkEntry_6.aspx") %>',
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
        function LoadBranch() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlBranch_MarkEntry_6.aspx") %>',
                data: { 'classId': $("#drpclass").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_branch").html("");
                    $("#div_branch").html(result.responseText);
                    HideLoader();
                }
            });
        }
        function LoadStream() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlStream_6.aspx") %>',
                data: { 'classId': $("#drpclass").val(), 'Branchid': $("#drpBranch").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_Stream").html("");
                    $("#div_Stream  ").html(result.responseText);
                    HideLoader();
                }
            });
        }
        function LoadSection() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSection_MarkEntry_6.aspx") %>',
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
        function LoadSubject() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSubject_MarkEntry_6_medium.aspx") %>',
                data: { 'classId': $("#drpclass").val(), 'sectionId': $("#drpsection").val(), 'BranchId': $("#drpBranch").val(), 'Medium': $("#drpMedium").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_Subject").html("");
                    $("#div_Subject").html(result.responseText);
                    HideLoader();
                }
            });
        }

        function LoadPaper() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlPaper_MarkEntry_6_medium.aspx") %>',
                data: { 'classId': $("#drpclass").val(), 'SubjectId': $("#drpSubject").val(), 'BranchId': $("#drpBranch").val(), 'Medium': $("#drpMedium").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_Paper").html("");
                    $("#div_Paper").html(result.responseText);
                    HideLoader();
                }
            });
        }
        
        function LoadFillList() {
            
           $("[id*=divList]").html("");
            ShowLoader();
            var SectionName = $("[id*=drpsection] option:selected").text();
            var Evail = $("[id*=drpEval]").val();
            var SubjectId = $("[id*=drpSubject]").val();
            var PaperId = $("[id*=drpPaper]").val();
             var Session = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var ClassId = $("#drpclass").val();
            var BranchId = $("#drpBranch").val();
            var StreamId = $("#drpStream").val();
            var SectionId = $("#drpsection").val();
            var Medium = $("#drpMedium").val();
            var Status = $("#drpStatus").val();
            if (BranchId == "" || StreamId == "") {
                HideLoader();
                $("[id*=divList]").html('');
                return;
            }
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/LoadMarksEntry.aspx")%>',
                data: {
                    'SectionName': SectionName,
                    'Evail': Evail,
                    'SubjectId': SubjectId,
                    'PaperId': PaperId,
                    'Session': Session,
                    'BranchCode': BranchCode,
                    'ClassId': ClassId,
                    'BranchId': BranchId,
                    'StreamId': StreamId,
                    'SectionId': SectionId,
                    'Medium': Medium,
                    'Status': Status
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    HideLoader();
                    $("[id*=divList]").html(result.responseText);
                    if ($("[id*=ddlTabIndex]").val() == "Vertical") {
                        TabFunction();
                    }
                    if ($("[id*=divList]").html()!="") {
                        $("[id*=lnkSubmit]").removeClass("hide");
                    }
                },
                error: function (result) {
                    HideLoader();
                    $("[id*=divList]").html(result.responseText);
                    if ($("[id*=ddlTabIndex]").val() == "Vertical") {
                        TabFunction();
                    }
                    if ($("[id*=divList]").html() != "") {
                        $("[id*=lnkSubmit]").removeClass("hide");
                    }
                }
            });
        }

        function TabFunction() {
            $('[id*=divList] table tbody tr').each(function () {
                $(this).closest('tr').find('td').each(function (i) {
                    $(this).find('input').attr('tabindex', i + 1);
                });
            });
        }
        // Save Marks and also save max mark if not exists

      <%--  function SaveMarks() {
            $("input[type=text]").removeClass("borders");
            var error = "";
            $("[id*=divList] table tbody tr th input[type=text]").each(function () {
                var values = this.value.toUpperCase();
                if (values == "AB" || values == "ML" || values == "NAD" || values == "") {
                    values = 0;
                }
                if (isNaN(parseFloat(values))) {
                    $(this).addClass("borders");
                    $(this).focus();
                    alert("Wrong Input:: Please enter all maximum marks!");
                    return;
                }
            });
            $("[id*=divList] table tbody tr td input[type=text]").each(function () {
                var values = this.value.toUpperCase();
                if (values == "AB" || values == "ML" || values == "NAD" || values == "") {
                    values = 0;
                }
                if (isNaN(parseFloat(values))) {
                    $(this).addClass("borders");
                    $(this).focus();
                    alert("Wrong Input:: Type ML, NAD, AB or Numbers in Box!");
                    return;
                }
            });
           ShowLoader();
            var ClassId = $("#drpclass").val();
            var BranchId = $("#drpBranch").val();
            var StreamId = $("#drpStream").val();
            var SectionId = $("#drpsection").val();
            var Evail = $("[id*=drpEval]").val();
            var SubjectId = $("[id*=drpSubject]").val();
            var PaperId = $("[id*=drpPaper]").val();
            var Session = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var LoginName = '<%=HttpContext.Current.Session["LoginName"] %>';
            var Medium = $("#drpMedium").val();
            var ClassName = $("[id*=drpclass] option:selected").text();
            var MaxMarks = "";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(1) th:eq(0) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(1) th:eq(1) input[type=text]").val();
            var Marks = "";
            var counts=$("[id*=divList] table tbody tr").length;
            for (var i = 2; i < counts; i++) {
                Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(1) span").html() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(4) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(5) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(6) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(7) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(8) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(9) span").html() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(10) span").html() + "$";
            }
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/SaveMarksEntry.aspx")%>',
                data: {
                    'ClassId': ClassId,
                    'BranchId': BranchId,
                    'StreamId': StreamId,
                    'SectionId': SectionId,
                    'Evaluation': Evail,
                    'SubjectId': SubjectId,
                    'PaperId': PaperId,
                    'SessionName': Session,
                    'BranchCode': BranchCode,
                    'LoginName': LoginName,
                    'MaxMarks': MaxMarks,
                    'Marks': Marks,
                    'Medium': Medium,
                    'ClassName': ClassName
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    HideLoader();
                    $("[id*=divList]").html(result.responseText);
                    if ($("[id*=ddlTabIndex]").val() == "Vertical") {
                        TabFunction();
                    }
                    if ($("[id*=divList]").html() != "") {
                        $("[id*=lnkSubmit]").removeClass("hide");
                    }
                },
                error: function (result) {
                    HideLoader();
                    $("[id*=divList]").html(result.responseText);
                    if ($("[id*=ddlTabIndex]").val() == "Vertical") {
                        TabFunction();
                    }
                    if ($("[id*=divList]").html() != "") {
                        $("[id*=lnkSubmit]").removeClass("hide");
                    }
                }
            });
        }--%>

        function SaveMarks() {
         
            $("input[type=text]").removeClass("borders");
            var error = "";
            $("[id*=divList] table tbody tr th input[type=text]").each(function () {
                var values = this.value.toUpperCase();
                if (values == "AB" || values == "ML" || values == "NAD" || values == "") {
                    values = 0;
                }
                if (isNaN(parseFloat(values))) {
                    $(this).addClass("borders");
                    $(this).focus();
                    error = "error";
                }
            });
            $("[id*=divList] table tbody tr td input[type=text]").each(function () {
                var values = this.value.toUpperCase();
                if (values == "AB" || values == "ML" || values == "NAD" || values == "") {
                    values = 0;
                }
                if (isNaN(parseFloat(values))) {
                    $(this).addClass("borders");
                    $(this).focus();
                    error = "error";
                }
            });
            if (error != "") {
                alert("Wrong Input:: Type ML, NAD, AB or Numbers in Box!");
                return;
            }
            ShowLoader();

           // var ClassIds = $("#drpclass").val()
           // var SectionName = $("#drpsection option:selected").text();
            var SubjectId = $("#drpSubject").val();
            var PaperId = $("#drpPaper").val();
            //var TermName = $("#drpEval").val();
            var Session = '<%=HttpContext.Current.Session["SessionName"] %>';
             var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var LoginName = '<%=HttpContext.Current.Session["LoginName"] %>';



            var ClassId = $("#drpclass").val();
            var BranchId = $("#drpBranch").val();
            var StreamId = $("#drpStream").val();
            var SectionId = $("#drpsection").val();
            var Evail = $("[id*=drpEval]").val();
            var SubjectId = $("[id*=drpSubject]").val();
            var PaperId = $("[id*=drpPaper]").val();
            var Session = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var LoginName = '<%=HttpContext.Current.Session["LoginName"] %>';
            var Medium = $("#drpMedium").val();
            var ClassName = $("[id*=drpclass] option:selected").text();


            var MaxMarks = "";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(1) th:eq(0) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(1) th:eq(1) input[type=text]").val();
          //  alert(MaxMarks)
           // MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val() + "##";
           // MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val() + "##";
            //MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(7)").find("table tr:eq(0) th:eq(0) input[type=text]").val() + "##";
           // MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(7)").find("table tr:eq(0) th:eq(1) input[type=text]").val();
            var Marks = "";
            var counts = $("[id*=divList] table tbody tr").length;
           // alert(counts);
            //counts = counts - 1;
           // for (var i = 1; i < counts; i++) {
            for (var i = 2; i < counts; i++) {
              //  alert();
                //Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(1) span").html() + "##";
                //Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(4) input[type=text]").val() + "##";
                //Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(5) input[type=text]").val() + "##";
                //Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(7) input[type=text]").val() + "##";
                //    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(8) input[type=text]").val() + "$";

                    Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(1) span").html() + "##";
                    Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(4) input[type=text]").val() + "##";
                    Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(5) input[type=text]").val() + "##";
                    Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(6) input[type=text]").val() + "##";
                    Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(7) input[type=text]").val() + "##";
                    Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(8) input[type=text]").val() + "##";
                    Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(9) span").html() + "##";
                    Marks += $("[id*=divList] table tbody tr:eq(" + i + ") td:eq(10) span").html() + "$";
            }
           // alert(Marks);
       $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/SaveMarksEntry.aspx") %>',
                data: {
                    'ClassId': ClassId,
                    'BranchId': BranchId,
                    'StreamId': StreamId,
                    'SectionId': SectionId,
                    'Evaluation': Evail,
                    'SubjectId': SubjectId,
                    'PaperId': PaperId,
                    'SessionName': Session,
                    'BranchCode': BranchCode,
                    'LoginName': LoginName,
                    'MaxMarks': MaxMarks,
                    'Marks': Marks,
                    'Medium': Medium,
                    'ClassName': ClassName



                    //'ClassId': ClassIds,
                    //'SectionName': SectionName,
                    //'SubjectId': SubjectId,
                    //'PaperId': PaperId,
                    //'TermName': TermName,
                    //'Session': Session,
                    //'BranchCode': BranchCode,
                    //'LoginName': LoginName,
                    //'MaxMarks': MaxMarks,
                    //'Marks': Marks
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    HideLoader();
                    $("[id*=msgbox]").html(result.responseText);
                    if ($("[id*=msgbox] span").html() == "Marks Saved Successfully.") {
                        reset();
                    }
                    setTimeout(function () {
                        $("[id*=msgbox] span").html('');
                    }, 5000);
                },
                error: function (result) {
                    HideLoader();
                    $("[id*=msgbox]").html(result.responseText);
                    if ($("[id*=msgbox] span").html() == "Marks Saved Successfully.") {
                        reset();
                    }
                    setTimeout(function () {
                        $("[id*=msgbox] span").html('');
                    }, 5000);
                }
            });
         }
        function reset()
        {
            $("[id*=lnkSubmit]").addClass("hide");
            $("[id*=divList]").html("");
            $("[id*=drpclass]").val("");
            $("[id*=drpBranch]").val("");
            $("[id*=drpStream]").val("");
            $("[id*=drpsection]").val("");
            $("[id*=drpEval]").val("TERM1");
            $("[id*=drpSubject]").val("");
            $("[id*=drpPaper]").val("");
            $("[id*=ddlTabIndex]").val("Vertical");
        }
    </script>
</asp:Content>
