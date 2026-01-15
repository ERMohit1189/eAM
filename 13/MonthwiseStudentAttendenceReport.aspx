<%@ Page Title="Attendance Report | eAM&#174;" Language="C#" MasterPageFile="stuRootManager.master" AutoEventWireup="true" CodeFile="MonthwiseStudentAttendenceReport.aspx.cs" Inherits="sp_MonthwiseStudentAttendenceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="dfg" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <ajaxToolkit:Accordion ID="Accordion1" runat="server" Width="100%" SuppressHeaderPostbacks="true">
                                    <HeaderTemplate >
                                        <h6 class="slide-u-d">
                                            <i class="fa fa-sort-amount-asc" style="padding: 0 10px 0 0"></i>
                                            <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("MonthName") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YearName") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblMonthYear" runat="server" Text='<%# Eval("MonthYearName") %>'></asp:Label>
                                        </h6>
                                        
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                            CssClass="table table-striped table-hover no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Day">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDay" runat="server" Text='<%# Bind("Day") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Total Present:&nbsp;<asp:Label ID="lblTotalPrs" runat="server" Text=""></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Attendance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAttendance" runat="server" Text='<%# Bind("Attendance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Total Absent:&nbsp;<asp:Label ID="lblTotalAb" runat="server" Text=""></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    
                                </ajaxToolkit:Accordion>
                            </div>
                        </div>

                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

