<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="downloads.aspx.cs" Inherits="website_downloads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div role="main" class="main main-page" style="margin: 30px 0px;">
        <%--End Left Side--%>

        <div id="content" class="content content-full">

            <div class="container">

                <div class="panel panel-danger">
                    <div class="panel-heading">DOWNLAODS</div>
                    <div class="row">

                        <div class="main-content  col-md-12 col-lg-12 my-mar-top2">

                            <div class=" myjustify my-mar-lrb">


                                <div class=" downloads">
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
</asp:Content>

