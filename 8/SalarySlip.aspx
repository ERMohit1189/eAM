<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/admin_root-manager.master" CodeFile="SalarySlip.aspx.cs" Inherits="_8_SalarySlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
 
      <style>
       
        .title {
            text-align: center;
            font-weight: bold;
            font-size: 20px;
        }
        .subtitle {
            text-align: center;
            margin-bottom: 20px;
        }
        table {
            border-collapse: collapse;
            width: 100%;
            margin-bottom: 20px;
        }
        th, td {
            border: 1px solid #000;
            padding: 6px;
            text-align: center;
        }
        .section-title {
            font-weight: bold;
            margin-top: 20px;
        }
        .note {
            text-align: center;
            font-size: 13px;
            margin-top: 30px;
        }
        table {
               border-collapse: collapse;
            width: 100%;
            margin-top: 20px;
        }
        th, td {
            border: 1px solid #000;
            padding: 6px;
            text-align: center;
        }
        .header {
            background-color: #f0f0f0;
            font-weight: bold;
            text-align: left;
        }
    </style>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
     <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-2 mgbt-xs-15">
                                        <label class="control-label">Month</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged">
                                                <asp:ListItem>Jan</asp:ListItem>
                                                <asp:ListItem>Feb</asp:ListItem>
                                                <asp:ListItem>Mar</asp:ListItem>
                                                <asp:ListItem>Apr</asp:ListItem>
                                                <asp:ListItem>May</asp:ListItem>
                                                <asp:ListItem>Jun</asp:ListItem>
                                                <asp:ListItem>Jul</asp:ListItem>
                                                <asp:ListItem>Aug</asp:ListItem>
                                                <asp:ListItem>Sep</asp:ListItem>
                                                <asp:ListItem>Oct</asp:ListItem>
                                                <asp:ListItem>Nov</asp:ListItem>
                                                <asp:ListItem>Dec</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 mgbt-xs-15">
                                        <label class="control-label">Year</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-3 mgbt-xs-15">
                                         <label class="control-label">Employee</label>
                                         <div class="">
                                             <asp:DropDownList ID="ddlSalary" runat="server" CssClass="form-control-blue" >
                                              

                                             </asp:DropDownList>
                                             <div class="text-box-msg">
                                             </div>
                                         </div>
                                     </div>

                                    <div class="col-sm-2  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkCalculated" runat="server" OnClick="lnkCalculated_Click"  CssClass="button">View</asp:LinkButton>
                                        <div id="divMsg" runat="server" style="left: 47px;"></div>
                                    </div>
 <div class="col-sm-12  ">
                                     <div class="col-sm-12  mgbt-xs-10" id="btndivNew" runat="server" visible="false">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float: right; font-size: 19px;" id="Panel2" runat="server">


                                                  <%--  <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>--%>
                                  <%--                  <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>--%>
                               <%--                     <asp:LinkButton ID="ImageButton3" runat="server"  OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>--%>
                                                    <asp:LinkButton ID="ImageButton4" runat="server" OnClientClick="PrintOnlyDiv(); return false;"  CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

                                                    <script>

