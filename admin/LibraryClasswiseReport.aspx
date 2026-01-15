<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LibraryClasswiseReport.aspx.cs" Inherits="admin_LibraryClasswiseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <table class="table">       
        <tr>
            <td align="right">
                Class <span class="vd_red">*</span>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpClass" runat="server" CssClass="textbox" Width="320px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            
        </tr>
       
        <tr>
            <td align="right" >
                Page size <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtPageSize" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPageSize" ErrorMessage="Can't leave blank!"
                    Style="color: #CC0000" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:LinkButton ID="LinkButton1" runat="server" SkinID="Show" CssClass="button" onclick="LinkButton1_Click">View</asp:LinkButton>
            </td>
        </tr>
    </table>
      <div style="float: right">
        <asp:Panel ID="Panel1" runat="server">
            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/print_icon.gif" OnClick="ImageButton4_Click" title="Print"
                Style="height: 16px;" />
        </asp:Panel>
    </div>
    <div runat="server" id="divExport">
    <table id="abc" runat="server" align="center" cellpadding="0" cellspacing="0" width="100%">
    <tr align="center">
                <td style="width:30px">
                    <asp:Image ID="Image1" runat="server" Height="71px" Width="73px" />
                    <asp:Label ID="lblCollegeName" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Library Classwise Report"></asp:Label>
                    
                </td>
            </tr>
        <tr>
        
            <td align="center">
            <br />
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Grid" 
                    Width="100%" onpageindexchanging="GridView1_PageIndexChanging" PageSize="100">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
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
                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("AuthorName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Publisher Name">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Publisher") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class Name">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
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
    </div>
    
</asp:Content>

