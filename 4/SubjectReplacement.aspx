<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SubjectReplacement.aspx.cs" Inherits="admin_SubjectReplacement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script>
        function fillTextbox() {
            var txtCompanyName = document.getElementById('<%= txtHeaderEmpId.ClientID %>').value;
            document.getElementById('<%= txtHeaderEmpId.ClientID %>').value = txtCompanyName.toString().trim();
        }
        function fillTextbox2() {
            var txtCompanyName2 = document.getElementById('<%= txtHeaderEmpId2.ClientID %>').value;
             document.getElementById('<%= txtHeaderEmpId2.ClientID %>').value = txtCompanyName2.toString().trim();
         }
    </script>
    <%-- ReSharper disable once Html.PathError --%>
    <%-- ReSharper disable once Html.PathError --%>
    <script type="text/javascript">
        function getEmployeeList() {
            $(function () {
                $("[id$=txtHeaderEmpId]").autocomplete({
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
        function getEmployeeList2() {
            $(function () {
                $("[id$=txtHeaderEmpId2]").autocomplete({
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
                        $("[id$=hfEmployeeId2]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getEmployeeList);
                Sys.Application.add_load(getEmployeeList2);
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding ">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label  no-padding">Old Username&nbsp;<span class="vd_red">*</span></label>
                                        <asp:TextBox ID="txtHeaderEmpId" runat="server" CssClass="form-control-blue" OnTextChanged="txtHeaderEmpId_TextChanged" AutoPostBack="True" placeholder="Enter Emp. ID/ Username/ Name"></asp:TextBox>
                                        <div class="text-box-msg">
                                            <asp:HiddenField ID="hfEmployeeId" runat="server" />
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkShow" runat="server" OnClick="lnkShow_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="Div3" runat="server" style="left: 75px;"></div>
                                    </div>


                                </div>

                                <div class="col-sm-12  mgbt-xs-5">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEcode" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Emp. Id">
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

                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mother's Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMName" runat="server" Text='<%# Bind("EMotherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date of Joining">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJoining" runat="server" Text='<%# Bind("RegistrationDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContact" runat="server" Text='<%# Bind("EMobileNo") %>'></asp:Label>
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
                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <Columns>


                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEcode1" runat="server" Text='<%# Eval("Ecode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Emp. Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpId1" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpName1" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Medium">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMedium" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClassName" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Stream">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Subject Group">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubjectGroup" runat="server" Text='<%# Bind("SubjectGroup") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Subject Group Activity**">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubjectName" runat="server" Text='<%# Bind("SubjectName") %>'></asp:Label>
                                                        <asp:Label ID="lblSubjectId" runat="server" Text='<%# Bind("SubjectId") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ClassTeacher" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClassTeacher" runat="server" Text='<%# Bind("ClassTeacher") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="60px" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding " runat="server" id="div1" visible="false">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label  no-padding">New Username&nbsp;<span class="vd_red">*</span></label>
                                        <asp:TextBox ID="txtHeaderEmpId2" runat="server" CssClass="form-control-blue" OnTextChanged="txtHeaderEmpId2_TextChanged" AutoPostBack="True" placeholder="Enter Emp. ID/ Username/ Name"></asp:TextBox>
                                        <div class="text-box-msg">
                                            <asp:HiddenField ID="hfEmployeeId2" runat="server" />
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkShowNew" runat="server" OnClick="lnkShowNew_Click" CssClass="button form-control-blue ">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12   mgbt-xs-5">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEcode" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Emp. Id">
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

                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mother Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMName" runat="server" Text='<%# Bind("EMotherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date of Joining">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJoining" runat="server" Text='<%# Bind("RegistrationDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContact" runat="server" Text='<%# Bind("EMobileNo") %>'></asp:Label>
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


                                <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                    <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue " OnClick="lnkSubmit_Click" ValidationGroup="a" Visible="False">Update</asp:LinkButton>

                                    <div id="Div2" runat="server" style="left: 75px;"></div>
                                </div>
                            </div>








                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

