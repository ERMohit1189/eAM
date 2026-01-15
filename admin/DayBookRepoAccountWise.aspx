<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true"CodeFile="DayBookRepoAccountWise.aspx.cs" Inherits="Admin_DayBookRepoAccountWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style1
        {
            width: 110%;
        }
        .style3
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #333333;
            padding-left: 5px;
            text-decoration: none;
            width: 56%;
        }
        .style4
        {            text-align: right;
            font-family: Arial, Helvetica, sans-serif;
            font-size: medium;
        }
        .style5
        {
            width: 541px;
        }
        .style6
        {
            width: 180px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <table class="style2">
        <tr>
            <td colspan="2" style="text-align: center; font-weight: 700">
                Day Book Income &amp; Expense</td>
        </tr>
        <tr>
            <td style="text-align: center">
            
                <asp:Label ID="lblOpengBal" runat="server" Visible="False"></asp:Label>
            
            </td>
            <td style="text-align: center; font-weight: 700">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center; font-weight: 700">
                &nbsp;</td>
            <td style="text-align: center; font-weight: 700">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; font-weight: 700">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3" align="right" style="padding-right: 5px">
            
                From Date :</td>
            <td style="text-align: left" class="style5">
            
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpFYY" runat="server" 
                            onselectedindexchanged="drpFYY_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DrpFMM" runat="server" 
                            onselectedindexchanged="DrpFMM_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DrpFDD" runat="server" 
                            onselectedindexchanged="DrpFDD_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            
            </td>
            </tr>
        <tr>
            <td class="style3" align="right" style="padding-right: 5px">
            
                &nbsp;</td>
            <td style="text-align: left" class="style5">
            
                &nbsp;</td>
            </tr>
        <tr>
            <td align="right" class="style3" style="padding-right: 5px">
            
                To Date :</td>
            <td style="text-align: left">
            
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="DrpTYY" runat="server" 
                            onselectedindexchanged="DrpTYY_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DrpTMM" runat="server" 
                            onselectedindexchanged="DrpTMM_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DrpTDD" runat="server" 
                            onselectedindexchanged="DrpTDD_SelectedIndexChanged" style="height: 22px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            
            </td>
            </tr>
        <tr>
            <td class="style4">
            
                &nbsp;</td>
            <td style="text-align: left">
            
                <asp:Label ID="lblResult" runat="server"></asp:Label>
                </td>
            </tr>
        <tr>
            <td class="style4">
            
                Account Head :</td>
            <td style="text-align: left">
            
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpAccounthead" runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </td>
            </tr>
        <tr>
            <td colspan="2" style="text-align: center">
            
                <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click" 
                    Height="26px" Width="55px" SkinID="Show"></asp:LinkButton>
            
            </td>
        </tr>
        <tr>
            <td colspan="2">
            
                <asp:Panel ID="Panel1" runat="server" style="text-align: right">
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/images/export_excel_icon.gif" 
    onclick="ImageButton1_Click" />
                    <asp:ImageButton ID="ImageButton2" runat="server" 
                        ImageUrl="~/images/export_word_icon.gif" 
    onclick="ImageButton2_Click" />
                    <asp:ImageButton ID="ImageButton3" runat="server" Height="16px" 
                        ImageUrl="~/images/export_pdf_icon.gif" Width="23px" 
                        onclick="ImageButton3_Click" />
                    <asp:ImageButton ID="ImageButton4" runat="server" 
                        ImageUrl="~/images/print_icon.gif" onclick="ImageButton4_Click" title="Print" />
                </asp:Panel>
            
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <table id="abc" runat="server">
            <tr>
            <td class="style6">
                   <asp:Label ID="lblAccounthead" runat="server"></asp:Label>
            </td>
            </tr>
            <tr>
            <td class="style6">
                   <div id="divExport" runat="server">
                   <asp:Panel ID="Panel2" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            ShowFooter="True" Height="201px" Width="177%" BackColor="#999999" 
            CellPadding="1" CellSpacing="1" GridLines="None" CssClass="Gridview_Border" >
           <AlternatingRowStyle CssClass="grid_alt_details_default"  />
            <Columns>
                <asp:TemplateField HeaderText="S. No.">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("CurrentDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Head">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Particulars") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Dr.">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Dr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cr.">
                    <FooterTemplate>
                        <table>
                            <tr>
                                <td >
                                    <asp:Label ID="Label8" runat="server" Text="Opening Balance :"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label7" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label9" runat="server" Text="Total Expense :"></asp:Label>
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblDrTotal" runat="server"></asp:Label>
                                    &nbsp;<br />
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    <asp:Label ID="Label10" runat="server" Text="Total Income Amount :"></asp:Label>
                                </td>
                                <td class="style4">
                                    <asp:Label ID="lblCrSum" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="Label11" runat="server" Text="Closing Balance :"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblClosingBal" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Cr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Balance">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("OpeningBalance") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#3399FF" Font-Names="arial" Font-Size="12px" 
                ForeColor="White" Height="35px" />
            <RowStyle BackColor="#CCCCCC" Height="25px" />
        </asp:GridView>
        </asp:Panel>
                </div>
            </td>
            </tr>
            </table>
            </td>
        </tr>
        <tr>
            <td class="style4" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

