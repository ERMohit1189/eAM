<%@ Page Title="Admission Form | eAM&#174;" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="false" CodeFile="Gpaf.aspx.cs" Inherits="Gpaf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
   <%-- <script src="../js/jquery-1.4.3.min.js"></script>--%>
    <script src="../js/jquery.min.js"></script>
    <%--<script src="https://js.paystack.co/v1/inline.js"></script>--%>
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
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetStudentForTc") %>',
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
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                    <div class="col-sm-12  no-padding" id="divsearch1" runat="server">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label"><%--Enter Enquiry No. (if you have)--%></label>
                                            <div class="">
                                                <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Enquiry No. (if you have)" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                            <asp:Button ID="Button11" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CausesValidation="false" CssClass="button form-control-blue" OnClick="Button11_Click" Text="View" />
                                        </div>
                                    </div>

                                    <div class="col-sm-12  no-padding">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divadmissiontype" runat="server">
                                            <label class="control-label">Select Type of Admission</label>
                                            <div class="">
                                                <asp:DropDownList ID="drpAdmissionType" runat="server" AutoPostBack="true" CssClass="form-control-blue" OnSelectedIndexChanged="drpAdmissionType_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text="New" Value="New"></asp:ListItem>
                                                    <asp:ListItem Text="Old" Value="Old"></asp:ListItem>
                                                    <asp:ListItem Text="New (Provisional)" Value="Provisional"></asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>



                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="name" style="display: none">
                                            <label class="control-label">Enter Name/S.R. No.</label>
                                            <div class="">
                                                <asp:TextBox ID="txtSrNo" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnTextChanged="txtSrNo_TextChanged"></asp:TextBox>
                                                <div class="text-box-msg">
                                                    <asp:HiddenField ID="hfStudentId" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Date</label>
                                            <div class="">
                                                <asp:TextBox ID="txtAdmissiondate" runat="server" CssClass="form-control-blue datepicker-normal validatetxt" Enabled="false"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                            <label class="control-label">Select Session&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpSession" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" id="divpaymentmode" runat="server">
                                            <label class="control-label">Select Mode of Payment</label>
                                            <div class="">
                                                <asp:DropDownList ID="DropDownMOD" runat="server" AutoPostBack="True"
                                                    CssClass="form-control-blue " OnSelectedIndexChanged="DropDownMOD_SelectedIndexChanged">
                                                   <asp:ListItem>Online</asp:ListItem>
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
                                            <label class="control-label">Father's Name&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                <div class="text-box-msg">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContactNo"
                                                        ErrorMessage="Please enter contact no." SetFocusOnError="True" Style="color: #FF0000" ValidationGroup="a"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
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
                                            <label class="control-label">Select Gender</label>
                                            <div class="">
                                                <asp:DropDownList ID="drpSex" runat="server" CssClass="form-control-blue">

                                                    <asp:ListItem>Male</asp:ListItem>
                                                    <asp:ListItem>Female</asp:ListItem>
                                                    <asp:ListItem>Transgender</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpClassss" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpClassss_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Select Stream &nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="drpBranchss" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                    OnSelectedIndexChanged="drpBranchss_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                         <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Select Medium &nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlMedium" runat="server" CssClass="form-control-blue validatedrp">
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Amount</label>
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

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                            <label class="control-label">Exemption</label>
                                            <div class="">
                                                <asp:TextBox ID="txtConcession" AutoPostBack="true" OnTextChanged="txtConcession_TextChanged" runat="server" Enabled="False" CssClass="form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3  half-width-50 mgbt-xs-15" style="display: none">
                                            <label class="control-label">Cheque Bounce Amount (if Any)</label>
                                            <div class="">
                                                <asp:TextBox ID="txtCBAmount" runat="server" CssClass="form-control-blue" Enabled="false"
                                                    AutoPostBack="true" OnTextChanged="txtCBAmount_TextChanged" Text="0"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Paid Amount</label>
                                            <div class="">
                                                <asp:TextBox ID="txtReceivedAmount" runat="server" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divGateway">
                                            <label class="control-label">Payment Gateway&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:DropDownList runat="server" ID="ddlPaymentGateway" CssClass="form-control-blue validatedrp" onchange="PaymentGatewayChange()">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-8 mgbt-xs-15 hide" id="divChargesDetails" style="background: #fafafa; font-weight: 700; font-size: 12px; padding-right: 5px; padding-left: 2px; margin-top: 10px; padding-top: 7px; border: solid 1px #ccc;">
                                                <div class="col-sm-3 col-xs-3 text-center" style="padding: 0; padding-right: 3px;">
                                                    <img ID="imgLogo" alt="" />
                                                </div>
                                                <div class="col-sm-9 text-left" id="divCharges" style="padding: 0; padding-left: 5px; min-height: 142px; overflow: auto;">
                                                </div>
                                            </div>
                                            <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15 text-center">

                                                <asp:LinkButton ID="Submit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn(this);" class="button form-control-blue" OnClick="Submit_Click">Submit</asp:LinkButton>

                                                <span id="lnkSubmitPayStack" class="button form-control-blue hide" onclick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn(this);payWithPaystack();">Submit</span>
                                                <div id="msgbox" runat="server" style="left: 75px">
                                                </div>
                                            </div>
                                        </div>
                                        
                                    </div>

                                    <div class=" col-sm-12 " id="divdatabind" runat="server">
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
                                                            <asp:Label ID="Label32" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Father's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
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
                                                                title="Cancel"  class="btn menu-icon vd_bd-red vd_red"><i class="fa fa-times"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Print Receipt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label38" runat="server" Text='<%# Bind("RecieptNo") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"
                                                                title="Print"  class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Print Form">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblTemplate" runat="server" Text='<%# Bind("Template") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="Label39" runat="server" Text='<%# Bind("RecieptNo") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkPrintAF" runat="server" OnClick="lnkPrintAF_Click"
                                                                title="Print Admission Form"  class="btn menu-icon vd_bd-green vd_green"><i class="icon-printer"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label34" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                            <asp:Label ID="Label3" runat="server" CssClass="hide" Text='<%# Bind("Cancel") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
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
                <asp:Panel runat="server" class="col-sm-12 " Style="z-index: 99999; position: fixed; background-color: rgba(237, 237, 237, 0.98); border: 3px solid #000; width: 80%; height: 80%; padding: 20px; top: 40px;" Visible="false" ID="divPopups">
                    <div class="col-sm-12 " style="background-color: rgba(237, 237, 237, 0.98); height: 100%; overflow: scroll;">

                        <div class="col-sm-12 ">
                            <div class="col-sm-12">
                                <div id="msg2" runat="server" style="left: 60px;"></div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Session</label>
                                <div class="">
                                    <asp:DropDownList ID="drpSessionPanel" runat="server" CssClass="form-control-blue" Enabled="false"></asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Enter Enquiry No</label>
                                <div class="">
                                    <asp:TextBox ID="TextBox1Panel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Type of Admission</label>
                                <div class="">
                                    <asp:DropDownList ID="drpAdmissionTypePanel" runat="server" CssClass="form-control-blue">
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
                                <label class="control-label">Enter Name/S.R. No.</label>
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
                                <label class="control-label">First Name</label>
                                <div class="">
                                    <%-- ReSharper disable once UnknownCssClass --%>
                                    <asp:TextBox ID="txtStudentNamePanel" runat="server" CssClass="form-control-blue validatetxt2"></asp:TextBox>
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
                                <label class="control-label">Father's Name</label>
                                <div class="">
                                    <%-- ReSharper disable once UnknownCssClass --%>
                                    <asp:TextBox ID="txtFatherNamePanel" runat="server" CssClass="form-control-blue validatetxt2"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Contact No.</label>
                                <div class="">
                                    <%-- ReSharper disable once UnknownCssClass --%>
                                    <asp:TextBox ID="txtContactNoPanel" runat="server" CssClass="form-control-blue validatetxt2"></asp:TextBox>
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
                                <label class="control-label">Class</label>
                                <div class="">
                                    <asp:DropDownList ID="drpClassssPanel" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                        OnSelectedIndexChanged="drpClassssPanel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Stream</label>
                                <div class="">
                                    <asp:DropDownList ID="drpBranchssPanel" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                        OnSelectedIndexChanged="drpBranchssPanel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>



                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Amount</label>
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

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Cheque Bounce Fine</label>
                                <div class="">
                                    <asp:TextBox ID="txtCBAmountPanel" runat="server" CssClass="form-control-blue" Enabled="true"
                                        placeholder="Cheque Bounce Amount (if Any)" AutoPostBack="true" OnTextChanged="txtCBAmountPanel_TextChanged" Text="0"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Paid Amount</label>
                                <div class="">
                                    <asp:TextBox ID="txtReceivedAmountPanel" runat="server" CssClass="form-control-blue" Enabled="false"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>

                                <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>

                                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" OnClick="LinkButton4_Click" CssClass="button-y">Update</asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="LinkButtons" runat="server" OnClick="LinkButtons_Click" CssClass="button-n">Cancel</asp:LinkButton>
                            </div>
                        </div>
                    </div>


                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
              
            </div>



            <br />

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
                <ajaxToolkit:ModalPopupExtender ID="Button10_ModalPopupExtender" runat="server"
                    BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True"
                    PopupControlID="Panel2" TargetControlID="Button10" CancelControlID="Button8">
                </ajaxToolkit:ModalPopupExtender>

            </div>
            
           <div style="display: none; visibility:hidden"> 
                <asp:Label runat="server" ID="data_key"></asp:Label>
                <asp:Label runat="server" ID="data_email"></asp:Label>
                <asp:Label runat="server" ID="data_PseudoUniqueReference"></asp:Label>
                <asp:Label runat="server" ID="data_txnNo"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnTxtNos" />
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnPayStack" runat="server" OnClick="btnPayStack_Click" Text="Submit"></asp:Button>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnPayStack" />
                    </Triggers>
                </asp:UpdatePanel>
               </div>
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Submit" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        function payWithPaystack() {
            //ValidateTextBox('.validatetxt');
            //ValidateDropdown('.validatedrp');
            //return validationReturn(this);
            $("[id*=hdnTxtNos]").val("");
            var data_key = $("[id*=data_key]").html();
            var data_email = $("[id*=data_email]").html();
            var data_amount = (parseFloat($("[id*=txtReceivedAmount]").val()) * 100);
            var data_firstname = $("[id*=txtStudentName]").val();
            var data_lastname = $("[id*=txtStudentName1]").val();
            var data_value = $("[id*=txtContactNo]").val();
            var data_PseudoUniqueReference = parseInt($("[id*=data_PseudoUniqueReference]").html());
            //alert(data_key+" | "+data_email+" | "+data_amount+" | "+data_firstname+" | "+data_lastname+" | "+data_value+" | "+data_PseudoUniqueReference);

            var handler = PaystackPop.setup({
                key: data_key,
                email: data_email,
                amount: data_amount,
                ref: '' + Math.floor((Math.random() * data_PseudoUniqueReference) + 1), 
                firstname: data_firstname,
                lastname: data_lastname,
                metadata: {
                    custom_fields: [
                    {
                        display_name: "Mobile Number",
                        variable_name: "mobile_number",
                        value: data_value
                    }
                  ]
                },
                callback: function (response) {
                    $("[id*=hdnTxtNos]").val(response.reference);
                    $("#<%=btnPayStack.ClientID %>").click();
                },
                onClose: function () {
                    alert('window closed');
                }
            });
            handler.openIframe();
        }
    </script>
    <script>

        $(document).ready(function () {
            $("[id*=LinkButton1]").click(function () {
                window.scrollTo(x - coord, y - coord);
            });
            $("[id*=LinkButton5]").click(function () {
                $("[id*=divPopup]").addClass("hide");
            });
        });
        function PaymentGatewayChange() {
            if ($("[id*=ddlPaymentGateway]").val() == 'PayStack') {
                $("[id*=Submit]").addClass("hide");
                $("#lnkSubmitPayStack").removeClass("hide");
            }
            else {
                $("[id*=Submit]").removeClass("hide");
                $("#lnkSubmitPayStack").addClass("hide");
            }
            if ($("[id*=ddlPaymentGateway]").val() == 'PayUMoney') {
                $("#divChargesDetails").removeClass("hide");
                var maindata = '<%=HttpContext.Current.Session["PayUMoney"] %>';
                var data = maindata.split("##");
                $("#imgLogo").attr("src", "../uploads/CollegeLogo/" + data[0]);
                $("#divCharges").html(data[1]);
            }
            if ($("[id*=ddlPaymentGateway]").val() == 'EassyPay') {
                $("#divChargesDetails").removeClass("hide");
                var maindata = '<%=HttpContext.Current.Session["EassyPay"] %>';
                var data = maindata.split("##");
                $("#imgLogo").attr("src", "../uploads/CollegeLogo/" + data[0]);
                $("#divCharges").html(data[1]);
            }
            if ($("[id*=ddlPaymentGateway]").val() == 'PayStack') {
                $("#divChargesDetails").removeClass("hide");
                var maindata = '<%=HttpContext.Current.Session["PayStack"] %>';
                var data = maindata.split("##");
                $("#imgLogo").attr("src", "../uploads/CollegeLogo/" + data[0]);
                $("#divCharges").html(data[1]);
            }
            if ($("[id*=ddlPaymentGateway]").val() == '') {
                $("#divChargesDetails").addClass("hide");
            }
            var sss = $("[id*=ddlPaymentGateway]").val();

        }
    </script>

</asp:Content>

