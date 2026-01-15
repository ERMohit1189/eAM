<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StudentQuickRegistration.aspx.cs" Inherits="admin_student_registration_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">


                        <div class="col-sm-12  ">
                            <div class="col-sm-4   no-padding mgbt-xs-15">
                                <div class="vd_input-wrapper">
                                    <span class="menu-icon"><i class="fa fa-eye"></i></span>
                                    <asp:TextBox ID="TextBox67" placeholder="Enter Student Enquery No." runat="server" CssClass="form-control-blue"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-sm-8  no-padding">
                                <div class="col-xs-6 no-padding text-left mgbt-xs-15">
                                    <asp:LinkButton ID="LinkButton1" runat="server" class="button form-control-blue" OnClick="LinkButton1_Click"> View</asp:LinkButton>

                                </div>
                                <div class="col-xs-6 no-padding" style="display: none;">
                                    <div class=" pull-left">
                                        <asp:LinkButton ID="LinkButton15" runat="server" OnClick="LinkButton15_Click" class="button form-control-blue">Preview</asp:LinkButton>

                                    </div>
                                </div>
                               
                                <div class="col-xs-6 text-right no-padding mgbt-xs-15">

                                    <asp:LinkButton ID="lnkQuickReg" runat="server" CssClass="btn-print-box" OnClick="lnkQuickReg_Click" Style="color: #CC0000"
                                        title="Go back to Full Registration page" data-placement="left"><i class="fa fa-reply"></i></asp:LinkButton>

                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12   vd_input-margin ">

                            <%--<ul class="nav nav-tabs nav-justified">
        <li class="active"><a href="#home-tab" data-toggle="tab"><span class="menu-icon">
            <i class="glyphicon glyphicon-list-alt"></i></span>&nbsp; General Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
        <li><a href="#posts-tab" data-toggle="tab">
            <span class="menu-icon"><i class="fa fa-users"></i></span>&nbsp; Family Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>

        <li><a href="#posts-tab2" data-toggle="tab"><span class="menu-icon">
            <i class=" icon-newspaper"></i></span>&nbsp; Official Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
        <li><a href="#list-tab2" data-toggle="tab"><span class="menu-icon">
            <i class="fa fa-file-archive-o"></i></span>&nbsp; Documents <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
        <li><a href="#list-tab" data-toggle="tab"><span class="menu-icon">
            <i class="icon-graduation"></i></span>&nbsp; Previous Education Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
        <li id="otherdetails" style="display: none"><a href="#poststab3" data-toggle="tab"><span class="menu-icon">
            <i class="glyphicon glyphicon-list-alt"></i></span>&nbsp; Scholarship Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
    </ul>--%>


                            <div class="row mgbt-xs-20">

                                <div class="col-sm-12  full-width-100">
                                    <div class="panel widget">
                                        <div class="panel-body padding-tlbr-15 form-main-box-border">
                                            <div class="row">
                                                <asp:UpdatePanel ID="UpdatePanel62" runat="server">
                                                    <ContentTemplate>
                                                        <script>
                                                            Sys.Application.add_load(datetime);
                                                        </script>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">First Name&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                <asp:TextBox ID="txtFirstNa" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">Middle Name</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                <asp:TextBox ID="txtMidNa" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">Last Name</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                <asp:TextBox ID="txtlast" placeholder="" runat="server" CssClass="form-control-blue "></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">Date of Birth&nbsp;<span class="vd_red"></span></label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                <asp:TextBox ID="txtStudentDOB" placeholder="yyyy MMM dd" onchange="BeginRequestHandlerNorm();"
                                                                    runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Category&nbsp;<span class="vd_red">*</span></label>
                                                            <div class=" controls ">
                                                                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Aadhar Card No.</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                <asp:TextBox ID="txtAadharNo" placeholder="Ex. 0000-0000-0000" runat="server"  CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Father's Name&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtfaNameee" runat="server" placeholder=""
                                                                            onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtguardianname',
                                                                    '#ContentPlaceHolder1_ContentPlaceHolderMainBox_DrpRelationship','Father');"
                                                                            CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Father's Occupation&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="drpOccupationfa" runat="server"
                                                                            AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Father's Mobile No.</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-mobile"></i></span>

                                                                <asp:UpdatePanel ID="UpdatePanel52" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtFatherOfficeMobileNo" placeholder=""
                                                                            runat="server" CssClass="form-control-blue "></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                                                            CssClass="imp" runat="server"
                                                                            ControlToValidate="txtFatherOfficeMobileNo" ErrorMessage="*"
                                                                            SetFocusOnError="True"
                                                                            ValidationExpression="^[0-9]{10,10}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Mother's Name&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                <asp:TextBox ID="txtmotherNameeee" runat="server" CssClass="form-control-blue validatetxt" placeholder=""
                                                                    onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtGuardiantwoName',
                                                            '#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpGuardiantwoRelationship','Mother');"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Mother's Occupation&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel31" runat="server" EnableViewState="true" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="drpOccupationmoth"
                                                                            runat="server" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="drpOccupationmoth_SelectedIndexChanged"
                                                                            CssClass="form-control-blue validatedrp">
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Mother's Mob No.</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-phone"></i></span>

                                                                <asp:UpdatePanel ID="UpdatePanel54" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtMotherOfficePhoneNo" placeholder="" runat="server"
                                                                            CssClass="form-control-blue "></asp:TextBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  mgbt-xs-9 ">
                                                            <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtPreaddress" placeholder="Please don't write State and City name here" runat="server"
                                                                            TextMode="MultiLine" Rows="3" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Country&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DrpPreCountry" runat="server"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="DrpPreCountry_SelectedIndexChanged"
                                                                            CssClass="form-control-blue ">
                                                                            <asp:ListItem Text="<-- Select Country -->"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                        </div>

                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DrpPreState" runat="server" AutoPostBack="True"
                                                                            CssClass="form-control-blue " OnSelectedIndexChanged="DrpPreState_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                           
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DrpPreCity" runat="server" CssClass="form-control-blue">
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                           
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">S.R. No.&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                <asp:TextBox ID="txtSr" placeholder="" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Fee Category &nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="drpPanelCardType" runat="server"
                                                                            CssClass="form-control-blue validatedrp">
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                            
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Date of Admission&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                <asp:TextBox ID="TextBox100" placeholder="" runat="server"
                                                                    CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                                                <div class="text-box-msg"></div>

                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Select Course&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel59" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DropCourse" runat="server"
                                                                            CausesValidation="True" CssClass="form-control-blue validatedrp"
                                                                            AutoPostBack="True" OnSelectedIndexChanged="DropCourse_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                            
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DropAdmissionClass" runat="server" AutoPostBack="True"
                                                                            CssClass="form-control-blue validatedrp"
                                                                            OnSelectedIndexChanged="DropAdmissionClass_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                            
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel58" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="drpSection" runat="server" CausesValidation="false"
                                                                            CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg"></div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Select Branch&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DropBranch" runat="server" CausesValidation="True"
                                                                            CssClass="form-control-blue validatedrp"
                                                                            OnSelectedIndexChanged="DropBranch_SelectedIndexChanged"
                                                                            AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                         
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Select Medium&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="drpMedium" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                           
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Board/ University&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DrpBoard" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                          
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Type of Admission&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:DropDownList ID="DrpNEWOLSAdmission" runat="server" CssClass="form-control-blue">
                                                                    <asp:ListItem Selected="True">New</asp:ListItem>
                                                                    <asp:ListItem>Old</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>



                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>

                                        </div>
                                    </div>
                                    <!-- Panel Widget -->
                                    <div class="row ">
                                        <div class="col-md-12 ">
                                            <div class="btn-center-box-130">
                                                <asp:UpdatePanel ID="UpdatePanel88" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="LinkButton14" OnClick="LinkButton14_Click"
                                                            
                                                            ValidationGroup="A" CssClass="btn vd_btn vd_bg-blue" runat="server"><i class="fa fa-paper-plane"></i> &nbsp; Submit </asp:LinkButton>
                                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                    </div>
                                </div>


                            </div>

                            <div style="display: none !important">
                                <div class="col-sm-6  full-width-100" style="display: none !important">
                                    <div class="panel widget">
                                        <div class="panel-heading vd_bg-dark-blue">
                                            <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Student's Other Details </h3>
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
                                                <asp:UpdatePanel ID="UpdatePanel61" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12  no-padding ">

                                                            <div class="col-sm-8  no-padding ">


                                                                <div class="col-sm-6  mgbt-xs-15">
                                                                    <label class="control-label">Age on Date</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                        <asp:TextBox ID="txtAgeOnDate" placeholder="Age on Date" onchange="BeginRequestHandlerNorm();"
                                                                            runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-12  mgbt-xs-15">
                                                                    <div class="col-xs-4 age-box  no-padding">
                                                                        <asp:TextBox ID="txtAgeYear" placeholder="00" runat="server" Enabled="false"
                                                                            CssClass="form-control-blue text-center"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-xs-4 age-box padding-lr-5">
                                                                        <asp:TextBox ID="txtAgeMonth" placeholder="00" runat="server" Enabled="false"
                                                                            CssClass="form-control-blue text-center"></asp:TextBox>
                                                                    </div>


                                                                    <div class="col-xs-4 age-box no-padding">
                                                                        <asp:TextBox ID="txtAgeDay" placeholder="00" runat="server" Enabled="false"
                                                                            CssClass="form-control-blue text-center"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-12  mgbt-xs-15">
                                                                    <label class="col-sm-2  no-padding control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="col-sm-10 no-padding controls">
                                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="vd_radio radio-success 
                                                                    validaterblist txt-capitalize-alpha"
                                                                            RepeatDirection="Horizontal" RepeatLayout="flow">
                                                                            <asp:ListItem Value="Male" Selected="True">Male</asp:ListItem>
                                                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                                                            <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        <div class="text-box-msg">
                                                                          
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  no-padding ">
                                                                    <div class="col-sm-12  mgbt-xs-15">
                                                                        <label class="control-label">Email</label>
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa fa-envelope"></i></span>
                                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                            <div class="text-box-msg">
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12  mgbt-xs-15">
                                                                        <asp:CheckBox ID="chkStEmail" runat="server" CssClass="vd_checkbox checkbox-success  "
                                                                            RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for Email Alert" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  no-padding ">
                                                                    <div class="col-sm-12  mgbt-xs-15">
                                                                        <label class="control-label">Mobile No.</label>
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa  fa-mobile"></i></span>
                                                                            <asp:TextBox ID="txtMobile" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12  mgbt-xs-15">
                                                                        <asp:CheckBox ID="chkStMobile" runat="server" CssClass="vd_checkbox checkbox-success "
                                                                            RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for SMS Alert" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4  no-padding">

                                                                <div class="col-sm-12  mgbt-xs-15">
                                                                    <label class="control-label">Student Photo</label>
                                                                    <div class="controls img-input-ped ">
                                                                        <asp:FileUpload ID="avatarUpload" runat="server"
                                                                            onchange="checksFileSizeandFileTypeinupdatePanel(this, 50000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','Avatars',
                                                            'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdStPhoto');"
                                                                            type="file" CssClass="form-control-blue " />

                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-12  mgbt-xs-10">
                                                                    <div class="stu-pic-box">
                                                                        <div class="stu-pic-box-main">
                                                                            <asp:Image alt="" ID="Avatar" class="Avatars" runat="server" ImageUrl="../img/user-pic/student-pic.png" />
                                                                            <asp:HiddenField ID="hdStPhoto" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Mother Tongue&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-bullhorn"></i></span>
                                                                <asp:TextBox ID="txtMotherTongue" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Home Town&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-home"></i></span>
                                                                <asp:TextBox ID="txtHomeTown" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Nationality&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-map-marker"></i></span>
                                                                <asp:TextBox ID="TextBox65" runat="server"
                                                                    CssClass="form-control-blue validatetxt" ToolTip="Nationality"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Religion&nbsp;<span class="vd_red">*</span></label>
                                                            <div class=" controls ">

                                                                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Caste</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                <asp:TextBox ID="TextBox66" placeholder="" runat="server" SkinID="TxtBoxDef" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Date of Issue of Aadhar Card</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                <asp:TextBox ID="txtAadharIssueDate" placeholder="Ex. 1990 JAN 01" runat="server"
                                                                    SkinID="TxtBoxDef" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Select Blood Group</label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="drpBloodGroup" runat="server" CssClass="form-control-blue">
                                                                            <asp:ListItem Text="<-- Select Blood Group -->"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                        </div>

                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">

                                                            <div class="col-sm-12  no-padding">
                                                                <label class="control-label">Vision</label>
                                                            </div>
                                                            <div class="col-sm-6 col-xs-6 no-padding">
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-eye"></i></span>
                                                                    <asp:TextBox ID="txtVRight" placeholder="Right" runat="server" CssClass="form-control-blue "></asp:TextBox>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-xs-6 no-padding">
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-eye"></i></span>
                                                                    <asp:TextBox ID="txtVLeft" placeholder="Left" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <div class="col-sm-12  no-padding  ">
                                                                <label class="control-label">Height</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-level-up"></i></span>
                                                                    <asp:TextBox ID="txtHeight" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-6 col-xs-6 no-padding" style="display: none">
                                                                <label class="control-label">&nbsp;</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <asp:DropDownList ID="DropDownList34" runat="server"
                                                                        CssClass="form-control-blue ">
                                                                        <asp:ListItem Selected="True" Text="cm">cm</asp:ListItem>
                                                                        <asp:ListItem Text="inches">inches</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <div class="col-sm-12  no-padding">
                                                                <label class="control-label">Weight</label>
                                                            </div>
                                                            <div class="col-sm-8 col-xs-8 no-padding  ">
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-database"></i></span>
                                                                    <asp:TextBox ID="txtWeight" placeholder="	" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4 col-xs-4 no-padding">

                                                                <div class="">
                                                                    <asp:DropDownList ID="DropDownList35" runat="server" CssClass="form-control-blue ">
                                                                        <asp:ListItem Selected="True" Text="kg">kg</asp:ListItem>
                                                                        <asp:ListItem Text="pound">pound</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Dental Hygiene</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-smile-o"></i></span>
                                                                <asp:TextBox ID="txtDental" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Oral Hygiene</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-smile-o"></i></span>
                                                                <asp:TextBox ID="txtOral" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Identification Mark</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-smile-o"></i></span>
                                                                <asp:TextBox ID="txtIMark" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Specific Ailment</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-smile-o"></i></span>
                                                                <asp:TextBox ID="txtSpeAilment" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Designation</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                <asp:TextBox ID="txtdesfa" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Qualification</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-lightbulb-o"></i></span>
                                                                <asp:TextBox ID="txtqufa" placeholder="" runat="server" CssClass="form-control-blue "></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Annual Income</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-money"></i></span>
                                                                <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtincomefa" runat="server" placeholder=""
                                                                            onkeyup="AddDecimalValue(event,this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtParentTotalIncome',
                                                                    '#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtincomemonthlymother');"
                                                                            CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Nationality&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-flag"></i></span>

                                                                <asp:UpdatePanel ID="UpdatePanel70" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtFatherNationality" placeholder="" runat="server"
                                                                            CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  no-padding">
                                                            <div class="col-sm-12   mgbt-xs-9">
                                                                <label class="control-label">Office Address</label>
                                                                <div class="">
                                                                    <asp:TextBox ID="txtoffaddfa" placeholder="" runat="server" TextMode="MultiLine"
                                                                        Rows="3" CssClass="form-control-blue"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-6  half-width-50 ">
                                                                <div class="col-sm-12  no-padding mgbt-xs-15">
                                                                    <label class="control-label">Email&nbsp;</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-envelope"></i></span>
                                                                        <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtemailfather" runat="server" placeholder=""
                                                                                    CssClass="form-control-blue"></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                    <asp:CheckBox ID="chkFaEmail" runat="server" CssClass="vd_checkbox checkbox-success"
                                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for Email Alert" />
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-6  half-width-50 ">
                                                                <div class="col-sm-12  no-padding mgbt-xs-15">
                                                                    <label class="control-label">Mobile No.&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-phone"></i></span>
                                                                        <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtcontfa" runat="server" placeholder=""
                                                                                    onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtcontactNo',
                                                                        '#ContentPlaceHolder1_ContentPlaceHolderMainBox_DrpRelationship','Father');"
                                                                                    CssClass="form-control-blue validatetxt" MaxLength="10"></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                    <asp:CheckBox ID="chkFaMobile" runat="server" CssClass="vd_checkbox checkbox-success"
                                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for SMS Alert" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4   mgbt-xs-15">
                                                            <label class="control-label">Father's Photo</label>
                                                            <div class="vd_input-wrapper controls img-input-ped ">
                                                                <asp:Label ID="lblFatherImageUrl" runat="server" Visible="False"></asp:Label>
                                                                <asp:Label ID="lblFatherImageName" runat="server" Visible="False"></asp:Label>
                                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control-blue" runat="server"
                                                                    onchange="checksFileSizeandFileTypeinupdatePanel(this, 50000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','imgFather',
                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdfatherPhoto');"
                                                                    type="file" />

                                                            </div>

                                                        </div>
                                                        <div class="col-sm-4   ">

                                                            <div class="vd_input-wrapper controls ">
                                                                <div class="stu-pic-box">
                                                                    <div class="stu-pic-box-main">
                                                                        <asp:Image ID="imgFather" runat="server" class="imgFather"
                                                                            ImageUrl="../img/user-pic/student-pic.png" alt="" />
                                                                        <asp:HiddenField ID="hdfatherPhoto" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="col-md-4 mgbt-xs-15" style="display: none">
                                                            <label class="control-label">Office Phone No.</label>
                                                            <div class="vd_input-wrapper controls ">

                                                                <span class="menu-icon"><i class="fa  fa-phone"></i></span>
                                                                <asp:UpdatePanel ID="UpdatePanel51" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtFatherOfficePhoneNo" placeholder="" runat="server"
                                                                            CssClass="form-control-blue "></asp:TextBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 mgbt-xs-15" style="display: none">
                                                            <label class="control-label">Office Email</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-envelope"></i></span>

                                                                <asp:UpdatePanel ID="UpdatePanel53" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtFatherOfficeEmail" placeholder=" " runat="server"
                                                                            CssClass="form-control-blue" onBlur="ValidateEmail(this,
                                                                        '#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtFatherOfficeEmail');"></asp:TextBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Designation</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-star"></i></span>

                                                                <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtdesmoth" runat="server" placeholder="" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Qualification</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-lightbulb-o"></i></span>
                                                                <asp:TextBox ID="txtqualimother" runat="server" CssClass="form-control-blue "></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Annual Income</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-money"></i></span>
                                                                <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtincomemonthlymother" placeholder="" runat="server" CssClass="form-control-blue"
                                                                            onkeyup="AddDecimalValue(event,this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtParentTotalIncome',
                                                                    '#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtincomefa');"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Nationality&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-flag"></i></span>
                                                                <asp:UpdatePanel ID="UpdatePanel84" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtMotherNationality" placeholder="" runat="server"
                                                                            CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Pin Code</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-road"></i></span>
                                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtPreZip" placeholder="Pin Code" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                            
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>


                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Phone No.</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-phone"></i></span>
                                                                <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtPrePhoneNo" placeholder="Phone No." runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>

                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Mobile No.</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-mobile"></i></span>
                                                                <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtPreMobileNo" placeholder="Mobile No." runat="server"
                                                                            CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                            
                                                                        </div>


                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                            <label class="control-label">Enquiry No. </label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                <asp:TextBox ID="txtEnquiryNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Card No. </label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                <asp:TextBox ID="txtCardNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Board/ University Roll No.</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                <asp:TextBox ID="txtUniversityBoardRollNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Institute Roll No. </label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                <asp:TextBox ID="txtSchoolcollegeRollno" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">File No. </label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-hand-o-right"></i></span>
                                                                <asp:TextBox ID="txtfileno" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                            <label class="control-label">Reference </label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-hand-o-right"></i></span>
                                                                <asp:TextBox ID="txtReferences" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Payment Frequency&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:DropDownList ID="drpFeeDepositMOD" runat="server" CssClass="form-control-blue">
                                                                    <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                    <%-- <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                        <asp:ListItem Value="S">Semester</asp:ListItem>--%>
                                                                    <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>


                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                            <label class="control-label">Select SMS Acknowledgment To &nbsp;<span class="vd_red">*</span> </label>
                                                            <div class="">
                                                                <asp:DropDownList ID="drpSMSAcknowledgmentTo" runat="server"
                                                                    CssClass="form-control-blue">
                                                                    <asp:ListItem Text="<-- Select SMS Acknowledgment To -->"></asp:ListItem>
                                                                    <asp:ListItem>Gaurdian 1</asp:ListItem>
                                                                    <asp:ListItem>Gaurdian 2</asp:ListItem>
                                                                    <asp:ListItem>Both</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                            <label class="control-label">Select Email Acknowledgment To &nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:DropDownList ID="drpEmailAcknowledgmentTo" runat="server"
                                                                    CssClass="form-control-blue">
                                                                    <asp:ListItem Text="<-- Select Email Acknowledgment To -->"></asp:ListItem>
                                                                    <asp:ListItem>Gaurdian 1</asp:ListItem>
                                                                    <asp:ListItem>Gaurdian 2</asp:ListItem>
                                                                    <asp:ListItem>Both</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>


                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Select House Name &nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DropDownList4" runat="server"
                                                                            CssClass="form-control-blue validatedrp">
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg">
                                                                        
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Remark </label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                <asp:TextBox ID="txtrema" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Admission done at </label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                <asp:TextBox ID="txtAddDoneat" placeholder="INR (Rupees)" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>




                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Select Stream&nbsp;<span class="vd_red"></span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel57" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DropStream" runat="server" CausesValidation="false"
                                                                            CssClass="form-control-blue">
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg"></div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>





                                                        <div class=" col-sm-4  half-width-50 mgbt-xs-15 ">
                                                            <label class=" control-label">Type of Education&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="mgtp-6">
                                                                <asp:RadioButtonList ID="RadioButtonList3"
                                                                    runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                                    <asp:ListItem Value="Yes" Selected="True"> Regular </asp:ListItem>
                                                                    <asp:ListItem Value="No"> Private </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                                            <label class=" control-label">Scholarship&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="mgtp-6">
                                                                <asp:UpdatePanel ID="UpdatePanel60" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:RadioButtonList ID="rbScholarship" onclick="onclickeds(this);" runat="server"
                                                                            CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                                            <asp:ListItem>Yes</asp:ListItem>
                                                                            <asp:ListItem Selected="True">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        <script>
                                                                            function onclickeds(rbScholarship) {
                                                                                var litab = document.getElementById("otherdetails");
                                                                                var litabRelatedDiv = document.getElementById("poststab3");
                                                                                var inputTag = rbScholarship.getElementsByTagName("input");

                                                                                if (inputTag[0].checked) {
                                                                                    litab.style.display = '';
                                                                                    litabRelatedDiv.style.display = '';
                                                                                }
                                                                                else {
                                                                                    litab.style.display = 'none';
                                                                                    litabRelatedDiv.style.display = 'none';
                                                                                }
                                                                            }
                                                                        </script>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                                            <label class=" control-label">Hostel Required&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="mgtp-6">
                                                                <asp:RadioButtonList ID="rbHostel" runat="server" CssClass="vd_radio radio-success"
                                                                    RepeatDirection="Horizontal" RepeatLayout="flow"
                                                                    onchange="visibleFalseTableColumn('ContentPlaceHolder1_ContentPlaceHolderMainBox_drpHostalMODB',this)">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No" Selected="True">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="drpHostalMODB" runat="server" style="display: none">
                                                            <label class=" control-label">Payment Frequency&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:DropDownList ID="drpHostalMOD" runat="server" CssClass="form-control-blue">
                                                                    <%--  <asp:ListItem><-- Mode of Fee Deposit --></asp:ListItem>--%>
                                                                    <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                    <%--<asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                        <asp:ListItem Value="S">Semester</asp:ListItem>--%>
                                                                    <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                                            <label class=" control-label">Library Facility&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="mgtp-6">
                                                                <asp:RadioButtonList ID="rbLibrary" runat="server" CssClass="vd_radio radio-success"
                                                                    RepeatDirection="Horizontal" RepeatLayout="flow"
                                                                    onchange="visibleFalseTableColumn('ContentPlaceHolder1_ContentPlaceHolderMainBox_drpLibraryMODB',this)">
                                                                    <asp:ListItem>Yes</asp:ListItem>
                                                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="drpLibraryMODB" runat="server" style="display: none">
                                                            <label class=" control-label">Payment Frequency&nbsp;<span class="vd_red">*</span></label>
                                                            <div class=" ">
                                                                <asp:DropDownList ID="drpLibraryMOD" runat="server" CssClass="form-control-blue ">
                                                                    <%-- <asp:ListItem><-- Mode of Fee Deposit --></asp:ListItem>--%>
                                                                    <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                    <%--<asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                        <asp:ListItem Value="S">Semester</asp:ListItem>--%>
                                                                    <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                                            <label class=" control-label">Transport Required &nbsp;<span class="vd_red">*</span></label>
                                                            <div class="mgtp-6">
                                                                <asp:RadioButtonList ID="rbTransport" runat="server" CssClass="vd_radio radio-success"
                                                                    RepeatDirection="Horizontal" RepeatLayout="flow"
                                                                    onchange="visibleFalseTableColumn('ContentPlaceHolder1_ContentPlaceHolderMainBox_drpTransportMODB',this)">
                                                                    <asp:ListItem>Yes</asp:ListItem>
                                                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                                                </asp:RadioButtonList>

                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="drpTransportMODB" runat="server" style="display: none">
                                                            <label class=" control-label">Payment Frequency&nbsp;<span class="vd_red">*</span></label>
                                                            <div class=" ">
                                                                <asp:DropDownList ID="drpTransportMOD" runat="server" CssClass="form-control-blue">
                                                                    <%-- <asp:ListItem><-- Mode of Fee Deposit --></asp:ListItem>--%>
                                                                    <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                    <%-- <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                        <asp:ListItem Value="S">Semester</asp:ListItem>--%>
                                                                    <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">


                                                            <asp:Label ID="lblDocument" class="control-label"
                                                                runat="server" Text='<%# Bind("DocumentType") %>'></asp:Label>
                                                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>

                                                            <div class="controls img-input-ped">
                                                                <asp:FileUpload ID="FileUpload4"
                                                                    CssClass="form-control-blue " runat="server"
                                                                    onChange="checksFileSizeandFileTypeinupdatePanel_witin_Repeater(this,'250600',
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

                                                        <div class="col-sm-4  half-width-50 btn-a-devices-1-p4-p2 mgbt-xs-15 ">
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

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Name of Entrance</label>
                                                            <div class="">
                                                                <asp:DropDownList onchange="displayTextbox();" ID="drpExamCrackedof" runat="server"
                                                                    CssClass="form-control-blue ">
                                                                </asp:DropDownList>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Roll No.</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                                <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Rank</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                                <asp:TextBox ID="txtRank" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Category Rank</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                                <asp:TextBox ID="txtCategoryRank" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Any Other</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                                <asp:TextBox ID="txtAnyOtherCategoryRank" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                <div class="text-box-msg"></div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                    </div>
                                    <!-- Panel Widget -->
                                </div>
                                <div class="row mgbt-xs-20" style="display: none !important">
                                    <div class="col-sm-6  full-width-100">
                                        <div class="panel widget">
                                            <div class="panel-heading vd_bg-dark-blue">
                                                <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-heart"></i></span>Health Details </h3>
                                                <div class="vd_panel-menu ">
                                                    <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon">
                                                        <i class="icon-minus3"></i>
                                                    </div>

                                                </div>
                                                <!-- vd_panel-menu -->

                                            </div>
                                            <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                <div class="row">
                                                    <asp:UpdatePanel ID="UpdatePanel66" runat="server">
                                                        <ContentTemplate>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </div>
                                            </div>
                                        </div>
                                        <!-- Panel Widget -->
                                    </div>

                                    <div class="col-sm-6  full-width-100">
                                        <div class="panel widget">
                                            <div class="panel-heading vd_bg-dark-blue">
                                                <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-wheelchair"></i></span>Physically Disabled Details </h3>
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
                                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                        <ContentTemplate>

                                                            <div class="col-sm-12   mgbt-xs-10">
                                                                <div class="vd_input-wrapper ">
                                                                    <label class="col-sm-4 no-padding control-label">Physically Disabled?</label>
                                                                    <div class="col-sm-8 no-padding controls">
                                                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:RadioButtonList ID="RadioButtonList8" runat="server"
                                                                                    CssClass="vd_radio radio-success" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="RadioButtonList8_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                                                    RepeatLayout="flow">
                                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="No" Selected="True">No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12  no-padding" id="Panel2" runat="server">

                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Name of Disability</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-tags"></i></span>
                                                                        <asp:TextBox ID="txtPhyName" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Certificate No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-mobile"></i></span>
                                                                        <asp:TextBox ID="txtCertificateNo" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-12   mgbt-xs-15">
                                                                    <label class="control-label">Issued By</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-thumbs-up"></i></span>
                                                                        <asp:TextBox ID="txtIssuedBy" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12   ">
                                                                    <label class="control-label">Details</label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtPhyDetail" runat="server"
                                                                            TextMode="MultiLine" Rows="3" CssClass="form-control-blue"></asp:TextBox>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>
                                    </div>

                                </div>



                                <div class="tab-pane" id="posts-tab" style="display: none !important">

                                    <div class="row mgbt-xs-20">

                                        <div class="col-sm-6  full-width-100">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-male"></i></span>Father's Details </h3>
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

                                                        <asp:UpdatePanel ID="UpdatePanel67" runat="server">
                                                            <ContentTemplate>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>


                                                    </div>


                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>

                                        <div class="col-sm-4  full-width-100">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class="fa  fa-female"></i></span>Mother's Details </h3>
                                                    <div class="vd_panel-menu ">
                                                        <div data-action="minimize" title="Minimize" data-toggle="tooltip"
                                                            data-placement="bottom" class=" menu entypo-icon">
                                                            <i class="icon-minus3"></i>
                                                        </div>
                                                    </div>
                                                    <!-- vd_panel-menu -->

                                                </div>
                                                <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                    <div class="row">

                                                        <asp:UpdatePanel ID="UpdatePanel68" runat="server">
                                                            <ContentTemplate>



                                                                <div class="col-sm-8  no-padding">
                                                                    <div class="col-sm-12   mgbt-xs-9">
                                                                        <label class="control-label">Office Address</label>
                                                                        <div class="">
                                                                            <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtofficeaddmother" runat="server" AutoPostBack="True"
                                                                                        TextMode="MultiLine" Rows="3" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg">
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-6  half-width-50 ">
                                                                        <div class="col-sm-12  no-padding mgbt-xs-15">
                                                                            <label class="control-label">Email&nbsp;</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa  fa-envelope"></i></span>

                                                                                <asp:TextBox ID="txtmotheremail" placeholder="" runat="server" CssClass="form-control-blue "
                                                                                    onBlur="ValidateEmail(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtGuardiantwoEmail');">
                                                                                </asp:TextBox>
                                                                                <div class="text-box-msg">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                            <asp:CheckBox ID="chkMoEmail" runat="server" Text="Tick for Email Alert"
                                                                                CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="Flow" />
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6  half-width-50 ">
                                                                        <div class="col-sm-12  no-padding mgbt-xs-15">
                                                                            <label class="control-label">
                                                                                Mobile No.&nbsp;<span class="vd_red">*</span>
                                                                            </label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa  fa-phone"></i></span>

                                                                                <asp:TextBox ID="txtmothercontact" placeholder="" runat="server"
                                                                                    CssClass="form-control-blue validatetxt" MaxLength="10"
                                                                                    onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtGuardiantwoContact',
                                                                '#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpGuardiantwoRelationship','Mother');"></asp:TextBox>
                                                                                <div class="text-box-msg">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                            <asp:CheckBox ID="chkMoMobile" runat="server" Text="Tick for SMS Alert"
                                                                                CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="Flow" />
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4   mgbt-xs-15">
                                                                    <label class="control-label">Mother's Photo</label>
                                                                    <div class="vd_input-wrapper controls img-input-ped ">
                                                                        <asp:FileUpload ID="FileUpload2" runat="server"
                                                                            CssClass="form-control-blue "
                                                                            onchange="checksFileSizeandFileTypeinupdatePanel(this, 50000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','imgMother',
                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdmotherPhoto');"
                                                                            type="file" />
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-4  ">
                                                                    <div class="stu-pic-box">
                                                                        <div class="stu-pic-box-main">
                                                                            <asp:Label ID="lblMotherImageUrl" runat="server" Visible="False"></asp:Label>
                                                                            <asp:Label ID="lblMotherImageName" runat="server" Visible="False"></asp:Label>
                                                                            <asp:Image ID="imgMother" ImageUrl="../img/user-pic/student-f-pic.png" runat="server" Class="imgMother" />
                                                                            <asp:HiddenField ID="hdmotherPhoto" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                </div>




                                                                <div class="col-md-8 no-padding " style="display: none">

                                                                    <div class="col-md-6" style="display: none">
                                                                        <label class="control-label">Office Mobile No.</label>
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa   fa-mobile"></i></span>
                                                                            <asp:UpdatePanel ID="UpdatePanel55" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtMotherOfficeMobileNo" placeholder="" runat="server"
                                                                                        CssClass="form-control-blue"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                                                                        CssClass="imp" runat="server" ControlToValidate="txtMotherOfficeMobileNo"
                                                                                        ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[0-9]{10,10}$"
                                                                                        ValidationGroup="A"></asp:RegularExpressionValidator>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-6 mgbt-xs-15" style="display: none">
                                                                        <label class="control-label">Office Email</label>
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa  fa-envelope"></i></span>

                                                                            <asp:UpdatePanel ID="UpdatePanel56" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtMotherOfficeEmail" runat="server" placeholder=" "
                                                                                        CssClass="form-control-blue" onBlur="ValidateEmail(this,
                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_txtFatherOfficeEmail');"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>







                                                                </div>


                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>


                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>
                                    </div>
                                    <div class="row mgbt-xs-20">

                                        <div class="col-sm-6  full-width-100">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class="glyphicon glyphicon-map-marker"></i></span>Present Address </h3>
                                                    <div class="vd_panel-menu ">
                                                        <div data-action="minimize" title="Minimize" data-toggle="tooltip"
                                                            data-placement="bottom" class=" menu entypo-icon">
                                                            <i class="icon-minus3"></i>
                                                        </div>
                                                    </div>
                                                    <!-- vd_panel-menu -->
                                                </div>
                                                <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                    <div class="row">
                                                        <asp:UpdatePanel ID="UpdatePanel63" runat="server">
                                                            <ContentTemplate>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>

                                        <div class="col-sm-6  full-width-100">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class="glyphicon glyphicon-map-marker"></i></span>Permanent Address </h3>
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
                                                        <asp:UpdatePanel ID="UpdatePanel65" runat="server">
                                                            <ContentTemplate>

                                                                <div class="col-sm-12   mgbt-xs-9 tick">
                                                                    <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>
                                                                    <asp:CheckBoxList ID="CheckBox1" runat="server" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="CheckBox1_CheckedChanged"
                                                                        TextAlign="Right" CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                        <asp:ListItem>Same as Present Address</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtPerAdd" placeholder="Please don't write State and City name here" runat="server" TextMode="MultiLine" Rows="3"
                                                                                    CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                                <div class="text-box-msg">
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Country&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpPerCountry" runat="server"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="DrpPerCountry_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue ">
                                                                                    <asp:ListItem Text="<-- Select Country -->"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <div class="text-box-msg">
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>


                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpPerState" runat="server" AutoPostBack="True"
                                                                                    onchange="ValidatorUpdateDisplay(ContentPlaceHolder1_ContentPlaceHolderMainBox_RequiredFieldValidator12)"
                                                                                    OnSelectedIndexChanged="DrpPerState_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                                <div class="text-box-msg">
                                                                                    
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class=" ">
                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DrpPerCity" runat="server" AutoPostBack="True"
                                                                                    onchange="ValidatorUpdateDisplay(ContentPlaceHolder1_ContentPlaceHolderMainBox_RequiredFieldValidator13)"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                                <div class="text-box-msg">
                                                                                   
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Pin Code</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-road"></i></span>
                                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtPerZip" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg">
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                                                        ControlToValidate="txtPerZip" ErrorMessage="*"
                                                                                        ValidationExpression="^[0-9]{6,6}$" ValidationGroup="A"
                                                                                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                                </div>

                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Phone No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-phone"></i></span>
                                                                        <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtPerPhoneNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg">
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Mobile No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-mobile"></i></span>
                                                                        <asp:UpdatePanel ID="UpdatePanel50" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtPerMobileNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg">
                                                                                   
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>
                                    </div>
                                    <div class="row mgbt-xs-20">

                                        <div class="col-sm-6  full-width-100">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class="fa  fa-money"></i></span>
                                                        Parent's Total Income and &nbsp; &nbsp;<span class="menu-icon"><i class="fa fa-picture-o"></i></span> Group Photo </h3>
                                                    <div class="vd_panel-menu ">
                                                        <div data-action="minimize" title="Minimize" data-toggle="tooltip"
                                                            data-placement="bottom" class=" menu entypo-icon">
                                                            <i class="icon-minus3"></i>
                                                        </div>

                                                    </div>
                                                    <!-- vd_panel-menu -->

                                                </div>
                                                <div class="panel-body padding-tlbr-15 form-main-box-border">


                                                    <div class="row">
                                                        <asp:UpdatePanel ID="UpdatePanel69" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-sm-12   mgbt-xs-15">
                                                                    <label class="col-sm-4  no-padding control-label">Parent's Total Income</label>
                                                                    <div class="col-sm-8  vd_input-wrapper no-padding ">
                                                                        <span class="menu-icon"><i class="fa  fa-money"></i></span>
                                                                        <asp:UpdatePanel ID="UpdatePanel64" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtParentTotalIncome" placeholder="Annual Income" runat="server"
                                                                                    CssClass="form-control-blue " onkeyup="CheckDecimalValue(event,this);"></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12   mgbt-xs-15">
                                                                    <label class="col-sm-4  no-padding  control-label">Upload Group Photo</label>
                                                                    <div class="col-sm-8   no-padding">

                                                                        <asp:FileUpload ID="FileUpload3" runat="server"
                                                                            CssClass="form-control-blue "
                                                                            onchange="checksFileSizeandFileTypeinupdatePanel(this, 100000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','imgGroupPhoto',
                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdbase64groupPhoto');"
                                                                            type="file" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-12   mgbt-xs-15">
                                                                    <div class="col-sm-4  no-padding">
                                                                    </div>
                                                                    <div class="col-sm-8  no-padding">
                                                                        <div class="col-sm-12  set-box-in-center no-padding">
                                                                            <div class="group-pic-box">
                                                                                <div class="group-pic-box-main">
                                                                                    <asp:Label ID="lblGroupImageName" runat="server" Visible="False"></asp:Label>
                                                                                    <asp:Label ID="lblGroupImageUrl" runat="server" Visible="False"></asp:Label>
                                                                                    <asp:Image ID="imgGroupPhoto" runat="server" Class="imgGroupPhoto" style="Height: 260px;" ImageUrl="../img/user-pic/group-photo.jpg" />
                                                                                    <asp:HiddenField ID="hdbase64groupPhoto" runat="server" />

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>
                                        <div class="col-sm-6  full-width-100">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Local Guardian Details </h3>
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
                                                        <asp:UpdatePanel ID="UpdatePanel73" runat="server">
                                                            <ContentTemplate>


                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Select Relationship&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <asp:DropDownList ID="DrpRelationship" runat="server" CssClass="form-control-blue validatedrp"
                                                                            AutoPostBack="True"
                                                                            OnSelectedIndexChanged="DrpRelationship_SelectedIndexChanged">
                                                                            <asp:ListItem Text="<-- Select Relationship -->"></asp:ListItem>
                                                                            <asp:ListItem>Father</asp:ListItem>
                                                                            <asp:ListItem>Mother</asp:ListItem>
                                                                            <asp:ListItem>Grand Father</asp:ListItem>
                                                                            <asp:ListItem>Grand Mother</asp:ListItem>
                                                                            <asp:ListItem>Brother</asp:ListItem>
                                                                            <asp:ListItem>Sister</asp:ListItem>
                                                                            <asp:ListItem>Uncle</asp:ListItem>
                                                                            <asp:ListItem>Aunty</asp:ListItem>
                                                                            <asp:ListItem>Others</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <div class="text-box-msg"></div>

                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Guardian Name&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtguardianname" placeholder="" runat="server"
                                                                                    CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-4 col-sm-6 mgbt-xs-15" style="display: none">
                                                                    <label class="control-label">Annual Income</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa  fa-money"></i></span>

                                                                        <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtfailyincome" runat="server" onkeyup="CheckDecimalValue(event,this);"
                                                                                    CssClass="textbox form-control-blue">0</asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>



                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Country</label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="drpG1Country" runat="server"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="drpG1Country_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue ">
                                                                                    <asp:ListItem Text="<-- Select Country -->"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <div class="text-box-msg"></div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>


                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">State</label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel78" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="drpG1State" runat="server"
                                                                                    AutoPostBack="True" CssClass="form-control-blue" OnSelectedIndexChanged="drpG1State_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <div class="text-box-msg"></div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">City</label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel79" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="drpG1City" runat="server" AutoPostBack="True" CssClass="form-control-blue ">
                                                                                </asp:DropDownList>
                                                                                <div class="text-box-msg"></div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>

                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Pin Code</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-road"></i></span>

                                                                        <asp:UpdatePanel ID="UpdatePanel82" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtG1Pin" placeholder="" runat="server"
                                                                                    CssClass="form-control-blue"
                                                                                    onchange="ValidatorUpdateDisplay
                                                                    (ContentPlaceHolder1_ContentPlaceHolderMainBox_RegularExpressionValidator8)"></asp:TextBox>
                                                                                <div class="text-box-msg">
                                                                                    
                                                                                </div>

                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  half-width-50 ">
                                                                    <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                        <label class="control-label">Email&nbsp;</label>
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa fa-envelope"></i></span>

                                                                            <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtemailfamily" placeholder="" runat="server"
                                                                                        CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12  no-padding mgbt-xs-5">
                                                                        <asp:CheckBox ID="chkGuaEmail" runat="server" Text="Tick for Email Alert"
                                                                            CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="Flow" />
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  half-width-50 ">
                                                                    <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                        <label class="control-label">Mobile No.&nbsp;<span class="vd_red">*</span></label>
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa  fa-phone"></i></span>

                                                                            <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtcontactNo" runat="server"
                                                                                        CssClass=" form-control-blue validatetxt"></asp:TextBox>
                                                                                    <div class="text-box-msg"></div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12  no-padding mgbt-xs-5">
                                                                        <asp:CheckBox ID="chkGuaMobile" runat="server" Text="Tick for SMS Alert"
                                                                            CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="Flow" />
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <%--<div class="col-sm-12  mgbt-xs-10">
                                                    <h4>Local Guardian 1 Address</h4>
                                                </div>--%>
                                                                <div class="col-md-6 mgbt-xs-15" style="display: none">
                                                                    <div class=" controls">

                                                                        <asp:UpdatePanel ID="UpdatePanel71" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:CheckBoxList ID="CheckBox2" runat="server"
                                                                                    AutoPostBack="True" OnSelectedIndexChanged="CheckBox2_CheckedChanged"
                                                                                    class="vd_checkbox checkbox-success" TextAlign="Right"
                                                                                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                    <asp:ListItem>Same as Student Present Address</asp:ListItem>
                                                                                </asp:CheckBoxList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-12  ">
                                                                    <label class="control-label">Local Guardian Address</label>
                                                                    <div class="">
                                                                        <asp:UpdatePanel ID="UpdatePanel77" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtG1Address" placeholder="" runat="server"
                                                                                    TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>



                                                                <div class="col-md-4 mgbt-xs-15" style="display: none">
                                                                    <label class="control-label">Phone No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-phone"></i></span>

                                                                        <asp:UpdatePanel ID="UpdatePanel80" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtG1PhoneNo" runat="server"
                                                                                    CssClass="form-control-blue" MaxLength="10" placeholder=""></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-4 " style="display: none">
                                                                    <label class="control-label">Mobile No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-mobile"></i></span>
                                                                        <asp:UpdatePanel ID="UpdatePanel81" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtG1MobileNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                                                    CssClass="imp" runat="server" ControlToValidate="txtG1MobileNo"
                                                                                    ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[0-9]{10,10}$"
                                                                                    ValidationGroup="A"></asp:RegularExpressionValidator>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>


                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>

                                    </div>

                                    <div class="row mgbt-xs-20" style="display: none">



                                        <div class="col-md-6 full-width-100" style="display: none">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Local Guardian 2 Details </h3>
                                                    <div class="vd_panel-menu ">
                                                        <div data-action="minimize" title="Minimize" data-toggle="tooltip"
                                                            data-placement="bottom" class=" menu entypo-icon">
                                                            <i class="icon-minus3"></i>
                                                        </div>

                                                    </div>
                                                    <!-- vd_panel-menu -->

                                                </div>
                                                <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                    <div class="row">
                                                        <asp:UpdatePanel ID="UpdatePanel74" runat="server">
                                                            <ContentTemplate>


                                                                <div class="col-md-4 col-sm-6 mgbt-xs-15">
                                                                    <label class="control-label">Select Relationship&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="vd_input-wrapper controls ">

                                                                        <asp:DropDownList ID="drpGuardiantwoRelationship" runat="server"
                                                                            CssClass="form-control-blue"
                                                                            AutoPostBack="True" OnSelectedIndexChanged="drpGuardiantwoRelationship_SelectedIndexChanged">
                                                                            <asp:ListItem Text="<-- Select Relationship -->"></asp:ListItem>
                                                                            <asp:ListItem>Father</asp:ListItem>
                                                                            <asp:ListItem Selected="True">Mother</asp:ListItem>
                                                                            <asp:ListItem>Grand Father</asp:ListItem>
                                                                            <asp:ListItem>Grand Mother</asp:ListItem>
                                                                            <asp:ListItem>Brother</asp:ListItem>
                                                                            <asp:ListItem>Sister</asp:ListItem>
                                                                            <asp:ListItem>Uncle</asp:ListItem>
                                                                            <asp:ListItem>Aunty</asp:ListItem>
                                                                            <asp:ListItem>Others</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-4 col-sm-6 mgbt-xs-15">
                                                                    <label class="control-label">Name&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtGuardiantwoName" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-4 col-sm-6 mgbt-xs-15">
                                                                    <label class="control-label">Annual Income</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-envelope"></i></span>

                                                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtGuardiantwoIncomeMonthly" runat="server"
                                                                                    CssClass="form-control-blue" onkeyup="CheckDecimalValue(event,this);">0</asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4 col-sm-6 mgbt-xs-15">
                                                                    <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-envelope"></i></span>

                                                                        <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtGuardiantwoContact" runat="server" CssClass="form-control-blue" MaxLength="10"></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>


                                                                <div class="col-md-8 mgbt-xs-15">
                                                                    <label class="control-label">Email</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-envelope"></i></span>

                                                                        <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtGuardiantwoEmail" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6 mgbt-xs-15">
                                                                    <h4>Local Guardian 2 Address</h4>
                                                                </div>
                                                                <div class="col-md-6 mgbt-xs-15">
                                                                    <div class=" controls">

                                                                        <asp:UpdatePanel ID="UpdatePanel72" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:CheckBoxList ID="CheckBox3" runat="server" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="CheckBox3_CheckedChanged" class="vd_checkbox checkbox-success"
                                                                                    TextAlign="Right" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                    <asp:ListItem>Same as Student's Present Address</asp:ListItem>
                                                                                </asp:CheckBoxList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12 vd_input-margin20">
                                                                    <div class="vd_input-wrapper ">

                                                                        <asp:UpdatePanel ID="G2UpdatePanel77" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtG2Address" placeholder="Address" runat="server" TextMode="MultiLine"
                                                                                    Rows="3" CssClass="form-control-blue"></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-4 mgbt-xs-15">
                                                                    <div class="vd_input-wrapper ">
                                                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="drpG2Country" runat="server" AutoPostBack="true"
                                                                                    OnSelectedIndexChanged="drpG2Country_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue ">
                                                                                    <asp:ListItem Text="<-- Select Country -->"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4 mgbt-xs-15">
                                                                    <div class="vd_input-wrapper ">

                                                                        <asp:UpdatePanel ID="G2UpdatePanel78" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="drpG2State" runat="server" AutoPostBack="True"
                                                                                    CssClass="form-control-blue" OnSelectedIndexChanged="drpG2State_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4 mgbt-xs-15">
                                                                    <div class="vd_input-wrapper ">

                                                                        <asp:UpdatePanel ID="G2UpdatePanel79" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="drpG2City" runat="server" AutoPostBack="True" CssClass="form-control-blue ">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4 mgbt-xs-15">
                                                                    <label class="control-label">Phone No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-phone"></i></span>

                                                                        <asp:UpdatePanel ID="G2UpdatePanel80" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtG2PhoneNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4 ">
                                                                    <label class="control-label">Mobile No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-mobile"></i></span>

                                                                        <asp:UpdatePanel ID="UpdatePanelG281" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtG2MobileNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="G2RegularExpressionValidator7"
                                                                                    CssClass="imp" runat="server" ControlToValidate="txtG2MobileNo" ErrorMessage="*"
                                                                                    SetFocusOnError="True"
                                                                                    ValidationExpression="^[0-9]{10,10}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4 ">
                                                                    <label class="control-label">Pin Code</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-road"></i></span>

                                                                        <asp:UpdatePanel ID="G2UpdatePanel82" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtG2Pin" placeholder="" runat="server" CssClass="form-control-blue"
                                                                                    onchange="ValidatorUpdateDisplay(ContentPlaceHolder1_ContentPlaceHolderMainBox_RegularExpressionValidator8)">

                                                                                </asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="G2RegularExpressionValidator8"
                                                                                    CssClass="imp" runat="server" ControlToValidate="txtG2Pin" ErrorMessage="*"
                                                                                    SetFocusOnError="True" ValidationExpression="^[0-9]{6,6}$"
                                                                                    ValidationGroup="A" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>
                                    </div>
                                </div>



                                <div class="tab-pane" id="posts-tab2" style="display: none !important">
                                    <div class="row mgbt-xs-20">

                                        <div class="col-sm-6  full-width-100">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>&nbsp; Admission Details </h3>
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
                                                        <asp:UpdatePanel ID="UpdatePanel76" runat="server">
                                                            <ContentTemplate>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-md-4" id="divopt" runat="server" visible="false">
                                                                    <div class="vd_input-wrapper ">
                                                                        <label class="col-sm-3 no-padding control-label">Opt. Subject</label>
                                                                        <div class="col-sm-9 mgbt-xs-15 no-padding controls">

                                                                            <asp:CheckBoxList ID="rbOptionalSubject" runat="server" RepeatDirection="Horizontal"
                                                                                RepeatLayout="Flow" CssClass="vd_checkbox checkbox-success">
                                                                            </asp:CheckBoxList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                        <asp:UpdatePanel ID="UpdatePanel83" runat="server">
                                                            <ContentTemplate>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>

                                        <div class="col-sm-6  full-width-100">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Student's Official Details </h3>
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
                                                        <asp:UpdatePanel ID="UpdatePanel85" runat="server">
                                                            <ContentTemplate>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>

                                        <div class="col-sm-6  full-width-100">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Student's Other Details </h3>
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
                                                        <asp:UpdatePanel ID="UpdatePanel86" runat="server">
                                                            <ContentTemplate>


                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Date of First Admission</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtDFA" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Course of First Admission</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtCFA" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Class of First Admission</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtCOFA" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Branch of First Admission</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtSFA" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane" id="list-tab2" style="display: none !important">

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
                                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Repeater ID="Repeater1" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="col-sm-12  no-padding ">
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

                                <div class="tab-pane" id="list-tab" style="display: none !important">
                                    <div class="row mgbt-xs-20">

                                        <div class="col-sm-6  full-width-100">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class=" icon-graduation"></i></span>Entrance Exam Details
                                        <asp:Label ID="lblsrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                    </h3>
                                                    <div class="vd_panel-menu ">
                                                        <div data-action="minimize" title="Minimize" data-toggle="tooltip"
                                                            data-placement="bottom" class=" menu entypo-icon">
                                                            <i class="icon-minus3"></i>
                                                        </div>

                                                    </div>
                                                    <!-- vd_panel-menu -->
                                                </div>
                                                <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                    <div id="rpt1" class="row">
                                                        <asp:UpdatePanel ID="UpdatePanel75" runat="server">
                                                            <ContentTemplate>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="rptPreviousEducation" runat="server">
                                                    <ItemTemplate>
                                                        <div class="col-sm-6  full-width-100">
                                                            <div class="panel widget">
                                                                <div class="panel-heading vd_bg-dark-blue">
                                                                    <h3 class="panel-title"><span class="menu-icon"><i class=" icon-graduation"></i></span>Previous Education Details
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
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
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

                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Board/ University</label>
                                                                            <div class="">
                                                                                <asp:DropDownList ID="drpBoard" runat="server"
                                                                                    CssClass="form-control-blue " Enabled="false">
                                                                                    <asp:ListItem Text="<-- Select Board/ University -->"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Select Result</label>
                                                                            <div class="">
                                                                                <asp:DropDownList ID="drpResult" runat="server"
                                                                                    CssClass="form-control-blue " Enabled="false">
                                                                                    <asp:ListItem Value="P">Passed</asp:ListItem>
                                                                                    <asp:ListItem Value="F">Failed</asp:ListItem>
                                                                                    <asp:ListItem Value="RA">Result Awaited</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Institute Name</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-institution"></i></span>
                                                                                <asp:TextBox ID="txtInstitute" Enabled="false" Text='<%# Bind("Institute") %>'
                                                                                    runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Passing Year</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-trophy"></i></span>
                                                                                <asp:TextBox ID="txtYop" Text='<%# Bind("Yop") %>' Enabled="false" runat="server"
                                                                                    CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Select Medium</label>
                                                                            <div class="">
                                                                                <asp:DropDownList ID="drpMedium" runat="server"
                                                                                    CssClass="form-control-blue " Enabled="false">
                                                                                    <asp:ListItem Value="English" Selected="True">English</asp:ListItem>
                                                                                    <asp:ListItem Value="Hindi">Hindi</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Subject</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-book"></i></span>
                                                                                <asp:TextBox ID="txtSubject" Enabled="false" Text='<%# Bind("Subject") %>'
                                                                                    runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Roll No.</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-bullhorn"></i></span>
                                                                                <asp:TextBox ID="txtRollNo" Enabled="false" Text='<%# Bind("RollNo") %>'
                                                                                    runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Certificate No.</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="glyphicon glyphicon-certificate"></i></span>
                                                                                <asp:TextBox ID="txtCertificateNo" Text='<%# Bind("CertificateNo") %>' Enabled="false"
                                                                                    runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Marks Sheet No.</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-shield"></i></span>
                                                                                <asp:TextBox ID="txtMarksSheetNo" Text='<%# Bind("MarksSheetNo") %>' Enabled="false"
                                                                                    runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Max Marks</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-thumbs-up"></i></span>
                                                                                <asp:TextBox ID="txtMM" Text='<%# Bind("MaxMarks") %>'
                                                                                    onkeyup="CheckIntegerValueonKeyUp(event, this);" Enabled="false"
                                                                                    runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Obtained Marks</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa  fa-thumbs-o-up"></i></span>
                                                                                <asp:TextBox ID="txtObtained" Text='<%# Bind("Obtained") %>' Enabled="false"
                                                                                    onkeyup="SetPercentage(event,this,
                                                                    '#ContentPlaceHolder1_ContentPlaceHolderMainBox_rptPreviousEducation_txtPer',
                                                                    '#ContentPlaceHolder1_ContentPlaceHolderMainBox_rptPreviousEducation_txtMM');"
                                                                                    runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4  half-width-50 half-width-100-tc mgbt-xs-15">
                                                                            <label class="control-label">Percent / Grade</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                                <asp:TextBox ID="txtPer" Text='<%# Bind("Per") %>'
                                                                                    Enabled="false" runat="server"
                                                                                    CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                                            <label class="control-label">Country</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                                <asp:TextBox ID="txtCountry" Enabled="false" runat="server"
                                                                                    CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                                            <label class="control-label">State</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                                <asp:TextBox ID="txtState" Enabled="false" runat="server"
                                                                                    CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6 " style="display: none">
                                                                            <label class="control-label">City</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                                <asp:TextBox ID="txtCity" Enabled="false" runat="server"
                                                                                    CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg"></div>
                                                                            </div>
                                                                        </div>


                                                                        <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 ">
                                                                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click"
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


                                        <div class="col-sm-12  text-center">
                                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkAddMore" OnClick="lnkAddMore_Click" CssClass="button form-control-blue" runat="server"> 
                                                <i class="fa fa-plus-square"></i> &nbsp; Add Education Details Box </asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane" id="poststab3" style="display: none !important">

                                    <div class="row mgbt-xs-20">

                                        <div class="col-sm-12  full-width-100">
                                            <div class="panel widget">
                                                <div class="panel-heading vd_bg-dark-blue">
                                                    <h3 class="panel-title"><span class="menu-icon"><i class="glyphicon glyphicon-list-alt"></i></span>&nbsp; Scholarship Details  </h3>
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
                                                        <asp:UpdatePanel ID="UpdatePanel87" runat="server">
                                                            <ContentTemplate>


                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Current Year and Duration of Course</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtDuration" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>

                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Registration No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtRegistration" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Caste Certificate No. </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtCastCerti" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Date of issue Income Certificate</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                        <asp:TextBox ID="TextBox148" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Income Certificate No. </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtIncomeCerti" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Date of issue Income Certificate</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                        <asp:TextBox ID="TextBox149" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Domicile Certificate No.  </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtRegiCer" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Date of issue Domicile Certificate </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                        <asp:TextBox ID="TextBox150" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">1st Year Admission Date</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                        <asp:TextBox ID="TextBox151" placeholder=" " runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Current Year Admission Date</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                        <asp:TextBox ID="TextBox152" placeholder=" " runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Type of Course </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-graduation-cap"></i></span>
                                                                        <asp:TextBox ID="txtTypeofCourse" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Type of Admission </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-lightbulb-o"></i></span>
                                                                        <asp:TextBox ID="txtTypeofAdmission" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Bank Account No.  </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtBankAccNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Bank Name </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-bank"></i></span>
                                                                        <asp:TextBox ID="txtBankName" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Branch Name of Bank  </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-map-marker"></i></span>
                                                                        <asp:TextBox ID="txtBranchNameofBank" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">IFS Code </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtIfsCode" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Student Name in Bank Passbook </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                        <asp:TextBox ID="txtStudentNameinPassbook" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Day Scholar / Hosteller</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-building"></i></span>
                                                                        <asp:TextBox ID="txtDayScholer" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Yearly None Refundeble Fee</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtYearlynonrefund" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Handicapped Type </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-wheelchair"></i></span>
                                                                        <asp:TextBox ID="txthandycaptype" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Handicapped Percentage  </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-wheelchair"></i></span>
                                                                        <asp:TextBox ID="txthandycapPer" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Handicapped Compensation</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-wheelchair"></i></span>
                                                                        <asp:TextBox ID="txthandycapCompe" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Receipt No. of Deposit Fee</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtReciptNoofDepositFee" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Deposit Fee Date  </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                        <asp:TextBox ID="TextBox154" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Last Year Scholarship Amount  </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtLastYearScholarAmount" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Last Year Scholarship Deposit Fee </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtLastYearScholarDepoFee" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Last Year Class / Course</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-graduation-cap"></i></span>
                                                                        <asp:TextBox ID="txtLastClass" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Last Year Exam Result </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-trophy"></i></span>
                                                                        <asp:TextBox ID="txtLastYearExamResult" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Last Year Exam Total Marks  </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-thumbs-up"></i></span>
                                                                        <asp:TextBox ID="txtLastYearExamTatalMarks" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Last Year Exam Total Obtain Marks</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-thumbs-o-up"></i></span>
                                                                        <asp:TextBox ID="txtLastYearExamTotalObtainMarks" placeholder="" runat="server"
                                                                            CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Scholarship Compensation Amount According to Class / Course  </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-graduation-cap"></i></span>
                                                                        <asp:TextBox ID="txtScholarCompeAmountAccotoClass" placeholder="" runat="server"
                                                                            CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Name of Institute</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-institution"></i></span>
                                                                        <asp:TextBox ID="txtNameofInstitute" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Intermediate Roll No. </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtIntermediateRollNo" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Intermediate Board  </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-tag"></i></span>
                                                                        <asp:TextBox ID="txtIntermediateBoard" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Intermediate Passing Year </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-certificate"></i></span>
                                                                        <asp:TextBox ID="txtIntermediateYearofPssing" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Is Entry based on Intermediate Marks Score </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-level-up"></i></span>
                                                                        <asp:TextBox ID="txtIsEntrybasedonInterMarksScore" placeholder="" runat="server"
                                                                            CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Total Marks in Intermediate</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-thumbs-up"></i></span>
                                                                        <asp:TextBox ID="txtTotalMarksinInter" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Total obtained Marks in Intermediate</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-thumbs-o-up"></i></span>
                                                                        <asp:TextBox ID="txtTotalobtainedMarksinInter" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Student Aadhar Card No. </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtStudentAdharNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Transfer Certificate No. </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                        <asp:TextBox ID="txtTransferCertiNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Transfer Certificate Date </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                        <asp:TextBox ID="TextBox153" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Last School / College Name </label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-institution"></i></span>
                                                                        <asp:TextBox ID="txtLastSchoolCollegeName" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>


                                                                <div class="col-sm-12  no-padding ">

                                                                    <div class="col-sm-4  half-width-50 no-padding ">

                                                                        <div class="col-sm-12  mgbt-xs-15">
                                                                            <label class="control-label">Last School / College Name </label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa icon-vcard"></i></span>
                                                                                <asp:TextBox ID="txtIdentityProof" placeholder="Identity Proof" runat="server"
                                                                                    CssClass="form-control-blue"></asp:TextBox>

                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-12  mgbt-xs-15">
                                                                            <label class="control-label">Upload Student Photo</label>
                                                                            <div class="vd_input-wrapper controls img-input-ped ">
                                                                                <asp:FileUpload ID="fpUploadPhoto" runat="server" CssClass="form-control-blue " />
                                                                            </div>
                                                                        </div>


                                                                        <div class="col-sm-12  mgbt-xs-15">
                                                                            <label class="control-label">Upload Student Signature</label>
                                                                            <div class="vd_input-wrapper controls img-input-ped ">
                                                                                <asp:FileUpload ID="fuUploadStudentSignature" runat="server" CssClass="form-control-blue " />
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-12  mgbt-xs-15">
                                                                            <label class="control-label">Upload Father / Mother Signature & Thumb Print</label>
                                                                            <div class="vd_input-wrapper controls img-input-ped ">
                                                                                <asp:FileUpload ID="fuUploadFatherMotherSigThumbPrint" runat="server" CssClass="form-control-blue " />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-4  half-width-50 no-padding ">

                                                                        <div class="col-sm-12  full-width-100 ">

                                                                            <div class="col-sm-12  mgbt-xs-15 no-padding">
                                                                                <div class="stu-pic-box">
                                                                                    <div class="stu-pic-box-main">
                                                                                        <img src="../img/student-pic/big.jpg" alt="" style="display: none;" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-sm-12   mgbt-xs-15 no-padding">
                                                                                <div class="stu-sign-pic-box">
                                                                                    <div class="stu-sign-pic-box-main">
                                                                                        <img src="../img/student-pic/user-signature.png" alt="" style="display: none;" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4  half-width-50 no-padding ">
                                                                        <div class="col-sm-12  full-width-100 no-padding">
                                                                            <div class="col-sm-10 col-xs-10 set-box-in-center no-padding">
                                                                                <div class="group-pic-box">
                                                                                    <div class="group-pic-box-main">
                                                                                        <img style="height: 261px;" class="imgGroupPhoto" src="../img/user-pic/group-photo.jpg" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                </div>
                                            </div>
                                            <!-- Panel Widget -->
                                        </div>

                                    </div>
                                </div>

                            </div>


                        </div>

                    </div>
                </div>
            </div>


            <script type="text/javascript">

                var pam1 = Sys.WebForms.PageRequestManager.getInstance();
                var pam2 = Sys.WebForms.PageRequestManager.getInstance();

                pam1.add_endRequest(BeginRequestHandlerUpdate);
                pam2.add_endRequest(BeginRequestHandlerNorm());

                function BeginRequestHandlerUpdate(sender, args) {
                    try {
                        var txtfromdate = document.getElementById('<%= txtAgeOnDate.ClientID %>').value;
    var txttodate = document.getElementById('<%= txtStudentDOB.ClientID %>').value;
    var fromdate = new Date(txtfromdate).format("yyyy/MM/dd");
    var todate = new Date(txttodate).format("yyyy/MM/dd");


    PageMethods.getAgeofStudent(fromdate, todate, Onsuccess);

}
    catch (err) {

        alert(err.toString());
    }
}

