<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PeriodFormat.aspx.cs" Inherits="Period_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

<table class="table">
                    <tr>
                        <td align="right">
                Season :</td>
                        <td align="left"><asp:UpdatePanel ID="UpdatePanel19" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                            onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Summer</asp:ListItem>
                            <asp:ListItem>Winter</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel></td>
                    </tr>
                    <tr>
                        <td align="right">
                Medium <span class="vd_red">*</span></td>
                        <td align="left"><asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpmedium" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="drpmedium_SelectedIndexChanged" CssClass="textbox" Width="200px">
                            <asp:ListItem>English</asp:ListItem>
                            <asp:ListItem>Hindi</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel></td>
                    </tr>
                    <tr>
                        <td align="right">
                Group <span class="vd_red">*</span></td>
                        <td align="left"><asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpgroup" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="drpgroup_SelectedIndexChanged" CssClass="textbox" Width="200px">
                            <asp:ListItem>Science</asp:ListItem>
                            <asp:ListItem>Commerce</asp:ListItem>
                            <asp:ListItem>Arts</asp:ListItem>
                            <asp:ListItem Selected="True">N/A</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel></td>
                    </tr>
                    <tr>
                        <td align="right">
                Class <span class="vd_red">*</span></td>
                        <td align="left"><asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="drpClass_SelectedIndexChanged" CssClass="textbox" Width="200px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel></td>
                    </tr>
                    <tr>
                        <td align="right">
                Section <span class="vd_red">*</span></td>
                        <td align="left"><asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpsection" runat="server" 
                            onselectedindexchanged="drpsection_SelectedIndexChanged" CssClass="textbox" Width="200px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" CssClass="button">View</asp:LinkButton>
                        </td>
                    </tr>
                    </table>

 <div style="padding-right: 15px; text-align: right; padding-bottom:4px;">
                     <asp:ImageButton ID="ImageButton1" runat="server" 
                         ImageUrl="~/images/export_word_icon.gif" title="Export to Word" 
                         onclick="ImageButton1_Click" />&nbsp;
                     <asp:ImageButton ID="ImageButton2" runat="server" 
                         ImageUrl="~/images/export_excel_icon.gif" title="Export to Excel" 
                         onclick="ImageButton2_Click" />&nbsp;
                     <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/export_pdf_icon.gif" title="Export to PDF" />&nbsp;
                     <asp:ImageButton ID="ImageButton4" runat="server" 
                         ImageUrl="~/images/print_icon.gif" title="Print" 
                         onclick="ImageButton4_Click"  />
                 </div>

 <div id="gdv" runat="server">
                            <table runat="server" id="abc" cellpadding="0" cellspacing="0" 
                     width="100%" >
                    <tr>
                        <td>
                            <div id="header" runat="server" style="width:80%"></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                    ShowFooter="True" Width="100%" CssClass="Grid">
                                    <AlternatingRowStyle CssClass="alt" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No">
                                            <ItemTemplate>
                                                <asp:Label ID="Label15" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Label ID="Label17" runat="server" Text='<%# Bind("SubjectAlloted") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paper Types">
                                            <ItemTemplate>
                                                <asp:Label ID="Label35" runat="server" Text='<%# Bind("PaperTypes") %>'></asp:Label>
                                                (<asp:Label ID="Label24" runat="server" Text='<%# Bind("Days") %>'></asp:Label>
                                                )
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mode of Subject">
                                            <ItemTemplate>
                                                <asp:Label ID="Label19" runat="server" Text='<%# Bind("Modeofsubject") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Faculty Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Label20" runat="server" Text='<%# Bind("facultyname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Period">
                                            <ItemTemplate>
                                                <asp:Label ID="Label21" runat="server" Text='<%# Bind("Period") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From Timing">
                                            <ItemTemplate>
                                                <asp:Label ID="Label22" runat="server" Text='<%# Bind("fromtiming") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To Timing">
                                            <ItemTemplate>
                                                <asp:Label ID="Label23" runat="server" Text='<%# Bind("totiming") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                        </td>
                    </tr>
                    </table>
                    </div>

</asp:Content>

