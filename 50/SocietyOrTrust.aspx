<%@ Page Title="Admission Portal Date | eAM&#174;" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="SocietyOrTrust.aspx.cs"
    Inherits="SocietyOrTrust" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(datetime);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Name of Organization&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtOrganization" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Registration Number</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue" MaxLength="150"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Phone Number</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue " MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Email</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue" MaxLength="250" onBlur="ValidateEmails(this);"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Website</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue" MaxLength="150"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Logo</label>
                                        <div class="">
                                            <asp:FileUpload ID="TextBox5" runat="server"  CssClass="form-control-blue "></asp:FileUpload>
                                        </div>
                                    </div>
                                     <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Accreditation Logo</label>
                                        <div class="">
                                            <asp:FileUpload ID="TextBox6" runat="server" CssClass="form-control-blue "></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Signature</label>
                                        <div class="">
                                            <asp:FileUpload ID="TextBox7" runat="server" CssClass="form-control-blue "></asp:FileUpload>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Address of Organization&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-12  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                </div>
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Name of Organization">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Organization" runat="server" Text='<%# Bind("Organization") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Registration Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="RegistrationNumber" runat="server" Text='<%# Bind("RegistrationNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Phone Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PhoneNumber" runat="server" Text='<%# Bind("PhoneNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Email" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Website">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Website" runat="server" Text='<%# Bind("Website") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Logo">
                                                    <ItemTemplate>
                                                         <asp:Image ID="Logo" runat="server" ImageUrl='<%# Eval("Logo") %>' Width="100px" Height="100px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Accreditation Logo">
                                                    <ItemTemplate>
                                                       <asp:Image ID="AccreditationLogo" runat="server" ImageUrl='<%# Eval("AccreditationLogo") %>' Width="100px" Height="100px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Signature">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Signature" runat="server" ImageUrl='<%# Eval("Signature") %>' Width="100px" Height="100px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address of Organization">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Address" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" OnClick="Button8_Click" CausesValidation="False" />
                                &nbsp;&nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
            <Triggers>
        <asp:PostBackTrigger ControlID="LinkButton1" />
    </Triggers>
    </asp:UpdatePanel>


</asp:Content>
