<%@ Page Title="" Language="C#" MasterPageFile="~/sp/sp_root-manager.master" AutoEventWireup="true" 
CodeFile="StudentRecipetForConvenceOnline_duplicate.aspx.cs" 
Inherits="sp_StudentRecipetForConvenceOnline_duplicate" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        #abc hr { margin: 0; padding: 0; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <%--Content starts--%>
    <table width="100%">
        <tr>
            <td>
                Your Receipt No. is :
                <asp:Label ID="Label1" runat="server" Style="color: #CC0000; font-weight: 700" Text="Label"></asp:Label>
            </td>
            <td align="right">
                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" Style="color: #FF0000" Font-Underline="False">Go back to Fee Deposit</asp:LinkButton>
            </td>
            <td align="center">
                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                <%--<asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Dot Matrix</asp:LinkButton>--%>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="print" Title="Print" Height="16px"
                    Width="16px" Font-Underline="True">Print</asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div runat="server" id="abc" style="margin: 0 auto; width: 700px; padding: 10px; background: #fff; font-size: 12px;">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" align="center" id="header1" runat="server">
                    <%--<uc1:Student ID="Student1" runat="server" />--%>
                </td>
            </tr>
            <tr>
                <td align="right" width="390">
                    <asp:Label ID="Label26" runat="server" Text="FEE RECEIPT" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lblCancel" runat="server" Style="color: #FF0000"></asp:Label>Student's Copy(Duplicate)
                </td>
            </tr>
        </table>
        <hr style="margin: 0; padding: 0;" />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" BorderStyle="None"
            GridLines="None">
            <Columns>
                <asp:TemplateField>
                    <FooterTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblPrevbalance" runat="server" Style="color: #FF0000;" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblPreviousBalance" runat="server" Style="color: #FF0000" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label32" runat="server" Text="Late Fee" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label17" runat="server" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                          <%--  <tr>
                                <td align="left">
                                    <asp:Label ID="Label10" runat="server" Text="Conveyance" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblBusConvence" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label35" runat="server" Text="Total Amount" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblAmttotal" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblDiscount" runat="server" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblDiscountValue" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label36" runat="server" Text="Concession" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label33" runat="server" Text='<%# Bind("Concession") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label37" runat="server" Text="Payable Amount" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label341" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr style="margin: 0; padding: 0;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label38" runat="server" Text="Recieved Amount" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/rupees.png" />
                                    &nbsp;<asp:Label ID="Label18" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label27" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label30" runat="server" Text='<%# Bind("RemainingAmount") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblNexDueAmt" runat="server" Style="font-weight: 700" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    ___________________
                                    <br />
                                    <asp:Label ID="Label14" runat="server" Text="Signature "></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </FooterTemplate>
                    <HeaderTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" style="text-align: left">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" Text="Receipt No. :" Font-Bold="False" Font-Size="12px"></asp:Label>
                                </td>
                                <td width="55%">
                                    &nbsp;
                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label5" runat="server" Text="Date :" Font-Bold="False" Font-Size="12px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="Label34" runat="server" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label20" runat="server" Text="S.R. No. :" Font-Bold="False" Font-Size="12px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="Label24" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label21" runat="server" Text="Installment :" Font-Bold="False" Font-Size="12px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="18%" align="right">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Student's Name :" Font-Size="12px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="Label8" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    <asp:Label ID="Label15" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    <asp:Label ID="Label16" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Class :" Font-Bold="False" Font-Size="12px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    (<asp:Label ID="Label19" runat="server" Text="" Font-Bold="False" Font-Names="Courier New" Font-Size="12px"></asp:Label>)
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label22" runat="server" Text="Father's Name :" Font-Bold="False" Font-Size="12px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="Label23" runat="server" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label28" runat="server" Text="Mode :" Font-Bold="False" Font-Size="12px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Label ID="Label29" runat="server" Text="Label" Font-Bold="False" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr style="margin: 0; padding: 0;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label39" runat="server" Text="#" Font-Size="12px"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="Label40" runat="server" Text="Particulars" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label41" runat="server" Text="Amount" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="5%" align="center">
                                    <asp:Label ID="Label25" runat="server" Text='<%# Bind("serialno") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                                <td valign="middle" style="padding-left: 75px;">
                                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("FeeType") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="right" width="20%" valign="middle">
                                    <asp:Label ID="lblAmt" runat="server" Visible="False" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("FeePayment") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle Font-Size="12px" />
        </asp:GridView>
    </div>
</asp:Content>

