<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ShiftMapping.aspx.cs" Inherits="_1.ShiftMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div runat="server" id="loader"></div>
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblClass" runat="server" class="control-label" Text="Class"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblShift" runat="server" class="control-label" Text="Shift"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="drpShift" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue " OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px"></div>
                                    </div>


                                </div>
                                <div class="col-sm-12  ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="grdShiftWithClass" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftName" runat="server" Text='<%# Eval("ShiftName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>

                                                        <asp:Label ID="lblClassId" runat="server" Text='<%# Eval("ClassId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblShiftId" runat="server" Text='<%# Eval("ShiftId") %>' Visible="false"></asp:Label>

                                                        <asp:LinkButton ID="lbtnEdit" runat="server" title="Edit Shift"  OnClick="lbtnEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" title="Delete Shift" OnClick="lbtnDelete_Click"
                                                              class="btn menu-icon vd_bd-red vd_red">
                                                            <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                        <asp:Label ID="lblValue" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="col-sm-12  no-padding">
                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <asp:Label ID="lblClassPanel" runat="server" class="control-label" Text="Class"></asp:Label>
                                                <div class="">
                                                    <asp:DropDownList ID="drpClassPanel" CssClass="form-control-blue" runat="server" Enabled="false"></asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <asp:Label ID="lblShiftPanel" runat="server" class="control-label" Text="Shift"></asp:Label>
                                                <div class="">
                                                    <asp:DropDownList ID="drpShiftPanel" CssClass="form-control-blue col-sm-6 col-xs-6"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-sm-6  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                                <asp:Button ID="btnUpdate" runat="server" CssClass="button-y" Text="Update" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                                                <asp:Button ID="btnClose" runat="server" CssClass="button-n" Text="Close" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                                    <%-- ReSharper disable once Asp.InvalidControlType --%>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                                        CancelControlID="btnClose" BackgroundCssClass="popup_bg">
                                    </ajaxToolkit:ModalPopupExtender>
                                </div>
                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup text-center">
                                            <tr>
                                                <td>
                                                    <h4>Are you sure you want to delete this?
                                                        <asp:Label ID="lblDelId" runat="server" Visible="False"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lnkDeleteNo" runat="server" CssClass="button-n" CausesValidation="false">No</asp:LinkButton>
                                                    &nbsp;&nbsp;
                                        <asp:LinkButton ID="lnkDeleteYes" runat="server" CssClass="button-y" CausesValidation="false" OnClick="lnkDeleteYes_Click">Yes</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:LinkButton ID="lnkTargetControl" runat="server" Style="display: none"></asp:LinkButton>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" BackgroundCssClass="popup_bg" runat="server" Enabled="true"
                                        CancelControlID="lnkDeleteNo" PopupControlID="Panel2" TargetControlID="lnkTargetControl">
                                    </ajaxToolkit:ModalPopupExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

