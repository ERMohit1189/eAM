<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Start of slider area -->

    <div class="slider-area" runat="server" id="slidershow" visible="false">
        <div id="slider" class="nivoSlider">
            <asp:Repeater runat="server" ID="repeaterslider">
                <ItemTemplate>
                    <img style="display: none" src='<%# Eval("SliderPath")%>' runat="server" data-thumb='<%# Eval("SliderPath")%>' alt="" title="#htmlcaption1" />
                </ItemTemplate>
            </asp:Repeater>
            <%-- <img style="display: none" src="../images/slider/1.jpg" data-thumb="images/slider/1.jpg" alt="" title="#htmlcaption1" />
            <img style="display: none" src="../images/slider/2.jpg" data-thumb="images/slider/2.jpg" alt="" title="#htmlcaption2" />--%>
        </div>
    </div>

    <!-- End of slider area -->
    <!-- About Area Start -->
    <section class="about-area pt-40">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-title theame-color text-center">
                        <h1>ABOUT   COLLEGE</h1>
                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim nostrud exercitation ullamco laboris nisi.</p>
                    </div>
                </div>
            </div>
            <div class="row pt-50">
                <div class="col-md-6 col-sm-6 ">
                    <div class="specialty-single pb-50">
                        <div class="icon-titel">
                            <i class="fa fa-heart " aria-hidden="true"></i>
                            <h6>Great Support</h6>
                        </div>
                        <div class="spe-discribe">
                            <p>
                                Lorem ipsum dolor sit amet, consectetur ish  dipisicing elit, sed do eiusmod tempor lorem incididunt ut labore et
                                Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim nostrud exercitation ullamco laboris nisi.
                                Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim nostrud exercitation ullamco laboris nisi. .
                            </p>
                        </div>
                    </div>
                </div>

                <div class="col-md-6 col-sm-12">
                    <div class="specialty-image">
                        <img src="images/about.png" alt="" />
                    </div>
                </div>
            </div>


        </div>
    </section>
    <!-- About Area End -->
    <!-- Service Area Start -->
    <section class="service-area ptb-40">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-title theame-color text-center">
                        <h1>Best Features</h1>
                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim nostrud exercitation ullamco laboris nisi.</p>
                    </div>
                </div>
            </div>
            <div class="row pt-60">

                <div class="col-md-4 col-sm-6 ">
                    <div class="service-single text-center mb-30">
                        <div class="srvc-icon pb-30">
                            <i class="zmdi zmdi-smartphone-android"></i>
                        </div>
                        <div class="srvc-titel">
                            <h6>Best Library</h6>
                        </div>
                        <div class="service-hover text-center">
                            <div class="hvr-icon">
                                <i class="fa fa-paint-brush theame-color" aria-hidden="true"></i>
                                <h6>graphic design</h6>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicings, eli sed do eiusmod tempor incididunt ut labore et dolor magna aliqua. Ut enim ad minim</p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 col-sm-6 ">
                    <div class="service-single text-center mb-30">
                        <div class="srvc-icon pb-30">
                            <i class="zmdi zmdi-camera"></i>
                        </div>
                        <div class="srvc-titel">
                            <h6>Computer Lab</h6>
                        </div>
                        <div class="service-hover text-center">
                            <div class="hvr-icon">
                                <i class="fa fa-paint-brush theame-color" aria-hidden="true"></i>
                                <h6>graphic design</h6>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicings, eli sed do eiusmod tempor incididunt ut labore et dolor magna aliqua. Ut enim ad minim</p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 col-sm-6 ">
                    <div class="service-single text-center mb-30 res-tab-m0">
                        <div class="srvc-icon pb-30">
                            <i class="zmdi zmdi-book"></i>
                        </div>
                        <div class="srvc-titel">
                            <h6>Smart Class</h6>
                        </div>
                        <div class="service-hover text-center">
                            <div class="hvr-icon">
                                <i class="fa fa-paint-brush theame-color" aria-hidden="true"></i>
                                <h6>graphic design</h6>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicings, eli sed do eiusmod tempor incididunt ut labore et dolor magna aliqua. Ut enim ad minim</p>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>
    <!-- Service Area End -->
    <!-- News and Events -->

    <section class="client-testimonial-area ptb-40 bg-1 overlay" runat="server" id="news" visible="false">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-title theame-color text-center text-white">
                        <h1>News and Events</h1>
                    </div>
                </div>
            </div>
            <div class="client-testimonial-owl pt-55">
                <asp:Literal runat="server" ID="ltrnews"></asp:Literal>
                <asp:Repeater runat="server" ID="repeaternews1">
                    <ItemTemplate>
                        <div class="client-testimonial-single bg-opacity-1">
                            <%--   <div class="ct-cp">
                                <img src='<%# Eval("NewsPhoto") %>' alt="" />
                            </div>--%>
                            <div class="client-say box-pd text-white">
                                <p>
                                    <h2><%# Eval("NoticeHeading") %></h2>
                                    <%# Eval("NoticeMessage") %>
                                </p>
                                <div class="client-info">
                                    <h6>Date</h6>
                                    <p>
                                        <h4><%# Eval("MMM") %> <%# Eval("dd") %> , <%# Eval("yyyy") %></h4>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater runat="server" ID="repeaternews">
                    <ItemTemplate>
                        <div class="client-testimonial-single bg-opacity-1">
                            <%--   <div class="ct-cp">
                                <img src='<%# Eval("NewsPhoto") %>' alt="" />
                            </div>--%>
                            <div class="client-say box-pd text-white">
                                <p>
                                    <h2><%# Eval("NoticeHeading") %></h2>
                                    <%# Eval("NoticeMessage") %>
                                </p>
                                <div class="client-info">
                                    <h6>Date</h6>
                                    <p>
                                        <h4><%# Eval("MMM") %> <%# Eval("dd") %> , <%# Eval("yyyy") %></h4>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>


    <!-- Testimonial -->
    <section class="wework-area bg-2 pt-40 pb-40 overlay client-testimonial-area" itemid="testimonial" runat="server" id="testimonial" visible="false">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-title theame-color text-center text-white">
                        <h1>Testimonial</h1>
                    </div>
                </div>
            </div>
            <div class="client-testimonial-owl pt-55">
                <asp:Literal runat="server" ID="ltrtestimonial"></asp:Literal>
                <asp:Repeater runat="server" ID="reptestimonial1">
                    <ItemTemplate>
                        <div class="client-testimonial-single bg-opacity-1">
                            <div class="ct-cp circle">
                                <img src="<%# Eval("ImagePath").ToString()!=""?Eval("ImagePath").ToString():"../Uploads/pics/Student.ico" %> " alt="" />
                            </div>
                            <div class="client-say box-pd text-white">
                                <h4><%# Eval("StudentName") %></h4>
                                <h4><%# Eval("Class") %> | <%# Eval("Batch") %></h4>
                                <p><%# Eval("NoticeMessage")%></p>
                                <%--   <div class="client-info">
                                    <h6>John Doe</h6>
                                    <p>Project Manager</p>
                                </div>--%>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Repeater runat="server" ID="reptestimonial">
                    <ItemTemplate>
                        <div class="client-testimonial-single bg-opacity-1">
                            <div class="ct-cp circle">
                                <img src="<%# Eval("ImagePath").ToString()!=""?Eval("ImagePath").ToString():"../Uploads/pics/Student.ico" %> " alt="" />
                            </div>
                            <div class="client-say box-pd text-white">
                                <h4><%# Eval("StudentName") %></h4>
                                <h4><%# Eval("Class") %> | <%# Eval("Batch") %></h4>
                                <p><%# Eval("NoticeMessage")%></p>
                                <%--   <div class="client-info">
                                    <h6>John Doe</h6>
                                    <p>Project Manager</p>
                                </div>--%>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

    <!-- Admission Note -->

    <%--<section class="wework-area bg-3 pt-100 pb-120 overlay">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-title theame-color text-center text-white">
                        <h1>Admission Note</h1>
                    </div>
                </div>
            </div>
            <div class="row pt-80">
                <div class="col-md-12 text-white">
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
                </div>
            </div>
        </div>
    </section>--%>
    <!-- Featured Vedio -->

    <section class="ourblog-area bg-5 pt-40 pb-40 overlay" runat="server" id="featuredshowvideo" visible="false">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-title theame-color text-center text-white">
                        <h1>Featured Video</h1>
                    </div>
                </div>
            </div>
            <div class="row pt-55">
                <asp:Literal runat="server" ID="ltrfeatured" Visible="false"></asp:Literal>
                <div runat="server" id="divshow11">
                    <asp:Repeater runat="server" ID="repeaterfeaturevideo1" Visible="false">
                        <ItemTemplate>
                            <div class="col-md-4 col-sm-6  res-pb-xs-30">
                                <div class="single-blog">
                                    <asp:Literal runat="server" ID="lblfeature1" Text='<%# Eval("YouTubeIFrameURL") %>'></asp:Literal>
                                    <%--  <iframe src='<%# Eval("YouTubeFrame") %>' width="200px" height="120px"></iframe>--%>
                                    <%--  <iframe class="video-box" frameborder="0" webkitallowfullscreen="" mozallowfullscreen="" allowfullscreen="" runat="server" id="iframe1"></iframe>--%>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-md-8 col-md-offset-2 col-sm-12 ">
                    <asp:Literal runat="server" ID="ltrfeatured1" Visible="false"></asp:Literal>
                    <asp:Repeater runat="server" ID="repeaterfeaturevideo2">
                        <ItemTemplate>
                            <div class="col-md-6 col-sm-6  res-pb-xs-30">
                                <div class="single-blog">
                                    <asp:Literal runat="server" ID="lblfeature1" Text='<%# Eval("YouTubeIFrameURL") %>'></asp:Literal>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <div class="col-md4 col-md-offset-4 col-sm-4 col-sm-offset-4 ">
                    <asp:Literal runat="server" ID="ltrfeatured2" Visible="false"></asp:Literal>
                    <asp:Repeater runat="server" ID="repeaterfeaturevideo3" Visible="false">
                        <ItemTemplate>
                            <div class="col-md-12 col-sm-12  res-pb-xs-30">
                                <div class="single-blog">
                                       <asp:Literal runat="server" ID="lblfeature1" Text='<%# Eval("YouTubeIFrameURL") %>'></asp:Literal>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </div>
    </section>
    <!-- Gallery -->
    <section class="latestshot-area ptb-40">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-title theame-color text-center">
                        <h1>our latest shot</h1>

                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="portfolio-content">
                <ul class="text-center portfolio-menu mt-55 mb-60">
                    <li class="active" data-filter="*">All</li>
                    <li class="filter" data-filter=".c1">Mobile App design</li>
                    <li class="filter" data-filter=".c2">Graphic design</li>
                    <li class="filter" data-filter=".c3">Web design</li>
                    <li class="filter" data-filter=".c4">Branding design</li>
                    <li class="filter" data-filter=".c5">Photography</li>
                </ul>
                <!-- Latest Portfolio Tab Menu -->
                <div class="grid img-full portfolio-box">
                    <!-- Portfolio Single -->
                    <div class="grid-sizer grid-item c1">
                        <div class="single-portfolio">
                            <div class="image-box theame-bg">
                                <img src="images/portfolio/1.jpg" alt="" />
                                <a class="venobox" href="images/portfolio/b-1.jpg"><i class="zmdi zmdi-link"></i></a>
                            </div>
                        </div>
                    </div>
                    <!-- Portfolio Single -->
                    <!-- Portfolio Single -->
                    <div class="grid-item c2 c4">
                        <div class="single-portfolio">
                            <div class="image-box theame-bg">
                                <img src="images/portfolio/2.jpg" alt="" />
                                <a class="venobox" href="images/portfolio/b-2.jpg"><i class="zmdi zmdi-link"></i></a>
                            </div>
                        </div>
                    </div>
                    <!-- Portfolio Single -->
                    <!-- Portfolio Single -->
                    <div class="grid-item c1 c3">
                        <div class="single-portfolio">
                            <div class="image-box theame-bg">
                                <img src="images/portfolio/3.jpg" alt="" />
                                <a class="venobox" href="images/portfolio/b-3.jpg"><i class="zmdi zmdi-link"></i></a>
                            </div>
                        </div>
                    </div>
                    <!-- Portfolio Single -->
                    <!-- Portfolio Single -->
                    <div class="grid-item c3 c1 c5">
                        <div class="single-portfolio">
                            <div class="image-box theame-bg">
                                <img src="images/portfolio/4.jpg" alt="" />
                                <a class="venobox" href="images/portfolio/b-4.jpg"><i class="zmdi zmdi-link"></i></a>
                            </div>
                        </div>
                    </div>
                    <!-- Portfolio Single -->
                    <!-- Portfolio Single -->
                    <div class="grid-item c4 c5 c3 c1">
                        <div class="single-portfolio">
                            <div class="image-box theame-bg">
                                <img src="images/portfolio/5.jpg" alt="" />
                                <a class="venobox" href="images/portfolio/b-5.jpg"><i class="zmdi zmdi-link"></i></a>
                            </div>
                        </div>
                    </div>
                    <!-- Portfolio Single -->
                </div>
            </div>
        </div>
    </section>



    <%--<div class="brandlogo-area pb-100">
        <div class="container">
            <div class="row">
                <div class="brand-carsoul">
                    <!-- Brand Single -->
                    <div class="col-md-12">
                        <div class="brand-single">
                            <a href="#">
                                <img src="images/clients/1.jpg" alt="" /></a>
                        </div>
                    </div>
                    <!-- Brand Single -->
                    <!-- Brand Single -->
                    <div class="col-md-12">
                        <div class="brand-single">
                            <a href="#">
                                <img src="images/clients/2.jpg" alt="" /></a>
                        </div>
                    </div>
                    <!-- Brand Single -->
                    <!-- Brand Single -->
                    <div class="col-md-12">
                        <div class="brand-single">
                            <a href="#">
                                <img src="images/clients/3.jpg" alt="" /></a>
                        </div>
                    </div>
                    <!-- Brand Single -->
                    <!-- Brand Single -->
                    <div class="col-md-12">
                        <div class="brand-single">
                            <a href="#">
                                <img src="images/clients/4.jpg" alt="" /></a>
                        </div>
                    </div>
                    <!-- Brand Single -->
                    <!-- Brand Single -->
                    <div class="col-md-12">
                        <div class="brand-single">
                            <a href="#">
                                <img src="images/clients/5.jpg" alt="" /></a>
                        </div>
                    </div>
                    <!-- Brand Single -->
                    <!-- Brand Single -->
                    <div class="col-md-12">
                        <div class="brand-single">
                            <a href="#">
                                <img src="images/clients/6.jpg" alt="" /></a>
                        </div>
                    </div>
                    <!-- Brand Single -->
                    <!-- Brand Single -->
                    <div class="col-md-12">
                        <div class="brand-single">
                            <a href="#">
                                <img src="images/clients/4.jpg" alt="" /></a>
                        </div>
                    </div>
                    <!-- Brand Single -->
                </div>
            </div>
        </div>
    </div>--%>
    <script>
        hidesection('news');
        hidesection('testimonial');
    </script>
</asp:Content>

