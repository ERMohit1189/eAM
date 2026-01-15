<%@ Page Title="" Language="C#" MasterPageFile="~/sp/sp_root-manager.master" AutoEventWireup="true" 
CodeFile="profile.aspx.cs" Inherits="admin_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-3">
                <div class="panel widget light-widget panel-bd-top ">
                    <div class="panel-body ">
                        <div class="col-sm-12 no-padding ">

                            <div class="form-group">
                                <div class=" no-padding">
                                    <div class="form-img text-center profile-pic-box2 mgbt-xs-15">
                                        <asp:Image ID="imgAvatars" class="Avatars" alt="" runat="server" />
                                    </div>
                                    <div class="form-img-action text-center ">
                                        <div class="file-u-btn2">
                                            <asp:FileUpload ID="avatarUpload" runat="server" onchange="checksFileSizeandFileType(this, 20000, 'jpg|png|jpeg|gif','Avatars');"
                                                Type="file" CssClass="form-control-blue " />
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" OnClick="LinkButton1_Click">Update</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="return confirm('Do you want to delete profile pic?')" 
                                                CssClass="button form-control-blue" OnClick="LinkButton2_Click">Delete</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-9">
                <div class="panel widget light-widget panel-bd-top ">
                    <div class="panel-body ">
                        <div class="col-sm-12 no-padding  mgbt-xs-20">
                            <h3 class="mgbt-xs-15 mgtp-10 font-semibold"><i class="icon-user mgr-10 profile-icon"></i>ABOUT</h3>
                            <div class="row">
                                <div class="col-sm-6 mgbt-xs-10">
                                    <div class="row mgbt-xs-0">
                                        <label class="col-xs-5 control-label">Name</label>
                                        <div class="col-xs-7 controls">
                                            <asp:TextBox ID="txtName" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <!-- col-sm-10 -->
                                    </div>
                                </div>
                                <div class="col-sm-6 mgbt-xs-10">
                                    <div class="row mgbt-xs-0">
                                        <label class="col-xs-5 control-label">Display Name</label>
                                        <div class="col-xs-7 controls">
                                            <asp:TextBox ID="txtDisplay" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <!-- col-sm-10 -->
                                    </div>
                                </div>
                                <div class="col-sm-6 mgbt-xs-10">
                                    <div class="row mgbt-xs-0">
                                        <label class="col-xs-5 control-label">Father's Name</label>
                                        <div class="col-xs-7 controls">
                                            <asp:TextBox ID="txtFathersName" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <!-- col-sm-10 -->
                                    </div>
                                </div>
                                <div class="col-sm-6 mgbt-xs-10">
                                    <div class="row mgbt-xs-0">
                                        <label class="col-xs-5 control-label">Contact No.</label>
                                        <div class="col-xs-7 controls">
                                            <asp:TextBox ID="txtContactNo" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <!-- col-sm-10 -->
                                    </div>
                                </div>
                                <div class="col-sm-6 mgbt-xs-10">
                                    <div class="row mgbt-xs-0">
                                        <label class="col-xs-5 control-label">E-mail</label>
                                        <div class="col-xs-7 controls">
                                            <asp:TextBox ID="txtEmail" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <!-- col-sm-10 -->
                                    </div>
                                </div>
                                <div class="col-sm-6 mgbt-xs-10">
                                    <div class="row mgbt-xs-0">
                                        <label class="col-xs-5 control-label">Address</label>
                                        <div class="col-xs-7 controls">
                                            <asp:TextBox ID="txtAddress" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <!-- col-sm-10 -->
                                    </div>
                                </div>
                                <div class="col-sm-6 mgbt-xs-10">
                                    <div class="row mgbt-xs-0">
                                        <label class="col-xs-5 control-label">Country</label>
                                        <div class="col-xs-7 controls">
                                            <asp:DropDownList ID="drpCountry" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                        <!-- col-sm-10 -->
                                    </div>
                                </div>
                                <div class="col-sm-6 mgbt-xs-10">
                                    <div class="row mgbt-xs-0">
                                        <label class="col-xs-5 control-label">State</label>
                                        <div class="col-xs-7 controls">
                                            <asp:DropDownList ID="drpState" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                        <!-- col-sm-10 -->
                                    </div>
                                </div>
                                <div class="col-sm-6 mgbt-xs-10">
                                    <div class="row mgbt-xs-0">
                                        <label class="col-xs-5 control-label">City</label>
                                        <div class="col-xs-7 controls">
                                            <asp:DropDownList ID="drpCity" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                        <!-- col-sm-10 -->
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

