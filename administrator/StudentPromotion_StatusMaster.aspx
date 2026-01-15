<%@ Page Title="" Language="C#" MasterPageFile="~/administrator/administrato_root-manager.master" AutoEventWireup="true"CodeFile="StudentPromotion_StatusMaster.aspx.cs" Inherits="admin_Promotion_StatusMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
  <div class="maincontent">
        <div class="codepart">
            <div class="hedingbg">
                <h3 class="h3txt">
                    Student</h3>
            </div>
            <div class="hedingline">
                <h4 class="h4txt">
                    Promotion Master</h4>
            </div>
            <div class="contentbox">
                <table width="100%">
        <tr>
            <td colspan="2" style="text-align: left; font-weight: 700;">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                Status</td>
            <td>
                <asp:TextBox ID="txtStatus" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="*" ControlToValidate="txtStatus" SetFocusOnError="True" 
                    style="color: #CC0000" ValidationGroup="a"></asp:RequiredFieldValidator>
            <span class="imp">*</span></td>
        </tr>
        <tr>
            <td class="style3">
                Remark</td>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" 
                    CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" 
                    ValidationGroup="a">Submit</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="style3" colspan="2">
              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CssClass="Gridview_Border" Width="476px">
                      <AlternatingRowStyle CssClass="grid_alt_details_default" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                    onclick="LinkButton2_Click" Text='<%# Bind("Id") %>' SkinID="Edit"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                                    onclick="LinkButton3_Click" Text='<%# Bind("Id") %>' SkinID="Delete"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="style3" colspan="2">
<div style="overflow:auto;  width:1px; height:1px;">
<asp:Panel ID="Panel1" runat="server" CssClass="popup">
    <table width="100%" class="table">
        <tr>
            <td align="right">
            Status :
            </td>
            <td>
                <asp:TextBox ID="txtStatusPanel" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                <asp:Button ID="Button9" runat="server" Style="display: none" />
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
            Remark :
            </td>
            <td>
                <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" CssClass="textbox" Height="50px" 
                    Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" OnClick="LinkButton4_Click">Update</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" OnClick="LinkButton5_Click">Cancel</asp:LinkButton>
                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="LinkButton5" PopupControlID="Panel1"
        TargetControlID="Button9" BackgroundCssClass="popup_bg">
    </asp:ModalPopupExtender>
</asp:Panel>
 </div>               </td>
        </tr>
        <tr>
            <td class="style3" colspan="2">
<div style="overflow:auto;  width:1px; height:1px;">
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
                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" />
                &nbsp;
                <asp:Button ID="Button8" runat="server" CausesValidation="False" OnClick="Button8_Click" Text="No" />
            </td>
        </tr>
    </table>
    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button8" DynamicServicePath=""
        Enabled="True" PopupControlID="Panel2" TargetControlID="Button7" BackgroundCssClass="popup_bg">
    </asp:ModalPopupExtender>
</asp:Panel>                  
</div>                                    
                                       
                                                 
                   </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
            </div>
        </div>
    </div>
</asp:Content>

