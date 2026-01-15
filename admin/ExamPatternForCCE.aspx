<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ExamPatternForCCE.aspx.cs" Inherits="admin_ExamPatternForCCE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" Runat="Server">
    Create Header (Only for CCE, Aplicable For Each FA)
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:LinkButton ID="LinkButton1" runat="server">Submit</asp:LinkButton>
    <asp:Repeater ID="Repeater1" runat="server"></asp:Repeater>
</asp:Content>

