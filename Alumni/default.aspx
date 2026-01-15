<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="defaultAlumani" %>

<!DOCTYPE html>
<%-- ReSharper disable once AspUnusedRegisterDirectiveHighlighting --%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <title>eAM&reg; Login</title>

    <!-- Set the viewport width to device width for mobile -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="shortcut icon" href="../img/ico/favicon.png" />

    <!-- Bootstrap & FontAwesome & Entypo CSS -->
    <link href="~/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <link href="~/css/font-entypo.css" rel="stylesheet" type="text/css" />
    <link href="~/css/animate.css" rel="stylesheet" />
    <!-- Fonts CSS -->
    <link href="~/css/fonts.css" rel="stylesheet" type="text/css" />

    <!-- Theme CSS -->
    <link href="~/css/theme.min.css" rel="stylesheet" type="text/css" />
    <link href="~/css/chrome.css" rel="stylesheet" type="text/chrome" />

    <!-- Responsive CSS -->
    <link href="~/css/theme-responsive.min.css" rel="stylesheet" type="text/css" />
    <!-- Custom CSS -->
    <link href="~/custom/custom.css" rel="stylesheet" type="text/css" />

    <!-- Head SCRIPTS -->
    <script src="../js/MyScript.js" s></script>
    <script src="../js/jquery.min.js"></script>
    <style>
        .login-layout .vd_login-page {
            margin: 30% auto 60px !important;
        }

        .hower_underline:hover {
            text-decoration: underline;
        }

        .vd_checkbox label {
            color: #fff;
        }

        body {
            display: grid;
            place-items: center;
            color: white;
        }

        .main__bg {
            background-image: linear-gradient(-60deg, rgba(255, 216, 0, 0.54) 50%, #00d135 50%);
            animation: slide 3s ease-in-out infinite alternate;
            position: fixed;
            top: 0;
            bottom: 0;
            left: -50%;
            right: -50%;
            opacity: 0.5;
            z-index: -1;
        }

        .layer1 {
            animation-direction: alternate-reverse;
            animation-duration: 4s;
        }

        .layer2 {
            animation-duration: 5s;
        }

        @keyframes slide {
            0% {
                transform: translateX(-25%);
            }

            100% {
                transform: translateX(25%);
            }
        }
        .btn {
            background: transparent;
            width: 200px;
            position: relative;
            padding: 7px;
            color: #1ECD97;
            cursor: pointer;
            text-align: center;
            text-transform: uppercase;
            letter-spacing: 3px;
            transition: all 500ms cubic-bezier(0.6, -0.28, 0.735, 0.045);
            border-radius: 4px;
            font-weight: 600;
            overflow: hidden;
            border: 2px solid #1ECD97;
            text-decoration: none;
        }
    </style>

</head>
<%-- backbg--%>
<body runat="server" id="pages" class="full-layout no-nav-left no-nav-right nav-top-fixed background-login responsive remove-navbar login-layout clearfix" data-active="pages" data-smooth-scrolling="1">
    <div class="main__bg"></div>
    <div class="main__bg layer1"></div>
    <div class="main__bg layer2"></div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script type="text/javascript">
            //document.addEventListener('contextmenu', event => event.preventDefault());
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
                    <div class="panel widget" style="background: #00000047;">
                        <div class="" runat="server" id="Expbody" visible="false" style="border-radius: 10px;">
                            <div class="form-group  mgbt-xs-20">
                                <div class="alert alert-danger text-center" id="alert" runat="server" style="font-size: 15px; padding: 4px;"></div>
                            </div>
                        </div>
                        <div class="panel-body" runat="server" style="border-radius: 10px;">
                            <asp:HiddenField runat="server" ID="hdnSubscription" />
                            <div class="login-img entypo-icon">
                                <div class="main-logo-center">
                                    <img src="../../img/logo.png" alt="eAM logo" />
                                </div>
                            </div>
                            <asp:Panel ID="panel1" runat="server" DefaultButton="LinkButton1">
                                <div class="form-group  mgbt-xs-20">
                                    <div class="col-md-12">
                                        <div class="vd_input-wrapper vd_input-margin">
                                            <span class="menu-icon"><i class="fa fa-user"></i></span>
                                            <asp:TextBox ID="txtUserName" placeholder="Username (Registered Contact No.)" runat="server"
                                                onFocus="this.select();" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                        </div>
                                        <div class="">
                                            <label class="control-label sr-only" for="password">Password</label>
                                        </div>
                                        <div class="vd_input-wrapper vd_input-margin">
                                            <span class="menu-icon"><i class="fa fa-key"></i></span>
                                            <asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password"
                                                CssClass="form-control-blue validatetxt" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div id="vd_login-error" class="alert alert-danger hidden"><i class="fa fa-exclamation-circle fa-fw"></i>Please fill in the necessary field(s) </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="">
                                                <div class="col-xs-6 no-mgpd">
                                                    <div class="pull-left mgtp-5">
                                                        <asp:CheckBox ID="chkRememberMe" runat="server" style="color: #000; margin-left:15px;" />
                                                        <label for="" style="left: 8px !important; color: #fff;">Remember me</label>
                                                    </div>
                                                </div>
                                                <div class="col-xs-6 no-mgpd">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');"
                                                                class="btn" OnClick="LinkButton1_Click" Style="width: 162px !important;"><i class="fa fa-sign-in" aria-hidden="true"></i>&nbsp;&nbsp;Log In</asp:LinkButton>
                                                            <i class="append-icon icon-picasa fa-spin mgr-09" id="wait" style="visibility: hidden; color: #fff !important;" runat="server"></i>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 text-center mgbt-xs-5">
                                        <div class="forget-link pull-left">
                                            <a class=" hower_underline" style="color: #fff !important;" href="forgot-password.aspx">Forgot Password? </a>

                                        </div>
                                    </div>
                                    <div class="col-md-6 text-center mgbt-xs-5">
                                        <div class="forget-link pull-right">
                                            <a class="hower_underline" style="color: #fff !important;" href="RegisterNow.aspx">Register Now </a>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Javascript =============================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/theme.js"></script>
    <script type="text/javascript" src="../custom/custom.js"></script>
    <script>
        $(document).ready(function () {
            $('input:text, input:password').bind('copy paste cut', function (e) {
                e.preventDefault();
            });
        });
    </script>
</body>
</html>
