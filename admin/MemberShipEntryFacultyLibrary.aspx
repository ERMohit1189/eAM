<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MemberShipEntryFacultyLibrary.aspx.cs"
    Inherits="admin_MemberShipEntryFacultyLibrary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
   
    <%--Content starts--%>
    <table class="table">
        <tr>
            <td>
                Enter Emp. ID. <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtHeaderEmpId" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHeaderEmpId" ErrorMessage="Can't leave blank!"
                    SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" ValidationGroup="a" CssClass="button">View</asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="Grid">
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
            <asp:TemplateField HeaderText="Emp Id">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="Label23" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Designation">
                <ItemTemplate>
                    <asp:Label ID="Label37" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Father Name">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("EFatherName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="85px" />
                <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Bottom" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date of Joining">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RegistrationDate") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact No.">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("EmergencyContactNo") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="70px" />
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
                            <asp:DropDownList ID="DrpYY" runat="server" OnSelectedIndexChanged="DrpYY_SelectedIndexChanged" AutoPostBack="True"
                                CssClass="textbox">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DrpMM" runat="server" OnSelectedIndexChanged="DrpMM_SelectedIndexChanged" AutoPostBack="True"
                                CssClass="textbox">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DrpDD" runat="server" OnSelectedIndexChanged="DrpDD_SelectedIndexChanged" AutoPostBack="True"
                                CssClass="textbox">
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
                            <asp:DropDownList ID="DrpYY0" runat="server" OnSelectedIndexChanged="DrpYY0_SelectedIndexChanged" AutoPostBack="True"
                                CssClass="textbox">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DrpMM0" runat="server" OnSelectedIndexChanged="DrpMM0_SelectedIndexChanged" AutoPostBack="True"
                                CssClass="textbox">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DrpDD0" runat="server" OnSelectedIndexChanged="DrpDD0_SelectedIndexChanged" AutoPostBack="True"
                                CssClass="textbox">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" ValidationGroup="a" CssClass="button">Submit</asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Style="margin-right: 9px" CssClass="Grid" Width="100%">
        <AlternatingRowStyle CssClass="alt" />
        <Columns>
            <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <asp:Label ID="Label31" runat="server" Text='<%# Bind("SNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Emp Id">
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
                    <asp:LinkButton ID="LinkButton4" runat="server" Text='<%# Bind("Id") %>' OnClick="LinkButton4_Click" CssClass="edit"
                        Height="16px" Width="16px"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton5" runat="server" Text='<%# Bind("Id") %>' OnClick="LinkButton5_Click" CssClass="delete"
                        Height="16px" Width="16px"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
<%--    <div style="overflow: auto; width: 2px; height: 1px">--%>
        <asp:Panel ID="Panel2" runat="server" CssClass="popup">
            <table class="table">
                <tr>
                    <td>
                        Emp. Id.
                    </td>
                    <td>
                        <asp:Label ID="Label36" runat="server" style="font-weight: 700"></asp:Label>
                        <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Membership Code
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True" CssClass="textbox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Member Date
                    </td>
                    <td>
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
                    <td>
                        Membership Valid Upto
                    </td>
                    <td>
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
    
<div style="overflow: auto; width: 2px; height: 1px">
        <asp:Panel ID="Panel3" runat="server" CssClass="popup">
        <table width="100%">
        <tr>
        <td align="center"><h4>
            Do you really want to delete this record?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
            <asp:Button ID="Button7" runat="server" Style="display: none" /></h4></td>
        </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Yes" CausesValidation="False" />
                &nbsp;
                    <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CausesValidation="False" />
                </td>
            </tr>
        </table>
            <table width="450px" height="100px" cellpadding="0" cellspacing="0" align="center" bgcolor="#23799F">
                <tr>
                    <td>
                        <table width="440px" cellpadding="0" cellspacing="0" align="center" style="background-color: #fff">
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                   
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right: 5px;">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:ModalPopupExtender ID="Panel3_ModalPopupExtender" runat="server" TargetControlID="Button7" PopupControlID="Panel3"
        CancelControlID="Button8">
    </asp:ModalPopupExtender>
</asp:Content>
