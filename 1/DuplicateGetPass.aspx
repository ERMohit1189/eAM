<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DuplicateGetPass.aspx.cs" Inherits="_1.AdminDuplicateGetPass" %>

<%@ Register Src="~/admin/usercontrol/gatepassDupli.ascx" TagPrefix="uc1" TagName="gatepass" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12  no-padding">
                            <div class="col-md-10  mgbt-xs-15" id="Div2" runat="server">
                                <div class="print-container" id="abc2" runat="server">
                                    <div class="gate_head_box  print-row fee-d-box-nhl">
                                        <div class="print-row  border-b ">
                                            <uc1:gatepass runat="server" ID="gatepass1" />
                                        </div>

                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <div class="  no-padding print-row">
                                                    <div class="  no-padding print-row">
                                                        <div class=" col-xs-6 col-sm-6 text-left">
                                                            <h4 class="font-bold head-add-input print-text marg-tb-5">Gate Pass No. :
                                                                <asp:Label ID="Label25" runat="server" Text='<%# Eval("Maxid")%>'></asp:Label></h4>
                                                        </div>
                                                        <div class=" col-xs-6 col-sm-6 text-right">
                                                            <h4 class="font-bold head-add-input print-text marg-tb-5 ">Date :
                                                                <asp:Label ID="Label26" runat="server" Text='<%# Bind("Date")%>'></asp:Label>
                                                            </h4>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-lg-12 col-md-12 col-sm-12  no-mgpd">
                                                    <div class=" col-xs-6 col-sm-6 mgbt-xs-10 p-mgbt-xs-10">
                                                        <fieldset>
                                                            <legend class="legend-me">Student's Details
                                                            </legend>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-mgpd">
                                                                <div class="table-pic-box3" style="margin-top: 6px;">
                                                                    <asp:Image ID="Image3" runat="server" ImageUrl='<%# ResolveClientUrl(Eval("StudentPhotopath").ToString()!=""? Eval("StudentPhotopath").ToString():"../img/user-pic/user-pic.jpg") %>' />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-mgpd">
                                                                <table class="table table-hover no-bm no-head-border print-table-text">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">S.R. No. :
                                                        <asp:Label ID="Label28" runat="server" Text='<%# Eval("srno")%>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Name :
                                                        <asp:Label ID="Label29" runat="server" Text='<%# Eval("StudentName")%>'></asp:Label></h4>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Father's Name :
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Eval("FatherName")%>'></asp:Label></h4>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Class :
                                                        <asp:Label ID="Label31" runat="server" Text='<%# Eval("ClassName")%>'></asp:Label>&nbsp;<asp:Label ID="Label23" runat="server" Text='<%# Eval("SectionName")%>'></asp:Label></h4>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Contact No. :
                                                        <asp:Label ID="Label32" runat="server" Text='<%# Eval("FamilyContactNo")%>'></asp:Label></h4>

                                                                            </td>
                                                                        </tr>


                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </fieldset>
                                                    </div>

                                                    <div class="col-xs-6 col-sm-6 mgbt-xs-10 p-mgbt-xs-10 ">
                                                        <fieldset>
                                                            <legend class="legend-me">Visitor's  Details
                                                            </legend>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-mgpd">
                                                                <div class="table-pic-box3" style="margin-top: 6px;">
                                                                    <asp:Image ID="Image4" runat="server"
                                                                        ImageUrl='<%# ResolveClientUrl(Eval("GuardianPhotoPath").ToString()!=""? Eval("GuardianPhotoPath").ToString():"../img/user-pic/user-pic.jpg") %>' />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-mgpd">
                                                                <table class="table table-hover no-bm no-head-border print-table-text">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Name :
                                                                            <asp:Label ID="Label27" runat="server" Text='<%# Eval("GuardianName")%>'></asp:Label></h4>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Contact No. :
                                                                            <asp:Label ID="Label36" runat="server" Text='<%# Eval("GuardionContact")%>'></asp:Label></h4>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Relation :
                                                                            <asp:Label ID="Label34" runat="server" Text='<%# Eval("Relation")%>'></asp:Label></h4>
                                                                            </td>
                                                                        </tr>


                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-sm-12  ">
                                                        <h4 class="font-bold pass-input">Reason :
                                                            <asp:Label ID="LaLabel33bel2" runat="server" Text='<%# Eval("Reason")%>'></asp:Label></h4>
                                                    </div>
                                                </div>

                                                <div class="gate-pass-cut marg-top-20">

                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left sign-size ">
                                                        <h4 class="font-bold  print-text" style="padding-left: 0;">Office In-charge's Signature</h4>
                                                        <%--  <h4 class=" dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-center  sign-size ">
                                                        <h4 class="font-bold  print-text">Principal's Signature</h4>
                                                        <%-- <h4 class="dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-right sign-size ">

                                                        <h4 class="font-bold  print-text">Visitor's Signature</h4>
                                                        <%--   <h4 class="dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                    </div>
                                                </div>
                                                <div class="  no-padding print-row">
                                                    <table style="width: 100%;">
                                                        <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px; padding: 0 15px;">Generated by eAM&reg; | Submitted by
                                                              <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("LoginName") %>'></asp:Label>&nbsp;on 
                                                              <asp:Label ID="lblFooterDate" runat="server" Text='<%# Bind("Date")%>'></asp:Label>
                                                        </td>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>


                                    <div class="gate-pass-cut col-lg-12 no-padding print-row">
                                        <div class="  ">
                                            <div class="cut-line-box">
                                                <h4><i class="fa fa-scissors"></i></h4>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="gate_head_box  print-row fee-d-box-nhl">
                                        <div class="print-row  border-b ">
                                            <uc1:gatepass runat="server" ID="gatepass" />
                                        </div>
                                        <asp:Repeater ID="Repeater2" runat="server">
                                            <ItemTemplate>
                                                <div class="  no-padding print-row">
                                                    <div class="  no-padding print-row">
                                                        <div class=" col-xs-6 col-sm-6 text-left">
                                                            <h4 class="font-bold head-add-input print-text marg-tb-5">Gate Pass No. :
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Maxid")%>'></asp:Label></h4>
                                                        </div>
                                                        <div class=" col-xs-6 col-sm-6 text-right">
                                                            <h4 class="font-bold head-add-input print-text marg-tb-5 ">Date :
                                                                <asp:Label ID="Label26" runat="server" Text='<%# Bind("Date")%>'></asp:Label>
                                                            </h4>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%--<div class="  no-padding print-row">
                                                    <div class="  col-sm-12 text-left">
                                                        
                                                        <div class="col-md-3 col-sm-3 col-xs-3 txt-middle">
                                                            <h4 class="font-bold pass-input display-inline">Class :
                                                       
                                                        </div>


                                                    </div>
                                                    <%-- <div class=" col-xs-6 col-sm-6 text-right">
                                                        <h4 class="font-bold head-add-input marg-tb-5 ">Date :
                                                                
                                                        </h4>
                                                    </div>
                                                </div>--%>
                                                <div class="col-lg-12 col-md-12 col-sm-12  no-mgpd">
                                                    <div class=" col-xs-6 col-sm-6 mgbt-xs-10 p-mgbt-xs-10">
                                                        <fieldset>
                                                            <legend class="legend-me">Student's Details
                                                            </legend>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-mgpd">
                                                                <div class="table-pic-box3" style="margin-top: 6px;">
                                                                    <asp:Image ID="Image6" runat="server"
                                                                        ImageUrl='<%# ResolveClientUrl(Eval("StudentPhotopath").ToString()!=""?
                                                                                    Eval("StudentPhotopath").ToString():"../img/user-pic/user-pic.jpg") %>' />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-mgpd">
                                                                <table class="table table-hover no-bm no-head-border print-table-text">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">S.R. No. :
                                                        <asp:Label ID="Label28" runat="server" Text='<%# Eval("srno")%>'></asp:Label></h4>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Name :
                                                        <asp:Label ID="Label29" runat="server" Text='<%# Eval("StudentName")%>'></asp:Label></h4>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Father's Name :
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Eval("FatherName")%>'></asp:Label></h4>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Class :
                                                         <asp:Label ID="Label31" runat="server" Text='<%# Eval("ClassName")%>'></asp:Label>&nbsp;<asp:Label ID="Label23" runat="server" Text='<%# Eval("SectionName")%>'></asp:Label></h4>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Contact No. :
                                                        <asp:Label ID="Label32" runat="server" Text='<%# Eval("FamilyContactNo")%>'></asp:Label></h4>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                    <div class=" col-xs-6 col-sm-6 mgbt-xs-10 p-mgbt-xs-10">
                                                        <fieldset>
                                                            <legend class="legend-me">Visitor's Details
                                                            </legend>
                                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 no-mgpd">
                                                                <div class="table-pic-box3" style="margin-top: 6px;">
                                                                    <asp:Image ID="Image7" runat="server"
                                                                                        ImageUrl='<%# ResolveClientUrl(Eval("GuardianPhotoPath").ToString()!=""? Eval("GuardianPhotoPath").ToString():"../img/user-pic/user-pic.jpg") %>' />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 no-mgpd">
                                                                <table class="table table-hover no-bm no-head-border print-table-text">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Name :
                                                        <asp:Label ID="Label27" runat="server" Text='<%# Eval("GuardianName")%>'></asp:Label></h4>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Contact No. :
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Eval("GuardionContact")%>'></asp:Label></h4>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <h4 class="font-bold pass-input">Relation :
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Eval("Relation")%>'></asp:Label></h4>

                                                                            </td>
                                                                        </tr>



                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-sm-12  ">
                                                        <h4 class="font-bold pass-input">Reason :
                                                           <asp:Label ID="Label33" runat="server" Text='<%# Eval("Reason")%>'></asp:Label></h4>
                                                    </div>
                                                </div>

                                                 <div class="gate-pass-cut marg-top-20">

                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-left sign-size ">
                                                        <h4 class="font-bold  print-text" style="padding-left: 0;">Office In-charge's Signature</h4>
                                                        <%--  <h4 class=" dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-center  sign-size ">
                                                        <h4 class="font-bold  print-text">Principal's Signature</h4>
                                                        <%-- <h4 class="dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-right sign-size ">

                                                        <h4 class="font-bold  print-text">Visitor's Signature</h4>
                                                        <%--   <h4 class="dot-box-res3-l"><b>&nbsp; </b></h4>--%>
                                                    </div>
                                                </div>
                                                <div class="  no-padding print-row">
                                                    <table style="width:100%">
                                                        <td colspan="2" class="text-right" style="font-family: Courier New; font-size: 11px; padding: 0 15px;">Generated by eAM&reg; | Submitted by
                                                              <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("LoginName") %>'></asp:Label>&nbsp;on 
                                                                  <asp:Label ID="lblFooterDate" runat="server" Text='<%# Bind("Date")%>'></asp:Label>
                                                        </td>
                                                    </table>
                                                </div>

                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </div>


                                </div>
                            </div>

                            <%--  <div class="col-md-2  mgbt-xs-15">
                                <asp:LinkButton ID="lnkPrint" runat="server" class="button form-control-blue" type="button" OnClick="lnkPrint_Click"><i class="fa fa-print append-icon"></i>Print</asp:LinkButton>
                            </div>--%>

                            <div class="col-md-2  mgbt-xs-15">

                                <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn-print-box" PostBackUrl="~/1/GatePassReport.aspx" Style="color: #CC0000"
                                    title="Go back to GatePass Report" data-placement="bottom"><i class="fa fa-reply"></i></asp:LinkButton>
                                &nbsp;
                            <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click" CssClass="btn-print-box"
                                title="Print GatePass" data-placement="bottom"><i class="icon-printer"></i></asp:LinkButton>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

