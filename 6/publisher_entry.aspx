<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="publisher_entry.aspx.cs"
    Inherits="publisher_entry" %>

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
                                        <label class="control-label">Publisher Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drppublisherCategory" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Publisher Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                        </div>
                                    </div>


                                    <div class="col-sm-4   half-width-50 mgbt-xs-9">
                                        <label class="control-label">Address</label>
                                        <div class="">
                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Phone 1</label>
                                        <div class="">
                                            <asp:TextBox ID="txtphone1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Phone 2</label>
                                        <div class="">
                                            <asp:TextBox ID="txtphoneno2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Mobile</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fax</label>
                                        <div class="">
                                            <asp:TextBox ID="txtFax" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Country</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="Drpcountry" runat="server" OnSelectedIndexChanged="Drpcountry_SelectedIndexChanged" AutoPostBack="True"
                                                        CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">State</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpstate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpstate_SelectedIndexChanged"
                                                        CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">City</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpCity" runat="server" AutoPostBack="True" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Pin</label>
                                        <div class="">
                                            <asp:TextBox ID="txtzip" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">E-mail</label>
                                        <div class="">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Website</label>
                                        <div class="">
                                            <asp:TextBox ID="txtwebsite" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" Rows="1" TextMode="MultiLine" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">

                                        <asp:LinkButton ID="LinkButton1" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" runat="server" SkinID="save" OnClick="LinkButton1_Click" ValidationGroup="z" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12  ">
                                    <div class=" table-responsive header  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered ">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue text-center vd_white" Width="50px" />
                                                    <ItemStyle CssClass="text-center " />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                    <ItemStyle CssClass="text-center " />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("PublisherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue text-left vd_white" />
                                                    <ItemStyle CssClass="text-left " />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                    <ItemStyle CssClass="text-center " />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="City">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                    <ItemStyle CssClass="text-center " />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                    <ItemStyle CssClass="text-center " />
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
                                       <%-- <div class="fl-scrolls fl-scrolls-hidden" style="width: 860px; left: 289.545px;"><div style="width: 1355px;"></div></div>--%>
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
                            <td>Category
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="lblCategory" Style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Publisher Name&nbsp;<span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtName0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Address
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress0" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Phone 1
                            </td>
                            <td>
                                <asp:TextBox ID="txtphone2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                            </tr>
                        <tr>
                            <td>Phone 2
                            </td>
                            <td>
                                <asp:TextBox ID="txtphoneno3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Mobile
                            </td>
                            <td>
                                <asp:TextBox ID="txtmobile0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                            </tr>
                        <tr>
                            <td>Fax
                            </td>
                            <td>
                                <asp:TextBox ID="txtFax0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Country
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="Drpcountry0" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="Drpcountry0_SelectedIndexChanged" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            </tr>
                        <tr>
                            <td>State
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="drpstate0" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpstate0_SelectedIndexChanged" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>City
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="drpCity0" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="drpCity0_SelectedIndexChanged" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            </tr>
                        <tr>
                            <td>Pin
                            </td>
                            <td>
                                <asp:TextBox ID="txtzip0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>E-mail
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                            </tr>
                        <tr>
                            <td>Website
                            </td>
                            <td>
                                <asp:TextBox ID="txtwebsite0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Remark
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtRemark0" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="LinkButton4"  OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();" runat="server" CausesValidation="False" OnClick="LinkButton4_Click"
                                    CssClass="button-y">Update</asp:LinkButton>
                                &nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" OnClick="LinkButton5_Click"
                            CssClass="button-n">Cancel</asp:LinkButton>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="Button9" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="LinkButton5" PopupControlID="Panel1"
                        TargetControlID="Button9" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>

                        </tr>

                        <tr>
                            <td align="center">
                                <asp:Button ID="Button8" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button8_Click" Text="No" />

                                &nbsp;  &nbsp; 
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button-y" OnClick="btnDelete_Click" Text="Yes" />

                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button8" DynamicServicePath=""
                        Enabled="True" PopupControlID="Panel2" TargetControlID="Button7" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>

                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
