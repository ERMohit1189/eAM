<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="ClassWiseActivity.aspx.cs" Inherits="commom_ClassWiseActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div runat="server" id="loader"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="drpClass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue validatedrp"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpSection_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue validatedrp"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Album Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpAlbum" runat="server" CssClass="form-control-blue validatedrp"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpAlbum_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Title&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Upload Image&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:FileUpload ID="fu1" runat="server" CssClass="form-control-blue"
                                                onchange="checksFileSizeandFileTypeinupdatePanel(this, 150000, 'jpg|JPG|png|PNG|jpeg|JPEG|gif|GIF','Avatars',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdAlbumPhoto');" />
                                            <div class="text-box-msg">
                                                <%--<asp:Label ID="lblErrormsg" runat="server" ForeColor="#DA4448"></asp:Label>--%>
                                            </div>
                                        </div>
                                    </div>
                                    

                                    <div class="col-sm-8  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server"
                                            OnClientClick="ValidateTextBox('.validatetxt');
                                                            ValidateDropdown('.validatedrp');return validationReturn();"
                                            OnClick="lnkSubmit_Click" CssClass="button">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px"></div>
                                    </div>
                                    <div class="col-sm-4  mgbt-xs-10">
                                        <div class="album-pic-box">
                                            <div class="album-pic-box-main">
                                                <asp:Image alt="" ID="Avatar" class="Avatars" runat="server" ImageUrl="../img/user-pic/activityablum.jpg" />
                                                <asp:HiddenField ID="hdAlbumPhoto" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 ">
                                        <div class="table-responsive2 table-responsive">
                                            <table class="table table-striped no-bm table-hover no-head-border table-bordered pro-table text-center">
                                                <tr id="hr1" runat="server">
                                                    <th>#</th>
                                                    <th>Album Name</th>
                                                    <th>Title</th>
                                                    <th>Class</th>
                                                    <th>Section</th>
                                                    <th>Stream</th>
                                                    <th>Image</th>
                                                    <th class="vd_bg-blue vd_white" align="center" valign="middle" scope="col" style="width:40px;">Edit</th>
                                                    <th class="vd_bg-blue vd_white" align="center" valign="middle" scope="col" style="width:40px;">Delete</th>
                                                    <th style="width:40px;">
                                                        <asp:CheckBox ID="ChkAll" AutoPostBack="true" OnCheckedChanged="ChkAll_CheckedChanged"
                                                            runat="server" /></th>
                                                </tr>
                                                <asp:Repeater ID="rpt1" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAlbum" runat="server" Text='<%# Eval("AlbumName") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblClass" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Section") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Branch") %>'></asp:Label>
                                                            </td>
                                                            <td class="width-20">
                                                                <asp:Label ID="lblimgName" runat="server" Text='<%# Eval("FilePath") %>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Image ID="imgAlbum" CssClass="width-20" runat="server" ImageUrl='<%# Eval("FilePath") %>' />
                                                            </td>
                                                            <td class="menu-action">
                                                                <asp:Label ID="lblEdit" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" title="Edit" 
                                                                                OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                               
                                                            </td>
                                                            <td class="menu-action">
                                                                <asp:Label ID="lblDelete" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"
                                                                                title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                
                                                            </td>
                                                            <td class="menu-action">
                                                                <asp:CheckBox ID="Chk" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:HiddenField ID="hdImagePath" runat="server" />
                                                <%--                                                <asp:HiddenField ID="hdClassId" runat="server" />
                                                <asp:HiddenField ID="hdSectionId" runat="server" />
                                                <asp:HiddenField ID="hdBranchId" runat="server" />--%>
                                                <tr id="fr1" runat="server" visible="false">
                                                    <td colspan="9"></td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkDeleteAll" runat="server"
                                                            OnClick="lnkDeleteAll_Click" CssClass="button">DeleteAll</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                            <table class="tab-popup">
                                                <tr>
                                                    <td>Class<span class="vd_red">*</span>
                                                    </td>
                                                    <td class="controls">
                                                        <asp:DropDownList ID="drpClassPanel" runat="server" CssClass="form-control-blue validatedrp1">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Section<span class="vd_red">*</span>
                                                    </td>
                                                    <td class="controls">
                                                        <asp:DropDownList ID="drpSectionPanel" runat="server" CssClass="form-control-blue validatedrp1">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Stream<span class="vd_red">*</span>
                                                    </td>
                                                    <td class="controls">
                                                        <asp:DropDownList ID="drpBranchPanel" runat="server" CssClass="form-control-blue validatedrp1">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Select Album Name<span class="vd_red">*</span>
                                                    </td>
                                                    <td class="controls">
                                                        <asp:DropDownList ID="drpAlbumPanel" runat="server" CssClass="form-control-blue validatedrp1">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Title Name<span class="vd_red">*</span>
                                                    </td>
                                                    <td class="controls">
                                                        <asp:TextBox ID="txtTitlePanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Upload Image<span class="vd_red">*</span>
                                                    </td>
                                                    <td class="controls">
                                                        <asp:FileUpload ID="fu1Panel" runat="server" CssClass="form-control-blue"
                                                                        onchange="checksFileSizeandFileTypeinupdatePanel(this, 150000, 'jpg|JPG|png|PNG|jpeg|JPEG|gif|GIF','AvatarPanel',
                                                                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdAlbumPhotoPanel');" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                   
                                                    <td class="controls album-pic-box">
                                                        <div class="album-pic-box-main">
                                                        <asp:Image alt="" ID="AvatarPanel" class="AvatarPanel"
                                                                   runat="server" ImageUrl="../img/user-pic/student-pic.png" />
                                                        <asp:HiddenField ID="hdAlbumPhotoPanel" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><asp:LinkButton ID="lnkUpdate" runat="server" class="button-y"
                                                                        OnClientClick="ValidateTextBox('.validatetxt1');
