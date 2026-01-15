<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StudentQuickRegistrationUpdation.aspx.cs" Inherits="admin_student_registration_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
        <ContentTemplate>
            <div class="col-lg-12">
                <div class="panel widget light-widget">
                    <div class="panel-body">
                        <div class="col-lg-12 no-padding">
                            <div class="vd_input-margin2 col-sm-2 no-padding">
                                <div class="vd_input-wrapper">
                                    <asp:DropDownList ID="DrpEnter" runat="server" CssClass="form-control-blue width-100">
                                        <asp:ListItem Value="srno">S.R. No.</asp:ListItem>
                                        <asp:ListItem Value="StEnRCode">Enrollment  No.</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="vd_input-margin2 col-sm-6 no-padding">
                                <div class="vd_input-wrapper">
                                    <span class="menu-icon"><i class="fa fa-eye"></i></span>
                                    <asp:TextBox ID="TxtEnter" runat="server" onchange="javascript:return fillTextbox();" class="form-control-blue width-100 "></asp:TextBox>
                                </div>
                            </div>

                            <div class=" vd_input-margin2 col-sm-2 no-padding">
                                <div class="col-sm-12 no-padding">
                                    <div class=" pull-left">
                                        <asp:LinkButton ID="lnkShow" runat="server" class="btn-joint btn vd_bg-blue vd_white width-100 " OnClick="lnkShow_Click">View</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class=" vd_input-margin2 col-sm-2 no-padding">
                                <div class="col-sm-12 no-padding">
                                    <div class=" pull-right">
                                        <asp:LinkButton ID="lnkRegistration" runat="server" class="btn-joint btn vd_bg-blue vd_white width-100 " OnClick="lnkRegistration_Click">Full Updation</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 vd_input-margin no-padding">
                            <div class="tabs">
                                <ul class="nav nav-tabs nav-justified">
                                    <li class="active"><a href="#home-tab" data-toggle="tab"><span class="menu-icon"><i class="glyphicon glyphicon-list-alt"></i></span>&nbsp; Update Registration Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                </ul>
                                <div class="tab-content form-box-border mgbt-xs-20">
                                    <div class="tab-pane active " id="home-tab">
                                        <div class="row mgbt-xs-20">
                                            <div class="col-md-12 no-padding full-width-100">
                                                <div class="col-md-6 no-padding full-width-100">
                                                    <div class="col-md-12 full-width-100">
                                                        <div class="panel widget">
                                                            <div class="panel-heading vd_bg-dark-blue">
                                                                <h3 class="panel-title"><span class="menu-icon"><i class="fa  fa-child"></i></span>Student's Personal Details </h3>
                                                                <div class="vd_panel-menu ">
                                                                    <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                                                                </div>
                                                            </div>
                                                            <div runat="server" id="div1" class="panel-body form-main-box-border">
                                                                <div class="row">
                                                                    <div class="col-md-4 vd_input-margin2">
                                                                        <div class="vd_input-wrapper">
                                                                            <span class="menu-icon"><i class="fa  fa-mobile"></i></span>
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtsrno" placeholder="S.R. No." runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-4 vd_input-margin2">
                                                                        <div class="vd_input-wrapper ">
                                                                            <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtFirstNa" placeholder="First Name*" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4 vd_input-margin2">
                                                                        <div class="vd_input-wrapper ">
                                                                            <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtMidNa" placeholder="Middle Name" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4 vd_input-margin2">
                                                                        <div class="vd_input-wrapper">
                                                                            <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtlast" placeholder="Last Name" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-4 vd_input-margin2">
                                                                        <div class="vd_input-wrapper">
                                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                            <asp:TextBox ClientIDMode="Static" ID="txtStudentDOB" placeholder="Date of Birth" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-4 vd_input-margin2">
                                                                        <div class="vd_input-wrapper">
                                                                            <span class="menu-icon"><i class="fa  fa-mobile"></i></span>
                                                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtMobile" placeholder="Mobile No." runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-12 mgbt-xs-12">

                                                                        <label class="col-sm-2 vd_input-margin no-padding control-label">Gender</label>
                                                                        <div class="col-sm-10 vd_input-margin no-padding controls">
                                                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:RadioButtonList ID="rbGender" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                                                        <asp:ListItem Value="Male" Selected="True">Male</asp:ListItem>
                                                                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                                                                        <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rbGender" Css="" ErrorMessage="*" SetFocusOnError="True" Style="color: #CC3300" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                        </div>
                                                        <!-- Panel Widget -->
                                                    </div>

                                                    <div class="col-md-12 full-width-100">
                                                        <div class="panel widget">
                                                            <div class="panel-heading vd_bg-dark-blue">
                                                                <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-male"></i></span>Father's Details </h3>
                                                                <div class="vd_panel-menu ">
                                                                    <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

                                                                </div>
                                                                <!-- vd_panel-menu -->

                                                            </div>
                                                            <div class="panel-body form-main-box-border">
                                                                <div class="row">
                                                                    <div class="col-md-6 marg-bot-60">
                                                                        <div class="vd_input-wrapper">
                                                                            <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtFatherName" runat="server" placeholder="Father's Name"
                                                                                        CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="vd_input-wrapper">
                                                                            <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtFamilyContactNo" runat="server" placeholder="Family Contact No."
                                                                                        CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- Panel Widget -->
                                                    </div>
                                                </div>
                                                <div class="col-md-6 no-padding full-width-100">
                                                    <div class="col-md-12 full-width-100">
                                                        <div class="panel widget">
                                                            <div class="panel-heading vd_bg-dark-blue">
                                                                <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>&nbsp; Admission Details </h3>
                                                                <div class="vd_panel-menu ">
                                                                    <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>

                                                                </div>
                                                                <!-- vd_panel-menu -->

                                                            </div>
                                                            <div class="panel-body form-main-box-border">
                                                                <div class="row">
                                                                    <div class="col-md-12 no-padding ">
                                                                        <div class="col-md-6 vd_input-margin20">
                                                                            <div class="vd_input-wrapper ">
                                                                                <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                <asp:TextBox ClientIDMode="Static" ID="txtDOA" placeholder="Date of Admission" runat="server" CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-6 vd_input-margin20">
                                                                            <div class="vd_input-wrapper">
                                                                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ClientIDMode="Static" ID="txtCardNo" placeholder="Enter Card No." runat="server"
                                                                                            CssClass="form-control-blue"></asp:TextBox>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-12 no-padding ">
                                                                        <div class="col-md-6">
                                                                            <div class="vd_input-wrapper ">
                                                                                <asp:UpdatePanel ID="UpdatePanel59" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="DropCourse" runat="server" CausesValidation="True" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                                                            OnSelectedIndexChanged="DropCourse_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6 ">
                                                                            <div class="vd_input-wrapper marg-bot-25 ">
                                                                                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="DropAdmissionClass" runat="server" AutoPostBack="True"
                                                                                            CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="DropAdmissionClass_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 no-padding ">

                                                                        <div class="col-md-6 ">
                                                                            <div class="vd_input-wrapper ">
                                                                                <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="DropBranch" runat="server" CausesValidation="True" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="vd_input-wrapper marg-bot-25 ">
                                                                                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="DropSection" runat="server" CausesValidation="True" CssClass="form-control-blue validatedrp"></asp:DropDownList>

                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 no-padding ">

                                                                        <div class="col-md-6 ">
                                                                            <div class="vd_input-wrapper marg-bot-25">
                                                                                <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="drpMedium" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>

                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-6 ">
                                                                            <div class="vd_input-wrapper ">
                                                                                <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="DrpBoard" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 no-padding ">

                                                                        <div class="col-md-6 ">
                                                                            <div class="vd_input-wrapper marg-bot-25">
                                                                                <asp:DropDownList ID="DrpTypeofAdmission" runat="server" CssClass="form-control-blue validatedrp">
                                                                                    <asp:ListItem Text="<-- Select Type of Admission -->"></asp:ListItem>
                                                                                    <asp:ListItem Selected="True">New</asp:ListItem>
                                                                                    <asp:ListItem>Old</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-6 ">
                                                                            <div class="vd_input-wrapper ">
                                                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="Dropcard" runat="server" CssClass="form-control-blue validatedrp">
                                                                                        </asp:DropDownList>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 no-padding ">

                                                                        <div class="col-md-6 ">
                                                                            <div class="vd_input-wrapper marg-bot-25">
                                                                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="Drophouse" runat="server" CssClass="form-control-blue validatedrp">
                                                                                        </asp:DropDownList>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-6">
                                                                            <div class="vd_input-wrapper ">
                                                                                <asp:DropDownList ID="drpFeeDepositMOD" runat="server" CssClass="form-control-blue">
                                                                                    <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                                    <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                                                    <asp:ListItem Value="S">Semester</asp:ListItem>
                                                                                    <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                        </div>
                                                                    </div>


                                                                    <div class="col-md-12 no-padding ">
                                                                        <div class="col-md-6 txt-middle-l">
                                                                            <div class="vd_input-wrapper ">
                                                                                <label class="col-sm-6 no-padding control-label">Transport Required </label>
                                                                                <div class="col-sm-6 mgbt-xs-15 no-padding controls">
                                                                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:RadioButtonList ID="rbTransport" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow" onchange="visibleFalseTableColumn('ContentPlaceHolder1_ContentPlaceHolderMainBox_drpTransportMODB',this)">
                                                                                                <asp:ListItem>Yes</asp:ListItem>
                                                                                                <asp:ListItem Selected="True">No</asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6" id="drpTransportMODB" runat="server" style="display: none">
                                                                            <div class="vd_input-wrapper ">
                                                                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="drpTransportMOD" runat="server" CssClass="form-control-blue vd_input-margin20">
                                                                                            <asp:ListItem Text="<-- Payment Frequency -->"></asp:ListItem>
                                                                                            <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                                            <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                                                            <asp:ListItem Value="S">Semester</asp:ListItem>
                                                                                            <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>

                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- Panel Widget -->
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row ">
                                                <div class="col-md-12 ">
                                                    <div class="btn-center-box-130">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="LinkButton14" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"
                                                                    ValidationGroup="A" CssClass="btn vd_btn vd_bg-blue" runat="server" OnClick="LinkButton14_Click">  <i class="fa fa-paper-plane"></i> &nbsp; Submit Details </asp:LinkButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

