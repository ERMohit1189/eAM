<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Jobposting.aspx.cs" Inherits="Jobposting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" Runat="Server">
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" Runat="Server">
    <div id="loader" runat="server"></div>
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(datetime);
            </script>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12  no-padding">
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                 <label class="control-label">From Date</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" class="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                 <label class="control-label">To Date</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" class="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Job Title</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtJobTitle" runat="server" TabIndex="3" class="form-control-blue validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Department</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtDepartment" runat="server" TabIndex="4" class="form-control-blue validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                             <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Designation</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtDesignation" runat="server" TabIndex="5" class="form-control-blue validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                             <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Experience</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtExperience" runat="server" TabIndex="6" class="form-control-blue validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Qualification</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtQualification" runat="server" TabIndex="7" class="form-control-blue validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Salary</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtSalary" runat="server" TabIndex="8" class="form-control-blue validatetxt" onkeypress="return fnNumeric();"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Job Description</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtJobDescription" runat="server" TabIndex="9" class="form-control-blue validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">No. of Post</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtNoofPost" runat="server" TabIndex="10" class="form-control-blue validatetxt" onkeypress="return fnNumeric();"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  btn-a-devices-2-p2 mgbt-xs-15">
                                
                                 <asp:LinkButton ID="Button1" runat="server" class="button form-control-blue" ValidationGroup="a"
                                            OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();"
                                            OnClick="Button1_Click" TabIndex="3"> <i class="fa fa-paper-plane"></i> &nbsp;Submit</asp:LinkButton>
                                <div id="msgbox" runat="server" style="left: 147px !important;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 mgbt-xs-20"   runat="server" id="GRID">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding form-group form-group-sm">

                                <div class="col-sm-12">
                                    <h4>Create Album</h4>
                                    <div class="table-responsive2 table-responsive">

                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue-np vd_white-np">#</th>
                                                    <th class="vd_bg-blue-np vd_white-np">From Date</th>
                                                    <th class="vd_bg-blue-np vd_white-np">To Date</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Job Title</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Department</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Designation</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Experience</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Qualification</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Salary</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Job Description</th>
                                                    <th class="vd_bg-blue-np vd_white-np">No. of Post</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Edit</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Delete</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label13" runat="server" Text='<%# Bind("FromDate","{0:dd-MMM-yyyy}") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ToDate","{0:dd-MMM-yyyy}") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("JobTitle") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Department") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Designation") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Experience") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Qualification") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("Salary") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("JobDescription") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("NoofPost") %>'></asp:Label></td>

                                                            <td class="menu-action" style="width: 40px;">
                                                                <asp:Label ID="lblids" runat="server" Text='<%# Bind("id")%>' Visible="false"></asp:Label>
                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="LinkButton1"
                                                                            CausesValidation="False" runat="server" title="Edit" 
                                                                            class="btn menu-icon vd_bd-green vd_green" OnClick="Linkedit_Click"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                            </td>
                                                            <td class="menu-action" style="width: 40px;">
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" title="Delete" 
                                                                    class="btn menu-icon vd_bd-red vd_red" OnClick="lnkDelete_Click"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>


                                                </asp:Repeater>

                                            </tbody>
                                        </table>

                                    </div>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                            <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                                <div class="col-sm-12 ">
                                 <asp:Button ID="Button5" runat="server" Style="display: none" />

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                 <label class="control-label">From Date</label>
                                <div class=" ">
                                    <asp:TextBox ID="ptxtFromDate" runat="server" TabIndex="1" class="form-control-blue validatetxt2 datepicker-normal"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                 <label class="control-label">To Date</label>
                                <div class=" ">
                                    <asp:TextBox ID="ptxtToDate" runat="server" TabIndex="2" class="form-control-blue validatetxt2 datepicker"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Job Title</label>
                                <div class=" ">
                                    <asp:TextBox ID="ptxtJobTitle" runat="server" TabIndex="3" class="form-control-blue validatetxt2"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Department</label>
                                <div class=" ">
                                    <asp:TextBox ID="ptxtDepartment" runat="server" TabIndex="4" class="form-control-blue validatetxt2"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                             <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Designation</label>
                                <div class=" ">
                                    <asp:TextBox ID="ptxtDesignation" runat="server" TabIndex="5" class="form-control-blue validatetxt2"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                             <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Experience</label>
                                <div class=" ">
                                    <asp:TextBox ID="ptxtExperience" runat="server" TabIndex="6" class="form-control-blue validatetxt2"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Qualification</label>
                                <div class=" ">
                                    <asp:TextBox ID="ptxtQualification" runat="server" TabIndex="7" class="form-control-blue validatetxt2"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Salary</label>
                                <div class=" ">
                                    <asp:TextBox ID="ptxtSalary" runat="server" TabIndex="8" class="form-control-blue validatetxt2"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Job Description</label>
                                <div class=" ">
                                    <asp:TextBox ID="ptxtJobDescription" runat="server" TabIndex="9" class="form-control-blue validatetxt2"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">No. of Post</label>
                                <div class=" ">
                                    <asp:TextBox ID="ptxtNoofPost" runat="server" TabIndex="10" class="form-control-blue validatetxt2"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  btn-a-devices-2-p2 mgbt-xs-15">
                               <asp:Button ID="btnupdate" CssClass="button-y" runat="server" Text="Update" TabIndex="3" OnClick="btnupdate_Click" />
                                                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel"  OnClick="Button4_Click" />
                                                                 <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                            </div>

                                       
                                </div>
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="Button4" PopupControlID="Panel1"
                            TargetControlID="Button5" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close" PopupDragHandleControlID="Panel1">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>

             <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                            <table class="tab-popup text-center">

                                <tr>
                                    <td style="text-align: center">
                                        <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                                        </h4>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" OnClick="Button8_Click" />
                                        &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button-y" Text="Yes" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                            PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>

        </div>
    </div>
 </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
