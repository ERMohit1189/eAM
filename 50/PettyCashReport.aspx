<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="PettyCashReport.aspx.cs"
    Inherits="PettyCashReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script src="../js/jquery.min.js"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                try {
                    Sys.Application.add_load(datetime);
                    
                }
                catch (ex) {

                }

            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-lg-12 col-md-12 col-sm-12   no-padding">

                                    <div class="col-sm-12   ">

                                        <div class="col-sm-4   mgbt-xs-15">
                                            <label class="control-label">From Date &nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:DropDownList ID="FromDDYears" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="FromDDYear_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="FromDDMonths" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="FromDDMonth_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="FromDDDates" runat="server" CssClass="form-control-blue col-sm-4" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="div8" runat="server">
                                            <asp:Label ID="gg" runat="server" class="control-label">To</asp:Label>
                                            <div class="">
                                                <asp:DropDownList ID="ToDDCYears" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="ToDDCYears_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ToDDMonths" runat="server" CssClass="form-control-blue col-sm-4" OnSelectedIndexChanged="ToDDMonths_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ToDDDates" runat="server" CssClass="form-control-blue col-sm-4"></asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4   mgbt-xs-15">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                                    <asp:DropDownList runat="server" ID="ddlBranch" CssClass="" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Select User Type&nbsp;<span class="vd_red"></span></label>
                                            <div class=" ">

                                                <asp:DropDownList ID="ddluserType" runat="server" AutoPostBack="True" CssClass="form-control-blue" OnSelectedIndexChanged="ddluserType_SelectedIndexChanged">
                                                    <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                    <asp:ListItem Value="2">Admin</asp:ListItem>
                                                    <asp:ListItem Value="3">Staff</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Select User&nbsp;<span class="vd_red"></span></label>
                                            <div class=" ">
                                                <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control-blue " OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4   mgbt-xs-15">
                                            <label class="control-label">Mode&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlMode" runat="server" CssClass="form-control-blue " AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged" onchange="MODChenge();">
                                                    <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                    <asp:ListItem>Cash</asp:ListItem>
                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                    <asp:ListItem>DD</asp:ListItem>
                                                    <asp:ListItem>Card</asp:ListItem>
                                                    <asp:ListItem>Online Transfer</asp:ListItem>
                                                    <asp:ListItem>Other</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-4 col-md-4 col-sm-4  btn-a-devices-2-p2  mgbt-xs-20">
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 74px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40px" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branch Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="BranchName" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="date" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Head">
                                                    <ItemTemplate>
                                                        <asp:Label ID="HeadType" runat="server" Text='<%# Bind("HeadType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PaymentMode" runat="server" Text='<%# Bind("PaymentMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Instrument Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ChequeDate" runat="server" Text='<%# Bind("ChequeDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Instrument No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ChequeNo" runat="server" Text='<%# Bind("ChequeNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reference Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="BankName" runat="server" Text='<%# Bind("BankName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Login Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br>
                                                        (<asp:Label ID="RecordDate" runat="server" Text='<%# Bind("RecordDate", "{0: dd-MMM-yyyy hh:mm:ss tt}") %>'></asp:Label>)
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Remark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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


        </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>
