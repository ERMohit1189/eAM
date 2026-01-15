<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/17/comman_root_manager.master" CodeFile="SubjectwiseCumulative.aspx.cs" Inherits="_17_G2_SubjectwiseCumulative" %>

<%--<%@ Page Title="" Language="C#" MasterPageFile="~/17/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarksEntryPrePrimary.aspx.cs" Inherits="MarksEntryPrePrimary" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../../js/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.4.1/jquery.easing.min.js"></script>
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
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_drpclass">
                                        <select id="drpclass" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_section">
                                        <select id="drpsection" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Evaluation&nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <select id="drpEval" class="form-control-blue">
                                            <option value="TERM1">TERM1</option>
                                            <option value="TERM2">TERM2</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Subject">
                                        <select id="drpSubject" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Paper">
                                        <select id="drpPaper" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Tab Index&nbsp;<span class="vd_red">*</span></label>
                                    <div id="">
                                        <select id="ddlTabIndex" class="form-control-blue">
                                            <option value='Vertical'>Vertical</option>
                                            <option value='Horizontal'>Horizontal</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                             <div class="col-sm-12   mgbt-xs-15">
                                <span class="txt-bold txt-middle-l text-primary">Note:- </span><span class="txt-bold txt-middle-l text-danger blink">Type ML For Medical Leave, NAD for New Admission and NP for Not Present.</span>
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
            $(document).on('blur', 'input[type=text]', function () {
                $(this).removeClass('borders');
                $(this).val(this.value.toUpperCase());
                var values = $.trim($(this).val());
                var strss = /^(\+|-)?(\d*\.?\d*)$/;
                if (values.match(strss)) {
                    //return true;
                }
                else {
                    if (values == "" || values == "NP" || values == "NAD" || values == "ML") {
                    }
                    else
                    {
                        $(this).val('');
                        $(this).focus();
                        return false;
                    }
                }

                var thisVal = 0;
                if (values != 'NP' && values != 'NAD' && values != 'ML' && isNaN(parseFloat($(this).val()))) {
                    $(this).val('');
                }
                else {
                    if (values != 'NP' && values != 'NAD' && values != 'ML' && !isNaN(parseFloat($(this).val()))) {
                        thisVal = parseFloat($(this).val());
                    }

                    var Test1 = 0, Test2 = 0, TH = 0, Prac = 0;
                    var MaxMarks1 = 0, MaxMarks2 = 0, MaxMarks4 = 0, MaxMarks5 = 0;
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val())) && !$(this).closest('tr').find('td:eq(4)').hasClass('hide')) {
                        Test1 = parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val())) && !$(this).closest('tr').find('td:eq(5)').hasClass('hide')) {
                        Test2 = parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(7) input[type=text]').val()))) {
                        TH = parseFloat($(this).closest('tr').find('td:eq(7) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(8) input[type=text]').val()))) {
                        Prac = parseFloat($(this).closest('tr').find('td:eq(8) input[type=text]').val());
                    }
                    
                    if (!isNaN(parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val())) && !$("[id*=divList] table tbody tr:eq(0) th:eq(4)").hasClass('hide')) {
                        MaxMarks1 = parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val());
                    }
                    if (!isNaN(parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val())) && !$("[id*=divList] table tbody tr:eq(0) th:eq(5)").hasClass('hide')) {
                        MaxMarks2 = parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val());
                    }
                    if (!isNaN(parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(7)").find("table tbody tr:eq(0) th:eq(0) input[type=text]").val()))) {
                        MaxMarks4 = parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(7)").find("table tbody tr:eq(0) th:eq(0) input[type=text]").val());
                    }
                    if (!isNaN(parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(7)").find("table tbody tr:eq(0) th:eq(1) input[type=text]").val()))) {
                        MaxMarks5 = parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(7)").find("table tbody tr:eq(0) th:eq(1) input[type=text]").val());
                    }
                   
                    var percentle1 = 0, total = 0;
                    percentle1 = (Test1 + Test2) / 2;
                    total = parseFloat(percentle1 + TH + Prac);

                    var grade1 = "";
                    if (total < 33) {
                        grade1 = "E";
                    }
                    else if (total >= 33 && total < 41) {
                        grade1 = "D";
                    }
                    else if (total >= 41 && total < 51) {
                        grade1 = "C2";
                    }
                    else if (total >= 51 && total < 61) {
                        grade1 = "C1";
                    }
                    else if (total >= 61 && total < 71) {
                        grade1 = "B2";
                    }
                    else if (total >= 71 && total < 81) {
                        grade1 = "B1";
                    }
                    else if (total >= 81 && total < 91) {
                        grade1 = "A2";
                    }
                    else if (total >= 91 && total <= 100) {
                        grade1 = "A1";
                    }
                    else {
                        grade1 = "";
                    }
                    var isMax = "";
                    if ($(this).attr('name') == "1") {
                        if (parseFloat(thisVal) > parseFloat(MaxMarks1)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    if ($(this).attr('name') == "2") {
                        if (parseFloat(thisVal) > parseFloat(MaxMarks2)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    
                    if ($(this).attr('name') == "6") {
                        if (parseFloat(thisVal) > parseFloat(MaxMarks4)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    if ($(this).attr('name') == "7") {
                        if (parseFloat(thisVal) > parseFloat(MaxMarks5)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    
                    if (isMax != "Yes") {
                        $(this).closest('tr').find('td:eq(6) span').html('');
                        $(this).closest('tr').find('td:eq(9) span').html('');
                        $(this).closest('tr').find('td:eq(10) span').html('');

                        $(this).closest('tr').find('td:eq(6) span').html(percentle1.toFixed(2));
                        $(this).closest('tr').find('td:eq(9) span').html(total.toFixed(0));
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
                LoadSection();
            });

            $(document).on('change', '#drpsection', function () {
                $("[id*=divList]").html("");
                LoadSubject();
            });
            $(document).on('change', '#drpEval', function () {
                $("[id*=divList]").html("");
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
        });

        function LoadClass() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlClass_MarkEntry_2.aspx") %>',
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

        function LoadSection() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSection_MarkEntry_2.aspx") %>',
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
                url: '<%=ResolveUrl("Server/ddlSubject_MarkEntry_2.aspx") %>',
                data: { 'classId': $("#drpclass").val(), 'sectionId': $("#drpsection").val() },
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
                url: '<%=ResolveUrl("Server/ddlPaper_MarkEntry_2.aspx") %>',
                data: { 'classId': $("#drpclass").val(), 'SubjectId': $("#drpSubject").val() },
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
            var ClassName = $("[id*=drpclass] option:selected").text();
            var ClassId = $("#drpclass").val();
            var SectionName = $("[id*=drpsection] option:selected").text();
            var SubjectId = $("[id*=drpSubject]").val();
            var PaperId = $("[id*=drpPaper]").val();
            var Evail = $("[id*=drpEval]").val();
            var Session = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/SubjectWisePrePrimaryServer.aspx") %>',
                data: {
                    'ClassName': ClassName,
                    'SectionName': SectionName,
                    'Session': Session,
                    'BranchCode': BranchCode,
                    'Evail': Evail,
                    'SubjectId': SubjectId,
                    'PaperId': PaperId,
                    'ClassId': ClassId
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

        function SaveMarks() {
            $("input[type=text]").removeClass("borders");
            var error = "";
            $("[id*=divList] table tbody tr th input[type=text]").each(function () {
                var values = this.value.toUpperCase();
                if (values == "NP" || values == "ML" || values == "NAD" || values == "") {
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
                if (values == "NP" || values == "ML" || values == "NAD" || values == "") {
                    values = 0;
                }
                if (isNaN(parseFloat(values))) {
                    $(this).addClass("borders");
                    $(this).focus();
                    error = "error";
                }
            });
            if (error != "") {
                alert("Wrong Input:: Type ML, NAD, NP or Numbers in Box!");
                return;
            }
            ShowLoader();

            var ClassIds = $("#drpclass").val()
            var SectionName = $("#drpsection option:selected").text();
            var SubjectId = $("#drpSubject").val();
            var PaperId = $("#drpPaper").val();
            var TermName = $("#drpEval").val();
            var Session = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var LoginName = '<%=HttpContext.Current.Session["LoginName"] %>';
            var MaxMarks = "";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(7)").find("table tr:eq(0) th:eq(0) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(7)").find("table tr:eq(0) th:eq(1) input[type=text]").val();
            var Marks = "";
            var counts=$("[id*=divList] table tbody tr").length;
            counts=counts-1;
            for (var i = 1; i < counts; i++) {
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(1) span").html() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(4) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(5) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(7) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(8) input[type=text]").val() + "$";
            }
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/SaveMarksEntryPrePrimaryServer.aspx") %>',
                data: {
                    'ClassId': ClassIds,
                    'SectionName': SectionName,
                    'SubjectId': SubjectId,
                    'PaperId': PaperId,
                    'TermName': TermName,
                    'Session': Session,
                    'BranchCode': BranchCode,
                    'LoginName': LoginName,
                    'MaxMarks': MaxMarks,
                    'Marks': Marks
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
            $("[id*=drpsection]").val("");
            $("[id*=drpEval]").val("TERM1");
            $("[id*=drpSubject]").val("");
            $("[id*=drpPaper]").val("");
            $("[id*=ddlTabIndex]").val("Vertical");
        }
    </script>
</asp:Content>


