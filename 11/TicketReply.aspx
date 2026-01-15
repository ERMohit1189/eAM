<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="TicketReply.aspx.cs" Inherits="common_TicketReply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- PAGE CONTENT WRAPPER -->
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="panel widget light-widget panel-bd-top">
                        <div class="panel-body ">
                            <div class="row">
                                <div class="col-sm-12  no-padding">
                                    <asp:Repeater ID="rpt1" runat="server">
                                        <ItemTemplate>

                                            <div class="col-sm-12  ">
                                                <div class="col-sm-12  no-padding box-subject">
                                                    <div class="mail-sub">
                                                        <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                                    </div>
                                                    <div class="mail-sub-title">
                                                        Created:
                                                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Eval("InsertDate") %>'></asp:Label>
                                                        | Updated: 
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("UpdateDate") %>'></asp:Label>
                                                    </div>

                                                </div>
                                                <div class="reply-box">
                                                    <div class="mail-send-title-box">
                                                        "<asp:Label ID="lblREFNO" runat="server" Text='<%# Eval("REFNO") %>' Font-Bold="true"></asp:Label>"
                                                        <asp:Label ID="lblSendto" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    </div>
                                                    <div class="mail-reply-box">
                                                        <asp:LinkButton ID="lnkReply" runat="server" title="Reply" OnClick="lnkReply_Click"
                                                            data-placement="bottom" class="btn-reply" Visible='<%# Eval("isClosed").ToString()=="True"?false:true %>'>
                                                            <span><i class="icon-reply"></i></span> Reply</asp:LinkButton>
                                                        <asp:Label ID="lblid" runat="server" Text='<%# Eval("REFNO") %>' Visible="false"></asp:Label>
                                                    </div>


                                                </div>
                                            </div>

                                            <div class="col-sm-12 " id="divReply" runat="server" visible="false">
                                                <div class="col-sm-12   box-dec">
                                                    <div class="col-sm-12  no-padding">

                                                        <div class="mail-user-send-title mgbt-xs-10">
                                                            Reply Conversation
                                                        </div>

                                                        <div class="set-note-editor-h mgbt-xs-15">
                                                            <asp:TextBox ID="txtReply" runat="server" TextMode="MultiLine" CssClass="form-control-blue summernote_email" Rows="1"></asp:TextBox>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6   mgbt-xs-15">
                                                        <label class="control-label">Attechment&nbsp;<span class="vd_red"></span></label>
                                                        <div class="col-sm-12  no-padding">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control-blue"
                                                                onChange="checksFileSizeandFileTypeinupdatePanel_witin_Repeater(this,'50000','pdf|doc|docx|xls|xlsx|txt|jpg|jpeg|png|gif|JPG|PNG|JPEG|GIF',
                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_rpt1_hidFileReply',
                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_rpt1_hidFileExtReply');"></asp:FileUpload>
                                                            <div class="text-box-msg">
                                                                <asp:HiddenField ID="hidFileReply" runat="server" />
                                                                <asp:HiddenField ID="hidFileExtReply" runat="server" />
                                                            </div>
                                                            <div class="btn-set-r-t">
                                                                <asp:LinkButton ID="lnkcattaReply" runat="server" OnClick="lnkcattaReply_Click" CssClass="form-control-blue button"><i class="icon-cross"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-sm-6  btn-a-devices-1-p4-p2  mgbt-xs-15">
                                                        <asp:HiddenField ID="hfid" runat="server" />
                                                        <asp:LinkButton ID="lnkSubmitReply" runat="server" CssClass="form-control-blue button"
                                                            OnClientClick="noteeditableReply(this);return validatenoteeditor();" OnClick="lnkSubmitReply_Click">Reply</asp:LinkButton>
                                                        <div id="msgbox1" runat="server" style="left: 75px"></div>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12  ">

                                                <asp:Repeater ID="rptReply" runat="server">
                                                    <ItemTemplate>
                                                        <div class="col-sm-12   box-dec">
                                                            <div class="col-sm-12  no-padding">
                                                                <div class="col-sm-12  no-padding pad-r-50">
                                                                    <div class="mail-user-send-title">
                                                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                        <span class="mail-user-sub-title">(<asp:Label ID="lblUserType" runat="server" Text='<%# Eval("LoginType") %>'></asp:Label>)</span>
                                                                    </div>


                                                                    <div class="mail-date-title">
                                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("InsertDate") %>'></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-12  hr-5">
                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="attechment-box">
                                                                    <asp:Label ID="lblReplyAttechmentPath" runat="server" Text='<%# Eval("AttechmentPath") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="lnkReplyAttechment" runat="server" title="Attechment" OnClick="lnkReplyAttechment_Click"
                                                                        data-placement="bottom" class="btn-attechment"
                                                                        Visible='<%# (string) Eval("AttechmentPath") != string.Empty %>'>
                                                                            <span><i class="fa fa-paperclip rotate-50"></i></span></asp:LinkButton>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-12  no-padding">
                                                                <div class="col-sm-12  no-padding">

                                                                    <asp:Label ID="lblReplyDiscription" runat="server" Text='<%# Eval("Msg") %>'></asp:Label>


                                                                </div>

                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                            </div>


                                            <div class="col-sm-12 ">
                                                <div class="col-sm-12   box-dec">
                                                    <div class="col-sm-12  no-padding">
                                                        <div class="col-sm-12  no-padding pad-r-50">
                                                            <div class="mail-user-send-title">
                                                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                <span class="mail-user-sub-title">(<asp:Label ID="lblUserType" runat="server" Text='<%# Eval("LoginType") %>'></asp:Label>)</span>
                                                            </div>
                                                            <div class="mail-date-title">
                                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("InsertDate") %>'></asp:Label>
                                                            </div>
                                                            <div class="col-sm-12  hr-5">
                                                                <br />
                                                            </div>
                                                        </div>
                                                        <div class="attechment-box">
                                                            <asp:Label ID="lblPath" runat="server" Text='<%# Eval("AttechmentPath") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkAttechment" runat="server" title="Attechment" OnClick="lnkAttechment_Click"
                                                                data-placement="bottom" class="btn-attechment"
                                                                Visible='<%# (string) Eval("AttechmentPath") != string.Empty %>'>
                                                            <span><i class="fa fa-paperclip rotate-50"></i></span></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12   no-padding ">

                                                        <div class="col-sm-12  no-padding">
                                                            <asp:Label ID="lblDiscription" runat="server" Text='<%# Eval("Msg") %>'></asp:Label>
                                                        </div>



                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <script>
                function noteeditableReply(element) {
                    var index = element.id.split('_')[4];
                    var txtReply = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_rpt1_txtReply_" + index);
                    var editable = document.querySelector(".note-editable");
                    txtReply.value = editable.innerHTML;

                }




                function validatenoteeditor() {

                    var editor = document.querySelector(".note-editor");
                    var editable = editor.querySelector(".note-editable");

                    if (editable.innerText.trim().length > 1) {
                        editor.style.border = "1px solid #d5d5d5";

                        return true;
                    }
                    else {
                        editor.style.border = "1px solid #fc0505";

                        return false;
                    }


                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

