<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="PrintReportCardXII_1718_New.aspx.cs" Inherits="Comman_G7_PrintReportCardXII_1718_New" %>

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
                                            <asp:DropDownList runat="server" ID="drpsection" class="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpsection_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="divHideForGardian3">
                                            <label class="control-label">Select Stream&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList runat="server" ID="drpBranch" class="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="divHideForGardian4">
                                            <label class="control-label">Select Group&nbsp;<span class="vd_red"></span></label>
                                            <asp:DropDownList runat="server" ID="drpStream" class="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpStream_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3 hide" runat="server" id="divHideForGardian5">
                                            <label class="control-label">Status</label>
                                            <asp:DropDownList runat="server" ID="drpStatus" class="form-control-blue">
                                                <asp:ListItem Value="A">Active</asp:ListItem>
                                                <asp:ListItem Value="B">Inactive</asp:ListItem>
                                                <asp:ListItem Value="W">Withdrwal</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="divHideForGardian6">
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
                                        <div class="col-sm-3" id="div2" runat="server" visible="false">
                                            <label class="control-label">Condition of Promotion &nbsp;<span class="vd_red"></span></label>
                                            <asp:DropDownList ID="drpPromoTo" runat="server" CssClass="form-control-blue ">
                                                <asp:ListItem Value="2">Passed & Promoted to Class </asp:ListItem>
                                                <asp:ListItem Value="1">Promoted to Class </asp:ListItem>
                                                <asp:ListItem Value="3">Conditionally Promoted to Class </asp:ListItem>
                                                <asp:ListItem Value="4">Detained in Class </asp:ListItem>
                                                <asp:ListItem Value="5">Promoted with Warning to Class</asp:ListItem>
                                                <asp:ListItem Value="6">Promoted on the basis of Annual Examination to Class</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1" id="div3" runat="server" visible="false">
                                            <label class="control-label">Class &nbsp;<span class="vd_red">*</span></label>
                                            <asp:TextBox ID="txtPromotedtoclass" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2" id="div4" runat="server" visible="false">
                                            <label class="control-label">School Re-opens On  &nbsp;<span class="vd_red">*</span></label><br />
                                            <asp:TextBox ID="txtSchoolReopenon" runat="server" CssClass="form-control-blue datepicker-normal" Style="float: left; width: 70%;" placeholder="Date"></asp:TextBox>
                                            <asp:TextBox ID="txtTimes" runat="server" CssClass="form-control-blue" Style="width: 30%;" placeholder="Time"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="divHideForGardian7">
                                            <label class="control-label">Date </label>
                                            <div class="">
                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="divHideForGardian8">
                                            <label class="control-label">Place </label>
                                            <asp:TextBox ID="txtPlace" runat="server" CssClass="form-control-blue" placeholder="Enter Place"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2" runat="server" id="divHideForGardian9">
                                            <label class="control-label">Attendance</label>
                                            <asp:DropDownList ID="drpAttendance" runat="server" CssClass="form-control-blue ">
                                                <asp:ListItem Value="0">Auto</asp:ListItem>
                                                <asp:ListItem Value="1">Manual</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1" runat="server" id="divHideForGardian10">
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
                                                    <div id="break_div" class="term2 table-responsive" style="page-break-after: always; padding: 5px; margin-top: 0px; border: 3px double #000;">
                                                        <table class="front table mp-table text-uppercase" style="margin-bottom: 5px; padding: 0px 5px !important; text-transform: uppercase; width: 100%;">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <div id="header" runat="server" class="col-md-12 no-padding"></div>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="padding: 5px !important;">
                                                                        <table class="front table term2 tbl1 mp-table p-table-bordered table-bordered text-uppercase text-left" style="margin:0px;">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">ADMISSION NO.- </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("admissionNo") %></span></th>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">STUDENT'S NAME- </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("StudentName") %></span></th>
                                                                                    <th class="p-pad-25" colspan="3"><span class="txt-rep-title-12-b customtext">DATE OF BIRTH- </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("dob") %></span></th>
                                                                                    <th rowspan="3" style="text-align: center; padding: 1px !important; width: 12%; background: transparent !important;">
                                                                                        <img src='<%# Eval("PhotoPath").ToString()==""?"../../uploads/pics/Student.ico":"../" +Eval("PhotoPath").ToString() %>' style="height: 120px;background-size: cover;width: 100%;"></th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">CLASS/SECTION- </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("class_section") %></span></th>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">FATHER'S NAME- </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("Fathername") %></span></th>
                                                                                    <th class="p-pad-25 "><span class="txt-rep-title-12-b customtext">MOTHER'S NAME- </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("MotherName") %></span></th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th class="p-pad-25" colspan="3"><span class="txt-rep-title-12-b customtext">ROLL NO.- </span><span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("InstituteRollNo") %></span></th>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="padding: 5px !important; padding-top:0px !important;">
                                                                        <table class="table term2 tbl mp-table p-table-bordered table-bordered text_center" style="margin-bottom: 0px">
                                                                            <tbody>
                                                                                <asp:Repeater runat="server" ID="rptmarksT1">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th class="text_center" style="font-size: 11px !important; vertical-align: middle;width:10%;" rowspan="3">SUBJECT CODE</th>
                                                                                            <th class="text_center" style="font-size: 11px !important; vertical-align: middle;width:30%;" rowspan="3">SUBJECT</th>
                                                                                            <th colspan="7" style="font-size: 11px !important; vertical-align: middle;width:60%;">TERM I</th>
                                                                                            </tr>
                                                                                        <tr>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TEST(10)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">TERM I EXAM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">CONVERSION</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TOTAL(100)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">GRADE</th>
                                                                                            </tr>
                                                                                        <tr>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>

                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="text_center">
                                                                                            <th style="text-align: center;" rowspan="2"><%# Eval("SubjectCode") %></th>
                                                                                            <th style="text-align: left;"><%# Eval("SubjectName") %></th>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblIA"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHYMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHY"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoardMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoard"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblTotal"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGrade"></asp:Label></td>
                                                                                        </tr>
                                                                                        <tr class="text_center">
                                                                                            <th style="text-align: left;">INTERNAL ASSESSMENT/PRAC.</th>
                                                                                            <td style="text-align: center" colspan="3"></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPracMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPrac"></asp:Label></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <tr class="text_center" runat="server" id="tr1">
                                                                                             <th class="text-left" colspan="2"><span><b>TOTAL (Best 5 - Language 1 compulsory)</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHTotal"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="text_center" runat="server" id="tr2">
                                                                                            <th class="text-left" colspan="2"><span><b>PERCENTAGE</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPercentage"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="text_center" runat="server" id="tr3">
                                                                                            <th class="text-left" colspan="2"><span><b>RANK</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHRank"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="text_center" runat="server" id="tr4">
                                                                                            <th class="text-left" colspan="2"><span><b>POSITION</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPosition"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptmarksT1Additional">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th colspan="7" style="font-size: 11px !important; vertical-align: middle;width:100%;">ADDITIONAL/SKILL SUBJECT</th>
                                                                                            </tr>
                                                                                        <tr>
                                                                                            <th class="text_center" style="font-size: 11px !important; vertical-align: middle;width:10%;" rowspan="3">SUBJECT CODE</th>
                                                                                            <th class="text_center" style="font-size: 11px !important; vertical-align: middle;width:30%;" rowspan="3">SUBJECT</th>
                                                                                            <th colspan="7" style="font-size: 11px !important; vertical-align: middle;width:60%;">TERM I</th>
                                                                                            </tr>
                                                                                        <tr>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TEST(10)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">TERM I EXAM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">CONVERSION</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TOTAL(100)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">GRADE</th>
                                                                                            </tr>
                                                                                        <tr>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>

                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="text_center">
                                                                                            <th style="text-align: center;" rowspan="2"><%# Eval("SubjectCode") %></th>
                                                                                            <th style="text-align: left;"><%# Eval("SubjectName") %></th>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblIA"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHYMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHY"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoardMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoard"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblTotal"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGrade"></asp:Label></td>
                                                                                        </tr>
                                                                                        <tr class="text_center">
                                                                                            <th style="text-align: left;">INTERNAL ASSESSMENT/PRAC.</th>
                                                                                            <td style="text-align: center" colspan="3"></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPracMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPrac"></asp:Label></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <tr class="text_center">
                                                                                             <th class="text-left" colspan="2"><span><b>TOTAL (Best 5 - Language 1 compulsory)</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHTotal"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="text_center">
                                                                                            <th class="text-left" colspan="2"><span><b>PERCENTAGE</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPercentage"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="text_center">
                                                                                            <th class="text-left" colspan="2"><span><b>RANK</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHRank"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="text_center">
                                                                                            <th class="text-left" colspan="2"><span><b>POSITION</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPosition"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptmarksT2">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th class="text_center" style="font-size: 11px !important; vertical-align: middle;width:7%;" rowspan="3">SUBJECT CODE</th>
                                                                                            <th class="text_center" style="font-size: 11px !important; vertical-align: middle;width:21%;" rowspan="3">SUBJECT</th>
                                                                                            <th colspan="7" style="font-size: 11px !important; vertical-align: middle;width:31%;">TERM I</th>
                                                                                            <th colspan="7" style="font-size: 11px !important; vertical-align: middle;width:31%;">TERM II</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="3">GRAND TOTAL (200)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="3">TOTAL (100)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="3">GRADE</th>
                                                                                            </tr>
                                                                                        <tr>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TEST (10)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">TERM I EXAM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">CONVERSION</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TOTAL (100)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">GRADE</th>

                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TEST (10)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">TERM II EXAM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">CONVERSION</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TOTAL (100)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">GRADE</th>
                                                                                            

                                                                                            </tr>
                                                                                        <tr>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="text_center">
                                                                                            <th style="text-align: center;" rowspan="2"><%# Eval("SubjectCode") %></th>
                                                                                            <th style="text-align: left;"><%# Eval("SubjectName") %></th>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblIA"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHYMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHY"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoardMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoard"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblTotal"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGrade"></asp:Label></td>

                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblIA2"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblAEMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblAE"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoardMM2"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoard2"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblTotal2"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGrade2"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGrandTotal"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGrandTotalF"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGradeF"></asp:Label></td>

                                                                                        </tr>
                                                                                        <tr class="text_center">
                                                                                            <th style="text-align: left;">INTERNAL ASSESSMENT/PRAC.</th>
                                                                                            <td style="text-align: center" colspan="3"></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPracMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPrac"></asp:Label></td>
                                                                                            <td style="text-align: center" colspan="3"></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPracMM2"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPrac2"></asp:Label></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                   <FooterTemplate>
                                                                                        <tr class="text_center" runat="server" id="tr1">
                                                                                            <th class="text-left" colspan="2"><span><b>TOTAL (Best 5 - Language 1 compulsory)</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHTotal"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHTotal2"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHTotalGT"></asp:Label></b></td>
                                                                                            <td></td>

                                                                                        </tr>
                                                                                        <tr class="text_center" runat="server" id="tr2">
                                                                                            <th class="text-left" colspan="2"><span><b>PERCENTAGE</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPercentage"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPercentage2"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPercentageGT"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="text_center" runat="server" id="tr3">
                                                                                            <th class="text-left" colspan="2"><span><b>RANK</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHRank"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHRank2"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHRankGT"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="text_center" runat="server" id="tr4">
                                                                                            <th class="text-left" colspan="2"><span><b>POSITION</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPosition"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPosition2"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPositionGT"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptmarksT2Additional">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th class="text_center" style="font-size: 11px !important; vertical-align: middle;width:8%;" rowspan="2">SUBJECT CODE</th>
                                                                                            <th class="text_center" style="font-size: 11px !important; vertical-align: middle;width:22%;" rowspan="2">SUBJECT</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TEST (10)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">TERM I EXAM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">CONVERSION</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TOTAL (100)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">GRADE</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TEST (10)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">TERM II EXAM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' colspan="2">CONVERSION</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TOTAL (100)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">GRADE</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">GRAND TOTAL (200)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">TOTAL (100)</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;' rowspan="2">GRADE</th>
                                                                                            </tr>
                                                                                        
                                                                                        <tr>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MM</th>
                                                                                            <th style='font-size: 11px !important; vertical-align: middle;'>MO</th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                         <tr class="text_center">
                                                                                            <th style="text-align: center;" rowspan="2"><%# Eval("SubjectCode") %></th>
                                                                                            <th style="text-align: left;"><%# Eval("SubjectName") %></th>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblIA"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHYMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblHY"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoardMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoard"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblTotal"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGrade"></asp:Label></td>

                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblIA2"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblAEMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblAE"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoardMM2"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblConvBoard2"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblTotal2"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGrade2"></asp:Label></td>
                                                                                             <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGrandTotal"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGrandTotalF"></asp:Label></td>
                                                                                            <td style="text-align: center" rowspan="2">
                                                                                                <asp:Label runat="server" ID="lblGradeF"></asp:Label></td>

                                                                                        </tr>
                                                                                        <tr class="text_center">
                                                                                            <th style="text-align: left;">INTERNAL ASSESSMENT/PRAC.</th>
                                                                                            <td style="text-align: center" colspan="3"></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPracMM"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPrac"></asp:Label></td>
                                                                                            <td style="text-align: center" colspan="3"></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPracMM2"></asp:Label></td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label runat="server" ID="lblPrac2"></asp:Label></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <tr class="text_center">
                                                                                            <th class="text-left" colspan="2"><span><b>TOTAL (Best 5 - Language 1 compulsory)</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHTotal"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHTotal2"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHTotalGT"></asp:Label></b></td>
                                                                                            <td></td>

                                                                                        </tr>
                                                                                        <tr class="text_center">
                                                                                            <th class="text-left" colspan="2"><span><b>PERCENTAGE</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPercentage"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPercentage2"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPercentageGT"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="text_center">
                                                                                            <th class="text-left" colspan="2"><span><b>RANK</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHRank"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHRank2"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHRankGT"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="text_center">
                                                                                            <th class="text-left" colspan="2"><span><b>POSITION</b></span></th>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPosition"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td colspan="5"></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPosition2"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td style="text-align: center"><b>
                                                                                                <asp:Label runat="server" ID="lblHPositionGT"></asp:Label></b></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptAttendance1">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" colspan="2">Attendance</th>
                                                                                            <td class="text-center" colspan="6"><%# Eval("t1Att") %></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptAttendance2">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" colspan="2">Attendance</th>
                                                                                            <td class="text-center" colspan="6"><%# Eval("t1Att") %></td>
                                                                                            <td></td>
                                                                                            <td class="text-center" colspan="6"><%# Eval("t2Att") %></td>
                                                                                            <td></td>
                                                                                            <td colspan="3"></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptSkil1">
                                                                                   <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th style="padding: 0px 2px !important;" rowspan="2" colspan="2" class="text-left text-uppercase">CO-SCHOLASTIC AREAS</th>
                                                                                            <th style="padding: 0px 2px !important; vertical-align: top;" colspan="7" class="text-uppercase">GRADE</th>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <th style="padding: 0px 2px !important; vertical-align: top;" class="text-uppercase" colspan="7">TERM I</th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" style="padding: 0px 2px !important;" colspan="2"><span class="text-uppercase"><%# Eval("CoscholasticName_1") %></span></td>
                                                                                            <td style="padding: 0px 2px !important;" class="text-center" colspan="7"><span><%# Eval("Grade_1") %></span></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptSkil2">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th style="padding: 0px 2px !important;" rowspan="2" colspan="2" class="text-left text-uppercase">CO-SCHOLASTIC AREAS</th>
                                                                                            <th style="padding: 0px 2px !important; vertical-align: top;" colspan="17" class="text-uppercase">GRADE</th>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <th style="padding: 0px 2px !important; vertical-align: top;" class="text-uppercase" colspan="7">TERM I</th>
                                                                                            <th style="padding: 0px 2px !important; vertical-align: top;" class="text-uppercase" colspan="7">TERM II</th>
                                                                                            <td colspan="3"></td>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" style="padding: 0px 2px !important;" colspan="2"><span class="text-uppercase"><%# Eval("CoscholasticName_1") %></span></td>
                                                                                            <td style="padding: 0px 2px !important;" class="text-center" colspan="7"><span><%# Eval("Grade_1") %></span></td>
                                                                                            <td style="padding: 0px 2px !important;" class="text-center" colspan="7"><span><%# Eval("Grade_2") %></span></td>
                                                                                                <td colspan="3"></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptRemark1">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" colspan='2' rowspan="2" style='padding: 2px !important;'>CLASS TEACHER'S REMARK</td>
                                                                                            <th class="text-center" colspan='7' style='padding: 2px !important;'>TERM I</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan='7' style='padding: 2px !important;'><span class='sentence-case'><%# Eval("Caption1") %></span></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptRemark2">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th class="text-left" colspan='2' rowspan="2" style='padding: 2px !important;'>CLASS TEACHER'S REMARK</td>
                                                                                            <th class="text-center" colspan='7' style='padding: 2px !important;'>TERM I</td>
                                                                                            <th class="text-center" colspan='7' style='padding: 2px !important;'>TERM II</td>
                                                                                                <td colspan="3"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan='7' style='padding: 2px !important;'><span class='sentence-case'><%# Eval("Caption1") %></span></td>
                                                                                            <td colspan='7' style='padding: 2px !important;'><span class='sentence-case'><%# Eval("Caption2") %></span></td>
                                                                                            <td colspan="3"></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class='table mp-table tbl p-table-bordered table-bordered text-uppercase' style="margin-bottom: 5px;">
                                                                            <tbody>
                                                                                
                                                                            </tbody>
                                                                        </table>
                                                                        
                                                                         <div class="col-sm-12 no-padding text-uppercase" style="padding: 0px; margin: 0px;">
                                                                            <table class="table" style="text-align: center; margin: 0px; margin-top: 5px; width: 100%;">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 40%; padding: 0px !important; vertical-align: top; padding-right: 10px !important;">
                                                                                            <table class="table mp-table tbl p-table-bordered table-bordered" style="text-align: center; margin-bottom: 0px;">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <th style="padding: 3px 5px !important;" colspan="4" class="text-center">GRADING SCALE FOR SCHOLASTIC/CO-SCHOLASTIC AREAS</th>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <th style="padding: 3px 5px !important;" class="text-center">MARKS</th>
                                                                                                        <th style="padding: 3px 5px !important;" class="text-center">GRADE</th>
                                                                                                        <th style="padding: 3px 5px !important;" class="text-center">MARKS</th>
                                                                                                        <th style="padding: 3px 5px !important;" class="text-center">GRADE</th>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">91-100</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">A1</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">51-60</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">C1</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">81-90</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">A2</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">41-50</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">C2</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">71-80</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">B1</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">33-40</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">D</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">61-70</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">B2</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">32 &amp; Below</td>
                                                                                                        <td style="padding: 3px 5px !important;" class="text-center">E (Essential Repeat)</td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                       <td style="width: 49%; padding: 0px !important; vertical-align: top;">
                                                                                            <div style="background: #ccc !important; border-bottom: 1px solid #000 !important;margin-bottom:0px !important;"><b style="padding: 0px; margin: 0px; font-size: 13px;">Graphical Analysis</b></div>
                                                                                            <asp:Chart ID="myChart" runat="server" BorderWidth="0" Width="550" Height="155" BackImage="" style="margin-right: 5px !important; margin-top: 0px !important;">
                                                                                                <Titles>
                                                                                                    <asp:Title ShadowOffset="3" Name="Items" />
                                                                                                </Titles>
                                                                                                <Legends>
                                                                                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="false" Name="Default"
                                                                                                        LegendStyle="Row" />
                                                                                                </Legends>
                                                                                                <ChartAreas>
                                                                                                    <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                                                                                                </ChartAreas>
                                                                                            </asp:Chart>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                        <table class="table term2 tbl mp-table p-table-bordered table-bordered text-uppercase" id="tblResult" runat="server" style="margin-top: 3px; margin-bottom: 0px; width: 100%;">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class="text-left" style="width: 40%; padding-right:1px;"><span style="font-weight: bold;"><span style="font-weight: bold;color: #0054b6 !important;">RESULT</span>&nbsp;&nbsp;&nbsp;
                                                                                        <asp:Label runat="server" ID="lblresulttype"></asp:Label></span>&nbsp;&nbsp;<asp:Label runat="server" ID="lblpromotedClass"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 40%;" class="text-left"><span style="font-weight: bold;">School reopens on &nbsp;</span>&nbsp;&nbsp;<asp:Label runat="server" ID="lblReopenon"></asp:Label></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class="table mp-table p-table-bordered table-bordered text-uppercase" style="margin-top: 2px; margin-bottom: 2px;">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 20%"><span style="font-weight: bold">Place</span>&nbsp;<asp:Label runat="server" ID="lblPlace"></asp:Label>
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Date</span>&nbsp;<asp:Label runat="server" ID="lblprintdate"></asp:Label></td>
                                                                                    <td style="width: 30%; vertical-align: bottom; text-align: center;">
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Class Teacher</span></td>
                                                                                    <td style="width: 25%; vertical-align: bottom; text-align: center;">
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Principal</span></td>
                                                                                    <td style="width: 25%; vertical-align: bottom; text-align: center;">
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Parents</span></td>
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
            var printWindow = window.open('', '', 'height=1100,width=1000, class="tbls"');
            printWindow.document.write('<html><head><title>REPORT CARD FOR XII</title>' + headContent + '</head>');
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


