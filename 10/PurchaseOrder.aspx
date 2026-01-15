<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PurchaseOrder.aspx.cs" Inherits="PurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function getQuotationList() {
            $(function () {
                $("[id$=txtQtnNo]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/VendorIDName.asmx/GetQtnNo") %>',
                            data: "{ 'QtnNo': '" + request.term + "'}",
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
                        $("[id$=hfQtnNo]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
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
        function ChecktenDigitss(inputtxt) {
            var phoneno = /^\d+$/;
            if (inputtxt.value.match(phoneno) && inputtxt != null) {
                inputtxt.style.border = "1px solid #D5D5D5";
                return true;
            }
            else {
                if (inputtxt.value != "") {
                    inputtxt.style.border = "1px solid Red";
                    inputtxt.value = "";
                    inputtxt.focus();
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
                Sys.Application.add_load(getQuotationList);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">

                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="dovPO">
                                        <label class="control-label">Enter Quotation No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtQtnNo" runat="server" CssClass="form-control-blue validatetxt" MaxLength="20" AutoPostBack="true" OnTextChanged="txtQtnNo_TextChanged"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hfQtnNo" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lbtnSearchBy" OnClick="lbtnSearchBy_Click" CssClass="button form-control-blue" runat="server">View </asp:LinkButton>

                                        <div runat="server" id="dvSearch" style="left: 55px;"></div>
                                    </div>

                                </div>
                                <div class="col-sm-12 no-padding" id="pnlcontrols" runat="server" visible="false">
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
                                                    <%--<asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Remark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="220px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Status" runat="server" Text='<%# Eval("Status").ToString()=="Approve"?"Approved":Eval("Status").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Document">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lbtnDonwload" runat="server" title="View File" NavigateUrl='<%# Eval("FilePath") %>' Target="_blank"  class="btn menu-icon vd_bd-red vd_red" Style="padding: 2px 6px;"> <i class="fa fa-eye"></i></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 no-padding">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control-blue validatetxt datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-8  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Tax Type&nbsp;<span class="vd_red">*</span></label>
                                            <div class=" ">
                                                <asp:DropDownList ID="ddlTaxType" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlTaxType_SelectedIndexChanged">
                                                     <asp:ListItem Value="NA">NA</asp:ListItem>
                                                    <asp:ListItem Value="GST">GST</asp:ListItem>
                                                    <asp:ListItem Value="IGST">IGST</asp:ListItem>
                                                    <asp:ListItem Value="UGST">UGST</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-8  half-width-50 mgbt-xs-15">
                                            <div class="">
                                                <asp:TextBox ID="txtPODescription" runat="server" CssClass="form-control-blue validatetxt" TextMode="MultiLine"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="table-responsive2 table-responsive">
                                            <asp:GridView ID="Gridview1" runat="server" ShowFooter="true" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="txtIndex" runat="server" Visible="false" Text='<%# Container.DataItemIndex %>'></asp:Label>
                                                            <asp:LinkButton ID="ButtonRemove" runat="server" class="btn menu-icon vd_bd-red vd_red" OnClick="ButtonRemove_Click" Style="padding: 1px 5px;"><i class="fa fa-close" style="font-size: 14px;"></i></asp:LinkButton>

                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="ButtonAdd" runat="server" class="btn menu-icon vd_bd-green vd_green" OnClick="ButtonAdd_Click" Style="padding: 2px 5px;"><i class="fa fa-plus-circle" style="font-size: 30px;"></i></asp:LinkButton>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                        <HeaderStyle Width="50" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle Width="50" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category *">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                                <asp:ListItem Value="Product">Product</asp:ListItem>
                                                                <asp:ListItem Value="Services">Services</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article *">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlArticle" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlArticle_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HSN Code *">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtHSNCode" runat="server" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="Label2f" runat="server" Style="font-weight: bold" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty. *">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQty" runat="server" CssClass="form-control-blue validatetxt" onkeypress="ChecktenDigitss(this);" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblSQty" runat="server" Style="font-weight: bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate *">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRate" runat="server" CssClass="form-control-blue validatetxt" AutoPostBack="true" OnTextChanged="txtRate_TextChanged" onkeypress="return fnNumeric()"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" CssClass="form-control-blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblSAmount" runat="server" Style="font-weight: bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tax (Max 40%)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTax" runat="server" CssClass="form-control-blue" onkeypress="return fnNumeric();" AutoPostBack="true" OnTextChanged="txtTax_TextChanged" Style="width: 50%; float: left;"></asp:TextBox>
                                                            <asp:Label ID="lblTax1" runat="server" CssClass="form-control-blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblSTax" runat="server" Style="font-weight: bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" CssClass="form-control-blue"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblsTotal" runat="server" Style="font-weight: bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>


                                        </div>

                                        <br />
                                    </div>
                                    <div class="col-sm-12 no-padding">
                                        <div class="col-sm-7">
                                            <label class="control-label">Terms&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtTerms" runat="server" CssClass="form-control-blue validatetxt" TextMode="MultiLine" Style="height: 135px;"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 60%;"></td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">Amount : </td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                        <asp:Label ID="lblGTotal" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr runat="server" id="trNa">
                                                    <td style="width: 60%;"></td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">Tax : </td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                        <asp:Label ID="lblNa" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trCGST" visible="false">
                                                    <td style="width: 60%;"></td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">CGST : </td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                        <asp:Label ID="lblCGST" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trSGST" visible="false">
                                                    <td style="width: 60%;"></td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">SGST : </td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                        <asp:Label ID="lblSGST" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trIGST" visible="false">
                                                    <td style="width: 60%;"></td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">IGST : </td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                        <asp:Label ID="lblIGST" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trUGST" visible="false">
                                                    <td style="width: 60%;"></td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">UGST : </td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px;">
                                                        <asp:Label ID="lblUGST" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 60%;"></td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px; border-top: 1px solid #000;">Total : </td>
                                                    <td style="width: 20%; text-align: right; font-weight: bold; font-size: 17px; padding: 3px; border-top: 1px solid #000;">
                                                        <asp:Label ID="lblGrandTotal" runat="server" Style="font-weight: bold" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 btn-a-devices-1-p4-p2   mgbt-xs-15">
                                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="button form-control-blue" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                    <div runat="server" id="msgbox" style="left: 55px;"></div>
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

