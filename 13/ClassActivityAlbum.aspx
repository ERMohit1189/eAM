<%@ Page Title="Activity Album | eAM&#174;" Language="C#" MasterPageFile="stuRootManager.master" AutoEventWireup="true" 
CodeFile="ClassActivityAlbum.aspx.cs" Inherits="sp_ClassActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div runat="server" id="loader"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12 ">
                                        <div class="table-responsive2 table-responsive">
                                            <table class="table table-striped no-bm table-hover no-head-border table-bordered pro-table text-center">
                                                <tr id="hr1" runat="server" style="display:none">
                                                    <th>#</th>
                                                    <th>Album Name</th>
                                                    <th>Cover Page</th>
                                                </tr>
                                                <asp:Repeater ID="rpt1" runat="server">
                                                    <ItemTemplate>

                                                        <div class="col-md-4">
                                                            <div class="activity-img-box">
                                                                <asp:LinkButton ID="lnkImage" runat="server" OnClick="lnkImage_Click">
                                                                    <asp:Image ID="imgAlbum" runat="server" ImageUrl='<%# Eval("FilePath") %>' />
                                                                </asp:LinkButton>
                                                                <asp:Label ID="lblimgName" runat="server" Text='<%# Eval("FilePath") %>'
                                                                    Visible="false"></asp:Label>
                                                            </div>
                                                            <div class="activity-txt-box">
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblsr" runat="server" CssClass="hide" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                               <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lnktitle_Click"><%# Eval("Title") %></asp:LinkButton>
                                                            </div>
                                                        </div>


                                                      
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

