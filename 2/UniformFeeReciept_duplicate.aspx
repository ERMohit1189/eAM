<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="UniformFeeReciept_duplicate.aspx.cs" Inherits="UniformFeeReciept_duplicate" %>

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
                          
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn-print-box" PostBackUrl="~/2/UniformFeeDeposit.aspx" Style="color: #CC0000"
                                title="Go back to Other Fee Deposit" data-placement="left"><i class="fa fa-reply"></i></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-print-box" title="Print Receipt" data-placement="left">
                                <i class="icon-printer"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30">
        <div class="col-sm-12 no-padding print-row marg-bot-30" runat="server" id="xyz">

            <div class="col-sm-12 print-row fee-d-box-nhl">
                <div class="col-sm-12 no-padding">
                   <%--  <table class="print-table-text">
                        <tr>
                            <td style="width: 75%" class="print-font-set" id="header1" runat="server"></td>
                            <td style="width: 25%; vertical-align: top;">
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="Label252" runat="server" Text="INVOICE"  Font-Size="15"></asp:Label><br /><asp:Label ID="Label15" runat="server" Text="(DUPLICATE)"  Font-Size="15"></asp:Label></h3>
                                    <h3 class="sub-adds-l text-center">
                                        <asp:Label ID="lblCancel" runat="server"></asp:Label>STUDENT'S COPY <br/>
                                        <asp:Label ID="Label2" runat="server" Text="(Canceled)" Visible="false"></asp:Label></h3>
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
                                        <asp:Label ID="lblCancelStudent" runat="server" Text="INVOICE"></asp:Label>
                                        <asp:Label ID="lblDuplicateStudent" runat="server" Text=""></asp:Label>
                                    </h3>
                                    <h3 class="sub-adds-l text-center" style="display: none">STATUS&nbsp;(<asp:Label ID="Label2" runat="server" Text=""></asp:Label>)
                                    </h3>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0;" />
                </div>

                <div class="col-sm-12 ">
                     <table class="print-table-text">
                        <tr>
                            <td class="text-left" style="width: 13%;">
                                <asp:Label ID="Label333" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
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
                            <td class="text-left" >
                                <asp:Label ID="Label9" runat="server" Text="Student's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" >: &nbsp;
                            <asp:Label ID="lblStudentName" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" >
                                <asp:Label ID="Label10" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" >: &nbsp;
                            <asp:Label ID="lblClass" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                                 (<asp:Label ID="lblSection" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>)
                                <asp:Label ID="lblBranch" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left" >
                                <asp:Label ID="Label11" runat="server" Text="Father's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" >: &nbsp;
                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" >
                                <asp:Label ID="Label12" runat="server" Text="S.R. No.  " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" >: &nbsp;
                           
                                <asp:Label ID="lblSrno" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label15" runat="server" Text="Mode" Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblMode" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label17" runat="server" Text="Status" Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="text-left" >
                                <asp:Label ID="Label13" runat="server" Text="Gender " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" >: &nbsp;
                            <asp:Label ID="lblGender" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" >
                                <asp:Label ID="Label14" runat="server" Text="Section" Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" >: &nbsp;
                               
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0;border-top: 1.5px solid #5d5d5d;" />
                </div>
                <div  class="col-sm-12 ">
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="None" CssClass="print-table-text" Font-Size="12px" >
                                    <Columns>

                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_sr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="left" Width="5%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Particulars">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_particular" runat="server" Text='<%# Bind("HeadName") %>' Font-Size="12px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="35%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Qty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="12%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="12%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Total" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="12%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                        </Columns>
                                </asp:GridView>
                    <table class="print-table-text">
                        <tr>
                            <th class="text-left" style="width: 86%;">
                                <asp:Label ID="Label4" runat="server" Text="Total Amount" Font-Size="12px"></asp:Label>
                            </th>

                            <th class="text-left" style="border-top: 1px solid;text-align: right;">
                                <asp:Label ID="lblHeadTotal" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </th>
                            
                        </tr>
                        </table>
                </div>
                 <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0;" />
                </div>
                <div class="col-sm-12 ">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="None" CssClass="print-table-text" Font-Size="12px" >
                        <Columns>

                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_sr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="left" Width="5%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Receipt No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Receipt_no" runat="server" Text='<%# Bind("Receipt_no") %>' Font-Size="12px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="20%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reciept Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Receipt_date" runat="server" Text='<%# Bind("RecieptDate") %>' Font-Size="12px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="13%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="8%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Exemption">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Concession" runat="server" Text='<%# Bind("Concession") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="12%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Received">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Received" runat="server" Text='<%# Bind("ReceivedAmount") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="14%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Balance" runat="server" Text='<%# Bind("NextDeuAmt") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="14%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PaymentSatus" runat="server" Text='<%# Bind("PaymentSatus") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="14%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0;border-top: 1.5px solid #5d5d5d;" />
                </div>

                <div class="col-sm-12 ">
                    <table class="print-table-text">
                        <tr>
                            <th class="text-left" style="width: 20%;">
                                <asp:Label ID="Label20" runat="server" Text="Paid Amount" Font-Size="12px"></asp:Label>
                            </th>

                            <th class="text-right" style="width: 80%;">
                                <asp:Label ID="lblTotalAmt" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>.00
                            </th>
                        </tr>

                        <tr>
                            <th class="text-left" style="width: 20%;">
                                <asp:Label ID="Label19" runat="server" Text="Amount in Words" Font-Size="12px"></asp:Label>
                            </th>

                            <th class="text-right" style="width: 80%;">
                                <asp:Label ID="lblAmountinWords" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>

                            </th>
                        </tr>

                        <tr>
                            <th class="text-left" colspan="2">
                                <asp:Label ID="lblremark" runat="server" Text="" Font-Bold="True" Font-Size="11px"></asp:Label>
                            </th>
                            
                        </tr>
                    </table>
                </div>

                <div class="col-sm-12 ">
                     <table class="print-table-text">
                       <%-- <tr>
                           <td colspan="4" style="font-family: Courier New; font-size: 11px; font-weight: bold; text-align: right; border-top: 1px solid;">Generated by eAM&reg; | Received by 
                                                                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;on 
                                                                <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label></td>
                        </tr>--%>
                       <tr>
                            <td colspan="2">
                                <hr style="margin: 0; padding: 0;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;width:50%">
                                <div id="grdfeeDetailscancelleddiv" runat="server" visible="false">
                                    Cancelled by
                                          <asp:Label ID="Label34" runat="server" Text=""></asp:Label>&nbsp;on 
                                          <asp:Label ID="Label39" runat="server" Text=""></asp:Label>
                                </div>
         
                            </td>

                            <td class="text-right" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;width:50%">
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
                <div class="col-sm-12 no-padding ">
                     <%--<table class="print-table-text">
                        <tr>
                            <td style="width: 75%" class="print-font-set" id="header2" runat="server"></td>
                            <td style="width: 25%; vertical-align: top;">
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="Label6" runat="server" Text="INVOICE"  Font-Size="15"></asp:Label><br /><asp:Label ID="Label16" runat="server" Text="(DUPLICATE)"  Font-Size="15"></asp:Label></h3>
                                    <h3 class="sub-adds-l text-center">
                                        <asp:Label ID="Label22" runat="server"></asp:Label>SCHOOL'S COPY <br/>
                                        <asp:Label ID="Label3" runat="server" Text="(Canceled)" Visible="false"></asp:Label></h3>
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
                                         <asp:Label ID="lblCancelSCHOOL" runat="server" Text="INVOICE"></asp:Label>
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
                    <hr style="margin: 3px 0; padding: 0;" />
                </div>

                <div class="col-sm-12 ">
                     <table class="print-table-text">
                        <tr>
                            <td class="text-left" style="width: 13%;">
                                <asp:Label ID="Label5" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" style="width: 35%;">: &nbsp;
                                           <asp:Label ID="lblRecieptNo0" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" style="width: 7%;">
                                <asp:Label ID="Label77" runat="server" Text="Date " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left" style="width: 30%;">: &nbsp;
                                           <asp:Label ID="lblRecieptDate0" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label99" runat="server" Text="Student's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblStudentName0" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label110" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblClass0" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                                (<asp:Label ID="lblSection0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>)
                                <asp:Label ID="lblBranch0" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label911" runat="server" Text="Father's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblFatherName0" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label192" runat="server" Text="S.R. No.  " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                           
                                <asp:Label ID="lblSrno0" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="text-left">
                                <asp:Label ID="Label16" runat="server" Text="Mode" Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblMode0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label21" runat="server" Text="Status" Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                                <asp:Label ID="lblStatus0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="text-left">
                                <asp:Label ID="Label193" runat="server" Text="Gender " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblGender0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label194" runat="server" Text="Section" Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                                
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0;border-top: 1.5px solid #5d5d5d;" />
                </div>
                <div  class="col-sm-12 ">
                    <asp:GridView ID="GridView30" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="None" CssClass="print-table-text" Font-Size="12px" >
                                    <Columns>

                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_sr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="left" Width="5%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Particulars">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_particular" runat="server" Text='<%# Bind("HeadName") %>' Font-Size="12px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="35%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Qty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="12%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="12%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Total" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="12%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                        </Columns>
                                </asp:GridView>
                    <table class="print-table-text">
                        <tr>
                            <th class="text-left" style="width: 86%;">
                                <asp:Label ID="Label8" runat="server" Text="Total Amount" Font-Size="12px"></asp:Label>
                            </th>

                            <th class="text-left" style="border-top: 1px solid;text-align: right;">
                                <asp:Label ID="lblHeadTotal0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </th>
                            
                        </tr>
                        </table>
                </div>
                 <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0;" />
                </div>
                <div class="col-sm-12 ">
                    <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="None" CssClass="print-table-text" Font-Size="12px" >
                        <Columns>

                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_sr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="left" Width="5%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Receipt No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Receipt_no" runat="server" Text='<%# Bind("Receipt_no") %>' Font-Size="12px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="20%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reciept Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Receipt_date" runat="server" Text='<%# Bind("RecieptDate") %>' Font-Size="12px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="13%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="8%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Exemption">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Concession" runat="server" Text='<%# Bind("Concession") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="12%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Received">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Received" runat="server" Text='<%# Bind("ReceivedAmount") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="14%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Balance" runat="server" Text='<%# Bind("NextDeuAmt") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="14%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PaymentSatus" runat="server" Text='<%# Bind("PaymentSatus") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="14%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0; padding: 0;border-top: 1.5px solid #5d5d5d;" />
                </div>

                <div class="col-sm-12 ">
                    <table class="print-table-text">
                        <tr>
                            <th class="text-left" style="width: 20%;">
                                <asp:Label ID="Label230" runat="server" Text="Paid Amount" Font-Size="12px"></asp:Label>
                            </th>

                            <th class="text-right" style="width: 80%;">
                                <asp:Label ID="lblTotalAmt0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>.00
                            </th>
                        </tr>

                        <tr>
                            <th class="text-left"  style="width: 20%;">
                                <asp:Label ID="Label139" runat="server" Text="Amount in Words" Font-Size="12px"></asp:Label>
                            </th>

                            <th class="text-right"  style="width: 80%;">
                                <asp:Label ID="lblAmountinWords0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>

                            </th>
                        </tr>
                        <tr>
                            <th class="text-left" colspan="2">
                                <asp:Label ID="lblremark0" runat="server" Text="" Font-Bold="True" Font-Size="11px"></asp:Label>
                            </th>
                            
                        </tr>
                    </table>
                </div>

                <div class="col-sm-12 ">
                     <table class="print-table-text">
                    <%--    <tr>
                           <td colspan="4" style="font-family: Courier New; font-size: 11px; font-weight: bold; text-align: right; border-top: 1px solid;">Generated by eAM&reg; | Received by 
                                                                <asp:Label ID="lblUserName0" runat="server" Text=""></asp:Label>&nbsp;on 
                                                                <asp:Label ID="lblFooterDate0" runat="server" Text=""></asp:Label></td>
                        </tr>--%>
                         <tr>
                             <td colspan="2">
                                 <asp:Label ID="Label3" runat="server" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                 <hr style="margin: 0; padding: 0;" />
                             </td>
                         </tr>
                         <tr>
                             <td class="text-left" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;width:50%">
                                 <div id="grdfeeDetailscancelleddiv1" runat="server" visible="false">
                                     Cancelled by
                                           <asp:Label ID="Label6" runat="server" Text=""></asp:Label>&nbsp;on 
                                           <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                 </div>
         
                             </td>

                             <td class="text-right" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;width:50%">
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
                <asp:HiddenField runat="server" ID="hdnClsssId" />
            </div>
        </div>
    </div>

</asp:Content>


