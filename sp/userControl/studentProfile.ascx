<%@ Control Language="C#" AutoEventWireup="true" CodeFile="studentProfile.ascx.cs" Inherits="sp_userControl_studentProfile" %>
<div class=" mgbt-xs-10 half-w-1280 sp-d-box1">
    <div class="panel widget panel-bd-top vd_bdt-grey vd_todo-widget light-widget ">
        <div class="panel-heading no-title ">
            <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-user-circle"></i></span>Student Profile</h3>
            <div class="vd_panel-menu">
                <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                <div data-action="refresh" title="Refresh" data-placement="bottom" class=" menu entypo-icon smaller-font"><i class="icon-cycle"></i></div>
                <div data-action="close" title="Close" data-placement="bottom" class=" menu entypo-icon"><i class="icon-cross"></i></div>
            </div>
        </div>
        <div class="panel-body">
            <div class=" no-padding">

                <div class="stu-pro-pic-box mgbt-xs-10">
                    <asp:Image ID="stImage" runat="server" class="profile-user-img img-circle" alt="student" />
                </div>
                <h3 class="pro-name mgbt-xs-5 text-center">
                    <asp:Label ID="lblStName" runat="server" Text=""></asp:Label>
                </h3>
                <h4 class="pro-class mgbt-xs-5 text-center">
                    <asp:Label ID="lblClass" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblSection" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblBranch" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblStream" runat="server" Text=""></asp:Label>&nbsp;(
                    <asp:Label ID="lblSrno" runat="server" Text=""></asp:Label>

                    )
                </h4>
                <hr />
            </div>
            <div class=" no-padding mgtp-10">

                <table class="table pro-tab no-bm  table-hover" >
                    <tbody>
                        <tr style="display:none;">
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-bullhorn pro-icon"></i>Session
                                    <asp:Label ID="lblSessionName" runat="server" Text=""></asp:Label>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-user pro-icon"></i>Father
                                <asp:Label ID="lblFather" runat="server" Text=""></asp:Label>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-female pro-icon"></i>Mother
                                 <asp:Label ID="lblMother" runat="server" Text=""></asp:Label>
                                </div>
                            </td>

                        </tr>

                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-birthday-cake pro-icon"></i>Date of Birth 
                                <asp:Label ID="lblDOB" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-phone-square pro-icon"></i><%--Contact No. --%>
                                <asp:Label ID="lblFaContactNo" runat="server" Text=""></asp:Label>
                                </div>

                            </td>

                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-envelope pro-icon"></i><%--Email --%>
                                <asp:Label ID="lblFaEmail" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="pro-content-box">
                                    <i class="fa fa-map-marker pro-icon"></i><%--Address--%>
                                    <asp:Label ID="lblSTAddress" runat="server" Text=""></asp:Label>
                                </div>
                            </td>

                        </tr>

                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
<div class=" no-padding sp-d-box2 stu-pro-box" style="display:none !important;">
    <div class=" no-padding stu-pro-box-sub"></div>
    <div class=" no-padding stu-pro-box-main">

        <div class="stu-pro-pic-box mgbt-xs-10">
            <asp:Image ID="stImage2" runat="server" class="profile-user-img img-circle" alt="student" />
        </div>
        <h3 class="pro-name mgbt-xs-5 text-center">
            <asp:Label ID="lblStName2" runat="server" Text=""></asp:Label>
        </h3>
        <h4 class="pro-class mgbt-xs-5 text-center">
            <asp:Label ID="lblClass2" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblSection2" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblBranch2" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblStream2" runat="server" Text=""></asp:Label>&nbsp;(
        <asp:Label ID="lblSrno2" runat="server" Text=""></asp:Label>
            )
        </h4>
    </div>
</div>

