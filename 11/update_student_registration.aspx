<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="update_student_registration.aspx.cs" Inherits="_11.UpdateStudentRegistration" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="update_student_registration.aspx.cs" Inherits="_11.UpdateStudentRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=TxtEnter]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetStudents") %>',
                            data: "{ 'studentId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d,
                                    function (item) {
                                        return {
                                            label: item.split('@')[0],
                                            val: item.split('@')[1]
                                        }
                                    }));
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfStudentId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
    <script type="text/javascript">
        function copyaddress() {
            $(document).ready(function () {
                $('input:checkbox[id*=chkCopy]').change(function () {
                    if ($(this).is(':checked')) {
                        $('#<%=txtPerAdd.ClientID %>').val($('#<%=txtPreaddress.ClientID %>').val());
                        // $('input:text[id*=txtPerAdd]').val($('input:text[id*=txtPreaddress]').val());
                        //$('input:text[id*=DrpPerState]').val($('input:text[id*=DrpPreState]').val());
                        //$('input:text[id*=DrpPerCity]').val($('input:text[id*=DrpPreCity]').val());
                        $('input:text[id*=txtPerZip]').val($('input:text[id*=txtPreZip]').val());
                        $('input:text[id*=txtPerMobileNo]').val($('input:text[id*=txtPreMobileNo]').val());
                        $('input:text[id*=txtPerPhoneNo]').val($('input:text[id*=txtPrePhoneNo]').val());
                    }
                    else {
                        $('#<%=txtPerAdd.ClientID %>').val('');
                        $('input:text[id*=txtPerZip]').val('');
                        $('input:text[id*=txtPerMobileNo]').val('');
                        $('input:text[id*=txtPerPhoneNo]').val('');
                        //$('input:text[id*=txtBillAddr1]').val('');
                        //$('input:text[id*=txtBillAddr2]').val('');
                    }
                });
            });
        }
    </script>
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
                    $(tis).attr('placeholder', 'Invalid Date.');
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
    <asp:UpdatePanel runat="server" ID="d">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <script>
                        Sys.Application.add_load(copyaddress);
                        Sys.Application.add_load(getStudentsList);
                        //checksFileSizeandFileTypeinupdatePanel(fileupload, size, filetype, imageClass, hiddenfield)
                    </script>
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-lg-12" id="div1" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                                <div class=" ">
                                                    <asp:DropDownList ID="drpClassforStaff" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="drpClassforStaff_SelectedIndexChanged" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:DropDownList ID="drpSectionforStaff" runat="server" CssClass="form-control-blue"
                                                        AutoPostBack="True" OnSelectedIndexChanged="drpSectionforStaff_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:DropDownList ID="drpBranchforStaff" runat="server" CssClass="form-control-blue"
                                                        AutoPostBack="True" OnSelectedIndexChanged="drpBranchforStaff_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                <label class="control-label">S.R. NO.&nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:DropDownList ID="drpSrno" runat="server" CssClass="form-control-blue"
                                                        AutoPostBack="True" OnSelectedIndexChanged="drpSrno_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-12  no-padding" id="div2" runat="server">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 select-list-hide display-none">
                                        <asp:DropDownList ID="DrpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Name/ S.R. No.!</asp:ListItem>
                                        </asp:DropDownList>
                                        <i>H</i>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                        <asp:TextBox ID="TxtEnter" placeholder="Enter Name/ S.R. No." runat="server"
                                            CssClass="form-control-blue width-100" AutoPostBack="true" OnTextChanged="TxtEnter_TextChanged"></asp:TextBox>

                                        <div class="text-box-msg">
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                        </div>
                                    </div>
                                    <%--///sdsds--%>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                        <div class=" pull-left">
                                            <asp:UpdatePanel ID="dgv" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkShow" runat="server" OnClick="lnkShow_Click"
                                                        CssClass="button form-control-blue">View</asp:LinkButton>
                                                    <div id="msgbox" runat="server" style="left: 58px"></div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class=" pull-right" style="display: none;">
                                            <asp:LinkButton ID="lnkPreview" runat="server" OnClick="lnkPreview_Click" ValidationGroup="bb"
                                                CssClass="button form-control-blue">Preview</asp:LinkButton>
                                            <asp:Label ID="Label4" runat="server" Visible="False"></asp:Label>
                                        </div>
                                        <div class=" pull-right" style="display: none">
                                            <asp:LinkButton ID="lnkQuickReg" runat="server" OnClick="lnkQuickReg_Click"
                                                CssClass="button form-control-blue">Quick Updation</asp:LinkButton>
                                        </div>
                                    </div>


                                </div>

                                <div class="col-sm-12   vd_input-margin ">
                                    <div class="tabs">
                                        <asp:HiddenField ID="hdnActiveTab" runat="server" />
                                        <ul class="nav nav-tabs nav-justified">
                                            <li id="liGeneralDetails" class="active"><a href="#home-tab" data-toggle="tab"><span class="menu-icon">
                                                <i class="glyphicon glyphicon-list-alt"></i></span>&nbsp; General Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                            <li id="liFamilyDetails"><a href="#posts-tab" data-toggle="tab">
                                                <span class="menu-icon"><i class="fa fa-users"></i></span>&nbsp; Family Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>

                                            <li id="liOfficialDetails"><a href="#posts-tab2" data-toggle="tab"><span class="menu-icon">
                                                <i class=" icon-newspaper"></i></span>&nbsp; Official Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                            <li id="liDocuments" onclick="captureImage();"><a href="#list-tab2" data-toggle="tab"><span class="menu-icon">
                                                <i class="fa fa-file-archive-o"></i></span>&nbsp; Photo & Documents <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                            <li id="liPreviousEducation"><a href="#list-tab" data-toggle="tab"><span class="menu-icon">
                                                <i class="icon-graduation"></i></span>&nbsp; Previous Education Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                            <li id="otherdetails" style="display: none"><a href="#poststab3" data-toggle="tab"><span class="menu-icon">
                                                <i class="glyphicon glyphicon-list-alt"></i></span>&nbsp; Scholarship Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>
                                            <%--<li id="helthdetails"><a href="#helthdetail" data-toggle="tab"><span class="menu-icon">
                                                <i class="glyphicon glyphicon-user"></i></span>&nbsp; Health Details <span class="menu-active"><i class="fa fa-caret-up"></i></span></a></li>--%>
                                        </ul>

                                        <div class="tab-content form-box-border mgbt-xs-20">

                                            <div class="tab-pane active " id="home-tab">

                                                <div class="row mgbt-xs-20">

                                                    <div class="col-sm-6  full-width-100">
                                                        <div class="panel widget">
                                                            <div class="panel-heading vd_bg-dark-blue">
                                                                <h3 class="panel-title"><span class="menu-icon"><i class="fa  fa-child"></i></span>Student's Personal Details </h3>
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
                                                                    <asp:UpdatePanel ID="UpdatePanel62" runat="server">
                                                                        <ContentTemplate>
                                                                            <script>
                                                                                Sys.Application.add_load(datetime);
                                                                            </script>
                                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                                <label class="control-label">First Name&nbsp;<span class="vd_red">*</span></label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                                    <asp:TextBox ID="txtFirstNa" runat="server" CssClass="form-control-blue  validatetxt "></asp:TextBox>
                                                                                    <div class="text-box-msg">
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                                <label class="control-label">Middle Name</label>
                                                                                <div class="vd_input-wrapper controls">
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
                                                                                    <asp:TextBox ID="txtlast" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg">
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12  no-padding ">

                                                                                <div class="col-sm-12  no-padding">
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
                                                                                    <div class="col-sm-4 ">
                                                                                        <label class="control-label">Date of Birth&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                            <asp:TextBox ID="txtStudentDOB" placeholder="yyyy MMM dd"
                                                                                                runat="server" CssClass="form-control-blue dateblank datepicker-normal validatetxt" onblur="dateformatvalidate(this)" onchange="BeginRequestHandlerNorm();"></asp:TextBox>
                                                                                            <div style="font-size: 10px; color: red;">Ex.: 01-Apr-2021</div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4  mgbt-xs-15">
                                                                                        <label class="control-label">UDISE (PEN)</label>
                                                                                        <div class="vd_input-wrapper controls">
                                                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                            <asp:TextBox ID="txtpen" runat="server" CssClass="form-control-blue" MaxLength="20"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4  mgbt-xs-15 hide">
                                                                                        <label class="control-label">Age on Date&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                            <asp:TextBox ID="txtAgeOnDate" placeholder="Age on Date"
                                                                                                runat="server" CssClass="form-control-blue currDate datepicker-normal validatetxt" onblur="dateformatvalidate(this)" onchange="BeginRequestHandlerNorm();"></asp:TextBox>
                                                                                            <div class="text-box-msg">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                    <div class="col-sm-12  mgbt-xs-15 hide">
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


                                                                                    <div runat="server" id="divStudentEmailContact" visible="false">
                                                                                        <div class="col-sm-6  no-padding ">
                                                                                            <div class="col-sm-12">
                                                                                                <label class="control-label">Email</label>
                                                                                                <div class="vd_input-wrapper controls ">
                                                                                                    <span class="menu-icon"><i class="fa fa-envelope"></i></span>
                                                                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-blue" onBlur="ValidateEmails(this);"></asp:TextBox>
                                                                                                    <div class="text-box-msg">
                                                                                                    </div>

                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-12  mgbt-xs-15">
                                                                                                <asp:CheckBox ID="chkStEmail" runat="server" CssClass="vd_checkbox checkbox-success  " Visible="false"
                                                                                                    RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for Email Alert" />
                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="col-sm-6  no-padding ">
                                                                                            <div class="col-sm-12 ">
                                                                                                <label class="control-label">Mobile No.</label>
                                                                                                <div class="vd_input-wrapper controls ">
                                                                                                    <span class="menu-icon"><i class="fa  fa-mobile"></i></span>
                                                                                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control-blue" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>

                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-12  mgbt-xs-15">
                                                                                                <asp:CheckBox ID="chkStMobile" runat="server" CssClass="vd_checkbox checkbox-success " Visible="false"
                                                                                                    RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for SMS Alert" />
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
                                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                <label class="control-label">Mother Tongue&nbsp;<span class="vd_red">*</span></label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa  fa-bullhorn"></i></span>
                                                                                    <asp:TextBox ID="txtMotherTongue" runat="server" CssClass="form-control-blue  validatetxt "></asp:TextBox>
                                                                                    <div class="text-box-msg">
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                <label class="control-label">Home Town&nbsp;<span class="vd_red">*</span></label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa  fa-home"></i></span>
                                                                                    <asp:TextBox ID="txtHomeTown" runat="server" CssClass="form-control-blue  validatetxt "></asp:TextBox>
                                                                                    <div class="text-box-msg">
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                <label class="control-label">Nationality&nbsp;<span class="vd_red">*</span></label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa  fa-map-marker"></i></span>
                                                                                    <asp:TextBox ID="TextBox65" runat="server"
                                                                                        CssClass="form-control-blue  validatetxt " ToolTip="Nationality"></asp:TextBox>
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
                                                                                <label class="control-label">Category&nbsp;<span class="vd_red">*</span></label>
                                                                                <div class=" controls ">
                                                                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue  validatedrp"></asp:DropDownList>
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
                                                                                    <%-- ReSharper disable once Asp.SkinNotResolved --%>
                                                                                    <asp:TextBox ID="TextBox66" runat="server" SkinID="TxtBoxDef" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg">
                                                                                    </div>

                                                                                </div>
                                                                            </div>

                                                                            <div class="col-sm-12  half-width-50 half-width-100-tc mgbt-xs-15">
                                                                                <label class="control-label" runat="server" id="lblAadhaar3">Aadhaar No</label>&nbsp;<span class="vd_red" id="txtAadharNored" runat="server">*</span>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                                    <%-- ReSharper disable once Asp.SkinNotResolved --%>
                                                                                    <asp:TextBox ID="txtAadharNo" runat="server"
                                                                                        SkinID="TxtBoxDef" CssClass="form-control-blue " MaxLength="12" AutoPostBack="true" OnTextChanged="txtAadharNo_TextChanged" onBlur="ChecktwellebDigitMobileNumber(this);"></asp:TextBox>
                                                                                    <div class="text-box-msg">
                                                                                    </div>

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

                                                                            <div class="col-sm-12  half-width-50 half-width-100-tc hide">
                                                                                <label class="control-label">
                                                                                    Date of Issue of
                                                                                    <label runat="server" id="lblAadhaar2">Aadhaar</label></label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                                    <%-- ReSharper disable once Asp.SkinNotResolved --%>
                                                                                    <asp:TextBox ID="txtAadharIssueDate" placeholder="Ex. 1990 JAN 01" runat="server"
                                                                                        SkinID="TxtBoxDef" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                                    <div class="text-box-msg">
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
                                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                <label class="control-label">Blood Group</label>
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

                                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">

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

                                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                <div class="col-sm-12  no-padding  ">
                                                                                    <label class="control-label">Height</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-level-up"></i></span>
                                                                                        <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control-blue"></asp:TextBox>
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

                                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                <div class="col-sm-12  no-padding">
                                                                                    <label class="control-label">Weight</label>
                                                                                </div>
                                                                                <div class="col-sm-12  no-padding  ">
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-database"></i></span>
                                                                                        <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg">
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4 col-xs-4 no-padding hide">

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

                                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                <label class="control-label">Dental Hygiene</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-smile-o"></i></span>
                                                                                    <%-- ReSharper disable once Asp.SkinNotResolved --%>
                                                                                    <asp:TextBox ID="txtDental" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg">
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                <label class="control-label">Oral Hygiene</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-smile-o"></i></span>
                                                                                    <%-- ReSharper disable once Asp.SkinNotResolved --%>
                                                                                    <asp:TextBox ID="txtOral" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg">
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                <label class="control-label">Identification Mark</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-smile-o"></i></span>
                                                                                    <%-- ReSharper disable once Asp.SkinNotResolved --%>
                                                                                    <asp:TextBox ID="txtIMark" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg">
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                <label class="control-label">Specific Ailment</label>
                                                                                <div class="vd_input-wrapper controls ">
                                                                                    <span class="menu-icon"><i class="fa fa-smile-o"></i></span>
                                                                                    <%-- ReSharper disable once Asp.SkinNotResolved --%>
                                                                                    <asp:TextBox ID="txtSpeAilment" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                    <div class="text-box-msg">
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
                                                                <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-wheelchair"></i></span>Specially-Abled Details </h3>
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
                                                                                    <label class="col-sm-4 no-padding control-label">Specially-Abled?</label>
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



                                                <div class="tab-pane" id="posts-tab">

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
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Father's Name&nbsp;<span class="vd_red">*</span></label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                                        <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="txtfaNameee" runat="server"
                                                                                                    onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtguardianname',
                                                                                                '#ContentPlaceHolder1_ContentPlaceHolderMainBox_DrpRelationship','Father');"
                                                                                                    CssClass="form-control-blue  validatetxt "></asp:TextBox>
                                                                                                <div class="text-box-msg">
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Occupation&nbsp;<span class="vd_red">*</span></label>
                                                                                    <div class="">
                                                                                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="drpOccupationfa" runat="server"
                                                                                                    AutoPostBack="True" CssClass="form-control-blue  validatedrp">
                                                                                                </asp:DropDownList>
                                                                                                <div class="text-box-msg">
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Designation</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                                        <asp:TextBox ID="txtdesfa" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg">
                                                                                        </div>
                                                                                    </div>

                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Qualification</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <%--  <span class="menu-icon"><i class="fa  fa-lightbulb-o"></i></span>--%>
                                                                                        <%--    <asp:TextBox ID="txtqufa"  runat="server" CssClass="form-control-blue "></asp:TextBox>--%>
                                                                                        <asp:DropDownList ID="txtqufa" runat="server" CssClass="form-control-blue "></asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Aadhaar No.&nbsp;<span class="vd_red" id="txtfaadhaarcardnored" runat="server">*</span></label>
                                                                                    <div class="">
                                                                                        <asp:TextBox ID="txtfaadhaarcardno" placeholder="" runat="server" CssClass="form-control-blue" MaxLength="12" 
                                                                                            onBlur="ChecktwellebDigitMobileNumber(this);"></asp:TextBox>

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
                                                                                                <asp:TextBox ID="txtincomefa" runat="server"
                                                                                                    onkeyup="AddDecimalValue(event,this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtParentTotalIncome',
                                                                                                '#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtincomemonthlymother');"
                                                                                                    CssClass="form-control-blue"></asp:TextBox>
                                                                                                <div class="text-box-msg">
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>

                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-sm-12  no-padding">


                                                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Nationality&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa  fa-flag"></i></span>

                                                                                            <asp:UpdatePanel ID="UpdatePanel70" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtFatherNationality" runat="server"
                                                                                                        CssClass="form-control-blue  validatetxt "></asp:TextBox>
                                                                                                    <div class="text-box-msg">
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4  half-width-50 ">
                                                                                        <div class="col-sm-12  no-padding mgbt-xs-15">
                                                                                            <label class="control-label">Email &nbsp;</label>
                                                                                            <div class="vd_input-wrapper controls ">
                                                                                                <span class="menu-icon"><i class="fa  fa-envelope"></i></span>
                                                                                                <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:TextBox ID="txtemailfather" runat="server" OnTextChanged="txtemailfather_TextChanged1"
                                                                                                            CssClass="form-control-blue" onBlur="CopyTextBox(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtemailfamily'); ValidateEmails(this);"></asp:TextBox>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                                            <asp:CheckBox ID="chkFaEmail" runat="server" CssClass="vd_checkbox checkbox-success" Visible="false"
                                                                                                RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for Email Alert" />
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-sm-4  half-width-50 ">
                                                                                        <div class="col-sm-12  no-padding mgbt-xs-15">
                                                                                            <label class="control-label">Mobile No.&nbsp;<span class="vd_red">*</span></label>
                                                                                            <div class="vd_input-wrapper controls ">
                                                                                                <span class="menu-icon"><i class="fa fa-phone"></i></span>
                                                                                                <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:TextBox ID="txtcontfa" runat="server"
                                                                                                            onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtcontactNo',
                                                                                                    '#ContentPlaceHolder1_ContentPlaceHolderMainBox_DrpRelationship','Father'); ChecktenDigitMobileNumber(this);"
                                                                                                            CssClass="form-control-blue  validatetxt " MaxLength="10"></asp:TextBox>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                                            <asp:CheckBox ID="chkFaMobile" runat="server" CssClass="vd_checkbox checkbox-success" Visible="false"
                                                                                                RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for SMS Alert" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Alternate Mobile No.</label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa fa-mobile"></i></span>
                                                                                            <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtPreMobileNo" placeholder="Mobile No." runat="server"
                                                                                                        CssClass="form-control-blue" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                                                                                    <div class="text-box-msg">
                                                                                                    </div>


                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12   mgbt-xs-9">
                                                                                        <label class="control-label">Office Address</label>
                                                                                        <div class="">
                                                                                            <asp:TextBox ID="txtoffaddfa" runat="server" TextMode="MultiLine"
                                                                                                Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-4 mgbt-xs-15" style="display: none">
                                                                                    <label class="control-label">Office Phone No.</label>
                                                                                    <div class="vd_input-wrapper controls ">

                                                                                        <span class="menu-icon"><i class="fa  fa-phone"></i></span>
                                                                                        <asp:UpdatePanel ID="UpdatePanel51" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="txtFatherOfficePhoneNo" runat="server"
                                                                                                    CssClass="form-control-blue "></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-12 no-padding " style="display: none">
                                                                                    <div class="col-md-6 " style="display: none">
                                                                                        <label class="control-label">Office Mobile No.</label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa fa-mobile"></i></span>

                                                                                            <asp:UpdatePanel ID="UpdatePanel52" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtFatherOfficeMobileNo"
                                                                                                        runat="server" CssClass="form-control-blue " MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>

                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-md-6 mgbt-xs-15" style="display: none">
                                                                                        <label class="control-label">Office E-mail</label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa  fa-envelope"></i></span>

                                                                                            <asp:UpdatePanel ID="UpdatePanel53" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtFatherOfficeEmail" runat="server"
                                                                                                        CssClass="form-control-blue" onBlur="ValidateEmails(this);"></asp:TextBox>
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

                                                        <div class="col-sm-6  full-width-100">
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
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Mother's Name&nbsp;<span class="vd_red">*</span></label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                                        <asp:TextBox ID="txtmotherNameeee" runat="server" CssClass="form-control-blue  validatetxt "
                                                                                            onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtGuardiantwoName',
                                                                                        '#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpGuardiantwoRelationship','Mother');"></asp:TextBox>
                                                                                        <div class="text-box-msg">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Occupation&nbsp;<span class="vd_red">*</span></label>
                                                                                    <div class="">
                                                                                        <asp:UpdatePanel ID="UpdatePanel31" runat="server" EnableViewState="true" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="drpOccupationmoth"
                                                                                                    runat="server" AutoPostBack="True"
                                                                                                    OnSelectedIndexChanged="drpOccupationmoth_SelectedIndexChanged"
                                                                                                    CssClass="form-control-blue  validatedrp">
                                                                                                </asp:DropDownList>
                                                                                                <div class="text-box-msg">
                                                                                                </div>
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
                                                                                                <asp:TextBox ID="txtdesmoth" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                                <div class="text-box-msg">
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Qualification</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <%-- <span class="menu-icon"><i class="fa  fa-lightbulb-o"></i></span>--%>
                                                                                        <%--   <asp:TextBox ID="txtqualimother" runat="server" CssClass="form-control-blue "></asp:TextBox>--%>
                                                                                        <asp:DropDownList ID="txtqualimother" runat="server" CssClass="form-control-blue "></asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Aadhaar No.&nbsp;<span class="vd_red" id="txtmaadharcardnored" runat="server">*</span></label>
                                                                                    <div class="">
                                                                                        <asp:TextBox ID="txtmaadharcardno" placeholder="" runat="server" CssClass="form-control-blue" 
                                                                                            MaxLength="12" onBlur="ChecktwellebDigitMobileNumber(this);"></asp:TextBox>

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
                                                                                                <asp:TextBox ID="txtincomemonthlymother" runat="server" CssClass="form-control-blue"
                                                                                                    onkeyup="AddDecimalValue(event,this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtParentTotalIncome',
                                                                                                '#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtincomefa');"></asp:TextBox>
                                                                                                <div class="text-box-msg">
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-4 mgbt-xs-15" style="display: none">
                                                                                    <label class="control-label">Office Phone No.</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa  fa-phone"></i></span>

                                                                                        <asp:UpdatePanel ID="UpdatePanel54" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="txtMotherOfficePhoneNo" runat="server"
                                                                                                    CssClass="form-control-blue "></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-sm-12  no-padding">
                                                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Nationality&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa  fa-flag"></i></span>
                                                                                            <asp:UpdatePanel ID="UpdatePanel84" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtMotherNationality" runat="server"
                                                                                                        CssClass="form-control-blue  validatetxt "></asp:TextBox>
                                                                                                    <div class="text-box-msg">
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-sm-6  half-width-50 ">
                                                                                        <div class="col-sm-12  no-padding mgbt-xs-15">
                                                                                            <label class="control-label">Email &nbsp;</label>
                                                                                            <div class="vd_input-wrapper controls ">
                                                                                                <span class="menu-icon"><i class="fa  fa-envelope"></i></span>

                                                                                                <asp:TextBox ID="txtmotheremail" runat="server" CssClass="form-control-blue "
                                                                                                    onBlur="ValidateEmails(this);">
                                                                                                </asp:TextBox>
                                                                                                <div class="text-box-msg">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                                            <asp:CheckBox ID="chkMoEmail" runat="server" Text="Tick for Email Alert" Visible="false"
                                                                                                CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="Flow" />
                                                                                            <div class="text-box-msg">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12  half-width-50 ">
                                                                                        <div class="col-sm-12  no-padding mgbt-xs-15" style="margin-bottom: 3px !important;">
                                                                                            <label class="control-label">
                                                                                                Mobile No.&nbsp;<span class="vd_red">*</span>
                                                                                            </label>
                                                                                            <div class="vd_input-wrapper controls ">
                                                                                                <span class="menu-icon"><i class="fa  fa-phone"></i></span>

                                                                                                <asp:TextBox ID="txtmothercontact" runat="server"
                                                                                                    CssClass="form-control-blue  validatetxt " MaxLength="10"
                                                                                                    onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtGuardiantwoContact',
                                                                                            '#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpGuardiantwoRelationship','Mother'); ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                                                                                <div class="text-box-msg">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                                            <asp:CheckBox ID="chkMoMobile" runat="server" Text="Tick for SMS Alert" Visible="false"
                                                                                                CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="Flow" />
                                                                                            <div class="text-box-msg">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12   mgbt-xs-9">
                                                                                        <label class="control-label">Office Address</label>
                                                                                        <div class="">
                                                                                            <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtofficeaddmother" runat="server" AutoPostBack="True"
                                                                                                        TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                                                                                    <div class="text-box-msg">
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
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
                                                                                                    <asp:TextBox ID="txtMotherOfficeMobileNo" runat="server"
                                                                                                        CssClass="form-control-blue" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>

                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-md-6 mgbt-xs-15" style="display: none">
                                                                                        <label class="control-label">Office E-mail</label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa  fa-envelope"></i></span>

                                                                                            <asp:UpdatePanel ID="UpdatePanel56" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtMotherOfficeEmail" runat="server"
                                                                                                        CssClass="form-control-blue" onBlur="ValidateEmails(this);"></asp:TextBox>
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
                                                                                <div class="col-sm-12  mgbt-xs-9 ">
                                                                                    <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>
                                                                                    <div class="">
                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="txtPreaddress" placeholder="Please don't write State and City name here" runat="server"
                                                                                                    TextMode="MultiLine" Rows="2" CssClass="form-control-blue  validatetxt "></asp:TextBox>
                                                                                                <div class="text-box-msg">
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
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

                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                                                                    <div class="">
                                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="DrpPreState" runat="server" AutoPostBack="True"
                                                                                                    CssClass="form-control-blue " OnSelectedIndexChanged="DrpPreState_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                                <div class="text-box-msg">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                                                                        ControlToValidate="DrpPreState" ErrorMessage="" SetFocusOnError="True"
                                                                                                        Style="color: #CC3300" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                                                    <div class="">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="DrpPreCity" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="DrpPreCity_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                                <div class="text-box-msg">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                                                                                        runat="server" ControlToValidate="DrpPreCity" ErrorMessage="*"
                                                                                                        SetFocusOnError="True" Style="color: #CC3300" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Pin Code</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-road"></i></span>
                                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="txtPreZip" placeholder="Pin Code" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                                <div class="text-box-msg">
                                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                                                                        CssClass="imp" runat="server" ControlToValidate="txtPreZip"
                                                                                                        ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[0-9]{6,6}$"
                                                                                                        ValidationGroup="A"></asp:RegularExpressionValidator>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
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
                                                                        <%--  <asp:UpdatePanel ID="UpdatePanel65" runat="server">
                                                                <ContentTemplate>--%>

                                                                        <%--   <div class="col-sm-12   mgbt-xs-10">
                                                                        <div class="">
                                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:CheckBoxList ID="CheckBox1" runat="server" AutoPostBack="True"
                                                                                        OnSelectedIndexChanged="CheckBox1_CheckedChanged"
                                                                                        TextAlign="Right" CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                        <asp:ListItem>Same as Present Address</asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>--%>

                                                                        <div class="col-sm-12   mgbt-xs-9">
                                                                            <asp:UpdatePanel ID="UpdatePanel65" runat="server">
                                                                                <ContentTemplate>
                                                                                    <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>
                                                                                    <asp:CheckBox ID="chkCopy" runat="server" TextAlign="Right"
                                                                                        OnCheckedChanged="chkCopy_CheckedChanged" AutoPostBack="true" CssClass="vd_checkbox checkbox-success" Text="&nbsp;Same as Present Address" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <div class="">
                                                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:TextBox ID="txtPerAdd" placeholder="Please don't write State and City name here"
                                                                                            runat="server" TextMode="MultiLine" Rows="2"
                                                                                            CssClass="form-control-blue  validatetxt "></asp:TextBox>
                                                                                        <div class="text-box-msg">
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-6  half-width-50 mgbt-xs-15">
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
                                                                        <div class="col-sm-6  half-width-50 mgbt-xs-15">
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
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                                                                ControlToValidate="DrpPerState" ErrorMessage="*" SetFocusOnError="True"
                                                                                                Style="color: #CC3300" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>

                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                                            <div class=" ">
                                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="DrpPerCity" runat="server" AutoPostBack="True"
                                                                                            onchange="ValidatorUpdateDisplay(ContentPlaceHolder1_ContentPlaceHolderMainBox_RequiredFieldValidator13)"
                                                                                            CssClass="form-control-blue">
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="A"
                                                                                                ControlToValidate="DrpPerCity" ErrorMessage="*" SetFocusOnError="True" Style="color: #CC3300"
                                                                                                InitialValue="<--Select-->"></asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                            <label class="control-label">Pin Code</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-road"></i></span>
                                                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:TextBox ID="txtPerZip" runat="server" CssClass="form-control-blue"></asp:TextBox>
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
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                                                            <label class="control-label">Phone No.</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-phone"></i></span>
                                                                                <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:TextBox ID="txtPerPhoneNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg">
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                                                            <label class="control-label">Mobile No.</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-mobile"></i></span>
                                                                                <asp:UpdatePanel ID="UpdatePanel50" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:TextBox ID="txtPerMobileNo" runat="server" CssClass="form-control-blue" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                                                                        <div class="text-box-msg">
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>



                                                                        <%--                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                                        --%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- Panel Widget -->
                                                        </div>
                                                    </div>

                                                    <div class="row mgbt-xs-20">
                                                        <div class="col-sm-12 ">

                                                            <div class="col-sm-6 " style="padding-left: 0;">
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
                                                                                        <label class="control-label">Relationship&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:DropDownList ID="DrpRelationship" runat="server" CssClass="form-control-blue  validatedrp"
                                                                                                AutoPostBack="True"
                                                                                                OnSelectedIndexChanged="DrpRelationship_SelectedIndexChanged">
                                                                                                <asp:ListItem Text="<-- Select-->"></asp:ListItem>
                                                                                                <asp:ListItem Text="Father"></asp:ListItem>
                                                                                                <asp:ListItem Text="Mother"></asp:ListItem>
                                                                                                <asp:ListItem Text="Grand Father"></asp:ListItem>
                                                                                                <asp:ListItem Text="Grand Mother"></asp:ListItem>
                                                                                                <asp:ListItem Text="Brother"></asp:ListItem>
                                                                                                <asp:ListItem Text="Sister"></asp:ListItem>
                                                                                                <asp:ListItem Text="Uncle"></asp:ListItem>
                                                                                                <asp:ListItem Text="Aunty"></asp:ListItem>
                                                                                                <asp:ListItem Text="Others"></asp:ListItem>
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
                                                                                                    <asp:TextBox ID="txtguardianname" runat="server"
                                                                                                        CssClass="form-control-blue  validatetxt "></asp:TextBox>
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



                                                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15  hide">
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

                                                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15  hide">
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

                                                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15 hide">
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

                                                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15 hide">
                                                                                        <label class="control-label">Pin Code</label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa fa-road"></i></span>

                                                                                            <asp:UpdatePanel ID="UpdatePanel82" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <%--    <asp:TextBox ID="txtG1Pin"  runat="server"
                                                                                                        CssClass="form-control-blue"
                                                                                                        onchange="ValidatorUpdateDisplay
                                                                                                (ContentPlaceHolder1_ContentPlaceHolderMainBox_RegularExpressionValidator8)"></asp:TextBox>
                                                                                                    <div class="text-box-msg">
                                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8"
                                                                                                            CssClass="imp" runat="server" ControlToValidate="txtG1Pin"
                                                                                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[0-9]{6,6}$"
                                                                                                            ValidationGroup="A" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                                                                    </div>--%>
                                                                                                    <asp:TextBox ID="txtG1Pin" runat="server" CssClass="form-control-blue"></asp:TextBox>
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
                                                                                                        <asp:TextBox ID="txtemailfamily" runat="server"
                                                                                                            CssClass="form-control-blue" onBlur="ValidateEmails(this);"></asp:TextBox>
                                                                                                        <div class="text-box-msg"></div>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12  no-padding mgbt-xs-5">
                                                                                            <asp:CheckBox ID="chkGuaEmail" runat="server" Text="Tick for Email Alert" Visible="false"
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
                                                                                                            CssClass=" form-control-blue  validatetxt " MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                                                                                        <div class="text-box-msg"></div>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-12  no-padding mgbt-xs-5">
                                                                                            <asp:CheckBox ID="chkGuaMobile" runat="server" Text="Tick for SMS Alert" Visible="false"
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
                                                                                                        CssClass="vd_checkbox checkbox-success" TextAlign="Right"
                                                                                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                                        <asp:ListItem>Same as Student Present Address</asp:ListItem>
                                                                                                    </asp:CheckBoxList>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12   mgbt-xs-15">
                                                                                        <label class="col-sm-12  no-padding control-label">Parent's Total Income</label>
                                                                                        <div class="col-sm-12  vd_input-wrapper no-padding ">
                                                                                            <span class="menu-icon"><i class="fa  fa-money"></i></span>
                                                                                            <asp:UpdatePanel ID="UpdatePanel90" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtParentTotalIncome" placeholder="Annual Income" runat="server"
                                                                                                        CssClass="form-control-blue " onkeyup="CheckDecimalValue(event,this);"></asp:TextBox>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12  hide">
                                                                                        <label class="control-label">Local Guardian Address</label>
                                                                                        <div class="">
                                                                                            <asp:UpdatePanel ID="UpdatePanel77" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtG1Address" runat="server"
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
                                                                                                        CssClass="form-control-blue" MaxLength="10"></asp:TextBox>
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
                                                                                                    <asp:TextBox ID="txtG1MobileNo" runat="server" CssClass="form-control-blue" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>

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
                                                                                            <asp:ListItem Text="Father"></asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Text="Mother"></asp:ListItem>
                                                                                            <asp:ListItem Text="Grand Father"></asp:ListItem>
                                                                                            <asp:ListItem Text="Grand Mother"></asp:ListItem>
                                                                                            <asp:ListItem Text="Brother"></asp:ListItem>
                                                                                            <asp:ListItem Text="Sister"></asp:ListItem>
                                                                                            <asp:ListItem Text="Uncle"></asp:ListItem>
                                                                                            <asp:ListItem Text="Aunty"></asp:ListItem>
                                                                                            <asp:ListItem Text="Others"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>

                                                                                </div>
                                                                                <div class="col-md-4 col-sm-6 mgbt-xs-15">
                                                                                    <label class="control-label">Name&nbsp;<span class="vd_red">*</span></label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="txtGuardiantwoName" runat="server" CssClass="form-control-blue"></asp:TextBox>
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
                                                                                                <asp:TextBox ID="txtGuardiantwoEmail" runat="server" CssClass="form-control-blue" onBlur="ValidateEmails(this);"></asp:TextBox>
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
                                                                                                <asp:TextBox ID="txtG2PhoneNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
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
                                                                                                <asp:TextBox ID="txtG2MobileNo" runat="server" CssClass="form-control-blue" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>

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
                                                                                                <%-- <asp:TextBox ID="txtG2Pin"  runat="server" CssClass="form-control-blue"
                                                                                                    onchange="ValidatorUpdateDisplay(ContentPlaceHolder1_ContentPlaceHolderMainBox_RegularExpressionValidator8)">

                                                                                                </asp:TextBox>
                                                                                                <asp:RegularExpressionValidator ID="G2RegularExpressionValidator8"
                                                                                                    CssClass="imp" runat="server" ControlToValidate="txtG2Pin" ErrorMessage="*"
                                                                                                    SetFocusOnError="True" ValidationExpression="^[0-9]{6,6}$"
                                                                                                    ValidationGroup="A" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                                                                                <asp:TextBox ID="txtG2Pin" runat="server" CssClass="form-control-blue"></asp:TextBox>
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



                                                <div class="tab-pane" id="posts-tab2">
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

                                                                                <div class="row" style="margin: 0;">
                                                                                    <div class="col-sm-4  half-width-50 ">
                                                                                        <label class="control-label">Date of Admission&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                            <asp:TextBox ID="TextBox100" runat="server"
                                                                                                CssClass="form-control-blue datepicker-normal  validatetxt " onblur="dateformatvalidate(this)"></asp:TextBox>
                                                                                            <div style="font-size: 10px; color: red;">Ex.: 01-Apr-2021</div>

                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Course&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:UpdatePanel ID="UpdatePanel59" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="DropCourse" runat="server"
                                                                                                        CausesValidation="True" CssClass="form-control-blue  validatedrp"
                                                                                                        AutoPostBack="True" OnSelectedIndexChanged="DropCourse_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                    <div class="text-box-msg">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15"
                                                                                                            runat="server" ControlToValidate="DropCourse" ErrorMessage="*"
                                                                                                            SetFocusOnError="True" Style="color: #CC0000; font-weight: 700"
                                                                                                            ValidationGroup="A" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="DropAdmissionClass" runat="server" AutoPostBack="True"
                                                                                                        CssClass="form-control-blue  validatedrp"
                                                                                                        OnSelectedIndexChanged="DropAdmissionClass_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                    <div class="text-box-msg">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                                                            ControlToValidate="DropAdmissionClass" ErrorMessage="*"
                                                                                                            SetFocusOnError="True" Style="color: #CC0000; font-weight: 700"
                                                                                                            ValidationGroup="A" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row" style="margin: 0;">
                                                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:UpdatePanel ID="UpdatePanel58" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="DropSection" runat="server" CausesValidation="false"
                                                                                                        CssClass="form-control-blue" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" AutoPostBack="true">
                                                                                                    </asp:DropDownList>
                                                                                                    <div class="text-box-msg"></div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="DropBranch" runat="server" CausesValidation="True"
                                                                                                        CssClass="form-control-blue  validatedrp"
                                                                                                        OnSelectedIndexChanged="DropBranch_SelectedIndexChanged"
                                                                                                        AutoPostBack="true">
                                                                                                    </asp:DropDownList>
                                                                                                    <div class="text-box-msg">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                                                                                            runat="server" ControlToValidate="DropBranch" ErrorMessage="*"
                                                                                                            SetFocusOnError="True" Style="color: #CC0000; font-weight: 700"
                                                                                                            ValidationGroup="A" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Group&nbsp;<span class="vd_red"></span></label>
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
                                                                                </div>
                                                                                <div class="row" style="margin: 0;">


                                                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="drpMedium" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                                                    <div class="text-box-msg">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                                                                                            ControlToValidate="drpMedium" ErrorMessage="*" SetFocusOnError="True"
                                                                                                            Style="color: #CC0000; font-weight: 700" ValidationGroup="A"
                                                                                                            InitialValue="<--Select-->"></asp:RequiredFieldValidator>
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
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="DrpBoard"
                                                                                                            ErrorMessage="*" SetFocusOnError="True" Style="color: #CC0000; font-weight: 700"
                                                                                                            ValidationGroup="A" InitialValue="<--Select-->"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>

                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Type of Admission&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:DropDownList ID="DrpNEWOLSAdmission" runat="server" CssClass="form-control-blue" Enabled="false">
                                                                                                <asp:ListItem Selected="True">NEW</asp:ListItem>
                                                                                                <asp:ListItem>OLD</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row" style="margin: 0;">
                                                                                    <div class=" col-sm-4  half-width-50 mgbt-xs-15 ">
                                                                                        <label class=" control-label">Type of Education&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:DropDownList ID="RadioButtonList3"
                                                                                                runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                                                                <asp:ListItem Value="Yes" Selected="True"> Regular </asp:ListItem>
                                                                                                <asp:ListItem Value="No"> Private </asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">APAAR ID&nbsp;<span class="vd_red" id="txtAparidred" runat="server">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:TextBox ID="txtAparid" placeholder="" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnTextChanged="txtAparid_TextChanged"></asp:TextBox>

                                                                                            <div class="text-box-msg">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Booklet/Form No.&nbsp;<span class="vd_red" id="txtProspectusred" runat="server">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:TextBox ID="txtProspectus" placeholder="" runat="server" CssClass="form-control-blue" OnTextChanged="txtProspectus_TextChanged" AutoPostBack="true"></asp:TextBox>

                                                                                            <div class="text-box-msg">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>

                                                                        <%--<asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="col-md-12" id="divopt" runat="server" visible="false">
                                                                                    <div class="vd_input-wrapper ">
                                                                                        <label class="col-sm-3 no-padding control-label">Optional Subject</label>
                                                                                        <div class="col-sm-9 mgbt-xs-15 no-padding controls">

                                                                                            <asp:CheckBoxList ID="rbOptionalSubject" runat="server" RepeatDirection="Horizontal"
                                                                                                RepeatLayout="Flow" CssClass="vd_checkbox checkbox-success">
                                                                                            </asp:CheckBoxList>

                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>--%>

                                                                        <asp:UpdatePanel ID="UpdatePanel83" runat="server">
                                                                            <ContentTemplate>



                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15 " style="display: none">
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
                                                                                            <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                                            <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15 " style="display: none">
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
                                                                                            <%-- <asp:ListItem><-- Select Mode of Fee Deposit --></asp:ListItem>--%>
                                                                                            <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                                            <%--<asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                                                    <asp:ListItem Value="S">Semester</asp:ListItem>--%>
                                                                                            <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15 " style="display: none">
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
                                                                                            <%-- <asp:ListItem><-- Select Mode of Fee Deposit --></asp:ListItem>--%>
                                                                                            <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                                            <%-- <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                                                    <asp:ListItem Value="S">Semester</asp:ListItem>--%>
                                                                                            <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divScholarship" visible="false">
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

                                                                                <div class="row" style="margin: 0;">
                                                                                    <div class="col-sm-4 mgbt-xs-15" style="display: none">
                                                                                        <label class="control-label">Enquiry No. </label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                            <asp:TextBox ID="txtEnquiryNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4 mgbt-xs-15">
                                                                                        <label class="control-label">S.R. No.&nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                            <asp:TextBox ID="txtSr" runat="server" CssClass="form-control-blue  validatetxt" Enabled="false"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4 mgbt-xs-15">
                                                                                        <label class="control-label">Fee Category &nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="drpPanelCardType" runat="server"
                                                                                                        CssClass="form-control-blue  " OnSelectedIndexChanged="drpPanelCardType_SelectedIndexChanged" AutoPostBack="true">
                                                                                                    </asp:DropDownList>
                                                                                                    <div class="text-box-msg">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                                                                                            ControlToValidate="drpPanelCardType" ErrorMessage="*"
                                                                                                            SetFocusOnError="True" Style="color: #CC0000; font-weight: 700"
                                                                                                            ValidationGroup="A" InitialValue="<--Select-->"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-sm-4 mgbt-xs-15">
                                                                                        <label class="control-label">House Name &nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="DropDownList4" runat="server"
                                                                                                        CssClass="form-control-blue">
                                                                                                    </asp:DropDownList>
                                                                                                    <div class="text-box-msg">
                                                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
                                                                                                        ControlToValidate="DropDownList4" ErrorMessage="*" SetFocusOnError="True"
                                                                                                        Style="color: #CC0000; font-weight: 700" ValidationGroup="A"
                                                                                                        InitialValue="<--Select-->"></asp:RequiredFieldValidator>--%>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row" style="margin: 0;">
                                                                                    <div class="col-sm-4 mgbt-xs-15" style="display: none">
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


                                                                                </div>
                                                                                <div class="row" style="margin: 0;">

                                                                                    <div class="col-sm-4 mgbt-xs-15">
                                                                                        <label class="control-label">Shift &nbsp;<span class="vd_red"></span></label>
                                                                                        <div class="">
                                                                                            <asp:UpdatePanel ID="UpdatePanel64" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control-blue">
                                                                                                    </asp:DropDownList>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-sm-4 mgbt-xs-15">
                                                                                        <label class="control-label">Student Machine ID</label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                            <asp:TextBox ID="txtCardNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4 mgbt-xs-15">
                                                                                        <label class="control-label">Machine No.</label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <asp:DropDownList runat="server" ID="ddlMachineNo" CssClass="form-control-blue"></asp:DropDownList>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                                <div class="row" style="margin: 0;">
                                                                                    <div class="col-sm-4 mgbt-xs-15">
                                                                                        <label class="control-label">Education Act &nbsp;<span class="vd_red"></span></label>
                                                                                        <div class="">
                                                                                            <asp:UpdatePanel ID="UpdatePanel69" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="ddlEducationAct" runat="server" CssClass="form-control-blue">
                                                                                                    </asp:DropDownList>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4 mgbt-xs-15">
                                                                                        <label class="control-label" style="font-size:12px">Board/ University Roll No.</label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                            <asp:TextBox ID="txtUniversityBoardRollNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4 mgbt-xs-15">
                                                                                        <label class="control-label">Institute Roll No. </label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                            <asp:TextBox ID="txtSchoolcollegeRollno" runat="server" CssClass="form-control-blue" onBlur="ChecktenDigitNumber(this);"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row" style="margin: 0;">

                                                                                    <div class="col-sm-4 mgbt-xs-15">
                                                                                        <label class="control-label">File No. </label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa  fa-hand-o-right"></i></span>
                                                                                            <asp:TextBox ID="txtfileno" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4 mgbt-xs-15">
                                                                                        <label class="control-label">Remark </label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                                            <asp:TextBox ID="txtrema" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-sm-4 mgbt-xs-15" style="display: none">
                                                                                        <label class="control-label">Reference </label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa  fa-hand-o-right"></i></span>
                                                                                            <asp:TextBox ID="txtReferences" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                                <div class="row" style="margin: 0;">
                                                                                    <div class="col-sm-4 mgbt-xs-15" style="display: none">
                                                                                        <label class="control-label">Select SMS Acknowledgment To &nbsp;<span class="vd_red">*</span> </label>
                                                                                        <div class="">
                                                                                            <asp:DropDownList ID="drpSMSAcknowledgmentTo" runat="server"
                                                                                                CssClass="form-control-blue">
                                                                                                <asp:ListItem Text="<-- Select SMS Acknowledgment To -->" Value=""></asp:ListItem>
                                                                                                <asp:ListItem Text="Gaurdian 1"></asp:ListItem>
                                                                                                <asp:ListItem Text="Gaurdian 2"></asp:ListItem>
                                                                                                <asp:ListItem Text="Both"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4 mgbt-xs-15" style="display: none">
                                                                                        <label class="control-label">Select E-mail Acknowledgment To &nbsp;<span class="vd_red">*</span></label>
                                                                                        <div class="">
                                                                                            <asp:DropDownList ID="drpEmailAcknowledgmentTo" runat="server"
                                                                                                CssClass="form-control-blue">
                                                                                                <asp:ListItem Text="<-- Select E-mail Acknowledgment To -->"></asp:ListItem>
                                                                                                <asp:ListItem Text="Gaurdian 1"></asp:ListItem>
                                                                                                <asp:ListItem Text="Gaurdian 2"></asp:ListItem>
                                                                                                <asp:ListItem Text="Both"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-sm-4 mgbt-xs-15" runat="server" id="divAdmissiondoneat">
                                                                                        <label class="control-label">Admission done at </label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                                            <asp:TextBox ID="txtAddDoneat" runat="server" CssClass="form-control-blue" placeholder="Amount"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
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
                                                        <div class="col-sm-12  full-width-100 hide">
                                                            <div class="col-sm-6  full-width-100" style="padding-left: 0px;">
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
                                                                                            <asp:TextBox ID="txtDFA" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Course of First Admission</label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                            <asp:TextBox ID="txtCFA" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Class of First Admission</label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                            <asp:TextBox ID="txtCOFA" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                            <div class="text-box-msg"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                        <label class="control-label">Branch of First Admission</label>
                                                                                        <div class="vd_input-wrapper controls ">
                                                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                            <asp:TextBox ID="txtSFA" runat="server" CssClass="form-control-blue"></asp:TextBox>
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
                                                </div>

                                                <div class="tab-pane" id="list-tab2">

                                                    <div class="row mgbt-xs-20">

                                                        <div class="col-sm-12 ">
                                                            <div class="panel widget">
                                                                <div class="panel-heading vd_bg-dark-blue">
                                                                    <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-file-archive-o"></i></span>&nbsp; Photos & Documents</h3>
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
                                                                                <div class="col-sm-12 " style="border-bottom: 1px solid #000; margin-bottom: 10px;">
                                                                                    <div class="col-sm-6  no-padding">
                                                                                        <div class="col-sm-8  mgbt-xs-15">
                                                                                            <label class="control-label">Student's Photo</label>
                                                                                            <div class="controls img-input-ped ">
                                                                                                <asp:FileUpload ID="avatarUpload" runat="server"
                                                                                                    onchange="checksFileSizeandFileTypeinupdatePanel(this, 100000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','Avatars',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdStPhoto');"
                                                                                                    type="file" CssClass="form-control-blue " />

                                                                                            </div>
                                                                                            <div class="col-sm-12  no-padding ">
                                                                                                <fieldset>
                                                                                                    <legend class="legend-me">Live Camera&nbsp;<span class="vd_red"></span>
                                                                                                    </legend>
                                                                                                    <div class=" col-sm-12  no-padding">
                                                                                                        <div id="webcam" class="webcam-object img-responsive img-thumbnail">
                                                                                                        </div>
                                                                                                        <div class=" col-sm-12  no-padding">
                                                                                                            <a onclick="take_snapshot();return false;" id="sdsd1" 
                                                                                                                class="pull-top btn-click" style="cursor: pointer;">
                                                                                                                <i class="fa fa-camera"></i>&nbsp; Capture Photo</a>
                                                                                                            <span class="vd_red" id="webcam_error" style="display:none">Webcam not detected!</span>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </fieldset>
                                                                                                <span id="Span1" style="display: none"></span>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-4  mgbt-xs-10">
                                                                                            <div class="stu-pic-box" style="width: 120px; height: 140px;">
                                                                                                <div class="stu-pic-box-main" style="width: 103px; height: 123px;">
                                                                                                    <asp:Image alt="" ID="Avatar" class="Avatars" runat="server" ImageUrl="../img/user-pic/student-pic.png" Style="width: 103px; height: 123px;" />
                                                                                                    <asp:HiddenField ID="hdStPhoto" runat="server" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>


                                                                                        <div class="col-sm-8  mgbt-xs-15">
                                                                                            <label class="control-label">Father's Photo</label>
                                                                                            <div class="vd_input-wrapper controls img-input-ped ">
                                                                                                <asp:Label ID="lblFatherImageUrl" runat="server" Visible="False"></asp:Label>
                                                                                                <asp:Label ID="lblFatherImageName" runat="server" Visible="False"></asp:Label>
                                                                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control-blue" runat="server"
                                                                                                    onchange="checksFileSizeandFileTypeinupdatePanel(this, 100000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','imgFather',
                                                                                    'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdfatherPhoto');"
                                                                                                    type="file" />

                                                                                            </div>

                                                                                        </div>
                                                                                        <div class="col-sm-4  mgbt-xs-15">

                                                                                            <div class="vd_input-wrapper controls ">
                                                                                                <div class="stu-pic-box" style="width: 120px; height: 140px;">
                                                                                                    <div class="stu-pic-box-main" style="width: 103px; height: 123px;">
                                                                                                        <asp:Image ID="imgFather" runat="server" class="imgFather"
                                                                                                            ImageUrl="../img/user-pic/student-pic.png" alt="sds" Style="width: 103px; height: 123px;" />
                                                                                                        <asp:HiddenField ID="hdfatherPhoto" runat="server" />
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </div>
                                                                                        <div class="col-sm-8  mgbt-xs-15">
                                                                                            <label class="control-label">Mother's Photo</label>
                                                                                            <div class="vd_input-wrapper controls img-input-ped ">
                                                                                                <asp:FileUpload ID="FileUpload2" runat="server"
                                                                                                    CssClass="form-control-blue" onchange="checksFileSizeandFileTypeinupdatePanel(this, 100000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','imgMother',
                                                                                    'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdmotherPhoto');"
                                                                                                    type="file" />
                                                                                            </div>

                                                                                        </div>
                                                                                        <div class="col-sm-4  mgbt-xs-15">
                                                                                            <div class="stu-pic-box" style="width: 120px; height: 140px;">
                                                                                                <div class="stu-pic-box-main" style="width: 103px; height: 123px;">
                                                                                                    <asp:Label ID="lblMotherImageUrl" runat="server" Visible="False"></asp:Label>
                                                                                                    <asp:Label ID="lblMotherImageName" runat="server" Visible="False"></asp:Label>
                                                                                                    <asp:Image ID="imgMother" ImageUrl="../img/user-pic/student-f-pic.png" runat="server" CssClass="imgMother" Style="width: 103px; height: 123px;" />
                                                                                                    <asp:HiddenField ID="hdmotherPhoto" runat="server" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>


                                                                                    </div>
                                                                                    <div class="col-sm-6 ">

                                                                                        <div class="col-sm-12 ">
                                                                                            <label class="control-label">Upload Group Photo</label>
                                                                                            <asp:FileUpload ID="FileUpload3" runat="server"
                                                                                                CssClass="form-control-blue "
                                                                                                onchange="checksFileSizeandFileTypeinupdatePanel(this, 200000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','imgGroupPhoto',
                                                                                    'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdbase64groupPhoto');"
                                                                                                type="file" />
                                                                                        </div>
                                                                                        <div class="col-sm-12 ">
                                                                                            <div class="group-pic-box" style="margin-top: 10px;">
                                                                                                <div class="group-pic-box-main">
                                                                                                    <asp:Label ID="lblGroupImageName" runat="server" Visible="False"></asp:Label>
                                                                                                    <asp:Label ID="lblGroupImageUrl" runat="server" Visible="False"></asp:Label>
                                                                                                    <asp:Image ID="imgGroupPhoto" runat="server" CssClass="imgGroupPhoto" ImageUrl="../img/user-pic/group-photo.jpg" />
                                                                                                    <asp:HiddenField ID="hdbase64groupPhoto" runat="server" />

                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                                <asp:Repeater ID="Repeater1" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-sm-12  no-padding ">
                                                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                                <asp:Label ID="lblDocument" class="control-label" runat="server" Text='<%# Bind("DocumentType") %>'></asp:Label>
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
                                                                                                    <asp:HyperLink runat="server" ID="doc_link" download="download"></asp:HyperLink>
                                                                                                    <div class="text-box-msg">

                                                                                                        <asp:HiddenField ID="hfFile" runat="server" />
                                                                                                        <asp:HiddenField ID="hdfilefileExtention" runat="server" />
                                                                                                    </div>
                                                                                                </div>

                                                                                            </div>

                                                                                            <div class="col-sm-4  half-width-50 btn-a-devices-1-p4-p2 mgbt-xs-15 ">
                                                                                                <div class="col-sm-4 col-xs-6 mgtp-6">

                                                                                                    <asp:CheckBox ID="lblSoftcopy" runat="server" CssClass="vd_checkbox checkbox-success"
                                                                                                        TextAlign="Right" Text="Soft Copy" />

                                                                                                </div>
                                                                                                <div class="col-sm-4 col-xs-6 mgtp-6">
                                                                                                    <asp:CheckBox ID="lblHardcopy" runat="server" class="vd_checkbox checkbox-success"
                                                                                                        TextAlign="Right" Text="Hard Copy" />
                                                                                                </div>
                                                                                                <div class="col-sm-4 col-xs-6 mgtp-6">

                                                                                                    <asp:CheckBox ID="chkVerified" runat="server" Text="Verified"
                                                                                                        CssClass="vd_checkbox checkbox-success" TextAlign="Right" />

                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                                                                <label class="control-label">Remark</label>
                                                                                                <div class="vd_input-wrapper ">

                                                                                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"
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

                                                <div class="tab-pane" id="list-tab">
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

                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Name of Entrance</label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList onchange="displayTextbox();" ID="drpExamCrackedof" runat="server"
                                                                                            CssClass="form-control-blue ">
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Roll No.</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                                                        <asp:TextBox ID="txtRollNo1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Rank</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                                                        <asp:TextBox ID="txtRank" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Category Rank</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                                                        <asp:TextBox ID="txtCategoryRank" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
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
                                                        <div class="col-sm-6  full-width-100">
                                                            <div class="panel widget">
                                                                <div class="panel-heading vd_bg-dark-blue">
                                                                    <h3 class="panel-title"><span class="menu-icon"><i class=" icon-graduation"></i></span>Previous Institute Details
                                                                  <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
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
                                                                    <div id="rpprevious" class="row">
                                                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Board/ University</label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList ID="drpPreviousBoard" runat="server"
                                                                                            CssClass="form-control-blue ">
                                                                                            <asp:ListItem Text="<-- Select Board/ University -->"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Name of Previous Institute</label>
                                                                                    <div class="">
                                                                                        <asp:TextBox ID="txtPreviousInstituteName" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">UDISE Code</label>
                                                                                    <div class="">
                                                                                        <%--  <span class="menu-icon"><i class="fa fa-bookmark"></i></span>--%>
                                                                                        <asp:TextBox ID="txtUdiaseCode" runat="server" MaxLength="150" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Contact No.</label>
                                                                                    <div class="">

                                                                                        <asp:TextBox ID="txtContactNoPrevious" runat="server" MaxLength="10" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Previous Class</label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList ID="ddlPreviousClass" runat="server"
                                                                                            CssClass="form-control-blue ">
                                                                                        </asp:DropDownList>
                                                                                        <%-- <asp:TextBox ID="txtPreviousClass" MaxLength="150" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Address</label>
                                                                                    <div class="">
                                                                                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="150" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Previous Medium</label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList ID="ddlMediumprevious" runat="server"
                                                                                            CssClass="form-control-blue ">
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Attendance</label>
                                                                                    <div class="">
                                                                                        <asp:TextBox ID="txtAttendance" MaxLength="150" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Result</label>
                                                                                    <div class="">
                                                                                        <asp:DropDownList ID="ddlResult" runat="server"
                                                                                            CssClass="form-control-blue">
                                                                                            <asp:ListItem Value="Pass">Pass</asp:ListItem>
                                                                                            <asp:ListItem Value="Fail">Fail</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Marks Percentage</label>
                                                                                    <div class="">
                                                                                        <asp:TextBox ID="txtMarksPercentage" MaxLength="150" runat="server" CssClass="form-control-blue"></asp:TextBox>
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
                                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server" Visible="false">
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
                                                                                                <asp:DropDownList ID="drpBoardPrevious" runat="server"
                                                                                                    CssClass="form-control-blue " Enabled="false">
                                                                                                    <asp:ListItem Text="<-- Select Board/ University -->"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                                <div class="text-box-msg"></div>
                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                            <label class="control-label">Result</label>
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
                                                                                            <label class="control-label">Medium</label>
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


                                                                                        <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 hide ">
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


                                                        <div class="col-sm-12  text-center hide">
                                                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="lnkAddMore" OnClick="lnkAddMore_Click" CssClass="button form-control-blue" runat="server"> 
                                                                            <i class="fa fa-plus-square"></i> &nbsp; Add Education Details Box </asp:LinkButton>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="tab-pane" id="poststab3" style="display: none">

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
                                                                                        <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>

                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Registration No.</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtRegistration" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Caste Certificate No. </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtCastCerti" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Date of issue Income Certificate</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                        <asp:TextBox ID="TextBox148" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Income Certificate No. </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtIncomeCerti" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Date of issue Income Certificate</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                        <asp:TextBox ID="TextBox149" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Domicile Certificate No.  </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtRegiCer" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Date of issue Domicile Certificate </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                        <asp:TextBox ID="TextBox150" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">1st Year Admission Date</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                        <asp:TextBox ID="TextBox151" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Current Year Admission Date</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                        <asp:TextBox ID="TextBox152" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Type of Course </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-graduation-cap"></i></span>
                                                                                        <asp:TextBox ID="txtTypeofCourse" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Type of Admission </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-lightbulb-o"></i></span>
                                                                                        <asp:TextBox ID="txtTypeofAdmission" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Bank Account No.  </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtBankAccNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Bank Name </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-bank"></i></span>
                                                                                        <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Branch Name of Bank  </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-map-marker"></i></span>
                                                                                        <asp:TextBox ID="txtBranchNameofBank" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">IFS Code </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtIfsCode" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Student Name in Bank Passbook </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                                        <asp:TextBox ID="txtStudentNameinPassbook" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Day Scholar / Hosteller</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-building"></i></span>
                                                                                        <asp:TextBox ID="txtDayScholer" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Yearly None Refundeble Fee</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtYearlynonrefund" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Handicapped Type </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-wheelchair"></i></span>
                                                                                        <asp:TextBox ID="txthandycaptype" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Handicapped Percentage  </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-wheelchair"></i></span>
                                                                                        <asp:TextBox ID="txthandycapPer" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Handicapped Compensation</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-wheelchair"></i></span>
                                                                                        <asp:TextBox ID="txthandycapCompe" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Receipt No. of Deposit Fee</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtReciptNoofDepositFee" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Deposit Fee Date  </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                        <asp:TextBox ID="TextBox154" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Last Year Scholarship Amount  </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtLastYearScholarAmount" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Last Year Scholarship Deposit Fee </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtLastYearScholarDepoFee" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Last Year Class / Course</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-graduation-cap"></i></span>
                                                                                        <asp:TextBox ID="txtLastClass" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Last Year Exam Result </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-trophy"></i></span>
                                                                                        <asp:TextBox ID="txtLastYearExamResult" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Last Year Exam Total Marks  </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-thumbs-up"></i></span>
                                                                                        <asp:TextBox ID="txtLastYearExamTatalMarks" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Last Year Exam Total Obtain Marks</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-thumbs-o-up"></i></span>
                                                                                        <asp:TextBox ID="txtLastYearExamTotalObtainMarks" runat="server"
                                                                                            CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Scholarship Compensation Amount According to Class / Course  </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa  fa-graduation-cap"></i></span>
                                                                                        <asp:TextBox ID="txtScholarCompeAmountAccotoClass" runat="server"
                                                                                            CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Name of Institute</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-institution"></i></span>
                                                                                        <asp:TextBox ID="txtNameofInstitute" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Intermediate Roll No. </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtIntermediateRollNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Intermediate Board  </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-tag"></i></span>
                                                                                        <asp:TextBox ID="txtIntermediateBoard" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Intermediate Passing Year </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-certificate"></i></span>
                                                                                        <asp:TextBox ID="txtIntermediateYearofPssing" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Is Entry based on Intermediate Marks Score </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-level-up"></i></span>
                                                                                        <asp:TextBox ID="txtIsEntrybasedonInterMarksScore" runat="server"
                                                                                            CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Total Marks in Intermediate</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-thumbs-up"></i></span>
                                                                                        <asp:TextBox ID="txtTotalMarksinInter" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Total obtained Marks in Intermediate</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-thumbs-o-up"></i></span>
                                                                                        <asp:TextBox ID="txtTotalobtainedMarksinInter" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Student Aadhar No. </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtStudentAdharNo" runat="server" CssClass="form-control-blue" MaxLength="12" onBlur="copyTextValue();"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Transfer Certificate No. </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox ID="txtTransferCertiNo" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Transfer Certificate Date </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                                        <asp:TextBox ID="TextBox153" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Last School / College Name </label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-institution"></i></span>
                                                                                        <asp:TextBox ID="txtLastSchoolCollegeName" runat="server" CssClass="form-control-blue"></asp:TextBox>
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
                                                                                                        <%--<img src="ss.jpg" alt="" style="display: none;" />--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                            <div class="col-sm-12   mgbt-xs-15 no-padding">
                                                                                                <div class="stu-sign-pic-box">
                                                                                                    <div class="stu-sign-pic-box-main">
                                                                                                        <%-- <img src="sws.jpg" alt="" style="display: none;" />--%>
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
                                                                                                        <%-- ReSharper disable once Asp.Image --%>
                                                                                                        <img style="height: 261px" class="imgGroupPhoto" src="../img/user-pic/group-photo.jpg" />
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
                                                <div class="tab-pane hide" id="helthdetail">
                                                    <div class="row mgbt-xs-20">


                                                        <div class="col-sm-6  full-width-100">
                                                            <div class="panel widget">
                                                                <div class="panel-heading vd_bg-dark-blue">
                                                                    <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Father's Details </h3>
                                                                    <div class="vd_panel-menu ">
                                                                        <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon">
                                                                            <i class="icon-minus3"></i>
                                                                        </div>

                                                                    </div>
                                                                    <!-- vd_panel-menu -->

                                                                </div>
                                                                <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                                    <div class="row">
                                                                        <asp:UpdatePanel runat="server" ID="ddd">
                                                                            <ContentTemplate>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label" runat="server" id="lblAadhaar">Aadhaar No.</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox runat="server" ID="txt_f_healthAdhaar" class="form-control-blue" MaxLength="12" onBlur="ChecktwellebDigitMobileNumber(this);"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Date of Birth</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox runat="server" ID="txt_f_healthDOB" class="form-control-blue datepicker-normal" onblur="dateformatvalidate(this)"></asp:TextBox>
                                                                                        <div style="font-size: 10px; color: red;">Ex.: 01-Apr-2021</div>
                                                                                    </div>
                                                                                </div>



                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Weight</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox runat="server" ID="txt_f_healthWeight" class="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Height</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox runat="server" ID="txt_f_healthHeight" class="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Blood group</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <asp:DropDownList ID="drphealth_f_BloodGroup" runat="server" CssClass="form-control-blue" Style="text-transform: uppercase;">
                                                                                            <asp:ListItem Selected="True" Value="2">Other</asp:ListItem>
                                                                                            <asp:ListItem Value="3">B+</asp:ListItem>
                                                                                            <asp:ListItem Value="4">O+</asp:ListItem>
                                                                                            <asp:ListItem Value="5">O -</asp:ListItem>
                                                                                            <asp:ListItem Value="6">A+</asp:ListItem>
                                                                                            <asp:ListItem Value="7">A-</asp:ListItem>
                                                                                            <asp:ListItem Value="8">B-</asp:ListItem>
                                                                                            <asp:ListItem Value="9">AB+</asp:ListItem>
                                                                                            <asp:ListItem Value="10">AB-</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6  full-width-100">
                                                            <div class="panel widget">
                                                                <div class="panel-heading vd_bg-dark-blue">
                                                                    <h3 class="panel-title"><span class="menu-icon"><i class="icon-vcard"></i></span>Mother's Details </h3>
                                                                    <div class="vd_panel-menu ">
                                                                        <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon">
                                                                            <i class="icon-minus3"></i>
                                                                        </div>

                                                                    </div>
                                                                    <!-- vd_panel-menu -->

                                                                </div>
                                                                <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                                    <div class="row">
                                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel89">
                                                                            <ContentTemplate>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label" runat="server" id="lblAadhaar1">Aadhaar No.</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox runat="server" ID="txt_m_healthAdhaar" class="form-control-blue" MaxLength="12" onBlur="ChecktwellebDigitMobileNumber(this);"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Date of Birth</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox runat="server" ID="txt_m_healthDOB" class="form-control-blue datepicker-normal" onblur="dateformatvalidate(this)"></asp:TextBox>
                                                                                        <div style="font-size: 10px; color: red;">Ex.: 01-Apr-2021</div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Weight</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox runat="server" ID="txt_m_healthWeight" class="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Height</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox runat="server" ID="txt_m_healthHeight" class="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg"></div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">Blood group</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <asp:DropDownList ID="drphealth_m_BloodGroup" runat="server" CssClass="form-control-blue" Style="text-transform: uppercase;">
                                                                                            <asp:ListItem Selected="True" Value="2">Other</asp:ListItem>
                                                                                            <asp:ListItem Value="3">B+</asp:ListItem>
                                                                                            <asp:ListItem Value="4">O+</asp:ListItem>
                                                                                            <asp:ListItem Value="5">O -</asp:ListItem>
                                                                                            <asp:ListItem Value="6">A+</asp:ListItem>
                                                                                            <asp:ListItem Value="7">A-</asp:ListItem>
                                                                                            <asp:ListItem Value="8">B-</asp:ListItem>
                                                                                            <asp:ListItem Value="9">AB+</asp:ListItem>
                                                                                            <asp:ListItem Value="10">AB-</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6 col-xs-6 half-width-50 mgbt-xs-15">
                                                                                    <label class="control-label">CWSN Specify</label>
                                                                                    <div class="vd_input-wrapper controls ">
                                                                                        <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                                                        <asp:TextBox runat="server" ID="txt_m_healthCWSN" class="form-control-blue"></asp:TextBox>
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


                                            </div>
                                            <div class="row ">
                                                <div class="col-md-12 ">
                                                    <div class="btn-center-box-130">
                                                        <asp:UpdatePanel ID="UpdatePanel88" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="LinkSubmit" OnClick="LinkSubmit_Click"
                                                                    OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp'); return validationReturn();"
                                                                    ValidationGroup="A" CssClass="btn vd_btn vd_bg-blue" runat="server"><i class="fa fa-paper-plane"></i> &nbsp; Submit </asp:LinkButton>
                                                                <div id="msgbox2" runat="server" style="left: 75px;"></div>
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

                <script type="text/javascript">

                    //var pam1 = Sys.WebForms.PageRequestManager.getInstance();
                    //var pam2 = Sys.WebForms.PageRequestManager.getInstance();

                    //pam1.add_endRequest(BeginRequestHandlerUpdate);
                    //pam2.add_endRequest(BeginRequestHandlerNorm());

                    function BeginRequestHandlerUpdate() {
                        try {
                            var txtfromdate = document.getElementById('<%= txtAgeOnDate.ClientID %>').value;
                            var txttodate = document.getElementById('<%= txtStudentDOB.ClientID %>').value;
                            var fromdate = new Date(txtfromdate).format("dd/MM/yyyy");
                            var todate = new Date(txttodate).format("dd/MM/yyyy");


                            window.PageMethods.getAgeofStudent(fromdate, todate, Onsuccess);

                        }
                        catch (err) {

                            alert(err.toString());
                        }
                    }

                    function BeginRequestHandlerNorm() {
                        try {
                            var txtfromdate = document.getElementById('<%= txtAgeOnDate.ClientID %>').value;
                            var txttodate = document.getElementById('<%= txtStudentDOB.ClientID %>').value;
                            var fromdate = new Date(txtfromdate).format("dd/MM/yyyy");
                            var todate = new Date(txttodate).format("dd/MM/yyyy");

                            window.PageMethods.getAgeofStudent(fromdate, todate, Onsuccess);

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

                <script>
                    visibleFalseTableColumn1('ContentPlaceHolder1_ContentPlaceHolderMainBox_drpTransportMODB', '<%=rbTransport.ClientID %>');
                </script>

                <script type="text/javascript">
                    function finalsubmit(value) {
                        if (value != null) {
                            var textelement;
                            var i;
                            if (value === 'C') {
                                textelement = document.getElementsByTagName("input");
                                for (i = 0; i < textelement.length; i++) {
                                    if (textelement[i].type.toLowerCase() === "text") {
                                        textelement[i].value = textelement[i].value.charAt(0).toUpperCase() + textelement[i].value.substring(1, textelement[i].value.length).toLowerCase();
                                    }
                                }
                            }
                            if (value === 'U') {
                                textelement = document.getElementsByTagName("input");
                                for (i = 0; i < textelement.length; i++) {
                                    if (textelement[i].type.toLowerCase() === "text") {
                                        textelement[i].value = textelement[i].value.toUpperCase();
                                    }
                                }
                            }
                            if (value === 'L') {
                                textelement = document.getElementsByTagName("input");
                                for (i = 0; i < textelement.length; i++) {
                                    if (textelement[i].type.toLowerCase() === "text") {
                                        textelement[i].value = textelement[i].value.toLowerCase();
                                    }
                                }
                            }

                        }
                    }
                </script>
                <script>
                    function checksFileSizeandFileTypeinupdatePanel(fileupload, size, filetype, imageClass, hiddenfield) {
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
                <script src="../webcam/newWebCam/webcam.min.js"></script>
                <script language="JavaScript">

                    function captureImage() {
                        Webcam.set({
                            width: 160,
                            height: 190,
                            image_format: 'jpeg',
                            jpeg_quality: 100,
                            flip_horiz: true,
                        });
                        Webcam.attach('#webcam');
                        Webcam.on('error', function (err) {
                            $("#webcam_error").css('display', 'block');
                            console.error("Webcam Error:", err);
                            if (err.name === 'OverconstrainedError' || err.name === 'ConstraintNotSatisfiedError') {
                                console.error("Webcam cannot meet the requested resolution or settings. Try adjusting the settings or using a different camera.");
                            }
                        });
                    }

                    function take_snapshot() {
                        Webcam.snap(function (data_uri) {
                            document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_Avatar').src = data_uri;
                            document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_hdStPhoto').value = data_uri.replace(/^data:image\/[a-z]+;base64,/, "");
                        });
                    }
                </script>

                <script type="text/javascript">
                    // Function to store currently selected tab
                    function storeActiveTab() {
                        var activeTabId = $(".nav-tabs li.active a").attr("href");
                        $('#<%= hdnActiveTab.ClientID %>').val(activeTabId);
                    }

                    // Restore tab after async postback
                    function restoreActiveTab() {
                        var selectedTab = $('#<%= hdnActiveTab.ClientID %>').val();
                        if (selectedTab) {
                            $('a[href="' + selectedTab + '"]').tab('show');
                        }
                    }

                    // Call restore on page load
                    $(document).ready(function () {
                        restoreActiveTab();
                    });

                    // For UpdatePanel partial postback
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                        restoreActiveTab(); // restore after async postback
                        bindTabChange();    // re-bind tab click
                    });

                    function bindTabChange() {
                        $('a[data-toggle="tab"]').off('shown.bs.tab').on('shown.bs.tab', function (e) {
                            var activeTabId = $(e.target).attr("href");
                            $('#<%= hdnActiveTab.ClientID %>').val(activeTabId);
                        });
                    }

                    // Initial binding of tab change event
                    bindTabChange();
                </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
