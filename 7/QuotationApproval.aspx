<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="QuotationApproval.aspx.cs" Inherits="_7.AdminQuotationApproval" %>

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

        function Redirect(msg, url) {
            window.location.href = url;
            alert(msg);
        }
    </script>
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
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-lg-12 no-padding">
                                    <div class="col-lg-12 no-padding" runat="server" id="divControls">
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <div class="form-group">
                                                <label class="control-label">Date From&nbsp;<span class="vd_red"></span></label>
                                                <asp:TextBox ID="txtQtnFromDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <div class="form-group">
                                                <label class="control-label">To&nbsp;<span class="vd_red"></span></label>
                                                <asp:TextBox ID="txtQtnToDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <div class="form-group">
                                                <label class="control-label">Quotation No. &nbsp;<span class="vd_red"></span></label>
                                                <asp:TextBox ID="txtQtnNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Vendor Name/Vendor Code&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtSearchBy" placeholder="" AutoPostBack="true" OnTextChanged="txtSearchBy_TextChanged" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                <asp:HiddenField ID="hfVendorId" runat="server" />
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 btn-a-devices-1-p4-p2 mgbt-xs-15">
                                            <asp:LinkButton ID="lbtnSearchBy" OnClick="lbtnSearchBy_Click" CssClass="button form-control-blue" runat="server">View </asp:LinkButton>
                                            <div runat="server" id="dvSearch" style="left: 55px;"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="gvBankBranchList" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendorId" Visible="false" runat="server" Text='<%# Bind("VendorId") %>'></asp:Label>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("QtnEnterDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="110px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTN. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="QtnNo" runat="server" Text='<%# Bind("QtnNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="110px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vendor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Vendor" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="200px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Title">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="220px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ref. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Remark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="220px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Document">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lbtnDonwload" runat="server" title="View File" NavigateUrl='<%# Eval("FilePath") %>' Target="_blank"  class="btn menu-icon vd_bd-red vd_red" Style="padding: 2px 6px;"> <i class="fa fa-eye"></i></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label36" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" title="Approve"  OnClick="lbtnEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
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

                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                        <table class="tab-popup">
                            <tr>
                                <td>
                                    <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemark0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server">Status</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control-blue validatedrp1">
                                        <asp:ListItem Text="<--Select-->" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Approve" Value="Approve"></asp:ListItem>
                                        <asp:ListItem Text="Reject" Value="Reject"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="Button3" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" Text="Update" />

                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                        CancelControlID="Button4" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
