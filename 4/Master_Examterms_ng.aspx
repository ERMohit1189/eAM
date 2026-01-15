<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Master_Examterms_ng.aspx.cs" Inherits="Master_Examterms_ng" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <script>
        Sys.Application.add_load(datetime);
    </script>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12   no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Exam Pattern&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlExamPattern" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="ddlExamPattern_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">No. Of Terms&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlNoofterms" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="ddlNoofterms_SelectedIndexChanged">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="1">One</asp:ListItem>
                                                <asp:ListItem Value="2">Two</asp:ListItem>
                                                <asp:ListItem Value="3">Three</asp:ListItem>
                                                <asp:ListItem Value="4">Four</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12   no-padding" runat="server" id="row_1" visible="false">

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Term1</label>
                                            <div class="">
                                                <asp:TextBox ID="txtTerm1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Start Date </label>
                                            <div class="">
                                                <asp:TextBox ID="txtT1StartDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">End Date </label>
                                            <div class="">
                                                <asp:TextBox ID="txtT1EndDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12   no-padding" runat="server" id="row_2" visible="false">

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Term2</label>
                                            <div class="">
                                                <asp:TextBox ID="txtTerm2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Start Date </label>
                                            <div class="">
                                                <asp:TextBox ID="txtT2StartDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">End Date </label>
                                            <div class="">
                                                <asp:TextBox ID="txtT2EndDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12   no-padding" runat="server" id="row_3" visible="false">

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Term3</label>
                                            <div class="">
                                                <asp:TextBox ID="txtTerm3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Start Date </label>
                                            <div class="">
                                                <asp:TextBox ID="txtT3StartDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">End Date </label>
                                            <div class="">
                                                <asp:TextBox ID="txtT3EndDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12   no-padding" runat="server" id="row_4" visible="false">

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Term4</label>
                                            <div class="">
                                                <asp:TextBox ID="txtTerm4" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Start Date </label>
                                            <div class="">
                                                <asp:TextBox ID="txtT4StartDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">End Date </label>
                                            <div class="">
                                                <asp:TextBox ID="txtT4EndDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12   no-padding">
                                        <div class="col-sm-4  half-width-50  btn-a-devices-2-p2  mgbt-xs-15">

                                            <asp:Button ID="lnkSave" runat="server" OnClick="lnkSave_Click" CssClass="button form-control-blue" ValidationGroup="a" Text="Submit" />
                                            <div id="msgbox" runat="server" style="left: 74px;"></div>

                                        </div>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pattern">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Pattern" runat="server" Text='<%# Bind("Pattern") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Term">
                                                        <ItemTemplate>
                                                            <asp:Label ID="term" runat="server" Text='<%# Bind("term") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Begin Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="beginDate" runat="server" Text='<%# Convert.ToDateTime(Eval("beginDate")).ToString("yyyy MMM dd") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="End Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="endDate" runat="server" Text='<%# Convert.ToDateTime(Eval("endDate")).ToString("yyyy MMM dd") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblIdForEdit" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkEdit" runat="server" title="Edit" 
                                                                OnClick="LinkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete"  Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblIdForDelete" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkDelete" runat="server" OnClick="LinkDelete_Click" title="Delete" data-toggle="tooltip"
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

                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                        <div class="col-sm-12   no-padding">

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Exam Pattern&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:DropDownList ID="ddlExamPattern0" runat="server" CssClass="form-control-blue" Enabled="false">
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12   no-padding">

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Term1</label>
                                <div class="">
                                    <asp:TextBox ID="txtTerm" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Start Date </label>
                                <div class="">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">End Date </label>
                                <div class="">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="tab-popup" style="text-align: right;">
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="LblIdForUpdate" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="btnUpdate" runat="server" CausesValidation="False" CssClass="button-y" OnClick="btnUpdate_Click" Text="Update" />
                                    &nbsp;
                           <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                        CancelControlID="Button4" BackgroundCssClass="popup_bg">
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
                                <td>
                                    <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" />
                                    &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CssClass="button-y" Text="Yes" CausesValidation="False" OnClick="btnDelete_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                        PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

