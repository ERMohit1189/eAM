<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="OtherReportBalanceList_new.aspx.cs"
    Inherits="admin_OtherReportBalanceList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <table>
        <tr>
            <td>
                Select Class
            </td>
            <td>
                <asp:DropDownList ID="drpClass" runat="server" CssClass="textbox" Width="200px">
                </asp:DropDownList>
            </td>
       
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" onclick="LinkButton1_Click">View</asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" style="float:right">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/export_word_icon.gif" OnClick="ImageButton1_Click"
            title="Export to Word" Style="height: 16px" />
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/export_excel_icon.gif" OnClick="ImageButton2_Click"
            title="Export to Excel" />
        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/export_pdf_icon.gif" OnClick="ImageButton3_Click"
            Style="width: 16px" title="Export to PDF" />
        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/print_icon.gif" OnClick="ImageButton4_Click" title="Print"
            Style="height: 16px;" />
    </asp:Panel>
    <div id="divExport" runat="server">
        <table width="100%" id="abc" runat="server">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" runat="server" Style="font-size: small"></asp:Label>
                </td>
                <td style="text-align: right">
                    <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Size="Small" Style="text-align: right"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="Grid" Width="100%" ShowFooter="True">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lblSno" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S.R. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# Bind("Srno") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudnetName" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Father's Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFName" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Class">
                                <ItemTemplate>
                                    <asp:Label ID="lblClass" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section">
                                <ItemTemplate>
                                    <asp:Label ID="lblSection" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileno" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Other Fees Amount" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalAmt" runat="server">500</asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblBalanceAmtTotal" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="grid_heading_default" />
                        <RowStyle CssClass="grid_details_default" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
