<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="StudentRegView.aspx.cs" Inherits="_11.AdminStudentRegView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-10">
                <div class="col-sm-12 print-row">
                    <div class="panel widget light-widget">
                        <div class="panel-body p-panel-body">
                            <div class="col-sm-12  no-padding">
                               
                                <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="show1">
                                    <label class="control-label">Enter S.R. No.</label>
                                    <div class="">
                                        <asp:TextBox ID="lblEnter" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4   half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15" runat="server" id="show2">
                                    <asp:Button ID="Button11" runat="server" CausesValidation="false" CssClass="button form-control-blue" OnClick="Button11_Click" Text="View Details" />
                                </div>
                               
                                <div class="col-sm-4" runat="server" id="show3" style="text-align:right; padding-bottom:20px;">
                                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl="~/2/CompositFeeDeposit.aspx" CssClass="btn-print-box"
                                title="Go to Fee Deposit" data-placement="left" style="text-decoration:underline;">Go to Fee Deposit</asp:HyperLink>&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click" CssClass="btn-print-box" style="border:1px solid #ccc; padding:5px 10px 5px 10px;"
                                title="Print Student Registration" data-placement="left"><i class="icon-printer"></i></asp:LinkButton>
                                </div>
                               
                            </div>
                            <div id="abc" runat="server">

                                <div class="print-row col-lg-12">
                                    <table class="table">
                                        <tr>
                                            <td style="padding: 0 !important;">
                                                <div class="" id="header" runat="server" style="width: 100%"></div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="print-row col-lg-12  mgbt-xs-15 p-mgbt-xs-5">
                                    <div class=" text-left col-lg-3 col-md-3 col-xs-3 col-sm-3">
                                        <h5 class="pull-left text-left ">S.R. No.</h5>
                                        <h4 class="pull-left text-left sr-no">
                                            <asp:Label ID="lblSrno" runat="server" style="font-size:13px !important;"></asp:Label></h4>

                                    </div>
                                    <div class="pull-left text-center col-lg-6 col-md-6 col-xs-6 col-sm-6 row" style="width:50% !important;">
                                        <div class="main-titel-box">
                                            <h3 class="form-name" style="padding-left: 30px;font-weight: bold;"><span>Admission Form</span></h3>
                                        </div>
                                    </div>
                                     <div class=" text-right col-lg-3 col-md-3 col-xs-3 col-sm-3" style="width: 26% !important;">
                                        <h5>SESSION<asp:Label ID="lblSession" runat="server" CssClass="sr-no" style="font-size:13px !important;"></asp:Label></h5>
                                        
                                    </div>
                                </div>

                                <style>        
                                    .grid-container {
                                        display: grid;
                                        grid-template-columns: repeat(2, 1fr);
                                        gap:0px;
                                        width:70%;
                                    }
                                    .grid-item {
                                        padding: 10px;
                                        border:2px solid #FFFFFF;
                                    }
                                    .photo-container {
                                        width:20%;
                                        text-align: center;            
                                        padding: 10px;background-color: #fbf9f9;
                                    }
                                    

                                    .grid-container2 {
                                        display: grid;
                                        grid-template-columns: repeat(4, 1fr);
                                        gap:0px;
                                        width:100%;
                                    }
                                    .student-photo img{width:120px; height:120px;}
                                </style>

                                <div style="display:flex; width:100%;">
                                    <div style="display:flex; gap:10px;width:100%; justify-content:space-between;background-color: #fbf9f9; border-radius:5px;">
                                        <div class="grid-container">
                                                <div class="grid-item">Student's Name</div>
                                                <div class="grid-item">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                    <asp:Label runat="server" ID="lblFirstNa"></asp:Label>
                                                    <asp:Label runat="server" ID="lblMidNa"></asp:Label>
                                                    <asp:Label runat="server" ID="lbllast"></asp:Label></h4>
                                                </div>
                                                <div class="grid-item">Date of Birth</div>
                                                <div class="grid-item">
                                                    <h4 class="reg-view-box mgtp-5 mgtp-5"><b>: &nbsp; </b>
                                                    <asp:Label runat="server" ID="lblStudentDOB"></asp:Label></h4>
                                                </div>
                                               <%-- <div class="grid-item">Age on Date</div>
                                                <div class="grid-item">
                                                      <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblAgeOnDate"></asp:Label></h4>
                                                </div>--%>
                                            <div class="grid-item">Gender</div>
                                            <div class="grid-item">
                                                <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                <asp:Label runat="server" ID="lblGender"></asp:Label></h4>
                                            </div>
                                            <div class="grid-item">Email</div>
                                            <div class="grid-item">
                                                 <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                    <asp:Label runat="server" ID="lblEmail"></asp:Label></h4>
                                            </div>
                                            <div class="grid-item">Mobile No.</div>
                                            <div class="grid-item">
                                                 <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                <asp:Label runat="server" ID="lblMobile"></asp:Label></h4>
                                            </div>
                                            <div class="grid-item">Mother Tongue</div>
                                            <div class="grid-item">
                                                   <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                    <asp:Label runat="server" ID="lblMotherTongue"></asp:Label></h4>
                                            </div>
                                            <div class="grid-item">Home Town</div>
                                            <div class="grid-item">
                                                <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                <asp:Label runat="server" ID="lblHomeTown"></asp:Label></h4>
                                            </div>
                                            <div class="grid-item">Nationality</div>
                                            <div class="grid-item">
                                                <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                <asp:Label runat="server" ID="lblNationality"></asp:Label></h4>
                                            </div>
                                            <div class="grid-item">Religion</div>
                                            <div class="grid-item">
                                                <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                <asp:Label runat="server" ID="lblReligion"></asp:Label></h4>
                                            </div>  
                                            <div class="grid-item">Category</div><div class="grid-item"><asp:Label runat="server" ID="lblCategory"></asp:Label></div>
                                            <div class="grid-item">Caste</div><div class="grid-item"><asp:Label runat="server" ID="lblCaste"></asp:Label></div>
                                            <div class="grid-item">Aadhaar No.</div><div class="grid-item"><asp:Label runat="server" ID="lblAadharNo"></asp:Label></div>
                                            <div class="grid-item">UDISE (PEN)</div><div class="grid-item"><asp:Label runat="server" ID="lbl_pen"></asp:Label></div>
                                        </div>
                                        <div class="photo-container">
                                            <div style="max-width:120px; width:100%; display:flex; justify-content:end;" class="student-photo">
                                                <asp:Image runat="server" ID="Image1" CssClass="family-pass-box float-right"></asp:Image>
                                            </div>
                                        </div>
                                    </div>                                
                                </div>
                             
                                <div style="width:100%; display:flex; justify-content:start;padding:20px 0px 12px;">
                                    <h3 style="font-weight:600;">Health Details</h3>
                                </div>
                                 <div style="display:flex; width:100%;background-color: #fbf9f9; border-radius:5px;">
                                     <div style="display:flex; gap:10px;width:100%;">
                                         <div class="grid-container2">   
                                             <div class="grid-item">Blood Group</div><div class="grid-item"><asp:Label runat="server" ID="lblBloodGroup"></asp:Label></div>
                                             <div class="grid-item">Vision</div><div class="grid-item"><span>L</span>
                                                 <asp:Label runat="server" ID="lblVLeft1"></asp:Label><b>&nbsp; </b><span>R</span>
                                                        <asp:Label runat="server" ID="lblVRight1"></asp:Label></div>
                                             <div class="grid-item">Height</div><div class="grid-item"><asp:Label runat="server" ID="lblHeight"></asp:Label></div>
                                             <div class="grid-item">Weight</div><div class="grid-item"><asp:Label runat="server" ID="lblWeight"></asp:Label></div>
                                                  <div class="grid-item">Dental Hygiene</div><div class="grid-item"><asp:Label runat="server" ID="lblDental"></asp:Label></div>
                                             <div class="grid-item">Oral Hygiene</div><div class="grid-item"><asp:Label runat="server" ID="lblOral"></asp:Label></div>
                                             <div class="grid-item">Identification Mark</div><div class="grid-item"><asp:Label runat="server" ID="lblIMark"></asp:Label></div>
                                             <div class="grid-item">Specific Ailment</div><div class="grid-item"><asp:Label runat="server" ID="lblSpeAilment"></asp:Label></div>
                                        
                                             <div class="grid-item">Specially-Abled?</div><div class="grid-item"><asp:Label runat="server" ID="lblPhysicallyDisabled"></asp:Label></div>
                                             <div class="grid-item">Name of Disability</div><div class="grid-item"><asp:Label runat="server" ID="lblPhyName"></asp:Label></div>
                                             <div class="grid-item">Certificate No.</div><div class="grid-item"><asp:Label runat="server" ID="lblCertificateNo"></asp:Label></div>
                                             <div class="grid-item">Issued By</div><div class="grid-item"><asp:Label runat="server" ID="lblIssuedBy"></asp:Label></div>                                            
                                         </div>                                         
                                     </div>                                
                                 </div>

                                <div style="width:100%; display:flex; justify-content:start;padding:20px 0px 12px;">
                                    <h3 style="font-weight:600;">Father's Details</h3>
                                </div>
                                 <div style="display:flex; width:100%;background-color: #fbf9f9; border-radius:5px;">
                                     <div style="display:flex; gap:10px;width:100%;">
                                         <div class="grid-container2">   
                                             <div class="grid-item">Father's Name</div><div class="grid-item"> <asp:Label runat="server" ID="lblfaNameee"></asp:Label></div>
                                             <div class="grid-item">Mobile No.</div><div class="grid-item"><asp:Label runat="server" ID="lblcontfa"></asp:Label></div>
                                             <div class="grid-item">Occupation</div><div class="grid-item"><asp:Label runat="server" ID="lblOccupationfa"></asp:Label></div>
                                             <div class="grid-item">Alternate Mobile</div><div class="grid-item"><asp:Label runat="server" ID="lblcontfaAltername"></asp:Label></div>
                                             <div class="grid-item">Designation</div><div class="grid-item"><asp:Label runat="server" ID="lbldesfa"></asp:Label></div>
                                             <div class="grid-item">Email</div><div class="grid-item"><asp:Label runat="server" ID="lblemailfather"></asp:Label></div>
                                             <div class="grid-item">Qualification</div><div class="grid-item"><asp:Label runat="server" ID="lblqufa"></asp:Label></div>
                                             <div class="grid-item">Nationality</div><div class="grid-item"><asp:Label runat="server" ID="lblFatherNationality"></asp:Label></div>
                                             <div class="grid-item">Annual Income</div><div class="grid-item"><asp:Label runat="server" ID="lblincomefa"></asp:Label></div>
                                             <div class="grid-item">Office Address</div><div class="grid-item"><asp:Label runat="server" ID="lbloffaddfa"></asp:Label></div>                                            
                                         </div>                                         
                                     </div>                                
                                 </div>


                                <div style="width:100%; display:flex; justify-content:start;padding:20px 0px 12px;">
                                    <h3 style="font-weight:600;">Mother's Details</h3>
                                </div>
                                 <div style="display:flex; width:100%;background-color: #fbf9f9; border-radius:5px;">
                                     <div style="display:flex; gap:10px;width:100%;">
                                         <div class="grid-container2">   
                                             <div class="grid-item">Mother's Name</div><div class="grid-item"><asp:Label runat="server" ID="lblmotherNameeee"></asp:Label></div>
                                             <div class="grid-item">Mobile No.</div><div class="grid-item"><asp:Label runat="server" ID="lblmothercontact"></asp:Label></div>
                                             <div class="grid-item">Occupation</div><div class="grid-item"><asp:Label runat="server" ID="lblOccupationmoth"></asp:Label></div>
                                             <div class="grid-item">Email</div><div class="grid-item"> <asp:Label runat="server" ID="lblmotheremail"></asp:Label></div>
                                             <div class="grid-item">Designation</div><div class="grid-item"><asp:Label runat="server" ID="lbldesmoth"></asp:Label></div>
                                             <div class="grid-item">Nationality</div><div class="grid-item"><asp:Label runat="server" ID="lblMotherNationality"></asp:Label></div>
                                             <div class="grid-item">Qualification</div><div class="grid-item"><asp:Label runat="server" ID="lblqualimother"></asp:Label></div>
                                             <div class="grid-item">Office Address</div><div class="grid-item"><asp:Label runat="server" ID="lblofficeaddmother"></asp:Label></div>
                                             <div class="grid-item">Annual Income</div><div class="grid-item"><asp:Label runat="server" ID="lblincomemonthlymother"></asp:Label></div>
                                             <div class="grid-item">Parent's Total Income</div><div class="grid-item"><asp:Label runat="server" ID="lblParentTotalIncome"></asp:Label></div>                                            
                                         </div>                                         
                                     </div>                                
                                 </div>

                                <div style="width:100%; display:flex; justify-content:start;padding:20px 0px 12px;">
                                    <h3 style="font-weight:600;">Guardian's Details</h3>
                                </div>
                                 <div style="display:flex; width:100%;background-color: #fbf9f9; border-radius:5px;">
                                     <div style="display:flex; gap:10px;width:100%;">
                                         <div class="grid-container2">   
                                             <div class="grid-item">Relationship</div><div class="grid-item"> <asp:Label runat="server" ID="lblRelationship"></asp:Label></div>
                                             <div class="grid-item">Email</div><div class="grid-item"><asp:Label runat="server" ID="lblemailfamily"></asp:Label></div>
                                             <div class="grid-item">Guardian's Name</div><div class="grid-item"><asp:Label runat="server" ID="lblguardianname"></asp:Label></div>
                                             <div class="grid-item">Mobile No.</div><div class="grid-item"><asp:Label runat="server" ID="lblcontactNo"></asp:Label></div>                                                                                      
                                         </div>                                         
                                     </div>                                
                                 </div>
                                <div style="width:100%; display:flex; justify-content:start;padding:20px 0px 12px;">
                                    <h3 style="font-weight:600;">Present Address</h3>
                                </div>
                                 <div style="display:flex; width:100%;background-color: #fbf9f9; border-radius:5px;">
                                     <div style="display:flex; gap:10px;width:100%;">
                                         <div class="grid-container2">   
                                            <asp:Label runat="server" ID="lblPersendtAddress"></asp:Label>
