<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TotalCollectionofFeeArrier.aspx.cs" Inherits="admin_TotalCollectionofFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>


    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">Select</label>
                                        <div class="txt-middle">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" CssClass="vd_radio radio-success" RepeatLayout="flow"
                                                RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">

                                                <asp:ListItem Selected="True">Date Wise</asp:ListItem>
                                                <asp:ListItem>Installment Wise</asp:ListItem>

                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-8  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Mode of Payment&nbsp;<span class="vd_red">*</span></label>
                                        <div class="txt-middle">
                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" CssClass="vd_radio radio-success" RepeatLayout="flow"
                                                RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem Selected="True">Cash</asp:ListItem>
                                                <asp:ListItem>Cheque</asp:ListItem>
                                                <asp:ListItem>DD</asp:ListItem>
                                                <asp:ListItem>Online</asp:ListItem>
                                                <asp:ListItem>Card</asp:ListItem>
                                                <asp:ListItem>Bank Deposit</asp:ListItem>
                                                <asp:ListItem>NEFT/RTGS</asp:ListItem>
                                                <asp:ListItem>Card</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding" id="Panel2" runat="server">


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Category</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpGroup_SelectedIndexChanged"
                                                        CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Installment</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownMonth" runat="server" AutoPostBack="True"
                                                        CssClass="form-control-blue" OnSelectedIndexChanged="DropDownMonth_SelectedIndexChanged"
                                                        SkinID="ddDefault" TabIndex="1">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label32" runat="server" Style="font-weight: 700; color: #CC0000"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding" id="Panel1" runat="server">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From Date</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="FromYY" runat="server"
                                                        OnSelectedIndexChanged="FromYY_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromMM" runat="server"
                                                        OnSelectedIndexChanged="FromMM_SelectedIndexChanged" SkinID="ddlSize1"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromDD" runat="server"
                                                        OnSelectedIndexChanged="FromDD_SelectedIndexChanged" SkinID="ddlSize1"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date </label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ToYY" runat="server"
                                                        OnSelectedIndexChanged="ToYY_SelectedIndexChanged" SkinID="ddlSize2"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToMM" runat="server"
                                                        OnSelectedIndexChanged="ToMM_SelectedIndexChanged" SkinID="ddlSize1"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToDD" runat="server"
                                                        OnSelectedIndexChanged="ToDD_SelectedIndexChanged" SkinID="ddlSize1"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" CssClass="button">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 58px;"></div>
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label33" runat="server" Style="font-weight: 700; color: #CC0000"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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

                                <div class="col-sm-12 ">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class=" table-responsive  table-responsive2" id="gdv" runat="server">
                                                <table id="abc" runat="server" class="table  no-bm no-p-b-table">
                                                    <tr>
                                                        <td class="p-pad-2 text-center p-h-titel-box">
                                                            <div id="header" runat="server"></div>
                                                        </td>
                                                    </tr>
                                                    <tr style="text-align:center;">
                                                        <td>
                                                            <asp:Label ID="lbltitel" runat="server" Text="Fee Collection (Session wise)"></asp:Label>

                                                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                            <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>

                                                            <asp:Label ID="lblTitle1" runat="server"></asp:Label>
                                                            <asp:Label ID="lblDate1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Small"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                                OnPageIndexChanged="GridView1_PageIndexChanged" ShowFooter="True" OnRowDataBound="GridView1_RowDataBound"
                                                                CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                                <AlternatingRowStyle CssClass="alt" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label24" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue-np text-center vd_white" VerticalAlign="Middle" Width="50px" />
                                                                        <ItemStyle CssClass="text-center " VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Class">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label21" runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue-np text-center vd_white" VerticalAlign="Middle" />
                                                                        <ItemStyle CssClass="text-center " VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Session">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSession" runat="server" Text='<%# Bind("SessionName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue-np text-center vd_white" VerticalAlign="Middle" />
                                                                        <ItemStyle CssClass="text-center " VerticalAlign="Middle" />
                                                                    </asp:TemplateField>



                                                                       <asp:TemplateField HeaderText="Arrier Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblArriemAmount" runat="server" Text='<%# Bind("ArrierAmount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <span>Arrier Total Amount : </span>
                                                                            <asp:Label ID="Label26011" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue-np vd_white" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        <ItemStyle CssClass=" " HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                    </asp:TemplateField>

                                                                 

                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label25" runat="server" Text='<%# Bind("RecievedAmount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <span>Total Amount : </span>
                                                                            <asp:Label ID="Label26" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="vd_bg-blue-np vd_white" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        <ItemStyle CssClass=" " HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle Height="30px" />
                                                                <HeaderStyle Height="35px" />
                                                                <RowStyle Height="28px" />
                                                            </asp:GridView>

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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

