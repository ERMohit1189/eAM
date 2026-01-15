<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/administrato_root-manager.master" AutoEventWireup="true" CodeFile="SubMenuHierarchical.aspx.cs"
    Inherits="SubMenuHierarchical" %>

<%-- ReSharper disable once AspUnusedRegisterDirectiveHighlighting --%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <style type="text/css">
        .style1 {
            width: 284px;
        }
    </style>
    <div id="loader" runat="server"></div>
    <%--Page Content Section Starts--%>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Main Menu&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DrpMainMenu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMainMenu_SelectedIndexChanged"
                                                CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Sub Menu&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DrpSubMenu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpSubMenu_SelectedIndexChanged"
                                                CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Hiearchical Menu Name</label>
                                        <div class="">
                                            <asp:TextBox ID="txtSubMenu" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Hearchical Menu Desc(Must Unique)</label>
                                        <div class="">
                                            <asp:TextBox ID="txtMenuDesc" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">URL</label>
                                        <div class="">
                                            <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px !important;"></div>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <div class="table-responsive2 table-responsive">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Menu">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ParentMenu") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Hiearchical Menu">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("SubMenu") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("SubMenuDesc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="Label36" runat="server" Text='<%# Bind("Mid") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" title="Edit " 
                                                                        OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="Label37" runat="server" Text='<%# Bind("Mid") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                                        title="Delete" 
                                                                        class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
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
                            <td>Hiearchical Menu
                            </td>
                            <td>

                                <asp:TextBox ID="txtSubMenuPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>Discription
                            </td>
                            <td>
                                <asp:Button ID="Button5" runat="server" Style="display: none" />

                                <asp:TextBox ID="txtSubMenuDescPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>Url
                            </td>
                            <td>

                                <asp:TextBox ID="txtUrlPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                &nbsp;&nbsp;
                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td >
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>

                        <tr>
                            <td >
                                <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" OnClick="Button8_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CssClass="button-y" OnClick="btnDelete_Click" Text="Yes" CausesValidation="False" />
                            </td>
                        </tr>

                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

