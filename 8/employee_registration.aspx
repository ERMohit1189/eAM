<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="employee_registration.aspx.cs"
    Inherits="_8.AdminEmployeeRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .MyTabStyle .ajax__tab_header {
            font-size: 14px;
            font-weight: bold;
            display: block;
            color: #222222;
            outline: none;
        }

            .MyTabStyle .ajax__tab_header .ajax__tab_outer {
                border-color: #222;
                color: #222;
                padding-left: 10px;
                margin-right: 3px;
                border: solid 1px #d7d7d7;
                background: #c5c5c5;
                background-image: linear-gradient(#f9f9f9, #ddd);
            }

            .MyTabStyle .ajax__tab_header .ajax__tab_inner {
                border-color: #666;
                color: #666;
                padding: 5px 10px 4px 0;
            }

                .MyTabStyle .ajax__tab_header .ajax__tab_inner a {
                    text-decoration: none;
                    color: #333;
                    outline: none;
                }

        .MyTabStyle .ajax__tab_hover .ajax__tab_outer {
            background-color: #ddd;
        }

        .MyTabStyle .ajax__tab_hover .ajax__tab_inner {
            color: #fff;
        }

        .MyTabStyle .ajax__tab_active .ajax__tab_outer {
            border-bottom-color: #ffffff;
            background-color: #d7d7d7;
            text-decoration: none;
            border: none;
        }

        .MyTabStyle .ajax__tab_active .ajax__tab_inner {
            color: #000;
            border-color: #333;
        }

        .MyTabStyle .ajax__tab_body {
            background-color: #fff;
            border-top-width: 0;
            border: solid 1px #d7d7d7;
            border-top-color: #ffffff;
        }
    </style>
    <script src="../js/jquery.min.js"></script>
    <script>
        function CopyStringName() {
            var name = $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtFirstName").val() +
                ($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtmidName").val() != "" ? " " + $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtmidName").val() : "") +
                ($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtlastName").val() != "" ? " " + $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtlastName").val() : "");
            $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtDisplay").val(name);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <%--  <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>--%>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body ">
                        <div class="col-sm-12  vd_input-margin ">
                            <div class="tabs">
                                <ul class="nav nav-tabs nav-justified">
                                    <li class="active"><a href="#home-tab" data-toggle="tab"><span class="menu-icon"><i class="glyphicon glyphicon-list-alt"></i></span>&nbsp; General Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                    <li><a href="#posts-tab2" data-toggle="tab"><span class="menu-icon"><i class=" icon-newspaper"></i></span>&nbsp; Official Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                    <%-- ReSharper disable once Html.IdNotResolved --%>
                                    <li><a href="#Documents1" data-toggle="tab"><span class="menu-icon">
                                        <i class="fa fa-file-archive-o"></i></span>&nbsp; Documents <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                    <li><a href="#posts-tab" data-toggle="tab"><span class="menu-icon"><i class="fa fa-university"></i></span>&nbsp; Qualification Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                    <li><a href="#list-tab" data-toggle="tab"><span class="menu-icon"><i class="icon-graduation"></i></span>&nbsp; Previous Employment <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>

                                    <li class="hide"><a href="#list-tab2" data-toggle="tab"><span class="menu-icon"><i class="fa fa-file-archive-o"></i></span>&nbsp; Earning & Deduction <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                    <li id="otherdetails" style="display: none"><a data-toggle="tab"><span class="menu-icon"><i class="glyphicon glyphicon-list-alt"></i></span>&nbsp; Scholarship Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>

                                </ul>
                                <div class="tab-content form-box-border-g mgbt-xs-20">
                                    <div class="tab-pane active " id="home-tab">
                                        <div class="row mgbt-xs-20">

                                            <div class="col-sm-6  full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="fa  fa-child"></i></span>Personal Details </h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon">
                                                                <i class="icon-minus3"></i>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                        <div class="row">
                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-4   ">
                                                                    <div class="col-sm-12 no-padding">
                                                                        <label class="control-label  no-padding">First Name&nbsp;<span class="vd_red">*</span></label>
                                                                    </div>
                                                                    <div class="col-xs-5 no-padding hide">
                                                                        <asp:DropDownList ID="DrpTitle" runat="server" CssClass="form-control-blue">
                                                                            <asp:ListItem>Mr.</asp:ListItem>
                                                                            <asp:ListItem>Ms.</asp:ListItem>
                                                                            <asp:ListItem>Mrs.</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xs-12 no-padding">
                                                                        <asp:TextBox ID="txtFirstName" runat="server" onblur="CopyStringName();"
                                                                            CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  ">
                                                                    <label class="control-label">Middle Name</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtmidName" runat="server" onblur="CopyStringName();" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 mgbt-lg-15">
                                                                    <label class="control-label">Last Name&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtlastName" runat="server" onblur="CopyStringName();" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  ">
                                                                    <label class="control-label">Display Name&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtDisplay" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  ">
                                                                    <label class="control-label">Date of Birth</label>
                                                                    <div class="">
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                            <asp:TextBox ID="txtStudentDOB" placeholder="yyyy MMM dd"
                                                                                runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="False">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpYear_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4 ">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DrpMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMonth_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DrpDate" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4 ">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4 mgbt-lg-15">
                                                                    <label class="control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:DropDownList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatLayout="Flow" CssClass="radio-success vd_radio validaterb">
                                                                            <asp:ListItem Selected="True">Male</asp:ListItem>
                                                                            <asp:ListItem>Female</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-4  ">
                                                                    <label class="control-label">Father&#39;s Name</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtfathername" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 mgbt-lg-15">
                                                                    <label class="control-label">Mother&#39;s Name</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtmothname" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 ">
                                                                    <label class="control-label">Marital Status</label>
                                                                    <div class="">
                                                                        <asp:DropDownList ID="DrpMaitalStatus" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="DrpMaitalStatus_OnSelectedIndexChanged" AutoPostBack="True">
                                                                            <asp:ListItem Value="1">Single</asp:ListItem>
                                                                            <asp:ListItem Value="2">Married</asp:ListItem>
                                                                            <asp:ListItem Value="3">Separated</asp:ListItem>
                                                                            <asp:ListItem Value="4">Divorced</asp:ListItem>
                                                                            <asp:ListItem Value="5">Widowed</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <div class="col-md-8" style="padding: 0; margin: 0;">
                                                                    <div class="col-sm-6" runat="server" visible="False" id="divhidemarr">
                                                                        <label class="control-label">Marriage Anniversary</label>
                                                                        <div class="">
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                <asp:TextBox ID="txtmarranniver" placeholder="yyyy MMM dd" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                                <div class="text-box-msg">
                                                                                </div>
                                                                            </div>
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 mgbt-lg-15" runat="server" visible="False" id="divhideSpouse">
                                                                        <label class="control-label">Spouse's Name</label>
                                                                        <div class="">
                                                                            <asp:TextBox ID="txtspousename" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                     <div class="col-sm-6  ">
                                                                         <label class="control-label">Mobile No.&nbsp;<span class="vd_red">*</span></label>
                                                                         <div class="">
                                                                             <asp:TextBox ID="txtmobileno" runat="server" OnTextChanged="txtmobileno_TextChanged" CssClass="form-control-blue validatetxt" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                                                             <div class="text-box-msg">
                                                                             </div>
                                                                         </div>
                                                                         <div class="">
                                                                             <asp:CheckBox ID="chkStMobile" runat="server" CssClass="vd_checkbox checkbox-success "
                                                                                 RepeatDirection="Horizontal" RepeatLayout="Flow" Text="View in Student/ Parent App" />
                                                                         </div>
                                                                     </div>
                                                                    
                                                                    <div class="col-sm-6  ">
                                                                        <label class="control-label">E-mail&nbsp;<span class="vd_red">*</span></label>
                                                                        <div class="">
                                                                            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                        <div class="">
                                                                            <asp:CheckBox ID="chkStEmail" runat="server" CssClass="vd_checkbox checkbox-success  "
                                                                                RepeatDirection="Horizontal" RepeatLayout="Flow" Text="View in Student/ Parent App" />
                                                                        </div>
                                                                    </div>

                                                                   <div class="col-sm-6  ">
    <label class="control-label" style="font-size: 12px;">Emergency Contact No.&nbsp;<span class="vd_red">*</span></label>
    <div class="">
        <asp:TextBox ID="txtemergencycontactno" runat="server" CssClass="form-control-blue validatetxt" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
        <div class="text-box-msg">
        </div>
    </div>
