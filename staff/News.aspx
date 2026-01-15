<%@ Page Title="" Language="C#" MasterPageFile="~/staff/staff_root-manager.master" AutoEventWireup="true"CodeFile="News.aspx.cs" Inherits="News" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
<div class="maincontent">
<div class="codepart">
<div class="hedingbg">
<h3 class="h3txt">Staff</h3></div>
<div class="hedingline"><h4 class="h4txt">News Master</h4></div>
<div class="contentbox">
<%-- ReSharper disable once Html.TagNotClosed --%>
<table align="center" style="margin-top:5px;" cellpadding="0" cellspacing="0" border="0" width="99%">
                        
                        <tr>
                            <td class="trgap2"></td>
                        </tr>
                        <tr>
                            <td>
                            <%-- ReSharper disable once Html.TagNotClosed --%>
                                 <table cellpadding="0" cellspacing="0" width="75%" align="center">
                                     <tr>
                                         <td>
                                         <%-- ReSharper disable once Html.TagNotClosed --%>
                                 <table style="margin-top:5px;" cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td width="35%" align="right">&nbsp;</td>
                                        <td width="50%" align="left">
                                            &nbsp;</td>
                                    </tr> 
                                     <tr>
                                        <td colspan="2" class="trgap1"></td>
                                    </tr>
                                    <tr>
                                        <td width="35%" align="right" valign="top">
                                            Title :</td>
                                        <td width="50%" align="left">
                                            <asp:TextBox ID="txtttle" SkinID="TxtBoxDef" runat="server" Height="22px" 
                                                Width="300px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txtttle" ErrorMessage="*" style="color: #CC0000"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="trgap1"></td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" width="35%" valign="top">Description :</td>
                                        <td width="50%" align="left">
                                           <asp:TextBox ID="txtdes" runat="server" SkinID="txtmulti" TextMode="MultiLine" 
                                                Width="300px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="txtdes" ErrorMessage="*" style="color: #CC0000"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td  colspan="2" class="trgap1"></td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" width="35%" valign="top">From :</td>
                                        <td width="50%" align="left" style="padding-left:5px;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpYear" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="DrpYear_SelectedIndexChanged" SkinID="ddlSize2">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpMonth" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="DrpMonth_SelectedIndexChanged" SkinID="ddlSize1">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDate" runat="server" AutoPostBack="True" 
                                                        SkinID="ddlSize0">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="trgap1" colspan="2"></td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" width="35%" valign="top">To :</td>
                                        <td width="50%" align="left" style="padding-left:5px;">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpYear1" 
    runat="server" SkinID="ddlSize2" 
                                                AutoPostBack="True" 
    onselectedindexchanged="DrpYear1_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpMonth1" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="DrpMonth1_SelectedIndexChanged" SkinID="ddlSize1">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDate1" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="DrpDate1_SelectedIndexChanged" SkinID="ddlSize0">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td  colspan="2" class="trgap1"></td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" width="35%" valign="top">&nbsp;</td>
                                        <td width="50%" align="left">
                                             <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                                                 RepeatDirection="Horizontal">
                                                 <asp:ListItem Selected="True">Student</asp:ListItem>
                                                 <asp:ListItem>Guardian</asp:ListItem>
                                             </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="trgap1"></td>
                                    </tr>
                                     <tr valign="top">
                                        <td align="right" width="35%"></td>
                                        <td width="50%" align="left" style="padding-left:5px;">
                                            <asp:LinkButton ID="LinkButton1" SkinID="Submit" runat="server" 
                                                onclick="LinkButton1_Click"></asp:LinkButton>
                                        </td>
                                    </tr>   
                                     <tr valign="top">
                                        <td align="right" width="35%" colspan="2" style="width: 85%; text-align: left">
                                        
                                        
                                        
                <%--   <div style="overflow: auto; width: 1px; height: 1px">--%>

                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        
                        <table border="0" align="center" cellpadding="0" cellspacing="0" 
                            bgcolor="#217AA2" width="500" height="272px">
  <tr>
    <td background="../images/pannel_03.jpg" align="center" >
        <table align="center" bgcolor="White" cellpadding="0" cellspacing="0" 
            width="490">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" width="30%">
                    Title :
                </td>
                <td style="text-align: left">
                    <asp:Button ID="Button5" runat="server" Style="display:none" />
                    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" 
                        CancelControlID="Button4" PopupControlID="Panel1" TargetControlID="Button5">
                    </asp:ModalPopupExtender>
                    <asp:TextBox ID="txtTitlePanel" runat="server" Height="22px" SkinID="TxtBoxDef" 
                        Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2" class="trgap1">
                    </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    Description :</td>
                <td align="left">
                    <asp:TextBox ID="txtDescriptionPanel" runat="server" SkinID="txtmulti" 
                        TextMode="MultiLine" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2" class="trgap1">
                    </td>
            </tr>
            <tr>
                <td align="right">
                    From :</td>
                <td style="text-align: left; padding-left:5px;">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drpYYPanelFrom" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="drpYYPanelFrom_SelectedIndexChanged" SkinID="ddlSize2">
                            </asp:DropDownList>
                            <asp:DropDownList ID="drpMMPanelFrom" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="drpMMPanelFrom_SelectedIndexChanged" SkinID="ddlSize1">
                            </asp:DropDownList>
                            <asp:DropDownList ID="drpDDPanelFrom" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="drpDDPanelFrom_SelectedIndexChanged" SkinID="ddlSize0">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2" class="trgap1">
                    </td>
            </tr>
            <tr>
                <td align="right">
                    To :</td>
               <td style="text-align: left; padding-left:5px;">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drpYYTo" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="drpYYTo_SelectedIndexChanged" SkinID="ddlSize2">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DrpMMToPanel" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="DrpMMToPanel_SelectedIndexChanged" SkinID="ddlSize1">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DrpDDToPanel" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="DrpDate1_SelectedIndexChanged" SkinID="ddlSize0">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
               <td align="right" colspan="2" class="trgap1">
                    </td>
            </tr>
            <tr>
                <td align="right">
                    Category :</td>
                <td align="left">
                    <asp:CheckBoxList ID="CheckBoxList2" runat="server" 
                        onselectedindexchanged="CheckBoxList2_SelectedIndexChanged" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="Student">Student</asp:ListItem>
                        <asp:ListItem>Guardian</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2" height="25">
                    </td>
            </tr>
            <tr>
                <td align="right">
                </td>
               <td style="text-align: left; padding-left:5px;">
                    <asp:Button ID="Button3" runat="server" CausesValidation="False" 
                        CssClass="black" onclick="Button3_Click" Text="Update" />
                    &nbsp;
                    <asp:Button ID="Button4" runat="server" CausesValidation="False" 
                        CssClass="black" onclick="Button4_Click" Text="Cancel" />
                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2" class="trgap1">
                    </td>
            </tr>
        </table>
      </td>
  </tr>
