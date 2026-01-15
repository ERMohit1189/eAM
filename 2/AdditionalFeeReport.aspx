<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AdditionalFeeReport.aspx.cs" Inherits="AdditionalFeeReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
     <script src="../js/jquery.min.js"></script>
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
     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
    <div id="loader" runat="server"></div>
    <script>
        Sys.Application.add_load(getStudentsList);
        
    </script>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12  no-padding ">
                            
                            <div class="col-sm-4   mgbt-xs-15" id="divBranch" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                        <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-2   mgbt-xs-15" id="divSession" runat="server">
                                <label class="control-label">Session</label>
                                <div class="">
                                    <asp:DropDownList runat="server" ID="DrpSessionName"></asp:DropDownList>
                                </div>
                            </div>
                           
                            <div class="col-sm-3   mgbt-xs-15">
                                <label class="control-label">From Date</label>
                                <div class="">
                                    <asp:DropDownList ID="fromDDYears" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="fromDDYear_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="fromDDMonths" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="fromDDMonth_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="fromDDDates" runat="server" CssClass="form-control-blue col-sm-4">
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3   mgbt-xs-15">
                                <label class="control-label">To Date</label>
                                <div class="">
                                    <asp:DropDownList ID="toDDYears" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="toDDYearC_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="toDDMonths" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="toDDMonthC_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="toDDDates" runat="server" CssClass="form-control-blue col-sm-4">
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2   mgbt-xs-15">
                                <label class="control-label">Status</label>
                                <div class="">
                                    <asp:DropDownList ID="drpStatus" runat="server" class="form-control-blue ">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Paid</asp:ListItem>
                                        <asp:ListItem>Pending</asp:ListItem>
                                        <asp:ListItem>Cancelled</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2   mgbt-xs-15">
                                <label class="control-label">Mode of Payment</label>
                                <div class="">
                                    <asp:DropDownList ID="DdlpaymentMode" runat="server" CssClass="vd_radio radio-success">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>DD</asp:ListItem>
                                        <asp:ListItem>Card</asp:ListItem>
                                        <asp:ListItem>Online Transfer</asp:ListItem>
                                        <asp:ListItem>Other</asp:ListItem>
                                        <asp:ListItem Value="Online">Online</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2   mgbt-xs-15">
                                <label class="control-label">Select User&nbsp;<span class="vd_red"></span></label>
                                <div class=" ">
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue "></asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                           
                            <div class="col-sm-4">
                                <label class="control-label">Enter Name/ S.R. No.&nbsp;<span class="vd_red"></span></label>
                                <asp:TextBox ID="txtSearch" placeholder="Enter Name/ S.R. No." runat="server" CssClass="form-control-blue" onkeyup="onchangetxt();" 
                                onpaste="onchangeatcopyandpaste()" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" />
                                <asp:HiddenField ID="hfStudentId" runat="server" />
                            </div>
                            <script>
                                function onchangetxt() {
                                    if (document.getElementById('<%= txtSearch.ClientID %>').value.length === 0) {
                                        document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                    }
                                }
                                function onchangeatcopyandpaste() {
                                    document.getElementById('<%= hfStudentId.ClientID %>').value = "";
                                }
                            </script>
                            
                            <div class="col-sm-12   mgbt-xs-15">
                                <div class="" style="margin-top: 25px;">
                                  <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="button form-control-blue ">View</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12  mgbt-xs-10" runat="server" id="divExport" visible="false">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                    title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                    title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                    title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

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
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div id="gdv1" runat="server">
                                                <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table">
                                                    <tr>
                                                        <td>
                                                            <div id="header" runat="server" class="col-md-12 no-padding"></div>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                <asp:GridView ID="GrdDisplay" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group  ">
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
                                                                                <asp:Label ID="ReceiptNo" runat="server"  Text='<%# Bind("Receipt_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="DepositDate" runat="server"  Text='<%# Bind("Date") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S.R. No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="SrNo" runat="server"  Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Student's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Name" runat="server"  Text='<%# Bind("Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Father's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="FatherName" runat="server"  Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Contact No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="FatherContactNo" runat="server"  Text='<%# Bind("FatherContactNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Class">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="CombineClassName" runat="server"  Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Mode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Mode" runat="server"  Text='<%# Bind("Mode") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Status" runat="server"  Text='<%# Bind("Status") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="Label2f" runat="server" Style="font-weight: bold" Text="Total"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Fee Heads">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="amount" runat="server"  Text='<%# Bind("amount") %>'></asp:Label>
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
                                                                                <asp:Label ID="BounceCharges" runat="server"  Text='<%# Bind("BounceCharges") %>'></asp:Label>
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
                                                                                <asp:Label ID="Exemption" runat="server"  Text='<%# Bind("Concession") %>'></asp:Label>
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
                                                                                <asp:Label ID="total" runat="server"  Text='<%# Bind("total") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="Footertotal" runat="server" Style="font-weight: bold"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Paid">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ReceivedAmount" runat="server"  Text='<%# Bind("paid") %>'></asp:Label>
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
                                                        </td>
                                                    </tr>
                                                </table>
                                                    </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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

