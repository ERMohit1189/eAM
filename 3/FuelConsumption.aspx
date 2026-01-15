<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="FuelConsumption.aspx.cs" Inherits="admin_FuleConsumption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
       <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12  no-padding">

                            

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Vehicle Type&nbsp;<span class="vd_red">* </span></label>
                                <div class="">
                                    <asp:DropDownList ID="drpVehicleType" runat="server" CssClass="form-control-blue validatedrp">
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Vehicle No.&nbsp;<span class="vd_red">* </span></label>
                                <div class="">
                                    <asp:DropDownList ID="drpVehicleNo" runat="server" CssClass="form-control-blue validatedrp">
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                           

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Date&nbsp;<span class="vd_red">* </span></label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
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
                                <label class="control-label">Fuel Type&nbsp;<span class="vd_red">* </span></label>
                                <div class="">
                                    <asp:DropDownList ID="DrpFuelType" runat="server" CssClass="form-control-blue validatedrp">
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Meter Reading(In Km.)&nbsp;<span class="vd_red">* </span></label>
                                <div class="">
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Remaining Fuel&nbsp;<span class="vd_red">* </span></label>
                                <div class="">
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Quantity&nbsp;<span class="vd_red">* </span></label>
                                <div class="">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            

                            <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                <label class="control-label">Remark</label>
                                <div class="">
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue" Rows="1" TextMode="MultiLine"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                <asp:LinkButton ID="lnkSubmit" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" runat="server" CssClass="button">Submit</asp:LinkButton>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<div class="maindiv">
        <table class="table" width="100%">
            <tr>
                <td>Vehicle Type
                </td>
                <td>
                    <asp:DropDownList ID="drpVehicleType" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
                <td>Vehicle No.
                </td>
                <td>
                    <asp:DropDownList ID="drpVehicleNo" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

        <br />
        <div width="100%">
            <asp:Panel ID="Panel1" runat="server">
                <h3><span class="imp">Fule Consumption</span></h3>
                <table class="table" width="100%">
                    <tr>
                        <td>Date
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DDDate" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>Fule Type
                        </td>
                        <td>
                            <asp:DropDownList ID="DrpFuleType" runat="server" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Meter Reading(In Km.)
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td>Remaining Fuel
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Quantity
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td>Remark
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox" Width="200px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button">Submit</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>--%>
</asp:Content>

