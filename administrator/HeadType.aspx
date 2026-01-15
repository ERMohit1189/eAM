<%@ Page Title="" Language="C#" MasterPageFile="~/administrator/administrato_root-manager.master" AutoEventWireup="true" CodeFile="HeadType.aspx.cs" Inherits="admin_HeadType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Head Type</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <table class="table" align="center" width="100%" id="tblInsert" runat="server">
                <tr>
                    <th>Head Type</th>
                </tr>
                <tr>
                    <td align="left">
                        <br />
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblHeadType" runat="server" Text="Head Type"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHeadType" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblHeadCode" runat="server" Text="Head Type Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHeadTypeCode" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblHeadMode" runat="server" Text="Head Mode"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblHeadMode" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="CR" Value="CR" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>

                            <tr id="rwHide" runat="server" visible="true">
                                <td>Is Active<span class="imp">*</span> </td>
                                <td>
                                    <asp:RadioButtonList ID="rblIsActive" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="7">
                        <br />
                        <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Enter" />
                        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" />
                    </td>
                </tr>
                <tr>
                    <th align="center">
                        <br />
                        Head Type List </th>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvHeadType" runat="server" AutoGenerateColumns="false" CssClass="Grid" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="#" />
                                <asp:BoundField DataField="HeadType" HeaderText="Head Type" />
                                <asp:BoundField DataField="HeadCode" HeaderText="Head Code" />
                                <asp:BoundField DataField="HeadMode" HeaderText="Head Mode" />

                                <asp:TemplateField HeaderText="Is Active">
                                    <ItemTemplate>
                                        <asp:Label ID="IsActive" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"IsActive"))==true?"Yes":"No" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="edit" Font-Size="0pt" Height="16px" OnClick="lbtnEdit_Click" Text='<%# Eval("H01ID") %>' Width="16px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete" Font-Size="0pt" Height="16px" OnClick="lbtnDelete_Click" Text='<%# Eval("H01ID") %>' Width="16px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            </table>

                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblHeadType0" runat="server" Text="Class"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHeadType0" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblHeadCode0" runat="server" Text="Head Type Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHeadTypeCode0" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblHeadMode0" runat="server" Text="Head Mode"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblHeadMode0" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                                        <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>

                            <tr id="rwHide0" runat="server" visible="true">
                                <td>Is Active<span class="imp">*</span> </td>
                                <td>
                                    <asp:RadioButtonList ID="rblIsActive0" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button" OnClick="Button3_Click" Text="Update" />
                                    &nbsp;
                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button" Text="Cancel" />
                                    <asp:Label ID="lblH01ID" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                        CancelControlID="Button4" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="pnlDelete" runat="server" CssClass="popup">
                    <table width="100%">
                        <tr>
                            <td align="center" height="50">
                                <h4>Do you really want to delete this record?<asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="50">
                                <asp:Button ID="btnYes" runat="server" CausesValidation="False" Text="Yes" OnClick="btnYes_Click" />
                                <asp:Button ID="btnNo" runat="server" CausesValidation="False" Text="No" OnClick="btnNo_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                        Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

