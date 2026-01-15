<%@ Page Title="" Language="C#" MasterPageFile="~/sp/sp_root-manager.master" AutoEventWireup="true" CodeFile="HostelFeePayment.aspx.cs"
    Inherits="HostelFeePayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery-1.4.3.min.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="https://js.paystack.co/v1/inline.js"></script>
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
    <style>
        .rdoType tbody tr td {
            width: 90px;
        }

            .rdoType tbody tr td label {
                vertical-align: bottom;
                margin-left: 3px;
            }

        .chk label {
            vertical-align: bottom;
            margin-left: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <%--Content starts--%>
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                try {
                    Sys.Application.add_load(getStudentsList);
                    Sys.Application.add_load(getStaffList);
                }
                catch (ex) {

                }
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" id="mainDiv" runat="server" visible="false">
                                    <div class="col-sm-12 ">
                                        <asp:RadioButtonList runat="server" ID="rdoType" RepeatDirection="Horizontal" CssClass="rdoType" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="Student" Selected="True">Student</asp:ListItem>
                                            <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-6 col-xs-6 no-padding">
                                        <div class="col-sm-5 col-xs-5 half-width-50 mgbt-xs-15" id="divStudent" runat="server">
                                            <asp:TextBox ID="txtSearchStudent" placeholder="Enter Name/ S.R. No." runat="server" AutoPostBack="True" CssClass="form-control-blue"
                                                OnTextChanged="txtSearchStudent_TextChanged" onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                        </div>
                                        <div class="col-sm-5 col-xs-5 half-width-50 mgbt-xs-15" id="divStaff" runat="server" visible="false">
                                            <asp:TextBox ID="txtSearchStaff" placeholder="Enter Name/ EmpId/ Empcode" runat="server" AutoPostBack="True" CssClass="form-control-blue"
                                                OnTextChanged="txtSearchStaff_TextChanged" onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
                                            <asp:HiddenField ID="hfStaffId" runat="server" />
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

                                        <div class="col-sm-2 col-xs-2 half-width-50 mgbt-xs-15">

                                            <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" CssClass="button form-control-blue"> View</asp:LinkButton>
                                            <asp:Label ID="lblFee" runat="server" Style="color: #FF0000"></asp:Label>

                                        </div>
                                        <div class="col-sm-12 ">
                                            <div id="msgbox0" runat="server"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <table style="width: 100%;" id="div1" runat="server" visible="false">
                                            <tr>
                                                <td class="tab-top">
                                                    <asp:GridView ID="grdStRecord" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                        class="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
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
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>' CssClass="hide"></asp:Label>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>' CssClass="hide"></asp:Label>
                                                                    <asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>' CssClass="hide"></asp:Label>
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
                                                            <asp:TemplateField HeaderText="Fee Group" Visible="False">
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
                                                        <HeaderStyle />
                                                    </asp:GridView>

                                                </td>
                                                <td class="tab-top tab-profile text-center" runat="server" id="div3">
                                                    <div class="gallery-item fee-pic-box">
                                                        <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                            <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                        </asp:HyperLink>
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" NavigateUrl="" Target="_blank" Text="more..."></asp:HyperLink>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td rowspan="2">
                                                    <div id="divmsgrecord" runat="server" style="left: 60px;"></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div class="col-sm-12 " id="div2" runat="server">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40px" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEcode" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Emp Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Father Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesi" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>


                                <div class="col-sm-12">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bed No.">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblbedid" Text='<%# Bind("bedid") %>' CssClass="hide"></asp:Label>
                                                    <asp:Label runat="server" ID="lblRoomAllotmentId" Text='<%# Bind("RoomAllotmentId") %>' CssClass="hide"></asp:Label>
                                                    <asp:Label runat="server" ID="txtBedNo" Text='<%# Bind("allotedRoom") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mode">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtMode" Text='<%# Bind("FrequencyofPayment") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Charges">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtPrice" Text='<%# Bind("BedCharge") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtFrom" Text='<%# Bind("DateFrom", "{0:dd MMM yyyy}") %>'></asp:Label><br />
                                                    To<br />
                                                    <asp:Label runat="server" ID="txttO" Text='<%# Bind("DateTo", "{0:dd MMM yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Months">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtMonths" Text='<%# Bind("TotalMonths") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtTotal" Text='<%# Bind("TotalAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtremark" Text='<%# Bind("remark") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="270px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Release">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtBookingStatus" CssClass="label label-success" Visible="false"><i class="fa fa-check"></i> Booked</asp:Label>
                                                    <asp:Label runat="server" ID="txtBookingUnavailable" CssClass="label label-warning" Visible="false"><i class="fa fa-close"></i> Released</asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                </div>

                                <div class="col-sm-12">
                                    <asp:GridView ID="GridOneTime" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblbedidO" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receipt No">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnView" runat="server" Text='<%# Bind("ReceiptNo") %>' OnClick="btnView_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbldate" Text='<%# Bind("DepositDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mode">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblModeO" Text='<%# Bind("MonthName") %>'></asp:Label>
                                                    (<asp:Label runat="server" ID="payMode" Text='<%# Bind("PaymentMode") %>'></asp:Label>)
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblAmountO" Text='<%# Bind("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fine">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblFineO" Text='<%# Bind("Fine") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Exemption">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblExemptionO" Text='<%# Bind("Exemption") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTotalO" Text='<%# Bind("Total") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Paid">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblPaidO" Text='<%# Bind("Paid") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Due">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblDueO" Text='<%# Bind("NextDue") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <div id="div4" runat="server" class="col-sm-12  no-padding" visible="false" style="margin-top: 20px;">

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15  no-padding" id="divdate" runat="server">
                                            <label class="control-label">Date</label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDDate" runat="server" CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divMod" runat="server">
                                            <label class="control-label">Mode</label>
                                            <div class="">
                                                <asp:DropDownList ID="DropDownMOD" runat="server" AutoPostBack="True" Enabled="false"
                                                    TabIndex="1" CssClass="form-control-blue " OnSelectedIndexChanged="DropDownMOD_SelectedIndexChanged">
                                                    <asp:ListItem>Cash</asp:ListItem>
                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                    <asp:ListItem>DD</asp:ListItem>
                                                    <asp:ListItem Selected="True">Online</asp:ListItem>
                                                    <asp:ListItem>Card</asp:ListItem>
                                                    <asp:ListItem>Bank Deposit</asp:ListItem>
                                                    <asp:ListItem>NEFT/RTGS</asp:ListItem>
                                                    <asp:ListItem>Other</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="div9" runat="server" visible="false">
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

                                    <div class="col-sm-12  no-padding" id="div5" runat="server" visible="false">

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15 no-padding" id="div8" runat="server">
                                            <asp:Label ID="Label54" runat="server" class="control-label"></asp:Label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="DDChkYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDChkYear_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDChkMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDChkMonth_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDChkDate" runat="server" CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>



                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <asp:Label ID="Label42" runat="server" class="control-label"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="txtChequeNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <asp:Label ID="lblBankName" class="control-label" runat="server" Text="Bank Name"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="txtbankName" runat="server" CssClass="form-control-blue" Text="NA"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 no-padding" runat="server" id="oneTimediv" visible="false">
                                        <div class="col-sm-2" style="padding-top: 30px;">
                                            <asp:Label ID="Label7" class="control-label" runat="server" Text="Total : "></asp:Label>
                                            <asp:Label ID="lbltotal" class="control-label" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label3" class="control-label" runat="server" Text="Fine"></asp:Label>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtFineO" OnTextChanged="lblFineO_TextChanged" onkeypress="return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-2 no-padding">
                                            <asp:Label ID="Label5" class="control-label" runat="server" Text="Exemption"></asp:Label>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtExemptionO" onkeypress="return isNumber(event)" OnTextChanged="txtExemptionO_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                         </ContentTemplate>
                                                </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-2 no-padding">
                                            <asp:Label ID="Label2" class="control-label" runat="server" Text="Payble"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdnPayble" />
                                            <asp:TextBox runat="server" ID="txtPayble" onkeypress="return isNumber(event)" OnTextChanged="txtPayble_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2" style="padding-top: 32px;">
                                            <asp:Label runat="server" class="control-label" ID="lblNextdues">Next Due :
                                                <asp:Label runat="server" ID="lblNextDue">0</asp:Label></asp:Label>
                                        </div>
                                        <div class="col-sm-2 no-padding" style="padding-top: 26px !important;">
                                            <asp:Button runat="server" ID="btnOneTime" CssClass="button form-control-blue" Text="Submit" />
                                        </div>
                                    </div>

                                </div>
                                <div class="col-sm-12">
                                    <asp:GridView ID="GridMonthly" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblbedid" Text='<%# Container.DataItemIndex+1 %>' CssClass="hide"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Months">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblMode"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblAmount"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fine">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblFine"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Exemption">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtExemption"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTotal"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Paid">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblPaid"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Due">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblDue"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="display: none; visibility: hidden">
                <asp:Label runat="server" ID="data_key"></asp:Label>
                <asp:Label runat="server" ID="data_email"></asp:Label>
                <asp:Label runat="server" ID="data_PseudoUniqueReference"></asp:Label>
                <asp:Label runat="server" ID="data_txnNo"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnTxtNos" />
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnPayStack" runat="server" OnClick="btnPayStack_Click" Text="Submit"></asp:Button>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnPayStack" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <script type="text/javascript" language="javascript">
                $("body").on("click", "#<%=btnOneTime.ClientID %>", function () {
                     payWithPaystack();
                 });
            </script>
            <script>
                function payWithPaystack() {
                    var data_key = $("[id*=data_key]").html();
                    var data_email = $("[id*=data_email]").html();
                    var data_amount = (parseFloat($("[id*=txtPayble]").val()) * 100);
                    var data_full_name = $("[id*=grdStRecord] tr:eq(1)").find("td:eq(1) span").html();
                    var data_name = data_full_name.split(" ");
                    var data_firstname = data_name[0];
                    var data_lastname = data_name[1];
                    var data_value = $("[id*=grdStRecord] tr:eq(1)").find("td:eq(6) span").html();
                    var data_PseudoUniqueReference = parseInt($("[id*=data_PseudoUniqueReference]").html());
                    //alert(data_key + " | " + data_email + " | " + data_amount + " | " + data_full_name + " | " + data_firstname + " | " + data_lastname + " | " + data_value + " | " + data_PseudoUniqueReference);

                    var handler = PaystackPop.setup({
                        key: data_key,
                        email: data_email,
                        amount: data_amount,
                        ref: '' + Math.floor((Math.random() * data_PseudoUniqueReference) + 1), // generates a pseudo-unique reference. Please replace with a reference you generated. Or remove the line entirely so our API will generate one for you
                        firstname: data_firstname,
                        lastname: data_lastname,
                        // label: "Optional string that replaces customer email"
                        metadata: {
                            custom_fields: [
                            {
                                display_name: "Mobile Number",
                                variable_name: "mobile_number",
                                value: data_value
                            }
                            ]
                        },
                        callback: function (response) {
                            $("[id*=hdnTxtNos]").val(response.reference);
                            $("#<%=btnPayStack.ClientID %>").click();
                    //$("[id*=btnPayStack]").click();
                },
                onClose: function () {
                    alert('window closed');
                }
            });
            handler.openIframe();
        }
            </script>
            <script>
                function isNumber(evt) {
                    evt = (evt) ? evt : window.event;
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    return true;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
