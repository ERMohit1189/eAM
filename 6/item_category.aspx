<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="item_category.aspx.cs" Inherits="item_category" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>



            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Item Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtcategory" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9 hide">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                       <div id="msgbox" runat="server" style="left:75px;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12  ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered ">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue text-center vd_white" Width="50px" />
                                                    <ItemStyle CssClass="text-center " />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue text-left vd_white" />
                                                    <ItemStyle CssClass="text-left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit"  OnClick="LinkButton2_Click"
                                                            class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue text-center vd_white" Width="50px" />
                                                    <ItemStyle CssClass="text-center menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue text-center vd_white" Width="50px" />
                                                    <ItemStyle CssClass="text-center menu-action" />
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
                            <td style="text-align:right;">Category Name&nbsp;<span class="vd_red">*</span></td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="TextBox3" CssClass="form-control-blue validatetxt1" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                <asp:Button ID="Button9" runat="server" Style="display: none" />
                                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                                    CancelControlID="LinkButton5" PopupControlID="Panel1" TargetControlID="Button9">
                                </asp:ModalPopupExtender>
                            </td>
                        </tr>

                        <tr class="hide">
                            <td style="text-align:right;" valign="top">Remark </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue" SkinID="txtmulti"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td></td>

                            <td style="padding-left: 5px; text-align:left;">
                                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="button-y" CausesValidation="False"
                                    OnClick="LinkButton4_Click" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();">Update</asp:LinkButton>
                                &nbsp;
                                    <asp:LinkButton ID="LinkButton5" runat="server" CssClass="button-n" CausesValidation="False"
                                        OnClick="LinkButton5_Click">Cancel</asp:LinkButton>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>


                    </table>
                </asp:Panel>
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
                                <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CssClass="button-n" />
                                &nbsp; &nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Yes" CssClass="button-y" CausesValidation="False" />
                            </td>
                        </tr>

                    </table>
                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server"
                        CancelControlID="Button8" DynamicServicePath="" Enabled="True"
                        PopupControlID="Panel2" TargetControlID="Button7" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                    </asp:ModalPopupExtender>

                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

