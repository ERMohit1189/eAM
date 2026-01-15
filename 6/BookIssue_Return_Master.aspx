<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" MaintainScrollPositionOnPostback="false" AutoEventWireup="true" CodeFile="BookIssue_Return_Master.aspx.cs" Inherits="_6.AdminBookIssueReturnMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ReSharper disable once Html.PathError --%>
    <%-- ReSharper disable once Html.PathError --%>
    <%-- ReSharper disable once Html.PathError --%>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(getStaffList);
                Sys.Application.add_load(prettyphoto);
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="div1" runat="server">

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <div class="">
                                            <asp:DropDownList ID="rdotype" runat="server" AutoPostBack="true" CssClass="form-control-blue" OnSelectedIndexChanged="rdotype_SelectedIndexChanged">
                                                <asp:ListItem Value="Student" Selected="True">Student</asp:ListItem>
                                                <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <%--<label class="control-label">Enter No.&nbsp;<span class="vd_red">*</span></label>--%>
                                        <div id="divStudent" runat="server">
                                            <asp:TextBox ID="txtSearchStudent" placeholder="Enter Name/ S.R. No." runat="server" AutoPostBack="True" CssClass="form-control-blue"
                                                OnTextChanged="txtSearchStudent_TextChanged" onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()"></asp:TextBox>
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                        <div id="divStaff" runat="server" visible="false">
                                            <asp:TextBox ID="txtSearchStaff" placeholder="Enter Name/ Empcode" runat="server" AutoPostBack="True" CssClass="form-control-blue"
                                                OnTextChanged="txtSearchStaff_TextChanged" onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
                                            <asp:HiddenField ID="hfStaffId" runat="server" />
                                        </div>
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


                                    <div class="col-sm-4  half-width-50   mgbt-xs-15">
                                        <asp:LinkButton ID="lnkview" runat="server" class="button form-control-blue"
                                            OnClientClick="return ValidateTextBox('.validatetxt')" Text="View" OnClick="lnkview_Click">View</asp:LinkButton>
                                    </div>

                                </div>

                                <div class="col-sm-12  mgbt-xs-10">
                                    <div class="table-responsive  table-responsive2">
                                        <table style="width: 100%;">
                                            <tr id="grdshow" visible="False" runat="server">
                                                <td class="tab-top">
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
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
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>' Style="display: none;"></asp:Label>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>' Style="display: none;"></asp:Label>
                                                                    <asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>' Style="display: none;"></asp:Label>
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
                                                                <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                            </asp:HyperLink>
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <asp:HyperLink runat="server" ID="hylinkmoredetails" NavigateUrl="" Target="_blank" Text="more..."></asp:HyperLink>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="grdshow2" visible="False" runat="server">
                                                <td colspan="2">
                                                    <asp:GridView ID="GrdEmp" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40px" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
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

                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div id="div6" runat="server" class="col-sm-12" visible="false">
                                    <div class="table-responsive  table-responsive2" style="margin-bottom:0 !important;">
                                        <ajaxToolkit:Accordion ID="ac1" runat="server" AutoSize="None" FadeTransitions="true"
                                            TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false" SelectedIndex="-1" CssClass="mgbt-xs-5">
                                            <Panes>
                                                <ajaxToolkit:AccordionPane ID="AcorPreviousHistory" runat="server">
                                                    <Header><span class="btn btn-default small-btn"><i class="fa fa-arrow-down"></i>&nbsp;Payment History</span></Header>
                                                    <Content>
                                                        <asp:GridView ID="grdHistory" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Deposit Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label19" runat="server" Text='<%# Bind("FeeDepositeDate") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Receipt No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("RecieptSrNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label30" runat="server" Text='<%# Bind("MOP") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label29" runat="server" Text='<%# Bind("Cancel") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Late Fee">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLateFee" runat="server" Text='<%# Bind("LateFee") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Damage Fee">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDamageAmount" runat="server" Text='<%# Bind("DamageAmount") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Paid Amount">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="Label38" runat="server" Style="font-weight: 700" Text="Total Amount : "></asp:Label>
                                                                        <asp:Label ID="Label39" runat="server" Style="font-weight: 700"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label21" runat="server" Text='<%# Bind("RecievedAmount") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr No." Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label24" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                        <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Print">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkprint" runat="server" OnClick="lnkprint_Click" CssClass="icon-print-color"
                                                                            title="Print" data-toggle="tooltip"
                                                                            data-placement="top"><i class="fa fa-print "></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </Content>
                                                </ajaxToolkit:AccordionPane>
                                            </Panes>
                                        </ajaxToolkit:Accordion>
                                    </div>
                                </div>

                                <div id="div2" runat="server" class="col-sm-12  no-padding" visible="false">
                                    <div id="div3" runat="server" class="col-sm-12  no-padding">

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" visible="false">
                                            <label class="control-label">Select No.&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue">
                                                    <asp:ListItem Value="accessionno">Accession No.</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Enter Accession No.&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtAccessionNo" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                            <asp:LinkButton ID="lnkaccessionview" runat="server" OnClick="lnkaccessionview_Click"
                                                OnClientClick="return ValidateTextBox('.validatetxt1')" class="button form-control-blue"> View</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 65px;"></div>
                                            <asp:HiddenField ID="hfmaxbookallowed" runat="server" Value="0" />
                                            <asp:HiddenField ID="hfmaxbookinamonth" runat="server" Value="0" />
                                        </div>




                                        <div class="col-sm-12 ">
                                            <div class="table-responsive  table-responsive2">
                                                <asp:GridView ID="grdaccsnogrd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Accession No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaccessinno" runat="server" Text='<%# Bind("accessionno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Issued">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblisissued" runat="server" Text='<%# Bind("isissued") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Issue To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIssueTo" runat="server" Text='<%# Bind("To") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Title">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Subject">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsubject" runat="server" Text='<%# Bind("SubjectTopic") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblclass" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcatagory" runat="server" Text='<%# Bind("Language") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub-Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsubcatagory" runat="server" Text='<%# Bind("SubCategory") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Author1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblauthor1" runat="server" Text='<%# Bind("Author1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Author2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblauthor2" runat="server" Text='<%# Bind("Author2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Price">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblprice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkbox" runat="server" OnCheckedChanged="chkbox_CheckedChanged" AutoPostBack="true" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </div>



                                    </div>
                                    <div id="div5" class="col-sm-12 " runat="server" visible="false">


                                        <fieldset>
                                            <legend>
                                                <span class="font-s-17">Issue</span>
                                            </legend>
                                            <div class="col-sm-12  no-padding">
                                                <div class="table-responsive  table-responsive2">
                                                    <asp:GridView ID="grdIssued" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Accession No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblaccessinno" runat="server" Text='<%# Bind("accessionno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Title">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Issue Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIssueDate" runat="server" Text='<%# Bind("issuedate") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Maximum Return Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaximumDate" runat="server" Text='<%# Bind("maxreturndate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>

                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>


                                    <div id="div4" class="col-sm-12 " runat="server" visible="false">

                                        <fieldset>
                                            <legend>
                                                <span class="font-s-17">Return</span>
                                            </legend>
                                            <div class="col-sm-12  no-padding">
                                                <div class="table-responsive  table-responsive2">
                                                    <asp:GridView ID="grdReturn" runat="server" AutoGenerateColumns="False" CssClass="table table-striped no-bm table-hover no-head-border table-bordered">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("BIRid") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Accession No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblaccessinno" runat="server" Text='<%# Bind("accessionno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Title">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Issue Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIssueDate" runat="server" Text='<%# Bind("issuedate") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Maximum Return Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaximumDate" runat="server" Text='<%# Bind("maxreturndate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Fine">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFine" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Book Condition">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="drpCondition" runat="server" CssClass="form-control-blue tab-in-pad width-50  pull-left"
                                                                        OnSelectedIndexChanged="drpCondition_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtDamageAmount" runat="server" CssClass="form-control-blue width-50 pull-right" Visible="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tab-in text-center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Return">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkReturn" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>

                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div id="div7" runat="server" class="col-sm-12 ">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divdate" runat="server">
                                            <label class="control-label">Date</label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" Enabled="false" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" Enabled="false" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDDate" runat="server" CssClass="form-control-blue col-xs-4" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="DDDate_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                            <label class="control-label">Concession&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtConcession" onkeyup="CheckIntegerValueonKeyUp(event,this);" runat="server" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 ">
                                            <asp:Label ID="Label41" runat="server" class="control-label" Text="Mode of Payment"></asp:Label>
                                            <div class="">
                                                <asp:DropDownList ID="DropDownMOD" runat="server" AutoPostBack="True"  Enabled="false" TabIndex="1" CssClass="form-control-blue " OnSelectedIndexChanged="DropDownMOD_SelectedIndexChanged">
                                                    <asp:ListItem>Cash</asp:ListItem>
                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                    <asp:ListItem>DD</asp:ListItem>
                                                    <asp:ListItem>Card</asp:ListItem>
                                                    <asp:ListItem>Online Transfer</asp:ListItem>
                                                    <asp:ListItem>Other</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                            <label class="control-label">Remark&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        
                                        <div class="col-sm-4 " id="table1" runat="server" visible="false">
                                            <asp:Label ID="Label4" class="control-label" runat="server"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="txtChequeDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 " id="table2" runat="server" visible="false">
                                            <asp:Label ID="Label42" class="control-label" runat="server"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4 " id="table12" runat="server" visible="false">
                                            <asp:Label ID="Label43" runat="server" class="control-label" Text="Bank Name"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 " id="table4" runat="server" visible="false">
                                            <asp:Label ID="Label5" runat="server" class="control-label" Text="Status"></asp:Label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlChequeStatus" runat="server" CssClass="form-control-blue">
                                                    <asp:ListItem Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Value="Paid"></asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  half-width-50  btn-a-devices-2-p2 mgbt-xs-15 text-center">
                                        <asp:LinkButton ID="lnkIssue" runat="server" CssClass="button form-control-blue" Visible="false" OnClick="lnkIssue_Click">Submit</asp:LinkButton>
                                        <div id="msgbox2" runat="server" style="left: 65px;"></div>
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


</asp:Content>

