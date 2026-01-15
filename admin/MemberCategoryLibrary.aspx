<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MemberCategoryLibrary.aspx.cs"
    Inherits="admin_MemberCategoryLibrary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
   
    <%--Content starts--%>
    <table class="table">
        <tr>
            <td align="right">Categeory Name
            </td>
            <td>
                <asp:DropDownList ID="drpCatagoryName" runat="server" CssClass="textbox" Width="220px">
                </asp:DropDownList>
            </td>
            <td align="right">Caution Money (Library)
            </td>
            <td>
                <asp:TextBox ID="txtcatMonLibrary" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Caution Money for Book Bank
            </td>
            <td>
                <asp:TextBox ID="txtcautionBookBank" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
            <td align="right">
            Monthly Charges
            </td>
            <td>
                <asp:TextBox ID="txtMonthlyChargesLibrary" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Fine fir Library (Per Day)
            </td>
            <td>
                <asp:TextBox ID="txtFineLibraryPerDay" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
            <td align="right">Fine for Book Bank (Per Day)
            </td>
            <td>
                <asp:TextBox ID="txtFineBookBankPerday" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
            Maximum Item for Library
            </td>
            <td>
                <asp:TextBox ID="txtMaximumitemLibrary" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
            <td align="right">
            Maximum Item for Book Bank
            </td>
            <td>
                <asp:TextBox ID="txtMaxItemBookBank" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Days of Return for Library
            </td>
            <td>
                <asp:TextBox ID="txtdaysRetLibrary" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
            <td align="right">Days of Return for Book Bank
            </td>
            <td>
                <asp:TextBox ID="txtDaysRetBookBank" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Membership Validity
            </td>
            <td>
                <asp:TextBox ID="txtMembershipValidityMonth" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
            <td align="right" valign="top">Remark
            </td>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td colspan="4" align="center">
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button">Submit</asp:LinkButton></td>
        </tr>
    </table>

                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Grid" 
        Width="100%">
                                <AlternatingRowStyle CssClass="alt" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Caution Money Library">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("CautionMoneyLibrary") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Caution Money Book Bank">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("CautionMoneyBookBank") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fine Library">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("FineLibraryPerDay") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fine Book Bank">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("FineBookBankPerDay") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" Text='<%# Bind("Id") %>' 
                                                CssClass="edit" Height="16px" Width="16px"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton3" runat="server" Text='<%# Bind("Id") %>' OnClick="LinkButton3_Click" 
                                                CssClass="delete" Height="16px" Width="16px"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <div style="overflow: auto; width: 1px; height: 1px">
                                <asp:Panel ID="Panel1" runat="server" Width="100%">
                                    <table align="center" bgcolor="White" cellpadding="0" cellspacing="0" width="440" style="border: 5px solid #24799F">
                                        <tr>
                                            <td class="trgap1" colspan="2">
                                                <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Category Name :
                                            </td>
                                            <td style="padding-left: 5px">
                                                <asp:Label ID="LblCatagoryName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Caution Money For Library :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcatMonLibrary0" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Caution Money For Book Bank :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcautionBookBank0" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Monthly Charge For Library :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMonthlyChargesLibrary0" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Fine For Library(per day) :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFineLibraryPerDay0" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Fine For Book Bank (per day) :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFineBookBankPerday0" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Maximum Items For Library :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMaximumitemLibrary0" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Maximum Items For Book Bank :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMaxItemBookBank0" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Days Of Return For Library :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdaysRetLibrary0" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Days Of Return For Book Bank&nbsp; :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDaysRetBookBank0" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Membership Validity(in months) :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMembershipValidityMonth0" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Remark :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemark0" runat="server" TextMode="MultiLine" SkinID="txtmulti"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" height="20">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="padding-left: 5px;">
                                                <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="black" OnClick="Button3_Click" Text="Update" />
                                                &nbsp;
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="black" OnClick="Button4_Click" Text="Cancel" />
                                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="trgap1" colspan="2">
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                                        CancelControlID="Button4">
                                    </asp:ModalPopupExtender>
                                </asp:Panel>
                            </div>

                            <div style="overflow: auto; width: 2px; height: 1px">
                                <asp:Panel ID="Panel2" runat="server" Width="100%">
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
                                                            Do you really want to delete this record?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="padding-right: 5px;">
                                                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Yes" CausesValidation="False" />
                                                        </td>
                                                        <td align="left" style="padding-left: 5px;">
                                                            <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CausesValidation="False" />
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
                            <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" TargetControlID="Button7" PopupControlID="Panel2"
                                CancelControlID="Button8">
                            </asp:ModalPopupExtender>

</asp:Content>
