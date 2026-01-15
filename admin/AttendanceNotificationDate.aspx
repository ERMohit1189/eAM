<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AttendanceNotificationDate.aspx.cs" Inherits="admin_AttendanceNotificationDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Notification Date
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <table class="table" align="center" width="100%" id="tblInsert" runat="server">
                <tr>
                    <th>Attendance Notification Date Master</th>
                </tr>
                <tr>
                    <td align="left">
                        <br />
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTime" runat="server" Text="Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTime" runat="server" CssClass="textbox"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>Is Active<span class="vd_red">*</span> </td>
                                <td>
                                    <asp:RadioButtonList ID="rblIsActive" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="gvAllDate" runat="server" CssClass="Grid" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField HeaderText="*" DataField="SrNo" />
                                            <asp:BoundField HeaderText="Date" DataField="Date" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="true" OnCheckedChanged="cbAll_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbSingle" runat="server" AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="7">
                        <br />
                        <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Enter" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Enter" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" />
                    </td>
                </tr>
                <tr>
                    <th align="center">
                        <br />
                        List </th>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvDate" runat="server" AutoGenerateColumns="false" CssClass="Grid" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="#" />
                                <asp:BoundField DataField="TimeValue" HeaderText="Time" />
                                <asp:BoundField DataField="DateValue_Str" HeaderText="Date(s)" />
                                <asp:BoundField DataField="Remark" HeaderText="Remark" />

                                <asp:TemplateField HeaderText="Is Active">
                                    <ItemTemplate>
                                        <asp:Label ID="IsActive" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"IsActive"))==true?"Yes":"No" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="edit" Font-Size="0pt" Height="16px" OnClick="lbtnEdit_Click" Text='<%# Eval("A02ID") %>' Width="16px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete" Font-Size="0pt" Height="16px" OnClick="lbtnDelete_Click" Text='<%# Eval("A02ID") %>' Width="16px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            </table>

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
                    <ajaxToolkit:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                        Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

