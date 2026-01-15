<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true"  CodeFile="StudentArrierReceiptGenerate.aspx.cs" Inherits="StudentReceiptGenerate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        #abc
        {
            height: 445px;
            width: 98%;
        }
        .style3
        {
            text-align: center;
        }
        .style4
        {
            width: 42%;
        }
        .style5
        {
            width: 100%;
        }
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
   <div class="maincontent">
        <div class="codepart">
            <div class="hedingbg">
                <h3 class="h3txt">
                    Fee</h3>
            </div>
            <div class="hedingline">
                <h4 class="h4txt">
                    Your Receipt No. is
                <asp:Label ID="Label1" runat="server" style="color: #CC0000; font-weight: 700" 
                    Text="Label"></asp:Label>
                </h4>
                <table cellpadding="0" cellspacing="0" class="style5">
                    <tr>
                        <td style="text-align: right">
                            <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click" 
                                style="color: #FF0000">Go back to Fee Deposit</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="contentbox">
            
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            <%--<asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Dot Matrix</asp:LinkButton>--%>
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Print</asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table align="center" cellpadding="0" cellspacing="0"  runat="server" id="abc">
                                <tr>
                                    <td runat="server" id="header1">
                                       
                                        <%--<uc1:Student ID="Student1" runat="server" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom: 1px solid #dfdfdf; text-align: center;" height="25" 
                                        valign="middle" align="center">
                                        <table align="center" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="center" width="70%">
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                       <asp:Label ID="Label26" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;" 
                                            Text="FEE RECEIPT"></asp:Label>
                                      
                                                    <asp:Label ID="lblCancel" runat="server" style="color: #FF0000"></asp:Label>
                                      
                                                </td>
                                                <td align="right">
                                                   
                                                    Student&#39;s Copy</td>
                                            </tr>
                                        </table>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                            Width="75%" ShowFooter="True" BorderStyle="None" GridLines="None">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <FooterTemplate>
                                                        <table width="670" align="center" cellpadding="0" cellspacing="0" >
                                                          
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-top:1px solid #dfdfdf;" >
                                                                    <asp:Label ID="lblPrevbalance" runat="server" style="color: #FF0000;"></asp:Label>
                                                                </td>
                                                             <td align="right" style="font-family:Arial; font-size:12px; border-top:1px solid #dfdfdf;">
                                                                    <asp:Label ID="lblPreviousBalance" runat="server" style="color: #FF0000"></asp:Label>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-top:1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label32" runat="server" Text="Late Fee"></asp:Label>
                                                                </td>
                                                                <td align="right" 
                                                                    style="font-family:Arial; font-size:12px; border-top:1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label17" runat="server" CssClass="address" Font-Bold="False" 
                                                                        Text="Label"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    Conveyance</td>
                                                                <td align="right" style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="lblBusConvence" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <strong>Total Amount</strong></td>
                                                                <td align="right" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="lblAmttotal" runat="server"></asp:Label>
                                                                    .00</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="lblDiscount" runat="server"></asp:Label>
                                                                </td>
                                                                <td align="right" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="lblDiscountValue" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    Concession</td>
                                                                <td align="right" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label33" runat="server" Text='<%# Bind("Concession") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <strong>Payable Amount</strong></td>
                                                                <td align="right" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Label ID="Label341" runat="server"></asp:Label>.00
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf;" 
                                                                     >
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
                                                                    style="font-family:Arial; font-size:12px; font-weight:bold;" >
                                                                   <asp:Label ID="Label27" runat="server" Text="Label" Font-Bold="False" 
                                                                       style="font-weight: 700"></asp:Label>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-family:Arial; font-size:12px; font-weight:bold;" >
                                                                    
                                                                    <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount"></asp:Label>
                                                                    
                                                                </td>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; font-weight:bold; text-align: right;">
                                                                    <asp:Label ID="Label30" runat="server" Font-Bold="False" 
                                                                        Text='<%# Bind("RemainingAmount") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="2" style="border-bottom:1px solid #dfdfdf" height="10px">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="2">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="right" style="text-align: left">
                                                                    &nbsp;&nbsp;<asp:Label ID="lblNexDueAmt" runat="server" style="font-weight: 700"></asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                               
                                                            </tr>
                                                        </table>
                                                    </FooterTemplate>
                                                    <HeaderTemplate>
                                                        <table width="670" align="center" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td align="left" CssClass="address" 
                                                                    
                                                                    
                                                                    
                                                                    
                                                                    
                                                                    style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf" width="16%" 
                                                                    >
                                                                    <asp:Label ID="Label2" runat="server" Text="Receipt No." Font-Bold="True" 
                                                                        Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                </td>
                                                                <td align="left" class="style4" cssclass="address" 
                                                                    style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf">
                                                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                                        Font-Size="12px" Text="Label"></asp:Label>
                                                                </td>
                                                                <td align="right" 
                                                                    style="border-bottom: 1px solid #dfdfdf;"  colspan="2" >
                                                                    <asp:Label ID="Label5" runat="server" Text="Date :" Font-Names="Arial" 
                                                                        Font-Size="12px" style="font-weight: 700"></asp:Label>
                                                                    <asp:Label ID="Label34" runat="server" Text="Label"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left"  height="20" >
                                                                    <asp:Label ID="Label20" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;"
                                                                    Text="S.R. No. :"></asp:Label>
                                                                    &nbsp;</td>
                                                                <td align="left" class="style4" height="20">
                                                                    <asp:Label ID="Label24" runat="server" Font-Names="Arial" Font-Size="12px" 
                                                                        Text="Label"></asp:Label>
                                                                </td>
                                                                <td align="right" colspan="2">
                                                                    <asp:Label ID="Label21" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;"  Text="Installment :"></asp:Label>
                                                                    <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" height="20" >
                                                                    <asp:Label ID="Label3" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;" 
                                                                         Text="Student's Name :"></asp:Label>
                                                                </td>
                                                                <td align="left" class="style4" height="20">
                                                                    <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                    <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                    <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                </td>
                                                                <td align="right"  colspan="2" >
                                                                    <asp:Label ID="Label4" runat="server"  style="font-family:Arial; font-size:12px; font-weight:bold;"
                                                                         Text="Class :"></asp:Label>
                                                                    &nbsp;<asp:Label ID="Label9" runat="server"  Font-Names="Arial" 
                                                                        Font-Size="12px"></asp:Label>
                                                                    (<asp:Label ID="Label19" runat="server"  Font-Names="Arial" 
                                                                        Font-Size="12px" Text="Label"></asp:Label>)</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="border-bottom: 1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label22" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;"
                                                                       Text="Father's Name :"></asp:Label>
                                                                </td>
                                                                <td align="left" class="style4" style="border-bottom: 1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="12px" 
                                                                        Text="Label"></asp:Label>
                                                                </td>
                                                                <td align="right" colspan="2" style="border-bottom: 1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label28" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;"  Text="Medium :"></asp:Label>
                                                                    <asp:Label ID="Label29" runat="server" Font-Names="Arial" 
                                                                        Font-Size="12px" Text="Label"></asp:Label>
                                                                </td>
                                                            </tr>
                                                           
                                                            <tr>
                                                                <td align="left"style="border-bottom: 1px solid #dfdfdf; font-family:Arial; font-size:12px; font-weight:bold;" 
                                                                    valign="middle" >
                                                                    Sr. No.</td>
                                                                <td align="left" 
                                                                    style="border-bottom: 1px solid #dfdfdf; font-family:Arial; font-size:12px; font-weight:bold;" 
                                                                    valign="middle">
                                                                    Particulars
                                                                </td>
                                                                <td align="left" valign="middle" style="border-bottom: 1px solid #dfdfdf; font-family:Arial; font-size:12px; font-weight:bold;">
                                                                    &nbsp;</td>
                                                                <td align="right" style="border-bottom: 1px solid #dfdfdf; font-family:Arial; font-size:12px; font-weight:bold;"valign="middle">
                                                                     Amount</td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table width="670" align="center" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td width="16%" valign="middle"  >
                                                                    <asp:Label ID="Label25" runat="server" style="font-family: Arial" 
                                                                        Text='<%# Bind("serialno") %>' Font-Size="12px"></asp:Label>
                                                                </td>
                                                                <td valign="middle" >
                                                                    <asp:Label ID="Label12" runat="server" CssClass="current" Font-Bold="False" 
                                                                        Font-Names="Arial" Font-Size="12px" Text='<%# Bind("FeeType") %>'></asp:Label>
                                                                </td>
                                                                <td 
                                                                    align="right" width="20%" valign="middle">
                                                                    <asp:Label ID="lblAmt" runat="server" Visible="False"></asp:Label>
                                                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("FeePayment") %>' 
                                                                        CssClass="address" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                </td>
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
                                <table align="center" cellpadding="0" cellspacing="0"  runat="server" id="Table1">
                                <tr>
                                    <td style="border-bottom:1px solid #dfdfdf" height="10px"></td>
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
                                                <td align="center" class="style3" width="85%">
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                       <asp:Label ID="Label6" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;" 
                                            Text="FEE RECEIPT"></asp:Label>
                                      
                                                    <asp:Label ID="lblCancelStudent" runat="server" style="color: #FF0000"></asp:Label>
                                      
                                                </td>
                                                <td align="right"  >
                                                    School&#39;s Copy</td>
                                            </tr>
                                        </table>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                            Width="75%" ShowFooter="True" BorderStyle="None" GridLines="None">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <FooterTemplate>
                                                        <table width="670" align="center" cellpadding="0" cellspacing="0" >
                                                          
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-top:1px solid #dfdfdf;" >
                                                                    <asp:Label ID="lblPrevBalAmt" runat="server" style="color: #FF0000;"></asp:Label>
                                                                </td>
                                                             <td align="right" 
                                                                    style="font-family:Arial; font-size:12px; border-top:1px solid #dfdfdf; font-weight: 700;">
                                                                    <asp:Label ID="lblPreviousBalance1" runat="server" style="color: #FF0000"></asp:Label>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-top:1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label32" runat="server" Text="Late Fee"></asp:Label>
                                                                </td>
                                                                <td align="right" 
                                                                    style="font-family:Arial; font-size:12px; border-top:1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label170" runat="server" CssClass="address" Font-Bold="False" 
                                                                        Text="Label"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    Conveyance</td>
                                                                <td align="right" style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="lblBusConvence1" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <strong>Total Amount</strong></td>
                                                                <td align="right" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="lblAmttotal" runat="server"></asp:Label>
                                                                    .00</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="lblDiscountGrd2" runat="server"></asp:Label>
                                                                </td>
                                                                <td align="right" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="lblDiscountValueGrd2" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    Concession</td>
                                                                <td align="right" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label330" runat="server" Text='<%# Bind("Concession") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <strong>Payable Amount</strong></td>
                                                                <td align="right" 
                                                                    style="font-family:Arial; font-size:12px; border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label342" runat="server"></asp:Label>.00</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf;" 
                                                                     >
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
                                                                    style="font-family:Arial; font-size:12px; font-weight:bold;" >
                                                                   <asp:Label ID="Label270" runat="server" Text="Label" Font-Bold="False" 
                                                                       style="font-weight: 700"></asp:Label>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-family:Arial; font-size:12px; font-weight:bold;" >
                                                                    
                                                                    <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount"></asp:Label>
                                                                    
                                                                </td>
                                                                <td align="left" 
                                                                    style="font-family:Arial; font-size:12px; font-weight:bold; text-align: right;">
                                                                    <asp:Label ID="Label300" runat="server" Font-Bold="False" 
                                                                        Text='<%# Bind("RemainingAmount") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="2" style="border-bottom:1px solid #dfdfdf" height="10px">
                                                                    </td>
                                                            </tr>
                                                        </table>
                                                    </FooterTemplate>
                                                    <HeaderTemplate>
                                                        <table width="670" align="center" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td align="left" CssClass="address" 
                                                                    
                                                                    
                                                                    
                                                                    
                                                                    
                                                                    style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf" width="16%" 
                                                                    >
                                                                    <asp:Label ID="Label2" runat="server" Text="Receipt No." Font-Bold="True" 
                                                                        Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                </td>
                                                                <td align="left" cssclass="address" 
                                                                    style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf">
                                                                    <asp:Label ID="Label70" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                                        Font-Size="12px" Text="Label"></asp:Label>
                                                                </td>
                                                                <td align="right" 
                                                                    style="border-bottom: 1px solid #dfdfdf;"  colspan="2" >
                                                                    <asp:Label ID="Label5" runat="server" Text="Date :" Font-Names="Arial" 
                                                                        Font-Size="12px" style="font-weight: 700"></asp:Label>
                                                                    <asp:Label ID="Label340" runat="server" Text="Label"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left"  height="20" >
                                                                    <asp:Label ID="Label20" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;"
                                                                    Text="S.R. No. :"></asp:Label>
                                                                </td>
                                                                <td align="left" height="20">
                                                                    <asp:Label ID="Label240" runat="server" Font-Names="Arial" Font-Size="12px" 
                                                                        Text="Label"></asp:Label>
                                                                </td>
                                                                <td align="right" colspan="2">
                                                                    <asp:Label ID="Label21" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;"  Text="Installment :"></asp:Label>
                                                                    <asp:Label ID="Label110" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" height="20" >
                                                                    <asp:Label ID="Label3" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;" 
                                                                         Text="Student's Name :"></asp:Label>
                                                                </td>
                                                                <td align="left" height="20">
                                                                    <asp:Label ID="Label80" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                    <asp:Label ID="Label150" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                    <asp:Label ID="Label160" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                </td>
                                                                <td align="right"  colspan="2" >
                                                                    <asp:Label ID="Label4" runat="server"  style="font-family:Arial; font-size:12px; font-weight:bold;"
                                                                         Text="Class :"></asp:Label>
                                                                    &nbsp;<asp:Label ID="Label90" runat="server"  Font-Names="Arial" 
                                                                        Font-Size="12px"></asp:Label>
                                                                    (<asp:Label ID="Label190" runat="server"  Font-Names="Arial" 
                                                                        Font-Size="12px" Text="Label"></asp:Label>)</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="border-bottom: 1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label22" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;"
                                                                       Text="Father's Name :"></asp:Label>
                                                                </td>
                                                                <td align="left" style="border-bottom:1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label230" runat="server" Font-Names="Arial" Font-Size="12px" 
                                                                        Text="Label"></asp:Label>
                                                                </td>
                                                                <td align="right" colspan="2" style="border-bottom: 1px solid #dfdfdf;">
                                                                    <asp:Label ID="Label28" runat="server" style="font-family:Arial; font-size:12px; font-weight:bold;"  Text="Medium :"></asp:Label>
                                                                    <asp:Label ID="Label290" runat="server" Font-Names="Arial" 
                                                                        Font-Size="12px" Text="Label"></asp:Label>
                                                                </td>
                                                            </tr>
                                                           
                                                            <tr>
                                                                <td align="left"style="border-bottom: 1px solid #dfdfdf; font-family:Arial; font-size:12px; font-weight:bold;" 
                                                                    valign="middle" >
                                                                    Sr. No.</td>
                                                                <td align="left" valign="middle" 
                                                                    
                                                                    style="border-bottom: 1px solid #dfdfdf; font-family:Arial; font-size:12px; font-weight:bold;">
                                                                    Particulars
                                                                </td>
                                                                <td align="left" valign="middle" style="border-bottom: 1px solid #dfdfdf; font-family:Arial; font-size:12px; font-weight:bold;">
                                                                    &nbsp;</td>
                                                                <td align="right" style="border-bottom: 1px solid #dfdfdf; font-family:Arial; font-size:12px; font-weight:bold;"valign="middle">
                                                                     Amount</td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table width="670" align="center" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td width="16%" valign="middle"  >
                                                                    <asp:Label ID="Label250" runat="server" style="font-family: Arial" 
                                                                        Text='<%# Bind("serialno") %>'></asp:Label>
                                                                </td>
                                                                <td valign="middle" >
                                                                    <asp:Label ID="Label120" runat="server" CssClass="current" Font-Bold="False" 
                                                                        Font-Names="Arial" Font-Size="12px" Text='<%# Bind("FeeType") %>'></asp:Label>
                                                                </td>
                                                                <td 
                                                                    align="right" width="20%" valign="middle">
                                                                    <asp:Label ID="lblAmt" runat="server" Visible="False"></asp:Label>
                                                                    <asp:Label ID="Label130" runat="server" Text='<%# Bind("FeePayment") %>' 
                                                                        CssClass="address" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                                </td>
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
                    <tr>
                        <td align="center">
                                        &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
            
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
            
                            &nbsp;</td>
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

