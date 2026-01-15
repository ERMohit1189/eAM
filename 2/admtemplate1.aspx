 <%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="admtemplate1.aspx.cs" Inherits="_2.admtemplate1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-10">
                <div class="col-sm-12 print-row">
                    <div class="panel widget light-widget">
                        <div class="panel-body p-panel-body">

                            <div id="abc" runat="server">

<%--                                <div class="print-row col-lg-12">
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
                                    <table class="table">
                                        <tr>
                                            <td style="padding:0 !important;">
                                                <div class="" id="header" runat="server" style="width: 85%"></div>
                                                <div class="right-img-box" style="top: 28px; right: 14px;">
                                                    <div class="img-pass-box text-center">

                                                        <h6>छात्र का वर्तमान पासपोर्ट साइज फोटोग्राफ <br />
                                                            Passport size Photograph of the student</h6>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="print-row col-lg-12  mgbt-xs-15 p-mgbt-xs-5">
                                    <div class=" text-left col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding">
                                        <h5 class="pull-left text-left ">क्र0सं0 / S. No.</h5>
                                        <h4 class="pull-left text-left sr-no">
                                            <asp:Label ID="lblSrno" runat="server"  style="text-transform:uppercase"></asp:Label></h4>

                                    </div>
                                    <div class="pull-left text-center col-lg-7 col-md-7 col-xs-7 col-sm-7 row no-padding" style="text-align: left !important; margin-left: 30px !important;">
                                        <div class="main-titel-box">
                                            <h3 class="form-name"><span style="font-size: 20px !important;">Admission Form (<asp:Label ID="lblSessionFor" runat="server" Text="Label" style="text-transform:uppercase"></asp:Label></span><asp:Label runat="server" ID="lblAdmissionType"  style="text-transform:uppercase;"></asp:Label>)</h3>
                                        </div>
                                    </div>
                                </div>

                                <div class="print-row col-lg-12">

                                    <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 no-padding">
                                        <h5 class="pull-left text-left ">कक्षा जिसमें प्रवेश चाहिए / Class in which admission is sought for : </h5>
                                        <h4 class="pull-left text-left sr-no">
                                            <asp:Label ID="lblClass" runat="server"  style="text-transform:uppercase"></asp:Label></h4>
                                    </div>

                                </div>

                                <div class="print-row col-lg-12">
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

                                </div>

                                <div class="print-row col-lg-12">

                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h5 class="pull-left text-left "><b>1.</b></h5>
                                        <h5 class="pull-left text-left left-padd-15"><b>(a) </b></h5>
                                        <div class="pull-left left-padd-10 p-marg-top-5 ">
                                            <h5 class=" no-margin ">विद्यार्थी का पूरा नाम / </h5>
                                            <h5 class="pull-left text-left ">Name of the child in full (in capital letters) : </h5>
                                        </div>
                                    </div>

                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                        <h4 class=" dot-box-res2 sr-no p-marg-top-15 marg-top-5">
                                            <asp:Label ID="lblStudentName" runat="server"  style="text-transform:uppercase"></asp:Label></h4>
                                    </div>

                                </div>

                                <script>
                                    function Clicked(nonCheck, check) {
                                        var checkbox1 = document.getElementById(nonCheck);
                                        var checkbox2 = document.getElementById(check);
                                        var sexdiv = document.getElementById('sexdiv');
                                        var span = sexdiv.getElementsByTagName('span');
                                        if (checkbox1.className === 'sex-check-box-blank') {
                                            checkbox1.className = 'icon-checkmark';
                                            checkbox2.className = 'sex-check-box-blank';
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



                                <div id="sexdiv" class="print-row col-lg-12">

                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h5 class="pull-left  left-padd-20"><b>(b) </b></h5>
                                        <div class="pull-left text-left left-padd-10">
                                            <h5 class="pull-left text-left ">लिंग / Gender : </h5>
                                        </div>
                                    </div>

                                    <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding ">
                                        <h5 class=" text-left ">पुरूष / Male <span class="sex-check-box5 male" id="span1" runat="server"  style="text-transform:uppercase"><i class="icon-checkmark" onclick="Clicked('ContentPlaceHolder1_ContentPlaceHolderMainBox_male','ContentPlaceHolder1_ContentPlaceHolderMainBox_female');" id="male" runat="server"></i></span></h5>
                                    </div>

                                    <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding ">
                                        <h5 class=" text-left ">स्त्री / Female <span class="sex-check-box10 female" id="span2" runat="server"  style="text-transform:uppercase"><i class="sex-check-box-blank" onclick="Clicked('ContentPlaceHolder1_ContentPlaceHolderMainBox_female','ContentPlaceHolder1_ContentPlaceHolderMainBox_male');" id="female" runat="server"></i></span></h5>
                                    </div>

                                </div>

                                <div class="print-row col-lg-12">

                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h5 class="pull-left text-left "><b>2.</b></h5>
                                        <div class="pull-left text-left left-padd-15">
                                            <h5 class="text-left ">जन्म तिथि (अंकों में) / Date of Birth :</h5>
                                        </div>
                                    </div>

                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                        <h5 class="pull-left text-left ">दिनांक / Date <span class="blank-box-f">&nbsp; </span><span class="blank-box-l">&nbsp; </span></h5>
                                        <h5 class="pull-left text-left ">माह  / Month <span class="blank-box-f">&nbsp; </span><span class="blank-box-l">&nbsp; </span></h5>
                                        <h5 class="pull-left text-left ">वर्ष / Year <span class="blank-box-f">&nbsp; </span><span class="blank-box-sl">&nbsp; </span><span class="blank-box-sl">&nbsp; </span><span class="blank-box-fl">&nbsp; </span></h5>
                                    </div>
                                </div>

                                <div class="print-row col-lg-12">

                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h5 class=" text-left left-padd-20 ">शब्दो में / in words</h5>
                                    </div>

                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 no-padding">
                                        <h4 class=" dot-box-res2 p-box-mar-tb"><b>&nbsp; </b></h4>
                                    </div>

                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h5 class=" text-left no-margin left-padd-20">कक्षा में प्रवेश के समय 31 मार्च को आयु / </h5>
                                        <h5 class="pull-left text-left left-padd-20">Age of the student as on 31st March : </h5>
                                    </div>

                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">

                                        <h5 class="pull-left text-left ">वर्ष
                                        <br />
                                            Year </h5>
                                        <h5 class="pull-left text-left blank-box-mang"><span class="blank-box-f">&nbsp; </span><span class="blank-box-sl">&nbsp; </span><span class="blank-box-sl">&nbsp; </span><span class="blank-box-fl">&nbsp; </span></h5>


                                        <h5 class="pull-left text-left ">माह
                                        <br />
                                            Month </h5>
                                        <h5 class="pull-left text-left blank-box-mang"><span class="blank-box-f">&nbsp; </span><span class="blank-box-fl">&nbsp; </span></h5>


                                        <h5 class="pull-left text-left ">दिन
                                        <br />
                                            Day </h5>
                                        <h5 class="pull-left text-left blank-box-mang"><span class="blank-box-f">&nbsp; </span><span class="blank-box-fl">&nbsp; </span></h5>
                                    </div>

                                </div>

                                <div class="print-row col-lg-12">
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h5 class="pull-left "><b>3.</b></h5>
                                        <h5 class=" text-left left-padd-20 ">बच्चे का रक्त समूह / Blood Group of the child :</h5>
                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                        <h4 class=" dot-box-res2 p-box-mar-tb"><b>&nbsp; </b></h4>
                                    </div>
                                </div>

                                <div class="print-row col-lg-12">

                                    <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                                        <h5 class="pull-left "><b>4.</b></h5>
                                        <h5 class=" left-padd-20 marg-bot-5">क्या आप सामान्य श्रेणी /  अनुसूचित जाति / अनुसूचित जनजाति / ओबीसी / आर्थिक रूप से कमजोर वर्ग / विकलांग / इकलौती कन्या हैं; यदि हाँ तो प्रमाण पत्र संलग्न करें।  </h5>
                                        <h5 class=" left-padd-20 marg-bot-5">Do you belong to General / SC / ST / OBC / EWS / Disabled / SG Child? Attach certificate.</h5>
                                        <h5 class=" left-padd-20 marg-bot-5">निम्नलिखित में से जो लागू हो, उसे सही ( <i class="icon-checkmark"></i>) करें। </h5>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12  no-padding">

                                        <h5 class="pull-left text-center box-line-height left-padd-25">सामान्य श्रेणी
                                        <br />
                                            Gen. Cat.
                                        <br />
                                            <span class="blank-box-ch">&nbsp; </span></h5>
                                        <h5 class="pull-left text-center box-line-height left-padd-25">अनु. जाति
                                    <br />
                                            SC
                                    <br />

                                            <span class="blank-box-ch">&nbsp; </span></h5>
                                        <h5 class="pull-left text-center box-line-height left-padd-25">अनु. जनजाति
                                    <br />
                                            ST
                                    <br />

                                            <span class="blank-box-ch">&nbsp; </span></h5>
                                        <h5 class="pull-left text-center box-line-height left-padd-25">ओ.बी.सी.
                                    <br />
                                            OBC
                                    <br />

                                            <span class="blank-box-ch">&nbsp; </span></h5>
                                        <h5 class="pull-left text-center box-line-height left-padd-25">आर्थिक रूप से कमजोर वर्ग
                                    <br />
                                            EWS
                                    <br />

                                            <span class="blank-box-ch">&nbsp; </span></h5>
                                        <h5 class="pull-left text-center box-line-height left-padd-25">विकलांग
                                    <br />
                                            Disabled
                                    <br />

                                            <span class="blank-box-ch">&nbsp; </span></h5>
                                        <h5 class="pull-left text-center box-line-height left-padd-25">इकलौती कन्या
                                    <br />
                                            S.G. Child
                                    <br />

                                            <span class="blank-box-ch">&nbsp; </span></h5>
                                    </div>
                                </div>

                                <div class="print-row col-lg-12">
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h5 class="pull-left "><b>5.</b></h5>
                                        <h5 class=" text-left left-padd-20 ">संलग्न प्रपत्र का ब्यौरा / Details of Certificate attached </h5>
                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                        <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                    </div>
                                </div>
                                <div class="print-row col-lg-12">

                                    <div class="col-lg-12 no-padding">
                                        <h5 class="pull-left "><b>6.</b></h5>
                                        <h5 class="pull-left text-left left-padd-15 ">माता-पिता का ब्यौरा / Details of Parents : </h5>
                                    </div>
                                    <div class="col-lg-12 no-padding">
                                        <%-- <table class="table table-bordered table-des">--%>
                                        <table class="table no-bm  p-table-bordered table-bordered table-des">
                                            <thead>
                                                <tr>
                                                    <td class="tab-titel1 p-pad-2 tab-titel50">
                                                        <h5><strong>माता -पिता का ब्यौरा/ Details of Parents</strong></h5>
                                                    </td>
                                                    <td class="tab-titel2 p-pad-2 tab-titel25">
                                                        <h5><strong>माता / Mother</strong></h5>
                                                    </td>
                                                    <td class="tab-titel3 p-pad-2 tab-titel25">
                                                        <h5><strong>पिता / Father</strong></h5>
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="p-pad-2">
                                                        <h5 class="pull-left marg-bot-5"><b>(i)</b></h5>
                                                        <h5 class=" text-left marg-bot-5 left-padd-25 ">नाम / Name (in Capital letters)</h5>
                                                    </td>
                                                    <td class="p-pad-2"></td>
                                                    <td class="p-pad-2"></td>
                                                </tr>
                                                <tr>
                                                    <td class="p-pad-2">
                                                        <h5 class="pull-left marg-bot-5"><b>(ii)</b></h5>
                                                        <h5 class=" text-left marg-bot-5  left-padd-25 ">राष्ट्रीयता / Nationality & व्यवसाय / Occupation</h5>

                                                    </td>
                                                    <td class="p-pad-2"></td>
                                                    <td class="p-pad-2"></td>
                                                </tr>
                                                <tr>
                                                    <td class="p-pad-2">
                                                        <h5 class="pull-left marg-bot-5"><b>(iii)</b></h5>
                                                        <h5 class=" text-left marg-bot-5 left-padd-25 ">कार्यालय का नाम, पूरा पता व दूरभाष / Name of Office & full address with tele. no. </h5>
                                                        <%--<h5 class=" marg-bot-5 text-left left-padd-25"></h5>--%>
                                                    </td>
                                                    <td class="p-pad-2"></td>
                                                    <td class="p-pad-2"></td>
                                                </tr>
                                                <tr>
                                                    <td class="p-pad-2">
                                                        <h5 class="pull-left "><b>(iv)</b></h5>
                                                        <h5 class=" text-left marg-bot-5 left-padd-25 ">पूर्ण आवासीय पता एवं दूरभाष / Full residential address with tele. no. </h5>
                                                        <%--<h5 class=" marg-bot-5 text-left left-padd-25">Full residential address with tele. no.</h5>--%>
                                                    </td>
                                                    <td class="p-pad-2"></td>
                                                    <td class="p-pad-2"></td>
                                                </tr>
                                                <tr>
                                                    <td class="p-pad-2">
                                                        <h5 class="pull-left marg-bot-5"><b>(v)</b></h5>
                                                        <h5 class=" text-left marg-bot-5 left-padd-25 ">स्थायी पता / Permanent Address</h5>
                                                    </td>
                                                    <td class="p-pad-2"></td>
                                                    <td class="p-pad-2"></td>
                                                </tr>
                                                <tr>
                                                    <td class="p-pad-2">
                                                        <h5 class="pull-left marg-bot-5"><b>(vi)</b></h5>
                                                        <h5 class=" text-left marg-bot-5  left-padd-25 ">वार्षिक आय / Annual Income ( <i class="fa fa-inr"></i>)</h5>
                                                    </td>
                                                    <td class="p-pad-2"></td>
                                                    <td class="p-pad-2"></td>
                                                </tr>
                                                <tr>
                                                    <td class="p-pad-2">
                                                        <h5 class="pull-left marg-bot-5"><b>(vii)</b></h5>
                                                        <h5 class=" text-left marg-bot-5 left-padd-25 ">शैक्षिक योग्यता / Educational Qualification</h5>
                                                    </td>
                                                    <td class="p-pad-2"></td>
                                                    <td class="p-pad-2"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>


                                </div>

                                <div class="print-row col-lg-12">
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h5 class="pull-left "><b>7.</b></h5>
                                        <h5 class="pull-left text-left left-padd-15 ">शारीरिक अपंगता / Physical Disability  </h5>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                        <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                        <h5 class="pull-left text-left  ">पहचान का निशान / Mark of Identificatioin  </h5>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                        <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h5 class="pull-left text-left  ">एलर्जी / Allergies </h5>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1  no-padding">
                                        <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                    </div>

                                </div>

                                <div class="print-row col-lg-12">
                                    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                                        <h5 class="pull-left "><b>8.</b></h5>
                                        <h5 class=" text-left p-marg-bot-0 left-padd-20 ">इसी विद्यालय में पढ़ने वाले सगे भाई / बहन के नाम एवं कक्षा (यदि हो)</h5>
                                        <h5 class="pull-left text-left  left-padd-15">Name and class of siblings studing in this school (if any) </h5>
                                    </div>
                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding">
                                        <h4 class=" dot-box-res p-marg-top-15 marg-top-5"><b>&nbsp; </b></h4>
                                    </div>
                                </div>
                                <div class="print-row col-lg-12">
                                    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                                        <h5 class="pull-left "><b>9.</b></h5>
                                        <h5 class=" text-left p-marg-bot-0 left-padd-20">स्थानीय अभिभावक का पता (यदि हो)</h5>
                                        <h5 class="pull-left text-left  left-padd-15">Name & address of local guardian with relation (if any) :</h5>
                                    </div>
                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding">
                                        <h4 class=" dot-box-res p-box-mar-tb"><b>&nbsp; </b></h4>
                                    </div>

                                    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 left-padd-25">
                                        <h4 class=" dot-box-res2 p-box-mar-tb"><b>&nbsp; </b></h4>
                                    </div>

                                    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                                        <h5 class="pull-left text-left  ">स्थानीय अभिभावक का दूरभाष / Contact No. of Local Guardian </h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h4 class=" dot-box-res2 p-box-mar-tb"><b>&nbsp; </b></h4>
                                    </div>
                                </div>
                                <div class="print-row col-lg-12">

                                    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding ">
                                        <h5 class="pull-left "><b>10.</b></h5>
                                        <h5 class=" text-left p-marg-bot-0 left-padd-20 ">अन्तिम विद्यालय का नाम व पता जहाँ पढ़ा हो </h5>
                                        <h5 class="pull-left text-left left-padd-10">Name & Address of the school last attended with Class :</h5>
                                    </div>
                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding">
                                        <h4 class=" dot-box-res p-box-mar-tb"><b>&nbsp; </b></h4>
                                    </div>
                                </div>

                                <div class="print-row col-lg-12">
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding ">
                                        <h5 class="pull-left "><b>11.</b></h5>
                                        <h5 class=" text-left  left-padd-20 ">क्या पिछला विद्यालय केन्द्रीय माध्यमिक शिक्षा बोर्ड से सम्बद्धता प्राप्त था / Whether last school was CBSE affiliated : </h5>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h4 class=" dot-box-res2 p-box-mar-tb"><b>&nbsp; </b></h4>
                                    </div>
                                </div>

                                <div class="print-row col-lg-12">

                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding ">
                                        <h5 class="pull-left "><b>12.</b></h5>
                                        <h5 class=" text-left p-marg-bot-0  left-padd-20 ">यदि पिछला विद्यालय केन्द्रीय माध्यमिक शिक्षा बोर्ड से सम्बद्ध नहीं है तो सम्बन्धित बोर्ड का नाम दर्शायें </h5>
                                        <h5 class="pull-left text-left left-padd-10">If the last school was not affiliated with CBSE, specify name of the Board :</h5>
                                    </div>
                                    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                                        <h4 class=" dot-box-res p-box-mar-tb"><b>&nbsp; </b></h4>
                                    </div>

                                </div>
                                <div class="print-row col-lg-12">

                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding ">
                                        <h5 class="pull-left "><b>13.</b></h5>
                                        <h5 class=" text-left  left-padd-20 ">(a) विगत परीक्षा परिणाम / Result of last examination </h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                        <h4 class=" dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding ">
                                        <h5 class=" text-left   ">(b) प्रतिशत / Percentage </h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h4 class=" dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                    </div>

                                </div>
                                <div class="print-row col-lg-12">

                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding ">
                                        <h5 class="pull-left "><b>14.</b></h5>
                                        <h5 class=" text-left p-marg-bot-0  left-padd-20 ">क्या स्थानान्तरण प्रमाण पत्र संलग्न है? हाँ / नहीं</h5>
                                        <h5 class="pull-left text-left left-padd-10">Whether last transfer certificate is attached? Yes / No :</h5>
                                    </div>


                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding ">
                                        <h5 class=" text-left p-marg-bot-0  ">टी0सी0 का दिनांक </h5>
                                        <h5 class="pull-left ">Date of T.C. </h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h4 class=" dot-box-res "><b>&nbsp; </b></h4>
                                    </div>

                                </div>
                                <div class="print-row col-lg-12">

                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding ">
                                        <h5 class="pull-left "><b>15.</b></h5>
                                        <h5 class=" text-left  left-padd-20 ">मातृ-भाषा / Mother Tongue </h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding ">
                                        <h5 class=" text-left  left-padd-25 ">गृह नगर / Home Town </h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>

                                </div>
                                <div class="print-row col-lg-12">

                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2  no-padding ">
                                        <h5 class="pull-left "><b>16.</b></h5>
                                        <h5 class=" text-left  left-padd-20 ">ईमेल / Email </h5>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding">
                                        <h5 class=" text-left  left-padd-25 ">आधार नं / Aadhaar No. </h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>

                                </div>

                                <div class="print-row col-lg-12 text-center">
                                    <h3 class="form-decl p-mgbt-xs-5"><span>Declaration by the parents</span></h3>
                                </div>

                                <div class=" col-lg-12 ">
                                    <h5 class=" text-left marg-bot-5 left-padd-20 ">मैं एतद् द्वारा घोषणा करता/करती हूँ कि मेरे द्वारा दी गयी उपर्युक्त सूचना मेरी जानकारी में सत्य व सही है। मैं विद्यालय के नियमों से प्रतिबद्ध रहूँगा/रहूँगी।</h5>
                                    <h5 class=" text-left marg-bot-5  left-padd-20 ">I hereby declare that the above information furnished by me is correct to the best of my knowledge & belief. I shall abide by the rules of the school.</h5>
                                    <h5 class=" text-left marg-bot-25 p-mgbt-xs-15 left-padd-20 ">  </h5>
                                </div>

                                <div class="print-row col-lg-12">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-left text-left  ">दिनांक / Date :</h5>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-right text-right">माता/पिता के हस्ताक्षर / Signature of Parents </h5>
                                    </div>
                                </div>

                                <div class=" col-lg-12" style="margin-top: 100px;">
                                    <div class="family-pic-box">
                                        <h5 class="text-center "><span>Paste Family Photograph (Self Attested) </span></h5>
                                    </div>
                                </div>

                                <div class=" col-lg-12 ">
                                    <h5 class="h-b-line">&nbsp;</h5>
                                </div>

                                <div class="print-row col-lg-12">
                                    <div class="col-lg-12 no-padding ">
                                        <h5 class="text-left  "><b>Test Report of the Student</b></h5>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding ">
                                        <h5 class="text-left  "><b>#</b></h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 ">
                                        <h5 class="text-center  "><b>Subject</b></h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h5 class="text-center  "><b>Class</b></h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h5 class="text-center  "><b>Fit Or Unfit</b></h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h5 class="text-center"><b>Sign. of the Teacher</b></h5>
                                    </div>

                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1  no-padding">
                                        <h5 class="text-left  "><b>1.</b></h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 ">
                                        <h5 class="text-center  "><b>English</b></h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>


                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1  no-padding">
                                        <h5 class="text-left  "><b>2.</b></h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 ">
                                        <h5 class="text-center  "><b>Hindi</b></h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>

                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1  no-padding">
                                        <h5 class="text-left  "><b>3.</b></h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 ">
                                        <h5 class="text-center  "><b>Maths</b></h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>

                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1  no-padding">
                                        <h5 class="text-left  "><b>4.</b></h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 ">
                                        <h5 class="text-center  "><b>Science</b></h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>

                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                        <h5 class="text-left  "><b>5.</b></h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2  ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                </div>

                                <div class=" col-lg-12 ">
                                    <h5 class="h-b-line">&nbsp;</h5>
                                </div>
                                <div class="print-row col-lg-12">
                                    <div class="col-lg-12 ">
                                        <h5 class="text-center office-title p-mgbt-xs-5"><span>FOR OFFICE USE ONLY </span></h5>
                                    </div>
                                </div>

                                <div class="print-row col-lg-12">

                                    <div class="col-lg-12  no-padding ">
                                        <h5 class="pull-left "><b>1.</b></h5>
                                        <h5 class=" text-left  left-padd-20 ">प्रमाणित किया जाता है की मैंने आवेदन-पत्र और सम्बद्ध कागजातों की जाँच कर ली है।</h5>
                                        <h5 class=" text-left  left-padd-20 ">Certified that I have checked the application form and the relevant papers are found in order.</h5>
                                    </div>
                                    <div class="col-lg-12 no-padding">
                                        <h5 class=" text-right p-mgtp-10 marg-top-20">प्रवेश प्रभारी / Admission In-Charge</h5>
                                    </div>
                                </div>
                                <div class="print-row col-lg-12 p-mgtp-10 marg-top-20">

                                    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5  no-padding ">
                                        <h5 class="pull-left "><b>2.</b></h5>
                                        <h5 class=" text-left  left-padd-20 ">सम्बद्ध कागजातों के निरीक्षणोंपरान्त एवम् शुल्क प्रप्तोपरान्त कृपया कक्षा </h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 print-row no-padding">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                        <h5 class="text-center ">वर्ग</h5>
                                    </div>

                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 print-row no-padding ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h5 class="text-center ">में प्रवेश दें।</h5>
                                    </div>
                                    </div>
                                     <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding ">
                                        <h5 class=" text-left  left-padd-20 ">Please admit to Class </h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                        <h5 class="text-center ">Section </h5>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                                        <h5 class="text-left ">after checking the relevant papers and realise the dues.</h5>
                                    </div>

                                </div>

                                <div class="print-row col-lg-12 p-mgtp-15 marg-top-20">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-left text-left  ">दिनांक / Date :</h5>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-right text-right">प्राचार्य / Principal </h5>
                                    </div>
                                </div>

                                <div class=" col-lg-12 ">
                                    <h5 class="h-b-line">&nbsp;</h5>
                                </div>

                                <div class="print-row col-lg-12 p-mgtp-5 marg-top-20">

                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding ">
                                        <h5 class=" text-left ">Admitted to Class </h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                        <h5 class="text-center ">Section </h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2  no-padding ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2  no-padding">
                                        <h5 class="text-center">Fee Receipt No.</h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>

                                </div>
                                <div class="print-row col-lg-12 ">
                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                        <h5 class="text-left ">Dated</h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2  no-padding ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2  ">
                                        <h5 class="text-left">issued.</h5>
                                    </div>
                                </div>
                                <div class="print-row col-lg-12 ">
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                        <h5 class="text-left ">Details of amount received: </h5>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 print-row no-padding">
                                        <div class="col-lg-12 print-row no-padding">
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding">
                                                <h5 class="text-left ">Admission Fee  </h5>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding ">
                                                <h5 class="pull-left text-left dot-box-w3"><b><i class="fa fa-inr"></i></b></h5>
                                                <h4 class="pull-left text-left dot-box">&nbsp; </h4>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 print-row no-padding">
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding">
                                                <h5 class="text-left ">Tuition Fee</h5>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding ">
                                                <h5 class="pull-left text-left dot-box-w3"><b><i class="fa fa-inr"></i></b></h5>
                                                <h4 class="pull-left text-left dot-box">&nbsp; </h4>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 print-row no-padding">
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding">
                                                <h5 class="text-left ">Any other Fee  </h5>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding ">
                                                <h5 class="pull-left text-left dot-box-w3"><b><i class="fa fa-inr"></i></b></h5>
                                                <h4 class="pull-left text-left dot-box">&nbsp; </h4>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 print-row no-padding">
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding">
                                                <h5 class="text-left ">Computer Fee</h5>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding ">
                                                <h5 class="pull-left text-left dot-box-w3"><b><i class="fa fa-inr"></i></b></h5>
                                                <h4 class="pull-left text-left dot-box">&nbsp; </h4>
                                            </div>
                                        </div>

                                        <div class="col-lg-12 print-row no-padding ">
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding pay-box">
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding">
                                                    <h5 class="text-left no-margin"><b>Total Fee</b></h5>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                                    <h5 class="pull-left text-left no-margin dot-box-w3"><b><i class="fa fa-inr"></i></b></h5>
                                                    <h4 class="pull-left text-left no-margin dot-box">&nbsp; </h4>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="print-row col-lg-12">

                                    <div class="col-lg-12  no-padding ">

                                        <h5 class=" text-left   ">कक्षा उपस्थिति पंजिका में नाम दर्ज किया गया।</h5>
                                        <h5 class=" text-left   ">Name has been entered in the Class Attendance Register : ( <i class="icon-checkmark"></i>) Yes <span class="blank-box-f">&nbsp; </span>&nbsp; &nbsp; No <span class="blank-box-f">&nbsp; </span></h5>
                                    </div>
                                    <div class="col-lg-12  no-padding ">

                                        <h5 class=" text-left   ">प्रमाणित किया जाता है की समस्त प्रविष्टियाँ छात्र पंजिका में दर्ज की गई एवम् शुल्क का भुगतान इस कार्यालय द्वारा किया गया।</h5>
                                        <h5 class=" text-left   ">Certified that all the entries have been made in the Scholar’s Register and the dues have been received.</h5>
                                    </div>

                                </div>
                                <div class="print-row col-lg-12 ">
                                    <div class="col-lg-12  no-padding">
                                        <h5 class=" text-left">विद्यार्थी की छात्र पंजीयन संख्या (ए. डब्लू. आर.)/</h5>
                                    </div>
                                    <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                                        <h5 class="text-left ">Registration No. of the student in Admission Withdrawal Register is </h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding ">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                        <h5 class="text-center">Vol.</h5>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3  no-padding">
                                        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                                    </div>

                                </div>

                                <div class="print-row col-lg-12 marg-top-20">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-left text-left  ">दिनांक / Date :</h5>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-right text-right">कार्यालय अधीक्षक / Office Suptd. </h5>
                                    </div>
                                </div>

                                <div class=" col-lg-12 ">
                                    <h5 class="h-b-line">&nbsp;</h5>
                                </div>

                                <div class="print-row col-lg-12">

                                    <div class="col-lg-12  no-padding ">

                                        <h5 class=" text-left   ">बोर्ड द्वारा निर्धारित मानकों के अनुसार छात्र के आवेदन को प्रवेश हेतु स्वीकार करते हुए अनुमोदित किया जाता है।</h5>
                                        <h5 class=" text-left   ">Admission considered by the school is in accordance with the provisions of the Board & approved.</h5>
                                    </div>

                                </div>
                                <div class="print-row col-lg-12 p-mgtp-10 marg-top-20">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-left text-left  ">&nbsp;</h5>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-right text-right">हस्ताक्षर प्राचार्य एवम् कार्यालय की मोहर </h5>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-left text-left  ">दिनांक / Date :</h5>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-right text-right">Sign. of Principal & Official Seal </h5>
                                    </div>
                                </div>
                                <!-- panel-body -->

                            </div>
                        </div>
                    </div>
                    <!-- Panel Widget -->
                </div>
            </div>
          
            <div class="col-sm-2">
               

                <asp:LinkButton ID="lnkback" runat="server" CssClass="btn-print-box" Style="color: #CC0000" OnClick="lnkback_Click"
                    title="Go back to Admission Form Fee Desposit" data-placement="left"><i class="fa fa-reply"></i></asp:LinkButton>
                &nbsp;
                            <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click" CssClass="btn-print-box"
                                title="Print Admission Form" data-placement="left"><i class="icon-printer"></i></asp:LinkButton>

            </div>
        </div>
      

    </div>
   
</asp:Content>

