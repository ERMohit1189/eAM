<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarksEntryNurtoPrep_1718.aspx.cs" Inherits="common_MarksEntryNurtoPrep_1718" %>

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
    <style>
        .disabled-row {
    pointer-events: none;  /* Prevent interaction */
    opacity: 0.6;  /* Visually indicate it's disabled */
}
    </style>
    <div id="loader" runat="server"></div>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12 no-padding" id="div1" runat="server">
                            <div class="col-sm-12  no-padding ">
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_drpclass">
                                        <select id="drpclass" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_section">
                                        <select id="drpsection" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Evaluation&nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <select id="drpEval" class="form-control-blue">
                                            <option value="TERM1">TERM1</option>
                                            <option value="TERM2">TERM2</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Subject">
                                        <select id="drpSubject" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Paper">
                                        <select id="drpPaper" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Activity&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Activity">
                                        <select id="drpActivity" class="form-control-blue">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
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
                                <span class="txt-bold txt-middle-l text-primary">Note:- </span><span class="txt-bold txt-middle-l text-danger blink" id="Firstmsg">Type ML For Medical Leave, NAD for New Admission and NP for Not Present.</span>
                                 <span class="txt-bold txt-middle-l text-danger blink hide" id="Secondmsg">Mark entry is locked. Please ask the admin to unlock it.</span>
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

                    var Evel1 = 0, Evel2 = 0, Evel3 = 0, Best2 = 0, Conversion = 0; var Grade = "";
                    var MaxMarks1 = 0, MaxMarks2 = 0, MaxMarks3 = 0;
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val()))) {
                        Evel1 = parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val()))) {
                        Evel2 = parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val());
                    }
                    if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(6) input[type=text]').val()))) {
                        Evel3 = parseFloat($(this).closest('tr').find('td:eq(6) input[type=text]').val());
                    }
                    

                    if (!isNaN(parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val()))) {
                        MaxMarks1 = parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val());
                    }
                    if (!isNaN(parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val()))) {
                        MaxMarks2 = parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val());
                    }
                    if (!isNaN(parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]").val()))) {
                        MaxMarks3 = parseFloat($("[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]").val());
                    }
                    
                    var p1 = (isNaN((Evel1 * 100) / MaxMarks1)? 0 : ((Evel1 * 100) / MaxMarks1));
                    var p2 = (isNaN((Evel2 * 100) / MaxMarks2)? 0 : ((Evel2 * 100) / MaxMarks2));
                    var p3 = (isNaN((Evel3 * 100) / MaxMarks3) ? 0 : ((Evel3 * 100) / MaxMarks3));
                    var Best2marks = 0;
                    var Best2Maxmarks = 0;

                    Evel1 = p1 > 0 ? Evel1 : 0;
                    Evel2 = p2 > 0 ? Evel2 : 0;
                    Evel3 = p3 > 0 ? Evel3 : 0;

                    var myArray = [
                        ['Max1', Evel1],
                        ['Max2', Evel2],
                        ['Max3', Evel3]
                    ];

                    myArray.sort(function (a, b) {
                        return a[1] - b[1];
                    });
                    Best2marks = parseFloat(myArray[1][1]) + parseFloat(myArray[2][1])
                    if (myArray[1][0] == "Max1") {
                        Best2Maxmarks = Best2Maxmarks + MaxMarks1;
                    }
                    else if (myArray[1][0] == "Max2") {
                        Best2Maxmarks = Best2Maxmarks + MaxMarks2;
                    }
                    else if (myArray[1][0] == "Max3") {
                        Best2Maxmarks = Best2Maxmarks + MaxMarks3;
                    }

                    if (myArray[2][0] == "Max1") {
                        Best2Maxmarks = Best2Maxmarks + MaxMarks1;
                    }
                    else if (myArray[2][0] == "Max2") {
                        Best2Maxmarks = Best2Maxmarks + MaxMarks2;
                    }
                    else if (myArray[2][0] == "Max3") {
                        Best2Maxmarks = Best2Maxmarks + MaxMarks3;
                    }
                    if (Best2Maxmarks > 0)
                    {
                        Conversion = ((Best2marks * 100) / Best2Maxmarks);
                    }
                    var grade1 = "";
                    if (Conversion < 40) {
                        grade1 = "D";
                    }
                    else if (Conversion >= 40 && Conversion < 59) {
                        grade1 = "C";
                    }
                    else if (Conversion >= 60 && Conversion < 69) {
                        grade1 = "B";
                    }
                    else if (Conversion >= 70 && Conversion < 79) {
                        grade1 = "B+";
                    }
                    else if (Conversion >= 80 && Conversion < 89) {
                        grade1 = "A";
                    }
                    else if (Conversion >= 90 && Conversion <= 100) {
                        grade1 = "A+";
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
                    if ($(this).attr('name') == "3") {
                        if (parseFloat(thisVal) > parseFloat(MaxMarks3)) {
                            isMax = "Yes";
                            $(this).addClass('borders');
                            $(this).focus();
                        }
                    }
                    if (isMax != "Yes") {
                        $(this).closest('tr').find('td:eq(7) span').html('');
                        $(this).closest('tr').find('td:eq(8) span').html('');
                        $(this).closest('tr').find('td:eq(9) span').html('');

                        $(this).closest('tr').find('td:eq(7) span').html(Best2marks.toFixed(1));
                        $(this).closest('tr').find('td:eq(8) span').html(Conversion.toFixed(1));
                        $(this).closest('tr').find('td:eq(9) span').html(grade1);
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
                $("[id*=divList]").html("");
                LoadActivity();
            });
            $(document).on('change', '#drpActivity', function () {
                if ($("[id*=drpActivity]").val() == "") {
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
        function LoadActivity() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlActivity_MarkEntry_2.aspx") %>',
                data: { 'classId': $("#drpclass").val(), 'SubjectId': $("#drpSubject").val(), 'PaperId': $("#drpPaper").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_Activity").html("");
                    $("#div_Activity").html(result.responseText);
                    HideLoader();
                }
            });
        }

        
        function LoadFillList() {
            
            $("[id*=divList]").html("");
            ShowLoader();
            var ClassName = $("[id*=drpclass] option:selected").text();
            var ClassId = $("#drpclass").val();
            var SectionId = $("[id*=drpsection]").val();
            var SectionName = $("[id*=drpsection] option:selected").text();
            var SubjectId = $("[id*=drpSubject]").val();
            var PaperId = $("[id*=drpPaper]").val();
            var Evail = $("[id*=drpEval]").val();
            var ActivityId = $("[id*=drpActivity]").val();
            var Session = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/MarkEntryNurtoPrepEmpty_1718Server.aspx") %>',
                data: {
                    'ClassName': ClassName,
                    'SectionName': SectionName,
                    'SectionId': SectionId,
                    'Session': Session,
                    'BranchCode': BranchCode,
                    'Evail': Evail,
                    'SubjectId': SubjectId,
                    'PaperId': PaperId,
                    'ClassId': ClassId,
                    'ActivityId': ActivityId
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    HideLoader();
                    $("[id*=divList]").html(result.responseText);
                    if ($("[id*=ddlTabIndex]").val() == "Vertical")
                    {
                        TabFunction();
                    }
                    if ($("[id*=divList]").html()!="") {
                        $("[id*=lnkSubmit]").removeClass("hide");
                    }
                  
                    if ($('#lnkSubmit').length > 0) {
                      //  alert("exists");
                        $("[id*=Firstmsg]").removeClass("hide");
                        $("[id*=Secondmsg]").addClass("hide");
                    } else {
                        $("[id*=Firstmsg]").addClass("hide");
                        $("[id*=Secondmsg]").removeClass("hide");

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
                    if ($('#lnkSubmit').length > 0) {
                        //  alert("exists");
                        $("[id*=Firstmsg]").removeClass("hide");
                        $("[id*=Secondmsg]").addClass("hide");
                    } else {
                        $("[id*=Firstmsg]").addClass("hide");
                        $("[id*=Secondmsg]").removeClass("hide");

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
            var SectionId = $("#drpsection").val();
            var SubjectId = $("#drpSubject").val();
            var PaperId = $("#drpPaper").val();
            var TermName = $("#drpEval").val();
            var ActivityId = $("[id*=drpActivity]").val();
            var Session = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var LoginName = '<%=HttpContext.Current.Session["LoginName"] %>';
            var MaxMarks = "";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(4) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(5) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table tbody tr:eq(0) th:eq(6) input[type=text]").val();
            var Marks = "";
            var counts=$("[id*=divList] table tbody tr").length;
            counts=counts-1;
            for (var i = 0; i < counts; i++) {
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(1) span").html() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(4) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(5) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(6) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(7) span").html() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(8) span").html() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(9) span").html() + "$";
            }
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/SaveMarkEntryNurtoPrepEmpty_1718Server.aspx") %>',
                data: {
                    'ClassId': ClassIds,
                    'SectionName': SectionName,
                    'SectionId':SectionId,
                    'SubjectId': SubjectId,
                    'PaperId': PaperId,
                    'ActivityId': ActivityId,
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

