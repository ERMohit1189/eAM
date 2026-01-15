<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Period_master.aspx.cs" Inherits="Period_master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <table class="table">
        <tr>
            <td align="right">
                Season :
            </td>
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Summer</asp:ListItem>
                            <asp:ListItem>Winter</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                Medium :
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpmedium" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="drpmedium_SelectedIndexChanged" CssClass="textbox" Width="200px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                Group :
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpgroup" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="drpgroup_SelectedIndexChanged" CssClass="textbox" Width="200px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                Class :
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="drpClass_SelectedIndexChanged" CssClass="textbox" Width="200px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                Section :
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpsection" runat="server" OnSelectedIndexChanged="drpsection_SelectedIndexChanged" 
                            CssClass="textbox" Width="200px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button">View</asp:LinkButton>
            </td>
        </tr>
        </table>
    <div style="padding-right: 15px; text-align: right; padding-bottom: 4px;">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/export_word_icon.gif" title="Export to Word" />&nbsp;
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/export_excel_icon.gif" title="Export to Excel" />&nbsp;
        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/export_pdf_icon.gif" title="Export to PDF" />&nbsp;
        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/print_icon.gif" title="Print" />&nbsp;
    </div>
    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblmess" runat="server" Style="color: #CC0000; font-weight: 700"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="800px" 
                CssClass="Grid">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField HeaderText="Sr No">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("SubjectAlloted") %>'></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paper Types">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("PaperTypes") %>'></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mode of Subject">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                                        <asp:ListItem>Theory</asp:ListItem>
                                        <asp:ListItem>Lab</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Faculty Name">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("FacultyName") %>'></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Faculty (Emp-Id)">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("FacultyEmpId") %>'></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Period">
                        <FooterTemplate>
                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="Label7" runat="server" Text="Lunch Time"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                        <asp:ListItem>I st</asp:ListItem>
                                        <asp:ListItem>2 nd</asp:ListItem>
                                        <asp:ListItem>3 rd</asp:ListItem>
                                        <asp:ListItem>4 th</asp:ListItem>
                                        <asp:ListItem>5 th</asp:ListItem>
                                        <asp:ListItem>6 th</asp:ListItem>
                                        <asp:ListItem>7 th</asp:ListItem>
                                        <asp:ListItem>8 th</asp:ListItem>
                                        <asp:ListItem>9 th</asp:ListItem>
                                        <asp:ListItem>10 th</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From Timing">
                        <FooterTemplate>
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Width="70px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="60px" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To Timing">
                        <FooterTemplate>
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Width="70px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="60px" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Days">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="M" />
                                    <asp:CheckBox ID="CheckBox2" runat="server" Text="T" />
                                    <asp:CheckBox ID="CheckBox3" runat="server" Text="W" />
                                    <asp:CheckBox ID="CheckBox4" runat="server" Text="Th" />
                                    <asp:CheckBox ID="CheckBox5" runat="server" Text="F" />
                                    <asp:CheckBox ID="CheckBox6" runat="server" Text="S" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <FooterTemplate>
                            <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click">LinkButton</asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" Text='<%# Bind("Id") %>' SkinID="Edit"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" Visible="False">
                        <FooterTemplate>
                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click">LinkButton</asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:LinkButton ID="LinkButton7" runat="server">LinkButton</asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>
                            <%--<asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click" 
                                                    Text='<%# Bind("Id") %>'></asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="LinkButton7" runat="server">LinkButton</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" CssClass="button">Submit</asp:LinkButton>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click" 
                                                    Text='<%# Bind("Id") %>'></asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>--%>
    <div style="overflow: auto; width: 1px; height: 1px">
        <asp:Panel ID="Panel1" runat="server" CssClass="popup">
            <table width="100%" class="table">
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Period : "></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="Button5" runat="server" Style="display: none" />
                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                    SkinID="ddDefault">
                                    <asp:ListItem>I st</asp:ListItem>
                                    <asp:ListItem>2 nd</asp:ListItem>
                                    <asp:ListItem>3 rd</asp:ListItem>
                                    <asp:ListItem>4 th</asp:ListItem>
                                    <asp:ListItem>5 th</asp:ListItem>
                                    <asp:ListItem>6 th</asp:ListItem>
                                    <asp:ListItem>7 th</asp:ListItem>
                                    <asp:ListItem>8 th</asp:ListItem>
                                    <asp:ListItem>9 th</asp:ListItem>
                                    <asp:ListItem>10 th</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        From Timing :
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        To Timing :
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDay" runat="server" Text="Days :"></asp:Label>
                    </td>
                    <td>
                        <asp:Panel ID="Panel3" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="CheckBoxPanel1" runat="server" Text="M" />
                                    <asp:CheckBox ID="CheckBoxPanel2" runat="server" Text="T" />
                                    <asp:CheckBox ID="CheckBoxPanel3" runat="server" Text="W" />
                                    <asp:CheckBox ID="CheckBoxPanel4" runat="server" Text="Th" />
                                    <asp:CheckBox ID="CheckBoxPanel5" runat="server" Text="F" />
                                    <asp:CheckBox ID="CheckBoxPanel6" runat="server" Text="S" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="Button3" runat="server" CausesValidation="False" Css OnClick="Button3_Click" Text="Update" />
                        &nbsp;
                        <asp:Button ID="Button4" runat="server" CausesValidation="False" Css OnClick="Button4_Click" Text="Cancel" />
                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lbllunch" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
            CancelControlID="Button4" BackgroundCssClass="popup_bg">
        </asp:ModalPopupExtender>
    </div>
    <div style="overflow: auto; width: 1px; height: 1px">
        <asp:Panel ID="Panel2" runat="server" CssClass="popup">
            <table width="100%" class="table">
                <tr>
                    <td align="center">
                        <h4>
                            Do you really want to delete this record?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                        </h4>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Yes" CausesValidation="False" />
                        &nbsp;
                        <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CausesValidation="False" />
                    </td>
                </tr>
            </table>
            <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" TargetControlID="Button7" PopupControlID="Panel2"
                CancelControlID="Button8">
            </asp:ModalPopupExtender>
        </asp:Panel>
    </div>
    <div style="overflow: auto; width: 1px; height: 1px">
        <asp:Panel ID="Panel4" runat="server" Width="100%">
            <table width="100%" class="table">
                <tr>
                    <td>
                        From Timing :
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtLunchFromTime" runat="server"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="Button6" runat="server" Style="display: none" />
                    </td>
                </tr>
                <tr>
                    <td>
                        To Tining :
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtToTimeLunch" runat="server"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnLunchUpdate" runat="server" CausesValidation="False" Css OnClick="btnLunchUpdate_Click" Text="Update" />
                        &nbsp;
                        <asp:Button ID="btnLunchCancel" runat="server" CausesValidation="False" Css OnClick="Button4_Click" Text="Cancel" />
                        <asp:Label ID="Label9" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="Label10" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="Panel4_ModalPopupExtender" runat="server" TargetControlID="Button6" PopupControlID="Panel4"
            CancelControlID="btnLunchCancel" BackgroundCssClass="popup_bg">
        </asp:ModalPopupExtender>
    </div>
</asp:Content>
