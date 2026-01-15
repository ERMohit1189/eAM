<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ManualDiscount_old.aspx.cs" Inherits="_2.ManualDiscount_old" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
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
                                if (data.d.length > 0) {
                                    response($.map(data.d,
                                        function (item) {
                                            return {
                                                label: item.split('@')[0],
                                                val: item.split('@')[1]
                                            }
                                        }));
                                }
                                else {
                                    $("[id$=hfStudentId]").val("000000");
                                }
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
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(prettyphoto);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <asp:TextBox ID="TxtEnter" placeholder="Enter Name/ S.R. No." runat="server" AutoPostBack="True" CssClass="form-control-blue validatetxt"
                                        OnTextChanged="TxtEnter_TextChanged" />
                                    <asp:HiddenField ID="hfStudentId" runat="server" />
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <asp:LinkButton ID="LinkButton7" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" runat="server" OnClick="LinkButton7_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                    <div id="msgbox" runat="server" style="left: 60px"></div>
                                </div>
                                <div class="col-sm-12  no-padding"></div>


                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <table style="width: 100%;" runat="server" id="grdshow" visible="False">
                                            <tr>
                                                <td class="tab-top">
                                                    <asp:GridView ID="Grdss" runat="server" AutoGenerateColumns="False" CssClass="table no-bm  table-striped table-hover no-head-border table-bordered">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label31" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enrollment No." Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label32" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                    <asp:Label ID="Label33" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                                                                    <asp:Label ID="Label23" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                    (<asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>)
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Medium">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label28" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date of Admission">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Contact No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                                <td class="tab-top tab-profile text-center ">
                                                    <div>
                                                        <div class="gallery-item fee-pic-box">
                                                            <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                                <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                            </asp:HyperLink>
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" NavigateUrl="" Target="_blank" Text="more..."></asp:HyperLink>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div class="col-sm-12 " runat="server" id="divControls" visible="false">

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label12" runat="server" class="control-label" Text="Discount For"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged"
                                                CssClass="form-control-blue">
                                                <asp:ListItem Value="">All</asp:ListItem>
                                                <asp:ListItem Value="Tuition Fee">Tuition Fee</asp:ListItem>
                                                <asp:ListItem Value="Transport Fee">Transport Fee</asp:ListItem>
                                                <asp:ListItem Value="Hostel Fee">Hostel Fee</asp:ListItem>
                                                <asp:ListItem Value="Miscellaneous Fee">Miscellaneous Fee</asp:ListItem>

                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15" id="divdDiscountNameInsert" runat="server">
                                        <label class="control-label">Discount Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList runat="server" ID="drpDiscountNameInsert" CssClass="form-control-blue"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-9" id="divRemark" runat="server">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15" id="divFillInstallment" runat="server">
                                        <label class="control-label">Fill in all Installment</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox4" CssClass="form-control-blue"
                                                onblur="copyText(this); decimalOrNumeric(this);" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <ItemTemplate>
                                            <div class="col-sm-3  half-width-50 mgbt-xs-15">

                                                <asp:Label ID="LabelMonthid" class="control-label hide" runat="server" Text='<%# Bind("MonthId") %>'></asp:Label>
                                                <asp:Label ID="Label9" class="control-label" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox1" runat="server" onblur="decimalOrNumeric(this);" CssClass="form-control-blue input"></asp:TextBox>

                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="javascript:scroll(0,0);" OnClick="LinkButton1_Click"
                                            ValidationGroup="a" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding hide">
                                    <div class="col-sm-6  mgbt-xs-15">

                                        <asp:Label ID="Label8" class="control-label" runat="server" Text="Select Class"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="drpclass" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpclass1_SelectedIndexChanged" ValidationGroup="b">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  mgbt-xs-15">

                                        <asp:Label ID="Label6" class="control-label" runat="server" Text="Select Head"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="drpHrads" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpHrads_SelectedIndexChanged" ValidationGroup="b">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-sm-12 ">
                                    <h2 runat="server" id="hdnTutionFeeDiscount" visible="false" style="font-size: 18px; margin-top:30px;">Tuition Fee Discount</h2>
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="GrdTutionFeeDetails" runat="server" CssClass="table no-bm  table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="False"
                                            ShowFooter="True">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Installment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" CssClass="hide" Text='<%# Bind("id") %>'></asp:Label>
                                                        <asp:Label ID="lblInstallmentId" CssClass="hide" runat="server" Text='<%# Bind("InstallmentId") %>'></asp:Label>
                                                        <asp:Label ID="Label1s" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="130" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount For">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("DiscountFor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="120" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount Name">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" Enabled="false" ID="drpDiscountName" CssClass="form-control-blue"></asp:DropDownList>
                                                        <asp:Label ID="lblDiscountName" runat="server" CssClass="hide" Text='<%# Bind("DiscountName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="dddd" runat="server" Text="Label">Total</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="170" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalDiscount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="90" />
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRemarks" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="240" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLoginName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label><br />
                                                        (<asp:Label ID="Label2" runat="server" Text='<%# Bind("RecordedDate", "{0:dd-MMM-yyyy hh:mm:ss tt}") %>'></asp:Label>)
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="140" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" SkinID="Delete"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdUpdate" runat="server" Text='<%# Bind("id") %>' CssClass="hide"></asp:Label>
                                                        <asp:LinkButton ID="lnkUpdateTutionFee" runat="server" OnClick="lnkUpdateTutionFee_Click"
                                                            title="Update"  class="btn menu-icon vd_bd-red vd_red"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                    <h2 runat="server" id="hdnTransportFeeDiscount" visible="false">Transport Fee Discount</h2>
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="GrdTransportFeeDetails" runat="server" CssClass="table no-bm  table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="False"
                                            ShowFooter="True">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Installment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" CssClass="hide" Text='<%# Bind("id") %>'></asp:Label>
                                                        <asp:Label ID="lblInstallmentId" CssClass="hide" runat="server" Text='<%# Bind("InstallmentId") %>'></asp:Label>
                                                        <asp:Label ID="Label1s" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="130" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount For">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("DiscountFor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="120" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount Name">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" Enabled="false" ID="drpDiscountName" CssClass="form-control-blue"></asp:DropDownList>
                                                        <asp:Label ID="lblDiscountName" runat="server" CssClass="hide" Text='<%# Bind("DiscountName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="dddd" runat="server" Text="Label">Total</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="170" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalDiscount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="90" />
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRemarks" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="240" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLoginName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label><br />
                                                        (<asp:Label ID="Label2" runat="server" Text='<%# Bind("RecordedDate", "{0:dd-MMM-yyyy hh:mm:ss tt}") %>'></asp:Label>)
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="140" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" SkinID="Delete"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdUpdate" runat="server" Text='<%# Bind("id") %>' CssClass="hide"></asp:Label>
                                                        <asp:LinkButton ID="lnkUpdateTransport" runat="server" OnClick="lnkUpdateTransport_Click"
                                                            title="Update"  class="btn menu-icon vd_bd-red vd_red"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                    <h2 runat="server" id="hdnHostelFeeDiscount" visible="false">Hostel Fee Discount</h2>
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="GrdHostelFeeDetails" runat="server" CssClass="table no-bm  table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="False"
                                            ShowFooter="True">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Installment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" CssClass="hide" Text='<%# Bind("id") %>'></asp:Label>
                                                        <asp:Label ID="lblInstallmentId" CssClass="hide" runat="server" Text='<%# Bind("InstallmentId") %>'></asp:Label>
                                                        <asp:Label ID="Label1s" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="130" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount For">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("DiscountFor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="120" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount Name">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="drpDiscountName" Enabled="false" CssClass="form-control-blue"></asp:DropDownList>
                                                        <asp:Label ID="lblDiscountName" runat="server" CssClass="hide" Text='<%# Bind("DiscountName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="dddd" runat="server" Text="Label">Total</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="170" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalDiscount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="90" />
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRemarks" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="240" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLoginName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label><br />
                                                        (<asp:Label ID="Label2" runat="server" Text='<%# Bind("RecordedDate", "{0:dd-MMM-yyyy hh:mm:ss tt}") %>'></asp:Label>)
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="140" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" SkinID="Delete"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdUpdate" runat="server" Text='<%# Bind("id") %>' CssClass="hide"></asp:Label>
                                                        <asp:LinkButton ID="lnkUpdateHostel" runat="server" OnClick="lnkUpdateHostel_Click"
                                                            title="Update"  class="btn menu-icon vd_bd-red vd_red"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                    <h2 runat="server" id="hdnMiscellaneousFeeDiscount" visible="false">Miscellaneous Fee Discount</h2>
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="GrdMiscellaneousFeeDetails" runat="server" CssClass="table no-bm  table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="False"
                                            ShowFooter="True">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Installment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" CssClass="hide" Text='<%# Bind("id") %>'></asp:Label>
                                                        <asp:Label ID="lblInstallmentId" CssClass="hide" runat="server" Text='<%# Bind("InstallmentId") %>'></asp:Label>
                                                        <asp:Label ID="Label1s" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="130" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount For">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("DiscountFor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="120" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount Name">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="drpDiscountName" Enabled="false" CssClass="form-control-blue"></asp:DropDownList>
                                                        <asp:Label ID="lblDiscountName" runat="server" CssClass="hide" Text='<%# Bind("DiscountName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="dddd" runat="server" Text="Label">Total</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="170" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalDiscount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="90" />
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRemarks" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="240" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLoginName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label><br />
                                                        (<asp:Label ID="Label2" runat="server" Text='<%# Bind("RecordedDate", "{0:dd-MMM-yyyy hh:mm:ss tt}") %>'></asp:Label>)
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="140" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" SkinID="Delete"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdUpdate" runat="server" Text='<%# Bind("id") %>' CssClass="hide"></asp:Label>
                                                        <asp:LinkButton ID="lnkUpdateMiscellaneous" runat="server" OnClick="lnkMiscellaneous_Click"
                                                            title="Update"  class="btn menu-icon vd_bd-red vd_red"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
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
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">

                        <tr>
                            <td style="text-align: center;">
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CssClass="button-n" />
                                &nbsp; &nbsp;
                                    <asp:Button ID="btnDelete" runat="server" OnClientClick="javascript:scroll(0,0);" OnClick="btnDelete_Click" Text="Yes" CausesValidation="False" CssClass="button-y" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" TargetControlID="Button7" PopupControlID="Panel2"
                    CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </ajaxToolkit:ModalPopupExtender>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <div class="mgtp-15 text-center">
                            <asp:Label ID="lblIds" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblDiscountName" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblmonthtId" runat="server" Visible="False"></asp:Label>
                        <div class="col-sm-12 ">
                            <table class="tab-popup">
                                <tr>
                                    <td>Discount Name <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpDiscountNamePanel" CssClass="form-control-blue"></asp:DropDownList>
                                        </td>
                                </tr>
                                <tr>
                                    <td>Amount <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="lblAmount" runat="server" CssClass="form-control-blue validatetxt1" onblur="decimalOrNumeric(this);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Remark <span class="vd_red"></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="lblRemark" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                
                               
                            </table>
                        </div>
                    </div>
                    <div class="mgtp-15  text-center">
                        <asp:LinkButton ID="lnkNo" runat="server" CssClass="button-n">Cancel</asp:LinkButton>&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkYes" runat="server" CssClass="button-y" OnClick="lnkYes_Click">Update</asp:LinkButton>
                    </div>
                    <asp:LinkButton ID="lnkTarget1" runat="server" Style="display: none"></asp:LinkButton>
                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="lnkNo"
                        Enabled="True" PopupControlID="Panel1" TargetControlID="lnkTarget1" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </asp:Panel>
            </div>

            <script>
                function copyText(textbox) {
                    try {
                        var input = document.querySelectorAll(".input");
                        var amount = textbox.value;
                        for (var i = 0; i < input.length; i++) {
                            input[i].value = (parseFloat(amount)==0?"":amount);
                        }
                    }
                    catch (err) {
                        alert(err.message);
                    }
                }
            </script>

            <script>
                function copyText1(textbox) {
                    try {
                        var input = document.querySelectorAll(".input1");
                        var amount = textbox.value;
                        for (var i = 0; i < input.length; i++) {
                            input[i].value = amount;
                        }
                    }
                    catch (err) {
                        alert(err.message);
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    <style>
        a.pp_next {
            display: none !important;
        }

        a.pp_previous {
            display: none !important;
        }

        div.light_square .pp_gallery a.pp_arrow_previous, div.light_square .pp_gallery a.pp_arrow_next {
            display: none !important;
        }

        .pp_gallery div {
            display: none !important;
        }
    </style>
</asp:Content>

