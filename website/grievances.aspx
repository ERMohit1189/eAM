<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="grievances.aspx.cs" Inherits="website_grievances" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="map-area-main two">

        <div class="container">
            <div class="row pb-100">
                <!-- Contact Left Area Start -->
                <div class="col-md-12">
                    <div class=" white-bg shadow ptb-100" style="padding: 20px 50px">
                        <div class="section-title text-center">
                            <h4>Leave a Massage</h4>
                        </div>
                        <form id="contact-form" class="form-group">
                            <div class="form-single">
                                <asp:TextBox ID="TextBox1" placeholder="Full Name" size="60" Class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ErrorMessage="*" ForeColor="Red" ControlToValidate="TextBox1"
                                    ValidationGroup="a" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-single">
                                <asp:TextBox ID="TextBox2" placeholder="Contact No." size="60" Class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ErrorMessage="*" ForeColor="Red" ControlToValidate="TextBox2"
                                    ValidationGroup="a" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-single">
                                <asp:TextBox ID="TextBox3" placeholder="E-mail" size="60" Class="form-control" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ErrorMessage="Please enter valid Email!" ControlToValidate="TextBox3" ForeColor="Red"
                                    ValidationExpression="[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|co.in|org|net|gov|mil|edu|biz|info|mobi|name|aero|jobs|museum|in)\b"
                                    ValidationGroup="a" SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>
                            </div>
                            <div class="form-single">
                                <asp:TextBox ID="TextBox4" placeholder="Subject" size="60" Class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-single">
                            </div>
                            <div class="form-textarea">
                                <asp:TextBox ID="TextBox5" TextMode="MultiLine" placeholder="Grievances" Class="my-dropdown txt-ped" Rows="5" runat="server"></asp:TextBox>
                            </div>
                            <div class="contact-button">
                                <asp:Button ID="Button1" runat="server" Text="Send Message" OnClick="lnkSubmit_Click" ValidationGroup="a" class="contact-submit" />

                                <asp:Button ID="Button2" runat="server" Text="reset" OnClick="lnkReset_Click" class="contact-submit" />

                                <div id="msgbox" runat="server" style="left: 147px !important;"></div>
                                <p class="form-messege"></p>
                            </div>
                        </form>
                    </div>
                </div>

            </div>
        </div>
    </section>

    <div class="contact-form">
    </div>

</asp:Content>

