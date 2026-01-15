<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmployeeDayBookReport.aspx.cs" Inherits="_7.EmployeeDayBookReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Day Book Report
    </title>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtEmpID]").autocomplete({
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
                            error: function (request, status, error) { alert(request); alert(status); alert(error); },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfEmpID]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
    <style>
        .links img {
            width: 30px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                try {
                    Sys.Application.add_load(getStudentsList);
                }
                catch (ex) {
                }

            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">
                                    
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From Date</label>
                                        <div class="">

                                            <script>
                                                Sys.Application.add_load(datetime);
                                            </script>

                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date</label>
                                        <div class="">
                                            <script>
                                                Sys.Application.add_load(datetime);
                                            </script>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 half-width-50 mgbt-xs-15 " id="disheadpaymentmode" runat="server">
                                        <label class="control-label">Payment Mode&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem>Cash</asp:ListItem>
                                                        <asp:ListItem>Cheque</asp:ListItem>
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
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Employee Name &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtEmpID" AutoPostBack="true" OnTextChanged="txtEmpID_TextChanged"
                                                runat="server" CssClass="form-control-blue  validatetxt" onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()"></asp:TextBox>
                                            <asp:HiddenField ID="hfEmpID" runat="server" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="button form-control-blue" OnClick="btnSearch_Click">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 30px"></div>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="gvDayBook" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center ">

                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name" HeaderStyle-Width="16%">
                                                        <ItemTemplate>
                                                            &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mode" HeaderStyle-Width="6%">
                                                        <ItemTemplate>
                                                            &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Bind("PaymentMode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Month/Type" HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            &nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Bind("SalaryMonth") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Head" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                            &nbsp;&nbsp;<asp:Label ID="Label5s" runat="server" Text='<%# Bind("HeadName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" HeaderStyle-Width="23%">
                                                        <ItemTemplate>
                                                            &nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            &nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Text='<%# Bind("InsertedDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="Labeldd" runat="server" Text="Total : "></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Income" HeaderStyle-Width="7%">
                                                        <ItemTemplate>
                                                            &nbsp;&nbsp;<asp:Label ID="Label8" runat="server" Text='<%# Bind("CRs") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="Label7F" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Expense" HeaderStyle-Width="7%">
                                                        <ItemTemplate>
                                                             &nbsp;&nbsp;<asp:Label ID="Label7" runat="server" Text='<%# Bind("DRs") %>'></asp:Label>
                                                           
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="Label8F" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:templatefield headertext="Balance" HeaderStyle-Width="7%">
                                                    <itemtemplate>
                                                        &nbsp;&nbsp;<asp:label id="label10" runat="server"  Text='<%# Bind("EmpBalance") %>'></asp:label>
                                                    </itemtemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="Label10F" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                </asp:templatefield>
                                                     <asp:TemplateField HeaderText="Invoice" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="invNo" runat="server" Text='<%# Bind("InvoiceNo") %>'></asp:Label>
                                                            <asp:HyperLink runat="server" ID="invPath" NavigateUrl='<%# Bind("InvoicePath") %>' ImageUrl="../img/downloadDoc.png" Text='<%# Bind("InvoiceNo") %>' Target="_blank" Font-Underline="true" ForeColor="Blue" CssClass="links"></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="grid_head" />
                                                <RowStyle CssClass="grid_middle" />
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

