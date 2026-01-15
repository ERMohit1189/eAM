<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="VendorPaymentReport.aspx.cs" Inherits="VendorPaymentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtVendorID]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/VendorIDName.asmx/GetStudents") %>',
                            data: "{ 'studentId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d,
                                    function (item) {
                                        return {
                                            label: item.split('@')[0],
                                            val: item.split('@')[1]
                                        }
                                    }));
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfVendorId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
             <script>
                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <asp:UpdatePanel ID="upMain" runat="server">
                                    <ContentTemplate>
                                        <div class="col-lg-12 no-padding" id="tblInsert" runat="server">
                                            <div class="col-lg-12 no-padding">

                                                <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                                    <label class="control-label">From Date&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                                    <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 mgbt-xs-15 ">
                                                    <label class="control-label">Head Category&nbsp;<span class="vd_red"></span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlHeadCategory" runat="server" CssClass="form-control-blue">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divVendor">
                                                    <label class="control-label">Vendor Name/ Code&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtVendorID" AutoPostBack="true" CssClass="form-control-blue validatetxt" OnTextChanged="txtVendorID_TextChanged" runat="server"></asp:TextBox>
                                                        <asp:HiddenField ID="hfVendorId" runat="server" />
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 half-width-50 mgbt-xs-15" style="padding-top: 26px;">
                                                    <asp:LinkButton ID="btnView" runat="server" CssClass="button form-control-blue" OnClick="btnView_Click">View</asp:LinkButton>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div id="msgbox" runat="server" style="left: 147px !important;"></div>
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
                                                            <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color hide"
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
                                                                                <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                                <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                                <asp:GridView ID="gvDayBook" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center" ShowFooter="true" Width="100%">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="#">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIndex" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Vendor">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblVendor" runat="server" Text='<%# Bind("Vendor") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="250" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Invoice Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Bind("InvoiceDate") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="110" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Invoice No.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Bind("InvoiceNo") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Head Category">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblHeadCategory" runat="server" Text='<%# Bind("HeadCategory") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="150" />
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Amount">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                             <FooterTemplate>
                                                                                                <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100" />
                                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Discount">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDiscount" runat="server" Text='<%# Bind("Discount") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lblTotalDiscount" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100" />
                                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Paid">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblPaid" runat="server" Text='<%# Bind("Paid") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lblTotalPaid" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100" />
                                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Balance">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lblTotalBalance" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100" />
                                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
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

