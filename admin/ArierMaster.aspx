<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ArierMaster.aspx.cs" Inherits="admin_ArierMaster"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
   
    <%--Content starts--%>
    <table class="table">
    <tr>
    <td>
    Select
    </td>
    <td>
        <asp:DropDownList ID="DrpEnter" runat="server" OnSelectedIndexChanged="DrpEnter_SelectedIndexChanged" 
            CssClass="textbox" Width="200px">
            <asp:ListItem Value="srno">S.R. No.</asp:ListItem>
            <asp:ListItem Value="StEnRCode">Enrollment  No.</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td>Enter</td>
    <td>
        <asp:TextBox ID="TxtEnter" runat="server" OnTextChanged="TxtEnter_TextChanged" CssClass="textbox" Width="200px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtEnter" ErrorMessage="Please enter Sr.No./Enrollment."
            SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="A" Display="None"></asp:RequiredFieldValidator><span class="vd_red">*</span></td>
            <td>
                <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" CssClass="button">View</asp:LinkButton>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" 
                    ValidationGroup="A" />
        </td>
    </tr>
        
    </table>

<asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="Grid"  Width="100%">
                                     <AlternatingRowStyle CssClass="alt" />
                                     <Columns>
                                         <asp:TemplateField HeaderText="S.R. No.">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label31" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Enrollment No.">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Name">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label32" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                 <asp:Label ID="Label33" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                                                 <asp:Label ID="Label23" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Father's Name">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Class">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label5" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="85px" />
                                             <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Bottom" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Section">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label6" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle Width="70px" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Medium">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label28" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Admission Date ">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label7" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Transport">
                                             <ItemTemplate>
                                                 <asp:Label ID="Label30" runat="server" Text='<%# Bind("TransportRequired") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                     </Columns>
                                     <HeaderStyle CssClass="grid_heading_default" />
                                     <RowStyle CssClass="grid_details_default" />
                                 </asp:GridView>

<asp:Panel ID="Panel3" runat="server">
                                     <table style="margin-top:5px;" cellpadding="0" 
    cellspacing="0" border="0" width="100%" align="center">
                                         <tr>
                                             <td align="right" colspan="2" style="text-align: left">
                                                 &nbsp;</td>
                                         </tr>
                                         <tr>
                                             <td width="50%" align="right">
                                                 Arrears&nbsp; Amount <span class="vd_red">*</span></td>
                                             <td width="50%" align="left">
                                                 <asp:TextBox ID="txtArrearAmt" runat="server" CssClass="textbox"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtArrearAmt" ErrorMessage="*" SetFocusOnError="True" 
                                    style="color: #CC0000" ValidationGroup="A"></asp:RequiredFieldValidator>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td width="50%" align="right" class="trgap1" colspan="2" 
                                             style="width: 100%">
                                                 &nbsp;</td>
                                         </tr>
                                         <tr>
                                             <td width="50%" align="right">
                                                 Arrears Session </td>
                                             <td width="50%" align="left">
                                                 <asp:DropDownList ID="drpSession" runat="server" CssClass="textbox">
                                                 </asp:DropDownList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td width="50%" align="right" class="trgap1" colspan="2">
                                                 &nbsp;</td>
                                         </tr>
                                         <tr>
                                             <td width="50%" align="right">
                                                 Deposit Date </td>
                                             <td width="50%" align="left">
                                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                     <ContentTemplate>
                                                         <asp:DropDownList ID="drpYY" runat="server" CssClass="textbox">
                                                         </asp:DropDownList>
                                                         <asp:DropDownList ID="drpMM" runat="server" CssClass="textbox">
                                                         </asp:DropDownList>
                                                         <asp:DropDownList ID="drpDD" runat="server" CssClass="textbox">
                                                         </asp:DropDownList>
                                                     </ContentTemplate>
                                                 </asp:UpdatePanel>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td colspan="2" class="trgap1">
                                             </td>
                                         </tr>
                                         <tr>
                                             <td width="50%" align="right" valign="top">
                                                 Remark </td>
                                             <td width="50%" align="left">
                                                 <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" 
                                                     CssClass="textbox"></asp:TextBox>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td colspan="2" class="trgap1">
                                             </td>
                                         </tr>
                                         <tr valign="top">
                                             <td align="right">
                                             </td>
                                             <td align="left" style="padding-left:5px;">
                                                 <asp:LinkButton ID="LinkButton1" SkinID="save" runat="server" 
                                                onclick="LinkButton1_Click" ValidationGroup="A"></asp:LinkButton>
                                             </td>
                                         </tr>
                                         <tr valign="top">
                                             <td align="right" width="50%" colspan="2" style="width: 100%; text-align: left">
                                             </td>
                                         </tr>
                                         <tr valign="top">
                                             <td align="right" width="50%" colspan="2" style="width: 100%; text-align: left">
                                             </td>
                                         </tr>
                                     </table>
                                 </asp:Panel>
                             
