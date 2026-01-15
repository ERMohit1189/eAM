<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="OtherFeeReceipt.aspx.cs" Inherits="_2.OtherFeeReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-6 no-padding">
                            <asp:Label ID="lblMedium" runat="server" Text="Receipt No. is : " class="  no-padding txt-bold  "></asp:Label>
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" Style="color: #CC0000; font-weight: 700" Text="Label"></asp:Label>
                        </div>
                        <div class="col-sm-6 no-padding text-right menu-action">

                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn-print-box" PostBackUrl="~/2/OtherFeeDeposit.aspx" Style="color: #CC0000"
                                title="Go back to Other Fee Deposit" data-placement="left"><i class="fa fa-reply"></i></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-print-box" title="Print Receipt" data-placement="left"><i class="icon-printer"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30">
        <div class="col-sm-12 no-padding print-row marg-bot-30" runat="server" id="abc">
            <div class="col-sm-12 print-row fee-d-box-nhl">
                <div class="col-sm-12">
                  <%--  <table style="font-family: Courier New; width: 100%;">
                        <tr>
                            <td style="width: 75%" class="print-font-set" id="header1" runat="server"></td>
                            <td style="width: 25%; vertical-align: top;">
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="Label4" runat="server" Text="FEE RECEIPT"></asp:Label></h3>
                                    <h3 class="sub-adds-l text-center">
                                        <asp:Label ID="lblCancel" runat="server"></asp:Label>STUDENT'S COPY (<asp:Label ID="recdeiptType" runat="server"></asp:Label>)</h3>
                                </div>
                            </td>
                        </tr>

                    </table>--%>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%" class="print-font-set" id="header1" runat="server"></td>

                        </tr>
                        <tr>
                            <td>
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="lblCancelStudent" runat="server" Text="FEE RECEIPT "></asp:Label>
                                        <asp:Label ID="lblDuplicateStudent" runat="server" Text=""></asp:Label>
                                    </h3>
                                    <h3 class="sub-adds-l text-center" style="display: none">STATUS&nbsp;(<asp:Label ID="Label4" runat="server" Text=""></asp:Label>)
                                    </h3>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0;border-top: 0.5px solid #5d5d5d !important;" />
                </div>

                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;">
                        <tr>
                            <td class="text-left" style="width: 13%;">
                                <asp:Label ID="Label5" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" style="width: 35%;">: &nbsp;
                                           <asp:Label ID="lblRecieptNo" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" style="width: 7%;">
                                <asp:Label ID="Label7" runat="server" Text="Date " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" style="width: 30%;">: &nbsp;
                                           <asp:Label ID="lblRecieptDate" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label9" runat="server" Text="Student's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblStudentName" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label10" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblClass" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                                <asp:Label ID="lblBranch" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label11" runat="server" Text="Father's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" style="width:15%">
                                <asp:Label ID="Label12" runat="server" Text="S.R. No.  " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                           
                                <asp:Label ID="lblSrno" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="">
                            <td class="text-left">
                                <asp:Label ID="Label14" runat="server" Text="Mode" Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                                <asp:Label ID="lblMode" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label13" runat="server" Text="Status" Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            
                        </tr>
                    </table>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0;border-top:1.5px solid #5d5d5d !important;" />
                </div>

                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;">
                        <tr>
                            <th class="text-left" style="width: 30%;">
                                <asp:Label ID="Label3" runat="server" Text="#" Font-Size="12px"></asp:Label>
                            </th>
                            <th class="text-center" style="width: 40%;">
                                <asp:Label ID="Label15" runat="server" Text="Particulars" Font-Size="12px"></asp:Label>
                            </th>
                            <th class="text-right" style="width: 30%;">
                                <asp:Label ID="Label16" runat="server" Text="Amount " Font-Size="12px"></asp:Label>
                            </th>

                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label17" runat="server" Text="1." Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-center">
                                <asp:Label ID="Label18" runat="server" Font-Names="Courier New" Font-Size="12px">Other Fee </asp:Label>
                            </td>
                            <td class="text-right">
                                <asp:Label ID="lblAmount" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>

                        </tr>
                    </table>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0;border-top: 0.5px solid #5d5d5d !important;" />
                </div>

                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;">
                        <tr id="chqBFee" runat="server" visible="false">
                            <td class="text-left" style="width: 40%;">
                                <asp:Label ID="Label27" runat="server" Text="Cheque Bounce Fee" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right" style="width: 60%;">
                                <asp:Label ID="lblchkBouncefee" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="divConcession" visible="false">
                            <td class="text-left" style="width: 40%;">
                                <asp:Label ID="Label20" runat="server" Text="Concession Amount" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right" style="width: 60%;">
                                -<asp:Label ID="lblConcessionAmount" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label6" runat="server" Text="Received Amount" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right">
                                <asp:Label ID="lblReceivedAmount" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label8" runat="server" Text="Amount in Words" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right">
                                <asp:Label ID="lblAmountinWords" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="divNextDue" visible="false">
                            <td class="text-left">
                                <asp:Label ID="Label28" runat="server" Text="Next Balance Amount" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right">
                                <asp:Label ID="lblNextBalancemount" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                     <%--   <tr>
                            <td colspan="4">
                                <asp:Label ID="lblRemark" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>

                            <td colspan="4" style="font-family: Courier New; font-size: 11px; font-weight: bold; text-align: right">
                                <hr style="margin: 0;" />
                                Received by 
                                                                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;on 
                                                                <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label>

                            </td>
                        </tr>--%>

                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblRemark" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                <hr style="margin: 0; padding: 0;" />
                            </td>
                        </tr>
                        <tr>
                            <td  class="text-left" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;width:50%">
                                <div id="grdfeeDetailscancelleddiv" runat="server" visible="false">
                                    Cancelled by
                                          <asp:Label ID="Label34" runat="server" Text=""></asp:Label>&nbsp;on 
                                          <asp:Label ID="Label39" runat="server" Text=""></asp:Label>
                                </div>
         
                            </td>

                            <td  class="text-right" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;width:50%">
                                Received by
                                  <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;on 
                                  <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label>                                        
                            </td>
                        </tr>


                        <tr runat="server" id="Tr1">
                            <td colspan="2">
                                <hr style="margin: 0; padding: 0;" />
                            </td>
                        </tr>
                        <tr runat="server" id="Tr2">
                            <td class="text-center" colspan="2" style="font-family: Courier New; font-size: 11px; padding: 5px 0 0 0;">
                                <asp:Label ID="lblRemark1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="Tr3">
                            <td class="text-center" colspan="2" style="font-family: Courier New; font-size: 11px; padding: 5px 0 0 0;">
                                <asp:Label ID="lblRemark2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    
                </div>
            </div>

            <div class=" col-lg-12 no-padding print-row">
                <div class="cut-line-box">
                    <h4><i class="fa fa-scissors"></i></h4>
                </div>
            </div>

            <div class="col-sm-12 print-row fee-d-box-nhl">
                <div class="col-sm-12">
                   <%-- <table style="font-family: Courier New; width: 100%;">
                        <tr>
                            <td style="width: 75%" class="print-font-set" id="header2" runat="server"></td>
                            <td style="width: 25%; vertical-align: top;">
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="Label2" runat="server" Text="FEE RECEIPT"></asp:Label></h3>
                                    <h3 class="sub-adds-l text-center">
                                        <asp:Label ID="Label22" runat="server"></asp:Label>SCHOOL'S COPY (<asp:Label ID="recdeiptType0" runat="server"></asp:Label>)</h3>
                                </div>
                            </td>
                        </tr>

                    </table>--%>
                     <table width="100%" cellpadding="0" cellspacing="0">
                         <tr>
                             <td style="width: 100%" class="print-font-set" id="header2" runat="server"></td>

                         </tr>
                         <tr>
                             <td>
                                 <div class="print-side-titel-box text-center">
                                     <h3 class="main-name-l text-center">
                                         <asp:Label ID="lblCancelSCHOOL" runat="server" Text="FEE RECEIPT "></asp:Label>
                                         <asp:Label ID="lblDuplicateSCHOOL" runat="server" Text=""></asp:Label>
                                     </h3>
                                     <h3 class="sub-adds-l text-center" style="display: none">STATUS&nbsp;(<asp:Label ID="lblStatus1" runat="server" Text=""></asp:Label>)
                                     </h3>
                                 </div>
                             </td>
                         </tr>
                     </table>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0;border-top: 0.5px solid #5d5d5d !important;" />
                </div>

                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;">
                        <tr>
                            <td class="text-left" style="width: 13%;">
                                <asp:Label ID="Label21" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" style="width: 35%;">: &nbsp;
                                           <asp:Label ID="lblRecieptNo0" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" style="width: 7%;">
                                <asp:Label ID="Label23" runat="server" Text="Date " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" style="width: 30%;">: &nbsp;
                                           <asp:Label ID="lblRecieptDate0" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label24" runat="server" Text="Student's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblStudentName0" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label25" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblClass0" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                                <asp:Label ID="lblBranch0" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label29" runat="server" Text="Father's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblFatherName0" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label31" runat="server" Text="S.R. No.  " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblSrno0" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="">
                            <td class="text-left">
                                <asp:Label ID="Label26" runat="server" Text="Mode " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                                <asp:Label ID="lblMode0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label33" runat="server" Text="Status" Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblStatus0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            
                        </tr>
                    </table>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0; border-top: 0.5px solid #5d5d5d !important;"  />
                </div>

                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;">
                        <tr>
                            <th class="text-left" style="width: 30%;">
                                <asp:Label ID="Label35" runat="server" Text="#" Font-Size="12px"></asp:Label>
                            </th>
                            <th class="text-center" style="width: 40%;">
                                <asp:Label ID="Label36" runat="server" Text="Particulars" Font-Size="12px"></asp:Label>
                            </th>
                            <th class="text-right" style="width: 30%;">
                                <asp:Label ID="Label37" runat="server" Text="Amount " Font-Size="12px"></asp:Label>
                            </th>

                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label38" runat="server" Text="1." Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-center">
                                <asp:Label ID="lblAdmissionFees0" runat="server" Font-Names="Courier New" Font-Size="12px">Other Fee</asp:Label>
                            </td>
                            <td class="text-right">
                                <asp:Label ID="lblAmount0" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>

                        </tr>
                    </table>
                </div>
                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0; border-top: 1.5px solid #5d5d5d !important;"  />
                </div>

                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;">
                        <tr id="chqBFee0" runat="server" visible="false">
                            <td class="text-left" style="width: 40%;">
                                <asp:Label ID="Label19" runat="server" Text="Cheque Bounce Fee" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right" style="width: 60%;">
                                <asp:Label ID="lblchkBouncefee0" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="divConcession0" visible="false">
                            <td class="text-left" style="width: 40%;">
                                <asp:Label ID="Label41" runat="server" Text="Concession Amount" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right" style="width: 60%;">
                                -<asp:Label ID="lblConcessionAmount0" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label43" runat="server" Text="Received Amount" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right">
                                <asp:Label ID="lblReceivedAmount0" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label45" runat="server" Text="Amount in Words" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right">
                                <asp:Label ID="lblAmountinWords0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>

                            </td>
                        </tr>
                        <tr runat="server" id="divNextDue0" visible="false">
                            <td class="text-left">
                                <asp:Label ID="Label30" runat="server" Text="Next Balance Amount" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right">
                                <asp:Label ID="lblNextBalancemount0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                       <%--  <tr>
                            <td colspan="4">
                                <asp:Label ID="lblRemark0" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>

                            <td colspan="4" class="text-right" style="font-family: Courier New; font-size: 11px; font-weight: bold;">
                                <hr style="margin: 0;" />
                                Received by 
                                                                <asp:Label ID="lblUserName0" runat="server" Text=""></asp:Label>&nbsp;on 
                                                                <asp:Label ID="lblFooterDate0" runat="server" Text=""></asp:Label></td>
                        </tr>--%>
                         <tr>
                             <td colspan="2">
                                 <asp:Label ID="lblRemark0" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                 <hr style="margin: 0; padding: 0;" />
                             </td>
                         </tr>
                         <tr>
                             <td  class="text-left" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;width:50%">
                                 <div id="grdfeeDetailscancelleddiv1" runat="server" visible="false">
                                     Cancelled by
                                           <asp:Label ID="Label2" runat="server" Text=""></asp:Label>&nbsp;on 
                                           <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                 </div>
         
                             </td>

                             <td  class="text-right" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;width:50%">
                                 Received by
                                   <asp:Label ID="lblUserName0" runat="server" Text=""></asp:Label>&nbsp;on 
                                   <asp:Label ID="lblFooterDate0" runat="server" Text=""></asp:Label>                                        
                             </td>
                         </tr>
                        
                        <tr id="v_hr" runat="server">
                            <td colspan="2">
                                <hr style="margin: 0; padding: 0;" />
                            </td>
                        </tr>
                        <tr id="v_r1" runat="server">
                            <td class="text-center" colspan="2" style="font-family: Courier New; font-size: 11px; padding: 5px 0 0 0;">
                                <asp:Label ID="lblRemark1_1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr id="v_r2" runat="server">
                            <td class="text-center" colspan="2" style="font-family: Courier New; font-size: 11px; padding: 5px 0 0 0;">
                                <asp:Label ID="lblRemark2_1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    
                </div>
            </div>


        </div>
    </div>

</asp:Content>

