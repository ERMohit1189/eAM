<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DownloadResume.aspx.cs" Inherits="website_DownloadResume" %>

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
                                <div class="col-sm-12  no-padding">
                                    <label class="control-label">Select Job Title&nbsp;<span class="vd_red">*</span></label>
                                    <div class="input-group ">
                                        <asp:DropDownList ID="DropDownList1" runat="server" class="form-control-blue" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true" Style="width: 100%;">
                                        </asp:DropDownList>
                                         <div runat="server" id="msgbox"></div>
                                        
                                    </div>
                                </div>
                                <div class="col-sm-12 form-group form-group-sm" runat="server" id="divList">
                                    <div class="table-responsive2 table-responsive">
                                        <h3><strong>Download </strong>Resume List</h3>
                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue-np vd_white-np">S.No.</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Date</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Name</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Mobile No.</th>
                                                    <th class="vd_bg-blue-np vd_white-np">E-mail</th>
                                                    <th class="vd_bg-blue-np vd_white-np">How Know </th>
                                                    <th class="vd_bg-blue-np vd_white-np">Applied For</th>
                                                    <%-- <th class="vd_bg-blue-np vd_white-np">Dscription</th>--%>

                                                    <th class="vd_bg-blue-np vd_white-np">Download</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Delete</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label></div>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("AppDate") %>'></asp:Label></div>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Name") %>'></asp:Label></div>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("MobileNo") %>'></asp:Label></div>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("HowUknow") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("AppliedFor") %>'></asp:Label>
                                                            </td>
                                                            <%--   <td>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("AboutU") %>'></asp:Label>
                                                            </td>--%>

                                                            <td>
                                                                <a href='<%# Eval("CvPath").ToString().Replace("~","..") %>' type="button" target="_blank" class="btn menu-icon vd_bd-green vd_green">
                                                                    <i class="fa fa-download"></i>Download CV</a>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" title="Delete"  class="btn menu-icon vd_bd-red vd_red" OnClick="LinkButton1_Click">
                                                                    <i class="glyphicon glyphicon-trash"></i>
                                                                    <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Bind("AppId") %>'></asp:Label>
                                                                </asp:LinkButton>

                                                            </td>
                                                        </tr>

                                                    </ItemTemplate>
                                                </asp:Repeater>


                                            </tbody>
                                        </table>

                                    </div>
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

