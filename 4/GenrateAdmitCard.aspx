<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="GenrateAdmitCard.aspx.cs" Inherits="admin_GenrateAdmitCard" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select S.R. NO.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpsrno" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpsrno_SelectedIndexChanged" CssClass="form-control-blue ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Eval&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpEval_SelectedIndexChanged">
                                                <asp:ListItem>FA1</asp:ListItem>
                                                <asp:ListItem>FA2</asp:ListItem>
                                                <asp:ListItem>SA1</asp:ListItem>
                                                <asp:ListItem>FA3</asp:ListItem>
                                                <asp:ListItem>FA4</asp:ListItem>
                                                <asp:ListItem>SA2</asp:ListItem>
                                                <asp:ListItem>PRE BOARD</asp:ListItem>
                                                <asp:ListItem>BOARD</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-1  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Hide Time&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:CheckBox ID="chkHideTime" runat="server" AutoPostBack="true" OnCheckedChanged="chkHideTime_CheckedChanged" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-1  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Hide Note&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:CheckBox ID="chkHideNote" runat="server" AutoPostBack="true" OnCheckedChanged="chkHideNote_CheckedChanged" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 mgbt-xs-10" runat="server" id="divshow" Visible="False">
                                        <div style="float: right; font-size: 19px;">
                                            <asp:LinkButton ID="lnkWord" runat="server" CssClass="icon-word-color" title="Export to Word" data-toggle="tooltip"
                                                data-placement="Bottom" OnClick="lnkWord_Click"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkExcel" runat="server" CssClass="icon-excel-color" title="Export to Excel" data-toggle="tooltip"
                                                data-placement="Bottom" OnClick="lnkExcel_Click"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click" CssClass="icon-print-color" title="Print"
                                                ><i class="fa fa-print "></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding">
                                    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30">
                                        <div id="divExport" runat="server" class="col-sm-12 no-padding print-row marg-bot-30">
                                            <div class=" table-responsive  table-responsive2">
                                                <asp:Repeater ID="rpStudentDetails" runat="server" OnItemCreated="rpStudentDetails_ItemCreated">
                                                    <ItemTemplate>
                                                        <div class="col-sm-12 print-row fee-d-box-nhl" runat="server">
                                                            <div class="col-sm-12 print-row">
                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 75%">
                                                                            <div id="header" runat="server"></div>
                                                                        </td>
                                                                        <td style="width: 25%; vertical-align: top;">
                                                                            <h3 class="main-name-l text-right" style="margin: 0 15px 0 0; line-height: 21px; text-transform: uppercase;">
                                                                                <asp:Label ID="Label4" runat="server" Text="Admit Card "></asp:Label></h3>
                                                                            <h3 style="font-size: 13px; font-weight: 600; margin: 5px 15px 5px 0" class="sub-adds-l text-right">
                                                                                <asp:Label runat="server" ID="lblEvaluation"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblSession"></asp:Label>
                                                                            </h3>
                                                                        </td>
                                                                    </tr>

                                                                </table>
                                                            </div>

                                                            <div class="col-sm-12 ">
                                                                <hr style="margin: 3px 0; padding: 0;" />
                                                            </div>

                                                            <div class="col-sm-12 ">
                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 90%">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td class="text-left" style="width: 21%;">
                                                                                        <asp:Label ID="Label5" runat="server" Text="S.R. No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left" style="width: 41%;">: &nbsp;
                                                     <asp:Label ID="lblRecieptNo" runat="server" Font-Bold="True" Text='<%# Bind("srno") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left" style="width: 13%;">
                                                                                        <asp:Label ID="Label7" runat="server" Text="D.O.B." Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left" style="width: 25%;">: &nbsp;
                                                     <asp:Label ID="lblRecieptDate" runat="server" Text='<%# Bind("DOB") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="text-left">
                                                                                        <asp:Label ID="Label9" runat="server" Text="Student Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left">: &nbsp;
                                                    <asp:Label ID="lblStudentName" runat="server" Font-Bold="True" Text='<%# Bind("Name") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left">
                                                                                        <asp:Label ID="Label10" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left">: &nbsp;
                                                    <asp:Label ID="lblClass" runat="server" Font-Names="Courier New" Font-Bold="True" Text='<%# Bind("ClassName") %>' Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="text-left">
                                                                                        <asp:Label ID="Label11" runat="server" Text="Father's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left">: &nbsp;
                                                   <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" Text='<%# Bind("FatherName") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left">
                                                                                        <asp:Label ID="Label12" runat="server" Text="Section" Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left">: &nbsp;
                           
                                                     <asp:Label ID="lblSrno" runat="server" Font-Names="Courier New" Text='<%# Bind("SectionName") %>' Font-Bold="True" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="text-left">
                                                                                        <asp:Label ID="Label13" runat="server" Text="Mother's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left">: &nbsp;
                                                 <asp:Label ID="lblContactNo" runat="server" Font-Bold="True" Text='<%# Bind("MotherName") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left">
                                                                                        <asp:Label ID="Label14" runat="server" Text="Roll No." Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="text-left">: &nbsp;
                                               <asp:Label ID="lblMode" runat="server" Font-Names="Courier New" Font-Bold="True" Text='<%# Bind("RollNo") %>' Font-Size="12px"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width: 10%; vertical-align: top;">
                                                                            <div class="admit-cp-box">
                                                                                <asp:Image ID="Image2" runat="server" Width="100px" Height="100px"
                                                                                    ImageUrl='<%# Eval("PhotoPath")+"?Date="+DateTime.Now.ToString(CultureInfo.InvariantCulture) %>' />
                                                                            </div>
                                                                        </td>
                                                                    </tr>

                                                                </table>
                                                            </div>

                                                            <div class="col-sm-12 ">
                                                                <hr style="margin: 3px 0; padding: 0;" />
                                                            </div>

                                                            <div class="col-sm-12 ">
                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                                                                    CssClass="table  p-table-bordered table-hover table-striped table-bordered text-center"
                                                                    Width="100%" Style="font-family: 'Courier New'; font-size: 12px;" OnRowDataBound="GridView1_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Day">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Day") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Subject">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Timings">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>

                                                                </asp:GridView>
                                                            </div>

                                                            <div class="col-sm-12 ">
                                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeader="false" GridLines="None">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label11" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>

                                                            <div class="col-md-12 no-padding" style="font-size: 12px;" id="divnote" runat="server">
                                                                <h4 style="font-weight: bold; font-size: 14px;">Important Note:</h4>
                                                                <ol>
                                                                    <li>Candidate must check all particulars carefully and corrections, if any, be brought to the notice of the school immediately. 
                                                                            Under no</li>
                                                                    <li>Circumstances corrections in particulars will be entertained beyond one year of issue of pass certificate.</li>
                                                                    <li>This card may be issued to the candidate after the attestation of photo and signature of the candidate by the Principal</li>
                                                                    <li>Blind/Dyslexic/Deaf/Spastic/Handicapped candidates are requested to bring the medical certificate/document supporting 
                                                                            their disability/disorder and its extent.</li>
                                                                    <li>This is a provisional admit card. The principal and candidate should sign at the appropriate place.</li>
                                                                </ol>
                                                            </div>

                                                            <table style="width: 100%">
                                                                <td style="width: 33.33%">

                                                                    <p class="text-center" style="margin-top: 80px !important; font-weight: bold">
                                                                        Full Signature of the
                                                                                <br />
                                                                        Candidate
                                                                                <br />
                                                                        (Not in BLOCK LETTERS)
                                                                    </p>

                                                                </td>
                                                                <td style="width: 33.33%">

                                                                    <p class="text-center" style="margin-top: 80px !important; font-weight: bold">
                                                                        Principal
                                                                                <br />
                                                                        (Signature & Stamp of the
                                                                                <br />
                                                                        Principal)
                                                                    </p>

                                                                </td>
                                                                <td>

                                                                    <p class="text-center" style="margin-top: 35px !important; font-weight: bold">
                                                                        <img src="../uploads/cbseexamsign/sing.png" />
                                                                        <br />
                                                                        Controller of Examinations
                                                                                <br />
                                                                        Central Board of Secondary
                                                                                <br />
                                                                        Education
                                                                    </p>

                                                                </td>
                                                            </table>

                                                            <div class="col-md-12 no-padding" id="divnateanddisclamir" runat="server">

                                                                <div class="col-md-12 mgtp-15" style="font-size: 12px;">
                                                                    <h5 style="font-weight: bold; font-size: 14px;">Note:</h5>
                                                                    <ol>
                                                                        <li>For dates and time of examination, please see DATE SHEET also.</li>
                                                                        <li>The candidate must keep this admission card at the time of Examination and present 
                                                                        on demand to the Superintendent of Examination Centre or any other person authorised on this behalf.</li>
                                                                        <li>Candidate will be allowed to appear in the subject/ course filled in by them in their application 
                                                                        forms and given against each name in the list of candidates supplied to thecentre. 
                                                                        A candidate shall not be ordinarily allowed to appear in subject/courses other than those given in the list.</li>
                                                                    </ol>
                                                                </div>

                                                                <div class="col-md-12 mgtp-15" style="font-size: 12px;">
                                                                    <h5 style="font-weight: bold; font-size: 14px;">Disclaimer:</h5>
                                                                    CBSE is not responsible for any inadvertent error that may have crept in the data being published on NET. 
                                                                The data published on net are for immediate information to the examinees
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div id="pagebreak" runat="server"></div>
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
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkWord" />
            <asp:PostBackTrigger ControlID="lnkExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

