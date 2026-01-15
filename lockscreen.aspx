<%@ Page Language="C#" MasterPageFile="~/blank.master" AutoEventWireup="true" CodeFile="lockscreen.aspx.cs" Inherits="lockscreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <style>
        .login-layout .vd_login-page {
            margin: 9% auto 60px !important;
        }
    </style>
    <div class="vd_body">
        <!-- Header Start -->

        <!-- Header Ends -->
        <div class="content">
            <div class="container">

                <!-- Middle Content Start -->

                <div class="vd_login-page">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="alert alert-danger vd_hidden" id="alert_danger" runat="server">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>

                                <span class="vd_alert-icon"><i class="fa fa-exclamation-circle vd_red"></i></span><strong>Error!</strong> You have entered incorrect Password.
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="alert alert-success vd_hidden" id="alert_success" runat="server">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>
                                <span class="vd_alert-icon"><i class="fa fa-check-circle vd_green"></i></span><strong>
                                    <asp:Label ID="lbllog" runat="server" Text="" CssClass="font-12"></asp:Label></strong>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="panel widget">
                        <div class="panel-body" style="border-radius: 10px;">
                            <div class="login-img entypo-icon">
                                <div class="main-logo-center">
                                    <img src="img/logo.png" alt="eAM logo" />
                                </div>
                            </div>
                            <div class="login-icon no-bd">
                                <asp:Image ID="imgUser" class="img-circle" runat="server" Style="width: 120px; height: 105px; margin-top: -24px;" alt="avatar" />
                            </div>


                            <div class="alert alert-danger vd_hidden">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>
                                <span class="vd_alert-icon"><i class="fa fa-exclamation-circle vd_red"></i></span><strong>Oh snap!</strong> Change a few things up and try submitting again.
                            </div>
                            <div class="alert alert-success vd_hidden">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>
                                <span class="vd_alert-icon"><i class="fa fa-check-circle vd_green"></i></span><strong>Well done!</strong>.
                            </div>
                            <div class="form-group  ">
                                <div class="col-md-12 mgbt-xs-20">
                                    <div class="">
                                        <label class="control-label sr-only" for="password">Password</label>
                                    </div>
                                    <div class="vd_input-wrapper" id="password-input-wrapper">
                                        <span class="menu-icon"><i class="fa fa-lock"></i></span>

                                        <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Password" onFocus="this.select();" CssClass="form-control-blue validatetxt" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div id="vd_login-error" class="alert alert-danger hidden"><i class="fa fa-exclamation-circle fa-fw"></i>Please fill the necessary field </div>
                            <div class="form-group">
                                <div class="col-md-12 text-center mgbt-xs-10">
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');"
                                        class="button loginbtn width-100" OnClick="LinkButton1_Click" Width="291px"><i class="fa fa-sign-in" aria-hidden="true"></i>&nbsp;&nbsp;Log In</asp:LinkButton>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12 text-center ">
                                    <div class="text-center font-semibold">
                                        <asp:HyperLink runat="server" ID="logout" NavigateUrl="default.aspx"><i class="fa fa-sign-out"></i>&nbsp;Log Out<span class="menu-icon"><i class="fa fa-angle-double-right fa-fw"></i></span></asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12 text-center mgbt-xs-5 text-center" style="margin-bottom: 0px !important;">
                                    <p class="login-footer-title vd_black-new" runat="server" id="lblCompanyName" visible="false" style="color: #333  !important; border-top: 1px solid #333;">
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Widget -->

                </div>
                <!-- vd_login-page -->



                <!-- Middle Content End -->

            </div>
            <!-- .container -->
        </div>
        <!-- .content -->
    </div>
    <script>
        function btt() {
            $("[id*=LinkButton1]").addClass("aspNetDisabled button loginbtn width-100");
        }
    </script>
</asp:Content>

