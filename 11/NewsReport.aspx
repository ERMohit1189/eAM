<%@ Page Title=" Bulletin Board | eAM" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="NewsReport.aspx.cs"
    Inherits="News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                        GridLines="None" ShowHeader="False" SkinID="san">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <table style="text-align: left" width="100%">
                                                        <tr>
                                                            <td>
                                                                <h5 class="vd_red mgbt-xs-5">Date:
                                                                    <asp:Label ID="Lbdate" runat="server" CssClass="right_menu"
                                                                        Text='<%# Bind("NoticeDate") %>'></asp:Label>
                                                                     <asp:Label ID="lblUploadedby" runat="server" CssClass="right_menu"
                                                                            Text='<%# Bind("LoginName") %>'></asp:Label>

                                                                </h5>

                                                                <h4 class="vd_black mgbt-xs-5 text-justify"
                                                                    style="font-size: 16px !important;">
                                                                    <asp:Label ID="LbleHead" runat="server" CssClass="date"
                                                                        Text='<%# Bind("NoticeHeading") %>'></asp:Label></h4>

                                                                <p class=" mgbt-xs-5 text-justify">
                                                                   
                                                                    <asp:Label ID="blMess" runat="server" CssClass="right_menu"
                                                                        Text='<%# Bind("NoticeMessage") %>'></asp:Label>
                                                                    
                                                                </p>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>

                                                <HeaderStyle BorderStyle="None" />

                                            </asp:TemplateField>
                                        </Columns>

                                        <FooterStyle BorderStyle="None" />
                                        <HeaderStyle BorderStyle="None" />

                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
