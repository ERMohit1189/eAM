<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="YearlyStudentReceiptGenerate.aspx.cs" Inherits="StudentReceiptGenerate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        #abc {
            height: 445px;
            width: 98%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-6 no-padding">
                            <asp:Label ID="lblMedium" runat="server" Text="Your Receipt No. is : " class="  no-padding txt-bold  "></asp:Label>
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" Style="color: #CC0000; font-weight: 700" Text="Label"></asp:Label>
                        </div>
                        <div class="col-sm-6 no-padding text-right menu-action">
                            <%--<asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/admin/fee_deposit.aspx" Style="color: #CC0000">Go back to Fee Deposit</asp:LinkButton>--%>
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn-print-box" PostBackUrl="~/admin/fee_deposit.aspx" Style="color: #CC0000"
                                title="Go back to Fee Deposit" data-placement="left"><i class="fa fa-reply"></i></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-print-box"
                                title="Receipt Print" data-placement="left"><i class="icon-printer"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30">
        <div class="col-sm-12 no-padding print-row marg-bot-30" runat="server" id="abc">
            <div class="col-sm-12 print-row fee-d-box-nhl">
                <div class="col-sm-12 no-padding">
                    <table class="print-table-text">
                        <tr>
                            <td style="width: 75%" class="print-font-set" id="header1" runat="server"></td>
                            <td style="width: 25%; vertical-align: top;">
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="lblCancel" runat="server" Text="FEE RECEIPT"></asp:Label></h3>
                                    <h3 class="sub-adds-l text-center">STATUS&nbsp;(<asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>)
                                    </h3>
                                    <h3 class="sub-adds-l text-center">STUDENT'S COPY </h3>
                                </div>
                            </td>
                        </tr>

                    </table>
                </div>
                <div class="col-sm-12 no-padding print-row">
                    <hr style="margin: 0; padding: 0;" />
                </div>



                <div class="col-sm-12 ">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" BorderStyle="None" CssClass="print-table-text "
                        GridLines="None">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table class="print-table-text">
                                        <tr>
                                            <td class="text-left" style="width: 13%;">
                                                <asp:Label ID="Label2" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left" style="width: 35%;">:&nbsp;
                                           <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left" style="width: 7%;">
                                                <asp:Label ID="Label5" runat="server" Text="Date " Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left" style="width: 30%;">:&nbsp;
                                           <asp:Label ID="Label34" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-left">
                                                <asp:Label ID="Label20" runat="server" Text="S.R. No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                                    <asp:Label ID="Label24" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="Label21" runat="server" Text="Installment " Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                                     <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-left">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Student's Name " Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                                     <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="Label4" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                <asp:Label ID="lblBranch" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px" Text=""></asp:Label>
                                                (<asp:Label ID="Label19" runat="server" Text="" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>)
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-left">
                                                <asp:Label ID="Label22" runat="server" Text="Father's Name" Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                                    <asp:Label ID="Label23" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="Label28" runat="server" Text="Mode" Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                                    <asp:Label ID="Label29" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <hr style="margin: 0; padding: 0;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table class="print-table-text">
                                                    <tr>
                                                        <th class="text-left" style="width: 15%;">
                                                            <asp:Label ID="Label39" runat="server" Text="#" Font-Size="12px"></asp:Label>
                                                        </th>
                                                        <th class="text-left" style="width: 65%;">
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
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="print-table-text">
                                        <tr>
                                            <td class="text-left" style="width: 15%;">
                                                <asp:Label ID="Label25" runat="server" Font-Size="12px" Font-Names="Courier New"></asp:Label>
                                            </td>
                                            <td class="text-left" style="width: 65%;">
                                                <asp:Label ID="Label12" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New" Font-Size="12px"
                                                    Text='<%# Bind("FeeType") %>'></asp:Label>
                                                <asp:Label ID="Label35" runat="server" Text=" ["></asp:Label>
                                                <asp:Label ID="Label10" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New" Font-Size="12px"
                                                    Text='<%# Bind("Month") %>'></asp:Label>
                                                <asp:Label ID="Label36" runat="server" Text="] "></asp:Label>
                                            </td>
                                            <td align="right" style="width: 20%;">
                                                <asp:Label ID="lblAmt" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Bind("FeePayment") %>' CssClass="address" Font-Names="Courier New"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <table class="print-table-text">
                                        <tr>
                                            <td align="left"
                                                style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf;">

                                                <asp:Label ID="Label37" runat="server" Text="Arrier Fee"></asp:Label>
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf;">
                                                <asp:Label ID="Label38" runat="server" CssClass="address" Font-Bold="False"
                                                    Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf;">
                                                <asp:Label ID="Label32" runat="server" Text="Late Fee"></asp:Label>
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf;">
                                                <asp:Label ID="Label17" runat="server" CssClass="address" Font-Bold="False" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">Conveyance
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">
                                                <asp:Label ID="lblBusConvence" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">
                                                <strong>Total Amount</strong>
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">
                                                <asp:Label ID="lblAmttotal" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                .00
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">Concession
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">
                                                <asp:Label ID="Label33" runat="server" Text='<%# Bind("Concession") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td align="left" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">
                                                <strong>Payable Amount</strong>
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="Label341" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>.00
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf;">
                                                <strong style="text-align: center">Received Amount</strong>
                                            </td>
                                            <td align="right" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf">
                                                <i class="fa fa-inr p-icon-size"></i>
                                                &nbsp;<asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Courier New" Font-Size="12px" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2" style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                <asp:Label ID="Label27" runat="server" Text="Label" Font-Bold="False" Style="font-weight: 700" Font-Names="Courier New"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount"></asp:Label>
                                            </td>
                                            <td align="left" style="font-family: Arial; font-size: 12px; font-weight: bold; text-align: right;">
                                                <asp:Label ID="Label30" runat="server" Font-Bold="False" Text='<%# Bind("RemainingAmount") %>' Font-Names="Courier New"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2" style="border-bottom: 1px solid #dfdfdf" height="10px"></td>
                                        </tr>

                                        <tr>

                                            <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px;">Generated by eAM&reg; | Received by
                                                                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;on 
                                                                <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                                <ItemStyle BorderStyle="None" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class=" col-sm-12 no-padding print-row"  style="display:none;">
                <div class="cut-line-box">
                    <h4><i class="fa fa-scissors"></i></h4>
                </div>

            </div>
       
            <div class="col-sm-12 print-row fee-d-box-nhl" runat="server" id="Table1">
                <div class="col-sm-12 no-padding print-row">
                    <table class="print-table-text ">
                        <tr>
                            <td style="width: 75%" id="header2" runat="server" class="print-font-set"></td>
                            <td style="width: 25%; vertical-align: top;">
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center" style="font-size: 13px; font-weight: 400;">
                                        <asp:Label ID="lblCancelStudent" runat="server" Text="FEE RECEIPT"></asp:Label></h3>
                                    <h3 class="sub-adds-l text-center" style="font-size: 13px; font-weight: 400;">STATUS&nbsp;(<asp:Label ID="lblStatus1" runat="server" Text=""></asp:Label>)
                                    </h3>
                                    <h3 style="font-size: 13px; font-weight: 400;" class="sub-adds-l text-center">SCHOOL'S COPY </h3>
                                </div>
                            </td>
                        </tr>

                    </table>
                </div>
                <div class="col-sm-12 no-padding print-row">
                    <hr style="margin: 0; padding: 0;" />
                </div>

                <div class="col-sm-12 ">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" BorderStyle="None" CssClass="print-table-text "
                        GridLines="None">
                        <Columns>
                            <asp:TemplateField>

                                <HeaderTemplate>
                                    <table class="print-table-text">
                                        <tr>
                                            <td class="text-left" style="width: 13%;">
                                                <asp:Label ID="Label2" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left" style="width: 35%;">:&nbsp;
                                        <asp:Label ID="Label70" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left" style="width: 7%;">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="False" Text="Date " Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left" style="width: 30%;">:&nbsp;
                                        <asp:Label ID="Label340" runat="server" Text="Label" Font-Bold="true" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-left">
                                                <asp:Label ID="Label20" runat="server" Text="S.R. No." Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                        <asp:Label ID="Label240" runat="server" Text="Label" Font-Names="Courier New" Font-Bold="true" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="Label21" runat="server" Text="Installment " Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                        <asp:Label ID="Label110" runat="server" Font-Bold="true" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-left">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Student's Name " Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                                    <asp:Label ID="Label80" runat="server" Font-Names="Courier New" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                <asp:Label ID="Label150" runat="server" Font-Bold="true" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                <asp:Label ID="Label160" runat="server" Font-Bold="true" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="Label4" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                        <asp:Label ID="Label90" runat="server" Font-Bold="true" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                <asp:Label ID="lblBranch" runat="server" Font-Bold="true" Font-Names="Courier New" Font-Size="12px" Text=""></asp:Label>
                                                (<asp:Label ID="Label190" runat="server" Text="Label" Font-Bold="true" Font-Names="Courier New" Font-Size="12px"></asp:Label>)
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-left">
                                                <asp:Label ID="Label22" runat="server" Font-Bold="False" Text="Father's Name " Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                        <asp:Label ID="Label230" runat="server" Text="Label" Font-Bold="true" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="Label28" runat="server" Font-Bold="False" Text="Mode " Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left">:&nbsp;
                                        <asp:Label ID="Label290" runat="server" Text="Label" Font-Bold="true" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="4">
                                                <hr style="margin: 0; padding: 0;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table class="print-table-text">
                                                    <tr>
                                                        <th class="text-left" style="width: 15%;">
                                                            <asp:Label ID="Label47" runat="server" Text="#" Font-Size="12px"></asp:Label>
                                                        </th>
                                                        <th class="text-left" style="width: 65%;">
                                                            <asp:Label ID="Label48" runat="server" Text="Particulars" Font-Size="12px"></asp:Label>
                                                        </th>
                                                        <th class="text-left" style="display: none;">
                                                            <asp:Label ID="Label50" runat="server" Text="Label" Font-Size="12px"></asp:Label>
                                                        </th>
                                                        <th colspan="2" class="text-right" style="width: 20%;">
                                                            <asp:Label ID="Label49" runat="server" Text="Amount" Font-Size="12px"></asp:Label>
                                                        </th>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="print-table-text">
                                        <tr>
                                            <td class="text-left" style="width: 15%;">
                                                <asp:Label ID="Label250" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="text-left" style="width: 65%;">
                                                <asp:Label ID="Label120" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New" Font-Size="12px"
                                                    Text='<%# Bind("FeeType") %>'></asp:Label>
                                                <asp:Label ID="Label35" runat="server" Text=" ["></asp:Label>
                                                <asp:Label ID="Label10" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New" Font-Size="12px"
                                                    Text='<%# Bind("Month") %>'></asp:Label>
                                                <asp:Label ID="Label36" runat="server" Text="] "></asp:Label>
                                            </td>
                                            <td colspan="2" class="text-right" style="width: 20%;">
                                                <asp:Label ID="lblAmt" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                <asp:Label ID="Label130" runat="server" Text='<%# Bind("FeePayment") %>' CssClass="address" Font-Names="Courier New"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <table class="print-table-text">
                                        <tr>
                                            <td align="left"
                                                style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf;">

                                                <asp:Label ID="Label37" runat="server" Text="Arrier Fee"></asp:Label>
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf;">
                                                <asp:Label ID="Label38" runat="server" CssClass="address" Font-Bold="False"
                                                    Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf;">
                                                <asp:Label ID="Label32" runat="server" Text="Late Fee"></asp:Label>
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-top: 1px solid #dfdfdf;">
                                                <asp:Label ID="Label170" runat="server" CssClass="address" Font-Bold="False" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">Conveyance
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">
                                                <asp:Label ID="lblBusConvence1" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">
                                                <strong>Total Amount</strong>
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">
                                                <asp:Label ID="lblAmttotal" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                .00
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">Concession
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">
                                                <asp:Label ID="Label330" runat="server" Text='<%# Bind("Concession") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td align="left" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">
                                                <strong>Payable Amount</strong>
                                            </td>
                                            <td align="right" style="font-family: Arial; font-size: 12px; border-bottom: 1px solid #dfdfdf;">
                                                <asp:Label ID="Label342" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>.00
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf;">
                                                <strong style="text-align: center">Received Amount</strong>
                                            </td>
                                            <td align="right" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #dfdfdf">
                                                <i class="fa fa-inr p-icon-size"></i>
                                                &nbsp;<asp:Label ID="Label180" runat="server" Font-Bold="False" Font-Names="Courier New" Font-Size="12px" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2" style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                <asp:Label ID="Label270" runat="server" Text="Label" Font-Bold="False" Style="font-weight: 700"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-family: Arial; font-size: 12px; font-weight: bold;">
                                                <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount"></asp:Label>
                                            </td>
                                            <td align="left" style="font-family: Arial; font-size: 12px; font-weight: bold; text-align: right;">
                                                <asp:Label ID="Label300" runat="server" Font-Bold="False" Text='<%# Bind("RemainingAmount") %>' Font-Names="Courier New"
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2" style="border-bottom: 1px solid #dfdfdf" height="10px"></td>
                                        </tr>
                                        <tr>

                                            <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px;">Generated by eAM&reg; | Received by
                                                                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;on 
                                                                <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

