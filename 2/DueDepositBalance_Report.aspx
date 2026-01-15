<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DueDepositBalance_Report.aspx.cs"
    Inherits="_2.DueDepositBalance_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <%-- ReSharper disable once Html.PathError --%>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=TxtEnter]").autocomplete({
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
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);

            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding " id="Panel1" runat="server">

                                    <asp:RadioButtonList ID="rblSelectType" runat="server" Visible="false">
                                        <asp:ListItem>Studentwise</asp:ListItem>
                                        <asp:ListItem Selected="True">Classwise</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 select-list-hide " style="display: none">
                                        <asp:DropDownList ID="DrpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter S.R. No./Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <i>H</i>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 " style="display: none">
                                        <asp:TextBox ID="TxtEnter" placeholder="Enter No." runat="server"
                                            class="form-control-blue width-100 validatetxt" AutoPostBack="true" OnTextChanged="TxtEnter_TextChanged"></asp:TextBox>

                                        <div class="text-box-msg">
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                        <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control-blue width-100 validatedrp" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control-blue width-100"></asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control-blue width-100"></asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                        <asp:DropDownList ID="ddlInstallment" runat="server" CssClass="form-control-blue width-100 validatedrp"></asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return ValidateDropdown('.validatedrp');"
                                            OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 64px"></div>

                                        <asp:Label ID="lblFee" runat="server"></asp:Label>
                                    </div>


                                </div>
                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel110" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Div1" runat="server">
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
                                <div class="col-sm-12  no-padding " id="DivExport" runat="server" visible="false">
                                    <div class="col-sm-12  ">
                                        <div class=" table-responsive  table-responsive2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <div id="header" runat="server" style="width: 85%"></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="title" class="col-md-12 col-lg-12 col-sm-12 text-center">
                                                            DUE DEPOSIT BALANCE TILL:
                                                <asp:Label ID="lblSession" runat="server" Text=""></asp:Label>
                                                            <br />
                                                            REPORT DATE:
                                                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridView1" ShowFooter="true" runat="server" AutoGenerateColumns="False"
                                                            OnRowDataBound="GridView1_RowDataBound"
                                                            CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label11" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Student's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label17" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Class">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                        (<asp:Label ID="Label19" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>)
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Stream" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBranchName" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Medium">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label21" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Date of Calculation">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("InsertDate") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalText" runat="server" Text="Total"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Fee">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFee" runat="server" Text='<%# Bind("Fee") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalFee" runat="server" Text="Label"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Transport">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTransport" runat="server" Text='<%# Bind("Transport") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalTransport" runat="server" Text="Label"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Fine">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFine" runat="server" Text='<%# Bind("Fine") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalFine" runat="server" Text="Label"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Exemption">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExemption" runat="server" Text='<%# Bind("Exemption") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalExemption" runat="server" Text="Label"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Total">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalTotal" runat="server" Text="Label"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Paid">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPaidAmount" runat="server" Text='<%# Bind("PaidFeeAmount") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalPaid" runat="server" Text="Label"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Balance">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("BalanceAmount") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalBalance" runat="server" Text="Label"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>





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
