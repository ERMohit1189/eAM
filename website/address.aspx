<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="address.aspx.cs" Inherits="website_address" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb-area ptb-25 broder-bottom">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="breadcrumb-list text-center text-uppercase">
                        <ul>
                            <li><a href="index.html">home</a><span class="divider"> // </span></li>
                            <li class="active">Contact Us</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <section class="map-area-main two">
        <div class="container">
            <div class="row pb-100">
                <div class="col-md-6">
                    <div class="map-area">
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d14245.049242909463!2d80.87659194197678!3d26.799775144097257!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x399c0ef8377d07df%3A0x53553a196f24a5b4!2sRED+ROSE+PUBLIC+SENIOR+SECONDARY+SCHOOL!5e0!3m2!1sen!2sin!4v1445428180059" class="my-iframe" style="width:100%; height:400px;margin:50px 0 0 0 "></iframe>

                    </div>
                </div>
                <!-- Contact Left Area Start -->
                <!-- Contact Right Area Start -->
                <div class="col-md-6">
                    <div class="cntct-right-adrs pl-80 pt-90">
                        <div class="section-title text-left">
                            <h4>Contact Us</h4>
                        </div>
                        <div class="adrs-details mt-60">
                            <ul>
                                <li>
                                    <a href="#"><i class="zmdi zmdi-pin"></i></a>
                                    <p>Vishnu Lok Colony, Kanpur Road, <br /> Lucknow</p>
                                </li>
                                <li>
                                    <a href="#"><i class="zmdi zmdi-email"></i></a>
                                    <p>+91 522-247-1091, 247-1092 , <br /> +91-995-620-8333</p>
                                </li>
                                <li>
                                    <a href="#"><i class="zmdi zmdi-phone"></i></a>
                                    <p>info@redrosepublicschool.edu.in
                                        <br />
                                        www.redrosepublicschool.edu.in
                                    </p>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Contact Right Area End -->
            </div>
        </div>
    </section>



    
</asp:Content>

