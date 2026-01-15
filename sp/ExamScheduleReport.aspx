<%@ Page Title="Exam Schedule | eAM&#174;" Language="C#" MasterPageFile="~/sp/sp_root-manager.master" AutoEventWireup="true" 
CodeFile="ExamScheduleReport.aspx.cs" Inherits="admin_UploadExamSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="grdDocList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped no-bm table-hover no-head-border table-bordered pro-table">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" ID="idd" CssClass="link" NavigateUrl='<%# Bind("DocPath") %>' download="ExamSchedule" style="color:blue;">
                                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np"  Width="350px" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Download">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocPath" runat="server" Text='<%# Bind("DocPath") %>' Visible="false"></asp:Label>
                                                        <asp:HyperLink ID="lnkPath" runat="server" Download="Download" 
                                                              class="btn menu-icon vd_bd-yellow vd_yellow"> 
                                                                    <i class="fa fa-download"></i></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <div id="msgbox" runat="server"></div>
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

