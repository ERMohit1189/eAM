<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="BlockStudent.aspx.cs" Inherits="_1.SuperAdminBlockedStudent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <%-- ReSharper disable once Html.PathError --%>
    <%-- ReSharper disable once Html.PathError --%>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtStudentEnter]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetStudents") %>',
                            data: "{ 'studentId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.split('@')[0],
                                        val: item.split('@')[1]
                                    };
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
                
                Sys.Application.add_load(prettyphoto);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 select-list-hide display-none">

                                        <asp:DropDownList ID="DrpStudentEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter S.R./ Enrollment No./Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <i>H</i>
                                        <div class="text-box-msg">
                                        </div>

                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                        <div class="">
                                            <asp:TextBox ID="txtStudentEnter" runat="server" CssClass="form-control-blue width-100 validatetxt"
                                                AutoPostBack="true" OnTextChanged="txtStudentEnter_TextChanged" placeholder="Enter Name/ S.R. No."></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:HiddenField ID="hfStudentId" runat="server" />
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkShow" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" OnClick="lnkShow_Click">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 64px"></div>

                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding" id="divExport" runat="server">

                                    <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2" id="abc" runat="server" Visible="False">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="tab-top">
                                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False"
                                                            CssClass="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" CssClass="vd_bg-blue-np vd_white-np" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Student's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                               
                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                              
                                                                <asp:TemplateField HeaderText="Class">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>' CssClass="hide"></asp:Label>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BranchName") %>' CssClass="hide"></asp:Label>
                                                                    <asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>' CssClass="hide"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
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
                                                                        <asp:Label ID="lblFamilyContact" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Enrollment No." Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>


                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td class="tab-top tab-profile text-center ">
                                                        <div>
                                                            <div class="gallery-item fee-pic-box">
                                                                <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                                    <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" style="Width: 48px; Height: 60px;"  />
                                                                </asp:HyperLink>
                                                            </div>
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <asp:HyperLink runat="server" ID="hylinkmoredetails" NavigateUrl="" Target="_blank" Text="more..."></asp:HyperLink>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-9" id="divRemark" runat="server">
                                        <label class="control-label">Remark&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue validatetxt" Rows="1" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15" id="divSubmit" runat="server">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="button form-control-blue" OnClick="LinkButton2_Click"  OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();">Block</asp:LinkButton>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label24" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.R. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Student's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Father's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFamilyContact" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label25" runat="server" Text='<%# Bind("blockedRemark") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unblock">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label37" runat="server" Text='<%# Bind("srno") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CausesValidation="False"
                                                                title="Unblock"  class="btn menu-icon vd_bd-green vd_green"><i class="fa fa-unlock-alt"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div style="overflow: auto; width: 2px; height: 1px">
                                    <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup text-center">
                                            <tr>
                                                <td>
                                                    <h4>Are you sure you want to unblock this student?
                            <asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                        <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button8" runat="server" CssClass="button-n" CausesValidation="False" Text="No" />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnDelete" runat="server" CssClass="button-y" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" />


                                                </td>
                                            </tr>
                                        </table>
                                        <%-- ReSharper disable once Asp.InvalidControlType --%>
                                        <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button8" DynamicServicePath=""
                                            Enabled="True" PopupControlID="Panel2" TargetControlID="Button7" BackgroundCssClass="popup_bg">
                                        </asp:ModalPopupExtender>
                                    </asp:Panel>
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

