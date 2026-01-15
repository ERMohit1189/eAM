<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="toppers.aspx.cs" Inherits="toppers" %>

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
                            <li class="active">Toppers</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <section class="choose-us ptb-100">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="specialty-single hidden-sm">
                        <div class="icon-titel">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>

                                    <asp:Label ID="Label2" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label></th>
                                                        
                                     <h6><asp:Label ID="Label1" runat="server" Text='<%# Bind("Titel") %>'></asp:Label>

                                    <a href='<%# Eval("Path").ToString() %>' type="button" target="_blank" class="my-btn-mb">
                                        <i class="fa fa-download"></i>Download</a> </h6>

                                </ItemTemplate>
                            </asp:Repeater>
                           
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>


    
</asp:Content>

