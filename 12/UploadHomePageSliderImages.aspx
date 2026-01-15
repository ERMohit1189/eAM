<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="UploadHomePageSliderImages.aspx.cs" Inherits="website_UploadHomePageSliderImages" %>

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

                                <div class=" no-padding form-group form-group-sm">
                                       <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">PHOTO TITLE&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                                <asp:TextBox ID="txtTitle" runat="server" class="form-control"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    

                                      <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">PHOTO&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                           <asp:FileUpload runat="server" ID="fu1" CssClass="form-control" TabIndex="2"
                                                    onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfFile','ContentPlaceHolder1_ContentPlaceHolderMainBox_hdfilefileExtention');" />
                                                <asp:HiddenField ID="hfFile" runat="server" />
                                                <asp:HiddenField ID="hdfilefileExtention" runat="server" />
                                            <div class="text-box-msg">
                                                  <span class="text-danger">
                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                            </span>
                                            </div><br />
                                        </div>
                                    </div>

                                      <div class="col-sm-4 half-width-50 mgbt-xs-15 btn-a-devices-2-p2">
                                        <asp:LinkButton ID="Button1" runat="server" class="button form-control-blue" ValidationGroup="a"
                                            OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();"
                                            OnClick="Button1_Click" TabIndex="3"> <i class="fa fa-paper-plane"></i> &nbsp;Submit</asp:LinkButton>
                                       <div id="msgbox" runat="server" style="left: 147px !important;"></div>
                                    </div>

                                  
                                </div>
                                   </div>
                        </div>
                    </div>
                                 <div class="col-sm-12 mgbt-xs-20" runat="server" id="divList">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding form-group form-group-sm">

                                <div class="col-sm-12">
                                    <h4>Home Slider Or Top Right/Left Images</h4>

                                    <div class="table-responsive2 table-responsive">

                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue-np vd_white-np">S.No.</th>
                                                    <th class="vd_bg-blue-np vd_white-np">PHOTO TITLE</th>
                                                    <th class="vd_bg-blue-np vd_white-np">PHOTO</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Edit</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Delete</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label13" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <div class="gallery-list">
                                                                    <asp:Image ID="Image1" runat="server" class=" gallery-item" Width="200" Height="120" ImageUrl='<%# Bind("Photo") %>' />
                                                                </div>
                                                            </td>

                                                            <td class="menu-action" style="width: 40px;">
                                                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("id")%>' Visible="false"></asp:Label>
                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="LinkButton1"
                                                                            CausesValidation="False" runat="server" title="Edit" 
                                                                            class="btn menu-icon vd_bd-green vd_green" OnClick="LinkButton1_Click"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </td>
                                                            <td class="menu-action" style="width: 40px;">
                                                                <asp:LinkButton ID="lnkDelete" runat="server"
                                                                    CausesValidation="False" title="Delete" 
                                                                    class="btn menu-icon vd_bd-red vd_red" OnClick="lnkDelete_Click"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
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
                    </div>


                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                            <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                                <div class="col-sm-12 ">
                                    <table class="tab-popup">

                                        <tr>
                                            <td>Select Slider</td>
                                            <td>
                                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox runat="server" ID="txtTitlep" class="form-control "></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Select Image</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-picture-o"></span></span>
                                                    <asp:FileUpload runat="server" ID="FileUpload1" CssClass="form-control" TabIndex="2"
                                                        onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfFile','ContentPlaceHolder1_ContentPlaceHolderMainBox_hdfilefileExtention');" />
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <div class="gallery-list">
                                                    <asp:Image ID="Image2" runat="server" class=" gallery-item" Width="200" Height="120" />
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="2">
                                                <%-- ReSharper disable once CenterTagIsObsolete --%>
                                                <center>
                                               <asp:Button ID="btnupdate" CssClass="button-y" runat="server" Text="Update" TabIndex="3" OnClick="btnupdate_Click" />
                                                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel"   />
                                                                 <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                            </center>
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="Button4" PopupControlID="Panel1"
                            TargetControlID="Button5" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close" PopupDragHandleControlID="Panel1">
                        </ajaxToolkit:ModalPopupExtender>
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

