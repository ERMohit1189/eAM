<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="admission_enquiry.aspx.cs"
    Inherits="_1.AdminAdmissionEnquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>


            <script>

                
                Sys.Application.add_load(scrollbar);
                Sys.Application.add_load(datetime);
            </script>


            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtEnquirydate" runat="server" CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpClass" runat="server" OnSelectedIndexChanged="drpadm_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Session&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSession" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Enquiry For&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpforade" runat="server" CssClass="form-control-blue validatedrp">
                                                <asp:ListItem Text="<--Select-->"></asp:ListItem>
                                                <asp:ListItem>Self</asp:ListItem>
                                                <asp:ListItem>Son</asp:ListItem>
                                                <asp:ListItem>Daughter</asp:ListItem>
                                                <asp:ListItem  Selected="True">Other</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Student's First Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtnamead" placeholder="" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Middle Name</label>
                                        <div class="">
                                            <asp:TextBox ID="txtnamead0" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Last Name&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtnamead1" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                        </div>
                                    </div>

                                   

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Gender&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpgender" runat="server" CssClass="form-control-blue validatedrp">
                                                <asp:ListItem Text="<--Select-->"></asp:ListItem>
                                                <asp:ListItem>Male</asp:ListItem>
                                                <asp:ListItem>Female</asp:ListItem>
                                                <asp:ListItem>Transgender</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Father's Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control-blue validatetxt" onblur="copyName(this)"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Contact No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtcontAdm" runat="server" CssClass="form-control-blue validatetxt"  MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Alternate Contact No.&nbsp;</label>
                                        <div class="">
                                            <asp:TextBox ID="txtmobAdmiss" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                      <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Email</label>
                                        <div class="">
                                            <asp:TextBox ID="txtemaAdmiss" runat="server" CssClass="form-control-blue"  onBlur="ValidateEmails(this);"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Visitor's Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtEnquiredPerson" placeholder="" runat="server" CssClass="form-control-blue validatetxt copyFather"></asp:TextBox>
                                        </div>
                                    </div>

                                     <div class="col-sm-4  half-width-50 mgbt-xs-15 ">
                                        <label class="control-label">Relationship&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drrelat" runat="server" CssClass="form-control-blue validatedrp">
                                                        <asp:ListItem><--Select--></asp:ListItem>
                                                        <asp:ListItem  Selected="True">Father</asp:ListItem>
                                                        <asp:ListItem>Mother</asp:ListItem>
                                                        <asp:ListItem>Grand Father</asp:ListItem>
                                                        <asp:ListItem>Grand Mother</asp:ListItem>
                                                        <asp:ListItem>Uncle</asp:ListItem>
                                                        <asp:ListItem>Anty</asp:ListItem>
                                                        <asp:ListItem>Brother</asp:ListItem>
                                                        <asp:ListItem>Sister</asp:ListItem>
                                                        <asp:ListItem>Other</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Country&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drCountry_SelectedIndexChanged"
                                                CssClass="form-control-blue">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">State&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drstate" runat="server" AutoPostBack="True" CssClass="form-control-blue" OnSelectedIndexChanged="drstate_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">City&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drcity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drcity_SelectedIndexChanged" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Address&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAddressAdmiss" placeholder="PLEASE DON'T WRITE STATE AND CITY NAME HERE" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Reference&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpreferenceaden" runat="server" CssClass="form-control-blue validatedrp">

                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display:none">
                                        <div class="">
                                            <asp:TextBox ID="txtVarValue" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lblref" class="control-label" runat="server" Text="" Visible="false"></asp:Label>&nbsp;<span class="vd_red"></span>
                                        <label class="control-label">Reference Detail</label>
                                        <div class="">
                                            <asp:TextBox ID="txtName" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" placeholder="" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <div class="controls">
                                            <asp:LinkButton ID="LinkButton3" CssClass="button form-control-blue" runat="server" OnClick="LinkButton1_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();">Submit</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 147px !important;"></div>
                                        </div>
                                    </div>
                                  <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                       <hr />
                                  </div>
                                    <div class="col-sm-12 half-width-50 mgbt-xs-15 no-padding">
                                        <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                            <label class="control-label">From&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control-blue datepicker-normal validatetxt22"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                            <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-blue datepicker-normal validatetxt22"></asp:TextBox>
                                            </div>
                                        </div>
                                          <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                            <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <div class="">
                                                 <asp:DropDownList ID="drpClassForView" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <div class="controls">
                                            <asp:LinkButton ID="LnkView" CssClass="button form-control-blue" runat="server" OnClick="LnkView_Click" OnClientClick="ValidateTextBox('.validatetxt22');return validationReturn();">View</asp:LinkButton>
                                            <div id="Div1" runat="server" style="left: 147px !important;"></div>
                                        </div>
                                             
                                    </div>
                                    </div>
                                    <div class="col-sm-12" runat="server" id="divGrid" visible="false">
                                        <div class="table-responsive2 table-responsive">

                                            <table class="table table-striped no-bm table-hover no-head-border table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th class="vd_bg-blue-np vd_white-np">#</th>
                                                        <th class="vd_bg-blue-np vd_white-np">Date</th>
                                                        <th class="vd_bg-blue-np vd_white-np">Enquiry No.</th>
                                                        <th class="vd_bg-blue-np vd_white-np">Class</th>
                                                        <th class="vd_bg-blue-np vd_white-np">Student's Name</th>
                                                        <th class="vd_bg-blue-np vd_white-np">Father's Name</th>
                                                        <th class="vd_bg-blue-np vd_white-np">Contact No.</th>
                                                        <th class="vd_bg-blue-np vd_white-np">Email</th>
                                                        <th class="vd_bg-blue-np vd_white-np">Status</th>
                                                        <th class="vd_bg-blue-np vd_white-np">Username</th>
                                                        <th class="vd_bg-blue-np vd_white-np">Edit</th>
                                                        <th class="vd_bg-blue-np vd_white-np">View</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="Repeater1" runat="server">
                                                        <ItemTemplate>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("Id") %>'></asp:Label></td>
                                                                <td>
                                                                   
                                                                    <asp:Label ID="date" runat="server" Text='<%# Bind("Date") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("EnquiryNo") %>'></asp:Label>

                                                                </td>
                                                                 <td>
                                                                    <asp:Label ID="Label19" runat="server" Text='<%# Bind("AdmissionClass") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </td>
                                                               
                                                                <td>
                                                                    <asp:Label ID="Label20" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label21" runat="server" Text='<%# Bind("EMail") %>'></asp:Label>
                                                                </td>
                                                                 <td>
                                                                    <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                 </td>
                                                                 <td class="text-center">
                                                                    <asp:Label ID="lblLoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br />
                                                                    (<asp:Label ID="Label6s" runat="server" Text='<%# Bind("RecordedDate") %>'></asp:Label>)
                                                                </td>
                                                                <td class="menu-action" style="width: 40px; text-align:center;">
                                                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("Id")%>' Visible="false"></asp:Label>
                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:LinkButton ID="LinkButton1" OnClick="LinkButton2_Click"
                                                                                CausesValidation="False" runat="server" title="Edit" 
                                                                                class="btn menu-icon vd_bd-green vd_green"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>

                                                                </td>
                                                                <td class="menu-action" style="width: 40px;">
                                                                    <asp:Label ID="LBLViewID" runat="server" Text='<%# Bind("Id")%>' Visible="false"></asp:Label>
                                                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:LinkButton ID="LinkViewFull" OnClick="LinkViewFull_Click"
                                                                                CausesValidation="False" runat="server" title="Edit" 
                                                                                class="btn menu-icon vd_bd-green vd_green"><i class="fa fa-eye"></i></asp:LinkButton>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="LinkViewFull" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>

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
                </div>
            </div>




            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown" style="width:50% !important; left:25% !important;">
                    <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                        <div class="col-sm-12 ">

                            <table class="tab-popup">
                                <tr>
                                    <td colspan="2">
                                        <strong>Enquiry No.
                                          <asp:Label ID="Label9" runat="server" Text="Label" ForeColor="#FF6600"></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date <span class="vd_red">*</span><br />
                                        <div>
                                         <asp:DropDownList ID="drpYYPanel" runat="server" OnSelectedIndexChanged="drpYYPanel_SelectedIndexChanged" CssClass="form-control-blue col-xs-4 validatedrp1">
                                        </asp:DropDownList>
                                        <asp:Button ID="Button1" runat="server" Style="display: none" />
                                        <asp:DropDownList ID="drpMMPanel" runat="server" OnSelectedIndexChanged="drpMMPanel_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="drpDDPanel" runat="server" CssClass="form-control-blue col-xs-4">
                                        </asp:DropDownList>
                                            </div>
                                    </td>
                                    <td>Class<span class="vd_red">*</span>
                                       <asp:Button ID="Button5" runat="server" Style="display: none" />
                                        <asp:DropDownList ID="drpClassPanel" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>Session
                                    
                                        <asp:DropDownList ID="drpSessionPanel" runat="server" CssClass="form-control-blue validatedrp1"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                            InitialValue="<-- Select Session -->" Display="Dynamic" CssClass="imp" ValidationGroup="b" SetFocusOnError="true"
                                            ControlToValidate="drpSessionPanel" ErrorMessage="Please select session."></asp:RequiredFieldValidator>
                                    </td>
                                    <td>Relationship
                                   
                                        <asp:DropDownList ID="drpRelationshipPanel" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem><--Select--></asp:ListItem>
                                            <asp:ListItem Selected="True">Father</asp:ListItem>
                                            <asp:ListItem>Mother</asp:ListItem>
                                            <asp:ListItem>Grand Father</asp:ListItem>
                                            <asp:ListItem>Grand Mother</asp:ListItem>
                                            <asp:ListItem>Uncle</asp:ListItem>
                                            <asp:ListItem>Anty</asp:ListItem>
                                            <asp:ListItem>Brother</asp:ListItem>
                                            <asp:ListItem>Sister</asp:ListItem>
                                            <asp:ListItem>Other</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="hide">
                                    <td>Enquiry For
                                    
                                        <asp:DropDownList ID="drpForPanel" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem>Self</asp:ListItem>
                                            <asp:ListItem>Son</asp:ListItem>
                                            <asp:ListItem>Daughter</asp:ListItem>
                                            <asp:ListItem Selected="True">Other</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>



                                </tr>
                               
                                <tr>
                                    <td>Student's First Name<span class="vd_red">*</span>
                                    
                                        <asp:TextBox ID="txtNamePanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNamePanel"
                                            SetFocusOnError="True" ValidationGroup="b" ErrorMessage="*" CssClass="imp" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>Middle Name
                                    
                                        <asp:TextBox ID="txtNamePanel0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td>Last Name
                                   

                                        <asp:TextBox ID="txtNamePanel1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                    <td>Gender
                                   
                                        <asp:DropDownList ID="drpgender0" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>Father's Name
                                        <asp:TextBox ID="txtFatherName0" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                    <td>Contact No. <span class="vd_red">*</span>
                                   
                                        <asp:TextBox ID="txtContactNoPanel" runat="server" CssClass="form-control-blue validatetxt1"  MaxLength="10" onBlur="ChecktenDigitMobileNumber(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactNoPanel"
                                            SetFocusOnError="True" ValidationGroup="b" ErrorMessage="*" CssClass="imp" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td>Alternate Contact No.
                                         <asp:TextBox ID="txtMobileNoPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                    <td>Visitor&#39;s Name <span class="vd_red">*</span>
                                        <asp:TextBox ID="txtEnquiredPerson0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEnquiredPerson0"
                                            SetFocusOnError="True" ValidationGroup="b" ErrorMessage="*" CssClass="imp" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>Email
                                        <asp:TextBox ID="txtEmailPanel" runat="server" CssClass="form-control-blue"  onBlur="ValidateEmails(this);"></asp:TextBox>
                                    </td>
                                    <td>Country <span class="vd_red">*</span>
                                    
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpCountryPanel" runat="server" OnSelectedIndexChanged="drpCountryPanel_SelectedIndexChanged"
                                                    CssClass="form-control-blue">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td>State
                                    
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpStatePanel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpStatePanel_SelectedIndexChanged"
                                                    CssClass="form-control-blue">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>City
                                    
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpCityPanel" runat="server" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>Address
                                   
                                        <asp:TextBox ID="txtAddressPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                    </td>
                                    <td>Reference
                                    
                                        <asp:DropDownList ID="drpReferencePanel" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                               
                                <tr style="display:none">
                                    <td>
                                        <asp:Label ID="lblVarValuePanel" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVarValuePanel" runat="server" CssClass="textbox"></asp:TextBox>

                                    </td>

                                </tr>
                                <tr>
                                    <td>Reference Details
                                   
                                        <asp:TextBox ID="txtRefrenceNamePanel" ReadOnly="true" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                    </td>
                                    <td>Remark
                                   
                                        <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>
                                    </td>
                                </tr>
                               
                                <tr>
                                    
                                </tr>
                                <tr style="display: none">
                                    <td></td>
                                    <td>How did you know about our Institution?
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td></td>
                                    <td>
                                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" Width="100%" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="Button3" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();" CssClass="button-y" OnClick="Button3_Click" Text="Update" ValidationGroup="b" />&nbsp;&nbsp;
                                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
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
                            <td align="center">
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>

                        <tr>
                            <td align="center">


                                <asp:Button ID="Button8" runat="server" CssClass="button-n" OnClick="Button8_Click" Text="No" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button-y" OnClick="btnDelete_Click" Text="Yes" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </ajaxToolkit:ModalPopupExtender>
            </div>
            <script>
                function copyName(tis)
                {
                    $('.copyFather').val($(tis).val());
                }
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
