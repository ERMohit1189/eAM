<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/administrato_root-manager.master" AutoEventWireup="true" CodeFile="Brand.aspx.cs" Inherits="BrandName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
          <div id="loader" runat="server"></div>  <%-- ==== in aspx file   --%>                            

<asp:UpdatePanel ID ="tyu" runat="server">
        <ContentTemplate>


    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-lg-12 no-padding">

                            <div class="col-sm-4   half-width-50 mgbt-xs-9">
                                <label class="control-label">Brand Name</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtBrandName" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    <div class="text-box-msg">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBrandName" ErrorMessage="*"
                                            Style="color: #CC0000"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button">Submit</asp:LinkButton>
                           <div id="msgbox" runat="server" style="left: 59px;"></div>

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
