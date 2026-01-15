<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PlannerType.aspx.cs" Inherits="admin_PlannerType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script>
        function colourpick() {

            $('.colorpicker-hex').ColorPicker({
                color: '#ff00ff',
                onSubmit: function (hsb, hex, rgb, el) {
                    $(el).val(hex);
                    $(el).ColorPickerHide();
                },
                onBeforeShow: function () {
                    $(this).ColorPickerSetColor(this.value);
                },
                onChange: function (hsb, hex, rgb) {
                    $('.colorpicker-hex').val('#' + hex);
                    $('.colorpicker-hex').siblings().css({ 'color': '#' + hex });
                }
            })
           .bind('keyup', function () {
               $(this).ColorPickerSetColor(this.value);
           }).siblings().click(function (e) {
               $(this).siblings().click();
           });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top ">
                    <div class="panel-body ">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-12 no-padding ">

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Planner Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtCption" runat="server" CssClass="validatetxt form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Choose Color&nbsp;<span class="vd_red">*</span></label>
                                        <div class="" style="width:80%; float:left;">
                                            <asp:TextBox ID="txtColorbox" runat="server" CssClass="form-control-blue colorpicker-hex"></asp:TextBox>
                                            <div  style="width:17%; float:right;">
                                        <div class="color-pick-box input-group-addon color-input vd_hover" style="top: -33px;
    right: -40px;position: relative;"><i class="fa fa-square"></i></div>
                                            <script>
                                                Sys.Application.add_load(colourpick);
                                            </script>
                                    </div>
                                        </div>
                                    </div>
                                    

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                  <%--  <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Apply All Branchs</label>
                                        <div class="">
                                            <asp:CheckBox ID="chkbranchs" Checked="true" runat="server" CssClass="form-control-blue" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" OnClick="lnkSubmit_Click" OnClientClick="return ValidateTextBox('.validatetxt');">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px;"></div>
                                    </div>

                                </div>
                                <div class="col-sm-12  ">
                                    <div class=" table-responsive  table-responsive2">
                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue-np vd_white-np text-center" style="width: 40px">#</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Planner Type</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Remark</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Color</th>
                                                    <th class="vd_bg-blue-np vd_white-np text-center" style="width: 40px">Edit</th>
                                                    <th class="vd_bg-blue-np vd_white-np text-center" style="width: 40px">Delete</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptPlannerType" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center">
                                                                <%# Container.ItemIndex + 1 %>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPlannerType" runat="server" Text='<%# Eval("PlannerType") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblremark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 26px;">
                                                                <span class="input-group-addon color-input vd_hover" style="color: <%# Eval("Color") %>"><i class="fa fa-square"></i></span>
                                                            </td>
                                                            <td class="menu-action text-center" style="vertical-align: middle;">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" title="Edit " 
                                                                            OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>

                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="lnkEdit" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </td>
                                                            <td class="menu-action text-center" style="vertical-align: middle;">
                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"
                                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>


                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="lnkDelete" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

                                    </div>
                                </div>

                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup">
                                            <tr>
                                                <td>Planner Type
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtCptionPanel" runat="server" CssClass=" form-control-blue validatetxt1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Choose Color
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtColorboxPanel" runat="server" CssClass="form-control-blue colorpicker-hex"></asp:TextBox>
                                                    <span class="color-pick-box-edit input-group-addon color-input vd_hover" id="pickColor" runat="server"><i class="fa fa-square"></i></span>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Remark
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemarkPanel" runat="server" CssClass=" form-control-blue"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>

                                                    <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="False"
                                                        OnClientClick="return ValidateTextBox('.validatetxt1');" CssClass="button-y" OnClick="lnkUpdate_Click" Text="Update" />
                                                    &nbsp; &nbsp;
                                                    
                                                         <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                                    <asp:Label ID="lblPlannerTypePanel" runat="server" Visible="False"></asp:Label>
                                                </td>

                                            </tr>
                                        </table>



                                    </asp:Panel>
                                    <asp:LinkButton ID="lnkTargetControl1" runat="server" Style="display: none" />
                                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="lnkTargetControl1" PopupControlID="Panel1"
                                        CancelControlID="lnkCancel" BackgroundCssClass="popup_bg">
                                    </ajaxToolkit:ModalPopupExtender>
                                </div>

                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup text-center">
                                            <tr>
                                                <td align="center">
                                                    <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                        <asp:LinkButton ID="lnkTargetControl2" runat="server" Style="display: none" />
                                                    </h4>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lnkNo" runat="server" Text="No" CssClass="button-n" />
                                                    &nbsp; &nbsp;
                                                    <asp:LinkButton ID="lnkYes" runat="server" OnClick="lnkYes_Click" CssClass="button-y " Text="Yes" CausesValidation="False" />
                                                </td>
                                            </tr>
                                        </table>

                                    </asp:Panel>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" Enabled="True" TargetControlID="lnkTargetControl2"
                                        PopupControlID="Panel2" CancelControlID="lnkNo" BackgroundCssClass="popup_bg">
                                    </ajaxToolkit:ModalPopupExtender>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

