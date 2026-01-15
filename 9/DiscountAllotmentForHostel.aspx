<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DiscountAllotmentForHostel.aspx.cs"
    Inherits="admin_DiscountAllotmentForHostel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            
            
             <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                               
                              <%--   <ContentTemplate>--%>
                                     <div class="col-sm-12 no-padding"  runat="server">
                                         <div class="col-sm-4  half-width-50 mgbt-xs-15 display-none">
                                                 <label class="control-label">Select</label>
                                                 <div class=" ">
                                                       <asp:DropDownList ID="DrpEnter" runat="server" OnSelectedIndexChanged="DrpEnter_SelectedIndexChanged" CssClass="form-control-blue">
                                                            <asp:ListItem Value="srno">S.R. No.</asp:ListItem>
                                                            <asp:ListItem Value="StEnRCode">Enrollment  No.</asp:ListItem>
                                                        </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                             </div>
                                             <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                               <%--  <label class="control-label"> <span class="vd_red">*</span></label>--%>
                                                 <div class=" ">
                                                       <asp:TextBox ID="TxtEnter" placeholder="Enter Name/ S.R. No." runat="server" OnTextChanged="TxtEnter_TextChanged" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                 </div>
                                            </div>
                                             <div class="col-sm-4  mgbt-xs-15">
                                                <asp:LinkButton ID="LinkButton7" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton7_Click" CssClass="button">View</asp:LinkButton>
                                                 <div id="msgbox" runat="server"  style="left:75px"></div>
                                             </div>
                                           
                                     </div>
                                <%-- </ContentTemplate>--%>
                            </div>
                        </div>
                   </div>
                </div>
            </div>
           
            <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="Grid" Width="100%">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <asp:Label ID="Label31" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
            </asp:GridView>
            <asp:Panel ID="Panel3" runat="server">
                <table class="table">
                    <tr>
                        <td>Discount Name <span class="vd_red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDiscName" runat="server" CssClass="form-control-blue" Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDiscName" ErrorMessage="*"
                                SetFocusOnError="True" Style="color: #FF0000" ValidationGroup="a"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Discount Type
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RdoDiscType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Percentage</asp:ListItem>
                                <asp:ListItem Selected="True">Amount</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>Discount Value <span class="vd_red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDiscValue" runat="server" CssClass="form-control-blue" Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDiscValue" ErrorMessage="*"
                                SetFocusOnError="True" Style="color: #FF0000" ValidationGroup="a"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Remark
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Height="50px" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblError" runat="server" Style="color: #FF0000"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button">Save</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>Discount Name
                            </td>
                            <td>
                                <asp:TextBox ID="txtDiscNamePanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td>Discount Type
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RdoAmountTypePanel" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem>Percentage</asp:ListItem>
                                    <asp:ListItem>Amount</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>Discount Value
                            </td>
                            <td>
                                <asp:TextBox ID="txtDiscountValuePanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center;">
                                <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                &nbsp;
                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="Button7" runat="server" Style="display: none" />
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align:center;">
                                <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CssClass="button-n" />

                                &nbsp;&nbsp; 
                                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Yes" CausesValidation="False" CssClass="button-y" />

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" TargetControlID="Button7" PopupControlID="Panel2"
                    CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>
            <asp:GridView ID="GrdDiscountDetails" runat="server" CssClass="Grid" AutoGenerateColumns="False" Width="100%">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <asp:Label ID="Label34" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                            <asp:Label ID="Label35" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
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
                    <asp:TemplateField HeaderText="Discount Name">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("DiscountName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount Type">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("DiscountType") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount Value">
                        <ItemTemplate>
                            <asp:Label ID="Label36" runat="server" Text='<%# Bind("DiscountValue") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate>
                            <asp:Label ID="Label37" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="edit" OnClick="LinkButton2_Click" Text='<%# Bind("DiscountId") %>'
                                CausesValidation="False" Height="16px" Width="16px"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="delete" OnClick="LinkButton3_Click" Text='<%# Bind("DiscountId") %>'
                                Height="16px" Width="16px"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
