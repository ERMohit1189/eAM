<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SmsReport.aspx.cs"
    Inherits="SuperAdmin_SmsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                try {
                    Sys.Application.add_load(datetime);
                }
                catch (ex) {

                }
                function alertmsg() {
                    alert(
                        "It looks like you are not connected to the Internet.\nPlease check your Internet connection and try again.");
                }
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-6  ">
                                        <div class="" style="float: left;">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" RepeatLayout="Flow " CssClass="vd_radio radio-success"
                                                OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">SMS Report</asp:ListItem>
                                                <asp:ListItem Value="2">Check Balance</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding" id="panel1" runat="server" visible="false" style="padding-top:20px !important;">
                                    <div class="col-sm-1 ">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" ValidationGroup="a" CssClass="button form-control-blue">View</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3  half-width-50">
                                        <asp:TextBox runat="server" ID="lblCheckAPIBalance" CssClass="form-control-blue" Enabled="false" Style="font-size: 18px; border: 0; border-bottom: 1px solid; height: 33px; float: left; font-family: cursive;"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-8  half-width-50">
                                        <asp:Label ID="message" runat="server" class="control-label text-danger"></asp:Label>
                                    </div>
                                    
                                </div>
                                <div class="col-sm-12  no-padding" id="panel12" runat="server" visible="true">
                                    <div class="col-sm-3 ">
                                        <asp:Label ID="lblChqDate" runat="server" class="control-label">From</asp:Label>
                                        <div class="">
                                            <asp:TextBox runat="server" ID="txtFromDate" CssClass="datepicker-normal"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 ">
                                        <asp:Label ID="Label1" runat="server" class="control-label">To</asp:Label>
                                        <div class="">
                                            <asp:TextBox runat="server" ID="txtToDate" CssClass="datepicker-normal"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 " style="padding-top: 24px;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button form-control-blue">View</asp:LinkButton>

                                    </div>

                                </div>
                                <div class="col-sm-12">
                                    <br />
                                </div>
                                <div class="col-sm-12">
                                    <div id="msgbox" runat="server" style="left: 74px;"></div>
                                </div>
                                <div class="col-sm-12" runat="server" id="lblStatus">
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
