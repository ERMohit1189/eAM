<%@ Page Title="" Language="C#" MasterPageFile="~/administrator/administrato_root-manager.master" AutoEventWireup="true" CodeFile="TruncateTable.aspx.cs" Inherits="admin_TruncateTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
    .style2
    {
        width: 166px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="maincontent">
        <div class="codepart">
            <div class="hedingbg">
                <h3 class="h3txt">
                    Student</h3>
            </div>
            <div class="hedingline">
                <h4 class="h4txt">
                    Delete All Students </h4>
            </div>
            <div class="contentbox">
                <table class="style1">
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Enter Table Name</td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Delete</asp:LinkButton>
                        </td>
                    </tr>
                    </table>
            </div>
            </div>
            </div>
 </asp:Content>

