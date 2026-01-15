<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="CategoryHead.aspx.cs" Inherits="CategoryHead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
    <script>
        
    </script>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">

                                    <asp:UpdatePanel ID="upMain" runat="server">
                                        <ContentTemplate>

                                            <div class="col-sm-12  no-padding">
                                                <div class="col-sm-2 half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Head Type&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="ddlHeadType" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlHeadType_SelectedIndexChanged">
                                                            <asp:ListItem Value="00"><-- Select Type --></asp:ListItem>
                                                            <asp:ListItem Value="Income">Income</asp:ListItem>
                                                            <asp:ListItem Value="Expense">Expense</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Head Category&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtHeadCategoryName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                                <div class="col-sm-6  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                                    <asp:LinkButton ID="btnInsert" runat="server" CssClass="button form-control-blue" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="btnInsert_Click">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnReset" runat="server" Visible="false" CssClass="button form-control-blue" OnClick="btnReset_Click">Reset</asp:LinkButton>
                                                    <div id="msgbox" runat="server" style="left: 145px;"></div>
                                                </div>

                                                <div class="col-sm-12  ">
                                                    <div class=" table-responsive  table-responsive2">
                                                        <asp:GridView ID="gvHeadMaster" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Head Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="HeadType" runat="server" Text='<%# Bind("HeadType") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Head Category">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="HeadCategory" runat="server" Text='<%# Bind("HeadCategory") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label36" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                        <asp:LinkButton ID="lbtnEdit" runat="server" title="Edit "  OnClick="lbtnEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
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
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>
                                <asp:Label ID="lblHeadType0" runat="server" CssClass="txt-middle-l txt-bold" Text="Head Type"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlHeadType0" runat="server" CssClass="form-control-blue">
                                    <asp:ListItem Value="Income">Income</asp:ListItem>
                                    <asp:ListItem Value="Expense">Expense</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHeadCategoryName0" runat="server" CssClass="txt-middle-l txt-bold" Text="Head Category"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHeadCategoryName0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td></td>
                            <td colspan="4" style="text-align: center;">
                                <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button-y" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" OnClick="Button3_Click" Text="Update" />
                                &nbsp;
                                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button5" runat="server" Style="display: none" />
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
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
                            <td style="text-align: center;" height="50">
                                <asp:Button ID="btnNo" runat="server" CssClass="button-n" CausesValidation="False" Text="No" OnClick="btnNo_Click" />
                                &nbsp;&nbsp;  
                                                                        <asp:Button ID="btnYes" runat="server" CssClass="button-y" CausesValidation="False" OnClientClick="javascript:scroll(0,0);" Text="Yes" OnClick="btnYes_Click" />

                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                        Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



