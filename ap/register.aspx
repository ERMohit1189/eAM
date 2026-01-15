<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Sign Up | eAM ®</title>
    <meta http-equiv="content-type" content="text/html;charset=UTF-8" />

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="<%# ResolveUrl("~/img/ico/apple-touch-icon-114-precomposed.png") %>" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="<%# ResolveUrl("~/img/ico/apple-touch-icon-72-precomposed.png") %>" />
    <link rel="apple-touch-icon-precomposed" href="<%# ResolveUrl("~/img/ico/apple-touch-icon-57-precomposed.png") %>" />
    <link rel="shortcut icon" href="<%# ResolveUrl("~/img/ico/favicon.png") %>">
    <script src="<%# ResolveUrl("~/js/MyScript.js") %>"></script>

    <!-- CSS -->

    <!-- Bootstrap & FontAwesome & Entypo CSS -->
    <link href="<%# ResolveUrl("~/css/bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/css/font-awesome.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/css/font-entypo.css") %>" rel="stylesheet" type="text/css" />

    <!-- Fonts CSS -->
    <link href="<%# ResolveUrl("~/css/fonts.css") %>" rel="stylesheet" type="text/css" />

    <!-- Plugin CSS -->
    <link href="<%# ResolveUrl("~/plugins/jquery-ui/jquery-ui.custom.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/prettyPhoto-plugin/css/prettyPhoto.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/isotope/css/isotope.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/pnotify/css/jquery.pnotify.css") %>" media="screen" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/google-code-prettify/prettify.css") %>" rel="stylesheet" type="text/css" />


    <link href="<%# ResolveUrl("~/plugins/mCustomScrollbar/jquery.mCustomScrollbar.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/tagsInput/jquery.tagsinput.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/bootstrap-switch/bootstrap-switch.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/daterangepicker/daterangepicker-bs3.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/bootstrap-timepicker/bootstrap-timepicker.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/colorpicker/css/colorpicker.css") %>" rel="stylesheet" type="text/css" />

    <link href="<%# ResolveUrl("~/css/summernote/summernote.css") %>" rel="stylesheet" type="text/css" />
    <!-- Specific CSS -->
    <link href="<%# ResolveUrl("~/plugins/jquery-file-upload/css/jquery.fileupload.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/jquery-file-upload/css/jquery.fileupload-ui.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/bootstrap-wysiwyg/css/bootstrap-wysihtml5-0.0.2.css") %>" rel="stylesheet" type="text/css" />

    <!-- Specific CSS -->
    <link href="<%# ResolveUrl("~/plugins/fullcalendar/fullcalendar.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/fullcalendar/fullcalendar.print.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%# ResolveUrl("~/plugins/introjs/css/introjs.min.css") %>" rel="stylesheet" type="text/css" />
    <!-- Specific CSS -->

    <!-- Theme CSS -->
    <link href="<%# ResolveUrl("~/css/theme.min.css") %>" rel="stylesheet" type="text/css" />

    <link href="<%# ResolveUrl("~/css/chrome.css") %>" rel="stylesheet" type="text/chrome" />
    <!-- chrome only css -->



    <!-- Responsive CSS -->
    <link href="<%# ResolveUrl("~/css/theme-responsive.min.css") %>" rel="stylesheet" type="text/css" />

    <link href="<%# ResolveUrl("~/css/summernote/summernote.css") %>" rel="stylesheet" />


    <!-- for specific page in style css -->

    <!-- for specific page responsive in style css -->


    <!-- Custom CSS -->
    <link href="<%# ResolveUrl("~/custom/custom.css") %>" rel="stylesheet" type="text/css" />

    <link href="<%# ResolveUrl("~/Smoke/smoke.css") %>" rel="stylesheet" />
    <script src="<%# ResolveUrl("~/Smoke/smoke.min.js") %>"></script>
    <script src="<%# ResolveUrl("~/Smoke/smoke.js") %>"></script>

    <!-- Head SCRIPTS -->
    <script type="text/javascript" src="<%# ResolveUrl("~/js/modernizr.js") %>"></script>
    <script type="text/javascript" src="<%# ResolveUrl("~/js/mobile-detect.min.js") %>"></script>
    <script type="text/javascript" src="<%# ResolveUrl("~/js/mobile-detect-modernizr.js") %>"></script>
    <script type="text/javascript" src="<%# ResolveUrl("~/js/modernizr.js") %>"></script>
    <script type="text/javascript" src="<%# ResolveUrl("~/js/MyScript.js") %>"></script>


    <style>
        .form-group {
            margin-right: -30px !important;
            margin-left: -30px !important;
            margin-bottom: 8px !important;
        }
    </style>
    <link href="<%# ResolveUrl("~/css/animate.css") %>" rel="stylesheet" />
