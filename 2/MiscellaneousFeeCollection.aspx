<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MiscellaneousFeeCollection.aspx.cs" Inherits="admin_MiscellaneousFeeCollection" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>


            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding ">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDDate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDate_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
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
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DDYearTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDMonthTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDDateTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDDate_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button form-control-blue ">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 63px;"></div>
                                    </div>

                                </div>

                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel1" runat="server">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" data-toggle="tooltip" data-placement="top"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" data-toggle="tooltip" data-placement="top"><i class="fa fa-print "></i></asp:LinkButton>

                                                <script>
                                                    Sys.Application.add_load(tooltip);
                                                </script>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ImageButton1" />
                                            <asp:PostBackTrigger ControlID="ImageButton2" />
                                            <asp:PostBackTrigger ControlID="ImageButton3" />
                                            <asp:PostBackTrigger ControlID="ImageButton4" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>


                                <div class="col-sm-12  ">
                                    <div id="gdv" runat="server">

                                        <table id="abc" runat="server" width="100%">
                                            <tr>
                                                <td class="p-pad-2 text-center p-h-titel-box">
                                                    <div id="header" runat="server"></div>
                                                </td>
                                            </tr>
                                            <tr style="text-align: center;">
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Miscellaneous Fee Collection"></asp:Label>

                                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                    <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                                                    &nbsp;
                                                    <asp:Label ID="lblTitle1" runat="server"></asp:Label>
                                                    <asp:Label ID="lblDate1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" Width="100%"
                                                        AutoGenerateColumns="False" ShowFooter="True">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sr" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_srno" runat="server" Text='<%# Bind("Srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_studentName" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Receipt No.">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton6" runat="server" Text='<%# Bind("Receipt_no") %>' OnClick="LinkButton6_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_date" runat="server" Text='<%# Bind("FeeDepositeDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    Total
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmountTotal" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>Receipt No.
                            </td>
                            <td>
                                <asp:Label ID="Label34" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Name
                            </td>
                            <td>
                                <asp:Label ID="Label35" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Date
                            </td>
                            <td>
                                <asp:Label ID="Label36" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="Grid" Width="100%">
                                    <AlternatingRowStyle CssClass="alt" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sr" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Head Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_headName" runat="server" Text='<%# Bind("HeadName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="Button4" runat="server" CssClass="button-y" OnClick="Button4_Click" Text="View" />
                                &nbsp; &nbsp;
                    <asp:Button ID="Button3" runat="server" CssClass="button-n" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="Button5_ModalPopupExtender" runat="server" BackgroundCssClass="popup_bg" DynamicServicePath=""
                    Enabled="True" PopupControlID="Panel2" TargetControlID="Button5">
                </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
