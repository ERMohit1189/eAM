<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StudentTestimonial.aspx.cs" Inherits="website_StudentTestimonial" %>


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

                                <div class="row no-padding form-group form-group-sm">
                                    
                                       <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Student's Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue form-control validatetxt" ValidationGroup="a" TabIndex="1"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                       <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue form-control validatetxt" TabIndex="2"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class=" control-label">Batch&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtHeading" runat="server" class="form-control-blue form-control validatetxt" TabIndex="3"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                              </div>
                                       <div class="row no-padding form-group form-group-sm">
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class=" control-label">Text&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" CssClass="form-control-blue form-control validatetxt" runat="server" TabIndex="4"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                   
                                             <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Image&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:FileUpload runat="server" ID="fu1" CssClass="form-control" TabIndex="5"
                                                onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfFile','ContentPlaceHolder1_ContentPlaceHolderMainBox_hdfilefileExtention');" />
                                            <asp:HiddenField ID="hfFile" runat="server" />
                                            <asp:HiddenField ID="hdfilefileExtention" runat="server" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                
                                      <div class="col-sm-4 half-width-50 btn-a-devices-2-p2 mgbt-xs-15">
                                         <asp:LinkButton ID="Button1" runat="server" CssClass="button form-control-blue" ValidationGroup="a"
                                            OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();"
                                            OnClick="Button1_Click" TabIndex="6"> <i class="fa fa-paper-plane"></i> &nbsp;Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 147px !important;"></div>
                                    </div>
                                       <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                           </div>
                                    
                                </div>


                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 mgbt-xs-20" runat="server" id="divList">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 ">
                                    <h4>Testimonial List</h4>
                                </div>

                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">

                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue-np vd_white-np">S.No.</th>
                                                    <th class="vd_bg-blue-np vd_white-np">STUDENT NAME</th>
                                                    <th class="vd_bg-blue-np vd_white-np">BATCH</th>
                                                    <th class="vd_bg-blue-np vd_white-np">CLASS</th>
                                                    <th class="vd_bg-blue-np vd_white-np">NOTICE MESSAGE</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Edit</th>
                                                    <th class="vd_bg-blue-np vd_white-np">Delete</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label13" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label14" runat="server" Text='<%# Bind("Batch") %>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label17" runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <div class="text-center" style='word-wrap: break-word; text-align: justify;'>
                                                                    <p>
                                                                        <asp:Label ID="Label19" runat="server" Style="word-wrap: normal; word-break: break-all;" Text='<%# Bind("NoticeMessage") %>'></asp:Label>
                                                                    </p>
                                                                </div>
                                                            </td>

                                                            <td class="menu-action" style="width: 40px;">
                                                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("NoticeId")%>' Visible="false"></asp:Label>
                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click"
                                                                            CausesValidation="False" runat="server" title="Edit" 
                                                                            class="btn menu-icon vd_bd-green vd_green"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </td>
                                                            <td class="menu-action" style="width: 40px;">
                                                                <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" runat="server"
                                                                    CausesValidation="False" title="Delete" 
                                                                    class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
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
                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                            <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                                <div class="col-sm-12 ">


                                    <table class="tab-popup">
                                        <tr>
                                            <td>Student Name</td>
                                            <td>
                                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox ID="TextBox3"  runat="server" CssClass="form-control-blue datepicker-normal" TabIndex="1"></asp:TextBox>

                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Class</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox ID="TextBox4"  runat="server" CssClass="form-control-blue datepicker-normal" TabIndex="2"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Batch</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox ID="txtHeadingUpdate" runat="server" class="form-control" TabIndex="3"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>News Text</td>
                                            <td>
                                                <asp:TextBox ID="txtDescriptionUPdate" TextMode="MultiLine"  TabIndex="4" class="form-control" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>

                                             <tr>
                                            <td>Select Image</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-picture-o"></span></span>
                                                    <asp:FileUpload runat="server" ID="FileUpload1" CssClass="form-control" TabIndex="2"
                                                        onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this, 'jpg|png|jpeg|gif|JPG|PNG|JPEG|GIF',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfFile1','ContentPlaceHolder1_ContentPlaceHolderMainBox_hdfilefileExtention1');" />
                                                    <asp:HiddenField ID="hfFile1" runat="server" />
                                                    <asp:HiddenField ID="hdfilefileExtention1" runat="server" />
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
                                               <asp:Button ID="btnupdate" CssClass="button-y" runat="server" Text="Update"  TabIndex="5"  OnClick="btnupdate_Click" />
                                                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
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


                                        <asp:Button ID="Button8" runat="server" CssClass="button-n" OnClick="Button8_Click" Text="No" />
                                        &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button-y" OnClick="btnDelete_Click" Text="Yes" />
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
