<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="index" %>

<!DOCTYPE html>
<%-- ReSharper disable once AspUnusedRegisterDirectiveHighlighting --%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
     <meta charset="utf-8" />
    <title>eAM in Member Login</title>
    
    
    <!-- Set the viewport width to device width for mobile -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0">    
    
    
    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="img/ico/apple-touch-icon-144-precomposed.html">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="img/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="img/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="img/ico/apple-touch-icon-57-precomposed.png">
    <link rel="shortcut icon" href="img/ico/favicon.png">
    
    
    <!-- CSS -->
       
    <!-- Bootstrap & FontAwesome & Entypo CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <!--[if IE 7]><link type="text/css" rel="stylesheet" href="css/font-awesome-ie7.min.css"><![endif]-->
    <link href="css/font-entypo.css" rel="stylesheet" type="text/css">    

    <!-- Fonts CSS -->
    <link href="css/fonts.css"  rel="stylesheet" type="text/css">
               
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
    <!--[if IE]> <link href="css/ie.css" rel="stylesheet" > <![endif]-->
    <link href="css/chrome.css" rel="stylesheet" type="text/chrome"> <!-- chrome only css -->    


        
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
    <script src="admin/js/MyScript.js"></script>
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script type="text/javascript" src="js/html5shiv.js"></script>
      <script type="text/javascript" src="js/respond.min.js"></script>     
    <![endif]-->

   
