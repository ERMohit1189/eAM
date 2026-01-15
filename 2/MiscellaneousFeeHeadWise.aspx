<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MiscellaneousFeeHeadWise.aspx.cs" Inherits="MiscellaneousFeeHeadWise" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .style1 {
            width: 136px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(tooltip);
            </script>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12 no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class From&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpClassFrom" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpClassFrom_SelectedIndexChanged"
                                                        CssClass="form-control-blue drpValidate">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpClassTo" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpClassTo_SelectedIndexChanged"
                                                        CssClass="form-control-blue drpValidate">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Head&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpFeeHead" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpFeeHead_SelectedIndexChanged" CssClass="form-control-blue drpValidate">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Amount&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAmt" runat="server" onblur="decimalOrNumeric(this);" Text="0.00" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15" style="padding-top:23px;">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>

                                        <asp:LinkButton ID="LinkSubmit" runat="server" OnClientClick="ValidateDropdown('.drpValidate');ValidateTextBox('.validatetxt'); return validationReturn();" OnClick="LinkSubmit_Click" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                                    </ContentTemplate>
                                            </asp:UpdatePanel>

                                    </div>

                                </div>

                                <div class="col-sm-12  ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                    ShowFooter="true" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                <asp:Label ID="lblClassId" runat="server" Visible="false" Text='<%# Bind("Classid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Fee Head">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("FeeHead") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Fee Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmt" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkEdit" runat="server" title="Edit " data-toggle="tooltip" data-placement="top"
                                                                    OnClick="LinkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinDelete" runat="server" OnClick="LinkDelete_Click"
                                                                    title="Delete" data-toggle="tooltip" data-placement="top"
                                                                    class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
                            <td>Fee Amount :</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtFeePaymentPanelAmt" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                <asp:Button ID="BtnUpdate" runat="server" CausesValidation="False" CssClass="button-y" OnClick="BtnUpdate_Click" Text="Update" />
                                &nbsp;
                                <asp:Button ID="BtnEditCancel" runat="server" CausesValidation="False" CssClass="button-n" OnClick="BtnEditCancel_Click" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1" CancelControlID="BtnEditCancel" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
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
                            <td style="text-align: center;">
                                <asp:Button ID="BtnDeleteCancel" runat="server" Text="No" OnClick="BtnDeleteCancel_Click" CssClass="button-n" CausesValidation="False" />
                                &nbsp;&nbsp; 
                                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="button-y" Text="Yes" CausesValidation="False" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True"
                    TargetControlID="Button7" PopupControlID="Panel2"
                    CancelControlID="BtnDeleteCancel" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>



            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>

