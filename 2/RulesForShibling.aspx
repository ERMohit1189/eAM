<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="RulesForShibling.aspx.cs" Inherits="admin_RulesForShibling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <table class="table">
        <tr>
            <td>Roule 1
            </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    <asp:ListItem Value="Both" Selected="True">Mother &amp; Father&#39;s Name</asp:ListItem>
                    <asp:ListItem Value="Mother">Mother&#39;s Name</asp:ListItem>
                    <asp:ListItem Value="Father">Father&#39;s Name</asp:ListItem>
                    <asp:ListItem Value="N/A">N/A</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>Roule 2
            </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList2" runat="server">
                    <asp:ListItem Value="Lower" Selected="True">Lower Class</asp:ListItem>
                    <asp:ListItem Value="Upper">Upper Class</asp:ListItem>
                    <asp:ListItem Value="N/A">N/A</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <table class="table">
        <tr>
            <td>Roule 3
            </td>
            <td>
                <ol style="line-height: 22px">
                    <li>
                        <asp:Label ID="Label1" runat="server" Text="Shibling"></asp:Label></li>
                    <li>
                        <asp:Label ID="Label2" runat="server" Text="Shibling"></asp:Label></li>
                    <li>
                        <asp:Label ID="Label3" runat="server" Text="Shibling"></asp:Label></li>
                    <li>
                        <asp:Label ID="Label4" runat="server" Text="Shibling"></asp:Label></li>
                    <li>
                        <asp:Label ID="Label5" runat="server" Text="Shibling"></asp:Label></li>
                    <li>
                        <asp:Label ID="Label6" runat="server" Text="Shibling"></asp:Label></li>
                </ol>
            </td>
            <td>
                <ul style="list-style: none">
                    <li>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Value="I">Individual</asp:ListItem>
                            <asp:ListItem Value="G">Group</asp:ListItem>
                        </asp:DropDownList></li>
                    <li>
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem Value="I">Individual</asp:ListItem>
                            <asp:ListItem Value="G">Group</asp:ListItem>
                        </asp:DropDownList></li>
                    <li>
                        <asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem Value="I">Individual</asp:ListItem>
                            <asp:ListItem Value="G">Group</asp:ListItem>
                        </asp:DropDownList></li>
                    <li>
                        <asp:DropDownList ID="DropDownList4" runat="server">
                            <asp:ListItem Value="I">Individual</asp:ListItem>
                            <asp:ListItem Value="G">Group</asp:ListItem>
                        </asp:DropDownList></li>
                    <li>
                        <asp:DropDownList ID="DropDownList5" runat="server">
                            <asp:ListItem Value="I">Individual</asp:ListItem>
                            <asp:ListItem Value="G">Group</asp:ListItem>
                        </asp:DropDownList></li>
                    <li>
                        <asp:DropDownList ID="DropDownList6" runat="server">
                            <asp:ListItem Value="I">Individual</asp:ListItem>
                            <asp:ListItem Value="G">Group</asp:ListItem>
                        </asp:DropDownList></li>
                </ul>
            </td>
            <td>
                <ul style="list-style: none">
                    <li>
                        <asp:TextBox ID="TextBox1" runat="server" Text="1" onKeyUp="validationFordiscountValue(this,'2');" onBlur="validationFordiscountValue(this,'2');"></asp:TextBox></li>
                    <li>
                        <asp:TextBox ID="TextBox2" runat="server" Text="1" onKeyUp="validationFordiscountValue(this,'3');" onBlur="validationFordiscountValue(this,'3');"></asp:TextBox></li>
                    <li>
                        <asp:TextBox ID="TextBox3" runat="server" Text="1" onKeyUp="validationFordiscountValue(this,'4');" onBlur="validationFordiscountValue(this,'4');"></asp:TextBox></li>
                    <li>
                        <asp:TextBox ID="TextBox4" runat="server" Text="1" onKeyUp="validationFordiscountValue(this,'5');" onBlur="validationFordiscountValue(this,'5');"></asp:TextBox></li>
                    <li>
                        <asp:TextBox ID="TextBox5" runat="server" Text="1" onKeyUp="validationFordiscountValue(this,'6');" onBlur="validationFordiscountValue(this,'6');"></asp:TextBox></li>
                    <li>
                        <asp:TextBox ID="TextBox6" runat="server" Text="1" onKeyUp="validationFordiscountValue(this,'7');" onBlur="validationFordiscountValue(this,'7');"></asp:TextBox></li>
                </ul>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center;">
                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>

