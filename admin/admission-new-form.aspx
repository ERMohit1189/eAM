<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="admission-new-form.aspx.cs" Inherits="admin_admission_new_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-9">
                <div class="col-sm-12 print-row">
                    <div class="panel widget light-widget">
                        <div class="panel-body p-panel-body">

                            <div id="abc" runat="server">

                                <%-- <div class="print-row col-lg-12">
                                <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 text-left">
                                    <h4 class="font-semibold mgbt-xs-15 p-mgbt-xs-5">Affiliation No.
                                        <asp:Label ID="lblAffiliationNo" runat="server"></asp:Label></h4>
                                </div>

                                <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 text-right">
                                    <h4 class="font-semibold mgbt-xs-15 p-mgbt-xs-5">School Code No.
                                        <asp:Label ID="lblSchoolCodeNo" runat="server"></asp:Label></h4>
                                </div>
                            </div>--%>

                                <div class="print-row col-lg-12">


                                    <div class=" col-lg-10 col-md-10 col-xs-10 col-sm-10  no-padding ">
                                        <div id="header" runat="server" style="width: 100%"></div>
                                    </div>

                                    <div class=" text-center col-lg-2 col-md-2 col-xs-2 col-sm-2  no-padding">
                                        <div class="img-pass-box pull-right">
                                            <h6>Paste the current passport size photograph of the student here</h6>
                                        </div>
                                    </div>

                                </div>

                                <div class="print-row col-lg-12  mgbt-xs-15 p-mgbt-xs-5">
                                    <div class=" text-left col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding">
                                        <h5 class="pull-left text-left ">क्र0सं0 / Sr. No. </h5>
                                        <h4 class="pull-left text-left sr-no">
                                            <asp:Label ID="lblSrno" runat="server"></asp:Label></h4>

                                    </div>
                                    <div class="pull-left text-center col-lg-7 col-md-7 col-xs-7 col-sm-7 row no-padding ">
                                        <div class="main-titel-box">
                                            
                                             <h3 class="form-decl p-mgbt-xs-5"><span>Admission Form</span></h3>
                                        </div>
                                    </div>
                                </div>

                                <%--   <div class="print-row col-lg-12">

                                <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 no-padding">
                                    <h5 class=" text-left no-margin ">कक्षा जिसमें प्रवेश चाहिए / </h5>
                                    <h5 class="pull-left text-left ">Class in which admission is sought for : </h5>
                                    <h4 class="pull-left text-left sr-no">
                                        <asp:Label ID="lblClass" runat="server"></asp:Label></h4>
                                </div>

                                <div class="col-lg-4 col-md-4 col-xs-4 col-sm-4 no-padding session-box">
                                    <h5 class=" text-left no-margin">सत्र / </h5>
                                    <h5 class="pull-left text-left ">Session : </h5>
                                    <h4 class="pull-left text-left sr-no">
                                        <asp:Label ID="lblSessionFor" runat="server" Text="Label"></asp:Label></h4>
                                </div>
                            </div>--%>

                                <%--                            <div class="print-row col-lg-12">
                                <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 no-left-padding">
                                    <h5 class="pull-left text-left ">लिए जाने वाले प्रस्तावित विषय / Subjects proposed to offer : </h5>

                                </div>
                                <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding">
                                    <h5 class="pull-left text-left dot-box-w3"><b>1. </b></h5>
                                    <h4 class="pull-left text-left dot-box">&nbsp; </h4>
                                </div>
                                <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding">
                                    <h5 class="pull-left text-left dot-box-w3"><b>2. </b></h5>
                                    <h4 class="pull-left text-left dot-box"><b>&nbsp; </b></h4>
                                </div>
                                <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding">
                                    <h5 class="pull-left text-left dot-box-w3"><b>3. </b></h5>
                                    <h4 class="pull-left text-left dot-box">&nbsp; </h4>
                                </div>
                                <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding">
                                    <h5 class="pull-left text-left dot-box-w3"><b>4. </b></h5>
                                    <h4 class="pull-left text-left dot-box"><b>&nbsp; </b></h4>
                                </div>
                                <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding">
                                    <h5 class="pull-left text-left dot-box-w3"><b>5. </b></h5>
                                    <h4 class="pull-left text-left dot-box">&nbsp; </h4>
                                </div>
                                <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding">
                                    <h5 class="pull-left text-left dot-box-w3"><b>6. </b></h5>
                                    <h4 class="pull-left text-left dot-box"><b>&nbsp; </b></h4>
                                </div>

                            </div>--%>

                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">1.</td>
                                            <td class="af-col-12">Name of Condidate</td>
                                            <td class="af-d-show">
                                                <asp:Label ID="lblStudentName" runat="server" Text="Amit Kashyap"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </div>


                                <script>
                                    function Clicked(nonCheck, Check) {
                                        var Checkbox1 = document.getElementById(nonCheck);
                                        var Checkbox2 = document.getElementById(Check);
                                        var sexdiv = document.getElementById('sexdiv');
                                        var span = sexdiv.getElementsByTagName('span');
                                        if (Checkbox1.className === 'sex-check-box-blank') {
                                            Checkbox1.className = 'icon-checkmark';
                                            Checkbox2.className = 'sex-check-box-blank';
                                            if (nonCheck === 'ContentPlaceHolder1_ContentPlaceHolderMainBox_male') {
                                                span[0].className = 'sex-check-box5 male';
                                                span[1].className = 'sex-check-box10 female';
                                            }
                                            else {
                                                span[0].className = 'sex-check-box10 male';
                                                span[1].className = 'sex-check-box5 female';
                                            }
                                        }
                                    }
                                </script>

                                <div id="sexdiv" class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">2.</td>
                                            <td class="af-col-22">Sex</td>
                                            <td class="af-col-23">
                                                <h5 class=" text-left af-d-show mrgn-0">Male <span class="sex-check-box5 male" id="span1" runat="server"><i class="icon-checkmark" onclick="Clicked('ContentPlaceHolder1_ContentPlaceHolderMainBox_male','ContentPlaceHolder1_ContentPlaceHolderMainBox_female');" id="male" runat="server"></i></span></h5>
                                            </td>
                                            <td class="">
                                                <h5 class=" text-left af-d-show mrgn-0">Female <span class="sex-check-box10 female" id="span2" runat="server"><i class="sex-check-box-blank" onclick="Clicked('ContentPlaceHolder1_ContentPlaceHolderMainBox_female','ContentPlaceHolder1_ContentPlaceHolderMainBox_male');" id="female" runat="server"></i></span></h5>

                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">3.</td>
                                            <td class="af-col-12">Father's Name</td>
                                            <td class="af-d-show">
                                                <asp:Label ID="Label1" runat="server" Text="Amit Kashyap"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">4.</td>
                                            <td class="af-col-12">Mother's Name</td>
                                            <td class="af-d-show">
                                                <asp:Label ID="Label2" runat="server" Text="Amit Kashyap"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">5.</td>
                                            <td class="af-col-12">Category</td>
                                            <td class="af-col-23 ">
                                                <h5 class="text-left af-d-show mrgn-0 ">GEN
                                       
                                                    <span class="blank-box-ch-af">&nbsp; </span></h5>
                                            </td>
                                            <td class="af-col-23 ">

                                                <h5 class="text-left af-d-show mrgn-0">OBC
                                

                                                    <span class="blank-box-ch-af">&nbsp; </span></h5>
                                            </td>
                                            <td class="af-col-23 af-d-show">

                                                <h5 class="text-left af-d-show mrgn-0">SC
                                   

                                                    <span class="blank-box-ch-af">&nbsp; </span></h5>
                                            </td>
                                            <td class=" af-d-show">

                                                <h5 class="text-left af-d-show mrgn-0">ST
                                    

                                                    <span class="blank-box-ch-af">&nbsp; </span></h5>


                                            </td>

                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">6.</td>
                                            <td class="af-col-12">Address for correspondence</td>
                                            <td class="af-d-show">
                                                <asp:Label ID="Label3" runat="server" Text="A - 1312/4 Indira Nagar"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12"></td>
                                            <td class="af-col-42">District &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label4" runat="server" Text="Lucknow"></asp:Label>

                                            </span>

                                            </td>
                                            <td class="af-col-42">State &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label6" runat="server" Text="Lucknow"></asp:Label>

                                            </span>
                                            </td>
                                            <td class="">PIN  &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label5" runat="server" Text="226016"></asp:Label>
                                            </span>
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12"></td>
                                            <td class="af-col-42 ">Telephone No.(s) &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label13" runat="server" Text="0522-2233444"></asp:Label></span>

                                            </td>
                                            <td class="">Mobile No. &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label16" runat="server" Text="8896807454"></asp:Label>
                                            </span></td>

                                        </tr>
                                    </table>
                                </div>

                                <%-- <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">7.</td>
                                            <td class="af-col-12">Telephone No.(s)</td>
                                            <td class="af-col-42 af-d-show">
                                                 <asp:Label ID="Label14" runat="server" Text="0522-2233444"></asp:Label>

                                            </td>
                                            <td class="af-col-42">Mobile No.</td>
                                            <td class="af-d-show">
                                                <asp:Label ID="Label15" runat="server" Text="8896807454"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>--%>



                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">7.</td>
                                            <td class="af-col-12">Permanent Address</td>
                                            <td class="af-d-show">
                                                <asp:Label ID="Label8" runat="server" Text="A - 1312/4 Indira Nagar"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12"></td>
                                            <td class="af-col-42">District &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label9" runat="server" Text="Lucknow"></asp:Label>

                                            </span>

                                            </td>
                                            <td class="af-col-42">State &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label10" runat="server" Text="Lucknow"></asp:Label>

                                            </span>
                                            </td>
                                            <td class="">PIN  &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label11" runat="server" Text="226016"></asp:Label>
                                            </span>
                                            </td>
                                        </tr>

                                    </table>
                                </div>


                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12"></td>
                                            <td class="af-col-42 ">Telephone No.(s) &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label7" runat="server" Text="0522-2233444"></asp:Label></span>

                                            </td>
                                            <td class="">Mobile No. &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label12" runat="server" Text="8896807454"></asp:Label>
                                            </span></td>

                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">8.</td>
                                            <td class="af-col-12">Date of Birth</td>
                                            <td class="af-d-show">
                                                <h5 class="pull-left text-left ">Date <span class="blank-box-f">&nbsp; </span><span class="blank-box-l">&nbsp; </span></h5>
                                                <h5 class="pull-left text-left ">Month <span class="blank-box-f">&nbsp; </span><span class="blank-box-l">&nbsp; </span></h5>
                                                <h5 class="pull-left text-left ">Year <span class="blank-box-f">&nbsp; </span><span class="blank-box-sl">&nbsp; </span><span class="blank-box-sl">&nbsp; </span><span class="blank-box-fl">&nbsp; </span></h5>

                                            </td>
                                        </tr>
                                    </table>
                                </div>




                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">9.</td>
                                            <td class="">Educational Qualification : 
                                            </td>

                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 ">

                                    <table class="table p-table-bordered table-bordered table-des text-center">
                                        <thead>
                                            <tr>
                                                <td>SI.No.
                                                </td>
                                                <td class=" text-left">Name of Exam
                                                </td>
                                                <td class=" ">Board University
                                                </td>
                                                <td class="">Year Passing
                                                </td>
                                                <td class="">Marks Obtained
                                                </td>
                                                <td class="">Percentager
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="text-center">1.
                                                </td>
                                                <td class=" text-left">High School
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="text-center">2.
                                                </td>
                                                <td class=" text-left">Intermediate

                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="text-center">3.
                                                </td>
                                                <td class=" text-left">Gradution
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="text-center">4.
                                                </td>
                                                <td class=" text-left">Any Other 

                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>


                                        </tbody>
                                    </table>
                                </div>



                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">10.</td>
                                            <td class=" af-col-12">Hosted Facility, if required 
                                                &nbsp;&nbsp;

                                                
                                            </td>
                                            <td class=" af-col-12">Yes &nbsp;<span class="blank-box-ch-af">&nbsp;</span>   &nbsp;&nbsp;&nbsp;&nbsp;
                                                No &nbsp;<span class="blank-box-ch-af">&nbsp; </span>
                                            </td>
                                            <td class="af-col-12">Hosted Facility, if required 
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>Yes &nbsp;<span class="blank-box-ch-af">&nbsp;</span>   &nbsp;&nbsp;&nbsp;&nbsp;
                                                No &nbsp;<span class="blank-box-ch-af">&nbsp; </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">11.</td>
                                            <td class=" af-col-12">UPSEE Rank(if appeared) 
                                                &nbsp;&nbsp;

                                                
                                            </td>
                                            <td class=" af-col-12 af-d-show">
                                                <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label>
                                            </td>
                                            <td class="af-col-12">Roll No.(UPSEE)
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class="af-d-show">
                                                <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>


                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">12.</td>
                                            <td class=" af-col-12">Categary Rank   
                                                &nbsp;&nbsp;

                                                
                                            </td>
                                            <td class=" af-col-12 af-d-show">
                                                <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
                                            </td>
                                            <td class="af-col-12">Any other (please specify)
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class="af-d-show">
                                                <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 ">
                                    <table class="table p-table-bordered table-bordered table-des " style="height: 100px;">
                                        <tr>
                                            <td class="valign-t">Co-curricular activities : 
                                                <asp:Label ID="Label20" runat="server" CssClass="af-d-show" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">13.</td>
                                            <td class="">Parent's Guardians Particulars
                                            </td>

                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12">Parent's Name
                                            </td>
                                            <td>
                                                <asp:Label ID="Label19" runat="server" CssClass="af-d-show" Text="Label"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12">Occupation 
                                            </td>
                                            <td class="af-col-12">
                                                <asp:Label ID="Label21" runat="server" CssClass="af-d-show" Text="Label"></asp:Label></td>
                                            <td class="af-col-12">Annual Income</td>
                                            <td>
                                                <asp:Label ID="Label22" runat="server" CssClass="af-d-show" Text="Label"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12">Office Address</td>
                                            <td class="af-d-show">
                                                <asp:Label ID="Label23" runat="server" Text="A - 1312/4 Indira Nagar"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </div>




                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12"></td>
                                            <td class="af-col-42 ">Telephone No.(s) &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label27" runat="server" Text="0522-2233444"></asp:Label></span>

                                            </td>
                                            <td class="">Mobile No. &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label28" runat="server" Text="8896807454"></asp:Label>
                                            </span></td>

                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 marg-bot-25">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="" style="text-align: justify;">I hereby declare that the entries in the admission form are true to the best of my knowledge and belief. If any of the entries is found incorrect concealed, the College reserves the right to debar me form 
                                             appearing in the examination or declare my examination null or void, or take any other action as deemed appropriate. I also do agree to abide by the rules and regulations of the college enforced from
                                             time to time. I further affirm and declare that no civil/criminal proceedings are pending against me.
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12 af-d-show">Guardian's Name & Signature </td>
                                            <td class="af-col-42 "></td>
                                            <td class="text-right af-d-show">Applicant Name & Signature</td>

                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class=" ">Date :  &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label24" runat="server" CssClass="af-d-show" Text="Label"></asp:Label></span>
                                            </td>


                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12 marg-bot-25">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>

                                            <td class=" ">Place :  &nbsp;&nbsp;&nbsp; <span class="af-d-show">
                                                <asp:Label ID="Label29" runat="server" CssClass="af-d-show" Text="Label"></asp:Label></span>
                                            </td>

                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1">14.</td>
                                            <td class="">List of Enclosures :
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12">1.
                                                <h2 class="af-dot-box"></h2>
                                            </td>
                                            <td class=""></td>
                                            <td class="af-col-12">2.
                                                <h2 class="af-dot-box"></h2>
                                            </td>
                                            <td class=""></td>
                                            <td class="af-col-12">3.
                                                <h2 class="af-dot-box"></h2>
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12">4.
                                                <h2 class="af-dot-box"></h2>
                                            </td>
                                            <td class=""></td>
                                            <td class="af-col-12">5.
                                                <h2 class="af-dot-box"></h2>
                                            </td>
                                            <td class=""></td>
                                            <td class="af-col-12">6.
                                                <h2 class="af-dot-box"></h2>
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12">7.
                                                <h2 class="af-dot-box"></h2>
                                            </td>
                                            <td class=""></td>
                                            <td class="af-col-12">8.
                                                <h2 class="af-dot-box"></h2>
                                            </td>
                                            <td class=""></td>
                                            <td class="af-col-12">9.
                                                <h2 class="af-dot-box"></h2>
                                            </td>

                                        </tr>
                                    </table>
                                </div>



                                <div class=" col-sm-12 marg-bot-25">
                                    <h5 class="h-b-line">&nbsp;</h5>
                                </div>

                                <div class="col-sm-12 ">
                                    <table class="table-af text-center">
                                        <tr>
                                            <td class="">
                                                <h5 class="text-center office-title p-mgbt-xs-5"><span>THE OFFICE USE ONLY </span></h5>
                                            </td>


                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12">Enclosures found correct (Yes/No)
                                            </td>
                                            <td class="">
                                                <h2 class="af-dot-box2"></h2>
                                            </td>

                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-27">If found any deficiency please mention it 
                                            </td>
                                            <td class="">
                                                <h2 class="af-dot-box2"></h2>
                                            </td>

                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-27">Checked by 
                                            </td>
                                            <td class=""></td>

                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12 ">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="">Signature 
                                            <h2 class="af-dot-box3"></h2>
                                            </td>
                                            <td class="af-col-27">Name 
                                           <h2 class="af-dot-box4"></h2>
                                            </td>
                                            <td class="af-col-27">Date 
                                            <h2 class="af-dot-box4"></h2>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                  <div class="col-sm-12 marg-bot-25">
                                    <table class="table-af ">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-27">Admission Committee Recommendation 
                                            </td>
                                            <td class="">
                                                <h2 class="af-dot-box2"></h2>
                                            </td>

                                        </tr>
                                    </table>
                                </div>

                                <div class="col-sm-12">
                                    <table class="table-af">
                                        <tr>
                                            <td class="af-col-1"></td>
                                            <td class="af-col-12 af-d-show">Date of Admission    </td>
                                            <td class="af-col-42 "></td>
                                            <td class="text-right af-d-show">Authorised Signature</td>

                                        </tr>
                                    </table>
                                </div>

                              




                            </div>
                        </div>
                    </div>
                    <!-- Panel Widget -->
                </div>
            </div>
            <!-- col-sm-9-->
            <div class="col-sm-3">
                <div class="mgbt-xs-5">
                   
                <asp:LinkButton ID="lnkback" runat="server" CssClass="btn-print-box" Style="color: #CC0000" 
                    title="Go back to admission form fee desposit" data-placement="left"><i class="fa fa-reply"></i></asp:LinkButton>
                &nbsp;
                            <asp:LinkButton ID="lnkPrint" runat="server"  CssClass="btn-print-box"
                                title="Print Admission Form" data-placement="left"><i class="icon-printer"></i></asp:LinkButton>
            </div>
        </div>
        <!-- row -->

    </div>

</asp:Content>

