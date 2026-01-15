<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ClassAllotment_ng.aspx.cs" Inherits="admin_ClassAllotment_ng" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
     <script>
        function fillTextbox() {
            var txtCompanyName = document.getElementById('<%= txtHeaderEmpId.ClientID %>').value;
            document.getElementById('<%= txtHeaderEmpId.ClientID %>').value = txtCompanyName.toString().trim();
        }
    </script>
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
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">


    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

             <script>
        Sys.Application.add_load(getEmployeeList);
        Sys.Application.add_load(datetime);
    </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                
                                <div class="col-sm-12  no-padding">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-12  no-padding ">
                                                 <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                   <label class="control-label  no-padding">Enter Emp. ID/ Username/ Name&nbsp;<span class="vd_red">*</span></label>
                                                    <asp:TextBox ID="txtHeaderEmpId" runat="server" onchange="javascript:return fillTextbox();" CssClass="form-control-blue" OnTextChanged="txtHeaderEmpId_TextChanged" AutoPostBack="True" placeholder="Enter Emp. ID/ Username/ Name"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                        <asp:HiddenField ID="hfEmployeeId" runat="server" />
                                                    </div>
                                                </div>


                                                <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                                    <asp:LinkButton ID="lnkShow" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="lnkShow_Click" CssClass="button form-control-blue ">View</asp:LinkButton>
                                                    <div id="msgbox" runat="server" style="left: 75px;"></div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   no-padding" runat="server" id="div1">
                                                <div class="col-sm-12 ">
                                                    <div class=" table-responsive  table-responsive2">
                                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border 
                                                            table-bordered text-center">
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
                                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Designation">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesi" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>


                                                <div class="col-sm-12   no-padding ">

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Class</label>
                                                        <div class="">
                                                            <asp:DropDownList ID="drpclass" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                                OnSelectedIndexChanged="drpclass_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Stream</label>
                                                        <div class="">
                                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue validatedrp">
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Section</label>
                                                        <div class="">
                                                            <asp:DropDownList ID="drpSection" runat="server" AutoPostBack="True" 
                                                                OnSelectedIndexChanged="drpSection_SelectedIndexChanged" SkinID="ddDefault" CssClass="form-control-blue validatedrp">
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Medium</label>
                                                        <div class="">
                                                            <asp:DropDownList ID="drpmedium" runat="server" 
                                                                CssClass="form-control-blue validatedrp" AutoPostBack="True" OnSelectedIndexChanged="drpmedium_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Subject</label>
                                                        <div class="">
                                                            <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control-blue">
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                                        <asp:LinkButton ID="btnSubmit" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();"
                                                            runat="server" CssClass="button form-control-blue" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                                    </div>

                                                </div>

                                            </div>

                                            <div class="col-sm-12 ">

                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="Grd1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
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
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Section">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Stream">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            

                                                            <asp:TemplateField HeaderText="Subject">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubjectName" runat="server" Text='<%# Bind("SubjectName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Edit" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="linkEdit" runat="server" title="Edit" 
                                                                        OnClick="linkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="linkDelete" runat="server" OnClick="linkDelete_Click" title="Delete" CausesValidation="False"
                                                                        data-placement="top" class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 no-padding ">
                                                <div style="overflow: auto; width: 1px; height: 1px">
                                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                                        <table class="tab-popup">
                                                            <tr>
                                                                <td>Class :
                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="drpclass1" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="drpclass1_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblid" runat="server" Text="" Visible="false"></asp:Label>


                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Stream :
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpBranch1" runat="server" CssClass="form-control-blue">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Section
                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="drpSection1" runat="server" SkinID="ddDefault" CssClass="form-control-blue">
                                                                    </asp:DropDownList>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Medium :
                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="drpmedium1" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="drpmedium1_SelectedIndexChanged">
                                                                    </asp:DropDownList>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Subject :
                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="drpSubjectGroup1" runat="server" CssClass="form-control-blue">
                                                                    </asp:DropDownList>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Paper :
                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="drpSubjectPaper1" runat="server" CssClass="form-control-blue">
                                                                    </asp:DropDownList>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Activity :
                                                                </td>
                                                                <td>

                                                                    <asp:DropDownList ID="drpSubject1" runat="server" CssClass="form-control-blue">
                                                                    </asp:DropDownList>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>

                                                                    <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button-y" OnClick="lnkUpdate_Click">Update</asp:LinkButton>
                                                                    &nbsp;&nbsp;
                                                                <asp:LinkButton ID="lnkCancle" runat="server" CssClass="button-n">Cancel</asp:LinkButton>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
                                                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                                                        BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True"
                                                        PopupControlID="Panel1" TargetControlID="Button2" CancelControlID="lnkCancle">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                </div>

                                                <div style="overflow: auto; width: 1px; height: 1px">
                                                    <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                                        <table class="tab-popup text-center">

                                                            <tr>
                                                                <td>
                                                                    <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                                        <asp:Button ID="Button7" runat="server" Style="display: none" />
                                                                    </h4>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style="text-align:center;">
                                                                    <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" CssClass="button-y" CausesValidation="False" Text="Yes" />


                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                                                        PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

