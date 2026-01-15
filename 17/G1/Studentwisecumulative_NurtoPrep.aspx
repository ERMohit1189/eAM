<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Studentwisecumulative_NurtoPrep.aspx.cs" Inherits="common_G2_Studentwisecumulative_NurtoPrep" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../../js/jquery.min.js"></script>
    <style>
        @import "compass/css3";

        .drpPromoToManual {
            -webkit-appearance: none !important;
            -moz-appearance: none !important;
            appearance: none !important;
        }


        .term1 > tbody > tr > td {
            padding: 1px 5px !important;
        }

        .front > tbody > tr > td {
            padding: 7px 5px !important;
        }

        .term1 > tbody > tr > th {
            padding: 1px 5px !important;
        }

        .front > tbody > tr > th {
            padding: 7px 5px !important;
        }

        .term1 > .mp-table > tbody > tr > td, .mp-table > tfoot > tr > td {
            font-size: 10px !important;
            padding: 1px 5px !important;
            color: #000 !important;
        }

        .front > .mp-table > tbody > tr > td, .mp-table > tfoot > tr > td {
            font-size: 10px !important;
            padding: 5px 5px !important;
            color: #000 !important;
        }

        .term2 > .mp-table > tbody > tr > td, .mp-table > tfoot > tr > td {
            font-size: 10px !important;
            color: #000 !important;
        }

        .term2 > tbody > tr > td {
            padding: 1px 5px !important;
        }

        .term2 > tbody > tr > th {
            padding: 1px 5px !important;
        }

        .sentence-case {
            text-transform: initial !important;
        }

        .tbl > tbody > tr > th {
            color: #0054b6 !important;
            font-size: 10px !important;
            font-weight: bold !important;
        }

        .tbl1 > tbody > tr > th {
            color: #0054b6 !important;
            font-size: 10px !important;
            font-weight: bold !important;
        }

        .tbl > tbody > tr > th > span > b {
            color: #0054b6 !important;
            font-size: 10px !important;
            font-weight: bold !important;
        }

        .tbl1 > tbody > tr > th > span > b {
            color: #0054b6 !important;
            font-size: 10px !important;
            font-weight: bold !important;
        }

        .tbl1 > tbody > tr > th > span:first-child {
            color: #0054b6 !important;
            font-size: 10px !important;
            font-weight: bold !important;
        }

        .tbl1 > tbody > tr > th > span:last-child {
            color: #000 !important;
            font-size: 10px !important;
            font-weight: normal !important;
        }

        .tbl > tbody > tr > th > span:first-child {
            color: #0054b6 !important;
            font-size: 10px !important;
            font-weight: bold !important;
        }
        .mp-table>tbody>tr>td {
    font-size: 10px !important;
}
        .mp-table>tbody>tr>th {
    font-size: 10px !important;
}
        .trBg {
    background: #f3f3f3 !important;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);

                Sys.Application.add_load(scrollbar);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 ">
                                    <div class="col-sm-12  no-padding">
                                        <div class="col-sm-2">
                                            <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList runat="server" ID="drpclass" class="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpclass_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList runat="server" ID="drpsection" class="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpsection_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                        <div class="col-sm-2">
                                            <label class="control-label">Status</label>
                                            <asp:DropDownList runat="server" ID="drpStatus" class="form-control-blue ">
                                                <asp:ListItem Value="A">Active</asp:ListItem>
                                                <asp:ListItem Value="B">Inactive</asp:ListItem>
                                                <asp:ListItem Value="W">Withdrwal</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <label class="control-label">Select S.R. NO.</label>
                                            <asp:DropDownList runat="server" ID="drpsrno" class="form-control-blue"></asp:DropDownList>

                                        </div>
                                        <div class="col-sm-2" runat="server">
                                            <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:LinkButton runat="server" ID="lnkView" class="button" Text="View" OnClick="lnkView_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"></asp:LinkButton>
                                                <div class="text-box-msg"></div>
                                            </div>
                                        </div>
                                    </div>
                                   <div class="col-sm-12  no-padding" visible="false" runat="server" id="icons" style="border-bottom: 2px solid #000; margin-bottom: 20px; padding-bottom: 5px !important;">
                                        <div style="float: right; font-size: 19px;" id="printbtn" class="">
                                            <a onclick="PrintDiv();" class="btn btn-sm">&nbsp;<i class="fa fa-print text-primary"></i>Print</a>
                                        </div>
                                    </div>
                                    <div id="divExport" class=" col-sm-12  no-padding">
                                        <div class="box-border-solid-h-a3 text-uppercase" style="padding: 5px;">
                                            <asp:Repeater runat="server" ID="rptStudent">
                                                <ItemTemplate>
                                                    <div class="term2" style="page-break-after: always;">
                                                        <table class="table mp-table p-table-bordered table-bordered trBg front text-uppercase" style="margin-bottom: 5px; text-transform: uppercase; width: 100%;">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="padding: 5px !important; padding-bottom: 2px !important;">
                                                                        <div id="header" runat="server" class="col-md-12 no-padding"></div>
                                                                        <table class="front table term2 tbl1 mp-table p-table-bordered table-bordered text-uppercase text-left" style="margin: 0px;">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">S.R. NO. : </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("admissionNo") %></span></th>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">STUDENT'S NAME : </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("StudentName") %></span></th>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">CLASS : </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("class_section") %></span></th>
                                                                                    <th class="p-pad-25" colspan="3"><span class="txt-rep-title-12-b customtext">DATE OF BIRTH : </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("dob") %></span></th>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class="front table term2 tbl mp-table p-table-bordered table-bordered text_center" style="margin-bottom: 10px">
                                                                            <tbody>
                                                                                <asp:Repeater runat="server" ID="rptmarks">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th style='vertical-align: middle; text-align: center;' colspan="2">EXAMS</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' colspan="9">TERM 1</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' colspan="9">TERM 2</th>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <th style='vertical-align: middle; text-align: center;' rowspan="3" colspan="2">SUBJECTS</th>
                                                                                        </tr>
                                                                                         <tr>
                                                                                            <th style='vertical-align: middle; text-align: center;' colspan="2">EVAL. 1</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' colspan="2">EVAL. 2</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' colspan="2">EVAL. 3</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' rowspan="2">TOTAL OF  BEST 2</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' rowspan="2">CONV. INTO 100</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' rowspan="2">GRADE</th>
                                                                                             <th style='vertical-align: middle; text-align: center;' colspan="2">EVAL. 4</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' colspan="2">EVAL. 5</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' colspan="2">EVAL. 6</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' rowspan="2">TOTAL OF  BEST 2</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' rowspan="2">CONV. INTO 100</th>
                                                                                            <th style='vertical-align: middle; text-align: center;' rowspan="2">GRADE</th>
                                                                                        </tr>
                                                                                         <tr>
                                                                                            <th style='vertical-align: middle; text-align: center;'>MM</th>
                                                                                            <th style='vertical-align: middle; text-align: center;'>OM</th>
                                                                                           <th style='vertical-align: middle; text-align: center;'>MM</th>
                                                                                            <th style='vertical-align: middle; text-align: center;'>OM</th>
                                                                                             <th style='vertical-align: middle; text-align: center;'>MM</th>
                                                                                            <th style='vertical-align: middle; text-align: center;'>OM</th>
                                                                                            <th style='vertical-align: middle; text-align: center;'>MM</th>
                                                                                            <th style='vertical-align: middle; text-align: center;'>OM</th>
                                                                                             <th style='vertical-align: middle; text-align: center;'>MM</th>
                                                                                            <th style='vertical-align: middle; text-align: center;'>OM</th>
                                                                                             <th style='vertical-align: middle; text-align: center;'>MM</th>
                                                                                            <th style='vertical-align: middle; text-align: center;'>OM</th>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <th style='vertical-align: middle; text-align: center; width:7%;'>S. NO.</th>
                                                                                            <th style='vertical-align: middle; text-align: left;''>SCHOLASTIC</th>
                                                                                             <th style='vertical-align: middle; text-align: center;' colspan="9"></th>
                                                                                            <th style='vertical-align: middle; text-align: center;' colspan="9"></th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="text_center">
                                                                                            <th style="text-align: center;">
                                                                                                <asp:Label runat="server" ID="Sno1"></asp:Label></th>
                                                                                            <th style="text-align: left;color: #0054b6 !important;"><%# Eval("SubjectName") %></th>
                                                                                            <th colspan="18"></th>
                                                                                        </tr>
                                                                                        <asp:Repeater runat="server" ID="rptmarksT2">
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td style="text-align: center; font-weight:bold;">
                                                                                                        <asp:Label runat="server" ID="Sno2" style="text-transform:lowercase"></asp:Label></td>
                                                                                                    <td style="text-align: left; font-weight:bold;color: #000 !important;">&nbsp;&nbsp;<%# Eval("PaperName") %></td>
                                                                                                    <th colspan="18"></th>
                                                                                                </tr>
                                                                                                <asp:Repeater runat="server" ID="rptmarksT3">
                                                                                                    <ItemTemplate>
                                                                                                        <tr class="text_center">
                                                                                                            <th style="text-align: right; font-weight:bold;color: #000 !important;"><i class="fa fa-dot-circle-o" style="font-size: 8px !important;"></i>
                                                                                                                <asp:Label runat="server" ID="Sno3"></asp:Label></th>
                                                                                                            <td style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("ActivityName") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("MaxMarks1") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Evel1") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("MaxMarks2") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Evel2") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("MaxMarks3") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Evel3") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Best2") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Conversion") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Grade") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("MaxMarks1_2") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Evel1_2") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("MaxMarks2_2") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Evel2_2") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("MaxMarks3_2") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Evel3_2") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Best2_2") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Conversion_2") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Grade_2") %></td>
                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                </asp:Repeater>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </tbody>
                                                                        </table>
                                                                        
                                                                    </td>
                                                                </tr>
                                                                
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function PrintDiv() {

            var headContent = document.getElementsByTagName('head')[0].innerHTML;

            var divContents = document.getElementById("divExport").innerHTML;
            var printWindow = window.open('', '', 'height=1020,width=1000, class="tbls"');
            printWindow.document.write('<html><head><title>REPORT CARD FOR Nur to Prep</title>' + headContent + '</head>');
            printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');

            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 2000);
            return false;
            printWindow.close();

        }
    </script>
</asp:Content>


