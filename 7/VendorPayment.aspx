<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="VendorPayment.aspx.cs" Inherits="VendorPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function getVendorAdvanceList() {
            $(function () {
                $("[id$=txtVendorIDP]").autocomplete({
                    source: function (request, response) {
                        $("[id$=hfVendorIdP]").val('');
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
                                $("[id$=hfVendorIdP]").val('');
                            },
                            failure: function (response) {
                                alert(response.responseText);
                                $("[id$=hfVendorIdP]").val('');
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfVendorIdP]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
        function getStudentsList() {
            $(function () {
                $("[id$=txtVendorID]").autocomplete({
                    source: function (request, response) {
                        $("[id$=hfVendorId]").val('');
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
                                $("[id$=hfVendorId]").val('');
                            },
                            failure: function (response) {
                                alert(response.responseText);
                                $("[id$=hfVendorId]").val('');
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
                Sys.Application.add_load(getVendorAdvanceList);
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
                                        <div class="col-sm-12 no-padding well">
                                            <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Vendor Payment Type</label>
                                                <div class="mgtp-6">
                                                    <asp:RadioButtonList ID="rdoVendorAdvancePamentType" runat="server" CssClass="vd_radio radio-success" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoVendorAdvancePamentType_SelectedIndexChanged">
                                                        <asp:ListItem Text="Advance Payment" Value="VendorAdvancePayment" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Invoice Payment" Value="VendorPayment"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 no-padding" id="divVendorAdvancePament" runat="server">
                                            <div class="col-sm-12 no-padding">
                                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Vendor Name/ Code&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtVendorIDP" AutoPostBack="true" CssClass="form-control-blue validatetxtP" OnTextChanged="txtVendorIDP_TextChanged" runat="server"></asp:TextBox>
                                                        <asp:HiddenField ID="hfVendorIdP" runat="server" />
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                    <script>
                                                        function onchangetxt() {
                                                            if (document.getElementById('<%= txtVendorIDP.ClientID %>').value.length === 0) {
                                                                    document.getElementById('<%= hfVendorIdP.ClientID %>').value = "";
                                                              }
                                                        }
                                                        function onchangeatcopyandpaste() {
                                                            document.getElementById('<%= hfVendorIdP.ClientID %>').value = "";
                                                        }
                                                    </script>
                                                </div>
                                                <div class="col-sm-3 mgbt-xs-15">
                                                    <label class="control-label">Head Type&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlHeadTypeP" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlHeadTypeP_SelectedIndexChanged">
                                                            <asp:ListItem Value="Expense">Advance Payment</asp:ListItem>
                                                            <asp:ListItem Value="Income">Advance Return</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 mgbt-xs-15">
                                                    <label class="control-label">Head Category&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlHeadCategoryP" runat="server" CssClass="form-control-blue validatedrpP">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 mgbt-xs-15">
                                                    <label class="control-label">Amount&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtAmountP" onkeypress="return fnNumeric()" onkeyup="return evtEnter(event,'txtDescriptionGen',13);" runat="server" CssClass="form-control-blue  validatetxtP"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 mgbt-xs-15">
                                                    <label class="control-label">Particulars&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtDescriptionGenP" onkeyup="return evtEnter(event,'txtDescriptionGen',13);" runat="server" CssClass="form-control-blue  validatetxtP"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 mgbt-xs-15">
                                                    <label class="control-label">Mode of Payment&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlPaymentModeP" runat="server" onchange="MODChengeP();" CssClass="form-control-blue">
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
                                                <div class="col-sm-3 mgbt-xs-15 hide" id="disPMdateP">
                                                    <asp:Label ID="lblChqDateP" runat="server" class="control-label txt-bold"></asp:Label>&nbsp;<span class="vd_red">*</span>
                                                    <div class=" ">
                                                        <script>
                                                            Sys.Application.add_load(datetime);
                                                        </script>

                                                        <asp:TextBox ID="txtDDChequeUTRDateP" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>

                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 mgbt-xs-15 hide" id="disPMcheqenoP">

                                                    <asp:Label ID="lblChequeP" runat="server" class="control-label txt-bold" Text="Cheque No."></asp:Label>&nbsp;<span class="vd_red">*</span>

                                                    <div class=" ">
                                                        <asp:TextBox ID="txtDDChequeUTRNoP" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 mgbt-xs-15 hide" id="disBankP">
                                                    <asp:Label ID="lblBankNameP" runat="server" class="control-label txt-bold"></asp:Label>&nbsp;<span class="vd_red">*</span>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtBankP" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 half-width-50 mgbt-xs-15 text-center">

                                                    <asp:LinkButton ID="btnInsertP" runat="server" CssClass="button form-control-blue" OnClientClick="ValidateTextBox('.validatetxtP');ValidateDropdown('.validatedrpP');return validationReturn();" OnClick="btnInsertP_Click">Submit</asp:LinkButton>
                                                    <div id="dvMSG" runat="server" style="left: 147px"></div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="gvDayBook" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center" ShowFooter="true" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Head Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="HeadCategory" runat="server" Text='<%# Bind("HeadCategory") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="200" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Particulars">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblParticulars" runat="server" Text='<%# Bind("Particulars") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="350" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Mode">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMode" runat="server" Text='<%# Bind("PaymentMode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                   <FooterTemplate>
                                                                    <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                  <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Payment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExpens" runat="server" Text='<%# Bind("Expens") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalExpens" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="70" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Received">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIncome" runat="server" Text='<%# Bind("Income") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalIncome" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="70" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Balance">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBalances" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalBalance" runat="server" Font-Bold="true"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="70" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="col-sm-12 no-padding" id="divVendorPament" runat="server" visible="false">
                                            <div class="col-sm-12 no-padding">
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Search By</label>
                                                    <div class="mgtp-6">
                                                        <asp:RadioButtonList ID="rblInvoiceType" runat="server" CssClass="vd_radio radio-success" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblInvoiceType_SelectedIndexChanged">
                                                            <asp:ListItem Text="Vendor Wise" Value="Vendor Wise" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Invoice Wise" Value="InvoiceWise"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divPo" visible="false">
                                                    <label class="control-label">Enter Invoice No.&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control-blue validatetxt" MaxLength="20"></asp:TextBox>
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
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15  btn-a-devices-1-p4-p2  mgbt-xs-15">
                                                    <div class=" pull-left">
                                                        <asp:UpdatePanel ID="dgv" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="lnkShow" runat="server" OnClick="lnkShow_Click" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" class="button form-control-blue">View</asp:LinkButton>

                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div id="msgbox" runat="server" style="left: 147px !important;"></div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-lg-12 " runat="server" id="divInvoice" visible="false">
                                                <br />
                                                <h3 class="pull-left" style="font-size: 16px; font-weight: bold;">Invoice Details</h3>
                                                <div class="pull-right" >
                                                <h2 class="pull-right" style="font-size: 16px; color: #9c0000; font-weight: bold;">Available Cash Balance :
                                                    <asp:Label ID="lblAvilableBal" runat="server"></asp:Label></h2><br />
                                                <h2 class="pull-right" style="font-size: 16px; color: #9c0000; font-weight: bold;">Available Advance Balance :
                                                    <asp:Label ID="lblAvilableAdvanceBal" runat="server"></asp:Label></h2></div>
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center" ShowFooter="true" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk" runat="server"></asp:CheckBox>
                                                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Invoice No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVendorId" runat="server" Visible="false" Text='<%# Bind("VendorId") %>'></asp:Label>
                                                                    <asp:Label ID="InvoiceNo" runat="server" Text='<%# Bind("InvoiceNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="70" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Particulars">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtParticulars" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="350" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="amount" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="70" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discount">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDiscount" runat="server" onkeypress="return fnNumeric();" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged" Text="0.00"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalDiscount" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="80" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Text='<%# Bind("amount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalTotal" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="70" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Advance">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAdvance" runat="server" onkeypress="return fnNumeric();" AutoPostBack="true" OnTextChanged="txtAdvance_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalAdvance" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="80" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Paid">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPaid" runat="server" onkeypress="return fnNumeric();" AutoPostBack="true" OnTextChanged="txtPaid_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalPaid" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="80" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Balance">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBalances" runat="server" Text="0.00"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalBalance" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="70" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>


                                            <div class="col-lg-12 no-padding" runat="server" id="divControls" visible="false">

                                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <script>
                                                            Sys.Application.add_load(datetime);
                                                        </script>
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Head Category&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlHeadCategory" runat="server" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 half-width-50 mgbt-xs-15 " id="Div2" runat="server">
                                                    <label class="control-label">Mode of Payment&nbsp;<span class="vd_red">*</span></label>
                                                    <div class=" ">
                                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" onchange="MODChenge();" CssClass="form-control-blue validatedrp">
                                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
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
                                                <div class="col-sm-3 half-width-50 mgbt-xs-15 hide" id="disPMdate">
                                                    <asp:Label ID="lblChqDate" runat="server" class="control-label txt-bold"></asp:Label>&nbsp;<span class="vd_red">*</span>
                                                    <div class=" ">
                                                        <script>
                                                            Sys.Application.add_load(datetime);
                                                        </script>

                                                        <asp:TextBox ID="txtDDChequeUTRDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>

                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 half-width-50 mgbt-xs-15 hide" id="disPMcheqeno">

                                                    <asp:Label ID="lblCheque" runat="server" class="control-label txt-bold" Text="Cheque No."></asp:Label>&nbsp;<span class="vd_red">*</span>

                                                    <div class=" ">
                                                        <asp:TextBox ID="txtDDChequeUTRNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 half-width-50 mgbt-xs-15 hide" id="disBank">
                                                    <asp:Label ID="lblBankName" runat="server" class="control-label txt-bold"></asp:Label>&nbsp;<span class="vd_red">*</span>
                                                    <div class=" ">
                                                        <asp:TextBox ID="txtBank" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 half-width-50 mgbt-xs-15 text-center">

                                                    <asp:LinkButton ID="btnInsert" runat="server" CssClass="button form-control-blue" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="btnInsert_Click">Submit</asp:LinkButton>
                                                    <div id="Div4" runat="server" style="left: 147px"></div>
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
    <script>
        function MODChenge() {
            $("[id*=disBank]").addClass("hide");
            $("[id*=disPMcheqeno]").addClass("hide");
            $("[id*=disPMdate]").addClass("hide");
            if ($("[id*=ddlPaymentMode]").val() == "Cash" || $("[id*=ddlPaymentMode]").val() == "") {
                $("[id*=txtDDChequeUTRDate]").removeClass("validatetxt");
                $("[id*=txtDDChequeUTRNo]").removeClass("validatetxt");
                $("[id*=txtBank]").removeClass("validatetxt");
                $("[id*=txtBank]").val("");
            }
            else {
                $("[id*=txtDDChequeUTRDate]").addClass("validatetxt");
                $("[id*=txtDDChequeUTRNo]").addClass("validatetxt");
                $("[id*=txtBank]").addClass("validatetxt");
                $("[id*=txtBank]").val("NA");



                $("[id*=disBank]").removeClass("hide");
                $("[id*=disPMcheqeno]").removeClass("hide");
                $("[id*=disPMdate]").removeClass("hide");
                if ($("[id*=ddlPaymentMode]").val() == "Cheque" || $("[id*=ddlPaymentMode]").val() == "DD") {
                    $("[id*=lblChqDate]").html("Instrument Date");
                    $("[id*=lblCheque]").html("Instrument No.");
                    $("[id*=lblBankName]").html("Issuer");
                }
                else if ($("[id*=ddlPaymentMode]").val() == "Online Transfer" || $("[id*=ddlPaymentMode]").val() == "Other") {
                    $("[id*=lblChqDate]").html("Transaction Date");
                    $("[id*=lblCheque]").html("Ref. No.");
                }
                else if ($("[id*=ddlPaymentMode]").val() == "Card") {
                    $("[id*=lblChqDate]").html("Transaction Date");
                    $("[id*=lblCheque]").html("Card No.");
                    $("[id*=lblBankName]").html("Issuer");
                }

                if ($("[id*=ddlPaymentMode]").val() == "Other") {
                    $("[id*=lblBankName]").html("Reference Name");
                }
            }

        }
        function MODChengeP() {
            $("[id*=disBankP]").addClass("hide");
            $("[id*=disPMcheqenoP]").addClass("hide");
            $("[id*=disPMdateP]").addClass("hide");
            if ($("[id*=ddlPaymentModeP]").val() == "Cash" || $("[id*=ddlPaymentModeP]").val() == "") {
                $("[id*=txtDDChequeUTRDateP]").removeClass("validatetxtP");
                $("[id*=txtDDChequeUTRNoP]").removeClass("validatetxtP");
                $("[id*=txtBankP]").removeClass("validatetxtP");
                $("[id*=txtBankP]").val("");
            }
            else {
                $("[id*=txtDDChequeUTRDateP]").addClass("validatetxtP");
                $("[id*=txtDDChequeUTRNoP]").addClass("validatetxtP");
                $("[id*=txtBankP]").addClass("validatetxtP");
                $("[id*=txtBankP]").val("NA");



                $("[id*=disBankP]").removeClass("hide");
                $("[id*=disPMcheqenoP]").removeClass("hide");
                $("[id*=disPMdateP]").removeClass("hide");
                if ($("[id*=ddlPaymentModeP]").val() == "Cheque" || $("[id*=ddlPaymentModeP]").val() == "DD") {
                    $("[id*=lblChqDateP]").html("Instrument Date");
                    $("[id*=lblChequeP]").html("Instrument No.");
                    $("[id*=lblBankNameP]").html("Issuer");
                }
                else if ($("[id*=ddlPaymentModeP]").val() == "Online Transfer" || $("[id*=ddlPaymentModeP]").val() == "Other") {
                    $("[id*=lblChqDateP]").html("Transaction Date");
                    $("[id*=lblChequeP]").html("Ref. No.");
                }
                else if ($("[id*=ddlPaymentModeP]").val() == "Card") {
                    $("[id*=lblChqDateP]").html("Transaction Date");
                    $("[id*=lblChequeP]").html("Card No.");
                    $("[id*=lblBankNameP]").html("Issuer");
                }

                if ($("[id*=ddlPaymentModeP]").val() == "Other") {
                    $("[id*=lblBankNameP]").html("Reference Name");
                }
            }

        }
    </script>
</asp:Content>


