<%@ Page Title="" Language="C#" MasterPageFile="stuRootManagerExam.master" AutoEventWireup="true" CodeFile="exampreview.aspx.cs"
    Inherits="exampreview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <script type="text/javascript" language="javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function EndRequestHandler(sender, args) {
            scrollTo(0, 0);
        }
    </script>
   
    <style>
        .radio {
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }

        input[type=checkbox] {
            width: 18px;
            height: 18px;
            margin: 9px 0px;
            line-height: normal;
            outline: none !important;
            background-color: #b8b8b8;
        }


        .input {
            font: 15px 'Open Sans', sans-serif;
            color: #333;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
            background-color: #f3f3f3;
            border: none;
            padding: 11px 14px 12px 14px;
            width: 100%;
            max-width: 270px;
            border-radius: 3px;
            -moz-appearance: none;
            -webkit-appearance: none;
            resize: none;
            outline: 0;
        }

        .form-radio, .form-checkbox {
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            display: inline-block;
            position: relative;
            background-color: #747474;
            color: #fff;
            top: 10px;
            height: 30px;
            width: 30px;
            border: 0;
            border-radius: 75px;
            cursor: pointer;
            margin-right: 7px;
            outline: none;
        }

        .form-checkbox {
            border-radius: 50%;
        }

            .form-radio:checked::before, .form-checkbox:checked::before {
                position: absolute;
                font: 13px/1 'Open Sans', sans-serif;
                left: 6px;
                top: 1px;
                content: '\02143';
                transform: rotate(40deg);
            }

            .form-radio:hover, .form-checkbox:hover {
                background-color: #747474;
            }

            .form-radio:checked, .form-checkbox:checked {
                background-color: #747474;
            }

        label {
            font: 15px/1.7 'Open Sans', sans-serif;
            color: #333;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
            cursor: pointer;
            font-weight: 700 !important;
        }

        input[type][disabled] {
            background-color: #f9f9f9;
            color: #ddd;
            cursor: default;
        }

            input[type][disabled] + label {
                color: #999;
                cursor: default;
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
document.onkeydown = function(e) {
        if (e.ctrlKey && 
            (e.keyCode === 67 || 
             e.keyCode === 86 || 
             e.keyCode === 85 ||
             e.keyCode === 2 ||
             e.keyCode === 117)) {
            return false;
        } else {
            return true;
        }
};
$(document).keypress("u",function(e) {
  if(e.ctrlKey)
  {
return false;
}
else
{
return true;
}
});
</script>
    <script type="text/javascript">
if (document.layers) {
    //Capture the MouseDown event.
    document.captureEvents(Event.MOUSEDOWN);
 
    //Disable the OnMouseDown event handler.
    document.onmousedown = function () {
        return false;
    };
}
else {
    //Disable the OnMouseUp event handler.
    document.onmouseup = function (e) {
        if (e != null && e.type == "mouseup") {
            //Check the Mouse Button which is clicked.
            if (e.which == 2 || e.which == 3) {
                //If the Button is middle or right then disable.
                return false;
            }
        }
    };
}
 
//Disable the Context Menu event.
document.oncontextmenu = function () {
    return false;
};
</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(scrollbar);
            </script>

            <div class="vd_content-section clearfix" oncontextmenu="return false">
                <asp:HiddenField runat="server" ID="hdnExamID" />
                <asp:HiddenField runat="server" ID="hdnDescriptiveExists" />
                <asp:HiddenField runat="server" ID="hdnResultStting" />
                <asp:HiddenField runat="server" ID="hdnIsExamTested" />

                <div class="row">
                    <div class="col-sm-12 " style="padding-left: 0px;">
                        <div class="panel widget light-widget panel-bd-top" style="border: 3px solid #393d41 !important;">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding" style="color: #fff; font-size: 19px;">
                                    <div class="col-sm-4" style="border: 1px solid #000 !important;">
                                        <span style="font-weight: bold; color: #000 !important;">Student's Name : </span>
                                        <asp:Label runat="server" ID="StuName" Style="font-weight: bold; color: #a94442 !important;"></asp:Label>
                                    </div>
                                    <div class="col-sm-4 text-center" style="font-weight: bold; color: #f00;">
                                        <input type="hidden" id="mins" />
                                        <span id="count2"></span>
                                    </div>
                                    <div class="col-sm-4 text-right" style="border: 1px solid #000 !important;">
                                        <span style="font-weight: bold; color: #000;">S.R. No.: </span>
                                        <asp:Label runat="server" ID="rollNo" Style="font-weight: bold; color: #a94442 !important;"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-12 " style="padding-right: 0px; padding-left: 0px; border-top: 3px solid #000; margin-top: 10px;">
                                    <div class="col-sm-9" style="padding-right: 5px; padding-left: 0px;" runat="server" id="divPdf">
                                        <%--<asp:Literal ID="ltEmbed" runat="server" />--%>
                                        <div id="ltEmbed" runat="server"></div>
                                        <div style="width: 100%; max-height: 700px; overflow: auto;">
                                            <asp:Image runat="server" ID="imgExam" Style="width: 100%;" />
                                        </div>
                                    </div>

                                    <div class="col-sm-3" style="border: 1px solid #ccc; padding-right: 0px; padding-left: 0px;" runat="server" id="tblAppend">
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="panel widget light-widget panel-bd-top" style="border: 3px solid #393d41 !important;">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding" style="background: #f5f5f5; color: #fff; font-size: 19px; text-align: center;">
                                   <asp:Button runat="server" ID="btnSave"  CssClass="button form-control-blue" OnClick="btnSave_Click" Text="Submit"></asp:Button>
                                    <div id="msgbox" runat="server" style="left: 75px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to end your test?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="No" OnClientClick="locationReload();" CssClass="button-n" OnClick="Button8_Click" CausesValidation="False" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" OnClick="btnFinalSubmit_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script>
        function AnswerType(tis) {
            var value = $(tis).val();
            var id = $(tis).attr('id');
            if (value == "TypeAnswer") {
                $("#divUpload" + id).addClass('hide');
                $("." + id).removeClass('hide');
            }
            else {
                $("#divUpload" + id).removeClass('hide');
                $("." + id).addClass('hide');
            }
        }
        function uploadAns(tis) {
            var id = $(tis).attr('SectionId');
            if (parseInt($("#upload_doc" + id).get(0).files.length) > 10) {
                $("#spnError" + id).removeClass('hide');
            }
            else {
                var scrollPxs = $("#upload_doc" + id).scrollTop();

                $("#spnError" + id).addClass('hide');
                var ExamID0 = getUrlVars();
                var files = $("#upload_doc" + id).get(0).files;
                
                var SectionId = $(tis).attr('SectionId');
                var fileData = new FormData();
                for (var i = 0; i < files.length; i++) {
                    fileData.append("fileInput", files[i]);
                }
                fileData.append("SectionId", SectionId);
                fileData.append("ExamID", ExamID0);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    contentType: false,
                    processData: false,
                    url: '<%=ResolveUrl("~/13/server/SaveUploadFiles.aspx") %>',
                    data: fileData,
                    success: function (result) {
                        $("#spnSuccess" + id).removeClass('hide');
                        //$("." + id).removeClass('hide');
                        $("#upload_doc" + id).val('');
                        LoadData(scrollPxs);
                    },
                    error: function (result) {
                        $("#spnSuccess" + id).removeClass('hide');
                        //$("." + id).removeClass('hide');
                        $("#upload_doc" + id).val('');
                        LoadData(scrollPxs);
                    }
                });
            }
        }
        function getUrlVars() {
            var vars = "";
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('=');
            vars = hashes[1];
            return vars;
        }
        function locationReload() {
            location.reload();
        }
        

        $(document).ready(function () {
            $("[id*=navbar-collapse-1]").addClass("hide");

            var SrNO = $("[id*=rollNo]").html();
            var ExamID = getUrlVars();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("server/GetDurationServer.aspx") %>',
                data: {
                    'ExamID': ExamID
                },
                dataType: "json",
                async: false,
                success: function (result) {
                    var mins = result.responseText.split("<!DOCTYPE html>");
                    var ss = $.trim(mins[0]);
                    $("#mins").val(parseInt(ss));
                },
                error: function (result) {
                    var mins = result.responseText.split("<!DOCTYPE html>");
                    var ss = $.trim(mins[0]);
                    $("#mins").val(parseInt(ss));
                }
            });

            //define your time in second
            var c = $("#mins").val();
            var t;
            timedCount();

            function timedCount() {
                //alert(c);
                var hours = parseInt(c / 3600) % 24;
                var minutes = parseInt(c / 60) % 60;
                var seconds = c % 60;
                //alert(hours);
                //alert(minutes);
                //alert(seconds);

                var result = (hours < 10 ? "0" + hours : hours) + "H :" + (minutes < 10 ? "0" + minutes : minutes) + "M :" + (seconds < 10 ? "0" + seconds : seconds);


                $('#count2').html(result);
                if (c == 0) {
                    var ExamIDs1 = getUrlVars();
                    alert("Your test has been ended successfully.");
                    var ResultStting = $("[id*=hdnResultStting]").val();
                    var DescriptiveExists = $("[id*=hdnDescriptiveExists]").val();
                    var hdnIsExamTested = $("[id*=hdnIsExamTested]").val();

                    if (ResultStting == "True" && DescriptiveExists == "False" && hdnIsExamTested=="True") {
                        location.href = "result.aspx?p=" + ExamIDs1 + "";
                    }
                    else {
                        location.href = "studashboard.aspx?ExamID=" + ExamIDs1 + "";
                    }
                }
                else {
                    var remainder = parseInt(seconds) % 5;
                    if (remainder == 0) {
                        var MIN = (hours * 3600) + (minutes * 60) + seconds;
                        var ExamIDs = getUrlVars();
                        $.ajax({
                            type: "POST",
                            url: '<%=ResolveUrl("server/SetDurationServer.aspx") %>',
                            data: {
                                'ExamID': ExamIDs,
                                'MIN': MIN
                            },
                            dataType: "json",
                            async: true,
                            success: function (result) {
                            },
                            error: function (result) {
                            }
                        });
                    }
                }
                c = c - 1;
                t = setTimeout(function () {
                    timedCount()
                },
                1000);
            }

            LoadData(1);





            
            $(document).on('change', '.chk', function () {
                var scrollPx = $("#ansDiv").scrollTop();
                var name = $(this).attr("name");
                var ChooseOption = "";
                if ($(this).is(':checked') == true) {
                    $("input[name=" + name + "]").prop("checked", false);
                    ChooseOption = $(this).val();
                    $(this).prop("checked", true);

                }
                else {
                    $("input[name=" + name + "]").prop("checked", false);
                    ChooseOption = "";
                    $(this).prop("checked", false);
                }


                var ExamID1 = getUrlVars();
                var QuestionId = $(this).attr("valueID");

                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("~/13/server/SaveMCQsserver.aspx") %>',
                    data: {
                        'ExamID': ExamID1,
                        'QuestionId': QuestionId,
                        'ChooseOption': ChooseOption
                    },
                    dataType: "json",
                    async: true,
                    success: function (result) {
                        LoadData(scrollPx);
                    },
                    error: function (result) {
                        LoadData(scrollPx);
                    }
                });

            });
            

        });

        function textarea(tis) {
                var scrollPx = $("#ansDiv").scrollTop();
                var ChooseOptions = $(tis).val();
                var ExamID2 = getUrlVars();
                var QuestionIds = $(tis).attr("valueID");
                
                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("~/13/server/SaveDescriptive.aspx") %>',
                    data: {
                        'ExamID': ExamID2,
                        'QuestionId': QuestionIds,
                        'ChooseOption': ChooseOptions
                    },
                    dataType: "json",
                    async: true,
                    success: function (result) {
                        LoadData(scrollPx);
                    },
                    error: function (result) {
                        LoadData(scrollPx);
                    }
                });

        };

         function LoadData(scrollPx) {

             var ExamIDss = $("[id*=hdnExamID]").val();
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("~/13/server/OnlineTestServer.aspx") %>',
                data: { 'ExamID': ExamIDss },
                dataType: "json",
                async: true,
                success: function (result) {
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_tblAppend").html(result.responseText);
                    var $foo = $("div#ansDiv");
                    $foo.scrollTop($foo.scrollTop() + parseInt(scrollPx));
                },
                error: function (result) {
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_tblAppend").html(result.responseText);
                    var $foo = $("div#ansDiv");
                    $foo.scrollTop($foo.scrollTop() + parseInt(scrollPx));
                }
            });
        }
    </script>



</asp:Content>
