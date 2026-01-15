<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true"
    CodeFile="UniformFeeDepositReport.aspx.cs" Inherits="UniformFeeDepositReport" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSearchStudent]").autocomplete({
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

        function getStaffList() {
            $(function () {
                $("[id$=txtSearchStaff]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetEmployee") %>',
                            data: "{ 'empId': '" + request.term + "'}",
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
                        $("[id$=hfStaffId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loaders" runat="server"></div>
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
                                    <div class="col-sm-4   " id="divBranch" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                                <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-3 " id="divSession" runat="server" visible="false">
                                        <label class="control-label">Session&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSession" runat="server" CssClass="form-control-blue ">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
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
                                                    <asp:DropDownList ID="FromDD" runat="server"
                                                        CssClass="form-control-blue col-xs-4 ">
                                                    </asp:DropDownList>


                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-sm-3   ">
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
                                    <div class="col-sm-3  hide">
                                        <label class="control-label">Select Frequency Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpFilter" runat="server" CssClass="form-control-blue" Enabled="false">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>OneTime</asp:ListItem>
                                                <asp:ListItem>Monthly</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 ">
                                        <label class="control-label">Status</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpStatus" runat="server" class="form-control-blue ">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Paid</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 ">
                                        <label class="control-label">Mode of Payment&nbsp;<span class="vd_red">*</span></label>
                                        <asp:DropDownList ID="drpPaymentMode" runat="server" CssClass="vd_radio radio-success">
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Cheque</asp:ListItem>
                                            <asp:ListItem>DD</asp:ListItem>
                                            <asp:ListItem>Online</asp:ListItem>
                                            <asp:ListItem>Card</asp:ListItem>
                                            <asp:ListItem>Bank Deposit</asp:ListItem>
                                            <asp:ListItem>NEFT/RTGS</asp:ListItem>
                                            <asp:ListItem>Other</asp:ListItem>
                                            <asp:ListItem Value="Online">Online</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3   ">
                                        <label class="control-label">Select User&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpUsers" runat="server" CssClass="form-control-blue ">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  hide">
                                        <label class="control-label">Select Type&nbsp;<span class="vd_red">*</span></label>
                                        <asp:DropDownList runat="server" ID="rdoType" CssClass="rdoType" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="Student" Selected="True">Student</asp:ListItem>
                                            <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-9 ">
                                        <div class="row">

                                            <div class="col-sm-4 col-xs-5  " id="divStudent" runat="server">
                                                <label class="control-label">S.R. No.&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtSearchStudent" placeholder="Enter Name/ S.R. No." runat="server" AutoPostBack="True" CssClass="form-control-blue"
                                                        OnTextChanged="txtSearchStudent_TextChanged" onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
                                                    <asp:HiddenField ID="hfStudentId" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-4 col-xs-5  " id="divStaff" runat="server" visible="false">
                                                <label class="control-label">EmpId/Empcode&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtSearchStaff" placeholder="Enter Name/ EmpId/ Empcode" runat="server" AutoPostBack="True" CssClass="form-control-blue"
                                                        OnTextChanged="txtSearchStaff_TextChanged" onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
                                                    <asp:HiddenField ID="hfStaffId" runat="server" />
                                                </div>
                                            </div>
                                            <script>
                                                function onchangetxt() {

                                                    if (document.getElementById('<%= txtSearchStudent.ClientID %>').value.length === 0) {
                                                    document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                                }
                                                if (document.getElementById('<%= txtSearchStaff.ClientID %>').value.length === 0) {
                                                    document.getElementById('<%= hfStaffId.ClientID %>').value = "";
                                                    }

                                                }

                                                function onchangeatcopyandpaste() {

                                                    document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                                document.getElementById('<%= hfStaffId.ClientID %>').value = "";
                                                }

                                            </script>

                                            <div class="col-sm-6 col-xs-2  ">
                                                <label class="control-label">&nbsp;</label>
                                                <div class="">
                                                    <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" CssClass="button form-control-blue"> View</asp:LinkButton>
                                                    <asp:Label ID="lblFee" runat="server" Style="color: #FF0000"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 ">
                                                <div id="msgbox0" runat="server"></div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-sm-12  mgbt-xs-5">
                                    <asp:UpdatePanel ID="UpdatePanel01" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;">
                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print"><i class="fa fa-print "></i></asp:LinkButton>
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
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div id="gdv1" class="print-row col-sm-12   no-padding" runat="server">
                                                <div id="abc" class="print-row col-sm-12   no-padding" runat="server">
                                                    <div class="print-row col-sm-12  no-padding" style="width: 85%">
                                                        <div id="header1" runat="server"></div>
                                                    </div>
                                                    <div class="print-row col-sm-12  text-center no-padding">
                                                        <asp:Label ID="Label1" runat="server" Text="Fee Collection (Receipt wise)"></asp:Label>

                                                        <asp:Label ID="lbloptions" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-12  no-padding print-row">
                                                        <div class=" table-responsive  table-responsive2">
                                                            <asp:GridView ID="GridOneTime" runat="server" AutoGenerateColumns="false" ShowFooter="true" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblbedidO" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Receipt No.">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnView" runat="server" Text='<%# Bind("ReceiptNo") %>' OnClick="btnView_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="170px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lbldate" Text='<%# Bind("DepositDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S.R. No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblSrNoOrEmpId" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Student's Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblName" Text='<%# Bind("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Father's Name" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblFatherName" Text='<%# Bind("fatherName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Class">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblClass" Text='<%# Bind("Class") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Mode">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblModeO" Text='<%# Bind("Mode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblsts" Text='<%# Bind("PaymentSatus") %>'></asp:Label>
                                                                        </ItemTemplate>

                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblAmountO" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Exemption">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblExemptionO" Text='<%# Bind("Exemption") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Paid">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblPaidO" Text='<%# Bind("Paid") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label runat="server" ID="lblTotalPaid"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Balance">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblDueO" Text='<%# Bind("NextDue") %>'></asp:Label>
                                                                        </ItemTemplate>

                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
