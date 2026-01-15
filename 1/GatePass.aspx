<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="GatePass.aspx.cs" Inherits="admin_GetPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .modalPopup {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            z-index: -1;
        }
    </style>
    <style>
        .x-navigation li.active2 > a .fa,
        .x-navigation li.active2 > a .glyphicon {
            color: #ffd559;
        }

        .x-navigation li.active21 > a .fa,
        .x-navigation li.active21 > a .glyphicon {
            color: #ffd559;
        }
    </style>
    <script type="text/javascript">
        function getStudentsList() {
            $("[id$=hfStudentId]").val('');
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
                                        };
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(getStudentsList);
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 select-list-hide display-none">
                                        <asp:DropDownList ID="DrpEnter" runat="server" Enabled="false">
                                            <asp:ListItem>Enter S.R./ Enrollment No./Name</asp:ListItem>
                                        </asp:DropDownList>
                                        <i>H</i>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                        <asp:TextBox ID="TxtEnter" placeholder="Enter Name/ S.R. No." runat="server"
                                            class="form-control-blue width-100 validatetxt" AutoPostBack="true" OnTextChanged="TxtEnter_TextChanged"></asp:TextBox>

                                        <div class="text-box-msg">
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" class="button form-control-blue"> View</asp:LinkButton>
                                        <asp:Label ID="lblFee" runat="server" Style="color: #FF0000"></asp:Label>
                                        <div id="msgbox" runat="server" style="left: 62px !important;"></div>
                                    </div>
                                </div>

                                <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide" id="div1" runat="server">
                                    <div class="col-lg-12 col-md-12 col-sm-12  no-padding mgbt-xs-15">
                                        <div class="col-lg-12 col-md-12 col-sm-12  no-padding student-box">
                                            <div class="table-responsive table-responsive2">
                                                <table style="width: 100%;">
                                                    <asp:Repeater ID="Grd1" runat="server">
                                                        <ItemTemplate>
                                                            <tbody>
                                                                <tr>
                                                                    <td class="tab-top">
                                                                        <div>
                                                                            <table class="table table-striped no-bm no-head-border table-bordered pro-table table-header-group" id="ContentPlaceHolder1_ContentPlaceHolderMainBox_grdStRecord" style="color: Black; background-color: White; border-collapse: collapse; mso-cellspacing: 0;">

                                                                                <tbody>
                                                                                    <tr>
                                                                                        <th style="width: 150px">S.R. No.</th>
                                                                                        <th>Student's Name</th>
                                                                                        <th>Father's Name</th>
                                                                                        <th>Class</th>
                                                                                        <th>Medium</th>
                                                                                        <th>Date of Admission</th>
                                                                                        <th>Contact No.</th>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center" valign="middle">
                                                                                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("srno") %>'></asp:Label></td>
                                                                                        <td align="center" valign="middle" style="display: none;">
                                                                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label></td>
                                                                                        <td align="center" valign="middle">
                                                                                            <asp:Label ID="Label14" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" valign="middle">
                                                                                            <asp:Label ID="Label17" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" valign="middle">
                                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("combineClassName") %>'></asp:Label>
                                                                                            <asp:Label ID="lblClass" runat="server" CssClass="hide" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                                            <asp:Label ID="lblBranch" runat="server" CssClass="hide" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                                            <asp:Label ID="lblSection" runat="server" CssClass="hide" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" valign="middle">
                                                                                            <asp:Label ID="Label21" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" valign="middle">
                                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" valign="middle">
                                                                                            <asp:Label ID="Label22" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>

                                                                            </table>
                                                                        </div>

                                                                    </td>
                                                                    <td class="tab-top tab-profile text-center ">
                                                                        <div class="gallery-item fee-pic-box">
                                                                            <asp:HyperLink ID="studentImg" runat="server" NavigateUrl="" data-rel="prettyPhoto[2]">
                                                                                <asp:Image ID="img" runat="server" ImageUrl="../img/user-pic/user-pic.jpg" Style="width: 48px; height: 60px;" />
                                                                            </asp:HyperLink>
                                                                            <asp:HyperLink runat="server" ID="hylinkmoredetails" NavigateUrl="" Target="_blank" Text="more..."></asp:HyperLink>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <fieldset>
                                        <legend>
                                            <span class="font-s-17">Gate Pass Detail </span>
                                        </legend>

                                        <div id="div3" runat="server" class="col-md-6 col-xs-6 ">
                                            <div class="col-sm-12 " id="div4" runat="server">
                                                <div class="col-sm-6  no-padding ">
                                                    <fieldset>
                                                        <legend class="legend-me">Live Camera&nbsp;<span class="vd_red"></span>
                                                        </legend>
                                                        <div class=" col-sm-12  no-padding">
                                                            <div id="webcam" class="webcam-object img-responsive img-thumbnail">
                                                            </div>
                                                            <div class=" col-sm-12  no-padding">
                                                                <a onclick="take_snapshot();return false;" id="sdsd1" class="pull-top btn-click" style="cursor:pointer;"><i class="fa fa-camera"></i>&nbsp; Capture Photo</a>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                    <span id="Span1" style="display: none"></span>
                                                </div>
                                                <div class="col-sm-6  no-padding">
                                                    <fieldset>
                                                        <legend class="legend-me">Visitor's Photo
                                                        </legend>
                                                        <div class=" col-sm-12  no-padding">
                                                            <%-- ReSharper disable once Asp.Image --%>
                                                            <asp:Image alt="" class="img-responsive img-thumbnail Avatars" Height="150" ID="Avatar" ImageUrl="~/img/user-pic/user-profile-demo.png" runat="server" TabIndex="10" Width="160" />
                                                            <asp:HiddenField ID="hdPhoto" runat="server" />

                                                        </div>
                                                        <div class="col-sm-12  mgbt-xs-15">
                                                            <label class="control-label">Upload Visitor's Photo&nbsp;<span class="vd_red"></span></label>
                                                            <div class="">
                                                                <asp:FileUpload ID="ImageUpload" runat="server" type="file" CssClass="form-control-blue"
                                                                    onchange="checksFileSizeandFileTypeinupdatePanel(this, 50000, 
                                                                                        'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','Avatars',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdPhoto');" />
                                                                <div class="text-box-msg">
                                                                    <%--                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                    ControlToValidate="ImageUpload" ForeColor="Red" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="div5" runat="server" class="col-md-6 col-xs-6 ">
                                            <div class="col-sm-12 " id="div6" runat="server">
                                                <div class="col-sm-6  no-padding ">
                                                    <fieldset>
                                                        <legend class="legend-me">Live Camera&nbsp;<span class="vd_red"></span>
                                                        </legend>
                                                        <div class=" col-sm-12  no-padding">
                                                            <div id="webcam1" class="webcam-object img-responsive img-thumbnail">
                                                            </div>
                                                            <div class=" col-sm-12  no-padding">
                                                                <a onclick="take_snapshotst();return false;" id="sdsd" class="pull-top btn-click" style="cursor:pointer;"><i class="fa fa-camera"></i>&nbsp; Capture Photo</a>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                    <span id="Span11" style="display: none"></span>
                                                </div>
                                                <div class="col-sm-6  no-padding">
                                                    <fieldset>
                                                        <legend class="legend-me">Student's Photo
                                                        </legend>
                                                        <div class=" col-sm-12  no-padding">
                                                            <%-- ReSharper disable once Asp.Image --%>
                                                            <asp:Image alt="" class="img-responsive img-thumbnail stphotoshow" Height="150" ID="imgstphoto" ImageUrl="~/img/user-pic/user-profile-demo.png" runat="server" TabIndex="10" Width="160" />
                                                            <asp:HiddenField ID="hpstudentphoto" runat="server" />

                                                        </div>
                                                        <div class="col-sm-12  mgbt-xs-15">
                                                            <label class="control-label">Upload Student's Photo&nbsp;<span class="vd_red"></span></label>
                                                            <div class="">
                                                                <asp:FileUpload ID="ImageUploadStudent" runat="server"
                                                                    onchange="checksFileSizeandFileTypeinupdatePanel(this, 50000, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF','stphotoshow','ContentPlaceHolder1_ContentPlaceHolderMainBox_hpstudentphoto');"
                                                                    type="file" CssClass="form-control-blue" />
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12  no-padding">
                                            <div class="col-sm-6  no-padding">
                                                <div class="col-sm-12  mgbt-xs-15">
                                                    <label class="control-label">Visitor's Name&nbsp;<span class="vd_red">* </span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtGuardianname" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtGuardianname" ForeColor="Red" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12  mgbt-xs-15">
                                                    <label class="control-label">Relation&nbsp;<span class="vd_red">* </span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtRelation" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtRelation" ForeColor="Red" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>




                                            </div>
                                            <div class="col-sm-6  no-padding">
                                                <div class="col-sm-12  mgbt-xs-15">
                                                    <label class="control-label">Visitor's Contact No.&nbsp;<span class="vd_red">* </span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtContactno" runat="server" CssClass="form-control-blue" onblur="ChecktenDigitMobileNumber(this)"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtContactno" ForeColor="Red" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12  mgbt-xs-9">
                                                    <label class="control-label">Reason&nbsp;<span class="vd_red">* </span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtReason" TextMode="MultiLine" Rows="1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtReason" ForeColor="Red" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>



                                            </div>
                                        </div>
                                    </fieldset>
                                    <%-- </div>--%>
                                    <div class="col-sm-12  text-center">
                                        <asp:LinkButton ID="lnkSubmit" CssClass="button form-control-blue" runat="server" OnClick="lnkSubmit_Click"><i class="fa fa-paper-plane"></i> &nbsp; Submit </asp:LinkButton>

                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding hide" id="Div2" runat="server">
                                    <div class="col-md-10  mgbt-xs-15">
                                        <div class="print-container" id="abc2" runat="server">
                                            <div class="gate_head_box  no-padding print-row">
                                                <div class="print-row  border-b ">
                                                    <div id="header" runat="server" class="print-font-set print-table-text"></div>
                                                </div>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>
                                                        <div class="  no-padding print-row">
                                                            <div class=" col-xs-6 col-sm-6 text-left">
                                                                <h4 class="font-bold head-add-input print-text marg-tb-5">Gate Pass No. :
                                                                <asp:Label ID="Label25" runat="server" Text='<%# Eval("Maxid")%>'></asp:Label></h4>
                                                            </div>
                                                            <div class=" col-xs-6 col-sm-6 text-right">
                                                                <h4 class="font-bold head-add-input print-text marg-tb-5 ">Date :
                                                                <asp:Label ID="Label26" runat="server" Text='<%# Bind("Date")%>'></asp:Label>
                                                                </h4>
                                                            </div>
                                                        </div>

                                                        <div class=" col-xs-6 col-sm-6 mgbt-xs-10 p-mgbt-xs-10 ">

                                                            <table class="table table-hover no-bm no-head-border table-bordered  print-table-text">
                                                                <tbody>
                                                                    <tr>
                                                                        <td rowspan="5" style="width: 115px">
                                                                            <div class="table-pic-box">
                                                                                <asp:Image ID="Image3" runat="server" AlternateText='<%# Eval("srno") %>'
                                                                                    ImageUrl='<%# ResolveClientUrl(!string.Equals((string) Eval("StudentPhotopath"), "", StringComparison.Ordinal) ?
                                                                                    Eval("StudentPhotopath").ToString():@"~\admin\images\dummy.png") %>' />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">S.R. No. :
                                                        <asp:Label ID="Label28" runat="server" Text='<%# Eval("srno")%>'></asp:Label></h4>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Student's Name :
                                                        <asp:Label ID="Label29" runat="server" Text='<%# Eval("StudentName")%>'></asp:Label></h4>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Father's Name :
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Eval("FatherName")%>'></asp:Label></h4>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Class & Section :
                                                        <asp:Label ID="Label31" runat="server" Text='<%# Eval("combineclassname")%>'></asp:Label>&nbsp;<asp:Label ID="Label23" runat="server" Text='<%# Eval("SectionName")%>'></asp:Label></h4>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Contact No. :
                                                        <asp:Label ID="Label32" runat="server" Text='<%# Eval("FamilyContactNo")%>'></asp:Label></h4>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <h4 class="font-bold pass-input">Reason :
                                                        <asp:Label ID="Label33" runat="server" Text='<%# Eval("Reason")%>'></asp:Label></h4>
                                                                        </td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>


                                                        </div>

                                                        <div class="col-xs-6 col-sm-6 mgbt-xs-10 p-mgbt-xs-10 ">
                                                            <table class="table table-hover no-bm no-head-border table-bordered  print-table-text">
                                                                <tbody>
                                                                    <tr>
                                                                        <td rowspan="5" style="width: 115px">
                                                                            <div class="table-pic-box">
                                                                                <asp:Image ID="Image4" runat="server" AlternateText='<%# Eval("srno") %>'
                                                                                    ImageUrl='<%# ResolveClientUrl(!string.Equals((string) Eval("GuardianPhotoPath"), "", StringComparison.Ordinal) ?
                                                                                        Eval("GuardianPhotoPath").ToString():@"~\img\user-pic\user-2X2.png") %>' />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Visitor's Name :
                                                                            <asp:Label ID="Label27" runat="server" Text='<%# Eval("GuardianName")%>'></asp:Label></h4>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Contact No. :
                                                                            <asp:Label ID="Label36" runat="server" Text='<%# Eval("GuardionContact")%>'></asp:Label></h4>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Relation :
                                                                            <asp:Label ID="Label34" runat="server" Text='<%# Eval("Relation")%>'></asp:Label></h4>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">&nbsp;&nbsp; </h4>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">&nbsp;&nbsp; </h4>

                                                                        </td>
                                                                    </tr>


                                                                </tbody>
                                                            </table>
                                                        </div>

                                                        <div class="gate-pass-cut marg-top-20">

                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left sign-size ">
                                                                <h4 class="font-bold  print-text" style="padding-left: 0;">Office In-charge's Signature</h4>
                                                                <%--  <h4 class=" dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-center  sign-size ">
                                                                <h4 class="font-bold  print-text">Principal's Signature</h4>
                                                                <%-- <h4 class="dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-right sign-size ">

                                                                <h4 class="font-bold  print-text">Visitor's Signature</h4>
                                                                <%--   <h4 class="dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                            </div>
                                                        </div>
                                                        <div class="  no-padding print-row">
                                                            <table style="width: 100%;">
                                                                <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px;">Generated by eAM&reg; | Submitted by
                                                                  <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("LoginName") %>'></asp:Label>&nbsp;on 
                                                                  <asp:Label ID="lblFooterDate" runat="server" Text='<%# Bind("Date")%>'></asp:Label>
                                                                </td>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>

                                            <div class="gate-pass-cut  no-padding print-row">
                                                <div class="  ">
                                                    <div class="cut-line-box">
                                                        <h4><i class="fa fa-scissors"></i></h4>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="gate_head_box  no-padding print-row">
                                                <div class="print-row  border-b  ">
                                                    <div id="header1" runat="server" class="print-font-set print-table-text"></div>
                                                </div>
                                                <asp:Repeater ID="Repeater2" runat="server">
                                                    <ItemTemplate>
                                                        <div class="  no-padding print-row">
                                                            <div class=" col-xs-6 col-sm-6 text-left print-text">
                                                                <h4 class="font-bold head-add-input print-text marg-tb-5">Gate Pass No. :
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Maxid")%>'></asp:Label></h4>
                                                            </div>
                                                            <div class=" col-xs-6 col-sm-6 text-right print-text">
                                                                <h4 class="font-bold head-add-input print-text marg-tb-5 ">Date :
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Date")%>'></asp:Label>
                                                                </h4>
                                                            </div>
                                                        </div>

                                                        <div class=" col-xs-6 col-sm-6 mgbt-xs-10 p-mgbt-xs-10 ">

                                                            <table class="table table-hover no-bm no-head-border table-bordered  print-table-text">
                                                                <tbody>
                                                                    <tr>
                                                                        <td rowspan="5" style="width: 115px">
                                                                            <div class="table-pic-box">
                                                                                <asp:Image ID="Image6" runat="server" AlternateText='<%# Eval("srno") %>'
                                                                                    ImageUrl='<%# ResolveClientUrl(!string.Equals((string) Eval("StudentPhotopath"), "", StringComparison.Ordinal) ?
                                                                                    Eval("StudentPhotopath").ToString():@"~\admin\images\dummy.png") %>' />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">S.R. No. :
                                                        <asp:Label ID="Label28" runat="server" Text='<%# Eval("srno")%>'></asp:Label></h4>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Student's Name :
                                                        <asp:Label ID="Label29" runat="server" Text='<%# Eval("StudentName")%>'></asp:Label></h4>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Father's Name :
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Eval("FatherName")%>'></asp:Label></h4>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Class & Section :
                                                        <asp:Label ID="Label31" runat="server" Text='<%# Eval("combineclassname")%>'></asp:Label>&nbsp;<asp:Label ID="Label23" runat="server" Text='<%# Eval("SectionName")%>'></asp:Label></h4>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Contact No. :
                                                        <asp:Label ID="Label32" runat="server" Text='<%# Eval("FamilyContactNo")%>'></asp:Label></h4>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <h4 class="font-bold pass-input">Reason :
                                                        <asp:Label ID="Label33" runat="server" Text='<%# Eval("Reason")%>'></asp:Label></h4>
                                                                        </td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>

                                                        </div>

                                                        <div class="col-xs-6 col-sm-6 mgbt-xs-10 p-mgbt-xs-10 ">
                                                            <table class="table table-hover no-bm no-head-border table-bordered  print-table-text">
                                                                <tbody>
                                                                    <tr>
                                                                        <td rowspan="5" style="width: 115px">
                                                                            <div class="table-pic-box">
                                                                                <asp:Image ID="Image7" runat="server" AlternateText='<%# Eval("srno") %>'
                                                                                    ImageUrl='<%# ResolveClientUrl(!string.Equals((string) Eval("GuardianPhotoPath"), "", StringComparison.Ordinal) ?
                                                                                    Eval("GuardianPhotoPath").ToString():@"~\img\user-pic\user-2X2.png") %>' />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Visitor's Name :
                                                        <asp:Label ID="Label27" runat="server" Text='<%# Eval("GuardianName")%>'></asp:Label></h4>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Contact No. :
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Eval("GuardionContact")%>'></asp:Label></h4>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">Relation :
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Eval("Relation")%>'></asp:Label></h4>

                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">&nbsp;&nbsp; </h4>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <h4 class="font-bold pass-input">&nbsp;&nbsp; </h4>

                                                                        </td>
                                                                    </tr>


                                                                </tbody>
                                                            </table>

                                                        </div>

                                                        <div class="gate-pass-cut marg-top-20">

                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left  sign-size ">
                                                                <h4 class="font-bold  print-text" style="padding-left: 0;">Office In-charge's Signature</h4>
                                                                <%--  <h4 class=" dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-center sign-size ">

                                                                <h4 class="font-bold  print-text">Principal's Signature</h4>
                                                                <%--  <h4 class="dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                            </div>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-right sign-size ">
                                                                <h4 class="font-bold  print-text">Visitor's Signature</h4>
                                                                <%--  <h4 class="dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                            </div>
                                                        </div>
                                                        <div class="  no-padding print-row">
                                                            <table style="width: 100%;">
                                                                <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px;">Generated by eAM&reg; | Submitted by
                                                                  <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("LoginName") %>'></asp:Label>&nbsp;on 
                                                                  <asp:Label ID="lblFooterDate" runat="server" Text='<%# Bind("Date")%>'></asp:Label>
                                                                </td>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2  mgbt-xs-15">
                                        <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click" CssClass="btn-print-box"
                                            title="Print Gate Pass" data-placement="left"><i class="icon-printer"></i></asp:LinkButton>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script src="../webcam/newWebCam/webcam.min.js"></script>
            <script language="JavaScript">
                //Webcam.set({
                //    width: 160,
                //    height: 190,
                //    image_format: 'jpeg',
                //    jpeg_quality: 100,
                //    constraints: {
                //        width: { exact: 160 },
                //        height: { exact: 190 }
                //    }
                //});
                //Webcam.attach('#webcam');
                //Webcam.attach('#webcam1');
                function captureImage() {
                    Webcam.set({
                        width: 160,
                        height: 190,
                        image_format: 'jpeg',
                        jpeg_quality: 100
                    });
                    Webcam.attach('#webcam');
                    Webcam.attach('#webcam1');
                }

                function take_snapshot() {
                    Webcam.snap(function (data_uri) {
                        document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_Avatar').src = data_uri;
                        document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_hdPhoto').value = data_uri.replace(/^data:image\/[a-z]+;base64,/, "");
                    });
                }
                function take_snapshotst() {
                    Webcam.snap(function (data_uri) {
                        document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_imgstphoto').src = data_uri;
                        document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_hpstudentphoto').value = data_uri.replace(/^data:image\/[a-z]+;base64,/, "");
                    });
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

