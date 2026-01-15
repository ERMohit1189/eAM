<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="employment-form.aspx.cs" Inherits="admin_employment_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script>
        .print - row{

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div class="vd_content-section clearfix" id="Panel1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-6 no-padding">
                            <asp:Label ID="lblaf" runat="server" Text="Employment Form" class="no-padding txt-bold  "></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblaffno" runat="server" Style="color: #CC0000; font-weight: 700" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-6 no-padding text-right menu-action">

                            <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click" CssClass="btn-print-box"
                                title="Print Employment Form" data-placement="left"><i class="icon-printer"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkBack" runat="server" class="btn-print-box" title="Go back to Employment Form" data-placement="bottom" type="button" PostBackUrl="~/8/Employmentform.aspx"><i class="fa fa-reply"></i></asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="font-size: 12px;" class="marg-bot-30" id="divexport" runat="server">

        <div class="print-row col-sm-12">
            <div class="col-sm-12 panel widget light-widget" id="abc" runat="server">
                <style>
                    h5, .h5 {
                        font-size: 14px !important;
                        font-weight: 600 !important;
                    }

                    h4, .h4 {
                        font-size: 15px !important;
                        font-weight: 600 !important;
                    }

                    h3, .h3 {
                        font-size: 16px !important;
                        font-weight: 600 !important;
                    }
                     table tr td, th {
                        font-size: 14px !important;
                        font-weight: 600 !important;                        
                    }
                     .table-padd table tr td, th{padding-block:8px !important;}
                    .vd_checkbox label {
                        display: inline-block;
                        cursor: pointer;
                        position: relative;
                        padding-left: 25px;
                        margin-right: 15px;
                        font-size: 15px;
                        font-weight:bold !important;
                        margin-bottom: 6px;
                        color: #000 !important;
                        transition: border 0.2s linear 0s, color 0.2s linear 0s;
                        line-height: 23px;
                    }

                        .vd_checkbox label:before {
                            content: "";
                            display: inline-block;
                            width: 16px;
                            height: 16px;
                            margin-right: 10px !important;
                            position: absolute;
                            left: 6px;
                            top: 3px;
                            background-color: #fff;
                            border: 2px solid #000 !important;
                            border-radius: 3px;
                            transition: border 0.2s linear 0s, color 0.2s linear 0s;
                        }
                </style>
                <br />
                <div class="panel-body p-panel-body" style="border: 2px solid #000; padding: 10px;">

                    <div class=" col-lg-12 print-row">

                        <div class=" col-lg-12 col-md-12 col-xs-12 col-sm-12  no-padding ">
                            <div id="header" runat="server" style="width: 100%"></div>
                        </div>

                      <%--<div class=" text-center col-lg-2 col-md-2 col-xs-2 col-sm-2 no-padding">
                            <div class="img-pass-box txt-upper-alpha pull-right">
                                <h6 style="font-size: 11px !important;line-height: 18px !important;">Recent Passport size photograph</h6>
                            </div>
                        </div>--%>
                    </div>
                    <div class="print-row col-sm-12  text-center" style="padding-bottom:20px; padding-top:20px;">
                            <div class="main-titel-box  txt-upper-alpha">
                                <h3 style="text-decoration: underline;"><span>Employment Form</span></h3>
                            </div>
                    </div>
                    <div class="print-row col-lg-12  mgbt-xs-15">
                        <div class="full-width no-padding" style="padding-bottom:2px; position:relative;">
                        <div style="display:flex; width:100%; justify-content:space-between; align-items:center;">
                            <div class="text-left">
                                <div>
                                    <h4 class="pull-left text-left txt-upper-alpha">Ref. No. </h4>
                                    <h4 class="pull-left text-left left-padd-10">
                                    <asp:Label ID="lblSrno" runat="server"></asp:Label></h4>
                                </div>
                            </div>
                           <!-- <div class=" text-center">
                                <div class="main-titel-box txt-upper-alpha">
                                    <h3 style="text-decoration: underline;"><span>Employment Form</span></h3>
                                </div>
                            </div>-->      
                            <div>
                                <div>
                                    <h4 class="text-right mgbt-xs-15 p-mgbt-xs-5">Date :
                                    <asp:Label ID="lblDate" runat="server"></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                        </div>

                    </div>
                    <div class="print-row col-lg-12 txt-upper-alpha" style="padding-bottom:2px;">
                        <div class="full-width no-padding" style="position:relative;">
                            <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                                <h3 class="pull-left"><b>Application for the post of </b></h3>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" style="padding-right: 1px !important;">
                                <h3 class="dot-box-res2 "><b>
                                    <asp:Label ID="lblDesi" runat="server"></asp:Label></b></h3>
                            </div>
                            <div style="position:absolute; right:10px;">
                                <div class="img-pass-box txt-upper-alpha pull-right">
                                    <h6 style="font-size: 11px !important;line-height: 18px !important;">Recent Passport size photograph</h6>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="print-row col-lg-10 txt-upper-alpha" style="padding-bottom:2px;">
                        <div class="full-width  no-padding">
                            <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                                <h3 class="pull-left"><b>Class & Subject Applying for </b></h3>
                            </div>
                            <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7" style="padding-right: 1px !important;">
                                <h3 class=" dot-box-res2 "><b>
                                    <br />
                                    <asp:Label ID="Label1" runat="server"></asp:Label></b></h3>
                            </div>
                        </div>
                    </div>

                    <div class="print-row col-lg-10 txt-upper-alpha" style="padding-bottom:2px;"> 
                        <div style="display:flex;justify-content:start; gap:20px; width:100%;">
                            <div class="no-padding" style="width:40%;">
                                <h5 class="pull-left " style="width:10px;"><b>1.</b></h5>
                                <h5 class="pull-left text-left left-padd-15 ">Name of Applicant</h5>
                            </div>
                            <div class="no-padding" style="width:60%;">
                                <h4 class=" dot-box-res2 "><b>
                                    <asp:Label ID="lblEmpName" runat="server"></asp:Label></b></h4>
                            </div>
                        </div>

                    </div>

                    <div class="print-row col-lg-10 txt-upper-alpha" style="padding-bottom:2px;">
                        <div class="full-width ">
                            <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                                <h5 class="pull-left " style="width:10px;"><b>2.</b></h5>
                                <h5 class="pull-left  left-padd-15 ">Father's/ Husband's Name</h5>
                            </div>
                            <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding">
                                <h4 class=" dot-box-res2 "><b>
                                    <asp:Label ID="lblFatherName" runat="server"></asp:Label></b></h4>
                            </div>
                        </div>
                    </div>

                    <div class="print-row col-lg-10 txt-upper-alpha" style="padding-bottom:2px;">

                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h5 class="pull-left" style="width:10px;"><b>3.</b></h5>
                            <h5 class="pull-left left-padd-15 ">Date of Birth</h5>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h5 class="pull-left left-padd-15 ">Marital Status</h5>

                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>

                    </div>
                    <div class="print-row col-lg-12 txt-upper-alpha" style="padding-bottom:2px;">

                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h5 class="pull-left " style="width:10px;"><b>4.</b></h5>
                            <h5 class=" text-left left-padd-25 ">Gender</h5>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                            <h4 class=" dot-box-res2 " style="padding-left: 10px;"><b>
                                <asp:Label ID="lblGender" runat="server"></asp:Label></b></h4>
                        </div>

                    </div>


                    <div class="print-row col-lg-12 txt-upper-alpha" style="padding-bottom:2px;">

                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h5 class="pull-left " style="width:10px;"><b>5.</b></h5>
                            <h5 class="pull-left left-padd-15 ">Religion</h5>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                            <h5 class=" text-left left-padd-25 ">Category</h5>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                            <asp:CheckBoxList runat="server" ID="rdoCast" RepeatDirection="Horizontal" class="vd_checkbox checkbox-success">
                                <asp:ListItem>GEN</asp:ListItem>
                                <asp:ListItem>OBC</asp:ListItem>
                                <asp:ListItem>SC/ST</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>

                    </div>

                    <div class="print-row col-lg-12 txt-upper-alpha" style="padding-bottom:2px;">

                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h5 class="pull-left " style="width:10px;"><b>6.</b></h5>
                            <h5 class="pull-left left-padd-15 ">Contact No. </h5>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h4 class=" dot-box-res2 "><b>
                                <asp:Label ID="lblMobileNo" runat="server"></asp:Label></b></h4>

                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h5 class=" text-left left-padd-25 ">Aadhaar No.</h5>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>



                    </div>

                    <div class="print-row col-lg-12 txt-upper-alpha" style="padding-bottom:2px;">

                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h5 class="pull-left " style="width:10px;"><b>7.</b></h5>
                            <h5 class="pull-left left-padd-15 ">Email ID</h5>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>

                    </div>

                    <div class="print-row col-lg-12 txt-upper-alpha" style="padding-bottom:2px;">

                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                            <h5 class="pull-left " style="width:10px;"><b>8.</b></h5>
                            <h5 class="pull-left left-padd-15 ">Present Address</h5>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>

                    </div>

                    <div class="print-row col-lg-12 " style="padding-bottom:5px;">
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                            <h5 class="pull-left "><b>&nbsp;</b></h5>
                            <h5 class="pull-left left-padd-25 ">&nbsp;</h5>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>
                    </div>

                    <div class="print-row col-lg-12 txt-upper-alpha" style="padding-bottom:2px;">

                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                            <h5 class="pull-left " style="width:10px;"><b>9.</b></h5>
                            <h5 class="pull-left left-padd-15 ">Permanent Address</h5>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>

                    </div>


                    <div class="print-row col-lg-12 txt-upper-alpha" style="padding-bottom:2px;">
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                            <h5 class="pull-left " style="width:10px;"><b>10.</b></h5>
                            <h5 class="pull-left left-padd-15 ">Mark of Identification</h5>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>
                    </div>


                    <div class="print-row col-lg-12 txt-upper-alpha" style="padding-top:10px;">

                        <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                            <h5 class="pull-left " style="width:10px;"><b>11.</b></h5>
                            <h5 class="pull-left left-padd-15"><b>Academic Information </b></h5>
                        </div>
                    </div>
                    <div class=" col-lg-12 txt-upper-alpha table-padd" style="padding-top:20px;">
                        <table class="table table-bordered-black table-des-black txt-upper-alpha text-center">
                            <thead>
                                <tr>
                                    <th class="tab-titel20" style="width:150px;">Qualification</th>
                                    <th class="tab-titel15">Year of Passing</th>
                                    <th class="tab-titel30">Subjects Offered</th>
                                    <th class="tab-titel15">SCHOOL/BOARD/COLLEGE/ UNIV. STATE</th>
                                    <th class="tab-titel20">DIVISION/ CGPA</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <h5 class=" marg-bot-5 ">Secondary</h5>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5 class=" marg-bot-5 ">Sr. Secondary</h5>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5 class=" marg-bot-5 ">Graduation</h5>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5 class=" marg-bot-5 ">Post Graduation<br />
                                            M.Phil/Ph.D. etc.</h5>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5 class=" marg-bot-5 ">Any Other</h5>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>


                    </div>
                </div>
                <div style="page-break-after: always;"></div>
                <div class="panel-body p-panel-body" style="border: 2px solid #000; padding: 10px;">
                    <div class="print-row col-lg-12 txt-upper-alpha">

                        <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                            <h5 class="pull-left "><b>12.</b></h5>
                            <h5 class="pull-left left-padd-15 ">Professional Information </h5>
                        </div>
                    </div>
                    <div class=" col-lg-12 txt-upper-alpha table-padd">
                        <table class="table table-bordered-black table-des-black txt-upper-alpha text-center">
                            <thead>
                                <tr>
                                    <th class="tab-titel20">Qualification</th>
                                    <th class="tab-titel15">Year of Passing</th>
                                    <th class="tab-titel30">Subjects Offered</th>
                                    <th class="tab-titel15">SCHOOL/BOARD/COLLEGE/ UNIV. STATE</th>
                                    <th class="tab-titel20">DIVISION/ CGPA</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <h5 class=" marg-bot-5 ">N.T.T.</h5>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5 class=" marg-bot-5 ">B.Ed.</h5>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5 class=" marg-bot-5 ">M.Ed.</h5>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <h5 class=" marg-bot-5 ">Any Other</h5>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>

                        <br />
                    </div>


                    <div class="print-row col-lg-12 txt-upper-alpha">

                        <div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                            <h5 class="pull-left "><b>13.</b></h5>
                            <h5 class="pull-left left-padd-15 ">DETAILS OF WORK EXPERIENCE<br />
                            <span style="font-size:13px; text-transform:initial;">(Please list your teaching experience, starting with the most recent.)</span>
                                </h5>
                        </div>
                    </div>
                    <%--<div class="col-lg-12 col-md-12 col-sm-12  no-padding">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding txt-upper-alpha">
                            <h5 class="pull-left" style="padding-left:17px;">TOTAL NUMBER OF YEARS EXPERIENCE</h5>
                        </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 p-mgbt-xs-5">
                            <h4 class="dot-box-res2"><b>&nbsp; </b></h4>
                        </div>
                        </div>--%>
                    <div class=" col-lg-12 txt-upper-alpha table-padd">
                        <table class="table table-bordered-black table-des-black txt-upper-alpha text-center">
                            <thead>
                                <tr>
                                    <th class="tab-titel20" style="width: 25%">Name of<br />
                                        School/Institution</th>
                                    <th class="tab-titel15" style="width: 20%">Designation &<br />
                                        Assignments</th>
                                    <th class="tab-titel30" style="width: 15%">From</th>
                                    <th class="tab-titel15" style="width: 15%">To</th>
                                    <th class="tab-titel20" style="width: 25%">Subject/ Classes Taught</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td style="height: 28px;"></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="height: 28px;"></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="height: 28px;"></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="height: 28px;"></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="height: 28px;"></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>


                    </div>
                     <div class="print-row col-lg-12 txt-upper-alpha">

                         <div class="col-lg-12 no-padding">
                             <h5 class="pull-left "><b>14.</b></h5>
                             <h5 class="pull-left left-padd-15">PROFESSIONAL DEVELOPMENT<br />
                                 <span style="font-size:13px; text-transform:initial;">(List any workshop, courses or conference that you have attended.)</span>
                             </h5>
                         </div>
                         <div class=" col-lg-12 no-padding">
                             <h4 class="dot-box-res2 "><b>&nbsp; </b></h4>
                         </div>

                     </div>
                    <div class="print-row  col-lg-12 txt-upper-alpha">

                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                            <h5 class="pull-left "><b>15.</b></h5>
                            <h5 class="pull-left left-padd-15 ">CO CURRICULAR ACTIVITIES</h5>
                        </div>

                        <div class="col-lg-5 col-md-5col-sm-5 col-xs-5 no-padding">
                            <h5 class="pull-left txt-lower-alpha left-padd-15"><b>(a)</b></h5>
                            <h5 class="pull-left left-padd-20 ">Sports, Athletics and NCC</h5>
                        </div>
                        <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding p-mgbt-xs-5">
                            <h4 class="dot-box-res2"><b>&nbsp; </b></h4>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                            <h5 class="pull-left txt-lower-alpha left-padd-15"><b>(b)</b></h5>
                            <h5 class="pull-left left-padd-20 ">Other Interest</h5>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                            <h4 class="dot-box-res2"><b>&nbsp; </b></h4>
                        </div>
                    </div>

                    <div class="print-row col-lg-12 txt-upper-alpha">

                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                            <h5 class="pull-left "><b>16.</b></h5>
                            <h5 class=" text-left" style="padding-left: 37px;">REFERENCE<br />
                                <span style="font-size:13px; text-transform:initial;">(Please provide contact information of your professional reference)</span>
                            </h5>
                            
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h5 class=" text-left" style="padding-left: 50px;">Name</h5>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding p-mgbt-xs-5">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h5 class=" text-left" style="padding-left: 50px;">Position</h5>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding p-mgbt-xs-5">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h5 class=" text-left" style="padding-left: 50px;">Contact No.</h5>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding p-mgbt-xs-5">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                            <h5 class=" text-left" style="padding-left: 50px;">Email </h5>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding p-mgbt-xs-5 ">
                            <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                        </div>
                    </div>

                    <%--<div class="print-row col-lg-12 txt-upper-alpha">

    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
        <h5 class="pull-left "><b>16.</b></h5>
        <h5 class=" text-left" style="padding-left: 50px;">Reference 2</h5>
    </div>
    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding ">
        <h4 class="  "><b>&nbsp; </b></h4>
    </div>
    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
        <h5 class=" text-left" style="padding-left: 70px;">Name</h5>
    </div>
    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding p-mgbt-xs-5">
        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
    </div>
    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
        <h5 class=" text-left" style="padding-left: 70px;">Position</h5>
    </div>
    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding p-mgbt-xs-5">
        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
    </div>
    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
        <h5 class=" text-left" style="padding-left: 70px;">Contact No.</h5>
    </div>
    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding p-mgbt-xs-5">
        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
    </div>
    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
        <h5 class=" text-left" style="padding-left: 70px;">Email </h5>
    </div>
    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding p-mgbt-xs-5 ">
        <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
    </div>
</div>--%>

                    <div class="print-row col-lg-12 txt-upper-alpha">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                <h5 class="pull-left "><b>17.</b></h5>
                                <h5 class="pull-left left-padd-15 ">Your last salary</h5>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                <h4 class="dot-box-res2"><b>&nbsp; </b></h4>
                            </div>
                            <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding">
                                <h5 class="pull-left "><b>18.</b></h5>
                                <h5 class="pull-left left-padd-15 ">If selected, then minimum salary expected</h5>
                            </div>
                            <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                                <h4 class="dot-box-res2"><b>&nbsp; </b></h4>
                            </div>
                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding">
                                <h5 class="pull-left "><b>19.</b></h5>
                                <h5 class="pull-left left-padd-15 ">If selected, how much time would you need to join?</h5>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                <h4 class="dot-box-res2"><b>&nbsp; </b></h4>
                            </div>

                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
    <h5 class="pull-left "><b>20.</b></h5>
    <h5 class="pull-left left-padd-15 ">Mention any course/ Studies you are pursuing at present. Will you need any<br />
        leave on this account?</h5>
</div>
<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
    <h4 class="dot-box-res2"><b>&nbsp; </b></h4>
</div>
                        </div>
                    </div>
                </div>
                <div style="page-break-after: always;"></div>
                <div class="panel-body p-panel-body" style="border: 2px solid #000; padding: 10px;">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding  txt-upper-alpha">

                        
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                            <h5 class="pull-left "><b>21.</b></h5>
                            <h5 class="pull-left left-padd-15 ">Required Joining Time and Date</h5>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding">
                            <h4 class="dot-box-res2"><b>&nbsp; </b></h4>
                        </div>
                        <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding">
                            <h5 class="pull-left "><b>22.</b></h5>
                            <h5 class="pull-left left-padd-15 ">Reason for leaving the last institution</h5>
                        </div>
                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                            <h4 class="dot-box-res2"><b>&nbsp; </b></h4>
                        </div>
                         <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 no-padding">
                            <h5 class="pull-left "><b>23.</b></h5>
                            <h5 class="pull-left left-padd-15 ">Reason for joining this institution</h5>
                        </div>
                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 no-padding">
                            <h4 class="dot-box-res2"><b>&nbsp; </b></h4>
                        </div>
                    </div>

                    <div class="print-row col-lg-12 ">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                            <br /><br />
                            <h5 class="text-center"><b>DECLARATION</b></h5>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                            <h4 style="line-height: 22px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;I hereby certify that all statements made, and information given by me in this application form are true, complete and correct to the best of my knowledge and belief. In the event of any
information or part of it being found false or incorrect before or after the interview or appointment. Action can be taken against me by the school and my candidature appointment shall automatically stand cancelled/ terminated.</h4>
                        </div>

                    </div>

                    <div class="print-row col-lg-12 txt-upper-alpha marg-top-30">

                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding ">
                            <h5 class="pull-left text-left">Place :</h5>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding ">
                            <h4 class=""><b>&nbsp; </b></h4>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding ">
                            <h5 class="pull-left text-left">Date :</h5>
                        </div>

                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 no-padding ">
                            <h5 class="pull-right text-right  left-padd-25 ">(Signature of the Applicant)</h5>
                        </div>

                    </div>
                    <div class="print-row col-lg-12 txt-upper-alpha">

                        <div class=" col-lg-12 no-padding ">
                            <br />
                            <h4 class="pull-left text-left ">Note: Attach all mark sheet & certificates.</h4>
                        </div>


                    </div>
                    <div class="print-row col-lg-12 ">
                        <br />
                        <br />
                        <br />
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                            <div class="main-titel-box text-center">
                                <h3 class="form-name" style="font-size: 14px;"><span>FOR OFFICE USE ONLY</span></h3>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                <h5 class="pull-left"><b>Short listed for interview on</b></h5>
                            </div>
                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding p-mgbt-xs-5">
                                <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                <h5 class="pull-left"><b>Remarks </b></h5>
                            </div>
                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 no-padding p-mgbt-xs-5">
                                <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                <h5 class="pull-left left-padd-25 "><b></b></h5>
                            </div>
                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 no-padding p-mgbt-xs-5">
                                <h4 class=" dot-box-res2 "><b>&nbsp; </b></h4>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 no-padding">
                                <h5 class="pull-left">
                                    <br />
                                     <br />
                                    <b>PRINCIPAL </b>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </h5>

                            </div>
                            <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 no-padding p-mgbt-xs-5">
                                <h4 class=""><b>&nbsp; </b></h4>
                            </div>
                        </div>

                    </div>

                </div>
                <!-- panel-body -->
                <br />
            </div>
        </div>
        <!-- Panel Widget -->
    </div>


</asp:Content>

