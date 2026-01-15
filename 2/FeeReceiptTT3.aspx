<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeeReceiptTT3.aspx.cs" Inherits="_2_FeeReceiptTT3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        #abc {
            height: 445px;
            width: 98%;
        }
    </style>
    <style>
        .cut-line-box h4 i {
            position: absolute;
            font-size: 17px;
            margin-top: -8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-lg-12" style="    padding: 0px 10px;">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-lg-6">
                            <asp:Label ID="lblMedium" runat="server" Text="Receipt No. is : " class="  no-padding txt-bold  "></asp:Label>
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" Style="color: #CC0000; font-weight: 700" Text=""></asp:Label>
                        </div>
                        <div class="col-lg-6 no-padding text-right menu-action">

                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn-print-box" PostBackUrl="~/2/CompositFeeDeposit.aspx" Style="color: #CC0000"
                                title="Go back to Fee Deposit"><i class="icon-reply"></i></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-print-box"
                                title="Print Receipt"><i class="icon-printer"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30">
        <div class="col-sm-12 no-padding print-row marg-bot-30" runat="server" id="abc">
            <div class="col-sm-12 print-row fee-d-box-nhl">
                <div class="col-sm-12 ">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%" class="print-font-set" id="header1" runat="server"></td>
                          <%--  <td style="width: 25%; vertical-align: top;">
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="lblCancelStudent" runat="server" Text="FEE RECEIPT "></asp:Label>
                                        <asp:Label ID="lblDuplicateStudent" runat="server" Text=""></asp:Label>
                                    </h3>
                                    <h3 class="sub-adds-l text-center" style="display: none">STATUS&nbsp;(<asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>)
                                    </h3>
                                    <h3 class="sub-adds-l text-center"><asp:Label ID="lblSTUDENTCOPY" runat="server" Text="STUDENT'S COPY"></asp:Label></h3>
                                </div>
                            </td>--%>
                        </tr>
                        
                    </table>
                </div>
                <div class="col-sm-12 ">
                    <hr style="margin: 0; padding: 0;" />
                </div>



                <div class="col-sm-12 ">
                    <table class="table no-head-border" style="margin: 0; font-family: Courier New !important;">
                        <asp:Repeater ID="rptStudentDetails" runat="server">
                            <ItemTemplate>

                                <tr>
                                    <td class="text-left" style="width: 13%; padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label2" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 35%; padding: 5px 10px 0 0 !important;">:&nbsp;
                                           <asp:Label ID="Label7" runat="server" Font-Bold="True" Text='<%# Eval("ReceiptNo") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 17%; padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label5" runat="server" Text="Date " Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 30%; padding: 5px 10px 0 0 !important;">:&nbsp;
                                           <asp:Label ID="Label34" runat="server" Text='<%# Eval("DepositDate") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label20" runat="server" Text="S.R. No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                                    <asp:Label ID="Label24" runat="server" Text='<%# Eval("srNo") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Student's Name " Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                          <asp:Label ID="Label8" runat="server" Text='<%# Eval("Name") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>

                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label4" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                       <asp:Label ID="Label9" runat="server" Text='<%# Eval("ClassName") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>

                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label22" runat="server" Text="Father's Name" Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                       <asp:Label ID="Label23" runat="server" Text='<%# Eval("FatherName") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label28" runat="server" Text="Mode" Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                       <asp:Label ID="Label29" runat="server" Text='<%# Eval("Mode") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label6" runat="server" Text="Status" Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" colspan="3" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                       <asp:Label ID="Label11" runat="server" Text='<%# Eval("receiptStatus") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding: 5px 0 0 0 !important;">
                                        <hr style="margin: 0; padding: 0;" />
                                    </td>
                                </tr>


                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:GridView ID="grdfeeDetails" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" BorderStyle="None"
                        GridLines="None">
                        <Columns>
                            <asp:TemplateField>

                                <HeaderTemplate>
                                    <table class="table table-striped no-head-border" style="margin-bottom: 0">
                                        <tr>
                                            <th class="text-left" style="width: 15%;font-family: Courier New;">
                                                <asp:Label ID="Label39" runat="server" Text="#" Font-Size="12px"></asp:Label>
                                            </th>
                                            <th class="text-left" style="width: 65%;font-family: Courier New;">
                                                <asp:Label ID="Label40" runat="server" Text="Particulars" Font-Size="12px"></asp:Label>
                                            </th>
                                            <th style="width: 20%;font-family: Courier New; text-align: right;">
                                                <asp:Label ID="Label41" runat="server" Text="Amount" Font-Size="12px"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="table no-head-border" style="margin-bottom: 0">
                                        <tr <%# Eval("feetype").ToString()=="Fee"?"": "style='background: #f4f4f4;'" %> >
                                            <td class="text-left pd-left" style="width: 15%;">
                                                <asp:Label ID="Label25" runat="server" Text='<%# Container.DataItemIndex+1 %>' Font-Size="12px" Font-Names="Courier New"></asp:Label>
                                            </td>
                                            <td class="text-left pd-left" style="width: 65%;">
                                                <asp:Label ID="Label12" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New"
                                                    Font-Size="12px" Text='<%# Bind("Particulars") %>' style="text-transform:uppercase"></asp:Label>

                                            </td>
                                            <td style="width: 20%;" class="text-right pd-left">
                                                <asp:Label ID="Label10" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New"
                                                    Font-Size="12px" Text='<%# Bind("amount") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0" style="font-family: Courier New;">
                                        <tr>
                                            <td colspan="4">
                                                <hr style="margin: 0; padding: 0;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table class="table no-head-border no-bm">
                                                    <tr>
                                                        <td  class="text-left" style="width: 15%;"></td>
                                                        <td class="text-left" style="width: 65%;">
                                                            <asp:Label ID="Label38" runat="server" Text="Total Amount" Font-Size="12px"></asp:Label><br />
                                                        </td>
                                                        <td style="padding: 0 5px !important; width: 20%; text-align: right;">
                                                            &nbsp;<asp:Label ID="Label18" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <hr style="margin: 0; padding: 0;" />
                                            </td>
                                        </tr>
                                        <tr>
                                          <%--  <td style="width: 50%;">
                                                <div class="col-lg-12 left-padd-0 right-bor-l">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 50%;" class="text-left">
                                                                <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td style="width: 50%;" class="text-right">
                                                                <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="hide">
                                                            <td class="text-left">
                                                                <asp:Label ID="Label17" runat="server" Font-Bold="False" Text="Next Due Amount" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="text-right">
                                                                <asp:Label ID="lblNexDueAmt" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </td>--%>
                                            <td>
                                                <div class="col-lg-12 right-padd-0">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 50%;" class="text-left">
                                                                <asp:Label ID="Label13" runat="server" Text="Received Amount" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td style="width: 50%; padding: 0 5px !important;" class="text-right">
                                                                &nbsp;<asp:Label ID="Label14" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">

                                                                <asp:Label ID="Label27" runat="server" Text="" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
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

                                            <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;">
                                                Received by
                                                                  <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;on 
                                                                  <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 15px;padding: 85px 0 0 0;font:bold;">
                                               Signature and Seal of Authorized Signatory
       
                                            </td>

                                        </tr>
                                        <%-- <tr>
                                            <td class="text-center" style="font-family: Courier New; font-size: 15px;padding: 115px 0 0 0;font:bold;">
                                                <asp:Label ID="Literal1" runat="server" Text="Signature and Seal of Authorized Signatory"></asp:Label><br />
                                                <span> 
                                                    <asp:Label ID="Literal2" runat="server" Text="Signature and Seal of Authorized Signatory"></asp:Label>
                                                    </span>
                                            </td>
                                            
                                        </tr>--%>
                                         <tr>
                                            <td colspan="4">
                                                <hr style="margin: 0; padding: 0;" />
                                            </td>
                                        </tr>
                                              <tr>

                                            <td  class="text-center" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;">
                                              
                                                                  <asp:Label ID="Literal1" runat="server" Text="" style="font:bold; font-weight: 700"></asp:Label><br />
                                                                   <span> <asp:Label ID="Literal2" runat="server" Text="" style="font:bold; font-weight: 700"></asp:Label></span>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                                <ItemStyle BorderStyle="None" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class=" col-sm-12 no-padding " runat="server" id="divsepration">
                <div class="cut-line-box">
                    <h4><i class="fa fa-scissors"></i></h4>
                </div>

            </div>
            <div class="col-sm-12 print-row fee-d-box-nhl" runat="server" id="Table1">
                <div class="col-sm-12" style="    padding: 10px 10px;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%" class="print-font-set" id="header2" runat="server"></td>
<%--                            <td style="width: 25%; vertical-align: top;">
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="lblCancelSCHOOL" runat="server" Text="FEE RECEIPT "></asp:Label>
                                        <asp:Label ID="lblDuplicateSCHOOL" runat="server" Text=""></asp:Label>
                                    </h3>
                                    <h3 class="sub-adds-l text-center" style="display: none">STATUS&nbsp;(<asp:Label ID="lblStatus1" runat="server" Text=""></asp:Label>)
                                    </h3>
                                    <h3 class="sub-adds-l text-center">SCHOOL'S COPY</h3>
                                </div>
                            </td>--%>
                        </tr>

                    </table>
                </div>
                <div class="col-sm-12">
                    <hr style="margin: 0; padding: 0;" />
                </div>

                <div class="col-sm-12">
                    <table class="table no-head-border" style="margin: 0; font-family: Courier New !important;">
                        <asp:Repeater ID="rptStudentDetails1" runat="server">
                            <ItemTemplate>

                                <tr>
                                    <td class="text-left" style="width: 13%; padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label15" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 35%; padding: 5px 10px 0 0 !important;">:&nbsp;
                                           <asp:Label ID="Label16" runat="server" Font-Bold="True" Text='<%# Eval("ReceiptNo") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 17%; padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label19" runat="server" Text="Date " Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 30%; padding: 5px 10px 0 0 !important;">:&nbsp;
                                           <asp:Label ID="Label21" runat="server" Text='<%# Eval("DepositDate") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label26" runat="server" Text="S.R. No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                                    <asp:Label ID="Label32" runat="server" Text='<%# Eval("srNo") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label33" runat="server" Font-Bold="False" Text="Student's Name " Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                          <asp:Label ID="Label35" runat="server" Text='<%# Eval("Name") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>

                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label36" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                       <asp:Label ID="Label37" runat="server" Text='<%# Eval("ClassName") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>

                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label42" runat="server" Text="Father's Name" Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                       <asp:Label ID="Label43" runat="server" Text='<%# Eval("FatherName") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label44" runat="server" Text="Mode" Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                       <asp:Label ID="Label45" runat="server" Text='<%# Eval("Mode") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 5px 10px 0 0 !important;">
                                        <asp:Label ID="Label46" runat="server" Text="Status" Font-Bold="False" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="text-left" colspan="3" style="padding: 5px 10px 0 0 !important;">:&nbsp;
                                       <asp:Label ID="Label47" runat="server" Text='<%# Eval("receiptStatus") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding: 5px 0 0 0 !important;">
                                        <hr style="margin: 0; padding: 0;" />
                                    </td>
                                </tr>


                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:GridView ID="grdfeeDetails1" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" BorderStyle="None"
                        GridLines="None">
                        <Columns>
                            <asp:TemplateField>

                                <HeaderTemplate>
                                    <table class="table table-striped no-head-border" style="margin-bottom: 0">
                                        <tr>
                                            <th class="text-left" style="width: 15%;font-family: Courier New;">
                                                <asp:Label ID="Label48" runat="server" Text="#" Font-Size="12px"></asp:Label>
                                            </th>
                                            <th class="text-left" style="width: 65%;font-family: Courier New;">
                                                <asp:Label ID="Label49" runat="server" Text="Particulars" Font-Size="12px"></asp:Label>
                                            </th>
                                            <th style="width: 20%;font-family: Courier New; text-align: right;">
                                                <asp:Label ID="Label50" runat="server" Text="Amount" Font-Size="12px"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="table no-head-border" style="margin-bottom: 0">
                                        <tr <%# Eval("feetype").ToString()=="Fee"?"": "style='background: #f4f4f4;'" %> >
                                            <td class="text-left pd-left" style="width: 15%;">
                                                <asp:Label ID="Label51" runat="server" Text='<%# Container.DataItemIndex+1 %>' Font-Size="12px" Font-Names="Courier New"></asp:Label>
                                            </td>
                                            <td class="text-left pd-left" style="width: 65%;">
                                                <asp:Label ID="Label52" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New"
                                                    Font-Size="12px" Text='<%# Eval("Particulars") %>' style="text-transform:uppercase"></asp:Label>

                                            </td>
                                            <td style="width: 20%;" class="text-right pd-left">
                                                <asp:Label ID="Label53" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New"
                                                    Font-Size="12px" Text='<%# Eval("Amount") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0" style="font-family: Courier New;">
                                        <tr>
                                            <td colspan="4">
                                                <hr style="margin: 0; padding: 0;" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="4">
                                                <table class="table no-head-border no-bm">
                                                    <tr>
                                                        <td  class="text-left" style="width: 15%;"></td>
                                                        <td class="text-left" style="width: 65%;">
                                                            <asp:Label ID="Label54" runat="server" Text="Total Amount" Font-Size="12px"></asp:Label><br />
                                                        </td>
                                                        <td  style="padding: 0 5px !important; width: 20%; text-align: right;">
                                                            &nbsp;<asp:Label ID="Label18_2" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="4">
                                                <hr style="margin: 0; padding: 0;" />
                                            </td>
                                        </tr>
                                        <tr>
                                          <%--  <td style="width: 50%;">
                                                <div class="col-lg-12 left-padd-0 right-bor-l">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 50%;" class="text-left">
                                                                <asp:Label ID="Label55" runat="server" Font-Bold="False" Text="Balance Amount" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td style="width: 50%;" class="text-right">
                                                                <asp:Label ID="Label30_2" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="hide">
                                                            <td class="text-left">
                                                                <asp:Label ID="Label56" runat="server" Font-Bold="False" Text="Next Due Amount" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="text-right">
                                                                <asp:Label ID="lblNexDueAmt_2" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </td>--%>
                                            <td>
                                                <div class="col-lg-12 right-padd-0">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 50%;" class="text-left">
                                                                <asp:Label ID="Label57" runat="server" Text="Received Amount" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td  class="text-right" style="width: 50%;padding: 0 5px !important;">
                                                                &nbsp;<asp:Label ID="Label14_2" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">

                                                                <asp:Label ID="Label27_2" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
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

                                            <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;">
                                                Received by
                                                                  <asp:Label ID="lblUserName_2" runat="server" Text=""></asp:Label>&nbsp;on 
                                                                  <asp:Label ID="lblFooterDate_2" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                          <tr>

                                            <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 15px;padding: 85px 0 0 0;font:bold;">
                                               Signature and Seal of Authorized Signatory
       
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="4">
                                                <hr style="margin: 0; padding: 0;" />
                                            </td>
                                        </tr>
                                        <tr>
  <td  class="text-center" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;font:bold;">
                                              
                                                                  <asp:Label ID="Label17" runat="server" Text="" style="font:bold; font-weight: 700"></asp:Label><br />
                                                                   <span> <asp:Label ID="Label30" runat="server" Text="" style="font:bold; font-weight: 700"></asp:Label></span>
                                            </td>
                                        </tr>
                                       

                                    </table>
                                </FooterTemplate>
                                <ItemStyle BorderStyle="None" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
