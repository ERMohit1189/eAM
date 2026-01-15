<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MiscellaneousFeesReciept_duplicate.aspx.cs" Inherits="admin_miscellaneousFeesReciept_duplicate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <%--Content Starts--%>
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

                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn-print-box" PostBackUrl="~/2/additionalFeesDeposit.aspx" Style="color: #CC0000"
                                title="Go back to Miscellaneous Fee Deposit" data-toggle="tooltip" data-placement="left"><i class="fa fa-reply"></i></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-print-box"
                                title="Receipt Print" data-toggle="tooltip" data-placement="left">
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
                    <table style="font-family: Courier New; width: 100%;">
                        <tr>
                            <td style="width: 75%" class="print-font-set" id="header1" runat="server"></td>
                            <td style="width: 25%; vertical-align: top;">
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="Label252" runat="server" Text="MISCELLANEOUS FEE "></asp:Label></h3>
                                    <h3 class="sub-adds-l text-center">
                                        <asp:Label ID="lblCancel" runat="server"></asp:Label>STUDENT'S COPY <br/> (DUPLICATE)<asp:Label ID="Label2" runat="server" Text="(Canceled)" Visible="false"></asp:Label></h3>
                                </div>
                            </td>
                        </tr>

                    </table>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0px; padding: 0;" />
                </div>

                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;">
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
                                <asp:Label ID="lblBranch" runat="server" Text="" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                                   (<asp:Label ID="lblSection" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>)
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label11" runat="server" Text="Father's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" Text="Label" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label12" runat="server" Text="S.R. No.  " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                           
                                <asp:Label ID="lblSrno" runat="server" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr  style="display: none">
                            <td class="text-left">
                                <asp:Label ID="Label13" runat="server" Text="Gender " Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                            <asp:Label ID="lblGender" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">
                                <asp:Label ID="Label14" runat="server" Text="Section" Font-Bold="False" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="text-left">: &nbsp;
                                
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0px; padding: 0;" />
                </div>

                <div class="col-sm-12 ">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="print-table-text" Width="100%" GridLines="None" Font-Size="12px">
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
                                <ItemStyle HorizontalAlign="Left" Width="45%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Exemption">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Concession" runat="server" Text='<%# Bind("Concession") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Received Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Received" runat="server" Text='<%# Bind("ReceivedAmount") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="20%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0px; padding: 0;" />
                </div>

                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;" class="print-table-text">
                        <tr>
                            <td class="text-left" style="width: 40%;">
                                <asp:Label ID="Label20" runat="server" Text="Total Amount" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right" style="width: 60%;">
                                <asp:Label ID="lblTotalAmt" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>.00
                            </td>
                        </tr>

                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label19" runat="server" Text="Amount in Words" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right">
                                <asp:Label ID="lblAmountinWords" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>

                            </td>
                        </tr>

                        <tr>

                            <td colspan="4" align="right" style="font-family: Courier New; font-size: 11px; font-weight: bold;">Generated by eAM&reg; | Received by
                                                                <asp:Label ID="lblUserName" runat="server" Text="" style="text-transform:capitalize"></asp:Label>&nbsp;on 
                                                                <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </div>

                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;">
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label4" runat="server" Text="MISCELLANEOUS FEE :" Font-Bold="True" Font-Size="11px"></asp:Label>

                                <asp:Repeater ID="Repeater2" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("HeadName") %>' Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                </asp:Repeater>
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
                <div class="col-sm-12 no-padding">
                    <table style="font-family: Courier New; width: 100%;">
                        <tr>
                            <td style="width: 75%" class="print-font-set" id="header2" runat="server"></td>
                            <td style="width: 25%; vertical-align: top;">
                                <div class="print-side-titel-box text-center">
                                    <h3 class="main-name-l text-center">
                                        <asp:Label ID="Label6" runat="server" Text="MISCELLANEOUS FEE"></asp:Label></h3>
                                    <h3 class="sub-adds-l text-center">
                                        <asp:Label ID="Label22" runat="server"></asp:Label>SCHOOL'S COPY <br/>(DUPLICATE)<asp:Label ID="Label3" runat="server" Text="(Canceled)" Visible="false"></asp:Label></h3>
                                </div>
                            </td>
                        </tr>

                    </table>
                </div>

                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0px; padding: 0;" />
                </div>

                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;">
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
                                <asp:Label ID="lblBranch0" runat="server" Text="" Font-Names="Courier New" Font-Bold="True" Font-Size="12px"></asp:Label>
                                (<asp:Label ID="lblSection0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>)
                                
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
                    <hr style="margin: 3px 0px; padding: 0;" />
                </div>

                <div class="col-sm-12 ">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                        Width="100%" CssClass="print-table-text" GridLines="None">
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_sr0" runat="server" Text='<%# Container.DataItemIndex+1 %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="5%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Particulars">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_particular0" runat="server" Text='<%# Bind("HeadName") %>' Font-Names="Courier New"
                                        Font-Size="12px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="45%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_amount0" runat="server" Text='<%# Bind("Amount") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Exemption">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Concession0" runat="server" Text='<%# Bind("Concession") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Received0" runat="server" Text='<%# Bind("ReceivedAmount") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="20%" Font-Names="Courier New" Font-Size="12px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12 ">
                    <hr style="margin: 3px 0px; padding: 0;" />
                </div>
                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;" class="print-table-text">
                        <tr>
                            <td class="text-left" style="width: 40%;">
                                <asp:Label ID="Label230" runat="server" Text="Total Amount" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right" style="width: 60%;">
                                <asp:Label ID="lblTotalAmt0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>.00
                            </td>
                        </tr>

                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label139" runat="server" Text="Amount in Words" Font-Size="12px"></asp:Label>
                            </td>

                            <td class="text-right">
                                <asp:Label ID="lblAmountinWords0" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="12px"></asp:Label>

                            </td>
                        </tr>
                        <tr>

                            <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px; font-weight: bold;">Generated by eAM&reg; | Received by
                                                                <asp:Label ID="lblUserName0" runat="server" Text="" style="text-transform:capitalize"></asp:Label>&nbsp;on 
                                                                <asp:Label ID="lblFooterDate0" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </div>

                <div class="col-sm-12 ">
                    <table style="font-family: Courier New; width: 100%;">
                        <tr>
                            <td class="text-left">
                                <asp:Label ID="Label15" runat="server" Text="MISCELLANEOUS FEE : " Font-Bold="True" Font-Size="11px"></asp:Label>

                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("HeadName") %>' Font-Size="11px"></asp:Label>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

