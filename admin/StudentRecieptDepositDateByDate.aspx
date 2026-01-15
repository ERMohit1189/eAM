<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StudentRecieptDepositDateByDate.aspx.cs" Inherits="admin_StudentRecieptDepositDateByDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
<div class="maincontent">
			<div class="codepart">
				<div class="hedingbg">
                    <h3 class="h3txt">Report</h3>
                </div>
				<div class="hedingline">
					<h4 class="h4txt">Total Collection&nbsp; Fee</h4>
				</div>
				<div class="contentbox">
    <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                <asp:Panel ID="Panel1" runat="server">
                    <table cellpadding="0" cellspacing="0" width="40%" align="center">
                        <tr>
                            <td align="right">
                                Select Date :</td>
                            <td align="left" style="padding-left:5px;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="FromYY" runat="server" 
                                            onselectedindexchanged="FromYY_SelectedIndexChanged" 
                                            CssClass="dropdownSize2" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="FromMM" runat="server" 
                                            onselectedindexchanged="FromMM_SelectedIndexChanged" SkinID="ddlSize1" 
                                            CssClass="dropdownSize1" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="FromDD" runat="server" 
                                            onselectedindexchanged="FromDD_SelectedIndexChanged" SkinID="ddlSize1" 
                                            CssClass="dropdownSize1">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td >
                                </td>
                            <td align="left" style="padding-left:5px;">
                                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Show</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    
                </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            &nbsp;</td>
                    </tr>
                   <tr>
            <td >
               
                  <div style="padding-right: 15px; text-align: right;">
                     <asp:ImageButton ID="ImageButton1" runat="server" 
                          ImageUrl="~/images/export_word_icon.gif" title="Export to Word" 
                          onclick="ImageButton1_Click" CausesValidation="False" />&nbsp;
                     <asp:ImageButton ID="ImageButton2" runat="server" 
                          ImageUrl="~/images/export_excel_icon.gif" title="Export to Excel" 
                          onclick="ImageButton2_Click" CausesValidation="False" />&nbsp;
                     <asp:ImageButton ID="ImageButton3" runat="server" 
                          ImageUrl="~/images/export_pdf_icon.gif" title="Export to PDF" 
                          onclick="ImageButton3_Click" CausesValidation="False" />&nbsp;
                     <asp:ImageButton ID="ImageButton4" runat="server" 
                          ImageUrl="~/images/print_icon.gif" title="Print" 
                          onclick="ImageButton4_Click" CausesValidation="False"  />&nbsp;
                 </div></td>
        </tr>
        <tr>
            
            <td>
                &nbsp;</td>
        </tr>
                    <tr>
                        <td align="center">
                        

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                      <div id="gdv" runat="server">
                        <table id="abc" runat="server" width="940px">
                        <tr>
                        <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            onpageindexchanged="GridView1_PageIndexChanged" ShowFooter="True" 
                            Width="99%" BackColor="#DFDFDF" CellPadding="1" CellSpacing="1" 
                            GridLines="None" onselectedindexchanged="GridView1_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="#EFF7FC" />
                            <Columns>
                                <asp:TemplateField HeaderText="S. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label24" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt No.">
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sr.No.">
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Student Name "></asp:TemplateField>
                                <asp:TemplateField HeaderText="Class Name"></asp:TemplateField>
                                <asp:TemplateField HeaderText="Section"></asp:TemplateField>
                                <asp:TemplateField HeaderText="Father Name"></asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount"></asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#EEEEEE" Height="30px" />
                            <HeaderStyle BackColor="#EEEEEE" Height="35px" />
                            <RowStyle BackColor="White" Height="28px" />
                        </asp:GridView>

                          </td>
                </tr>
                </table>
                </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
              
                </td>
                    </tr>
                </table>
                &nbsp;</div>
    </div>
    </div>
</asp:Content>

