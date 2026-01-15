<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmployeeAttendanceMonthWise.aspx.cs" Inherits="EmployeeAttendanceMonthWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script type="text/javascript">

        function getEmployeeList() {
            $(function () {
                $("[id$=txtHeaderEmpId]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetEmployee") %>',
                            data: "{ 'empId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d,
                                    function(item) {
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
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getEmployeeList);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12 ">
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15 no-padding">
                                        <asp:RadioButtonList ID="rbDepartmentwise" runat="server" AutoPostBack="True"
                                            RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_radio radio-success"
                                            OnSelectedIndexChanged="rbDepartmentwise_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">Departmentwise</asp:ListItem>
                                            <asp:ListItem>Employeewise</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 no-padding">
                                        <label class="control-label">Select Year and Month&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpDDEmpYY" runat="server" CssClass="form-control-blue col-sm-6">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDDEmpMM" runat="server" CssClass="form-control-blue col-sm-6">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDDEmpDD" runat="server" Visible="false" CssClass="form-control-blue col-sm-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divdepartment" runat="server">
                                        <label class="control-label">Department&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpDepartment" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DrpDepartment_SelectedIndexChanged" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divemp2" runat="server" visible="false">
                                        <label class="control-label">&nbsp;<span class="vd_Black"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtHeaderEmpId" placeholder="Enter Emp. ID /Emp. Code /Name" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnTextChanged="txtHeaderEmpId_TextChanged"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:HiddenField ID="hfEmployeeId" runat="server" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divemp3">
                                        <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="button" OnClick="lnkView_Click">View</asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15 no-padding" id="divemp4" runat="server">
                                         <label class="control-label">Set Attendance in All Dates<span class="vd_red"></span></label>
                                         <div class="">
                                             <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                 <ContentTemplate>
                                                        <asp:DropDownList ID="drpAttendancealldate" runat="server" Style="width: 200px;" AutoPostBack="true" 
                                                        OnSelectedIndexChanged="drpAttendancealldate_SelectedIndexChanged"></asp:DropDownList>
                                                 </ContentTemplate>
                                             </asp:UpdatePanel>
                                         </div>
                                     </div>
                               
                                    <div class=" table-responsive  table-responsive2">
                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <thead>
                                                <tr>
                                                    <asp:Repeater ID="rptHeader" runat="server" OnItemDataBound="rptHeader_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <th>#</th>
                                                           <%-- <th>Emp. ID
                                                            </th>--%>
                                                            <th>Name
                                                            </th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <th>
                                                                <asp:Label ID="lblMonthDate" runat="server" Text='<%# Eval("day") %>'></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lbldayname" runat="server" Text='<%# Eval("dayname") %>'></asp:Label>
                                                                <br />
                                                                <asp:DropDownList ID="drpAttendanceall" runat="server" Style="width: 60px;" AutoPostBack="true" 
                                                                    OnSelectedIndexChanged="drpAttendanceall_SelectedIndexChanged"></asp:DropDownList>
                                                            </th>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptEmp" runat="server" OnItemDataBound="rptEmp_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.ItemIndex+1 %></td>
                                                          <%--  <td>
                                                                <asp:Label ID="lblempid" runat="server" Text='<%# Eval("Empid") %>'></asp:Label>
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblempid" runat="server" Text='<%# Eval("Empid") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                                            </td>
                                                            <asp:Repeater ID="rptDays" runat="server" OnItemDataBound="rptDays_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <td>
                                                                        <asp:Label ID="lblMonthDate" runat="server" Text='<%# Eval("date") %>' Visible="false"></asp:Label>
                                                                        <asp:DropDownList ID="drpAttendancevalue" runat="server" Style="width: 60px;"></asp:DropDownList>
                                                                        <br />
                                                                        <div class="in-out" runat="server" id="div_inout" visible="false">
                                                                            <div><span>IN: </span><span><asp:Label ID="lblIN" runat="server"></asp:Label></span></div>
                                                                            <div style="border-top: 1px solid #000000;"><span>OUT: </span><span><asp:Label ID="lblOUT" runat="server" ></asp:Label></span></div>
                                                                        </div>                                                                       
                                                                    </td>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="col-sm-12  half-width-50 mgbt-xs-15 text-right">
                                        <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" OnClick="lnkSubmit_Click" Visible="false">Submit</asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                                <div id="divmsgbox" style="right:20px" runat="server"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <style>
                .in-out{
                    display: flex;
                    flex-direction: column;
                    align-items: center;
                    font-size: 10px;
                }
            </style>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



