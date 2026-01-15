<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="LoginScreenWallpaper.aspx.cs" Inherits="admin_LoginScreenWallpaper" %>

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
        .group-pic-box {
    width: 100%;
    border: 1px solid #ccc;
    padding: 10px;
    height: 280px;
    background: #ececec;
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

                                    <div class="col-sm-5  half-width-50 mgbt-xs-15">
                                        <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                            <label class=" control-label">Display Background&nbsp;<span class="vd_red">*</span></label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButtonList runat="server" ID="RadioButtonList1" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" CssClass="vd_radio radio-success  validaterblist txt-capitalize-alpha" RepeatDirection="Horizontal" RepeatLayout="flow">
                                            <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>

                                        <div class="col-sm-12 half-width-50 mgbt-xs-15" runat="server" id="div001">
                                            <label class="control-label">Select Wallpaper&nbsp;<span class="vd_red">*</span></label>
                                            <div class=" ">
                                                <asp:FileUpload runat="server" ID="avatarUpload" CssClass="form-control" TabIndex="1" onchange="checksFileSizeandFileTypeinupdatePanel(this, 5000000000000000000000000, 'jpg|jpeg|JPG|JPEG','Avatars','ContentPlaceHolder1_ContentPlaceHolderMainBox_hdStPhoto');" type="file" />
                                                <asp:HiddenField ID="hdStPhoto" runat="server" />
                                                <div class="text-box-msg">
                                                    <span class="text-danger">
                                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="* For best visualization, wallpaper dimension should be 1700 x 1000 !"></asp:Label>
                                                    </span>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-sm-6 half-width-50 mgbt-xs-15 btn-a-devices-2-p2">
                                            <asp:LinkButton ID="Button1" runat="server" CssClass="button form-control-blue" ValidationGroup="a"
                                                OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();"
                                                TabIndex="2" OnClick="Button1_Click"> Submit</asp:LinkButton>
                                            
                                        </div>
                                        <div class="col-sm-6 half-width-50 mgbt-xs-15 btn-a-devices-2-p2" runat="server" id="div003">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" ValidationGroup="a"
                                                OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();"
                                                TabIndex="2" OnClick="LinkButton1_Click"> Set Default Wallpaper</asp:LinkButton>
                                            
                                        </div>
                                        <div class="col-sm-12 half-width-50 mgbt-xs-15 btn-a-devices-2-p2">
                                            <div id="msgbox" runat="server"></div>
                                            <div id="msgdefault" runat="server"></div>
                                        </div>

                                    </div>
                                    <div class="col-sm-7  mgbt-xs-10 " runat="server" id="div002">
                                        <div class="group-pic-box">
                                            <div class="group-pic-box-main" style="background-image:none !important;">
                                                <asp:Image alt="" ID="Avatar" class="Avatars" Height="260px" runat="server" />
                                                
                                            </div>
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

