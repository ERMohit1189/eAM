<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LeaveApplicationForm.aspx.cs" Inherits="staff_LeaveApplicationForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <script type="text/javascript">
        function getEmployeeList() {
            $(function () {
                $("[id$=txtEnter]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetEmployeeForCode") %>',
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
                        $("[id$=hfEmployeeId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getEmployeeList);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">

                                    <div class="col-sm-4 half-width-50 mgbt-xs-15 select-list-hide display-none">

                                        <asp:DropDownList ID="drpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter Emp. Id/Username/Name</asp:ListItem>

                                        </asp:DropDownList>
                                        <i>H</i>
                                        <div class="text-box-msg">
                                        </div>

                                    </div>


                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <asp:TextBox ID="txtEnter" placeholder="Enter Emp. ID/Username/Name" runat="server" AutoPostBack="True" CssClass="form-control-blue txtbox"
                                            OnTextChanged="txtEnter_TextChanged" />
                                        <asp:HiddenField ID="hfEmployeeId" runat="server" />
                                        <div class="text-box-msg">
                                        </div>
                                    </div>


                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkShow" runat="server" CssClass="button" OnClick="lnkShow_Click">View</asp:LinkButton>
                                        <div id="Div1" runat="server" style="left: 75px;"></div>
                                    </div>


                                </div>
                                <div class="col-sm-12" runat="server" id="divempDetails" visible="false">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEcode" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp. Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesi" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="col-sm-12 no-padding" runat="server" id="divControls" visible="false">


                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">From Date&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="FromYY" runat="server" OnSelectedIndexChanged="FromYY_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromMM" runat="server" OnSelectedIndexChanged="FromMM_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromDD" runat="server" CssClass="form-control-blue col-xs-4" OnSelectedIndexChanged="FromDD_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ToYY" runat="server" OnSelectedIndexChanged="ToYY_SelectedIndexChanged" CssClass="form-control-blue col-xs-4"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToMM" runat="server" OnSelectedIndexChanged="ToMM_SelectedIndexChanged" CssClass="form-control-blue col-xs-4"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToDD" runat="server" CssClass="form-control-blue col-xs-4" OnSelectedIndexChanged="ToDD_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 no-padding">
                                        <div class="col-sm-12">
                                            <div class="table-responsive2 table-responsive">
                                                <asp:GridView ID="GridGenrate" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="date" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Abbreviation">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlAbbribation" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlAbbribation_SelectedIndexChanged">
                                                                    <asp:ListItem Value="L">Leave</asp:ListItem>
                                                                    <asp:ListItem Value="HD">Half Day</asp:ListItem>
                                                                    <asp:ListItem Value="SL">Short Leave</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Leave Type">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlLaevetype" runat="server" CssClass="form-control-blue">
                                                                    <asp:ListItem Value="F">Full Day</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 half-width-50 mgbt-xs-9">
                                            <label class="control-label">Reason&nbsp;<span class="vd_red">* </span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtReason" CssClass="form-control-blue validatetxt" TextMode="MultiLine" Rows="1" runat="server"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                            <label class="control-label">Contact No. 1&nbsp;<span class="vd_red">* </span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtContactno1" CssClass="form-control-blue validatetxt" MaxLength="10" runat="server" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                            <label class="control-label">Contact No. 2</label>
                                            <div class="">
                                                <asp:TextBox ID="txtContactno2" CssClass="form-control-blue" runat="server" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 half-width-50 mgbt-xs-9">
                                            <label class="control-label">Address</label>
                                            <div class="">
                                                <asp:TextBox ID="txtAddress" CssClass="form-control-blue" runat="server" Rows="1" TextMode="MultiLine"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>



                                        <div class="col-sm-4 half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkButton1_Click">Submit</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 75px;"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12" runat="server" id="divLeaveList" visible="false">
                                    <div class="table-responsive2 table-responsive">
                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                            <asp:Repeater runat="server" ID="rpt">

                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="background:#393a3e !important; color:#fff; font-weight:bold;">
                                                            <asp:Label ID="Label1dd" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="LeavGroup" runat="server" Visible="false" Text='<%# Bind("LeavGroup") %>'></asp:Label>
                                                        </td>
                                                        <td style="background:#393a3e !important; color:#fff; font-weight:bold;">
                                                            <asp:Label ID="lblEcode" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                        </td>
                                                        <td style="background:#393a3e !important; color:#fff; font-weight:bold;">
                                                            <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                        </td>
                                                        <td style="background:#393a3e !important; color:#fff; font-weight:bold;">
                                                            <asp:Label ID="Contact1" runat="server" Text='<%# Bind("Contact1") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="50px" />
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Date" runat="server" Text='<%# Bind("ApplicationDate", "{0: dd-MMM-yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Abbreviation">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Abbribation" runat="server" Text='<%# Bind("Abbribation") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Laeve Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Laevetype" runat="server" Text='<%# Bind("Laevetype") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Reason">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Reason" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label><br />
                                                                            <asp:Label ID="HrReason" runat="server" ForeColor="Red" Text='<%# Bind("HrReason") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <br />
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </table>
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