</table>

                    </asp:Panel>
                    
                                       
                                           
                                        <%-- ReSharper disable once Html.TagNotClosed --%>
                    </div>
                                         </td>
                                    </tr>   
                                     <tr valign="top">
                                        <td align="right" width="35%" colspan="2" style="width: 85%; text-align: left">


                    <div style="overflow: auto; width: 1px; height: 1px">



             <asp:Panel ID="Panel2" runat="server" Width="100%">
                   
                   
                    <table width="450px" bgcolor="#25789D" cellpadding="0" cellspacing="0"  height="100px" align="center">
             <tr>
             <td>
             <table width="440px" bgcolor="White" cellpadding="0" cellspacing="0" 
                     align="center" >
                        <tr>
                            <td colspan="2" align="center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                Do you really want to delete this record?<asp:Label ID="lblvalue" 
                                    runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="Button7" runat="server" style="display:none" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-right:5px;" >
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" 
                                    onclick="btnDelete_Click" Text="Yes" />
                            </td>
                            <td style="padding-left:5px;" align="left">
                                <asp:Button ID="Button8" runat="server" onclick="Button8_Click" Text="No" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-right:3px;">
                                &nbsp;</td>
                            <td align="left" style="padding-left:3px;">
                                &nbsp;</td>
                        </tr>
                    </table>
             </td>
             </tr>
             </table>
                </asp:Panel>                  
                                    
                                        
                                          <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" 
                       DynamicServicePath="" Enabled="True" TargetControlID="Button7" PopupControlID="Panel2" CancelControlID="Button8">
                   </asp:ModalPopupExtender>
                   </div>
                                         </td>
                                    </tr>   
                                 </table>
                                         </td>
                                     </tr>
                                 </table>
                            </td>
                        </tr>
                        <tr>
                             <td >
                                 &nbsp;</td>
                        </tr>
                        <tr>
                             <td >
                               &nbsp;
                            </td>
                        </tr>
                        <tr>
                             <td align="center">
                                <asp:GridView ID="Grd" runat="server" CssClass="Gridview_Border" 
                                     AutoGenerateColumns="False" Width="100%">
                                 <AlternatingRowStyle CssClass="grid_alt_details_default"  />
                                     <Columns>
                                         <asp:TemplateField HeaderText="S. No.">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                             <ItemStyle HorizontalAlign="Center" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Title">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label2" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="From Date">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label3" runat="server" Text='<%# Bind("FromDate") %>'></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="75px" />
                                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="To Date">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label4" runat="server" Text='<%# Bind("ToDate") %>'></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="75px" />
                                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Edit">
                                             <ItemTemplate>
                                                 <asp:LinkButton ID="LinkButton2" SkinID="Edit" runat="server" 
                                                     CausesValidation="False" onclick="LinkButton2_Click" 
                                                     Text='<%# Bind("NewsId") %>'></asp:LinkButton>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                                                 Width="30px" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Delete">
                                             <ItemTemplate>
                                                 <asp:LinkButton ID="LinkButton3" SkinID="Delete" runat="server" 
                                                     onclick="LinkButton3_Click" Text='<%# Bind("NewsId") %>' 
                                                     CausesValidation="False"></asp:LinkButton>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                         </asp:TemplateField>
                                     </Columns>
                                 <HeaderStyle CssClass="grid_heading_default" />
                                 <RowStyle CssClass="grid_details_default"  /> 
                                 </asp:GridView>
                             </td>
                        </tr>
                        <tr>
                             <td >
                                 &nbsp;</td>
                        </tr>
                        </table>

</div>
</div>
</div>
</asp:Content>