</head>
<body id="pages" class="full-layout no-nav-left no-nav-right  nav-top-fixed background-login     responsive remove-navbar login-layout   clearfix" data-active="pages " data-smooth-scrolling="1">

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <script type="text/javascript">
             //
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
             prm.add_beginRequest(BeginRequestHandler);
             // Raised after an asynchronous postback is finished and control has been returned to the browser.
             prm.add_endRequest(EndRequestHandler);
             function BeginRequestHandler(sender, args) {
                 //Shows the modal popup - the update progress
                 var wait = document.getElementById('wait');
                 wait.style.visibility = 'visible';
                 var button = document.getElementById('LinkButton1');
                 button.disabled = true;
                 button.value = "";
             }

             function EndRequestHandler(sender, args) {
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

                            <div class="vd_content-wrapper">
                                <div class="vd_container">
                                    <div class="vd_content clearfix">
                                        <div class="vd_content-section clearfix">
                                            <div class="vd_login-page">
                                                <%--<div class="heading clearfix">
                <div class="logo">
                  <h2 class="mgbt-xs-5"><img src="img/logo.png" alt="logo"></h2>
                </div>
                <h4 class="text-center font-semibold vd_grey">LOGIN TO YOUR ACCOUNT</h4>
              </div>--%>
                                                <div class="panel widget">
                                                    <div class="panel-body">
                                                        <div class="login-img entypo-icon">
                                                            <%--  <i class="icon-key"></i> --%>
                                                            <img src="img/logo.png" alt="eAM logo" />

                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <div class="alert alert-danger vd_hidden" id="alert_danger" runat="server">
                                                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>

                                                                    <span class="vd_alert-icon"><i class="fa fa-exclamation-circle vd_red"></i></span><strong>Error!</strong> You have entered incorrect Username or Password.
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
                                                                    <span class="vd_alert-icon"><i class="fa fa-check-circle vd_green"></i></span><strong>You are logged in successfully.</strong>.
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                        <div class="form-group  mgbt-xs-20">
                                                            <div class="col-md-12">
                                                                <div class="label-wrapper sr-only">
                                                                    <%-- ReSharper disable once Html.IdNotResolved --%>
                                                                    <label class="control-label" for="email">Email</label>
                                                                </div>
                                                                <div class="vd_input-wrapper vd_input-margin">
                                                                    <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                    <asp:TextBox ID="txtUserName" placeholder="Username" runat="server" onFocus="this.select();" CssClass="form-control-blue ValidateTextBox"></asp:TextBox>

                                                                </div>
                                                                <%--<asp:Label ID="Label1" runat="server" Text="Label" CssClass="vd_red" Visible="false"></asp:Label>--%>
                                                                <div class="label-wrapper">
                                                                    <%-- ReSharper disable once Html.IdNotResolved --%>
                                                                    <label class="control-label sr-only" for="password">Password</label>
                                                                </div>
                                                                <div class="vd_input-wrapper vd_input-margin">
                                                                    <span class="menu-icon"><i class="fa fa-lock"></i></span>
                                                                    <asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password" CssClass="form-control-blue ValidateTextBox" runat="server" class="required"></asp:TextBox>

                                                                </div>
                                                                <%--<asp:Label ID="Label2" runat="server" Text="Label" CssClass="vd_red" Visible="false"></asp:Label>--%>

                                                                <div class="vd_input-wrapper vd_input-margin">
                                                                    <span class="menu-icon"><i class="fa fa-map-marker"></i></span>

                                                                    <asp:DropDownList ID="DrpBranchName" runat="server" class="required" CssClass="select-box form-control-blue">
                                                                    </asp:DropDownList>

                                                                </div>
                                                                <div class="vd_input-wrapper vd_input-margin">
                                                                    <span class="menu-icon"><i class="fa fa-calendar"></i></span>

                                                                    <asp:DropDownList ID="DrpSessionName" runat="server" class="required" CssClass="select-box form-control-blue">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="vd_login-error" class="alert alert-danger hidden"><i class="fa fa-exclamation-circle fa-fw"></i>Please fill the necessary field </div>
                                                        <div class="form-group">


                                                            <div class="col-md-12 text-center mgbt-xs-5">
                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Button ID="LinkButton1" runat="server" OnClientClick="return ValidateTextBox('.ValidateTextBox');" class="btn vd_bg-blue vd_white width-100" OnClick="LinkButton1_Click" Width="291px" Text="Login" />
                                                                        <i class="append-icon icon-picasa fa-spin mgr-09" id="wait" style="visibility: hidden" runat="server"></i>
                                                                        <%-- <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return ValidateTextBox('.ValidateTextBox');" class="btn vd_bg-blue vd_white width-100" OnClick="LinkButton1_Click" Width="291px">
                                                                            <i class="append-icon icon-picasa fa-spin mgr-10" id="wait" style="visibility: hidden" runat="server"></i>Login
                                                                        </asp:LinkButton>--%>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="row">
                                                                    <div class="col-xs-6">
                                                                        <div class="vd_checkbox">
                                                                            <asp:CheckBox ID="chkRememberMe" runat="server" />

                                                                            <%-- ReSharper disable once Html.IdNotResolved --%>
                                                                            <label for="chkRememberMe">Stay signed in</label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xs-6 text-right">
                                                                        <div class="forget-link"><a href="forgot-password.aspx">Forgot Password? </a></div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                                <!-- Panel Widget -->
                                                <div class="register-panel text-center font-semibold">
                                                    <p class="login-footer-title">
                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <!-- vd_login-page -->

                                        </div>
                                        <!-- .vd_content-section -->

                                    </div>
                                    <!-- .vd_content -->
                                </div>
                                <!-- .vd_container -->
                            </div>
                            <!-- .vd_content-wrapper -->

                            <!-- Middle Content End -->

                        </div>
                        <!-- .container -->
                    </div>
                    <!-- .content -->

                    <!-- Footer Start -->
                    <%--<footer class="footer-2"  id="footer">      
    <div class="vd_bottom ">
        <div class="container">
            <div class="row">
              <div class=" ">
                <div class="copyright text-center">
                  	Copyright &copy;2014 Venmond Inc. All Rights Reserved 
                </div>
              </div>
            </div><!-- row -->
        </div><!-- container -->
    </div>
  </footer>--%>
                    <!-- Footer END -->

                </div>
    
    </form>




    <!-- .vd_body END  -->
    <a id="back-top" href="#" data-action="backtop" class="vd_back-top visible"><i class="fa  fa-angle-up"></i></a>
    <!--
    <%-- ReSharper disable once Asp.Warning --%>
    <a class="back-top" href="#" id="back-top"> <i class="icon-chevron-up icon-white"> </i> </a> -->

    <!-- Javascript =============================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script type="text/javascript" src="js/jquery.js"></script>
    <!--[if lt IE 9]>
      <script type="text/javascript" src="js/excanvas.js"></script>      
    <![endif]-->
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
