<%@ Page Title="Contact Us | eAM" Language="C#" MasterPageFile="mainblank.master" AutoEventWireup="true" CodeFile="Contact_Us.aspx.cs" Inherits="Contact_Us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <style>
        /*---------------------------------------
  Contact section              
-----------------------------------------*/

        .contact-info .fa {
            padding-right: 5px;
        }

        #contact .form-control {
            border: none;
            border-bottom: 2px solid #f0f0f0;
            border-radius: 0px;
            box-shadow: none;
            font-size: 18px;
            margin-top: 10px;
            margin-bottom: 10px;
            -webkit-transition: all ease-in-out 0.4s;
            transition: all ease-in-out 0.4s;
        }

            #contact .form-control:focus {
                border-bottom-color: #999999;
            }

        #contact input {
            height: 55px;
        }

        #contact button#submit {
            background: #2b2b2b;
            border: none;
            border-radius: 50px;
            color: #ffffff;
            height: 50px;
            margin-top: 24px;
        }

            #contact button#submit:hover {
                background: #7682cc;
                color: #ffffff;
            }
    </style>
<section class="default-section sec-padd6" style="margin-bottom: 30px;">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-title">
                        <h2 style="font-size: 30px;">
                            <br />
                                Get in touch</h2>
                           <hr />
                        <span class="decor"></span>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="col-sm-3">
                        <div class="">
                            <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12">
                    <br />
                    <div class="col-sm-8" runat="server" id="divDetails" visible="false">
                        <!-- CONTACT INFO -->
                        <div class="wow fadeInUp contact-info" data-wow-delay="0.4s">
                            <p><b><i class="fa fa-building"></i>
                                <asp:Label runat="server" ID="instituteName"></asp:Label></b></p>
                            <p><i class="fa fa-map-marker"></i>
                                <asp:Label runat="server" ID="address"></asp:Label></p>
                            <p><i class="fa fa-envelope"></i>
                                <asp:Label runat="server" ID="email"></asp:Label></p>
                            <p><i class="fa fa-phone"></i>
                                <asp:Label runat="server" ID="Phone"></asp:Label></p>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>

</asp:Content>

