<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="ClearOnlinePayment.aspx.cs"
    Inherits="ClearOnlinePayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
   <div id="loader" runat="server"></div>
     <div id="jsloader1" class="loaderClassHide">
            <img src='<%= ResolveUrl("~/img/2load.gif") %>' style="background-repeat: no-repeat; width: 130px; margin: 18% auto;" />
        </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From</label>
                                        <div class="">
                                            <input type="text" id="txtFromDate" class="form-control-blue datepicker-normal currDate" readonly="true" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To</label>
                                        <div class="">
                                            <input type="text" id="txtToDate" class="form-control-blue datepicker-normal currDate" readonly="true" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <button type="button" id="btnView" class="button">View</button>
                                            <div id="divmsg" runat="server" style="left: 75px"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 ">
                                        <label class="control-label text-danger"> Note:- Please select maximum 5 days between two dates.</label>
                                        <br /><br />
                                        </div>
                                </div>
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <table id="tblAppend" class="table table-striped table-hover no-bm no-head-border table-bordered">
                                           <thead><tr><th class="vd_bg-blue vd_white" style="text-align:center; vertical-align:middle; width: 40px;">#</th><th class="vd_bg-blue vd_white" style="text-align:center; vertical-align:middle;">S.R. No.</th><th class="vd_bg-blue vd_white" style="text-align:center; vertical-align:middle;">Receipt No.</th><th class="vd_bg-blue vd_white" style="text-align:center; vertical-align:middle;">Date</th><th class="vd_bg-blue vd_white" style="text-align:center; vertical-align:middle;">Amount</th><th class="vd_bg-blue vd_white" style="text-align:center; vertical-align:middle;">Cancel Status</th><th class="vd_bg-blue vd_white" style="text-align:center; vertical-align:middle;">Status (Payment Status)</th><th class="vd_bg-blue vd_white" style="text-align:center; vertical-align:middle;"><input type="checkbox" id="chkAll" checked="checked"></th></tr></thead>
                                            <tbody></tbody>
                                        </table>
                                        <span id="norecord" style="color:red;"></span>
                                    </div>
                                </div>
                                  <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <button type="button" id="btnSubmit" class="button hide">Clear Payment</button>
                                        </div>
                                    </div>
                                <span id="gg"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../js/jquery.min.js"></script>
       
    <script type = "text/javascript">
        $(document).ready(function () {
            GetData();
            $("#btnView").click(function () {
                GetData();
            });

            $("#btnSubmit").click(function () {
                $("#btnSubmit").prop("disabled", true);
                UpdateData();
                $("#btnSubmit").prop("disabled", false);
            });

            $("#chkAll").click(function () {
                var len = $("#tblAppend tbody tr").length;
                if ($(this).prop("checked") == true) {
                    $(".chk").prop("checked", true);
                }
                else {
                    $(".chk").prop("checked", false);
                }
            });
            $("#chk_").on("change", function () {
                var len = $("#tblAppend tbody tr").length;
                var cnts = 0;
                for (var i = 0; i < len; i++) {
                    if ($("#tblAppend tbody tr:eq(" + i + ") td:eq(6) input[type=checkbox]").prop("checked") == true) {
                        cnts = cnts + 1;
                    }
                }
                if (cnts == len) {
                    $("#chkAll").prop("checked", true);
                }
                else {
                    $("#chkAll").prop("checked", false);
                }
            });
        });
        var cnts = 0;
        function GetData() {
            ShowLoader();
            $("[id*=hdncnt]").val('0');
            $("[id*=btnView]").html("Please wait...");
            $("[id*=btnView]").prop("disabled", false);
            $("[id*=tblAppend] tbody").html('');
            $("#norecord").html('');

            var fromDate = $("#txtFromDate").val();
            var toDate = $("#txtToDate").val();

            const date1 = new Date(fromDate);
            const date2 = new Date(toDate);
            const diffTime = Math.abs(date2 - date1);
            const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
            if ((parseInt(diffDays) + 1) > 5) {
                alert('Please select maximum 5 days between two dates');
                $("[id*=btnView]").prop("disabled", false);
                $("[id*=btnView]").html("View");
                $("[id*=btnSubmit]").addClass('hide');
                $("[id*=tblAppend]").addClass('hide');
                HideLoader();
                return;
            }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "ClearOnlinePayment.aspx/GetData",
                data: "{'fromDate':'" + fromDate + "','toDate':'" + toDate + "'}",
                dataType: "json",
                async: true,
                success: function (data) {

                    var i = 0;
                    var result = data.d;
                    if (result.length > 0) {
                        for (i = 0; i < result.length; i++) {

                            $("[id*=tblAppend] tbody").append('<tr id=' + result[i].TxnID + '><td style="text-align:center; vertical-align:middle;">' + (i + 1) + '</td><td style="text-align:center; vertical-align:middle;">' + result[i].SrNo + '</td><td style="text-align:center; vertical-align:middle;">' + result[i].RecieptSrNo + '</td><td style="text-align:center; vertical-align:middle;">' + result[i].FeeDepositeDate + '</td><td style="text-align:center; vertical-align:middle;">' + result[i].RecievedAmount + '</td><td style="text-align:center; vertical-align:middle;">' + result[i].CancelStatus + '</td><td style="text-align:center; vertical-align:middle;">' + result[i].Status + '</td><td style="text-align:center; vertical-align:middle;"><input type="checkbox" id="chk_' + (i + 1) + '" class="chk" checked="checked"></td></tr>');
                            //onlineData(result[i].TxnID, result[i].SrNo, result[i].RecieptSrNo, result[i].FeeDepositeDate, result[i].RecievedAmount, result[i].Status);
                        }
                        $("[id*=btnSubmit]").removeClass('hide');
                        $("[id*=tblAppend]").removeClass('hide');
                    }
                    else {
                        $("[id*=tblAppend] tbody").html('');
                        $("[id*=btnSubmit]").addClass('hide');
                        $("[id*=tblAppend]").addClass('hide');
                        $("[id*=btnView]").prop("disabled", false);
                        $("[id*=btnView]").html("View");
                        //$("#norecord").html('No record found!');
                    }

                    onlineData();


                },
                error: function (data) {

                    $("[id*=btnView]").prop("disabled", false);
                    $("[id*=btnView]").html("View");
                    var r = data.responseText;
                    var errorMessage = r.Message;
                    alert(errorMessage);
                }
            });
            HideLoader();
        }
        function onlineData() {

            var len = $("#tblAppend tbody tr").length;
            $("#tblAppend tbody tr").each(function () {
                var TxnID = $(this).attr('id');
                $.ajax({
                    type: "POST",
                    url: "ClearOnlinePayment.aspx/GetOnlineData",
                    data: "{merchantTransactionIds:'" + TxnID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (Data) {
                        var res = JSON.parse(Data.d);
                        if (res.status ==0) {
                            if (Data.d.length > 0) {
                                var resultStatus = res.result[0].status;
                                resultStatus = resultStatus.toLowerCase();
                                //alert(resultStatus);
                                if ((resultStatus == "money settled" || resultStatus == "settlement in process")) {
                                    var datasts = $("#" + TxnID).find('td:eq(5)').html();
                                    $("#" + TxnID).find('td:eq(6)').html(datasts + ' (' + resultStatus + ')');
                                }
                                else {
                                    $("#" + TxnID).remove();
                                }
                            }
                        }
                        else {
                            $("#" + TxnID).remove();
                        }
                    },
                    failure: function (Data) {
                    }
                });
            });

            var len2 = $("#tblAppend tbody tr").length;
            if (len2 == 0) {
                $("[id*=tblAppend] tbody").html('');
                $("[id*=btnSubmit]").addClass('hide');
                $("[id*=tblAppend]").addClass('hide');
                $("[id*=btnView]").prop("disabled", false);
                $("[id*=btnView]").html("View");
            }
            if (len2 > 0) {
                $("[id*=tblAppend]").removeClass('hide');
                $("[id*=btnSubmit]").removeClass('hide');
                $("[id*=btnView]").prop("disabled", false);
                $("[id*=btnView]").html("View");
            }
            for (var j = 0; j < len2; j++) {
                $("#tblAppend tbody tr:eq(" + j + ") td:eq(0)").html((j + 1));
            }

        }

        function UpdateData() {
            var data = "";
            var cc = 1;
            var len = $("#tblAppend tbody tr").length;
            for (var i = 0; i < len; i++) {
                if ($("#tblAppend tbody tr:eq(" + i + ") td:eq(7) input[type=checkbox]").prop("checked")==true) {
                    data = data + $("#tblAppend tbody tr:eq(" + i + ") td:eq(1)").html() + "##";
                    data = data + $("#tblAppend tbody tr:eq(" + i + ") td:eq(2)").html() + "$";
                }
            }
            $.ajax({
                type: "POST",
                url: "ClearOnlinePayment.aspx/UpdateData",
                data: '{data: ' + JSON.stringify(data) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert("Payments clear successfully.");
                    window.location.reload();
                }
            });
        }
        

        

</script>
</asp:Content>
