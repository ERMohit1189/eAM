<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarkEntryReportVItoVIII_1617.aspx.cs" Inherits="staff_MarkEntryReportVItoVIII_1617" %>

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

                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Group&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSubjectGroup" runat="server" CssClass="validatedrp" OnSelectedIndexChanged="drpSubjectGroup_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpSubject_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Evaluation&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                           <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpEval_SelectedIndexChanged">
                                                    <asp:ListItem>FA1</asp:ListItem>
                                                    <asp:ListItem>FA2</asp:ListItem>
                                                    <asp:ListItem>SA1</asp:ListItem>
                                                    <asp:ListItem>FA3</asp:ListItem>
                                                    <asp:ListItem>FA4</asp:ListItem>
                                                    <asp:ListItem>SA2</asp:ListItem>
                                                </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <div id="msgbox" runat="server" style="left: 0;"></div>
                                    </div>
                                </div>

                               <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">
                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
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

                                <div class="col-sm-12 " id="divExport" runat="server">
                                    <div class=" table-responsive  table-responsive2 ">

                                        <table runat="server" id="abc" style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <div id="header" runat="server" class="col-sm-12" style="width: 80%"></div>

                                                    <div class="col-sm-12 text-center">
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small" Text="Subject Wise Cumulative"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-12 text-center">
                                                        <span style="font-weight: bold; font-family: Verdana; font-size: small">Class:</span>
                                                        <asp:Label ID="lblClass" runat="server"></asp:Label>
                                                        |
                                                <span style="font-weight: bold; font-family: Verdana; font-size: small">Section:</span>
                                                        <asp:Label ID="lblSection" runat="server"></asp:Label>
                                                        |
                                                <span style="font-weight: bold; font-family: Verdana; font-size: small">Subject:</span>
                                                        <asp:Label ID="lblSubject" runat="server"></asp:Label>
                                                        |
                                                <span style="font-weight: bold; font-family: Verdana; font-size: small">Evaluation:</span>
                                                        <asp:Label ID="lblEval" runat="server"></asp:Label>
                                                        |
                                            <span style="font-weight: bold; font-family: Verdana; font-size: small">Date:</span>
                                                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-pad-2 ">
                                                    <div id="table3" runat="server" visible="false">


                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" class="table  mp-table p-table-bordered table-bordered">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        <asp:Label ID="Label18" runat="server" Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="p-tot-tit p-pad-n" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label16" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="p-tot-tit p-pad-n" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Student's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="p-sub-tit p-pad-n " />
                                                                    <HeaderStyle CssClass="p-sub-tit p-pad-n sub-w-150" />
                                                                </asp:TemplateField>
                                                                <%--------------------------------------------Pen Paper Test------------------------------------------------------------------------------------------------%>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <table class="table mp-table lbn-rbn-table no-tb no-bm">
                                                                            <tr>
                                                                                <th colspan="4" class=" head-c p-tot-tit p-pad-n bor-t-n">
                                                                                    <p class="txt-tab-p txt-tab-p-p">Pen Paper test</p>
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <th class="bor-r head-c p-tot-tit sub-m-w-48 p-pad-n">T1</th>
                                                                                <th class="bor-r head-c p-tot-tit sub-m-w-48 p-pad-n">T2</th>
                                                                                <th class="bor-r head-c p-tot-tit sub-m-w-48 p-pad-n">T3</th>
                                                                                <th class=" p-tot-tit head-c sub-m-w-48 p-pad-n">T4</th>
                                                                                <th class=" p-tot-tit p-pad-n" style="display: none">Test5</th>
                                                                                <th class=" p-tot-tit p-pad-n" style="display: none">Test6</th>
                                                                            </tr>
                                                                            <tr>
                                                                                <th class="bor-r head-c p-tot-tit sub-m-w-48 p-pad-n">
                                                                                    <asp:Label ID="PPTMM1" runat="server" Text="20"></asp:Label></th>
                                                                                <th class="bor-r head-c p-tot-tit sub-m-w-48 p-pad-n">
                                                                                    <asp:Label ID="PPTMM2" runat="server" Text="20"></asp:Label></th>
                                                                                <th class="bor-r head-c p-tot-tit sub-m-w-48 p-pad-n">
                                                                                    <asp:Label ID="PPTMM3" runat="server" Text="20"></asp:Label></th>
                                                                                <th class=" p-tot-tit head-c sub-m-w-48 p-pad-n">
                                                                                    <asp:Label ID="PPTMM4" runat="server" Text="20"></asp:Label></th>
                                                                                <th class=" p-tot-tit p-pad-n" style="display: none">Test5</th>
                                                                                <th class=" p-tot-tit p-pad-n" style="display: none">Test6</th>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>

                                                                    <ItemTemplate>
                                                                        <table class="table mp-table lbn-rbn-table no-tb table-nb no-bm">
                                                                            <tr>
                                                                                <td class="bor-r text-center sub-m-w-48 p-pad-3">
                                                                                    <asp:Label ID="Label17" runat="server" Text=""></asp:Label></td>
                                                                                <td class="bor-r text-center sub-m-w-48 p-pad-3">
                                                                                    <asp:Label ID="Label30" runat="server" Text=""></asp:Label></td>
                                                                                <td class="bor-r text-center sub-m-w-48 p-pad-3">
                                                                                    <asp:Label ID="Label31" runat="server" Text=""></asp:Label></td>
                                                                                <td class=" text-center sub-m-w-48 p-pad-3">
                                                                                    <asp:Label ID="Label32" runat="server" Text=""></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="p-pad-0 no-tb" />
                                                                    <HeaderStyle CssClass="p-pad-0 no-tb" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="text-rotate-207 txt-p-30">
                                                                            <asp:Label ID="Label26" runat="server" Text="Total"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label27" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="lblMM1" runat="server" Text="" Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-n" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>

                                                                        <p class="txt-tab-p text-rotate-207 txt-p-20">
                                                                            <asp:Label ID="Label28" runat="server" Text="Conv. in 10"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">A </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label29" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-n" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>
                                                                <%--------------------------------------------Pen Paper End------------------------------------------------------------------------------------------------%>
                                                                <%--------------------------------------------Individual Activity Start------------------------------------------------------------------------------------------------%>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-20">
                                                                            <asp:Label ID="Label7" runat="server" Text="C.W+H.W"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="CWMM" runat="server" Text="5"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label33" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-35" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ACT">
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="Label9" runat="server" Text="Lab Man."></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="LabMM" runat="server" Text="5"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label34" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-35" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="Label11" runat="server" Text="Work Book"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="WBMM" runat="server" Text="5"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label35" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-35" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="lblMBMM" runat="server" Text="Map Book"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="MBMM" runat="server" Text="5"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label36" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-35" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="lblWAMM" runat="server" Text="Written Actvity"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="WAMM" runat="server" Text="Conv. in 10"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label37" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-35" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="text-rotate-207 txt-p-30">
                                                                            <asp:Label ID="Label6" runat="server" Text="Total"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="lblMM2" runat="server" Text="" Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-5  sub-m-w-48" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="Label20" runat="server" Text="Conv. in 10 of Ind. Act."></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">B</p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>

                                                                <%--------------------------------------------Individual Activity End---------------------------------------------------------------------------------------------%>

                                                                <%--------------------------------------------Group Activity Start------------------------------------------------------------------------------------------------%>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="lblMMMM" runat="server" Text="Model Making"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="MMMM" runat="server" Text="5"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label38" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="lblDebMM" runat="server" Text="Debate / Quiz"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="DebMM" runat="server" Text="5"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label39" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="lblResMM" runat="server" Text="Reserarch Work"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="ResMM" runat="server" Text="5"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label40" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="lblGroupMM" runat="server" Text="Group Discussion"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="GroupMM" runat="server" Text="5"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label41" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="lblHouseMM" runat="server" Text="House Duty"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="HouseMM" runat="server" Text="5"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label42" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="lblWrittenMM" runat="server" Text="Written Group"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="WrittenMM" runat="server" Text="10"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label43" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="text-rotate-207 txt-p-30">
                                                                            <asp:Label ID="Label22" runat="server" Text="Total"></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label23" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="lblMM3" runat="server" Text="" Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0  sub-m-w-48" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p text-rotate-207 txt-p-13">
                                                                            <asp:Label ID="Label24" runat="server" Text="Conv. in 10 of Grp. Act."></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">C</p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label25" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>

                                                                <%--------------------------------------------Group Activity End------------------------------------------------------------------------------------------------%>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="text-rotate-207 txt-p-30">
                                                                            A + B + C = D
                                                                
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-0  sub-m-w-48" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="text-rotate-207 txt-p-30">
                                                                            Conv. in 10 of D
                                                                        </p>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label44" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="text-center p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit  p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Gr.">
                                                                    <HeaderTemplate>
                                                                        <p class="text-rotate-207 txt-p-30">
                                                                            Grade
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="p-tot-tit p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit  p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-pad-2 ">
                                                    <div id="table4" runat="server" visible="false">


                                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" CssClass="table  mp-table p-table-bordered table-bordered" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        <asp:Label ID="Label18" runat="server" Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="p-tot-tit p-pad-n" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label16" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="p-tot-tit p-pad-n" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Student's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="p-tot-tit p-pad-n" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="txt-tab-p">
                                                                            <asp:Label ID="Label5" runat="server" Text="UT"></asp:Label>
                                                                        </p>
                                                                        <p class="txt-tab-p-tb">
                                                                            <asp:Label ID="SATMM" runat="server" Text=""></asp:Label>
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label45" runat="server" Text="Label"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass=" p-pad-n" />
                                                                    <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-35" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="text-rotate-207 txt-p-30">
                                                                            Conv. in 30
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label46" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="p-tot-tit p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit  p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <p class="text-rotate-207 txt-p-30">
                                                                            Grade
                                                                        </p>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="p-tot-tit p-pad-3" />
                                                                    <HeaderStyle CssClass="p-tot-tit  p-pad-0 sub-m-w-48" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>

                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

