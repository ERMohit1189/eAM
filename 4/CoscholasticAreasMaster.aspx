<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="CoscholasticAreasMaster.aspx.cs" Inherits="admin_CoscholasticAreasMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12  ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-12  no-padding">
                                                <div class="col-sm-4 half-width-50 mgbt-xs-15 hide">
                                                    <label class="control-label">Mode of Entry&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="mgtp-6">
                                                        <asp:DropDownList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal" 
                                                            CssClass="vd_radio radio-success" RepeatLayout="Flow">
                                                            <asp:ListItem>Single Class</asp:ListItem>
                                                            <asp:ListItem Selected="True">Multiple Class</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                                    <label class="control-label">From Class</label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpclass" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                                    <label class="control-label">To Class</label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpclass0" runat="server" CssClass="form-control-blue" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Medium</label>
                                                    <div class="">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="drpmedium" runat="server"
                                                                    SkinID="ddDefault" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drpmedium_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <div class="text-box-msg">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="<--Select-->" Display="Dynamic" SetFocusOnError="true" ControlToValidate="drpmedium" ValidationGroup="a"
                                                                        runat="server" ErrorMessage="*" CssClass="imp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Part</label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpPart" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drpPart_SelectedIndexChanged">
                                                            <asp:ListItem Text="<--Select-->" Value="<--Select-->"></asp:ListItem>
                                                            <asp:ListItem Value="1">Part 1 (For Coscholastic)</asp:ListItem>
                                                            <asp:ListItem Value="2">Part 2 (For Discipline)</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <div class="text-box-msg">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="<--Select-->" Display="Dynamic" SetFocusOnError="true" ControlToValidate="drpPart" ValidationGroup="a"
                                                                runat="server" ErrorMessage="*" CssClass="imp"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Coscholastic</label>
                                                    <div class="">
                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                                                Style="color: #CC0000" SetFocusOnError="True" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-8 half-width-50   mgbt-xs-15" style="padding-top:26px;">
                                                    <asp:LinkButton ID="lnkSubmit" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" runat="server" CssClass="button form-control-blue" OnClick="lnkSubmit_Click" ValidationGroup="a">Submit</asp:LinkButton>
                                                    <div id="msgbox" runat="server" style="left: 74px;"></div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 ">
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered ">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ClassId") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Medium">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Coscholastic Group">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("CoscholasticGroup") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Display Order">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelDO" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                                    <asp:TextBox ID="txtDO" runat="server" OnTextChanged="txtDO_TextChanged" AutoPostBack="true" Width="40" Text='<%# (Eval("DisplayOrder").ToString()==""?"0":Eval("DisplayOrder").ToString()) %>' class="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label36" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton IID="LinkButton2" runat="server" title="Edit" 
                                                                        OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" title="Delete" CausesValidation="False"
                                                                        data-placement="top" class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
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

                            <td>Coscholastic Group <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtCoscholasticPanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>

                        </tr>
                        <tr>
                            <td>Part<span class="vd_red">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlPartPanel" runat="server" CssClass="form-control-blue" ValidationGroup="b">
                                    <asp:ListItem Text="<--Select-->" Value="<--Select-->"></asp:ListItem>
                                    <asp:ListItem Value="1">Part 1 (For Coscholastic)</asp:ListItem>
                                    <asp:ListItem Value="2">Part 2 (For Discipline)</asp:ListItem>
                                </asp:DropDownList>

                                <div class="text-box-msg">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="<--Select-->" Display="Dynamic" SetFocusOnError="true" ControlToValidate="ddlPartPanel" ValidationGroup="b"
                                        runat="server" ErrorMessage="*" CssClass="imp"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CausesValidation="False" ValidationGroup="b" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                &nbsp; &nbsp;
                               <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblClassId0" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblSectionName0" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblMedium0" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button5" runat="server" Style="display: none" />
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
                                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" />

                                &nbsp;  &nbsp;
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

