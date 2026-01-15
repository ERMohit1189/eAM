<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Employmentform.aspx.cs" Inherits="admin_Employmentform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <script>
        function clicked(txtbox, drp) {
            var flag = true;
            var textBox = document.getElementsByClassName(txtbox);
            var dropDown = document.getElementsByClassName(drp);
            for (var i = 0; i < textBox.length; i++) {
                if (textBox[i].value === '') {
                    textBox[i].style.border = '1px solid red';
                    flag = false;
                }
                else {
                    textBox[i].style.border = '1px solid #CCC';
                }
            }
            for (var i = 0; i < dropDown.length; i++) {
                if (dropDown[i].options[0].selected === true) {
                    dropDown[i].style.border = '1px solid red';
                    flag = false;
                }
                else {
                    dropDown[i].style.border = '1px solid #CCC';
                }
            }
            return flag;
        }


    </script>
    <script>
        function CheckIntegerValueonKeyUp(e, TextBox) {
            var index = TextBox.value.toLowerCase().indexOf(String.fromCharCode(e.keyCode).toLowerCase());
            var values = TextBox.value;
            var amount = "";

            if (e.keyCode >= 65 && e.keyCode <= 90) {
                for (var i = 0; i < values.length; i++) {
                    if (i !== index) {
                        amount = amount + values[i];
                    }
                }
                TextBox.value = amount;
            }
            else if (e.keyCode >= 106 && e.keyCode <= 109) {
                for (var i = 0; i < values.length; i++) {
                    if (i !== index) {
                        amount = amount + values[i];
                    }
                }
                TextBox.value = amount;
            }
            else if (e.keyCode === 111) {
                for (var i = 0; i < values.length; i++) {
                    if (i !== index) {
                        amount = amount + values[i];
                    }
                }
                TextBox.value = amount;
            }
            else if (e.keyCode >= 186 && e.keyCode <= 189) {
                for (var i = 0; i < values.length; i++) {
                    if (i !== index) {
                        amount = amount + values[i];
                    }
                }
                TextBox.value = amount;
            }
            else if (e.keyCode >= 191 && e.keyCode <= 192) {
                for (var i = 0; i < values.length; i++) {
                    if (i !== index) {
                        amount = amount + values[i];
                    }
                }
                TextBox.value = amount;
            }
            else if (e.keyCode >= 219 && e.keyCode <= 222) {
                for (var i = 0; i < values.length; i++) {
                    if (i !== index) {
                        amount = amount + values[i];
                    }
                }
                TextBox.value = amount;
            }
            else if (e.keyCode === 190 || e.keyCode === 110) {
                for (var i = 0; i < values.length; i++) {
                    if (values[i] !== '.') {
                        amount = amount + values[i];
                    }
                }
                TextBox.value = amount;
            }
        }
    </script>
    <script>
        function showHusbandNametxt(drpGender, txtBox) {
            var textBox = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_" + txtBox);
            textBox.value = "";
            if (drpGender.value === "Female") {
                textBox.style.display = "block";
            }
            else {
                textBox.style.display = "none";
            }
        }
        
        Sys.Application.add_load(datetime);
    </script>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="maindiv" runat="server">
                                     <div class="col-sm-3 ">
                                        <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control-blue datepicker-normal validatetxt" Enabled="false"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Applicant's Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtApplicantName" CssClass="form-control-blue validatetxt" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <label class="control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSex" runat="server" onchange="showHusbandNametxt(this,'txtHusbandName')" CssClass="form-control-blue validatedrp">
                                                <asp:ListItem Text="<--Select-->" Value="<--Select-->"></asp:ListItem>
                                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                                <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Father's Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtFatherName" CssClass="form-control-blue validatetxt" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 " style="display: none">
                                        <label class="control-label">Husband's Name</label>
                                        <div class="">
                                            <asp:TextBox ID="txtHusbandName" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 ">
                                        <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtContactNo" placeholder="" CssClass="form-control-blue validatetxt" MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 ">
                                        <label class="control-label">Email&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtEmail" placeholder="" CssClass="form-control-blue validatetxt" onBlur="ValidateEmails(this);" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-3 ">
                                        <label class="control-label">Designation&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpdes" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Amount&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAmount" placeholder="" onkeyup="CheckIntegerValueonKeyUp(event,this);" CssClass="form-control-blue validatetxt" runat="server" Text=""></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Education Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpEducationType" runat="server" CssClass="validatedrp form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drpEducationType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-9 mgbt-xs-15">
                                        <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                        <div class="mgtp-6">
                                            <asp:CheckBoxList ID="chkSubject" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow" CssClass="vd_checkbox checkbox-success"></asp:CheckBoxList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" class="btn vd_btn vd_bg-blue"
                                             OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn(this);" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px"></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <br />
                                        <hr />
                                    </div>
                                    

                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-12 no-padding">
                                    <div class="col-sm-3  no-padding">
                                        <label class="control-label">From</label>
                                        <div class="">
                                            <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 ">
                                        <label class="control-label">To</label>
                                        <div class="">
                                            <asp:TextBox ID="txtTo" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50 ">
                                        <label class="control-label">Status</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="">All</asp:ListItem>
                                                <asp:ListItem Value="0">Active</asp:ListItem>
                                                <asp:ListItem Value="1">Cancelled</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-3  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkView" runat="server" class="btn vd_btn vd_bg-blue" OnClick="lnkView_Click"><i class="fa fa-eye"></i>&nbsp;View</asp:LinkButton>
                                    </div>
                                         
                                        </div>
                                    <div class="table-responsive2 table-responsive">
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <HeaderTemplate>
                                                <table class="table table-striped table-hover no-bm no-head-border table-bordered">
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Ref. No.</th>
                                                        <th class="text-left">Applicant's Name</th>
                                                        <th class="text-left">Father's Name</th>
                                                        <th>Date</th>
                                                        <th>Gender</th>
                                                        <th>Contact No.</th>
                                                        <th>Designation</th>
                                                        <th>Education Type</th>
                                                        <th>Amount</th>
                                                        <th>Status</th>
                                                        <th>Paid</th>
                                                        <th>Edit</th>
                                                        <th>Cancel</th>
                                                        <th>Print</th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 50px">
                                                        <asp:Label ID="EFDate" runat="server" Text='<%# Eval("EFDate") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="IsCancel" runat="server" Text='<%# Eval("IsCancel") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                        <%# Container.ItemIndex+1 %>
                                                    </td>
                                                    <td><%# Eval("Id") %></td>
                                                    <td class="text-left"><%# Eval("EmpName") %></td>
                                                    <td class="text-left"><%# Eval("EmpFather") %></td>
                                                    <td><%# Eval("EFDate") %></td>
                                                    <td><%# Eval("EmpGender") %></td>
                                                    <td><%# Eval("EmpContactNo") %></td>
                                                    <td><%# Eval("EmpDesName") %></td>
                                                    <td><%# Eval("EducationType") %></td>
                                                    <td class="text-right"><asp:Label ID="lblAmount" runat="server" Text='<%# Eval("EmpAmount") %>'></asp:Label></td>
                                                    <td><%# Eval("IsCancel").ToString()=="True"?"Cancelled":"Paid" %></td>
                                                    <td class="text-right"><asp:Label ID="lblPaid" runat="server" Text='<%# Eval("IsCancel").ToString()=="True"?"0.00":Eval("EmpAmount") %>'></asp:Label></td>
                                                    <td class="text-center" style="width: 40px">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" title="Edit"  OnClick="lnkEdit_Click"
                                                                    class="btn menu-icon vd_bd-yellow vd_yellow" style="padding: 0px 6px;"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="lnkEdit" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td class="text-center" style="width: 40px">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click"
                                                                    title="Cancel"  class="btn menu-icon vd_bd-red vd_red" style="padding: 0px 6px;"><i class="fa fa-close"></i></asp:LinkButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="lnkCancel" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td class="text-center" style="width: 40px">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="lnkPrintEF" runat="server" OnClick="lnkPrintEF_Click"
                                                                    title="Print"  class="btn menu-icon vd_bd-red vd_red" style="padding: 0px 6px;"><i class="fa fa-print"></i></asp:LinkButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="lnkPrintEF" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            
                                        </asp:Repeater>
                                        <tr runat="server" id="trGooter" visible="false">
                                                    <td colspan="9" class="text-right" style="font-weight:bold;">Total</td>
                                                    <td class="text-right" style="font-weight:bold;"><asp:Label ID="lblTotalAmount" runat="server"></asp:Label></td>
                                                    <td class="text-right"></td>
                                                    <td class="text-right" style="font-weight:bold;"><asp:Label ID="lblTotalPaid" runat="server"></asp:Label></td>
                                                    <td class="text-right" colspan="3"></td>
                                                </tr>
                                                </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <%-- <div runat="server" id="myModal">--%>
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                        <div class="col-sm-12 ">

                            <table class="tab-popup">
                                <tr>
                                    <td>Applicant's Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEditdate" CssClass="form-control-blue datepicker-normal" runat="server" Visible="false" ></asp:TextBox>
                                        <asp:TextBox ID="txtApplicantNamePanel" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Gender
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpSexPanel" runat="server" onchange="showHusbandNametxt(this,'txtHusbandNamePanel')" CssClass="drpPanel">
                                            <asp:ListItem Text="<--Select Gender-->" Value="<--Select Gender-->"></asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                            <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Father's Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFatherNamePanel" CssClass="form-control-blue" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr style="display:none">
                                    <td>Husband's Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHusbandNamePanel" runat="server" Style="display: none"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Enter Contact No.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactNoPanel" CssClass="form-control-blue" runat="server" MaxLength="10"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Email
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailPanel" CssClass="form-control-blue" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Education Type
                                    </td>
                                    <td>

                                        <asp:DropDownList ID="drpEducationTypePanel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpEducationTypePanel_SelectedIndexChanged" CssClass="drpPanel">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Designation
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpdesPanel" runat="server" CssClass="drpPanel"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Enter Amount
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAmountPanel" runat="server" Text=""></asp:Label>
                                      <%--  <asp:TextBox ID="txtAmountPanel" onkeyup="CheckIntegerValueonKeyUp(event,this);" 
                                            CssClass="form-control-blue" runat="server" Enabled="false"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>

                                    <td colspan="2">
                                        <asp:CheckBoxList ID="chkSubjectPanel" CssClass="vd_checkbox" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:LinkButton ID="lnkUpdate" runat="server"
                                            CssClass="button-y" OnClientClick="return clicked('txtboxPanel', 'drpPanel');" type="button" OnClick="lnkUpdate_Click">Update</asp:LinkButton>

                                        <asp:LinkButton ID="lnkNo" runat="server"
                                            CssClass="button-n" type="button" OnClientClick="return popuphide('ContentPlaceHolder1_ContentPlaceHolderMainBox_myModal');">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="Button2_ModalPopupExtender" runat="server"
                    BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True" CancelControlID="lnkNo"
                    PopupControlID="Panel1" TargetControlID="Button2">
                </ajaxToolkit:ModalPopupExtender>

            </div>

            <div style="overflow: auto; width: 1px; height: 1px">

                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">

                        <tr>
                            <td style="text-align: center;">
                                <h4>Are you sure you want to cancel this?
                                    <asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                </h4>
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: center;">
                                <asp:LinkButton ID="Button9" runat="server" CssClass="button-n">No</asp:LinkButton>
                               &nbsp;&nbsp;
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="button-y" OnClick="btnDelete_Click">Yes</asp:LinkButton>
                                <%--<asp:Button ID="Button9" runat="server" CssClass="button-n" Text="No" CssClass="button-y" />
                               &nbsp;&nbsp;
                                        <asp:Button ID="btnDelete" runat="server" CssClass="button-y" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" />--%>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:LinkButton ID="LinkButton1" runat="server" Text="Button" Style="display: none">&nbsp;</asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" BackgroundCssClass="popup_bg"
                    TargetControlID="LinkButton1" PopupControlID="Panel2" CancelControlID="Button9">
                </ajaxToolkit:ModalPopupExtender>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

