<%@ Page Title="" Language="C#" MasterPageFile="~/14/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarkEntryNurtoPrep.aspx.cs" Inherits="MarkEntryNurtoPrep" %>


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
                if (values == "A+" || values == "A" || values == "B+" || values == "B" || values == "C" || values == "") {
                   
                }
                else {
                    $(this).val('');
                    $(this).focus();
                    alert('Please enter only A+,A,B+,B,C');
                    $(this).addClass('borders');
                }
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
                url: '<%=ResolveUrl("Server/ddlClassMarkEntryG1.aspx") %>',
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
                url: '<%=ResolveUrl("Server/MarksEntryNurToPrepServer.aspx") %>',
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
            var counts = $("[id*=divList] table tbody tr").length;
            var Marks = "";
            for (var i = 0; i < counts; i++) {
                Marks += $("[id*=divList] table tbody tr:eq(" + (i) + ") td:eq(1) span").html() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i) + ") td:eq(4) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i) + ") td:eq(5) input[type=text]").val() + "##";
                Marks += $("[id*=divList] table tbody tr:eq(" + (i) + ") td:eq(6) input[type=text]").val() + "$";
            }
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("Server/SaveMarksEntryNurtoPrep.aspx") %>',
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
            $("[id*=divList]").html("");
            $("#drpPaper").val("");
        }
    </script>
</asp:Content>
