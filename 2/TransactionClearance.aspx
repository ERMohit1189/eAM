<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TransactionClearance.aspx.cs" Inherits="TransactionClearance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery-1.4.3.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div id="Div1" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtFromDate" placeholder="From Date" runat="server" CssClass="form-control-blue datepicker-normal validatetxt1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtToDate" placeholder="To Date" runat="server" CssClass="form-control-blue datepicker-normal validatetxt1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="return ValidateTextBox('.validatetxt1');"
                                            CssClass="button form-control-blue">View</asp:LinkButton></td>
                                        <div id="msgbox" runat="server" style="left: 65px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <table class="table no-bm no-head-border table-bordered pro-table table-header-group text-center" id="tblList">
                                            <tr style="border:0px !important;" runat="server" id="trChkAll" visible="false">
                                                <td colspan="5" style="border:0px !important;"></td>
                                                <td  class="text-center" style="width:70px !important; border:0px !important;"><asp:CheckBox ID="chkAll" runat="server" Text="Check All" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged"></asp:CheckBox></td>
                                            </tr>
                                            <asp:Repeater ID="rptStudent" runat="server">
                                                <ItemTemplate>
                                                    <tr id="tr1" runat="server">
                                                        <th class="text-center" style="height: 34px; width: 36px;">
                                                            <asp:Label ID="lblSr" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                        </th>
                                                        <th class="text-center" style="width:120px !important;">
                                                            <span>Instrument No. : </span><asp:Label ID="lblChequeNo" runat="server" Text='<%# Bind("ChequeNo") %>'></asp:Label>
                                                        </th>
                                                        <th class="text-center" style="width:120px !important;">
                                                            <span>Date : </span><asp:Label runat="server" ID="lblDate" Text='<%# Bind("ChequeDate") %>'></asp:Label>
                                                        </th>
                                                        <th class="text-center" style="width: 200px !important;">
                                                            <span>Bank : </span><asp:Label ID="lblBankName" runat="server" Text='<%# Bind("BankName") %>'></asp:Label>
                                                        </th>
                                                         <th class="text-center" style="width:100px !important;">
                                                           <asp:DropDownList runat="server" ID="ddlStatus" style="min-width:70px !important;">
                                                               <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                               <asp:ListItem Value="Paid">Paid</asp:ListItem>
                                                               <asp:ListItem Value="Bounced">Bounced</asp:ListItem>
                                                           </asp:DropDownList>
                                                        </th>
                                                        <th class="text-center" style="width:70px !important;">
                                                            <asp:CheckBox ID="chk" runat="server"></asp:CheckBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" style="padding: 15px !important;">
                                                            <table cellspacing="0" rules="all" class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group" border="1" style="border-collapse: collapse;">
                                                                <asp:Repeater ID="RepeaterFee" runat="server">
                                                                    <HeaderTemplate>
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th>S.R. No.</th>
                                                                            <th>Student's Name</th>
                                                                            <th>Father's Name</th>
                                                                            <th>Class</th>
                                                                            <th>Receipt No.</th>
                                                                            <th>Amount</th>
                                                                        </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><asp:Label ID="lblSr" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label></td>
                                                                            <td><asp:Label runat="server" ID="lblSrNo" Text='<%# Bind("SrNo") %>'></asp:Label></td>
                                                                            <td><asp:Label runat="server" ID="lblName" Text='<%# Bind("Name") %>'></asp:Label></td>
                                                                            <td><asp:Label runat="server" ID="lblFatherName" Text='<%# Bind("FatherName") %>'></asp:Label></td>
                                                                            <td><asp:Label runat="server" ID="lblCombineClassName" Text='<%# Bind("CombineClassName") %>'></asp:Label></td>
                                                                            <td><asp:Label runat="server" ID="lblReceiptNo" Text='<%# Bind("ReceiptNo") %>'></asp:Label></td>
                                                                            <td class="text-right"><asp:Label runat="server" ID="lblPaidAmount" Text='<%# Bind("PaidAmount") %>'></asp:Label></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <tr class="text-right">
                                                                            <td colspan="5"></td>
                                                                            <td>Total</td>
                                                                            <td>
                                                                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </div>

                                <div class="col-sm-12  text-center">
                                    <asp:LinkButton ID="LnkSubmit" runat="server"  OnClientClick="return ValidateTextBox('.validatetxt');"
                                        CssClass="button form-control-blue" OnClick="LnkSubmit_Click" Visible="false">Submit</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

