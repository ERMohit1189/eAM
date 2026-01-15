<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="QuickSMSTemplets.aspx.cs" Inherits="QuickSMSTemplets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function GetCount(txtStr) {
            document.getElementById("<%= Label12.ClientID %>").innerHTML = txtStr.length;
            document.getElementById("<%= Label13.ClientID %>").innerHTML = txtStr.length;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                //Sys.Application.add_load(GetCount(txtStr));
            </script>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Title&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtMessageTitle" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12   mgbt-xs-15">
                                    <label class="control-label">SMS &nbsp;<span class="vd_red">*</span></label>
                                    <div class="mgbt-xs-5">
                                        <asp:TextBox ID="txtMessage" onkeyup="GetCount(this.value);" onblur="GetCount(this.value);" onclick="GetCount(this.value);" runat="server"
                                            TextMode="MultiLine" Rows="6" Font-Size="12" CssClass="form-control-blue"></asp:TextBox>
                                        <div class="text-box-msg ">
                                            <asp:Label ID="Label11" runat="server" CssClass="control-label " Text="Entered Characters:"></asp:Label>
                                            <span id="spanDisplay">
                                                <asp:Label ClientIDMode="Static" ID="Label12" CssClass="control-label txt-bold" runat="server" Text="0"></asp:Label></span>
                                            <asp:Label ID="Label3" runat="server" CssClass="control-label " Text="(For Unicode SMS: No. of characters will be extra according to content.)"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="btnSave" runat="server" CssClass="button form-control-blue" OnClick="btnSave_Click">Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnClear" runat="server" CssClass="button mrgn-tb-25 form-control-red" OnClick="btnClear_Click">Clear</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-6  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <div id="msgbox" runat="server" style="left: 70px !important;"></div>
                                    </div>
                                    <div class="col-sm-2  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="btnDelateAll" runat="server" CssClass="button form-control-blue" OnClick="btnDelateAll_Click">Delete All</asp:LinkButton>
                                    </div>
                                </div>


                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2" style="border:none;">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Bind("Id") %>'></asp:Label>
                                                        <asp:Label ID="lblsr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30%" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SMS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("SMS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" title="Edit" 
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
                                        </asp:GridView>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">

                        <tr>
                            <td>Title&nbsp;<span class="vd_red">*</span></td>
                            <td>
                                <asp:TextBox ID="txtMessageTitle0" runat="server" CssClass="form-control-blue validatetxt1" onblur="CreateShortName(this,'#ContentPlaceHolder1_txtMessageTitle0');"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PanelRequiredFieldValidator2" runat="server" ControlToValidate="txtMessageTitle0"
                                    SetFocusOnError="true" Display="Dynamic" CssClass="imp" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>SMS</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtMessage0" onkeyup="GetCount(this.value);" onblur="GetCount(this.value);" onclick="GetCount(this.value);" runat="server"
                                    TextMode="MultiLine" Rows="6" Font-Size="12" CssClass="form-control-blue  validatetxt"></asp:TextBox>
                                <div class="">
                                    <asp:Label ID="Label4" runat="server" CssClass="control-label " Text="Entered Characters:"></asp:Label>
                                    <span id="spanDisplays">
                                        <asp:Label ClientIDMode="Static" ID="Label13" CssClass="control-label txt-bold" runat="server" Text="0"></asp:Label></span>
                                    <asp:Label ID="Label6" runat="server" CssClass="control-label " Text="(For Unicode SMS: No. of characters will be extra according to content.)"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button-y" OnClick="lnkUpdate_Click">Update</asp:LinkButton>
                                &nbsp;
                                                    <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n" CausesValidation="false">Cancel</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><div id="msgbox2" runat="server" style="top:80% !important; right:0% !important"></div></td>
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
                            <td colspan="2" style="text-align: center;">Are you sure you want to delete this?
                                                 <asp:Label ID="lblvalue" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkDeleteNo" runat="server" CssClass="button-n" CausesValidation="false">No</asp:LinkButton>

                                &nbsp;&nbsp;
                                                      <asp:LinkButton ID="lnkDeleteYes" runat="server" CssClass="button-y" OnClientClick="javascript:scroll(0,0);" CausesValidation="false" OnClick="lnkDeleteYes_Click">Yes</asp:LinkButton>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Label ID="Label2" runat="server" Style="display: none"></asp:Label>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" BackgroundCssClass="popup_bg" runat="server" Enabled="true"
                    CancelControlID="lnkDeleteNo" PopupControlID="Panel2" TargetControlID="Label2">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td colspan="2" style="text-align: center;">Are you sure you want to delete all SMS templates?
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkDeleteNoAll" runat="server" CssClass="button-n" CausesValidation="false">No</asp:LinkButton>

                                &nbsp;&nbsp;
                                                      <asp:LinkButton ID="lnkDeleteYesAll" runat="server" CssClass="button-y" OnClientClick="javascript:scroll(0,0);" CausesValidation="false" OnClick="lnkDeleteYesAll_Click">Yes</asp:LinkButton>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Label ID="Label5" runat="server" Style="display: none"></asp:Label>
                <ajaxToolkit:ModalPopupExtender ID="Panel3_ModalPopupExtender" BackgroundCssClass="popup_bg" runat="server" Enabled="true"
                    CancelControlID="lnkDeleteNoAll" PopupControlID="Panel3" TargetControlID="Label5">
                </ajaxToolkit:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

