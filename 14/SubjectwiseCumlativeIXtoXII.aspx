<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="SubjectwiseCumlativeIXtoXII.aspx.cs" Inherits="SubjectwiseCumlativeIXtoXII" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
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
                                                    <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Select Stream&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpBranch" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpBranch_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpsection" runat="server" CssClass="form-control-blue  validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Select Evaluation&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpEval_SelectedIndexChanged">
                                                            <asp:ListItem Value="TERM1">TERM 1</asp:ListItem>
                                                            <asp:ListItem Value="TERM2">TERM 2</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Select Subject&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Select Paper&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpPaper" runat="server" CssClass="form-control-blue  validatedrp" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpPaper_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15" style="padding-top: 26px;">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                                    <div id="msgbox" runat="server" style="left: 75px;"></div>

                                                </div>
                                            </div>



                                            <div class="col-sm-12  " id="divExport" runat="server" visible="false">
                                                <div class="col-sm-12 no-padding">

                                                    <div style="float: right; font-size: 19px;">

                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                                    title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                                    title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                                    title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                                    title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
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
                                                    <table class="table mp-table p-table-bordered table-bordered"
                                                        style="margin-bottom: 0 !important;">
                                                        <tr>
                                                            <td colspan="4" class="text-center">SUBJECT WISE CUMULATIVE
                                                                <asp:Label ID="LblSession" runat="server" Text=""></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 40%;">SUBJECT TEACHER NAME:&nbsp;
                                                                    <asp:Label ID="lblSubjectTeacherName" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td style="width: 20%;">SUBJECT:&nbsp;
                                                                    <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td style="width: 20%;">CLASS:&nbsp;
                                                                    <asp:Label ID="lblClass" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="lblSection" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td style="width: 20%;">DATE:&nbsp;
                                                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                                                                    class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label6" runat="server" Visible="false"></asp:Label>
                                                                                <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="p-tot-tit p-pad-n" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-35" Width="40px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S.R. No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsrnos" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="p-tot-tit p-pad-n" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-48" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Student's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n " />
                                                                            <HeaderStyle CssClass="p-sub-tit p-pad-n sub-w-175" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Father's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n " />
                                                                            <HeaderStyle CssClass="p-sub-tit p-pad-n sub-w-175" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label5" runat="server" Text="THEORY"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMMTHEORYhy" runat="server" Text=""></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTHEORYhy" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15 tab-in" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label7" runat="server" Text="PROJECT/PRAC."></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMMPRAChy" runat="server" Text=""></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPRAChy" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="TOTAL">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalhy" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
                                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false"
                                                                    class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label6" runat="server" Visible="false"></asp:Label>
                                                                                <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="p-tot-tit p-pad-n" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-35" Width="40px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S.R. No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="p-tot-tit p-pad-n" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-48" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Student's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n " />
                                                                            <HeaderStyle CssClass="p-sub-tit p-pad-n sub-w-175" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Father's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n " />
                                                                            <HeaderStyle CssClass="p-sub-tit p-pad-n sub-w-175" />
                                                                        </asp:TemplateField>

                                                                       <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label5" runat="server" Text="THEORY"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMMTHEORYhy" runat="server" Text=""></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTHEORYhy" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15 tab-in" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label7" runat="server" Text="PROJECT/PRAC."></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMMPRAChy" runat="server" Text=""></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPRAChy" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="TOTAL">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalhy" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                                        </asp:TemplateField>

                                                                        <%--===========================TERM 2 START========================================--%>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label5aa" runat="server" Text="THEORY"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMMTHEORYae" runat="server" Text=""></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTHEORYae" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15 tab-in" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label7aa" runat="server" Text="PROJECT/PRAC."></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMMPRACae" runat="server" Text=""></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPRACae" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="TOTAL">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalae" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="G. TOTAL">
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="lblGTotal" runat="server" Text="G. TOTAL"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblGMM" runat="server" Text="200"></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lnlGTotal" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
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
</asp:Content>

