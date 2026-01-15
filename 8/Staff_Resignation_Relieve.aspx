<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Staff_Resignation_Relieve.aspx.cs" Inherits="admin_Staff_Resignation_Relieve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function EndRequestHandler() {
            scrollTo(0, 0);
        }
  
        function getEmployeeList() {
            $(function () {
                $("[id$=txtEnter]").autocomplete({
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="th" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(getEmployeeList);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hidden" runat="server" Visible="False">
                                        <label class="control-label">Select&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpEnter" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem>EmpId</asp:ListItem>
                                                <asp:ListItem>EmpCode</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                       <%-- <label class="control-label">Enter&nbsp;<span class="vd_red">*</span></label>--%>
                                        <div class="">
                                            <asp:TextBox ID="txtEnter" runat="server" CssClass="form-control-blue" onKeyUp="" placeholder="Enter Name/ Emp. Id/ Username" OnTextChanged="txtEnter_OnTextChanged" AutoPostBack="True" ></asp:TextBox>
                                            <asp:HiddenField ID="hfEmployeeId" runat="server" />
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEnter" ErrorMessage="*"
                                                    SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkShow" runat="server" CssClass="button" OnClick="lnkShow_Click" ValidationGroup="a">View</asp:LinkButton>
                                        <div id="msgbox1" runat="server" style="left: 64px"></div>
                                    </div>

                                    <div class="col-sm-12  ">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
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
                                                            <asp:Label ID="lblDesi" runat="server" Text='<%# Bind("DesNameNew") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  no-padding " id="div1" runat="server" visible="false">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Date of Relieving&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="DrpDDEmpYY" runat="server" OnSelectedIndexChanged="DrpDDEmpYY_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-sm-4" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DrpDDEmpMM" runat="server" OnSelectedIndexChanged="DrpDDEmpMM_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-sm-4" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DrpDDEmpDD" runat="server" OnSelectedIndexChanged="DrpDDEmpDD_SelectedIndexChanged"
                                                    CssClass="form-control-blue col-sm-4">
                                                </asp:DropDownList>
                                            </div>
                                        </div>


                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Status&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue">
                                                    <asp:ListItem>Relieved</asp:ListItem>
                                                    <asp:ListItem>Absconded</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Remark&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                                <div class="text-box-msg">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRemark" ErrorMessage="Please Enter Reason."
                                                        SetFocusOnError="True" Style="color: #CC0000" ValidationGroup="a1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                            <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" ValidationGroup="a1" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  no-padding" style="padding-top:30px !important;">
                                        
                                        <div class="col-sm-4 ">
                                            <label class="control-label">Sort by:&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:RadioButtonList ID="rbShort" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbShort_SelectedIndexChanged"
                                                    AutoPostBack="true" RepeatLayout="Flow" CssClass="vd_radio radio-success">
                                                    <asp:ListItem Selected="True">Emp. Id</asp:ListItem>
                                                    <asp:ListItem>Date of Relieving</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 ">
                                            <label class="control-label">Order:&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:RadioButtonList ID="rbOrder" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbOrder_SelectedIndexChanged"
                                                    AutoPostBack="true" RepeatLayout="Flow" CssClass="vd_radio radio-success">
                                                    <asp:ListItem Value="Asc">Ascending </asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="Desc">Descending</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  ">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="Grd1" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" AutoGenerateColumns="false" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Emp. Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Username">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEcode" runat="server" Text='<%# Bind("Empcode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesi" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContact" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date of Joining">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJoining" runat="server" Text='<%# Bind("DateofJoining") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date of Relieving">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReleving" runat="server" Text='<%# Bind("Dateofreleving") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Mother's Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMName" runat="server" Text='<%# Bind("MotherName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Remark" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" title="Edit"
                                                                
                                                                OnClick="LinkButton3_Click" CausesValidation="False"
                                                                class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"
                                                            CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">

                                            <table class="tab-popup">
                                                <tr>
                                                    <td>Date&nbsp;<span class="vd_red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="drpYYPanel" runat="server" CssClass="form-control-blue col-sm-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DrpMMPanel" runat="server" CssClass="form-control-blue col-sm-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DrpDDPanel" runat="server" CssClass="form-control-blue col-sm-4">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Cancel&nbsp;<span class="vd_red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue">
                                                            <asp:ListItem>No</asp:ListItem>
                                                            <asp:ListItem>Yes</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Remark&nbsp;<span class="vd_red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                               


                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                <asp:Button ID="Button3" runat="server" CssClass="button-y" OnClick="Button3_Click" Text="Update" OnClientClick="javascript:scroll(0,0);" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                                         <asp:Label ID="Label9" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>


                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                
                                            </div>
                                        </asp:Panel>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" TargetControlID="Button5"
                                            PopupControlID="Panel2" CancelControlID="Button4" BackgroundCssClass="popup_bg">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

