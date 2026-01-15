<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ProvisionalFormPrint.aspx.cs" Inherits="_2.ProvisionalFormPrint" %>

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
                                <div class="panel-body p-panel-body" style="border:2px solid #000;">
                                <style>
                                    .icon-checkmark{left:-2px;position: relative;top: -2px;}
                                    @media print {
                                        h1, h2, h3, h4, h5, h6 {
                                            margin: 0 0 7px !important;
                                            font-weight: 600 !important;
                                            font-family: unset !important;
                                            color: inherit;
                                            text-rendering: optimizelegibility;
                                            font-size: 13px !important;

                                        }

                                        .sr-no {
                                            /* border-bottom: 2px dotted rgb(128, 128, 128); */
                                            font-size: 20px;
                                            font-weight: 600;
                                            padding: 0px;
                                            margin: -4px 0px 0px 5px;
                                        }
                                        .dot-box-res2 {
                                            border-bottom: 0.5px dotted rgb(128, 128, 128);
                                            width: 100%;
                                            margin-left: 5px;
                                            margin-bottom: 3px !important;
                                        }
                                       
                                    }

                                </style>
                                <div class="print-row col-lg-12">
                                    <table class="table">
                                        <tr>
                                            <td style="padding: 0 !important;">
                                                <div class="" id="header" runat="server"></div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                    
                                <div class="print-row col-lg-12  mgbt-xs-15 p-mgbt-xs-5">
                                    <%--<div class=" text-left col-sm-5 no-padding">
                                        <h5 class="pull-left text-left ">S. No. ..........................</h5>
                                        <h4 class="pull-left text-left sr-no">
                                            </h4>

                                    </div>--%>
                                   
                                    <div class="col-sm-12 row no-padding " style="text-align: center !important;">
                                        <style>
                                            .title{
                                                display: flex;
                                                justify-content: center;
                                                align-items: center;
                                            }
                                        </style>
                                        <div class="title main-titel-box" style="text-align:center;display:flex;text-decoration:underline;justify-content:center;align-items:center;">
                                            <h3 style="text-transform: uppercase;"><span style="font-size: 20px !important;font-weight: 600;">Provisional Form</span></h3>                                            
                                            <h3 style="font-size: 20px !important;text-transform: uppercase;font-weight: 600;"> 
                                            (<asp:Label ID="lblSessionFor" runat="server" Text="Label" Style="text-transform: uppercase;font-size: 20px !important;    font-weight: 600;"></asp:Label>)</h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="print-row col-lg-12  mgbt-xs-15 p-mgbt-xs-5" style="padding-top: 0px;">
                                    <div class=" text-left col-lg-10 col-md-10 col-xs-10 col-sm-10 no-padding">
                                        <h5 class="pull-left text-left "  style="display: flex;margin-bottom:0px;">
                                            Admission No. &nbsp;<span style="display: flex;flex-direction: column;">
                                                <asp:Label ID="lblSrno" runat="server" Font-Bold="true"></asp:Label></span></h5>
                                    </div>
                                </div>
                                <div class="print-row col-lg-12" style="padding-top: 0px;">

                                    <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 no-padding">
                                        <h5 class="pull-left text-left ">CLASS to which admission sought : </h5>
                                        <h4 class="pull-left text-left sr-no">
                                            <asp:Label ID="lblClass" runat="server" Style="text-transform: uppercase"></asp:Label></h4>
                                    </div>
                                </div>
                                   
                                <div class="print-row col-lg-12" style="padding-top: 0px; position:relative;">
                                    <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12 no-padding">
                                        <h5 class="pull-left text-left "><b>PERSONAL DETAILS:-</b></h5>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12 no-padding">
                                        <h5 class="pull-left text-left "><b>1. &nbsp;Name &nbsp;</b>
                                            <asp:Label ID="lblStudentName" runat="server" CssClass="sr-no" Style="text-transform: uppercase"></asp:Label></h5>
                                    </div>
                                    <div style="position:absolute; right:15px;">
                                        <asp:Image runat="server" ID="photo" CssClass="img-pass-box text-center" Style="width: 78px; height: 98px;" />
                                    </div>
                                </div>


                                <div id="sexdiv" class="print-row col-lg-12" style="padding-top: 10px;">
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h5 class="pull-left text-left "><b>2.</b></h5>
                                        <div class="pull-left text-left left-padd-15">
                                            <h5 class="text-left ">Gender : </h5>
                                        </div>
                                    </div>

                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h5 class=" text-left ">Male <label class="sex-check-box5 male" id="span1" runat="server" style="text-transform: uppercase; width: 20px; height: 20px; padding: 5px 5px;"><i class="icon-checkmark" id="male" runat="server"></i></label></h5>
                                    </div>

                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h5 class=" text-left ">Female <label class="sex-check-box5 female" id="span2" runat="server" style="text-transform: uppercase; width: 20px; height: 20px; padding: 5px 5px;"><i class="sex-check-box-blank" id="female" runat="server"></i></label></h5>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h5 class=" text-left ">Any other <label class="sex-check-box5 Anyother" id="span3" runat="server" style="text-transform: uppercase; width: 20px; height: 20px; padding: 5px 5px;"><i class="sex-check-box-blank" id="Anyother" runat="server"></i></label></h5>
                                    </div>
                                </div>

                                <div class="print-row col-lg-12" style="padding-top: 0px;">

                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h5 class="pull-left text-left "><b>3.</b></h5>
                                        <div class="pull-left text-left left-padd-15">
                                            <h5 class="text-left ">Date of Birth :</h5>
                                        </div>
                                    </div>

                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10  no-padding">
                                        <h5 class="pull-left text-left ">Date <asp:TextBox runat="server" ID="dd" class="blank-box-f" Font-Bold="true" style="width: 50px; height: 20px; padding: 5px 5px;">&nbsp; </asp:TextBox></h5>
                                        <h5 class="pull-left text-left ">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Month <asp:TextBox runat="server" ID="mm" Font-Bold="true" class="blank-box-f" style="width: 55px; height: 20px; padding: 5px 5px;">&nbsp; </asp:TextBox></h5>
                                        <h5 class="pull-left text-left ">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Year <asp:TextBox runat="server" ID="yy" class="blank-box-f" Font-Bold="true" style="width: 80px; height: 20px; padding: 5px 5px;">&nbsp; </asp:TextBox></h5>
                                    </div>
                                </div>

                                <div class="print-row col-lg-12" style="padding-top: 10px;">

                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 no-padding">
                                        <h5 class=" text-left left-padd-20 ">In words</h5>
                                    </div>
                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 no-padding">
                                        <h4 class=" dot-box-res2 p-box-mar-tb"><asp:Label ID="inword" runat="server"  Font-Bold="true"></asp:Label><b>&nbsp; </b></h4>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 20px;">
                                        <h5 class=" text-left">(Attach Birth Certificate issued by the Competent Authority)</h5>
                                    </div>
                                </div>
                                <div class="print-row col-lg-12" style="padding-top: 10px;">

                                    <div class="col-lg-12 no-padding">
                                        <h5 class="pull-left "><b>4.</b></h5>
                                        <h5 class="pull-left text-left left-padd-15 "><b>Details of Parents : </b></h5>
                                    </div>
                                    <div class="col-lg-12 no-padding">
                                        <%-- <table class="table table-bordered table-des">--%>
                                        <table class="table p-table-bordered" style="    margin-bottom: 5px;">
                                            <thead>
                                                <tr>
                                                    <td class="p-pad-2 text-center">
                                                        <h5><strong>Details</strong></h5>
                                                    </td>
                                                    <td class="p-pad-2 text-center">
                                                        <h5><strong>Mother</strong></h5>
                                                    </td>
                                                    <td class="p-pad-2 text-center">
                                                        <h5><strong>Father/ Guardian</strong></h5>
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="p-pad-2" style="width: 22%;">
                                                        <h5 class="pull-left marg-bot-5"><b>(i)</b></h5>
                                                        <h5 class=" text-left marg-bot-5 left-padd-25 ">Name</h5>
                                                    </td>
                                                    <td class="p-pad-2" style="width: 39%;"><asp:Label ID="mother" runat="server"  Font-Bold="true"></asp:Label></td>
                                                    <td class="p-pad-2" style="width: 39%;"><asp:Label ID="father" runat="server"  Font-Bold="true"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="p-pad-2" style="width: 22%;">
                                                        <h5 class="pull-left marg-bot-5"><b>(ii)</b></h5>
                                                        <h5 class=" text-left marg-bot-5  left-padd-25 ">Educational Qualification</h5>

                                                    </td>
                                                    <td class="p-pad-2" style="width: 39%;"><asp:Label ID="motherQuli" runat="server"  Font-Bold="true"></asp:Label></td>
                                                    <td class="p-pad-2" style="width: 39%;"><asp:Label ID="fatherQuli" runat="server"  Font-Bold="true"></asp:Label></td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="p-pad-2" style="width: 22%;">
                                                        <h5 class="pull-left marg-bot-5"><b>(iii)</b></h5>
                                                        <h5 class=" text-left marg-bot-5 left-padd-25 ">Mobile No.</h5>
                                                    </td>
                                                    <td class="p-pad-2" style="width: 39%;"><asp:Label ID="mothermobile" runat="server"  Font-Bold="true"></asp:Label></td>
                                                    <td class="p-pad-2" style="width: 39%;"><asp:Label ID="fathermobile" runat="server"  Font-Bold="true"></asp:Label></td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="p-pad-2" style="width: 22%;">
                                                        <h5 class="pull-left marg-bot-5"><b>(iv)</b></h5>
                                                        <h5 class=" text-left marg-bot-5  left-padd-25 ">Occupation</h5>
                                                    </td>
                                                    <td class="p-pad-2" style="width: 39%;"><asp:Label ID="MotherOcu" runat="server"  Font-Bold="true"></asp:Label></td>
                                                    <td class="p-pad-2" style="width: 39%;"><asp:Label ID="fatherOcu" runat="server"  Font-Bold="true"></asp:Label></td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="p-pad-2" style="width: 22%;">
                                                        <h5 class="pull-left marg-bot-5"><b>(v)</b></h5>
                                                        <h5 class=" text-left marg-bot-5 left-padd-25 ">Annual Income</h5>
                                                    </td>
                                                    <td class="p-pad-2" style="width: 39%;"><asp:Label ID="motherincome" runat="server"  Font-Bold="true"></asp:Label></td>
                                                    <td class="p-pad-2" style="width: 39%;"><asp:Label ID="fatherincome" runat="server"  Font-Bold="true"></asp:Label></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>


                                </div>
                                

                                <div class="print-row col-lg-12" style="padding-top: 10px;">

                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h5 class="pull-left "><b>5.</b></h5>
                                        <h5 class=" left-padd-20 marg-bot-5">Category: (Attach proof)</h5>
                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                        <h4 class="dot-box-res2 p-box-mar-tb "><asp:Label ID="category" runat="server"  Font-Bold="true"></asp:Label><b>&nbsp; </b></h4>
                                    </div>
                                </div>
                                <div class="print-row col-lg-12">
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h5 class="pull-left "><b>6.</b></h5>
                                        <h5 class=" text-left left-padd-20 ">Aadhaar No. (Attach proof)</h5>
                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                        <h4 class="dot-box-res2 p-box-mar-tb "><asp:Label ID="aadhar" runat="server"  Font-Bold="true"></asp:Label><b>&nbsp; </b></h4>
                                    </div>
                                </div>
                                
                                <div class="print-row col-lg-12" style="padding-top: 20px;">
                                     <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-padding">
                                        <h5 class="pull-left text-left"><b>7.</b> Contact No. of local guardian</h5>
                                    </div>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                        <h4 class="dot-box-res2 p-box-mar-tb "><asp:Label ID="guardianContact" runat="server"  Font-Bold="true"></asp:Label><b>&nbsp; </b></h4>
                                    </div>
                                </div>


                                <div class="print-row col-lg-12" style="padding-top: 20px;">

                                    <div class="col-lg-12 no-padding">
                                        <h5 class="pull-left "><b>8.</b></h5>
                                        <h5 class="pull-left text-left left-padd-15 ">Details of siblings (if any)</h5>
                                    </div>
                                    <div class="col-lg-12 no-padding">
                                        <%-- <table class="table table-bordered table-des">--%>
                                        <table class="table no-bm  p-table-bordered table-bordered table-des">
                                            <thead>
                                                <tr style="height: 25px;">
                                                    <td class="tab-titel1 p-pad-2 tab-titel50" style="width: 25% !important;">
                                                        <h5><strong>Name</strong></h5>
                                                    </td>
                                                    <td class="tab-titel2 p-pad-2 tab-titel25" style="width: 25% !important;">
                                                        <h5><strong>Brother/Sister</strong></h5>
                                                    </td>
                                                    <td class="tab-titel3 p-pad-2 tab-titel25" style="width: 10% !important;">
                                                        <h5><strong>Age</strong></h5>
                                                    </td>
                                                    <td class="tab-titel3 p-pad-2 tab-titel25" style="width: 20% !important;">
                                                        <h5><strong>Class studying in</strong></h5>
                                                    </td>
                                                    <td class="tab-titel3 p-pad-2 tab-titel25" style="width: 20% !important;">
                                                        <h5><strong>School studying in</strong></h5>
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr style="height: 25px;">
                                                    <td class="p-pad-2"></td>
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
                                                    <td class="p-pad-2"></td>
                                                </tr>
                                                <tr style="height: 25px;">
                                                    <td class="p-pad-2"></td>
                                                    <td class="p-pad-2"></td>
                                                    <td class="p-pad-2"></td>
                                                    <td class="p-pad-2"></td>
                                                    <td class="p-pad-2"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>


                                </div>

                               

                                <div class="print-row col-lg-12" style="padding-top:10px;padding-bottom:30px;">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                        <br />
                                        <h5 class="text-center"><b>DECLARATION</b></h5>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                        <h5 class="text-left" style="line-height:26px;">
                                            <b>I hereby declare that the above information including Name of the Candidate, Date of Birth, Father’s/ Guardian’s
                                            Name, Mother’s Name furnished by me is correct to the best of my knowledge & belief. I shall abide by the rules of
                                            the School.</b>
                                        </h5>
                                    </div>
                                </div>

                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding">
                                    <div class="col-lg-4 col-md-4 col-sm-41 col-xs-4 no-padding">
                                        <h5 class="pull-left text-left"><b>Date</b></h5>
                                    </div>                                  
                                   
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-padding">
                                        <h5 class="pull-right text-right"><b>Signature of the Parent(s)/ Guardian </b></h5>
                                    </div>
                                </div>

                                
                                
                                <div class="print-row col-lg-12" style="padding-top: 50px; padding-left: 20px;">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-left text-left  "><b>Date</b> :..........................</h5>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6  no-padding ">
                                        <h5 class="pull-right text-right"><b>Sign. of Principal & Official Seal</b></h5>
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
