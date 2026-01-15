<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="news_archive.aspx.cs" Inherits="website_news_archive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb-area ptb-25 broder-bottom">
			<div class="container">
				<div class="row">
					<div class="col-md-12">
						<div class="breadcrumb-list text-center text-uppercase">
							<ul>
								<li><a href="#">home</a><span class="divider"> // </span></li>
								<li class="active">News and Events</li>
							</ul>							
						</div>
					</div>
				</div>
			</div>
		</div>
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
                <asp:Repeater runat="server" ID="Repeater1">
                    <ItemTemplate>
                        <div class="client-testimonial-single bg-opacity-1">
                            <%--   <div class="ct-cp">
                                <img src='<%# Eval("NewsPhoto") %>' alt="" />
                            </div>--%>
                            <div class="client-say box-pd text-white">
                                <p>
                                    <h2><asp:Label ID="lblId" runat="server" Text='<%# Bind("SrNo") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblTitel" runat="server" Text='<%# Bind("NoticeHeading") %>'></asp:Label></h3></h2>
                                    <asp:Label ID="lblMessage" runat="server" Text='<%# Bind("NoticeMessage") %>'></asp:Label>
                                </p>
                                <div class="client-info">
                                    <h6>Date</h6>
                                    <p>
                                        <h4><asp:Label ID="lblNoticedate" runat="server" Text='<%# Bind("NoticeDate") %>'></asp:Label></h4>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>


    
</asp:Content>

