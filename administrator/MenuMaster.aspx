<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/administrato_root-manager.master" AutoEventWireup="true" CodeFile="MenuMaster.aspx.cs" Inherits="MenuMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Main Menu&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtMainMenu" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Description</label>
                                        <div class="">
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Menu For</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpMenufor" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpMenufor_SelectedIndexChanged">
                                                <asp:ListItem>Admin</asp:ListItem>
                                                <asp:ListItem>Staff</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" Visible="true" CssClass="form-control-blue button" OnClick="LinkButton1_Click">Submit</asp:LinkButton>
                                        <div id="mssgbox" runat="server" style="left: 74px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12  ">
                                    <div class="table-responsive2  table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Srno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Menu Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Text") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Menu For">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccess" runat="server" Text='<%# Bind("MenuFor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("MenuID") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" title="Edit " 
                                                            OnClick="LinkButton3_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("MenuID") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"
                                                            title="Delete" 
                                                            class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
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
                            <td>Main Menu </td>
                            <td>
                                <asp:TextBox ID="txtMainMenuPanel" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>Description </td>
                            <td>
                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                <asp:TextBox ID="txtMainMenuDescPanel" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>Menu For</td>
                            <td>
                                <asp:DropDownList ID="drpMenuforPanel" runat="server" CssClass="form-control-blue">
                                    <asp:ListItem>Admin</asp:ListItem>
                                    <asp:ListItem>Staff</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                &nbsp;&nbsp;
                               <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>


                    </table>

                </asp:Panel>


                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" BackgroundCssClass="popup_bg"
                    TargetControlID="Button5" PopupControlID="Panel1" CancelControlID="Button4">
                </asp:ModalPopupExtender>

            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td align="center"><h4> Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server"
                                Visible="False"></asp:Label>
                                <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" />
                                &nbsp; &nbsp;
                                <asp:Button ID="btnDelete" runat="server" CssClass="button-y" OnClick="btnDelete_Click"
                                    Text="Yes" CausesValidation="False" />
                            </td>
                        </tr>

                    </table>

                </asp:Panel>

                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" BackgroundCssClass="popup_bg"
                    DynamicServicePath="" Enabled="True" TargetControlID="Button7" PopupControlID="Panel2" CancelControlID="Button8">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


