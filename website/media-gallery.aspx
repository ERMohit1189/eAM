<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="media-gallery.aspx.cs" Inherits="website_media_gallery" %>

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
                                <div class="panel-heading">
                                    MEDIA GALLERY
                                </div>

                                <div class="row">


                                    <div class="col-lg-12 col-md-12 col-sm-12  ">



                                        <div class="pane-content">
                                            <div class="view view-gallery view-id-gallery view-display-id-gallery_v2 gallery-grid view-dom-id-d857a7b6595952743ad6f08ca03945bd">

                                                <div class="view-content">
                                                    <div>

                                                        <asp:Repeater ID="Repeater1" runat="server">
                                                            <ItemTemplate>
                                                                <div class=" margin-bottom-30">

                                                                    <div class="col-lg-4 col-md-4 col-sm-6  mar-bottom">

                                                                        <div class="gallery-large img-box">
                                                                            <div class="item content">
                                                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Bind("ImagePath") %>'>
                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl='<%# Bind("ImagePath") %>' CssClass="img-thumbnail" Width="257" Height="200" /></asp:Image>
                                                                                </asp:HyperLink>

                                                                                <div class="description">
                                                                                    <div class="xcolor">

                                                                                        <a href='<%# Eval("ImagePath").ToString().Remove(0,2) %>' data-rel="prettyPhoto[g_gal]">
                                                                                            <i class="fa fa-search-plus">&nbsp;</i></a>
                                                                                    </div>
                                                                                    <div class="body">
                                                                                        <a>
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Title") %>'></asp:Label></a>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </div>


                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>


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
        </div>
    </div>
</asp:Content>

