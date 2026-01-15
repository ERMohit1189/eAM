<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="additionalFeesDeposit.aspx.cs" Inherits="additionalFeesDeposit" %>

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
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-4 mgbt-xs-15">
                                        <div class="">
                                            <asp:TextBox ID="TxtEnter" placeholder="Enter Name/ S.R. No." runat="server" 
                                                CssClass="form-control-blue validatetxt" OnTextChanged="TxtEnter_TextChanged" AutoPostBack="True"
                                                  onblur="javascript:__doPostBack('ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolderMainBox$LinkButton2','')"></asp:TextBox>
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

                                <div class="col-sm-12 ">
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
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                                                                    <asp:Label ID="Label23" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
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
                                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                    (<asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>)
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
                                                <td class="tab-top tab-profile text-center ">
                                                    <div>
                                                        <div class="gallery-item fee-pic-box">
                                                            <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                                <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Width="48px" Height="60px" />
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



                                <div class="col-sm-12  no-padding" id="Tables" runat="server">
                                    <div class="col-sm-12  no-padding">

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
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

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <asp:Label ID="Label16" runat="server" class="control-label" Text="Date"></asp:Label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged" Enabled="false"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged" Enabled="false"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDDate" runat="server" AutoPostBack="True" Enabled="false" CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="table2" runat="server" visible="false">
                                            <asp:Label ID="Label42" class="control-label" runat="server"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                         <div class="col-sm-4 half-width-50 mgbt-xs-15" id="chkdt" runat="server" visible="false">
                                        <asp:Label ID="lblChqDate" runat="server" class="control-label"></asp:Label>
                                        <div class="">
                                            <asp:TextBox runat="server" ID="txtChequeDate" CssClass="datepicker-normal"></asp:TextBox>
                                        </div>
                                    </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="table12" runat="server" visible="false">
                                            <asp:Label ID="Label43" runat="server" class="control-label" Text="Bank Name"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15" id="table12Status" runat="server" visible="false">
                                        <asp:Label ID="lblStatus" runat="server" class="control-label" Text="Status"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="drpStatus" runat="server">
                                                <asp:ListItem>Paid</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    </div>

                                    <div class="col-sm-6 col-xs-6">
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
                                                            <asp:TemplateField HeaderText="Particulars">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("Id") %>' Visible="False"></asp:Label>
                                                                    <asp:Label ID="Label32" runat="server" Text='<%# Bind("HeadName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    Total
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="380px" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="TextBox1" runat="server" Text='<%# Bind("Amount") %>' ></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label33" runat="server" Text=""></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Exemption">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TextBox3" Text='<%# Bind("Concession") %>' CssClass="form-control-blue" runat="server" Enabled="false" AutoPostBack="True" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalConcession" runat="server" Text=""></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="150px" />
                                                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Due">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRecevied" runat="server" Text='<%# Bind("Amount") %>' ></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalReceived" runat="server" Text=""></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Action">
                                                                 <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged"></asp:CheckBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="150px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-md-6 col-xs-6 ui-tabs-panel text-center" runat="server" id="divBtn" visible="false">
                                        <div class="col-md-4 col-xs-4 ui-tabs-panel text-center"></div>
                                        <div class="col-md-4 col-xs-4 ui-tabs-panel text-center" style="background: #e0e0e0; padding:20px;">
                                            <div class="text-center">
                                                Payable Amount : 
                                                <asp:Label ID="TotalPayableAmount" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                                <br></br>
                                            </div>
                                            <div class="text-center" id="divBounce" runat="server" visible="false">
                                                Bounce Charges : 
                                                <asp:Label ID="BounceCharges" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                                <br></br>
                                                Total : 
                                                <asp:Label ID="totalAmt" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="text-center">
                                                <asp:LinkButton ID="Button1" OnClick="Button1_Click" CssClass="button form-control-blue" runat="server">   Submit  </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12 " runat="server" id="divReport" visible="false">

                                    <div class="col-sm-12  no-padding">
                                        <br /><br />
                                       <asp:Label ID="Label4" runat="server" class="control-label" Text="Payment History"></asp:Label>
                                        <div class=" table-responsive">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group  ">
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="40px" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Receipt No.">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="ReceiptNo" runat="server" OnClick="LinkButton6_Click" Style="font-weight: 700" Text='<%# Bind("Receipt_no") %>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="DepositDate" runat="server" Style="font-weight: 700" Text='<%# Bind("Date") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Mode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Mode" runat="server" Style="font-weight: 700" Text='<%# Bind("Mode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Status" runat="server" Style="font-weight: 700" Text='<%# Bind("Status") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="Label2f" runat="server" Style="font-weight: bold" Text="Total"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="amount" runat="server" Style="font-weight: 700" Text='<%# Bind("amount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="Footeramount" runat="server" Style="font-weight: bold"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Fine">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="BounceChargess" runat="server" Style="font-weight: 700" Text='<%# Bind("BounceCharges") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="FooterBounceCharges" runat="server" Style="font-weight: bold"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Exemption">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Exemption" runat="server" Style="font-weight: 700" Text='<%# Bind("Concession") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="FooterExemption" runat="server" Style="font-weight: bold"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="total" runat="server" Style="font-weight: 700" Text='<%# Bind("total") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="Footertotal" runat="server" Style="font-weight: bold"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Paid Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ReceivedAmount" runat="server" Style="font-weight: 700" Text='<%# Bind("paid") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="FooterReceivedAmount" runat="server" Style="font-weight: bold"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
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
            </div>





            <asp:LinkButton ID="Button2" runat="server" Style="display: none"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="model" runat="server" BackgroundCssClass="popup_bg"
                TargetControlID="Button2" PopupControlID="Panel1">
            </ajaxToolkit:ModalPopupExtender>
            <asp:LinkButton ID="Button7" runat="server" Style="display: none"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="popup_bg"
                TargetControlID="Button7" PopupControlID="Panel3">
            </ajaxToolkit:ModalPopupExtender>
            <asp:LinkButton ID="Button10" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="popup_bg"
                TargetControlID="Button10" PopupControlID="Panel4">
            </ajaxToolkit:ModalPopupExtender>



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
                            <td>Student's Name</td>
                            <td>
                                <asp:Label ID="Label35" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Date</td>
                            <td>
                                <asp:Label ID="Label36" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="table-responsive2 table-responsive">
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-head-border table-bordered">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sr" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Particulars">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_headName" runat="server" Text='<%# Bind("HeadName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="Button4" runat="server" CssClass="button-y" OnClick="Button4_Click" Text="View" />
                                &nbsp; &nbsp;
                               <asp:Button ID="Button3" runat="server" CssClass="button-n" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup">
                        <tr>
                            <td>Receipt No.
                            </td>
                            <td>
                                <asp:Label ID="Label37" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Student's Name</td>
                            <td>
                                <asp:Label ID="Label38" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>S.R. No.</td>
                            <td>
                                <asp:Label ID="Label44" runat="server" Text="Label"></asp:Label>
                                <asp:Label ID="Label10" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="Label11" runat="server" Text="Label" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Date</td>
                            <td>
                                <asp:Label ID="Label39" runat="server" Text="Label" Visible="False"></asp:Label>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDYear0" runat="server" AutoPostBack="True"
                                            CssClass="form-control-blue col-xs-4" OnSelectedIndexChanged="DDYear0_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="DDMonth0" runat="server" AutoPostBack="True"
                                            CssClass="form-control-blue col-xs-4" OnSelectedIndexChanged="DDMonth0_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="DDDate0" runat="server" AutoPostBack="True"
                                            CssClass="form-control-blue col-xs-4">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">
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
                                            <asp:TemplateField HeaderText="Particulars">
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
                            <td colspan="2" class="text-center">
                                <asp:Button ID="Button5" runat="server" CssClass="button-y" OnClick="Button5_Click" Text="Update" />
                                &nbsp; &nbsp;
                                       <asp:Button ID="Button6" runat="server" CssClass="button-n" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
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

