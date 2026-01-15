<%@ Page Title="Dashboard | eAM&reg;" Language="C#" MasterPageFile="~/15/mainRootManager.master"  AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        .orange {
            background: #d53e43 !important;
            color: white !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix" style="min-height:600px;">
        <div class="row">
            <div id="w1" runat="server"></div>
        </div>
        <div class="row text-center">
            <h1 style="text-transform:uppercase;">Welcome to Alumni Portal</h1>
            <h2>Helpline Number: +91 9956208333</h2>
            
        </div>
    </div>
</asp:Content>