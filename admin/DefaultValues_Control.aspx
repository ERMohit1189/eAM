<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DefaultValues_Control.aspx.cs" Inherits="SuperAdmin_SetDefaultValue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>


            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding ">
                                    <div class="col-sm-4 mgbt-xs-15">
                                        <asp:DropDownList ID="drpDefaultvalueof" runat="server" OnSelectedIndexChanged="drpDefaultvalueof_SelectedIndexChanged" CssClass="form-control-blue" AutoPostBack="True">
                                            <asp:ListItem Text="<--Select-->" Value="<--Select-->"></asp:ListItem>
                                            <asp:ListItem Value="Country">Country</asp:ListItem>
                                            <asp:ListItem Value="State">State</asp:ListItem>
                                            <asp:ListItem Value="City">City</asp:ListItem>
                                            <asp:ListItem Value="Blood Group">Blood Group</asp:ListItem>
                                            <asp:ListItem Value="Board">Board/University</asp:ListItem>
                                            <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                            <asp:ListItem Value="Nationality">Nationality</asp:ListItem>
                                            <asp:ListItem Value="MotherTongue">Mother Tongue</asp:ListItem>
                                            <asp:ListItem Value="HomeTown">Home Town</asp:ListItem>
                                            <asp:ListItem Value="TypeofAdmission">Type of Admission</asp:ListItem>
                                            <asp:ListItem Value="Religion">Religion</asp:ListItem>
                                            <asp:ListItem Value="Category">Category</asp:ListItem>
                                            <asp:ListItem Value="Occupation">Occupation</asp:ListItem>
                                            <asp:ListItem Value="FeeGroup">Fee Category</asp:ListItem>
                                            <asp:ListItem Value="House">House</asp:ListItem>
                                            <asp:ListItem Value="Title">Title</asp:ListItem>
                                            <asp:ListItem Value="MaritalStatus">Marital Status</asp:ListItem>
                                           <%-- <asp:ListItem Value="ModeofDeposit">Mode of Deposit</asp:ListItem>--%>
                                            <asp:ListItem Value="ModeofPayment">Mode of Payment</asp:ListItem>
                                           <%-- <asp:ListItem Value="ModeofEducation">Mode of Education</asp:ListItem>--%>
                                          <%--  <asp:ListItem Value="SemesterType">Semester Type </asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4 mgbt-xs-15" runat="server" id="divVal" visible="false">
                                        <asp:DropDownList ID="drpDefaultvalue" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                        <asp:TextBox ID="txtDefaultvalue" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </div>
                                     <div class="col-sm-4" runat="server" id="divbtn" visible="false">
                                    <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
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

