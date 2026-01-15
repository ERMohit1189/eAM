<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AllStudentReceiptMonthDate.aspx.cs" Inherits="_2.AdminAllStudentReceiptMonthDate" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <%-- ReSharper disable once Html.PathError --%>
    <%-- ReSharper disable once Html.PathError --%>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSearch]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetStudents") %>',
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
                        $("[id$=hfStudentId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
            </script>

            <table id="Table1" runat="server" width="100%">
                <tr style="text-align: center;">
                    <td id="header2" runat="server"></td>
                </tr>
            </table>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-md-12 no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Session&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSession" runat="server" CssClass="form-control-blue ">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Select&nbsp;<span class="vd_red">*</span></label>
                                        <div class="txt-middle">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" CssClass="vd_radio radio-success"
                                                RepeatLayout="flow" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True">Date Wise</asp:ListItem>
                                                <asp:ListItem>Installment Wise</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="modDiv" runat="server">
                                        <label class="control-label">Select Mode&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpFilter" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Installment</asp:ListItem>
                                                <asp:ListItem>Yearly</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="classDiv" runat="server" visible="false">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Plese Select Class!"
                                                    InitialValue="<--Select-->" ControlToValidate="drpClass" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Status</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpStatus" runat="server" class="form-control-blue ">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem Selected="True">Paid</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                                <asp:ListItem>Cancelled</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-8  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Mode of Payment&nbsp;<span class="vd_red">*</span></label>
                                        <div class="txt-middle">
                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" CssClass="vd_radio radio-success"
                                                RepeatLayout="flow" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem Selected="True">Cash</asp:ListItem>
                                                <asp:ListItem>Cheque</asp:ListItem>
                                                <asp:ListItem>DD</asp:ListItem>
                                                <asp:ListItem>Online</asp:ListItem>
                                                <asp:ListItem>Card</asp:ListItem>
                                                <asp:ListItem>Bank Deposit</asp:ListItem>
                                                <asp:ListItem>NEFT/RTGS</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select User&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue ">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <div class="col-sm-12  no-padding" id="Panel1" runat="server">
                                    <%--<div class="col-md-12 ">

                                        <h4>Date Wise</h4>
                                    </div>--%>

                                    <div class="col-sm-12  no-padding">

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <label class="control-label">From Date&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">


                                                        <asp:DropDownList ID="FromYY" runat="server" OnSelectedIndexChanged="FromYY_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4 " AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="FromMM" runat="server" OnSelectedIndexChanged="FromMM_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="FromDD" runat="server" OnSelectedIndexChanged="FromDD_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4 ">
                                                        </asp:DropDownList>


                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="ToYY" runat="server" OnSelectedIndexChanged="ToYY_SelectedIndexChanged" CssClass="form-control-blue col-xs-4 "
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ToMM" runat="server" OnSelectedIndexChanged="ToMM_SelectedIndexChanged" CssClass="form-control-blue col-xs-4 "
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ToDD" runat="server" CssClass="form-control-blue col-xs-4 ">
                                                        </asp:DropDownList>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <label class="control-label">Enter S.R. No./Name&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control-blue"
                                                            onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
                                                        <asp:HiddenField ID="hfStudentId" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                            <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" CausesValidation="false" CssClass="button form-control-blue" 
                                                OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();">View</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 60px;"></div>

                                        </div>
                                    </div>


                                </div>

                                <div class="col-sm-12  no-padding" id="Panel2" runat="server">
                                    <%-- <div class="col-sm-12 ">

                                        <h4>Installment Wise</h4>
                                    </div>--%>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server" class="col-md-12 no-padding">
                                            <ContentTemplate>
                                                <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpGroup" runat="server" AutoPostBack="True" CssClass="form-control-blue "
                                                        OnSelectedIndexChanged="DrpGroup_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server" class="col-md-12 no-padding">
                                            <ContentTemplate>
                                                <label class="control-label">Select Installment&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:DropDownList ID="DropDownMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownMonth_SelectedIndexChanged"
                                                        TabIndex="1" CssClass="form-control-blue ">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton6" runat="server" CssClass="button form-control-blue" ValidationGroup="a" OnClick="LinkButton6_Click" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();">View</asp:LinkButton>
                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label32" runat="server" Style="color: #CC0000; font-weight: 700"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>



                                <div class="col-sm-12  mgbt-xs-5">
                                    <asp:UpdatePanel ID="UpdatePanel00" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel3" runat="server">
                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" data-toggle="tooltip" data-placement="top"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" data-toggle="tooltip" data-placement="top"><i class="fa fa-print "></i></asp:LinkButton>
                                                <script>
                                                    Sys.Application.add_load(tooltip);
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
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div id="gdv1" class="print-row col-sm-12   no-padding" runat="server">
                                                <div id="abc" class="print-row col-sm-12   no-padding" runat="server">
                                                    <div class="print-row col-sm-12  no-padding" style="width:85%">
                                                        <div id="header1" runat="server"></div>
                                                    </div>
                                                    <div class="print-row col-sm-12  text-center no-padding">
                                                        <asp:Label ID="Label1" runat="server" Text="Fee Collection (Receipt wise)"></asp:Label>

                                                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                        <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>

                                                        <asp:Label ID="lblTitle1" runat="server"></asp:Label>
                                                        <asp:Label ID="lblDate1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>

                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-12  no-padding print-row">
                                                        <div class=" table-responsive  table-responsive2">
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnPageIndexChanged="GridView1_PageIndexChanged"
                                                                OnPageIndexChanging="GridView1_PageIndexChanging" ShowFooter="True"
                                                                CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group"
                                                                OnRowDataBound="GridView1_RowDataBound">
                                                                <AlternatingRowStyle CssClass="alt" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label30" runat="server" Text='<%# Bind("SNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" Width="50px" />
                                                                        <ItemStyle CssClass="text-center " />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S.R. No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label24" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                                        <ItemStyle CssClass="text-center " />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label25" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <HeaderStyle CssClass="vd_bg-blue text-left vd_white" Width="170px" />
                                                                        <ItemStyle CssClass="text-left " />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Address">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                                        <ItemStyle CssClass="text-center " />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Class">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label21" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                                        <ItemStyle CssClass="text-center " />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Section">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label28" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                                        <ItemStyle CssClass="text-center " />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label19" runat="server" Text='<%# Bind("FeeDepositeDate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                                        <ItemStyle CssClass="text-center " Width="100px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Receipt No.">
                                                                        <ItemTemplate>

                                                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click"
                                                                                Text='<%# Bind("RecieptSrNo") %>' CssClass="tab-in-link"></asp:LinkButton>&nbsp;
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" Width="230px" />
                                                                        <ItemStyle CssClass="text-center " Width="230px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Installment">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblinstal" runat="server" Text='<%# Eval("FeeMonth") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                                        <ItemStyle CssClass="text-center " />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Mode">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMOP" runat="server" Text='<%# Bind("MOP") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                                        <ItemStyle CssClass="text-center " />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                                        <FooterTemplate>
                                                                            <span>Total : </span>
                                                                        </FooterTemplate>
                                                                        <FooterStyle CssClass="text-right" />
                                                                        <ItemStyle CssClass="text-center " />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Fees">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFees" runat="server" Text='<%# Bind("Fees") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>

                                                                            <asp:Label ID="lblTotalFees" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-right vd_white" />
                                                                        <ItemStyle CssClass="text-right " />
                                                                        <FooterStyle CssClass="text-right" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Conveyance">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblConvience" runat="server" Text='<%# Bind("ConveyanceAmount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>

                                                                            <asp:Label ID="lblTotalConvience" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-right vd_white" />
                                                                        <ItemStyle CssClass="text-right " />
                                                                        <FooterStyle CssClass="text-right" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Fine">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFine" runat="server" Text='<%# Bind("LateFeeAmount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>

                                                                            <asp:Label ID="lblTotalFine" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-right vd_white" />
                                                                        <ItemStyle CssClass="text-right " />
                                                                        <FooterStyle CssClass="text-right" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Exemption">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDiscountAmount" runat="server" Text='<%# Bind("DiscountAmount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalDiscount" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-right vd_white" />
                                                                        <ItemStyle CssClass="text-right " />
                                                                        <FooterStyle CssClass="text-right" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Total">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("TotalAmount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>

                                                                            <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-right vd_white" />
                                                                        <ItemStyle CssClass="text-right " />
                                                                        <FooterStyle CssClass="text-right" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Recieved">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label29" runat="server" Text='<%# Bind("RecievedAmount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="Label31" runat="server" Text=""></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue text-right vd_white" />
                                                                        <FooterStyle CssClass="text-right txt-bold" />
                                                                        <ItemStyle CssClass="text-right " />
                                                                        <FooterStyle CssClass="text-right" />
                                                                    </asp:TemplateField>
                                                                </Columns>

                                                            </asp:GridView>
                                                        </div>
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
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel4" runat="server" CssClass="popup animated2 fadeInDown col-md-4">
                    <div class="col-sm-12 text-center no-padding">
                        <asp:Label ID="lblcancel" runat="server" Style="font-weight: 700; color: #CC0000;"></asp:Label>
                    </div>

                    <table class="table table-striped table-hover no-head-border table-bordered">
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">Receipt No. </div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="lblID" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">Installment </div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="lblTotalFee" runat="server"></asp:Label>
                                        <asp:Button ID="Button2" runat="server" Style="display: none" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="tr7" runat="server">
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">Late Fee</div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="lblLate" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="tr8" runat="server">
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">Cheque Bounced Fee</div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="lblChequeBounce" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="tr9" runat="server">
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">Previous Balance </div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="Label25" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="tr10" runat="server">
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">Conveyance</div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="Label31" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="Panel7" runat="server">
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblDiscountPanel" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="lblDiscountPanelValue" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="tr11" runat="server">
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">Concession</div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="lblConcession" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">Total Amount</div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">Paid Amount</div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="lblPaidAmount" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="tr12" runat="server">
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">Current Balance</div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="lblBalace" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-6">Remark </div>
                                    <div class="col-sm-6 text-right">
                                        <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12 text-center">
                                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" CssClass=" button-y" Text="View" />&nbsp;&nbsp;
                                    <asp:Button ID="Button1" runat="server" CssClass="button-n" Text="OK" />
                                </div>
                            </td>
                        </tr>
                    </table>

                    <%-- ReSharper disable once Asp.InvalidControlType --%>
                    <ajaxToolkit:ModalPopupExtender ID="Panel4_ModalPopupExtender" runat="server" CancelControlID="Button1" PopupControlID="Panel4"
                        TargetControlID="Button2" BackgroundCssClass="popup_bg" BehaviorID="Panel4_ModalPopupExtender_Close">
                    </ajaxToolkit:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
