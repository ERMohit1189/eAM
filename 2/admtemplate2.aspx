<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="admtemplate2.aspx.cs" Inherits="_2.admtemplate2" %>

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
                                <div class="panel-body p-panel-body" style="border: 2px solid #000;">
                                    <style>
                                        @media print {
                                            h1, h2, h3, h4, h5, h6 {
                                                margin: 0 0 7px !important;
                                                font-weight: 600 !important;
                                                font-family: unset !important;
                                                color: inherit;
                                                text-rendering: optimizelegibility;
                                                font-size: 12px !important;
                                            }

                                            .sr-no {
                                                /* border-bottom: 2px dotted rgb(128, 128, 128); */
                                                font-size: 20px;
                                                font-weight: 600;
                                                padding: 0px;
                                                margin: -4px 0px 0px 5px;
                                            }

                                            .dot-box-res2 {
                                                border-bottom: 1px dotted rgb(128, 128, 128) !important;
                                                width: 100%;
                                                margin-left: 5px;
                                                margin-bottom: 3px !important;
                                            }

                                        }
                                        .dot-box-res2 {
                                            border-bottom: 1px dotted rgb(128, 128, 128) !important;
                                            width: 100%;
                                            margin-left: 5px;
                                            margin-bottom: 3px !important;
                                        }
                                        .sr-number {
                                            font-size: 20px;
                                            font-weight: 600;
                                            padding: 0px 25px 0px 5px;
                                            margin: -4px 0px 0px 5px;
                                        }
                                        .panel-body .table td{padding:3px 5px !important;}
                                    </style>
                                    <div class="print-row col-lg-12">
                                        <table class="table">
                                            <tr>
                                                <td style="padding: 0 !important;">
                                                    <div class="" id="header" runat="server" style="width:100%;"></div>                                                   
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="print-row col-xs-12  mgbt-xs-15 p-mgbt-xs-5 text-center">
                                        <div style="display:flex;gap:5px; align-items:center;justify-content:center;">
                                        <div class="main-titel-box" style="display:flex;gap:5px; align-items:center;">
                                            <h3 style="text-transform: uppercase; margin-bottom:0;display:flex;gap:5px; align-items:center;">
                                                <span style="font-size: 20px !important;font-weight: 600;">Admission Form</span>
                                                <asp:Label runat="server" ID="lblAdmissionType" Style="text-transform: uppercase;    font-weight: 600; font-size: 20px !important;"></asp:Label>
                                                <span style="font-size: 20px !important;font-weight: 600;">(<asp:Label ID="lblSessionFor" runat="server" CssClass="" Text="Label" Style="font-size: 20px !important;text-transform: uppercase;font-weight: 600;"></asp:Label>)</span>
                                                </h3>
                                             <h3 style="margin-bottom:0;font-weight: 600;font-size: 20px !important;"></h3>
                                        </div>
                                        </div>
                                    </div>
                                    <div class="print-row col-xs-12  mgbt-xs-15 p-mgbt-xs-5" style="padding-top: 0px;">
                                        <div class="col-lg-12 col-md-6 col-xs-6 col-sm-6 no-padding">
                                            <h5 class="pull-left text-left ">S. No.&nbsp;</h5>
                                            <h4 class="pull-left text-left sr-no">
                                                <asp:Label ID="lblSrno" runat="server" Style="text-transform: uppercase"></asp:Label>
                                            </h4>
                                        </div>
                                    </div>

                                    <div class="print-row col-xs-12  mgbt-xs-15 p-mgbt-xs-5" style="padding-top: 0px;">
                                        <div style="display:flex;gap:10px;align-items:center;">
                                            <div style="display:flex;gap:10px;align-items:center;">
                                                <h5 class="pull-left text-left" style="white-space:nowrap;"><b>Admission No. (To be filled by office)&nbsp;</b></h5>
                                            </div>
                                           <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pull-left text-left" style="padding-left: 0px !important; margin-left: 0px;">
                                                <h4 class="dot-box-res2 p-box-mar-tb"><b>&nbsp; </b></h4>
                                            </div>
                                            <div class=" text-left col-lg-1 col-md-1 col-xs-1 col-sm-1 no-padding pull-left text-left">
                                                <h5 class="pull-left text-left" style="white-space:nowrap;"><b>UDISE PEN&nbsp;&nbsp;&nbsp;</b></h5>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pull-right text-left no-padding" style="padding-left:10px !important;">
                                                <h4 class=" dot-box-res2 p-box-mar-tb"><b>&nbsp; </b></h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="print-row col-xs-12" style="padding-top:0px; padding-bottom:0px !important;">
                                        <div class="row">
                                            <div class="col-xs-9">
                                                <div class="col-xs-12 no-padding" style="margin-bottom:6px;">
                                                    <h5 class="pull-left text-left ">CLASS in which admission is sought : </h5>
                                                    <h5 class="pull-left text-left">
                                                        <asp:Label ID="lblClass" runat="server" Style="text-transform: uppercase; height: 20px; padding:0px 5px;"></asp:Label>
                                                    </h5>
                                                </div>

                                                 <div class="col-xs-12 no-padding" style="margin-bottom:6px;">
                                                     <h5 class="pull-left text-left "><b>1.&nbsp;Name of the child in full (in capital letters)</b>
                                                         <asp:Label ID="lblStudentName" runat="server" Style="text-transform: uppercase; height: 20px; padding:0px 5px;"></asp:Label>
                                                     </h5>
                                                 </div>
                                                <div class="col-xs-12 no-padding" style="margin-bottom:6px;">
                                                     <h5 class="pull-left text-left "><b>2.&nbsp;Gender</b>
                                                         <asp:Label ID="lblGender" runat="server" Style="text-transform: uppercase; height: 20px; padding:0px 5px;"></asp:Label>
                                                     </h5>
                                                 </div>

                                                <div id="sexdiv" class="col-xs-12 no-padding" style="margin-bottom:6px;">
                                                    <div class="col-xs-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                                        <h5 class="pull-left text-left "><b>3.&nbsp;a.&nbsp;Date of Birth</b>
                                                            <asp:Label ID="lbldob" runat="server" Style="text-transform: uppercase; height: 20px; padding:0px 5px;"></asp:Label>
                                                        </h5>
                                                    </div>
                                                </div>
                                                <div class="row" style="padding-top:8px;padding-left:10px;">
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                                        <h5 class=" text-left left-padd-20 "><b>b.&nbsp;In words</b></h5>
                                                    </div>
                                                    <div class="col-xs-8 no-padding">
                                                        <h4 class=" dot-box-res2 p-box-mar-tb" id="dobInBirrd" runat="server"><b>&nbsp; </b></h4>
                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 20px;">
                                                        <h5 class=" text-left" style="font-size: 11px;">(Attach photocopy of Birth Certificate issued by the concerned authority)</h5>
                                                    </div>
                                                </div>
                                                <div class="row" style="padding-top:4px;padding-left:10px;">
                                                    <div class="col-xs-6 no-padding">
                                                        <h5 class=" text-left left-padd-20 "><b>c.&nbsp;Age of the child as on 31st March</b></h5>
                                                    </div>
                                                    <div class="col-xs-6 no-padding">
                                                        <h4 class=" dot-box-res2 p-box-mar-tb" id="H1" runat="server"><b>&nbsp; </b></h4>
                                                    </div>                                                   
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div style="display:flex; justify-content:end;">
                                                    <div class="img-pass-box text-center" style="display:flex; justify-content:center;align-items:center; width:140px; height: 145px;">                                                        
                                                        <h5>Student's Latest Photo</h5>
                                                    </div>
                                                 </div>
                                            </div>
                                        </div>
                                        

                                    </div>
                                   
                                    
                                    
                                    <div class="print-row col-lg-12" style="padding-top:6px;">

                                        <div class="col-lg-12 no-padding">
                                            <h5 class="pull-left "><b>4.&nbsp;Whether the candidate is- </b></h5>
                                        </div>
                                        <div id="ccc" class="print-row col-lg-12" style="padding-top:0px;">
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                <h5 class="pull-left text-left "><b>a.</b></h5>
                                                <div class="pull-left text-left left-padd-15">
                                                    <h5 class="text-left ">Single Girl Child:</h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                <h5 class=" text-left ">Yes
                                                    <label class="sex-check-box5 male" id="span4" runat="server" style="text-transform: uppercase; width: 18px; height: 17px; padding: 5px 5px;"><i class="sex-check-box-blank" id="I1" runat="server"></i></label>
                                                </h5>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                <h5 class=" text-left ">No
                                                    <label class="sex-check-box5 female" id="span5" runat="server" style="text-transform: uppercase; width: 20px; height: 20px; padding: 5px 5px;"><i class="sex-check-box-blank" id="I2" runat="server"></i></label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div id="ccc" class="print-row col-lg-12" style="padding-top:0px;">
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                                <h5 class="pull-left text-left "><b>b.</b></h5>
                                                <div class="pull-left text-left left-padd-15">
                                                    <h5 class="text-left ">Specially abled (Divyangjan):</h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                <h5 class=" text-left ">Yes
                                                    <label class="sex-check-box5 male" id="span6" runat="server" style="text-transform: uppercase; width: 20px; height: 20px; padding: 5px 5px;"><i class="sex-check-box-blank" id="I3" runat="server"></i></label>
                                                </h5>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                <h5 class=" text-left ">No
                                                    <label class="sex-check-box5 female" id="span7" runat="server" style="text-transform: uppercase; width: 20px; height: 20px; padding: 5px 5px;"><i class="sex-check-box-blank" id="I4" runat="server"></i></label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="print-row col-lg-12" style="padding-top:0px;">
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                            <h5 class="pull-left "><b>5.&nbsp;Religion</b></h5>
                                        </div>
                                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5"><h4 class="dot-box-res2 p-box-mar-tb " style="padding-left: 0;"><b>&nbsp; </b></h4></div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                                <h5 class="pull-left text-left left-padd-15 " style="padding-right: 0;"><b>6.&nbsp;Blood Group</b></h5>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4  no-padding" style="padding-left: 0;">
                                                <h4 class="dot-box-res2 p-box-mar-tb " style="padding-left: 0;"><b>&nbsp; </b></h4>
                                            </div>
                                        </div>

                                         <div class="print-row col-lg-12" style="padding-top:6px;">
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                            <h5 class="pull-left "><b>7.&nbsp;Category (Attach photocopy)</b></h5>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-8 no-padding">

                                            <h5 class="pull-left text-center box-line-height" style="margin-right: 25px !important;">General
                                                <label class="blank-box-ch" style="text-transform: uppercase; width: 20px; height: 20px; padding:0px 5px;">&nbsp; </label>
                                            </h5>
                                            <h5 class="pull-left text-center box-line-height" style="margin-right: 25px !important;">SC

                                                <label class="blank-box-ch" style="text-transform: uppercase; width: 20px; height: 20px; padding:0px 5px;">&nbsp; </label>
                                            </h5>
                                            <h5 class="pull-left text-center box-line-height" style="margin-right: 25px !important;">ST

                                                <label class="blank-box-ch" style="text-transform: uppercase; width: 20px; height: 20px; padding: 0px 5px;">&nbsp; </label>
                                            </h5>
                                            <h5 class="pull-left text-center box-line-height" style="margin-right: 25px !important;">OBC
    
                                                <label class="blank-box-ch" style="text-transform: uppercase; width: 20px; height: 20px; padding:0px 5px;">&nbsp; </label>
                                            </h5>
                                            <h5 class="pull-left text-center box-line-height" style="margin-right: 25px !important;">EWS

                                                <label class="blank-box-ch" style="text-transform: uppercase; width: 20px; height: 20px; padding:0px 5px;">&nbsp; </label>
                                            </h5>

                                        </div>
                                    </div>
                                    <div class="print-row col-lg-12">
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                            <h5 class="pull-left "><b>8.&nbsp;Aadhaar No. (Attach photocopy)</b></h5>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                            <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                        </div>
                                    </div>
                                   
                                    <div class="print-row col-lg-12" style="padding-top:6px;">

                                        <div class="col-lg-12 no-padding">
                                            <h5 class="pull-left "><b>9.&nbsp;Details of Parents</b></h5>
                                        </div>
                                        <div class="col-lg-12 no-padding">
                                            <table class="table p-table-bordered" style="margin-bottom: 5px;">
                                                <thead>
                                                    <tr>
                                                        <td class="p-pad-2 text-center" valign="middle" style="vertical-align:middle !important;">
                                                            <h5 style="margin-bottom:0;padding-top:4px;"><strong>Details</strong></h5>
                                                        </td>
                                                        <td class="p-pad-2 text-center" valign="middle" style="vertical-align:middle !important;">
                                                            <h5 style="margin-bottom:0;padding-top:4px;"><strong>Mother</strong></h5>
                                                        </td>
                                                        <td class="p-pad-2 text-center" valign="middle" style="vertical-align:middle !important;">
                                                            <h5 style="margin-bottom:0;padding-top:4px;"><strong>Father/ Guardian</strong></h5>
                                                        </td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td class="p-pad-2" style="width: 22%;">
                                                            <h5 class="pull-left marg-bot-5" style="margin-bottom:0;padding-top:3px;"><b>i.</b></h5>
                                                            <h5 class=" text-left marg-bot-5 left-padd-25 " style="margin-bottom:0;padding-top:3px;">Name</h5>
                                                        </td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="p-pad-2" style="width: 22%;">
                                                            <h5 class="pull-left marg-bot-5" style="margin-bottom:0;padding-top:3px;"><b>ii.</b></h5>
                                                            <h5 class=" text-left marg-bot-5  left-padd-25 "style="padding-top:3px;white-space:nowrap !important;margin-bottom:0;">Educational Qualification</h5>

                                                        </td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="p-pad-2" style="width: 22%;">
                                                            <h5 class="pull-left marg-bot-5" style="margin-bottom:0;padding-top:3px;"><b>iii.</b></h5>
                                                            <h5 class=" text-left marg-bot-5 left-padd-25 " style="margin-bottom:0;padding-top:3px;">Occupation</h5>
                                                        </td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="p-pad-2" style="width: 22%;">
                                                            <h5 class="pull-left marg-bot-5" style="margin-bottom:0;padding-top:3px;"><b>iv.</b></h5>
                                                            <h5 class=" text-left marg-bot-5 left-padd-25 " style="margin-bottom:0;padding-top:3px;">Phone No.</h5>
                                                        </td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="p-pad-2" style="width: 22%;">
                                                            <h5 class="pull-left marg-bot-5" style="margin-bottom:0;padding-top:3px;"><b>v.</b></h5>
                                                            <h5 class=" text-left marg-bot-5 left-padd-25 " style="margin-bottom:0;padding-top:3px;">E-mail</h5>
                                                        </td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="p-pad-2" style="width: 22%;">
                                                            <h5 class="pull-left marg-bot-5" style="margin-bottom:0;padding-top:3px;"><b>vi.</b></h5>
                                                            <h5 class=" text-left marg-bot-5  left-padd-25 " style="margin-bottom:0;padding-top:3px;">Aadhaar No.</h5>
                                                        </td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="p-pad-2" style="width: 22%;">
                                                            <h5 class="pull-left marg-bot-5" style="margin-bottom:0;padding-top:3px;"><b>vii.</b></h5>
                                                            <h5 class=" text-left marg-bot-5 left-padd-25 " style="margin-bottom:0;padding-top:3px;">Residential Address</h5>
                                                        </td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="p-pad-2" style="width: 22%;">
                                                            <h5 class="pull-left marg-bot-5" style="margin-bottom:0;padding-top:3px;"><b>viii.</b></h5>
                                                            <h5 class=" text-left marg-bot-5 left-padd-25 " style="margin-bottom:0;padding-top:3px;">Official Address</h5>
                                                        </td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="p-pad-2" style="width: 22%;">
                                                            <h5 class="pull-left marg-bot-5" style="margin-bottom:0;padding-top:3px;"><b>ix.</b></h5>
                                                            <h5 class=" text-left marg-bot-5 left-padd-25 " style="margin-bottom:0;padding-top:3px;">Annual Income</h5>
                                                        </td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                        <td class="p-pad-2" style="width: 39%;"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>


                                    </div>
                                    <div class="print-row col-lg-12" style="padding-top:6px;">
                                        <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding">
                                            <h5 class="pull-left text-left"><b>10. a. Name & address of local guardian with relation (if any)</b></h5>
                                        </div>
                                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                                            <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                        </div>
                                    </div>
                                    <div class="print-row col-lg-12" style="padding-top:0px;">
                                         <div class="print-row col-lg-12 no-padding" style="padding-top: 10px;padding-right:0px !important;">
                                             <div class="col-xs-4">
                                                 <h5 class="pull-left text-left" style="padding-left:7px;"> <b>b. Contact No. of local guardian</b></h5>
                                             </div>
                                             <div class="col-xs-8 no-padding">
                                                 <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                             </div>
                                         </div>
                                    </div>
                                    <div class="print-row col-lg-12" style="padding-top:0px;">
                                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                                            <h5 class="pull-left "><b>11.&nbsp;Name & Address of the last school attended</b></h5>
                                        </div>
                                        <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding">
                                            <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 20px; padding-right: 0px; padding-bottom:15px !important;">
                                            <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                        </div>
                                    </div>
                                    <div class="print-row col-lg-12">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                            <h5 class="pull-left "><b>12.&nbsp;Class last attended</b></h5>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-left: 0px;">
                                            <h4 class="dot-box-res2 p-box-mar-tb " style="padding-left: 0px;"><b>&nbsp; </b></h4>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2" style="padding-right: 0; margin-left:-10px !important;">
                                            <h5 class="pull-left "><b style="white-space:nowrap;">13.&nbsp;Mother Tongue</b></h5>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding" style="padding-left: 0px; padding-left: 0px; margin-left:10px !important;">
                                            <h4 class="dot-box-res2 p-box-mar-tb " style="padding-left: 0px; padding-left: 0px;"><b>&nbsp; </b></h4>
                                        </div>

                                    </div>
                                    <div class="print-row col-lg-12" style="padding-top:6px;">

                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                            <h5 class="pull-left "><b>14.&nbsp;Previous School affiliation</b></h5>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">

                                            <h5 class="pull-left text-center box-line-height left-padd-25">(i)&nbsp;&nbsp;CBSE
                                                <label class="blank-box-ch" style="text-transform: uppercase; width: 20px; height: 20px; padding:0px 5px;">&nbsp; </label>
                                            </h5>
                                            <h5 class="pull-left text-center box-line-height left-padd-25">(ii)&nbsp;ICSE

                                                 <label class="blank-box-ch" style="text-transform: uppercase; width: 20px; height: 20px; padding:0px 5px;">&nbsp; </label>
                                            </h5>
                                            <h5 class="pull-left text-center box-line-height left-padd-25">(iii)&nbsp;IB

                                                 <label class="blank-box-ch" style="text-transform: uppercase; width: 20px; height: 20px; padding:0px 5px;">&nbsp; </label>
                                            </h5>
                                            <h5 class="pull-left text-center box-line-height left-padd-25">(iv)&nbsp;State Board

                                                <label class="blank-box-ch" style="text-transform: uppercase; width: 20px; height: 20px; padding:0px 5px;">&nbsp; </label>
                                            </h5>

                                        </div>
                                    </div>
                                    
                                    

                                </div>
                                <div style="page-break-after: always;"></div>
                               
                                
                                <div class="panel-body p-panel-body" style="border: 2px solid #000;">
                                     <div class="print-row col-lg-12" style="padding-top:8px;">
                                         <div class="col-xs-4 no-padding">
                                             <h5 class="pull-left "><b>15. a. Result of last class (Attach proof)</b></h5>
                                         </div>
                                         <div class="col-xs-4 no-padding">
                                             <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                         </div>
                                         <div class="col-xs-2 no-padding">
                                             <h5 class="pull-left "><b>b. Percentage</b></h5>
                                         </div>
                                         <div class="col-xs-2 no-padding">
                                             <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                         </div>
                                     </div>
                                    <div class="print-row col-lg-12" style="padding-top:8px;padding-right:10px;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 20%;">
                                                    <h5><b style="white-space:nowrap;">16.&nbsp;a. Transfer Certificate No. (attach original T.C.)</b></h5>
                                                </td>
                                                <td style="width: 34%;">
                                                    <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                                </td>
                                                <td style="width:18%;">
                                                    <h5><b>b.&nbsp;Date of Issue</b></h5>
                                                </td>
                                                <td style="width: 20%;">
                                                    <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                   


                                    <div class="print-row col-lg-12" style="padding-top: 10px;">

                                        <div class="col-lg-12 no-padding">
                                            <h5 class="pull-left "><b>17.&nbsp;Details of siblings studying in this school (if any)</b></h5>
                                        </div>
                                        <div class="col-lg-12 no-padding">
                                            <table class="table no-bm  p-table-bordered table-bordered table-des">
                                                <thead>
                                                    <tr style="height: 25px;">
                                                        <td class="tab-titel1 p-pad-2 tab-titel50" style="width: 50% !important;">
                                                            <h5 style="margin-bottom:2px;padding-top:3px;"><strong>Name</strong></h5>
                                                        </td>
                                                        <td class="tab-titel2 p-pad-2 tab-titel25" style="width: 15% !important;">
                                                            <h5 style="margin-bottom:2px;padding-top:3px;"><strong>Relation</strong></h5>
                                                        </td>
                                                        <td class="tab-titel3 p-pad-2 tab-titel25" style="width: 15% !important;">
                                                            <h5 style="margin-bottom:2px;padding-top:3px;"><strong>Age</strong></h5>
                                                        </td>
                                                        <td class="tab-titel3 p-pad-2 tab-titel25" style="width: 20% !important;">
                                                            <h5 style="margin-bottom:2px;padding-top:3px;"><strong>Class studying in</strong></h5>
                                                        </td>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 25px;">
                                                        <td class="p-pad-2"></td>
                                                        <td class="p-pad-2"></td>
                                                        <td class="p-pad-2"></td>
                                                        <td class="p-pad-2"></td>
                                                    </tr>
                                                    <tr style="height: 25px;">
                                                        <td class="p-pad-2"></td>
                                                        <td class="p-pad-2"></td>
                                                        <td class="p-pad-2"></td>
                                                        <td class="p-pad-2"></td>
                                                    </tr>
                                                    <tr style="height: 25px;">
                                                        <td class="p-pad-2"></td>
                                                        <td class="p-pad-2"></td>
                                                        <td class="p-pad-2"></td>
                                                        <td class="p-pad-2"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>


                                    </div>

                                    <div class="print-row col-lg-12" style="padding-top:10px;">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                            <h5 class="pull-left "><b>18.</b></h5>
                                            <h5 class="pull-left text-left left-padd-15 "></h5>
                                        </div>
                                    </div>
                                    <div class="print-row col-xs-12" style="padding-top:0px;padding-bottom:0px;display:flex;justify-content:space-between;">
                                        <div class="row">
                                            <div class=" col-xs-4"  style="padding-top:0px !important;">

                                                <div class="family-pic-box" style="padding:0px 30px; width:175px; height:175px;display:flex; justify-content:center;align-items:center;">
                                                    <h5 class="text-center " style="white-space:nowrap;"><span>Mother's Photo</span></h5>
                                                </div>
                                                <p style="padding-top:16px;">
                                                    <h4 class="dot-box-res2 p-box-mar-tb " style="margin-bottom:0px;"><b>&nbsp; </b></h4>
                                                </p>
                                                <p style="text-align:center;padding-top:0px;white-space:nowrap; style="white-space:nowrap;"">Mother's Signature</p>
                                            </div>
                                            
                                            <div class="col-xs-4"  style="padding-top:0px !important;">
                                                <div class="family-pic-box" style="padding:0px 30px; width:175px; height:175px; display:flex; justify-content:center;align-items:center;">
                                                    <h5 class="text-center "><span>Father's Photo</span></h5>
                                                </div>
                                                <p style="padding-top:16px;">
                                                    <h4 class="dot-box-res2 p-box-mar-tb " style="margin-bottom:0px;"><b>&nbsp; </b></h4>
                                                </p>
                                                <p style="text-align:center;padding-top:0px;white-space:nowrap;">Father's Signature</p>
                                            </div>
                                            <div class=" col-xs-4"  style="padding-top:0px !important;">
                                                <div class="family-pic-box" style="padding:0px 30px; width:175px; height:175px;display:flex; justify-content:center;align-items:center;">
                                                    <h5 class="text-center " style="white-space:nowrap;"><span>Local Guardian's <br />Photo</span></h5>
                                                </div>
                                                <p style="padding-top:16px;">
                                                    <h4 class="dot-box-res2 p-box-mar-tb " style="margin-bottom:0px;"><b>&nbsp; </b></h4>
                                                </p>
                                                <p style="text-align:center;padding-top:0px;white-space:nowrap;">Guardian's Signature</p>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="print-row col-lg-12" style="padding-top:0px;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                            <h5 class="pull-left text-left left-padd-15 no-padding"><b>19.&nbsp;Test Report of the Student</b></h5>
                                        </div>
                                        <br />

                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-right:0px;padding-left:24px;">
                                        <table style="width:100%; margin-left:0;margin-top:4px;">
                                            <tr>
                                                <td style="width: 5%;">
                                                    <h5>English</h5>
                                                </td>
                                                <td style="width: 10%;">
                                                    <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                                </td>
                                                <td style="width: 10%; text-align: right;">
                                                    <h5>Hindi</h5>
                                                </td>
                                                <td style="width: 10%;">
                                                    <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                                </td>
                                                <td style="width: 10%; text-align: right;">
                                                    <h5>Maths</h5>
                                                </td>
                                                <td style="width: 10%;">
                                                    <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                                </td>
                                                <td style="width: 10%; text-align: right;">
                                                    <h5>Science</h5>
                                                </td>
                                                <td style="width: 10%;">
                                                    <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                                </td>                                               
                                            </tr>
                                        </table>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-top:15px;padding-right:0px;padding-left:8px;">
                                            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                                <h5 class="pull-left text-left left-padd-15 ">Remark</h5>
                                            </div>
                                            <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding" style="padding-left:10px !important;">
                                                <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                <h5 class="pull-left text-left left-padd-15 "><b>Teacher’s Signature</b></h5>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                                <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                            </div>

                                        </div>
                                    </div>
                                  

                                    <div class="print-row col-lg-12 no-padding">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">                                           
                                            <h5 class="text-center left-padd-15 " style="padding-top:18px;"><b>DECLARATION</b></h5>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                            <h5 class="text-left" style="line-height: 26px;margin-bottom:0;">
                                                I hereby declare that the information provided above, including the Name and Date of Birth of the Candidate, Mother’s Name, and Father’s/Guardian’s Name, is correct to the best of my knowledge and belief. I shall abide by the rules of the school.
                                            </h5>
                                        </div>
                                    </div>

                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-top:15px; padding-left: 0;">
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                            <h5 class="pull-left text-left"><b>Date</b></h5>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                            <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                            <h5 class="text-right" style="text-align:right;"><b>Signature of the Parent(s)/ Guardian </b></h5>
                                        </div>

                                    </div>

                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-top:20px; padding-left: 0;">
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 no-padding">
                                            <h5 class="pull-left text-left"><b>Place</b></h5>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                            <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                            <h5 class="pull-left text-left"><b>Relation with candidate</b></h5>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                            <h4 class="dot-box-res2 p-box-mar-tb " style="margin-left:14px;"><b>&nbsp; </b></h4>
                                        </div>

                                    </div>
                                    <div class="print-row col-lg-12 no-padding">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                            <h4 class=" dot-box-res p-box-mar-tb" style="margin: 0 !important;"><b>&nbsp; </b></h4>
                                        </div>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-top:15px; padding-left: 0; margin-bottom:10px;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                            <h5 class="text-center left-padd-15 "><b>FOR THE OFFICE USE ONLY</b></h5>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-top: 5px; padding-left: 0px;">
                                            <h5>Certified that I have checked the application form and the relevant papers are found in order.&nbsp;Please admit to class<br /><br />
                                                .........................................................&nbsp;Section.........................................................</h5>
                                        </div>

                                    </div>
                                    <div class="print-row col-lg-12" style="padding-top:35px; padding-left: 0;">
                                        <div class="col-xs-1 no-padding">
                                            <h5 class="pull-left text-left"><b>Date</b></h5>
                                        </div>
                                        <div class="col-xs-3 no-padding">
                                            <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                        </div>
                                        <div class="col-xs-8 no-padding ">
                                            <h5 class="text-right"><b>Admission In-Charge</b></h5>
                                        </div>
                                    </div>
                                    <div class="print-row col-lg-12 no-padding">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                            <h4 class=" dot-box-res p-box-mar-tb" style="margin: 0 !important;"><b>&nbsp; </b></h4>
                                        </div>
                                    </div>

                                    
                                    <div class="print-row col-lg-12" style="padding-top: 10px;padding-left:0px;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12  no-padding ">
                                            <h5>Admission considered by the school is in accordance with the provisions of the Board & approved. </h5>
                                        </div>
                                    </div>
                                    <div class="print-row col-lg-12" style="padding-top:50px; padding-left:0px;">
                                         <div class="col-xs-1 no-padding">
                                             <h5 class="pull-left text-left"><b>Date</b></h5>
                                         </div>
                                         <div class="col-xs-3 no-padding">
                                             <h4 class="dot-box-res2 p-box-mar-tb "><b>&nbsp; </b></h4>
                                         </div>
                                        <div class="col-xs-8  no-padding ">
                                            <h5 class="pull-right text-right"><b>Signature of Principal & Official Seal</b></h5>
                                        </div>
                                    </div>



                                </div>
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

