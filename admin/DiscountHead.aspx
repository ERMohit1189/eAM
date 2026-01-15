<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DiscountHead.aspx.cs" Inherits="admin_DiscountHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .modalPopup {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <%-- Start Script --%>
         <script type="text/javascript">
             //
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
             prm.add_beginRequest(BeginRequestHandler);
             // Raised after an asynchronous postback is finished and control has been returned to the browser.
             prm.add_endRequest(EndRequestHandler);
             function BeginRequestHandler(sender, args) {
                 //Shows the modal popup - the update progress

                 var popup = $find('<%= UpdateProgress1_ModalPopupExtender.ClientID %>');
            if (popup != null) {
                popup.show();

            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= UpdateProgress1_ModalPopupExtender.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <%-- End Script --%>

    <%-- Start Progress Panel --%>
     <div aling="center" id="show" runat="server">
        <table>
            <tr align="center">
                <td>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:Image ID="Image1" runat="server" AlternateText="Processing" ImageUrl="~/SuperAdmin/images/waiting.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <ajaxToolkit:ModalPopupExtender ID="UpdateProgress1_ModalPopupExtender" runat="server" BackgroundCssClass="modalPopup" DynamicServicePath=""
                        Enabled="True" PopupControlID="UpdateProgress1" TargetControlID="UpdateProgress1">
                    </ajaxToolkit:ModalPopupExtender>
                </td>
            </tr>
        </table>

    </div>
    <%-- End Progress Panel --%>

    <div id="mainDiv" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>Discount Head For
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="S">Sibling Head</asp:ListItem>
                                <asp:ListItem Value="Y">Yearly Fee Head</asp:ListItem>
                                <asp:ListItem Value="O" Selected="True">Other Head</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>

                    </tr>
                    <tr>
                        <td>Head Name
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="a" CssClass="imp" ErrorMessage="*" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr id="row3" runat="server">

                        <td>No. of Sibling
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox" Text="1"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>

                        <td>Remark
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" TextMode="MultiLine"></asp:TextBox>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="2" align="center">
                            <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click" ValidationGroup="a" CssClass="button">Submit</asp:LinkButton>
                        </td>
                    </tr>
                </table>

                <asp:GridView ID="GridView1" runat="server" CssClass="Grid" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ParentHeadvalue" HeaderText="ParentHeadvalue" Visible="false" />
                        <asp:BoundField DataField="HeadName" HeaderText="HeadName" />
                        <asp:BoundField DataField="Remark" HeaderText="Remark" />
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" CssClass="edit" Font-Size="0px" Width="16px" Height="16px" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDeleteConfirm" runat="server" OnClick="lnkDeleteConfirm_Click" CssClass="delete" Font-Size="0px" CausesValidation="false" Width="16px" Height="16px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <div style="overflow: auto; height: 1px; width: 1px">
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup">
                        <table class="table">
                            <tr>
                                <td>Discount Head For
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="PanelRadioButtonList1" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="PanelRadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="S">Sibling Head</asp:ListItem>
                                        <asp:ListItem Value="Y">Yearly Fee Head</asp:ListItem>
                                        <asp:ListItem Value="O">Other Head</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>Head Name
                                </td>
                                <td>
                                    <asp:TextBox ID="PanelTextBox1" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="b" CssClass="imp" ErrorMessage="*" ControlToValidate="PanelTextBox1"></asp:RequiredFieldValidator>
                                </td>
                                
                            </tr>
                            <tr id="panelRow3" runat="server">
                                <td>No. of Sibling
                                </td>
                                <td>
                                    <asp:TextBox ID="PanelTextBox3" runat="server" CssClass="textbox" Text="1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Remark
                                </td>
                                <td>
                                    <asp:TextBox ID="PanelTextBox2" runat="server" CssClass="textbox" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button" OnClick="lnkUpdate_Click" ValidationGroup="b">Update</asp:LinkButton>
                                    &nbsp;
                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" CssClass="button" OnClientClick="setFocus('ContentPlaceHolder1_TextBox1');">Cancel</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text="Label" Style="display: none"></asp:Label>
                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" BackgroundCssClass="popup_bg"
                        CancelControlID="lnkCancel" TargetControlID="Label2" PopupControlID="Panel1" Enabled="true">
                    </ajaxToolkit:ModalPopupExtender>
                </div>

                <div style="overflow: auto; height: 1px; width: 1px">
                    <asp:Panel ID="Panel2" runat="server" CssClass="popup">
                        <table width="100%">
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center">Do you want to delete this record?
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:LinkButton ID="lnkDelete" CssClass="button" runat="server" OnClick="lnkDelete_Click" CausesValidation="false">Yes</asp:LinkButton>
                                    &nbsp;
                        <asp:LinkButton ID="lnkNo" CssClass="button" runat="server" CausesValidation="false" OnClientClick="setFocus('ContentPlaceHolder1_TextBox1');">No</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Label ID="Label4" runat="server" Text="Label" Style="display: none"></asp:Label>
                    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" BackgroundCssClass="popup_bg"
                        CancelControlID="lnkNo" TargetControlID="Label4" PopupControlID="Panel2" Enabled="true">
                    </ajaxToolkit:ModalPopupExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

