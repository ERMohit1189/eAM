<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="additionalFeesHead.aspx.cs" Inherits="additionalFeesHead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <%-- ==== in aspx file   --%>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                
            </script>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding ">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpFromClass" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpFromClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpToClass" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpToClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                        <div class="txt-middle">
                                            <asp:DropDownList ID="ddlGender" runat="server">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Male</asp:ListItem>
                                                <asp:ListItem>Female</asp:ListItem>
                                                <asp:ListItem>Transgender</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 mgbt-xs-15">
                                        <label class="control-label">Head Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="*"
                                                    SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 mgbt-xs-15">
                                        <label class="control-label">Amount&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="TextBox3" Display="Dynamic" ErrorMessage="*"
                                                    SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 mgbt-xs-15 hide">
                                        <label class="control-label">Exemption</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox5" runat="server" Text="0" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15  btn-a-devices-6-p6 mgt-xs-15 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" Style="margin-top: 26px;" OnClick="LinkButton1_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                </div>



                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered ">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        <asp:Label ID="lblSer" runat="server" Text='<%# Bind("Ids") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Head Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_headName" runat="server" Text='<%# Bind("HeadName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ClassName" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gender">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Gender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="Edit" runat="server" title="Edit" 
                                                            OnClick="Edit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="Delete" runat="server" OnClick="Delete_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
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
                        <td>Head Name <span class="vd_red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Amount  <span class="vd_red">*</span></td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>

                        </td>
                    </tr>
                    <tr class="hide">
                        <td>Exemption </td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control-blue"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="Button2" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CssClass="button-y" OnClick="Button2_Click" Text="Update" />
                            &nbsp;&nbsp;
                            <asp:Button ID="Button7" runat="server" CssClass="button-n" Text="Cancel" />
                            <asp:LinkButton ID="Button5" runat="server" Style="display: none">&nbsp;</asp:LinkButton>

                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="Button7" TargetControlID="Button5" PopupControlID="Panel1"
                BackgroundCssClass="popup_bg">
            </asp:ModalPopupExtender>
                 </div>
             <div style="overflow: auto; width: 1px; height: 1px">
            <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                <table class="tab-popup text-center">
                    <tr>
                        <td>
                            <h4>Are you sure you want to delete this?<asp:Label ID="Label3" runat="server"
                                Visible="False"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button4" runat="server" Text="No" CssClass="button-n" />
                            &nbsp;&nbsp; 
                            <asp:Button ID="Button3" runat="server" Text="Yes" CssClass="button-y"
                                OnClick="Button3_Click" />

                            <asp:LinkButton ID="Button6" runat="server" Style="display: none">&nbsp;</asp:LinkButton>

                        </td>

                    </tr>
                </table>
            </asp:Panel>
            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                CancelControlID="Button4"
                TargetControlID="Button6" PopupControlID="Panel2"
                BackgroundCssClass="popup_bg">
            </asp:ModalPopupExtender>
                 </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

