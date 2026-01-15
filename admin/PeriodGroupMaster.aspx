<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PeriodGroupMaster.aspx.cs" Inherits="admin_PeriodGroupMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
<table class="table" width="50%">
<tr>
<td>
Group
</td>
<td>
    <asp:TextBox ID="txtGrpName" runat="server" CssClass="textbox"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Class
</td>
<td>
    <asp:CheckBoxList ID="chkClass" runat="server" RepeatDirection="Horizontal">
    </asp:CheckBoxList>
</td>
</tr>
<tr>
<td colspan="2" align="right" >
    <asp:LinkButton ID="lnkShow" runat="server" CssClass="button" onclick="lnkShow_Click">Submit</asp:LinkButton>
</td>
</tr>
</table>
<table width="100%">
<tr>
<td>
    <asp:GridView ID="GridView1" runat="server" CssClass="Grid" AutoGenerateColumns="false" width="100%">
    <Columns>
    <asp:TemplateField HeaderText="#" >
     <ItemTemplate>
         <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
     </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Group Name">
     <ItemTemplate>
         <asp:Label ID="Label2" runat="server" Text='<%# Bind("GroupName") %>'></asp:Label>
     </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Remark">
     <ItemTemplate>
         <asp:Label ID="Label3" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
     </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Edit">
     <ItemTemplate>
         <asp:LinkButton ID="lnkEdit" runat="server" Text='<%# Bind("Id") %>' onclick="lnkEdit_Click" Font-Size="0" Height="16" Width="16" CssClass="edit"></asp:LinkButton>
     </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Delete">
     <ItemTemplate>
         <asp:LinkButton ID="lnkDelete" runat="server" Text='<%# Bind("Id") %>' onclick="lnkDelete_Click" Font-Size="0" Height="16" Width="16" CssClass="delete"></asp:LinkButton>
     </ItemTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView>
</td>
</tr>
</table>

<div style="overflow: auto; width: 1px; height: 1px">
    <asp:Panel ID="Panel1" runat="server" CssClass="popup">
        <table class="table" width="50%">
<tr>
<td>
Group
</td>
<td>
    <asp:TextBox ID="txtGrpName1" runat="server" CssClass="textbox"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Class
</td>
<td>
    <asp:DropDownList ID="drpClass" runat="server">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td colspan="2" align="right" >
    <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button" onclick="lnkUpdate_Click">Update</asp:LinkButton>
    <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button" >Cancle</asp:LinkButton>
</td>
</tr>
</table>
        <asp:Button ID="Button9" runat="server" Style="display: none" />
       <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="lnkCancel"      BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True" PopupControlID="Panel1" TargetControlID="Button9">
       </asp:ModalPopupExtender>
    </asp:Panel>
    </div>

    <div style="overflow: auto; width: 1px; height: 1px">
    <asp:Panel ID="Panel2" runat="server" CssClass="popup">
                 <table width="100%">
                     <tr>
                         <td>
                             &nbsp;
                         </td>
                     </tr>
             <tr>
             <td align="center"><h4>Do you really want to Cancel this receipt?</h4><asp:Label ID="lblvalue" 
                                 runat="server" Visible="False"></asp:Label></td>
             </tr>
                     <tr>
                         <td>
                             &nbsp;
                         </td>
                     </tr>
             <tr>
             <td align="center">
                 <asp:Button ID="btnDelete" runat="server" CausesValidation="False" 
                                 onclick="btnDelete_Click" Text="Yes" CssClass="button"  />
                                 &nbsp;&nbsp;
                                <asp:Button ID="Button8" runat="server" CausesValidation="False" Text="No" CssClass="button" /> 
                                 
                                 </td>
             </tr>
             </table>

    </asp:Panel>
    <asp:Button ID="Button1" runat="server" Style="display: none" />
       <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button8"      
       BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True" PopupControlID="Panel2" TargetControlID="Button9">
       </asp:ModalPopupExtender>
    </div>
</asp:Content>


