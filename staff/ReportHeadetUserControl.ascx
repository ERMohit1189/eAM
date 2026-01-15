<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportHeadetUserControl.ascx.cs" Inherits="ReportHeadetUserControl" %>

<style type="text/css">
    .style1
    {
        height: 16px;
    }
    </style>

<table align="left" cellpadding="0" cellspacing="0" width="80%">


    <tr>
        <td valign="top" width="150px" align="left">
            <asp:Image ID="Image1" runat="server" Height="75px" Width="75px"  />
        </td>
        <td align="left" valign="top">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
            <asp:Label ID="Label1" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="18px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
            <asp:Label ID="Label2" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12pt"></asp:Label>
            <asp:Label ID="Label8" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style1">
                        <asp:Label ID="Label12" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12pt" style="font-family: Arial; font-size: medium"></asp:Label>
                        <asp:Label ID="Label13" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12pt"></asp:Label>
                        <asp:Label ID="Label14" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12pt"></asp:Label>
                        </td>
                </tr>
                <tr>
                    <td align="center" class="style1">
                        <asp:Label ID="Lbleph" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12px" style="font-weight: 700"></asp:Label>
            <asp:Label ID="Label7" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12px"></asp:Label>
                        <asp:Label ID="Lblemail" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12px" style="font-weight: 700"></asp:Label>
            <asp:Label ID="Label9" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblWebsite" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12px" style="font-weight: 700"></asp:Label>
                        <asp:Label ID="Label11" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12px" style="font-family: Arial; font-size: small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="Label15" runat="server" CssClass="address" Font-Names="Arial" 
                            Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                </table>
        </td>
    </tr>
</table>



