<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="website_feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="page-main-content" class="main-content  col-md-9 col-lg-9 sb-r">

        <div class="main-content-inner">

            <div class="content-main">
                <div class="region region-content">
                    <div id="block-system-main" class="block block-system no-title">
                        <div class="block-inner clearfix">

                            <div class="panel panel-danger">
                                <div class="panel-heading">FEEDBACK</div>

                                <div class="row">

                                    <div class="col-lg-12 col-md-12 col-sm-12  my-mar-top">
                                        <div class=" myjustify my-mar-lrb">

                                            <div class="contact-form">
                                                <asp:TextBox ID="TextBox1" placeholder="Full Name" size="60" Class="my-txt-box" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ErrorMessage="*" ForeColor="Red" ControlToValidate="TextBox1"
                                                    ValidationGroup="a" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="TextBox2" placeholder="Contact No." size="60" Class="my-txt-box" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ErrorMessage="*" ForeColor="Red" ControlToValidate="TextBox2"
                                                    ValidationGroup="a" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="TextBox3" placeholder="E-mail" size="60" Class="my-txt-box" runat="server"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                    ErrorMessage="Please enter valid Email!" ControlToValidate="TextBox3" ForeColor="Red"
                                                    ValidationExpression="[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|co.in|org|net|gov|mil|edu|biz|info|mobi|name|aero|jobs|museum|in)\b"
                                                    ValidationGroup="a" SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="TextBox4" placeholder="Subject" size="60" Class="my-txt-box" runat="server"></asp:TextBox>

                                                <asp:TextBox ID="TextBox5" TextMode="MultiLine" placeholder="Message" Class="my-dropdown txt-ped" Rows="5" runat="server"></asp:TextBox>



                                                <asp:Button ID="Button1" runat="server" Text="Send Message" OnClick="lnkSubmit_Click" ValidationGroup="a" class="webform-submit button-primary btn-primary btn form-submit" />

                                                <asp:Button ID="Button2" runat="server" Text="reset" OnClick="lnkReset_Click" class="webform-submit button-primary btn-primary btn form-submit btn-mar-top" />

                                                <div id="msgbox" runat="server" style="left: 147px !important;"></div>
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

