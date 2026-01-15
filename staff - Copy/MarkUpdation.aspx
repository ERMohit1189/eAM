<%@ Page Title="" Language="C#" MasterPageFile="~/staff/staff_root-manager.master" AutoEventWireup="true" CodeFile="MarkUpdation.aspx.cs" Inherits="staff_MarkUpdation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .modalPopup
        {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            /* ReSharper disable once CssNotResolved */
            xindex:-1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (charCode === 46) {
                var inputValue = $("#inputfield").val();
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode !== 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
</script>
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


<%----------------------------------------------------------------------------------------------------------------%>
<br />
   
            <table class="table">
      <tr>
      <td>
        Select Class: <span class="imp">*</span>
      </td>
        <td>
            <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True" 
                onselectedindexchanged="drpclass_SelectedIndexChanged" CssClass="textbox">
            </asp:DropDownList>
      </td>
      <td>
        Select Section:  <span class="imp">*</span>
      </td>
        <td>
            <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True" 
                onselectedindexchanged="drpsection_SelectedIndexChanged" CssClass="textbox">
            </asp:DropDownList>
      </td>
      </tr>
      <tr>
      <td>
          Select Evaluation:    <span class="imp">*</span>
      </td>
      <td>
            <asp:DropDownList ID="drpEval" runat="server" CssClass="textbox" 
                onselectedindexchanged="drpEval_SelectedIndexChanged">
                <asp:ListItem>FA1</asp:ListItem>
                <asp:ListItem>FA2</asp:ListItem>
                <asp:ListItem>SA1</asp:ListItem>
                <asp:ListItem>FA3</asp:ListItem>
                <asp:ListItem>FA4</asp:ListItem>
                <asp:ListItem>SA2</asp:ListItem>
            </asp:DropDownList>
      </td>
               <td>
          Select Subject:      <span class="imp">*</span>
      </td>
      <td>
            <asp:DropDownList ID="drpSubject" runat="server" CssClass="textbox" AutoPostBack="True" 
                onselectedindexchanged="drpSubject_SelectedIndexChanged">
            </asp:DropDownList>
      </td>
       
      
      </tr>
     

      </table>
      <table id="table1" runat="server">
      <tr>
      <td>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="Grid"
        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" 
                        OnRowUpdating="GridView1_RowUpdating">
    <Columns>
    <asp:TemplateField HeaderText="#">
<ItemTemplate>
    <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
    <asp:TemplateField HeaderText="S.R. No.">
<ItemTemplate>
    <asp:Label ID="Label16" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Student's Name">
<ItemTemplate>
    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>
<HeaderTemplate>
    <asp:Label ID="Label5" runat="server" Text="UT"></asp:Label>
    <br />
    <asp:Label ID="Label6" runat="server" Text="20"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
    <asp:Label ID="Label17" runat="server" Text='<%# Bind("UT") %>'></asp:Label>
</ItemTemplate>
<EditItemTemplate>
    <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" CssClass="textbox" Width="40px" Text='<%# Bind("UT") %>' OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ErrorMessage="*" ControlToValidate="TextBox1" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField >
<HeaderTemplate>
    <asp:Label ID="Label7" runat="server" Text="ACT"></asp:Label>
    <br />
    <asp:Label ID="Label8" runat="server" Text="15"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
    <asp:Label ID="Label18" runat="server" Text='<%# Bind("ACT1") %>'></asp:Label>
</ItemTemplate>
<EditItemTemplate>
    <asp:TextBox ID="TextBox2" runat="server" AutoPostBack="true" CssClass="textbox" Width="40px" Text='<%# Bind("ACT1") %>' OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
    ErrorMessage="*" ControlToValidate="TextBox2" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="ACT">
<HeaderTemplate>
    <asp:Label ID="Label9" runat="server" Text="ACT"></asp:Label>
    <br />
    <asp:Label ID="Label10" runat="server" Text="15"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
    <asp:Label ID="Label19" runat="server" Text='<%# Bind("ACT2") %>'></asp:Label>
</ItemTemplate>
<EditItemTemplate>
    <asp:TextBox ID="TextBox3" runat="server" AutoPostBack="true" CssClass="textbox" Width="40px" Text='<%# Bind("ACT2") %>' OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
    ErrorMessage="*" ControlToValidate="TextBox3" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField >
<HeaderTemplate>
    <asp:Label ID="Label11" runat="server" Text="H.W/C.W"></asp:Label>
    <br />
    <asp:Label ID="Label12" runat="server" Text="5"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
    <asp:Label ID="Label20" runat="server" Text='<%# Bind("HW_CW") %>'></asp:Label>
</ItemTemplate>
<EditItemTemplate>
    <asp:TextBox ID="TextBox4" runat="server" AutoPostBack="true" CssClass="textbox" Width="40px" Text='<%# Bind("HW_CW") %>' OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
    ErrorMessage="*" ControlToValidate="TextBox4" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="ATT">
<HeaderTemplate>
    <asp:Label ID="Label13" runat="server" Text="ATT"></asp:Label>
    <br />
    <asp:Label ID="Label14" runat="server" Text="5"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
    <asp:Label ID="Label21" runat="server" Text='<%# Bind("ATT") %>'></asp:Label>
</ItemTemplate>
<EditItemTemplate>
    <asp:TextBox ID="TextBox5" runat="server" AutoPostBack="true" CssClass="textbox" Width="40px" Text='<%# Bind("ATT") %>' OnTextChanged="TextBox5_TextChanged"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
    ErrorMessage="*" ControlToValidate="TextBox5" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Total">
<ItemTemplate>
    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="10%">
<ItemTemplate>
    <asp:Label ID="Label3" runat="server" Text='<%# Bind("TenPercent") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Gr.">
<ItemTemplate>
    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Grade") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Edit">
 <ItemTemplate>
<asp:LinkButton ID="lnkEdit" runat="server" 
CssClass="edit" Height="16px" Width="16px" Font-Size="0Px" CommandName="Edit" Enabled="false"></asp:LinkButton>
</ItemTemplate>
<EditItemTemplate>
    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" CssClass="button" ValidationGroup="a">Update</asp:LinkButton>
    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel" CssClass="button">Cancel</asp:LinkButton>
</EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Id" Visible="false">
<ItemTemplate>
    <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
    </Columns>
    </asp:GridView>
      </td>
      </tr>
      </table>
   

</asp:Content>



