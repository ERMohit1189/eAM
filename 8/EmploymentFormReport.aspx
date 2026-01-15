<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmploymentFormReport.aspx.cs" Inherits="admin_EmploymentFormReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>



            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpFromyear" runat="server" OnSelectedIndexChanged="drpFromyear_SelectedIndexChanged" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpFrommonth" runat="server" OnSelectedIndexChanged="drpFrommonth_SelectedIndexChanged" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpFromdate" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpToyear" runat="server" OnSelectedIndexChanged="drpToyear_SelectedIndexChanged" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpTomonth" runat="server" OnSelectedIndexChanged="drpToyear_SelectedIndexChanged1" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="drpTodate" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="form-control-blue button" OnClick="lnkView_Click">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 47px;"></div>
                                    </div>


                                </div>
                                <div class="col-sm-12  mgbt-xs-10" runat="server" id="divPrintBtns" visible="false">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

                                                <script>
                                                    
                                                </script>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ImageButton1" />
                                            <asp:PostBackTrigger ControlID="ImageButton2" />
                                            <asp:PostBackTrigger ControlID="ImageButton3" />
                                            <asp:PostBackTrigger ControlID="ImageButton4" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    
                                </div>

                                <div class="col-sm-12 " id="gdv" runat="server"  visible="false">
                                    <div class=" table-responsive  table-responsive2">

                                        <table runat="server" id="abc" width="100%" style="margin-top:40px;">
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" runat="server" id="header1"></td>
                                                        </tr>
                                                    </table>
                                                </td>

                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:Label ID="lbltitel" runat="server" Text="Employment Form Fee Collection Report"></asp:Label>
                                                    &nbsp; &nbsp;
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                    <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                                                    &nbsp; &nbsp; &nbsp;
                    <asp:Label ID="lblTitle1" runat="server"></asp:Label>
                                                    <asp:Label ID="lblDate1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                                                    <asp:Label ID="Label1" runat="server" Text=")"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Repeater ID="Repeater1" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table table-striped table-hover no-bm no-head-border table-bordered table-header-group">
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Receipt No.</th>
                                                                    <th>Date</th>
                                                                    <th class="text-left">Name</th>
                                                                    <th class="text-left">Father's Name</th>
                                                                    <th>Gender</th>
                                                                    <th>Contact No.</th>
                                                                    <th>Designation</th>
                                                                    <th class="text-right">Amount</th>
                                                                    <th>Status</th>
                                                                    <th class="text-right">Paid</th>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex+1 %></td>
                                                                <td><%# Eval("RecieptNo") %></td>
                                                                <td><%# Eval("EFDate") %></td>
                                                                <td class="text-left"><%# Eval("EmpName") %></td>
                                                                <td class="text-left"><%# Eval("EmpFather") %></td>
                                                                <td><%# Eval("EmpGender") %></td>
                                                                <td><%# Eval("EmpContactNo") %></td>
                                                                <td><%# Eval("EmpDesName") %></td>
                                                                <td class="text-right">
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("EmpAmount") %>'></asp:Label></td>
                                                                <td><%# Eval("IsCancel").ToString()=="True"?"Cancelled":"Paid" %></td>
                                                                <td class="text-right"><asp:Label ID="lblPaid" runat="server" Text='<%# Eval("IsCancel").ToString()=="True"?"0.00":Eval("EmpAmount") %>'></asp:Label></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <tr>
                                                                <td class="text-right" colspan="8" style="font-weight:bold;">Total</td>
                                                                <td class="text-right" style="font-weight:bold;"><asp:Label ID="lblTotalAmount" runat="server"></asp:Label></td>
                                                                <td></td>
                                                                <td class="text-right" style="font-weight:bold;"><asp:Label ID="lblTotalPaid" runat="server"></asp:Label></td>
                                                            </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
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






        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

