<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DayBookEntryReport.aspx.cs" Inherits="DayBookEntryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Day Book Entry Report
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <asp:UpdatePanel ID="upMain" runat="server">
                                    <ContentTemplate>
                                        <div class="col-lg-12 no-padding" id="tblInsert" runat="server">
                                            <div class="col-lg-12 no-padding">
                                                <div class="col-sm-3 half-width-50 ">
                                                    <label class="control-label">From Date&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <script>
                                                            Sys.Application.add_load(datetime);
                                                        </script>
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 half-width-50">
                                                    <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <script>
                                                            Sys.Application.add_load(datetime);
                                                        </script>
                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 half-width-50">
                                                    <label class="control-label">Head Type&nbsp;<span class="vd_red"></span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlHeadType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHeadType_SelectedIndexChanged" CssClass="form-control-blue">
                                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                            <asp:ListItem Value="Income">Income</asp:ListItem>
                                                            <asp:ListItem Value="Expense">Expense</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3 half-width-50 mgbt-xs-15 ">
                                                    <label class="control-label">Head&nbsp;<span class="vd_red"></span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlHead" runat="server" CssClass="form-control-blue">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3  ">
                                                    <label class="control-label">Head Category&nbsp;<span class="vd_red"></span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlHeadCategory" runat="server" CssClass="form-control-blue">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 half-width-50 mgbt-xs-15 " id="disheadpaymentmode" runat="server">
                                                    <label class="control-label">Mode of Payment&nbsp;<span class="vd_red"></span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control-blue">
                                                            <asp:ListItem Value="" Selected="True">All</asp:ListItem>
                                                            <asp:ListItem>Cash</asp:ListItem>
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
                                                
                                                <div class="col-sm-3">
                                                    <label class="control-label">Select User&nbsp;<span class="vd_red"></span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue "></asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 half-width-50 mgbt-xs-15" style="padding-top: 26px;">
                                                    <asp:LinkButton ID="btnView" runat="server" CssClass="button form-control-blue" OnClick="btnView_Click"><i class="fa fa-eye"></i>&nbsp;View</asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class="col-sm-12  mgbt-xs-10" runat="server" id="divExport" visible="false">
                                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                    <ContentTemplate>
                                                        <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                            <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                                title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                                title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                                title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                                title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

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
                                            <div class="col-lg-12 " runat="server">

                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <div id="gdv1" runat="server">
                                                                <table align="center" id="abc" runat="server" visible="false" width="100%" class="table no-p-b-table">
                                                                    <tr>
                                                                        <td>
                                                                            <div id="header" runat="server" class="col-md-12 no-padding"></div>
                                                                            <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                                <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                                <%--<br />--%>
                                                                                <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                                <div class="col-sm-12 text-right hide">
                                                                                    <b>Cash Opening :
                                                                                    <asp:Label ID="lblOpening" runat="server"></asp:Label></b><br />
                                                                                    <b>Cash Closing :
                                                                                    <asp:Label ID="lblClosing" runat="server"></asp:Label></b>
                                                                                </div>
                                                                                <asp:GridView ID="gvDayBook" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center" ShowFooter="true" Width="100%">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="#">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIndex" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40" />
                                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Head">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="HeadName" runat="server" Text='<%# Bind("HeadName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="150" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Head Category">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="HeadCategory" runat="server" Text='<%# Bind("HeadCategory") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="150" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Particulars">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblParticulars" runat="server" Text='<%# Bind("Particulars") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="320" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Username">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblLoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br />
                                                                                                (<asp:Label ID="lblRecordedDate" runat="server" Text='<%# Bind("RecordedDate") %>'></asp:Label>)
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="200" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Mode">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblMode" runat="server" Text='<%# Bind("PaymentMode") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="110" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Income">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIncome" runat="server" Text='<%# Bind("Income") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lblTotalIncome" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="110" />
                                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Expense">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblExpens" runat="server" Text='<%# Bind("Expens") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lblTotalExpens" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="110" />
                                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>

                                                                                       <%-- <asp:TemplateField HeaderText="Balance">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblBalances" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lblTotalBalance" runat="server" Font-Bold="true" Text="Total" Visible="false"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="110" />
                                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>--%>
                                                                                 
                                                                                    </Columns>
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

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

