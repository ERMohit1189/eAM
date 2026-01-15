<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Visitorpageprint.aspx.cs" Inherits="admin.AdminDefault3" %>


<%@ Register Src="~/admin/usercontrol/visitor_gatepass.ascx" TagPrefix="uc1" TagName="visitor_gatepass" %>




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
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12  no-padding">
                            <div class="col-sm-12 no-padding text-right menu-action">
                                <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="Go back to Check In page" NavigateUrl="Visitorpage.aspx" ForeColor="#CC0000" CssClass="btn-print-box"><i class="fa fa-reply"></i></asp:HyperLink>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-print-box"
                                    title="Print Gate Pass" data-placement="left"><i class="icon-printer"></i></asp:LinkButton>
                            </div>
                            <div class="col-md-10  mgbt-xs-15">

                                <div class="print-container" id="abc" runat="server">
                                    <div class=" print-row fee-d-box-nhl" style="    height: 410px;">

                                        <div class="col-md-12 col-sm-12  border-b">
                                            
                                                <uc1:visitor_gatepass runat="server" ID="visitor_gatepass" />
                                           
                                            <%--<div class="col-md-4 col-sm-4 " style="padding: 0px;" >
                                                <h3 style="text-align: right; font-size: 18px;font-family: arial; font-weight: 500; letter-spacing: 1px; margin: 7px 0px 0 0;">VISITOR GATE PASS</h3>
                                                 <p style="    font-size: 12px; text-align:right">(Origional)</p>
                                            </div>--%>

                                        </div>
                                        <%-- <div class="  no-padding print-row">
                                            <div class="  col-sm-12 text-left" style="padding: 0px 15px; margin: 0px;">
                                                <div class="col-md-6 col-sm-6 col-xs-6 txt-middle">
                                                   
                                                </div>

                                            </div>
                                        </div>--%>
                                        <div class="  col-sm-12 mgbt-xs-12 p-mgbt-xs-12" style="padding: 0px 10px; margin: 0px; text-transform: uppercase;">
                                            <div class="col-lg-8 col-md-8 col-xs-8 col-sm-8" style="padding: 0px 0px; margin: 0px;">
                                                <table class="table table-hover no-bm no-head-border">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 27%;">
                                                                <h4 class="pass-input">Gate Pass No. </h4>

                                                            </td>
                                                            <td style="width: 73%;">:&nbsp;
                                                               <asp:Label runat="server" ID="Label1" CssClass="font-bold"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 27%;">
                                                                <h4 class="pass-input">Visitor's Name
                                                                </h4>
                                                            </td>
                                                            <td style="width: 73%;">:&nbsp;
                                                               <asp:Label runat="server" ID="Label2" CssClass="font-bold"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 27%;">
                                                                <h4 class=" pass-input">Contact No. </h4>
                                                            </td>
                                                            <td style="width: 73%;">:&nbsp;
                                                             <asp:Label runat="server" ID="Label3" CssClass="font-bold"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 27%;">
                                                                <h4 class=" pass-input">Purpose of visit</h4>
                                                            </td>
                                                            <td style="width: 73%;">:&nbsp;
                                                            <asp:Label runat="server" ID="Label4" CssClass="font-bold"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 27%;">
                                                                <h4 class=" pass-input">Whom to meet</h4>
                                                            </td>
                                                            <td style="width: 73%;">:&nbsp;
                                                            <asp:Label runat="server" ID="Label5" CssClass="font-bold"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 27%;">
                                                                <h4 class=" pass-input">Address</h4>
                                                            </td>
                                                            <td style="width: 73%;">:&nbsp;
                                                             <asp:Label runat="server" ID="Label8" CssClass="font-bold"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="width: 27%;">
                                                                <h4 class=" pass-input">Email </h4>
                                                            </td>
                                                            <td style="width: 73%;">:&nbsp;
                                                            <asp:Label runat="server" ID="Label7" CssClass="font-bold"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 27%;">
                                                                <h4 class=" pass-input">Gender</h4>
                                                            </td>
                                                            <td style="width: 73%;">:&nbsp;
                                                             <asp:Label runat="server" ID="Label6" CssClass="font-bold"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        
                                                    <tr>
                                                        <td style="width: 27%;">
                                                            <h4 class=" pass-input">Visitor's Card No.</h4>
                                                        </td>
                                                        <td style="width: 73%;">:&nbsp;
                                                            <asp:Label runat="server" ID="cardno" CssClass="font-bold"></asp:Label>
                                                        </td>
                                                    </tr>

                                                        <tr>
                                                            <td style="width: 27%;">
                                                                <h4 class="pass-input" style="float: left">Signature<br />
                                                                    <p style="margin: 0px -3px;">[whom to meet]</p></h4>
                                                            </td>
                                                            <td style="width: 73%;">:&nbsp;

                                                            </td>
                                                        </tr>

                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-xs-4 col-sm-4" style="padding: 0px 0px; margin: 0px;">
                                                <table class="table table-hover no-bm no-head-border">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                               <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding">
                                                                    <h4 class="pass-input" style="text-align: left; float: left; font-size:10px !important;margin-top: 4px;">In Time  </h4>
                                                                </div>
                                                              <div class="col-lg-9 col-md-9 col-xs-9 col-sm-9 no-padding text-right"> : &nbsp;
                                                                     <asp:Label runat="server" ID="lbldate" CssClass="font-bold" Font-Size="10px"></asp:Label>
                                                                    <asp:Label runat="server" ID="lbltime" CssClass="font-bold" Font-Size="10px"></asp:Label>
                                                                </div>
                                                                 
                                                        
                                                              
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 130px">
                                                                <div class="visitor-pic-box" style="float: right;">
                                                                    <asp:Image ID="Image3" runat="server"
                                                                        ImageUrl='../images/clients/visitor.jpg' />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td >
                                                                <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3 no-padding">
                                                                     <h4 class="pass-input" style="text-align: left; font-size: 10px !important;margin-top: 4px;">Out Time</h4>
                                                                    </div>
                                                                <div class="col-lg-9 col-md-9 col-xs-9 col-sm-9 no-padding text-right"> : &nbsp;
                                                                    <asp:Label runat="server" ID="Label9" CssClass="font-bold" Font-Size="10px"></asp:Label>
                                                                    </div>
                                                                 

                                                            </td>
                                                        </tr>



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
                </div>
            </div>
        </div>
   
</asp:Content>