function BeginRequestHandlerNorm() {
    try {
        var txtfromdate = document.getElementById('<%= txtAgeOnDate.ClientID %>').value;
    var txttodate = document.getElementById('<%= txtStudentDOB.ClientID %>').value;
    var fromdate = new Date(txtfromdate).format("yyyy/MM/dd");
    var todate = new Date(txttodate).format("yyyy/MM/dd");


    PageMethods.getAgeofStudent(fromdate, todate, Onsuccess);

}
    catch (err) {

        alert(err.toString());
    }
}
function Onsuccess(datepart) {
    try {
        var txtYear = document.getElementById('<%= txtAgeYear.ClientID %>');
    var txtMonth = document.getElementById('<%=txtAgeMonth.ClientID %>');
    var txtDate = document.getElementById('<%=txtAgeDay.ClientID %>');
    txtYear.value = datepart[0];
    txtMonth.value = datepart[1];
    txtDate.value = datepart[2];
}
    catch (err) {

        alert(err.toString());
    }
}
            </script>

            <script type="text/javascript">
                function finalsubmit(value) {
                    if (value != null) {
                        if (value === 'C') {
                            var textelement = document.getElementsByTagName("input");
                            for (var i = 0; i < textelement.length; i++) {
                                if (textelement[i].type.toLowerCase() === "text") {
                                    textelement[i].value = textelement[i].value.charAt(0).toUpperCase() + textelement[i].value.substring(1, textelement[i].value.length).toLowerCase();
                                }
                            }
                        }
                        if (value === 'U') {
                            var textelement = document.getElementsByTagName("input");
                            for (var i = 0; i < textelement.length; i++) {
                                if (textelement[i].type.toLowerCase() === "text") {
                                    textelement[i].value = textelement[i].value.toUpperCase();
                                }
                            }
                        }
                        if (value === 'L') {
                            var textelement = document.getElementsByTagName("input");
                            for (var i = 0; i < textelement.length; i++) {
                                if (textelement[i].type.toLowerCase() === "text") {
                                    textelement[i].value = textelement[i].value.toLowerCase();
                                }
                            }
                        }

                    }
                }
            </script>

        </div>
</asp:Content>

