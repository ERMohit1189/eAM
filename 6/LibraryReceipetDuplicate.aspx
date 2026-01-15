<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LibraryReceipetDuplicate.aspx.cs"
    Inherits="admin_LibraryReceipetDuplicate" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        #abc hr {
            margin: 0;
            padding: 0;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-lg-6 no-padding">
                            <asp:Label ID="lblMedium" runat="server" Text="Receipt No. is : " class="  no-padding txt-bold  "></asp:Label>
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" Style="color: #CC0000; font-weight: 700" Text=""></asp:Label>
                        </div>
                        <div class="col-lg-6 no-padding text-right menu-action">
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" Style="color: #CC0000" title="Go back to Book Issue and Retrun " ><i class="fa fa-reply"></i></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-print-box" title="Receipt Print" ><i class="icon-printer"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30">
        <div class="col-sm-12 no-padding print-row marg-bot-30" runat="server" id="abc">
            <div class="col-sm-12  fee-d-box-nhl">
                <div class="col-sm-12 no-padding print-row">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%" id="header1" runat="server"></td>
                        </tr>
                        
                        <tr>
                            <td>
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="Label6" runat="server" Text="FEE RECEIPT "></asp:Label>
                                        (<asp:Label ID="lblDuplicateStudent" runat="server" Text="DUPLICATE"></asp:Label>)
                                    </h3>
                                    <h3 class="sub-adds-l text-center" style="display: none">STATUS&nbsp;(<asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>)
                                    </h3>
                                </div>
                            </td>
                        </tr>

                    </table>
                </div>
                <div class="col-sm-12 no-padding print-row">
                    <hr style="margin: 0; padding: 0;" />
                </div>

                <div class="col-sm-12 no-padding print-row">
                    <table style="width: 100%;">
                        <tr>
                            <td style="padding: 0 0 !important; vertical-align: top !important;">


                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <table style="width: 100%; font-family: Courier New;">
                                            <tr>
                                                <td class="text-left" style="width: 17%;">
                                                    <asp:Label ID="Label2" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left" style="width: 45%;">:&nbsp;
                                           <asp:Label ID="Label7" runat="server" Font-Bold="True" Text='<%# Bind("ReceiptNo") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left" style="width: 14%;">
                                                    <asp:Label ID="Label5" runat="server" Text="Date " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left" style="width: 24%;">:&nbsp;
                                           <asp:Label ID="Label34" runat="server" Text='<%# Bind("Date") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="text-left">
                                                    <asp:Label ID="Label20" runat="server" Text="S.R. No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left">:&nbsp;
                                            <asp:Label ID="Label24" runat="server" Text='<%# Bind("srno") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left">
                                                    <asp:Label ID="Label28" runat="server" Text="Mode" Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left">:&nbsp;
                                                    <asp:Label ID="Label29" runat="server" Text='<%# Bind("Mode") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="text-left">
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Student's Name " Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left">:&nbsp;
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px" Text='<%# Bind("studentName") %>'></asp:Label>
                                                </td>
                                                <td class="text-left">
                                                    <asp:Label ID="Label4" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left">:&nbsp;
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px" Text='<%# Bind("class") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                        <td class="text-left">
                                            <asp:Label ID="Label22" runat="server" Text="Father's Name" Font-Bold="False" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td class="text-left" colspan="3">:&nbsp;
                                                    <asp:Label ID="Label23" runat="server" Text='<%# Bind("Fathername") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="Label28" runat="server" Text="Mode" Font-Bold="False" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td class="text-left">:&nbsp;
                                                    <asp:Label ID="Label29" runat="server" Text='<%# Bind("Mode") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>--%>
                                            <tr>
                                                <td colspan="4">
                                                    <hr style="margin: 0; padding: 0;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <th class="text-left" style="width: 20%;">
                                                                <asp:Label ID="Label39" runat="server" Text="#" Font-Size="12px"></asp:Label>
                                                            </th>
                                                            <th class="text-center" style="width: 60%;">
                                                                <asp:Label ID="Label40" runat="server" Text="Particulars" Font-Size="12px"></asp:Label>
                                                            </th>
                                                            <th class="text-left" style="display: none;">
                                                                <asp:Label ID="Label50" runat="server" Text="Label" Font-Size="12px" Visible="true"></asp:Label>
                                                            </th>
                                                            <th style="width: 20%; text-align: right;">
                                                                <asp:Label ID="Label41" runat="server" Text="Amount" Font-Size="12px"></asp:Label>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </td>

                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="false" ShowHeader="false" BorderStyle="None"
                                    GridLines="None">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" style="font-family: Courier New;">
                                                    <tr>
                                                        <td class="text-left" width="10%">
                                                            <asp:Label ID="Label25" runat="server" Text='<%# Container.DataItemIndex+1 %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                        </td>
                                                        <td valign="middle" class="text-center" width="70%">
                                                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("Particulars") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                        </td>
                                                        <td align="right" width="20%" valign="middle">
                                                            <asp:Label ID="lblAmt" runat="server" Visible="False" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("Amount") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:GridView>
                                <asp:Repeater ID="Repeater2" runat="server">
                                    <ItemTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0" style="font-family: Courier New;">
                                            <tr>
                                                <td colspan="4">
                                                    <hr style="margin: 0; padding: 0;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label38" runat="server" Text="Total Amount" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <i class="fa fa-inr p-icon-size"></i>
                                                    &nbsp;<asp:Label ID="Label18" runat="server" Text='<%# Bind("Receivedamount") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <hr style="margin: 0; padding: 0;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; visibility: hidden">
                                                    <div class="col-lg-12 left-padd-0 right-bor-l">
                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="width: 50%;" class="text-left">
                                                                    <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount " Font-Size="12px"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" class="text-right">
                                                                    <i class="fa fa-inr p-icon-size"></i>
                                                                    <asp:Label ID="Label30" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-left">
                                                                    <asp:Label ID="Label17" runat="server" Font-Bold="False" Text="Next Due Amount" Font-Size="12px"></asp:Label>
                                                                </td>
                                                                <td class="text-right">
                                                                    <i class="fa fa-inr p-icon-size"></i>
                                                                    <asp:Label ID="lblNexDueAmt" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </div>
                                                </td>
                                                <td style="width: 50%;">
                                                    <div class="col-lg-12 right-padd-0">
                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="width: 50%;" class="text-left">
                                                                    <asp:Label ID="Label10" runat="server" Text="Received Amount" Font-Size="12px"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" class="text-right">
                                                                    <i class="fa fa-inr p-icon-size"></i>
                                                                    &nbsp;<asp:Label ID="Label14" runat="server" Text='<%# Bind("Receivedamount") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">

                                                                    <asp:Label ID="Label27" runat="server" Text='<%# Bind("Amountinword") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="4">
                                                    <hr style="margin: 0; padding: 0;" />
                                                </td>
                                            </tr>
                                            <tr>

                                                <td colspan="2" align="right" style="font-family: Courier New; font-size: 11px;">Received by
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("Receveiedby") %>'></asp:Label>
                                                </td>
                                            </tr>

                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </td>
                           <%-- <td style="width: 10%; padding: 0px 0px !important; vertical-align: top !important;" id="tdhieght">



                                <div class="bar-code-box-center">
                                    <barcode:barcodelabel id="lblBarCode" runat="server" cssclass="vertical-barcode2" text="" labelrowstyle-cssclass="barcode-label-style" barcodeencoding="Code39NC" barminwidth="1" barmaxwidth="2" barminheight="30" barmaxheight="40" />
                                </div>

                            </td>--%>
                        </tr>
                    </table>
                </div>
            </div>
           <%-- <script>
                var barheight = document.getElementById("tdhieght");
                var lblBarCode_labelCellBottom = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_lblBarCode_labelCellBottom");
                barheight.style.height = (lblBarCode_labelCellBottom.innerText.length * 23).toString() + "px";
            </script>--%>

            <div class=" col-lg-12 no-padding print-row">
                <div class="cut-line-box">
                    <h4><i class="fa fa-scissors"></i></h4>
                </div>

            </div>

            <div class="col-sm-12  fee-d-box-nhl" runat="server" id="Table1">
                <div class="col-sm-12 no-padding print-row">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 105%" id="header2" runat="server">
                                <%--<uc2:School ID="School1" runat="server" />--%>
                            </td>
                        </tr>
                        
                        <tr>
                             <td>
                                 <div class="print-side-titel-box text-center">
                                     <h3 class="main-name-l text-center">
                                         <asp:Label ID="lblCancelSCHOOL" runat="server" Text="FEE RECEIPT "></asp:Label>
                                         (<asp:Label ID="lblDuplicateSCHOOL" runat="server" Text="DUPLICATE"></asp:Label>)
                                     </h3>
                                     <h3 class="sub-adds-l text-center" style="display: none">STATUS&nbsp;(<asp:Label ID="lblStatus1" runat="server" Text=""></asp:Label>)
                                     </h3>
                                 </div>
                             </td>
                        </tr>
                    </table>
                </div>
                <div class="col-sm-12 no-padding print-row">
                    <hr style="margin: 0; padding: 0;" />
                </div>

                <div class="col-sm-12 no-padding print-row">
                    <table style="width: 100%;">
                        <tr>
                            <td style="padding: 0px 0px !important; vertical-align: top !important;">

                                <asp:Repeater ID="Repeater3" runat="server">
                                    <ItemTemplate>
                                        <table style="width: 100%; font-family: Courier New;">
                                            <tr>
                                                <td class="text-left" style="width: 17%;">
                                                    <asp:Label ID="Label2" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left" style="width: 45%;">:&nbsp;
                                           <asp:Label ID="Label7" runat="server" Font-Bold="True" Text='<%# Bind("ReceiptNo") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left" style="width: 14%;">
                                                    <asp:Label ID="Label5" runat="server" Text="Date " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left" style="width: 24%;">:&nbsp;
                                           <asp:Label ID="Label34" runat="server" Text='<%# Bind("Date") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="text-left">
                                                    <asp:Label ID="Label20" runat="server" Text="S.R. No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left">:&nbsp;
                                            <asp:Label ID="Label24" runat="server" Text='<%# Bind("srno") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left">
                                                    <asp:Label ID="Label28" runat="server" Text="Mode" Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left">:&nbsp;
                                                    <asp:Label ID="Label29" runat="server" Text='<%# Bind("Mode") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="text-left">
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Student's Name " Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left">:&nbsp;
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px" Text='<%# Bind("studentName") %>'></asp:Label>
                                                </td>
                                                <td class="text-left">
                                                    <asp:Label ID="Label4" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="text-left">:&nbsp;
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px" Text='<%# Bind("class") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                        <td class="text-left">
                                            <asp:Label ID="Label22" runat="server" Text="Father's Name" Font-Bold="False" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td class="text-left" colspan="3">:&nbsp;
                                                    <asp:Label ID="Label23" runat="server" Text='<%# Bind("Fathername") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="Label28" runat="server" Text="Mode" Font-Bold="False" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td class="text-left">:&nbsp;
                                                    <asp:Label ID="Label29" runat="server" Text='<%# Bind("Mode") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>--%>
                                            <tr>
                                                <td colspan="4">
                                                    <hr style="margin: 0; padding: 0;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <th class="text-left" style="width: 20%;">
                                                                <asp:Label ID="Label39" runat="server" Text="#" Font-Size="12px"></asp:Label>
                                                            </th>
                                                            <th class="text-center" style="width: 60%;">
                                                                <asp:Label ID="Label40" runat="server" Text="Particulars" Font-Size="12px"></asp:Label>
                                                            </th>
                                                            <th class="text-left" style="display: none;">
                                                                <asp:Label ID="Label50" runat="server" Text="Label" Font-Size="12px" Visible="true"></asp:Label>
                                                            </th>
                                                            <th align="right" style="width: 20%;">
                                                                <asp:Label ID="Label41" runat="server" Text="Amount" Font-Size="12px"></asp:Label>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </td>

                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="false" ShowHeader="false" BorderStyle="None"
                                    GridLines="None">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" style="font-family: Courier New;">
                                                    <tr>
                                                        <td class="text-left" width="10%">
                                                            <asp:Label ID="Label25" runat="server" Text='<%# Container.DataItemIndex+1 %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                        </td>
                                                        <td valign="middle" class="text-center" width="70%">
                                                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("Particulars") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                        </td>
                                                        <td align="right" width="20%" valign="middle">
                                                            <asp:Label ID="lblAmt" runat="server" Visible="False" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("Amount") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:GridView>
                                <asp:Repeater ID="Repeater4" runat="server">
                                    <ItemTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0" style="font-family: Courier New;">
                                            <tr>
                                                <td colspan="4">
                                                    <hr style="margin: 0; padding: 0;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label38" runat="server" Text="Total Amount" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <i class="fa fa-inr p-icon-size"></i>
                                                    &nbsp;<asp:Label ID="Label18" runat="server" Text='<%# Bind("Receivedamount") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <hr style="margin: 0; padding: 0;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; visibility: hidden">
                                                    <div class="col-lg-12 left-padd-0 right-bor-l">
                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="width: 50%;" class="text-left">
                                                                    <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount " Font-Size="12px"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" class="text-right">
                                                                    <i class="fa fa-inr p-icon-size"></i>
                                                                    <asp:Label ID="Label30" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-left">
                                                                    <asp:Label ID="Label17" runat="server" Font-Bold="False" Text="Next Due Amount" Font-Size="12px"></asp:Label>
                                                                </td>
                                                                <td class="text-right">
                                                                    <i class="fa fa-inr p-icon-size"></i>
                                                                    <asp:Label ID="lblNexDueAmt" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </div>
                                                </td>
                                                <td style="width: 50%;">
                                                    <div class="col-lg-12 right-padd-0">
                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="width: 50%;" class="text-left">
                                                                    <asp:Label ID="Label10" runat="server" Text="Received Amount" Font-Size="12px"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" class="text-right">
                                                                    <i class="fa fa-inr p-icon-size"></i>
                                                                    &nbsp;<asp:Label ID="Label14" runat="server" Text='<%# Bind("Receivedamount") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">

                                                                    <asp:Label ID="Label27" runat="server" Text='<%# Bind("Amountinword") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="4">
                                                    <hr style="margin: 0; padding: 0;" />
                                                </td>
                                            </tr>
                                            <tr>

                                                <td colspan="2" align="right" style="font-family: Courier New; font-size: 11px;">Received by
                                             <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("Receveiedby") %>'></asp:Label>
                                                </td>
                                            </tr>

                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>


                            </td>
                          <%--  <td style="width: 10%; padding: 0px 0px !important; vertical-align: top !important;" id="td1">
                                <div class="bar-code-box-center">
                                    <barcode:barcodelabel id="lblBarCodesch" cssclass="vertical-barcode2" runat="server"
                                        labelrowstyle-cssclass="barcode-label-style" text="" barcodeencoding="Code39NC" barminwidth="1" barmaxwidth="2" barminheight="30" barmaxheight="40" />
                                </div>
                            </td>--%>
                        </tr>
                    </table>
                </div>

            </div>
        </div>
        <</div>
</asp:Content>


