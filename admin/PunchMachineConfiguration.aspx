<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PunchMachineConfiguration.aspx.cs" Inherits="admin.PunchMachineConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding well">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Biometric Device For&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rdoBioMatricFor" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success 
                                                txt-capitalize-alpha">
                                                <asp:ListItem Value="Staff" Selected="True">Staff</asp:ListItem>
                                                <asp:ListItem Value="Student">Student</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Device Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDevice" runat="server">
                                                <asp:ListItem Value="MBIO5N">M-BIO5N</asp:ListItem>
                                                <asp:ListItem Value="RFENTRY2">RFENTRY2</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">IP Address&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtIpAddress" Placeholder="Ex. 192.168.001.224" 
                                                runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <script>
                                        function IpAddress() {
                                            $('#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtIpAddress').on('keyup', function () {
                                                $(this).val(
                                                    function (index, value) {
                                                        value = value.replace(/\W/gi, '').replace(/(.{3})/g, '$1.');
                                                        if (value.length > 15) {
                                                            value = value.substring(0, 15);
                                                        }
                                                        return value;
                                                    });
                                            });
                                        };
                                        Sys.Application.add_load(IpAddress);

                                    </script>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Machine No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtMachineNo" Placeholder="Ex. 1" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Port No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtPortNo" runat="server" CssClass="form-control-blue validatetxt" Text="5005"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3   ">
                                        <asp:Label ID="lbluserName" runat="server" class="control-label" Text="Username"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control-blue" Text=""></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3   ">
                                        <asp:Label ID="lblPassword" runat="server" class="control-label" Text="Password"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control-blue" Text="0"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3   ">
                                        <asp:Label ID="lblServerPortNo" runat="server" class="control-label" Text="Server Port No."></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtServerPortNo" runat="server" CssClass="form-control-blue" Text="5005"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 mgbt-xs-15 ">
                                        <asp:Label ID="Label1" runat="server" class="control-label" Text="Connection Mode"></asp:Label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rbList" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success 
                                                txt-capitalize-alpha">
                                                <asp:ListItem Selected="True" Value="Nw">Network</asp:ListItem>
                                                <asp:ListItem>USB</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3   ">
                                        <asp:Label ID="lblPush" runat="server" class="control-label" Text="Push"></asp:Label>
                                        <div class="">
                                            <asp:CheckBox ID="cbPush" runat="server" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" OnClick="lnkSubmit_OnClick" OnClientClick="return ValidateTextBox('.validatetxt');">Submit</asp:LinkButton>
                                            <div class="text-box-msg" style="width:100% !important">
                                                <div id="msgbox" runat="server" style="left: 75px" ></div>
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <div id="divExport" runat="server">
                                            <asp:GridView ID="grdPunchMachineConfiguration" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-striped table-hover no-bm no-head-border table-bordered table-header-group text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>                                                    
                                                    <asp:TemplateField HeaderText="Device For">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeviceFor" runat="server" Text='<%# Eval("DeviceFor") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IP Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIpAddress" runat="server" Text='<%# Eval("IpAddress") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Machine No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMachineNo" runat="server" Text='<%# Eval("MachineNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Port No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPortNo" runat="server" Text='<%# Eval("PortNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Username">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Password">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("Password") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Server Port No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblServerPortNo" runat="server" Text='<%# Eval("ServerPortNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Device Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeviceType" runat="server" Text='<%# Eval("DeviceType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Connection Mode">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblConnMode" runat="server" Text='<%# Eval("ConnMode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Push">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIsPush" runat="server" Text='<%# Eval("IsPush") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label36" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" title="Edit"
                                                                OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> 
                                                                <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_OnClick"
                                                                title="Delete" class="btn menu-icon vd_bd-red vd_red">
                                                                <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel2" runat="server" CssClass="popup">
                                        <table class="table form-group">
                                            <tr>
                                                <td align="right">Biometric Device For <span class="vd_red">*</span>
                                                </td>
                                                <td class="controls">
                                                    <asp:Button ID="Button1" runat="server" Style="display: none" />
                                                    <asp:DropDownList ID="drpDeviceForPanel" runat="server" Enabled="false">
                                                        <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                                        <asp:ListItem Value="Student">Student</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">Device Type <span class="vd_red">*</span>
                                                </td>
                                                <td class="controls">
                                                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                    <asp:DropDownList ID="drpDeviceTypePanel" runat="server">
                                                        <asp:ListItem Value="MBIO5N">M-BIO5N</asp:ListItem>
                                                        <asp:ListItem Value="RFENTRY2">RFENTRY2</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">IP Address <span class="vd_red">*</span>
                                                </td>
                                                <td class="controls">
                                                    <asp:TextBox ID="txtIPAddressPanel" Placeholder="Ex. 192.168.001.224" runat="server"
                                                        CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                        <script>
                                                            function IpAddressPanel() {
                                                                $('#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtIPAddressPanel').on('keyup', function () {
                                                                    $(this).val(
                                                                        function (index, value) {
                                                                            value = value.replace(/\W/gi, '').replace(/(.{3})/g, '$1.');
                                                                            if (value.length > 15) {
                                                                                value = value.substring(0, 15);
                                                                            }
                                                                            return value;
                                                                        });
                                                                });
                                                            };
                                                            Sys.Application.add_load(IpAddressPanel);

                                                        </script>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">Machine No. <span class="vd_red">*</span>
                                                </td>
                                                <td class="controls">
                                                    <asp:TextBox ID="txtMachineNoPanel" Placeholder="Ex. 1" runat="server"
                                                        CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">Port No.<span class="vd_red">*</span>
                                                </td>
                                                <td class="controls">
                                                   <asp:TextBox ID="txtPortNoPanel" runat="server" CssClass="form-control-blue validatetxt1" 
                                                       Text="5005"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">Username
                                                </td>
                                                <td class="controls">
                                                    <asp:TextBox ID="txtUsernamePanel" runat="server" CssClass="form-control-blue" 
                                                        Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">Password
                                                </td>
                                                <td class="controls">
                                                    <asp:TextBox ID="txtPasswordPanel" runat="server" CssClass="form-control-blue" 
                                                        Text="0"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">Server Port No.
                                                </td>
                                                <td class="controls">
                                                    <asp:TextBox ID="txtServerPortNoPanel" runat="server" CssClass="form-control-blue" 
                                                        Text="0"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">Connection Mode
                                                </td>
                                                <td class="controls">
                                                    <asp:RadioButtonList ID="rbListPanel" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success txt-capitalize-alpha">
                                                        <asp:ListItem Selected="True" Value="Nw">Network</asp:ListItem>
                                                        <asp:ListItem>USB</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">Push
                                                </td>
                                                <td class="controls">
                                                    <asp:CheckBox ID="cbPushPanel" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnUpdate" runat="server" CausesValidation="False" CssClass="button" 
                                                        OnClick="btnUpdate_Click" Text="Update" OnClientClick="return ValidateTextBox('.validatetxt1');" />
                                                    &nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="button" Text="Cancel" />
                                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <%-- ReSharper disable once Asp.InvalidControlType --%>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel2"
                                        CancelControlID="btnCancel" BackgroundCssClass="popup_bg">
                                    </ajaxToolkit:ModalPopupExtender>
                                </div>

                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <h4>Are you sure you want to delete this?
                                                        <asp:Label ID="lblPDeviceFor" runat="server" Visible="False"></asp:Label>
                                                        <asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                        <asp:LinkButton ID="lnkTargetControl" runat="server" Style="display: none"></asp:LinkButton></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:LinkButton ID="lnkNo" runat="server" CssClass="button-n">No</asp:LinkButton>
                                                    &nbsp; &nbsp;
                                                    <asp:LinkButton ID="lnkYes" runat="server" OnClick="lnkYes_OnClick" CssClass="button-y">Yes</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <%-- ReSharper disable once Asp.InvalidControlType --%>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="lnkNo" DynamicServicePath=""
                                            Enabled="True" PopupControlID="Panel1" TargetControlID="lnkTargetControl" BackgroundCssClass="popup_bg">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

