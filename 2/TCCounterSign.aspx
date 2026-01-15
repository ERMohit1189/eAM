<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TCCounterSign.aspx.cs" Inherits="Templates" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
          <style>
              .certificate-container {     
                  width:1024px !important;
                  margin:2px auto;
                  padding:4px;
                  border: 2px solid #000 !important;
                  background-color: white !important;
                  position: relative;
                  font-size: 12px;    
                  background-color:white;
                }
              .certificate-table {
                  width: 100%;
                  border-collapse: collapse;
                  margin-bottom: 5px;
                  padding:4px;
                  background-color: white !important;
                   border: 1px solid #000 !important;                 
                }

                .records-table {
                  width: 100%;
                  border-collapse: collapse;
                  margin-bottom:6px;       
                   background-color: white !important;
                }

                .header {
                  text-align: center;
                  padding:3px 0;
                }

                .school-title {
                  text-align: center;
                }

                .school-title h1 {
                  margin: 0;
                  font-size: 24px;
                }

                .school-title p {
                  margin: 3px 0;
                }

                .form-subtitle {
                  font-weight: bold;
                  margin: 3px 0;
                }

                .logo-container {
                  text-align: center;
                  margin-bottom: 10px;
                  position:absolute;
                  padding-top:10px;
                  left:0;
                  margin-left:40px;
                }

                .school-logo {
                  width: 60px;
                  height: auto;
                }
                th, td {
                  border: 1px solid #000;
                  padding:4px 4px;
                  text-align: left;
                  vertical-align: top;
                  font-size: 12px;
                   background-color: white !important;
                }
                .heading {
                  font-weight: bold;                  
                      font-size: 12px !important;
                }
                th {
                  background-color: #f2f2f2;
                  text-align: center;
                }
                

                .data-cell {
                  vertical-align: top;
                }

                .form-row {
                  padding:3px 5px;
                }

                .class-label {
                  text-align: center;
                  vertical-align: middle;
                  font-weight: bold;
                  writing-mode: vertical-lr;
                  transform: rotate(180deg);
                  white-space: nowrap;
                  font-size:12px;border-left: 1px solid #000 !important;
                }

                .class-number {
                  text-align: center;
                  font-weight: bold;
                }

                .centered-content {
                  text-align: center;
                }

                .scholar-name, .institution-name {
                  text-align: center;
                  vertical-align: middle;
                  font-weight: bold;
                }

                .certificate-overlay {
                  position: absolute;
                  top: 50%;
                  left: 50%;
                  transform: translate(-50%, -50%);
                  opacity: 0.5;
                  z-index: 1;
                  pointer-events: none;
                }

                .certified-stamp {
                  width: 200px;
                  height: auto;
                  transform: rotate(-20deg);
                }

                .certifications {
                  margin-top:8px;
                  font-size: 12px;
                  background-color:white !important;
                }

                .certification {
                  display: flex;
                  margin-bottom:4px;
                }



                .certification-number {
                  margin-right: 5px;
                }

                .certification-text {
                  flex: 1;
                }

                .right-aligned {
                  text-align: right;
                  margin-top: 10px;
                  font-weight: bold;
                }

                .footer {
                  display: flex;
                  justify-content: space-between;
                  margin-top:3px;
                  font-weight: bold;
                   background-color: white !important;
                }

                /* New styles for attendance table */
                .attendance-section {
                  margin: 5px 0;
                }

                .attendance-title {
                  text-align: center;
                  font-size: 15px;
                  font-weight: bold;
                  margin:3px 0px 7px;
                  padding-top:12px;
                }

                .attendance-table {
                  width: 100%;
                  border-collapse: collapse;
                   background-color: white !important;
                }

                .attendance-table th,
                .attendance-table td {
                  border: 1px solid #000;
                  padding:4px 4px;
                  text-align: center;
                   background-color: white !important;
                }

                .superscript {
                  font-weight: bold;
                }

                /* Styles for remarks section */
                .remarks-section {
                  margin: 5px 0;
                }

                .remarks-title {
                  text-align: center;
                  font-size: 15px;
                  font-weight: bold;
                  margin-bottom:10px;
                  padding-top:10px;
                }

                .remarks-content {
                  border: 1px solid #000;
                  background-color: white !important;
                }

                .remarks-row {
                  height: auto;
                  border-bottom: 1px solid #000;
                }

                .remarks-row:last-child {
                  border-bottom: none;
                }

                /* Styles for PEN number section */
                .pen-number-section {
                  display: flex;
                  align-items: center;
                  margin:10px 0;
                }

               .pen-label {
                  font-weight: bold;
                  margin-right:5px;
                }

                .pen-boxes {
                  display: flex;
                }

               .pen-box{
                 display:flex;
                 justify-content:center;
                 align-items:center;
              }

               .no-print .btn {
                  display: inline-block;
                  padding: 3px 6px;
                  /* margin-bottom: 0; */
                  font-size: 12px;
              }
               .pad-4{padding:2px 3px !important;}
                /* Styles for receipt section */
                .receipt-section {
                  margin:12px 0 4px;
                }

                .date-section {
                  display: flex;
                  justify-content: space-between;
                  margin: 4px 0;
                }

                .signature-line {
                  font-weight: bold;
                }

                .receipt-text {
                  margin: 5px 0;
                }
                .label{white-space: normal;color:#000000;    padding:0px 0px 0px;}
                @media print {
                  .certificate-container {
                    border: none;
                    padding: 0;
                  }
  
                  @page {
                    size: A4;
                    margin:.5cm;
                  }
                }
          </style>
            <style type="text/css" media="print">
                  .no-print {
        display: none !important;
    }
                </style>
            <div class="vd_content-section clearfix" id="Div1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-6 no-padding">
                            <asp:Label ID="lblaf" runat="server" Text="Affiliation No. is : " class="  no-padding txt-bold  " Visible="false"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblaffno" runat="server" Style="color: #CC0000; font-weight: 700" Text="" Visible="false"></asp:Label>
                        </div>
                        <div class="col-sm-6 no-padding text-right menu-action">
                           
                            <asp:LinkButton ID="lnkPrint" runat="server" OnClientClick="PrintOnlyDiv(); return false;"  CssClass="btn-print-box"
                                title="Print T.C." data-placement="left"><i class="icon-printer"></i></asp:LinkButton>
                     
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
            <div class="certificate-container">
                <div id="PrintDiv" runat="server">

                
                  <table class="certificate-table">
                    <tbody>
                      <tr>
                        <td colSpan='3' class="header">
                          <div class="logo-container">
                         <%--   <img src="/Uploads/CollegeLogo/logo_1.jpg" alt="School Logo" class="school-logo" />--%>
                              <asp:Image ID="ImageUrls" runat="server" class="school-logo"  />
                          </div>
                          <div class="school-title">
                            <h1 style='font-weight:bold;font-size:32px;'><asp:Label ID="lblSchoolName" runat="server" Text=""></asp:Label></h1>
                            <p><asp:Label ID="lblSchoolAddress" runat="server" Text=""></asp:Label></p>
                            <p class="form-subtitle">SCHOLAR'S REGISTER & TRANSFER CERTIFICATE FORM</p>
                          </div>
                        </td>
                      </tr>
                      <tr>
                        <td class="form-row">
                          <span class="heading">Admission No.</span>
                          <span class="value"><asp:Label ID="Label31" runat="server" Text=""></asp:Label></span>
                        </td>
                       <td class="form-row" style="width:500px;">
                         <%-- <span class="label">Withdrawal File No.</span>
                          <span class="value">01</span>--%>
                             <span class="heading">Transfer Certificate No.</span>
                          <span class="value"><asp:Label ID="Label30" runat="server" Text=""></asp:Label></span>
                        </td>
                        <td class="form-row" style="width:300px;">
                         
                          <span class="heading">Register No.</span>
                          <span class="value"><asp:Label ID="Label32" runat="server"></asp:Label></span>
                        </td>
                      </tr>
                     <tr>
                        <td class="form-row" style="width:340px;">
                            <span class="heading">Name of the Scholar with caste, If Hindu otherwise religion</span>
                        </td>
                        <td class="form-row" rowspan="2">
                            <div>
                                <div><span class="heading">Mother's Name:</span> <asp:Label ID="Label5" Font-Bold="true" runat="server" Text=""></asp:Label></div>
                                <div><span class="heading">Father's/Guardian's Name:</span> <asp:Label ID="Label4" Font-Bold="true" runat="server" Text=""></asp:Label></div>
                                <div><span class="heading">Occupation:</span> <asp:Label ID="Label9" Font-Bold="true" runat="server" Text=""></asp:Label></div>
                                <div><span class="heading">Address:</span> <asp:Label ID="Label8" Font-Bold="true" runat="server" Text=""></asp:Label></div>
                                
                            </div>
                        </td>
                        <td class="data-cell">
                            <span class="heading">The Last Institution attended by the Scholar</span>
                        </td>
                    </tr>
                      <tr>
                        <td class="data-cell scholar-name">
                          <div class="centered-content">
                            <strong><asp:Label ID="Label3" Font-Bold="true" runat="server" Text=""></asp:Label></strong><br />
                            <asp:Label ID="Label6" Font-Bold="true" runat="server" Text=""></asp:Label>, <asp:Label ID="Label7" Font-Bold="true" runat="server" Text=""></asp:Label>
                          </div>
                        </td>
                       
                        <td class="data-cell institution-name">
                          <div class="centered-content">
                            <strong><asp:Label ID="Label14" Font-Bold="true" runat="server" Text=""></asp:Label></strong>
                          </div>
                        </td>
                      </tr>
                    
                      <tr>
                            <td class="data-cell">
                              <span style="color:#000;">Date of Birth in Figures & Word</span>
                            </td>
                           <td colSpan="2" class="data-cell">
                                <span class="heading" style="color:#000;"><asp:Label ID="Label10" Font-Bold="true" runat="server" Text=""></asp:Label> <asp:Label ID="Label11" Font-Bold="true" runat="server" Text=""></asp:Label></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="data-cell">
                                <div style="display:flex; justify-content:start;gap:24px;align-items:center;">
                                    <div style="display:flex; justify-content:start;gap:10px;align-items:center;width:230px;">
                                        <div class="pen-label">UDISE PEN</div>                        
                                        <div class="pen-boxes">
                                            <asp:Literal ID="litPenBoxes" runat="server" />
                                        </div>
                                    </div>                                   
                                </div>
                               </td>
                               <td colSpan="2" class="data-cell">
                                    <div style="display:flex; justify-content:start;gap:10px;align-items:center;">
                                         <div class="pen-label">APAAR ID</div>                          
                                         <div class="pen-boxes">
                                             <asp:Literal ID="litApaarID" runat="server" />
                                          </div>
                                    </div>
                                </td>
                          </tr>
                    </tbody>
                  </table>

                  <table class="records-table">
                    <thead>
                      <tr>
                          <th rowSpan="2"></th>
                            <th rowSpan="2">Class</th>
                            <th rowSpan="2">Date of Admission</th>
                            <th rowSpan="2">Date of Promotion</th>
                            <th rowSpan="2">Date of Removal</th>
                            <th rowSpan="2">Cause of removal</th>
                            <th rowSpan="2">Year</th>
                            <th rowSpan="2">Conduct</th>
                            <th rowSpan="2">Work</th>  
                            <th class="no-print">Action</th>
                      </tr>
                    </thead>
                    <tbody>
                     <asp:Repeater ID="rptPrimarySection" runat="server">
    <ItemTemplate>
        <tr>
            <td class="class-label" style="border-left: 1px solid #000 !important; background-color:transparent !important" rowspan='<%# Eval("ClassList").GetType().GetProperty("Count").GetValue(Eval("ClassList"), null) %>'>
                <%# Eval("CourseName") %>
            </td>
            <td class="class-number"><%# Eval("ClassList[0].ClassName") %></td>
            <td><%# Eval("ClassList[0].DateOfAdmission") %></td>
            <td><%# Eval("ClassList[0].DateOfPromotion") %></td>
            <td><%# Eval("ClassList[0].DateOfRemoval") %></td>
            <td><%# Eval("ClassList[0].CauseOfRemoval") %></td>
            <td><%# Eval("ClassList[0].AcademicYear") %></td>
            <td><%# Eval("ClassList[0].Conduct") %></td>
            <td><%# Eval("ClassList[0].Work") %></td>
          <td class="no-print">
    <asp:HiddenField ID="IDValue" runat="server" Value='<%# String.Format("{0}", Eval("ClassList[0].ID")) %>' />
    <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" OnClick="LinkButton2_Click" CommandArgument='<%# Eval("ClassList[0].ID") %>' CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> 
        <i class="fa fa-pencil"></i>
    </asp:LinkButton>
</td>
            
        </tr>

        <asp:Repeater ID="rptInnerRows" runat="server"
                      DataSource='<%# ((CourseGroup)Container.DataItem).ClassList.Skip(1).ToList() %>'>
            <ItemTemplate>
                <tr>
                    <td class="class-number"><%# Eval("ClassName") %></td>
                    <td style="white-space:nowrap;"><%# Eval("DateOfAdmission") %></td>
                    <td><%# Eval("DateOfPromotion") %></td>
                    <td><%# Eval("DateOfRemoval") %></td>
                    <td><%# Eval("CauseOfRemoval") %></td>
                    <td><%# Eval("AcademicYear") %></td>
                    <td><%# Eval("Conduct") %></td>
                    <td><%# Eval("Work") %></td>
                     <td class="no-print">
                         <asp:HiddenField ID="IDValue" runat="server" Value='<%# String.Format("{0}", Eval("ID")) %>' />
                      <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" OnClick="LinkButton2_Click" CommandArgument='<%# Eval("ID") %>' CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
              </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
</asp:Repeater>
                    </tbody>
                  </table>

                <div class="certifications">
                       <div class="certification">
                         <div class="certification-number">1.</div>
                         <div class="certification-text">
                           Certified that the entries as regards details of the students have been duly checked from the Admission Form and they are complete.
                         </div>
                       </div>
                       <div class="certification">
                         <div class="certification-number">2.</div>
                         <div class="certification-text">
                           Certified that the above Student's Register has been posted up to date of the student's leaving as required by the Departmental rules.<br/>
Note: If Student has been among the first five in the class, this fact should be mentioned in the column of work. In the case of student leaving of the classes IX to XII of the attendance or lecture should be entered at the back of this form.

                         </div>
                       </div>
                     </div>

                     <div class="footer" style="padding-top:30px;">
                       <div class="prepared-by">
                         <span>Prepared by <asp:Label ID="Label12" runat="server"></asp:Label></span>
                       </div>
                       <div class="dated">
                         <span>Dated <asp:Label ID="Label15" Font-Bold="true" runat="server" Text=""></asp:Label></span>
                       </div>
                       <div class="head-of-institution">
                         <span>Head of Institution <asp:Label ID="lblHead1" runat="server"></asp:Label></span>
                       </div>
                     </div>

                 
                  <div class="attendance-section">
                    <h2 class="attendance-title">STATEMENT OF ATTENDANCE </h2>
                    <table class="attendance-table">
                      <thead>
                        <tr>
                          <th>Class</th>
                          <th>Year</th>
                          <th>Subject</th>
                          <th>No. of meetings held</th>
                          <th>No. of meetings attended</th>
                            <th class="no-print">Action</th>
                        </tr>
                      </thead>
                      <tbody>
                  
                            <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="padding:13px 4px !important;"><%# Eval("ClassName") %></td>
                    <td style="padding:13px 4px !important;white-space:nowrap;"><%# Eval("DateOfAdmission") %></td>
                    <td style="padding:13px 4px !important;"><%# Eval("DateOfPromotion") %></td>
                    <td style="padding:13px 4px !important;white-space:nowrap;"><%# Eval("AcademicYear") %></td>
                    <td style="padding:13px 4px !important;"><%# Eval("Work") %></td>
                     <td class="no-print" style="padding:10px 4px !important;">
                        <asp:HiddenField ID="IDValue1" runat="server" Value='<%# String.Format("{0}", Eval("ID")) %>' />
                      <asp:LinkButton ID="LinkButton3" runat="server" title="Edit" OnClick="LinkButton3_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
                      </tbody>
                    </table>
                  </div>

                  <div class="remarks-section">
                    <h3 class="remarks-title">REMARKS REGARDING OTHER ACTIVITIES OF THE STUDENT

<asp:LinkButton ID="LinkButton2" runat="server" title="Edit" OnClick="LinkButton2_Click1"  CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow no-print"> <i class="fa fa-pencil"></i></asp:LinkButton>
                    </h3>
                                            
                    <div>
                        
                    <div class="remarks-row pad-4"><asp:Literal ID="litRemark1" runat="server" /></div>
                    <%--<div class="remarks-row"><asp:Literal ID="litRemark2" runat="server" /></div>
                    <div class="remarks-row"><asp:Literal ID="litRemark3" runat="server" /></div>
                    <div class="remarks-row"><asp:Literal ID="litRemark4" runat="server" /></div>--%>
                    </div>
                  </div>

                  <div class="pen-number-section">
                  
                  </div>

                  <div class="receipt-section">
                    <div class="date-section" style="padding-top:30px;">
                      <p>Date: <asp:Label ID="Label17"  runat="server" Text=""></asp:Label></p>
                      <p class="signature-line">Head of Institution</p>
                    </div>
                    <p class="receipt-text">Received a copy of scholar's Register</p>
                    <div class="date-section">
                      <p>Date: - <asp:Label ID="Label16" runat="server" Text=""></asp:Label></p>
                      <p class="signature-line">Sig. of Recipient</p>
                    </div>
                  </div>

                  <div class="certificate-overlay">
                        
                  </div>
                   
             </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup text-center">

                        <tr>
                            <td>Course <span class="vd_red">*</span>
                            </td>
                            <td>
                                  <asp:Button ID="Button5" runat="server" Style="display: none" />
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Class <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Date Of Admission 
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue dateblank datepicker-normal" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Date of Promotion 
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue dateblank datepicker-normal"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td>Date of Removal 
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control-blue dateblank datepicker-normal"></asp:TextBox>
                                <asp:TextBox ID="TxtID" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td>Cause of Removal
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox10"  runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Year 
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control-blue" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Conduct 
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Work 
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" CssClass="button-y"  OnClick="Button3_Click" Text="Update" ValidationGroup="aa" />&nbsp;&nbsp;
                                                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>

                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup text-center">

                        <tr>
                            <td>Course <span class="vd_red">*</span>
                            </td>
                            <td>
                                  <asp:Button ID="Button1" runat="server" Style="display: none" />
                                <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control-blue" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Class <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control-blue" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                          <tr>
                            <td>Year 
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox17" runat="server" CssClass="form-control-blue" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Subjects 
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control-blue" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>No. of Lectures Delivered
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td>No. of Lectures Attended 
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                <asp:TextBox ID="TextBox15" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>

                                <asp:Button ID="Button2" runat="server" CssClass="button-y"  OnClick="Button2_Click" Text="Update"  />&nbsp;&nbsp;
                                                        <asp:Button ID="Button6" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button6_Click" Text="Cancel" />
                                <asp:Label ID="Label18" runat="server" Visible="False"></asp:Label>


                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel2"
                    CancelControlID="Button6" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup text-center">

                        <tr>
                            <td>REMARKS  <span class="vd_red">*</span>
                            </td>
                            <td>
                                  <asp:Button ID="Button7" runat="server" Style="display: none" />
                                <asp:TextBox ID="TextBox16" runat="server" CssClass="form-control-blue" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button8" runat="server" CssClass="button-y"  OnClick="Button8_Click" Text="Update"  />&nbsp;&nbsp;
                                <asp:Button ID="Button9" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button9_Click" Text="Cancel" />
                                <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button7" PopupControlID="Panel3"
                    CancelControlID="Button9" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
            </div>
            
            <script type="text/javascript">
                function PrintOnlyDiv() {
                    var divContents = document.getElementById('<%= PrintDiv.ClientID %>').innerHTML;
                    var printWindow = window.open('', '', 'height=800,width=1000');

                    printWindow.document.write('<html><head><title>Print</title>');

                    // ✅ Automatically include all <link> and <style> from current document
                    var styles = document.querySelectorAll('link[rel="stylesheet"], style');
                    styles.forEach(function (styleNode) {
                        printWindow.document.write(styleNode.outerHTML);
                    });

                    printWindow.document.write('</head><body>');
                    printWindow.document.write(divContents);
                    printWindow.document.write('</body></html>');
                    printWindow.document.close();

                    printWindow.focus();
                    printWindow.print();
                    printWindow.close();
                    printWindow.close();
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
