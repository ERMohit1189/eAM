<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="pafReciept.aspx.cs"
    Inherits="_2.AdminPafReciept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .style1 {
            height: 23px;
        }
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
            <div class="col-sm-12">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-6 no-padding">
                            <asp:Label ID="lblMedium" runat="server" Text="Receipt No. is : " class="  no-padding txt-bold  "></asp:Label>
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" Style="color: #CC0000; font-weight: 700" Text="Label"></asp:Label>
                        </div>
                        <div class="col-sm-6 no-padding text-right menu-action">

                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn-print-box" Style="color: #CC0000"
                                title="Go back to Admission Form Fee Deposit" data-placement="left"><i class="fa fa-reply"></i></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-print-box" title="Print Receipt" data-placement="left"><i class="icon-printer"></i></asp:LinkButton>
                        </div>

                        <div class="col-md-12 no-padding">
                            <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30">
                                <div class="col-sm-12 no-padding print-row marg-bot-30" runat="server" id="abc">
                                    <div class="col-sm-12 print-row fee-d-box-nhl">
                                        <div class="col-sm-12 ">
                                            <table style="width: 100%;">                                             
                                              <tr>
                                                   <td style="width: 100%" class="print-font-set" id="header1" runat="server"></td>
                                               </tr>
                                               <tr>
                                                   <td>
                                                       <div class="print-side-titel-box text-center">
                                                           <h3 class="main-name-l text-center">
                                                               <asp:Label ID="Label19" runat="server" Text="FEE RECEIPT"></asp:Label>
                                                               (ORIGINAL)
                                                           </h3>
                                                       </div>
                                                   </td>
                                               </tr>                                                   
                                            </table>
                                        </div>
                                        <div class="col-sm-12 ">
                                            <hr style="margin: 0; padding: 0;" />
                                        </div>

                                        <div class="col-sm-12 ">
                                            <table style="width: 100%; margin: 0; font-family: Courier New !important;">
                                                <tr>
                                                    <td class="text-left" style="width: 13%;">
                                                        <asp:Label ID="Label5" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td class="text-left" style="width: 35%;">: &nbsp;
                                           <asp:Label ID="lblRecieptNo" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td class="text-left" style="width: 7%;">
                                                        <asp:Label ID="Label8" runat="server" Text="Date " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td class="text-left" style="width: 30%;">: &nbsp;
                                           <asp:Label ID="lblRecieptDate" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="Student's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td>: &nbsp;
                            <asp:Label ID="lblStudentName" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <%-- <td>
                                <asp:Label ID="Label10" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td>: &nbsp;
                            <asp:Label ID="lblClass" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                                <asp:Label ID="lblBranch" runat="server" Text=""></asp:Label>
                            </td>--%>
                                                    <td class="text-left" style="width: 13%;">
                                                        <asp:Label ID="Label9" runat="server" Text="Father's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td>: &nbsp;
                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                                                                                                <td>
                            <asp:Label ID="Label22" runat="server" Text="Session " Font-Bold="False" Font-Size="12px"></asp:Label>
                        </td>
                        <td>: &nbsp;
<asp:Label ID="lblSession" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                        </td>
                                                    
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server" Text="Contact No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td>: &nbsp;
                            <asp:Label ID="lblContactNo" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>                 
                                                </tr>
                                                <tr>
                                                                            <td>
                            <asp:Label ID="Label12" runat="server" Text="Mode  " Font-Bold="False" Font-Size="12px"></asp:Label>
                        </td>
                        <td>: &nbsp;
<asp:Label ID="lblMode" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                        </td>
                                                                                                                                <td>
                            <asp:Label ID="Label6" runat="server" Text="Status " Font-Bold="False" Font-Size="12px"></asp:Label>
                        </td>
                        <td>: &nbsp;
<asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                        </td>
                                                                            
                                                </tr>

                                            </table>
                                        </div>

                                        <div class="col-sm-12 ">
                                            <hr style="margin: 0; padding: 0;" />
                                        </div>

                                        <div class="col-sm-12 ">
                                            <table style="width: 100%; margin: 0; font-family: Courier New !important;">
                                                <tr>
                                                    <th class="text-left" style="width: 15%;">
                                                        <asp:Label ID="Label13" runat="server" Text="#" Font-Size="12px"></asp:Label>
                                                    </th>
                                                    <th class="text-left" style="width: 65%;">
                                                        <asp:Label ID="Label14" runat="server" Text="Particulars" Font-Size="12px"></asp:Label>
                                                    </th>
                                                    <th class="text-right" style="width: 20%;">
                                                        <asp:Label ID="Label15" runat="server" Text="Amount " Font-Size="12px"></asp:Label>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td class="text-left">
                                                        <asp:Label ID="Label16" runat="server" Text="1." Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td class="text-left">
                                                        <asp:Label ID="lblAdmissionFees" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblAmount" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td class="text-left">
                                                        <asp:Label ID="Label20" runat="server" Text="Discount" Font-Size="12px"></asp:Label>
                                                    </td>

                                                    <td class="text-right"  colspan="2">-<asp:Label ID="lblConcession" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                        </div>

                                        <div class="col-sm-12 ">
                                            <hr style="margin: 0; padding: 0;" />
                                        </div>

                                        <div class="col-sm-12 ">
                                            <table style="width: 100%; margin: 0; font-family: Courier New !important;">
                                                <tr id="chqBFee" runat="server">
                                                    <td class="text-left" style="width: 40%;">
                                                        <asp:Label ID="Label4" runat="server" Text="Cheque Bounce Fee" Font-Size="12px"></asp:Label>
                                                    </td>

                                                    <td class="text-right" style="width: 60%;">
                                                        <asp:Label ID="lblchkBouncefee" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="text-left" style="width: 40%;">
                                                        <asp:Label ID="Label2" runat="server" Text="Received Amount" Font-Size="12px"></asp:Label>
                                                    </td>

                                                    <td class="text-right" style="width: 60%;">
                                                        <asp:Label ID="lblReceivedAmount" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="text-left" style="width: 40%;">
                                                        <asp:Label ID="Label17" runat="server" Text="Amount in Words" Font-Size="12px"></asp:Label>
                                                    </td>

                                                    <td class="text-right" style="width: 60%;">
                                                        <asp:Label ID="lblAmountinWords" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                               <tr>
                                                   <td colspan="2">
                                                       <hr style="margin: 0; padding: 0;" />
                                                   </td>
                                               </tr>
                                                <tr>
                                                     <td class="text-right" colspan="2" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;width:50%">
                                                       <%--  Generated by eAM&reg; --%> 
                                                                                                         Received by
                                                         <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;on 
                                                         <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label>
                                                     </td>
                                                </tr>
                                                 <tr runat="server" id="v_hr">
                                                    <td colspan="2">
                                                        <hr style="margin: 0; padding: 0;" />
                                                    </td>
                                                </tr>
                                                 <tr runat="server" id="v_r1">
                                                     <td class="text-center" colspan="2" style="font-family: Courier New; font-size: 11px; padding: 5px 0 0 0;">
                                                         <asp:Label ID="lblRemark1" runat="server" Text=""></asp:Label>
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
                                        <div class="col-sm-12 no-padding ">
                                            <table style="width: 100%; margin: 0; font-family: Courier New !important;">
                                                <tr>
                                                    <td style="width: 100%" class="print-font-set" id="header2" runat="server"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="print-side-titel-box text-center">
                                                            <h3 class="main-name-l text-center">
                                                                <asp:Label ID="Label3" runat="server" Text="FEE RECEIPT"></asp:Label>
                                                                (ORIGINAL)
                                                                 <%--<asp:Label ID="lblCancel" runat="server"></asp:Label>--%>
                                                            </h3>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-sm-12 ">
                                            <hr style="margin: 0; padding: 0;" />
                                        </div>

                                        <div class="col-sm-12 ">
                                            <table style="width: 100%; margin: 0; font-family: Courier New !important;">
                                                <tr>
                                                    <td class="text-left" style="width: 13%;">
                                                        <asp:Label ID="Label18" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td class="text-left" style="width: 35%;">: &nbsp;
                                           <asp:Label ID="lblRecieptNo0" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td class="text-left" style="width: 7%;">
                                                        <asp:Label ID="Label21" runat="server" Text="Date " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td class="text-left" style="width: 30%;">: &nbsp;
                                           <asp:Label ID="lblRecieptDate0" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="text-left">
                                                        <asp:Label ID="Label23" runat="server" Text="Student's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td class="text-left">: &nbsp;
                            <asp:Label ID="lblStudentName0" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <%-- <td class="text-left" >
                                <asp:Label ID="Label25" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" >: &nbsp;
                            <asp:Label ID="lblClass0" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                                <asp:Label ID="lblBranch0" runat="server" Text=""></asp:Label>
                            </td>--%>
                                                    <td class="text-left" style="width: 13%;">
                                                        <asp:Label ID="Label29" runat="server" Text="Father's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td class="text-left">: &nbsp;
                            <asp:Label ID="lblFatherName0" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                                                                                                <td>
                            <asp:Label ID="Label25" runat="server" Text="Session " Font-Bold="False" Font-Size="12px"></asp:Label>
                        </td>
                        <td>: &nbsp;
