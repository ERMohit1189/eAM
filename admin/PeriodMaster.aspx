<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PeriodMaster.aspx.cs" Inherits="admin_PeriodMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
<style type="text/css">
.modalPopup
{
background-color: #696969;
filter: alpha(opacity=40);
opacity: 0.7;
xindex:-1;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
 <script type="text/javascript">
     var prm = Sys.WebForms.PageRequestManager.getInstance();
     //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
     prm.add_beginRequest(BeginRequestHandler);
     // Raised after an asynchronous postback is finished and control has been returned to the browser.
     prm.add_endRequest(EndRequestHandler);
     function BeginRequestHandler(sender, args) {
         //Shows the modal popup - the update progress
         var popup1 = args.get_postBackElement();
         if (popup1 != null) {
             var popup = $find('<%= UpdateProgress1_ModalPopupExtender.ClientID %>');
             if (popup != null) {
                 popup.show();
             }
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
<div aling="center" id="show" runat="server">
<table>
      <tr align="center">
      <td>
         <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <asp:Image ID="Image1" runat="server" AlternateText="Processing" ImageUrl="~/SuperAdmin/images/waiting.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:ModalPopupExtender ID="UpdateProgress1_ModalPopupExtender" runat="server" BackgroundCssClass="modalPopup" DynamicServicePath=""
                Enabled="True" PopupControlID="UpdateProgress1" TargetControlID="UpdateProgress1">
            </asp:ModalPopupExtender>
      </td>
      </tr>
</table>
   
</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
              <div class="maindiv">
<table id="Table1" runat="server" class="table">
<tr>
<td colspan=2>
<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" 
        onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
    <asp:ListItem>For Boys</asp:ListItem>
    <asp:ListItem>For Girls</asp:ListItem>
    <asp:ListItem Selected="True">Combined</asp:ListItem>
</asp:RadioButtonList>
</td>
</tr>
<tr>
<td>
Period Name :
</td>
<td>
    <asp:TextBox ID="txtPeriodName" runat="server" CssClass="textbox"></asp:TextBox>
</td>
</tr>
<tr>
<td>
From Time :
</td>
<td>
    <asp:TextBox ID="txtFromHour" runat="server" CssClass="textbox" Width="56px"></asp:TextBox>
      <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" Width="3px" Text=":" Enabled="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="txtFromMinute" runat="server" CssClass="textbox" Width="54px"></asp:TextBox>
</td>
</tr>
<tr>
<td>
To Time :
</td>
<td>
    <asp:TextBox ID="txtToHour" runat="server" CssClass="textbox" Width="56px"></asp:TextBox>
    <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" Width="3px" Text=":" Enabled="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="txtToMinute" runat="server" CssClass="textbox" Width="54px"></asp:TextBox>
</td>
</tr>
<tr align="center">
<td colspan=2>
    <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" onclick="lnkSubmit_Click">Submit</asp:LinkButton>
</td>
</tr>
</table>
<table Width="100%">
<tr>
<td>
    <asp:GridView ID="Grd" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="false" ShowFooter="true">
    <Columns>
    <asp:TemplateField HeaderText="#">
    <ItemTemplate>
        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="Type">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="Period Name">
    <ItemTemplate>
        <asp:Label ID="Label3" runat="server" Text='<%# Bind("PeriodName") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="From Time">
    <ItemTemplate>
        <asp:Label ID="Label4" runat="server" Text='<%# Bind("FromTime") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="To Time">
    <ItemTemplate>
        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ToTime") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="TimePeriod">
    <ItemTemplate>
        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
    </ItemTemplate>
    <FooterTemplate>
        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
    </FooterTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="Edit">
    <ItemTemplate>
        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="edit" Text='<%# Bind("Id") %>' OnClick="lnkEdit_Click" Font-Size="0" Width="16" Height="16"></asp:LinkButton>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Delete">
    <ItemTemplate>
        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="delete" Text='<%# Bind("Id") %>' OnClick="lnkDelete_Click" Font-Size="0" Width="16" Height="16"></asp:LinkButton>
    </ItemTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView>
</td>
</tr>
</table>
<div style="overflow:auto; width:1px; height:1px">
    <asp:Panel ID="Panel1" runat="server" CssClass="popup">
    <table class="table">
<tr>
<td colspan=2>
<asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
    <asp:ListItem>For Boys</asp:ListItem>
    <asp:ListItem>For Girls</asp:ListItem>
    <asp:ListItem>Combined</asp:ListItem>
</asp:RadioButtonList>
</td>
</tr>
<tr>
<td>
Period Name :
</td>
<td>
    <asp:TextBox ID="txtPeriodName1" runat="server" CssClass="textbox"></asp:TextBox>
</td>
</tr>
<tr>
<td>
From Time :
</td>
<td>
    <asp:TextBox ID="txtFromHour1" runat="server" CssClass="textbox" Width="56px"></asp:TextBox>
     <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox" Width="3px" Text=":" Enabled="false"></asp:TextBox>
      <asp:TextBox ID="txtFromMinute1" runat="server" CssClass="textbox" Width="54px"></asp:TextBox>
</td>
</tr>
<tr>
<td>
To Time :
</td>
<td>
    <asp:TextBox ID="txtToHour1" runat="server" CssClass="textbox" Width="56px"></asp:TextBox>
     <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox" Width="3px" Text=":" Enabled="false"></asp:TextBox>
      <asp:TextBox ID="txtToMinute1" runat="server" CssClass="textbox" Width="54px"></asp:TextBox>
</td>
</tr>
<tr align="center">
<td colspan=2>
    <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button" onclick="lnkUpdate_Click">Submit</asp:LinkButton> 
    <asp:Label ID="lblId" runat="server" Text="" Visible="false"></asp:Label>   
      <asp:LinkButton ID="lnkCancle" runat="server" CssClass="button" >Cancle</asp:LinkButton>
</td>
</tr>
</table>
    </asp:Panel>
     <asp:Button ID="Button1" runat="server" Text="Button" style="display:none" />
            <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="lnkCancle" DynamicServicePath=""
                Enabled="True" PopupControlID="Panel1" TargetControlID="Button1" BackgroundCssClass="popup_bg">
            </asp:ModalPopupExtender>
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
                 <asp:Button ID="btnyes" runat="server" CausesValidation="False" 
                                 onclick="btnYes_Click" Text="Yes" CssClass="button"  />
                                 &nbsp;&nbsp;
                                <asp:Button ID="btnNo" runat="server" CausesValidation="False" Text="No" CssClass="button" /> 
                                 
                                 </td>
             </tr>
             </table>

    </asp:Panel>
     <asp:Button ID="Button2" runat="server" Text="Button" style="display:none" />
            <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="btnNo" DynamicServicePath=""
                Enabled="True" PopupControlID="Panel2" TargetControlID="Button2" BackgroundCssClass="popup_bg">
            </asp:ModalPopupExtender>
    </div>
</div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

