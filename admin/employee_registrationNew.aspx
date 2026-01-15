<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="employee_registrationNew.aspx.cs"
    Inherits="admin_employee_registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .MyTabStyle .ajax__tab_header {
            font-size: 14px;
            font-weight: bold;
            display: block;
            color: #222;
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
                padding: 5px 10px 4px 0px;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body ">
                        <div class="col-sm-12  vd_input-margin ">
                            <div class="tabs">
                                <ul class="nav nav-tabs nav-justified">
                                    <li class="active"><a href="#home-tab" data-toggle="tab"><span class="menu-icon"><i class="glyphicon glyphicon-list-alt"></i></span>&nbsp; General Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                    <li><a href="#posts-tab" data-toggle="tab"><span class="menu-icon"><i class="fa fa-users"></i></span>&nbsp; Other Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                    <li><a href="#list-tab" data-toggle="tab"><span class="menu-icon"><i class="icon-graduation"></i></span>&nbsp; Previous Employment <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                    <li><a href="#posts-tab2" data-toggle="tab"><span class="menu-icon"><i class=" icon-newspaper"></i></span>&nbsp; Official Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                    <li><a href="#list-tab2" data-toggle="tab"><span class="menu-icon"><i class="fa fa-file-archive-o"></i></span>&nbsp; Earning & Deduction <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                    <%-- ReSharper disable once Html.IdNotResolved --%>
                                    <li id="otherdetails" style="display: none"><a href="#poststab3" data-toggle="tab"><span class="menu-icon"><i class="glyphicon glyphicon-list-alt"></i></span>&nbsp; Scholarship Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>

                                </ul>
                                <div class="tab-content form-box-border-g mgbt-xs-20">
                                    <div class="tab-pane active " id="home-tab">
                                        <div class="row mgbt-xs-20">

                                            <div class="col-sm-6  full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="fa  fa-child"></i></span>Staff Personal Details </h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon">
                                                                <i class="icon-minus3"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                        <div class="row">
                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-4    mgbt-xs-15">
                                                                    <label class="control-label  no-padding">First Name&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="col-xs-5 no-padding">
                                                                        <asp:DropDownList ID="DrpTitle" runat="server" CssClass="form-control-blue">
                                                                            <asp:ListItem>Mr.</asp:ListItem>
                                                                            <asp:ListItem>Ms.</asp:ListItem>
                                                                            <asp:ListItem>Mrs.</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xs-7 no-padding">
                                                                        <asp:TextBox ID="txtFirstName" runat="server" onkeyup="CopyString('ContentPlaceHolder1_TabContainer1_TabPanel1_',this,'txtDisplay')"
                                                                            CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4   mgbt-xs-15">
                                                                    <label class="control-label">Middle Name</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtmidName" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4   mgbt-xs-15">
                                                                    <label class="control-label">Last Name&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtlastName" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6   mgbt-xs-15">
                                                                    <label class="control-label">Display Name&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtDisplay" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6   mgbt-xs-15">
                                                                    <label class="control-label">Date of Birth</label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpYear_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4  validatedrp">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DrpMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMonth_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4  validatedrp">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DrpDate" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4  validatedrp">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-8  ">
                                                                <div class="col-sm-12   mgbt-xs-9">
                                                                    <label class="control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="mgtp-6">
                                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" SkinID="radio"
                                                                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatLayout="Flow" CssClass="radio-success vd_radio validaterb">
                                                                            <asp:ListItem Selected="True">Male</asp:ListItem>
                                                                            <asp:ListItem>Female</asp:ListItem>
                                                                            <asp:ListItem>Transgender</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <div class="col-sm-6   mgbt-xs-15">
                                                                    <label class="control-label">Father&#39;s Name</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtfathername" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6   mgbt-xs-15">
                                                                    <label class="control-label">Mother&#39;s Name</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtmothname" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Spouse Name</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtspousename" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Marital Status</label>
                                                                    <div class="">
                                                                        <asp:DropDownList ID="DrpMaitalStatus" runat="server" CssClass="form-control-blue">
                                                                            <asp:ListItem>Single</asp:ListItem>
                                                                            <asp:ListItem>Married</asp:ListItem>
                                                                            <asp:ListItem>Divorced</asp:ListItem>
                                                                            <asp:ListItem>Widowed</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6   mgbt-xs-15">
                                                                    <label class="control-label">E-mail</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6   mgbt-xs-15">
                                                                    <label class="control-label">Mobile No.&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtmobileno" runat="server" OnTextChanged="txtmobileno_TextChanged" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <div class="col-sm-6   mgbt-xs-15">
                                                                    <label class="control-label">Emergency Contact No.&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtemergencycontactno" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6  mgbt-xs-15">
                                                                    <label class="control-label">Upload Photo&nbsp;<span class="vd_red"></span></label>
                                                                    <div class="">
                                                                        <asp:FileUpload ID="avatarUpload" runat="server" CssClass="form-control-blue" onchange="checksFileSizeandFileType(this, 16000, 'jpg|png|jpeg|gif','Avatars');" type="file" />
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4  ">
                                                                <div class="col-sm-12  mgbt-xs-15">
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

                                            <div class="col-sm-6  full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Present Address</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                                                        </div>

                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row mgbt-xs-0">
                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-4   mgbt-xs-15">
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

                                                                <div class="col-sm-4   mgbt-xs-15">
                                                                    <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpPresCity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpPresCity_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4   mgbt-xs-15">
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

                                                                <div class="col-sm-12  ">
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
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                                                        </div>
                                                    </div>

                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row mgbt-xs-5">

                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-4   mgbt-xs-15">
                                                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" CssClass="vd_checkbox checkbox-success" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Same as Present Address" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-sm-12  no-padding">
                                                                    <div class="col-sm-4  mgbt-xs-15">
                                                                        <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                                                        <div class="">
                                                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:DropDownList ID="DrpPerState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpPerState_SelectedIndexChanged"
                                                                                        CssClass="form-control-blue validatedrp">
                                                                                    </asp:DropDownList>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-4  mgbt-xs-15">
                                                                        <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                                        <div class="">
                                                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:DropDownList ID="DrpPerCity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpPerCity_SelectedIndexChanged"
                                                                                        CssClass="form-control-blue validatedrp">
                                                                                    </asp:DropDownList>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-4  mgbt-xs-15">
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

                                                                    <div class="col-sm-12  ">
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
                                    </div>

                                    <div class="tab-pane" id="posts-tab">
                                        <div class="row mgbt-xs-20">
                                            <div class="col-md-6 full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Other Details</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row mgbt-xs-0">
                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Religion&nbsp;<span class="vd_red">* </span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpReligion" runat="server" AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Nationality</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtnat" runat="server" CssClass="form-control-blue">Indian</asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Category&nbsp;<span class="vd_red">* </span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpCategory" runat="server" AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Caste</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtcaste" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Height</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtheight" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Weight</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtweight" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Diseases</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtdiseas" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Blood Group &nbsp;<span class="vd_red">* </span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="drpblood" runat="server" AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Identification Mark</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtidentmark" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Hobbies</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txthobbies" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                                    <asp:Label ID="Label2" runat="server" class="control-label" Text="Hostel Required"></asp:Label>
                                                                    <div class="mgtp-6">
                                                                        <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal" SkinID="radio" RepeatLayout="Flow" CssClass="radio-success vd_radio"
                                                                            OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged">
                                                                            <asp:ListItem>Yes</asp:ListItem>
                                                                            <asp:ListItem Selected="True">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <asp:Label ID="Label3" runat="server" class="control-label" Text="Transport Required"></asp:Label>
                                                                    <div class="mgtp-6">
                                                                        <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal" SkinID="radio" RepeatLayout="Flow" CssClass="radio-success vd_radio">
                                                                            <asp:ListItem>Yes</asp:ListItem>
                                                                            <asp:ListItem Selected="True">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        <div class="text-box-msg">
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

                                    <div class="tab-pane" id="list-tab">
                                        <div class="row mgbt-xs-20">
                                            <div class="col-md-6 full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Previous Employment Details</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row mgbt-xs-0">
                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Country</label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpCountry_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>

                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">State</label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpState_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">City</label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpCity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpCity_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Type of Organization</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="drptyporganizat" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Designation</label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpDesignation" runat="server" CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Profile</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtProfile" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Name of Organisataion</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtName0rga" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Deals</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtdealin" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
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
                                                                                <asp:DropDownList ID="DrpDate2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpDate2_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">To</label>
                                                                    <div class="controls">
                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpYear3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpYear3_SelectedIndexChanged"
                                                                                    SkinID="ddlSize2" CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DrpMonth3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMonth3_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="DrpDate3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpDate3_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue col-xs-4 select-box-padd">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                                    <label class="control-label">Address</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="txtaddres" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                                    <label class="control-label">Reason of Resign</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="txtreasonresign" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
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
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row mgbt-xs-0">
                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Joining Date</label>
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
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Employee Id &nbsp;<span class="vd_red">* </span></label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Department &nbsp;<span class="vd_red">* </span></label>
                                                                    <div class="controls">
                                                                        <asp:DropDownList ID="txtDepartmentName" runat="server" CssClass="form-control-blue validatedrp">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">File No.</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="txtFileno" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Designation &nbsp;<span class="vd_red">* </span></label>
                                                                    <div class="controls">
                                                                        <asp:DropDownList ID="drpdes" AutoPostBack="true" OnSelectedIndexChanged="DrpDesignation_SelectedIndexChanged" runat="server" CssClass="form-control-blue validatedrp">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Employee Category &nbsp;<span class="vd_red">* </span></label>
                                                                    <div class="controls">
                                                                        <asp:DropDownList ID="drpempcategory" runat="server" CssClass="form-control-blue validatedrp">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Reference</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="txtrefere" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-8  half-width-50 mgbt-xs-9">
                                                                    <label class="control-label">Remark</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="txtremak" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
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
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Shift Details</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row mgbt-xs-0">
                                                            <div class="col-sm-12  ">
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Employment Type &nbsp;<span class="vd_red">* </span></label>
                                                                    <div class="mgtp-6">
                                                                        <asp:RadioButtonList ID="rblEmploymentType" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow" runat="server">
                                                                            <asp:ListItem Text="Full Time" Value="FullTime" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Part Time" Value="PartTime"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Shift &nbsp;<span class="vd_red">* </span></label>
                                                                    <div class="controls">
                                                                        <asp:UpdatePanel runat="server" ID="upShift">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="ddlEmpShift" OnSelectedIndexChanged="ddlEmpShift_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="form-control-blue validatedrp">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <%--    <asp:PostBackTrigger ControlID="ddlEmpShift" />--%>
                                                                            </Triggers>
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
                                                                    <Triggers>
                                                                        <%--    <asp:PostBackTrigger ControlID="ddlEmpShift" />--%>
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="tab-pane" id="list-tab2">

                                        <div class="row mgbt-xs-20">
                                            <div class="col-sm-12  ">
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">CTC</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtCTC" runat="server" AutoPostBack="true" OnTextChanged="txtCTC_TextChanged" CssClass="form-control-blue"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row mgbt-xs-20">
                                            <div class="col-md-6 full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Earning Details</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row mgbt-xs-0">
                                                            <div class="col-sm-12  ">

                                                                <asp:Repeater ID="repEarnings" runat="server" OnItemDataBound="repEarnings_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <asp:Label ID="lbl" runat="server" Text='<%# Eval("Component") %>' class="control-label"></asp:Label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="txt" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <asp:Label ID="lblHR01ID" Visible="false" Text='<%# Eval("HR01ID") %>' runat="server" class="control-label"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>

                                                                <%--   <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Basic</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">HRA</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Conveyance</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Spl. Allowance</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Other Allowance</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Total</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>--%>
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
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row mgbt-xs-0">
                                                            <div class="col-sm-12 ">

                                                                <asp:Repeater ID="repDeductions" runat="server" OnItemDataBound="repDeductions_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <asp:Label ID="lbl" runat="server" Text='<%# Eval("Component") %>' class="control-label"></asp:Label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="txt" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <asp:Label ID="lblHR01ID" Visible="false" Text='<%# Eval("HR01ID") %>' runat="server" class="control-label"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>


                                                                <%--  <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">PF</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">ESI</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Other</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Total</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6 full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Bank Details</h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

                                                        </div>
                                                        <!-- vd_panel-menu -->
                                                    </div>
                                                    <div class="panel-body form-main-box-border-g">
                                                        <div class="row mgbt-xs-0">
                                                            <div class="col-sm-12 ">
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">PAN</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Bank</label>
                                                                    <div class="">
                                                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Branch</label>
                                                                    <div class="">
                                                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">IFSC</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                                    <label class="control-label">Address</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="TextBox15" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Postal / Zip Code</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">A/C No.</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">A/C Type</label>
                                                                    <div class="">
                                                                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control-blue">
                                                                        </asp:DropDownList>
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
                                <div class=" text-center">


                                    <asp:LinkButton runat="server" OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();" ID="LinkButton1" OnClick="LinkButton1_Click" CssClass="button">Submit</asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
