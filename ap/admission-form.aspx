<%@ Page Title="" Language="C#" MasterPageFile="~/ap/admin_root-manager.master" AutoEventWireup="true" CodeFile="admission-form.aspx.cs" Inherits="ap.ApAdmissionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_body">
        <!-- Header Start -->

        <!-- Header Ends -->
        <div class="content">
            <div class="container">

                <!-- Middle Content Start -->

                <div class="vd_content-wrapper">
                    <div class="vd_container">
                        <div class="vd_content clearfix">
                            <div class="vd_content-section clearfix">
                                <div class="vd_login-page">
                                    <div class="panel widget">
                                        <div class="panel-body">
                                            <div class="login-img entypo-icon">
                                                <div class="main-logo-center">
                                                    <img src="../img/logo.png" alt="eAM logo" />
                                                </div>
                                            </div>

                                            <div class="form-horizontal">

                                                <div class="col-md-12">
                                                    
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="">
                                                                <asp:DropDownList ID="drpSession" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                                                <div class="text-box-msg">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                                                Display="Dynamic" CssClass="imp" ValidationGroup="a" SetFocusOnError="true"
                                                                                                ControlToValidate="drpSession" ErrorMessage="Please select session."></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-md-12">

                                                            <div class="vd_input-wrapper" id="email-input-wrapper">
                                                                <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                <asp:TextBox ID="TextBox3" runat="server" placeholder="Student's First Name" CssClass="form-control-blue"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="vd_input-wrapper" id="Div1">
                                                                <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                <asp:TextBox ID="TextBox1" runat="server" placeholder="Middle Name" CssClass="form-control-blue"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="vd_input-wrapper" id="Div2">
                                                                <span class="menu-icon"><i class="fa fa-user"></i></span>
                                                                <asp:TextBox ID="TextBox2" runat="server" placeholder="Last Name" CssClass="form-control-blue"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="">
                                                                <asp:DropDownList ID="drpSex" runat="server" CssClass="form-control-blue">
                                                                    <asp:ListItem>Male</asp:ListItem>
                                                                    <asp:ListItem>Female</asp:ListItem>
                                                                    <asp:ListItem>Transgender</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="vd_input-wrapper" id="password-input-wrapper">
                                                                <span class="menu-icon"><i class="fa fa-bullhorn"></i></span>

                                                                <asp:DropDownList ID="DropDownList1" CssClass="select-box form-control-blue" runat="server">
                                                                    <asp:ListItem>Select Class</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <span>Admission form Fee INR <asp:Label ID="Label1" runat="server" ForeColor="red" Text="XXX"></asp:Label></span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">

                                                        <div class="col-md-12 text-center mgbt-xs-5">

                                                            <asp:LinkButton ID="LinkButton1" CssClass="btn vd_bg-blue vd_white width-100" runat="server">Payment</asp:LinkButton>
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Panel Widget -->
                                    <%-- ReSharper disable once UnknownCssClass --%>
                                    <div class="register-panel text-center font-semibold">
                                        <p class="login-footer-title">© Axon IT Services </p>
                                    </div>
                                    <!-- Panel Widget -->

                                </div>
                                <!-- vd_login-page -->

                            </div>
                            <!-- .vd_content-section -->

                        </div>
                        <!-- .vd_content -->
                    </div>
                    <!-- .vd_container -->
                </div>
                <!-- .vd_content-wrapper -->

                <!-- Middle Content End -->

            </div>
            <!-- .container -->
        </div>
        <!-- .content -->
    </div>
</asp:Content>

