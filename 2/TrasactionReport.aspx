<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TrasactionReport.aspx.cs" Inherits="TrasactionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div runat="server" id="msg1" class="text-danger"></div>
                                </div>
                                <div class="col-sm-12  no-padding " runat="server" id="divMain">


                                    <div class="col-sm-4  " id="divBranch" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                                <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-2  " id="divSession" runat="server">
                                        <label class="control-label">Session</label>
                                        <div class="">
                                            <asp:DropDownList runat="server" ID="DrpSessionName"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3   mgbt-xs-15">
                                        <label class="control-label">From Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="fromDDYears" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="fromDDYear_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="fromDDMonths" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="fromDDMonth_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="fromDDDates" runat="server" CssClass="form-control-blue col-sm-4">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3   mgbt-xs-15">
                                        <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="toDDYears" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="toDDYearC_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="toDDMonths" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="toDDMonthC_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="toDDDates" runat="server" CssClass="form-control-blue col-sm-4">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2   mgbt-xs-15">
                                        <label class="control-label">Mode of Payment</label>
                                        <div class="">
                                            <asp:DropDownList ID="DdlpaymentMode" runat="server" CssClass="vd_radio radio-success">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Cheque</asp:ListItem>
                                                <asp:ListItem>DD</asp:ListItem>
                                                <asp:ListItem>Card</asp:ListItem>
                                                <asp:ListItem>Online Transfer</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                                <asp:ListItem Value="Online">Online</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2   mgbt-xs-15">
                                        <label class="control-label">Status</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpStatus" runat="server" class="form-control-blue ">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Paid</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                                <asp:ListItem>Cancelled</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="" style="margin-top: 25px;">
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  mgbt-xs-10" runat="server" id="divExport" visible="false">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel1" runat="server">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o hide"></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o hide"></i></asp:LinkButton>
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

                                <div class="col-sm-12" id="divExport2" runat="server">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div id="gdv1" runat="server">
                                                    <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table" visible="false">
                                                        <tr>
                                                            <td>
                                                                <div id="header" runat="server" class="col-md-12 no-padding"></div>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                    <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                    <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("DepositDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Txn. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Labels1" runat="server" Text='<%# Eval("TxnID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Receipt No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="s1" runat="server" Text='<%# Eval("ReceiptNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="s2" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="s3" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Class">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="s4" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="s5" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Payment Gateway">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="s6" runat="server" Text='<%# Eval("GateWayName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Reference Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="sh6" runat="server" Text='<%# Eval("ChequeDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Reference No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="sf6" runat="server" Text='<%# Eval("ChequeNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Reference Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="sd6" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="s7" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Mode">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="s8" runat="server" Text='<%# Eval("mode") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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

