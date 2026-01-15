<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="NewsEvents.aspx.cs" Inherits="website_NewsEvents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

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
        input[type=checkbox] {
            width: 15px !important;
        }
        .ajax__calendar_body
        {
            z-index: 100004;
        }
    </style>
    <script src="https://cdn.ckeditor.com/4.7.3/standard/ckeditor.js"></script>
    <script>
        function CKEditor() {
            CKEDITOR.replace('CKEditorControl1');
        }
        $(document).ready(function () {
            CKEditor();
        });
    </script>
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
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="row no-padding form-group form-group-sm">

                                    <div class="col-sm-4 mgbt-xs-15">
                                        <label class="control-label">From Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control-blue datepicker-normal" placeholder="dd-MMM-yyyy" TabIndex="1" ReadOnly="true"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 mgbt-xs-15">
                                        <label class="control-label">To Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control-blue datepicker-normal" placeholder="dd-MMM-yyyy" TabIndex="2"  ReadOnly="true"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 mgbt-xs-15">
                                        <label class=" control-label">News Title&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtTitle" runat="server" class="form-control-blue validatetxt" TabIndex="3"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row no-padding form-group form-group-sm">
                                    <div class="col-sm-12 mgbt-xs-15">
                                        <label class=" control-label">News Text&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <CKEditor:CKEditorControl ID="txtDescription" runat="server"  Height="200" TabIndex="4"></CKEditor:CKEditorControl>
                                            <div class="text-box-msg">
                                                <asp:CheckBox runat="server" ID="chkIcon" CssClass="checkbox-success" TabIndex="5" /><img src="../img/new-icon.gif" style="margin-top: -14px;" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 btn-a-devices-2-p2 mgbt-xs-15 text-center">
                                        <asp:LinkButton ID="btnSave" runat="server" CssClass="button form-control-blue" ValidationGroup="a"
                                            OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();"
                                            OnClick="btnSave_Click" TabIndex="6"> <i class="fa fa-paper-plane"></i> &nbsp;Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 147px !important;"></div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 mgbt-xs-20" runat="server" id="divList" visible="false">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 ">
                                    <h4>News & Event List</h4>
                                </div>
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                            <thead>
                                                <tr>
                                                    <th class="vd_bg-blue-np vd_white-np" style="width:5%;">#</th>
                                                    <th class="vd_bg-blue-np vd_white-np" style="width:10%;">Icon</th>
                                                    <th class="vd_bg-blue-np vd_white-np" style="width:15%;">From Date</th>
                                                    <th class="vd_bg-blue-np vd_white-np" style="width:15%;">To Date</th>
                                                    <th class="vd_bg-blue-np vd_white-np" style="width:40%;">Title</th>
                                                    <th class="vd_bg-blue-np vd_white-np hide" style="width:10%;">Description</th>
                                                    <th class="vd_bg-blue-np vd_white-np" style="width:7%;">Edit</th>
                                                    <th class="vd_bg-blue-np vd_white-np" style="width:7%;">Delete</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="LabelNoIcon" runat="server" Text="No Icon"></asp:Label>
                                                            <asp:Image ID="imgIcon" runat="server" ImageUrl="../img/new-icon.gif" Visible="false"></asp:Image>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelFromDate" runat="server" Text='<%# Bind("FromDate", "{0:dd-MMM-yyyy}") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="LabelTodate" runat="server" Text='<%# Bind("Todate", "{0:dd-MMM-yyyy}") %>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                            </td>
                                                            <td class="hide">
                                                                <div class="text-center" style='word-wrap: break-word; text-align: justify;'>
                                                                    <p>
                                                                        <asp:Label ID="LabelDescription" runat="server" Style="word-wrap: normal; word-break: break-all;" Text='<%# Bind("Description") %>'></asp:Label>
                                                                    </p>
                                                                </div>
                                                            </td>

                                                            <td class="menu-action" style="width: 40px;">
                                                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("id")%>' Visible="false"></asp:Label>
                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="LinkEdit" OnClick="LinkEdit_Click"
                                                                            CausesValidation="False" runat="server" title="Edit" 
                                                                            class="btn menu-icon vd_bd-green vd_green"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                    </ContentTemplate>
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
                                            <td>From Date</td>
                                            <td>
                                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="ptxtFromDate" runat="server" CssClass="form-control-blue datepicker-normal" TabIndex="1"></asp:TextBox>

                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>To Date</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="ptxtToDate" runat="server" CssClass="form-control-blue datepicker-normal" TabIndex="2"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>News Title</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox ID="ptxtTitle" runat="server" class="form-control" TabIndex="3"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>News Text</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                <CKEditor:CKEditorControl ID="ptxtDescription" runat="server" Height="150"></CKEditor:CKEditorControl>
                                                    <asp:CheckBox runat="server" ID="pchkIcon" CssClass="checkbox-success" style="width: 15px;" />
                                                    <img src="../img/new-icon.gif" style="margin-top: -14px;" />
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="2" style="text-align: center !important;">
                                                <asp:Button ID="btnUpdate" CssClass="button-y" runat="server" Text="Update" TabIndex="5" OnClick="btnUpdate_Click" />
                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="button-n" OnClick="btnCancel_Click" Text="Cancel" />
                                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>

                                    </table>



                                </div>
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="btnCancel" PopupControlID="Panel1"
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


                                        <asp:Button ID="btnNodelete" runat="server" CssClass="button-n" OnClick="btnNodelete_Click" Text="No" />
                                        &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button-y" OnClick="btnDelete_Click" Text="Yes" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                            PopupControlID="Panel2" CancelControlID="btnNodelete" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>


                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
