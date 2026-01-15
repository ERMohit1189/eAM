<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MasterUpdate.aspx.cs" Inherits="_1.AdminMasterUpdate" %>

<%-- ReSharper disable once AspUnusedRegisterDirectiveHighlighting --%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="jhgj" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">
                                    <div class="col-md-2 col-sm-4 col-xs-6 ">
                                        <table class="table  table-hover no-bm  table-bordered " id="table1" style="float: left;">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue vd_white ">Masters
                                                    </th>
                                                    <th class="vd_bg-blue vd_white text-center">
                                                        <asp:CheckBox ID="ChkAll1" runat="server" Checked="true" onClick="CheckUncheck('table1',this);" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Class Master"></asp:Label>
                                                    </td>
                                                    <th class="text-center">
                                                        <asp:CheckBox ID="Chk1" runat="server" Enabled="false" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Section Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk2" runat="server" Enabled="false" />
                                                    </td>
                                                </tr>



                                            </tbody>

                                        </table>
                                    </div>

                                    <div class="col-md-2 col-sm-4 col-xs-6 ">
                                        <table class="table  table-hover no-bm  table-bordered " id="table2" style="float: left; margin-left: 5px">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue vd_white ">Masters
                                                    </th>
                                                    <th class="vd_bg-blue vd_white text-center">
                                                        <asp:CheckBox ID="ChkAll2" runat="server" Checked="true" onClick="CheckUncheck('table2',this);" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="House Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk7" Enabled="false" runat="server" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label16" runat="server" Text="Group Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk16" Enabled="false" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="col-md-2 col-sm-4 col-xs-6 ">
                                        <table class="table  table-hover no-bm  table-bordered " id="table3" style="float: left; margin-left: 5px">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue vd_white ">Masters
                                                    </th>
                                                    <th class="vd_bg-blue vd_white text-center">
                                                        <asp:CheckBox ID="ChkAll3" runat="server" Checked="true" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Text="Staff DocumentType Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk9" Enabled="false" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" Text="Student DocumentType Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk8" Enabled="false" runat="server" />
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="col-md-2 col-sm-4 col-xs-6 ">
                                        <table class="table  table-hover no-bm  table-bordered " id="table4" style="float: left; margin-left: 5px">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue vd_white ">Masters
                                                    </th>
                                                    <th class="vd_bg-blue vd_white text-center">
                                                        <asp:CheckBox ID="ChkAll4" runat="server" Checked="true" onClick="CheckUncheck('table4',this);" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text="FeeGroup Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk3" Enabled="false" runat="server" onClick="CheckUncheck('table1',this);" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Month Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk4" Enabled="false" runat="server" onClick="CheckUncheck('table1',this);" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="Fee Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk5" Enabled="false" runat="server" onClick="CheckUncheck('table1',this);" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text="FeeAlloted Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk6" Enabled="false" runat="server" onClick="CheckUncheck('table1',this);" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label17" runat="server" Text="Range Basis Fine Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk17" Enabled="false" runat="server" onClick="CheckUncheck('table1',this);" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="col-md-2 col-sm-4 col-xs-6 ">
                                        <table class="table  table-hover no-bm  table-bordered " id="table5" style="float: left; margin-left: 5px">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue vd_white ">Masters
                                                    </th>
                                                    <th class="vd_bg-blue vd_white text-center">
                                                        <asp:CheckBox ID="ChkAll5" runat="server" Checked="true" onClick="CheckUncheck('table5',this);" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" Text="VehicleType Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk10" runat="server" Enabled="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server" Text="Vehicle Registration Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk11" runat="server" Enabled="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label12" runat="server" Text="Vehicle Route Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk12" runat="server" Enabled="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label13" runat="server" Text="Vehicle Pickup Location Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk13" runat="server" Enabled="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label14" runat="server" Text="Vehicle Drop Location Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk14" runat="server" Enabled="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label15" runat="server" Text="Locationwise Vehicle Amount Master"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="Chk15" runat="server" Enabled="false" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <div class="col-sm-12   mgbt-xs-15">
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button">Update</asp:LinkButton>
                                    <div id="msgbox" runat="server" style="left: 76px"></div>
                                </div>

                                <div class="col-sm-12  ">
                                    <asp:CheckBox ID="CheckBox1" runat="server" BackColor="Green" BorderColor="Green" BorderStyle="Solid" Checked="True" Enabled="False" />&nbsp;&nbsp;<asp:Label ID="Label18" runat="server" Text="Can Update Master" CssClass="imp"></asp:Label>
                                    <br />
                                    <asp:CheckBox ID="CheckBox2" runat="server" BackColor="Red" BorderColor="Red" BorderStyle="Solid" Enabled="False" />&nbsp;&nbsp;<asp:Label ID="Label19" runat="server" Text="Can Not Update Master" CssClass="imp"></asp:Label>
                                </div>

                            </div>
                        </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

