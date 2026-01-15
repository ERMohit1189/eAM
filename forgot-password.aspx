<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgot-password.aspx.cs" Inherits="forgot_password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta charset="utf-8" />
    <title>Forgot Password | eAM</title>


    <!-- Set the viewport width to device width for mobile -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />


    <!-- Fav and touch icons -->

    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="img/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="img/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="img/ico/apple-touch-icon-57-precomposed.png">
    <link rel="shortcut icon" href="img/ico/favicon.png">


    <!-- CSS -->

    <!-- Bootstrap & FontAwesome & Entypo CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <link href="css/font-entypo.css" rel="stylesheet" type="text/css">

    <!-- Fonts CSS -->
    <link href="css/fonts.css" rel="stylesheet" type="text/css">

    <!-- Plugin CSS -->
    <link href="plugins/jquery-ui/jquery-ui.custom.min.css" rel="stylesheet" type="text/css">
    <link href="plugins/prettyPhoto-plugin/css/prettyPhoto.css" rel="stylesheet" type="text/css">
    <link href="plugins/isotope/css/isotope.css" rel="stylesheet" type="text/css">
    <link href="plugins/pnotify/css/jquery.pnotify.css" media="screen" rel="stylesheet" type="text/css">
    <link href="plugins/google-code-prettify/prettify.css" rel="stylesheet" type="text/css">


    <link href="plugins/mCustomScrollbar/jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css">
    <link href="plugins/tagsInput/jquery.tagsinput.css" rel="stylesheet" type="text/css">
    <link href="plugins/bootstrap-switch/bootstrap-switch.css" rel="stylesheet" type="text/css">
    <link href="plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css">
    <link href="plugins/bootstrap-timepicker/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css">
    <link href="plugins/colorpicker/css/colorpicker.css" rel="stylesheet" type="text/css">

    <!-- Specific CSS -->


    <!-- Theme CSS -->
    <link href="css/theme.min.css" rel="stylesheet" type="text/css">
    <link href="css/chrome.css" rel="stylesheet" type="text/chrome">
    <!-- chrome only css -->



    <!-- Responsive CSS -->
    <link href="css/theme-responsive.min.css" rel="stylesheet" type="text/css">




    <!-- for specific page in style css -->

    <!-- for specific page responsive in style css -->


    <!-- Custom CSS -->
    <link href="custom/custom.css" rel="stylesheet" type="text/css">



    <!-- Head SCRIPTS -->
    <script type="text/javascript" src="js/modernizr.js"></script>
    <script type="text/javascript" src="js/mobile-detect.min.js"></script>
    <script type="text/javascript" src="js/mobile-detect-modernizr.js"></script>

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <script src="js/MyScript.js"></script>

</head>
<body id="pages" class="full-layout no-nav-left no-nav-right nav-top-fixed background-login responsive login-layout clearfix " data-active="pages " data-smooth-scrolling="1">
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
        <div class="vd_body">
            <!-- Header Start -->

            <!-- Header Ends -->
            <div class="content">
                <div class="container">
                    <!-- Middle Content Start -->


                    <div class="vd_login-page" >
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="alert alert-danger vd_hidden" id="alert_danger" runat="server">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>

                                            <span class="vd_alert-icon"><i class="fa fa-exclamation-circle vd_red"></i></span><strong>Identification Failed!</strong> Username or Mobile No. not correct. Please contact Admin.
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
                                            <span class="vd_alert-icon"><i class="fa fa-check-circle vd_green"></i></span><strong>Password sent successfully.</strong>.
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

                               


                                <h4 class="text-center font-semibold vd_grey">Forgot Password</h4>

                             <%--   <div class="alert alert-danger vd_hidden">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>

                                    <span class="vd_alert-icon"><i class="fa fa-exclamation-circle vd_red"></i></span>
                                    <strong>Error! </strong>Identification Failed! Username or Mobile No. not correct. Pleage contact to Admin.

                                </div>--%>
                               <%-- <div class="alert alert-success vd_hidden">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>
                                    <span class="vd_alert-icon"><i class="fa fa-check-circle vd_green"></i></span><strong>Well done!</strong>.
                                </div>--%>

                                <div class="form-group  mgbt-xs-20">
                                    <div class="col-md-12">

                                        <div class="vd_input-wrapper vd_input-margin">
                                            <span class="menu-icon"><i class="fa fa-user"></i></span>
                                            <asp:TextBox ID="txtUsername" placeholder="Username" runat="server" onFocus="this.select();" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
<%--                                        <div class="vd_input-wrapper vd_input-margin">
                                            <span class="menu-icon"><i class="fa fa-map-marker"></i></span>

                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="select-box form-control-blue">
                                                <asp:ListItem Value="Kanpur Road "></asp:ListItem>
                                                <asp:ListItem Value="Gomti Nagar"></asp:ListItem>
                                                <asp:ListItem Value="Indira Nagar"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>--%>

                                        <div class="vd_input-wrapper vd_input-margin">
                                            <span class="menu-icon"><i class="fa fa-phone"></i></span>
                                            <asp:TextBox ID="txtContactno" placeholder="Registered Mobile No." runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                        </div>

                                    </div>
                                </div>
                                <div id="vd_login-error" class="alert alert-danger hidden"><i class="fa fa-exclamation-circle fa-fw"></i>Please fill the necessary field </div>
                                <div class="form-group">

                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-xs-6 text-left ">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="LinkButton1" class="button loginbtn width-100" 
                                                            OnClientClick="return ValidateTextBox('.validatetxt');" runat="server" Text="Send" OnClick="LinkButton1_Click" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-xs-6 text-center mgbt-xs-5">
                                                <a href="default.aspx" class="btn vd_bg-blue vd_white width-100">Cancel</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="row" style="margin:0;">
                                    <div class="text-center font-semibold" runat="server" id="divCompany" visible="false">
                                        <br />
                            <p class="login-footer-title" style="color: #333  !important; border-top:1px solid #333;">
                                
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </p>
                        </div>
                        </div>
                                </div>
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
    </form>


    <!-- .vd_body END  -->
    <a id="back-top" href="#" data-action="backtop" class="vd_back-top visible"><i class="fa  fa-angle-up"></i></a>
    <!--


    <!-- Javascript =============================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script type="text/javascript" src="js/jquery.js"></script>

    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src='plugins/jquery-ui/jquery-ui.custom.min.js'></script>
    <script type="text/javascript" src="plugins/jquery-ui-touch-punch/jquery.ui.touch-punch.min.js"></script>

    <script type="text/javascript" src="js/caroufredsel.js"></script>
    <script type="text/javascript" src="js/plugins.js"></script>

    <script type="text/javascript" src="plugins/breakpoints/breakpoints.js"></script>
    <script type="text/javascript" src="plugins/dataTables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="plugins/prettyPhoto-plugin/js/jquery.prettyPhoto.js"></script>

    <script type="text/javascript" src="plugins/mCustomScrollbar/jquery.mCustomScrollbar.concat.min.js"></script>
    <script type="text/javascript" src="plugins/tagsInput/jquery.tagsinput.min.js"></script>
    <script type="text/javascript" src="plugins/bootstrap-switch/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="plugins/blockUI/jquery.blockUI.js"></script>
    <script type="text/javascript" src="plugins/pnotify/js/jquery.pnotify.min.js"></script>

    <script type="text/javascript" src="js/theme.js"></script>
    <script type="text/javascript" src="custom/custom.js"></script>
</body>
</html>
