<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EditQuotation.aspx.cs" Inherits="admin_EditQuotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Quotation Approval
    </title>
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

    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vendor</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlVendor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Quotation</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlQuotation" AutoPostBack="true" OnSelectedIndexChanged="ddlQuotation_SelectedIndexChanged" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Status</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Text="--All--" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Cancelled" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div runat="server" id="dvMSG"></div>

                                    <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="gvQuotation" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center ">
                                                <Columns>
                                                    <asp:BoundField DataField="SrNo" HeaderText="#" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" HeaderStyle-Width="40px" />
                                                    <asp:BoundField DataField="RefNo" HeaderText="Ref No." HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="QuotationFor" HeaderText="Quotation Title" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="Overview" HeaderText="Overview" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <%--<asp:BoundField DataField="RecurringAmount" HeaderText="Recurring Amount" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />--%>
                                                    <asp:BoundField DataField="OrganizationName" HeaderText="Organization Name" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="Date" HeaderText="Quotation Date" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="ApprovalDate" HeaderText="Approval Date" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />
                                                    <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-CssClass="vd_bg-blue vd_white text-center" />

                                                    <asp:TemplateField HeaderText="View">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDownload" runat="server" Text='<%# Eval("FilePath") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lbtnDonwload" runat="server" title="Download" OnClick="lbtnDonwload_Click" data-toggle="tooltip" data-placement="top" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-download"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("RefNo") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" Visible='<%# Eval("Status").ToString()=="2"?true:false%>' title="Edit" OnClick="lbtnEdit_Click" data-toggle="tooltip" data-placement="top" class="btn menu-icon vd_bd-green vd_green"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
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





