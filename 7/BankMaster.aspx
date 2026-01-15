<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="BankMaster.aspx.cs" Inherits="admin_BankMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Head Type</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function EndRequestHandler() {
            scrollTo(0, 0);
        }
    </script>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <asp:UpdatePanel ID="upMain" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-12  no-padding" id="tblInsert" runat="server">

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Bank Name&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                <label class="control-label">Remark</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" visible="false">
                                                <label class="control-label">Is Active&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:RadioButtonList ID="rblIsActive" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio-success vd_radio">
                                                        <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                                <asp:LinkButton ID="btnInsert" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" runat="server" CssClass="button form-control-blue" OnClick="btnInsert_Click">Submit</asp:LinkButton>
                                                <asp:LinkButton ID="btnReset" runat="server" CssClass="button form-control-blue" OnClick="btnReset_Click">Reset</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 147px"></div>
                                            </div>


                                            <div class="col-sm-12 ">
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="gvBankList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center ">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Bank Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="BankName" runat="server" Text='<%# Bind("BankName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remark">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Remark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="IsActive" runat="server" Text='<%# Eval("IsActive").ToString()=="True"?"Active":"Inactive" %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label36" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="lbtnEdit" runat="server" title="Edit"  OnClick="lbtnEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label37" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click"
                                                                        title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="50px" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>
                                <asp:Label ID="lblBankName0" runat="server" Text="Bank Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBankName0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRemark0" runat="server" Text="Remark"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemark0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Is Active<span class="vd_red">*</span> </td>
                            <td>
                                <asp:RadioButtonList ID="rblIsActive0" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" CausesValidation="False" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();javascript:scroll(0,0);" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                &nbsp;
                                                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button5" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="pnlDelete" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height:50px">
                                <asp:Button ID="btnNo" runat="server" CssClass="button-n" CausesValidation="False" Text="No" OnClick="btnNo_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnYes" runat="server" CssClass="button-y" CausesValidation="False" OnClientClick="javascript:scroll(0,0);" Text="Yes" OnClick="btnYes_Click" />

                            </td>
                        </tr>
                    </table>
                    <ajaxToolkit:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                        Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </asp:Panel>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>





</asp:Content>




