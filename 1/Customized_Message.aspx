<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Customized_Message.aspx.cs" Inherits="_1.SuperAdminCustomizedMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
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
    <script type="text/javascript">
        function GetCount(txtStr) {
            document.getElementById("<%= Label12.ClientID %>").innerHTML = txtStr.length;
        }
        function alertmsg() {
            alert(
                "It looks like you are not connected to the Internet.\nPlease check your Internet connection and try again.");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
                Sys.Application.add_load(GetCount(txtStr));
            </script>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12 no-padding ">
                                    <div class="col-md-5 col-sm-8  no-padding mgbt-xs-20">
                                        <div class="form-group">
                                            <div class="col-lg-12 col-sm-12 ">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" class="vd_radio radio-success" RepeatLayout="Flow"
                                                    OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem>Student Wise</asp:ListItem>
                                                    <asp:ListItem Selected="True">Bulk</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12 no-padding ">

                                    <div class="col-sm-12 no-padding " id="table1" runat="server">

                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <div class="">
                                                <asp:TextBox ID="txtSearch" placeholder="Enter Name/ S.R. No." runat="server" AutoPostBack="True" CssClass="form-control-blue validatetxt"
                                            OnTextChanged="txtSearch_TextChanged" onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
                                        <asp:HiddenField ID="hfStudentId" runat="server" />
                                        <div class="text-box-msg">
                                        </div>
                                            </div>
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
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="return ValidateTextBox('.validatetxt1');" OnClick="LinkButton2_Click" CssClass="button form-control-blue">Show</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 70px !important;"></div>
                                            <asp:Label ID="lblFee" runat="server" Style="color: #FF0000"></asp:Label>

                                        </div>
                                    </div>



                                    <div class="col-sm-12 no-padding " id="table2" runat="server">
                                        <div class="col-sm-3 half-width-50 mgbt-xs-15 hide">
                                            <label class="control-label">Course</label>
                                            <div class="">
                                                <asp:DropDownList ID="drpCourse" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpCourse_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                            <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpClassCourse" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpClassCourse_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                            <label class="control-label">Section</label>
                                            <div class="">
                                                <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                            <label class="control-label">Stream</label>
                                            <div class="">
                                                <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12 no-padding ">
                                    <div class="col-sm-12 no-padding" runat="server" id="table3">
                                        <div class="col-sm-12 half-width-50 mgbt-xs-15">
                                            <label class="control-label">Quick SMS Title</label>
                                            <div class="">
                                                <asp:DropDownList ID="drpQuickMessage" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpQuickMessage_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12  mgbt-xs-15">
                                            <label class="control-label">SMS &nbsp;<span class="vd_red">*</span></label>
                                            <div class="mgbt-xs-5">
                                                <asp:TextBox ID="txtMessage" onkeyup="GetCount(this.value);" onblur="GetCount(this.value);" onclick="GetCount(this.value);" runat="server"
                                                    TextMode="MultiLine" Rows="4" Font-Size="12" CssClass="form-control-blue  validatetxt"></asp:TextBox>
                                                <div class="text-box-msg ">
                                                    <asp:Label ID="Label11" runat="server" CssClass="control-label " Text="Entered Characters:"></asp:Label>
                                                    <span id="spanDisplay">
                                                        <asp:Label ClientIDMode="Static" ID="Label12" CssClass="control-label txt-bold" runat="server" Text="0"></asp:Label></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                                    <asp:Label ID="Label3" runat="server" CssClass="control-label " Text="(For Unicode SMS: No. of characters will be extra according to content.)"></asp:Label>

                                        </div>
                                       
                                    </div>
                                </div>
                               
                                <div class="col-sm-12 text">
                                    <br />
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkButton1_Click">Send</asp:LinkButton>
                                    <div id="msg1" runat="server" style="left: 70px !important;"></div>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="button mrgn-tb-25 form-control-blue" OnClick="LinkButton3_Click" Visible="false">Stop</asp:LinkButton>
                                </div>
                                <div class="col-sm-12" style="padding-top:40px;">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enrollment No." Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                        (<asp:Label ID="Label6" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>)
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Medium" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label28" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date of Admission ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="ChkAll" runat="server" OnCheckedChanged="ChkAll_CheckedChanged" AutoPostBack="true" Checked="True"></asp:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk" runat="server" Checked="True"></asp:CheckBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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

