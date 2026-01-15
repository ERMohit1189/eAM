<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Visitorpage.aspx.cs" Inherits="admin_Visitorpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
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
        var specialKeys = new Array();
        specialKeys.push(8);
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) !== -1);
            return ret;
        }

        function IsNumericAlpha(e) {
            var keyCode = e.which ? e.which : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || keyCode === 32 || keyCode === 44);

            return ret;
        }

        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode !== 32 && keyCode !== 46)
                return false;
            return true;
        }


        function ValidateAlphaIsNumber(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode !== 32)
                return false;
            return true;
        }


    </script>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSearch]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/addname.asmx/GetStudents") %>',
                            data: "{ 'studentId': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.split('@')[0],
                                        val: item.split('@')[0]
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

                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Visitor's Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtReligion" runat="server" Style="text-transform: uppercase;" onKeyPress="return ValidateAlpha(event);" MaxLength="100" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="txtReligion" ForeColor="Red" ErrorMessage="Enter Name !" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Additional No. of visitors&nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtnopurson" runat="server" Style="text-transform: uppercase;" onkeypress="return IsNumeric(event);" MaxLength="5" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4   half-width-50 mgbt-xs-15" style="margin-bottom: 21px !important;">
                                        <label class="control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:RadioButtonList ID="RadioButtonList1g" runat="server" CssClass="vd_radio radio-success 
                                                                                                validaterblist txt-capitalize-alpha"
                                                RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem Value="M" Selected="True">Male</asp:ListItem>
                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                                <asp:ListItem Value="T">Transgender</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                    ControlToValidate="RadioButtonList1g" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                    Style="color: #CC3300" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtmobileno" onkeypress="return IsNumeric(event);" MaxLength="10" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtmobileno" ErrorMessage="Enter 10 Disit Mobile Number !" ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtmobileno" ForeColor="Red" ErrorMessage="Enter Mobile Number !" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Email</label>
                                        <div class=" ">
                                            <asp:TextBox ID="emailidtxt" Style="text-transform: uppercase;" MaxLength="100" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RegularExpressionValidator ID="validateEmail"
                                                    runat="server" ErrorMessage="Invalid email."
                                                    ControlToValidate="emailidtxt" ForeColor="Red"
                                                    ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject/Purpose of visit&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="subjectvisittxt" runat="server" Style="text-transform: uppercase;" MaxLength="200" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="subjectvisittxt" ForeColor="Red" ErrorMessage="Enter Subject/Purpose of visit !" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  mgbt-xs-15 ">
                                        <label class="control-label">Whom to meet&nbsp;<span class="vd_red">*</span></label>
                                        <asp:TextBox ID="txtSearch" runat="server"
                                            class="form-control-blue width-100 validatetxt"></asp:TextBox>
                                        <div class="text-box-msg">
                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                        </div>
                                    </div>

                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Card No.&nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtpassno" onkeypress="return IsNumericAlpha(event);" MaxLength="150" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4   half-width-50 mgbt-xs-15">
                                        <label class="control-label">Address</label>
                                        <div class=" ">
                                            <asp:TextBox ID="addresstxt" runat="server" Style="text-transform: uppercase;" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6 col-xs-6 " id="div3" runat="server">
                                        <div class="col-sm-6  no-padding ">
                                            <style>
                                                video {
                                                    width: 174px !important;
                                                    height: 130px !important;
                                                }
                                            </style>
                                            <fieldset style="height: 201px;">
                                                <legend class="legend-me">Live Camera&nbsp;<span class="vd_red">*</span>
                                                </legend>
                                                <div class=" col-sm-12  no-padding">
                                                    <%--<div id="webcam" class="webcam-object visitor-box" style="width: 119px; height: 136px;">
                                                    </div>--%>
                                                    <%-- <asp:LinkButton ID="btnCapture" Text="Capture" runat="server" CssClass="visitor-box pull-top btn-click"
                                                        OnClientClick="take_snapshot();return false;"><i class="fa fa-camera"></i>&nbsp; Capture Photo</asp:LinkButton>--%>
                                                    <span id="camStatus" style="display: none"></span>
                                                    <div id="my_camera"></div>
                                                    <br />
                                                    <%--<input type="button" value="Take Snapshot" onclick="take_snapshot()">--%>
                                                    <a href="javascript:" onclick="take_snapshot()" class="visitor-box pull-top btn-click"><i class="fa fa-camera"></i>&nbsp; Capture Photo</a>
                                                    <input type="hidden" name="image" class="image-tag">
                                                </div>
                                            </fieldset>

                                        </div>
                                        <div class="col-sm-6  no-padding">
                                            <fieldset>
                                                <legend class="legend-me">Visitor's Photo
                                                </legend>
                                                <div class=" col-sm-12  no-padding">
                                                    <asp:Image alt="" ID="Avatar" TabIndex="10" Height="150"
                                                        class="img-responsive img-thumbnail Avatars" runat="server" ImageUrl="~/Uploads/EmptyImage.jpg" />

                                                    <asp:HiddenField ID="hdPhoto" runat="server" />
                                                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </div>
                                            </fieldset>
                                        </div>

                                    </div>


                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" OnClick="LinkButton1_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px !important;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script src="../webcam/newWebCam/webcam.min.js"></script>
            <script language="JavaScript">
                Webcam.set({
                    width: 150,
                    height: 110,
                    image_format: 'jpeg',
                    jpeg_quality: 200
                });
                Webcam.attach('#my_camera');
                function take_snapshot() {
                    Webcam.snap(function (data_uri) {
                        $(".image-tag").val(data_uri);
                        document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_Avatar').src = data_uri;
                        document.getElementById('ContentPlaceHolder1_ContentPlaceHolderMainBox_hdPhoto').value = data_uri.replace(/^data:image\/[a-z]+;base64,/, "");
                    });
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

