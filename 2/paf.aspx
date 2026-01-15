<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="false" CodeFile="paf.aspx.cs" Inherits="AdminPaf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        .smoke {
            font-size: 20px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script src="../js/jquery.min.js"></script>
    <script>
        function ddlChange() {
            $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpAdmissionType").change(function () {
                showSrnoDiv();
            });

            $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpAdmissionTypePanel").change(function () {
                showSrnoDiv();
            });
        }
        function showSrnoDiv() {
            if ($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpAdmissionType").val() === "Old") {
                $("#name").show();
                $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtSrNo").addClass("validatetxt");
                $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtSrNo").focus();
            }
            else {
                $("#name").hide();
                $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtSrNo").removeClass("validatetxt");

            }

            if ($("#ContentPlaceHolder1_ContentPlaceHolderMainBox_drpAdmissionTypePanel").val() === "Old") {
                $("#name1").show();
                $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtSrNo1").focus();
            }
            else {
                $("#name1").hide();
                $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtSrNo1").val("");
                $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_hfStudentId1").val("");
            }
        }
    </script>
    <script type="text/javascript">
        function getStudentsList() {
            $(function () {
                $("[id$=txtSrNo]").autocomplete({
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
                            error: function (request, status, error) { alert(request); alert(status); alert(error); },
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
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);

                Sys.Application.add_load(scrollbar);
                Sys.Application.add_load(ddlChange);
                Sys.Application.add_load(showSrnoDiv);
                Sys.Application.add_load(getStudentsList);
            </script>
            <%--<div id="loader" runat="server"></div>--%>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding" id="divsearch1" runat="server">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label"><%--Enter Enquiry No. (if you have)--%></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Enquiry No. (if you have)" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15" style="margin: 22px 0px 0px 0px;">
                                         <asp:LinkButton ID="Button11" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp2ss');return validationReturn();" OnClick="Button11_Click" class="button form-control-blue" CausesValidation="false"><i class="fa fa-eye"></i>&nbsp;View</asp:LinkButton>
                                      <%--  <asp:Button ID="Button11" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp2ss');return validationReturn();" CausesValidation="false" CssClass="button form-control-blue" OnClick="Button11_Click" Text="View" />--%>
                                        <div id="msgView" runat="server" style="left: 75px">
                                        </div>
                                    </div>
                                    <div class="col-sm-12  no-padding">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtAdmissiondate" runat="server" CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divpaymentmode" runat="server">
                                            <label class="control-label">Mode of Payment&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="DropDownMOD" runat="server" AutoPostBack="True"
                                                    CssClass="form-control-blue " OnSelectedIndexChanged="DropDownMOD_SelectedIndexChanged">
                                                    <asp:ListItem>Cash</asp:ListItem>
                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                    <asp:ListItem>DD</asp:ListItem>
                                                    <asp:ListItem>Card</asp:ListItem>
                                                    <asp:ListItem>Online Transfer</asp:ListItem>
                                                    <%--<asp:ListItem>Other</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="a"
                                                        CssClass="text-danger" ControlToValidate="DropDownMOD"
                                                        ErrorMessage="Please Select MOD." Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="div8" runat="server" visible="false">
                                            <asp:Label ID="Label54" runat="server" class="control-label"></asp:Label>
                                            <div class="">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="DDChkYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDChkYear_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDChkMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDChkMonth_SelectedIndexChanged"
                                                            CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DDChkDate" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2  half-width-50 mgbt-xs-15" id="div5" runat="server" visible="false">
                                            <asp:Label ID="Label42" runat="server" class="control-label"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue "></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2  half-width-50 mgbt-xs-15" id="div6" runat="server" visible="false">
                                            <asp:Label ID="Label2" runat="server" class="control-label"></asp:Label>
                                            <div class="">
                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue "></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divsts" runat="server" visible="false">
                                            <label class="control-label">Status</label>
                                            <div class="">
                                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue " Text="Paid" ForeColor="Red" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Session&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpSession" runat="server" CssClass="form-control-blue validatedrp validatedrp2ss" OnSelectedIndexChanged="drpSession_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divadmissiontype" runat="server">
                                                    <label class="control-label">Type of Admission&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpAdmissionType" runat="server" AutoPostBack="true" CssClass="form-control-blue" OnSelectedIndexChanged="drpAdmissionType_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Text="New" Value="New"></asp:ListItem>
                                                            <asp:ListItem Text="Old" Value="Old"></asp:ListItem>
                                                            <asp:ListItem Text="New (Provisional)" Value="New (Provisional)"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15" id="name" style="display: none">
                                                    <label class="control-label">Student's Name/ S.R. No.</label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtSrNo" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnTextChanged="txtSrNo_TextChanged" placeholder="Student's Name/ S.R. No."></asp:TextBox>
                                                        <div class="text-box-msg">
                                                            <asp:HiddenField ID="hfStudentId" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Student's First Name&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtStudentName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                                    </div>
                                                </div>
                                           


                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Middle Name</label>
                                            <div class="">
                                                <asp:TextBox ID="txtStudentName0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Last Name</label>
                                            <div class="">
                                                <asp:TextBox ID="txtStudentName1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                                
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Date of Birth</label>
                                            <div class="">
                                                <asp:TextBox ID="txtDob" runat="server" 
                                                    CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Father's Name&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control-blue validatetxt" MaxLength="10" onblur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Email</label>
                                            <div class="">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                                <div class="text-box-msg">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter Valid Email ID" ValidationGroup="vgSubmit" ControlToValidate="txtEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
 <div class="col-sm-4  half-width-50 mgbt-xs-15">
     <label class="control-label">Select Gender&nbsp;<span class="vd_red">*</span></label>
     <div class="">
         <asp:DropDownList ID="drpSex" runat="server" CssClass="form-control-blue validatedrp">

             <asp:ListItem><-- Select --></asp:ListItem>
             <asp:ListItem>Male</asp:ListItem>
             <asp:ListItem>Female</asp:ListItem>
             <asp:ListItem>Transgender</asp:ListItem>
         </asp:DropDownList>
         <div class="text-box-msg">
         </div>
     </div>
 </div>


                                       
                                                </div>
</div>
<div class="col-sm-12">
    <div class="row">    <div class="col-sm-4  half-width-50 mgbt-xs-15">
        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
        <div class="">
            <asp:DropDownList ID="drpClassss" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                OnSelectedIndexChanged="drpClassss_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-4  half-width-50 mgbt-xs-15">
        <label class="control-label">Select Stream&nbsp;<span class="vd_red">*</span></label>
        <div class="">
            <asp:DropDownList ID="drpBranchss" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                OnSelectedIndexChanged="drpBranchss_SelectedIndexChanged">
            </asp:DropDownList>

        </div>
    </div>
                                    
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Select Medium&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlMedium" runat="server" CssClass="form-control-blue validatedrp">
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Amount&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtAmt" runat="server" AutoPostBack="true" Enabled="false" OnTextChanged="txtAmt_TextChanged" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                <div class="text-box-msg">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAmt"
                                                        ErrorMessage="Please enter amount." SetFocusOnError="True" Style="color: #FF0000" ValidationGroup="a"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmt"
                                                        ErrorMessage="Please enter a valid amount(0-9)." SetFocusOnError="True" Style="color: #FF0000"
                                                        ValidationExpression="[0-9]*" ValidationGroup="a" Display="Dynamic"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Exemption</label>
                                            <div class="">
                                                <asp:TextBox ID="txtConcession" AutoPostBack="true" OnTextChanged="txtConcession_TextChanged" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3  half-width-50 mgbt-xs-15" style="display: none">
                                            <label class="control-label">Cheque Bounce Amount (if Any)</label>
                                            <div class="">
                                                <asp:TextBox ID="txtCBAmount" runat="server" CssClass="form-control-blue" Enabled="true"
                                                    AutoPostBack="true" OnTextChanged="txtCBAmount_TextChanged" Text="0"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Paid Amount&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtReceivedAmount" runat="server" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Booklet/Form No.</label>
                                            <div class="">
                                                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <label class="control-label">Template&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlTemplateAdmission" runat="server" class="form-control-blue validatedrp">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="Template 1">Hindi Version</asp:ListItem>
                                                <asp:ListItem Value="Template 2">English Version</asp:ListItem>
                                                <asp:ListItem Value="Template 3">Extended Version</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
    </div>                          </div>
                                        <div class="col-sm-12">
                                            <asp:LinkButton ID="Submit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn(this);" class="button form-control-blue" OnClick="Submit_Click">Submit</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 75px">
                                            </div>

                                        </div>
                                    <div class="col-sm-12">
                                        <br />
                                        <hr />
                                    </div>
                                    </div>
                                    <div class="col-sm-12 no-padding">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">

                                            <label class="control-label">Form Date</label>
                                            <div class="">
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control-blue datepicker-normal validatetxtss"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">To Date</label>
                                            <div class="">
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-blue datepicker-normal validatetxtss"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6  half-width-50" style="padding-top: 5px;">
                                            <label class="control-label"></label>
                                            <div class="">
                                                <asp:LinkButton ID="LinkView" runat="server" OnClientClick="ValidateTextBox('.validatetxtss');ValidateDropdown('.validatedrpss');return validationReturn(this);" class="button form-control-blue" OnClick="LinkView_Click">View</asp:LinkButton>
                                            </div>
                                            <br />
                                            <div id="msgbox2" runat="server" style="left: 75px">
                                            </div>
                                        </div>
                                        <div class=" col-sm-12 " id="divdatabind" runat="server">
                                            <br />
                                            <div class=" table-responsive  table-responsive2">
                                                <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-head-border table-bordered"
                                                    AllowPaging="True" OnPageIndexChanging="Grd_PageIndexChanging" PageSize="100">
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label31" runat="server" Text='<%# Bind("sno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40px" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Receipt No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label18" runat="server" Text='<%# Bind("RecieptNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("AdmissionFromDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type of Admission">
                                                            <ItemTemplate>
                                                                <asp:Label ID="AdmissionType" runat="server" Text='<%# Bind("AdmissionType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Student's Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label32" runat="server" Text='<%# Bind("StudentNames") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Class") %>'></asp:Label>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stream">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbranch" runat="server" Text='<%# Bind("Branch") %>'></asp:Label>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label33" runat="server" Text='<%# Bind("ReceivedAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("mop") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label34" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                <asp:Label ID="Label3" runat="server" CssClass="hide" Text='<%# Bind("Cancel") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit Receipt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" title="Edit" data-toggle="tooltip"
                                                                    data-placement="top" OnClick="LinkButton1_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cancel Receipt" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label37" runat="server" Text='<%# Bind("RecieptNo") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                                    title="Cancel" class="btn menu-icon vd_bd-red vd_red"><i class="fa fa-times"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Print Receipt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label38" runat="server" Text='<%# Bind("RecieptNo") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"
                                                                    title="Print" class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Print Form">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTemplate" runat="server" Text='<%# Bind("Template") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="Label39" runat="server" Text='<%# Bind("RecieptNo") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="lnkPrintAF" runat="server" OnClick="lnkPrintAF_Click"
                                                                    title="Print Admission Form" class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <HeaderStyle CssClass="grid_heading_default" />
                                                    <PagerSettings PageButtonCount="100" />
                                                    <RowStyle CssClass="grid_details_default" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                            <div class="col-sm-12 tab-popup">
                                <asp:Button ID="Button9" runat="server" Style="display: none" />
                                <div class="col-sm-12 ">
                                    <div class="col-sm-12">
                                        <div id="msg2" runat="server" style="left: 60px;"></div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Session</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSessionPanel" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Enter Enquiry No</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1Panel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Type of Admission</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpAdmissionTypePanel" runat="server" CssClass="form-control-blue hide">
                                                <asp:ListItem Text="<-- Select Admission Type -->"></asp:ListItem>
                                                <asp:ListItem Selected="True" Text="New" Value="New"></asp:ListItem>
                                                <asp:ListItem Text="Old" Value="Old"></asp:ListItem>
                                                <asp:ListItem Text="New (Provisional)" Value="Provisional"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" id="name1" style="display: none">
                                        <label class="control-label">S.R. No.</label>
                                        <div class="">
                                            <asp:TextBox ID="txtSrNo1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:HiddenField ID="hfStudentId1" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Admission Form Date</label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:TextBox ID="txtAdmissiondatePanel" runat="server" CssClass="form-control-blue validatetxt2 datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Student's First Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:TextBox ID="txtStudentNamePanel" runat="server" CssClass="form-control-blue validatetxtss1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Middle Name</label>
                                        <div class="">
                                            <asp:TextBox ID="txtStudentNamePanel0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Last Name</label>
                                        <div class="">
                                            <asp:TextBox ID="txtStudentNamePanel1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date of Birth</label><%--&nbsp;<span class="vd_red">*</span>--%>
                                        <div class="">
                                            <asp:TextBox ID="txtdobPanel" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Father's Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:TextBox ID="txtFatherNamePanel" runat="server" CssClass="form-control-blue validatetxtss1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:TextBox ID="txtContactNoPanel" runat="server" CssClass="form-control-blue validatetxtss1" MaxLength="10" onblur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Gender</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSexPanel" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Text="<-- Select Gender -->"></asp:ListItem>
                                                <asp:ListItem Text="Male"></asp:ListItem>
                                                <asp:ListItem Text="Female"></asp:ListItem>
                                                <asp:ListItem Text="Transgender"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Booklet/Form No.</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Template&nbsp;<span class="vd_red">*</span></label>
                                        <asp:DropDownList ID="DropDownList1" runat="server" class="form-control-blue validatedrpss1">
                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
                                            <asp:ListItem Value="Template 1">Hindi Version</asp:ListItem>
                                            <asp:ListItem Value="Template 2">English Version</asp:ListItem>
                                            <asp:ListItem Value="Template 3">Extended Version</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Class</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClassssPanel" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpClassssPanel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Stream</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpBranchssPanel" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpBranchssPanel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Amount&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:TextBox ID="txtAmtPanel" runat="server" AutoPostBack="true" OnTextChanged="txtAmtPanel_TextChanged" CssClass="form-control-blue validatetxt2" Enabled="false"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Exemption</label>
                                        <div class="">
                                            <asp:TextBox ID="txtConcessionPanel" runat="server" AutoPostBack="true" OnTextChanged="txtConcession1_TextChanged" Enabled="False" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Cheque Bounce Fine</label>
                                        <div class="">
                                            <asp:TextBox ID="txtCBAmountPanel" runat="server" CssClass="form-control-blue" Enabled="true"
                                                placeholder="Cheque Bounce Amount (if Any)" AutoPostBack="true" OnTextChanged="txtCBAmountPanel_TextChanged" Text="0"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Paid Amount&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtReceivedAmountPanel" runat="server" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>

                                        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>

                                        <asp:LinkButton ID="LinkButton4" runat="server" OnClientClick="ValidateTextBox('.validatetxtss1');ValidateDropdown('.validatedrpss1');return validationReturn(this);" OnClick="LinkButton4_Click" CssClass="button-y">Update</asp:LinkButton>
                                        &nbsp;&nbsp;
                                <asp:LinkButton ID="LinkButtons" runat="server" OnClick="LinkButtons_Click" CssClass="button-n">Cancel</asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="LinkButtons" PopupControlID="Panel1"
                        TargetControlID="Button2" BackgroundCssClass="popup_bg">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>


                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                            <table class="tab-popup">

                                <tr>
                                    <td style="text-align: center;">
                                        <h4>Do you really want to Cancel this receipt?</h4>
                                        <asp:Label ID="lblvalue"
                                            runat="server" Visible="False"></asp:Label></td>
                                </tr>

                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Button ID="Button8" runat="server" CausesValidation="False"
                                            Text="No" CssClass="button-n" />
                                        &nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False"
                            OnClick="btnDelete_Click" Text="Yes" CssClass="button-y" />



                                    </td>
                                </tr>
                            </table>

                        </asp:Panel>
                        <asp:Button ID="Button10" runat="server" Text="Button" Style="display: none" />
                        <%-- ReSharper disable once Asp.InvalidControlType --%>
                        <ajaxToolkit:ModalPopupExtender ID="Button10_ModalPopupExtender" runat="server"
                            BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True"
                            PopupControlID="Panel2" TargetControlID="Button10" CancelControlID="Button8">
                        </ajaxToolkit:ModalPopupExtender>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Submit" />
        </Triggers>
    </asp:UpdatePanel>

    <script>

        $(document).ready(function () {
            $("[id*=LinkButton1]").click(function () {
                window.scrollTo(x - coord, y - coord);
            });
            $("[id*=LinkButton5]").click(function () {
                $("[id*=divPopup]").addClass("hide");
            });
        });
    </script>

</asp:Content>

