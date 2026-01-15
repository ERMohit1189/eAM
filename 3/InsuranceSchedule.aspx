<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="InsuranceSchedule.aspx.cs" Inherits="admin_InsuranceSchedule" %>

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

                            <div class="col-sm-12 ">
                                <fieldset>
                                    <legend>
                                        <span class="font-s-17">Insurence Schedule Details </span>
                                    </legend>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fuel Type&nbsp;<span class="vd_red">* </span></label>
                                        <div class="mgtp-6">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server"  CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem>New Policy</asp:ListItem>
                                                <asp:ListItem>Old Policy</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Policy Issuing office&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Policy No.&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Policy Insured On&nbsp;<span class="vd_red">* </span></label>
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
                                        <label class="control-label">From Date&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Date&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Insurence No.&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DrpFuelType" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Insurence Address</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control-blue validatetxt" Rows="1" TextMode="MultiLine"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle No.&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Intermediary Code&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle IDV&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                   

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Contact No.&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Hypothecated With&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle Total Value&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Cover Note No.&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Premium&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Attechment&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control-blue validatetxt" />
                                           
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" runat="server" CssClass="button">Submit</asp:LinkButton>
                                    </div>

                                </fieldset>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--   <div class="maindiv">
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
                <h3><span class="imp">Insurence Schedule Details</span></h3>
                <table class="table" width="100%">
                    <tr>
                        <td>Policy Type
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem>New Policy</asp:ListItem>
                                <asp:ListItem>Old Policy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>Policy Issuing office
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>Policy No.
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox14" runat="server" CssClass="textbox" Width="200"></asp:TextBox>
                        </td>
                        <td>Policy Insured On
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
                    </tr>
                    <tr>
                        <td>From Date
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DrpFromYY" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpFromMM" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpFromDD" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>To Date
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DrpToYY" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpToMM" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpToDD" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>Insurence No.
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td>Insurence Address
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox" Width="200px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Vehicle No.
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td>Intermediary Code
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Vehicle IDV
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td>Contact No.
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox7" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Hypothecated With
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td>Vehicle Total Value
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox9" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Hypothecated With
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox10" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td>Vehicle Total Value
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox11" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Cover Note No.
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox12" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td>Premium
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox13" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Attechment
                        </td>
                        <td colspan="3">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="textbox" />
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