</head>
<body runat="server" id="pages" class="full-layout no-nav-left no-nav-right nav-top-fixed background-login responsive remove-navbar login-layout clearfix" data-active="pages" data-smooth-scrolling="1" style="background-image: url(../Uploads/LoginWallpaper/eAM_Default_bg.png); background-size: 100%;">
    <form id="form2" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="vd_body">
                    <div class="content">
                        <div class="container">
                            <div class="">
                                <div class="vd_container">
                                    <div class="vd_content clearfix">
                                        <div class="clearfix">
                                            <div class="vd_login-page">
                                                <div class="panel widget">
                                                    <div class="panel-body" style="border-radius: 10px; padding: 10px 20px;">
                                                        <div class="login-img entypo-icon">
                                                            <div class="main-logo-center">
                                                                <img src="../img/logo.png" alt="eAM logo" />
                                                            </div>
                                                        </div>
                                                        <div class="form-horizontal">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <div class="col-md-12">
                                                                        <div class="vd_input-wrapper" id="last-name-input-wrapper">
                                                                            <span class="menu-icon"><i class="fa fa-phone-square"></i></span>
                                                                            <asp:TextBox ID="txtregmobile" runat="server" placeholder="Enter Mobile No"  MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-12">
                                                                        <div class="vd_input-wrapper" id="password-input-wrapper">
                                                                            <span class="menu-icon"><i class="fa fa-key"></i></span>
                                                                            <asp:TextBox ID="txtregpassword" runat="server" TextMode="Password" MaxLength="40" placeholder="Enter desired Password here" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-12">
                                                                        <div class="vd_input-wrapper" id="Confirmpassword-input-wrapper">
                                                                            <span class="menu-icon"><i class="fa fa-key"></i></span>
                                                                            <asp:TextBox ID="txtConfirmpassword" runat="server" TextMode="Password" MaxLength="40" placeholder="Enter Confirm Password here" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group hide">
                                                                    <div class="col-md-12">
                                                                        <div class="vd_input-wrapper">
                                                                            <span class="menu-icon"><i class="fa fa-map-marker"></i></span>
                                                                            <asp:DropDownList ID="DrpBranchName" runat="server" CssClass="select-box form-control-blue">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" style="margin-bottom: 0 !important;">
                                                                    <div class="col-md-12">
                                                                        <div id="vd_login-error" class="alert alert-danger hidden"><i class="fa fa-exclamation-circle fa-fw"></i>Please fill the necessary field </div>
                                                                        <div class="vd_checkbox hide">
                                                                            <asp:CheckBox ID="chktermsconditions" runat="server" class="vd_checkbox checkbox-success" Checked="true" Text="I agree to " />
                                                                            <a href="Terms.aspx" class="check-text-r" target="_blank">&nbsp;&nbsp; Terms & Conditions. </a>
                                                                        </div>
                                                                        
                                                                    </div>
                                                                    <div class="col-md-12 text-center mgbt-xs-5">
                                                                        <asp:LinkButton ID="LinkButton1" CssClass="btn vd_bg-blue vd_white width-100" runat="server" OnClick="LinkButton1_OnClick" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn(this);"><i class="fa fa-paper-plane"></i> &nbsp; Register</asp:LinkButton>
                                                                        <div class="text-box-msg">
                                                                            <div id="msg1" runat="server"></div>
                                                                            <div id="msgbox" class="alertmsg" runat="server" style="left: -10px !important; width: 300px; margin-top: 10px;"></div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 mgbt-xs-10 mgtp-0 hide" style="padding-top: 0px; margin-bottom: 0px !important;">
                                                                        For Help and Support:
                                                                    <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                                                       <asp:Label runat="server" ID="lblsupport"></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <div class="register-panel text-center font-semibold">
                                                                    Already have an account?&nbsp;<asp:LinkButton ID="linkLogin" runat="server" style="text-decoration: underline; color: #23709e !important;" OnClick="linkLogin_Click">Click here to Sign In</asp:LinkButton>
                                                                </div>
                                                                <div class="register-panel text-center font-semibold">
                                                                        <asp:HyperLink ID="LinkButton2" runat="server" NavigateUrl="~/ap/default.aspx">Click here to go back<span class="menu-icon"></span></asp:HyperLink>
                                                                    </div>
                                                            </div>
                                                            <div class="col-md-12 text-center mgbt-xs-5 text-center" style="padding-left: 13px; padding-right: 13px; text-align: left !important;">
                                                                    <i>By clicking Login, you agree to our <a href="Termsand_Conditions.aspx" style="color: #23709e; cursor: pointer;" class="hower_underline">Terms</a>
                                                                        and have read and acknowledged our <a href="Privacy_Policy.aspx" style="color: #23709e; cursor: pointer;" class="hower_underline">Privacy Policy,</a>&nbsp;<span>
                                                                            <a href="../Refund_Policy.aspx" style="color: #23709e; cursor: pointer;" class="hower_underline">Refund Policy</a></span>,&nbsp;and&nbsp;
                                        <span><a href="Product_Services_Pricing.aspx" style="color: #23709e; cursor: pointer;" class="hower_underline">Fee Structure.</a></span>&nbsp; 
                                        <a href="Contact_Us.aspx" style="color: #23709e; cursor: pointer;" class="hower_underline">Contact Us</a>&nbsp; for details.</i>

                                                                </div>
                                                            <div class="col-md-12 text-center mgbt-xs-5">
                                                                    <p class="login-footer-title vd_black-new" runat="server" id="lblCompanyName" visible="false" style="margin-bottom: 0px !important; color: #333  !important; border-top: 1px solid #333;">
                                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                    </p>
                                                                </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                            <table class="tab-popup">
                                <tr>
                                    <td>Enter OTP</td>
                                    <td>
                                        <asp:Button ID="Button7" runat="server" Style="display: none" />
                                        <asp:TextBox ID="txtotpno" runat="server" class="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center" colspan="2">
                                        <asp:Button ID="btnsignin" runat="server" CausesValidation="False" CssClass="button-y" OnClick="btnsignin_OnClick" Text="Sign in" />
                                        <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" Enabled="True" TargetControlID="Button7"
                            PopupControlID="Panel1" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <!-- .vd_body END  -->
    <a id="back-top" href="../#" data-action="backtop" class="vd_back-top visible"><i class="fa  fa-angle-up"></i></a>
    <script type="text/javascript" src='<%= ResolveClientUrl("~/js/jquery.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveClientUrl("~/js/bootstrap.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveClientUrl("~/plugins/jquery-ui/jquery-ui.custom.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveClientUrl("~/plugins/jquery-ui-touch-punch/jquery.ui.touch-punch.min.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveClientUrl("~/js/caroufredsel.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/js/plugins.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/breakpoints/breakpoints.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/dataTables/jquery.dataTables.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/prettyPhoto-plugin/js/jquery.prettyPhoto.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/mCustomScrollbar/jquery.mCustomScrollbar.concat.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/tagsInput/jquery.tagsinput.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/bootstrap-switch/bootstrap-switch.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/blockUI/jquery.blockUI.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/pnotify/js/jquery.pnotify.min.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveUrl("~/js/theme.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/custom/custom.js") %>'></script>

    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/bootstrap-timepicker/bootstrap-timepicker.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/daterangepicker/moment.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/daterangepicker/daterangepicker.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/colorpicker/colorpicker.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/ckeditor/ckeditor.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/ckeditor/adapters/jquery.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/js/bootstrap-datepicker.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/bootstrap-wysiwyg/js/wysihtml5-0.3.0.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/bootstrap-wysiwyg/js/bootstrap-wysihtml5-0.0.2.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/plugins/introjs/js/intro.min.js") %>'></script>



    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.iframe-transport.js") %>'></script>
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload.js") %>'></script>
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload-process.js") %>'></script>
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload-image.js") %>'></script>
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload-audio.js") %>'></script>
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload-video.js") %>'></script>
    <script src='<%= ResolveUrl("~/plugins/jquery-file-upload/js/jquery.fileupload-validate.js") %>'></script>
    <script src='<%= ResolveUrl("~/plugins/summernote/summernote.js") %>'></script>



</body>
</html>
