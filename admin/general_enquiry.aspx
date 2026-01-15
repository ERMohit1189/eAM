<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="general_enquiry.aspx.cs"
    Inherits="admin_general_enquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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

                                    <div class="col-sm-4  half-width-50 ">
                                        <label class="control-label">Date &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpEnyear" runat="server" OnSelectedIndexChanged="drpEnyear_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="drpenmonth" runat="server" OnSelectedIndexChanged="drpenmonth_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="drpendate" runat="server" CssClass="form-control-blue col-xs-4 mgbt-xs-15">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject &nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtsubject" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtsubject" ErrorMessage="Can't leave blank!"
                                                    Style="color: #CC0000" SetFocusOnError="True" Display="Dynamic"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtnamead" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtnamead" ErrorMessage="Can't leave blank!"
                                                    Style="color: #CC0000" SetFocusOnError="True" Display="Dynamic"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtmobAdmiss" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="txtmobAdmiss" ErrorMessage="Can't leave blank!"
                                                    Style="color: #CC0000" SetFocusOnError="True" Display="Dynamic"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Alternate Contact No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtcontAdm" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">E-mail </label>
                                        <div class="">
                                            <asp:TextBox ID="txtemaAdmiss" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Country&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drCountry" runat="server" OnSelectedIndexChanged="drCountry_SelectedIndexChanged" CssClass="form-control-blue">
                                                <asp:ListItem>India</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">State&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drstate" runat="server" OnSelectedIndexChanged="drstate_SelectedIndexChanged" CssClass="form-control-blue">
                                                <asp:ListItem>India</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">City&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drcity" runat="server" OnSelectedIndexChanged="drcity_SelectedIndexChanged" CssClass="form-control-blue">

                                                <asp:ListItem>India</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Organization</label>
                                        <div class="">
                                            <asp:TextBox ID="txtorg" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Address</label>
                                        <div class="">
                                            <asp:TextBox ID="txtAddressAdmiss" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtreferenceaden" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-6-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');"
                                            OnClick="LinkButton1_Click" CssClass="button" ValidationGroup="a">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px !important;"></div>
                                    </div>

                                    <div class="col-sm-12   ">
                                        <div class="table-responsive2  table-responsive">
                                            <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-head-border table-bordered">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enquiry No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("EnquiryNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("MobileNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="E-mail ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("EMail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="City">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("CityName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" title="Edit Enquiry" 
                                                                OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> 
                                                                <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CausesValidation="False"
                                                                title="Delete"  class="btn menu-icon vd_bd-red vd_red">
                                                                <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="grid_heading_default" />
                                                <RowStyle CssClass="grid_details_default" />
                                            </asp:GridView>

                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                        <div class="col-sm-12 ">
                            <table class="tab-popup">
                                <tr>
                                    <td>Enquiry No. &nbsp;<span class="vd_red">*</span></td>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" ForeColor="#FF6600"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Date &nbsp;<span class="vd_red">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="drpYYPanel" runat="server" OnSelectedIndexChanged="drpYYPanel_SelectedIndexChanged"
                                            CssClass="form-control-blue col-xs-4 ">
                                        </asp:DropDownList>
                                        <asp:Button ID="Button5" runat="server" Style="display: none" />
                                        <asp:DropDownList ID="drpMMPanel" runat="server" OnSelectedIndexChanged="drpMMPanel_SelectedIndexChanged"
                                            CssClass="form-control-blue col-xs-4">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="drpDDPanel" runat="server" CssClass="form-control-blue col-xs-4 mgbt-xs-15">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Subject &nbsp;<span class="vd_red">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtSubjectPanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Name &nbsp;<span class="vd_red">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Contact No. &nbsp;<span class="vd_red">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtMobilePanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Alternate Contact No.</td>
                                    <td>
                                        <asp:TextBox ID="txtContactNoPanel" runat="server" CssClass="form-control-blue"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Email</td>
                                    <td>
                                        <asp:TextBox ID="txtEmailPanel" runat="server" CssClass="form-control-blue"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Country</td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpCountryPanel" runat="server" OnSelectedIndexChanged="drpCountryPanel_SelectedIndexChanged"
                                                    CssClass="form-control-blue">
                                                    <asp:ListItem>India</asp:ListItem>
                                                    <asp:ListItem>Other</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>State</td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpStatePanel" runat="server" OnSelectedIndexChanged="drpStatePanel_SelectedIndexChanged"
                                                    CssClass="form-control-blue">
                                                    <asp:ListItem>India</asp:ListItem>
                                                    <asp:ListItem>Other</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>City</td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpCityPanel" runat="server" CssClass="form-control-blue">
                                                    <asp:ListItem>India</asp:ListItem>
                                                    <asp:ListItem>Other</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Organization</td>
                                    <td>
                                        <asp:TextBox ID="txtOrg1" runat="server" CssClass="form-control-blue"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Address</td>
                                    <td>
                                        <asp:TextBox ID="txtAddPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Remark</td>
                                    <td>
                                        <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="Button3" runat="server" OnClientClick="return ValidateTextBox('.validatetxt1');"
                                            CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                        &nbsp;<asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                        <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
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
