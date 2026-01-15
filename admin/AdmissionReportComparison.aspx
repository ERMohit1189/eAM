<%@ Page Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AdmissionReportComparison.aspx.cs" Inherits="admin_StudentSessionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Admission Report Comparison</title>
      <style>
        table {
            width: 100%;
            border-collapse: collapse;
            text-align: center;
            margin-top: 20px;
        }

        th, td {
            border: 1px solid #000;
            padding: 8px;
        }

        th {
            background-color: #f2f2f2;
        }

        .main-header {
            font-size: 18px;
            font-weight: bold;
            text-align: center;
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>


    <div class="main-header">Admission Report Comparison (Last 5 Years)</div>

    <table>
        <thead>
            <tr>
                <th rowspan="2">Year</th>
                <th colspan="3">Admissions</th>
                <th colspan="4">Students Left</th>
            </tr>
            <tr>
                <th>New Students</th>
                <th>Old Students (Promoted)</th>
                <th>Total Admissions</th>
                <th>Withdrawal</th>
                <th>TC Issued</th>
                <th>Total Left</th>
                <th>Net Strength</th>
            </tr>
        </thead>
        <tbody>
             <asp:Literal ID="ltAdmissionBody" runat="server" EnableViewState="false"></asp:Literal>
        </tbody>
    </table>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
