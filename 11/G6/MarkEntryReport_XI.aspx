<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="MarkEntryReport_XI.aspx.cs" Inherits="staff_MarkEntryReport_XI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <table class="table">
        <tr>
            <td>Select Class: <span class="imp">*</span>
            </td>
            <td>

                <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="textbox">
                </asp:DropDownList>


            </td>

            <td>Select Branch :
            </td>
            <td>
                <asp:DropDownList ID="drpBranch" runat="server" CssClass="textbox" AutoPostBack="true" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>


        </tr>
        <tr>
            <td>Select Section:  <span class="imp">*</span>
            </td>
            <td>

                <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="textbox">
                </asp:DropDownList>

            </td>
            <td>Subject Group:  <span class="imp">*</span>
            </td>
            <td>
                <asp:DropDownList ID="drpSubjectGroup" runat="server" CssClass="validatedrp" OnSelectedIndexChanged="drpSubjectGroup_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td>Select Subject:      <span class="imp">*</span>
            </td>
            <td>

                <asp:DropDownList ID="drpSubject" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="drpSubject_SelectedIndexChanged">
                </asp:DropDownList>


            </td>
            <td>Select Evaluation:      <span class="imp">*</span>
            </td>
            <td>

                <asp:DropDownList ID="drpEval" runat="server" CssClass="textbox" AutoPostBack="True"
                    OnSelectedIndexChanged="drpEval_SelectedIndexChanged">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>FA1</asp:ListItem>
                    <asp:ListItem>FA2</asp:ListItem>
                    <asp:ListItem>HY</asp:ListItem>
                    <asp:ListItem>P1</asp:ListItem>
                    <asp:ListItem>P2</asp:ListItem>
                    <asp:ListItem>AE</asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>

    </table>
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
    <div id="divExport" runat="server">
        <table id="abc" runat="server" width="100%">
            <tr style="text-align: center;">
                <td style="width: 30px">
                    <asp:Image ID="Image1" runat="server" Height="71px" Width="73px" />
                    <asp:Label ID="lblCollegeName" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr style="text-align: center;">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="RECORD OF ACADEMIC PERFORMANCE"></asp:Label>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Subject Wise Cumulative"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>


                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="Grid table-des"
                        OnRowCreated="GridView1_RowCreated">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%------------------------------------------FA1 Start-------------------------------------------------%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table width="100%" class="table-des" style="border: none; margin: 0">
                                        <tr>
                                            <th colspan="6">UT</th>
                                        </tr>
                                        <tr>
                                            <th>Test1</th>
                                            <th style="display: none">Test2</th>
                                            <th style="display: none">Test3</th>
                                            <th style="display: none">Test4</th>
                                            <th style="display: none">Test5</th>
                                            <th style="display: none">Test6</th>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%" class="table-des" style="border: none; margin: 0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label10" runat="server" Text="Total"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label14" runat="server" Text="ACT. / ASL"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label16" runat="server" Text="C.A"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label18" runat="server" Text="ATT. / ANE"></asp:Label>
                                    <br />
                                    <asp:Label ID="Label19" runat="server" Text="5"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gr. Total">
                                <ItemTemplate>
                                    <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Con. 10%">
                                <ItemTemplate>
                                    <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%------------------------------------------FA1 End-------------------------------------------------%>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label23" runat="server" Text=""></asp:Label>
                                </HeaderTemplate>
                            </asp:TemplateField>

                            <%------------------------------------------FA2 Start-------------------------------------------------%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table width="100%" class="table-des" style="border: none; margin: 0">
                                        <tr>
                                            <th colspan="6">UT</th>
                                        </tr>
                                        <tr>
                                            <th>Test1</th>
                                            <th>Test2</th>
                                            <th>Test3</th>
                                            <th style="display: none">Test4</th>
                                            <th style="display: none">Test5</th>
                                            <th style="display: none">Test6</th>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%" class="table-des" style="border: none; margin: 0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label24" runat="server" Text="Label"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label25" runat="server" Text="Label"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label26" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label27" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label28" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label29" runat="server" Text="Label"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label30" runat="server" Text="Total"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label31" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label34" runat="server" Text="ACT. / ASL"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label35" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label36" runat="server" Text="C.A"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label37" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label38" runat="server" Text="ATT. / ANE"></asp:Label>
                                    <br />
                                    <asp:Label ID="Label39" runat="server" Text="5"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label40" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gr. Total">
                                <ItemTemplate>
                                    <asp:Label ID="Label41" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Con. 10%">
                                <ItemTemplate>
                                    <asp:Label ID="Label42" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%------------------------------------------FA2 End-------------------------------------------------%>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label43" runat="server" Text=""></asp:Label>
                                </HeaderTemplate>
                            </asp:TemplateField>

                            <%------------------------------------------HY Start-------------------------------------------------%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label44" runat="server" Text="T 20%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label45" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label46" runat="server" Text="PRAC"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label47" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label48" runat="server" Text="TH."></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label49" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label50" runat="server" Text="PRAC + Th."></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label51" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label52" runat="server" Text="(P+T) 80%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label53" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label54" runat="server" Text="80% (P+TH) + 20% TEST"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label55" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label56" runat="server" Text="100%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label57" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%------------------------------------------HY End-------------------------------------------------%>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label58" runat="server" Text=""></asp:Label>
                                </HeaderTemplate>
                            </asp:TemplateField>

                            <%------------------------------------------FA3 Start-------------------------------------------------%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table width="100%" class="table-des" style="border: none; margin: 0">
                                        <tr>
                                            <th colspan="6">UT</th>
                                        </tr>
                                        <tr>
                                            <th>Test1</th>
                                            <th>Test2</th>
                                            <th>Test3</th>
                                            <th style="display: none">Test4</th>
                                            <th style="display: none">Test5</th>
                                            <th style="display: none">Test6</th>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%" class="table-des" style="border: none; margin: 0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label59" runat="server" Text="Label"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label60" runat="server" Text="Label"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label61" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label62" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label63" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label64" runat="server" Text="Label"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label65" runat="server" Text="Total"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label66" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label69" runat="server" Text="ACT. / ASL"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label70" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label71" runat="server" Text="C.A"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label72" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label73" runat="server" Text="ATT. / ANE"></asp:Label>
                                    <br />
                                    <asp:Label ID="Label74" runat="server" Text="5"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label75" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gr. Total">
                                <ItemTemplate>
                                    <asp:Label ID="Label76" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Con. 10%">
                                <ItemTemplate>
                                    <asp:Label ID="Label77" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%------------------------------------------FA3 End-------------------------------------------------%>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label78" runat="server" Text=""></asp:Label>
                                </HeaderTemplate>
                            </asp:TemplateField>

                            <%------------------------------------------FA4 Start-------------------------------------------------%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table width="100%" class="table-des" style="border: none; margin: 0">
                                        <tr>
                                            <th colspan="6">UT</th>
                                        </tr>
                                        <tr>
                                            <th>Test1</th>
                                            <th>Test2</th>
                                            <th>Test3</th>
                                            <th style="display: none">Test4</th>
                                            <th style="display: none">Test5</th>
                                            <th style="display: none">Test6</th>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%" class="table-des" style="border: none; margin: 0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label79" runat="server" Text="Label"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label80" runat="server" Text="Label"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label81" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label82" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label83" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label84" runat="server" Text="Label"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label85" runat="server" Text="Total"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label86" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label89" runat="server" Text="ACT. / ASL"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label90" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label91" runat="server" Text="C.A"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label92" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label93" runat="server" Text="ATT. / ANE"></asp:Label>
                                    <br />
                                    <asp:Label ID="Label94" runat="server" Text="5"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label95" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gr. Total">
                                <ItemTemplate>
                                    <asp:Label ID="Label96" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Con. 10%">
                                <ItemTemplate>
                                    <asp:Label ID="Label97" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%------------------------------------------FA4 End-------------------------------------------------%>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label98" runat="server" Text=""></asp:Label>
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <%------------------------------------------AE Start-------------------------------------------------%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label99" runat="server" Text="T 20%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label100" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label101" runat="server" Text="PRAC"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label102" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label103" runat="server" Text="TH."></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label104" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label105" runat="server" Text="PRAC + Th."></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label106" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label107" runat="server" Text="(P+T) 80%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label108" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label109" runat="server" Text="80% (P+TH) + 20% TEST"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label110" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label111" runat="server" Text="100%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label112" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%------------------------------------------AE End-------------------------------------------------%>
                        </Columns>
                    </asp:GridView>


                </td>
            </tr>

            <tr>
                <td>


                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                        OnRowCreated="GridView2_RowCreated" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%------------------------------------------FA Start-------------------------------------------------%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <th colspan="6">UT</th>
                                        </tr>
                                        <tr>
                                            <th>Test1</th>
                                            <th>Test2</th>
                                            <th>Test3</th>
                                            <th style="display: none">Test4</th>
                                            <th style="display: none">Test5</th>
                                            <th style="display: none">Test6</th>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></td>
                                            <td style="display: none">
                                                <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label14" runat="server" Text="ACT./ASL"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label16" runat="server" Text="C.A"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label18" runat="server" Text="ATT./ANE"></asp:Label>
                                    <br />
                                    <asp:Label ID="Label19" runat="server" Text="5"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gr. Total">
                                <ItemTemplate>
                                    <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Con. 10%">
                                <ItemTemplate>
                                    <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%------------------------------------------FA1 End-------------------------------------------------%>
                        </Columns>
                    </asp:GridView>


                </td>
            </tr>

            <tr>
                <td>


                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                        OnRowCreated="GridView3_RowCreated" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%------------------------------------------HY Start-------------------------------------------------%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Text="T 20%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label6" runat="server" Text="PRAC"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label8" runat="server" Text="TH."></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label10" runat="server" Text="PRAC+Th."></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label12" runat="server" Text="(P+T) 80%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label14" runat="server" Text="80% (P+TH) + 20% TEST"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label16" runat="server" Text="100%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%------------------------------------------HY End-------------------------------------------------%>
                        </Columns>
                    </asp:GridView>


                </td>
            </tr>
        </table>
    </div>
</asp:Content>

