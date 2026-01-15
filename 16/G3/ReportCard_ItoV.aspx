<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="ReportCard_ItoV.aspx.cs" Inherits="ReportCard_ItoV" %>

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
            padding: 0px 4px !important;
        }

        .front > tbody > tr > td {
            padding: 0px 4px !important;
        }

        .term1 > tbody > tr > th {
            padding: 0px 4px !important;
        }

        .front > tbody > tr > th {
            padding: 0px 4px !important;
        }

        .term1 > .mp-table > tbody > tr > td, .mp-table > tfoot > tr > td {
            font-size: 11px !important;
            padding: 1px 5px !important;
            color: #000 !important;
        }

        .front > .mp-table > tbody > tr > td, .mp-table > tfoot > tr > td {
            font-size: 11px !important;
            padding: 0px 4px !important;
            color: #000 !important;
        }

        .term2 > .mp-table > tbody > tr > td, .mp-table > tfoot > tr > td {
            font-size: 11px !important;
            color: #000 !important;
        }

        .term2 > tbody > tr > td {
            padding: 0px 4px !important;
        }

        .term2 > tbody > tr > th {
            padding: 0px 4px !important;
        }

        .sentence-case {
            text-transform: initial !important;
        }

        .tbl > tbody > tr > th {
            color: #0054b6 !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }

        .tbl1 > tbody > tr > th {
            color: #0054b6 !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }

        .tbl > tbody > tr > th > span > b {
            color: #0054b6 !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }

        .tbl1 > tbody > tr > th > span > b {
            color: #0054b6 !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }

        .tbl1 > tbody > tr > th > span:first-child {
            color: #0054b6 !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }

        .tbl1 > tbody > tr > th > span:last-child {
            color: #000 !important;
            font-size: 12px !important;
            font-weight: normal !important;
        }

        .tbl > tbody > tr > th > span:first-child {
            color: #0054b6 !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }
        .logo-size:first-child {
                margin-top: 15px !important;
                margin-left: 5px;
        }
        .logo-size:last-child {
                margin-top: 15px !important;
                margin-right: 5px;
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
                                        <asp:Label runat="server" ID="lblError" Visible="false" Style="color: red"></asp:Label>
                                        <div class="col-sm-3" runat="server" id="divHideForGardian1">
                                            <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList runat="server" ID="drpclass" class="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpclass_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                        <div class="col-sm-3" runat="server" id="divHideForGardian2">
                                            <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList runat="server" ID="drpsection" class="form-control-blue validatedrp"></asp:DropDownList>

                                        </div>
                                        <div class="col-sm-3">
                                        <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlMedium" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                        <div class="col-sm-3" runat="server" id="divHideForGardian3">
                                            <label class="control-label">Status</label>
                                            <asp:DropDownList runat="server" ID="drpStatus" class="form-control-blue" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged" AutoPostBack="true">
                                             <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="A" Selected="True">Active</asp:ListItem>
                                            <asp:ListItem Value="AB">Active & Blocked</asp:ListItem>
                                            <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                            <asp:ListItem Value="B">Blocked</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="divHideForGardian4">
                                            <label class="control-label">Select S.R. NO.</label>
                                            <asp:DropDownList runat="server" ID="drpsrno" class="form-control-blue"></asp:DropDownList>

                                        </div>
                                        <div class="col-sm-3">
                                            <label class="control-label">Evaluation</label>
                                            <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpEval_SelectedIndexChanged">
                                                <asp:ListItem>Term1</asp:ListItem>
                                                <asp:ListItem>Term2</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3 hide" runat="server" id="divHideForGardian6">
                                            <label class="control-label">Place </label>
                                            <asp:TextBox ID="txtPlace" runat="server" CssClass="form-control-blue" placeholder="Enter Place"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2" runat="server" id="divHideForGardian7">
                                            <label class="control-label">Attendance</label>
                                            <asp:DropDownList ID="drpAttendance" runat="server" CssClass="form-control-blue ">
                                                <asp:ListItem Value="0">Auto</asp:ListItem>
                                                <asp:ListItem Value="1">Manual</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1" runat="server" id="divHideForGardian8">
                                            <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:LinkButton runat="server" ID="lnkView" class="button" Text="View" OnClick="lnkView_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"></asp:LinkButton>
                                                <div class="text-box-msg"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  no-padding" runat="server" visible="false" id="icons" style="margin-bottom: 0px; margin-top: 30px;">
                                        <div style="float: right; font-size: 19px;" id="printbtn" class="">
                                            <a onclick="PrintDiv();" class="btn btn-sm">&nbsp;<i class="fa fa-print text-primary"></i>Print</a>
                                        </div>
                                    </div>

                                    <div id="divExport">
                                        <div class="col-sm-12 box-border-solid-h-a3 text-uppercase" style="padding: 5px;">
                                            <asp:Repeater runat="server" ID="rptStudent">
                                                <ItemTemplate>
                                                    <div id="break_div" class="term2 table-responsive" style="height: 1028px; page-break-after: always; padding: 5px; margin-top: 0px; border: 3px double #000;">
                                                        <table class="front table mp-table text-uppercase" style="margin-bottom: 2px; padding: 0px 5px !important; text-transform: uppercase; width: 100%;">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <div id="header" runat="server" class="col-md-12 no-padding"></div>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 5px !important;">
                                                                        <table class="front table term2 tbl1 mp-table p-table-bordered table-bordered text-uppercase text-left" style="margin:0px;">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">S.R. NO.-&nbsp;</span>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("admissionNo") %></span></th>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">STUDENT'S NAME :&nbsp;</span>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("StudentName") %></span></th>
                                                                                    <th class="p-pad-25" colspan="3"><span class="txt-rep-title-12-b customtext">DATE OF BIRTH :&nbsp;</span>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("dob") %></span></th>
                                                                                    <th rowspan="2" style="text-align: center; padding: 1px !important; width: 13%; background: transparent !important;">
                                                                                        <img src='<%# Eval("PhotoPath").ToString()==""?"../../uploads/pics/Student.ico":"../" +Eval("PhotoPath").ToString() %>' style="height: 90px; background-size: cover;width: 100%;"></th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">CLASS :&nbsp;</span>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("class_section") %></span></th>
                                                                                    <th class="p-pad-25 "><span class="txt-rep-title-12-b customtext">MEDIUM :&nbsp;</span>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("Medium") %></span></th>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">FATHER'S NAME- </span>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("Fathername") %></span></th>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="padding: 5px !important; padding-top:0px !important;">
                                                                        <table id="Term1Mark"  class="Term1Mark table term2 tbl mp-table p-table-bordered table-bordered text_center" style="margin-bottom: 3px">
                                                                            <tbody>
                                                                                <asp:Repeater runat="server" ID="rptMarkTerm1">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th style=' vertical-align: middle;'>SUBJECT</th>
                                                                                            <th style=' vertical-align: middle;'>UT-1</th>
                                                                                            <th style=' vertical-align: middle;'>UT-2</th>
                                                                                            <th style=' vertical-align: middle;'>H.Y.</th>
                                                                                            <th style=' vertical-align: middle;'>TOTAL</th>
                                                                                            <th style=' vertical-align: middle;' rowspan="2">GRADE</th>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <th style=' vertical-align: middle;'>M.M.</th>
                                                                                            <th style=' vertical-align: middle;'>25</th>
                                                                                            <th style=' vertical-align: middle;'>25</th>
                                                                                            <th style=' vertical-align: middle;'>100</th>
                                                                                            <th style=' vertical-align: middle;'>150</th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="text_center">
                                                                                             <th style="text-align: left;"><%# Eval("SubjectName") %></th>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT1"><%# Eval("UT1") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT2"><%# Eval("UT2") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHY"><%# Eval("HY") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblTotal"><%# Eval("TotalHY") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblGrade"></asp:Label></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptMarkTerm1Aditional">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th style=' vertical-align: middle; text-align:center;' colspan="6">ADDITIONAL SUBJECT</th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="text_center">
                                                                                             <th style="text-align: left;"><%# Eval("SubjectName") %></th>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT1"><%# Eval("UT1") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT2"><%# Eval("UT2") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHY"><%# Eval("HY") %></asp:Label></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="text_center">
                                                                                    <th class="text-left"><span><b>TOTAL</b></span></th>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT1Total_T1"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT2Total_T1"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblHYTotal_T1"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGTotalHY_T1"></asp:Label></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr class="text_center">
                                                                                    <th class="text-left"><span><b>PERCENTAGE</b></span></th>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT1Per_T1"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT2Per_T1"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblHYPer_T1"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGTotalPerHY_T1"></asp:Label></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr class="text_center">
                                                                                    <th class="text-left"><span><b>DIVISION</b></span></th>
                                                                                     <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGTotalDevHY_T1"></asp:Label></td>
                                                                                     <td></td>
                                                                                </tr>
                                                                                 <tr class="text_center">
                                                                                    <th class="text-left"><span><b>RANK</b></span></th>
                                                                                     <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblTrm1Rank_T1"></asp:Label></td>
                                                                                     <td></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table id="Term2Mark" class="Term2Mark table term2 tbl mp-table p-table-bordered table-bordered text_center" style="margin-bottom: 3px">
                                                                            <tbody>
                                                                                <asp:Repeater runat="server" ID="rptMarkTerm2">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th style='vertical-align: middle;'>SUBJECT</th>
                                                                                            <th style='vertical-align: middle;'>UT-1</th>
                                                                                            <th style='vertical-align: middle;'>UT-2</th>
                                                                                            <th style='vertical-align: middle;'>H.Y.</th>
                                                                                            <th style='vertical-align: middle;'>TOTAL</th>
                                                                                            <th style='vertical-align: middle;'>UT-3</th>
                                                                                            <th style='vertical-align: middle;'>UT-4</th>
                                                                                            <th style='vertical-align: middle;'>A.E.</th>
                                                                                            <th style='vertical-align: middle;'>TOTAL</th>
                                                                                            <th style='vertical-align: middle;'>Grand TOTAL</th>
                                                                                            <th style='vertical-align: middle;' rowspan="2">GRADE</th>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <th style='vertical-align: middle;'>M.M.</th>
                                                                                            <th style='vertical-align: middle;'>25</th>
                                                                                            <th style='vertical-align: middle;'>25</th>
                                                                                            <th style='vertical-align: middle;'>100</th>
                                                                                            <th style='vertical-align: middle;'>150</th>
                                                                                            <th style='vertical-align: middle;'>25</th>
                                                                                            <th style='vertical-align: middle;'>25</th>
                                                                                            <th style='vertical-align: middle;'>100</th>
                                                                                            <th style='vertical-align: middle;'>150</th>
                                                                                            <th style='vertical-align: middle;'>300</th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="text_center">
                                                                                            <th style="text-align: left;"><%# Eval("SubjectName") %></th>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT1"><%# Eval("UT1") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT2"><%# Eval("UT2") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHY"><%# Eval("HY") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblTotalHY"><%# Eval("TotalHY") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT3"><%# Eval("UT3") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT4"><%# Eval("UT4") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblAE"><%# Eval("AE") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblTotalAE"><%# Eval("TotalAE") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblGGtotal"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblGGrade"></asp:Label></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>

                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptMarkTerm2Aditional">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th style='vertical-align: middle; text-align: center;' colspan="11">ADDITIONAL SUBJECT</th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="text_center">
                                                                                            <th style="text-align: left;"><%# Eval("SubjectName") %></th>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT1"><%# Eval("UT1") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT2"><%# Eval("UT2") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHY"><%# Eval("HY") %></asp:Label></td>
                                                                                            <td></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT3"><%# Eval("UT3") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblUT4"><%# Eval("UT4") %></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblAE"><%# Eval("AE") %></asp:Label></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tr class="text_center">
                                                                                    <th class="text-left"><span><b>TOTAL</b></span></th>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT1Total_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT2Total_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblHYTotal_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGTotalHY_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT3Total_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT4Total_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblAETotal_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGTotalAE_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGGTotal_T2"></asp:Label></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr class="text_center">
                                                                                    <th class="text-left"><span><b>PERCENTAGE</b></span></th>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT1Per_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT2Per_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblHYPer_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGTotalPerHY_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT3Per_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblUT4Per_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblAEPer_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGTotalPerAE_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGGTotalPer_T2"></asp:Label></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr class="text_center">
                                                                                    <th class="text-left"><span><b>DIVISION</b></span></th>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGTotalDevHY_T2"></asp:Label></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                     <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGTotalDevAE_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblGGTotalDev_T2"></asp:Label></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr class="text_center">
                                                                                    <th class="text-left"><span><b>RANK</b></span></th>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblTrm1Rank_T2"></asp:Label></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                     <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblTrm2Rank_T2"></asp:Label></td>
                                                                                    <td style="text-align: center">
                                                                                        <asp:Label runat="server" ID="lblAllRank_T2"></asp:Label></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class="table term2 tbl mp-table p-table-bordered table-bordered text-uppercase" style="margin-bottom: 2px;">
                                                                            <tbody>
                                                                                <asp:Repeater runat="server" ID="rptAttendance1">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" style="width: 29%;">Attendance</th>
                                                                                            <td class="text-center"><%# Eval("t1Att") %></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptAttendance2">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" style="width:15.8%;">Attendance</th>
                                                                                            <td style="width: 29.7%;" class="text-center"><%# Eval("t1Att") %></td>
                                                                                            <td class="text-center"><%# Eval("t2Att") %></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </tbody>
                                                                        </table>
                                                                         <table class="table term2 tbl mp-table p-table-bordered table-bordered text-uppercase" style="margin-bottom: 3px">
                                                                            <tbody>
                                                                                <asp:Repeater runat="server" ID="rptSkil1">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th style="padding: 0px 2px !important; width: 50%;" class="text-left text-uppercase">PERSONALITY DEVELOPMENT CHART</th>
                                                                                            <th style="padding: 0px 2px !important; width: 50%;" class="text-center text-uppercase">HALF YEARLY EXAMINATION</th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" style="padding: 0px 2px !important;"><span class="text-uppercase"><%# Eval("CoscholasticName_1") %></span></th>
                                                                                            <td style="padding: 0px 2px !important;" class="text-center"><span><%# Eval("Grade_1") %></span></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptSkil2">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th style="padding: 0px 2px !important; width: 33%" class="text-left text-uppercase">PERSONALITY DEVELOPMENT CHART</th>
                                                                                            <th style="padding: 0px 2px !important; width: 33%;" class="text-center text-uppercase">HALF YEARLY EXAMINATION</th>
                                                                                            <th style="padding: 0px 2px !important; width: 33%;" class="text-center text-uppercase">ANNUAL EXAMINATION</th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" style="padding: 0px 2px !important;"><span class="text-uppercase"><%# Eval("CoscholasticName_1") %></span></th>
                                                                                            <td style="padding: 0px 2px !important;" class="text-center"><span><%# Eval("Grade_1") %></span></td>
                                                                                            <td style="padding: 0px 2px !important;" class="text-center"><span><%# Eval("Grade_2") %></span></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </tbody>
                                                                        </table>

                                                                         <table class='table mp-table tbl p-table-bordered table-bordered text-uppercase' style="margin-bottom: 2px;">
                                                                            <tbody>
                                                                                <asp:Repeater runat="server" ID="rptRemark1">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" colspan='2' style='padding: 2px !important; width: 23%;'><span style='font-weight: bold'>CLASS TEACHER'S REMARK</th>
                                                                                            <td colspan='2' style='padding: 2px !important; width: 77%;'><span class='upper-case'><%# Eval("Caption1") %></span></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptRemark2">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" style="padding: 2px !important; width: 23%;"><span style='font-weight: bold'>CLASS TEACHER'S REMARK</span></th>
                                                                                            <td style="padding: 2px !important; width: 32%;"><span class='upper-case'><%# Eval("Caption1") %></span></td>
                                                                                            <td style="padding: 2px !important;""><span class='upper-case'><%# Eval("Caption2") %></span></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </tbody>
                                                                        </table>
                                                                        
                                                                         <div class="col-sm-12 no-padding text-uppercase" style="padding: 0px; margin: 0px;">
                                                                            <table class="table mp-table tbl p-table-bordered table-bordered" style="text-align: center; margin-bottom: 0px;">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <th style="padding: 0px 4px !important;" colspan="4" class="text-center">GRADING SCALES FOR MAIN SUBJECTS</th>
                                                                                                        <th style="padding: 0px 4px !important;" colspan="2" class="text-center">GRADING SCALES FOR ADDITIONAL SUBJECTS</th>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <th style="padding: 0px 4px !important;" class="text-center">AE</th>
                                                                                                        <th style="padding: 0px 4px !important;" class="text-center">HYE</th>
                                                                                                        <th style="padding: 0px 4px !important;" class="text-center">GRADING SCALE</th>
                                                                                                        <th style="padding: 0px 4px !important;" class="text-center">GRADE</th>
                                                                                                        <th style="padding: 0px 4px !important;" class="text-center">GRADING SCALE</th>
                                                                                                        <th style="padding: 0px 4px !important;" class="text-center">GRADE</th>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">258-300</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">129-15</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">EXCELLENT</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">A+</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">EXCELLENT</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">A</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">213-257</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">107-128</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">VERY GOOD</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">A</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">VERY GOOD</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">B</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">183-212</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">92-106</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">GOOD</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">B+</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">GOOD</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">C</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">153-182</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">77-91</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">AVERAGE</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">B</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">AVERAGE</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">D</td>
                                                                                                    </tr>
                                                                                                     <tr>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">108-152</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">54-76</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">POOR</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">C</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">POOR</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">E</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">0-107</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">0-53</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">VERY POOR</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center">D</td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center"></td>
                                                                                                        <td style="padding: 0px 4px !important;" class="text-center"></td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                        </div>
                                                                         <table class="table term2 tbl mp-table p-table-bordered table-bordered text-uppercase" id="tblResult" runat="server" style="margin-top: 3px; margin-bottom: 0px; width: 100%;">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class="text-left" style="width: 40%; padding-right:1px;"><span style="font-weight: bold;color: #0054b6 !important;">RESULT</span>&nbsp;&nbsp;&nbsp;<span style="font-weight: bold;">
                                                                                        <asp:Label runat="server" ID="lblresulttype"></asp:Label></span>&nbsp;&nbsp;<asp:Label runat="server" ID="lblpromotedClass"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 40%;" class="text-left"><span style="font-weight: bold;">School reopens on &nbsp;</span>&nbsp;&nbsp;<asp:Label runat="server" ID="lblReopenon"></asp:Label></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class="table mp-table p-table-bordered table-bordered text-uppercase" style="margin-top: 2px; margin-bottom: 0px;">
                                                                            <tbody>
                                                                                <tr><td style="width: 25%"><span style="font-weight: bold">Place</span>&nbsp;<asp:Label runat="server" ID="lblPlace"></asp:Label>
                                                                                    <td style="width: 25%; vertical-align: bottom; text-align: center;" rowspan="2">
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Class Teacher</span></td>
                                                                                    <td style="width: 25%; vertical-align: bottom; text-align: center;" rowspan="2">
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Principal</span></td>
                                                                                    <td style="width: 25%; vertical-align: bottom; text-align: center;" rowspan="2">
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Parents</span></td>
                                                                                    </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%; vertical-align: bottom; text-align: left;">
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Date</span>&nbsp;<asp:Label runat="server" ID="lblprintdate"></asp:Label></td>
                                                                                    
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <div class='text-center' runat="server" visible="false" id="divTagline">This is an electronically generated report card through Parent Portal.</div>
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
            printWindow.document.write('<html><head><title>REPORT CARD FOR ItoV</title>' + headContent + '</head>');
            printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');

            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 2000);
            return false;
            printWindow.close();

        }
        function showHide() {
            if ($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpEval").val() == "Term1") {
                $(".Term1Mark").removeClass('hide');
                $(".Term2Mark").addClass('hide');
            }
            if ($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpEval").val() == "Term2") {
                $(".Term1Mark").addClass('hide');
                $(".Term2Mark").removeClass('hide');
            }
        }
    </script>
</asp:Content>

