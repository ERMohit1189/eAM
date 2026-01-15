<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Career.aspx.cs" Inherits="website_Career" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script>
                

            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class=" no-padding form-group form-group-sm">

                                    <div class="row">
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">From Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtFromDate" placeholder="YYYY-MM-DD" runat="server" class="form-control-blue datepicker-normal validatetxt" TabIndex="1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtToDate" placeholder="YYYY-MM-DD" runat="server" class="form-control-blue datepicker-normal validatetxt" TabIndex="2"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Job Title&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtTitle" runat="server" class="form-control validatetxt" TabIndex="3"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                        </div>
                                     <div class="row">
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Department&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtDep" runat="server" class="form-control validatetxt" TabIndex="4"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Designation&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtPost" runat="server" class="form-control validatetxt" TabIndex="5"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Experience&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtExp" runat="server" class="form-control validatetxt" TabIndex="6"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                         </div>
                                     <div class="row">
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Qualification&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtQuali" runat="server" class="form-control validatetxt" TabIndex="7"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Salary&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtSalary" runat="server" class="form-control validatetxt" TabIndex="8"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Job Description&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtJobDisc" TextMode="MultiLine" class="form-control validatetxt" runat="server" TabIndex="9"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                         </div>
                                     <div class="row">
                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">No. of Post&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtNoofPos" runat="server" class="form-control validatetxt" TabIndex="10"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 half-width-50 mgbt-xs-15 btn-a-devices-2-p2">
                                        <asp:LinkButton ID="Button1" runat="server" class="button form-control-blue" ValidationGroup="a"
                                            OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();"
                                            OnClick="Button1_Click" TabIndex="11"> <i class="fa fa-paper-plane"></i> &nbsp;Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 147px !important;"></div>
                                    </div>
                                         </div>
                                </div>

                            </div>


                            <div class="col-sm-12">

                                <h3><strong>Career </strong>List</h3>
                             <%--   <div class="table-responsive2 table-responsive">

                                    <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                        <thead>
                                            <tr>
                                                <th class="vd_bg-blue-np vd_white-np">S.No.</th>
                                                <th class="vd_bg-blue-np vd_white-np">FROM DATE</th>
                                                <th class="vd_bg-blue-np vd_white-np">TO DATE</th>
                                                <th class="vd_bg-blue-np vd_white-np">TITLE</th>
                                                <th class="vd_bg-blue-np vd_white-np">DEPARTMENT</th>
                                                <th class="vd_bg-blue-np vd_white-np">EXPERIENCE</th>
                                                <th class="vd_bg-blue-np vd_white-np">SALARY</th>
                                                <th class="vd_bg-blue-np vd_white-np">NO. OF POSITION</th>
                                                <th class="vd_bg-blue-np vd_white-np">DESIGNATION</th>
                                                <th class="vd_bg-blue-np vd_white-np">QUALIFICATION</th>
                                                <th class="vd_bg-blue-np vd_white-np">JOB DESCRIPTION</th>
                                                <th class="vd_bg-blue-np vd_white-np">Edit</th>
                                                <th class="vd_bg-blue-np vd_white-np">Delete</th>

                                            </tr>
                                        </thead>
                                        <tbody>--%>
                                            <asp:Repeater ID="Repeater1" runat="server">
                                              <%--  <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("SRNo") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("FDate") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("TDate") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("JobTitle") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("DepartMent") %>'></asp:Label>
                                                        </td>
                                                          <td>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Experience") %>'></asp:Label>
                                                        </td>
                                                          <td>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("Salary") %>'></asp:Label>
                                                        </td>
                                                         <td>
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("NoofPosition") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Post") %>'></asp:Label>
                                                        </td>
                                                      
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("Qualification") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("JobDescription") %>'></asp:Label>
                                                        </td>

                                                        <td class="menu-action" style="width: 40px;">
                                                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("JobId")%>' Visible="false"></asp:Label>
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="LinkButton1"
                                                                        CausesValidation="False" runat="server" title="Edit" 
                                                                        class="btn menu-icon vd_bd-green vd_green" OnClick="LinkButton1_Click"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>

                                                        </td>
                                                        <td class="menu-action" style="width: 40px;">
                                                            <asp:LinkButton ID="lnkDelete" runat="server"
                                                                CausesValidation="False" title="Delete" 
                                                                class="btn menu-icon vd_bd-red vd_red" OnClick="lnkDelete_Click"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>--%>


                                                   <HeaderTemplate>
                                           
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li class="boxborder">
                                                <div class="row  ">
                                                    <div class="col-lg-1 col-md-2  ">
                                                        <b><span class="panel-title3  hide-title" style="font-size:15px;">S.No.</span></b>
                                                        <asp:Label ID="lblsr" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-5  ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">FROM DATE :</span></b>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FDate") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-5 ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">TO DATE :</span></b>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("TDate") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-2  ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">TITLE :</span></b>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("JobTitle") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-5  ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">DEPARTMENT :</span></b>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("DepartMent") %>'></asp:Label>
                                                    </div>
                                                     <div class="col-lg-3 col-md-5 ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">EXPERIENCE :</span></b>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Experience") %>'></asp:Label>
                                                    </div>

                                                    <div class="col-lg-3 col-md-7 ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">SALARY :</span></b>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("Salary") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-5  ">
                                                      <b>   <span class="panel-title3  hide-title" style="font-size:15px;">NO. OF POSITION :</span></b>
                                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("NoofPosition") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-7  ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">DESIGNATION :</span></b>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("Post") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-5 col-md-5  ">
                                                       <b>  <span class="panel-title3 hide-title" style="font-size:15px;">QUALIFICATION :</span></b>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("Qualification") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-12 col-md-6  ">
                                                       <b>  <span class="panel-title3  hide-title" style="font-size:15px;">JOB DESCRIPTION :</span></b>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("JobDescription") %>'></asp:Label>
                                                    </div>
                                                   
                                                    <div class="col-lg-4 col-md-6 ">
                                                        <div class="col-lg-4 col-md-6 col-xs-4">
                                                              <asp:Label ID="lblid" runat="server" Text='<%# Bind("JobId")%>' Visible="false"></asp:Label>
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="LinkButton1"
                                                                        CausesValidation="False" runat="server" title="Edit" 
                                                                        class="btn menu-icon vd_bd-green vd_green" OnClick="LinkButton1_Click"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-lg-4 col-md-6 col-xs-4">
                                                             <asp:LinkButton ID="lnkDelete" runat="server"
                                                                CausesValidation="False" title="Delete" 
                                                                class="btn menu-icon vd_bd-red vd_red" OnClick="lnkDelete_Click"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </div>
   

                                                    </div>
                                                </div>
                                               
                                            </li>
                                        </ItemTemplate>


                                            </asp:Repeater>

                                     <%--   </tbody>
                                    </table>

                                </div>--%>
                            </div>
                        </div>
                    </div>


                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                            <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                                <div class="col-sm-12 ">
                                    <table class="tab-popup">

                                        <tr>
                                            <td>From Date</td>
                                            <td>
                                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="Fdate0" placeholder="YYYY-MM-DD" runat="server" class="form-control-blue datepicker-normal" TabIndex="1"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>To Date</td>
                                            <td>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="Tdate0" placeholder="YYYY-MM-DD" runat="server" class="form-control-blue datepicker-normal" TabIndex="2"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Job Title</td>
                                            <td>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>

                                                    <asp:TextBox ID="txtTitle0" runat="server" class="form-control" TabIndex="3"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Department</td>
                                            <td>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>

                                                    <asp:TextBox ID="txtDep0" runat="server" class="form-control" TabIndex="4"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Designation</td>
                                            <td>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>

                                                    <asp:TextBox ID="txtPost0" runat="server" class="form-control" TabIndex="5"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Experience</td>
                                            <td>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>

                                                    <asp:TextBox ID="txtExp0" runat="server" class="form-control" TabIndex="6"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Qualification</td>
                                            <td>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>

                                                    <asp:TextBox ID="txtQuali0" runat="server" class="form-control" TabIndex="7"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Salary</td>
                                            <td>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>

                                                    <asp:TextBox ID="txtSalary0" runat="server" class="form-control" TabIndex="8"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Job Description</td>
                                            <td>
                                                <asp:TextBox ID="txtJobDisc0" TextMode="MultiLine" class="form-control" runat="server" TabIndex="9"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>No. of Post</td>
                                            <td>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>

                                                    <asp:TextBox ID="txtNoofPos0" runat="server" class="form-control" TabIndex="10"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="2">
                                                <center>
                                               <asp:Button ID="btnupdate" CssClass="button-y" runat="server" Text="Update" TabIndex="11"  OnClick="btnupdate_Click" />
                                                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel"  TabIndex="12"  />
                                                                 <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                            </center>
                                            </td>
                                        </tr>

                                    </table>
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
                                        <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" />
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

