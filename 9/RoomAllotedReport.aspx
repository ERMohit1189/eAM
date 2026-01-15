<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="RoomAllotedReport.aspx.cs"
    Inherits="RoomAllotedReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
                                <div class="col-sm-12 no-padding" runat="server">
                                    <div class="col-sm-3  hide">
                                        <asp:DropDownList runat="server" ID="rdoType" CssClass="rdoType" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="">All</asp:ListItem>
                                            <asp:ListItem Value="Student"  Selected="True">Student</asp:ListItem>
                                            <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-6 col-xs-6 no-padding">
                                        <div class="col-sm-5 col-xs-5 half-width-50 mgbt-xs-15" id="divStudent" runat="server" visible="false">
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
                                        <div class="col-sm-3 ">
                                        <div class="">
                                            <label class="label-control">Class</label>
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control-blue ">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                        <div class="col-sm-2 col-xs-2 half-width-50 mgbt-xs-15" style="padding-top:26px;">

                                            <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" CssClass="button form-control-blue"> View</asp:LinkButton>
                                            <asp:Label ID="lblFee" runat="server" Style="color: #FF0000"></asp:Label>

                                        </div>
                                        <div class="col-sm-12 ">
                                            <div id="msgbox0" runat="server"></div>
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
                                <div class="col-sm-12">
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
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="ddd" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S.R. No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Bind("SrNoOrEmpId") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Student's Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtName" Text='<%# Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtClass" Text='<%# Bind("Class") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bed No.">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblbedid" Text='<%# Bind("bedid") %>' CssClass="hide"></asp:Label>
                                                    <asp:Label runat="server" ID="lblRoomAllotmentId" Text='<%# Bind("RoomAllotmentId") %>' CssClass="hide"></asp:Label>
                                                    <asp:Label runat="server" ID="txtBedNo" Text='<%# Bind("allotedRoom") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Duration">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtFrom" Text='<%# Bind("DateFrom", "{0:dd MMM yyyy}") %>'></asp:Label>
                                                    -To-
                                                    <asp:Label runat="server" ID="txttO" Text='<%# Bind("DateTo", "{0:dd MMM yyyy}") %>'></asp:Label>
                                                    <br /><label style="color:#db0000;">(<asp:Label runat="server" ID="txtMonths" Text='<%# Bind("TotalMonths") %>'></asp:Label> Months)</label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="135px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtMode" Text='<%# Bind("PaymentType") %>'></asp:Label>
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
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtTotal" Text='<%# Bind("TotalAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remark" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtremark" Text='<%# Bind("remark") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="270px" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Booking Status">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtBookingStatus" CssClass="label label-success" Visible="false"><i class="fa fa-check"></i> Occupied</asp:Label>
                                                    <asp:Label runat="server" ID="txtBookingUnavailable" CssClass="label label-warning" Visible="false"><i class="fa fa-close"></i> Released</asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
