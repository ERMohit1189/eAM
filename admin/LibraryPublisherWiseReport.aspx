<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LibraryPublisherWiseReport.aspx.cs" Inherits="admin_LibraryTitleWiseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <table class="table">
        <tr>
            <td align="right">
                Publisher Name <span class="vd_red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox" Width="320px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Paze size <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtPageSize" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPageSize" ErrorMessage="Can't leave blank!"
                    Style="color: #CC0000" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button">View</asp:LinkButton>
            </td>
        </tr>
    </table>
    <div style="padding-right: 15px; text-align: right; padding-bottom: 4px;">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/export_word_icon.gif" title="Export to Word" />&nbsp;
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/export_excel_icon.gif" title="Export to Excel" />&nbsp;
        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/export_pdf_icon.gif" title="Export to PDF" />&nbsp;
        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/print_icon.gif" title="Print" />
    </div>
    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="print" Height="16px" 
        Width="16px">Print</asp:LinkButton>
    <table id="abc" runat="server" align="center" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                    PageSize="100" CssClass="Grid" Width="100%">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accession No.">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("AccessionNo") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ISBN/ISSN No.">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("ISBNISSN") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Author">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("Author1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Publisher Name">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Publisher") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edition">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("Edition") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pages">
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("Pages") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Library Entry Date">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("LibraryEntryDate") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
