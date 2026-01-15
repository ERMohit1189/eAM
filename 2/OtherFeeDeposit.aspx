<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="OtherFeeDeposit.aspx.cs" Inherits="_2.OtherFeeDeposit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
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
                            error: function (request, status, error) { alert(request); alert(status); alert(error); },
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
    <style>
        .table-responsive2 {
            border:0 !important;
        }
    </style>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(scrollbar);
                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(prettyphoto);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-4 mgbt-xs-15">
                                        <div class="">
                                            <asp:TextBox ID="TxtEnter" placeholder="Enter Name/ S.R. No." runat="server" CssClass="form-control-blue validatetxt" OnTextChanged="TxtEnter_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton2_Click" class="button form-control-blue" ValidationGroup="a"><i class="fa fa-eye"></i>&nbsp;View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 60px;"></div>
                                    </div>
                                </div>



                                <div class="col-sm-12 no-padding" style="padding: 13px !important;">
                                    <div class="table-responsive2 table-responsive">
                                        <table style="width: 100%;" runat="server" id="grdshow" visible="False">
                                            <tr>
                                                <td class="tab-top">
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table no-bm  table-striped table-hover no-head-border table-bordered">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enrollment No." Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGender" runat="server" Text='<%# Bind("Gender") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblClassId" runat="server" Text='<%# Bind("ClassId") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Medium">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMedium" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFeeGroup" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date of Admission">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDOA" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                                               <%-- <td class="tab-top tab-profile text-center ">
                                                    <div>
                                                        <div class="gallery-item" style="right: 14px; top: 13px; width: 49px; height: 0px;">
                                                            <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                                <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                            </asp:HyperLink>
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" NavigateUrl="" Target="_blank" Text="more..."></asp:HyperLink>
                                                    </div>
                                                </td>--%>
                                                <td class="tab-top tab-profile text-center onprint">
                                                    <div class="gallery-item fee-pic-box">
                                                        <asp:HyperLink ID="studentImg" runat="server" data-rel="prettyPhoto[2]">
                                                            <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                        </asp:HyperLink>
                                                        <%--<a href="#" target="_blank" class="more-btn">more...</a>--%>
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" Target="_blank" Text="more..." CssClass=""></asp:HyperLink>
                                                    </div>

                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>



                                <div id="Tables" runat="server" visible="false">

                                    <div class="col-sm-12 no-padding">
                                        <div class="col-sm-4 mgbt-xs-15 ">
                                            <asp:Label ID="Label16" runat="server" class="control-label" Text="Date"></asp:Label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="DDYear" runat="server" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged" Enabled="false"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDMonth" runat="server" TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged" Enabled="false"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDDate" runat="server" TabIndex="4" AutoPostBack="True" Enabled="false" CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mgbt-xs-15">
                                            <asp:Label ID="Label12" runat="server" class="control-label" Text="Mode of Payment"></asp:Label>
                                            <div class="">
                                                <asp:DropDownList ID="DropDownMOD" runat="server" AutoPostBack="True" TabIndex="1" CssClass="form-control-blue " OnSelectedIndexChanged="DropDownMOD_SelectedIndexChanged">
                                                    <asp:ListItem>Cash</asp:ListItem>
                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                    <asp:ListItem>DD</asp:ListItem>
                                                    <asp:ListItem>Card</asp:ListItem>
                                                    <asp:ListItem>Online Transfer</asp:ListItem>
                                                    <%--<asp:ListItem>Other</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                         <div class="col-sm-4 mgbt-xs-15" id="divModeDate"  runat="server" visible="false">
                                            <asp:Label ID="Label6" runat="server" class="control-label" Text=""></asp:Label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="DDYearMode" runat="server" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="DDYearMode_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDMonthMode" runat="server" TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="DDMonthMode_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDDateMode" runat="server" TabIndex="4" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        

                                        <div class="col-sm-4 mgbt-xs-15 " id="divModeNo" runat="server" visible="false">
                                            <asp:Label ID="Label42" class="control-label" runat="server"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="txtModeNo" runat="server" CssClass="form-control-blue validatetxt" TabIndex="5"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                       
                                        <div class="col-sm-4 mgbt-xs-15 " id="divbankname" runat="server" visible="false">
                                            <asp:Label ID="Label" runat="server" class="control-label" Text="Bank Name"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="txtbankname" runat="server" CssClass="form-control-blue" TabIndex="6" Text="NA"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mgbt-xs-15 " id="divStatus" runat="server" visible="false">
                                            <asp:Label ID="Label5" runat="server" class="control-label" Text="Status"></asp:Label>
                                            <div class="">
                                                <asp:DropDownList ID="DropDownStatus" runat="server" TabIndex="7" CssClass="form-control-blue ">
                                                    <asp:ListItem>Paid</asp:ListItem>
                                                    <asp:ListItem>Pending</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mgbt-xs-15" id="divRemark" runat="server" visible="false">
                                            <asp:Label ID="Label4" runat="server" class="control-label" Text="Remark"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue" TabIndex="8" Text="NA"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        
                                    </div>

                                    <div class="col-sm-12 ">
                                    <div class="col-sm-6 no-padding">

                                        <div class="table-responsive2 table-responsive">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table no-bm  table-striped table-hover no-head-border table-bordered" ShowFooter="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label31" runat="server" Text='<%# Bind("Ids") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fee Head">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("Id") %>' Visible="False"></asp:Label>
                                                                    <asp:Label ID="Label32" runat="server" Text='<%# Bind("HeadName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    Total
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="HeadAmount" runat="server" Text='<%# Bind("Amount") %>'>0</asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="TotalHeadAmount" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-1 no-padding"></div>
                                    <div class="col-sm-6 " runat="server" id="divSubmit" style="padding-right: 0px;">
                                        <table class="table no-bm  show-payble-box">
                                             <tr>
                                                 <th style="text-align: left; width: 25%;">
                                                     <asp:Label runat="server" ID="lblAmtName" Text="Amount"></asp:Label></th>
                                                 <td class="text-right txt-bold">
                                                     <asp:Label ID="LblHeadTotalAmount" runat="server" Style="padding-left: 5px; padding-right: 5px;">0</asp:Label></td>
                                                 <th style="text-align: left; width: 25%;">Exemption</th>
                                                 <td>
                                                     <asp:TextBox runat="server" ID="txtDiscount" CssClass="underlined text-left input-border-btm vd_bd-red text-right" onblur="DiscountChange()" Text="0.00" onkeypress="return isNumber(event)"></asp:TextBox>
                                                 </td>
                                             </tr>

                                             <tr runat="server" id="divChequeFine" class="hide">
                                                 <th style="text-align: left; width: 25%;">Cheque Bounce Fee</th>
                                                 <td>
                                                     <asp:Label ID="lblChequeFine" runat="server" Style="padding-left: 5px;" Text="0.00"></asp:Label></td>
                                             </tr>

                                             <tr style="display: none;">
                                                 <th style="text-align: left; width: 25%;">Payable Amount</th>
                                                 <td>
                                                     <asp:Label runat="server" ID="txtPayable" Text="0.00"></asp:Label>
                                                 </td>
                                             </tr>

                                             <tr>
                                                 <th style="text-align: left; width: 25%;">Paid</th>
                                                 <td>
                                                     <asp:TextBox runat="server" ID="TextPay" CssClass="underlined text-left input-border-btm vd_bd-red text-right" onblur="TextPayChange()" Text="0.00" onkeypress="return isNumber(event)"></asp:TextBox>
                                                 </td>
                                                 <th style="text-align: left; width: 30%;">Balance Amount</th>
                                                 <td class="text-right txt-bold">
                                                     <asp:Label runat="server" ID="lblNextDueAmt" Style="padding-left: 5px; padding-right: 5px;">0</asp:Label></td>
                                             </tr>
                                        </table>
                                    </div>
                                        
                                        
                                    <div class="col-sm-12 no-padding text-primary">
                                        <br />
                                        <asp:Button ID="btnSubmit" CssClass="button form-control-blue" runat="server" OnClick="btnSubmit_Click" 
                                            Text="Submit"
                                            OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();this.disabled='true'; this.value='Please Wait...';"/>
                                        <div id="msgbox2" runat="server" style="left: 60px;"></div>
                                        <br />
                                    </div>
                                </div>
                                    </div>

                                <div class="col-sm-12 mgtp-15">

                                    <div class="col-sm-12 no-padding">
                                        <div class="table-responsive2 table-responsive">
                                            <asp:GridView ID="GridView2" runat="server" class="table table-striped table-hover no-bm no-head-border table-bordered" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sn" runat="server" Text='<%# Bind("ids") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receipt No.">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton6" runat="server"
                                                                Text='<%# Bind("Receipt_no") %>' OnClick="LinkButton6_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_date" runat="server" Text='<%# Bind("DepositDate", "{0:dd MMM yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mode">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PaymentMode" runat="server" Text='<%# Bind("Mode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PaymentSatus" runat="server" Text='<%# Bind("PaymentSatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PayableAmount" runat="server" Text='<%# Bind("PayableAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fine">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BounceCharges" runat="server" Text='<%# Bind("BounceCharges") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Exemption">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Discount" runat="server" Text='<%# Bind("Discount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Paid Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_PaidAmt" runat="server" Text='<%# Bind("PaidAmt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_NextDeuAmt" runat="server" Text='<%# Bind("NextDeuAmt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    
                                                    
                                                    
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>

                                <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
                                <%-- ReSharper disable once Asp.InvalidControlType --%>
                                <asp:ModalPopupExtender ID="model" runat="server" BackgroundCssClass="popup_bg"
                                    TargetControlID="Button2" PopupControlID="Panel1">
                                </asp:ModalPopupExtender>
                                <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none" />
                                <%-- ReSharper disable once Asp.InvalidControlType --%>
                                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="popup_bg"
                                    TargetControlID="Button7" PopupControlID="Panel3">
                                </asp:ModalPopupExtender>
                                <asp:Button ID="Button10" runat="server" Text="Button" Style="display: none" />
                                <%-- ReSharper disable once Asp.InvalidControlType --%>
                                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="popup_bg"
                                    TargetControlID="Button10" PopupControlID="Panel4">
                                </asp:ModalPopupExtender>
                                <br />

                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">

                                        <table class="tab-popup">
                                            <tr>
                                                <td>Receipt No.
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label34" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Name</td>
                                                <td>
                                                    <asp:Label ID="Labelname" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Deposit Date</td>
                                                <td>
                                                    <asp:Label ID="LabelDepositDate" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">

                                                    <div class="table-responsive2 table-responsive">
                                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-head-border table-bordered" ShowFooter="True">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sr" runat="server" Text='<%# Bind("ids") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Head Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_headName" runat="server" Text='<%# Bind("HeadName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        Total
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_HeadAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="TotalHeadAmount0" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center;">
                                                    <asp:Button ID="Button4" runat="server" CssClass="button-y" OnClick="View_Click" Text="View" />
                                                    &nbsp; &nbsp;
                               <asp:Button ID="Button3" runat="server" CssClass="button-n" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">

                                                <table class="tab-popup">
                                                    <tr>
                                                        <td>Receipt No.
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label37" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td>S.R. No.</td>
                                                        <td>
                                                            <asp:Label ID="Label44" runat="server" Text="Label"></asp:Label>
                                                            <asp:Label ID="Label10" runat="server" Text="Label" Visible="false"></asp:Label>
                                                            <asp:Label ID="Label11" runat="server" Text="Label" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Name</td>
                                                        <td>
                                                            <asp:Label ID="Label38" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td>Date</td>
                                                        <td>
                                                            <asp:Label ID="Label39" runat="server" Text="Label" Visible="False"></asp:Label>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="DDYear0" runat="server" AutoPostBack="True"
                                                                        CssClass="form-control-blue col-xs-4" OnSelectedIndexChanged="DDYear_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="DDMonth0" runat="server" AutoPostBack="True"
                                                                        CssClass="form-control-blue col-xs-4" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="DDDate0" runat="server" AutoPostBack="True"
                                                                        CssClass="form-control-blue col-xs-4">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="4">
                                                            <div class="table-responsive2 table-responsive">
                                                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="table table-striped table-hover no-head-border table-bordered" ShowFooter="True">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label40" runat="server" Text='<%# Bind("Ids") %>'></asp:Label>
                                                                                <asp:Label ID="Label45" runat="server" Text='<%# Bind("Id") %>' Visible="False"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Head Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label41" runat="server" Visible="False"></asp:Label>
                                                                                <asp:Label ID="Label42" runat="server" Text='<%# Bind("HeadName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="Label43" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label9" runat="server" Text="" Visible="false"></asp:Label>
                                                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnTextChanged="TextBox2_TextChanged">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tab-in" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Exemption">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtConcession0" CssClass="form-control-blue" runat="server" AutoPostBack="True" OnTextChanged="txtConcession0_TextChanged" Enabled="False"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTotalConcession0" runat="server" Text='<%# Bind("Concession") %>'></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tab-in" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Due">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRecevied0" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTotalReceived0" runat="server" Text='<%# Bind("ReceivedAmount") %>'></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" class="text-center">
                                                            <asp:Button ID="Button5" runat="server" CssClass="button-y" OnClick="Button5_Click" Text="Update" />
                                                            &nbsp; &nbsp;
                                       <asp:Button ID="Button6" runat="server" CssClass="button-n" Text="Cancel" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>

                                        </asp:Panel>

                                        <div style="overflow: auto; width: 1px; height: 1px">

                                            <asp:Panel ID="Panel4" runat="server" CssClass="popup animated2 fadeInDown">
                                                <table class="tab-popup">

                                                    <tr>
                                                        <td style="text-align: center;">
                                                            <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                                <asp:Button ID="Button8" runat="server" Style="display: none" />
                                                            </h4>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="text-align: center;">
                                                            <asp:Button ID="Button9" runat="server" CssClass="button-n" CausesValidation="False" Text="No" />
                                                            &nbsp;&nbsp;
                                        <asp:Button ID="btnDelete" runat="server" CssClass="button-y" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <br />
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>


                </div>
            </div>



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
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function TextPayChange() {

            var Amount = parseFloat($("[id*=LblHeadTotalAmount]").html());
            var ChequeFine = parseFloat($("[id*=lblChequeFine]").html());
            var Discount = parseFloat($("[id*=txtDiscount]").val());
            var Payable = (Amount + ChequeFine) - Discount;
            var Pay = parseFloat($("[id*=TextPay]").val());
            if (Pay < Payable)
            {
                $("#tr_BalAmt").removeClass('hide');
                var NextDueAmt = (Payable - Pay);
                $("[id*=lblNextDueAmt]").html(NextDueAmt.toFixed(2));
            }
            else
            {
                $("[id*=TextPay]").val("0.00");
                $("[id*=lblNextDueAmt]").html("0.00");
                $("#tr_BalAmt").addClass('hide');
            }
        }
        function DiscountChange() {
            var Amount = parseFloat($("[id*=LblHeadTotalAmount]").html());
            var ChequeFine = parseFloat($("[id*=lblChequeFine]").html());
            var Discount = parseFloat($("[id*=txtDiscount]").val());
            var Payable = (Amount + ChequeFine) - Discount;
            $("[id*=txtPayable]").html((Amount + ChequeFine).toFixed(2));
            $("[id*=TextPay]").val(Payable.toFixed(2));
            $("[id*=lblNextDueAmt]").html("0.00");
        }
    </script>
</asp:Content>

