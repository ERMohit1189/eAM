<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="ReportCard_NurtoPrep_1718.aspx.cs" Inherits="common_G1_ReportCard_NurtoPrep_1718" %>

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
                                            <asp:DropDownList runat="server" ID="drpclass" class="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpclass_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                        <div class="col-sm-3" runat="server" id="divHideForGardian2">
                                            <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList runat="server" ID="drpsection" class="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpsection_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                        <div class="col-sm-3 hide" runat="server" id="divHideForGardian3">
                                            <label class="control-label">Status</label>
                                            <asp:DropDownList runat="server" ID="drpStatus" class="form-control-blue ">
                                                <asp:ListItem Value="A">Active</asp:ListItem>
                                                <asp:ListItem Value="B">Inactive</asp:ListItem>
                                                <asp:ListItem Value="W">Withdrwal</asp:ListItem>
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
                                        <div class="col-sm-3" runat="server" id="divHideForGardian5">
                                            <label class="control-label">Date </label>
                                            <div class="">
                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3" runat="server" id="divHideForGardian6">
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
                                    <div class="col-sm-12  no-padding" runat="server" id="icons" style="margin-bottom: 0px; margin-top: 30px;">
                                        <div style="float: right; font-size: 19px;" id="printbtn" class="">
                                            <a onclick="PrintDiv();" class="btn btn-sm">&nbsp;<i class="fa fa-print text-primary"></i>Print</a>
                                        </div>
                                    </div>

                                    <div id="divExport" class=" col-sm-12  no-padding">
                                        <div class="box-border-solid-h-a3 text-uppercase" style="padding: 5px;">
                                            <asp:Repeater runat="server" ID="rptStudent">
                                                <ItemTemplate>
                                                    <div class="term2" style="page-break-after: always; padding: 5px; border: 3px double #000;">
                                                        <table class="front table mp-table text-uppercase" style="margin-bottom: 5px; text-transform: uppercase; width: 100%;">
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
                                                                                    <th class="p-pad-25" colspan="3"><span class="txt-rep-title-12-b customtext">DATE OF BIRTH : </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("dob") %></span></th>
                                                                                    <th rowspan="3" style="text-align: center; padding: 1px !important; width: 15%; background: transparent !important;">
                                                                                        <img src='<%# Eval("PhotoPath").ToString()==""?"../../uploads/pics/Student.ico":"../" +Eval("PhotoPath").ToString() %>' style="height: 117px; background-size: cover; width: 100%;"></th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">CLASS : </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("class_section") %></span></th>
                                                                                    <th class="p-pad-25"><span class="txt-rep-title-12-b customtext">FATHER'S NAME : </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("Fathername") %></span></th>
                                                                                    <th class="p-pad-25 "><span class="txt-rep-title-12-b customtext">MOTHER'S NAME : </span>
                                                                                        <br>
                                                                                        <span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("MotherName") %></span></th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th class="p-pad-25" colspan="3"><span class="txt-rep-title-12-b customtext">ROLL NO. : </span><span class="txt-rep-title-12-b customtext" style="font-weight: normal !important;"><%# Eval("InstituteRollNo") %></span></th>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class="front table term2 tbl mp-table p-table-bordered table-bordered text_center" style="margin-bottom: 10px">
                                                                            <tbody>
                                                                                <asp:Repeater runat="server" ID="rptmarks">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th style='vertical-align: middle; text-align: center; width:7%;'>S. NO.</th>
                                                                                            <th style='vertical-align: middle; text-align: left; width:43%;'>SCHOLASTIC</th>
                                                                                            <th style='vertical-align: middle; text-align: center;'class="thTerm1">TERM 1</th>
                                                                                            <th style='vertical-align: middle; text-align: center; width:25%;' class="thTerm2">TERM 2</th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="text_center">
                                                                                            <th style="text-align: center;">
                                                                                                <asp:Label runat="server" ID="Sno1"></asp:Label></th>
                                                                                            <th style="text-align: left;color: #0054b6 !important;" colspan="3" class="colspanTerm2"><%# Eval("SubjectName") %></th>
                                                                                        </tr>
                                                                                        <asp:Repeater runat="server" ID="rptmarksT2">
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td style="text-align: center; font-weight:bold;">
                                                                                                        <asp:Label runat="server" ID="Sno2" style="text-transform:lowercase"></asp:Label></td>
                                                                                                    <td style="text-align: left; font-weight:bold;color: #000 !important;" class="colspanTerm2" colspan="3">&nbsp;&nbsp;<%# Eval("PaperName") %></td>
                                                                                                </tr>
                                                                                                <asp:Repeater runat="server" ID="rptmarksT3">
                                                                                                    <ItemTemplate>
                                                                                                        <tr class="text_center">
                                                                                                            <th style="text-align: right; font-weight:bold;color: #000 !important;"><i class="fa fa-dot-circle-o" style="font-size: 8px !important;"></i>
                                                                                                                <asp:Label runat="server" ID="Sno3"></asp:Label></th>
                                                                                                            <td style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("ActivityName") %></td>
                                                                                                            <td style="text-align: center;"><%# Eval("Grage1") %></td>
                                                                                                            <td style="text-align: center;" class="term2td"><%# Eval("Grade2") %></td>
                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                </asp:Repeater>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptSkil1">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th></th>
                                                                                            <th colspan="2" style="padding: 1.5px 5px !important;" class="text-left text-uppercase">CO-SCHOLASTIC</span></th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th style="text-align: center; font-weight:bold;"><asp:Label runat="server" ID="Sno1"></asp:Label></th>
                                                                                            <th colspan="2" style="text-align: left; padding: 1.5px 5px !important;"><span class="text-uppercase"><%# Eval("CoscholasticGroup") %></span></th>
                                                                                        </tr>
                                                                                        <asp:Repeater runat="server" ID="rptSkil1A">

                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <th style="text-align: center; font-weight:bold; color:#000 !important;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" style="color:#000 !important;" ID="Sno2"></asp:Label></th>
                                                                                                    <td style="padding: 1.5px 5px !important;"><span class="text-uppercase"><%# Eval("CoscholasticName_1") %></span></td>
                                                                                                    <td style="padding: 1.5px 5px !important;" class="text-center"><span><%# Eval("Grade_1") %></span></td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptSkil2">
                                                                                    <HeaderTemplate>
                                                                                        <tr>
                                                                                            <th></th>
                                                                                            <th colspan="3" style="padding: 1.5px 5px !important;" class="text-left text-uppercase">CO-SCHOLASTIC</span></th>
                                                                                        </tr>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th style="text-align: center; font-weight:bold;"><asp:Label runat="server" ID="Sno1"></asp:Label></th>
                                                                                            <th colspan="3" style="text-align: left; padding: 1.5px 5px !important;"><span class="text-uppercase"><%# Eval("CoscholasticGroup") %></span></th>
                                                                                        </tr>
                                                                                        <asp:Repeater runat="server" ID="rptSkil2A">

                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <th style="text-align: center; font-weight:bold; color:#000 !important;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" style="color:#000 !important;" ID="Sno2"></asp:Label></th>
                                                                                                    <td style="padding: 1.5px 5px !important;"><span class="text-uppercase"><%# Eval("CoscholasticName_1") %></span></td>
                                                                                                    <td style="padding: 1.5px 5px !important;" class="text-center"><span><%# Eval("Grade_1") %></span></td>
                                                                                                    <td style="padding: 1.5px 5px !important;" class="text-center"><span><%# Eval("Grade_2") %></span></td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class="table tbl term2 mp-table p-table-bordered table-bordered text-uppercase" style="margin-bottom: 0px; margin-top: 0px; width: 100%;">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <th colspan="2" style="width:50%; text-align:left;">HEALTH STATUS</th>
                                                                                    <th class="thTerm1"  style="width:50%">TERM 1</th>
                                                                                    <th style="width:25%" class="thTerm2">TERM 2</th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th colspan="2" style=" text-align:left; color:#000 !important;">HEIGHT (CMS.)</th>
                                                                                    <th><asp:Label runat="server" ID="lblHeightTerm1" style="color:#000 !important;"></asp:Label></th>
                                                                                    <th class="thTerm2"><asp:Label runat="server" ID="lblHeightTerm2" style="color:#000 !important;"></asp:Label></th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <th colspan="2" style=" text-align:left; color:#000 !important;">WEIGHT (KG.)</th>
                                                                                    <th><asp:Label runat="server" ID="lblWeightTerm1" style="color:#000 !important;"></asp:Label></th>
                                                                                    <th class="thTerm2"><asp:Label runat="server" ID="lblWeightTerm2" style="color:#000 !important;"></asp:Label></th>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class="table tbl term2 mp-table p-table-bordered table-bordered text-uppercase" style="margin-bottom: 0px; margin-top: 0px; width: 100%;">
                                                                            <tbody>
                                                                                <asp:Repeater runat="server" ID="rptAttendance1">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th style="width: 42%;" class="text-left">Attendance</th>
                                                                                            <td class="text-center"><%# Eval("t1Att") %></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptAttendance2">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th style="width: 15%" class="text-left">Attendance</th>
                                                                                            <td style="width: 32%;" class="text-center"><%# Eval("t1Att") %></td>
                                                                                            <td style="width: 32%;" class="text-center"><%# Eval("t2Att") %></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class='table mp-table tbl p-table-bordered table-bordered text-uppercase' style="margin-bottom: 0px; margin-top: 0px; width: 100%;">
                                                                            <tbody>
                                                                                <asp:Repeater runat="server" ID="rptRemark1">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th style="width: 42%;" class="text-left">CLASS TEACHER'S REMARK</th>
                                                                                            <td class="text-left"><span class='upper-case'><%# Eval("Caption1") %></span></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="rptRemark2">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <th style="width: 15%" class="text-left">CLASS TEACHER'S REMARK</th>
                                                                                            <td style="width: 32%;" class="text-left"><span class='upper-case'><%# Eval("Caption1") %></span></td>
                                                                                            <td style="width: 32%;" class="text-left"><span class='upper-case'><%# Eval("Caption2") %></span></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class="table term2 tbl mp-table p-table-bordered table-bordered text-uppercase" id="tblResult" runat="server" style="margin-top: 0px; margin-bottom: 10px; width: 100%;">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <th style="width: 15%" class="text-left">RESULT</th>
                                                                                    <td class="text-left" style="width: 32%;"><span style="font-weight: bold;">
                                                                                        <asp:Label runat="server" ID="lblresulttype"></asp:Label></span>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblpromotedClass"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 32%;" class="text-left"><span style="font-weight: bold;">School reopens on &nbsp;</span>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblReopenon"></asp:Label></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class="table mp-table p-table-bordered table-bordered text-uppercase" style="margin-bottom: 5px;">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 20%"><span style="font-weight: bold">Place</span>&nbsp;<asp:Label runat="server" ID="lblPlace"></asp:Label><br>
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Date</span>&nbsp;<asp:Label runat="server" ID="lblprintdate"></asp:Label></td>
                                                                                    <td style="width: 30%; vertical-align: bottom; text-align: center;">
                                                                                        <br>
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Class Teacher</span></td>
                                                                                    <td style="width: 25%; vertical-align: bottom; text-align: center;">
                                                                                        <br>
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Principal</span></td>
                                                                                    <td style="width: 25%; vertical-align: bottom; text-align: center;">
                                                                                        <br>
                                                                                        <br>
                                                                                        <br>
                                                                                        <span style="font-weight: bold">Parents</span></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <div class="col-sm-12 no-padding text-uppercase" style="padding: 0px; margin: 0px;">
                                                                            <table class="table" style="text-align: center; margin: 0px; padding: 10px; margin-top: 5px; width: 100%;">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 100%; padding: 0px !important; vertical-align: top; padding-right: 5px !important;">
                                                                                            <table class="table mp-table tbl p-table-bordered table-bordered" style="text-align: center; margin-bottom: 0px;">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <th style="padding: 6px 5px !important;" colspan="2" class="text-center">CRITERIA FOR SCHOLASTIC AREAS</th>
                                                                                                        <th style="padding: 6px 5px !important;" colspan="2" class="text-center">CRITERIA FOR CO-SCHOLASTIC AREAS</th>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <th style="padding: 6px 5px !important;" class="text-center">GRADING SCALE</th>
                                                                                                        <th style="padding: 6px 5px !important;" class="text-center">GRADE</th>
                                                                                                        <th style="padding: 6px 5px !important;" class="text-center">GRADING SCALE</th>
                                                                                                        <th style="padding: 6px 5px !important;" class="text-center">GRADE</th>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">OUTSTANDING (90% - 100%)</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">A+</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">OUTSTANDING</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">A+</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">EXCELLENT (80% - 89%)</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">A</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">EXCELLENT</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">A</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">VERY GOOD (70% - 79%)</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">B+</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">VERY GOOD</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">B</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">GOOD (60% -69%)</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">B</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">GOOD</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">C</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">SATISFACTORY (40 - 59%)</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">C</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">AVERAGE</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">D</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">SCOPE FOR IMPROVEMENT (BELOW 40%)</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center">D</td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center"></td>
                                                                                                        <td style="padding: 6px 5px !important;" class="text-center"></td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
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
            printWindow.document.write('<html><head><title>REPORT CARD FOR Nur to Prep</title>' + headContent + '</head>');
            printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');

            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 2000);
            return false;
            printWindow.close();

        }
        function term2td() {
            if ($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpEval").val() == "Term1") {
                $(".term2td").addClass('hide');
                $(".thTerm2").addClass('hide');
                $(".colspanTerm2").prop('colspan', "2");
                $(".thTerm1").css('width', '50%');
            }
            if ($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpEval").val() == "Term2") {
                $(".term2td").removeClass('hide');
                $(".thTerm2").removeClass('hide');
                $(".colspanTerm2").prop('colspan', "3");
                $(".thTerm1").css('width', '25%');
            }
        }
    </script>
</asp:Content>

