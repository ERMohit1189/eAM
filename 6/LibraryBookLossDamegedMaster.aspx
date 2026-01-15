<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LibraryBookLossDamegedMaster.aspx.cs" Inherits="admin_LibraryBookLossDamegedMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
     <script>
                
            </script>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>




            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="divinsert" runat="server">



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Damage Category  &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtDC" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Damage Amount &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <div class="col-xs-9 no-padding">
                                                <asp:TextBox ID="txtDA" runat="server" CausesValidation="false" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            </div>
                                            <div class="col-xs-3 no-padding">
                                                <asp:DropDownList ID="drpAmountin" runat="server" CssClass="form-control-blue">
                                                    <asp:ListItem Value="%">%</asp:ListItem>
                                                    <asp:ListItem Value="Rs.">Rs.</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stock Minus</label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpStockMinus" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');" CssClass="button form-control-blue" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px;"></div>

                                    </div>


                                </div>
                                <div class="col-sm-12 ">
                                    <asp:GridView ID="grd1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-head-border table-bordered">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="vd_bg-blue text-center vd_white" Width="50px" />
                                                <ItemStyle CssClass="text-center " />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Damage Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDC" runat="server" Text='<%# Bind("DamageCategory") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Damage Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDA" runat="server" Text='<%# Bind("DamageAmountnew") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Stock Minus">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockminus" runat="server" Text='<%# Eval("Stockminus").ToString()=="True"?"Yes":"No" %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Bind("id") %>'></asp:Label>
                                                    <asp:LinkButton ID="lnkEdit" title="Edit"  runat="server" class="btn menu-icon vd_bd-yellow vd_yellow" OnClick="lnkEdit_Click"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="vd_bg-blue text-center vd_white" Width="50px" />
                                                <ItemStyle CssClass="text-center menu-action" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" title="Delete"  class="btn menu-icon vd_bd-red vd_red" OnClick="lnkDelete_Click"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="vd_bg-blue text-center vd_white" Width="50px" />
                                                <ItemStyle CssClass="text-center menu-action" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup ">

                                            <tr>
                                                <td>DAMAGE CATEGORY</td>
                                                <td>
                                                    <asp:TextBox ID="txtDCPanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>DAMAGE AMOUNT</td>
                                                <td>
                                                    <div class="col-xs-8 no-padding tab-in">
                                                        <asp:TextBox ID="txtDAPanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                    </div>
                                                    <div class="col-xs-4 no-padding tab-in">
                                                        <asp:DropDownList ID="drpAmountinPanel" runat="server" CssClass="form-control-blue">
                                                            <asp:ListItem Value="%">%</asp:ListItem>
                                                            <asp:ListItem Value="Rs.">Rs.</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Stock Minus</td>
                                                <td>
                                                     <asp:DropDownList ID="drpStockMinusPanel" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>REMARK</td>
                                                <td><asp:TextBox ID="txtRemarkPanel" runat="server" CssClass="form-control-blue" TextMode="MultiLine" Rows="1"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>   <asp:LinkButton ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click" OnClientClick="return ValidateTextBox('.validatetxt1');" CssClass="button-y">Update</asp:LinkButton>
                                                &nbsp;&nbsp;
                                         <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n">Cancel</asp:LinkButton></td>
                                            </tr>
                                           
                                        </table>

                                       
                                    </asp:Panel>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="LinkButton1" PopupControlID="Panel1"
                                        CancelControlID="lnkCancel" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                                    </ajaxToolkit:ModalPopupExtender>
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
                                                <td class="text-center">
                                                    <asp:LinkButton ID="lnkNo" runat="server" CssClass="button-n">No</asp:LinkButton>
                                                    &nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkyes" runat="server" CssClass="button-y" OnClick="lnkyes_Click">Yes</asp:LinkButton>


                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton>
                                    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" Enabled="True" TargetControlID="LinkButton2"
                                        PopupControlID="Panel2" CancelControlID="lnkNo" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
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

