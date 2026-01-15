<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EveryStudentYearlyReportBalance.aspx.cs" Inherits="_2.AdminEveryStuBalancedList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>


            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpFilter" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem>Defaulters</asp:ListItem>
                                                <asp:ListItem>All</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpCard" runat="server" CssClass="form-control-blue  validatedrp"
                                                AutoPostBack="True" OnSelectedIndexChanged="drpCard_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue  validatedrp"
                                                AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Installment&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpStream" runat="server" CssClass="form-control-blue  validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-6-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();"
                                            CssClass="form-control-blue  button">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 60px;"></div>
                                    </div>
                                </div>


                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel1" runat="server">


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
                                    <div class="table-responsive2 table-responsive" runat="server" id="divExport">
                                        <table class="table mp-table no-tb tab-td-n">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <div id="header1" runat="server" style="width: 100%;"></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">
                                                        <asp:Label ID="Label2" runat="server" Text="Balance List of Students"></asp:Label>
                                                        &nbsp;<asp:Label ID="Label3" runat="server" Text="("></asp:Label>
                                                        <asp:Label ID="lblCurrentDate" runat="server"></asp:Label>
                                                        <asp:Label ID="Label4" runat="server" Text=")"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="false" CssClass="table mp-table no-tb tab-td-n">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <table style="width: 100%;" class=" text-center">
                                                                            <tr>
                                                                                <td>
                                                                                    <span style="font-weight: 700">Class : </span>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                                    <span style="font-weight: 700">|</span>
                                                                                    <span style="font-weight: 700">Section : </span>
                                                                                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                                                    <span style="font-weight: 700">|</span>
                                                                                    <span style="font-weight: 700">Stream : </span>
                                                                                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                                                            CssClass="table table-striped table-hover no-bm no-head-border mgtp-5 table-bordered pro-table table-header-group">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="#">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSno" runat="server" CssClass="sr" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblTotalSTU" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-center" Width="40px" />
                                                                                    <FooterStyle CssClass="text-center" Font-Bold="true" />
                                                                                    <ItemStyle CssClass="text-center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Bind("Srno") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="text-center" Width="70px" />
                                                                                    <ItemStyle CssClass="text-center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblStuName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="text-left" />
                                                                                    <ItemStyle CssClass="text-left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblFaName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="text-left" />
                                                                                    <ItemStyle CssClass="text-left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Mobile No.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("FatherContactNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="text-center" />
                                                                                    <ItemStyle CssClass="text-center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Section" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label20" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="text-center" />
                                                                                    <ItemStyle CssClass="text-center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Stream" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label19" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="text-center" />
                                                                                    <ItemStyle CssClass="text-center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Fee Mode" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblFeeMode" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label5" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblApr" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblAprSum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label6" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMay" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblMaySum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label7" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblJun" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblJunSum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label8" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblJul" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblJulSum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label9" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAug" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblAugSum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label10" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSep" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblSepSum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label11" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblOct" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblOctSum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label12" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNov" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblNovSum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label13" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDec" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblDecSum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label14" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblJan" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblJanSum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label15" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblFeb" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblFebSum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="Label16" runat="server"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMar" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblMarSum" runat="server"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Cheque Bounce Fine">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblChequeBounceFine" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblTotalChequeBounceFine" runat="server" Style="font-weight: 700"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Total">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTotBalance" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblTotMonthDues" runat="server" Style="font-weight: 700"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle CssClass="text-right" />
                                                                                    <ItemStyle CssClass="text-right" />
                                                                                    <FooterStyle CssClass="text-right" Font-Bold="true" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>

                                                                    </ItemTemplate>

                                                                    <FooterTemplate>

                                                                        <strong>Total Dues till this Month: </strong>
                                                                        <asp:Label ID="lblTotMonthDues" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle CssClass="p-pad-0" />
                                                                    <ItemStyle CssClass="p-pad-0" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>




                            </div>
                        </div>
                    </div>
                </div>
            </div>
           <%-- <script>
                function reOrderGrid() {
                    var span = document.getElementsByClassName("sr");
                    for (var i = 0; i < span.length; i++) {
                        span[i].textContent = (i + 1);
                    }
                }
            </script>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

