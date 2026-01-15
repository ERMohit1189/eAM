<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="tc.aspx.cs" Inherits="website_tc" %>

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
                                <div class="panel-heading">DOWNLOAD TC</div>

                                <div class="row">




                                    <div class="col-lg-12 col-md-12 col-sm-12  my-mar-top">
                                        <div class=" myjustify my-mar-lrb">

                                            <div class="contact-form">
                                                <asp:TextBox ID="txtSrno" placeholder="Enter S.R No." size="60" Class="my-txt-box" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please fill student S.R. No.!" ControlToValidate="txtSrno" CssClass="text-danger" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                <asp:Button ID="Button1" runat="server" Text="Show tc" class="webform-submit button-primary btn-primary btn form-submit" OnClick="Button1_Click" ValidationGroup="a" />

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-12 col-md-12 col-sm-12  my-mar-top">
                                        <div class=" myjustify my-mar-lrb">
                                            <iframe height="1000px" width="100%" id="ifram1" runat="server"></iframe>

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

