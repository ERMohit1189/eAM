<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/administrato_root-manager.master" AutoEventWireup="true" CodeFile="CreateSupperAdmin.aspx.cs"
    Inherits="CreateSupperAdmin" %>

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
                                <div class="col-lg-12 no-padding">
                                    <div class="col-sm-4   half-width-50 mgbt-xs-9">
                                        <label class="control-label">Name &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-9">
                                        <label class="control-label">Father's Name &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control-blue validatetxt" ></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-9">
                                        <label class="control-label">Contact No. &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control-blue validatetxt"   MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-9">
                                        <label class="control-label">Email &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-blue validatetxt" onBlur="ValidateEmails(this);"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-9">
                                        <label class="control-label">Username &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="TextUserId" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-9">
                                        <label class="control-label">Password &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control-blue validatetxt" TextMode="Password"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-9">
                                        <label class="control-label">Display Name &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-9">
                                        <label class="control-label">Status &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button">Submit</asp:LinkButton>
                                         <div id="msgbox" runat="server" style="left: 75px;"></div>

                                    </div>
                                   
                                </div>

                                <div class="col-lg-12 no-padding">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Email" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Display Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DisplayName" runat="server" Text='<%# Bind("DisplayName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="UserId" runat="server" Text='<%# Bind("UserId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Password">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2IsActive" runat="server" Text='<%# Bind("IsActive") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="UserIdE" runat="server" Text='<%# Bind("UserId") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit " 
                                                            OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="UserIdD" runat="server" Text='<%# Bind("UserId") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
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
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup text-center">

                        <tr>
                            <td>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="Button7" runat="server" Style="display: none" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" OnClick="Button8_Click" />

                                &nbsp; &nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="button-y" OnClick="btnDelete_Click" Text="Yes" />

                            </td>
                        </tr>



                    </table>

                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
            </div>
            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup">

                        <tr>
                            <td align="right" width="35%">Name
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPanelName" runat="server" CssClass="form-control-blue validatetxt1" Width="200px"></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="35%">Father's Name
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPanelFatherName" runat="server" CssClass="form-control-blue validatetxt1" Width="200px"></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="35%">Contact No.
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPanelContactNo" runat="server" CssClass="form-control-blue validatetxt1"  MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);" Width="200px"></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="35%">Email
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPanelEmail" runat="server" CssClass="form-control-blue validatetxt1"  onBlur="ValidateEmails(this);" Width="200px"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Style="display: none" />
                               
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="35%">Password
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPanelPassword" runat="server" CssClass="form-control-blue validatetxt1" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top !important">Display Name&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPanelDisplayName" runat="server" CssClass="form-control-blue" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top !important">Status&nbsp;
                            </td>
                            <td align="left">
                                            <asp:DropDownList ID="ddlPanelStatus" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td align="left" style="padding-left: 5px;">
                                <asp:Button ID="Button1" runat="server" CssClass="button-n" Text="Cancel" OnClick="Button1_Click" />
                                &nbsp;
                                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CssClass="button-y">Update</asp:LinkButton>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button3"
                    PopupControlID="Panel1" CancelControlID="Button1" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
