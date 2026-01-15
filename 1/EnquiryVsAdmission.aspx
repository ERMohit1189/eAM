<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EnquiryVsAdmission.aspx.cs"
    Inherits="_1.EnquiryVsAdmission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <script>

                
                Sys.Application.add_load(scrollbar);
                Sys.Application.add_load(datetime);
            </script>
            <style>
                .mp-table2 .tab-b-45 {
    width: 77% !important;
    padding: 2px !important;
}
            </style>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:TextBox ID="txtFromdate" runat="server" CssClass="form-control-blue validatetxt1 datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control-blue validatetxt1 datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:Button ID="btnView" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CssClass="button form-control-blue" OnClick="btnView_Click" Text="View" />
                                        <div id="msgView" runat="server" style="left: 75px">
                                        </div>
                                    </div>
                                </div>
                                <div class=" col-sm-12 " id="divDetails" runat="server" visible="false" style="padding-top: 40px;">
                                    <div class="col-sm-12  mgbt-xs-10" runat="server" id="divExport" visible="false">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                        title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                        title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                        title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                        title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

                                                    <script>
                                                        
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

                                    <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div id="gdv1" runat="server">
                                                        <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table">
                                                            <tr class="hide">
                                                                <td>
                                                                    <div id="header" runat="server" class="col-md-12 no-padding"></div>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                        <div class=" col-sm-12 no-padding text-center">
                                                                            <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;">Enquiry Vs. Admission</asp:Label><br />
                                                                            <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                        </div>
                                                                        <div class=" col-sm-6 no-padding" style="padding-top: 53px !important;">
                                                                            <div class=" table-responsive  table-responsive2">
                                                                                <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-head-border table-bordered">
                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Total Enquiry">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Enquiry" runat="server" Text='<%# Bind("AllEnquery") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Pending">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Pending" runat="server" Text='<%# Bind("PendingEnquery") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Form Issued">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="FormIssued" runat="server" Text='<%# Bind("FormIssuedEnquery") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Registered">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Registered" runat="server" Text='<%# Bind("RegisteredEnquery") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>


                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="grid_heading_default" />
                                                                                    <PagerSettings PageButtonCount="100" />
                                                                                    <RowStyle CssClass="grid_details_default" />
                                                                                </asp:GridView>
                                                                            </div>


                                                                        </div>
                                                                        <div class=" col-sm-6 no-padding">
                                                                            <div class="col-md-8 col-xs-8 no-padding ">
                                                                                <asp:Chart ID="Chart1" runat="server">
                                                                                    <Titles>
                                                                                        <asp:Title ShadowOffset="5" Name="Title1" />
                                                                                    </Titles>
                                                                                    <Legends>
                                                                                        <asp:Legend Alignment="Center" Docking="Bottom"
                                                                                            IsTextAutoFit="False" Name="Default"
                                                                                            LegendStyle="Row" />
                                                                                    </Legends>
                                                                                    <Series>
                                                                                        <asp:Series Name="Default" />
                                                                                    </Series>
                                                                                    <ChartAreas>
                                                                                        <asp:ChartArea Name="ChartArea1"
                                                                                            BorderWidth="0" />
                                                                                    </ChartAreas>
                                                                                </asp:Chart>
                                                                            </div>
                                                                            <div class="col-md-3 col-xs-3 no-padding ">
                                                                                <table style="width: 100%; margin-top: 100px;" class="mp-table2">

                                                                                    <tr class="vd_green">
                                                                                        <td class="tab-b-45 text-right" style="color: #020096 !important">Total Enquiry 
                                                                                        </td>
                                                                                        <td class="text-center tab-b-10">:
                                                                                        </td>
                                                                                        <td class="tab-b-45 text-right">
                                                                                            <asp:Label ID="lblEnquiry" ForeColor="#020096" runat="server"></asp:Label>
                                                                                    </tr>
                                                                                    <tr class="vd_yellow">
                                                                                        <td class="tab-b-45  text-right" style="color: #F89C2C !important">Pending 
                                                                                        </td>
                                                                                        <td class="text-center tab-b-10">:
                                                                                        </td>
                                                                                        <td class="tab-b-45 text-right">
                                                                                            <asp:Label ID="lblPending" ForeColor="#F89C2C" runat="server"></asp:Label>
                                                                                    </tr>
                                                                                    <tr class=" vd_red">
                                                                                        <td class="tab-b-45 text-right" style="color: #DA4448  !important">Form Issued </td>
                                                                                        <td class="text-center tab-b-10">:
                                                                                        </td>
                                                                                        <td class="tab-b-45 text-right">
                                                                                            <asp:Label ID="lblFormIssued" ForeColor="#DA4448 " runat="server"></asp:Label>
                                                                                    </tr>

                                                                                    <tr class=" vd_red">
                                                                                        <td class="tab-b-45 text-right" style="color: #1FAE66  !important">Registered</td>
                                                                                        <td class="text-center tab-b-10">:
                                                                                        </td>
                                                                                        <td class="tab-b-45 text-right">
                                                                                            <asp:Label ID="lblRegistered" ForeColor="#1FAE66 " runat="server"></asp:Label>
                                                                                        </td>

                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
