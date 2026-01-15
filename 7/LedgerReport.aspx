<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LedgerReport.aspx.cs" Inherits="_7.AdminLedgerReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Ledge Report</title>
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
                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">From Date</label>
                                        <div class="">
                                            <script>
                                                Sys.Application.add_load(datetime);
                                            </script>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date</label>
                                        <div class="">
                                            <script>
                                                Sys.Application.add_load(datetime);
                                            </script>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                   <div class="col-sm-3 half-width-50 mgbt-xs-15 hide">
                                                    <label class="control-label">Mode of Payment&nbsp;<span class="vd_red"></span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control-blue">
                                                            <asp:ListItem Value="">All</asp:ListItem>
                                                            <asp:ListItem Selected="True">Cash</asp:ListItem>
                                                            <asp:ListItem>Cheque</asp:ListItem>
                                                            <asp:ListItem>DD</asp:ListItem>
                                                            <asp:ListItem>Card</asp:ListItem>
                                                            <asp:ListItem>Online Transfer</asp:ListItem>
                                                            <asp:ListItem>Other</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                    <div class="col-sm-3 half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="button form-control-blue" runat="server" Text="View" />
                                        <div id="msgbox" runat="server" style="left: 67px"></div>
                                    </div>

                                    <div class="col-sm-12  mgbt-xs-10">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float: right; font-size: 19px;" id="Panel2" runat="server" visible="false">
                                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
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
                                    <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2" id="divExport" runat="server" visible="false">
                                            <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table">
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
                                                            <asp:GridView ID="gvLedger" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center ">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue vd_white text-center" />
                                                                        <ItemStyle CssClass="text-center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("RecordDate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue vd_white text-center" />
                                                                        <ItemStyle CssClass="text-center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cash Opening">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOpening" runat="server" Text='<%# (Eval("RecordDate").ToString()=="Total" ? " " : Eval("Opening").ToString()) %>'></asp:Label>
                                                                            <%--<%# (Eval("RecordDate").ToString()=="Total" ? " " : Eval("Opening").ToString()) %>--%>
                                                                        </ItemTemplate>
                                                                        <%-- <FooterTemplate>
                                                                            <asp:Label ID="total" runat="server" Text="Total"></asp:Label>
                                                                        </FooterTemplate>--%>
                                                                        <HeaderStyle CssClass="vd_bg-blue vd_white text-right" />
                                                                        <ItemStyle CssClass="text-right" />
                                                                        <FooterStyle CssClass="text-right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Income">
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblIncome" runat="server" Text='<%# Bind("Income") %>'></asp:Label>--%>
                                                                            <asp:Label ID="lblIncome" runat="server" Text='<%# (Eval("RecordDate").ToString()=="Total" ? "<b>"+Eval("Income").ToString()+"</b>" : Eval("Income").ToString()) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                     <%--   <FooterTemplate>
                                                                            <asp:Label ID="totalIncome" runat="server" Text="Total"></asp:Label>
                                                                        </FooterTemplate>--%>
                                                                        <HeaderStyle CssClass="vd_bg-blue vd_white text-right" />
                                                                        <ItemStyle CssClass="text-right" />
                                                                        <FooterStyle CssClass="text-right" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Expense">
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblExpens" runat="server" Text='<%# "<b>"+Eval("Expense").ToString()+"</b>" %>'></asp:Label>--%>
                                                                            <asp:Label ID="lblExpens" runat="server" Text='<%# (Eval("RecordDate").ToString()=="Total" ? "<b>"+Eval("Expense").ToString()+"</b>" : Eval("Expense").ToString()) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                      <%--  <FooterTemplate>
                                                                            <asp:Label ID="totalExpens" runat="server" Text="Total"></asp:Label>
                                                                        </FooterTemplate>--%>
                                                                        <HeaderStyle CssClass="vd_bg-blue vd_white text-right" />
                                                                        <ItemStyle CssClass="text-right" />
                                                                        <FooterStyle CssClass="text-right" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Cash Closing">
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblClosing" runat="server" Text='<%# Bind("RunningTotal") %>'></asp:Label>--%>
                                                                            <asp:Label ID="lblClosing" runat="server" Text='<%# (Eval("RecordDate").ToString()=="Total" ? " " : "<b>"+Eval("RunningTotal").ToString()+"</b>") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue vd_white text-right" />
                                                                        <ItemStyle CssClass="text-right" />
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
