<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DayBookAccountHead.aspx.cs"
    Inherits="Admin_DayBookDayBookAccountHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
   
    <%--Content starts--%>
    <table class="table">
        <tr>
            <td>
                Select Amount
            </td>
            <td>
                <asp:DropDownList ID="drpAccType" runat="server" CssClass="textbox" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CssClass="button">View</asp:LinkButton>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <asp:Panel ID="Panel1" runat="server" Style="text-align: right">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/export_excel_icon.gif" OnClick="ImageButton1_Click" />
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/export_word_icon.gif" OnClick="ImageButton2_Click" />
                    <asp:ImageButton ID="ImageButton3" runat="server" Height="16px" ImageUrl="~/images/export_pdf_icon.gif" Width="23px"
                        OnClick="ImageButton3_Click" />
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/print_icon.gif" OnClick="ImageButton4_Click" title="Print" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table id="abc" runat="server" style="width: 100%">
        <tr>
            <td align="center">
                <div id="divExport" runat="server">
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server" Width="100%">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="100%" 
                                        CssClass="Grid">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("CurrentDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dr.">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblDrSum" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Dr") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cr.">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCrSum" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Cr") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
