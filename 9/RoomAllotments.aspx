<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="RoomAllotments.aspx.cs"
    Inherits="admin_RoomAllotments" %>

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
                                    <div class="col-sm-12 ">
                                        <asp:RadioButtonList runat="server" ID="rdoType" RepeatDirection="Horizontal" CssClass="rdoType hide" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" AutoPostBack="true">
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
                                            <div id="msgbox" runat="server" style="left: 85px;"></div>
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

                                <div class="col-sm-12 no-padding" runat="server" id="div4" visible="false">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Category <span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DrpCategory" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Building <span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DrpbuildingLocation" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="DrpbuildingLocation_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Room Type <span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DrpRoomType" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="DrpRoomType_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label" id="lblRoomNo" runat="server">Room No. <span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DrpRoomNo" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="DrpRoomNo_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div runat="server" id="div5" visible="true">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Bed No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblBedId" Text='<%# Bind("id") %>' CssClass="hide"></asp:Label>
                                                        <asp:Label runat="server" ID="txtBedNo" Text='<%# Bind("BedNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bed Charges">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="txtPrice" Text='<%# Bind("BedCharge") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bed Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="txtBedStatusDamaged" CssClass="label label-danger" Visible="false"><i class="fa fa-close"></i> Damaged</asp:Label>
                                                        <asp:Label runat="server" ID="txtBedStatusOk" CssClass="label label-success" Visible="false"><i class="fa fa-check"></i> OK</asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Booking Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="txtBookingStatus" CssClass="label label-warning" Visible="false"><i class="fa fa-check"></i> Occupied</asp:Label>
                                                        <asp:Label runat="server" ID="txtBookingUnavailable" CssClass="label label-danger" Visible="false"><i class="fa fa-close"></i> Unavailable</asp:Label>
                                                        <asp:CheckBox runat="server" ID="chkFree" Text="Vacant" Visible="false" CssClass="chk text-success" OnCheckedChanged="chkFree_CheckedChanged" AutoPostBack="true" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Type" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="txtPaymentType" Text='<%# Bind("PaymentType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtremark" Text='<%# Bind("remark") %>' ReadOnly="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                    </div>
                                    <div class="col-sm-12 no-padding">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divdateFrom" runat="server" visible="false">
                                            <label class="control-label">From</label>
                                            <div class="">
                                                <div class="col-sm-12  half-width-50 mgbt-xs-15 no-padding">
                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DDYearFrom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYearFrom_SelectedIndexChanged"
                                                                CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDMonthFrom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonthFrom_SelectedIndexChanged"
                                                                CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDDateFrom" runat="server" CssClass="form-control-blue col-xs-4" OnSelectedIndexChanged="DDDateFrom_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divdateTo" runat="server" visible="false">
                                            <label class="control-label">To</label>
                                            <div class="">
                                                <div class="col-sm-12  half-width-50 mgbt-xs-15 no-padding">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DDYearTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYearTo_SelectedIndexChanged"
                                                                CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDMonthTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonthTo_SelectedIndexChanged"
                                                                CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DDDateTo" runat="server" CssClass="form-control-blue col-xs-4"  OnSelectedIndexChanged="DDDateTo_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2  half-width-50 mgbt-xs-15 hide" id="divFrequencyofPayment" runat="server" visible="false">
                                            <label class="control-label">Frequency of Payment</label>
                                            <div class="">
                                                <asp:TextBox runat="server" ID="lblPaymentType" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                <asp:DropDownList ID="drpFrequencyofPayment" runat="server" CssClass="form-control-blue hide" Enabled="false">
                                                    <asp:ListItem Value="OneTime">One Time</asp:ListItem>
                                                    <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4" style="padding-top: 32px;">
                                         <asp:Label runat="server" CssClass="label label-success" style="font-size: 100%;">Total Months <i class="fa fa-calendar"></i>&nbsp;&nbsp;<asp:Label runat="server" ID="lblMonths">0</asp:Label></asp:Label>
                                         <asp:Label runat="server" CssClass="label label-success"  style="font-size: 100%;">Payable Amount <i class="fa fa-inr"></i>&nbsp;<asp:Label runat="server" ID="LblPaybleAmount">0</asp:Label></asp:Label>
                                            
                                     </div>
                                    
                                    </div>

                                     
                                </div>
                                 <div class="col-sm-12">
                                     <asp:HiddenField runat="server" ID="hdnPaybleAmount" />
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Bed No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="txtBedNos" Text='<%# Bind("allotedRoom") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Duration">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Durationf" Text='<%# Bind("DateFrom", "{0:dd-MMM-yyyy}") %>'></asp:Label> <span>-To-</span>
                                                        <asp:Label runat="server" ID="Durationt" Text='<%# Bind("DateTo", "{0:dd-MMM-yyyy}") %>'></asp:Label><br />
                                                        <label style="color:#FF0000;">(<asp:Label runat="server" ID="Label2" Text='<%# Bind("TotalMonths") %>'></asp:Label> Months)</label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bed Charges">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="txtPrices" Text='<%# Bind("TotalAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Booking Status">
                                                    <ItemTemplate>
                                                       <asp:Label runat="server" ID="txtBookings" CssClass='<%# Eval("BookedStatus").ToString()=="True"?"label label-warning":"label label-success" %>' Text='<%# Eval("BookedStatus").ToString()=="True"?"Occupied":"Vacant" %>'></asp:Label>                                                   
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Type" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="txtPaymentType" Text='<%# Bind("PaymentType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>
                                     <asp:UpdatePanel runat="server" ID="ff">
                                         <ContentTemplate>

                                         
                                     <table class="table table-striped table-hover  no-bm no-head-border table-bordered text-center" id="tbls">
                                     <asp:Repeater ID="rptFeeStructure" runat="server">
                                         <HeaderTemplate>
                                             <tr>
                                                 <th style="width: 36px;">#</th>
                                                 <th class="text-left">Installment</th>
                                                 <th class="text-right"><asp:CheckBox runat="server" ID="chkFill" OnCheckedChanged="chkFill_CheckedChanged" AutoPostBack="true" /> Installment Fee</th>
                                                 <th class="text-center" style="width: 50px;">Delete</th>
                                                 </tr>
                                         </HeaderTemplate>
                                         <ItemTemplate>
                                             <tr id="tr1" runat="server">
                                                 <td class="text-center" style="height: 34px; width: 36px;">
                                                     <p style="margin: 0px; width: 16px;">
                                                         <asp:Label ID="lblSr" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                     </p>
                                                 </td>
                                                 <td class="text-left">
                                                     <p style="margin: 0px; width: 170px; padding-left: 25px;">
                                                         <asp:Label ID="lblInstallmentId" runat="server" Text='<%# Bind("InstallmentId") %>' Visible="false"></asp:Label>
                                                         <asp:Label ID="lblInstallment" runat="server" CssClass="position-r" Text='<%# Bind("Installment") %>'></asp:Label>
                                                     </p>
                                                 </td>
                                                 <td class="text-right">
                                                     <asp:TextBox ID="txtAmount" runat="server" onblur="calculateAmt(this)" CssClass="text-right" Text='<%# Eval("Amount").ToString()==string.Empty?"0.00":Eval("Amount") %>'></asp:TextBox></td>
                                                 <td class="text-right">
                                                     <asp:Label ID="lblids" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" Visible="false"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                 </td>
                                             </tr>
                                         </ItemTemplate>
                                        
                                     </asp:Repeater>
                                         <tr>
                                             <td colspan="3" class="text-right"><asp:Label ID="txtAmountTotal" visible="false" runat="server" Text="0.00"></asp:Label></div></colspan="">
                                             <td></td>
                                         </tr>
                                         </table>
                                             </ContentTemplate>
                                     </asp:UpdatePanel>
                                    <div class="text-right" runat="server" id="divAmountTotal" visible="false"> 
                                                     
                                     <div class="col-sm-12 text-center"  style="padding-top: 26px;">
                                        <asp:LinkButton ID="LinkSubmit" runat="server" CssClass="button form-control-blue" OnClick="LinkSubmit_Click" Visible="false"> Submit</asp:LinkButton>
                                        
                                    </div>
                            </div>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup text-center">

                        <tr>
                            <td><h4>Are you sure you want to delete this?<asp:Label ID="lblvalue"
                                runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="Button7" runat="server" Style="display: none" />
                                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server"
                                    CancelControlID="Button8" DynamicServicePath="" Enabled="True"
                                    PopupControlID="Panel2" TargetControlID="Button7">
                                </asp:ModalPopupExtender></h4>
                            </td>
                        </tr>

                        <tr>
                            <td >
                                <asp:Button ID="Button8" CssClass="button-n" runat="server" CausesValidation="False"
                                    OnClick="Button8_Click" Text="No" />
                                &nbsp;&nbsp; 
                                <asp:Button ID="btnDelete" runat="server" CssClass="button-y" CausesValidation="False"
                                    OnClick="btnDelete_Click" Text="Yes" />


                            </td>
                        </tr>

                    </table>

                </asp:Panel>
            </div>
            <script>
                function calculateAmt(tis) {
                    decimalOrNumeric(tis);
                    var len = $('#tbls tbody tr').length;
                    var total = 0;
                    for (var i = 1; i < len; i++) {
                        var amt = $('#tbls tbody tr:eq(' + i + ') td:eq(2) input[type=text]').val();
                        total = total + parseFloat(amt==""?"0":amt);
                    }
                    var payable = parseFloat($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_LblPaybleAmount").html());
                    if (payable < total) {
                        alert("Sorry, Please enter total instalments amount equal to payble amount!");
                        total = 0;
                        $(tis).val("0.00");
                        for (var i = 1; i < len; i++) {
                            var amt = $('#tbls tbody tr:eq(' + i + ') td:eq(2) input[type=text]').val();
                            total = total + parseFloat(amt == "" ? "0" : amt);
                        }
                    }
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtAmountTotal").html(total.toFixed(2));
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
