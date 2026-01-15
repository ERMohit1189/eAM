<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="photo-gallery.aspx.cs" Inherits="website_photo_gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="latestshot-area ptb-100">
			<div class="container-fluid">
				<div class="portfolio-content">
					<!-- Latest Portfolio Tab Menu -->
					<ul class="text-center portfolio-menu mb-60">
						<li class="" data-filter="*">All</li>
						<li class="filter" data-filter=".c1">Mobile App design</li>
						<li class="filter" data-filter=".c2">Graphic design</li>
						<li class="filter" data-filter=".c3">Web design</li>
						<li class="filter" data-filter=".c4">Branding design</li>
						<li class="filter active" data-filter=".c5">Photography</li>
					</ul>
					<!-- Latest Portfolio Tab Menu -->
					<div class="grid img-full portfolio-box" style="position: relative; height: 660px;">
						<!-- Portfolio Single -->
						<div class="grid-sizer grid-item c1" style="position: absolute; left: 0%; top: 0px; display: none;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/1.jpg" alt="">
									<a class="venobox" href="../images/portfolio/1.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c2 c4" style="position: absolute; left: 0%; top: 0px; display: none;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/2.jpg" alt="">
									<a class="venobox" href="../images/portfolio/2.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c1 c3" style="position: absolute; left: 0%; top: 0px; display: none;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/3.jpg" alt="">
									<a class="venobox" href="../images/portfolio/3.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c3 c1 c5" style="position: absolute; left: 0%; top: 0px;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/4.jpg" alt="">
									<a class="venobox" href="../images/portfolio/4.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c4 c5 c3 c1" style="position: absolute; left: 19.9621%; top: 0px;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/5.jpg" alt="">
									<a class="venobox" href="../images/portfolio/5.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c4 c1" style="position: absolute; left: 39.9874%; top: 0px; display: none;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/5.jpg" alt="">
									<a class="venobox" href="../images/portfolio/5.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c4 c5 c3" style="position: absolute; left: 39.9874%; top: 0px;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/5.jpg" alt="">
									<a class="venobox" href="../images/portfolio/5.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c5 c3" style="position: absolute; left: 59.9495%; top: 0px;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/4.jpg" alt="">
									<a class="venobox" href="../images/portfolio/5.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c4 c2 c1" style="position: absolute; left: 79.9747%; top: 0px; display: none;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/3.jpg" alt="">
									<a class="venobox" href="../images/portfolio/5.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c4 c5 c2" style="position: absolute; left: 79.9747%; top: 0px;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/2.jpg" alt="">
									<a class="venobox" href="../images/portfolio/5.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c4 c3 c2" style="position: absolute; left: 19.9621%; top: 330px; display: none;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/1.jpg" alt="">
									<a class="venobox" href="../images/portfolio/5.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c4 c5 c1" style="position: absolute; left: 0%; top: 330px;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/2.jpg" alt="">
									<a class="venobox" href="../images/portfolio/5.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c5 c3 c1" style="position: absolute; left: 19.9621%; top: 330px;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/3.jpg" alt="">
									<a class="venobox" href="../images/portfolio/5.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c2 c3 c1" style="position: absolute; left: 39.9874%; top: 330px; display: none;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/4.jpg" alt="">
									<a class="venobox" href="../images/portfolio/5.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
						<!-- Portfolio Single -->
						<div class="grid-item c4 c5 c3 c1" style="position: absolute; left: 39.9874%; top: 330px;">
							<div class="single-portfolio">
								<div class="image-box">
									<img src="../images/portfolio/5.jpg" alt="">
									<a class="venobox" href="../images/portfolio/5.jpg"><i class="zmdi zmdi-link"></i></a>
								</div>
							</div>
						</div>
						<!-- Portfolio Single -->
					</div>
				</div>
				<!-- Pagination Count Area Strat -->
				<div class="container">
					<div class="row">
						<div class="col-md-12">
							<div class="pagination-count text-center broder ptb-15 mt-60 white-bg">
								<ul>
									<li><a href="#"><i class="zmdi zmdi-caret-left"></i></a></li>
									<li><a href="#">1</a></li>
									<li><a href="#">2</a></li>
									<li><a href="#">3</a></li>
									<li><a href="#">4</a></li>
									<li><a href="#"><i class="zmdi zmdi-caret-right"></i></a></li>
								</ul>
							</div>
						</div>
					</div>
				</div>
				<!-- Pagination Count Area End -->
			</div>
		</div>
    <div id="page-main-content" class="main-content  col-md-9 col-lg-9 sb-r">

        <div class="main-content-inner">

            <div class="content-main">
                <div class="region region-content">
                    <div id="block-system-main" class="block block-system no-title">
                        <div class="block-inner clearfix">


                            <div class="panel panel-danger">
                                <div class="panel-heading">PHOTO GALLERY</div>

                                <div class="row">




                                    <div class="col-lg-12 col-md-12 col-sm-12  ">



                                        <div class="pane-content">
                                            <div class="view view-gallery view-id-gallery view-display-id-gallery_v2 gallery-grid view-dom-id-d857a7b6595952743ad6f08ca03945bd">

                                                <div class="view-content">
                                                    <div>


                                                        <div class=" margin-bottom-30">
                                                            <asp:Repeater ID="Repeater1" runat="server">
                                                                <ItemTemplate>
                                                                    <div class="col-lg-4 col-md-4 col-sm-6  mar-bottom">
                                                                        <div class="gallery-large img-box">
                                                                            <div class="content">
                                                                                <asp:ImageButton ID="ImageButton1" runat="server"
                                                                                    ImageUrl='<%# Bind("ImagePath") %>' AlternateText='<%# Bind("AlbumName") %>'
                                                                                    OnClick="ImageButton1_Click" CssClass="img-thumbnail form-control-pink" Width="100%" Height="200" />

                                                                                <div class="txt-center body">
                                                                                    <h2 class="txt-mar2">
                                                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("AlbumName") %>' OnClick="LinkButton1_Click"></asp:LinkButton>
                                                                                    </h2>
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
    </div>
</asp:Content>

