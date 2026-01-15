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
                                                            <asp:ListItem Value="TERM1">TERM 1</asp:ListItem>
                                                            <asp:ListItem Value="TERM2">TERM 2</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
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
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Activity&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpActivityId" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpActivityId_SelectedIndexChanged">
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
                                                               
                                                                <div class="row">
                                                                    <div class="col-sm-12 text-center">
                                                                         <b>SUBJECT WISE CUMULATIVE</b><br />
                                                                <b>CLASS :</b>&nbsp;
                                                                    <asp:Label ID="lblClass" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;<b>Session </b>:&nbsp;(<asp:Label ID="LblSession" runat="server" Text=""></asp:Label>)
                                                                <br />
                                                                        <b>Subject</b>&nbsp;:&nbsp;
                                                                <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;
                                                                        <b>Paper</b>&nbsp;:&nbsp;
                                                                <asp:Label ID="lblPaper" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;
                                                                        <b>Activity</b>&nbsp;:&nbsp;
                                                                <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-6 text-left">
                                                                        <b>SUBJECT TEACHER NAME :&nbsp;</b>
                                                                    <asp:Label ID="lblSubjectTeacherName" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-6 text-right"><b>DATE :&nbsp;</b>
                                                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></div>
                                                                </div>
                                                            
                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                                                                    class="table mp-table p-table-bordered table-bordered">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label600" runat="server" Visible="false"></asp:Label>
                                                                                <asp:Label ID="Label1500" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="p-pad-n" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" Width="40px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S.R. No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1600" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="p-pad-n" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Student's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label100" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="p-pad-n" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Father's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label800" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="p-pad-n" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label5300" runat="server" Text="EVAL. 1"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMmEvel1_1" runat="server"></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEvel1_1" runat="server" Text='<%# Bind("Evel1") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label5400" runat="server" Text="EVAL. 2"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMmEvel2_1" runat="server"></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTEvel2_1" runat="server" Text='<%# Bind("Evel2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label5500" runat="server" Text="EVAL. 3"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMmEvel3_1" runat="server"></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEvel3_1" runat="server" Text='<%# Bind("Evel3") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="TOTAL OF  BEST TWO">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LabelBest200" runat="server" Text='<%# Bind("Best2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="CONV. INTO 100">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LabelConversion00" runat="server" Text='<%# Bind("Conversion") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="GRADE">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGRADE00" runat="server" Text='<%# Bind("Grade") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label5100" runat="server" Text="EVAL. 4"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMmEvel4_2" runat="server"></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEvel400" runat="server" Text='<%# Bind("Evel1_2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label10500" runat="server" Text="EVAL. 5"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMmEvel5_2" runat="server"></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTEvel500" runat="server" Text='<%# Bind("Evel2_2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label ID="Label5060" runat="server" Text="EVAL. 6"></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMmEvel6_2" runat="server"></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEvel600" runat="server" Text='<%# Bind("Evel3_2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="TOTAL OF  BEST TWO">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LabelBest200" runat="server" Text='<%# Bind("Best2_2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="CONV. INTO 100">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LabelConversion200" runat="server" Text='<%# Bind("Conversion_2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="GRADE">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGRADE200" runat="server" Text='<%# Bind("Grade_2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass=" p-pad-n text-center" />
                                                                            <HeaderStyle CssClass="p-tot-tit p-pad-n" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
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

