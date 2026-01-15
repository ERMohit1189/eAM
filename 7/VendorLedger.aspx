<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="VendorLedger.aspx.cs" EnableEventValidation="false" Inherits="admin_VendorLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Invoice List
    </title>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSearchBy]").autocomplete({
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
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vendor Name/ Code&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtSearchBy" placeholder="" AutoPostBack="true" OnTextChanged="txtSearchBy_TextChanged" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <asp:HiddenField ID="hfVendorId" runat="server" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Status&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="">All</asp:ListItem>
                                                <asp:ListItem Value="Complete">Paid</asp:ListItem>
                                                <asp:ListItem Value="Balance">Pending</asp:ListItem>
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-6 half-width-50  btn-a-devices-3-p6 mgbt-xs-15" style="padding-top: 26px;">
                                        <asp:LinkButton ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 47px;"></div>
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

                                    <div class="col-sm-12 " runat="server" id="pnlcontrols" visible="false">
                                        <div class="col-lg-12 " runat="server">

                                            <div class=" table-responsive  table-responsive2">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div id="gdv1" runat="server">
                                                            <table align="center" id="abc" runat="server" visible="false" width="100%" class="table no-p-b-table">
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
                                                                            <div class=" table-responsive  table-responsive2">
                                                                                <asp:GridView ID="gvInvoice" runat="server" AutoGenerateColumns="false" EmptyDataText="No records Found"
                                                                                    CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center ">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="#">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDownload" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="datess" HeaderText="Date" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                                                        <asp:BoundField DataField="subject" HeaderText="Subject" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" ItemStyle-CssClass="text-right" />
                                                                                        <asp:BoundField DataField="Tax" HeaderText="Tax" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" ItemStyle-CssClass="text-right" />
                                                                                        <asp:BoundField DataField="total" HeaderText="Total" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" ItemStyle-CssClass="text-right" />
                                                                                        <asp:BoundField DataField="disc" HeaderText="Discount" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" ItemStyle-CssClass="text-right" />
                                                                                        <asp:BoundField DataField="gTotal" HeaderText="Grand Total" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" ItemStyle-CssClass="text-right" />
                                                                                        <asp:BoundField DataField="Paids" HeaderText="Paid" HeaderStyle-CssClass="vd_bg-blue vd_white text-right" ItemStyle-CssClass="text-right" />
                                                                                        <asp:BoundField DataField="Bal" HeaderText="Balance" HeaderStyle-CssClass="vd_bg-blue vd_white text-right" ItemStyle-CssClass="text-right" />
                                                                                        <asp:TemplateField HeaderText="Invoice">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnk" runat="server" title="View File"  OnClick="lnk_Click" class="btn menu-icon vd_bd-red vd_red" Style="padding: 2px 6px;"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                                                                <asp:HyperLink ID="lbtnDonwload" runat="server" title="View File" NavigateUrl='<%# Eval("FilePath") %>' Visible="false" Target="_blank"  class="btn menu-icon vd_bd-red vd_red" Style="padding: 2px 6px;"> <i class="fa fa-eye"></i></asp:HyperLink>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



