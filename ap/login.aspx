<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>
<%-- ReSharper disable once AspUnusedRegisterDirectiveHighlighting --%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <title>eAM&reg; Login</title>

    <!-- Set the viewport width to device width for mobile -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />


    <!-- Fav and touch icons -->

    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="../img/ico/apple-touch-icon-114-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="../img/ico/apple-touch-icon-72-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" href="../img/ico/apple-touch-icon-57-precomposed.png" />
    <link rel="shortcut icon" href="../img/ico/favicon.png" />


    <!-- CSS -->

    <!-- Bootstrap & FontAwesome & Entypo CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <link href="../css/font-entypo.css" rel="stylesheet" type="text/css" />
    <link href="../css/animate.css" rel="stylesheet" />
    <!-- Fonts CSS -->
    <link href="../css/fonts.css" rel="stylesheet" type="text/css" />

    <!-- Plugin CSS -->
    <link href="../plugins/jquery-ui/jquery-ui.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../plugins/prettyPhoto-plugin/css/prettyPhoto.css" rel="stylesheet" type="text/css" />
    <link href="../plugins/isotope/css/isotope.css" rel="stylesheet" type="text/css" />
    <link href="../plugins/pnotify/css/jquery.pnotify.css" media="screen" rel="stylesheet" type="text/css" />
    <link href="../plugins/google-code-prettify/prettify.css" rel="stylesheet" type="text/css" />


    <link href="../plugins/mCustomScrollbar/jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css" />
    <link href="../plugins/tagsInput/jquery.tagsinput.css" rel="stylesheet" type="text/css" />
    <link href="../plugins/bootstrap-switch/bootstrap-switch.css" rel="stylesheet" type="text/css" />
    <link href="../plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />
    <link href="../plugins/bootstrap-timepicker/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../plugins/colorpicker/css/colorpicker.css" rel="stylesheet" type="text/css" />

    <!-- Specific CSS -->


    <!-- Theme CSS -->
    <link href="../css/theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/chrome.css" rel="stylesheet" type="text/chrome" />
    <!-- chrome only css -->



    <!-- Responsive CSS -->
    <link href="../css/theme-responsive.min.css" rel="stylesheet" type="text/css" />




    <!-- for specific page in style css -->

    <!-- for specific page responsive in style css -->


    <!-- Custom CSS -->
    <link href="../custom/custom.css" rel="stylesheet" type="text/css" />



    <!-- Head SCRIPTS -->
    <script type="text/javascript" src="../js/modernizr.js"></script>
    <script type="text/javascript" src="../js/mobile-detect.min.js"></script>
    <script type="text/javascript" src="../js/mobile-detect-modernizr.js"></script>
    <script src="../js/MyScript.js"></script>
    <script src="../js/jquery.min.js"></script>
    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.min.js"></script>--%>
    <style>
        .login-layout .vd_login-page {
            margin: 9% auto 60px !important;
        }

        .hower_underline:hover {
            text-decoration: underline;
        }
    </style>
