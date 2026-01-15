<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PeriodTypeMaster.aspx.cs" Inherits="admin_TypeofPeriodMaster" %>

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
             <div ID="abc" runat="server">
              <table CssClass="table">
                  <tr>
  <td>
  Type of Period : <span style="color:Red">*</span>
  </td>
  <td>
      <asp:TextBox ID="txtPeriodtype" runat="server" CssClass="textbox" Width="200"></asp:TextBox>
  </td>
   <td>
         &nbsp;
  </td>
  </tr>
                  <tr>
  <td>
  Short Name :
  </td>
  <td>
      <asp:TextBox ID="txtShortName" runat="server" CssClass="textbox" Width="200"></asp:TextBox>
  </td>
   <td>
       <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" onclick="lnkSubmit_Click">Submit</asp:LinkButton>
  </td>
  </tr>
                 </table>
             </div>
                
  <table width="100%">
  <tr>
  <td>
      <asp:GridView ID="Grd" runat="server" CssClass="Grid" AutoGenerateColumns="false"  width="100%">
      <Columns>
      <asp:TemplateField HeaderText="#">
      <ItemTemplate>
          <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
       <asp:TemplateField HeaderText="Period Type">
      <ItemTemplate>
          <asp:Label ID="lblPtyte" runat="server" Text='<%# Bind("PTypeName") %>'></asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
        <asp:TemplateField HeaderText="Short Name">
      <ItemTemplate>
          <asp:Label ID="lblShortName" runat="server" Text='<%# Bind("ShortName") %>'></asp:Label>
      </ItemTemplate>
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
  <div style="overFlow:auto; width:1px; height:1px" >
      <asp:Panel ID="Panel1" runat="server" CssClass="popup">
      <table class="table">
  <tr>
  <td>
  Type of Period : <span style="color:Red">*</span>
  </td>
  <td>
      <asp:TextBox ID="txtPeriodtype1" runat="server" CssClass="textbox" Width="200"></asp:TextBox>
      <asp:Label ID="lblId" runat="server" Text="" Visible="false"></asp:Label>
  </td>
   <td>
         &nbsp;
  </td>
  </tr>
  <tr>
  <td>
  Short Name :
  </td>
  <td>
      <asp:TextBox ID="txtShortName1" runat="server" CssClass="textbox" Width="200"></asp:TextBox>
  </td>
   <td>
      &nbsp;
  </td>
  </tr>
  <tr>
  <td>
        &nbsp;
  </td>
  <td>
  <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button" onclick="lnkUpdate_Click">Submit</asp:LinkButton>    
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

