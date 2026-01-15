<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmailIdPanelSetting.aspx.cs"
    Inherits="SuperAdmin_EmailIdPanelSetting" %>

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
                                <div class="col-sm-12 no-padding" runat="server">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label1" runat="server" class="control-label" > From E-mail Id<span class="vd_red">*</span></asp:Label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Can't leave blank!"
                                            SetFocusOnError="True" style="color: rgb(204, 0, 0); display: inline;" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label2" runat="server" CssClass="form-control-blue"> Password</asp:Label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                               
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 btn-a-devices-2-p2  mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" ValidationGroup="a" runat="server" CssClass="button   form-control-blue">Submit</asp:LinkButton>
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
