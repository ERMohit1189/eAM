<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AlumniRegistrationDetails.aspx.cs" Inherits="website_AdmissionEnquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style>
        .x-navigation li.active2 > a .fa,
        .x-navigation li.active2 > a .glyphicon {
            color: #ffd559;
        }

        .x-navigation li.active21 > a .fa,
        .x-navigation li.active21 > a .glyphicon {
            color: #ffd559;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script>
                

            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-4">
                                       <h3 ><strong>Alumni </strong> Registration List</h3>
                                </div>
                                 <div class="col-sm-8">
                                      <div id="msgbox" runat="server" style="left: 147px !important;"></div>
                                </div>
                               
                                <div class="col-sm-12 form-group form-group-sm">
                                
                   <asp:Repeater ID="Repeater1" runat="server">
                                         
                                   <HeaderTemplate>
                                           
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li class="boxborder">
                                                <div class="row  ">
                                                    <div class="col-lg-1 col-md-2  ">
                                                        <b><span class="panel-title3  hide-title" style="font-size:15px;">S.No.</span></b>
                                                        <asp:Label ID="lblsr" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-5  ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">Name :</span></b>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-5 ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">Father Name :</span></b>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("FName") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-2  ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">Class :</span></b>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("PClass") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-5  ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">Batch :</span></b>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("PYear") %>'></asp:Label>
                                                    </div>
                                                     <div class="col-lg-3 col-md-5 ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">Contact :</span></b>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Contact") %>'></asp:Label>
                                                    </div>

                                                    <div class="col-lg-3 col-md-7 ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">Email ID :</span></b>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-5  ">
                                                      <b>   <span class="panel-title3  hide-title" style="font-size:15px;">Address :</span></b>
                                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-7  ">
                                                        <b> <span class="panel-title3  hide-title" style="font-size:15px;">Country :</span></b>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-5  ">
                                                       <b>  <span class="panel-title3 hide-title" style="font-size:15px;">State :</span></b>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-3 col-md-7  ">
                                                       <b>  <span class="panel-title3  hide-title" style="font-size:15px;">City :</span></b>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-3 col-md-5  ">
                                                       <b>  <span class="panel-title3  hide-title" style="font-size:15px;">Profession :</span></b>
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("Profession") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-7  ">
                                                       <b>  <span class="panel-title3  hide-title" style="font-size:15px;">Company :</span></b>
                                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("Cmpy") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2 col-md-5  ">
                                                      <b>   <span class="panel-title3  hide-title" style="font-size:15px;">Designtion :</span></b>
                                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("Desig") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-4 col-md-12  ">
                                                       <b>  <span class="panel-title3  hide-title" style="font-size:15px;">Comment :</span></b>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-1 col-md-6 col-xs-6 " >
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Bind("Photo") %>' Width="100px" Height="100px" />
                                                    </div>
                                                    <div class="col-lg-12 col-md-6 col-xs-6">
     <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" title="Delete"  class="btn menu-icon vd_bd-red vd_red" OnClick="LinkButton1_Click"><i class="glyphicon glyphicon-trash"></i> <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Bind("Id") %>'></asp:Label></asp:LinkButton>
                                                    </div>
                                                </div>
                                               
                                            </li>
                                        </ItemTemplate>

                                                </asp:Repeater>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                            <table class="tab-popup text-center">

                                <tr>
                                    <td style="text-align: center">
                                        <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                                        </h4>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" />
                                        &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button-y" Text="Yes" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                            PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>


                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

