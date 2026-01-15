<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="fuel_type.aspx.cs"
    Inherits="admin_fuel_type" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fuel Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue validatetxt" placeholder="e.g. CNG"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please enter fuel type."
                                                    ValidationGroup="a" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>

                                </div>


                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fuel Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FuelType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit Fule" 
                                                            OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" title="Delete" data-toggle="tooltip"
                                                            data-placement="top" class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
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

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>Fuel Type 
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass=" form-control-blue validatetxt1"></asp:TextBox>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Remark 
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" Height="50px" Width="200px" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="LinkButton4" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" runat="server" OnClick="LinkButton4_Click" CssClass="button-y">Update</asp:LinkButton>
                                &nbsp; &nbsp;
                                                        <asp:LinkButton ID="LinkButton5" runat="server" CssClass="button-n">Cancel</asp:LinkButton>
                                <asp:Button ID="Button9" runat="server" Style="display: none" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button9"
                    PopupControlID="Panel1" CancelControlID="LinkButton5" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel2" runat="server">
                    <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">
                        <table class="tab-popup text-center">
                            <tr>
                                <td>
                                    <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                        <asp:Button ID="Button7" runat="server" Style="display: none" />
                                    </h4>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Button ID="Button8" runat="server" CausesValidation="False" OnClick="Button8_Click" Text="No" CssClass="button-n" />
                                    &nbsp;&nbsp;  
                                                            <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" CssClass="button-y" />

                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="Panel3_ModalPopupExtender" runat="server" CancelControlID="Button8" DynamicServicePath=""
                        Enabled="True" PopupControlID="Panel3" TargetControlID="Button7" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
