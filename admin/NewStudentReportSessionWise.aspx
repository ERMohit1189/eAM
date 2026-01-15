<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="NewStudentReportSessionWise.aspx.cs" Inherits="admin_NewStudentReportSessionWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
 <div class="maincontent">
        <div class="codepart">
            <div class="hedingbg">
                <h3 class="h3txt">
                    List of All Students</h3>
            </div>
            <div class="hedingline">
                <h4 class="h4txt">
                    List of All Students (According to Session)</h4>
            </div>
            <div class="contentbox">

    <table align="center" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td align="right" >
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" >
                <table align="center" cellpadding="0" cellspacing="0" width="40%">
                    <tr>
                        <td align="right">
                            Academic Year :</td>
                        <td align="left">
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged" SkinID="ddDefault">
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Select Type Of Student</td>
                        <td align="left">
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Old</asp:ListItem>
                                <asp:ListItem>New</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2" style="text-align: center">
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" SkinID="Show"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
           
            <td>
                   <div style="padding-right: 15px; text-align: right; padding-bottom:4px;" >
                     <asp:ImageButton ID="ImageButton1" runat="server" 
                           ImageUrl="~/images/export_word_icon.gif" title="Export to Word" 
                           onclick="ImageButton1_Click" style="width: 16px" />&nbsp;
                     <asp:ImageButton ID="ImageButton2" runat="server" 
                           ImageUrl="~/images/export_excel_icon.gif" title="Export to Excel" 
                           onclick="ImageButton2_Click" />&nbsp;
                     <asp:ImageButton ID="ImageButton3" runat="server" 
                           ImageUrl="~/images/export_pdf_icon.gif" title="Export to PDF" 
                           onclick="ImageButton3_Click" />&nbsp;
                     <asp:ImageButton ID="ImageButton4" runat="server" 
                           ImageUrl="~/images/print_icon.gif" title="Print" 
                           onclick="ImageButton4_Click"  />
                 </div></td>
        </tr>
        <tr>
            <td>
             <div id="gdv" runat="server">
                <table  id="abc" runat="server" align="center" width="100%" 
                    style="font-family:Arial; font-size:small;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="trgap1" style="padding-right: 15px">
                            </td>
                    </tr>
                    <tr>
                        <td align="center">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="S. No.">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("SNo") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.R. No.">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Enrollment No.">
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Father's Name">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mother's Name">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("MotherName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Medium">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date of Birth">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("DOB") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:GridView>
                        </td>
                    </tr>
                </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>

    </div>
    </div>
    </div>
</asp:Content>

