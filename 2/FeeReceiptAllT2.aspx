<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="FeeReceiptAllT2.aspx.cs" Inherits="FeeReceiptAllT2" %>

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
        .table > tbody > tr > td {
            line-height: 1;
        }
        table > tbody > tr > td {
            line-height: 1;
        }
        .main-titel-box h1 {
            font-size: 12px !important;
        }
        .main-titel-box h3 {
            font-size: 11px !important;
        }
        .logo-size {
    width: 55px !important;
    height: auto !important;
}
        .append-icon {
    margin-left: 2px !important;
    margin-right: 0px !important;
}
        .pd-left {
    padding: 0 5px !important;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-lg-12" style="    padding: 10px 10px;">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-lg-6">
                            <asp:Label ID="lblMedium" runat="server" Text="Receipt No. is : " class="  no-padding txt-bold  "></asp:Label>
                            
                            <asp:Label ID="Label1" runat="server" Style="color: #CC0000; font-weight: 700" Text=""></asp:Label>
                        </div>
                        <div class="col-lg-6 no-padding text-right menu-action">

                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn-print-box" PostBackUrl="~/2/CompositFeeDeposit.aspx" Style="color: #CC0000"
                                title="Go back to Fee Deposit"><i class="icon-reply"></i></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-print-box"
                                title="Print Receipt"><i class="icon-printer"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30">
        <div class="col-sm-12 no-padding print-row marg-bot-30" runat="server" id="abc">
            <div class="col-sm-6 print-row fee-d-box-nhl" style="padding: 5px; width:49.5%; float:left;">
                <div class="col-sm-12" style="padding:5px 7px 0px 7px;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%" class="print-font-set" id="header_1" runat="server">
                                <div class="text-left col-lg-2 col-md-2 col-xs-2 col-sm-2 no-padding" style="padding-left: 0px; text-align: center;">
                                    <div class="mgbt-xs-5 p-mgbt-xs-5">
                                        <asp:Image runat="server" ID="image1"  class="logo-size" />
                                    </div>
                                </div>
                                <div class="text-center col-lg-10 col-md-10 col-xs-10 col-sm-10 no-padding ">
                                    <div class="main-titel-box">
                                        <h1 class="main-name">
                                            <asp:Label runat="server" ID="lblInstitute1"  style="font-weight: bold;font-size: 11px !important;"></asp:Label>
                                        </h1>
                                        <h3 class="sub-adds ">
                                            <asp:Label runat="server" ID="lblAddress1"  style="font-size: 10px !important;"></asp:Label>
                                        </h3>
                                        <h3 class="sub-adds" style="white-space:nowrap;font-size: 10px !important;">
                                            <i class="fa fa-phone-square" style="margin-left: 2px !important; margin-right: 1px !important;"></i><asp:Label runat="server" ID="lblContactno1"></asp:Label>&nbsp;
                                            <i class="fa fa-envelope-o" style="margin-left: 2px !important; margin-right: 1px !important;"></i><asp:Label runat="server" ID="lblEmail1"></asp:Label>
                                        </h3>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-sm-12 " style="padding:0px 7px 0px 7px;">
                    <hr style="margin: 0; padding: 0;" />
                </div>
                <div class="col-sm-12 " style="padding: 7px;">
                    <table class="table no-head-border" style="margin: 0; font-family: Courier New !important;">
                        <asp:Repeater ID="rptStudentDetails" runat="server">
                            <ItemTemplate>

                                <tr>
                                    <td class="text-left" style="width: 22%; padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label2" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 78%; padding: 0px !important; line-height: 1;" colspan="3">:<asp:Label ID="Label7" runat="server" Font-Bold="True" Text='<%# Eval("ReceiptNo") %>' Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 22%;padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Student's Name " Font-Size="10px" style="white-space:nowrap;"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 78%;padding: 0px !important; line-height: 1;" colspan="3">:<asp:Label ID="Label8" runat="server" Text='<%# Eval("Name") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                    
                                </tr>
                                <tr>
                                     <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label22" runat="server" Text="Father's Name" Font-Bold="False" Font-Size="10px" style="white-space:nowrap;"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;" colspan="3">:<asp:Label ID="Label23" runat="server" Text='<%# Eval("FatherName") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 22%; padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label20" runat="server" Text="S.R. No. " Font-Bold="False" Font-Size="10px" style="white-space:nowrap;"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 43%; padding: 0px !important; line-height: 1;">:<asp:Label ID="Label24" runat="server" Text='<%# Eval("srNo") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label4" runat="server" Text="Class " Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">:<asp:Label ID="Label9" runat="server" Text='<%# Eval("ClassName") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label28" runat="server" Text="Mode" Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">:<asp:Label ID="Label29" runat="server" Text='<%# Eval("Mode") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                     <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label6" runat="server" Text="Status" Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">:<asp:Label ID="Label11" runat="server" Text='<%# Eval("receiptStatus") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label13" runat="server" Text="Contact No." Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1; white-space:nowrap;">:<asp:Label ID="Label1f5" runat="server" Text='<%# Eval("FatherContactNo") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label5" runat="server" Text="Date " Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1; white-space:nowrap;">:<asp:Label ID="Label34" runat="server" Text='<%# Eval("DepositDate") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
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
                                    <table class="table table-striped no-head-border" style="margin-bottom: 0;background-color: #E0E0E0 !important;border-bottom: 1px solid; border-top: 1px solid;">
                                        <tr>
                                            <th class="text-left" style="width: 5%;font-family: Courier New;background-color: #E0E0E0 !important;">
                                                <asp:Label ID="Label39" runat="server" Text="#" Font-Size="10px"></asp:Label>
                                            </th>
                                            <th class="text-left" style="width: 75%;font-family: Courier New;background-color: #E0E0E0 !important;">
                                                <asp:Label ID="Label40" runat="server" Text="Particulars" Font-Size="10px"></asp:Label>
                                            </th>
                                            <th style="width: 20%;font-family: Courier New; text-align: right;background-color: #E0E0E0 !important;">
                                                <asp:Label ID="Label41" runat="server" Text="Amount" Font-Size="10px"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="table no-head-border" style="margin-bottom: 0">
                                        <tr <%# Eval("feetype").ToString()=="Fee"?"": "style='background: #f4f4f4;'" %> >
                                            <td class="text-left pd-left" style="width: 5%;">
                                                <asp:Label ID="Label25" runat="server" Text='<%# Container.DataItemIndex+1 %>' Font-Size="10px" Font-Names="Courier New"></asp:Label>
                                            </td>
                                           <td class="text-left pd-left" style="width: 75%;">
                                                <asp:Label ID="Label12" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New"
                                                    Font-Size="10px" Text='<%# Bind("Particulars") %>' style="text-transform:uppercase"></asp:Label>

                                            </td>
                                            <td style="width: 20%;" class="text-right pd-left">
                                                <asp:Label ID="Label10" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New"
                                                    Font-Size="10px" Text='<%# Bind("amount") %>'></asp:Label>
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
                                                        <td colspan="2" class="text-left" style="width: 80%;">
                                                            <asp:Label ID="Label38" runat="server" Text="Total Amount" Font-Size="10px"></asp:Label><br />
                                                        </td>
                                                        <td style="padding: 0 5px !important; width: 20%; text-align: right;">
                                                            <asp:Label ID="Label18" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
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
                                            <td style="width: 47%;">
                                                <div class="col-lg-12 left-padd-0 right-bor-l" style="padding-right: 5px;">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 60%;" class="text-left">
                                                                <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount" Font-Size="10px"></asp:Label>
                                                            </td>
                                                            <td style="width: 40%;" class="text-right">
                                                                <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="hide">
                                                            <td class="text-left">
                                                                <asp:Label ID="Label17" runat="server" Font-Bold="False" Text="Next Due Amount" Font-Size="10px"></asp:Label>
                                                            </td>
                                                            <td class="text-right">
                                                                <asp:Label ID="lblNexDueAmt" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </td>
                                            <td style="width: 53%;">
                                                <div class="col-lg-12 right-padd-0" style="padding-left: 5px;">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 65%;" class="text-left">
                                                                <asp:Label ID="Label10" runat="server" Text="Received Amount" Font-Size="10px"></asp:Label>
                                                            </td>
                                                            <td style="width: 35%; padding: 0 5px !important;" class="text-right">
                                                                <asp:Label ID="Label14" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                                            <td colspan="2" style="text-align:right;">

                                                                <asp:Label ID="Label27" runat="server" Text="" Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                                            </td>
                                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <hr style="margin: 0; padding: 0;" />
                                            </td>
                                        </tr>
                                        <tr>

                                            <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;">Received by
                                                                  <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;on
                                                                  <asp:Label ID="lblFooterDate" runat="server" Text=""></asp:Label>
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
            <div class="col-sm-6 print-row fee-d-box-nhl" runat="server" id="Table1"  style="padding: 5px; width:49.5%; float:right;">
                <div class="col-sm-12" style="padding:5px 7px 0px 7px;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%" class="print-font-set" id="header_2" runat="server">
                                <div class="text-left col-lg-2 col-md-2 col-xs-2 col-sm-2 no-padding" style="padding-left: 0px; text-align: center;">
                                    <div class="mgbt-xs-5 p-mgbt-xs-5">
                                        <asp:Image runat="server" ID="image2"  class="logo-size" />
                                    </div>
                                </div>
                                <div class="text-center col-lg-10 col-md-10 col-xs-10 col-sm-10 no-padding ">
                                    <div class="main-titel-box">
                                        <h1 class="main-name">
                                            <asp:Label runat="server" ID="lblInstitute2"  style="font-weight: bold;font-size: 11px !important;"></asp:Label>
                                        </h1>
                                        <h3 class="sub-adds ">
                                            <asp:Label runat="server" ID="lblAddress2"  style="font-size: 10px !important;"></asp:Label>
                                        </h3>
                                        <h3 class="sub-adds" style="white-space:nowrap;font-size: 10px !important;">
                                            <i class="fa fa-phone-square" style="margin-left: 2px !important; margin-right: 1px !important;"></i><asp:Label runat="server" ID="lblContactno2"></asp:Label>&nbsp;
                                            <i class="fa fa-envelope-o" style="margin-left: 2px !important; margin-right: 1px !important;"></i><asp:Label runat="server" ID="lblEmail2"></asp:Label>
                                        </h3>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-sm-12" style="padding:0px 7px 0px 7px;">
                    <hr style="margin: 0; padding: 0;" />
                </div>

                <div class="col-sm-12" style="padding: 7px;">
                    <table class="table no-head-border" style="margin: 0; font-family: Courier New !important;">
                        <asp:Repeater ID="rptStudentDetails1" runat="server">
                            <ItemTemplate>

                                <tr>
                                    <td class="text-left" style="width: 22%; padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label2" runat="server" Text="Receipt No. " Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 78%; padding: 0px !important; line-height: 1;" colspan="3">:<asp:Label ID="Label7" runat="server" Font-Bold="True" Text='<%# Eval("ReceiptNo") %>' Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 22%;padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Student's Name " Font-Size="10px" style="white-space:nowrap;"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 78%;padding: 0px !important; line-height: 1;" colspan="3">:<asp:Label ID="Label8" runat="server" Text='<%# Eval("Name") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                    
                                </tr>
                                <tr>
                                     <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label22" runat="server" Text="Father's Name" Font-Bold="False" Font-Size="10px" style="white-space:nowrap;"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;" colspan="3">:<asp:Label ID="Label23" runat="server" Text='<%# Eval("FatherName") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 22%; padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label20" runat="server" Text="S.R. No. " Font-Bold="False" Font-Size="10px" style="white-space:nowrap;"></asp:Label>
                                    </td>
                                    <td class="text-left" style="width: 43%; padding: 0px !important; line-height: 1;">:<asp:Label ID="Label24" runat="server" Text='<%# Eval("srNo") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label4" runat="server" Text="Class " Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">:<asp:Label ID="Label9" runat="server" Text='<%# Eval("ClassName") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label28" runat="server" Text="Mode" Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">:<asp:Label ID="Label29" runat="server" Text='<%# Eval("Mode") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                     <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label6" runat="server" Text="Status" Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">:<asp:Label ID="Label11" runat="server" Text='<%# Eval("receiptStatus") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label13" runat="server" Text="Contact No." Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1; white-space:nowrap;">:<asp:Label ID="Label1f5" runat="server" Text='<%# Eval("FatherContactNo") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1;">
                                        <asp:Label ID="Label5" runat="server" Text="Date " Font-Bold="False" Font-Size="10px"></asp:Label>
                                    </td>
                                    <td class="text-left" style="padding: 0px !important; line-height: 1; white-space:nowrap;">:<asp:Label ID="Label34" runat="server" Text='<%# Eval("DepositDate") %>' Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
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
                                    <table class="table table-striped no-head-border" style="margin-bottom: 0;background-color: #E0E0E0 !important;border-bottom: 1px solid; border-top: 1px solid;">
                                        <tr>
                                            <th class="text-left" style="width: 5%;font-family: Courier New;background-color: #E0E0E0 !important;">
                                                <asp:Label ID="Label39" runat="server" Text="#" Font-Size="10px"></asp:Label>
                                            </th>
                                            <th class="text-left" style="width: 75%;font-family: Courier New;background-color: #E0E0E0 !important;">
                                                <asp:Label ID="Label40" runat="server" Text="Particulars" Font-Size="10px"></asp:Label>
                                            </th>
                                            <th style="width: 20%;font-family: Courier New; text-align: right;background-color: #E0E0E0 !important;">
                                                <asp:Label ID="Label41" runat="server" Text="Amount" Font-Size="10px"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="table no-head-border" style="margin-bottom: 0">
                                        <tr <%# Eval("feetype").ToString()=="Fee"?"": "style='background: #f4f4f4;'" %> >
                                            <td class="text-left pd-left" style="width: 5%;">
                                                <asp:Label ID="Label25" runat="server" Text='<%# Container.DataItemIndex+1 %>' Font-Size="10px" Font-Names="Courier New"></asp:Label>
                                            </td>
                                            <td class="text-left pd-left" style="width: 75%;">
                                                <asp:Label ID="Label12" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New"
                                                    Font-Size="10px" Text='<%# Eval("Particulars") %>' style="text-transform:uppercase"></asp:Label>

                                            </td>
                                            <td style="width: 20%;" class="text-right pd-left">
                                                <asp:Label ID="Label10" runat="server" CssClass="current" Font-Bold="False" Font-Names="Courier New"
                                                    Font-Size="10px" Text='<%# Eval("Amount") %>'></asp:Label>
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
                                                            <asp:Label ID="Label38" runat="server" Text="Total Amount" Font-Size="10px"></asp:Label><br />
                                                        </td>
                                                        <td  style="padding: 0 5px !important; width: 20%; text-align: right;">
                                                            <asp:Label ID="Label18_2" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
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
                                            <td style="width: 47%;">
                                                <div class="col-lg-12 left-padd-0 right-bor-l" style="padding-right: 5px;">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 60%;" class="text-left">
                                                                <asp:Label ID="Label31" runat="server" Font-Bold="False" Text="Balance Amount" Font-Size="10px"></asp:Label>
                                                            </td>
                                                            <td style="width: 40%;" class="text-right">
                                                                <asp:Label ID="Label30_2" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="hide">
                                                            <td class="text-left">
                                                                <asp:Label ID="Label17" runat="server" Font-Bold="False" Text="Next Due Amount" Font-Size="10px"></asp:Label>
                                                            </td>
                                                            <td class="text-right">
                                                                <asp:Label ID="lblNexDueAmt_2" runat="server" Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </td>
                                            <td style="width: 53%;">
                                                <div class="col-lg-12 right-padd-0"  style="padding-left: 5px;">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 65%;" class="text-left">
                                                                <asp:Label ID="Label10" runat="server" Text="Received Amount" Font-Size="10px"></asp:Label>
                                                            </td>
                                                            <td  class="text-right" style="width: 35%;padding: 0 5px !important;">
                                                                <asp:Label ID="Label14_2" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                                            <td colspan="2" style="text-align:right;">

                                                                <asp:Label ID="Label27_2" runat="server" Text="Label" Font-Bold="True" Font-Names="Courier New" Font-Size="10px"></asp:Label>
                                                            </td>
                                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <hr style="margin: 0; padding: 0;" />
                                            </td>
                                        </tr>
                                        <tr>

                                            <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px;padding: 5px 0 0 0;">Received by
                                                                  <asp:Label ID="lblUserName_2" runat="server" Text=""></asp:Label>&nbsp;on
                                                                  <asp:Label ID="lblFooterDate_2" runat="server" Text=""></asp:Label>
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