</script>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:PostBackTrigger ControlID="ImageButton1" />--%>
                                             <%--   <asp:PostBackTrigger ControlID="ImageButton2" />--%>
                                             <%--   <asp:PostBackTrigger ControlID="ImageButton3" />--%>
                                              <%--  <asp:PostBackTrigger ControlID="ImageButton4" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                        <div runat="server" id="divExport" visible="false">
                                         <div class="title"><asp:Label ID="lblSchoolName" runat="server" /></div>
                                         <div class="subtitle"><asp:Label ID="lblSchoolAddress" runat="server" /><br /><br /> <asp:Label ID="Label1" runat="server" /></div>
                                          <table>
    <tr>
        <td>Employee ID: <asp:Label ID="lblEmpID" runat="server" /></td>
        <td>Bank Account No.: <asp:Label ID="lblAccountNo" runat="server" /></td>
    </tr>
    <tr>
        <td>Name: <asp:Label ID="lblName" runat="server" /></td>
        <td>Branch & IFSC: <asp:Label ID="lblBranchIFSC" runat="server" /></td>
    </tr>
    <tr>
        <td>Designation: <asp:Label ID="lblDesignation" runat="server" /></td>
        <td>Bank Name: <asp:Label ID="lblBankName" runat="server" /></td>
    </tr>
    <tr>
        <td>Date of Joining: <asp:Label ID="lblDOJ" runat="server" /></td>
        <td>Payable Days: <asp:Label ID="lblPayableDays" runat="server" /></td>
    </tr>
    <tr>
        <td>PF No.: <asp:Label ID="lblPFNo" runat="server" /></td>
        <td>PAN: <asp:Label ID="lblPAN" runat="server" /></td>
    </tr>
    <tr>
        <td>ESIC No.: <asp:Label ID="lblESIC" runat="server" /></td>
        <td>UAN: <asp:Label ID="lblUAN" runat="server" /></td>
    </tr>
</table>
                                          <asp:Panel ID="pnlPaySlip" runat="server">
            <table>
                <tr>
                    <th colspan="5" class="header">EARNINGS</th>
                </tr>
                <tr>
                    <th>Description</th>
                    <th>Rate</th>
                    <th>Monthly</th>
                    <th>Arrear</th>
                    <th>Total</th>
                </tr>
                <asp:Repeater ID="rptEarnings" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Description") %></td>
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("Monthly") %></td>
                            <td><%# Eval("Arrear") %></td>
                            <td><%# Eval("Total") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
              <%--  <tr>
                    <td colspan="4"><strong>Gross Pay</strong></td>
                    <td><strong><asp:Label ID="lblGrossPay" runat="server" /></strong></td>
                </tr>--%>

                <tr>
                    <th colspan="5" class="header">DEDUCTIONS</th>
                </tr>
                <tr>
                    <th>Description</th>
                    <th>Rate</th>
                    <th>Monthly</th>
                    <th>Arrear</th>
                    <th>Total</th>
                </tr>
                <asp:Repeater ID="rptDeductions" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Description") %></td>
                            <td><%# Eval("Rate") %></td>
                            <td><%# Eval("Monthly") %></td>
                            <td><%# Eval("Arrear") %></td>
                            <td><%# Eval("Total") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
               <%-- <tr>
                    <td colspan="4"><strong>Gross Deduction</strong></td>
                    <td><strong><asp:Label ID="lblGrossDeduction" runat="server" /></strong></td>
                </tr>--%>
                <tr>
    <td colspan="5" style="text-align:left; font-weight:bold; padding-top:10px;">
        Net Pay: <asp:Label ID="lblNetPay" runat="server" Text=""></asp:Label>
    </td>
</tr>
            </table>
        </asp:Panel>
                                          <div class="note">This is a computer-generated pay slip and does not require signature.</div>
                                       </div>
     </div>
   </div>
        </div>
                            </div>
        </div>
                    </div>
        </div>
             <script type="text/javascript">
                 function PrintOnlyDiv() {
                     var divContents = document.getElementById('<%= divExport.ClientID %>').innerHTML;
                     var printWindow = window.open('', '', 'height=800,width=1000');

                     printWindow.document.write('<html><head><title>Print</title>');

                     // ✅ Automatically include all <link> and <style> from current document
                     var styles = document.querySelectorAll('link[rel="stylesheet"], style');
                     styles.forEach(function (styleNode) {
                         printWindow.document.write(styleNode.outerHTML);
                     });

                     printWindow.document.write('</head><body>');
                     printWindow.document.write(divContents);
                     printWindow.document.write('</body></html>');
                     printWindow.document.close();

                     printWindow.focus();
                     printWindow.print();
                     printWindow.close();
                     printWindow.close();
                 }
             </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
