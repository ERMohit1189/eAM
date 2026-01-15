<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="ClassTeacherComment_ng.aspx.cs" Inherits="ClassTeacherComment_ng" %>


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
                                    <label class="control-label">S.R. No.</label>
                                    <div class="" id="div_srno">
                                        <select id="drpsrno" class="form-control-blue"></select>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="col-sm-12   mgbt-xs-15">
                                <span class="txt-bold txt-middle-l text-primary">Note:- </span><span class="txt-bold txt-middle-l text-danger blink"> Please enter only numeric value in boxes.</span>
                            </div>--%>
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
                LoadsrNO();
                if ($("#ddlSubject").val() == "") {
                    $("[id*=divList]").html("");
                }
                else {
                    var ClassId = $("#drpclass").val();
                    var SectionId = $("#drpsection").val();
                    var BranchId = $("#drpBranch").val();
                    if (ClassId == "") { alert("Select Class"); return true; }
                    else if (SectionId == "") { alert("Select Section"); return true; }
                    else {
                        LoadFillList();
                    }
                }
                
            });

            $(document).on('change', '#drpsrno', function () {
                if ($("#drpsrno").val() == "") {
                    $("[id*=divList]").html("");
                }
                else {
                    var ClassId = $("#drpclass").val();
                    var SectionId = $("#drpsection").val();
                    var BranchId = $("#drpBranch").val();
                    if (ClassId == "") { alert("Select Class"); return true; }
                    else if (SectionId == "") { alert("Select Section"); return true; }
                    else if (BranchId == "") { alert("Select Branch"); return true; }
                    else {
                        LoadFillList();
                    }
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
        
        function LoadFillList() {
            ShowLoader();
            $("[id*=divList]").html("");
            var ClassId = $("#drpclass").val();
            var SectionId = $("#drpsection").val();
            var BranchId = $("#drpBranch").val();
            var SubjectId = $("#ddlSubject").val();
            var SubjectName = $("#ddlSubject option:selected").text();
            var srNO = $("#drpsrno").val();
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("server/ClassTeacherCommentServer.aspx") %>',
                data: {
                    'ClassId': ClassId,
                    'SectionId':SectionId,
                    'BranchId': BranchId,
                    'srNO': srNO,
                    'SessionName': SessionName
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    HideLoader();
                    $("[id*=divList]").html(result.responseText);
                    if ($("[id*=divList]").html() != "") {
                        $("[id*=lnkSubmit]").removeClass("hide");
                    }
                },
                error: function (result) {
                    HideLoader();
                    $("[id*=divList]").html(result.responseText);
                    if ($("[id*=divList]").html() != "") {
                        $("[id*=lnkSubmit]").removeClass("hide");
                    }
                }
            });
        }

     
        // Save Marks and also save max mark if not exists

        function SaveMarks() {
            
            ShowLoader();

            var ClassId = $("#drpclass").val();
            var SectionId = $("#drpsection").val();
            var BranchId = $("#drpBranch").val();
            var SessionName = '<%=HttpContext.Current.Session["SessionName"] %>';
            var LoginName = '<%=HttpContext.Current.Session["LoginName"] %>';
           
                var Marks = "";
                var counts = $("[id*=divList] table tbody tr").length;
                counts = counts - 1;
                for (var i = 0; i < counts; i++) {
                    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(1) span").html() + "##"; // Srno
                    Marks += $("[id*=divList] table tbody tr:eq(" + (i + 1) + ") td:eq(4) input[type=text]").val() + "$"; // comment
                }
                if (ClassId == "") { alert("Select Class"); return true; }
                else if (SectionId == "") { alert("Select Section"); return true; }
                else if (BranchId == "") { alert("Select Branch"); return true; }
                else {
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("Server/SaveClassTeacherCommentServer.aspx") %>',
                    data: {
                        'ClassId': ClassId,
                        'SessionName': SessionName,
                        'LoginName': LoginName,
                        'Marks': Marks
                    },
                    dataType: "json",
                    async: true,
                    success: function (result) {
                        HideLoader();
                        $("#msgbox").html(result.responseText);
                        if ($("[id*=msgbox] span").html() == "Saved Successfully.") {
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
                        if ($("[id*=msgbox] span").html() == "Saved Successfully.") {
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
