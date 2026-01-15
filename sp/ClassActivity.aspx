<%@ Page Title="Activity | eAM&#174;" Language="C#" MasterPageFile="~/sp/sp_root-manager.master" AutoEventWireup="true" CodeFile="ClassActivity.aspx.cs" Inherits="sp_ClassActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <%--<link href="../js/resource/blueimp-gallery.min.css" rel="stylesheet" />
    <script src="../js/resource/jquery.blueimp-gallery.min.js"></script>
     <script src="../js/jquery.min.js"></script>--%>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div runat="server" id="loader"></div>
    <script src="../js/jquery-1.4.3.min.js"></script>
    <script src="<%# ResolveUrl("~/js/MyScript.js") %>"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(prettyphoto);
            </script>
          
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12 ">
                                         <div class="table-responsive2 table-responsive">
                                            <table class="table table-striped no-bm table-hover no-head-border table-bordered pro-table text-center">
                                                <tr id="hr1" runat="server" style="display:none;">
                                                    <th>#</th>
                                                    <th>Title</th>
                                                    <th>Album Activity</th>
                                                </tr>
                                                <asp:Repeater ID="rpt1" runat="server">
                                                    <ItemTemplate>


                                                        <div class="col-md-4">
                                                            <div class="gallery-item activity-img-box">
                                                                <asp:HyperLink ID="lnkImage" runat="server" NavigateUrl='<%# Eval("FilePath") %>' data-rel="prettyPhoto[2]">
                                                                    <asp:Image ID="imgAlbum" runat="server" ImageUrl='<%# Eval("FilePath") %>' />
                                                                </asp:HyperLink>
                                                                
                                                               
                                                                <asp:Label ID="lblimgName" runat="server" Text='<%# Eval("FilePath") %>'
                                                                    Visible="false"></asp:Label>
                                                            </div>
                                                            <div class="activity-txt-box">
                                                                <asp:Label ID="lblsr" CssClass="hide" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>

                                                             <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                                            </div>

                                                            

                                                             
                                                       <%-- <tr>
                                                            <td>
                                                                <asp:Label ID="lblsr" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                                            </td>
                                                            <td class="width-20">
                                                                <asp:LinkButton ID="lnkImage" runat="server">
                                                                    <asp:Image ID="imgAlbum" CssClass="width-20" runat="server" ImageUrl='<%# Eval("FilePath") %>' />
                                                                </asp:LinkButton>
                                                                <asp:Label ID="lblimgName" runat="server" Text='<%# Eval("FilePath") %>'
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>--%>
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
    <style>
        a.pp_next {
            display: none !important;
        }

        a.pp_previous {
            display: none !important;
        }

        div.light_square .pp_gallery a.pp_arrow_previous, div.light_square .pp_gallery a.pp_arrow_next {
            display: none !important;
        }

        .pp_gallery div {
            display: none !important;
        }
    </style>
</asp:Content>

