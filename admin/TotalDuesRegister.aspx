<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TotalDuesRegister.aspx.cs" Inherits="admin_TotalDuesRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .style2
    {
        color: #FF6600;
            text-align: center;
        }
        .style5
        {
            width: 100%;
        }
        .style7
        {
            color: #000000;
        }
        .style8
        {
            width: 1190px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
<div class="maincontent1">
	<div class="codepart1">
		<div class="hedingbg">
             <h3 class="h3txt"> Fee Report</h3>
        </div>
		<div class="hedingline">
			<h4 class="h4txt">Dues Register Report</h4>
		</div>
		<div class="contentbox1">   
            <table align="center" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                <td align="center">
                    <table cellpadding="0" cellspacing="0" style="width:100%" >
                    <tr>
                        <td height="10"></td>
                    </tr>
                    <tr>
                        <td align="right" class="style2" >                            
                            <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click" 
                                SkinID="Show"></asp:LinkButton>                            
                        &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right:25px; float:right;">
                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                ImageUrl="~/admin/images/WordImage.jpg" onclick="ImageButton1_Click" 
                                Height="16px" Width="16px" />
                            <asp:ImageButton ID="ImageButton2" runat="server" 
                                ImageUrl="~/admin/images/ExcelImage.jpg" onclick="ImageButton2_Click" 
                                Height="16px" Width="16px" />
                     <asp:ImageButton ID="ImageButton3" runat="server" 
                                ImageUrl="~/images/export_pdf_icon.gif" title="Export to PDF" 
                                onclick="ImageButton3_Click" style="width: 16px" />
                     <asp:ImageButton ID="ImageButton4" runat="server" 
                         ImageUrl="~/images/print_icon.gif" title="Print" 
                         onclick="ImageButton4_Click" style="width: 16px"  />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <table align="left" id="abc" runat="server" width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel1" runat="server">                    
                                        <div id="gdv" runat="server">                    
                                            <asp:Label ID="lblRegister" runat="server" style="font-weight: 700; font-size: large"></asp:Label>
                                                    <br />
                                             <asp:GridView ID="grdDisplayRepo" runat="server" AutoGenerateColumns="False" 
                                                 ShowFooter="True" style="font-size:x-small; font-family: Verdana;"  
                                                Width="100%" BackColor="#DFDFDF" CellPadding="2" CellSpacing="1" 
                                                HorizontalAlign="Center">
                                                        <AlternatingRowStyle BackColor="#EFF7FC" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Class Name">
                                                                <FooterTemplate>
                                                                    <strong>Total Dues :</strong>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" style="font-weight: 700" 
                                                                        Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Arrear">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblArrearTotAmt" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblArrearAmt" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apr">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblGrandTDuesApr" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTotInstallmentApr" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="style7">Deposit :</span><asp:Label ID="lblApr" runat="server" 
                                                                                    style="font-family: Verdana;" CssClass="style7"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblBalanceApr" runat="server" style="color: #ffffff" Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Balance :<asp:Label ID="lblRBalApr" runat="server" style="font-family: Verdana"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Total Dues :
                                                                                <asp:Label ID="lblTotDuesApr" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana; font-weight: 700;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="May">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblGrandTDuesMay" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0" class="style5">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTotInstallmentMay" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="style6">Deposit :</span><asp:Label ID="lblMay" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblBalanceMay" runat="server" style="color: #0066FF" 
                                                                                    Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Balance :<asp:Label ID="lblRBalMay" runat="server" style="font-family: Verdana"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Total Dues :<asp:Label ID="lblTotDuesMay" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana; font-weight: 700;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Jul">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblGrandTDuesJul" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0" class="style5">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTotInstallmentJul" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="style6">Deposit :</span><asp:Label ID="lblJul" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblBalanceJul" runat="server" style="color: #0066FF" 
                                                                                    Visible="False" ViewStateMode="Disabled"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Balance :<asp:Label ID="lblRBalJul" runat="server" style="font-family: Verdana"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Total Dues :<asp:Label ID="lblTotDuesJul" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana; font-weight: 700;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Aug">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblGrandTDuesAug" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0" class="style5">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTotInstallmentAug" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="style6">Deposit :</span><asp:Label ID="lblAug" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblBalanceAug" runat="server" style="color: #0066FF" 
                                                                                    Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Balance :<asp:Label ID="lblRBalAug" runat="server" style="font-family: Verdana"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Total Dues :<asp:Label ID="lblTotDuesAug" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana; font-weight: 700;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sep">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblGrandTDuesSep" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0" class="style5">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTotInstallmentSep" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="style6">Deposit :</span><asp:Label ID="lblSep" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblBalanceSep" runat="server" style="color: #0066FF" 
                                                                                    Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Balance :<asp:Label ID="lblRBalSep" runat="server" style="font-family: Verdana"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Total Dues :<asp:Label ID="lblTotDuesSep" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana; font-weight: 700;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Oct">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblGrandTDuesOct" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0" class="style5">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTotInstallmentOct" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="style6">Deposit :</span><asp:Label ID="lblOct" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblBalanceOct" runat="server" style="color: #0066FF" 
                                                                                    Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Balance :<asp:Label ID="lblRBalOct" runat="server" style="font-family: Verdana"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Total Dues :<asp:Label ID="lblTotDuesOct" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana; font-weight: 700;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nov">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblGrandTDuesNov" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0" class="style5">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTotInstallmentNov" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="style6">Deposit :</span><asp:Label ID="lblNov" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblBalanceNov" runat="server" style="color: #0066FF" 
                                                                                    Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Balance :<asp:Label ID="lblRBalNov" runat="server" style="font-family: Verdana"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Total Dues :<asp:Label ID="lblTotDuesNov" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana; font-weight: 700;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dec">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblGrandTDuesDec" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0" class="style5">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTotInstallmentDec" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="style6">Deposit :</span><asp:Label ID="lblDec" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblBalanceDec" runat="server" style="color: #0066FF" 
                                                                                    Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Balance :<asp:Label ID="lblRBalDec" runat="server" style="font-family: Verdana"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Total Dues :<asp:Label ID="lblTotDuesDec" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana; font-weight: 700;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Jan">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblGrandTDuesJan" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0" class="style5">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTotInstallmentJan" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="style6">Deposit :</span><asp:Label ID="lblJan" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblBalanceJan" runat="server" style="color: #0066FF" 
                                                                                    Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Balance :<asp:Label ID="lblRBalJan" runat="server" style="font-family: Verdana"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Total Dues :<asp:Label ID="lblTotDuesJan" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana; font-weight: 700;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Feb">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblGrandTDuesFeb" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0" class="style5">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTotInstallmentFeb" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="style6">Deposit :</span><asp:Label ID="lblFeb" runat="server" 
                                                                                    style="color: #000000; font-family: Verdana;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblBalanceFeb" runat="server" style="color: #ffffff;" 
                                                                                    Visible="False"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Balance :<asp:Label ID="lblRBalFeb" runat="server" style="font-family: Verdana"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Total Dues :<asp:Label ID="lblTotDuesFeb" runat="server" 
                                                                                    style="color: #000000; font-weight: 700; font-family: Verdana;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#EEEEEE" HorizontalAlign="Left" 
                                                            VerticalAlign="Middle" Font-Bold="True" />
                                                        <RowStyle BackColor="White" Height="30px" />
                                                    </asp:GridView>
                                            <table>
                                            <tr>
                                            <td align="center" class="style8">
                                                <asp:Label ID="lblTDues" runat="server" style="color: #FF0000"></asp:Label>
                                            </td>
                                            </tr>
                                            </table>
                        
                                        </div>
                         
                              </asp:Panel>
                                    </td>
                                 
                                </tr>
                      </table>
                      </td>
                      </tr>          
                           
                   
                 </table>
                </td>
            </tr>
            </table>
        </div>
    </div>
</div>   

</asp:Content>

