<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="VendorInvoice.aspx.cs" Inherits="admin_VendorInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Vendor Quotation</title>

    <script type="text/javascript">

        function fnNumeric() {
            var code = window.event.keyCode;
            if ((code >= 48 && code <= 57) || (code === 45) || (code === 46)) {
                /*checknos = true;*/
                return true;
            }
            else {
                /*checknos= false;*/
                window.event.keyCode = 0;
                return false;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <%--   <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>--%>
                        <div class="col-lg-12 no-padding" id="tblInsert" runat="server">
                            <div class="col-lg-12 no-padding">
                                <div class="col-sm-4 no-padding">
                                    <div class="form-group">
                                        <asp:Label ID="lblVendorType" runat="server" class="col-sm-4 txt-middle-l txt-bold no-padding" Text="Date of Invoice"></asp:Label>
                                        <div class="col-sm-8 controls  mgbt-xs-20">
                                            <asp:TextBox ID="txtDOI" placeholder="" ReadOnly="true" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 no-padding" style="display: none;">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" class="col-sm-4 txt-middle-l txt-bold no-padding" Text="RefNo"></asp:Label>
                                        <div class="col-sm-8 controls  mgbt-xs-20">
                                            <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control-blue" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 no-padding">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" class="col-sm-4 txt-middle-l txt-bold no-padding" Text="Quotation"></asp:Label>
                                        <div class="col-sm-8 controls  mgbt-xs-20">
                                            <asp:DropDownList ID="ddlQuotation" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlQuotation_SelectedIndexChanged" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12 no-padding mgbt-xs-10">
                                <div class=" table-responsive  table-responsive2">
                                    <asp:GridView ID="gvQuotation" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center ">
                                        <Columns>
                                            <asp:BoundField DataField="RefNo" HeaderText="Ref No." HeaderStyle-CssClass="vd_bg-blue vd_white text-center" HeaderStyle-Width="100px" />
                                            <asp:BoundField DataField="QuotationFor" HeaderText="Quotation Title" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                            <asp:BoundField DataField="Overview" HeaderText="Overview" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                            <asp:BoundField DataField="RecurringAmount" HeaderText="Recurring Amount" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                            <asp:BoundField DataField="OrganizationName" HeaderText="Organization Name" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                            <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                            <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="col-lg-12 no-padding">
                                <div class="col-sm-4 no-padding">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" class="col-sm-4 txt-middle-l txt-bold no-padding" Text="Invoice No"></asp:Label>
                                        <div class="col-sm-8 controls  mgbt-xs-20">

                                            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4 no-padding">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" class="col-sm-4 txt-middle-l txt-bold no-padding" Text="Upload File"></asp:Label>
                                        <div class="col-sm-8 controls  mgbt-xs-20">
                                            <asp:UpdatePanel ID="upFile" runat="server" EnableViewState="true" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:FileUpload ID="fuFile" CssClass="form-control-blue" runat="server" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnInsert" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 no-padding">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" class="col-sm-4 txt-middle-l txt-bold no-padding" Text="Remark"></asp:Label>
                                        <div class="col-sm-8 controls  mgbt-xs-20">
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12 no-padding text-center ">

                                <asp:LinkButton ID="btnInsert" runat="server" CssClass="button2" OnClick="btnInsert_Click">Enter</asp:LinkButton>
                                <asp:LinkButton ID="btnReset" runat="server" CssClass="button2" OnClick="btnReset_Click">Reset</asp:LinkButton>
                            </div>

                            <div class="col-lg-12 no-padding">
                            </div>

                        </div>
                     <%--   </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>




