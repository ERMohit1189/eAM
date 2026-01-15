<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="testimonials.aspx.cs" Inherits="website_testimonials" %>

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
                                <div class="panel-heading">TESTIMONIALS</div>
                                <div class="row">

                                    <%--<div class="main-content  col-md-12 col-lg-12 my-mar-top2">
                                        <div class="col-lg-6">
                                                <div class="testimonial-list-pic">
                                                    <img src="sites/default/files/teacher.jpg"  />
                                                </div>

                                         </div>
                                         <div class="col-lg-6">
                                                <div class="testimonial-use-title">
                                                    <h3>Amit Kashyap</h3>
                                                    <p>MCA</p>
                                                    <p>2015-2016</p>
                                                </div>
                                         </div>
                                        <div class="col-lg-12">
                                                <div class="testimonial-use-text">
                                                    
                                                    <p>lorem ipsum dolor sit amet, consectetur adipiscing lorem ipsum dolor sit amet, consectetur adipiscing lorem ipsum
                                                         dolor sit amet, consectetur adipiscing lorem ipsum dolor sit amet, consectetur adipiscing lorem ipsum dolor sit
                                                         amet, consectetur adipiscing</p>
                                                    
                                                </div>
                                         </div>
                                      </div>--%>


                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <ItemTemplate>
                                            <div class="main-content  col-md-12 col-lg-12 my-mar-top2">
                                                <div class="col-lg-6">
                                                    <div class="testimonial-list-pic">
                                                        <asp:Image ID="Image1" ImageUrl='<%# Eval("ImagePath")!= "" ?Eval("ImagePath").ToString():"~/uploads/pics/Student.ico" %>' runat="server" />
                                                    </div>

                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="testimonial-use-title">
                                                        <h3><%# Eval("StudentName") %></h3>
                                                        <p><%# Eval("Class") %></p>
                                                        <p><%# Eval("Batch") %></p>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="testimonial-use-text">

                                                        <p><%# Eval("NoticeMessage") %></p>

                                                    </div>
                                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("NoticeId") %>' Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="main-content  col-md-12 col-lg-12 my-mar-top2">
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

