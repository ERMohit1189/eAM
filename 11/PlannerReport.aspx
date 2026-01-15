<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="PlannerReport.aspx.cs"
    Inherits="sp_PlannerReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-md-12">
                                    <asp:RadioButtonList ID="rbList" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbList_SelectedIndexChanged"
                                        RepeatLayout="Flow" CssClass="vd_radio radio-success">
                                        <asp:ListItem>Calendar View</asp:ListItem>
                                        <asp:ListItem Selected="True">Tabular View</asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>
                                <div class="col-sm-12  no-padding" id="div1" runat="server">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="FromYY" runat="server" OnSelectedIndexChanged="FromYY_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromMM" runat="server" OnSelectedIndexChanged="FromMM_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromDD" runat="server"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                        <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ToYY" runat="server" OnSelectedIndexChanged="ToYY_SelectedIndexChanged" CssClass="form-control-blue col-xs-4"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToMM" runat="server" OnSelectedIndexChanged="ToMM_SelectedIndexChanged" CssClass="form-control-blue col-xs-4"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToDD" runat="server" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                   

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">

                                        <asp:LinkButton ID="lnkShow" runat="server" OnClick="lnkShow_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px;"></div>
                                    </div>

                                </div>
                                <div class="col-sm-12  mgbt-xs-10" runat="server" id="divbtns" visible="false">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel1" runat="server">
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
                                    <div class="table-responsive  table-responsive2" id="mainDiv" runat="server">
                                        <div id="header" runat="server" style="width: 80%;"></div>
                                        <asp:GridView ID="Grd" runat="server"
                                            CssClass="table table-striped table-hover no-head-border table-bordered pro-table table-header-group" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center"
                                                        VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="From Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("FromDate1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"
                                                        CssClass="vd_bg-blue vd_white" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="To Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("ToDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"
                                                        CssClass="vd_bg-blue vd_white" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("PlannerName") %>'
                                                            ForeColor='<%# System.Drawing.ColorTranslator.FromHtml(Eval("Color").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Description") %>'
                                                            ForeColor='<%# System.Drawing.ColorTranslator.FromHtml(Eval("Color").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
