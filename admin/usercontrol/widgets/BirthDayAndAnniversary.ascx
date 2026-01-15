<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BirthDayAndAnniversary.ascx.cs" Inherits="admin_usercontrol_widgets_Wid2" %>
<div class="col-md-4  mgbt-xs-10 dash-b-w">
   <div class="panel widget panel-bd-top vd_bdt-blue  vd_todo-widget light-widget">
        <div class="panel-heading no-title ">
            <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-address-book"></i></span><a href="javascript:">Admissions at a Glance</a></h3>
            
            <div class="vd_panel-menu">
                <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                <div data-action="refresh" title="Refresh" data-placement="bottom" class=" menu entypo-icon smaller-font"><i class="icon-cycle"></i></div>
                <div data-action="close" title="Close" data-placement="bottom" class=" menu entypo-icon"><i class="icon-cross"></i></div>
            </div>
        </div>
        <div class="panel-body no-padding">

            <!-- vd_panel-menu -->
            <div class=" pad-lr-15-1024-260">
                <div class="col-md-12 col-xs-12 no-padding">
                    <div class="col-md-12 col-xs-12 no-padding">
                        <table style="width: 100%; border-bottom:1px dotted #000; margin-bottom:10px;" class="mp-table2">
                            <tr class="vd_blue">
                                <td class="tab-b-50 text-right">Admissions (Today/Total) 
                                </td>
                                <td class="text-center tab-b-10">:
                                </td>
                                <td class="tab-b-40 text-right">
                                    <asp:LinkButton ID="lblAdmissions" CssClass="vd_blue" runat="server" PostBackUrl="~/1/LisOfAllStudent.aspx?Type=1"></asp:LinkButton>
                            </td></tr>
                            <tr class="vd_green">
                                <td class="tab-b-50 text-right">Admissions Forms (Today/Total) 
                                </td>
                                <td class="text-center tab-b-10">:
                                </td>
                                <td class="tab-b-40 text-right">
                                    <asp:LinkButton ID="lblAdmissionForms" CssClass="vd_green" runat="server" PostBackUrl="~/2/AdmissionFormCollectionRepo.aspx?Type=1"></asp:LinkButton>
                            </td></tr>
                            <tr class="vd_yellow">
                                <td class="tab-b-50 text-right">Enquiries (Today/Total) 
                                </td>
                                <td class="text-center tab-b-10">:
                                </td>
                                <td class="tab-b-40 text-right">
                                    <asp:LinkButton ID="lblEnquiries" CssClass="vd_yellow" runat="server" PostBackUrl="~/1/AllAdmissionReport.aspx?Type=1"></asp:LinkButton>
                            </td></tr>
                            <tr class="vd_red">
                                <td class="tab-b-50 text-right">T.C. (Today/Total) 
                                </td>
                                <td class="text-center tab-b-10">:
                                </td>
                                <td class="tab-b-40 text-right">
                                    <asp:LinkButton ID="lblTC" runat="server" CssClass="vd_red" PostBackUrl="~/2/TCCollectionReport.aspx?Type=1"></asp:LinkButton>
                            </td></tr>
                        </table>
                        <table style="width: 100%; border-bottom:1px dotted #000; margin-bottom:30px;" class="mp-table2">
                            <tr class="vd_green">
                                <td class="tab-b-50 text-right">Admissions Vs. Adm. Forms 
                                </td>
                                <td class="text-center tab-b-10">:
                                </td>
                                <td class="tab-b-40 text-right">
                                    <asp:LinkButton ID="lblAdmissionsVsAdmForms" CssClass="vd_green" runat="server" PostBackUrl="javascript:"></asp:LinkButton>
                            </td></tr>
                            <tr class="vd_red">
                                <td class="tab-b-50 text-right">Adm. Forms Vs. Enquiries 
                                </td>
                                <td class="text-center tab-b-10">:
                                </td>
                                <td class="tab-b-40 text-right">
                                    <asp:LinkButton ID="lblAdmFormsVsEnquiries" CssClass="vd_red" runat="server" PostBackUrl="javascript:"></asp:LinkButton>
                            </td></tr>
                            <tr class="vd_yellow">
                                <td class="tab-b-50 text-right">Admissions Vs. Enquiries
                                </td>
                                <td class="text-center tab-b-10">:
                                </td>
                                <td class="tab-b-40 text-right">
                                    <asp:LinkButton ID="lblAdmissionsVsEnquiries" CssClass="vd_yellow" runat="server" PostBackUrl="~/1/EnquiryVsAdmission.aspx?Type=1"></asp:LinkButton>
                            </td></tr>
                        </table>
                    </div>
                </div>
                <div class="col-md-12 col-xs-12 no-padding">
                    <h3 class="panel-title"><span class="menu-icon" style="color:#000;"><i class="fa fa-birthday-cake"></i></span>&nbsp;&nbsp;<b style="color:#000;">Birthdays & Anniversaries</b></h3>
                    <div class="col-md-8 col-xs-8 no-padding"></div>
                    <div class="col-md-4 col-xs-4 no-padding">
                        <table style="width: 100%; margin-bottom:28px;" class="mp-table2">
                            <tr class="vd_green">
                                <td class="tab-b-45 ">Student 
                                </td>
                                <td class="text-center tab-b-10">:
                                </td>
                                <td class="tab-b-45 text-right">
                                    <asp:LinkButton ID="lblStuTotal"  CssClass="vd_green" runat="server" PostBackUrl="~/8/BirthdayReport.aspx?perType=S&print=1"></asp:LinkButton>
                            </tr>
                            <tr class="vd_yellow">
                                <td class="tab-b-45">Staff
                                </td>
                                <td class="text-center tab-b-10">:
                                </td>
                                <td class="tab-b-45 text-right">
                                    <asp:LinkButton ID="lblEmpTotal"  CssClass="vd_yellow" runat="server" PostBackUrl="~/8/BirthdayReport.aspx?perType=E&print=1"></asp:LinkButton>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            
        </div>
    </div>
</div>
