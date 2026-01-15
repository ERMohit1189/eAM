<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="CourseMaster.aspx.cs" Inherits="_1.AdminCourseMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <asp:HiddenField ID="HiddenField1" runat="server" />


            <script>

                
                Sys.Application.add_load(scrollbar);
            </script>


            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" id="table1" runat="server">
                                    <div class="col-sm-4   mgbt-xs-15" id="divBranch" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                                <asp:DropDownList runat="server" ID="ddlBranch" CssClass="validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                   <%-- <div class="col-sm-2   mgbt-xs-15" id="divSession" runat="server">
                                        <label class="control-label">Session</label>
                                        <div class="">
                                            <asp:DropDownList runat="server" ID="DrpSessionName" CssClass="validatedrp"></asp:DropDownList>

                                        </div>
                                    </div>--%>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label runat="server" ID="lblSession" CssClass="hide"></asp:Label>
                                        <label class="control-label">Course&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtCourse" runat="server" CssClass="form-control-blue validatetxt" onKeyup="CopyString('ContentPlaceHolder1_ContentPlaceHolderMainBox_',this,'txtCourseCode');" onblur="CopyString('ContentPlaceHolder1_',this,'txtCourseCode');"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Course Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpCourseType" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Text="<--Select-->" Value="0"></asp:ListItem>
                                                <asp:ListItem Value="1">Pre Primary</asp:ListItem>
                                                <asp:ListItem Value="2">Primary</asp:ListItem>
                                                <asp:ListItem Value="3">Junior</asp:ListItem>
                                                <asp:ListItem Value="4">Secondary</asp:ListItem>
                                                <asp:ListItem Value="5">Senior Secondary</asp:ListItem>
                                                <asp:ListItem Value="6">Under Graduate</asp:ListItem>
                                                <asp:ListItem Value="7">Post Graduate</asp:ListItem>
                                                <asp:ListItem Value="10">Other</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                    <%--<div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Duration (Years)&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control-blue validatetxt"
                                                onKeyup="CreateBatchNo();"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Batch&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtBatch" runat="server" CssClass="form-control-blue validatetxt" Enabled="false"></asp:TextBox>

                                        </div>
                                    </div>--%>

                                    <div class="col-sm-2  half-width-50 mgbt-xs-15" style="display:none;">
                                        <label class="control-label">Course Code&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtCourseCode" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Display Order&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Course Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpCourseCategory" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Text="<--Select-->" Value="0"></asp:ListItem>
                                                <asp:ListItem Value="1">Management</asp:ListItem>
                                                <asp:ListItem Value="2">Engineering</asp:ListItem>
                                                <asp:ListItem Value="3">Other</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" Rows="1" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:Button ID="btnsave" runat="server" CssClass="button form-control-blue" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="btnsave_Click" Text="Submit" />
                                        <div id="msgbox" runat="server" style="left: 74px;"></div>

                                    </div>


                                </div>
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2  table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        <asp:Label ID="id" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CourseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Course" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:BoundField DataField="CourseType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Course Type" HeaderStyle-CssClass="vd_bg-blue vd_white" Visible="false" />
                                                <asp:BoundField DataField="Duration" Visible="false" HeaderText="Duration" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:BoundField DataField="Batch" Visible="false" HeaderText="Batch" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:BoundField DataField="CourseCode" Visible="false" HeaderText="Course Code" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:BoundField DataField="CourseCategory" HeaderText="Course Category" HeaderStyle-CssClass="vd_bg-blue vd_white" Visible="false" />
                                                <asp:BoundField DataField="DisplayOrder" HeaderText="Display Order" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="lnkEdit" runat="server" title="Edit Course" 
                                                            OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkConfirmDelete" runat="server" OnClick="lnkConfirmDelete_Click" CausesValidation="False"
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


                                    <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">

                                            <table class="tab-popup">
                                                <tr>
                                                    <td>Course&nbsp;<span class="vd_red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtPanelCourse" runat="server" CssClass="form-control-blue validatetxt1" Enabled="false"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr class="hide">
                                                    <td>Course Type&nbsp;<span class="vd_red">*</span></td>
                                                    <td>
                                                        <asp:DropDownList ID="drpPanelCourseType" runat="server" CssClass="form-control-blue validatedrp1">
                                                            <asp:ListItem Text="<--Select-->" Value="0"></asp:ListItem>
                                                            <asp:ListItem Value="1">Pre Primary</asp:ListItem>
                                                            <asp:ListItem Value="2">Primary</asp:ListItem>
                                                            <asp:ListItem Value="3">Junior</asp:ListItem>
                                                            <asp:ListItem Value="4">Secondary</asp:ListItem>
                                                            <asp:ListItem Value="5">Senior Secondary</asp:ListItem>
                                                            <asp:ListItem Value="6">Graduate</asp:ListItem>
                                                            <asp:ListItem Value="7">Post Graduate</asp:ListItem>
                                                            <asp:ListItem Value="10">Other</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                              <%--  <tr>
                                                    <td>Duration&nbsp;<span class="vd_red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtPanelDuration" runat="server" CssClass="form-control-blue validatetxt1"
                                                            onKeyup="CreateBatch(event,this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtBatch','#ContentPlaceHolder1_ContentPlaceHolderMainBox_HiddenField1');"></asp:TextBox>

                                                    </td>
                                                </tr>--%>
                                                <%--<tr>
                                                    <td>Batch&nbsp;<span class="vd_red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtPanelBatch" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>

                                                    </td>

                                                </tr>--%>
                                                <tr style="display:none;">
                                                    <td>Course Code&nbsp;<span class="vd_red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtPanelCourseCode" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Display Order&nbsp;<span class="vd_red">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtPanelDisplayOrder" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>

                                                    </td>

                                                </tr>
                                                <tr class="hide">
                                                    <td>Course Category&nbsp;<span class="vd_red">*</span></td>
                                                    <td>
                                                        <asp:DropDownList ID="drpPanelCourseCategory" runat="server" CssClass="form-control-blue">
                                                            <asp:ListItem Text="<--Select-->" Value="0"></asp:ListItem>
                                                            <asp:ListItem Value="1">Management</asp:ListItem>
                                                            <asp:ListItem Value="2">Engineering</asp:ListItem>
                                                            <asp:ListItem Value="3">Other</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                                <tr class="hide">
                                                    <td>Remark</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPanelRemark" runat="server" Rows="2" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button-y" OnClick="lnkUpdate_Click" OnClientClick="ValidateDropdown('.validatedrp1');ValidateTextBox('.validatetxt1');return validationReturn();">Update</asp:LinkButton>
                                                        &nbsp;
                                                <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n" CausesValidation="false">Cancel</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>

                                        </asp:Panel>
                                        <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
                                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" BackgroundCssClass="popup_bg" runat="server" Enabled="true"
                                            CancelControlID="lnkCancel" PopupControlID="Panel1" TargetControlID="Button1">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </div>

                                    <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                            <table class="tab-popup text-center">
                                                <tr>
                                                    <td>
                                                        <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label></h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkDeleteNo" runat="server" CssClass="button-n" CausesValidation="false">No</asp:LinkButton>
                                                        &nbsp;&nbsp;
                                                            <asp:LinkButton ID="lnkDeleteYes" runat="server" CssClass="button-y" CausesValidation="false" OnClick="lnkDeleteYes_Click">Yes</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Label ID="Label2" runat="server" Style="display: none"></asp:Label>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" BackgroundCssClass="popup_bg" runat="server" Enabled="true"
                                            CancelControlID="lnkDeleteNo" PopupControlID="Panel2" TargetControlID="Label2">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">

                function CreateBatchNo() {
                    var sessin = $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_lblSession").html();
                    var Duration = $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtDuration").val();
                    if (Duration != "") {
                        var data = sessin.split("-");
                        var sessionIncrre = parseInt(data[0]) + parseInt(Duration);
                        var complete = data[0] + "-" + sessionIncrre;
                        $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtBatch").val(complete)
                    }
                    else {
                        $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtBatch").val("");
                    }

                }

            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

