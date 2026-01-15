<%@ Page Title="" Language="C#" MasterPageFile="~/sp/sp_root-manager.master" AutoEventWireup="true" CodeFile="HostelFeeReciept.aspx.cs" Inherits="HostelFeeReciept" %>

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
                          
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn-print-box" PostBackUrl="~/sp/HostelFeePayment.aspx" Style="color: #CC0000"
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
                     <table class="print-table-text">
                        <tr>
                            <td style="width: 75%" class="print-font-set" id="header1" runat="server"></td>
                            <td style="width: 25%; vertical-align: top;">
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="Label252" runat="server" Text="FEE RECEIPT"  Font-Size="15"></asp:Label><br /><asp:Label ID="Label15" runat="server" Text="(ORIGINAL)"  Font-Size="15"></asp:Label></h3>
                                    <h3 class="sub-adds-l text-center">
                                        <asp:Label ID="lblCancel" runat="server"></asp:Label>STUDENT'S COPY <br/>
                                        <asp:Label ID="Label2" runat="server" Text="(Canceled)" Visible="false"></asp:Label></h3>
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

                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
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
                                <ItemStyle HorizontalAlign="left" Width="3%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Receipt No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ReceiptNo" runat="server" Text='<%# Bind("ReceiptNo") %>' Font-Size="12px"></asp:Label>
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
                            <asp:TemplateField HeaderText="Fine">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Fine" runat="server" Text='<%# Bind("Fine","{0:N2}") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="8%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Exemption">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Concession" runat="server" Text='<%# Bind("Exemption") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Total" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Received">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Received" runat="server" Text='<%# Bind("Paid") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Balance" runat="server" Text='<%# Bind("NextDue") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PaymentSatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" Font-Names="Courier New" Font-Size="12px" />
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
                        <tr>
                           <td colspan="4" style="font-family: Courier New; font-size: 11px; font-weight: bold; text-align: right; border-top: 1px solid;">Generated by eAM&reg; | Received by 
                                                                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;on 
                                                                <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>

           <asp:HiddenField runat="server" ID="hdnClsssId" />
        </div>
    </div>

</asp:Content>


