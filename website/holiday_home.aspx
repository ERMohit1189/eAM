<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="holiday_home.aspx.cs" Inherits="website_holiday_home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="page-main-content" class="main-content  col-md-9 col-lg-9 sb-r">

        <div class="main-content-inner">

            <div class="content-main">
                <div class="region region-content">
                    <div id="block-system-main" class="block block-system no-title">
                        <div class="block-inner clearfix">
                            <div class="panel panel-danger">
                                <div class="panel-heading">HOLIDAY HOMEWORK</div>
                                <div class="row">

                                    <div class="main-content  col-md-12 col-lg-12 my-mar-top2">

                                        <div class=" myjustify my-mar-lrb">


                                            <div class=" downloads">
                                                <%--<table class="table table-hover">

                                                    <tbody>

                                                        <tr>
                                                            <th>1</th>
                                                            <td>
                                                                <div class="row">

                                                                    <div class="col-lg-7 col-md-7 col-xs-10">
                                                                        <p>Download for XII Students Exam</p>
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-2 col-xs-6">
                                                                        <img src="misc/new-icon.gif" alt="" /></div>
                                                                    <div class="col-lg-3 col-md-3 col-xs-6"><a href="document/aa.docx" type="button" target="_blank" class="my-btn-mb"><i class="fa fa-download"></i>Download</a></div>

                                                                </div>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>2</th>
                                                            <td>
                                                                <div class="row">

                                                                    <div class="col-lg-7 col-md-7 col-xs-10">
                                                                        <p>Download foR Students</p>
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-2 col-xs-6">
                                                                        <img src="misc/new-icon.gif" alt="" /></div>
                                                                    <div class="col-lg-3 col-md-3 col-xs-6"><a href="document/aa.docx" type="button" target="_blank" class="my-btn-mb"><i class="fa fa-download"></i>Download</a></div>

                                                                </div>

                                                            </td>
                                                        </tr>

                                                    </tbody>
                                                </table>--%>
                                                <table class="table table-hover">
                                        <tbody>
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <th>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label></th>
                                                        <td>
                                                            <div class="row">

                                                                <div class="col-lg-9 col-md-9 col-xs-10">
                                                                    <p>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Titel") %>'></asp:Label>
                                                                    </p>
                                                                </div>
                                                                <div class="col-lg-1 col-md-1 col-xs-6">
                                                                    <img src="misc/new-icon.gif" alt="" />
                                                                </div>
                                                                <div class="col-lg-2 col-md-2 col-xs-6">
                                                                    <a href='<%# Eval("Path").ToString() %>' type="button" target="_blank" class="my-btn-mb">
                                                                        <i class="fa fa-download"></i>Download</a>
                                                                </div>
                                                            </div>

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
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
        </div>
    </div>
</asp:Content>