</head>
<%-- backbg--%>
<body runat="server" id="pages" class="full-layout no-nav-left no-nav-right nav-top-fixed background-login responsive remove-navbar login-layout clearfix" data-active="pages" data-smooth-scrolling="1">

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script type="text/javascript">
            //
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
            prm.add_beginRequest(BeginRequestHandler);
            // Raised after an asynchronous postback is finished and control has been returned to the browser.
            prm.add_endRequest(EndRequestHandler);
            function BeginRequestHandler() {
                //Shows the modal popup - the update progress
                var wait = document.getElementById('wait');
                wait.style.visibility = 'visible';
                var button = document.getElementById('LinkButton1');
                button.disabled = true;
                button.value = "";
            }

            function EndRequestHandler() {
                //Hide the modal popup - the update progress
                var wait = document.getElementById('wait');
                wait.style.visibility = 'hidden';
                var button = document.getElementById('LinkButton1');
                button.disabled = false;
                button.value = "Login";
            }
        </script>

        <!-- Header Start -->

        <!-- Header Ends -->
        <div class="content">
            <div class="container">

                <script>
                    $("[id*=txtUserName]", "[id*=txtPassword]").keypress(function (event) {
                        var keycode = (event.keyCode ? event.keyCode : event.which);
                        if (keycode == '13') {
                            $("[id*=LinkButton1]").click();
                        }
                    });
                    function scrollup() {
                        var alertsuccess = document.getElementById('<%= alert_success.ClientID %>');
                        var alertdanger = document.getElementById('<%= alert_danger.ClientID %>');
                        alertsuccess.className = "alert alert-success vd_hidden animated bounce";
                        alertdanger.className = "alert alert-danger vd_hidden animated bounce";
                        setTimeout(displaynone, 2000);
                    }
                    function displaynone() {
                        var alertsuccess = document.getElementById('<%= alert_success.ClientID %>');
                        var alertdanger = document.getElementById('<%= alert_danger.ClientID %>');
                        alertsuccess.style.display = "none";
                        alertdanger.style.display = "none";
                    }
                    function blockDisplay() {
                        setTimeout(scrollup, 20000);
                    }

                </script>

                <div class="vd_login-page">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <script>
                                Sys.Application.add_load(blockDisplay);
                            </script>
                            <div class="alert alert-danger vd_hidden" id="alert_danger" runat="server">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>

                                <span class="vd_alert-icon"><i class="fa fa-exclamation-circle vd_red"></i></span>
                                <strong>Sorry!</strong>
                                <asp:Label ID="lblError" runat="server" Text="You have entered incorrect Username or Password."></asp:Label>
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
                                    <asp:Label ID="lbllog" runat="server" Text="" Font-Size="12px"></asp:Label>
                                </strong>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="panel widget">
                        <div class="panel-body" style="border-radius: 10px;">
                            <div class="login-img entypo-icon">
                                <div class="main-logo-center">
                                    <img src="../img/logo.png" alt="eAM logo" />
                                </div>
                            </div>
                            <asp:Panel ID="panel1" runat="server" DefaultButton="LinkButton1">
                                <div class="form-group  mgbt-xs-20">
                                    <div class="col-md-12">
                                        <div class="sr-only">
                                            <label class="control-label" for="email">Email</label>
                                        </div>
                                        <div class="vd_input-wrapper vd_input-margin">
                                            <span class="menu-icon"><i class="fa fa-user"></i></span>
                                            <asp:TextBox ID="txtUserName" placeholder="Mobile No." runat="server" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"
                                                onFocus="this.select();" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                        </div>
                                        <div class="">
                                            <label class="control-label sr-only" for="password">Password</label>
                                        </div>
                                        <div class="vd_input-wrapper vd_input-margin">
                                            <span class="menu-icon"><i class="fa fa-key"></i></span>
                                            <asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password"
                                                CssClass="form-control-blue validatetxt" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="vd_input-wrapper vd_input-margin hide">
                                            <span class="menu-icon"><i class="fa fa-map-marker"></i></span>
                                            <asp:DropDownList ID="DrpBranchName" runat="server" CssClass="select-box form-control-blue">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="vd_input-wrapper vd_input-margin hide">
                                            <span class="menu-icon"><i class="fa fa-calendar"></i></span>
                                            <asp:DropDownList ID="DrpSessionName" runat="server" CssClass="select-box form-control-blue">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div id="vd_login-error" class="alert alert-danger hidden"><i class="fa fa-exclamation-circle fa-fw"></i>Please fill in the necessary field(s) </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-xs-6 no-mgpd hide">
                                                    <div class="vd_checkbox pull-left mgtp-5">
                                                        <asp:CheckBox ID="chkRememberMe" runat="server" />

                                                        <label for="chkRememberMe" style="left: -5px !important">Remember me</label>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 no-mgpd">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');"
                                                        class="button loginbtn width-100" OnClick="LinkButton1_Click"><i class="fa fa-sign-in" aria-hidden="true"></i>&nbsp;&nbsp;Log In</asp:LinkButton>
                                                    <i class="append-icon icon-picasa fa-spin mgr-09" id="wait" style="visibility: hidden; color: #fff !important;" runat="server"></i>
                                                </div>




                                            </div>
                                            <div class="col-md-12 mgbt-xs-10 mgtp-0 hide" style="padding-top: 0px; margin-bottom: 0px !important;">
                                                For Help and Support:
                                                                            <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                                <asp:Label runat="server" ID="lblsupport"></asp:Label>
                                            </div>
                                            <div class="col-md-12 mgbt-xs-10 mgtp-0">
                                                <div id="msgbox" class="alertmsg" runat="server" style="left: -10px !important; width: 100%; margin-top: 10px;"></div>
                                            </div>
                                            <div class="register-panel text-center font-semibold">
                                                Don't have an account?
                                                                        <asp:LinkButton ID="lnkFake" runat="server" OnClick="lnkFake_OnClick" Style="text-decoration: underline; color: #23709e !important;">Click here to register<span class="menu-icon"></span></asp:LinkButton>
                                            </div>
                                            <div class="register-panel text-center font-semibold">
                                                <asp:HyperLink ID="LinkButton2" runat="server" NavigateUrl="~/ap/default.aspx">Click here to go back<span class="menu-icon"></span></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 text-center mgbt-xs-5 hide">
                                        <div class="forget-link pull-left">
                                            <a class="postlink-color hower_underline" style="color: #23709e !important;" href="forgot-password.aspx">Forgot Password? </a>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="form-group">
                                <div class="col-md-12 text-center mgbt-xs-5 text-center" style="padding-left: 13px; padding-right: 13px; text-align: left !important;">
                                    <i>By clicking Login, you agree to our <a href="Termsand_Conditions.aspx" style="color: #23709e; cursor: pointer;" class="hower_underline">Terms</a>
                                        and have read and acknowledged our <a href="Privacy_Policy.aspx" style="color: #23709e; cursor: pointer;" class="hower_underline">Privacy Policy,</a>&nbsp;<span>
                                            <a href="Refund_Policy.aspx" style="color: #23709e; cursor: pointer;" class="hower_underline">Refund Policy</a></span>,&nbsp;and&nbsp;
                                        <span><a href="Product_Services_Pricing.aspx" style="color: #23709e; cursor: pointer;" class="hower_underline">Fee Structure.</a></span>&nbsp; 
                                        <a href="Contact_Us.aspx" style="color: #23709e; cursor: pointer;" class="hower_underline">Contact Us</a>&nbsp; for details.</i>

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
                        <div>
                        </div>
                    </div>
                    <!-- Panel Widget -->
                    <%--<div class="text-center font-semibold">
                        <div class="form-group">
                            <div class="col-md-12 text-center mgbt-xs-5 text-center" style="background: #edeef2; border-radius: 4px;">
                                <span><a href="Refund_Policy.aspx" style="color: #000; cursor: pointer;">Refund Policy</a>&nbsp|&nbsp<a href="Product_Services_Pricing.aspx" style="color: #000; cursor: pointer;">Product/Services Pricing</a>&nbsp|&nbsp<a href="Contact_Us.aspx" style="color: #000; cursor: pointer;">Contact Us</a></span>
                                <br />
                                <p class="login-footer-title vd_black-new" style="color: #000  !important; border-top:1px solid #000;">
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>--%>
                </div>

                <!-- vd_login-page -->


                <!-- Middle Content End -->

            </div>
            <!-- .container -->
        </div>
        <!-- .content -->




    </form>
    <script>
        function btt() {
            $("[id*=LinkButton1]").addClass("aspNetDisabled button loginbtn width-100");
        }
    </script>

    <!-- .vd_body END  -->
    <a id="back-top" href="#" data-action="backtop" class="vd_back-top visible"><i class="fa  fa-angle-up"></i></a>
    <!--
  
    <!-- Javascript =============================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script type="text/javascript" src="../js/jquery.js"></script>

    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <script type="text/javascript" src='../plugins/jquery-ui/jquery-ui.custom.min.js'></script>
    <script type="text/javascript" src="../plugins/jquery-ui-touch-punch/jquery.ui.touch-punch.min.js"></script>

    <script type="text/javascript" src="../js/caroufredsel.js"></script>
    <script type="text/javascript" src="../js/plugins.js"></script>

    <script type="text/javascript" src="../plugins/breakpoints/breakpoints.js"></script>
    <script type="text/javascript" src="../plugins/dataTables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../plugins/prettyPhoto-plugin/js/jquery.prettyPhoto.js"></script>

    <script type="text/javascript" src="../plugins/mCustomScrollbar/jquery.mCustomScrollbar.concat.min.js"></script>
    <script type="text/javascript" src="../plugins/tagsInput/jquery.tagsinput.min.js"></script>
    <script type="text/javascript" src="../plugins/bootstrap-switch/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="../plugins/blockUI/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../plugins/pnotify/js/jquery.pnotify.min.js"></script>

    <script type="text/javascript" src="../js/theme.js"></script>
    <script type="text/javascript" src="../custom/custom.js"></script>
</body>
</html>
