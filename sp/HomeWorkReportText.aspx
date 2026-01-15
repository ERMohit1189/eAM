<%@ Page Title="" Language="C#" MasterPageFile="~/sp/sp_root-manager.master" AutoEventWireup="true" CodeFile="HomeWorkReportText.aspx.cs" Inherits="sp.AdminUploadHomeWorkText" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12  no-padding">
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
                    
                    <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel1" runat="server">
                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                    title="Export to Word" Visible="False" data-placement="top"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                    title="Export to Excel" Visible="False"  data-placement="top"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                    title="Export to PDF" Visible="False"  data-placement="top"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                                <script>
                                                    
                                                </script>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ImageButton1" />
                                            <asp:PostBackTrigger ControlID="ImageButton2" />
                                            <asp:PostBackTrigger ControlID="ImageButton3" />
                                            <asp:PostBackTrigger ControlID="ImageButton4" />
                                        </Triggers> 
                                    </asp:UpdatePanel>
                                </div>
                    <div class="col-sm-12 ">
                        
                        <div class="col-sm-12 ">
                            <div class="table-responsive2 table-responsive" id="mainDiv" runat="server">
                                <div id="header" runat="server" style="width: 80%;"></div>
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
                                                                <%--         | 
                                                <asp:Label runat="server" ID="lblClass" Text='<%#Eval("ClassName") %>'></asp:Label>
                                                                        <asp:Label runat="server" ID="lblSection" Text='<%# Eval("SectionName") %>'></asp:Label>
                                                                        <asp:Label runat="server" ID="lblBranch" Text='<%# Eval("BranchName") %>' Visible='<%# Eval("BranchName").ToString() != "Other"?true:false %>'></asp:Label>--%>

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
                                <div id="msgbox" runat="server"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

