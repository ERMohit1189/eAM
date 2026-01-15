<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="DateWiseCollection.aspx.cs" Inherits="DateWiseCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">
                                    <div class="col-sm-4   mgbt-xs-15" id="divBranch" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label">Institute Branch&nbsp;<span class="vd_red">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-2   mgbt-xs-15" id="divSession" runat="server">
                                        <label class="control-label">Session</label>
                                        <div class="">
                                            <asp:DropDownList runat="server" ID="DrpSessionName"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date</label>
                                        <div class="">
                                            <asp:DropDownList ID="DDYear" runat="server" OnSelectedIndexChanged="DDYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue col-sm-3">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DDMonth" runat="server" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue col-sm-3">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DDDate" runat="server" OnSelectedIndexChanged="DDDate_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue col-sm-3 "></asp:DropDownList>
                                            <asp:TextBox runat="server" ID="dayName" CssClass="form-control-blue col-sm-3" Enabled="false" Style="width: 25%;"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Status</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpStatus" runat="server" class="form-control-blue ">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Paid</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                                <asp:ListItem>Cancelled</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3   mgbt-xs-15">
                                        <label class="control-label">Mode of Payment</label>
                                        <div class="">
                                            <asp:DropDownList ID="DdlpaymentMode" runat="server" CssClass="vd_radio radio-success">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Cash</asp:ListItem>
                                                <asp:ListItem>Cheque</asp:ListItem>
                                                <asp:ListItem>DD</asp:ListItem>
                                                <asp:ListItem>Card</asp:ListItem>
                                                <asp:ListItem>Online Transfer</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                                <asp:ListItem Value="Online">Online</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select User&nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue "></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgt-xs-15">
                                        <div class="" style="margin-top: 20px;">
                                            <asp:CheckBox ID="chkExclude" runat="server" class="vd_checkbox checkbox-success" Text="Exclude Other Fee"></asp:CheckBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8   mgbt-xs-15">
                                        <div class="" style="margin-top: 15px;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="btnView" runat="server" CssClass="button form-control-blue" OnClick="btnView_Click">View</asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <%--<span id="btnView" class="button">view</span>--%>
                                            <div class="text-box-msg"></div>
                                            <div id="headerDiv" runat="server" class="hide"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  mgbt-xs-10" runat="server" id="div1" visible="false">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                    title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                    title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                    title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

                                                <script>
                                                    
                                                </script>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ImageButton1" />
                                            <asp:PostBackTrigger ControlID="ImageButton2" />
                                            <asp:PostBackTrigger ControlID="ImageButton3" />
                                            <asp:PostBackTrigger ControlID="ImageButton4" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div id="gdv1" runat="server" visible="false">
                                                    <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table text-center">
                                                        <tr>
                                                            <td>
                                                                <div id="header" runat="server" class="col-md-12 no-padding text-center"></div>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                    <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                    <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                    <div id="divExport" class="" runat="server"></div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--<script src="../js/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            
            $("#btnView").click(function () {
                    LoadData();
            });
            LoadData();
            //getDay();
        });

        function removeClassses() {
            $("[id*=divBranch]").removeClass("hide");
            $("[id*=divSession]").removeClass("hide");
        }
        function getDay() {
            var day = $("[id*=DDDate]").val();
            var month = $("[id*=DDMonth]").val();
            var year = $("[id*=DDYear]").val();
            var dateString = day + "-" + month + "-" + year;
           
            var d = new Date(dateString);
            var dayName = d.toString().split(' ')[0];
            if (dayName=="Sun") {
                dayName = "Sunday";
            }
            if (dayName == "Mon") {
                dayName = "Monday";
            }
            if (dayName == "Tue") {
                dayName = "Tuesday";
            }
            if (dayName == "Wed") {
                dayName = "Wednesday";
            }
            if (dayName == "Thu") {
                dayName = "Thursday";
            }
            if (dayName == "Fri") {
                dayName = "Friday";
            }
            if (dayName == "Sat") {
                dayName = "Saturday";
            }
            $("[id*=day]").val(dayName);
        }
    </script>
    <script>


        function PrintDiv() {
            var headContent = document.getElementsByTagName('head')[0].innerHTML;

            var divContents = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_divExport").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000, class="tbls"');
            printWindow.document.write('<html><head><title>Datewise Collection</title>' + headContent + '</head>');
            printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');

            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 1500);
            return false;
            printWindow.close();

        }

        //$.noConflict();
        function LoadData() {
            var headerDiv = $("[id*=headerDiv]").html();
            $("[id*=divExport]").html('');
            ShowLoader();
            var days = $("[id*=DDDate]").val();
            var Month = $("[id*=DDMonth]").val();
            var Year = $("[id*=DDYear]").val();
            var Date = days + '-' + Month + '-' + Year;
            var Status = $("[id*=drpStatus]").val();
            var user = $("[id*=DropDownList1]").val();
            var isExclude = 0;
            if ($("[id*=chkExclude]").prop('checked') == true) {
                isExclude = 1;
            }

            var PaymentMode = $("[id*=DdlpaymentMode]").val();
            var SessionName = $("[id*=DrpSessionName]").val();
            var BranchCode = $("[id*=ddlBranch]").val();
                    $.ajax({
                        type: "POST",
                        url: '<%=ResolveUrl("Server/DatewiseCollectionServerAll.aspx") %>',
                        data: {
                            'Date': Date,
                            'PaymentMode': PaymentMode,
                            'Status': Status,
                            'SessionName': SessionName,
                            'BranchCode': BranchCode,
                            'isExclude': isExclude,
                            'user': user
                        },
                        dataType: "json",
                        async: true,
                        success: function (result) {
                            HideLoader();
                            $("[id*=divExport]").html(result.responseText);
                            $(".divHeader").append(headerDiv);
                            
                            
                        },
                        error: function (result) {
                            HideLoader();
                            $("[id*=divExport]").html(result.responseText);
                            $(".divHeader").append(headerDiv);
                            if ($("#hdncnts").val() != "undefined") {
                                if ($("#hdncnts").val() != "0") {
                                    $("#div1").removeClass('hide');
                                }
                                else {
                                    $("#div1").addClass('hide');
                                }
                            }
                            
                        }
                    });
                }
    </script>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