<div style="overflow: auto; width: 1px; height: 1px">
<asp:Panel ID="Panel2" runat="server" CssClass="popup">

             <table width="100%">
             <tr>
             <td><h3>Do you really want to delete this record?<asp:Label ID="lblvalue" runat="server" 
                                    Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" style="display:none" /></h3></td>
             </tr>
             <tr>
             <td align="center"><asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                                    Text="Yes" CausesValidation="False" /> &nbsp;&nbsp;
                 <asp:Button ID="Button8" runat="server" Text="No" onclick="Button8_Click" />                   
                                    </td>
             </tr>
             </table>

                </asp:Panel>                  
                               <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" 
                       DynamicServicePath="" Enabled="True" TargetControlID="Button7" PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                   </asp:ModalPopupExtender>      
                   </div>

<div style="overflow: auto; width: 1px; height: 1px">
<asp:Panel ID="Panel1" runat="server" CssClass="popup">
 <table width="100%" class="table">
 <tr>
 <td align="right">Arrears Amount </td>
 <td><asp:TextBox ID="txtArearAmtPanel" runat="server" CssClass="textbox" 
         Width="200px"></asp:TextBox>
                     <asp:Button ID="Button5" runat="server" Style="display:none" /></td>
 </tr>
 <tr>
 <td align="right">Arrears Session </td>
 <td><asp:DropDownList ID="drpSessionPanel" runat="server" CssClass="textbox" 
         Width="220px">
                     </asp:DropDownList></td>
 </tr>
 <tr>
 <td align="right">Deposit Date </td>
 <td><asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drpYYP" runat="server" CssClass="textbox">
                            </asp:DropDownList>
                            <asp:DropDownList ID="drpMMP" runat="server" CssClass="textbox">
                            </asp:DropDownList>
                            <asp:DropDownList ID="drpDDP" runat="server" CssClass="textbox">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel></td>
 </tr>
 <tr>
 <td align="right" valign="top">Remark </td>
 <td><asp:TextBox ID="txtRemarkPanel" runat="server" 
                        TextMode="MultiLine" CssClass="textbox" Height="50px" 
         Width="200px"></asp:TextBox></td>
 </tr>
 <tr>
 <td colspan="2" align="center"><asp:Button ID="Button3" runat="server" CausesValidation="False" 
                        CssClass="black" onclick="Button3_Click" Text="Update" />
                    &nbsp;
                    <asp:Button ID="Button4" runat="server" CausesValidation="False" 
                        CssClass="black" onclick="Button4_Click" Text="Cancel" />
                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label></td>
 
 </tr>
 
 </table>
</asp:Panel>
                    
<asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5"
PopupControlID="Panel1" CancelControlID="Button4" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
</asp:ModalPopupExtender>
                                          
                                                
                                          
                    </div>

<asp:GridView ID="GrdDiscountDetails" runat="server" CssClass="Gridview_Border" 
AutoGenerateColumns="False" Width="100%">
                        <AlternatingRowStyle CssClass="grid_alt_details_default"  />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="Label34" runat="server" 
                  Text='<%# Bind("Sno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="Label38" runat="server" Text='<%# Bind("ArrierDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S.R. No.">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Enrollment No">
                                <ItemTemplate>
                                    <asp:Label ID="Label35" runat="server" 
                  Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Class">
                                <ItemTemplate>
                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section">
                                <ItemTemplate>
                                    <asp:Label ID="lblSection" runat="server" Text='<%# Bind("Section") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Arrears Amount">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ArrearAmt") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Arrears Session">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ArrierSession") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="Label37" runat="server" 
                  Text='<%# Bind("Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" SkinID="Edit" runat="server" 
         CssClass="linkbtn2edit" onclick="LinkButton2_Click" 
         Text='<%# Bind("ArrierId") %>' CausesValidation="False"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton3" SkinID="Delete" runat="server" 
         CssClass="linkbtn1delete" onclick="LinkButton3_Click" 
         Text='<%# Bind("ArrierId") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="grid_heading_default" />
                        <RowStyle CssClass="grid_details_default"  />
                    </asp:GridView>

</asp:Content>

