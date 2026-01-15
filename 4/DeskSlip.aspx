<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DeskSlip.aspx.cs" Inherits="DeskSlip" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Course&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" CssClass="form-control-blue ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpsection" runat="server" CssClass="form-control-blue " AutoPostBack="true" OnSelectedIndexChanged="drpsection_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 half-width-50 mgbt-xs-15" style="padding-top:24px;">
                                        <asp:LinkButton runat="server" ID="btnView" OnClick="btnView_Click" CssClass="btn vd_bg-blue vd_white form-control-blue">View</asp:LinkButton>
                                    </div>

                                    <div class="col-sm-12 mgbt-xs-10" runat="server" id="divshow" visible="False">
                                        <div style="float: right; font-size: 19px;">
                                           
                                            <span onclick="PrintDiv()" class="btn btn-sm btn-default" title="Print" style="cursor:pointer;"><i class="fa fa-print "></i>&nbsp;Print</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding">
                                    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30">
                                        <div id="divExport"  class="col-sm-12 no-padding print-row marg-bot-30">
                                            <div class=" table-responsive  table-responsive2">
                                                <asp:Repeater ID="rpStudentDetails" runat="server">
                                                    <ItemTemplate>
                                                        <div runat="server" style="padding:3px; width:33%; float:left;">
                                                        <div class="col-sm-12 print-row fee-d-box-nhl" runat="server"  style="padding:3px; height: 125px; border-radius: unset;">
                                                            <div class="col-sm-12"  style="padding: 0px;">
                                                                <table style="width: 100%;">
                                                                        <tr>
                                                                            <td class="text-left" style="width: 20%; padding:5px;">
                                                                                <asp:Label ID="Label9" runat="server" Text="Name" Font-Bold="False" Font-Size="13px"></asp:Label>
                                                                            </td>
                                                                            <td class="text-left" style="padding:5px;">:&nbsp; 
                                                                                <asp:Label ID="lblStudentName" runat="server" Font-Bold="True" Text='<%# Bind("Name") %>' Font-Names="Courier New" Font-Size="13px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="text-left" style="width: 20%; padding:5px;">
                                                                                <asp:Label ID="Label1" runat="server" Text="Class" Font-Bold="False" Font-Size="13px"></asp:Label>
                                                                            </td>
                                                                            <td class="text-left" style="padding:5px;">:&nbsp;
                                                                                <asp:Label ID="Label2" runat="server" Font-Names="Courier New" Font-Bold="True" Text='<%# Bind("CombineClassName") %>' Font-Size="13px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="text-left" style="width: 20%; padding:5px;">
                                                                                <asp:Label ID="Label10" runat="server" Text="S.R. No." Font-Bold="False" Font-Size="11px" style="white-space: nowrap;"></asp:Label>
                                                                            </td>
                                                                            <td class="text-left" style="padding:5px;">:&nbsp;
                                                                                <asp:Label ID="lblClass" runat="server" Font-Names="Courier New" Font-Bold="True" Text='<%# Bind("srno") %>' Font-Size="13px"></asp:Label>
                                                                                &nbsp;
                                                                                <asp:Label ID="Label14" runat="server" Text="Roll No." Font-Bold="False" Font-Size="11px"></asp:Label>
                                                                                :&nbsp;
                                                                                <asp:Label ID="lblMode" runat="server" Font-Names="Courier New" Font-Bold="True" Text='<%# Bind("RollNo") %>' Font-Size="13px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                            </div>
                                                        </div>
                                                        </div>
                                                        <div id="pagebreak" runat="server"></div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function PrintDiv() {
            
            var headContent = document.getElementsByTagName('head')[0].innerHTML;

            var divContents = document.getElementById("divExport").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000, class="tbls"');
            printWindow.document.write('<html><head><title>Examination Admit Card</title>' + headContent + '</head>');
            var TermNmae = $("[id*=drpEval]").val();
            if (TermNmae == 'Term1') {
                printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');
            }
            else {
                printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');
            }

            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 1500);
            return false;
            printWindow.close();

        }
    </script>
</asp:Content>

