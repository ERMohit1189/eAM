<%@ Control Language="C#" AutoEventWireup="true" CodeFile="classTeacherProfile.ascx.cs" Inherits="sp_userControl_classTeacherProfile" %>
<div class=" mgbt-xs-10 half-w-1280 sp-d-box1">
    <div class="panel widget panel-bd-top vd_bdt-grey vd_todo-widget light-widget ">
        <div class="panel-heading no-title ">
            <h3 class="panel-title"><span class="menu-icon"><i class="fa fa-user"></i></span>Class Teacher Profile</h3>
            <div class="vd_panel-menu">
                <div data-action="minimize" title="Minimize" data-placement="bottom" class=" menu entypo-icon"><i class="icon-minus3"></i></div>
                <div data-action="refresh" title="Refresh" data-placement="bottom" class=" menu entypo-icon smaller-font"><i class="icon-cycle"></i></div>
                <div data-action="close" title="Close" data-placement="bottom" class=" menu entypo-icon"><i class="icon-cross"></i></div>
            </div>
        </div>
        <div class="panel-body">
            <div class=" no-padding">
                <div class="stu-pro-pic-box mgbt-xs-10">

                    <asp:Image ID="ctImage" runat="server" class="profile-user-img img-circle " alt="" />

                </div>
                <h3 class="pro-name mgbt-xs-5 text-center txt-upper-alpha">
                    <asp:Label ID="lblCTNAME" runat="server" Text=""></asp:Label>

                </h3>
            </div>
            <div class=" no-padding mgtp-10">
                <table class="table pro-tab no-bm  table-hover">
                    <tbody>
                       <tr id="moshow" runat="server" visible="false">
                            <td>
                                <%--Contact No.&nbsp; : &nbsp;<asp:Label ID="lblCTCONTACT" runat="server" Text=""></asp:Label>--%>
                                <div class="pro-content-box">
                                    <i class="fa fa-phone-square pro-icon"></i><%--Contact No. --%>
                                    <asp:Label ID="lblCTCONTACT" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr  id="emailshow" runat="server" visible="false">
                            <td>
                                <%-- Email &nbsp; : &nbsp;
                                <asp:Label ID="lblCTEMAIL" runat="server" Text=""></asp:Label>--%>
                                <div class="pro-content-box">
                                    <i class="fa fa-envelope pro-icon"></i><%--Email --%>
                                <asp:Label ID="lblCTEMAIL" runat="server" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>

