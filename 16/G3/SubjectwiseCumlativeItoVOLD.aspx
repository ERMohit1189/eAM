<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="SubjectwiseCumlativeItoVOLD.aspx.cs" Inherits="SubjectwiseCumlativeItoV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" id="div1" runat="server">

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>

                                            <div class="col-sm-12  no-padding ">

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpsection" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpsection_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
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
                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Evaluation&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpEval_SelectedIndexChanged">
                                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                            <asp:ListItem Value="TERM1">TERM 1</asp:ListItem>
                                                            <asp:ListItem Value="TERM2">TERM 2</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control-blue validatedrp"
                                                            AutoPostBack="true" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged"></asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpPaper" runat="server" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Status&nbsp;<span class="vd_red">*</span></label>
                                    <div id="div_Status">
                                       <asp:DropDownList runat="server" ID="drpStatus" class="form-control-blue">
                                             <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="A" Selected="True">Active</asp:ListItem>
                                            <asp:ListItem Value="AB">Active & Blocked</asp:ListItem>
                                            <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                            <asp:ListItem Value="B">Blocked</asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                                </div>
                                                <div class="col-sm-3  half-width-50 mgbt-xs-15" style="display: none">
                                                    <label class="control-label">Maximum Marks&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtMax" runat="server" CssClass="form-control-blue" Width="50px" Enabled="false"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3" runat="server" id="divHideForGardian8">
                                                    <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                                    <div class="">
                                                        <asp:LinkButton runat="server" ID="lnkView" class="button" Text="View" OnClick="lnkView_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"></asp:LinkButton>
                                                        <div class="text-box-msg"></div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <asp:Label ID="Label33" runat="server" class="txt-bold txt-middle-l text-danger" Text="Note:- ML=>Medical Leave,  NAD=>New Admission, AB=>Absent."></asp:Label>

                                            </div>



                                            <div class="col-sm-12  " id="divExport" runat="server" visible="false">
                                                <div class="col-sm-12 no-padding">

                                                    <div style="float: right; font-size: 19px;">

                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                                    title="Export to Word"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                                    title="Export to Excel"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                                    title="Export to PDF"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                                    title="Print"><i class="fa fa-print "></i></asp:LinkButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="ImageButton1" />
                                                                <asp:PostBackTrigger ControlID="ImageButton2" />
                                                                <asp:PostBackTrigger ControlID="ImageButton3" />
                                                                <asp:PostBackTrigger ControlID="ImageButton4" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>


                                                    </div>
                                                </div>
                                                <div class=" table-responsive  table-responsive2 " id="table1" runat="server">
                                                    <%-- <div id="header" runat="server"></div>--%>
                                                    <table class="table mp-table p-table-bordered table-bordered" style="margin-bottom: 0 !important;">
                                                        <tr  style="border:0 !important;">
                                                            <th class="text-center" style="border:0 !important; font-size:16px;">SUBJECT WISE CUMULATIVE
                                                                <asp:Label ID="LblSession" runat="server" Text=""></asp:Label><br />
                                                                 CLASS:&nbsp;<asp:Label ID="lblClass" runat="server" Text=""></asp:Label>
                                                                MEDIUM:&nbsp;
                                                                <asp:Label ID="lblMedium" runat="server" Text=""></asp:Label>
                                                                TERM:&nbsp;<asp:Label ID="lblTerm" runat="server" Text=""></asp:Label><br />
                                                            
                                                                SUBJECT:&nbsp;<asp:Label ID="lblSubject" runat="server" Text=""></asp:Label>
                                                                PAPER:&nbsp;<asp:Label ID="lblPaper" runat="server" Text=""></asp:Label>
                                                                DATE:&nbsp;
                                                                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table id="Term1Mark"  class="Term1Mark table term2 tbl mp-table p-table-bordered table-bordered text_center hide" style="margin-bottom: 3px">
                                                                    <tbody>
                                                                        <tr>
                                                                            <th style='vertical-align: middle; text-align:left;'>S.R. No.</th>
                                                                            <th style='vertical-align: middle; text-align:left;'>STUDENT'S NAME</th>
                                                                            <th style='vertical-align: middle; text-align:center;' colspan="3">UT-1</th>
                                                                            <th style='vertical-align: middle; text-align:center;' colspan="3">UT-2</th>
                                                                            <th style='vertical-align: middle; text-align:center;' colspan="4">Half Yearly Exam</th>
                                                                            <th style='vertical-align: middle; text-align:center;' rowspan="2">TOTAL</th>
                                                                        </tr>
                                                                        <tr>
                                                                             <th style='vertical-align: middle; text-align:center;'></th>
                                                                            <th style='vertical-align: middle; text-align:center;'></th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TH</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>VIVA</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TOTAL</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TH</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>VIVA</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TOTAL</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TH</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>ASSI/ PROJ</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>VIVA/ DICT</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TOTAL</th>
                                                                        </tr>
                                                                        <asp:Repeater runat="server" ID="rptMaxMarkTerm1">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                      <th style='vertical-align: middle; text-align:left;'></th>
                                                                                    <th style='vertical-align: middle; text-align:left;'></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT1TH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT1Viva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT1Total") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT2TH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT2Viva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT2Total") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYTH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYASSI") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYViva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYTotal") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYGTotal") %></th>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <asp:Repeater runat="server" ID="rptMarkTerm1">
                                                                            <ItemTemplate>
                                                                                <tr class="text_center">
                                                                                     <th style='vertical-align: middle; text-align:left;'><%# Eval("SrNo") %></th>
                                                                                    <th style='vertical-align: middle; text-align:left;'><%# Eval("Name") %></th>
                                                                                    <td style='vertical-align: middle; text-align:center;'><%# Eval("UT1TH") %></td>
                                                                                    <td style='vertical-align: middle; text-align:center;'><%# Eval("UT1Viva") %></td>
                                                                                    <td style='vertical-align: middle; text-align:center;'><%# Eval("UT1Total") %></td>
                                                                                    <td style='vertical-align: middle; text-align:center;'><%# Eval("UT2TH") %></td>
                                                                                    <td style='vertical-align: middle; text-align:center;'><%# Eval("UT2Viva") %></td>
                                                                                    <td style='vertical-align: middle; text-align:center;'><%# Eval("UT2Total") %></td>
                                                                                    <td style='vertical-align: middle; text-align:center;'><%# Eval("HYTH") %></td>
                                                                                    <td style='vertical-align: middle; text-align:center;'><%# Eval("HYASSI") %></td>
                                                                                    <td style='vertical-align: middle; text-align:center;'><%# Eval("HYViva") %></td>
                                                                                    <td style='vertical-align: middle; text-align:center;'><%# Eval("HYTotal") %></td>
                                                                                    <td style='vertical-align: middle; text-align:center;'><%# Eval("HYGTotal") %></td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                                <table id="Term2Mark" class="Term2Mark table term2 tbl mp-table p-table-bordered table-bordered text_center hide" style="margin-bottom: 3px">
                                                                    <tbody>
                                                                        <tr>
                                                                             <th style='vertical-align: middle; text-align:left;'>S.R. No.</th>
                                                                            <th style='vertical-align: middle; text-align:left;'>STUDENT'S NAME</th>
                                                                            <th style='vertical-align: middle; text-align:center;' colspan="3">UT-1</th>
                                                                            <th style='vertical-align: middle; text-align:center;' colspan="3">UT-2</th>
                                                                            <th style='vertical-align: middle; text-align:center;' colspan="4">Half Yearly Exam</th>
                                                                            <th style='vertical-align: middle; text-align:center;' rowspan="2">TOTAL</th>
                                                                            <th style='vertical-align: middle; text-align:center;' colspan="3">UT-3</th>
                                                                            <th style='vertical-align: middle; text-align:center;' colspan="3">UT-4</th>
                                                                            <th style='vertical-align: middle; text-align:center;' colspan="4">Annual Exam</th>
                                                                            <th style='vertical-align: middle; text-align:center;' rowspan="2">TOTAL</th>
                                                                            <th style='vertical-align: middle; text-align:center;' rowspan="2">GRAND TOTAL</th>
                                                                        </tr>
                                                                        <tr>
                                                                              <th style='vertical-align: middle; text-align:center;'></th>
                                                                            <th style='vertical-align: middle; text-align:center;'></th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TH</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>VIVA</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TOTAL</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TH</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>VIVA</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TOTAL</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TH</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>ASSI/ PROJ</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>VIVA/ DICT</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TOTAL</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TH</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>VIVA</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TOTAL</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TH</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>VIVA</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TOTAL</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TH</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>ASSI/ PROJ</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>VIVA/ DICT</th>
                                                                            <th style='vertical-align: middle; text-align:center;'>TOTAL</th>
                                                                        </tr>
                                                                        <asp:Repeater runat="server" ID="rptMaxMarkTerm2">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                      <th style='vertical-align: middle; text-align:left;'></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT1TH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT1Viva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT1Total") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT2TH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT2Viva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT2Total") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYTH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYASSI") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYViva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYTotal") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYGTotal") %></th>

                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT3TH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT3Viva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT3Total") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT4TH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT4Viva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT4Total") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("AETH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("AEASSI") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("AEViva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("AETotal") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("AEGTotal") %></th>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <asp:Repeater runat="server" ID="rptMarkTerm2">
                                                                            <ItemTemplate>
                                                                                <tr class="text_center">
                                                                                    <th style='vertical-align: middle; text-align:left;'><%# Eval("SrNo") %></th>
                                                                                    <th style='vertical-align: middle; text-align:left;'><%# Eval("Name") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT1TH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT1Viva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT1Total") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT2TH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT2Viva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT2Total") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYTH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYASSI") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYViva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYTotal") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("HYGTotal") %></th>

                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT3TH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT3Viva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT3Total") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT4TH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT4Viva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("UT4Total") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("AETH") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("AEASSI") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("AEViva") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("AETotal") %></th>
                                                                                    <th style='vertical-align: middle; text-align:center;'><%# Eval("AEGTotal") %></th>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../../js/jquery.min.js"></script>
    <script>
        
        function Showhide(evel) {

            if (evel == "1") {
                $("#Term1Mark").removeClass('hide');
                $("#Term2Mark").addClass('hide');
            }
            if (evel == "2") {
                $("#Term1Mark").addClass('hide');
                $("#Term2Mark").removeClass('hide');
            }
        }
    </script>
</asp:Content>

