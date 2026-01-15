<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterNow.aspx.cs" Inherits="RegisterNow" %>

<!DOCTYPE html>
<%-- ReSharper disable once AspUnusedRegisterDirectiveHighlighting --%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <title>eAM&reg; Alumni Registration</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="shortcut icon" href="../img/ico/favicon.png" />
    <!-- Bootstrap & FontAwesome & Entypo CSS -->
    <link href="~/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <link href="~/css/font-entypo.css" rel="stylesheet" type="text/css" />
    <link href="~/css/animate.css" rel="stylesheet" />
    <!-- Fonts CSS -->
    <link href="~/css/fonts.css" rel="stylesheet" type="text/css" />

    <!-- Plugin CSS -->
    <link href="~/plugins/prettyPhoto-plugin/css/prettyPhoto.css" rel="stylesheet" type="text/css" />

    <!-- Theme CSS -->
    <link href="~/css/theme.min.css" rel="stylesheet" type="text/css" />

    <!-- Responsive CSS -->
    <link href="~/css/theme-responsive.min.css" rel="stylesheet" type="text/css" />

    <!-- Custom CSS -->
    <link href="~/custom/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <!-- Head SCRIPTS -->
    <script src="../js/MyScript.js"></script>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <script>
        $(function () {
            $('.datepicker-normal').datepicker({ dateFormat: 'dd-M-yy', changeYear: true, changeMonth: true, yearRange: '-75:+00' });
        });
    </script>
    <style>
        .login-layout .vd_login-page {
            margin: 1% auto 60px !important;
        }

        .hower_underline:hover {
            text-decoration: underline;
        }

        .login-layout .vd_login-page {
            width: 100%;
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

        .form-control-blue {
            color: #000 !important;
        }
    </style>
</head>
<body runat="server" id="pages" class="no-nav-left no-nav-right nav-top-fixed background-login responsive remove-navbar login-layout clearfix" data-active="pages" data-smooth-scrolling="1">

    <div class="main__bg"></div>
    <div class="main__bg layer1"></div>
    <div class="main__bg layer2"></div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnRegisterNow" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div class="alert alert-danger vd_hidden" id="alert_danger" runat="server" style="max-width: 800px; float: right;">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>

                    <span class="vd_alert-icon"><i class="fa fa-exclamation-circle vd_red"></i></span>
                    <strong>Sorry!</strong>
                    <asp:Label ID="lblError" runat="server" Text="You have entered incorrect Username or Password."></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnRegisterNow" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div class="alert alert-warning vd_hidden" id="alert_warning" runat="server" style="max-width: 800px; float: right;">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>
                    <span class="vd_alert-icon"><i class="fa fa-check-circle vd_green"></i></span><strong>
                        <asp:Label ID="lblWarning" runat="server" Text="" Font-Size="16px"></asp:Label>
                    </strong>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnRegisterNow" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div class="alert alert-success vd_hidden" id="alert_success" runat="server" style="max-width: 800px; float: right;">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true"><i class="icon-cross"></i></button>
                    <span class="vd_alert-icon"><i class="fa fa-check-circle vd_green"></i></span><strong>
                        <asp:Label ID="lbllog" runat="server" Text="" Font-Size="16px"></asp:Label>
                    </strong>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="mainUp">
            <ContentTemplate>

                <div class="content">
                    <div class="container">
                        <div class="vd_login-page">
                            <div class="panel widget" style="background: #00000047; box-shadow: 0px 0px 10px #a7a7a7;">
                                <div class="" runat="server" id="Expbody" visible="false" style="border-radius: 10px;">
                                    <div class="form-group  mgbt-xs-20">
                                        <div class="alert alert-danger text-center" id="alert" runat="server" style="font-size: 15px; padding: 4px;"></div>
                                    </div>
                                </div>
                                <div class="panel-body" runat="server" style="border-radius: 10px;">
                                    <asp:Panel ID="panel1" runat="server" DefaultButton="BtnRegisterNow">
                                        <div class="col-md-12 text-center">
                                            <h2 style="box-shadow: 0px -9px 10px #ccc; padding: 6px; text-transform: uppercase;">Alumni Registration </h2>
                                            <br />
                                        </div>
                                        <div class="col-md-12" runat="server" id="divVerify">
                                            <div class="col-md-3">
                                                <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox runat="server" ID="txtContactNo1" CssClass="form-control-blue validatetxt1 validatetxt2" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);" AutoPostBack="true" OnTextChanged="txtContactNo1_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6" style="padding-top: 24px;" runat="server" id="divBtnSend">
                                                <asp:LinkButton ID="BtnSend" runat="server" OnClientClick="ValidateTextBox('.validatetxt1'); return validationReturn();"
                                                    class="btn" OnClick="BtnSend_Click" Style="width: 140px !important;"><i class="fa fa-plane"></i>&nbsp;Send OTP</asp:LinkButton>
                                            </div>
                                            <div class="col-md-3" runat="server" id="divOtp" visible="false">
                                                <label class="control-label">OTP&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox runat="server" ID="txtOtp" CssClass="form-control-blue validatetxt2"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6" style="padding-top: 24px;" runat="server" id="divBtnVarify" visible="false">
                                                <asp:LinkButton ID="BtnVarify" runat="server" OnClientClick="ValidateTextBox('.validatetxt2'); return validationReturn();"
                                                    class="btn" OnClick="BtnVarify_Click" Style="width: 150px !important;"><i class="fa fa-check"></i>&nbsp;Verify OTP</asp:LinkButton>
                                                <asp:LinkButton ID="BtnClear" runat="server"
                                                    class="btn" OnClick="BtnClear_Click" Style="width: 100px !important;"><i class="fa fa-close"></i>&nbsp;Clear</asp:LinkButton>
                                            </div>

                                        </div>
                                        <div class="form-group  mgbt-xs-20" runat="server" id="divForm" visible="false">

                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control-blue validatetxt" Enabled="false"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Student's First Name&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtFname" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Middle Name</label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtMname" CssClass="form-control-blue"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Last Name</label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtLname" CssClass="form-control-blue"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-3" style="padding-right:5px !important; padding-left:5px !important;">
                                                    <label class="control-label">Date of Birth&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="DDYear" runat="server" OnSelectedIndexChanged="DDYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue col-sm-3">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDMonth" runat="server" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue col-sm-3">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDDate" runat="server" OnSelectedIndexChanged="DDDate_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue col-sm-3 "></asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList runat="server" ID="drpGender" CssClass="form-control-blue validatedrp">
                                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                                            <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Last class attended&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtLastClassAttended" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Year last attended&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList runat="server" ID="drpLastYearAttended" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label class="control-label">Email&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Branch&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList runat="server" ID="drpBranch" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Aadhaar No.&nbsp;<span class="vd_red">*</span></label>
                                                    <asp:Label ID="Label2AAdhar" runat="server" Text="" Font-Size="16px"></asp:Label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtAadhaarNo" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    </div>
                                                     <script>
                                                                        function Aadhar() {
                                                                            $('#txtAadhaarNo').on('keyup', function () {
                                                                                $(this).val(
                                                                                    function (index, value) {
                                                                                        value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                                        if (value.length > 14) {
                                                                                            value = value.substring(0, 14);
                                                                                        }
                                                                                        return value;
                                                                                    });
                                                                            });
                                                                        };
                                                                        Sys.Application.add_load(Aadhar);

                                                                    </script>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Graduation</label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtGraduation" CssClass="form-control-blue"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label class="control-label">Year of Graduation</label>
                                                    <div class="">
                                                        <asp:DropDownList runat="server" ID="drpYearOfGraduation" CssClass="form-control-blue">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Post-Graduation</label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtPostGraduation" CssClass="form-control-blue"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Year of Post-Graduation</label>
                                                    <div class="">
                                                        <asp:DropDownList runat="server" ID="drpYearOfPostGraduation" CssClass="form-control-blue">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Others</label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtOthers" CssClass="form-control-blue"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label class="control-label">Year of Others</label>
                                                    <div class="">
                                                        <asp:DropDownList runat="server" ID="drpYearofOthers" CssClass="form-control-blue">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Current Occupation&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtCurrentOccupation" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Marital Status&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList runat="server" ID="drpMaritalStatus" CssClass="form-control-blue validatedrp">
                                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                            <asp:ListItem Value="Married">Married</asp:ListItem>
                                                            <asp:ListItem Value="Unmarried">Unmarried</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Country&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList runat="server" ID="drpCountry" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList runat="server" ID="drpState" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpState_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList runat="server" ID="drpCity" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Document (Max size 200 Kb)&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:FileUpload runat="server" ID="fileDoc" CssClass="form-control-blue validatetxt" type="file"
                                                            onchange="checksFileSizeandFileTypeinupdatePanel(this, 200000, 'jpg|png|jpeg|pdf|JPG|PNG|JPEG|PDF',
                                                                                        'hdnDoc','hdnDocExt');" />
                                                        <asp:HiddenField ID="hdnDocExt" runat="server" />
                                                        <asp:HiddenField ID="hdnDoc" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label">Recent Photo (Max size 100 Kb)&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:FileUpload runat="server" ID="filePhoto" CssClass="form-control-blue validatetxt" type="file"
                                                            onchange="checksFileSizeandFileTypeinupdatePanelPhoto(this, 100000, 'jpg|png|jpeg|JPG|PNG|JPEG','Avatars',
                                                                                        'hdnPhoto','hdnPhotoExt');" />
                                                        <asp:HiddenField ID="hdnPhotoExt" runat="server" />
                                                        <asp:HiddenField ID="hdnPhoto" runat="server" />
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-9">
                                                    <label class="control-label">Current Address &nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" MaxLength="150" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="">
                                                        <asp:Image runat="server" ID="imgPhoto" CssClass="form-control-blue Avatars" Style="max-height: 100px;"></asp:Image>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 text-left">
                                                <br />
                                                <div style="box-shadow: 0px 9px 10px #ccc; padding: 6px;">
                                                    <a class="btn btn-sm" href="default.aspx"><i class="fa fa-backward"></i>&nbsp;Back to Login </a>
                                                </div>
                                                <br />
                                            </div>
                                            <div class="col-md-6 text-right">
                                                <br />
                                                <div style="box-shadow: 0px 9px 10px #ccc; padding: 6px;">
                                                    <asp:LinkButton ID="BtnRegisterNow" runat="server"
                                                        class="btn" OnClick="BtnRegisterNow_Click" Style="width: 162px !important;"
                                                        OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp'); return validationReturn();"><i class="fa fa-floppy-o"></i>&nbsp;Register</asp:LinkButton>
                                                </div>
                                                <br />
                                            </div>
                                        </div>

                                    </asp:Panel>

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
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <!-- Javascript =============================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../plugins/jquery-ui-touch-punch/jquery.ui.touch-punch.min.js"></script>
    <script type="text/javascript" src="../js/caroufredsel.js"></script>
    <script type="text/javascript" src="../js/plugins.js"></script>
    <script type="text/javascript" src="../plugins/breakpoints/breakpoints.js"></script>
    <script type="text/javascript" src="../plugins/prettyPhoto-plugin/js/jquery.prettyPhoto.js"></script>
    <script type="text/javascript" src="../plugins/tagsInput/jquery.tagsinput.min.js"></script>
    <script type="text/javascript" src="plugins/bootstrap-switch/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="../js/theme.js"></script>
    <script type="text/javascript" src="../custom/custom.js"></script>
    <script>
        function checksFileSizeandFileTypeinupdatePanel(fileupload, size, filetype, hiddenfield, extns) {
            if (fileupload.files.length > 0) {
                var filename = fileupload.files[0].name;
                var filesize = fileupload.files[0].size;
                if (filesize <= size) {
                    var extSplit = filename.split('.');
                    var extReverse = extSplit.reverse();
                    var ext = extReverse[0];
                    var splitfileext = filetype.split('|');
                    var flag = false;

                    for (var i = 0; i < splitfileext.length; i++) {
                        if (ext === splitfileext[i]) {
                            flag = true;
                            break;
                        }

                    }
                    if (flag === false) {
                        alert('Only ' + filetype + ' files are allowed!');
                        fileupload.value = "";
                    }
                }
                else {
                    alert('File size should not more than ' + (size / 1000) + ' Kb');
                    fileupload.value = "";
                }

                var reader = new FileReader();
                reader.onloadend = function () {
                    var base64Url = reader.result.split(',');
                    document.getElementById(hiddenfield).value = base64Url[base64Url.length - 1];
                    document.getElementById(extns).value = ext;
                };
                if (fileupload.files[0]) {
                    reader.readAsDataURL(fileupload.files[0]);
                }
                else {
                }
            }
            else {
            }

        }
        function checksFileSizeandFileTypeinupdatePanelPhoto(fileupload, size, filetype, imageClass, hiddenfield, extns) {
            var img = document.querySelector('.' + imageClass);
            if (fileupload.files.length > 0) {
                var filename = fileupload.files[0].name;
                var filesize = fileupload.files[0].size;
                if (filesize <= size) {
                    var extSplit = filename.split('.');
                    var extReverse = extSplit.reverse();
                    var ext = extReverse[0];
                    var splitfileext = filetype.split('|');
                    var flag = false;

                    for (var i = 0; i < splitfileext.length; i++) {
                        if (ext === splitfileext[i]) {
                            flag = true;
                            break;
                        }

                    }
                    if (flag === false) {
                        alert('Only ' + filetype + ' files are allowed!');
                        fileupload.value = "";
                    }
                }
                else {
                    alert('File size should not more than ' + (size / 1000) + ' Kb');
                    fileupload.value = "";
                }

                var reader = new FileReader();
                reader.onloadend = function () {
                    img.src = reader.result;
                    var base64Url = reader.result.split(',');
                    document.getElementById(hiddenfield).value = base64Url[base64Url.length - 1];
                    document.getElementById(extns).value = ext;
                };
                if (fileupload.files[0]) {
                    reader.readAsDataURL(fileupload.files[0]);
                }
                else {
                    img.src = "";
                }
            }
            else {
                img.src = "";
            }

        }
    </script>
</body>
</html>
