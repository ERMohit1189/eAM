<%@ Page Title="" Language="C#" MasterPageFile="~/14/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarksEntryItoVIII.aspx.cs" Inherits="MarksEntryItoVIII" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
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
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Class">
                                        <select id="drpClass" class="form-control-blue validatedrp">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Branch">
                                        <select id="drpBranch" class="form-control-blue validatedrp">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Section">
                                        <select id="drpSection" class="form-control-blue validatedrp">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Term&nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <select id="drpTerm" class="form-control-blue validatedrp">
                                            <option value=""><--Select--></option>
                                            <option value="Term1">TERM1</option>
                                            <option value="Term2">TERM2</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Subject">
                                        <select id="drpSubject" class="form-control-blue validatedrp">
                                            <option value=''><--Select--></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Paper">
                                        <select id="drpPaper" class="form-control-blue validatedrp">
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
                                <span class="txt-bold txt-middle-l text-primary">Note:- </span><span class="txt-bold txt-middle-l text-danger blink">Type ML For Medical Leave, NAD for New Admission and NP for Not Present.</span>
                            </div>
                            <div class="col-sm-12  ">
                                <div class=" table-responsive  table-responsive2 " id="divList" runat="server">
                                </div>
                                <div class="col -sm-12  text-center">
                                    <div id="msgbox" runat="server" style="left: 155px;"></div>
                                </div>
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

            $(document).on('blur', 'input[type=text]', function () {
                $(this).removeClass('borders');
                $(this).val(this.value.toUpperCase());
                var values = $.trim($(this).val());
                var thisVal = 0;
                if (values != "" && values != "NP" && values != "NAD" && values != "ML") {
                    if (!isNaN(values)) {
                        thisVal = parseFloat($(this).val());
                        $(this).val(thisVal.toFixed(1));
                    }
                    else {
                        $(this).addClass('borders');
                        $(this).val('');
                        $(this).focus();
                        return;
                    }
                }

                var ThNO = parseInt($(this).attr('name')) + 3;
                var thVal = parseFloat($("[id*=divList] table thead tr:eq(1) th:eq(" + ThNO + ") input[type=text]").val());
                if (thisVal > thVal) {
                    $(this).addClass('borders');
                    $(this).val('');
                    $(this).focus();
                    return;
                }
                var HYAE = 0, Test1 = 0, Test2 = 0, Test3 = 0;
                var MaxHYAE = 0, MaxMarks1 = 0, MaxMarks2 = 0, MaxMarks3 = 0;
                
                var Test1HasHide = $(this).closest('tr').find('td:eq(4)').hasClass("hide");
                var Test2HasHide = $(this).closest('tr').find('td:eq(5)').hasClass("hide");
                var Test3HasHide = $(this).closest('tr').find('td:eq(6)').hasClass("hide");
                if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val())) && !Test1HasHide) {
                    Test1 = parseFloat($(this).closest('tr').find('td:eq(4) input[type=text]').val());
                }
                if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val())) && !Test2HasHide) {
                    Test2 = parseFloat($(this).closest('tr').find('td:eq(5) input[type=text]').val());
                }
                if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(6) input[type=text]').val())) && !Test3HasHide) {
                    Test3 = parseFloat($(this).closest('tr').find('td:eq(6) input[type=text]').val());
                }
                if (!isNaN(parseFloat($(this).closest('tr').find('td:eq(8) input[type=text]').val()))) {
                    HYAE = parseFloat($(this).closest('tr').find('td:eq(8) input[type=text]').val());
                }

                
                if (!isNaN(parseFloat($("[id*=divList] table thead tr:eq(1) th:eq(4) input[type=text]").val()))) {
                    MaxMarks1 = parseFloat($("[id*=divList] table thead tr:eq(1) th:eq(4) input[type=text]").val());
                }
                if (!isNaN(parseFloat($("[id*=divList] table thead tr:eq(1) th:eq(5) input[type=text]").val()))) {
                    MaxMarks2 = parseFloat($("[id*=divList] table thead tr:eq(1) th:eq(5) input[type=text]").val());
                }
                if (!isNaN(parseFloat($("[id*=divList] table thead tr:eq(1) th:eq(6) input[type=text]").val()))) {
                    MaxMarks3 = parseFloat($("[id*=divList] table thead tr:eq(1) th:eq(6) input[type=text]").val());
                }
                if (!isNaN(parseFloat($("[id*=divList] table thead tr:eq(1) th:eq(8) input[type=text]").val()))) {
                    MaxHYAE = parseFloat($("[id*=divList] table thead tr:eq(1) th:eq(8) input[type=text]").val());
                }
                var sum1 = 0, maxsum1 = 0, sum2 = 0, maxsum2 = 0, sum3 = 0, maxsum3 = 0, MonthlyTestAY = 0, aggregateHYAE = 0;
                var p1 = (!isNaN((Test1 * 100) / MaxMarks1) ? ((Test1 * 100) / MaxMarks1) : 0);
                var p2 = (!isNaN((Test2 * 100) / MaxMarks2) ? ((Test2 * 100) / MaxMarks2) : 0);
                var p3 = (!isNaN((Test3 * 100) / MaxMarks3) ? ((Test3 * 100) / MaxMarks3) : 0);

                MonthlyTestAY = p1 > p2 ? (p1 > p3 ? Test1 : (p2 > p3 ? Test2 : Test3)) : (p2 > p3 ? Test2 : Test3);
                aggregateHYAE = (MonthlyTestAY + HYAE);
                $(this).closest('tr').find('td:eq(7) span').html(MonthlyTestAY>0?MonthlyTestAY.toFixed(1):"");
                $(this).closest('tr').find('td:eq(9) span').html(aggregateHYAE>0?aggregateHYAE.toFixed(1):"");

            });




            $(document).on('click', '#lnkSubmit', function () {
                $("[id*=divList]").attr("disabled");
                SaveMarks();
            });
            LoadClass();

            $(document).on('change', '#drpClass', function () {
                $("[id*=divList]").html("");
                $("#drpTerm").val("");
                LoadBranch();
                LoadSection();
                $("#drpSubject").html("<option value=''><--Select--></option>");
                $("#drpPaper").html("<option value=''><--Select--></option>");
            });
            $(document).on('change', '#drpBranch', function () {
                $("[id*=divList]").html("");
                $("#drpTerm").val("");
                $("#drpSubject").html("<option value=''><--Select--></option>");
                $("#drpPaper").html("<option value=''><--Select--></option>");
                $("#drpSection").val("");
            });
            $(document).on('change', '#drpSection', function () {
                $("[id*=divList]").html("");
                $("#drpTerm").val("");
                $("#drpSubject").html("<option value=''><--Select--></option>");
                $("#drpPaper").html("<option value=''><--Select--></option>");
            });
            $(document).on('change', '#drpTerm', function () {
                $("[id*=divList]").html("");
                $("#drpSubject").html("<option value=''><--Select--></option>");
                $("#drpPaper").html("<option value=''><--Select--></option>");
                LoadSubject();
            });
            $(document).on('change', '#drpSubject', function () {
                $("[id*=divList]").html("");
                $("#drpPaper").html("");
                LoadPaper();
            });

            $(document).on('change', '#drpPaper', function () {
                if ($("#drpPaper").val() == "") {
                    alert("");
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
                url: '<%=ResolveUrl("Server/ddlClassMarkEntryG2.aspx") %>',
                data: {},
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_Class").html("");
                    $("#div_Class").html(result.responseText);
                    HideLoader();
                }
            });
        }
        function LoadBranch() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlBranchMarkEntry.aspx") %>',
                data: { 'classId': $("#drpClass").val() },
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
        function LoadSection() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSectionMarkEntry.aspx") %>',
                data: { 'classId': $("#drpClass").val() },
                dataType: "json",
                async: true,
                success: function (result) {
                },
                error: function (result) {
                    ShowLoader();
                    $("#div_Section").html("");
                    $("#div_Section").html(result.responseText);
                    HideLoader();
                }
            });
        }
        function LoadSubject() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/ddlSubjecMarkEntry.aspx") %>',
                data: { 'classId': $("#drpClass").val(), 'BranchId': $("#drpBranch").val() },
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
                url: '<%=ResolveUrl("Server/ddlPaperMarkEntry.aspx") %>',
                data: { 'classId': $("#drpClass").val(), 'BranchId': $("#drpBranch").val(), 'SubjectId': $("#drpSubject").val() },
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
            var ClassId = $("#drpClass").val();
            var BranchId = $("#drpBranch").val();
            var SectionId = $("#drpSection").val();
            var SubjectId = $("#drpSubject").val();
            var PaperId = $("#drpPaper").val();
            var Term = $("#drpTerm").val();
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/MarksEntryItoVIIIServer.aspx") %>',
                data: {
                    'ClassId': ClassId,
                    'BranchId': BranchId,
                    'SectionId': SectionId,
                    'SubjectId': SubjectId,
                    'PaperId': PaperId,
                    'Term': Term,
                    'SessionName': SessionName,
                    'BranchCode': BranchCode
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
            $('[id*=divList] table tbody tr').each(function () {
                $(this).closest('tr').find('td').each(function (i) {
                    $(this).find('input').attr('tabindex', i + 1);
                });
            });
        }

        function SaveMarks() {
            $("input[type=text]").removeClass("borders");
            var error1 = "";
            var error2 = "";
            $("[id*=divList] table thead tr:eq(1) th input[type=text]").each(function () {
                var values = this.value.toUpperCase();
                if (isNaN(parseFloat(values))) {
                    $(this).addClass("borders");
                    $(this).focus();
                    error1 = "error1";
                }
            });
            if (error1 != "") {
                alert("Wrong Input:: Please enter maximum marks!");
                return;
            }

            ShowLoader();

            var ClassId = $("#drpClass").val();
            var BranchId = $("#drpBranch").val();
            var SectionId = $("#drpSection").val();
            var SubjectId = $("#drpSubject").val();
            var PaperId = $("#drpPaper").val();
            var Term = $("#drpTerm").val();
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            var BranchCode = '<%=HttpContext.Current.Session["BranchCode"] %>';
            var LoginName = '<%=HttpContext.Current.Session["LoginName"] %>';
            var MaxMarks = "";
            MaxMarks += $("[id*=divList] table thead tr:eq(1) th:eq(8) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table thead tr:eq(1) th:eq(4) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table thead tr:eq(1) th:eq(5) input[type=text]").val() + "##";
            MaxMarks += $("[id*=divList] table thead tr:eq(1) th:eq(6) input[type=text]").val() + "$";
            var Marks = "";
            var counts = $("[id*=divList] table tbody tr").length;
           
            for (var i = 0; i < counts; i++) {
                Marks += $("[id*=divList] table tbody tr:eq(" + (i) + ") td:eq(1) span").html() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i) + ") td:eq(8) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i) + ") td:eq(4) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i) + ") td:eq(5) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i) + ") td:eq(6) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i) + ") td:eq(7) span").html() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i) + ") td:eq(9) span").html() + "$";
            }
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/SaveMarksEntryItoVIII.aspx") %>',
                data: {
                    'ClassId': ClassId,
                    'BranchId': BranchId,
                    'SectionId': SectionId,
                    'SubjectId': SubjectId,
                    'PaperId': PaperId,
                    'Term': Term,
                    'SessionName': SessionName,
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
                        $("[id*=msgbox] span").hide('slide', { direction: 'left' }, 3000);
                    }, 5000);
                },
                error: function (result) {
                    HideLoader();
                    $("[id*=msgbox]").html(result.responseText);
                    if ($("[id*=msgbox] span").html() == "Marks Saved Successfully.") {
                        reset();
                    }
                    setTimeout(function () {
                        $("[id*=msgbox] span").hide('slide', { direction: 'left' }, 3000);
                    }, 5000);
                }
            });
        }

        function reset() {
            $("#lnkSubmit").addClass("hide");
            $("#drpSubject").val("");
            $("[id*=divList]").html("");
            $("#drpPaper").html("");
            $("#drpPaper").html("<option value=''><--Select--></option>");
        }
    </script>
</asp:Content>
