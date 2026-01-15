<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SalaryBifurcation.aspx.cs" Inherits="SalaryBifurcation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <style>
        input[type=checkbox] {
            width: 14px !important;
            height: 17px !important
        }
    </style>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Designation</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDesignation" CssClass="form-control-blue" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  btn-a-devices-2-p2 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:LinkButton ID="lnkCalculated" runat="server" OnClick="lnkView_Click" CssClass="button">View</asp:LinkButton>
                                            <div id="divMsg" runat="server" style="left: 47px;"></div>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-12  ">
                                        <div runat="server" id="divExport">
                                            <div class=" table-responsive  table-responsive2 ">
                                                <table class="table table-striped table-hover no-bm no-head-border table-bordered" id="tbl">
                                                    <asp:Repeater ID="rptEmp" runat="server">
                                                        <HeaderTemplate>
                                                            <tr class="itemrow">
                                                                <th>#</th>
                                                                <th>Emp. Id.</th>
                                                                <th>Name</th>
                                                                <th>Earning</th>
                                                                <th>Deductions</th>
                                                                <th>Net Pay</th>
                                                                <th>Date</th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="text-center itemrow">
                                                                <td style="white-space: nowrap !important;"><%# Container.ItemIndex+1 %>
                                                                    <td class="">
                                                                        <asp:Label ID="EmpId" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label></td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Name" runat="server" Text='<%# Eval("name") %>' Style="white-space: nowrap !important;"></asp:Label>
                                                                    </td>
                                                                    <td class="text-left" style="vertical-align: top !important; font-size: 10px; font-weight: bold;">
                                                                        <asp:Repeater ID="rptHead1" runat="server">
                                                                            <ItemTemplate>
                                                                                <div class="col-sm-12">
                                                                                    <div style="float: left;">
                                                                                        <asp:Label ID="ComponentName" runat="server" Text='<%# Eval("ComponentName") %>'></asp:Label>
                                                                                    </div>
                                                                                    <div style="float: right;">
                                                                                        <asp:Label ID="ComponentValue" runat="server" Text='<%# Eval("ComponentValue") %>'></asp:Label>
                                                                                    </div>
                                                                                </div>

                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <asp:Panel class="col-sm-12" Style="background: #ccc;" runat="server" ID="divEar">
                                                                            <div style="float: left;">
                                                                                <b>Total</b>
                                                                            </div>
                                                                            <div style="float: right;">
                                                                                <b>
                                                                                    <asp:Label ID="totalEarning" runat="server"></asp:Label></b>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td class="text-left" style="vertical-align: top !important; font-size: 10px; font-weight: bold;">
                                                                        <asp:Repeater ID="rptHead2" runat="server">
                                                                            <ItemTemplate>
                                                                                <div class="col-sm-12">
                                                                                    <div style="float: left;">
                                                                                        <asp:Label ID="ComponentName" runat="server" Text='<%# Eval("ComponentName") %>'></asp:Label>
                                                                                    </div>
                                                                                    <div style="float: right;">
                                                                                        <asp:Label ID="ComponentValue" runat="server" Text='<%# Eval("ComponentValue") %>'></asp:Label>
                                                                                    </div>
                                                                                </div>

                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <asp:Panel class="col-sm-12" Style="background: #ccc;" runat="server" ID="divDed">
                                                                            <div style="float: left;">
                                                                                <b>Total</b>
                                                                            </div>
                                                                            <div style="float: right;">
                                                                                <b>
                                                                                    <asp:Label ID="totalDeduction" runat="server"></asp:Label></b>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td style="text-align: right;">
                                                                        <asp:Label ID="NetPay" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LoginName" runat="server"></asp:Label><br />
                                                                        <asp:Label ID="Recdate" runat="server"></asp:Label>
                                                                    </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

