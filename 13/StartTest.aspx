<%@ Page Title="Start Test | eAM&#174;" Language="C#" MasterPageFile="~/13/stuRootManager.master" AutoEventWireup="true" CodeFile="StartTest.aspx.cs" Inherits="StartTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">


    <style>
        .orange {
            background: #da4448 !important;
            color: white !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class=" pad-lr-15-1024-260 table-responsive2 table-responsive">
                            <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activity">
                                        <ItemTemplate>
                                            <asp:Label ID="ExamID" runat="server" CssClass="hide" Text='<%# Bind("ExamID") %>'></asp:Label>
                                            <asp:HyperLink ID="lnk" ToolTip="" runat="server" Text="Start Test" CssClass="text-danger"></asp:HyperLink>
                                            <asp:Label ID="Status" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="100" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject">
                                        <ItemTemplate>
                                            <asp:Label ID="Subject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="150" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paper">
                                        <ItemTemplate>
                                            <asp:Label ID="Paper" runat="server" Text='<%# Bind("Paper") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="150" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test Name">
                                        <ItemTemplate>
                                            <asp:Label ID="SRNo" runat="server" Text='<%# Bind("ExamName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="150" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Valid Till" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExamValidFrom" runat="server" Text='<%# Bind("ExamValidFrom", "{0:dd-MMM-yyyy}") %>'></asp:Label>&nbsp; to &nbsp;
                                                        <asp:Label ID="ExamValidTo" runat="server" Text='<%# Bind("ExamValidTo", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  Width="150" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>--%>
                                    <%-- <asp:TemplateField HeaderText="Time Duration" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="StartFrom" runat="server" Text='<%# Bind("StartFrom", "{0:hh:mm tt}") %>'></asp:Label>&nbsp; to &nbsp;
                                                        <asp:Label ID="EndTo" runat="server" Text='<%# Bind("EndTo", "{0:hh:mm tt}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white"  Width="100" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>--%>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

