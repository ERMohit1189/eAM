<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="Uploadphoto.aspx.cs" Inherits="web2_Uploadphoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" Runat="Server">
        <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12  no-padding">
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Select Album&nbsp;<span class="vd_red">*</span></label>
                                <div class=" ">
                                    <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Album Title&nbsp;<span class="vd_red">*</span></label>
                                <div class=" ">
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Select Image&nbsp;<span class="vd_red">*</span></label>
                                <div class=" ">
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  btn-a-devices-3-p6 mgbt-xs-15">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button">Submit</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

