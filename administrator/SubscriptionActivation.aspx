<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/administrato_root-manager.master" AutoEventWireup="true" CodeFile="SubscriptionActivation.aspx.cs" Inherits="SubscriptionActivation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-lg-12 no-padding">
                                    <div class="col-sm-3   half-width-50 mgbt-xs-9">
                                        <asp:Label ID="Label1" runat="server" class="control-label" Text="Emp. Code"></asp:Label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3   half-width-50 mgbt-xs-9">
                                        <asp:Label ID="Label4" runat="server" class="control-label" Text="Name"></asp:Label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3   half-width-50 mgbt-xs-9">
                                        <asp:Label ID="Labels2" runat="server" class="control-label" Text="D.O.B.">*</asp:Label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtdob" runat="server" CssClass="form-control-blue  datepicker-normal validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-3   half-width-50 mgbt-xs-9">
                                        <asp:Label ID="Label2" runat="server" class="control-label" Text="Contact No."></asp:Label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtContact" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" OnClick="LinkButton1_Click" CssClass="button">Submit</asp:LinkButton>
                                         <div id="msgbox" runat="server" style="left: 75px;"></div>

                                    </div>
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

