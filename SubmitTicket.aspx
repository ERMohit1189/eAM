<%@ Page Title="" Language="C#" MasterPageFile="~/root-manager.master" AutoEventWireup="true" CodeFile="SubmitTicket.aspx.cs" Inherits="SubmitTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12 no-padding">
                            <div class="col-sm-12 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="lblClassSection" runat="server" class="col-sm-4 text-right txt-bold " Text="Select Ticket"></asp:Label>
                                    <div class="col-sm-6 mgbt-xs-15">
                                        
                                        <asp:RadioButtonList ID="selticket" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_radio radio-success" >
                                            <asp:ListItem Text="Submit Ticket" Value="ST"></asp:ListItem>
                                            <asp:ListItem Text="My Ticket" Value="MT"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 no-padding">
                            <div class="col-sm-12 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label1" runat="server" class="col-sm-4 text-right txt-bold txt-middle-l" Text="Select Categary (Optional)"></asp:Label>
                                    <div class="col-sm-6  mgbt-xs-15">
                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control-blue" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                         <div class="col-sm-12 no-padding">
                            <div class="col-sm-12 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label5" runat="server" class="col-sm-4 text-right txt-bold txt-middle-l" Text="Subject"></asp:Label>
                                    <div class="col-sm-6  mgbt-xs-15">
                                        <asp:TextBox ID="TextBox3" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 no-padding">
                            <div class="col-sm-12 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label2" runat="server" class="col-sm-4 text-right txt-bold txt-middle-l" Text="Describe Your Ticket (Optional)"></asp:Label>
                                    <div class="col-sm-6  mgbt-xs-15">
                                        <asp:TextBox ID="TextBox1" CssClass="form-control-blue" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 no-padding">
                            <div class="col-sm-12 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label3" runat="server" class="col-sm-4 text-right txt-bold txt-middle-l" Text="Your Email Address"></asp:Label>
                                    <div class="col-sm-6   mgbt-xs-15">
                                        <asp:TextBox ID="TextBox2" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 no-padding">
                            <div class="col-sm-12 no-padding ">
                                <div class="form-group ">
                                    <asp:Label ID="Label4" runat="server" class="col-sm-4 text-right txt-bold txt-middle-l" Text="Attachment File"></asp:Label>
                                    <div class="col-sm-6  mgbt-xs-15">
                                        <asp:FileUpload ID="FileUpload1" CssClass="form-control-blue" runat="server" />
                                    </div>
                                    <div class="col-sm-2 mgbt-xs-15">
                                        <asp:LinkButton runat="server" ID="submit" CssClass="button form-control-blue">Submit</asp:LinkButton>
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

