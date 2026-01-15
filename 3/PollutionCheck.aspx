<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PollutionCheck.aspx.cs" Inherits="admin_PollutionCheck" %>

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
                                <label class="control-label">Vehicle Registration No.&nbsp;<span class="vd_red">* </span></label>
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
                                        <span class="font-s-17">Pollution Check Details </span>
                                    </legend>



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">PUCC No.&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Receipt No.&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Time&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9">
                                        <label class="control-label">Make&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox13" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Model&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Category&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Engine Stroke&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Year of Manufacturing&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Emission Norms&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                           <asp:TextBox ID="TextBox16" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fuel Type&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Valid Upto&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-blue validatetxt" ></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Certificate Fee&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>



                                   
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Attechment&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control-blue" />

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

    <%-- <div class="maindiv">
        <table class="table" width="100%">
            <tr>
                <td>Vehicle Type
                </td>
                <td>
                    <asp:DropDownList ID="drpVehicleType" runat="server" CssClass="textbox">
                    </asp:DropDownList>
                </td>
                <td>Vehicle Registration No.
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
                <h3><span class="imp">Pollution Check Details</span></h3>
                <table class="table" width="100%">
                    <tr>
                        <td>PUCC No.
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
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

                    </tr>
                    <tr>
                        <td>Receipt No.
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td>Time
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DrpHH" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpMM" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpSS" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>Make
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox" Width="200px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>Model
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Category
                        </td>
                        <td>
                            <asp:DropDownList ID="Drpcategory" runat="server" CssClass="textbox">
                                <asp:ListItem>2 Wheeler</asp:ListItem>
                                <asp:ListItem>3 Wheeler</asp:ListItem>
                                <asp:ListItem>4 Wheeler</asp:ListItem>
                                <asp:ListItem>8 Wheeler</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>Engine Stroke
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Year of Manufacturing
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DrpManufactYY" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpManufactMM" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpManufactDD" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>Emission Norms
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Fule Type
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td>Valid Upto
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DrpValidYY" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpValidMM" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="DrpValidDD" runat="server" AutoPostBack="True" CssClass="textbox">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>Certificate Fee
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
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