ValidateDropdown('.validatedrp1');return validationReturn();"
                                                                        type="button" OnClick="lnkUpdate_Click">Update</asp:LinkButton>
                                                        &nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkCancel" runat="server" class="button-n" type="button">Cancel</asp:LinkButton>
                                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                                
                                            </div>
                                        </asp:Panel>
                                        <asp:LinkButton ID="Button2" runat="server" Style="display: none"></asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                                            BackgroundCssClass="popup_bg" Enabled="True" CancelControlID="lnkCancel"
                                            PopupControlID="Panel1" TargetControlID="Button2">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </div>

                                    <br />

                                    <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                            <table class="tab-popup">

                                                <tr>
                                                    <td class="text-center">
                                                        <h4>Do you really want to Delete this record?</h4>
                                                        <asp:Label ID="lblvalue"
                                                            runat="server" Visible="False"></asp:Label>
                                                        <asp:Label ID="lblpath" runat="server" Text="" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="text-center">
                                                        <asp:LinkButton ID="lnkNo" runat="server" CssClass="button-n">No</asp:LinkButton>
                                                        &nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkYes" runat="server" CssClass="button-y" OnClick="lnkYes_Click">Yes</asp:LinkButton>

                                                    </td>
                                                </tr>
                                            </table>

                                        </asp:Panel>
                                        <asp:LinkButton ID="Button10" runat="server" Style="display: none"></asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server"
                                            BackgroundCssClass="popup_bg" Enabled="True"
                                            PopupControlID="Panel2" TargetControlID="Button10" CancelControlID="lnkNo">
                                        </ajaxToolkit:ModalPopupExtender>

                                    </div>

                                    <div style="overflow: auto; width: 1px; height: 1px">
                                        <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">
                                            <table class="tab-popup">

                                                <tr>
                                                    <td class="text-center">
                                                        <h4>Do you really want to Delete these record's?</h4>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="text-center">
                                                        <asp:LinkButton ID="lnkNo1" runat="server" CssClass="button-n">No</asp:LinkButton>
                                                        &nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkYes1" runat="server" CssClass="button-y"
                                                        OnClick="lnkYes1_Click">Yes</asp:LinkButton>

                                                    </td>
                                                </tr>
                                            </table>

                                        </asp:Panel>
                                        <asp:LinkButton ID="LinkButton3" runat="server" Style="display: none"></asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel3_ModalPopupExtender" runat="server"
                                            BackgroundCssClass="popup_bg" Enabled="True"
                                            PopupControlID="Panel3" TargetControlID="LinkButton3" CancelControlID="lnkNo1">
                                        </ajaxToolkit:ModalPopupExtender>

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

