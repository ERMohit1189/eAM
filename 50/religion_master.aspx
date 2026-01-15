<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="religion_master.aspx.cs"
    Inherits="religion_master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Religion &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtReligion" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReligion" ErrorMessage="Please enter religion."
                                                    Style="color: #CC0000" SetFocusOnError="True" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark </label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button form-control-blue" ValidationGroup="a">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px !important;"></div>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <div class="table-responsive  table-responsive2">
                                            <asp:GridView ID="Grd" runat="server" CssClass="table table-striped table-hover no-bm no-head-border pro-table table-bordered" AutoGenerateColumns="False">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px"  />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Religion">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ReligionName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("ReligionRemark") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label36" runat="server" Text='<%# Bind("ReligionId") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" title="Edit Religion"  OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px"  />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label37" runat="server" Text='<%# Bind("ReligionId") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                                title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="grid_heading_default" />
                                                <RowStyle CssClass="grid_details_default" />
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                            <table class="tab-popup">
                                                <tr>
                                                    <td>Religion <span class="vd_red">*</span>
                                                    </td>
                                                    <td class="controls">
                                                        <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                        <asp:TextBox ID="txtReligionPanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Remark
                                                    </td>
                                                    <td class="controls">
                                                        <asp:TextBox ID="txtRemarkPanel" runat="server" CssClass="form-control-blue" TextMode="MultiLine" row="2"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="Button3" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" runat="server" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                                        &nbsp;
                                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                                            CancelControlID="Button4" BackgroundCssClass="popup_bg">
                                        </asp:ModalPopupExtender>
                                    </div>

                                    <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">

                                            <table class="tab-popup text-center">

                                                <tr>
                                                    <td>
                                                        <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                            <asp:Button ID="Button7" runat="server" CssClass="button " Style="display: none" />
                                                        </h4>
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


                                        </asp:Panel>
                                        <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                                            PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                                        </asp:ModalPopupExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