<%--                                         <div class="grid-item">Address</div><div class="grid-item"><asp:Label runat="server" ID="lblPreaddress"></asp:Label></div>
                                             <div class="grid-item">Pin Code</div><div class="grid-item"><asp:Label runat="server" ID="lblPreZip"></asp:Label></div>
                                             <div class="grid-item">City</div><div class="grid-item"><asp:Label runat="server" ID="lblPreCity"></asp:Label></div>
                                             <div class="grid-item">State</div><div class="grid-item"> <asp:Label runat="server" ID="lblPreState"></asp:Label></div>    
                                             <div class="grid-item">Country</div><div class="grid-item"><asp:Label runat="server" ID="lblPreCountry"></asp:Label></div> --%>
                                         </div>                                         
                                     </div>                                
                                 </div>

                                <div style="width:100%; display:flex; justify-content:start;padding:20px 0px 12px;">
                                    <h3 style="font-weight:600;">Permanent Address</h3>
                                </div>
                                 <div style="display:flex; width:100%;background-color: #fbf9f9; border-radius:5px;">
                                     <div style="display:flex; gap:10px;width:100%;">
                                         <div class="grid-container2">   
                                             <asp:Label runat="server" ID="lblPermanentAddress"></asp:Label>
<%--                                          <div class="grid-item">Address</div><div class="grid-item"><asp:Label runat="server" ID="lblPerAdd"></asp:Label></div>
                                             <div class="grid-item">Pin Code</div><div class="grid-item"><asp:Label runat="server" ID="lblPerZip"></asp:Label></div>
                                             <div class="grid-item">City</div><div class="grid-item"> <asp:Label runat="server" ID="lblPerCity"></asp:Label></div>
                                             <div class="grid-item">State</div><div class="grid-item"> <asp:Label runat="server" ID="lblPerState"></asp:Label></div>    
                                             <div class="grid-item">Country</div><div class="grid-item"> <asp:Label runat="server" ID="lblPerCountry"></asp:Label></div> --%>
                                         </div>                                         
                                     </div>                                
                                 </div>

                                <div style="width:100%; display:flex; justify-content:start;padding:20px 0px 12px;">
                                    <h3 style="font-weight:600;">Admission Details</h3>
                                </div>
                                 <div style="display:flex; width:100%;background-color: #fbf9f9; border-radius:5px;">
                                     <div style="display:flex; gap:10px;width:100%;">
                                         <div class="grid-container2">   
                                             <div class="grid-item">Date of Admission</div><div class="grid-item"><asp:Label runat="server" ID="lblDOA"></asp:Label></div>
                                             <div class="grid-item">Course</div><div class="grid-item"><asp:Label runat="server" ID="lblCourse"></asp:Label></div>
                                             <div class="grid-item">Class</div><div class="grid-item"><asp:Label runat="server" ID="lblClass"></asp:Label></div>
                                             <div class="grid-item">Section</div><div class="grid-item"><asp:Label runat="server" ID="lblSection"></asp:Label></div>    
                                             <div class="grid-item">Stream</div><div class="grid-item"><asp:Label runat="server" ID="lblBranch"></asp:Label></div> 
                                             <div class="grid-item">Medium</div><div class="grid-item"><asp:Label runat="server" ID="lblMedium"></asp:Label></div>
                                             <div class="grid-item">Board/ University</div><div class="grid-item"><asp:Label runat="server" ID="lblBoard"></asp:Label></div>
                                             <div class="grid-item">Type of Admission</div><div class="grid-item"><asp:Label runat="server" ID="lblNEWOLSAdmission"></asp:Label></div>
                                             <div class="grid-item">Type of Education</div><div class="grid-item"><asp:Label runat="server" ID="lblTypeofEducation"></asp:Label></div>
                                             <div class="grid-item">S.R. No.</div><div class="grid-item"><asp:Label runat="server" ID="lblSr"></asp:Label></div>
                                             <div class="grid-item">Card No.</div><div class="grid-item"><asp:Label runat="server" ID="lblCardNo"></asp:Label></div>
                                             <div class="grid-item">Board/ University Roll No.</div><div class="grid-item"><asp:Label runat="server" ID="lblUniversityBoardRollNo1"></asp:Label></div>
                                             <div class="grid-item">Institute Roll No.</div><div class="grid-item"><asp:Label runat="server" ID="lblSchoolcollegeRollno"></asp:Label></div>
                                             <div class="grid-item">File No.</div><div class="grid-item"> <asp:Label runat="server" ID="lblfileno"></asp:Label></div>
                                             <div class="grid-item">House Name</div><div class="grid-item"> <asp:Label runat="server" ID="lblHouseName"></asp:Label></div>
                                             <div class="grid-item">Fee Category</div><div class="grid-item">  <asp:Label runat="server" ID="lblPanelCardType"></asp:Label></div>
                                             <div class="grid-item">Remark</div><div class="grid-item">  <asp:Label runat="server" ID="lblrema"></asp:Label></div>                                            
                                             <div class="grid-item">Education Act</div><div class="grid-item">  <asp:Label runat="server" ID="lbleducationAct"></asp:Label></div>                                            
                                             <div class="grid-item">APAAR ID</div><div class="grid-item">  <asp:Label runat="server" ID="lblApparID"></asp:Label></div>                                            
                                             <div class="grid-item">Shift</div><div class="grid-item">  <asp:Label runat="server" ID="lblShift"></asp:Label></div>                                            
                                             <div class="grid-item">Machine No.</div><div class="grid-item">  <asp:Label runat="server" ID="lblMachineNo"></asp:Label></div>                                            
                                         </div>                                         
                                     </div>                                
                                 </div>
                                 <div style="width:100%; display:flex; justify-content:start;padding:20px 0px 12px;">
                                    <h3 style="font-weight:600;">Previous Education Details</h3>
                                </div>
                                 <div style="display:flex; width:100%;background-color: #fbf9f9; border-radius:5px;">
                                     <div style="display:flex; gap:10px;width:100%;">
                                         <div class="grid-container2">   
                                             <div class="grid-item">Board/ University</div><div class="grid-item"><asp:Label runat="server" ID="lblboardPre"></asp:Label></div>
                                             <div class="grid-item">Name of Previous Institute</div><div class="grid-item"><asp:Label runat="server" ID="lblPreviousInstite"></asp:Label></div>
                                             <div class="grid-item">UDISE Code</div><div class="grid-item"><asp:Label runat="server" ID="lbludisecodePre"></asp:Label></div>
                                             <div class="grid-item">Contact No.</div><div class="grid-item"><asp:Label runat="server" ID="lblContactPre"></asp:Label></div>    
                                             <div class="grid-item">Previous Class</div><div class="grid-item"><asp:Label runat="server" ID="lblPreviousClass"></asp:Label></div> 
                                             <div class="grid-item">Address</div><div class="grid-item"><asp:Label runat="server" ID="lblAddressPre"></asp:Label></div>
                                             <div class="grid-item">Previous Medium</div><div class="grid-item"><asp:Label runat="server" ID="lblMediumPre"></asp:Label></div>
                                             <div class="grid-item">Attendance</div><div class="grid-item"><asp:Label runat="server" ID="lblAttendancePre"></asp:Label></div>
                                             <div class="grid-item">Result</div><div class="grid-item"><asp:Label runat="server" ID="lblResultPre"></asp:Label></div>
                                             <div class="grid-item">Marks Percentage</div><div class="grid-item"><asp:Label runat="server" ID="lblMarksPrecentage"></asp:Label></div>
                                         </div>                                         
                                     </div>                                
                                 </div>

                                <div style="display:flex; justify-content:space-between; width:100%;background-color: #fbf9f9; border-radius:5px;padding:20px 40px;">
                                    <div style="display:flex; gap:20px;">
                                        <div style="max-width:120px; width:100%; display:flex; justify-content:end;" class="student-photo">
                                           <asp:Image ID="imgFathernew" runat="server" class="family-pass-box" ImageUrl="../img/user-pic/student-pic.png" alt="" />
                                        </div>
                                        <div style="max-width:120px; width:100%; display:flex; justify-content:end;" class="student-photo">
                                           <asp:Image ID="imgMothernew" ImageUrl="../img/user-pic/student-f-pic.png" runat="server" Class="family-pass-box" />
                                        </div>
                                    </div>
                                    <div>
                                         <div style="max-width:520px; width:100%; display:flex; justify-content:end;" class="student-photo">
                                             <asp:Image ID="imgGroupPhotonew" runat="server" Class="family-pass-box" Style="height: 280px; width: 100%;" ImageUrl="../img/user-pic/group-photo.jpg" />
                                         </div>
                                    </div>
                                </div>
                                 <div style="display:flex; justify-content:space-between; width:100%;background-color: #fbf9f9; border-radius:5px;padding:20px 40px;">
                                        <div>
                                            PRINCIPAL'S SIGNATURE
                                        </div>
                                     <div>
                                         PARENT'S SIGNATURE
                                       </div>
                                </div>
                                <div class="print-row col-lg-12 round-bd" style="display:none;">
                                    <!--<div class="col-md-12">
                                        <div class="col-md-8 col-sm-8 col-xs-8" style="padding: 0; margin: 0;">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Student's Name</h5>
                                                </div>
                                                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Date of Birth</h5>
                                                </div>
                                                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                    <h4 class="reg-view-box mgtp-5 mgtp-5"><b>: &nbsp; </b>
                                                     </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding" style="display: none;">
                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Age on Date</h5>
                                                </div>
                                                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       ></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Gender</h5>
                                                </div>
                                                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                     </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding" runat="server" id="K12EmailDiv">
                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Email</h5>
                                                </div>
                                                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding" runat="server" id="K12MobileDiv">
                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Mobile No.</h5>
                                                </div>
                                                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Mother Tongue</h5>
                                                </div>
                                                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Home Town</h5>
                                                </div>
                                                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                <h5 class=" text-left left-padd-20 ">Nationality</h5>
                                            </div>
                                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                  </h4>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                            <div class="col-lg-3 col-md-3 col-sm-2 col-xs-3 no-padding">
                                                <h5 class=" text-left left-padd-20 ">Religion</h5>
                                            </div>
                                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                   </h4>
                                            </div>
                                        </div>
                                        </div>
                                        <div class="col-md-4 col-sm-4 col-xs-4" style="padding: 0; margin: 0;">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="float-right">
                                                    <asp:Image runat="server" ID="imgshow" CssClass="family-pass-box float-right"></asp:Image>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">

                                        
                                        <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                                <h5 class=" text-left left-padd-20 ">Category</h5>
                                            </div>
                                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                   </h4>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                                <h5 class=" text-left left-padd-20 ">Caste</h5>
                                            </div>
                                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                  </h4>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                                <h5 class=" text-left left-padd-20 " runat="server" id="lblAadhaar">Aadhaar No.</h5>
                                            </div>
                                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                               </h4>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                                <h5 class=" text-left left-padd-20 " runat="server" id="H1">UDISE (PEN)</h5>
                                            </div>
                                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                                <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                    </h4>
                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div class="col-md-12">
                                        <h4 style="border-bottom: 1px solid #000; width: 115px;">Health Details</h4>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Blood Group</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Vision</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblVLeft"></asp:Label><b>&nbsp; </b>
                                                        <asp:Label runat="server" ID="lblVRight"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Height</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                      </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Weight</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                      </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Specially-Abled?</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Oral Hygiene</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       
                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Identification Mark</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Specific Ailment</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Dental Hygiene</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>

                                        </div>



                                        <div class="col-sm-12  no-padding" id="Panel2" runat="server">
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                                <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">

                                                        <h5 class=" text-left left-padd-20 ">Name of Disability</h5>
                                                    </div>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                        <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                        <h5 class=" text-left left-padd-20 ">Certificate No.</h5>
                                                    </div>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                        <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                           </h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                                <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                        <h5 class=" text-left left-padd-20 ">Issued By</h5>
                                                    </div>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                        <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                           </h4>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                        <h5 class=" text-left left-padd-20 ">Details</h5>
                                                    </div>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                        <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                            <asp:Label ID="lblPhyDetail" runat="server"></asp:Label></h4>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>



                                    <div class="col-md-12">
                                        <h4 style="border-bottom: 1px solid #000; width: 115px;">Father's Details</h4>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Father's Name</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Occupation</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Designation</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>

                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Qualification</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>

                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Annual Income</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                             <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Mobile No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                             <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Alternate Mobile No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Email</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Nationality</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                           
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Office Address</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <h4 style="border-bottom: 1px solid #000; width: 115px;">Mother's Details</h4>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Mother's Name</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Occupation</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Designation</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Qualification</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Annual Income</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                     </h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Mobile No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Email</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Nationality</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Office Address</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Parent's Total Income</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <h4 style="border-bottom: 1px solid #000; width: 115px;">Guardian's Details</h4>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Relationship</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Guardian's Name</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">City</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b style="float: left;">: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblG1City11" CssClass="shift-left"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">State</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b style="float: left;">: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblG1State11" CssClass="shift-left"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Country</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblG1Country11"></asp:Label></h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Pin Code</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblG1Pin"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Email</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Guardian's Address</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblG1Address"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Mobile No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <h4 style="border-bottom: 1px solid #000; width: 115px;">Present Address</h4>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Address</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">City</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                      </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">State</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Country</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">

                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Pin Code</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                     </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Phone No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblPrePhoneNo"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Mobile No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblPreMobileNo"></asp:Label></h4>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <h4 style="border-bottom: 1px solid #000; width: 115px;">Permanent Address</h4>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Address</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">City</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">State</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Country</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            
                                            
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Pin Code</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                      </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Phone No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblPerPhoneNo"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Mobile No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblPerMobileNo"></asp:Label></h4>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                 <br />
                                <div class="print-row col-lg-12 round-bd">

                                    <div class="col-md-12">
                                        <h4 style="border-bottom: 1px solid #000; width: 115px;">Admission Details</h4>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Date of Admission</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Class</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b style="float: left;">: &nbsp; </b>
                                                        <%--<asp:TextBox ID="lblAdmissionClass" runat="server"></asp:TextBox>--%>
                                                       

                                                    </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Stream</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Medium</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b style="float: left;">: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Type of Admission</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding" runat="server" id="ScholarshipDiv">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Scholarship</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="rbScholarship"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding" style="display: none">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Library Facility</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="rbLibrary"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">S.R. No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Board/ University Roll No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblUniversityBoardRollNo"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">File No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Fee Category</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                      </h4>
                                                </div>
                                            </div>
                                            
                                            

                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Date of First Admission</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblDFA"></asp:Label></h4>
                                                </div>
                                            </div>
                                           
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Course</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b style="float: left;">: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Section</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b style="float: left;">: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Group</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b style="float: left;">: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblStream" CssClass="shift-left"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Board/ University</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Type of Education</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding" style="display: none">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Hostel Required</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="rbHostel"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding" style="display: none">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Transport Required</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="rbTransport"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Card No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Institute Roll No.</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding" style="display: none;">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Payment Frequency</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblFeeDepositMOD"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">House Name</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                       </h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding" runat="server" id="Admissiondoneat">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Admission done at</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblAddDoneat"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Course of First Admission</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblCFA"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Branch of First Admission</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblSFA"></asp:Label></h4>
                                                </div>
                                            </div>
                                             <div class="col-lg-12 col-md-12 col-sm-12  no-padding hide">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Class of First Admission</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                        <asp:Label runat="server" ID="lblCOFA"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                    <h5 class=" text-left left-padd-20 ">Remark</h5>
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                                    <h4 class="reg-view-box mgtp-5"><b>: &nbsp; </b>
                                                      </h4>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4 col-sm-4 col-xs-4">
                                            <%--<div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <h5 class="pull-left "><b>35.</b></h5>
                                                <h5 class=" text-left left-padd-20 ">Father's Photo</h5>
                                            </div>--%>
                                            <div class="col-lg-8 col-md-10 col-sm-10 col-xs-10 no-padding">
                                                <h5 class=" text-left left-padd-5 ">Father's Photo</h5>
                                                <div class="family-img-box left-padd-5">
                                                    <asp:Image ID="imgFather" runat="server" class="family-pass-box" ImageUrl="../img/user-pic/student-pic.png" alt="" />
                                                </div>
                                            </div>
                                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 no-padding">
                                                <h5 class=" text-left left-padd-5 ">Mother's Photo</h5>
                                                <div class="family-img-box left-padd-5">
                                                    <asp:Image ID="imgMother" ImageUrl="../img/user-pic/student-f-pic.png" runat="server" Class="family-pass-box" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-8 col-sm-8 col-xs-8">
                                            <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                                <h5 class=" text-left left-padd-5 ">Group Photo</h5>
                                                <div class="family-img-box left-padd-5">
                                                    <asp:Image ID="imgGroupPhoto" runat="server" Class="family-pass-box" Style="height: 280px; width: 100%;" ImageUrl="../img/user-pic/group-photo.jpg" />

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-left">
                                            <h5 class="lbl-rep-title-13 mgtp-40 left-padd-5">PRINCIPAL'S SIGNATURE </h5>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-right">
                                            <h5 class="lbl-rep-title-13 mgtp-40 left-padd-5">PARENT'S SIGNATURE</h5>
                                        </div>
                                    </div>-->

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

