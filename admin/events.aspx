<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="events.aspx.cs"
    Inherits="admin_events" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .style1 {
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList3" runat="server" SkinID="ddlSize0" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Contact Person&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox3" ErrorMessage="Can't leave blank!" Style="color: #CC0000" SetFocusOnError="True" Display="Dynamic"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Title&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Can't leave blank!"
                                                    Style="color: #CC0000" SetFocusOnError="True" Display="Dynamic"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Contact No. 1&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Contact No. 2</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Event Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem>Cultural</asp:ListItem>
                                                <asp:ListItem>Others</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList9" runat="server" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList10" runat="server" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">E-mail ID </label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">URL</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fax</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Description</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select&nbsp;<span class="vd_red">*</span></label>
                                        <div class="controls txt-middle">
                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" CssClass="vd_checkbox checkbox-success" RepeatLayout="Flow">
                                                <asp:ListItem>Admin</asp:ListItem>
                                                <asp:ListItem>Students</asp:ListItem>
                                                <asp:ListItem>Teacher</asp:ListItem>
                                            </asp:CheckBoxList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button" ValidationGroup="a">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px !important;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12   ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Event Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("EventType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Fromdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Todate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact Person">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("ContactPerson") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit Events" 
                                                            OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="grid_heading_default" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <RowStyle CssClass="grid_details_default" />
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
                            <td>Title <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTitleNamePanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td>Event Type <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpEventPanel" runat="server" CssClass="form-control-blue">
                                    <asp:ListItem>Cultural</asp:ListItem>
                                    <asp:ListItem>Others</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>From <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="drpFromYYPanel" runat="server" CssClass="form-control-blue col-xs-4">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="drpFromMMPanel" runat="server" CssClass="form-control-blue col-xs-4">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="drpFromDDPanel" runat="server" CssClass="form-control-blue col-xs-4">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>To <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="drpToYYPanel" runat="server" CssClass="form-control-blue col-xs-4">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="drpToMonthPanel" runat="server" CssClass="form-control-blue col-xs-4">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="drpToddPanel" runat="server" CssClass="form-control-blue col-xs-4">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>Contact Person <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtContactPerson1Panel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Contact No. 1 <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtContactNo1Panel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Contact No. 2
                            </td>
                            <td>
                                <asp:TextBox ID="txtContactNo2Panel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Description <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">
                                <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                &nbsp;
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="Button4" PopupControlID="Panel1"
                        TargetControlID="Button5" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td align="center">
                                <h4>Do you really want to delete this record?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" OnClick="Button8_Click" />

                                &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="button-y" Text="Yes" CausesValidation="False" />

                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
