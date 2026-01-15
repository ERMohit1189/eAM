<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="UnitMaster.aspx.cs" Inherits="_10.UnitMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script>
        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode !== 32 && keyCode !== 46)

                return false;
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" runat="server">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Unit Name &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtUnit" CssClass="form-control-blue validatetxt" onKeyPress="return ValidateAlpha(event);" MaxLength="100" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark &nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtremark" CssClass="form-control-blue" MaxLength="100" runat="server"></asp:TextBox>
                                            <div class="text-box-msg"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 btn-a-devices-2-p2  mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="lnkSubmit_OnClick">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px"></div>
                                    </div>
                                </div>

                               <div class="col-sm-12" runat="server" id="divlistshow">
                                                <div class="table-responsive2 table-responsive">
                                                    <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                                        <tbody>
                                                            <asp:Repeater ID="Repeater1" runat="server">
                                                                <HeaderTemplate>
                                                                    <tr>
                                                                        <th class="vd_bg-blue-np vd_white-np">#</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">Unit Name</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">Remark</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">Edit</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">Delete</th>
                                                                    </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label12" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("UnitName") %>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                        </td>

                                                                        <td class="menu-action" style="width: 40px;">
                                                                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("Id")%>' Visible="false"></asp:Label>
                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:LinkButton ID="LinkButton1" runat="server"
                                                                                CausesValidation="False" title="Edit" 
                                                                                class="btn menu-icon vd_bd-red vd_red" OnClick="LinkButton1_OnClick"><i class="glyphicon glyphicon-pencil"></i></asp:LinkButton>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>

                                                                        </td>
                                                                        <td class="menu-action" style="width: 40px;">
                                                                            <asp:LinkButton ID="lnkDelete" runat="server"
                                                                                CausesValidation="False" title="Delete" 
                                                                                class="btn menu-icon vd_bd-red vd_red" OnClick="lnkDelete_OnClick"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>

                                                </div>
                                            </div>

                            </div>
                        </div>
                    </div>

                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                            <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                                <div class="col-sm-12 ">
                                    <table class="tab-popup">
                                        <tr>
                                            <td>Unit Name</td>
                                            <td>
                                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                <div class="input-group input-group-margin ">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <%-- ReSharper disable once UnknownCssClass --%>
                                                    <asp:TextBox ID="txtEditUnitName" onKeyPress="return ValidateAlpha(event);" MaxLength="100" runat="server" class="form-control-blue validatetxtt"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Remark</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox ID="txtEditRemark" runat="server" MaxLength="100" class="form-control-blue"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div style="text-align: center;">
                                                    <asp:Button ID="btnupdate" CssClass="button-y" runat="server" Text="Update" TabIndex="3" OnClick="btnupdate_OnClick" OnClientClick="ValidateTextBox('.validatetxtt');return validationReturn();" />
                                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="Button4" PopupControlID="Panel1"
                            TargetControlID="Button5" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close" PopupDragHandleControlID="Panel1">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>

                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                            <table class="tab-popup text-center">

                                <tr>
                                    <td style="text-align: center">
                                        <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                                        </h4>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" />
                                        &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button-y" Text="Yes" OnClick="btnDelete_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                            PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

