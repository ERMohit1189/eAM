<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AllotTestToStaff.aspx.cs"
    Inherits="AllotTestToStaff" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
    <style>
        .vd_checkbox label {
            margin-bottom:0px !important;
        }
        #ContentPlaceHolder1_ContentPlaceHolderMainBox_Panel1 {
            overflow: auto !important;
    max-height: 550px !important;
        }
    </style>
    <script>
        function fillTextbox() {
            var txtCompanyName = document.getElementById('<%= txtHeaderEmpId.ClientID %>').value;
            document.getElementById('<%= txtHeaderEmpId.ClientID %>').value = txtCompanyName.toString().trim();
        }
    </script>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getEmployeeList);
                
                Sys.Application.add_load(datetime);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <asp:TextBox ID="txtHeaderEmpId" runat="server" onchange="javascript:return fillTextbox();" CssClass="form-control-blue" OnTextChanged="txtHeaderEmpId_TextChanged" AutoPostBack="True" placeholder="Enter Emp. ID/ Emp. Code/ Name"></asp:TextBox>
                                        <div class="text-box-msg">
                                            <asp:HiddenField ID="hfEmployeeId" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-8 half-width-50 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" OnClick="LinkButton2_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                                <div id="msgbox1" runat="server" style="left: 55px"></div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                                <Columns>
                                                    
                                                    <asp:TemplateField HeaderText="Emp. Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Username">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEcode" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Father's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mother's Name"  Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMName" runat="server" Text='<%# Bind("EMotherName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date of Joining">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJoining" runat="server" Text='<%# Bind("RegistrationDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContact" runat="server" Text='<%# Bind("EMobileNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesi" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                <div class="col-sm-12 no-padding" runat="server" id="divFilters" visible="false">
                                    <div class="col-sm-4">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control-blue validatedrps" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 ">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 ">
                                        <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control-blue validatedrps" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4" runat="server" id="divPaper" visible="false">
                                        <label class="control-label">Paper&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:CheckBoxList ID="ddlPaper" runat="server" CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-left">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxts');ValidateDropdown('.validatedrps');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button form-control-blue" style="margin-top:24px;">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;margin-top:29px;"></div>

                                    </div>
                                </div>

                                <div class="col-sm-12">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        
                                                        <asp:Label ID="LblId" runat="server" Text='<%# Bind("Examid") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Class" runat="server" Text='<%# Bind("classname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Subject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                              <%--  <asp:TemplateField HeaderText="Paper">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Paper" runat="server" Text='<%# Bind("Paper") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Section" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Term Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TermName" runat="server" Text='<%# Bind("TermName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Test Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExamName" runat="server" Text='<%# Bind("ExamName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Duration">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Duration" runat="server" Text='<%# Bind("Duration") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="File" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" ID="nnn" NavigateUrl='<%# Bind("FilePath") %>' download="download" Target="_blank" style="text-decoration:underline;">Test File</asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Exam Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExamDate" runat="server" Text='<%# Eval("Examdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Result Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResultDate" runat="server" Text='<%# Eval("Resultdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               
                                               <asp:TemplateField HeaderText="Allot To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmp" runat="server" Text='<%# Eval("Ecode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="200" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Paperid" runat="server" Text='<%# Bind("Paperid") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>
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
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" OnClick="Button8_Click" CausesValidation="False" />
                                &nbsp;&nbsp;
                                                        <asp:Button ID="btnDeleteYes" runat="server" OnClick="btnDeleteYes_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    

</asp:Content>
