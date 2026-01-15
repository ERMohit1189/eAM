<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmpProfile.ascx.cs" Inherits="staff_userControl_EmpProfile" %>

<div class="mgbt-xs-10 col-md-4 sp-d-box1">
    <div class="panel widget panel-bd-top vd_bdt-grey vd_todo-widget light-widget ">
        <div class="panel-heading no-title ">
            <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-user-circle"></i></span>Staff Profile</h3>
            <%--<div class="vd_panel-menu">
                 <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                 <div data-action="refresh" title="Refresh" data-placement="bottom" class=" menu entypo-icon smaller-font"><i class="icon-cycle"></i></div>
                 <div data-action="close" title="Close" data-placement="bottom" class=" menu entypo-icon"><i class="icon-cross"></i></div>
             </div>--%>
        </div>
        <div class="panel-body">
            <div class="no-padding">
                <div class="stu-pro-pic-box mgbt-xs-10">
                    <asp:Image ID="stImage" runat="server" class="profile-user-img img-circle" alt="Staff Image" />
                </div>
                <h4 class="pro-name mgbt-xs-5 text-center">
                    <asp:Label ID="lblStName" runat="server" Text=""></asp:Label>
                </h4>
                <hr />
            </div>
            <div class=" no-padding mgtp-10">
                <table class="table pro-tab no-bm  table-hover">
                    <tbody>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-user pro-icon"></i>Emp ID:
                                     <asp:Label ID="lblEmpId" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-user pro-icon"></i>Username:
                                    <asp:Label ID="lblEmpCode" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-female pro-icon"></i>Designation:
                                     <asp:Label ID="lblDes" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-birthday-cake pro-icon"></i>Date of Joining:
                                     <asp:Label ID="lblDOJ" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-phone-square pro-icon"></i>Contact No.:
                                     <asp:Label ID="lblContactNo" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-phone-square pro-icon"></i>Emergency No.:
                                     <asp:Label ID="lblENo" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-envelope pro-icon"></i>EmaiL:
                                     <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-map-marker pro-icon"></i>Address:
                                     <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
