<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarksEntry_ng.aspx.cs" Inherits="MarksEntry_ng" %>


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
                                    <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_drpBranch">
                                        <select id="drpBranch" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Subject">
                                        <select id="ddlSubject" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Term&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_terms">
                                        <select id="ddlTerm" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Tab Index&nbsp;<span class="vd_red">*</span></label>
                                    <div id="">
                                        <select id="ddlTabIndex" class="form-control-blue">
                                            <option value='Horizontal'>Horizontal</option>
                                            <option value='Vertical'>Vertical</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12   mgbt-xs-15">
                                <span class="txt-bold txt-middle-l text-primary">Note:- </span><span class="txt-bold txt-middle-l text-danger blink"> Please enter (AB) if student is absent else enter only numeric value,  in boxes.</span>
                            </div>
                            <div class="col-sm-12  ">
                                <div class=" table-responsive  table-responsive2 " id="divList" runat="server">
                                </div>
                                <div class="col -sm-12  text-center"><div id="msgbox" style="left: 155px;"></div></div>
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
                    data: { 'SessionName': $("#DrpSessionName").val() },
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
                $.trim($(this).val());

                var thisVal = 0;
                var abStatus = "";
                if (isNaN(parseFloat($(this).val()))) {
                    if ($(this).val() == "AB") {
                        $(this).closest('tr').css("background", "rgb(255, 164, 164)");
                        $(this).closest('tr').find('td:eq(10) span').html('AB');
                        $(this).closest('tr').find('td:eq(11) span').html('Absent');
                        $(this).val("AB");
                        return;
                    }
                    else {
                        var sts2 = 0;
                        var len = 0;
                        len = $(this).closest('tr').find('td').length
                        for (var i = 0; i < len; i++) {
                            var values = $(this).closest('tr').find('td:eq(' + i + ') input[type=text]').val();
                            if (values == 'ab' || values == 'AB') {
                                sts2 = sts2 + 1;
                            }
                        }
                        if (sts2 > 0) {
                            $(this).closest('tr').find('td:eq(10) span').html('AB');
                            $(this).closest('tr').find('td:eq(11) span').html('Absent');
                            $(this).closest('tr').css("background", "rgb(255, 164, 164)");
                        }
                    }
                    return;
                }
                else {
                    thisVal = parseFloat($(this).val());
                }

                var conutive = 0, affective = 0, phychomotor = 0, TotalCA  = 0, examss = 0, TotalMarks = 0;
                var Max_conutive = 0, Max_affective = 0, Max_phychomotor = 0, maxTotalCa = 0, Max_exam = 0, maxTotal = 0;
                if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val()))) {
                    conutive = parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val());
                }
                if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val()))) {
                    affective = parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val());
                }
                if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(6) input[type=text]').val()))) {
                    phychomotor = parseFloat($(this).closest('tr').find('td:eq(6) input[type=text]').val());
                }
                if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(8) input[type=text]').val()))) {
                    examss = parseFloat($(this).closest('tr').find('td:eq(8) input[type=text]').val());
                }
                
                if (!isNaN(parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val()))) {
                    Max_conutive = parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val());
                }
                if (!isNaN(parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val()))) {
                    Max_affective = parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val());
                }
                if (!isNaN(parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]").val()))) {
                    Max_phychomotor = parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]").val());
                }
                maxTotalCa = (parseFloat(Max_conutive) + parseFloat(Max_affective) + parseFloat(Max_phychomotor)).toString();
                $("[id*=lblMaxTotal]").html(parseFloat(maxTotalCa).toFixed(0));

                if (!isNaN(parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(8) input[type=text]").val()))) {
                    Max_exam = parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(8) input[type=text]").val());
                }
                maxTotal = (parseFloat(maxTotalCa) + parseFloat(Max_exam)).toString();
                $("[id*=lblMaxMarkObtained]").html(parseFloat(maxTotal).toFixed(0));
                
                TotalMarks = (parseFloat(conutive) + parseFloat(affective) + parseFloat(phychomotor) + parseFloat(examss)).toFixed(0);
                var grade = "";
                var tot = (parseFloat(conutive) + parseFloat(affective) + parseFloat(phychomotor) + parseFloat(examss)).toFixed(0);
                if (tot >= 75)
                {grade =  "A1"; }
                else if (tot >= 70)
                {grade =  "B2"; }
                else if (tot >= 65)
                {grade =  "B3"; }
                else if (tot >= 64)
                {grade =  "C4"; }
                else if (tot >= 55)
                {grade =  "C5"; }
                else if (tot >= 50)
                {grade =  "C6"; }
                else if (tot >= 45)
                {grade =  "D7"; }
                    else if (tot >= 40)
                {grade =  "E8"; }
                else 
                { grade = "F9"; }
                
                
                var teacherRemarks = "";
                if (tot >= 70)
                { teacherRemarks= "Excellent"; }
                else if (tot >= 60)
                { teacherRemarks= "Good"; }
                else if (tot >= 50)
                { teacherRemarks= "Average"; }
                else if (tot >= 40)
                { teacherRemarks = "B-average"; }
                else
                { teacherRemarks = "Fail"; }
                

                var isMax = "";
                if ($(this).attr('name') == "1") {
                    if (parseFloat(thisVal) > parseFloat(Max_conutive)) {
                        isMax = "Yes";
                        $(this).addClass('borders');
                        $(this).focus();
                    }
                }
                if ($(this).attr('name') == "2") {
                    if (parseFloat(thisVal) > parseFloat(Max_affective)) {
                        isMax = "Yes";
                        $(this).addClass('borders');
                        $(this).focus();
                    }
                }
                if ($(this).attr('name') == "3") {
                    if (parseFloat(thisVal) > parseFloat(Max_phychomotor)) {
                        isMax = "Yes";
                        $(this).addClass('borders');
                        $(this).focus();
                    }
                }
                if ($(this).attr('name') == "4") {
                    if (parseFloat(thisVal) > parseFloat(Max_exam)) {
                        isMax = "Yes";
                        $(this).addClass('borders');
                        $(this).focus();
                    }
                }
                
                if (isMax != "Yes") {
                    $(this).val(isNaN(parseFloat($(this).val())) ? "" : parseFloat($(this).val()).toFixed(1))
                    $(this).closest('tr').find('td:eq(7) span').html('');
                    $(this).closest('tr').find('td:eq(9) span').html('');
                    $(this).closest('tr').find('td:eq(10) span').html('');
                    $(this).closest('tr').find('td:eq(11) span').html('');
                    $(this).closest('tr').find('td:eq(7) span').html((parseFloat(conutive) + parseFloat(affective) + parseFloat(phychomotor))==0?"":(parseFloat(conutive) + parseFloat(affective) + parseFloat(phychomotor)).toFixed(1));
                    $(this).closest('tr').find('td:eq(9) span').html((parseFloat(conutive) + parseFloat(affective) + parseFloat(phychomotor) + parseFloat(examss))==0?"":(parseFloat(conutive) + parseFloat(affective) + parseFloat(phychomotor) + parseFloat(examss)).toFixed(0));
                    if (abStatus == "") {
                        
                        $(this).closest('tr').find('td:eq(10) span').html(grade.toString());
                        $(this).closest('tr').find('td:eq(11) span').html(teacherRemarks.toString());
                    }
                    else {
                        return true;
                    }
                }
                var sts2 = 0;
                var len=0;
                len=$(this).closest('tr').find('td').length
                for (var i = 0; i < len; i++) {
                    var values = $(this).closest('tr').find('td:eq(' + i + ') input[type=text]').val();
                    if (values == 'ab' || values == 'AB') {
                        sts2 = sts2 + 1;
                    }
                }
                if (sts2 > 0) {
                    $(this).closest('tr').find('td:eq(10) span').html('AB');
                    $(this).closest('tr').find('td:eq(11) span').html('Absent');
                    $(this).closest('tr').css("background", "rgb(255, 164, 164)");
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
                LoadBranch();
            });

            $(document).on('change', '#drpBranch', function () {
                $("[id*=divList]").html("");
                LoadSubject();
            });
            $(document).on('change', '#ddlSubject', function () {
                $("[id*=divList]").html("");
                LoadTerm();
            });

            $(document).on('change', '#ddlTerm', function () {
                if ($("#ddlTerm").val() == "") {
                    $("[id*=divList]").html("");
                }
                else {
                    var ClassId = $("#drpclass").val();
                    var SectionId = $("#drpsection").val();
                    var BranchId = $("#drpBranch").val();
                    var SubjectId = $("#ddlSubject").val();
                    var TermId = $("#ddlTerm").val();
                    if (ClassId == "") { alert("Select Class"); return true; }
                    else if (SectionId == "") { alert("Select Section"); return true; }
                    else if (BranchId == "") { alert("Select Branch"); return true; }
                    else if (SubjectId == "") { alert("select Subject"); return true; }
                    else if (TermId == "") { alert("select Exam Term"); return true; }
                    else {
                        LoadFillList();
                    }
                }
            });
            
            $(document).on('change', '#ddlTabIndex', function () {
                var ClassId = $("#drpclass").val();
                var SectionId = $("#drpsection").val();
                var BranchId = $("#drpBranch").val();
                var SubjectId = $("#ddlSubject").val();
                var TermId = $("#ddlTerm").val();
                if (ClassId == "") { alert("Select Class"); return true; }
                else if (SectionId == "") { alert("Select Section"); return true; }
                else if (BranchId == "") { alert("Select Branch"); return true; }
                else if (SubjectId == "") { alert("select Subject"); return true; }
                else if (TermId == "") { alert("select Exam Term"); return true; }
                else {
                    LoadFillList();
                }
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
                    HideLoader();
                }
            });
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
                    HideLoader();
                }
            });
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
                    HideLoader();
                }
            });
        }

        function LoadSubject() {
            ShowLoader();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSubject_MarkEntry.aspx") %>',
                data: { 'classId': $("#drpclass").val(), 'branchId': $("#drpBranch").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    $("#div_Subject").html("");
                    $("#div_Subject").html(result.responseText);
                    HideLoader();
                }
            });
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
                    HideLoader();
                }
            });
        }
        
        function LoadFillList() {
            ShowLoader();
            $("[id*=divList]").html("");
            var ClassId = $("#drpclass").val();
            var SectionId = $("#drpsection").val();
            var BranchId = $("#drpBranch").val();
            var SubjectId = $("#ddlSubject").val();
            var TermId = $("#ddlTerm").val();
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("server/MarksEntryServer.aspx") %>',
                data: {
                    'ClassId': ClassId,
                    'SectionId':SectionId,
                    'BranchId': BranchId,
                    'SubjectId': SubjectId,
                    'TermId': TermId,
                    'SessionName': SessionName
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
        }

        function TabFunction() {
            $("[id*=divList] table tbody tr").each(function () {
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
                if (values == "") {
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
                if (values == "") {
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

            var ClassId = $("#drpclass").val();
            var SectionId = $("#drpsection").val();
            var BranchId = $("#drpBranch").val();
            var SubjectId = $("#ddlSubject").val();
            var TermId = $("#ddlTerm").val();
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            var LoginName = '<%=HttpContext.Current.Session["LoginName"] %>';
           
                var MaxMarks = "";
                MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val() + "##";
                MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val() + "##";
                MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]").val() + "##";
                MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(7) span:eq(1)").html() + "##";
                MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(8) input[type=text]").val() + "##";
                MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(9) span:eq(1)").html() + "##";

                var Marks = "";
                var counts = $("[id*=divList] table tbody tr").length;
                counts = counts - 1;
                for (var i = 0; i < counts; i++) {
                    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(1) span").html() + "##"; // Srno
                    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(4) input[type=text]").val() + "##"; // conutive
                    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(5) input[type=text]").val() + "##"; // affective
                    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(6) input[type=text]").val() + "##"; // phychomotor
                    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(7) span").html() + "##"; // totalCA
                    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(8) input[type=text]").val() + "##"; // exam
                    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(9) span").html() + "##"; // totalMarks
                    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(10) span").html() + "##"; // Grade
                    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(11) span").html() + "$"; // teacher remarks
                }
                if (ClassId == "") { alert("Select Class"); return true; }
                else if (SectionId == "") { alert("Select Section"); return true; }
                else if (BranchId == "") { alert("Select Branch"); return true; }
                else if (SubjectId == "") { alert("select Subject"); return true; }
                else if (TermId == "") { alert("select Exam Term"); return true; }
                else {
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("Server/SaveMarksEntryServer.aspx") %>',
                    data: {
                        'ClassId': ClassId,
                        'SectionId': SectionId,
                        'BranchId': BranchId,
                        'SubjectId': SubjectId,
                        'TermId': TermId,
                        'SessionName': SessionName,
                        'LoginName': LoginName,
                        'MaxMarks': MaxMarks,
                        'Marks': Marks
                    },
                    dataType: "json",
                    async: true,
                    success: function (result) {
                        HideLoader();
                        $("#msgbox").html(result.responseText);
                        if ($("[id*=msgbox] span").html() == "Marks Saved Successfully.") {
                            reset();
                        }
                        setTimeout(function () {
                            $("[id*=msgbox] span").hide('slide', { direction: 'left' }, 3000);
                            LoadFillList();
                        }, 5000);
                    },
                    error: function (result) {
                        HideLoader();
                        $("#msgbox").html(result.responseText);
                        if ($("[id*=msgbox] span").html() == "Marks Saved Successfully.") {
                            reset();
                        }
                        setTimeout(function () {
                            $("[id*=msgbox] span").hide('slide', { direction: 'left' }, 3000);
                            LoadFillList();
                        }, 5000);
                    }
                });
            }
        }

        function reset()
        {
            $("#lnkSubmit").addClass("hide");
            $("[id*=divList]").html("");
        }
    </script>
</asp:Content>
