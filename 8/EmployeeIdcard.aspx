<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmployeeIdcard.aspx.cs"
    Inherits="admin_EmployeeIdcard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <script type="text/javascript">
        function getEmployeeList() {
            $(function () {
                $("[id$=TxtEnter]").autocomplete({
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
    <asp:UpdatePanel ID="dfg" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getEmployeeList);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 select-list-hide display-none">

                                        <asp:DropDownList ID="DrpEnter" runat="server" OnSelectedIndexChanged="DrpEnter_SelectedIndexChanged" Enabled="false">
                                            <asp:ListItem>Enter Empid/EmpCode/Name</asp:ListItem>

                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>

                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:TextBox ID="TxtEnter" runat="server" placeholder="Enter Emp. ID/ Emp. Code/ Name"  AutoPostBack="True" CssClass="form-control-blue txtbox"
                                            OnTextChanged="TxtEnter_TextChanged" />
                                        <asp:HiddenField ID="hfEmployeeId" runat="server" />
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox1" runat="server" style="left: 64px"></div>

                                    </div>

                                </div>
                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <table align="center" cellpadding="2" cellspacing="2" style="width: 100%">
                                            <tr>
                                                <td class="style9" colspan="2">
                                                    <%--<uc1:IdCard ID="IdCard1" runat="server" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9" rowspan="2">&nbsp;
                                          <asp:Image ID="Image1" runat="server" Height="80px" Width="75px" />
                                                    &nbsp;
                                                </td>
                                                <td valign="bottom">
                                                    <asp:Label ID="Label26" runat="server" Font-Bold="False" Text="Employee Id :"></asp:Label>
                                                    &nbsp;<asp:Label ID="Label27" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="bottom">
                                                    <asp:Label ID="Label35" runat="server" Style="font-family: 'Free 3 of 9 Extended'; font-weight: 700; font-size: x-large"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    <asp:Label ID="Label33" runat="server" Style="font-weight: 700" Text="Date of Joining"></asp:Label>
                                                </td>
                                                <td valign="bottom">
                                                    <asp:Label ID="Label34" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    <asp:Label ID="Label17" runat="server" Text="Name" Font-Bold="True"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label28" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                    <asp:Label ID="Label18" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                                <td class="style11">
                                                    <asp:Label ID="Label29" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    <asp:Label ID="Label24" runat="server" Text="Emergency Contact No." Font-Bold="True"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label32" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">&nbsp;
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
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
