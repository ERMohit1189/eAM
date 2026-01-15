<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="HomeWorkReport.aspx.cs" Inherits="_11_HomeWorkReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="col-sm-12  no-padding">
                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                        <label class="control-label">Class&nbsp;<span class="vd_red"></span></label>
                        <div class="">
                            <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue validatedrp">
                            </asp:DropDownList>
                            <div class="text-box-msg"></div>
                        </div>
                    </div>

                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                        <label class="control-label">From Date&nbsp;<span class="vd_red"></span></label>
                        <div class="">
                            <%-- ReSharper disable once UnknownCssClass --%>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                            <div class="text-box-msg">
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                        <label class="control-label">To Date&nbsp;<span class="vd_red"></span></label>
                        <div class="">
                            <%-- ReSharper disable once UnknownCssClass --%>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                            <div class="text-box-msg">
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                        <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                        <div class="">
                            <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" OnClick="lnkSubmit_OnClick">Submit</asp:LinkButton>
                            <div class="text-box-msg">
                            </div>
                        </div>
                    </div>

                </div>

                <div class="col-sm-12 ">
                    <div class="table-responsive2 table-responsive">
                        <asp:GridView ID="grdDocList" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" ShowHeader="False">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <table align="left" width="100%">
                                            <tr>
                                                <td>
                                                    <h5 class="vd_red mgbt-xs-5">
                                                        <asp:Label ID="Lbdate" runat="server" CssClass="right_menu"
                                                            Text='<%# Bind("Date") %>'></asp:Label>
                                                        | 
                                                    <asp:Label runat="server" ID="lblClass" Text='<%#Eval("ClassName") %>'></asp:Label>\
                                                             <asp:Label runat="server" ID="lblBranch" Text='<%# Eval("BranchName") %>'></asp:Label>
                                                       (<asp:Label runat="server" ID="lblSection" Text='<%# Eval("SectionName") %>'></asp:Label>)


                                                        <%--                                                    <asp:Label ID="lblUploadedby" runat="server" CssClass="right_menu" 
                                                    Text='<%# Bind("LoginName") %>'></asp:Label>--%>
                                                    </h5>
                                                    <h5 class="vd_red mgbt-xs-5"></h5>
                                                    <h4 class="vd_red font-semi-bold mgbt-xs-5 text-justify" style="font-size: 16px !important;">
                                                        <asp:Label ID="LbleHead" runat="server" CssClass="date"
                                                            Text='<%# Bind("Title") %>' ForeColor="Black"></asp:Label></h4>

                                                    <p class=" mgbt-xs-5 text-justify">
                                                        <asp:Label ID="blMess" runat="server" CssClass="right_menu"
                                                            Text='<%# Bind("HomeWork") %>'></asp:Label>
                                                    </p>
                                                    <hr />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <HeaderStyle BorderStyle="None" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

