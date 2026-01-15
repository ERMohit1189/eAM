<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="state_master.aspx.cs"
    Inherits="state_master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function EndRequestHandler(sender, args) {
            scrollTo(0, 0);
        }
    </script>

    <asp:UpdatePanel ID="upp" runat="server">
        <ContentTemplate>
              <script>
                  
                  Sys.Application.add_load(scrollbar);
    </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Country &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="col-sm-12 form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                <asp:ListItem>India</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="TextBox1" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please enter state."
                                                    Style="color: #CC0000" SetFocusOnError="True" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark</label>
                                        <div class=" ">
                                            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" runat="server" OnClick="LinkButton1_Click" CssClass="button" ValidationGroup="a">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px !important;"></div>
                                    </div>

                                </div>
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CountryName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
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
                                                <td>Country <span class="vd_red">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCountry0" runat="server" CssClass="form-control-blue" Enabled="false"></asp:DropDownList>
                                                    <asp:Button ID="Button1" runat="server" Style="display: none" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>State <span class="vd_red">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStatePanel" runat="server" CssClass="form-control-blue validatetxt1" Enabled="false"></asp:TextBox>
                                                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Remark
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="2"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button-y" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" OnClick="Button3_Click" Text="Update" />
                                                    &nbsp;
                                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                     <div id="Div1" runat="server" style="left: 75px !important;"></div>
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
                                                <td align="center">
                                                    <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                        <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CssClass="button-n" />
                                                    &nbsp; &nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
