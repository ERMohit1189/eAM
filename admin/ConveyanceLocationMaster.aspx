<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ConveyanceLocationMaster.aspx.cs"
    Inherits="admin_BusLocationMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
  
    <%--Content starts--%>
    <table class="table">
        <tr>
            <td align="right">
                Vehicle Type <span class="vd_red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="DrpVehicleType" runat="server" AutoPostBack="True" 
                    OnSelectedIndexChanged="DrpVehicleType_SelectedIndexChanged" CssClass="textbox" Width="220px" 
                    >
                </asp:DropDownList>
            </td>
            <td align="right" >
                Vehicle No. <span class="vd_red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="drpvehicleno" runat="server" AutoPostBack="True"
                    CssClass="textbox" Width="220px">
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr>
       <td align="right">
                Location Name <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtlocationName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlocationName" ErrorMessage="Please enter location name."
                    Style="color: #CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td align="right" valign="top">
                Remark
            </td>
            <td>
                <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                One-Way Fare <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtonewayfare" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtonewayfare" ErrorMessage="Please enter one way fare."
                    Style="color: #CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtonewayfare" ErrorMessage="Enter only numeric value(0-9)."
                    Style="color: #CC0000" ValidationExpression="[0-9]*" ValidationGroup="a" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td align="right">
                Two-Way Fare <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txttwowayfare" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txttwowayfare" ErrorMessage="Please enter two way fare."
                    Style="color: #CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txttwowayfare" ErrorMessage="Enter only numeric value(0-9)."
                    Style="color: #CC0000" ValidationExpression="[0-9]*" ValidationGroup="a" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button">Submit</asp:LinkButton>
            </td>
        </tr>
    </table>
    <div style="padding-right: 15px; text-align: right; padding-bottom: 4px;">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/export_word_icon.gif" title="Export to Word" />&nbsp;
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/export_excel_icon.gif" title="Export to Excel" />&nbsp;
        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/export_pdf_icon.gif" title="Export to PDF" />&nbsp;
        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/print_icon.gif" title="Print" />
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Grid" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Vehicle Type">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("VehicleType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Name">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("BusLocationName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Vehicle No.">
                <ItemTemplate>
                    <asp:Label ID="lblvehicleno" runat="server" Text='<%# Bind("VehicleNo") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="One-way Fare">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("OneWayFare") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Two-way Fare">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("TwowayFare") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click" Text='<%# Bind("Id") %>' 
                        CssClass="edit" Font-Size="0pt" Height="16px" Width="16px"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" Text='<%# Bind("Id") %>' 
                        CssClass="delete" Font-Size="0pt" Height="16px" Width="16px"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="40px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="overflow: auto; width: 2px; height: 1px">
        <asp:Panel ID="Panel1" runat="server" CssClass="popup">
            <table width="100%" class="table">
                <tr>
                    <td align="right">
                        Vehicle type :
                    </td>
                    <td>
                        <asp:DropDownList ID="DrpVehicleType0" runat="server" CssClass="textbox" Width="220px" 
                            onselectedindexchanged="DrpVehicleType0_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Vehicle No. :
                    </td>
                    <td>
                        <asp:DropDownList ID="drpvehicleno0" runat="server" CssClass="textbox" Width="220px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Location Name :
                    </td>
                    <td>
                        <asp:TextBox ID="txtlocationName0" runat="server" ReadOnly="True" CssClass="textbox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        One-way Fare :
                    </td>
                    <td>
                        <asp:TextBox ID="txtonewayfare0" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Two way Fare :
                    </td>
                    <td>
                        <asp:TextBox ID="txttwowayfare0" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Remark :
                    </td>
                    <td>
                        <asp:TextBox ID="txtremark0" runat="server" TextMode="MultiLine" CssClass="textbox" Height="50px" Width="200px"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" OnClick="LinkButton4_Click" CssClass="button">Update</asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" CssClass="button">Cancel</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
            <asp:Button ID="Button9" runat="server" Style="display: none" />
        </asp:Panel>
        <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button9" PopupControlID="Panel1"
            CancelControlID="LinkButton5" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
        </asp:ModalPopupExtender>
    </div>
    <div style="overflow: auto; width: 2px; height: 1px">
        <asp:Panel ID="Panel2" runat="server" CssClass="popup">
            <table width="100%">
                <tr>
                    <td align="center" height="50">
                        <h4>
                            Do you really want to delete this record?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                            <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                    </td>
                </tr>
                <tr>
                    <td align="center" height="50">
                        <asp:Button ID="btnDelete" runat="server" CssClass="button" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" />
                        &nbsp;&nbsp;
                        <asp:Button ID="Button8" runat="server" CssClass="button" CausesValidation="False" Text="No" />
                    </td>
                </tr>
            </table>
            <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button8" DynamicServicePath=""
                Enabled="True" PopupControlID="Panel2" TargetControlID="Button7" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
            </asp:ModalPopupExtender>
        </asp:Panel>
    </div>
</asp:Content>
