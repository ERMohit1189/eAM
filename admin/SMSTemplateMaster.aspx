<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SMSTemplateMaster.aspx.cs" Inherits="SMSTemplateMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">

        function fnNumeric() {
            var code = window.event.keyCode;
            if ((code >= 48 && code <= 57) || (code === 45) || (code === 46)) {
                /*checknos = true;*/
                return true;
            }
            else {
                /*checknos= false;*/
                window.event.keyCode = 0;
                return false;
            }
        }
        function ChecktenDigitss(inputtxt) {
            var phoneno = /^\d+$/;
            if (inputtxt.value.match(phoneno) && inputtxt != null) {
                inputtxt.style.border = "1px solid #D5D5D5";
                return true;
            }
            else {
                if (inputtxt.value != "") {
                    inputtxt.style.border = "1px solid Red";
                    inputtxt.value = "";
                    inputtxt.focus();
                    return false;
                }
            }
        }
        function chkOrder(tis) {
            var sts = 0;
            var tis_id = $(tis).attr('id');
            var rowCnt = $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_Gridview1 tbody tr").length;
            if (rowCnt>2) {
                for (var i = 1; i < rowCnt; i++) {
                    var txtId = $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_Gridview1 tbody tr:eq(" + 1 + ") td:eq(2) input[type=text]").attr('id');
                    var txtVal = $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_Gridview1 tbody tr:eq(" + 1 + ") td:eq(2) input[type=text]").val();
                    if (tis_id != txtId && txtVal != "" && txtVal == $(tis).val()) {
                        sts = 1;
                    }
                }
                if (sts == 1) {
                    $(tis).val('');
                    $(tis).css('border', '1px solid red');
                }
                else {
                    $(tis).css('border', '1px solid #ccc');
                }
            }
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">

                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Page Name (SMS Send From)<span class="vd_red">*</span></label>
                                        <div class="mgtp-6">
                                            <asp:DropDownList ID="ddlPageName" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlPageName_SelectedIndexChanged">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                
                                                <asp:ListItem Value="CompositeFee">Composite Fee</asp:ListItem>
                                                
                                                <asp:ListItem Value="AdmissionFormFee">Admission Form Fee</asp:ListItem>
                                                <asp:ListItem Value="TransferCertificateFee">Transfer Certificate Fee</asp:ListItem>
                                                <asp:ListItem Value="CharacterCertificateFee">Character Certificate Fee</asp:ListItem>
                                                <asp:ListItem Value="OtherFee">Other Fee</asp:ListItem>
                                                <asp:ListItem Value="AdditionalFee">Additional Fee</asp:ListItem>
                                                <asp:ListItem Value="ProductFee">Product Fee</asp:ListItem>
                                                <asp:ListItem Value="LibraryFine">Library Fine</asp:ListItem>

                                                <asp:ListItem Value="TransactionClearance">Transaction Clearance</asp:ListItem>

                                                <asp:ListItem Value="ReceiptCancellation">Receipt Cancellation</asp:ListItem>
                                                <asp:ListItem Value="FeeOverdueReminder">Fee Overdue Reminder</asp:ListItem>
                                                <asp:ListItem Value="StudentRegistration">Student Registration</asp:ListItem>

                                                
                                                <asp:ListItem Value="StaffRegistration">Staff Registration</asp:ListItem>
                                                <asp:ListItem Value="AdminSignup">Admin Account Creation</asp:ListItem>

                                                
                                                <asp:ListItem Value="GuardianLoginCredentials">Guardian Login Credentials</asp:ListItem>
                                                <asp:ListItem Value="StudentLoginCredentials">Student Login Credentials</asp:ListItem>

                                                <asp:ListItem Value="ForgotPassword">Forgot Password</asp:ListItem>

                                                
                                                <asp:ListItem Value="StudentGatePass">Student Gate Pass</asp:ListItem>
                                                <asp:ListItem Value="VisitorGatePass">Visitor Gate Pass</asp:ListItem>

                                                <asp:ListItem Value="AdmissionEnquiry">Admission Enquiry</asp:ListItem>
                                                <asp:ListItem Value="AdmissionPortal">Admission Portal OTP</asp:ListItem>
                                                <asp:ListItem Value="TransportAlert">Transport Alert</asp:ListItem>

                                                <asp:ListItem Value="ResultMessage">Result Message</asp:ListItem>
                                                <asp:ListItem Value="HolidayMessage">Holiday Message</asp:ListItem>
                                                <asp:ListItem Value="GreetingMessage">Greeting Message</asp:ListItem>
                                                <asp:ListItem Value="CustomMessage">Custom Message</asp:ListItem>

                                                <asp:ListItem Value="StudentDailyAttendanceManual">Student Daily Attendance (Manual)</asp:ListItem>
                                                <asp:ListItem Value="StudentDailyAttendanceAuto">Student Daily Attendance (Auto)</asp:ListItem>

                                                
                                                <asp:ListItem Value="StaffAttendanceManual">Staff Attendance (Manual)</asp:ListItem>
                                                <asp:ListItem Value="StaffAttendanceAuto">Staff Attendance (Auto)</asp:ListItem>

                                                <%--<asp:ListItem Value="EmploymentFormAlert">Employment Form Alert</asp:ListItem>--%>
                                                <asp:ListItem Value="AlumniPortal">Alumni Portal</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Template For<span class="vd_red">*</span></label>
                                        <div class="mgtp-6">
                                            <asp:DropDownList ID="ddlTemplateFor" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlTemplateFor_SelectedIndexChanged">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                        <asp:DropDownList ID="ddlParametersHide" runat="server" CssClass="form-control-blue hide">
                                                                
                                         </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                    <label class="control-label">Send For<span class="vd_red">*</span></label>
                                    <div class="mgtp-6">
                                        <asp:DropDownList ID="drpSendFor" runat="server" CssClass="form-control-blue validatedrp" 
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlTemplateFor_SelectedIndexChanged">
                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
                                            <asp:ListItem Value="SMS">SMS</asp:ListItem>
                                            <asp:ListItem Value="WhatsApp">WhatsApp</asp:ListItem>
                                            <asp:ListItem Value="Email">Email</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                </div>
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-4">
                                        <label class="control-label">Parameters for Template<span class="vd_red">*</span></label>
                                        <div class="table-responsive2 table-responsive">
                                            
                                            <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="txtIndex" runat="server" Visible="false" Text='<%# Container.DataItemIndex %>'></asp:Label>
                                                            <asp:LinkButton ID="ButtonRemove" runat="server" class="btn menu-icon vd_bd-red vd_red" OnClick="ButtonRemove_Click" Style="padding: 1px 5px;"><i class="fa fa-close" style="font-size: 14px;"></i></asp:LinkButton>

                                                        </ItemTemplate>
                                                        
                                                        <HeaderStyle Width="50" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle Width="50" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Parameter Name *">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlParameters" runat="server" CssClass="form-control-blue validatedrp">
                                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="200" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Display No. *" >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control-blue validatetxt" onblur="chkOrder(this)"
                                                                Value='<%# Container.DataItemIndex+1 %>' Enabled="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="100" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:LinkButton ID="ButtonAdd" runat="server" class="btn menu-icon vd_bd-green vd_green" OnClick="ButtonAdd_Click" Style="padding: 2px 5px;"><i class="fa fa-plus-circle" style="font-size: 30px;"></i></asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="col-sm-8 no-padding">
                                        <label class="control-label">SMS Template&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtSMSTemplate" runat="server" CssClass="form-control-blue validatetxt" TextMode="MultiLine" placeholder="Write SMS Template format here..." Style="height: 130px;"></asp:TextBox>
                                                <div class="text-box-msg text-danger">
                                                    Note:- For passing parameter, please copy and paste <span style="color:blue; font-weight:bold; font-size:16px;">{{{}}}</span> placeholder. Please don't put any character just after the placeholder.
                                                    <%--Note:-  For passing parameter, please copy and paste&nbsp;&nbsp;<span style="color:blue; font-weight:bold; font-size:16px;">{{{}}}</span>&nbsp;&nbsp;placeholder.--%>
                                                    <br />
                                                    <asp:Label runat="server" ID="lblDummyMsg"></asp:Label>                                                    
                                                </div>
                                            </div>
                                        <br /><br />
                                    </div>
                                   
                                </div>
                                 <div class="col-sm-12  mgbt-xs-15 text-center">
                                     <br /><br />
                                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="button form-control-blue" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                    <div runat="server" id="msgbox" style="left: 55px;"></div>
                                        <br />
                                    </div>
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Page Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2PageName" runat="server" Text='<%# Bind("PageName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="130px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Template For">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7TemplateFor" runat="server" Text='<%# Bind("TemplateFor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="130px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Send For">
                                                     <ItemTemplate>
                                                         <asp:Label ID="Label7SendFor" runat="server" Text='<%# Bind("SendFor") %>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="130px" CssClass="vd_bg-blue vd_white" />
                                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="SMS Parameters">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3ParameterName" runat="server" Text='<%# Bind("Parameter") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SMS Template">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Template") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="500px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="grid_heading_default" />
                                            <RowStyle CssClass="grid_details_default" />
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                            <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                            <table class="tab-popup text-center">
                                                <tr>
                                                    <td style="text-align:center;">
                                                        <h4>Do you really want to delete this record?</h4>
                                                        <asp:Label ID="lblPageName" runat="server" Visible="False"></asp:Label>
                                                            <asp:Label ID="lblTemplateFor" runat="server" Visible="False"></asp:Label>
                                                            <asp:Label ID="lblSendFor" runat="server" Visible="False"></asp:Label>
                                                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td style="text-align:center; height:50px;">
                                                        <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CssClass="button-n" />
                                                        &nbsp; &nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Yes" CssClass="button-y" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                            </table>

                                        </asp:Panel>
                                        <%-- ReSharper disable once Asp.InvalidControlType --%>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                                            PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </div>


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>

