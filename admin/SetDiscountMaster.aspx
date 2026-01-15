<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SetDiscountMaster.aspx.cs" Inherits="admin_RulesForDiscount" %>
<%@ Register Src="~/admin/usercontrol/loader.ascx" TagPrefix="uc4" TagName="loader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <uc4:loader runat="server" ID="loader" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="table">
                <tr>
                    <td colspan="2">
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                            <asp:ListItem Value="S">Sibling</asp:ListItem>
                            <asp:ListItem Value="Y">Yearly</asp:ListItem>
                            <asp:ListItem Value="O">Other</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drpDiscountHead" runat="server" onchange="return ValidateDropdown('.validatedropdown')" Class="textbox validatedropdown" AutoPostBack="True" OnSelectedIndexChanged="drpDiscountHead_SelectedIndexChanged"></asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" InitialValue="<--Select-->" SetFocusOnError="True" ControlToValidate="drpDiscountHead" runat="server" CssClass="imp" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table class="table">
                <tr>
                    <td id="column1" colspan="4" style="display: none" runat="server">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" onchange="ShowhidehtmlControl(this,'individual','ContentPlaceHolder1_classes','Individual','Class');ShowhidehtmlControl(this,'individual','ContentPlaceHolder1_gender','Individual','Class');">
                            <asp:ListItem Value="Individual">Individual</asp:ListItem>
                            <asp:ListItem Value="Class">Class</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="individual" style="display: none">
                    <td>
                        <asp:DropDownList ID="drpEnter" runat="server" CssClass="textbox">
                            <asp:ListItem Value="srno">S.R.No.</asp:ListItem>
                            <asp:ListItem Value="StEnRCode">Enrollment No.</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr id="classes" style="display: none" runat="server">
                    <td>From Class<span class="vd_red">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="DrpFromClass" runat="server" onchange="return ValidateDropdown('.validatedropdown')" CssClass="textbox validatedropdown"></asp:DropDownList>
                        <%--<uc1:drpClass runat="server" ID="DrpFromClass" />--%>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" InitialValue="<--Select-->" 
                             CssClass="imp"  ControlToValidate="DrpFromClass"
                            runat="server" ErrorMessage="*" ></asp:RequiredFieldValidator>--%>
                    </td>
                    <td>To Class
                    </td>
                    <td>
                        <asp:DropDownList ID="DrpToClass" runat="server" CssClass="textbox"></asp:DropDownList>
                        <%--<uc1:drpClass runat="server" ID="DrpToClass" />--%>
                    </td>
                </tr>
                <tr id="gender" style="display: none" runat="server">
                    <td>Gender<span class="vd_red">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="DrpGender" runat="server" CssClass="textbox"></asp:DropDownList>
                        <%--<uc2:drpGender runat="server" ID="DrpGender" />--%>
                    </td>
                    <td>Category<span class="vd_red">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="DrpCategory" runat="server" CssClass="textbox"></asp:DropDownList>
                        <%--<uc3:drpCategory runat="server" ID="DrpCategory" />--%>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <table class="table">
                <tr>
                    <td>Remark
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="textbox" TextMode="MultiLine" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table class="Grid" width="100%" id="table1">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <HeaderTemplate>

                                    <tr>
                                        <th>Installment
                                        </th>
                                        <th>
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" onkeyup="CheckDecimalValue(event, this);CopyRepeaterHeaderText(this,'table1');" onblur="CheckDecimalValue(event, this);CopyRepeaterHeaderText(this,'table1');return ValidateRepeater('table1');" placeholder="Set Amount"></asp:TextBox>
                                        </th>
                                    </tr>

                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Installment") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>
            </table>
             <asp:LinkButton ID="LinkButton1" runat="server" Class="button" ValidationGroup="a" OnClientClick="ValidateDropdown('.validatedropdown');ValidateRepeater('table1');return validationReturn();" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton> 
        </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>

