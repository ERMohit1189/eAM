<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ServiceSchedule.aspx.cs" Inherits="admin_ServiceSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top ">
                    <div class="panel-body ">
                        <div class="col-sm-12  no-padding">
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Vehicle Type</label>
                                <div class="">
                                    <asp:DropDownList ID="drpVehicleType" runat="server" AutoPostBack="True" CssClass="form-control-blue"></asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Vehicle No.</label>
                                <div class="">
                                    <asp:DropDownList ID="drpVehicleNo" runat="server" AutoPostBack="True" CssClass="form-control-blue"></asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12  no-padding">
                            <h3><span class="imp">Service Schedule Details</span></h3>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Fuel Consume</label>
                                <div class="">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue" AutoPostBack="True"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Registration No.</label>
                                <div class="">
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue" AutoPostBack="True"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Date</label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True"
                                                CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True"
                                                CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DDDate" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Remark</label>
                                <div class="">
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue" AutoPostBack="True"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                <div id="msgbox" runat="server" style="left: 75px;"></div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

