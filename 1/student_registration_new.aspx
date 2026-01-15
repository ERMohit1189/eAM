<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="student_registration_new.aspx.cs" Inherits="_1.Adminstudent_registration_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function copyaddress() {
            $(document).ready(function () {
                $('input:checkbox[id*=chkCopy]').change(function () {
                    <%--if ($(this).is(':checked')) {


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
                    }--%>
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div id="loader" runat="server"></div>
    <%-- ReSharper disable once Html.TagNotClosed --%>
    <div class="vd_content-section clearfix">
        <script>
            Sys.Application.add_load(copyaddress);
        </script>
        <%-- ReSharper disable once Html.TagNotClosed --%>
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">


                        <div class="col-sm-12  ">
                            <div class="col-sm-4   no-padding mgbt-xs-15">
                                <div class="vd_input-wrapper">
                                    <span class="menu-icon"><i class="fa fa-eye"></i></span>
                                    <input type="text" id="txt_enquiryNo" class="form-control-blue" placeholder="Enter Student Enquiry No." />
                                </div>

                            </div>
                            <div class="col-sm-8  no-padding">
                                <div class="col-xs-6 no-padding text-left mgbt-xs-15">
                                    <button type="button" id="btn_enquiry" class="button form-control-blue">View</button>
                                    <%--<asp:LinkButton ID="LinkButton1" runat="server" class="button form-control-blue" OnClick="LinkButton1_Click"> View</asp:LinkButton>--%>
                                </div>
                                <div class="col-xs-6 no-padding" style="display: none;">
                                    <div class=" pull-left">
                                        <button type="button" id="btn_previous" class="button form-control-blue">Preview</button>
                                        <%--<asp:LinkButton ID="LinkButton15" runat="server" OnClick="LinkButton15_Click" class="button form-control-blue">Preview</asp:LinkButton>--%>
                                    </div>
                                </div>
                                <div class="col-xs-6 text-right no-padding mgbt-xs-15">
                                    <button type="button" id="btn_QuickReg" class="button form-control-blue">Quick Registration</button>
                                    <%--<asp:LinkButton ID="lnkQuickReg" runat="server" OnClick="lnkQuickReg_Click" class="button form-control-blue">Quick Registration</asp:LinkButton>--%>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12   vd_input-margin ">
                            <div class="tabs">
                                <ul class="nav nav-tabs nav-justified">
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
                                </ul>

                                <div class="tab-content form-box-border mgbt-xs-20">

                                    <div class="tab-pane active " id="home-tab">

                                        <div class="row mgbt-xs-20">

                                            <div class="col-sm-6  full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class="fa  fa-child"></i></span>Student's Personal Details </h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
                                                                class=" menu entypo-icon">
                                                                <i class="icon-minus3"></i>
                                                            </div>

                                                        </div>
                                                        <!-- vd_panel-menu -->

                                                    </div>
                                                    <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                        <div class="row">

                                                            <script>
                                                                Sys.Application.add_load(datetime);
                                                            </script>
                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                <label class="control-label">First Name&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                    <input type="text" id="txt_FirstNa" class="form-control-blue" placeholder="Enter First Name" />
                                                                    <%--<asp:TextBox ID="txtFirstNa" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>--%>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                <label class="control-label">Middle Name</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                    <input type="text" id="txt_MidNa" class="form-control-blue" placeholder="Enter Middle Name" />
                                                                    <%--<asp:TextBox ID="txtMidNa" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  mgbt-xs-15">
                                                                <label class="control-label">Last Name</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                    <input type="text" id="txt_last" class="form-control-blue" placeholder="Enter Last Name" />
                                                                    <%--<asp:TextBox ID="txtlast" placeholder="" runat="server" CssClass="form-control-blue "></asp:TextBox>--%>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12  no-padding ">

                                                                <div class="col-sm-8  no-padding ">
                                                                    <div class="col-sm-6  mgbt-xs-15">
                                                                        <label class="control-label">Date of Birth&nbsp;<span class="vd_red"></span></label>
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                            <%--<asp:TextBox ID="txtStudentDOB" placeholder="yyyy MMM dd" onchange="BeginRequestHandlerNorm();"
                                                                                        runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>--%>
                                                                            <input type="text" id="txt_StudentDOB" class="form-control-blue datepicker-normal" placeholder="yyyy MMM dd" />
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-6  mgbt-xs-15">
                                                                        <label class="control-label">Age on Date</label>
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                                            <%--<asp:TextBox ID="txtAgeOnDate" placeholder="Age on Date" onchange="BeginRequestHandlerNorm();"
                                                                                        runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>--%>
                                                                            <input type="text" id="txt_AgeOnDate" class="form-control-blue datepicker-normal" placeholder="Age on Date" />
                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-12  mgbt-xs-15">
                                                                        <div class="col-xs-4 age-box  no-padding">
                                                                            <%--<asp:TextBox ID="txtAgeYear" placeholder="00" runat="server" Enabled="false"
                                                                                        CssClass="form-control-blue text-center"></asp:TextBox>--%>
                                                                            <input type="text" id="txt_AgeYear" class="form-control-blue text-center" placeholder="00" />
                                                                        </div>

                                                                        <div class="col-xs-4 age-box padding-lr-5">
                                                                            <%-- <asp:TextBox ID="txtAgeMonth" placeholder="00" runat="server" Enabled="false"
                                                                                        CssClass="form-control-blue text-center"></asp:TextBox>--%>
                                                                            <input type="text" id="txt_AgeMonth" class="form-control-blue text-center" placeholder="00" />
                                                                        </div>


                                                                        <div class="col-xs-4 age-box no-padding">
                                                                            <%--<asp:TextBox ID="txtAgeDay" placeholder="00" runat="server" Enabled="false"
                                                                                        CssClass="form-control-blue text-center"></asp:TextBox>--%>
                                                                            <input type="text" id="txt_AgeDay" class="form-control-blue text-center" placeholder="00" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-12  mgbt-xs-15">
                                                                        <label class="col-sm-2  no-padding control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                                                        <div class="col-sm-10 no-padding controls">
                                                                            <%--<asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="vd_radio radio-success 
                                                                                                validaterblist txt-capitalize-alpha"
                                                                                        RepeatDirection="Horizontal" RepeatLayout="flow">
                                                                                        <asp:ListItem Value="Male" Selected="True">Male</asp:ListItem>
                                                                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                                                                        <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                    <div class="text-box-msg">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                                            ControlToValidate="RadioButtonList1" Css="" ErrorMessage="*" SetFocusOnError="True"
                                                                                            Style="color: #CC3300" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                                                    </div>--%>
                                                                            <label class="radio-inline">
                                                                                <input type="radio" name="radioGender" style="color: #CC3300" value="Male" checked="checked">Male</label>
                                                                            <label class="radio-inline">
                                                                                <input type="radio" name="radioGender" style="color: #CC3300" value="Female">Female</label>
                                                                            <label class="radio-inline">
                                                                                <input type="radio" name="radioGender" style="color: #CC3300" value="Transgender">Transgender</label>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-6  no-padding ">
                                                                        <div class="col-sm-12  mgbt-xs-15">
                                                                            <label class="control-label">Email</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa fa-envelope"></i></span>
                                                                                <%--<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                                <input type="text" id="txt_Email" class="form-control-blue" placeholder="" />
                                                                                <div class="text-box-msg">
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-12  mgbt-xs-15">
                                                                            <%-- <asp:CheckBox ID="chkStEmail" runat="server" CssClass="vd_checkbox checkbox-success  "
                                                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for Email Alert" />--%>
                                                                            <label class="checkbox-inline">
                                                                                <input type="checkbox" class="vd_checkbox checkbox-success" id="chkStEmail" value="0">Tick for Email Alert</label>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-6  no-padding ">
                                                                        <div class="col-sm-12  mgbt-xs-15">
                                                                            <label class="control-label">Mobile No.</label>
                                                                            <div class="vd_input-wrapper controls ">
                                                                                <span class="menu-icon"><i class="fa  fa-mobile"></i></span>
                                                                                <%--<asp:TextBox ID="txtMobile" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                                <input type="text" id="txt_Mobile" class="form-control-blue" placeholder="" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-12  mgbt-xs-15">
                                                                            <%--<asp:CheckBox ID="chkStMobile" runat="server" CssClass="vd_checkbox checkbox-success "
                                                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for SMS Alert" />--%>
                                                                            <label class="checkbox-inline">
                                                                                <input type="checkbox" class="vd_checkbox checkbox-success" id="chkStMobile" value="0">Tick for Email Alert</label>

                                                                        </div>
                                                                    </div>
                                                                    <script>
                                                                        function Aadhar() {
                                                                            $('#txt_Mobile').on('keyup', function () {
                                                                                $(this).val(
                                                                                    function (index, value) {
                                                                                        value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                                        if (value.length > 10) {
                                                                                            value = value.substring(0, 10);
                                                                                        }
                                                                                        return value;
                                                                                    });
                                                                            });
                                                                        };
                                                                        Sys.Application.add_load(Aadhar);

                                                                    </script>
                                                                </div>

                                                                <div class="col-sm-4  no-padding">

                                                                    <div class="col-sm-12  mgbt-xs-15">
                                                                        <label class="control-label">Student Photo</label>
                                                                        <div class="controls img-input-ped ">
                                                                            <%--<asp:FileUpload ID="avatarUpload" runat="server"
                                                                                        onchange="checksFileSizeandFileTypeinupdatePanel(this, 50000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','Avatars',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdStPhoto');"
                                                                                        type="file" CssClass="form-control-blue " />--%>
                                                                            <input type="file" id="studentUpload" name="studentUpload" class="form-control-blue" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-12  mgbt-xs-10">
                                                                        <div class="stu-pic-box">
                                                                            <div class="stu-pic-box-main">
                                                                                <%--<asp:Image alt="" ID="Avatar" class="Avatars" runat="server" ImageUrl="../img/user-pic/student-pic.png" />
                                                                                        <asp:HiddenField ID="hdStPhoto" runat="server" />--%>
                                                                                <img src="../img/user-pic/student-pic.png" id="imgStudent" class="Avatars" />
                                                                                <input type="hidden" id="hdStPhoto" />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                            </div>


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
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
                                                                class=" menu entypo-icon">
                                                                <i class="icon-minus3"></i>
                                                            </div>

                                                        </div>
                                                        <!-- vd_panel-menu -->

                                                    </div>
                                                    <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                        <div class="row">

                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Mother Tongue&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-bullhorn"></i></span>
                                                                    <%--<asp:TextBox ID="txtMotherTongue" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_MotherTongue" class="form-control-blue" placeholder="Mother Tongue" />
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Home Town&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-home"></i></span>
                                                                    <%--<asp:TextBox ID="txtHomeTown" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_HomeTown" class="form-control-blue" placeholder="Home Town" />

                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Nationality&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-map-marker"></i></span>
                                                                    <%--<asp:TextBox ID="TextBox65" runat="server"
                                                                                CssClass="form-control-blue validatetxt" ToolTip="Nationality"></asp:TextBox--%>
                                                                    <input type="text" id="txt_Nationality" class="form-control-blue" placeholder="Nationality" />

                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Religion&nbsp;<span class="vd_red">*</span></label>
                                                                <div class=" controls ">


                                                                    <%--<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue"></asp:DropDownList>--%>
                                                                    <select id="ddlReligion" class="form-control-blue"></select>


                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Category&nbsp;<span class="vd_red">*</span></label>
                                                                <div class=" controls ">

                                                                    <%--<asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>--%>
                                                                    <select id="ddlCategory" class="form-control-blue"></select>



                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Caste</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                    <%-- ReSharper disable once Asp.SkinNotResolved --%>
                                                                    <%--<asp:TextBox ID="TextBox66" placeholder="" runat="server" SkinID="TxtBoxDef" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_Caste" class="form-control-blue" placeholder="" />
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="col-sm-12  half-width-50 half-width-100-tc mgbt-xs-15">
                                                                <label class="control-label">Aadhaar No.</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                    <input type="text" id="txt_AadharNo" class="form-control-blue" placeholder="0000 0000 0000" />
                                                                    <%--<asp:TextBox ID="txtAadharNo" placeholder="0000 0000 0000" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <script>
                                                                function Aadhar() {
                                                                    $('#txt_AadharNo').on('keyup', function () {
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

                                                            <div class="col-sm-12  half-width-50 half-width-100-tc">
                                                                <label class="control-label">Date of Issue of Aadhaar</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                                    <input type="text" id="txt_AadharIssueDate" class="form-control-blue datepicker-normal" placeholder="Ex. 1990 JAN 01" />

                                                                    <%--<asp:TextBox ID="txtAadharIssueDate" placeholder="Ex. 1990 JAN 01" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>--%>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>



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
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom" class=" menu entypo-icon">
                                                                <i class="icon-minus3"></i>
                                                            </div>

                                                        </div>
                                                        <!-- vd_panel-menu -->

                                                    </div>
                                                    <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                        <div class="row">

                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Select Blood Group</label>
                                                                <div class="">

                                                                    <%--<asp:DropDownList ID="drpBloodGroup" runat="server" CssClass="form-control-blue">
                                                                                        <asp:ListItem Text="<-- Select Blood Group -->"></asp:ListItem>
                                                                                    </asp:DropDownList>--%>
                                                                    <select id="ddlBloodGroup" class="form-control-blue">
                                                                        <option value=""></option>
                                                                    </select>
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">

                                                                <div class="col-sm-12  no-padding">
                                                                    <label class="control-label">Vision</label>
                                                                </div>
                                                                <div class="col-sm-6 col-xs-6 no-padding">
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-eye"></i></span>
                                                                        <input type="text" id="txt_VRight" class="form-control-blue" placeholder="Right" />

                                                                        <%--<asp:TextBox ID="txtVRight" placeholder="Right" runat="server" CssClass="form-control-blue "></asp:TextBox>--%>
                                                                        <div class="text-box-msg">
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 col-xs-6 no-padding">
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-eye"></i></span>
                                                                        <%--<asp:TextBox ID="txtVLeft" placeholder="Left" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                        <input type="text" id="txt_VLeft" class="form-control-blue" placeholder="Left" />
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
                                                                        <%--<asp:TextBox ID="txtHeight" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                        <input type="text" id="txt_Height" class="form-control-blue" placeholder="Height" />

                                                                        <div class="text-box-msg">
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-xs-6 no-padding" style="display: none">
                                                                    <label class="control-label">&nbsp;</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <%--<asp:DropDownList ID="DropDownList34" runat="server"
                                                                                    CssClass="form-control-blue ">
                                                                                    <asp:ListItem Selected="True" Text="cm">cm</asp:ListItem>
                                                                                    <asp:ListItem Text="inches">inches</asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                                        <select id="ddlHeight" class="form-control-blue">
                                                                            <option value="cm">cm</option>
                                                                            <option value="inches">inches</option>
                                                                        </select>
                                                                        <div class="text-box-msg">
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                <div class="col-sm-12  no-padding">
                                                                    <label class="control-label">Weight</label>
                                                                </div>
                                                                <div class="col-sm-8 col-xs-8 no-padding  ">
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-database"></i></span>
                                                                        <%--<asp:TextBox ID="txtWeight" placeholder="	" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                        <input type="text" id="txt_Weight" class="form-control-blue" placeholder="Weight" />

                                                                        <div class="text-box-msg">
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4 col-xs-4 no-padding">

                                                                    <div class="">
                                                                        <%--<asp:DropDownList ID="DropDownList35" runat="server" CssClass="form-control-blue ">
                                                                                    <asp:ListItem Selected="True" Text="kg">kg</asp:ListItem>
                                                                                    <asp:ListItem Text="pound">pound</asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                                        <select id="ddlWeight" class="form-control-blue">
                                                                            <option value="kg">kg</option>
                                                                            <option value="pound">pound</option>
                                                                        </select>
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
                                                                    <%--<asp:TextBox ID="txtDental" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_Dental" class="form-control-blue" placeholder="" />

                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Oral Hygiene</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-smile-o"></i></span>
                                                                    <%-- ReSharper disable once Asp.SkinNotResolved --%>
                                                                    <%--<asp:TextBox ID="txtOral" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_Oral" class="form-control-blue" placeholder="" />
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Identification Mark</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-smile-o"></i></span>
                                                                    <%-- ReSharper disable once Asp.SkinNotResolved --%>
                                                                    <%--<asp:TextBox ID="txtIMark" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_IMark" class="form-control-blue" placeholder="" />
                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Specific Ailment</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-smile-o"></i></span>
                                                                    <%--<asp:TextBox ID="txtSpeAilment" SkinID="TxtBoxDef" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_SpeAilment" class="form-control-blue" placeholder="" />

                                                                    <div class="text-box-msg">
                                                                    </div>

                                                                </div>
                                                            </div>



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
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
                                                                class=" menu entypo-icon">
                                                                <i class="icon-minus3"></i>
                                                            </div>

                                                        </div>
                                                        <!-- vd_panel-menu -->

                                                    </div>
                                                    <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                        <div class="row">

                                                            <div class="col-sm-12   mgbt-xs-10">
                                                                <div class="vd_input-wrapper ">
                                                                    <label class="col-sm-4 no-padding control-label">Physically Disabled?</label>
                                                                    <div class="col-sm-8 no-padding controls">

                                                                        <%--<asp:RadioButtonList ID="RadioButtonList8" runat="server"
                                                                                            CssClass="vd_radio radio-success" AutoPostBack="True"
                                                                                            OnSelectedIndexChanged="RadioButtonList8_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                                                            RepeatLayout="flow">
                                                                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                                            <asp:ListItem Value="No" Selected="True">No</asp:ListItem>
                                                                                        </asp:RadioButtonList>--%>
                                                                        <label class="radio-inline">
                                                                            <input type="radio" name="radio_Disabled" class="vd_radio radio-success" value="Yes">Yes</label>
                                                                        <label class="radio-inline">
                                                                            <input type="radio" name="radio_Disabled" class="vd_radio radio-success" value="No" checked="checked">No</label>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12  no-padding" id="Panel2" runat="server">

                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Name of Disability</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-tags"></i></span>
                                                                        <%--<asp:TextBox ID="txtPhyName" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                        <input type="text" id="txt_DisabilityName" class="form-control-blue" placeholder="" />

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                                    <label class="control-label">Certificate No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-mobile"></i></span>
                                                                        <%--<asp:TextBox ID="txtCertificateNo" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                        <input type="text" id="txt_CertificateNo" class="form-control-blue" placeholder="" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-12   mgbt-xs-15">
                                                                    <label class="control-label">Issued By</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-thumbs-up"></i></span>
                                                                        <%--<asp:TextBox ID="txtIssuedBy" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                        <input type="text" id="txt_IssuedBy" class="form-control-blue" placeholder="" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12   ">
                                                                    <label class="control-label">Details</label>
                                                                    <div class="">
                                                                        <%--<asp:TextBox ID="txtPhyDetail" runat="server"
                                                                                    TextMode="MultiLine" Rows="3" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                        <textarea id="txt_Disabilitydetails" class="form-control-blue" placeholder="Write here..."></textarea>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                    </div>
                                                    <!-- Panel Widget -->
                                                </div>
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
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
                                                                class=" menu entypo-icon">
                                                                <i class="icon-minus3"></i>
                                                            </div>

                                                        </div>
                                                        <!-- vd_panel-menu -->

                                                    </div>
                                                    <div class="panel-body padding-tlbr-15 form-main-box-border">


                                                        <div class="row">


                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Father's Name&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-user"></i></span>

                                                                    <%--<asp:TextBox ID="txtfaNameee" runat="server" placeholder=""
                                                                                            onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtguardianname',
                                                                                                '#ContentPlaceHolder1_ContentPlaceHolderMainBox_DrpRelationship','Father');"
                                                                                            CssClass="form-control-blue validatetxt"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_fatherName" class="form-control-blue" placeholder="Father's Name" />

                                                                    <div class="text-box-msg">
                                                                    </div>


                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Select Occupation&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">

                                                                    <%--<asp:DropDownList ID="drpOccupationfa" runat="server"
                                                                                            AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                                                                        </asp:DropDownList>--%>
                                                                    <select id="ddlOccupationfather" class="form-control-blue">
                                                                    </select>
                                                                    <div class="text-box-msg">
                                                                    </div>


                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Designation</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                    <%--<asp:TextBox ID="txtdesfa" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_fatherDesc" class="form-control-blue" placeholder="Father's Designation" />

                                                                    <div class="text-box-msg">
                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Qualification</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-lightbulb-o"></i></span>
                                                                    <%--<asp:TextBox ID="txtqufa" placeholder="" runat="server" CssClass="form-control-blue "></asp:TextBox>--%>
                                                                    <input type="text" id="txt_fatherQuali" class="form-control-blue" placeholder="Qualification" />

                                                                    <div class="text-box-msg">
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Annual Income</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-money"></i></span>

                                                                    <%--<asp:TextBox ID="txtincomefa" runat="server" placeholder=""
                                                                                            onkeyup="AddDecimalValue(event,this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtParentTotalIncome',
                                                                                                '#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtincomemonthlymother');"
                                                                                            CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_fatherIncome" class="form-control-blue" placeholder="Annual Income" />

                                                                    <div class="text-box-msg">
                                                                    </div>



                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Nationality&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-flag"></i></span>


                                                                    <%--<asp:TextBox ID="txtFatherNationality" placeholder="" runat="server"
                                                                                            CssClass="form-control-blue validatetxt"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_FatherNationality" class="form-control-blue" placeholder="Nationality" />

                                                                    <div class="text-box-msg">
                                                                    </div>


                                                                </div>
                                                            </div>

                                                            <div class="col-sm-8  no-padding">
                                                                <div class="col-sm-12   mgbt-xs-9">
                                                                    <label class="control-label">Office Address</label>
                                                                    <div class="">
                                                                        <%--<asp:TextBox ID="txtoffaddfa" placeholder="" runat="server" TextMode="MultiLine"
                                                                                        Rows="3" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                        <textarea id="txt_FatherOfficeAddr" class="form-control-blue" placeholder="Write here..."></textarea>

                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  half-width-50 ">
                                                                    <div class="col-sm-12  no-padding mgbt-xs-15">
                                                                        <label class="control-label">Email&nbsp;</label>
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa  fa-envelope"></i></span>

                                                                            <%-- <asp:TextBox ID="txtemailfather" runat="server" placeholder=""
                                                                                                    CssClass="form-control-blue"></asp:TextBox>--%>
                                                                            <input type="text" id="txt_FatherEmail" class="form-control-blue" placeholder="Email" />



                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                        <%--<asp:CheckBox ID="chkFaEmail" runat="server" CssClass="vd_checkbox checkbox-success"
                                                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for Email Alert" />--%>
                                                                        <label class="checkbox-inline">
                                                                            <input type="checkbox" typeof="chkFaEmail" class="vd_checkbox checkbox-success" value="0">Tick for Email Alert
                                                                        </label>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  half-width-50 ">
                                                                    <div class="col-sm-12  no-padding mgbt-xs-15">
                                                                        <label class="control-label">Mobile No.&nbsp;<span class="vd_red">*</span></label>
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa fa-phone"></i></span>

                                                                            <%-- <asp:TextBox ID="txtcontfa" runat="server" placeholder=""
                                                                                                    onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtcontactNo',
                                                                                                    '#ContentPlaceHolder1_ContentPlaceHolderMainBox_DrpRelationship','Father');"
                                                                                                    CssClass="form-control-blue validatetxt" MaxLength="10"></asp:TextBox>--%>
                                                                            <input type="text" id="txt_FatherContact" class="form-control-blue" placeholder="Mobile No." />



                                                                        </div>
                                                                    </div>
                                                                    <script>
                                                                        function Aadhar() {
                                                                            $('#txt_FatherContact').on('keyup', function () {
                                                                                $(this).val(
                                                                                    function (index, value) {
                                                                                        value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                                        if (value.length > 10) {
                                                                                            value = value.substring(0, 10);
                                                                                        }
                                                                                        return value;
                                                                                    });
                                                                            });
                                                                        };
                                                                        Sys.Application.add_load(Aadhar);

                                                                    </script>
                                                                    <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                        <%--<asp:CheckBox ID="chkFaMobile" runat="server" CssClass="vd_checkbox checkbox-success"
                                                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Text="Tick for SMS Alert" />
                                                                                     <label class="checkbox-inline">--%>
                                                                        <input type="checkbox" typeof="chkFaMobile" class="vd_checkbox checkbox-success" value="0">Tick for SMS Alert
                                                                                    </label>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4   mgbt-xs-15">
                                                                <label class="control-label">Father's Photo</label>
                                                                <div class="vd_input-wrapper controls img-input-ped ">
                                                                    <%--<asp:Label ID="lblFatherImageUrl" runat="server" Visible="False"></asp:Label>
                                                                                <asp:Label ID="lblFatherImageName" runat="server" Visible="False"></asp:Label>--%>
                                                                    <span id="lblFatherImageUrl" class="hide"></span>
                                                                    <span id="lblFatherImageName" class="hide"></span>
                                                                    <%--<asp:FileUpload ID="FileUpload1" CssClass="form-control-blue" runat="server"
                                                                                    onchange="checksFileSizeandFileTypeinupdatePanel(this, 50000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','imgFather',
                                                                                    'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdfatherPhoto');"
                                                                                    type="file" />--%>
                                                                    <input type="file" id="fatherPhotoUpload" class="form-control-blue" />
                                                                </div>

                                                            </div>
                                                            <div class="col-sm-4   ">

                                                                <div class="vd_input-wrapper controls ">
                                                                    <div class="stu-pic-box">
                                                                        <div class="stu-pic-box-main">
                                                                            <%--<asp:Image ID="imgFather" runat="server" class="imgFather"
                                                                                            ImageUrl="../img/user-pic/student-pic.png" alt="" />--%>
                                                                            <%--<asp:HiddenField ID="hdfatherPhoto" runat="server" />--%>
                                                                            <img src="../img/user-pic/student-pic.png" id="imgFather" class="imgFather" />
                                                                            <input type="hidden" id="hdfatherPhoto" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <div class="col-md-4 mgbt-xs-15" style="display: none">
                                                                <label class="control-label">Office Phone No.</label>
                                                                <div class="vd_input-wrapper controls ">

                                                                    <span class="menu-icon"><i class="fa  fa-phone"></i></span>

                                                                    <%--<asp:TextBox ID="txtFatherOfficePhoneNo" placeholder="" runat="server"
                                                                                            CssClass="form-control-blue "></asp:TextBox>--%>
                                                                    <input type="text" id="txt_FatherOfficePhoneNo" class="form-control-blue" placeholder="Office Phone No." />



                                                                </div>
                                                            </div>
                                                            <script>
                                                                function Aadhar() {
                                                                    $('#txt_FatherOfficePhoneNo').on('keyup', function () {
                                                                        $(this).val(
                                                                            function (index, value) {
                                                                                value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                                if (value.length > 10) {
                                                                                    value = value.substring(0, 10);
                                                                                }
                                                                                return value;
                                                                            });
                                                                    });
                                                                };
                                                                Sys.Application.add_load(Aadhar);

                                                            </script>
                                                            <div class="col-md-8 no-padding " style="display: none">
                                                                <div class="col-md-6 " style="display: none">
                                                                    <label class="control-label">Office Mobile No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa fa-mobile"></i></span>


                                                                        <%--<asp:TextBox ID="txtFatherOfficeMobileNo" placeholder=""
                                                                                                runat="server" CssClass="form-control-blue "></asp:TextBox>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                                                                                CssClass="imp" runat="server"
                                                                                                ControlToValidate="txtFatherOfficeMobileNo" ErrorMessage="*"
                                                                                                SetFocusOnError="True"
                                                                                                ValidationExpression="^[0-9]{10,10}$" ValidationGroup="A"></asp:RegularExpressionValidator>--%>
                                                                        <input type="text" id="txt_FatherOfficeMobileNo" class="form-control-blue" placeholder="Office Mobile No." />



                                                                    </div>
                                                                </div>
                                                                <script>
                                                                    function Aadhar() {
                                                                        $('#txt_FatherOfficeMobileNo').on('keyup', function () {
                                                                            $(this).val(
                                                                                function (index, value) {
                                                                                    value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                                    if (value.length > 10) {
                                                                                        value = value.substring(0, 10);
                                                                                    }
                                                                                    return value;
                                                                                });
                                                                        });
                                                                    };
                                                                    Sys.Application.add_load(Aadhar);

                                                                </script>
                                                                <div class="col-md-6 mgbt-xs-15" style="display: none">
                                                                    <label class="control-label">Office Email</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-envelope"></i></span>


                                                                        <%--<asp:TextBox ID="txtFatherOfficeEmail" placeholder=" " runat="server"
                                                                                                CssClass="form-control-blue" onBlur="ValidateEmail(this,
                                                                                                    '#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtFatherOfficeEmail');"></asp:TextBox>--%>
                                                                        <input type="text" id="txt_FatherOfficeEmail" class="form-control-blue" placeholder="Office Email" />



                                                                    </div>
                                                                </div>


                                                            </div>








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


                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Mother's Name&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                    <%--<asp:TextBox ID="txtmotherNameeee" runat="server" CssClass="form-control-blue validatetxt" placeholder=""
                                                                                    onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtGuardiantwoName',
                                                                                        '#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpGuardiantwoRelationship','Mother');"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_motherName" class="form-control-blue" placeholder="Mother's Name" />

                                                                    <div class="text-box-msg">
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Select Occupation&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">

                                                                    <%--<asp:DropDownList ID="drpOccupationmoth"
                                                                                            runat="server" AutoPostBack="True"
                                                                                            OnSelectedIndexChanged="drpOccupationmoth_SelectedIndexChanged"
                                                                                            CssClass="form-control-blue validatedrp">
                                                                                        </asp:DropDownList>--%>
                                                                    <select id="ddlOccupationmother" class="form-control-blue">
                                                                    </select>
                                                                    <div class="text-box-msg">
                                                                    </div>


                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Designation</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-star"></i></span>


                                                                    <%--<asp:TextBox ID="txtdesmoth" runat="server" placeholder="" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_motherDesi" class="form-control-blue" placeholder="Mother's Designation" />

                                                                    <div class="text-box-msg">
                                                                    </div>


                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Qualification</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-lightbulb-o"></i></span>
                                                                    <asp:TextBox ID="txtqualimother" runat="server" CssClass="form-control-blue "></asp:TextBox>
                                                                    <input type="text" id="txt_motherQuali" class="form-control-blue" placeholder="Qualification" />

                                                                    <div class="text-box-msg">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Annual Income</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-money"></i></span>

                                                                    <%--<asp:TextBox ID="txtincomemonthlymother" placeholder="" runat="server" CssClass="form-control-blue"
                                                                                            onkeyup="AddDecimalValue(event,this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtParentTotalIncome',
                                                                                                '#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtincomefa');"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_incomemonthlymother" class="form-control-blue" placeholder="Annual Income" />

                                                                    <div class="text-box-msg">
                                                                    </div>


                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Nationality&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-flag"></i></span>

                                                                    <%--<asp:TextBox ID="txtMotherNationality" placeholder="" runat="server"
                                                                                            CssClass="form-control-blue validatetxt"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_MotherNationality" class="form-control-blue" placeholder="Nationality" />

                                                                    <div class="text-box-msg">
                                                                    </div>


                                                                </div>
                                                            </div>
                                                            <div class="col-md-4 mgbt-xs-15" style="display: none">
                                                                <label class="control-label">Office Phone No.</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-phone"></i></span>


                                                                    <%--<asp:TextBox ID="txtMotherOfficePhoneNo" placeholder="" runat="server"
                                                                                            CssClass="form-control-blue "></asp:TextBox>--%>
                                                                    <input type="text" id="txt_MotherOfficePhoneNo" class="form-control-blue" placeholder="Office Phone No." />



                                                                </div>
                                                            </div>
                                                            <script>
                                                                function Aadhar() {
                                                                    $('#txt_MotherOfficePhoneNo').on('keyup', function () {
                                                                        $(this).val(
                                                                            function (index, value) {
                                                                                value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                                if (value.length > 10) {
                                                                                    value = value.substring(0, 10);
                                                                                }
                                                                                return value;
                                                                            });
                                                                    });
                                                                };
                                                                Sys.Application.add_load(Aadhar);

                                                            </script>

                                                            <div class="col-sm-8  no-padding">
                                                                <div class="col-sm-12   mgbt-xs-9">
                                                                    <label class="control-label">Office Address</label>
                                                                    <div class="">

                                                                        <%--<asp:TextBox ID="txtofficeaddmother" runat="server" AutoPostBack="True"
                                                                                                TextMode="MultiLine" Rows="3" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                        <textarea id="txt_motherOfficeAddr" class="form-control-blue" placeholder="Office Address"></textarea>

                                                                        <div class="text-box-msg">
                                                                        </div>


                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6  half-width-50 ">
                                                                    <div class="col-sm-12  no-padding mgbt-xs-15">
                                                                        <label class="control-label">Email&nbsp;</label>
                                                                        <div class="vd_input-wrapper controls ">
                                                                            <span class="menu-icon"><i class="fa  fa-envelope"></i></span>

                                                                            <%--<asp:TextBox ID="txtmotheremail" placeholder="" runat="server" CssClass="form-control-blue "
                                                                                            onBlur="ValidateEmail(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtGuardiantwoEmail');">
                                                                                        </asp:TextBox>--%>
                                                                            <input type="text" id="txt_motheremail" class="form-control-blue" placeholder="Email" />

                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                        <%--<asp:CheckBox ID="chkMoEmail" runat="server" Text="Tick for Email Alert"
                                                                                        CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="Flow" />--%>
                                                                        <label class="checkbox-inline">
                                                                            <input type="checkbox" id="chkMoEmail" class="vd_checkbox checkbox-success" value="0">Tick for Email Alert
                                                                        </label>
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

                                                                            <%--<asp:TextBox ID="txtmothercontact" placeholder="" runat="server"
                                                                                            CssClass="form-control-blue validatetxt" MaxLength="10"
                                                                                            onBlur="CopyText(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtGuardiantwoContact',
                                                                                            '#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpGuardiantwoRelationship','Mother');"></asp:TextBox>--%>
                                                                            <input type="text" id="txt_mothercontact" class="form-control-blue" placeholder="Mobile No." />

                                                                            <div class="text-box-msg">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <script>
                                                                        function Aadhar() {
                                                                            $('#txt_mothercontact').on('keyup', function () {
                                                                                $(this).val(
                                                                                    function (index, value) {
                                                                                        value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                                        if (value.length > 10) {
                                                                                            value = value.substring(0, 10);
                                                                                        }
                                                                                        return value;
                                                                                    });
                                                                            });
                                                                        };
                                                                        Sys.Application.add_load(Aadhar);

                                                                    </script>
                                                                    <div class="col-sm-12  no-padding mgbt-xs-10">
                                                                        <%--<asp:CheckBox ID="chkMoMobile" runat="server" Text="Tick for SMS Alert"
                                                                                        CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="Flow" />--%>
                                                                        <label class="checkbox-inline">
                                                                            <input type="checkbox" id="chkMoMobile" class="vd_checkbox checkbox-success" value="0">Tick for SMS Alert
                                                                        </label>
                                                                        <div class="text-box-msg">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4   mgbt-xs-15">
                                                                <label class="control-label">Mother's Photo</label>
                                                                <div class="vd_input-wrapper controls img-input-ped ">
                                                                    <%-- <asp:FileUpload ID="FileUpload2" runat="server"
                                                                                    CssClass="form-control-blue "
                                                                                    onchange="checksFileSizeandFileTypeinupdatePanel(this, 50000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','imgMother',
                                                                                    'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdmotherPhoto');"
                                                                                    type="file" />--%>

                                                                    <input type="file" id="motherPhotoUpload" class="form-control-blue" />
                                                                </div>

                                                            </div>

                                                            <div class="col-sm-4  ">
                                                                <div class="stu-pic-box">
                                                                    <div class="stu-pic-box-main">
                                                                        <%--<asp:Label ID="lblMotherImageUrl" runat="server" Visible="False"></asp:Label>
                                                                                    <asp:Label ID="lblMotherImageName" runat="server" Visible="False"></asp:Label>--%>
                                                                        <span id="lblMotherImageUrl" class="hide"></span>
                                                                        <span id="lblMotherImageName" class="hide"></span>
                                                                        <%--<asp:Image ID="imgMother" ImageUrl="../img/user-pic/student-f-pic.png" runat="server" Class="imgMother" />--%>
                                                                        <img src="../img/user-pic/student-f-pic.png" id="imgMother" class="imgMother" />
                                                                        <asp:HiddenField ID="hdmotherPhoto" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>




                                                            <div class="col-md-8 no-padding " style="display: none">

                                                                <div class="col-md-6" style="display: none">
                                                                    <label class="control-label">Office Mobile No.</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa   fa-mobile"></i></span>

                                                                        <%-- <asp:TextBox ID="txtMotherOfficeMobileNo" placeholder="" runat="server"
                                                                                                CssClass="form-control-blue"></asp:TextBox>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                                                                                CssClass="imp" runat="server" ControlToValidate="txtMotherOfficeMobileNo"
                                                                                                ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[0-9]{10,10}$"
                                                                                                ValidationGroup="A"></asp:RegularExpressionValidator>--%>
                                                                        <input type="text" id="txt_MotherOfficeMobileNo" class="form-control-blue" placeholder="Office Mobile No." />



                                                                    </div>
                                                                </div>
                                                                <script>
                                                                    function Aadhar() {
                                                                        $('#txt_MotherOfficeMobileNo').on('keyup', function () {
                                                                            $(this).val(
                                                                                function (index, value) {
                                                                                    value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                                    if (value.length > 10) {
                                                                                        value = value.substring(0, 10);
                                                                                    }
                                                                                    return value;
                                                                                });
                                                                        });
                                                                    };
                                                                    Sys.Application.add_load(Aadhar);

                                                                </script>
                                                                <div class="col-md-6 mgbt-xs-15" style="display: none">
                                                                    <label class="control-label">Office Email</label>
                                                                    <div class="vd_input-wrapper controls ">
                                                                        <span class="menu-icon"><i class="fa  fa-envelope"></i></span>


                                                                        <%--<asp:TextBox ID="txtMotherOfficeEmail" runat="server" placeholder=" "
                                                                                                CssClass="form-control-blue" onBlur="ValidateEmail(this,
                                                                                                    'ContentPlaceHolder1_ContentPlaceHolderMainBox_txtFatherOfficeEmail');"></asp:TextBox>--%>
                                                                        <input type="text" id="txt_MotherOfficeEmail" class="form-control-blue" placeholder="Office Email" />



                                                                    </div>
                                                                </div>

                                                            </div>






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

                                                            <div class="col-sm-12  mgbt-xs-9 ">
                                                                <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">

                                                                    <%--<asp:TextBox ID="txtPreaddress" placeholder="Please don't write State and City name here" runat="server"
                                                                                            TextMode="MultiLine" Rows="3" CssClass="form-control-blue validatetxt"></asp:TextBox>--%>
                                                                    <textarea id="txt_Preaddress" class="form-control-blue" placeholder="Please don't write State and City name here"></textarea>

                                                                    <div class="text-box-msg">
                                                                    </div>


                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Country&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">

                                                                    <%--<asp:DropDownList ID="DrpPreCountry" runat="server"
                                                                                            AutoPostBack="true" OnSelectedIndexChanged="DrpPreCountry_SelectedIndexChanged"
                                                                                            CssClass="form-control-blue ">
                                                                                            <asp:ListItem Text="<-- Select Country -->"></asp:ListItem>
                                                                                        </asp:DropDownList>--%>
                                                                    <select id="ddlPreCountry" class="form-control-blue">
                                                                        <option value=""><-- Select Country --></option>
                                                                    </select>
                                                                    <div class="text-box-msg">
                                                                    </div>



                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                                                <div class="">

                                                                    <%--<asp:DropDownList ID="DrpPreState" runat="server" AutoPostBack="True"
                                                                                            CssClass="form-control-blue " OnSelectedIndexChanged="DrpPreState_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                                                                ControlToValidate="DrpPreState" Css="" ErrorMessage="" SetFocusOnError="True"
                                                                                                Style="color: #CC3300" ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                                                                    <select id="ddlPreState" class="form-control-blue">
                                                                    </select>
                                                                </div>


                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">

                                                                <%--<asp:DropDownList ID="DrpPreCity" runat="server" CssClass="form-control-blue">
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                                                                                runat="server" ControlToValidate="DrpPreCity" Css="" ErrorMessage="*"
                                                                                                SetFocusOnError="True" Style="color: #CC3300" ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                                                                <select id="ddlPreCity" class="form-control-blue">
                                                                </select>
                                                            </div>


                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Pin Code</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-road"></i></span>

                                                            <%--<asp:TextBox ID="txtPreZip" placeholder="Pin Code" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                                                                CssClass="imp" runat="server" ControlToValidate="txtPreZip"
                                                                                                ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[0-9]{6,6}$"
                                                                                                ValidationGroup="A"></asp:RegularExpressionValidator>
                                                                                        </div>--%>
                                                            <input type="text" id="txt_PreZip" class="form-control-blue" placeholder="Pin Code" />



                                                        </div>
                                                    </div>
                                                    <script>
                                                        function Aadhar() {
                                                            $('#txt_PreZip').on('keyup', function () {
                                                                $(this).val(
                                                                    function (index, value) {
                                                                        value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                        if (value.length > 6) {
                                                                            value = value.substring(0, 6);
                                                                        }
                                                                        return value;
                                                                    });
                                                            });
                                                        };
                                                        Sys.Application.add_load(Aadhar);

                                                    </script>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Phone No.</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-phone"></i></span>

                                                            <%--<asp:TextBox ID="txtPrePhoneNo" placeholder="Phone No." runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_PrePhoneNo" class="form-control-blue" placeholder="Phone No." />

                                                            <div class="text-box-msg">
                                                            </div>



                                                        </div>
                                                    </div>
                                                    <script>
                                                        function Aadhar() {
                                                            $('#txt_PrePhoneNo').on('keyup', function () {
                                                                $(this).val(
                                                                    function (index, value) {
                                                                        value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                        if (value.length > 10) {
                                                                            value = value.substring(0, 10);
                                                                        }
                                                                        return value;
                                                                    });
                                                            });
                                                        };
                                                        Sys.Application.add_load(Aadhar);

                                                    </script>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Mobile No.</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-mobile"></i></span>

                                                            <%-- <asp:TextBox ID="txtPreMobileNo" placeholder="Mobile No." runat="server"
                                                                                            CssClass="form-control-blue"></asp:TextBox>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                                                                                CssClass="imp" runat="server" ControlToValidate="txtPreMobileNo" ErrorMessage="*"
                                                                                                SetFocusOnError="True" ValidationExpression="^[0-9]{10,10}$"
                                                                                                ValidationGroup="A"></asp:RegularExpressionValidator>
                                                                                        </div>--%>
                                                            <input type="text" id="txt_PreMobileNo" class="form-control-blue" placeholder="Mobile No." />


                                                        </div>
                                                    </div>
                                                    <script>
                                                        function Aadhar() {
                                                            $('#txt_PreMobileNo').on('keyup', function () {
                                                                $(this).val(
                                                                    function (index, value) {
                                                                        value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                        if (value.length > 10) {
                                                                            value = value.substring(0, 10);
                                                                        }
                                                                        return value;
                                                                    });
                                                            });
                                                        };
                                                        Sys.Application.add_load(Aadhar);

                                                    </script>




                                                </div>
                                            </div>
                                        </div>
                                        <!-- Panel Widget -->
                                   

                                    <div class="col-sm-6  full-width-100">
                                        <div class="panel widget">
                                            <div class="panel-heading vd_bg-dark-blue">
                                                <h3 class="panel-title"><span class="menu-icon"><i class="glyphicon glyphicon-map-marker"></i></span>Permanent Address </h3>
                                                <div class="vd_panel-menu ">
                                                    <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
                                                        class=" menu entypo-icon">
                                                        <i class="icon-minus3"></i>
                                                    </div>
                                                </div>
                                                <!-- vd_panel-menu -->

                                            </div>
                                            <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                <div class="row">


                                                    <%--    <asp:UpdatePanel ID="UpdatePanel65" runat="server">
                                                    --%>

                                                    <div class="col-sm-12   mgbt-xs-9 tick">
                                                        <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>

                                                        <%--<asp:CheckBox ID="chkCopy" runat="server" TextAlign="Right" CssClass="vd_checkbox checkbox-success" Text="&nbsp;Same as Present Address" />--%>
                                                        <label class="checkbox-inline">
                                                            <input type="checkbox" id="chkCopyaddr" class="vd_checkbox checkbox-success">&nbsp;Same as Present Address
                                                        </label>
                                                        <div class="">

                                                            <%--<asp:TextBox ID="txtPerAdd" placeholder="Please don't write State and City name here" runat="server" TextMode="MultiLine" Rows="3"
                                                                                    CssClass="form-control-blue validatetxt"></asp:TextBox>--%>
                                                            <textarea id="txt_PerAdd" class="form-control-blue" placeholder="Please don't write State and City name here"></textarea>

                                                            <div class="text-box-msg">
                                                            </div>


                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Country&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="DrpPerCountry" runat="server"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="DrpPerCountry_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue ">
                                                                                    <asp:ListItem Text="<-- Select Country -->"></asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                            <select id="ddlPerCountry" class="form-control-blue">
                                                                <option value=""><-- Select Country --></option>
                                                            </select>
                                                            <div class="text-box-msg">
                                                            </div>




                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="DrpPerState" runat="server" AutoPostBack="True"
                                                                                    onchange="ValidatorUpdateDisplay(ContentPlaceHolder1_ContentPlaceHolderMainBox_RequiredFieldValidator12)"
                                                                                    OnSelectedIndexChanged="DrpPerState_SelectedIndexChanged"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                                <div class="text-box-msg">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                                                        ControlToValidate="DrpPerState" Css="" ErrorMessage="*" SetFocusOnError="True"
                                                                                        Style="color: #CC3300" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                                                </div>--%>
                                                            <select id="ddlPerState" class="form-control-blue">
                                                            </select>



                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                                        <div class=" ">

                                                            <%--<asp:DropDownList ID="DrpPerCity" runat="server" AutoPostBack="True"
                                                                                    onchange="ValidatorUpdateDisplay(ContentPlaceHolder1_ContentPlaceHolderMainBox_RequiredFieldValidator13)"
                                                                                    CssClass="form-control-blue">
                                                                                </asp:DropDownList>
                                                                                <div class="text-box-msg">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="A"
                                                                                        ControlToValidate="DrpPerCity" Css="" ErrorMessage="*" SetFocusOnError="True" Style="color: #CC3300"
                                                                                        InitialValue="<--Select-->"></asp:RequiredFieldValidator>
                                                                                </div>--%>
                                                            <select id="ddlPerCity" class="form-control-blue">
                                                            </select>


                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Pin Code</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-road"></i></span>

                                                            <%--<asp:TextBox ID="txtPerZip" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg">
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                                                        ControlToValidate="txtPerZip" ErrorMessage="*"
                                                                                        ValidationExpression="^[0-9]{6,6}$" ValidationGroup="A"
                                                                                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                                </div>--%>
                                                            <input type="text" id="txt_PerZip" class="form-control-blue" placeholder="PIN Code" />



                                                        </div>
                                                    </div>

                                                    <script>
                                                        function Aadhar() {
                                                            $('#txt_PerZip').on('keyup', function () {
                                                                $(this).val(
                                                                    function (index, value) {
                                                                        value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                        if (value.length > 6) {
                                                                            value = value.substring(0, 6);
                                                                        }
                                                                        return value;
                                                                    });
                                                            });
                                                        };
                                                        Sys.Application.add_load(Aadhar);

                                                    </script>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Phone No.</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-phone"></i></span>

                                                            <%--<asp:TextBox ID="txtPerPhoneNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_PerPhoneNo" class="form-control-blue" placeholder="Phone No." />

                                                            <div class="text-box-msg">
                                                            </div>


                                                        </div>
                                                    </div>
                                                    <script>
                                                        function Aadhar() {
                                                            $('#txt_PerPhoneNo').on('keyup', function () {
                                                                $(this).val(
                                                                    function (index, value) {
                                                                        value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                        if (value.length > 10) {
                                                                            value = value.substring(0, 10);
                                                                        }
                                                                        return value;
                                                                    });
                                                            });
                                                        };
                                                        Sys.Application.add_load(Aadhar);

                                                    </script>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Mobile No.</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-mobile"></i></span>

                                                            <%--<asp:TextBox ID="txtPerMobileNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                <div class="text-box-msg">
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" CssClass="imp" runat="server"
                                                                                        ControlToValidate="txtPerMobileNo" ErrorMessage="*" SetFocusOnError="True"
                                                                                        ValidationExpression="^[0-9]{10,10}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                                                                                </div>--%>
                                                            <input type="text" id="txt_PerMobileNo" class="form-control-blue" placeholder="Phone No." />



                                                        </div>
                                                    </div>
                                                    <script>
                                                        function Aadhar() {
                                                            $('#txt_PerMobileNo').on('keyup', function () {
                                                                $(this).val(
                                                                    function (index, value) {
                                                                        value = value.replace(/\W/gi, '').replace(/(.{4})/g, '$1 ');
                                                                        if (value.length > 10) {
                                                                            value = value.substring(0, 10);
                                                                        }
                                                                        return value;
                                                                    });
                                                            });
                                                        };
                                                        Sys.Application.add_load(Aadhar);

                                                    </script>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Panel Widget -->
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

                                                    <div class="col-sm-12   mgbt-xs-15">
                                                        <label class="col-sm-4  no-padding control-label">Parent's Total Income</label>
                                                        <div class="col-sm-8  vd_input-wrapper no-padding ">
                                                            <span class="menu-icon"><i class="fa  fa-money"></i></span>

                                                            <%-- <asp:TextBox ID="txtParentTotalIncome" placeholder="Annual Income" runat="server"
                                                                                            CssClass="form-control-blue " onkeyup="CheckDecimalValue(event,this);"></asp:TextBox>--%>
                                                            <input type="text" id="txt_ParentTotalIncome" class="form-control-blue" placeholder="Parent's Total Income" />



                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12   mgbt-xs-15">
                                                        <label class="col-sm-4  no-padding  control-label">Upload Group Photo</label>
                                                        <div class="col-sm-8   no-padding">

                                                            <%--<asp:FileUpload ID="FileUpload3" runat="server"
                                                                                    CssClass="form-control-blue "
                                                                                    onchange="checksFileSizeandFileTypeinupdatePanel(this, 100000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','imgGroupPhoto',
                                                                                    'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdbase64groupPhoto');"
                                                                                    type="file" />--%>

                                                            <input type="file" id="GroupPhotoUpload" class="form-control-blue" />
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12   mgbt-xs-15">
                                                        <div class="col-sm-4  no-padding">
                                                        </div>
                                                        <div class="col-sm-8  no-padding">
                                                            <div class="col-sm-12  set-box-in-center no-padding">
                                                                <div class="group-pic-box">
                                                                    <div class="group-pic-box-main">
                                                                        <%--<asp:Label ID="lblGroupImageName" runat="server" Visible="False"></asp:Label>
                                                                                            <asp:Label ID="lblGroupImageUrl" runat="server" Visible="False"></asp:Label>--%>
                                                                        <span id="lblGroupImageName" class="hide"></span>
                                                                        <span id="lblGroupImageUrl" class="hide"></span>
                                                                        <%--<asp:Image ID="imgGroupPhoto" runat="server" Class="imgGroupPhoto" Height="475" ImageUrl="../img/user-pic/group-photo.jpg" />--%>
                                                                        <img src="../img/user-pic/group-photo.jpg" class="imgGroupPhoto" height="475" />
                                                                        <%--<asp:HiddenField ID="hdbase64groupPhoto" runat="server" />--%>
                                                                        <input type="hidden" id="hdbase64groupPhoto" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>



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
                                                    <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
                                                        class=" menu entypo-icon">
                                                        <i class="icon-minus3"></i>
                                                    </div>

                                                </div>
                                                <!-- vd_panel-menu -->

                                            </div>
                                            <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                <div class="row">



                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Select Relationship&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <%--<asp:DropDownList ID="DrpRelationship" runat="server" CssClass="form-control-blue validatedrp"
                                                                                    AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="DrpRelationship_SelectedIndexChanged">
                                                                                    <asp:ListItem Text="<-- Select Relationship -->"></asp:ListItem>
                                                                                    <asp:ListItem Text="Father"></asp:ListItem>
                                                                                    <asp:ListItem Text="Mother"></asp:ListItem>
                                                                                    <asp:ListItem Text="Grand Father"></asp:ListItem>
                                                                                    <asp:ListItem Text="Grand Mother"></asp:ListItem>
                                                                                    <asp:ListItem Text="Brother"></asp:ListItem>
                                                                                    <asp:ListItem Text="Sister"></asp:ListItem>
                                                                                    <asp:ListItem Text="Uncle"></asp:ListItem>
                                                                                    <asp:ListItem Text="Aunty"></asp:ListItem>
                                                                                    <asp:ListItem Text="Others"></asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                            <select id="ddlRelationship" class="form-control-blue">
                                                                <option value=""><-- Select Relationship --></option>
                                                                <option value="Father"></option>
                                                                <option value="Mother"></option>
                                                                <option value="Grand Father"></option>
                                                                <option value="Grand Mother"></option>
                                                                <option value="Brother"></option>
                                                                <option value="Sister"></option>
                                                                <option value="Uncle"></option>
                                                                <option value="Aunty"></option>
                                                                <option value="Others"></option>
                                                            </select>
                                                            <div class="text-box-msg"></div>

                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Guardian Name&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-user"></i></span>

                                                            <%--<asp:TextBox ID="txtguardianname" placeholder="" runat="server"
                                                                                            CssClass="form-control-blue validatetxt"></asp:TextBox>--%>
                                                            <input type="text" id="txt_guardianname" class="form-control-blue" placeholder="Guardian Name" />
                                                            <div class="text-box-msg"></div>


                                                        </div>
                                                    </div>

                                                    <div class="col-md-4 col-sm-6 mgbt-xs-15" style="display: none">
                                                        <label class="control-label">Annual Income</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa  fa-money"></i></span>


                                                            <%-- <asp:TextBox ID="txtfailyincome" runat="server" onkeyup="CheckDecimalValue(event,this);"
                                                                                            CssClass="textbox form-control-blue">0</asp:TextBox>--%>
                                                            <input type="text" id="txt_failyincome" class="form-control-blue" placeholder="Annual Income" value="0" />



                                                        </div>
                                                    </div>



                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Country</label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="drpG1Country" runat="server"
                                                                                            AutoPostBack="true" OnSelectedIndexChanged="drpG1Country_SelectedIndexChanged"
                                                                                            CssClass="form-control-blue ">
                                                                                            <asp:ListItem Text="<-- Select Country -->"></asp:ListItem>
                                                                                        </asp:DropDownList>--%>
                                                            <select id="ddlG1Country" class="form-control-blue">
                                                                <option value=""><-- Select Country --></option>
                                                            </select>
                                                            <div class="text-box-msg"></div>




                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">State</label>
                                                        <div class="">

                                                            <%-- <asp:DropDownList ID="drpG1State" runat="server"
                                                                                            AutoPostBack="True" CssClass="form-control-blue" OnSelectedIndexChanged="drpG1State_SelectedIndexChanged">
                                                                                        </asp:DropDownList>--%>
                                                            <select id="ddlG1State" class="form-control-blue">
                                                            </select>
                                                            <div class="text-box-msg"></div>


                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">City</label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="drpG1City" runat="server" AutoPostBack="True" CssClass="form-control-blue ">
                                                                                        </asp:DropDownList>--%>
                                                            <select id="ddlG1City" class="form-control-blue">
                                                            </select>
                                                            <div class="text-box-msg"></div>



                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Pin Code</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-road"></i></span>


                                                            <%--<asp:TextBox ID="txtG1Pin" placeholder="" runat="server"
                                                                                            CssClass="form-control-blue"
                                                                                            onchange="ValidatorUpdateDisplay
                                                                                                (ContentPlaceHolder1_ContentPlaceHolderMainBox_RegularExpressionValidator8)"></asp:TextBox>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8"
                                                                                                CssClass="imp" runat="server" ControlToValidate="txtG1Pin"
                                                                                                ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[0-9]{6,6}$"
                                                                                                ValidationGroup="A" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                                                        </div>--%>
                                                            <input type="text" id="txt_G1Pin" class="form-control-blue" placeholder="PIN Code" />




                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6  half-width-50 ">
                                                        <div class="col-sm-12  no-padding mgbt-xs-10">
                                                            <label class="control-label">Email&nbsp;</label>
                                                            <div class="vd_input-wrapper controls ">
                                                                <span class="menu-icon"><i class="fa fa-envelope"></i></span>


                                                                <%-- <asp:TextBox ID="txtemailfamily" placeholder="" runat="server"
                                                                                                CssClass="form-control-blue"></asp:TextBox>--%>
                                                                <input type="text" id="txt_emailfamily" class="form-control-blue" placeholder="Email" />

                                                                <div class="text-box-msg"></div>


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


                                                                <%--<asp:TextBox ID="txtcontactNo" runat="server"
                                                                                                CssClass=" form-control-blue validatetxt"></asp:TextBox>--%>
                                                                <input type="text" id="txt_contactNo" class="form-control-blue" placeholder="Mobile No." />

                                                                <div class="text-box-msg"></div>


                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12  no-padding mgbt-xs-5">
                                                            <%--<asp:CheckBox ID="chkGuaMobile" runat="server" Text="Tick for SMS Alert"
                                                                                    CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="Flow" />--%>
                                                            <label class="checkbox-inline">
                                                                <input type="checkbox" id="chkGuaMobile" class="vd_checkbox checkbox-success">Tick for SMS Alert
                                                            </label>
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <%--<div class="col-sm-12  mgbt-xs-10">
                                                                                <h4>Local Guardian 1 Address</h4>
                                                                            </div>--%>
                                                    <div class="col-md-6 mgbt-xs-15" style="display: none">
                                                        <div class=" controls">


                                                            <%--<asp:CheckBoxList ID="CheckBox2" runat="server"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="CheckBox2_CheckedChanged"
                                                                                            class="vd_checkbox checkbox-success" TextAlign="Right"
                                                                                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                            <asp:ListItem>Same as Student Present Address</asp:ListItem>
                                                                                        </asp:CheckBoxList>--%>
                                                            <label class="checkbox-inline">
                                                                <input type="checkbox" id="chkSameAddress" class="vd_checkbox checkbox-success">Same as Student Present Address
                                                            </label>


                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12  ">
                                                        <label class="control-label">Local Guardian Address</label>
                                                        <div class="">

                                                            <%--<asp:TextBox ID="txtG1Address" placeholder="" runat="server"
                                                                                            TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <textarea id="txt-G1Address" class="form-control-blue" placeholder="Write here.."></textarea>
                                                            <div class="text-box-msg"></div>


                                                        </div>
                                                    </div>



                                                    <div class="col-md-4 mgbt-xs-15" style="display: none">
                                                        <label class="control-label">Phone No.</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-phone"></i></span>


                                                            <%--<asp:TextBox ID="txtG1PhoneNo" runat="server"
                                                                                            CssClass="form-control-blue" MaxLength="10" placeholder=""></asp:TextBox>--%>
                                                            <input type="text" id="txt_G1PhoneNo" class="form-control-blue" placeholder="Phone No." />



                                                        </div>
                                                    </div>

                                                    <div class="col-md-4 " style="display: none">
                                                        <label class="control-label">Mobile No.</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-mobile"></i></span>

                                                            <%--<asp:TextBox ID="txtG1MobileNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                                                            CssClass="imp" runat="server" ControlToValidate="txtG1MobileNo"
                                                                                            ErrorMessage="*" SetFocusOnError="True" ValidationExpression="^[0-9]{10,10}$"
                                                                                            ValidationGroup="A"></asp:RegularExpressionValidator>--%>
                                                            <input type="text" id="txt_G1MobileNo" class="form-control-blue" placeholder="Mobile No." />



                                                        </div>
                                                    </div>




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



                                                    <div class="col-md-4 col-sm-6 mgbt-xs-15">
                                                        <label class="control-label">Select Relationship&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="vd_input-wrapper controls ">

                                                            <%-- <asp:DropDownList ID="drpGuardiantwoRelationship" runat="server"
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
                                                                                </asp:DropDownList>--%>
                                                            <select id="ddlGuardiantwoRelationship" class="form-control-blue">
                                                                <option value=""><-- Select Relationship --></option>
                                                                <option value="Father"></option>
                                                                <option value="Mother"></option>
                                                                <option value="Grand Father"></option>
                                                                <option value="Grand Mother"></option>
                                                                <option value="Brother"></option>
                                                                <option value="Sister"></option>
                                                                <option value="Uncle"></option>
                                                                <option value="Aunty"></option>
                                                                <option value="Others"></option>
                                                            </select>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-4 col-sm-6 mgbt-xs-15">
                                                        <label class="control-label">Name&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-user"></i></span>

                                                            <%--<asp:TextBox ID="txtGuardiantwoName" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_GuardiantwoName" class="form-control-blue" placeholder="Name" />



                                                        </div>
                                                    </div>

                                                    <div class="col-md-4 col-sm-6 mgbt-xs-15">
                                                        <label class="control-label">Annual Income</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-envelope"></i></span>


                                                            <%--<asp:TextBox ID="txtGuardiantwoIncomeMonthly" runat="server"
                                                                                            CssClass="form-control-blue" onkeyup="CheckDecimalValue(event,this);">0</asp:TextBox>--%>
                                                            <input type="text" id="txt_GuardiantwoIncomeMonthly" class="form-control-blue" value="0" />



                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 col-sm-6 mgbt-xs-15">
                                                        <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-envelope"></i></span>


                                                            <%--<asp:TextBox ID="txtGuardiantwoContact" runat="server" CssClass="form-control-blue" MaxLength="10"></asp:TextBox>--%>
                                                            <input type="text" id="txt_GuardiantwoContact" class="form-control-blue" placeholder="Contact No." />



                                                        </div>
                                                    </div>


                                                    <div class="col-md-8 mgbt-xs-15">
                                                        <label class="control-label">Email</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-envelope"></i></span>


                                                            <%--<asp:TextBox ID="txtGuardiantwoEmail" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_GuardiantwoEmail" class="form-control-blue" placeholder="Email" />



                                                        </div>
                                                    </div>

                                                    <div class="col-md-6 mgbt-xs-15">
                                                        <h4>Local Guardian 2 Address</h4>
                                                    </div>
                                                    <div class="col-md-6 mgbt-xs-15">
                                                        <div class=" controls">


                                                            <%--<asp:CheckBoxList ID="CheckBox3" runat="server" AutoPostBack="True"
                                                                                            OnSelectedIndexChanged="CheckBox3_CheckedChanged" class="vd_checkbox checkbox-success"
                                                                                            TextAlign="Right" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                            <asp:ListItem>Same as Student's Present Address</asp:ListItem>
                                                                                        </asp:CheckBoxList>--%>
                                                            <label class="checkbox-inline">
                                                                <input type="checkbox" id="chksameArre2" value="">Same as Student's Present Address
                                                            </label>


                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 vd_input-margin20">
                                                        <div class="vd_input-wrapper ">


                                                            <%-- <asp:TextBox ID="txtG2Address" placeholder="Address" runat="server" TextMode="MultiLine"
                                                                                            Rows="3" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <textarea id="txt_G2Address" class="form-control-blue" placeholder="Address"></textarea>


                                                        </div>
                                                    </div>

                                                    <div class="col-md-4 mgbt-xs-15">
                                                        <div class="vd_input-wrapper ">

                                                            <%--<asp:DropDownList ID="drpG2Country" runat="server" AutoPostBack="true"
                                                                                            OnSelectedIndexChanged="drpG2Country_SelectedIndexChanged"
                                                                                            CssClass="form-control-blue ">
                                                                                            <asp:ListItem Text="<-- Select Country -->"></asp:ListItem>
                                                                                        </asp:DropDownList>--%>
                                                            <select id="ddlG2Country" class="form-control-blue">
                                                                <option value=""><-- Select Country --></option>
                                                            </select>


                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 mgbt-xs-15">
                                                        <div class="vd_input-wrapper ">


                                                            <%--<asp:DropDownList ID="drpG2State" runat="server" AutoPostBack="True"
                                                                                            CssClass="form-control-blue" OnSelectedIndexChanged="drpG2State_SelectedIndexChanged">
                                                                                        </asp:DropDownList>--%>
                                                            <select id="ddlG2State" class="form-control-blue">
                                                            </select>



                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 mgbt-xs-15">
                                                        <div class="vd_input-wrapper ">


                                                            <%--<asp:DropDownList ID="drpG2City" runat="server" AutoPostBack="True" CssClass="form-control-blue ">
                                                                                        </asp:DropDownList>--%>
                                                            <select id="ddlG2City" class="form-control-blue">
                                                            </select>



                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 mgbt-xs-15">
                                                        <label class="control-label">Phone No.</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-phone"></i></span>


                                                            <%--<asp:TextBox ID="txtG2PhoneNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_G2PhoneNo" placeholder="Phone No." class="form-control-blue" />


                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 ">
                                                        <label class="control-label">Mobile No.</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-mobile"></i></span>


                                                            <%--<asp:TextBox ID="txtG2MobileNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                                                        <asp:RegularExpressionValidator ID="G2RegularExpressionValidator7"
                                                                                            CssClass="imp" runat="server" ControlToValidate="txtG2MobileNo" ErrorMessage="*"
                                                                                            SetFocusOnError="True"
                                                                                            ValidationExpression="^[0-9]{10,10}$" ValidationGroup="A"></asp:RegularExpressionValidator>--%>
                                                            <input type="text" id="txt_G2MobileNo" placeholder="Mobile No." class="form-control-blue" />



                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 ">
                                                        <label class="control-label">Pin Code</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-road"></i></span>


                                                            <%-- <asp:TextBox ID="txtG2Pin" placeholder="" runat="server" CssClass="form-control-blue"
                                                                                            onchange="ValidatorUpdateDisplay(ContentPlaceHolder1_ContentPlaceHolderMainBox_RegularExpressionValidator8)">

                                                                                        </asp:TextBox>
                                                                                        <asp:RegularExpressionValidator ID="G2RegularExpressionValidator8"
                                                                                            CssClass="imp" runat="server" ControlToValidate="txtG2Pin" ErrorMessage="*"
                                                                                            SetFocusOnError="True" ValidationExpression="^[0-9]{6,6}$"
                                                                                            ValidationGroup="A" EnableClientScript="true"></asp:RegularExpressionValidator>--%>
                                                            <input type="text" id="txt_G2Pin" placeholder="PIN Code" class="form-control-blue" />



                                                        </div>
                                                    </div>


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
                                                    <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
                                                        class=" menu entypo-icon">
                                                        <i class="icon-minus3"></i>
                                                    </div>

                                                </div>
                                                <!-- vd_panel-menu -->

                                            </div>
                                            <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                <div class="row">



                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Date of Admission&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                            <%-- <asp:TextBox ID="TextBox100" placeholder="" runat="server"
                                                                                    CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>--%>
                                                            <input type="text" id="txt_G2dateOfadmmission" placeholder="PIN Code" class="form-control-blue" />

                                                            <div class="text-box-msg"></div>

                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Select Course&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="DropCourse" runat="server"
                                                                                            CausesValidation="True" CssClass="form-control-blue validatedrp"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="DropCourse_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15"
                                                                                                runat="server" ControlToValidate="DropCourse" ErrorMessage="*"
                                                                                                SetFocusOnError="True" Style="color: #CC0000; font-weight: 700"
                                                                                                ValidationGroup="A" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                        </div>--%>
                                                            <select id="ddlCourse" class="form-control-blue">
                                                            </select>


                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="DropAdmissionClass" runat="server" AutoPostBack="True"
                                                                                            CssClass="form-control-blue validatedrp"
                                                                                            OnSelectedIndexChanged="DropAdmissionClass_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                                                ControlToValidate="DropAdmissionClass" ErrorMessage="*"
                                                                                                SetFocusOnError="True" Style="color: #CC0000; font-weight: 700"
                                                                                                ValidationGroup="A" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                        </div>--%>
                                                            <select id="ddlAdmissionClass" class="form-control-blue">
                                                            </select>


                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">

                                                            <%-- <asp:DropDownList ID="drpSection" runat="server" CausesValidation="false"
                                                                                            CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" AutoPostBack="true">
                                                                                        </asp:DropDownList>--%>
                                                            <select id="ddlSection" class="form-control-blue">
                                                            </select>
                                                            <div class="text-box-msg"></div>


                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Select Branch&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="DropBranch" runat="server" CausesValidation="True"
                                                                                            CssClass="form-control-blue validatedrp"
                                                                                            OnSelectedIndexChanged="DropBranch_SelectedIndexChanged"
                                                                                            AutoPostBack="true">
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                                                                                runat="server" ControlToValidate="DropBranch" ErrorMessage="*"
                                                                                                SetFocusOnError="True" Style="color: #CC0000; font-weight: 700"
                                                                                                ValidationGroup="A" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                        </div>--%>
                                                            <select id="ddlBranch" class="form-control-blue">
                                                            </select>


                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Select Stream&nbsp;<span class="vd_red"></span></label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="DropStream" runat="server" CausesValidation="false"
                                                                                            CssClass="form-control-blue">
                                                                                        </asp:DropDownList>--%>
                                                            <select id="ddlStream" class="form-control-blue">
                                                            </select>
                                                            <div class="text-box-msg"></div>


                                                        </div>
                                                    </div>



                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Select Medium&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="drpMedium" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                                                                                ControlToValidate="drpMedium" ErrorMessage="*" SetFocusOnError="True"
                                                                                                Style="color: #CC0000; font-weight: 700" ValidationGroup="A"
                                                                                                InitialValue="<--Select-->"></asp:RequiredFieldValidator>
                                                                                        </div>--%>
                                                            <select id="ddlMedium" class="form-control-blue">
                                                            </select>



                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Board/ University&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="DrpBoard" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="DrpBoard"
                                                                                                ErrorMessage="*" SetFocusOnError="True" Style="color: #CC0000; font-weight: 700"
                                                                                                ValidationGroup="A" InitialValue="<--Select-->"></asp:RequiredFieldValidator>
                                                                                        </div>--%>
                                                            <select id="ddlBoard" class="form-control-blue">
                                                            </select>



                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Type of Admission&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <%-- <asp:DropDownList ID="DrpNEWOLSAdmission" runat="server" CssClass="form-control-blue">
                                                                                    <asp:ListItem Selected="True">New</asp:ListItem>
                                                                                    <asp:ListItem>Old</asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                            <select id="ddlNEWOLSAdmission" class="form-control-blue">
                                                                <option value="New" selected="selected">New</option>
                                                                <option value="Old">Old</option>
                                                            </select>
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class=" col-sm-4  half-width-50 mgbt-xs-15 ">
                                                        <label class=" control-label">Type of Education&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="mgtp-6">
                                                            <%--<asp:RadioButtonList ID="RadioButtonList3"
                                                                                    runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                                                    <asp:ListItem Value="Yes" Selected="True"> Regular </asp:ListItem>
                                                                                    <asp:ListItem Value="No"> Private </asp:ListItem>
                                                                                </asp:RadioButtonList>--%>

                                                            <label class="radio-inline">
                                                                <input type="radio" name="optradio" class="vd_radio radio-success" value="Yes" checked="checked">Regular
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="optradio" class="vd_radio radio-success" value="No">Private

                                                            </label>
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>





                                                    <div class="col-md-12" id="divopt" runat="server" visible="false">
                                                        <div class="vd_input-wrapper ">
                                                            <label class="col-sm-3 no-padding control-label">Opt. Subject</label>
                                                            <div class="col-sm-9 mgbt-xs-15 no-padding controls">

                                                                <%--<asp:CheckBoxList ID="rbOptionalSubject" runat="server" RepeatDirection="Horizontal"
                                                                                        RepeatLayout="Flow" CssClass="vd_checkbox checkbox-success">
                                                                                    </asp:CheckBoxList>--%>
                                                                <label class="checkbox-inline">
                                                                    <input type="checkbox" id="rbOptionalSubject" class="vd_checkbox checkbox-success" value="">Option 1
                                                                </label>

                                                            </div>
                                                        </div>
                                                    </div>






                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                                        <label class=" control-label">Scholarship&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="mgtp-6">

                                                            <%--<asp:RadioButtonList ID="rbScholarship" onclick="onclickeds(this);" runat="server"
                                                                                            CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                                                            <asp:ListItem>Yes</asp:ListItem>
                                                                                            <asp:ListItem Selected="True">No</asp:ListItem>
                                                                                        </asp:RadioButtonList>--%>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="radioScholarship" value="Yes" checked="checked">Yes
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="radioScholarship" value="No" checked="checked">No
                                                            </label>


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


                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 " style="display: none">
                                                        <label class=" control-label">Hostel Required&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="mgtp-6">
                                                            <%--<asp:RadioButtonList ID="rbHostel" runat="server" CssClass="vd_radio radio-success"
                                                                                    RepeatDirection="Horizontal" RepeatLayout="flow"
                                                                                    onchange="visibleFalseTableColumn('ContentPlaceHolder1_ContentPlaceHolderMainBox_drpHostalMODB',this)">
                                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="No" Selected="True">No</asp:ListItem>
                                                                                </asp:RadioButtonList>--%>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="radioHostel" value="Yes" checked="checked">Yes
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="radioHostel" value="No" checked="checked">No
                                                            </label>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="drpHostalMODB" runat="server" style="display: none">
                                                        <label class=" control-label">Payment Frequency&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <%--<asp:DropDownList ID="drpHostalMOD" runat="server" CssClass="form-control-blue">
                                                                                    <%--  <asp:ListItem><-- Mode of Fee Deposit --></asp:ListItem>
                                                                                    <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                                    <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                                                    <asp:ListItem Value="S">Semester</asp:ListItem>
                                                                                    <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                            <select id="ddlHostalMOD" class="form-control-blue">
                                                                <option value="I" selected="selected">Installment</option>
                                                                <option value="A">Annual</option>
                                                            </select>
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 " style="display: none">
                                                        <label class=" control-label">Library Facility&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="mgtp-6">
                                                            <%--<asp:RadioButtonList ID="rbLibrary" runat="server" CssClass="vd_radio radio-success"
                                                                                    RepeatDirection="Horizontal" RepeatLayout="flow"
                                                                                    onchange="visibleFalseTableColumn('ContentPlaceHolder1_ContentPlaceHolderMainBox_drpLibraryMODB',this)">
                                                                                    <asp:ListItem>Yes</asp:ListItem>
                                                                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                                                                </asp:RadioButtonList>--%>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="radioLibrary" value="Yes" checked="checked">Yes
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="radioLibrary" value="No" checked="checked">No
                                                            </label>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="drpLibraryMODB" runat="server" style="display: none">
                                                        <label class=" control-label">Payment Frequency&nbsp;<span class="vd_red">*</span></label>
                                                        <div class=" ">
                                                            <%--<asp:DropDownList ID="drpLibraryMOD" runat="server" CssClass="form-control-blue ">
                                                                                   <asp:ListItem><-- Mode of Fee Deposit --></asp:ListItem>
                                                                                    <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                                   <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                                                    <asp:ListItem Value="S">Semester</asp:ListItem>
                                                                                    <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                                </asp:DropDownList>--%>

                                                            <select id="ddlLibraryMOD" class="form-control-blue">
                                                                <option value="I" selected="selected">Installment</option>
                                                                <option value="A">Annual</option>
                                                            </select>
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 " style="display: none">
                                                        <label class=" control-label">Transport Required &nbsp;<span class="vd_red">*</span></label>
                                                        <div class="mgtp-6">
                                                            <%-- <asp:RadioButtonList ID="rbTransport" runat="server" CssClass="vd_radio radio-success"
                                                                                    RepeatDirection="Horizontal" RepeatLayout="flow"
                                                                                    onchange="visibleFalseTableColumn('ContentPlaceHolder1_ContentPlaceHolderMainBox_drpTransportMODB',this)">
                                                                                    <asp:ListItem>Yes</asp:ListItem>
                                                                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                                                                </asp:RadioButtonList>--%>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="radioTransport" value="Yes" checked="checked">Yes
                                                            </label>
                                                            <label class="radio-inline">
                                                                <input type="radio" name="radioTransport" value="No" checked="checked">No
                                                            </label>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="drpTransportMODB" runat="server" style="display: none">
                                                        <label class=" control-label">Payment Frequency&nbsp;<span class="vd_red">*</span></label>
                                                        <div class=" ">
                                                            <%--<asp:DropDownList ID="drpTransportMOD" runat="server" CssClass="form-control-blue">
                                                                                     <asp:ListItem><-- Mode of Fee Deposit --></asp:ListItem>
                                                                                    <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                                     <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                                                    <asp:ListItem Value="S">Semester</asp:ListItem>
                                                                                    <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                            <select id="ddlTransportMOD" class="form-control-blue">
                                                                <option value="I" selected="selected">Installment</option>
                                                                <option value="A">Annual</option>
                                                            </select>
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>




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
                                                    <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
                                                        class=" menu entypo-icon">
                                                        <i class="icon-minus3"></i>
                                                    </div>

                                                </div>
                                                <!-- vd_panel-menu -->

                                            </div>
                                            <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                <div class="row">



                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                        <label class="control-label">Enquiry No. </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtEnquiryNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_EnquiryNo" class="form-control-blue" placeholder="Enquiry No." />
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">S.R. No.&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtSr" placeholder="" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>--%>
                                                            <input type="text" id="txt_SrNo" class="form-control-blue" placeholder="S.R. No." />
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Card No. </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtCardNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_CardNo" class="form-control-blue" placeholder="Card No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Board/ University Roll No.</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtUniversityBoardRollNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_UniversityBoardRollNo" class="form-control-blue" placeholder="Board/ University Roll No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Institute Roll No. </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtSchoolcollegeRollno" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_SchoolcollegeRollno" class="form-control-blue" placeholder="Institute Roll No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">File No. </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtfileno" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_fileno" class="form-control-blue" placeholder="File No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                        <label class="control-label">Reference </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtReferences" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_References" class="form-control-blue" placeholder="Reference" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Payment Frequency&nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <%--<asp:DropDownList ID="drpFeeDepositMOD" runat="server" CssClass="form-control-blue">
                                                                                    <asp:ListItem Value="I">Installment</asp:ListItem>
                                                                                     <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                                                                    <asp:ListItem Value="S">Semester</asp:ListItem>
                                                                                    <asp:ListItem Value="A">Annual</asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                            <select id="ddlFeeDepositMOD" class="form-control-blue">
                                                                <option value="I" selected="selected">Installment</option>
                                                                <option value="A">Annual</option>
                                                            </select>
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Fee Category &nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="drpPanelCardType" runat="server"
                                                                                            CssClass="form-control-blue validatedrp">
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                                                                                ControlToValidate="drpPanelCardType" ErrorMessage="*"
                                                                                                SetFocusOnError="True" Style="color: #CC0000; font-weight: 700"
                                                                                                ValidationGroup="A" InitialValue="<--Select-->"></asp:RequiredFieldValidator>
                                                                                        </div>--%>
                                                            <select id="ddlPanelCardType" class="form-control-blue">
                                                                <option value="I" selected="selected">Installment</option>
                                                                <option value="A">Annual</option>
                                                            </select>


                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                        <label class="control-label">Select SMS Acknowledgment To &nbsp;<span class="vd_red">*</span> </label>
                                                        <div class="">
                                                            <%-- <asp:DropDownList ID="drpSMSAcknowledgmentTo" runat="server"
                                                                                    CssClass="form-control-blue">
                                                                                    <asp:ListItem Text="<-- Select SMS Acknowledgment To -->"></asp:ListItem>
                                                                                    <asp:ListItem Text="Gaurdian 1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Gaurdian 2"></asp:ListItem>
                                                                                    <asp:ListItem Text="Both"></asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                            <select id="ddlSMSAcknowledgmentTo" class="form-control-blue">
                                                                <option value="" selected="selected"><-- Select SMS Acknowledgment To --></option>
                                                                <option value="G1">Gaurdian 1</option>
                                                                <option value="G2">Gaurdian 2</option>
                                                                <option value="Both">Both</option>
                                                            </select>
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                        <label class="control-label">Select Email Acknowledgment To &nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <%--<asp:DropDownList ID="drpEmailAcknowledgmentTo" runat="server"
                                                                                    CssClass="form-control-blue">
                                                                                    <asp:ListItem Text="<-- Select Email Acknowledgment To -->"></asp:ListItem>
                                                                                    <asp:ListItem Text="Gaurdian 1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Gaurdian 2"></asp:ListItem>
                                                                                    <asp:ListItem Text="Both"></asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                            <select id="ddlEmailAcknowledgmentTo" class="form-control-blue">
                                                                <option value="" selected="selected"><-- Select Email Acknowledgment To --></option>
                                                                <option value="G1">Gaurdian 1</option>
                                                                <option value="G2">Gaurdian 2</option>
                                                                <option value="Both">Both</option>
                                                            </select>
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>


                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Select House Name &nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">

                                                            <%--<asp:DropDownList ID="DropDownList4" runat="server"
                                                                                            CssClass="form-control-blue validatedrp">
                                                                                        </asp:DropDownList>
                                                                                        <div class="text-box-msg">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server"
                                                                                                ControlToValidate="DropDownList4" ErrorMessage="*" SetFocusOnError="True"
                                                                                                Style="color: #CC0000; font-weight: 700" ValidationGroup="A"
                                                                                                InitialValue="<--Select-->"></asp:RequiredFieldValidator>
                                                                                        </div>--%>
                                                            <select id="ddlHouseName" class="form-control-blue">
                                                            </select>


                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Remark </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                            <%--<asp:TextBox ID="txtrema" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_remark" class="form-control-blue" placeholder="Remark" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Admission done at </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-tag"></i></span>
                                                            <%--<asp:TextBox ID="txtAddDoneat" placeholder="INR (Rupees)" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_AddDoneat" class="form-control-blue" placeholder="INR (Rupees)" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>




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
                                                    <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
                                                        class=" menu entypo-icon">
                                                        <i class="icon-minus3"></i>
                                                    </div>

                                                </div>
                                                <!-- vd_panel-menu -->

                                            </div>
                                            <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                <div class="row">



                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Date of First Admission</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtDFA" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>--%>
                                                            <input type="text" id="txt_dateOfAddm" class="form-control-blue datepicker-normal" placeholder="INR (Rupees)" />
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Course of First Admission</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtCFA" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_courseOfAddm" class="form-control-blue" placeholder="Course of First Admission" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Class of First Admission</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtCOFA" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_classOfAddm" class="form-control-blue" placeholder="Class of First Admission" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Branch of First Admission</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtSFA" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_branchOfAddm" class="form-control-blue" placeholder="Branch of First Admission" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>



                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="list-tab2">

                                <div class="row mgbt-xs-20">

                                    <div class="col-sm-12  full-width-100">
                                        <div class="panel widget">
                                            <div class="panel-heading vd_bg-dark-blue">
                                                <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-file-archive-o"></i></span>&nbsp; Document </h3>
                                                <div class="vd_panel-menu ">
                                                    <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
                                                        class=" menu entypo-icon">
                                                        <i class="icon-minus3"></i>
                                                    </div>

                                                </div>
                                                <!-- vd_panel-menu -->

                                            </div>
                                            <div class="panel-body padding-tlbr-15 form-main-box-border">
                                                <div class="row">
                                                    <div class="col-sm-12  no-padding ">
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                            <span id="lblDocument_1" class="control-label">BIRTH CERTIFICATE</span>
                                                            <div class="controls img-input-ped">
                                                                <input type="file" name="" id="FileUpload1_1" class="form-control-blue ">
                                                                <div class="text-box-msg">
                                                                    <input type="hidden" name="" id="hfFile_1">
                                                                    <input type="hidden" name="" id="hdfilefileExtention_1">
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 btn-a-devices-1-p4-p2 mgbt-xs-15 ">
                                                            <div class="col-sm-4 col-xs-6 mgtp-6">
                                                                <span class="vd_checkbox checkbox-success" style="text-transform: uppercase;">
                                                                    <input id="Chksoft_1" type="checkbox" name=""><label for="Chksoft_1">Softcopy</label></span>
                                                            </div>
                                                            <div class="col-sm-4 col-xs-6 mgtp-6">
                                                                <span class="vd_checkbox checkbox-success" style="text-transform: uppercase;">
                                                                    <input id="Chkhard_1" type="checkbox" name=""><label for="Chkhard_1">Hardcopy</label></span>
                                                            </div>
                                                            <div class="col-sm-4 col-xs-6 mgtp-6">
                                                                <span class="vd_checkbox checkbox-success" style="text-transform: uppercase;">
                                                                    <input id="Chkverified_1" type="checkbox" name=""><label for="Chkverified_1">Verified</label></span>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                                            <label class="control-label">Remark</label>
                                                            <div class="vd_input-wrapper ">
                                                                <textarea name="txt_Remark_1" rows="1" cols="20" id="txt_Remark_1" class="form-control-blue" placeholder="" style="text-transform: uppercase;"></textarea>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12  no-padding ">
                                                        <hr>
                                                    </div>
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
                                                                  <%--<asp:Label ID="lblsrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>--%>
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


                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Name of Entrance</label>
                                                        <div class="">
                                                            <%-- <asp:DropDownList onchange="displayTextbox();" ID="drpExamCrackedof" runat="server"
                                                                                    CssClass="form-control-blue ">
                                                                                </asp:DropDownList>--%>
                                                            <select id="ddlExamCrackedof" class="form-control-blue"></select>
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Roll No.</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                            <%--<asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_RollNo" class="form-control-blue" placeholder="Roll No." />
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Rank</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                            <input type="text" id="txt_Rank" class="form-control-blue" placeholder="Rank" />
                                                            <%--<asp:TextBox ID="txtRank" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Category Rank</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                            <%--<asp:TextBox ID="txtCategoryRank" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_CategoryRank" class="form-control-blue" placeholder="Category Rank" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Any Other</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-bookmark"></i></span>
                                                            <%--<asp:TextBox ID="txtAnyOtherCategoryRank" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_AnyOtherCategoryRank" class="form-control-blue" placeholder="Any Other" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                        <!-- Panel Widget -->
                                    </div>
                                    <asp:Repeater ID="rptPreviousEducation" runat="server">
                                        <ItemTemplate>
                                            <div class="col-sm-6  full-width-100">
                                                <div class="panel widget">
                                                    <div class="panel-heading vd_bg-dark-blue">
                                                        <h3 class="panel-title"><span class="menu-icon"><i class=" icon-graduation"></i></span>Previous Education Details
                                                                  <%--<asp:Label ID="lblsrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>--%>
                                                        </h3>
                                                        <div class="vd_panel-menu ">
                                                            <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
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
                                                                    <%--<asp:TextBox ID="txtExam" Text='<%# Bind("Exam") %>'
                                                                                            onkeyup="enableControlNew('rpt',this,'.form-control-blue');"
                                                                                            Enabled="true" runat="server" CssClass="form-control-blue">
                                                                                                <div class="text-box-msg"></div>
                                                                                        </asp:TextBox>--%>
                                                                    <input type="text" id="txt_Exam" class="form-control-blue" placeholder="Name of Examination" />

                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Board/ University</label>
                                                                <div class="">
                                                                    <%--<asp:DropDownList ID="drpBoard" runat="server"
                                                                                            CssClass="form-control-blue " Enabled="false">
                                                                                            <asp:ListItem Text="<-- Select Board/ University -->"></asp:ListItem>
                                                                                        </asp:DropDownList>--%>
                                                                    <select id="ddlBoard" class="form-control-blue">
                                                                        <option value=""><-- Select Board/ University --></option>
                                                                    </select>
                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Select Result</label>
                                                                <div class="">
                                                                    <%--<asp:DropDownList ID="drpResult" runat="server"
                                                                                            CssClass="form-control-blue " Enabled="false">
                                                                                            <asp:ListItem Value="P">Passed</asp:ListItem>
                                                                                            <asp:ListItem Value="F">Failed</asp:ListItem>
                                                                                            <asp:ListItem Value="RA">Result Awaited</asp:ListItem>
                                                                                        </asp:DropDownList>--%>
                                                                    <select id="ddlResult" class="form-control-blue">
                                                                        <option value="P">Passed</option>
                                                                        <option value="F">Failed</option>
                                                                        <option value="RA">Result Awaited</option>
                                                                    </select>
                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Institute Name</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-institution"></i></span>
                                                                    <%-- <asp:TextBox ID="txtInstitute" Enabled="false" Text='<%# Bind("Institute") %>'
                                                                                            runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_Institute" class="form-control-blue" placeholder="Institute Name" />

                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Passing Year</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-trophy"></i></span>
                                                                    <%-- <asp:TextBox ID="txtYop" Text='<%# Bind("Yop") %>' Enabled="false" runat="server"
                                                                                            CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_Yop" class="form-control-blue" placeholder="Passing Year" />

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
                                                                    <select id="ddlMedium" class="form-control-blue">
                                                                        <option value="English" selected="selected">English</option>
                                                                        <option value="Hindi">Hindi</option>
                                                                    </select>
                                                                    <div class="text-box-msg"></div>
                                                                </div>

                                                            </div>

                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Subject</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-book"></i></span>
                                                                    <%--<asp:TextBox ID="txtSubject" Enabled="false" Text='<%# Bind("Subject") %>'
                                                                                            runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_Subject" class="form-control-blue" placeholder="Subject" />

                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Roll No.</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-bullhorn"></i></span>
                                                                    <%--<asp:TextBox ID="txtRollNo" Enabled="false" Text='<%# Bind("RollNo") %>'
                                                                                            runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_RollNo" class="form-control-blue" placeholder="Roll No." />

                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Certificate No.</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="glyphicon glyphicon-certificate"></i></span>
                                                                    <%-- <asp:TextBox ID="txtCertificateNo" Text='<%# Bind("CertificateNo") %>' Enabled="false"
                                                                                            runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_CertificateNo" class="form-control-blue" placeholder="Certificate No." />

                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Marks Sheet No.</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-shield"></i></span>
                                                                    <%--<asp:TextBox ID="txtMarksSheetNo" Text='<%# Bind("MarksSheetNo") %>' Enabled="false"
                                                                                            runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_MarksSheetNo" class="form-control-blue" placeholder="Marks Sheet No." />

                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Max Marks</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-thumbs-up"></i></span>
                                                                    <%--<asp:TextBox ID="txtMM" Text='<%# Bind("MaxMarks") %>'
                                                                                            onkeyup="CheckIntegerValueonKeyUp(event, this);" Enabled="false"
                                                                                            runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_MM" class="form-control-blue" placeholder="Max Marks" />

                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                                <label class="control-label">Obtained Marks</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa  fa-thumbs-o-up"></i></span>
                                                                    <%--<asp:TextBox ID="txtObtained" Text='<%# Bind("Obtained") %>' Enabled="false"
                                                                                            onkeyup="SetPercentage(event,this,
                                                                                                '#ContentPlaceHolder1_ContentPlaceHolderMainBox_rptPreviousEducation_txtPer',
                                                                                                '#ContentPlaceHolder1_ContentPlaceHolderMainBox_rptPreviousEducation_txtMM');"
                                                                                            runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_Obtained" class="form-control-blue" placeholder="Obtained Marks" />

                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 half-width-100-tc mgbt-xs-15">
                                                                <label class="control-label">Percent / Grade</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                    <%--<asp:TextBox ID="txtPer" Text='<%# Bind("Per") %>'
                                                                                            Enabled="false" runat="server"
                                                                                            CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_Per" class="form-control-blue" placeholder="Percent / Grade" />

                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                                <label class="control-label">Country</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                    <%--<asp:TextBox ID="txtCountry" Enabled="false" runat="server"
                                                                                            CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_Country" class="form-control-blue" placeholder="Country" />

                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                                                <label class="control-label">State</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                    <%--<asp:TextBox ID="txtState" Enabled="false" runat="server"
                                                                                            CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_State" class="form-control-blue" placeholder="State" />

                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6 " style="display: none">
                                                                <label class="control-label">City</label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa fa-star"></i></span>
                                                                    <%--<asp:TextBox ID="txtCity" Enabled="false" runat="server"
                                                                                            CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_City" class="form-control-blue" placeholder="City" />

                                                                    <div class="text-box-msg"></div>
                                                                </div>
                                                            </div>


                                                            <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 ">

                                                                <%--<asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click"
                                                                                                CssClass="button form-control-blue" runat="server"> 
                                                                                                        <i class="glyphicon glyphicon-trash"></i> &nbsp; Delete Full Details </asp:LinkButton>--%>
                                                                <button type="button" id="lnkDelete" class="button form-control-blue"><i class="glyphicon glyphicon-trash"></i>&nbsp; Delete Full Details</button>




                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- Panel Widget -->
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>




                                    <div class="col-sm-12  text-center">

                                        <%--<asp:LinkButton ID="lnkAddMore" OnClick="lnkAddMore_Click" CssClass="button form-control-blue" runat="server"> 
                                                                            <i class="fa fa-plus-square"></i> &nbsp; Add Education Details Box </asp:LinkButton>--%>
                                        <button type="button" id="lnkAddMore" class="button form-control-blue"><i class="fa fa-plus-square"></i>&nbsp; Add Education Details Box</button>



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
                                                    <div data-action="minimize" title="Minimize" data-toggle="tooltip" data-placement="bottom"
                                                        class=" menu entypo-icon">
                                                        <i class="icon-minus3"></i>
                                                    </div>
                                                </div>
                                                <!-- vd_panel-menu -->
                                            </div>
                                            <div class="panel-body padding-tlbr-15 form-main-box-border">


                                                <div class="row">



                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Current Year and Duration of Course</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtDuration" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_Duration" class="form-control-blue" placeholder="Duration" />

                                                            <div class="text-box-msg"></div>

                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Registration No.</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtRegistration" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_Registration" class="form-control-blue" placeholder="Registration No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Caste Certificate No. </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtCastCerti" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_CastCerti" class="form-control-blue" placeholder="Caste Certificate No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Date of issue Income Certificate</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                            <%--<asp:TextBox ID="TextBox148" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>--%>
                                                            <input type="text" id="txt_DOIIC" class="form-control-blue datepicker-normal" placeholder="Date of issue Income Certificate" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Income Certificate No. </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtIncomeCerti" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_IncomeCerti" class="form-control-blue" placeholder="Income Certificate No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Date of issue Income Certificate</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                            <%--<asp:TextBox ID="TextBox149" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>--%>
                                                            <input type="text" id="txt_DOIIC2" class="form-control-blue" placeholder="Income Certificate No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Domicile Certificate No.  </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtRegiCer" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_RegiCer" class="form-control-blue" placeholder="Domicile Certificate No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Date of issue Domicile Certificate </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                            <%--<asp:TextBox ID="TextBox150" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>--%>
                                                            <input type="text" id="txt_DOIDC" class="form-control-blue" placeholder="Date of issue Domicile Certificate" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">1st Year Admission Date</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                            <%--<asp:TextBox ID="TextBox151" placeholder=" " runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>--%>
                                                            <input type="text" id="txt_1stYAD" class="form-control-blue datepicker-normal" placeholder="1st Year Admission Date" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Current Year Admission Date</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                            <%--<asp:TextBox ID="TextBox152" placeholder=" " runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>--%>
                                                            <input type="text" id="txt_CYAD" class="form-control-blue datepicker-normal" placeholder="Current Year Admission Date" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Type of Course </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-graduation-cap"></i></span>
                                                            <%--<asp:TextBox ID="txtTypeofCourse" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_TypeofCourse" class="form-control-blue" placeholder="Type of Course" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Type of Admission </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-lightbulb-o"></i></span>
                                                            <%--<asp:TextBox ID="txtTypeofAdmission" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_TypeofAdmission" class="form-control-blue" placeholder="Type of Admission" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Bank Account No.  </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtBankAccNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_BankAccNo" class="form-control-blue" placeholder="Bank Account No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Bank Name </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-bank"></i></span>
                                                            <%--<asp:TextBox ID="txtBankName" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_BankName" class="form-control-blue" placeholder="Bank Name" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Branch Name of Bank  </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-map-marker"></i></span>
                                                            <%--<asp:TextBox ID="txtBranchNameofBank" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_BranchNameofBank" class="form-control-blue" placeholder="Branch Name of Bank" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">IFS Code </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtIfsCode" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_IfsCode" class="form-control-blue" placeholder="IFS Code" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Student Name in Bank Passbook </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                            <%--<asp:TextBox ID="txtStudentNameinPassbook" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_StudentNameinPassbook" class="form-control-blue" placeholder="Student Name in Bank Passbook" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Day Scholar / Hosteller</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-building"></i></span>
                                                            <%--<asp:TextBox ID="txtDayScholer" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_DayScholer" class="form-control-blue" placeholder="Day Scholar / Hosteller" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Yearly None Refundeble Fee</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtYearlynonrefund" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_Yearlynonrefund" class="form-control-blue" placeholder="Yearly None Refundeble Fee" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Handicapped Type </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-wheelchair"></i></span>
                                                            <%--<asp:TextBox ID="txthandycaptype" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_handycaptype" class="form-control-blue" placeholder="Handicapped Type" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Handicapped Percentage  </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-wheelchair"></i></span>
                                                            <%--<asp:TextBox ID="txthandycapPer" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_handycapPer" class="form-control-blue" placeholder="Handicapped Percentage" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Handicapped Compensation</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-wheelchair"></i></span>
                                                            <%--<asp:TextBox ID="txthandycapCompe" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_handycapCompe" class="form-control-blue" placeholder="Handicapped Compensation" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Receipt No. of Deposit Fee</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtReciptNoofDepositFee" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_ReciptNoofDepositFee" class="form-control-blue" placeholder="Receipt No. of Deposit Fee" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Deposit Fee Date  </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                            <%--<asp:TextBox ID="TextBox154" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>--%>
                                                            <input type="text" id="txt_DateofDepositFee" class="form-control-blue datepicker-normal" placeholder="Deposit Fee Date" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Last Year Scholarship Amount  </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtLastYearScholarAmount" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_LastYearScholarAmount" class="form-control-blue" placeholder="Last Year Scholarship Amount" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Last Year Scholarship Deposit Fee </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtLastYearScholarDepoFee" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_LastYearScholarDepoFee" class="form-control-blue" placeholder="Last Year Scholarship Deposit Fee" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Last Year Class / Course</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-graduation-cap"></i></span>
                                                            <%--<asp:TextBox ID="txtLastClass" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_LastClass" class="form-control-blue" placeholder="Last Year Class / Course" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Last Year Exam Result </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-trophy"></i></span>
                                                            <%--<asp:TextBox ID="txtLastYearExamResult" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_LastYearExamResult" class="form-control-blue" placeholder="Last Year Exam Result" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Last Year Exam Total Marks  </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-thumbs-up"></i></span>
                                                            <%--<asp:TextBox ID="txtLastYearExamTatalMarks" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_LastYearExamTatalMarks" class="form-control-blue" placeholder="Last Year Exam Total Marks" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Last Year Exam Total Obtain Marks</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-thumbs-o-up"></i></span>
                                                            <%--<asp:TextBox ID="txtLastYearExamTotalObtainMarks" placeholder="" runat="server"
                                                                                    CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_LastYearExamTotalObtainMarks" class="form-control-blue" placeholder="Last Year Exam Total Obtain Marks" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Scholarship Compensation Amount According to Class / Course  </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-graduation-cap"></i></span>
                                                            <%-- <asp:TextBox ID="txtScholarCompeAmountAccotoClass" placeholder="" runat="server"
                                                                                    CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_ScholarCompeAmountAccotoClass" class="form-control-blue" placeholder="Scholarship Compensation Amount According to Class / Course" />

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
                                                            <%--<asp:TextBox ID="txtIntermediateRollNo" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_IntermediateRollNo" class="form-control-blue" placeholder="Intermediate Roll No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Intermediate Board  </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-tag"></i></span>
                                                            <%--<asp:TextBox ID="txtIntermediateBoard" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_IntermediateBoard" class="form-control-blue" placeholder="Intermediate Board" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Intermediate Passing Year </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-certificate"></i></span>
                                                            <%--<asp:TextBox ID="txtIntermediateYearofPssing" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_IntermediateYearofPssing" class="form-control-blue" placeholder="Intermediate Passing Year" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Is Entry based on Intermediate Marks Score </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-level-up"></i></span>
                                                            <%--<asp:TextBox ID="txtIsEntrybasedonInterMarksScore" placeholder="" runat="server"
                                                                                    CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_IsEntrybasedonInterMarksScore" class="form-control-blue" placeholder="Is Entry based on Intermediate Marks Score" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Total Marks in Intermediate</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-thumbs-up"></i></span>
                                                            <%--<asp:TextBox ID="txtTotalMarksinInter" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_TotalMarksinInter" class="form-control-blue" placeholder="Total Marks in Intermediate" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Total obtained Marks in Intermediate</label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-thumbs-o-up"></i></span>
                                                            <%--<asp:TextBox ID="txtTotalobtainedMarksinInter" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_TotalobtainedMarksinInter" class="form-control-blue" placeholder="Total obtained Marks in Intermediate" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Student Aadhar Card No. </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtStudentAdharNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_StudentAdharNo" class="form-control-blue" placeholder="Student Aadhar Card No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Transfer Certificate No. </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-hand-o-right"></i></span>
                                                            <%--<asp:TextBox ID="txtTransferCertiNo" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_TransferCertiNo" class="form-control-blue" placeholder="Transfer Certificate No." />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Transfer Certificate Date </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa  fa-calendar"></i></span>
                                                            <%--<asp:TextBox ID="TextBox153" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>--%>
                                                            <input type="text" id="txt_TransferCertiDate" class="form-control-blue datepicker-normal" placeholder="Transfer Certificate Date" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Last School / College Name </label>
                                                        <div class="vd_input-wrapper controls ">
                                                            <span class="menu-icon"><i class="fa fa-institution"></i></span>
                                                            <%--<asp:TextBox ID="txtLastSchoolCollegeName" placeholder=" " runat="server" CssClass="form-control-blue"></asp:TextBox>--%>
                                                            <input type="text" id="txt_LastSchoolCollegeName" class="form-control-blue" placeholder="Last School / College Name" />

                                                            <div class="text-box-msg"></div>
                                                        </div>
                                                    </div>


                                                    <div class="col-sm-12  no-padding ">

                                                        <div class="col-sm-4  half-width-50 no-padding ">

                                                            <div class="col-sm-12  mgbt-xs-15">
                                                                <label class="control-label">Last School / College Name </label>
                                                                <div class="vd_input-wrapper controls ">
                                                                    <span class="menu-icon"><i class="fa icon-vcard"></i></span>
                                                                    <%--<asp:TextBox ID="txtIdentityProof" placeholder="Identity Proof" runat="server"
                                                                                            CssClass="form-control-blue"></asp:TextBox>--%>
                                                                    <input type="text" id="txt_IdentityProof" class="form-control-blue" placeholder="Last School / College Name" />


                                                                </div>
                                                            </div>

                                                            <div class="col-sm-12  mgbt-xs-15">
                                                                <label class="control-label">Upload Student Photo</label>
                                                                <div class="vd_input-wrapper controls img-input-ped ">
                                                                    <%--<asp:FileUpload ID="fpUploadPhoto" runat="server" CssClass="form-control-blue " />--%>
                                                                    <input type="file" id="fpUploadPhoto" class="form-control-blue" />
                                                                </div>
                                                            </div>


                                                            <div class="col-sm-12  mgbt-xs-15">
                                                                <label class="control-label">Upload Student Signature</label>
                                                                <div class="vd_input-wrapper controls img-input-ped ">
                                                                    <%--<asp:FileUpload ID="fuUploadStudentSignature" runat="server" CssClass="form-control-blue " />--%>
                                                                    <input type="file" id="fuUploadStudentSignature" class="form-control-blue" />

                                                                </div>
                                                            </div>

                                                            <div class="col-sm-12  mgbt-xs-15">
                                                                <label class="control-label">Upload Father / Mother Signature & Thumb Print</label>
                                                                <div class="vd_input-wrapper controls img-input-ped ">
                                                                    <%--<asp:FileUpload ID="fuUploadFatherMotherSigThumbPrint" runat="server" CssClass="form-control-blue " />--%>
                                                                    <input type="file" id="fuUploadFatherMotherSigThumbPrint" class="form-control-blue" />

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4  half-width-50 no-padding ">

                                                            <div class="col-sm-12  full-width-100 ">

                                                                <div class="col-sm-12  mgbt-xs-15 no-padding">
                                                                    <div class="stu-pic-box">
                                                                        <div class="stu-pic-box-main">
                                                                            <%-- ReSharper disable once Html.PathError --%>
                                                                            <%-- ReSharper disable once Html.PathError --%>
                                                                            <img src="../img/student-pic/big.jpg" alt="" style="display: none;" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-12   mgbt-xs-15 no-padding">
                                                                    <div class="stu-sign-pic-box">
                                                                        <div class="stu-sign-pic-box-main">
                                                                            <%-- ReSharper disable once Html.PathError --%>
                                                                            <%-- ReSharper disable once Html.PathError --%>
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


                                                </div>

                                            </div>
                                        </div>
                                        <!-- Panel Widget -->
                                    </div>

                                </div>
                            </div>
                            </div>
                                </div>


                            <div class="row ">
                                <div class="col-md-12 ">
                                    <div class="btn-center-box-130">
                                        <%-- <asp:LinkButton ID="LinkButton14" OnClick="LinkButton14_Click"
                                                                OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"
                                                                ValidationGroup="A" CssClass="btn vd_btn vd_bg-blue" runat="server"><i class="fa fa-paper-plane"></i> &nbsp; Submit </asp:LinkButton>--%>
                                        <button type="button" id="btn_submit" class="btn vd_btn vd_bg-blue"><i class="fa fa-paper-plane"></i>&nbsp; Submit</button>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>


                                    </div>
                                </div>

                            </div>


                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>

            <%--<script type="text/javascript">

                var pam1 = Sys.WebForms.PageRequestManager.getInstance();
                var pam2 = Sys.WebForms.PageRequestManager.getInstance();
                pam1.add_endRequest(BeginRequestHandlerUpdate);
                pam2.add_endRequest(BeginRequestHandlerNorm());
                // ReSharper disable once UnusedParameter
                function BeginRequestHandlerUpdate(args) {
                    try {
                        var txtfromdate = document.getElementById('<%= txtAgeOnDate.ClientID %>').value;
                        var txttodate = document.getElementById('<%= txtStudentDOB.ClientID %>').value;
                        var fromdate = new Date(txtfromdate).format("yyyy/MM/dd");
                        var todate = new Date(txttodate).format("yyyy/MM/dd");
                        window.PageMethods.GetAgeofStudent(fromdate, todate, Onsuccess);
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
                        window.PageMethods.GetAgeofStudent(fromdate, todate, Onsuccess);
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
            </script>--%>

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
    <%-- ReSharper disable once Html.TagNotClosed --%>
</asp:Content>

