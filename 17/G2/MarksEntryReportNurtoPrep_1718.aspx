<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarksEntryReportNurtoPrep_1718.aspx.cs" Inherits="common_MarksEntryReportNurtoPrep_1718" %>

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

                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Evaluation&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpEval_SelectedIndexChanged">
                                                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                            <asp:ListItem Value="TERM1">TERM 1</asp:ListItem>
                                                            <asp:ListItem Value="TERM2">TERM 2</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label"> Subject&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpSubjectGroup" runat="server" CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="drpSubjectGroup_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpSubject_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                    <label class="control-label">Maximum Marks&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtMax" runat="server" CssClass="form-control-blue" Width="50px" Enabled="false"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <asp:Label ID="Label33" runat="server" class="txt-bold txt-middle-l text-danger" Text="Note:- ML=>Medical Leave,  NAD=>New Admission, NP=>Not Present."></asp:Label>

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
                                                            <td colspan="4" class="text-center">SUBJECT WISE CUMULATIVE <asp:Label ID="LblSession" runat="server" Text=""></asp:Label></td>
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
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowCreated="GridView1_RowCreated"
                                                        class="table mp-table p-table-bordered table-bordered">
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
                                                                    <asp:Label ID="Label16" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
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
                                                                    <asp:Label ID="Label5" runat="server" Text="TEST1"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMT1" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblT1" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15 tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="Label7" runat="server" Text="TEST2"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMT2" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblT2" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField Visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="Label9" runat="server" Text="TEST3"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMT3" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblT3" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center  tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Best One">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Con. in 5">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblNB" runat="server" Text="N.B."></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMNB" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNB" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblSE" runat="server" Text="S.E."></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMSE" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSE" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblHY" runat="server" Text="H.Y."></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMHY" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHY" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblH1Total" runat="server" Text="TOTAL"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblH2Total" runat="server" Text="100"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="GRADE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrade" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="G. TOTAL" Visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblGTotal" runat="server" Text="G. TOTAL"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblGMM" runat="server" Text="200"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnlGTotal" runat="server" Text=""></asp:Label>
                                                                    <asp:Label ID="lblGGrade" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" OnRowCreated="GridView2_RowCreated"
                                                        class="table mp-table p-table-bordered table-bordered">
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
                                                                    <asp:Label ID="Label16" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
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
                                                                    <asp:Label ID="Label5" runat="server" Text="TEST1"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMT1" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblT1" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15 tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="Label7" runat="server" Text="TEST2"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMT2" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblT2" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField Visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="Label9" runat="server" Text="TEST3"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMT3" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblT3" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center  tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Best One">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Con. in 5">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblNB" runat="server" Text="N.B."></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMNB" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNB" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblSE" runat="server" Text="S.E."></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMSE" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSE" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblHY" runat="server" Text="H.Y."></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMHY" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHY" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblH1Total" runat="server" Text="TOTAL"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblH2Total" runat="server" Text="100"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="GRADE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrade" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>
                                                            <%--===========================TERM 2 START========================================--%>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblTEST4" runat="server" Text="TEST4"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMT4" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblT4" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15 tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblTEST5" runat="server" Text="TEST5"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMT5" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblT5" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblTEST6" runat="server" Text="TEST6"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMT6" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblT6" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center  tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Best One">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBestofTwo" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Con. in 5">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConinten" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblNB2" runat="server" Text="N.B."></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMNB2" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNB2" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblSE2" runat="server" Text="S.E."></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMSE2" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSE2" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblHY2" runat="server" Text="A.E."></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblMMHY2" runat="server" Text=""></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHY2" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center tab-in" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n tab-b-15  tab-in" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblH1Total2" runat="server" Text="TOTAL"></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblH2Total2" runat="server" Text="100"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotal2" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass=" p-pad-n text-center" />
                                                                <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="GRADE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrade2" runat="server" Text=""></asp:Label>
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
                                                                    <asp:Label ID="lnlGTotal" runat="server" Text="" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblGGrade" runat="server" Text=""></asp:Label>
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

