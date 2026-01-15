<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="ParticularStudentMarksNUR1toPrep.aspx.cs" Inherits="admin_ParticularStudentMarksItoV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <%-- <div class="col-sm-12 no-padding ">
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="pull-right">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/export_word_icon.gif" OnClick="ImageButton1_Click"
                                        title="Export to Word" Style="height: 16px" />
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/export_excel_icon.gif" OnClick="ImageButton2_Click"
                                        title="Export to Excel" />
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/print_icon.gif" OnClick="ImageButton4_Click" title="Print"
                                        Style="height: 16px;" />
                                </div>
                            </asp:Panel>
                        </div>--%>

                        <div class="col-sm-12 mgbt-xs-10" id="divPrint" runat="server">
                            <asp:Panel ID="Panel2" runat="server">
                                <div style="float: right; font-size: 19px;">
                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                    <%-- <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>--%>
                                    <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="col-sm-12 no-padding panel" id="divExport" runat="server">
                            <div class=" table-responsive  table-responsive2 ">
                                <table runat="server" id="abc" class="table no-p-b-table">
                                    <tr>
                                        <td colspan="2" class="p-pad-2 text-center p-h-titel-box">
                                            <div id="header1" runat="server" style="width: 80%">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="p-pad-1 text-center p-h-titel-box">
                                            <asp:Label ID="Label1" runat="server" Text="STUDENT CUMULATIVE OF ACADEMIC PERFORMANCE"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="p-pad-2 text-center p-h-titel-box">
                                            <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                <ItemTemplate>
                                                    <table id="Table1" class="table no-bm mp-table p-table-bordered table-bordered" runat="server">
                                                        <tr>
                                                            <td class="p-pad-n p-sub-tit tab-b-5">Student Name </td>
                                                            <td class="p-pad-n text-left tab-b-15">
                                                                <asp:Label ID="Label88" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                            </td>

                                                            <td class="p-pad-n p-sub-tit tab-b-5">Class </td>
                                                            <td class="p-pad-n text-left tab-b-15">
                                                                <asp:Label ID="Label89" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                            </td>
                                                            <td class="p-pad-n p-sub-tit tab-b-5">Father Name </td>
                                                            <td class="p-pad-n text-left tab-b-15">
                                                                <asp:Label ID="Label90" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                            </td>
                                                            <td class="p-pad-n p-sub-tit tab-b-5">D.O.B. </td>
                                                            <td class="p-pad-n text-left tab-b-15">
                                                                <asp:Label ID="Label98" runat="server" Text='<%# Bind("D") %>'></asp:Label>
                                                            </td>
                                                            <td class="p-pad-n p-sub-tit tab-b-5">Address </td>
                                                            <td colspan="3" class="p-pad-n text-left tab-b-15">
                                                                <asp:Label ID="Label91" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td class="p-pad-n p-sub-tit tab-b-5">Height </td>
                                                            <td class="p-pad-n text-left tab-b-15">
                                                                <asp:Label ID="Label92" runat="server" Text='<%# Bind("H") %>'></asp:Label>
                                                            </td>
                                                            <td class="p-pad-n p-sub-tit tab-b-5">Weight </td>
                                                            <td class="p-pad-n text-left tab-b-15">
                                                                <asp:Label ID="Label93" runat="server" Text='<%# Bind("W") %>'></asp:Label>
                                                            </td>
                                                            <td class="p-pad-n p-sub-tit tab-b-5">Blood Group </td>
                                                            <td class="p-pad-n text-left tab-b-15">
                                                                <asp:Label ID="Label94" runat="server" Text='<%# Bind("BG") %>'></asp:Label>
                                                            </td>
                                                            <td class="p-pad-n p-sub-tit tab-b-5">Teeth </td>
                                                            <td class="p-pad-n text-left tab-b-15">
                                                                <asp:Label ID="Label97" runat="server" Text='<%# Bind("T") %>'></asp:Label>
                                                            </td>
                                                            <td class="p-pad-n p-sub-tit tab-b-5">Vission (L) </td>
                                                            <td class="p-pad-n text-left tab-b-5">
                                                                <asp:Label ID="Label95" runat="server" Text='<%# Bind("L") %>'></asp:Label>
                                                            </td>
                                                            <td class="p-pad-n p-sub-tit tab-b-5">Vission (R) </td>
                                                            <td class="p-pad-n text-left tab-b-5">
                                                                <asp:Label ID="Label96" runat="server" Text='<%# Bind("R") %>'></asp:Label>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="p-pad-2">
                                            <table class="table mp-table no-bm p-table-bordered  table-bordered" style="border-collapse: collapse; width: 1135px;">
                                                <thead>
                                                    <tr>
                                                        <th colspan="21" class="p-pad-n text-center">ACADEMIC PERFORMANCE/SCHOLASTIC AREAS
                                                        </th>
                                                    </tr>
                                                    <tr class="text-center">
                                                        <td colspan="5" class="p-pad-n p-tot-tit"></td>
                                                        <td colspan="2" class="p-pad-n p-tot-tit">MAY/JULY</td>
                                                        <td colspan="2" class="p-pad-n p-tot-tit">AUG</td>
                                                        <td colspan="2" class="p-pad-n p-tot-tit">SEPT.</td>
                                                        <td colspan="2" class="p-pad-n p-tot-tit">AVERAGE</td>
                                                        <td colspan="2" class="p-pad-n p-tot-tit">DEC</td>
                                                        <td colspan="2" class="p-pad-n p-tot-tit">JAN</td>
                                                        <td colspan="2" class="p-pad-n p-tot-tit">FEB</td>
                                                        <td colspan="2" class="p-pad-n p-tot-tit">AVERAGE</td>
                                                    </tr>
                                                </thead>
                                                <tbody>


                                                    <tr class="">
                                                        <td colspan="4" class="p-pad-n p-sub-tit sub-w-250">Subject</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Marks</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Marks</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Marks</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Marks</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Marks</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Marks</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Marks</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Marks</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Marks</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="21" class="p-pad-0 no-tb">
                                                            <asp:GridView ID="GridView2" runat="server" class="table mp-table no-tb no-bm  table-bordered" AutoGenerateColumns="false" ShowFooter="true" ShowHeader="false" Style="border-collapse: collapse;">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label29" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="Label30" runat="server" Text='<%# Bind("SubjectGroup") %>' Font-Bold="true" CssClass="p-pl-tab-titel1"></asp:Label>
                                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table mp-table lbn-rbn-table no-tb no-bm mgtp-5 p-table-bordered table-bordered" ShowHeader="false" ShowFooter="true" Style="border-collapse: collapse;">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("SubjectName") %>'></asp:Label>
                                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("SubjectId") %>' Visible="false"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="p-sub-tit p-pad-n sub-w-250" />
                                                                                        <FooterStyle CssClass="p-sub-tit p-pad-n sub-w-250" />
                                                                                    </asp:TemplateField>
                                                                                    <%------------------------------------------------First Marks----------------------------------------------------%>
                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:Label ID="Label31" runat="server" CssClass="p-sub-tit" Font-Bold="true" Text="MARKS"></asp:Label>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label32" runat="server" Text='<%# Bind("MaxMarks") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label33" runat="server" Text=""></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <%----------------------------------------------------------------------------------------------------%>

                                                                                    <%------------------------------------------MAY/JULY Start-------------------------------------------------%>
                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:Label ID="Label3" runat="server" Text="MARKS"></asp:Label>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label34" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="GRADE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label35" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <%------------------------------------------MAY/JULY End-------------------------------------------------%>



                                                                                    <%------------------------------------------AUG Start-------------------------------------------------%>
                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:Label ID="Label7" runat="server" Text="MARKS"></asp:Label>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label36" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="GRADE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label37" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <%------------------------------------------AUG End-------------------------------------------------%>


                                                                                    <%------------------------------------------SEPT. Start-------------------------------------------------%>
                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:Label ID="Label11" runat="server" Text="MARKS"></asp:Label>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label38" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="GRADE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label39" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <%------------------------------------------SEPT. End-------------------------------------------------%>


                                                                                    <%------------------------------------------AVERAGE Start-------------------------------------------------%>
                                                                                    <asp:TemplateField HeaderText="MARKS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label50" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label40" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="GRADE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label51" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label41" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <%------------------------------------------AVERAGE End-------------------------------------------------%>

                                                                                    <%------------------------------------------DEC Start-------------------------------------------------%>
                                                                                    <asp:TemplateField HeaderText="MARKS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label42" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="GRADE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label43" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <%------------------------------------------DEC End-------------------------------------------------%>


                                                                                    <%------------------------------------------JAN Start-------------------------------------------------%>
                                                                                    <asp:TemplateField HeaderText="MARKS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label44" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="GRADE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label45" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <%------------------------------------------JAN End-------------------------------------------------%>


                                                                                    <%------------------------------------------FEB End-------------------------------------------------%>
                                                                                    <asp:TemplateField HeaderText="MARKS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label24" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label46" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="GRADE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label25" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label47" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <%------------------------------------------FEB End-------------------------------------------------%>


                                                                                    <%------------------------------------------AVERAGE Start-------------------------------------------------%>
                                                                                    <asp:TemplateField HeaderText="MARKS">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label27" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label48" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="GRADE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label28" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="Label49" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <ItemStyle CssClass="p-pad-n sub-m-w-52" HorizontalAlign="Center" />
                                                                                        <FooterStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <%------------------------------------------AVERAGE End-------------------------------------------------%>
                                                                                    <%-----------------------------------------------------------------------------------------------------%>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>

                                                                            <table class="table mp-table lbn-rbn-table tb-bn-table no-tb no-bm  p-table-bordered table-bordered">
                                                                                <tr>
                                                                                    <td class="p-sub-tit p-pad-n sub-w-250">Grand Total
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label26" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label52" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label53" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label54" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label55" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label56" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label57" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label58" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label59" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label61" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label62" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label63" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label64" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label65" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label66" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label67" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td class="p-tot-tit p-pad-n sub-m-w-52">
                                                                                        <asp:Label ID="Label68" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>

                                                                        </FooterTemplate>
                                                                        <ItemStyle CssClass="p-pad-t-0 no-tb" />
                                                                        <FooterStyle CssClass="p-pad-0 no-tb" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </tbody>

                                            </table>
                                        </td>

                                        <td class="p-pad-2 tab-top">
                                            <table class="table mp-table no-bm p-table-bordered  table-bordered" style="border-collapse: collapse; width: 715px;">
                                                <thead>
                                                    <tr>
                                                        <th colspan="12" class="p-pad-n text-center">PERSONALITY PROFILE
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" class="p-pad-n p-tot-tit"></td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-70">MAY/JULY</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">AUG</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">SEPT.</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-70">AVERAGE</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">DEC</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">JAN</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">FEB</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-70">AVERAGE</td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td colspan="4" class="p-pad-n p-sub-tit sub-w-175"></td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-70">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-70">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-52">Grade</td>
                                                        <td class="p-pad-n p-tot-tit sub-m-w-70">Grade</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="12" class="p-pad-0 no-tb">
                                                            <asp:GridView ID="GridView3" runat="server" class="table mp-table no-tb no-bm  table-bordered" ShowHeader="false" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="Label10" runat="server" Font-Bold="true" CssClass="p-pl-tab-titel1" Text='<%# Bind("CoscholasticGroup") %>'></asp:Label>
                                                                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" class="table mp-table lbn-rbn-table no-tb no-bm mgtp-5 p-table-bordered table-bordered" ShowHeader="false">
                                                                                <Columns>

                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label14" runat="server" Text='<%# Bind("CoscholasticName") %>'></asp:Label>
                                                                                            <asp:Label ID="Label23" runat="server" Text='<%# Bind("CoscholasticId") %>' Visible="false"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="p-sub-tit p-pad-n sub-w-250" />
                                                                                        <FooterStyle CssClass="p-sub-tit p-pad-n sub-w-250" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />

                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label16" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="p-tot-tit p-pad-n sub-m-w-52" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="p-tot-tit p-pad-n sub-m-w-70" />
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="p-pad-t-0 no-tb" />
                                                                        <FooterStyle CssClass="p-pad-0 no-tb" />
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <table class="table mp-table no-bm p-table-bordered  table-bordered mgtp-5">
                                                <tr>
                                                    <td class="p-pad-n p-tot-tit" style="width: 34.4%"></td>
                                                    <td class="p-pad-n p-tot-tit" style="width: 34%">HY</td>
                                                    <td class="p-pad-n p-tot-tit">AE</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <h5 class="txt-rep-title-11-b">Attendance</h5>
                                                    </td>
                                                    <td class="text-center">
                                                        <h5 class="txt-rep-title-11">
                                                            <asp:Label ID="lbla1" runat="server" Text=""></asp:Label></h5>
                                                    </td>
                                                    <td class="text-center">
                                                        <h5 class="txt-rep-title-11">
                                                            <asp:Label ID="lbla2" runat="server" Text=""></asp:Label></h5>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <h5 class="txt-rep-title-11-b">General Remark</h5>
                                                    </td>
                                                    <td class="text-center  ">
                                                        <h5 class="txt-rep-title-11"><asp:Label ID="lblHY" runat="server" Text=""></asp:Label></h5>
                                                    </td>
                                                    <td class="text-center  ">
                                                        <h5 class="txt-rep-title-11"><asp:Label ID="lblAE" runat="server" Text=""></asp:Label></h5>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table class="table mp-table no-bm p-table-bordered  table-bordered mgtp-5" style="border-collapse: collapse; width: 715px;">
                                                <thead>
                                                    <tr>
                                                        <th colspan="3">Grading System</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>A+</td>
                                                        <td>Outstanding</td>
                                                        <td>90% to 100%</td>
                                                    </tr>
                                                    <tr>
                                                        <td>A</td>
                                                        <td>Excellent</td>
                                                        <td>75% to 89%</td>
                                                    </tr>
                                                    <tr>
                                                        <td>B</td>
                                                        <td>Very Good</td>
                                                        <td>56% to 74%</td>
                                                    </tr>
                                                    <tr>
                                                        <td>C</td>
                                                        <td>Good</td>
                                                        <td>35% to 55%</td>
                                                    </tr>
                                                    <tr>
                                                        <td>D</td>
                                                        <td>Scope for Improvement</td>
                                                        <td>Below 35%</td>
                                                    </tr>
                                                </tbody>
                                            </table>
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

