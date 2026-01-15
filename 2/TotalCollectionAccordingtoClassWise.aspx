<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TotalCollectionAccordingtoClassWise.aspx.cs"
    Inherits="admin_Total_CollectionAccordingtoClassWise" %>

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
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpClass" runat="server" OnSelectedIndexChanged="drpClass_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select User&nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue "></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="FromYY" runat="server" OnSelectedIndexChanged="FromYY_SelectedIndexChanged" CssClass="form-control-blue col-xs-4"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromMM" runat="server" OnSelectedIndexChanged="FromMM_SelectedIndexChanged" CssClass="form-control-blue col-xs-4"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromDD" runat="server" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ToYY" runat="server" OnSelectedIndexChanged="ToYY_SelectedIndexChanged" CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToMM" runat="server" OnSelectedIndexChanged="ToMM_SelectedIndexChanged" CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToDD" runat="server" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-8  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Mode of Payment&nbsp;<span class="vd_red">*</span></label>
                                        <div class="controls mgtp-6">
                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="vd_radio radio-success"
                                                OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem Selected="True">Cash</asp:ListItem>
                                                <asp:ListItem>Cheque</asp:ListItem>
                                                <asp:ListItem>DD</asp:ListItem>
                                                <asp:ListItem>Online</asp:ListItem>
                                                <asp:ListItem>Card</asp:ListItem>
                                                <asp:ListItem>Bank Deposit</asp:ListItem>
                                                <asp:ListItem>NEFT/RTGS</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="pull-left">
                                                <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 59px;"></div>

                                            </div>
                                        </asp:Panel>
                                    </div>

                                </div>

                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel3" runat="server">
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
                                    <div class=" table-responsive  table-responsive2" id="divExport" runat="server">
                                        <table runat="server" id="abc" class="table  no-bm no-p-b-table">
                                            <tr>
                                                <td class="p-pad-2 text-center p-h-titel-box">
                                                    <div id="header" runat="server"></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="p-pad-1 text-center p-h-titel-box">
                                                    <asp:Label ID="Label1" runat="server" Text="Fee Collection (Class wise)"></asp:Label>

                                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                    <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Names="Verdana"></asp:Label>

                                                    <asp:Label ID="lblTitle1" runat="server"></asp:Label>
                                                    <asp:Label ID="lblDate1" runat="server" Font-Bold="True" Font-Names="Verdana"></asp:Label>

                                                    <asp:Label ID="Label10" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="gdv" runat="server">
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="false" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField>

                                                                    <ItemTemplate>
                                                                        <table style="width: 100%" class="table-header-group">
                                                                            <tbody>
                                                                                <tr class="text-center">
                                                                                    <td><span><b>Class</b></span>
                                                                                        <asp:Label ID="lblclass" runat="server" Text='<%# Bind("Class") %>'></asp:Label></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>

                                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowFooter="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                                            <AlternatingRowStyle CssClass="alt" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="#">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblReceptNo" runat="server" Text='<%# Bind("RecieptSrNo") %>' CssClass="hide"></asp:Label>
                                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        &nbsp;
                                                                                    </FooterTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Installment">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="FeeMonth" runat="server" Text='<%# Bind("FeeMonth") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Session">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("SessionName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Deposit Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label33" runat="server" Text='<%# Bind("FeeDepositeDate") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Mode">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label34s" runat="server" Text='<%# Bind("MOP") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Fees">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label34d" runat="server" Text='<%# Bind("ActualAmount") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Conveyance">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label34a" runat="server" Text='<%# Bind("BusConvience") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("RecievedAmount") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                            </Columns>

                                                                        </asp:GridView>
                                                                        <table style="width: 100%; display: none;">
                                                                            <tr>
                                                                                <td class="p-pad-0 no-tb">
                                                                                    <asp:Label ID="lblNotfound" Visible="false" runat="server" Style="color: #0033CC"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <table class="mp-table" style="width: 100%">
                                                                            <tr>
                                                                                <td class="text-right p-pad-32">Grand Total : &nbsp;
                                                                                <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                    <ItemStyle CssClass="p-pad-0 p-tot-tit no-tb" />
                                                                    <FooterStyle CssClass="p-pad-0 p-tot-tit no-tb" />
                                                                </asp:TemplateField>
                                                            </Columns>

                                                        </asp:GridView>
                                                    </div>
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





            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                <ContentTemplate>
                    <asp:Label ID="Label32" runat="server" Style="color: #CC0000; font-weight: 700"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
