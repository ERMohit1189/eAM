<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MemberShipEntryLibrary.aspx.cs"
    Inherits="admin_MemberShipEntryLibrary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
  
    <%--Content starts--%>
    <table>
        <tr>
            <td>
                Select
            </td>
            <td>
                <asp:DropDownList ID="DrpEnter" runat="server" SkinID="ddDefault" 
                    OnSelectedIndexChanged="DrpEnter_SelectedIndexChanged" CssClass="textbox">
                    <asp:ListItem Value="srno">S.R. No.</asp:ListItem>
                    <asp:ListItem Value="StEnRCode">Enrollment  No.</asp:ListItem>
                    <asp:ListItem>University/Board Roll No.</asp:ListItem>
                    <asp:ListItem>School/College Roll No.</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                Enter <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="TxtEnter" runat="server" OnTextChanged="TxtEnter_TextChanged" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtEnter" ErrorMessage="Can't leave blank!"
                    SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="button">View</asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="Grid" Height="16px" Width="100%">
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
            <asp:TemplateField HeaderText="S.R. No.">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Enrollment No.">
                <ItemTemplate>
                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                    <asp:Label ID="Label23" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Father's Name">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Class">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="85px" />
                <ItemStyle Width="50px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Section">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Medium" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fee Category" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="Label28" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Admission Date " Visible="False">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Transport" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="Label30" runat="server" Text='<%# Bind("TransportRequired") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Panel ID="Panel1" runat="server">
        <table class="table">
            <tr>
                <td align="right">
                    Membership Code&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" CssClass="textbox" Width="200px"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td align="right">
                    Membership Date&nbsp;
                </td>
                <td style="padding-left: 5px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DrpYY" runat="server" OnSelectedIndexChanged="DrpYY_SelectedIndexChanged" 
                                AutoPostBack="True" CssClass="textbox">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DrpMM" runat="server" OnSelectedIndexChanged="DrpMM_SelectedIndexChanged" 
                                AutoPostBack="True" CssClass="textbox">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DrpDD" runat="server" OnSelectedIndexChanged="DrpDD_SelectedIndexChanged" 
                                AutoPostBack="True" CssClass="textbox">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
           
            <tr>
                <td align="right">
                    Membership Valid upto&nbsp;
                </td>
                <td style="padding-left: 5px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DrpYY0" runat="server" OnSelectedIndexChanged="DrpYY0_SelectedIndexChanged"
                                AutoPostBack="True" CssClass="textbox">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DrpMM0" runat="server" OnSelectedIndexChanged="DrpMM0_SelectedIndexChanged"
                                AutoPostBack="True" CssClass="textbox">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DrpDD0" runat="server" OnSelectedIndexChanged="DrpDD0_SelectedIndexChanged"
                                AutoPostBack="True" CssClass="textbox">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
           
            <tr>
                <td align="right">
                    &nbsp;
                </td>
                <td style="padding-left: 5px">
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" ValidationGroup="a" CssClass="button">Save</asp:LinkButton>
                </td>
            </tr>
           
        </table>
    </asp:Panel>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Grid" Width="100%">
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
            <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <asp:Label ID="Label31" runat="server" Text='<%# Bind("SNo") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="S.R. No.">
                <ItemTemplate>
                    <asp:Label ID="Label32" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Membership Code">
                <ItemTemplate>
                    <asp:Label ID="Label33" runat="server" Text='<%# Bind("MemberCode") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Membership Date">
                <ItemTemplate>
                    <asp:Label ID="Label34" runat="server" Text='<%# Bind("MembershipDate") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Membership Valid upto">
                <ItemTemplate>
                    <asp:Label ID="Label35" runat="server" Text='<%# Bind("MembershipValidUpto") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton4" runat="server" Text='<%# Bind("Id") %>' OnClick="LinkButton4_Click" 
                        CssClass="edit" Height="16px" Width="16px"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton5" runat="server" Text='<%# Bind("Id") %>' OnClick="LinkButton5_Click" 
                        CssClass="delete" Height="16px" Width="16px"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="overflow: auto; width: 2px; height: 1px">
        <asp:Panel ID="Panel2" runat="server" CssClass="popup">
            <table class="table">
                
                <tr>
                    <td height="20" class="style2" align="right">
                        S.R. No.
                    </td>
                    <td height="20">
                        <asp:Label ID="Label36" runat="server"></asp:Label>
                        <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none" />
                    </td>
                </tr>
                
                <tr>
                    <td class="style3" align="right">
                        Membership Code
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True" CssClass="textbox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <td height="20" align="right">
                        Membership Date
                    </td>
                    <td height="20">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="DrpYY1" runat="server" OnSelectedIndexChanged="DrpYY1_SelectedIndexChanged"
                                    AutoPostBack="True" CssClass="textbox">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DrpMM1" runat="server" OnSelectedIndexChanged="DrpMM1_SelectedIndexChanged"
                                    AutoPostBack="True" CssClass="textbox">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DrpDD1" runat="server" OnSelectedIndexChanged="DrpDD1_SelectedIndexChanged"
                                    AutoPostBack="True" CssClass="textbox">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                
                <tr>
                    <td height="20" align="right">
                        Membership Valid upto
                    </td>
                    <td height="20">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="DrpYY2" runat="server" OnSelectedIndexChanged="DrpYY2_SelectedIndexChanged"
                                    AutoPostBack="True" CssClass="textbox">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DrpMM2" runat="server" OnSelectedIndexChanged="DrpMM2_SelectedIndexChanged"
                                    AutoPostBack="True" CssClass="textbox">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DrpDD2" runat="server" OnSelectedIndexChanged="DrpDD2_SelectedIndexChanged"
                                    AutoPostBack="True" CssClass="textbox">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
               
                <tr>
                   
                    <td colspan="2" align="center">
                        <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button" OnClick="Button3_Click" 
                            Text="Update" />
                        &nbsp;
                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button" OnClick="Button4_Click" 
                            Text="Cancel" />
                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
               
            </table>
            <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel2"
                CancelControlID="Button4" BackgroundCssClass="popup_bg">
            </asp:ModalPopupExtender>
        </asp:Panel>
    </div>
    <div style="overflow: auto; width: 2px; height: 1px">
        <asp:Panel ID="Panel3" runat="server" CssClass="popup">
        <table width="100%">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        <tr>
        <td align="center">
           <h4> Do you really want to delete this record?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label></h4>
            <asp:Button ID="Button7" runat="server" Style="display: none" /></td>

        </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Yes" CausesValidation="False" 
                        CssClass="button" />
                &nbsp;
                    <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CausesValidation="False" 
                        CssClass="button" />
                </td>
            </tr>
        </table>
            
        </asp:Panel>
        <asp:ModalPopupExtender ID="Panel3_ModalPopupExtender" runat="server" TargetControlID="Button7" PopupControlID="Panel3"
            CancelControlID="Button8" BackgroundCssClass="popup_bg">
        </asp:ModalPopupExtender>
    </div>
</asp:Content>