<asp:Label ID="lblSession0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                        </td>
                                                   
                                                    <td class="text-left">
                                                        <asp:Label ID="Label33" runat="server" Text="Contact No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td class="text-left">: &nbsp;
                            <asp:Label ID="lblContactNo0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                                            <td class="text-left">
                            <asp:Label ID="Label31" runat="server" Text="Mode  " Font-Bold="False" Font-Size="12px"></asp:Label>
                        </td>
                        <td class="text-left">: &nbsp;
<asp:Label ID="lblMode0" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                        </td>
                                                                                                                                <td>
                            <asp:Label ID="Label10" runat="server" Text="Status " Font-Bold="False" Font-Size="12px"></asp:Label>
                        </td>
                        <td>: &nbsp;
<asp:Label ID="lblStatus0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                        </td>
                                                                            
                                                </tr>
                                            </table>
                                        </div>

                                        <div class="col-sm-12 ">
                                            <hr style="margin: 0; padding: 0;" />
                                        </div>

                                        <div class="col-sm-12 ">
                                            <table style="width: 100%; margin: 0; font-family: Courier New !important;">
                                                <tr>
                                                    <th class="text-left" style="width: 30%;">
                                                        <asp:Label ID="Label35" runat="server" Text="#" Font-Size="12px"></asp:Label>
                                                    </th>
                                                    <th class="text-left" style="width: 40%;">
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
                                                    <td class="text-left">
                                                        <asp:Label ID="lblAdmissionFees0" runat="server" Font-Names="Courier New" Font-Size="12px">Registration Form of </asp:Label>
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblAmount0" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td class="text-left">
                                                        <asp:Label ID="Label41" runat="server" Text="Discount" Font-Size="12px"></asp:Label>
                                                    </td>

                                                    <td class="text-right" colspan="2">-<asp:Label ID="lblConcession1" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                        <div class="col-sm-12 ">
                                            <hr style="margin: 0; padding: 0;" />
                                        </div>

                                        <div class="col-sm-12 ">
                                            <table style="width: 100%; margin: 0; font-family: Courier New !important;">
                                                <tr id="chqBFee0" runat="server">
                                                    <td class="text-left" style="width: 40%;">
                                                        <asp:Label ID="Label46" runat="server" Text="Cheque Bounce Fee" Font-Size="12px"></asp:Label>
                                                    </td>

                                                    <td class="text-right" style="width: 60%;">
                                                        <asp:Label ID="lblchkBouncefee0" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="text-left">
                                                        <asp:Label ID="Label43" runat="server" Text="Received Amount" Font-Size="12px"></asp:Label>
                                                    </td>

                                                    <td class="text-right">
                                                        <asp:Label ID="lblReceivedAmount1" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>

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
                                                <tr>
                                                   <td colspan="2" >
                                                       <hr style="margin: 0; padding: 0;" />
                                                   </td>
                                               </tr>
                                                <tr>
                                                     <td class="text-right" colspan="2" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;width:50%">
                                                       <%--  Generated by eAM&reg; --%> 
                                                                                                      Received by
                                                             <asp:Label ID="lblUserName0" runat="server" Text=""></asp:Label>&nbsp;on 
                                                             <asp:Label ID="lblFooterDate0" runat="server" Text=""></asp:Label>
                                                     </td>
                                                </tr>
                                                 <tr runat="server" id="v_hr1">
                                                    <td colspan="2">
                                                        <hr style="margin: 0; padding: 0;" />
                                                    </td>
                                                </tr>
                                                 <tr runat="server" id="v_r2">
                                                     <td class="text-center" colspan="2" style="font-family: Courier New; font-size: 11px; padding: 5px 0 0 0;">
                                                         <asp:Label ID="lblRemark2" runat="server" Text=""></asp:Label>
                                                     </td>
                                                 </tr>
                                            </table>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>



</asp:Content>
