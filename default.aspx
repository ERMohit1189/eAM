<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="index" %>

<!DOCTYPE html>
<%-- ReSharper disable once AspUnusedRegisterDirectiveHighlighting --%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>eAM® - Education Management System</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="img/ico/apple-touch-icon-114-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="img/ico/apple-touch-icon-72-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" href="img/ico/apple-touch-icon-57-precomposed.png" />
    <link rel="shortcut icon" href="img/ico/favicon.png" />

    <!-- Google Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet">

    <!-- Modern Design Styles -->
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        :root {
            --primary-color: #4F46E5;
            --primary-dark: #4338CA;
            --primary-light: #EEF2FF;
            --secondary-color: #10B981;
            --text-dark: #1F2937;
            --text-light: #6B7280;
            --border-color: #E5E7EB;
            --error-color: #EF4444;
            --success-color: #10B981;
            --bg-gradient-start: #667eea;
            --bg-gradient-end: #764ba2;
        }

        body {
            font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif !important;
            background: linear-gradient(135deg, var(--bg-gradient-start) 0%, var(--bg-gradient-end) 100%) !important;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
            position: relative;
            overflow-x: hidden;
        }

        /* Animated background elements */
        .bg-shapes {
            position: fixed;
            width: 100%;
            height: 100%;
            overflow: hidden;
            z-index: 0;
            top: 0;
            left: 0;
        }

        .shape {
            position: absolute;
            background: rgba(255, 255, 255, 0.1);
            border-radius: 50%;
            animation: float 20s infinite ease-in-out;
        }

        .shape-1 {
            width: 300px;
            height: 300px;
            top: -100px;
            left: -100px;
            animation-delay: 0s;
        }

        .shape-2 {
            width: 200px;
            height: 200px;
            bottom: -50px;
            right: -50px;
            animation-delay: 7s;
        }

        .shape-3 {
            width: 150px;
            height: 150px;
            top: 50%;
            right: 10%;
            animation-delay: 3s;
        }

        @keyframes float {
            0%, 100% {
                transform: translateY(0) rotate(0deg);
                opacity: 0.5;
            }
            50% {
                transform: translateY(-30px) rotate(180deg);
                opacity: 0.8;
            }
        }

        #form1 {
            width: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .content {
            position: relative;
            z-index: 1;
            width: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .container {
            width: 100%;
            max-width: 1100px;
            margin: 0 auto;
        }

        .vd_login-page {
            width: 100%;
            margin: 0 !important;
        }

        .modern-login-card {
            background: white;
            border-radius: 24px;
            box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.25);
            overflow: hidden;
            animation: slideUp 0.6s ease-out;
            display: grid;
            grid-template-columns: 1fr 1fr;
        }

        @keyframes slideUp {
            from {
                opacity: 0;
                transform: translateY(30px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        /* Left Panel - Branding */
        .modern-login-card::before {
            content: '';
            background: linear-gradient(135deg, var(--primary-color) 0%, var(--primary-dark) 100%);
            padding: 60px 50px;
            color: white;
            display: flex;
            flex-direction: column;
            justify-content: center;
            position: relative;
            overflow: hidden;
        }

        .login-branding {
            background: linear-gradient(135deg, var(--primary-color) 0%, var(--primary-dark) 100%);
            padding: 60px 50px;
            color: white;
            display: flex;
            flex-direction: column;
            justify-content: center;
            position: relative;
            overflow: hidden;
            grid-column: 1;
        }

        .login-branding::before {
            content: '';
            position: absolute;
            width: 400px;
            height: 400px;
            background: rgba(255, 255, 255, 0.1);
            border-radius: 50%;
            top: -200px;
            right: -200px;
        }

        .logo-section {
            position: relative;
            z-index: 1;
            margin-bottom: 40px;
        }

        .logo-section h1 {
            font-size: 48px;
            font-weight: 700;
            margin-bottom: 10px;
            letter-spacing: -1px;
            color: white;
        }

        .logo-section p {
            font-size: 18px;
            opacity: 0.9;
            font-weight: 300;
        }

        .feature-list {
            position: relative;
            z-index: 1;
            list-style: none;
            margin-top: 30px;
        }

        .feature-list li {
            display: flex;
            align-items: center;
            margin-bottom: 20px;
            font-size: 16px;
            opacity: 0.95;
        }

        .feature-list li::before {
            content: '✓';
            display: inline-block;
            width: 28px;
            height: 28px;
            background: rgba(255, 255, 255, 0.2);
            border-radius: 50%;
            text-align: center;
            line-height: 28px;
            margin-right: 15px;
            font-weight: 600;
        }

        /* Right Panel - Login Form */
        .card-body {
            padding: 60px 50px !important;
            display: flex;
            flex-direction: column;
            justify-content: center;
            grid-column: 2;
            background: white !important;
            border-radius: 0 !important;
        }

        .login-img {
            text-align: center;
            margin-bottom: 30px;
        }

        .login-img img {
            max-width: 180px;
            height: auto;
        }

        .login-header {
            margin-bottom: 30px;
            text-align: center;
        }

        .login-header h2 {
            font-size: 28px;
            font-weight: 700;
            color: var(--text-dark);
            margin-bottom: 8px;
        }

        .login-header p {
            font-size: 15px;
            color: var(--text-light);
        }

        /* Alert Styles */
        .alert {
            padding: 14px 18px;
            border-radius: 12px;
            margin-bottom: 20px;
            display: flex;
            align-items: flex-start;
            font-size: 14px;
            animation: slideDown 0.3s ease-out;
            border: none;
        }

        @keyframes slideDown {
            from {
                opacity: 0;
                transform: translateY(-10px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .alert-danger {
            background: #FEE2E2 !important;
            color: #991B1B !important;
            border-left: 4px solid var(--error-color) !important;
        }

        .alert-success {
            background: #D1FAE5 !important;
            color: #065F46 !important;
            border-left: 4px solid var(--success-color) !important;
        }

        .vd_hidden {
            display: none !important;
        }

        /* Form Styles */
        .form-group {
            margin-bottom: 20px;
        }

        .input-group {
            position: relative;
            display: flex;
            align-items: stretch;
            width: 100%;
            border: 2px solid var(--border-color);
            border-radius: 12px;
            transition: all 0.3s ease;
            overflow: hidden;
        }

        .input-group:focus-within {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 4px var(--primary-light);
        }

        .input-group-text {
            background: transparent !important;
            border: none !important;
            padding: 14px 16px;
            color: var(--text-light);
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .input-group-text i {
            font-size: 18px;
        }

        .form-control {
            border: none !important;
            padding: 12px 16px !important;
            font-size: 15px !important;
            line-height: 1.2 !important;
            min-height: 44px !important;
            height: auto !important;
            flex: 1;
            font-family: 'Inter', system-ui, -apple-system, 'Segoe UI', Roboto, 'Helvetica Neue', Arial !important;
            color: var(--text-dark) !important;
            box-shadow: none !important;
            border-radius: 0 !important;
            background: transparent !important;
        }
        .input-group-text {
            padding: 10px 12px !important;
        }
        /* Small adjustment for large inputs */
        .form-control.form-control-lg { padding: 14px 16px !important; min-height:48px !important; }
        .form-control:focus {
            outline: none !important;
            box-shadow: none !important;
        }

        .form-control::placeholder {
            color: #9CA3AF;
        }

        /* Checkbox */
        .form-check {
            display: flex;
            align-items: center;
            margin-top: 10px;
        }

        .form-check-input {
            width: 18px !important;
            height: 18px !important;
            margin-right: 8px !important;
            cursor: pointer;
            accent-color: var(--primary-color);
        }

        .form-check-label {
            font-size: 14px;
            color: var(--text-dark);
            cursor: pointer;
            user-select: none;
            margin: 0;
        }

        /* Buttons */
        .btn-primary {
            width: 100%;
            padding: 16px !important;
            background: linear-gradient(135deg, var(--primary-color) 0%, var(--primary-dark) 100%) !important;
            color: white !important;
            border: none !important;
            border-radius: 12px !important;
            font-size: 16px !important;
            font-weight: 600 !important;
            cursor: pointer;
            transition: all 0.3s ease;
            display: flex !important;
            align-items: center;
            justify-content: center;
            gap: 10px;
            box-shadow: 0 4px 12px rgba(79, 70, 229, 0.3) !important;
        }

        .btn-primary:hover {
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(79, 70, 229, 0.4) !important;
        }

        .btn-primary:active {
            transform: translateY(0);
        }

        .aspNetDisabled {
            opacity: 0.6;
            cursor: not-allowed !important;
        }

        /* Links */
        .postlink-color {
            font-size: 14px;
            color: var(--primary-color) !important;
            text-decoration: none;
            font-weight: 500;
            transition: color 0.3s ease;
        }

        .hower_underline:hover {
            color: var(--primary-dark) !important;
            text-decoration: underline !important;
        }

        /* Footer */
        .login-footer-title {
            margin-top: 20px;
            padding-top: 20px;
            border-top: 1px solid var(--border-color) !important;
            font-size: 13px;
            color: var(--text-light) !important;
            text-align: center;
        }

        .form-group > .col-md-12 {
            padding: 0 15px;
        }

        .form-group i {
            font-size: 13px;
            color: var(--text-light);
            line-height: 1.6;
        }

        .form-group i a {
            color: var(--primary-color) !important;
        }

        /* Loading spinner */
        .fa-spin {
            animation: spin 1s linear infinite;
        }

        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }

        /* Responsive */
        @media (max-width: 768px) {
            .modern-login-card {
                grid-template-columns: 1fr;
            }

            .login-branding {
                display: none;
            }

            .card-body {
                padding: 40px 30px !important;
            }

            .logo-section h1 {
                font-size: 36px;
            }
        }

        /* Hidden elements */
        .hide, .d-none {
            display: none !important;
        }

        /* Row layouts */
        .row {
            display: flex;
            flex-wrap: wrap;
            margin: 0 -15px;
        }

        .col-6 {
            flex: 0 0 50%;
            max-width: 50%;
            padding: 0 15px;
        }

        .no-mgpd {
            margin: 0;
            padding: 0;
        }

        .text-start {
            text-align: left;
            margin-top: 15px;
        }

        .text-center {
            text-align: center;
        }

        .w-100 {
            width: 100% !important;
        }

        .ms-2 {
            margin-left: 8px;
        }

        .mgbt-xs-5 {
            margin-bottom: 5px;
        }

        .sr-only {
            position: absolute;
            width: 1px;
            height: 1px;
            padding: 0;
            margin: -1px;
            overflow: hidden;
            clip: rect(0, 0, 0, 0);
            white-space: nowrap;
            border-width: 0;
        }

        /* Back to top button */
        #back-top {
            position: fixed;
            bottom: 30px;
            right: 30px;
            background: var(--primary-color);
            color: white;
            width: 40px;
            height: 40px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            text-decoration: none;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            transition: all 0.3s ease;
            z-index: 1000;
        }

        #back-top:hover {
            background: var(--primary-dark);
            transform: translateY(-3px);
        }

        .visible {
            opacity: 1;
        }
    </style>

    <!-- Original CSS for compatibility -->
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <!-- Head SCRIPTS -->
    <script type="text/javascript" src="js/jquery.min.js"></script>
</head>

<body runat="server" id="pages" class="full-layout">
    <!-- Animated background shapes -->
    <div class="bg-shapes">
        <div class="shape shape-1"></div>
        <div class="shape shape-2"></div>
        <div class="shape shape-3"></div>
    </div>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <script type="text/javascript">
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);

            function BeginRequestHandler() {
                var wait = document.getElementById('wait');
                if (wait) wait.style.visibility = 'visible';
                var button = document.getElementById('LinkButton1');
                if (button) {
                    button.disabled = true;
                    button.classList.add('aspNetDisabled');
                }
            }

            function EndRequestHandler() {
                var wait = document.getElementById('wait');
                if (wait) wait.style.visibility = 'hidden';
                var button = document.getElementById('LinkButton1');
                if (button) {
                    button.disabled = false;
                    button.classList.remove('aspNetDisabled');
                }
            }

            function scrollup() {
                var alertsuccess = document.getElementById('<%= alert_success.ClientID %>');
                var alertdanger = document.getElementById('<%= alert_danger.ClientID %>');
                if (alertsuccess) alertsuccess.className = "alert alert-success vd_hidden";
                if (alertdanger) alertdanger.className = "alert alert-danger vd_hidden";
                setTimeout(displaynone, 2000);
            }

            function displaynone() {
                var alertsuccess = document.getElementById('<%= alert_success.ClientID %>');
                var alertdanger = document.getElementById('<%= alert_danger.ClientID %>');
                if (alertsuccess) alertsuccess.style.display = "none";
                if (alertdanger) alertdanger.style.display = "none";
            }

            function blockDisplay() {
                setTimeout(scrollup, 20000);
            }

            function ValidateTextBox(selector) {
                var isValid = true;
                $(selector).each(function() {
                    if ($(this).val().trim() === '') {
                        isValid = false;
                        $(this).closest('.input-group').css('border-color', 'var(--error-color)');
                    } else {
                        $(this).closest('.input-group').css('border-color', 'var(--border-color)');
                    }
                });

                if (!isValid) {
                    $('#vd_login-error').removeClass('d-none');
                    setTimeout(function() {
                        $('#vd_login-error').addClass('d-none');
                    }, 3000);
                }
                return isValid;
            }
        </script>

        <div class="content">
            <div class="container">
                <div class="vd_login-page modern-login-card">
                    <!-- Left Panel - Branding -->
                    <div class="login-branding">
                        <div class="logo-section">
                            <h1>eAM®</h1>
                            <p>Education Management System</p>
                        </div>
                        <ul class="feature-list">
                            <li>Comprehensive Student Management</li>
                            <li>Staff & Alumni Portal Access</li>
                            <li>Integrated Payment Gateway</li>
                            <li>Real-time Dashboard & Analytics</li>
                            <li>Secure & Scalable Platform</li>
                        </ul>
                    </div>

                    <!-- Right Panel - Login Form -->
                    <div class="card widget default-card">
                        <!-- Expiry Alert -->
                        <div class="" runat="server" id="Expbody" visible="false" style="border-radius: 10px; padding: 0 50px;">
                            <div class="form-group mgbt-xs-20">
                                <div class="alert alert-danger text-center" id="alert" runat="server" style="font-size:15px; padding:10px;"></div>
                            </div>
                        </div>

                        <div class="card-body p-4" runat="server">
                            <asp:HiddenField runat="server" ID="hdnSubscription" />

                            <!-- Logo -->
                            <div class="login-img">
                                <div class="main-logo-center">
                                    <img src="img/logo.png" alt="eAM logo" />
                                </div>
                            </div>

                            <!-- Login Header -->
                            <div class="login-header">
                                <h2>Welcome Back</h2>
                                <p>Please sign in to access your account</p>
                            </div>

                            <!-- Error Alert -->
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <script>Sys.Application.add_load(blockDisplay);</script>
                                    <div class="alert alert-danger vd_hidden" id="alert_danger" runat="server">
                                        <div>
                                            <span style="font-size: 18px; margin-right: 10px;">⚠</span>
                                        </div>
                                        <div>
                                            <strong>Sorry!</strong>
                                            <asp:Label ID="lblError" runat="server" Text="You have entered incorrect Username or Password."></asp:Label>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <!-- Success Alert -->
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="alert alert-success vd_hidden" id="alert_success" runat="server">
                                        <div>
                                            <span style="font-size: 18px; margin-right: 10px;">✓</span>
                                        </div>
                                        <div>
                                            <strong>
                                                <asp:Label ID="lbllog" runat="server" Text="" Font-Size="12px"></asp:Label>
                                            </strong>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <!-- Login Form -->
                            <asp:Panel Id="panel1" runat="server" DefaultButton="LinkButton1">
                                <!-- Username -->
                                <div class="form-group mgbt-xs-20">
                                    <div class="col-md-12">
                                        <div class="sr-only">
                                            <label class="control-label" for="email">Username</label>
                                        </div>
                                        <div class="mb-3">
                                            <div class="input-group">
                                                <span class="input-group-text">
                                                    <i class="fa fa-user" aria-hidden="true"></i>
                                                </span>
                                                <asp:TextBox ID="txtUserName" placeholder="Username" runat="server" onFocus="this.select();" CssClass="form-control form-control-lg validatetxt"></asp:TextBox>
                                            </div>
                                        </div>

                                        <!-- Password -->
                                        <div class="sr-only">
                                            <label class="control-label" for="password">Password</label>
                                        </div>
                                        <div class="mb-3">
                                            <div class="input-group">
                                                <span class="input-group-text">
                                                    <i class="fa fa-key" aria-hidden="true"></i>
                                                </span>
                                                <asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password" CssClass="form-control form-control-lg validatetxt" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <!-- Hidden dropdowns -->
                                        <div class="vd_input-wrapper vd_input-margin hide">
                                            <asp:DropDownList ID="DrpBranchName" runat="server" CssClass="select-box form-control-blue"></asp:DropDownList>
                                        </div>
                                        <div class="vd_input-wrapper vd_input-margin hide">
                                            <asp:DropDownList ID="DrpSessionName" runat="server" CssClass="select-box form-control-blue"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <!-- Validation Error -->
                                <div id="vd_login-error" class="alert alert-danger d-none">
                                    <i class="fa fa-exclamation-circle"></i> Please fill in the necessary field(s)
                                </div>

                                <!-- Remember Me & Login Button -->
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-6 no-mgpd">
                                                <div class="form-check">
                                                    <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-check-input" />
                                                    <label for="chkRememberMe" class="form-check-label ms-2">Remember me</label>
                                                </div>
                                            </div>
                                            <div class="col-6 no-mgpd">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');" class="btn btn-primary btn-lg w-100" OnClick="LinkButton1_Click">
                                                            <i class="fa fa-sign-in" aria-hidden="true"></i>&nbsp;&nbsp;Log In
                                                        </asp:LinkButton>
                                                        <i class="fa fa-spinner fa-spin" id="wait" style="visibility: hidden; color:#fff !important; position: absolute; margin-left: -30px; margin-top: 18px;" runat="server"></i>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- Forgot Password -->
                                    <div class="col-md-12 text-center mgbt-xs-5">
                                        <div class="text-start">
                                            <a class="postlink-color hower_underline" href="forgot-password.aspx">Forgot Password?</a>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <!-- Footer Links -->
                            <div class="form-group">
                                <div class="col-md-12 text-center mgbt-xs-5" style="padding-left: 13px; padding-right: 13px; text-align: left !important;">
                                    <i>By clicking Login, you agree to our
                                        <a href="Termsand_Conditions.aspx" class="hower_underline">Terms</a>
                                        and have read and acknowledged our
                                        <a href="Privacy_Policy.aspx" class="hower_underline">Privacy Policy</a>,
                                        <a href="Refund_Policy.aspx" class="hower_underline">Refund Policy</a>,
                                        <a href="UGC_License.aspx" class="hower_underline">UGC License</a>, and
                                        <a href="Product_Services_Pricing.aspx" class="hower_underline">Fee Structure</a>.
                                        <a href="Contact_Us.aspx" class="hower_underline">Contact Us</a> for details.
                                    </i>
                                </div>
                            </div>

                            <!-- Company Name -->
                            <div class="form-group">
                                <div class="col-md-12 text-center mgbt-xs-5" style="margin-bottom: 0px !important;">
                                    <p class="login-footer-title" runat="server" id="lblCompanyName" visible="false">
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Back to top button -->
    <a id="back-top" href="#" data-action="backtop" class="vd_back-top visible">
        <i class="fa fa-angle-up"></i>
    </a>

    <!-- Scripts -->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>

    <script>
        $(document).ready(function() {
            // Enter key support
            $("[id*=txtUserName], [id*=txtPassword]").keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13') {
                    event.preventDefault();
                    $("[id*=LinkButton1]").click();
                }
            });

            // Back to top button
            $('#back-top').click(function(e) {
                e.preventDefault();
                $('html, body').animate({scrollTop: 0}, 500);
            });
        });
    </script>
</body>
</html>