</div>

                                                                    <div class="col-sm-6  ">
                                                                        <label class="control-label" runat="server" id="lblAadhaar">Aadhaar No.</label>
                                                                        <div class="">
                                                                            <asp:TextBox ID="txtAadhar" runat="server" CssClass="form-control-blue" placeholder="0000 0000 0000"></asp:TextBox>
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <script>
                                                                        function Aadhar() {
                                                                            $('#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtAadhar').on('keyup', function () {
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
                                                                <div class="col-md-4" style="padding: 0; margin: 0;">
                                                                    <div class="col-sm-12 ">
                                                                        <label class="control-label">Upload Photo&nbsp;<span class="vd_red"></span></label>
                                                                        <div class="">
                                                                            <asp:FileUpload ID="avatarUpload" runat="server" CssClass="form-control-blue" onchange="checksFileSizeandFileType(this, 100000, 'jpg|png|jpeg|gif','Avatars');" type="file" />
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 ">
                                                                        <div class="stu-pic-box">
                                                                            <div class="stu-pic-box-main">
                                                                                <asp:Image ID="Avatar" runat="server" src="../img/user-pic/student-pic.png" Class="Avatars" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6  full-width-100">
    <div class="panel widget">
        <div class="panel-heading vd_bg-dark-blue">
            <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Other Details</h3>
            <div class="vd_panel-menu ">
                <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

            </div>
            <!-- vd_panel-menu -->
        </div>
        <div class="panel-body form-main-box-border-g">
            <div class="row">
                <div class="col-sm-12  ">
                    <div class="col-sm-4">
                        <label class="control-label">Religion&nbsp;<span class="vd_red">* </span></label>
                        <div class="">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DrpReligion" runat="server" AutoPostBack="True" CssClass="form-control-blue">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label class="control-label">Nationality</label>
                        <div class="">
                            <asp:TextBox ID="txtnat" runat="server" CssClass="form-control-blue">Indian</asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4  mgbt-lg-15">
                        <label class="control-label">Category&nbsp;<span class="vd_red">* </span></label>
                        <div class="">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DrpCategory" runat="server" AutoPostBack="True" CssClass="form-control-blue">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label class="control-label">Blood Group &nbsp;<span class="vd_red">* </span></label>
                        <div class="">
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="drpblood" runat="server" AutoPostBack="True" CssClass="form-control-blue">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label class="control-label">Caste</label>
                        <div class="">
                            <asp:TextBox ID="txtcaste" runat="server" CssClass="form-control-blue"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4 mgbt-lg-15">
                        <label class="control-label">Height</label>
                        <div class="">
                            <asp:TextBox ID="txtheight" runat="server" CssClass="form-control-blue"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4 ">
                        <label class="control-label">Weight</label>
                        <div class="">
                            <asp:TextBox ID="txtweight" runat="server" CssClass="form-control-blue"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <label class="control-label">Diseases</label>
                        <div class="">
                            <asp:TextBox ID="txtdiseas" runat="server" CssClass="form-control-blue"></asp:TextBox>
                        </div>
                    </div>
                    
                     <div class="col-sm-4 mgbt-lg-15">
                        <label class="control-label">Languages Known &nbsp;<span class="vd_red"></span></label>
                        <div class=" ">
                            <asp:TextBox ID="txtdlanguages" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            <div class="text-box-msg">
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-sm-6">
                        <label class="control-label">Hobbies</label>
                        <div class="">
                            <asp:TextBox ID="txthobbies" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control-blue"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <label class="control-label">Identification Mark</label>
                        <div class="">
                            <asp:TextBox ID="txtidentmark" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control-blue"></asp:TextBox>
                        </div>
                    </div>
                   

                    <div class="col-sm-4  half-width-50 mgbt-xs-9 boxhide">
                        <asp:Label ID="Label2" runat="server" class="control-label" Text="Hostel Required"></asp:Label>
                        <div class="mgtp-6">
                            <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio-success vd_radio"
                                OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem Selected="True">No</asp:ListItem>
                            </asp:RadioButtonList>
                            <div class="text-box-msg">
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 boxhide">
                        <asp:Label ID="Label3" runat="server" class="control-label" Text="Transport Required"></asp:Label>
                        <div class="mgtp-6">
                            <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio-success vd_radio">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem Selected="True">No</asp:ListItem>
                            </asp:RadioButtonList>
                            <div class="text-box-msg">
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-4" runat="server" id="margindiv" style="margin-bottom: 108px;">
                           <br />
                    </div>

                 
                </div>
            </div>
        </div>
    </div>
</div>

                                            <div class="col-sm-6  full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Present Address</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                                                        </div>

                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row">
                                                            <div class="col-sm-12  ">
                                                                  <div class="col-sm-12 mgbt-lg-15 ">
                                                                 &nbsp;
                                                                  </div>
                                                                <div class="col-sm-4  ">
                                                                    <label class="control-label">Country&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpPreSCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpPreSCountry_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  ">
                                                                    <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpPreSta" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpPreSta_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 mgbt-lg-15">
                                                                    <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpPresCity" runat="server" OnSelectedIndexChanged="DrpPresCity_SelectedIndexChanged" CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  ">
                                                                    <label class="control-label">Pin</label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="DrpPresZip" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-8  ">
                                                                    <label class="control-label">Address &nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                                            <ContentTemplate>

                                                                                <asp:TextBox ID="txtPreseAdd" placeholder="Please don't write State and City name here" runat="server" TextMode="MultiLine" CssClass="form-control-blue validatetxt" Rows="1"></asp:TextBox>

                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-sm-6  full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Permanent Address</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                                                        </div>
                                                    </div>

                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row mgbt-xs-5">

                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-12  ">
                                                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" CssClass="vd_checkbox checkbox-success" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Same as Present Address" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>

                                                                <div class="col-sm-4  ">
                                                                    <label class="control-label">Country&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpPerCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpPerCountry_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4 ">
                                                                    <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpPerState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpPerState_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 mgbt-lg-15 ">
                                                                    <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpPerCity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpPerCity_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 ">
                                                                    <label class="control-label">Pin</label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtPerZip" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-8  ">
                                                                    <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtPermAdd" placeholder="Please don't write State and City name here" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                            <asp:Label ID="Label1" runat="server" Visible="true"></asp:Label>
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

                                    <div class="tab-pane" id="posts-tab2">
                                        <div class="row mgbt-xs-20">
                                            <div class="col-md-6 full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Official Details</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row">
                                                            <div class="col-sm-4">
                                                                <label class="control-label">Date of Joining</label>
                                                                <div class="">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="drpyearhai" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpyearhai_SelectedIndexChanged"
                                                                                CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                            </asp:DropDownList>
                                                                            <asp:DropDownList ID="drpmonthhai" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpmonthhai_SelectedIndexChanged"
                                                                                CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                            </asp:DropDownList>
                                                                            <asp:DropDownList ID="drpdinhai" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpdinhai_SelectedIndexChanged"
                                                                                CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <label class="control-label">Employee ID&nbsp;<span class="vd_red">* </span></label>
                                                                <div class="">
                                                                    <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4 mgbt-lg-15">
                                                                <label class="control-label">Department &nbsp;<span class="vd_red">* </span></label>
                                                                <div class="controls">
                                                                    <asp:DropDownList ID="txtDepartmentName" runat="server" CssClass="form-control-blue">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            
                                                            <div class="col-sm-4">
                                                                <label class="control-label">Shift Category &nbsp;<span class="vd_red">* </span></label>
                                                                <div class="controls">
                                                                    <asp:DropDownList ID="drpdes" runat="server" CssClass="form-control-blue">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4 mgbt-lg-15">
                                                                <label class="control-label">Designation &nbsp;<span class="vd_red">* </span></label>
                                                                <div class="controls">
                                                                    <asp:DropDownList ID="drpEmpdes" runat="server" CssClass="form-control-blue">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                             <div class="col-sm-4">
                                                                 <label class="control-label">Employee Category &nbsp;<span class="vd_red">* </span></label>
                                                                 <div class="controls">
                                                                     <asp:DropDownList ID="drpempcategory" runat="server" CssClass="form-control-blue">
                                                                     </asp:DropDownList>
                                                                 </div>
                                                             </div>
                                                        </div>
                                                        <div class="row">
                                                           
                                                            <div class="col-md-4" runat="server" id="divPfNo">
                                                                <label class="control-label">EPF No. <span class="vd_red"></span></label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtPFNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4 mgbt-lg-15" runat="server" id="divUanNo">
                                                                <label class="control-label">UAN<span class="vd_red"></span></label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtUAN" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4 mgbt-lg-15" runat="server" id="divEsicNo">
                                                                <label class="control-label">ESIC No. <span class="vd_red"></span></label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtEsicNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-4 ">
                                                                <label class="control-label">Machine ID &nbsp;<span class="vd_red">*</span></label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtMachineid" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4 ">
                                                                <label class="control-label">Machine No. &nbsp;<span class="vd_red">*</span></label>
                                                                <div class="controls">
                                                                    <asp:DropDownList runat="server" ID="ddlMachineNo" CssClass="form-control-blue validatedrp">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4 mgbt-lg-15">
                                                                <label class="control-label">File No.</label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtFileno" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label class="control-label">Reference</label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtrefere" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-8 mgbt-lg-15">
                                                                <label class="control-label">Training Details</label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtTrainingDetails" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label class="control-label">Teaching Subjects</label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtTeachingSubjects" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label class="control-label">Remark</label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtremak" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="col-md-6 full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Shift Details</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row">
                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">Employment Type &nbsp;<span class="vd_red">* </span></label>
                                                                    <div class="mgtp-6">
                                                                        <asp:RadioButtonList ID="rblEmploymentType" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow" runat="server">
                                                                            <asp:ListItem Text="Full Time" Value="FullTime" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Part Time" Value="PartTime"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">Shift &nbsp;<span class="vd_red">* </span></label>
                                                                    <div class="controls">
                                                                        <asp:UpdatePanel runat="server" ID="upShift">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="ddlEmpShift" OnSelectedIndexChanged="ddlEmpShift_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="form-control-blue validatedrp">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                          
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12  ">
                                                                <asp:UpdatePanel runat="server" ID="upGV">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="gvShiftDetail" runat="server" CssClass="Grid" AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ShiftName" HeaderText="Shift Name" />
                                                                                <asp:BoundField DataField="ShiftTime" HeaderText="Shift Time" />
                                                                                <asp:BoundField DataField="FromTime" HeaderText="From Time" />
                                                                                <asp:BoundField DataField="ToTime" HeaderText="To Time" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </ContentTemplate>
                                                                
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <div class="col-md-6 full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Bank Details</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row">
                                                            <div class="col-sm-12 ">
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">PAN</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtpanno" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <asp:UpdatePanel runat="server" ID="dsf">
                                                                    <ContentTemplate>
                                                                        <div class="col-sm-4">
                                                                            <label class="control-label">Bank</label>
                                                                            <div class="">
                                                                                <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 mgbt-lg-15">
                                                                            <label class="control-label">Branch</label>
                                                                            <div class="">
                                                                                <asp:DropDownList ID="ddlBankBranch" runat="server" OnSelectedIndexChanged="ddlBankBranchDetails_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>


                                                                        <div class="col-sm-4">
                                                                            <label class="control-label">IFSC</label>
                                                                            <div class="">
                                                                                <asp:TextBox ID="txtbranchifsc" runat="server" CssClass="form-control-blue" ReadOnly="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                                            <label class="control-label">Address</label>
                                                                            <div class="">
                                                                                <asp:TextBox ID="bankbranchadd" runat="server" TextMode="MultiLine" ReadOnly="true" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4" style="margin-bottom:18px">
                                                                            <label class="control-label">Postal / Zip Code</label>
                                                                            <div class="">
                                                                                <asp:TextBox ID="branchpincode" runat="server" CssClass="form-control-blue" ReadOnly="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">A/C No.</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">A/C Type</label>
                                                                    <div class="">
                                                                        <asp:DropDownList ID="ddlAccType" runat="server" CssClass="form-control-blue">
                                                                            <asp:ListItem title="Currant Account" Text="CA" Value="CA" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem title="Cash Credit" Text="CC" Value="CC"></asp:ListItem>
                                                                            <asp:ListItem title="Saving Account" Text="SV" Value="SV"></asp:ListItem>
                                                                            <asp:ListItem title="Recurring Deposit" Text="RD" Value="RD"></asp:ListItem>
                                                                            <asp:ListItem title="Fixed Deposit Receipt" Text="FDR" Value="FDR"></asp:ListItem>
                                                                            <asp:ListItem title="LOAN" Text="LOAN" Value="LOAN"></asp:ListItem>
                                                                            <asp:ListItem title="Public Provident Fund" Text="PPF" Value="PPF"></asp:ListItem>
                                                                            <asp:ListItem title="Other" Text="Other" Value="OT"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div style="margin-bottom: 268px;">&nbsp;</div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="tab-pane" id="Documents1">
                                        <div class="row mgbt-xs-20">
                                            <div class="col-sm-12  full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-file-archive-o"></i></span>&nbsp; Document </h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-placement="bottom"
                                                                class=" menu entypo-icon">
                                                                <i class="icon-minus3"></i>
                                                            </div>

                                                        </div>
                                                        <!-- vd_panel-menu -->

                                                    </div>
                                                    <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                        <div class="row">
                                                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Repeater ID="Repeater1" runat="server">
                                                                        <ItemTemplate>
                                                                            <div class="col-sm-12  no-padding ">
                                                                                <div class="col-sm-4">


                                                                                    <asp:Label ID="lblDocument" class="control-label"
                                                                                        runat="server" Text='<%# Bind("DocumentType") %>'></asp:Label>
                                                                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>

                                                                                    <div class="controls img-input-ped">
                                                                                        <asp:FileUpload ID="FileUpload4"
                                                                                            CssClass="form-control-blue " runat="server"
                                                                                            onChange="checksFileSizeandFileTypeinupdatePanel_witin_Repeater(this,'500000',
                                                                                                        'pdf|doc|docx|txt|jpg|jpeg|png|gif|JPG|PNG|JPEG|GIF',
                                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_Repeater1_hfFile',
                                                                                                         'ContentPlaceHolder1_ContentPlaceHolderMainBox_Repeater1_hdfilefileExtention',
                                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_Repeater1_Chksoft');
                                                                                                        GetChecked();" />
                                                                                        <div class="text-box-msg">

                                                                                            <asp:HiddenField ID="hfFile" runat="server" />
                                                                                            <asp:HiddenField ID="hdfilefileExtention" runat="server" />
                                                                                        </div>
                                                                                    </div>

                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 btn-a-devices-1-p4-p2 ">
                                                                                    <div class="col-sm-4 col-xs-6 mgtp-6">

                                                                                        <asp:CheckBox ID="Chksoft" runat="server" class="vd_checkbox checkbox-success"
                                                                                            TextAlign="Right" Text="Softcopy" />

                                                                                    </div>
                                                                                    <div class="col-sm-4 col-xs-6 mgtp-6">
                                                                                        <asp:CheckBox ID="Chkhard" runat="server" class="vd_checkbox checkbox-success"
                                                                                            TextAlign="Right" Text="Hardcopy" />
                                                                                    </div>
                                                                                    <div class="col-sm-4 col-xs-6 mgtp-6">

                                                                                        <asp:CheckBox ID="chkVerified" runat="server" Text="Verified"
                                                                                            class="vd_checkbox checkbox-success" TextAlign="Right" />

                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                                                    <label class="control-label">Remark</label>
                                                                                    <div class="vd_input-wrapper ">

                                                                                        <asp:TextBox ID="txtRemark" placeholder="" runat="server" TextMode="MultiLine"
                                                                                            Rows="1" CssClass="form-control-blue"></asp:TextBox>

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12  no-padding ">
                                                                                <hr />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>


                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- Panel Widget -->
                                            </div>
                                        </div>

                                    </div>


                                    <div class="tab-pane" id="posts-tab">
                                        <div class="row mgbt-xs-20">
                                            <%-- // Educ --%>

                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                <ContentTemplate>
                                                    <asp:Repeater ID="rptPreviousEducation" runat="server">
                                                        <ItemTemplate>
                                                            <div class="col-sm-6  full-width-100">
                                                                <div class="panel widget">
                                                                    <div class="panel-heading vd_bg-dark-blue">
                                                                        <h3 class="panel-title"><span class="menu-icon"><i class=" icon-graduation"></i></span>Qualification Details
                                                                  <asp:Label ID="lblsrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                        </h3>
                                                                        <div class="vd_panel-menu ">
                                                                            <div data-action="minimize" title="Minimize" data-placement="bottom"
                                                                                class=" menu entypo-icon">
                                                                                <i class="icon-minus3"></i>
                                                                            </div>

                                                                        </div>
                                                                        <!-- vd_panel-menu -->
                                                                    </div>
                                                                    <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                                        <div id="rpt" class="row">
                                                                            <div class="col-sm-4">
                                                                                <label class="control-label">Name of Examination</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                                                    <asp:TextBox ID="txtExam" Text='<%# Bind("Exam") %>'
                                                                                        onkeyup="enableControlNew('rpt',this,'.form-control-blue');"
                                                                                        Enabled="true" runat="server" CssClass="form-control-blue">
                                                                                                <div class="text-box-msg"></div>
                                                                                    </asp:TextBox>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-sm-4">
                                                                                <label class="control-label">Board/ University</label>
                                                                                <div class="">
                                                                                    <asp:DropDownList ID="drpBoard" runat="server"
                                                                                        CssClass="form-control-blue ">
                                                                                    </asp:DropDownList>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-sm-4">
                                                                                <label class="control-label">Select Result</label>
                                                                                <div class="">
                                                                                    <asp:DropDownList ID="drpResult" runat="server"
                                                                                        CssClass="form-control-blue ">
                                                                                        <asp:ListItem Value="P">Passed</asp:ListItem>
                                                                                        <asp:ListItem Value="F">Failed</asp:ListItem>
                                                                                        <asp:ListItem Value="RA">Result Awaited</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-sm-12">
                                                                                <label class="control-label">Institute Name</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-institution"></i></span>
                                                                                    <asp:TextBox ID="txtInstitute" Text='<%# Bind("Institute") %>'
                                                                                        runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-sm-4">
                                                                                <label class="control-label">Passing Year</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-trophy"></i></span>
                                                                                    <asp:TextBox ID="txtYop" Text='<%# Bind("Yop") %>' runat="server"
                                                                                        CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-sm-4">
                                                                                <label class="control-label">Select Medium</label>
                                                                                <div class="">
                                                                                    <asp:DropDownList ID="drpMedium" runat="server"
                                                                                        CssClass="form-control-blue ">
                                                                                        <asp:ListItem Value="English" Selected="True">English</asp:ListItem>
                                                                                        <asp:ListItem Value="Hindi">Hindi</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>

                                                                            </div>

                                                                            <div class="col-sm-4">
                                                                                <label class="control-label">Subject</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-book"></i></span>
                                                                                    <asp:TextBox ID="txtSubject" Text='<%# Bind("Subject") %>'
                                                                                        runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <label class="control-label">Roll No.</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-bullhorn"></i></span>
                                                                                    <asp:TextBox ID="txtRollNo" Text='<%# Bind("RollNo") %>'
                                                                                        runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <label class="control-label">Certificate No.</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="glyphicon glyphicon-certificate"></i></span>
                                                                                    <asp:TextBox ID="txtCertificateNo" Text='<%# Bind("CertificateNo") %>'
                                                                                        runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <label class="control-label">Marks Sheet No.</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-shield"></i></span>
                                                                                    <asp:TextBox ID="txtMarksSheetNo" Text='<%# Bind("MarksSheetNo") %>'
                                                                                        runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <label class="control-label">Max Marks</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-thumbs-up"></i></span>
                                                                                    <asp:TextBox ID="txtMM" Text='<%# Bind("MaxMarks") %>'
                                                                                        onkeyup="CheckIntegerValueonKeyUp(event, this);"
                                                                                        runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <label class="control-label">Obtained Marks</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa  fa-thumbs-o-up"></i></span>
                                                                                    <asp:TextBox ID="txtObtained" Text='<%# Bind("Obtained") %>'
                                                                                        onkeyup="SetPercentage(event,this,
                                                                                                '#ContentPlaceHolder1_ContentPlaceHolderMainBox_rptPreviousEducation_txtPer',
                                                                                                '#ContentPlaceHolder1_ContentPlaceHolderMainBox_rptPreviousEducation_txtMM');"
                                                                                        runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4  half-width-50 half-width-100-tc">
                                                                                <label class="control-label">Percent / Grade</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                                    <asp:TextBox ID="txtPer" Text='<%# Bind("Per") %>'
                                                                                        runat="server"
                                                                                        CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4" style="display: none">
                                                                                <label class="control-label">Country</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                                    <asp:TextBox ID="txtCountry" runat="server"
                                                                                        CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4" style="display: none">
                                                                                <label class="control-label">State</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                                    <asp:TextBox ID="txtState" runat="server"
                                                                                        CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-6 " style="display: none">
                                                                                <label class="control-label">City</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                                    <asp:TextBox ID="txtCity" runat="server"
                                                                                        CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </div>
                                                                            </div>


                                                                            <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 hide">
                                                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_OnClick"
                                                                                            CssClass="button form-control-blue" runat="server"> 
                                                                                                        <i class="glyphicon glyphicon-trash"></i> &nbsp; Delete Full Details </asp:LinkButton>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>


                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <!-- Panel Widget -->
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <div class="col-sm-12  text-center hide">
                                                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkAddMore" OnClick="lnkAddMore_OnClick" CssClass="button form-control-blue" runat="server"> 
                                                                            <i class="fa fa-plus-square"></i> &nbsp; Add Qualification Details Box </asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>


                                        </div>
                                    </div>

                                    <div class="tab-pane" id="list-tab">
                                        <div class="row mgbt-xs-20">
                                            <asp:UpdatePanel ID="UpdatePanel203" runat="server">
                                                <ContentTemplate>
                                                    <asp:Repeater ID="reppreviousemployment" runat="server">
                                                        <ItemTemplate>
                                                            <div class="col-md-6 full-width-100">
                                                                <div class="panel widget">
                                                                    <div class="panel-heading vd_bg-dark-blue">
                                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Previous Employment Details
                                                                            <asp:Label ID="lblsrno1" runat="server" Text='<%# Bind("srno1") %>'></asp:Label>
                                                                        </h3>
                                                                        <div class="vd_panel-menu ">
                                                                            <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

                                                                        </div>
                                                                        <!-- vd_panel-menu -->
                                                                    </div>
                                                                    <div class="panel-body form-main-box-border-g">
                                                                        <div class="row">
                                                                            <div class="col-sm-12  ">
                                                                                <div class="col-sm-4">
                                                                                    <label class="control-label">Country</label>
                                                                                    <div class="">
                                                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="DrpCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpCountry_SelectedIndexChanged" CssClass="form-control-blue">
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>

                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <label class="control-label">State</label>
                                                                                    <div class="">
                                                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="DrpState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpState_SelectedIndexChanged" CssClass="form-control-blue">
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <label class="control-label">City</label>
                                                                                    <div class="">
                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="DrpCity" runat="server" AutoPostBack="True"
                                                                                                    CssClass="form-control-blue">
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <label class="control-label">Name of Organization</label>
                                                                                    <div class="">
                                                                                        <asp:TextBox ID="txtName0rga" Text='<%# Bind("NameofOrganization") %>' runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <label class="control-label">Type of Organization</label>
                                                                                    <div class="">
                                                                                        <asp:TextBox ID="drptyporganizat" Text='<%# Bind("TypeofOrganization") %>' runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <label class="control-label">Designation</label>
                                                                                    <div class="">
                                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="DrpDesignation" Text='<%# Bind("Designation") %>' runat="server" CssClass="form-control-blue">
                                                                                                </asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <label class="control-label">Profile</label>
                                                                                    <div class="">
                                                                                        <asp:TextBox ID="txtProfile" Text='<%# Bind("Profile") %>' runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-sm-4">
                                                                                    <label class="control-label">Details</label>
                                                                                    <div class="">
                                                                                        <asp:TextBox ID="txtdealin" Text='<%# Bind("Details") %>' runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                                                    <label class="control-label">Address</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtaddres" Text='<%# Bind("Address") %>' runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                                                    <label class="control-label">Reason of Resign</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtreasonresign" Text='<%# Bind("ReasonofResign") %>' runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <label class="control-label">From</label>
                                                                                    <div class="">
                                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="DrpYear2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpYear2_SelectedIndexChanged"
                                                                                                    CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                                                </asp:DropDownList>
                                                                                                <asp:DropDownList ID="DrpMonth2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMonth2_SelectedIndexChanged"
                                                                                                    CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                                                </asp:DropDownList>
                                                                                                <asp:DropDownList ID="DrpDate2" runat="server" AutoPostBack="True"
                                                                                                    CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4">
                                                                                    <label class="control-label">To</label>
                                                                                    <div class="controls">
                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="DrpYear3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpYear3_SelectedIndexChanged" CssClass="form-control-blue col-xs-4 select-box-padd" />
                                                                                                <asp:DropDownList ID="DrpMonth3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMonth3_SelectedIndexChanged"
                                                                                                    CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                                                </asp:DropDownList>
                                                                                                <asp:DropDownList ID="DrpDate3" runat="server" AutoPostBack="True"
                                                                                                    CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 ">
                                                                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:LinkButton ID="lnkDelete1" OnClick="lnkDelete1_OnClick"
                                                                                                CssClass="button form-control-blue" runat="server"> 
                                                                                        <i class="glyphicon glyphicon-trash"></i> &nbsp; Delete Full Details </asp:LinkButton>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>


                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <div class="col-sm-12  text-center">
                                                <asp:UpdatePanel ID="UpdatePanel2003" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkAddMore1" OnClick="lnkAddMore1_OnClick" CssClass="button form-control-blue" runat="server"> <i class="fa fa-plus-square"></i> &nbsp; Add Previous Employment Box </asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="tab-pane" id="list-tab2">
                                        <div class="row mgbt-xs-20">

                                            <div class="col-md-6 full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Earning Details</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row">
                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">Basic</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">HRA</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">Conveyance</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4">
                                                                    <label class="control-label">Spl. Allowance</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">Other Allowance</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">Total</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-6 full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Deduction Details</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row">
                                                            <div class="col-sm-12 ">
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">PF</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">ESI</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">Other</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <label class="control-label">Total</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control-blue"></asp:TextBox>
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
                                <div class=" text-left">
                                    <asp:UpdatePanel ID="UpdatePanel88" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton runat="server" OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();" ID="LinkButton1" OnClick="LinkButton1_Click" CssClass="button">Submit</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 75px;"></div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="LinkButton1" />
                                            <%--<asp:AsyncPostBackTrigger ControlID="lnk_1" EventName="Click" />--%>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--  </ContentTemplate>
        </asp:UpdatePanel>--%>
</asp:Content>
