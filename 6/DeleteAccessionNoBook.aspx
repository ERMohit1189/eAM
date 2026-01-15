<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DeleteAccessionNoBook.aspx.cs"
    Inherits="admin_DeleteAccessionNoBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
           <script>
                
            </script>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>



            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Accession No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAccession" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display:none">
                                        <label class="control-label">Permanently</label>
                                        <div class="mgtp-6">
                                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Yes" CssClass="vd_checkbox checkbox-success" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button form-control-blue">Dispose</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 70px;"></div>
                                    </div>

                                </div>

                                <div class="col-sm-12  ">
                                    <div class="table-responsive  table-responsive2">
                                        <asp:Label ID="lblResults" runat="server" Style="color: #FF0000; font-weight: 700"></asp:Label>
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Accession No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("AccessionNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("DeleteBookYesno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("DeleteBookRemark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="LinkButton2" SkinID="Edit" runat="server" CssClass="linkbtn2edit" OnClick="LinkButton2_Click" Text='<%# Bind("Id") %>'></asp:LinkButton>--%>
                                                         <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit"  OnClick="LinkButton2_Click"
                                                            class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass=" menu-action" Width="50px" />
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
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                                <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" OnClick="Button8_Click" />
                                &nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" CssClass="button-y" OnClick="btnDelete_Click" Text="Yes" CausesValidation="False" />

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td >Accession No.</td>
                            <td>
                                <asp:TextBox ID="txtAccessNoPanel" runat="server" CssClass="form-control-blue validatetxt1" Width="200px" ReadOnly></asp:TextBox>
                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td >Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarkPanel" runat="server" CssClass="form-control-blue" TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >Recover
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox">
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2" style="text-align:center;">
                                <asp:LinkButton ID="LinkButton4" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" runat="server" OnClick="LinkButton4_Click" CssClass="button-y">Update</asp:LinkButton>
                                &nbsp;
                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" CssClass="button-n">Cancel</asp:LinkButton>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>


                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="LinkButton5" PopupControlID="Panel1"
                    TargetControlID="Button5" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
