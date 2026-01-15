<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StudentQuickRegistration.aspx.cs" Inherits="_1.StudentQuickRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        .cursor {
            cursor: unset !important;
        }

        .redBorder {
            border: 1px solid red !important;
        }
    </style>
    <script>
        function dateformatvalidate(tis) {
            let valDate = $(tis).val();
            if (valDate != "") {
                let isValidDate = Date.parse(valDate);
                if (isNaN(isValidDate)) {
                    $(tis).addClass('redBorder')
                    $(tis).val('');
                    $(tis).attr('placeholder', 'Invalid Date');
                }
                else {
                    let mS = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];
                    let date = new Date($(tis).val());
                    const day = date.toLocaleString('default', { day: '2-digit' });
                    const month = date.toLocaleString('default', { month: 'short' });
                    const month2 = date.toLocaleString('default', { month: 'numeric' });
                    const year = date.toLocaleString('default', { year: 'numeric' });
                    var date1 = day + '-' + month + '-' + year;
                    //var date2 = day + '/' + (month2 < 10 ? '0' + month2 : month2) + '/' + year;
                    //var date3 = (month2 < 10 ? '0' + month2 : month2) + '/' + day + '/' + year;
                    //if (date1 == $(tis).val() || date2 == $(tis).val() || date3 == $(tis).val()) {
                    if (date1 == $(tis).val()) {
                        $(tis).removeClass('redBorder');
                    }
                    else {
                        $(tis).addClass('redBorder');
                        $(tis).val('');
                        $(tis).attr('placeholder', 'Invalid Date Format');
                    }
                }
            }
        }
        if ($(".datepicker-normal").hasClass('hasDatepicker')) {
            $(".datepicker-normal").addClass('cursor');
            $(".datepicker-normal").removeClass('hasDatepicker');
            $(".datepicker-normal").css('border', 'none !important');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <asp:UpdatePanel ID="UpdatePanel69" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-12" runat="server" id="divReceipForAddmission">
                                    <div class="col-sm-4   mgbt-xs-15" style="padding-left: 0;">
                                        <div class="vd_input-wrapper">
                                            <span class="menu-icon"><i class="fa fa-eye"></i></span>
                                            <asp:TextBox ID="TextBox67" placeholder="Enter Admission Receipt No." runat="server" AutoPostBack="true" OnTextChanged="TextBox67_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 ">
                                        <div class="col-xs-6 no-padding text-left mgbt-xs-15">
                                            <asp:LinkButton ID="LinkButton1" runat="server" class="button form-control-blue" OnClick="LinkButton1_Click"> View</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 75px;"></div>
                                        </div>
                                        <div class="col-xs-6 text-right no-padding mgbt-xs-15"></div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="col-sm-12   vd_input-margin ">
                            <div class="row mgbt-xs-20">
                                <div class="col-sm-6  full-width-100">
                                    <div class="panel widget">
                                        <div class="panel-heading vd_bg-dark-blue">
                                            <h3 class="panel-title"><span class="menu-icon"><i class="fa  fa-child"></i></span>Student's Personal Details </h3>
                                            <!-- vd_panel-menu -->
                                        </div>
                                        <div class="panel-body padding-tlbr-15 form-main-box-border">
                                            <div class="row">
                                                <asp:UpdatePanel ID="UpdatePanel62" runat="server">
                                                    <ContentTemplate>
                                                        <script>
                                                            Sys.Application.add_load(datetime);
                                                        </script>
                                                        <div class="col-sm-8  mgbt-xs-15">
                                                            <label class="control-label">Student's Name (First Name)&nbsp;<span class="vd_red">*</span></label>
                                                            <div>
                                                                <asp:TextBox ID="txtFirstNa" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="col-sm-12  no-padding control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="col-sm-12 no-padding controls">
                                                                <asp:DropDownList ID="RadioButtonList1" runat="server" CssClass="vd_radio radio-success 
                                                                                                validaterblist txt-capitalize-alpha"
                                                                    RepeatDirection="Horizontal" RepeatLayout="flow">
                                                                    <asp:ListItem Value="Male" Selected="True">Male</asp:ListItem>
                                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                                    <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12  no-padding ">
                                                            <div class="col-sm-12  no-padding ">
                                                                <div class="col-sm-4  mgbt-xs-15">
                                                                    <label class="control-label">Religion&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class=" controls ">
                                                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  mgbt-xs-15" style="margin-bottom: 2px !important;">
                                                                    <label class="control-label">Date of Birth&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                        <asp:TextBox ID="txtStudentDOB" placeholder="dd-MMM-yyyy"
                                                                            runat="server" CssClass="form-control-blue dateblank datepicker-normal validatetxt" onblur="dateformatvalidate(this)" onchange="BeginRequestHandlerNorm();"></asp:TextBox>
                                                                        <div style="font-size: 10px; color: red;">Ex.: 01-Apr-2021</div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  mgbt-xs-15" style="margin-bottom: 2px !important;">
                                                                    <label class="control-label">Age on Date&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                        <asp:TextBox ID="txtAgeOnDate" placeholder="Age on Date" onblur="dateformatvalidate(this)" onchange="BeginRequestHandlerNorm();"
                                                                            runat="server" CssClass="form-control-blue currDate datepicker-normal validatetxt"></asp:TextBox>
                                                                        <div style="font-size: 10px; color: red;">Ex.: 01-Apr-2021</div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  mgbt-xs-15">
                                                                    <label class="control-label">Years&nbsp;<span class="vd_red"></span></label>
                                                                    <div class="">
                                                                        <asp:TextBox ID="txtAgeYear" placeholder="00" runat="server" Enabled="false"
                                                                            CssClass="form-control-blue text-center"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  mgbt-xs-15">
                                                                    <label class="control-label">Months&nbsp;<span class="vd_red"></span></label>
                                                                    <div class="">
                                                                         <asp:TextBox ID="txtAgeMonth" placeholder="00" runat="server" Enabled="false"
                                                                            CssClass="form-control-blue text-center"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  mgbt-xs-15">
                                                                    <label class="control-label">Days&nbsp;<span class="vd_red"></span></label>
                                                                    <div class="">
                                                                         <asp:TextBox ID="txtAgeDay" placeholder="00" runat="server" Enabled="false"
                                                                            CssClass="form-control-blue text-center"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  mgbt-xs-15">
                                                                    <label class="control-label">Category&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class=" controls ">
                                                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue "></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6  mgbt-xs-15 hide">
                                                                    <label class="control-label">Nationality&nbsp;<span class="vd_red">*</span></label>
                                                                    <div class="">
                                                                        <span class="menu-icon"><i class="fa  fa-map-marker"></i></span>
                                                                        <asp:TextBox ID="TextBox65" runat="server"
                                                                            CssClass="form-control-blue validatetxt validatetxtA" ToolTip="Nationality"></asp:TextBox>
                                                                        <div class="text-box-msg">
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4  half-width-100-tc mgbt-xs-15">
                                                                    <label class="control-label" runat="server" id="lblAadhaar2">Aadhaar No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                        <asp:TextBox ID="txtAadharNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 <div class="col-sm-4  half-width-100-tc mgbt-xs-15">
                                                                    <label class="control-label" runat="server" id="Label1">UDISE (PEN)</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                      <asp:TextBox ID="txtpen" runat="server" CssClass="form-control-blue" MaxLength="20"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                    </div>
                                                                </div>
                                                                <script>
                                                                    function Aadhar() {
                                                                        $('').on('keyup', function () {
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
                                            <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>&nbsp; Student's Admission Details </h3>
                                        </div>
                                        <div class="panel-body padding-tlbr-15 form-main-box-border">
                                            <div class="row">
                                                <asp:UpdatePanel ID="UpdatePanel76" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12  no-padding">
                                                            <div class="col-sm-4  mgbt-xs-15" style="margin-bottom: 2px !important;">
                                                                <label class="control-label">Date of Admission&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                    <asp:TextBox ID="TextBox100" placeholder="" runat="server"
                                                                        CssClass="form-control-blue datepicker-normal validatetxt" onblur="dateformatvalidate(this)"></asp:TextBox>
                                                                    <div style="font-size: 10px; color: red;">Ex.: 01-Apr-2021</div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                <label class="control-label">Course&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">
                                                                    <asp:UpdatePanel ID="UpdatePanel59" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="DropCourse" runat="server"
                                                                                CausesValidation="True" CssClass="form-control-blue validatedrp"
                                                                                AutoPostBack="True" OnSelectedIndexChanged="DropCourse_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">
                                                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="DropAdmissionClass" runat="server" AutoPostBack="True"
                                                                                CssClass="form-control-blue validatedrp"
                                                                                OnSelectedIndexChanged="DropAdmissionClass_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12  no-padding">
                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">
                                                                    <asp:UpdatePanel ID="UpdatePanel58" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="drpSection" runat="server" CausesValidation="false"
                                                                                CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">
                                                                    <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="DropBranch" runat="server" CssClass="form-control-blue validatedrp"
                                                                                OnSelectedIndexChanged="DropBranch_SelectedIndexChanged"
                                                                                AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                <label class="control-label">Group&nbsp;<span class="vd_red"></span></label>
                                                                <div class="">
                                                                    <asp:DropDownList ID="DropStream" runat="server" CssClass="form-control-blue">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12  no-padding">
                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">
                                                                    <asp:DropDownList ID="drpMedium" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                <label class="control-label">Board/ University&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">
                                                                    <asp:DropDownList ID="DrpBoard" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                <label class="control-label">Type of Admission&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">
                                                                    <asp:DropDownList ID="DrpNEWOLSAdmission" runat="server" CssClass="form-control-blue">
                                                                        <asp:ListItem Selected="True">NEW</asp:ListItem>
                                                                        <asp:ListItem>OLD</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15" style="display: none">
                                                            <label class="control-label">Enquiry No. </label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                <asp:TextBox ID="txtEnquiryNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">Fee Category &nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:DropDownList ID="drpPanelCardType" runat="server"
                                                                    CssClass="form-control-blue validatedrp">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-8  mgbt-xs-15" runat="server" id="divSrNo" visible="false">
                                                            <label class="control-label">S.R. No.&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <div style="width: 20%; padding: 0; float: left; padding-top: 6px;">
                                                                    <asp:Label ID="lblSrString" placeholder="" runat="server" CssClass="form-control-blue"></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtSr" AutoPostBack="true" placeholder="" runat="server" Style="width: 80%; padding-left: 0;" OnTextChanged="txtSr_TextChanged"></asp:TextBox>
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
                            <div class="row mgbt-xs-20">
                                <div class="col-sm-6  full-width-100">
                                    <div class="panel widget">
                                        <div class="panel-heading vd_bg-dark-blue">
                                            <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-male"></i></span>Parent's Details </h3>
                                        </div>
                                        <div class="panel-body padding-tlbr-15 form-main-box-border">
                                            <div class="row">

                                                <asp:UpdatePanel ID="UpdatePanel67" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">Father's Name&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:TextBox ID="txtfaNameee" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">Occupation&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:DropDownList ID="drpOccupationfa" runat="server"
                                                                    AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  ">
                                                            <div class="col-sm-12  no-padding mgbt-xs-15">
                                                                <label class="control-label">Mobile No.&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-phone"></i></span>
                                                                    <asp:TextBox ID="txtcontfa" runat="server" CssClass="form-control-blue validatetxt" MaxLength="10"></asp:TextBox>

                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">Mother's Name&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:TextBox ID="txtmotherNameeee" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">Occupation&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:DropDownList ID="drpOccupationmoth" runat="server"
                                                                    CssClass="form-control-blue validatedrp">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">
                                                                Mobile No.&nbsp;<span class="vd_red">*</span>
                                                            </label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa  fa-phone"></i></span>
                                                                <asp:TextBox ID="txtmothercontact" placeholder="" runat="server"
                                                                    CssClass="form-control-blue validatetxt" MaxLength="10"></asp:TextBox>
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
                                            <h3 class="panel-title"><span class="menu-icon"><i class="glyphicon glyphicon-map-marker"></i></span>Present Address </h3>
                                        </div>
                                        <div class="panel-body padding-tlbr-15 form-main-box-border">
                                            <div class="row">
                                                <div class="col-sm-4  mgbt-xs-15 hide">
                                                    <label class="control-label">Country&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="DrpPreCountry" runat="server" CssClass="form-control-blue validatedrp">
                                                                    <asp:ListItem Text="<-- Select Country -->"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel63" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-8 mgbt-xs-15">
                                                            <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:TextBox ID="txtPreaddress" placeholder="Please don't write State and City name here" runat="server"
                                                                 style="padding: 4px;"   CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                            </div>
                                                        </div>


                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="DrpPreState" runat="server" AutoPostBack="True"
                                                                            CssClass="form-control-blue " OnSelectedIndexChanged="DrpPreState_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:DropDownList ID="DrpPreCity" runat="server" CssClass="form-control-blue validatedrp">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">Pin Code</label>
                                                            <div class="">
                                                                <asp:TextBox ID="txtPreZip" placeholder="Pin Code" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  mgbt-xs-15">
                                                            <label class="control-label">House Name &nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:DropDownList ID="DropDownList4" runat="server"
                                                                    CssClass="form-control-blue validatedrp">
                                                                </asp:DropDownList>
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
                            <div class="row ">
                                <div class="col-md-12 ">
                                    <div class="btn-center-box-130" runat="server" id="btnDiv">
                                        <asp:UpdatePanel ID="UpdatePanel88" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkButton14" OnClick="LinkButton14_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"
                                                    CssClass="btn vd_btn vd_bg-blue" runat="server">Submit</asp:LinkButton>
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
    <script type="text/javascript">

            //var pam1 = Sys.WebForms.PageRequestManager.getInstance();
            //var pam2 = Sys.WebForms.PageRequestManager.getInstance();
            //pam1.add_endRequest(BeginRequestHandlerUpdate);
            //pam2.add_endRequest(BeginRequestHandlerNorm());
            function BeginRequestHandlerUpdate(args) {
                try {
                    var txtfromdate = document.getElementById('<%= txtAgeOnDate.ClientID %>').value;
                    var txttodate = document.getElementById('<%= txtStudentDOB.ClientID %>').value;
                    var fromdate = new Date(txtfromdate).format("yyyy/MM/dd");
                    var todate = new Date(txttodate).format("yyyy/MM/dd");
                    window.PageMethods.GetAgeofStudent(fromdate, todate, Onsuccess);
                }
                catch (err) {

                    //alert(err.toString());
                }
            }

            function BeginRequestHandlerNorm() {
                try {
                    var txtfromdate = document.getElementById('<%= txtAgeOnDate.ClientID %>').value;
                    var txttodate = document.getElementById('<%= txtStudentDOB.ClientID %>').value;
                    var fromdate = new Date(txtfromdate).format("yyyy/MM/dd");
                    var todate = new Date(txttodate).format("yyyy/MM/dd");
                    window.PageMethods.GetAgeofStudent(fromdate, todate, Onsuccess);
                }
                catch (err) {
                    //alert(err.toString());
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
                    //alert(err.toString());
                }
            }
        </script>
    
</asp:Content>

