<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="BalanceStudentReceiptGenerate_Duplicate.aspx.cs" Inherits="admin_BalanceStudentReceiptGenerate_Duplicate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .style3 {
            text-align: center;
        }

        .style4 {
            width: 42%;
        }

        .style5 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="maincontent">
        <div class="codepart">

            <div class="contentbox">

                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table align="center" cellpadding="0" cellspacing="0" runat="server">
                                <tr>
                                    <td>
                                        <div class="hedingbg">
                                            <h3 class="h3txt">Fee</h3>
                                        </div>
                                        <div class="hedingline">
                                            <h4 class="h4txt">Your Receipt No. is
                <asp:Label ID="Label1" runat="server" Style="color: #CC0000; font-weight: 700"
                    Text="Label"></asp:Label>
                                            </h4>
                                            <table cellpadding="0" cellspacing="0" class="style5">
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click"
                                                            Style="color: #FF0000">Go back to Balance Fee Deposit</asp:LinkButton>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                        <%--<asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Dot Matrix</asp:LinkButton>--%>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton1_Click" CssClass="print" Title="Print" Height="16px"
                                                            Width="16px" Font-Underline="True">Print</asp:LinkButton>
                                                </tr>
                                            </table>
                                        </div>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table align="center" cellpadding="0" cellspacing="0" runat="server" id="abc">
                                            <tr>
                                                <td id="header1" runat="server">

                                                    <%--<uc1:Student ID="Student1" runat="server" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: 1px solid #dfdfdf; text-align: center;" height="25"
                                                    valign="middle" align="center">
                                                    <table align="center" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <%--<td align="center" width="70%">--%>
                                                            <td align="center" class="style3" width="85%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                       <asp:Label ID="Label26" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;"
                                           Text="FEE RECEIPT"></asp:Label>

                                                                <asp:Label ID="lblCancel" runat="server" Style="color: #FF0000"></asp:Label>

                                                            </td>
                                                            <td align="right">Student&#39;s Copy (Duplicate)</td>
                                                        </tr>
                                                    </table>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                        Width="75%" ShowFooter="True" BorderStyle="None" GridLines="None"
                                                        CellPadding="4">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <FooterTemplate>
                                                                    <table width="670" align="center" cellpadding="0" cellspacing="0">

                                                                        <tr>
                                                                            <td align="left"
                                                                                style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf;">
                                                                                <asp:Label ID="lblPrevbalance" runat="server" Style="color: #FF0000;">Balance Amount (Previous) : </asp:Label>
                                                                            </td>
                                                                            <td align="right" style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf;">
                                                                                <asp:Label ID="lblPreviousBalance" runat="server" Style="color: #FF0000"></asp:Label>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf;">
                                                                                <strong style="text-align: center">Received Amount</strong></td>
                                                                            <td align="right"
                                                                                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf">
                                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/rupees.png" />
                                                                                &nbsp;<asp:Label ID="Label18" runat="server" Font-Bold="False"
                                                                                    Font-Names="Arial" Font-Size="12px" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" colspan="2"
                                                                                style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                <asp:Label ID="Label27" runat="server" Text="Label" Font-Bold="False"
                                                                                    Style="font-weight: 700"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="font-family: Arial; font-size: 12px; font-weight: bold;">

                                                                                <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount"></asp:Label>

                                                                            </td>
                                                                            <td align="left"
                                                                                style="font-family: Arial; font-size: 12px; font-weight: bold; text-align: right;">
                                                                                <asp:Label ID="Label30" runat="server" Font-Bold="False"
                                                                                    Text='<%# Bind("RemainingAmount") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" colspan="2" style="border-bottom: 1px solid #dfdfdf" height="10px"></td>
                                                                        </tr>
                                                                       
                                                                        <tr>

                                                                            <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px;">Generated by eAM&reg; | Received by
                                                                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;on 
                                                                <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                    </table>
                                                                </FooterTemplate>
                                                                <HeaderTemplate>
                                                                    <table width="670" align="center" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="left" cssclass="address"
                                                                                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf" width="16%">
                                                                                <asp:Label ID="Label2" runat="server" Text="Receipt No." Font-Bold="True"
                                                                                    Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                            </td>
                                                                            <td align="left" class="style4" cssclass="address"
                                                                                style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf">
                                                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                                    Font-Size="12px" Text="Label"></asp:Label>
                                                                            </td>
                                                                            <td align="right"
                                                                                style="border-bottom: 1px solid #dfdfdf;" colspan="2">
                                                                                <asp:Label ID="Label5" runat="server" Text="Date :" Font-Names="Arial"
                                                                                    Font-Size="12px" Style="font-weight: 700"></asp:Label>
                                                                                <asp:Label ID="Label34" runat="server" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" height="20">
                                                                                <asp:Label ID="Label20" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;"
                                                                                    Text="S.R. No. :"></asp:Label>
                                                                                &nbsp;</td>
                                                                            <td align="left" class="style4" height="20">
                                                                                <asp:Label ID="Label24" runat="server" Font-Names="Arial" Font-Size="12px"
                                                                                    Text="Label"></asp:Label>
                                                                            </td>
                                                                            <td align="right" colspan="2">
                                                                                <asp:Label ID="Label21" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;" Text="Installment :"></asp:Label>
                                                                                <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" height="20">
                                                                                <asp:Label ID="Label3" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;"
                                                                                    Text="Student's Name :"></asp:Label>
                                                                            </td>
                                                                            <td align="left" class="style4" height="20">
                                                                                <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                                <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                                <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                            </td>
                                                                            <td align="right" colspan="2">
                                                                                <asp:Label ID="Label4" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;"
                                                                                    Text="Class :"></asp:Label>
                                                                                &nbsp;<asp:Label ID="Label9" runat="server" Font-Names="Arial"
                                                                                    Font-Size="12px"></asp:Label>
                                                                                <asp:Label ID="lblBranch" runat="server" Text=""></asp:Label>
                                                                                (<asp:Label ID="Label19" runat="server" Font-Names="Arial"
                                                                                    Font-Size="12px" Text="Label"></asp:Label>)</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="border-bottom: 1px solid #dfdfdf;">
                                                                                <asp:Label ID="Label22" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;"
                                                                                    Text="Father's Name :"></asp:Label>
                                                                            </td>
                                                                            <td align="left" class="style4" style="border-bottom: 1px solid #dfdfdf;">
                                                                                <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="12px"
                                                                                    Text="Label"></asp:Label>
                                                                            </td>
                                                                            <td align="right" colspan="2" style="border-bottom: 1px solid #dfdfdf;">
                                                                                <asp:Label ID="Label28" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;" Text="Medium :"></asp:Label>
                                                                                <asp:Label ID="Label29" runat="server" Font-Names="Arial"
                                                                                    Font-Size="12px" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td align="left" style="border-bottom: 1px solid #dfdfdf; font-family: Arial; font-size: 12px; font-weight: bold;"
                                                                                valign="middle">S. No.</td>
                                                                            <td align="left"
                                                                                style="border-bottom: 1px solid #dfdfdf; font-family: Arial; font-size: 12px; font-weight: bold;"
                                                                                valign="middle">Particulars
                                                                            </td>
                                                                            <td align="left" valign="middle" style="border-bottom: 1px solid #dfdfdf; font-family: Arial; font-size: 12px; font-weight: bold;">&nbsp;</td>
                                                                            <td align="right" style="border-bottom: 1px solid #dfdfdf; font-family: Arial; font-size: 12px; font-weight: bold;" valign="middle">Amount</td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table width="670" align="center" style="border-color: #ffffff;" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="16%" valign="middle">&nbsp;</td>
                                                                            <td valign="middle">&nbsp;</td>
                                                                            <td
                                                                                align="right" width="20%" valign="middle">&nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table align="center" cellpadding="0" cellspacing="0" runat="server" id="Table1">
                                                        <tr>
                                                            <td style="border-bottom: 1px solid #dfdfdf" height="10px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td id="header2" runat="server">
                                                                <%--<uc2:School ID="School1" runat="server" />--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border-bottom: 1px solid #dfdfdf; text-align: center;" height="25"
                                                                valign="middle" align="center">
                                                                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td align="center" class="style3" width="85%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                       <asp:Label ID="Label6" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;"
                                           Text="FEE RECEIPT"></asp:Label>

                                                                            <asp:Label ID="lblCancelStudent" runat="server" Style="color: #FF0000"></asp:Label>

                                                                        </td>
                                                                        <td align="right">School&#39;s Copy (Duplicate)</td>
                                                                    </tr>
                                                                </table>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                                                                    Width="75%" ShowFooter="True" BorderStyle="None" GridLines="None"
                                                                    CellPadding="4">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <FooterTemplate>
                                                                                <table width="670" align="center" cellpadding="0" cellspacing="0">

                                                                                    <tr>
                                                                                        <td align="left"
                                                                                            style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf;">
                                                                                            <asp:Label ID="lblPrevBalAmt" runat="server" Style="color: #FF0000;">Balance Amount (Previous) : </asp:Label>
                                                                                        </td>
                                                                                        <td align="right"
                                                                                            style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf; font-weight: 700;">
                                                                                            <asp:Label ID="lblPreviousBalance1" runat="server" Style="color: #FF0000"></asp:Label>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf;">
                                                                                            <strong style="text-align: center">Received Amount</strong></td>
                                                                                        <td align="right"
                                                                                            style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf">
                                                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/rupees.png" />
                                                                                            &nbsp;<asp:Label ID="Label180" runat="server" Font-Bold="False"
                                                                                                Font-Names="Arial" Font-Size="12px" Text="Label"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" colspan="2"
                                                                                            style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                                                            <asp:Label ID="Label270" runat="server" Text="Label" Font-Bold="False"
                                                                                                Style="font-weight: 700"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" style="font-family: Arial; font-size: 12px; font-weight: bold;">

                                                                                            <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount"></asp:Label>

                                                                                        </td>
                                                                                        <td align="left"
                                                                                            style="font-family: Arial; font-size: 12px; font-weight: bold; text-align: right;">
                                                                                            <asp:Label ID="Label300" runat="server" Font-Bold="False"
                                                                                                Text='<%# Bind("RemainingAmount") %>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                   
                                                                                    <tr>

                                                                                        <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px;">Generated by eAM&reg; | Received by
                                                                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;on 
                                                                <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </FooterTemplate>
                                                                            <HeaderTemplate>
                                                                                <table width="670" align="center" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td align="left" cssclass="address"
                                                                                            style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf" width="16%">
                                                                                            <asp:Label ID="Label2" runat="server" Text="Receipt No." Font-Bold="True"
                                                                                                Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" cssclass="address"
                                                                                            style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf">
                                                                                            <asp:Label ID="Label70" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                                                Font-Size="12px" Text="Label"></asp:Label>
                                                                                        </td>
                                                                                        <td align="right"
                                                                                            style="border-bottom: 1px solid #dfdfdf;" colspan="2">
                                                                                            <asp:Label ID="Label5" runat="server" Text="Date :" Font-Names="Arial"
                                                                                                Font-Size="12px" Style="font-weight: 700"></asp:Label>
                                                                                            <asp:Label ID="Label340" runat="server" Text="Label"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" height="20">
                                                                                            <asp:Label ID="Label20" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;"
                                                                                                Text="S.R. No. :"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" height="20">
                                                                                            <asp:Label ID="Label240" runat="server" Font-Names="Arial" Font-Size="12px"
                                                                                                Text="Label"></asp:Label>
                                                                                        </td>
                                                                                        <td align="right" colspan="2">
                                                                                            <asp:Label ID="Label21" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;" Text="Installment :"></asp:Label>
                                                                                            <asp:Label ID="Label110" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" height="20">
                                                                                            <asp:Label ID="Label3" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;"
                                                                                                Text="Student's Name :"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" height="20">
                                                                                            <asp:Label ID="Label80" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                                            <asp:Label ID="Label150" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                                            <asp:Label ID="Label160" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                                        </td>
                                                                                        <td align="right" colspan="2">
                                                                                            <asp:Label ID="Label4" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;"
                                                                                                Text="Class :"></asp:Label>
                                                                                            &nbsp;<asp:Label ID="Label90" runat="server" Font-Names="Arial"
                                                                                                Font-Size="12px"></asp:Label>
                                                                                            <asp:Label ID="lblBranch" runat="server" Text=""></asp:Label>
                                                                                            (<asp:Label ID="Label190" runat="server" Font-Names="Arial"
                                                                                                Font-Size="12px" Text="Label"></asp:Label>)</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" style="border-bottom: 1px solid #dfdfdf;">
                                                                                            <asp:Label ID="Label22" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;"
                                                                                                Text="Father's Name :"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" style="border-bottom: 1px solid #dfdfdf;">
                                                                                            <asp:Label ID="Label230" runat="server" Font-Names="Arial" Font-Size="12px"
                                                                                                Text="Label"></asp:Label>
                                                                                        </td>
                                                                                        <td align="right" colspan="2" style="border-bottom: 1px solid #dfdfdf;">
                                                                                            <asp:Label ID="Label28" runat="server" Style="font-family: Arial; font-size: 12px; font-weight: bold;" Text="Medium :"></asp:Label>
                                                                                            <asp:Label ID="Label290" runat="server" Font-Names="Arial"
                                                                                                Font-Size="12px" Text="Label"></asp:Label>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td align="left" style="border-bottom: 1px solid #dfdfdf; font-family: Arial; font-size: 12px; font-weight: bold;"
                                                                                            valign="middle">S. No.</td>
                                                                                        <td align="left" valign="middle"
                                                                                            style="border-bottom: 1px solid #dfdfdf; font-family: Arial; font-size: 12px; font-weight: bold;">Particulars
                                                                                        </td>
                                                                                        <td align="left" valign="middle" style="border-bottom: 1px solid #dfdfdf; font-family: Arial; font-size: 12px; font-weight: bold;">&nbsp;</td>
                                                                                        <td align="right" style="border-bottom: 1px solid #dfdfdf; font-family: Arial; font-size: 12px; font-weight: bold;" valign="middle">Amount</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <table width="670" align="center" style="border-color: #ffffff;" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td width="16%" valign="middle">&nbsp;</td>
                                                                                        <td valign="middle">&nbsp;</td>
                                                                                        <td
                                                                                            align="right" width="20%" valign="middle">&nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>


            </div>

        </div>
    </div>
</asp:Content>
