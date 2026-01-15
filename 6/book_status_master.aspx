<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="book_status_master.aspx.cs" Inherits="admin_book_status_master" %>

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
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Book Status &nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtBookStatus" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBookStatus" 
                                                    Style="color: #CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Renewal Cost &nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtRenewalCost" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRenewalCost" 
                                                    Style="color: #CC0000" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="form-control-blue button">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <div class="table-responsive2 table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Book Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("BookStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Renewal Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("RenewalCost") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                               
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>

                                                            <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton6" runat="server" title="Edit"  OnClick="LinkButton6_Click"
                                                                class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" Width="50px" />
                                                        <ItemStyle CssClass="text-center menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>

                                                            <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click"
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
            </div>



            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>Book Status :</td>
                            <td>
                                <asp:TextBox ID="txtBookStatus0" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Renewal Cost :</td>
                            <td>
                                <asp:TextBox ID="txtRenewalCost0" runat="server" CssClass="form-control-blue" SkinID="TxtBoxDef"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Remark :</td>
                            <td>
                                <asp:TextBox ID="txtRemark0" runat="server" SkinID="txtmulti" CssClass="form-control-blue"
                                    TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">
                                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="button-y" CausesValidation="False"
                                    OnClick="LinkButton4_Click">Update</asp:LinkButton>
                                &nbsp;
                                    <asp:LinkButton ID="LinkButton5" CssClass="button-n" runat="server" CausesValidation="False"
                                        OnClick="LinkButton5_Click">Cancel</asp:LinkButton>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label></td>
                            <td></td>
                        </tr>

                    </table>

                    <asp:Button ID="Button9" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                        CancelControlID="LinkButton5" PopupControlID="Panel1" TargetControlID="Button9" BehaviorID="Panel1_ModalPopupExtender_Close">
                    </asp:ModalPopupExtender>

                </asp:Panel>
            </div>


            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue"
                                    runat="server" Visible="False"></asp:Label></h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                                <asp:Button ID="Button8" runat="server" CssClass="button-n" CausesValidation="False"
                                    OnClick="Button8_Click" Text="No" />
                                &nbsp;&nbsp;  
                                <asp:Button ID="btnDelete" CssClass="button-y" runat="server" CausesValidation="False"
                                    OnClick="btnDelete_Click" Text="Yes" />


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

