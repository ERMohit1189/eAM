<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarkEntryReport_VItoVIII.aspx.cs" Inherits="staff_MarkEntryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12 no-padding ">
                            <div class="col-sm-6  no-padding mgbt-xs-15">
                                <div class="form-group ">
                                    <asp:Label ID="Label85" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Select Class *"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6  no-padding mgbt-xs-15">
                                <div class="form-group ">
                                    <asp:Label ID="Label5" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Select Section *"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 no-padding ">
                            <div class="col-sm-6  no-padding mgbt-xs-15">
                                <div class="form-group ">
                                    <asp:Label ID="Label28" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Select Subject *"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpSubject_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6  no-padding mgbt-xs-15">
                                <div class="form-group ">
                                    <asp:Label ID="Label29" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Select Evaluation *"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpEval_SelectedIndexChanged">
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>FA1</asp:ListItem>
                                            <asp:ListItem>FA2</asp:ListItem>
                                            <asp:ListItem>SA1</asp:ListItem>
                                            <asp:ListItem>FA3</asp:ListItem>
                                            <asp:ListItem>FA4</asp:ListItem>
                                            <asp:ListItem>SA2</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 no-padding">
                            <div style="float: right">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/word.png" OnClick="ImageButton1_Click"
                                        title="Export to Word" Style="height: 16px" />
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/excel.png" OnClick="ImageButton2_Click"
                                        title="Export to Excel" />
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/pdf.png" OnClick="ImageButton3_Click"
                                        Style="width: 16px" title="Export to PDF" />
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/print.png" OnClick="ImageButton4_Click" title="Print"
                                        Style="height: 16px;" />
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="col-sm-12 no-padding panel" id="divExport" runat="server">
                            <div class=" table-responsive  table-responsive2 ">

                                <table runat="server" id="abc" class="table no-p-b-table">
                                    <tr>
                                        <td class="p-pad-2 text-center p-h-titel-box">
                                            <asp:Image ID="Image1" runat="server" Height="71px" Width="71px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="p-pad-1 text-center p-h-titel-box">
                                            <asp:Label ID="lblCollegeName" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="p-pad-1 text-center p-h-titel-box">
                                            <asp:Label ID="Label1" runat="server" Text="RECORD OF ACADEMIC PERFORMANCE"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="p-pad-2 text-center p-h-titel-box">(<asp:Label ID="lblSessionName" runat="server" Text="Label"></asp:Label>
                                            ) &nbsp;

                                            <asp:Label ID="lblEval" runat="server" Text="Label"></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="Student Wise Cumulative"></asp:Label>
                                            <asp:Label ID="Label84" runat="server" Text=""></asp:Label>
                                            &nbsp; &nbsp;
                                            <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="p-pad-2 ">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table mp-table no-tb no-bm  p-table-bordered2 table-bordered"
                                                OnRowCreated="GridView1_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField ControlStyle-CssClass="tab-titel15">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-tot-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ControlStyle-CssClass="tab-titel15">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-tot-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ControlStyle-CssClass="tab-titel15">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-sub-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-sub-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <%------------------------------------------FA1 Start-------------------------------------------------%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text="UT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label5" runat="server" Text="20"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label8" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ACT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label10" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label11" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label13" runat="server" Text="H.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label3" runat="server" Text="C.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label14" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ATT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label16" runat="server" Text="ATT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label17" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-48" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------FA1 End-------------------------------------------------%>

                                                    <asp:TemplateField Visible="false">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>




                                                    <%------------------------------------------FA2 Start-------------------------------------------------%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <table class="table mp-table lbn-rbn-table no-tb no-bm">
                                                                <tr>
                                                                    <td colspan="3" class="p-tot-tit p-pad-n bor-t-n">UT</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="bor-r p-tot-tit sub-m-w-48 p-pad-n">T1</td>
                                                                    <td class="bor-r p-tot-tit sub-m-w-48 p-pad-n">T2</td>
                                                                    <td class=" p-tot-tit sub-m-w-48 p-pad-n">T3</td>
                                                                    <td class=" p-tot-tit p-pad-n" id="Th1" runat="server" visible="false">Test4</td>
                                                                    <td class=" p-tot-tit p-pad-n" id="Th2" runat="server" visible="false">Test5</td>
                                                                    <td class=" p-tot-tit p-pad-n" style="display: none">Test6</td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table class="table mp-table lbn-rbn-table no-tb no-bm  ">
                                                                <tr>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label23" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label24" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label25" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" style="display: none">
                                                                        <asp:Label ID="Label87" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" id="Td1" runat="server" visible="false">
                                                                        <asp:Label ID="Label88" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" id="Td2" runat="server" visible="false">
                                                                        <asp:Label ID="Label89" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-0 no-tb" />
                                                        <HeaderStyle CssClass="p-pad-0 no-tb" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label26" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label27" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label28" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ACT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label29" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label30" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label31" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label32" runat="server" Text="H.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label84" runat="server" Text="C.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label33" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label34" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ATT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label35" runat="server" Text="ATT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label36" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label37" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label38" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-48" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-48" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label39" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label40" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------FA2 End-------------------------------------------------%>

                                                    <asp:TemplateField Visible="false">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>

                                                    <%------------------------------------------SA1 Start-------------------------------------------------%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label41" runat="server" Text="UT"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label99" runat="server" Text="Label"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label42" runat="server" Text="30%"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label100" runat="server" Text="Label"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label43" runat="server" Text="Gr."></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label101" runat="server" Text="Label"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------SA1 End-------------------------------------------------%>

                                                    <%------------------------------------------FA1+FA2+SA1 Start-------------------------------------------------%>
                                                    <asp:TemplateField HeaderText="Per.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFASA1Per" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFASA1" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------FA1+FA2+SA1 End-------------------------------------------------%>

                                                    <%------------------------------------------FA3 Start-------------------------------------------------%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <%-- <table width="100%" class="table-des" style="border: none; margin: 0px">
                                                                <tr>
                                                                    <th colspan="6">UT</th>
                                                                </tr>
                                                                <tr>
                                                                    <th>Test1</th>
                                                                    <th>Test2</th>
                                                                    <th>Test3</th>
                                                                    <th>Test4</th>
                                                                    <th id="Th3" runat="server" visible="false">Test5</th>
                                                                    <th id="Th4" runat="server" visible="false">Test6</th>
                                                                </tr>
                                                            </table>--%>
                                                            <table class="table mp-table lbn-rbn-table no-tb no-bm">
                                                                <tr>
                                                                    <td colspan="3" class="p-tot-tit p-pad-n bor-t-n">UT</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="bor-r p-tot-tit sub-m-w-48 p-pad-n">T1</td>
                                                                    <td class="bor-r p-tot-tit sub-m-w-48 p-pad-n">T2</td>
                                                                    <td class=" p-tot-tit sub-m-w-48 p-pad-n">T3</td>
                                                                    <td class=" p-tot-tit p-pad-n" id="Th3" runat="server" visible="false">Test4</td>
                                                                    <td class=" p-tot-tit p-pad-n" id="Th4" runat="server" visible="false">Test5</td>
                                                                    <td class=" p-tot-tit p-pad-n" style="display: none">Test6</td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table class="table mp-table lbn-rbn-table no-tb no-bm  ">
                                                                <tr>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label44" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label45" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label46" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" runat="server" visible="false">
                                                                        <asp:Label ID="Label92" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" id="Td3" runat="server" visible="false">
                                                                        <asp:Label ID="Label91" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" id="Td4" runat="server" visible="false">
                                                                        <asp:Label ID="Label90" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-0 no-tb" />
                                                        <HeaderStyle CssClass="p-pad-0 no-tb" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label47" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label48" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label49" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ACT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label50" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label51" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label52" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label53" runat="server" Text="H.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label85" runat="server" Text="C.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label54" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label55" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ATT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label56" runat="server" Text="ATT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label57" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label58" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label59" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-48" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-48" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label60" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label61" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------FA3 End-------------------------------------------------%>



                                                    <asp:TemplateField Visible="false">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label62" runat="server" Text=""></asp:Label>
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>




                                                    <%------------------------------------------FA4 Start-------------------------------------------------%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <%--<table width="100%" class="table-des" style="border: none; margin: 0px">
                                                                <tr>
                                                                    <th colspan="6">UT</th>
                                                                </tr>
                                                                <tr>
                                                                    <th>Test1</th>
                                                                    <th>Test2</th>
                                                                    <th>Test3</th>
                                                                    <th>Test4</th>
                                                                    <th id="Th5" runat="server" visible="false">Test5</th>
                                                                    <th id="Th6" runat="server" visible="false">Test6</th>
                                                                </tr>
                                                            </table>--%>
                                                            <table class="table mp-table lbn-rbn-table no-tb no-bm">
                                                                <tr>
                                                                    <td colspan="3" class="p-tot-tit p-pad-n bor-t-n">UT</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="bor-r p-tot-tit sub-m-w-48 p-pad-n">T1</td>
                                                                    <td class="bor-r p-tot-tit sub-m-w-48 p-pad-n">T2</td>
                                                                    <td class=" p-tot-tit sub-m-w-48 p-pad-n">T3</td>
                                                                    <td class=" p-tot-tit p-pad-n" style="display: none">Test4</td>
                                                                    <td id="Th5" runat="server" class=" p-tot-tit p-pad-n" style="display: none">Test5</td>
                                                                    <td id="Th6" runat="server" class=" p-tot-tit p-pad-n" style="display: none">Test6</td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table class="table mp-table lbn-rbn-table no-tb no-bm  ">
                                                                <tr>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label63" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label64" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label65" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" runat="server" visible="false">
                                                                        <asp:Label ID="Label93" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" id="Td5" runat="server" visible="false">
                                                                        <asp:Label ID="Label94" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" id="Td6" runat="server" visible="false">
                                                                        <asp:Label ID="Label95" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label66" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label67" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label68" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ACT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label69" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label70" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label71" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label72" runat="server" Text="H.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label86" runat="server" Text="C.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label73" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label74" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ATT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label75" runat="server" Text="ATT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label76" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label77" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label78" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-48" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-48" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label79" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label80" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------FA4 End-------------------------------------------------%>

                                                    <asp:TemplateField Visible="false">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                    <%------------------------------------------SA2 Start-------------------------------------------------%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label81" runat="server" Text=""></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label102" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label82" runat="server" Text=""></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label103" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label83" runat="server" Text=""></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label104" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------SA2 End-------------------------------------------------%>
                                                    <%------------------------------------------FA2+FA3+SA2 Start-------------------------------------------------%>
                                                    <asp:TemplateField HeaderText="Per.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFASA2Per" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFASA2" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------FA2+FA3+SA2 End-------------------------------------------------%>
                                                    <%------------------------------------------FA1+FA2+FA3+FA4 Start-------------------------------------------------%>
                                                    <asp:TemplateField HeaderText="Per.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalFaPer" runat="server" Text="Label"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalFa" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------FA1+FA2+FA3+FA4 End-------------------------------------------------%>

                                                    <%------------------------------------------SA1+SA2 Start-------------------------------------------------%>
                                                    <asp:TemplateField HeaderText="Per.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrandTotalPer" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrandTotal" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------SA1+SA2 End-------------------------------------------------%>


                                                    <%------------------------------------------FA+SA Start-------------------------------------------------%>
                                                    <asp:TemplateField HeaderText="Per.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalFASAPer" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalFASA" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n sub-m-w-45" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n sub-m-w-45" />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------FA+SA End-------------------------------------------------%>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                       <td class="p-pad-2 ">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="table mp-table no-tb no-bm  p-table-bordered2 table-bordered"
                                                OnRowCreated="GridView2_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-tot-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-tot-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-tot-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <%------------------------------------------FA1 Start-------------------------------------------------%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text="UT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label5" runat="server" Text="20"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label8" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ACT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label10" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label11" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label13" runat="server" Text="H.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label105" runat="server" Text="C.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label14" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ATT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label16" runat="server" Text="ATT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label17" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------FA1 End-------------------------------------------------%>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>

                                    <tr>
                                       <td class="p-pad-2 ">
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" CssClass="table mp-table no-tb no-bm  p-table-bordered2 table-bordered"
                                                OnRowCreated="GridView3_RowCreated" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-tot-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-tot-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-tot-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <%------------------------------------------FA Start-------------------------------------------------%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                           
                                                            <table class="table mp-table lbn-rbn-table no-tb no-bm">
                                                                <tr>
                                                                    <td colspan="3" class="p-tot-tit p-pad-n bor-t-n">UT</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="bor-r p-tot-tit sub-m-w-48 p-pad-n">T1</td>
                                                                    <td class="bor-r p-tot-tit sub-m-w-48 p-pad-n">T2</td>
                                                                    <td class=" p-tot-tit sub-m-w-48 p-pad-n">T3</td>
                                                                    <td class=" p-tot-tit p-pad-n" style="display: none">Test4</td>
                                                                    <td class=" p-tot-tit p-pad-n" id="Th7" runat="server" visible="false">Test5</td>
                                                                    <td class=" p-tot-tit p-pad-n" id="Th8" runat="server" visible="false">Test6</td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table class="table mp-table lbn-rbn-table no-tb no-bm  ">
                                                                <tr>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48">
                                                                        <asp:Label ID="Label6" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" runat="server" visible="false">
                                                                        <asp:Label ID="Label96" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" id="Td7" runat="server" visible="false">
                                                                        <asp:Label ID="Label97" runat="server" Text=""></asp:Label></td>
                                                                    <td class=" bor-t-n text-center p-pad-n sub-m-w-48" id="Td8" runat="server" visible="false">
                                                                        <asp:Label ID="Label98" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-0 no-tb" />
                                                        <HeaderStyle CssClass="p-pad-0 no-tb" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label8" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ACT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label10" runat="server" Text="ACT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label11" runat="server" Text="15"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                       <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label13" runat="server" Text="H.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label106" runat="server" Text="C.W"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label14" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                       <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ATT">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label16" runat="server" Text="ATT"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Label17" runat="server" Text="5"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                       <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                       <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------FA1 End-------------------------------------------------%>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="p-pad-2 ">
                                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" CssClass="table mp-table no-tb no-bm  p-table-bordered2 table-bordered"
                                                OnRowCreated="GridView4_RowCreated" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                         <ItemStyle CssClass="p-tot-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                         <ItemStyle CssClass="p-tot-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                         <ItemStyle CssClass="p-tot-tit p-pad-n " />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <%------------------------------------------SA Start-------------------------------------------------%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text="UT"></asp:Label>
                                                            <%--    <br />
                                                                <asp:Label ID="Label5" runat="server" Text="20"></asp:Label>--%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                         <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="30%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                         <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                         <ItemStyle CssClass="p-pad-n " HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="p-tot-tit p-pad-n " />
                                                    </asp:TemplateField>
                                                    <%------------------------------------------SA End-------------------------------------------------%>
                                                </Columns>
                                            </asp:GridView>
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


</asp:Content>

