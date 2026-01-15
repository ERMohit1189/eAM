<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MemberCategoryFacultyLibrary.aspx.cs"
    Inherits="admin_MemberCategoryLibrary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
  
    <%--Content starts--%>
    <table class="table">
        <tr>
            <td align="right">
                Fine For Library(per day) <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtFineLibraryPerDay" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
            <td>
                Maximum Items For Library <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtMaximumitemLibrary" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Days Of Return For Library <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtdaysRetLibrary" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
            <td>
                Membership Validity(in months) <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtMembershipValidityMonth" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                Remark
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="textbox" Height="50px" Width="650px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" AccessKey="s" CssClass="button">Submit</asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
