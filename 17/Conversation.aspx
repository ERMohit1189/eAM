<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="Conversation.aspx.cs" Inherits="_11.CommonConversation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <script>
        function setFocus() {
            var textSubject = document.getElementById('<%= txtSubject.ClientID %>');
            if (textSubject != null) {
                textSubject.focus();
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(setFocus);
                
                Sys.Application.add_load(scrollbar);
                Sys.Application.add_load(txtAreaHtml);
            </script>
            <!-- PAGE CONTENT WRAPPER -->
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 " id="divDropdown" runat="server" visible="false">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15"  runat="server" visible="false" id="divshowclass">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" CssClass="form-control-blue" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15"   runat="server" visible="false" id="divshowsection">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSection" CssClass="form-control-blue" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="drpSection_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15"   runat="server" visible="false" id="divshowbranch">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpBranch" CssClass="form-control-blue" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg"></div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15"   runat="server"  id="divcontype">
                                        <label class="control-label">Select Conversation</label>
                                        <div class="mgtp-5">
                                            <asp:RadioButtonList ID="rblist1" CssClass="vd_radio radio-success" runat="server" AutoPostBack="true"
                                                RepeatDirection="Horizontal" OnSelectedIndexChanged="rblist1_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">Submit Conversation</asp:ListItem>
                                                <asp:ListItem>My Conversation</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-12  no-padding" id="div1" runat="server">
                       
                        <div class="col-md-4 col-sm-6 " runat="server"  id="divteacherlist">
                            <div class="panel widget light-widget panel-bd-top ">
                                <div class="panel-body ">
                                    <div data-rel="scroll" data-scrollheight="645" class="scroll-show-always">
                                        <div class="col-sm-12 ">


                                            <div class="table-responsive2 table-responsive">
                                                <table class="table table-striped table-hover no-head-border no-bm table-bordered pro-table text-center">
                                                    <asp:Repeater ID="rpt2" runat="server">
                                                        <HeaderTemplate>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>S.R. No.</th>
                                                                <th class="text-left">Name</th>
                                                                <th>
                                                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" /></th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblsr" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblFamilyEmail" runat="server" Text='<%# Eval("Email") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblFamilyContactNo" runat="server" Text='<%# Eval("ContactNo") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Eval("Ids") %>'></asp:Label>
                                                                </td>
                                                                <td class="text-left">
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                       

                        <div  runat="server" id="divconlist">
                            <div class="panel widget light-widget panel-bd-top ">
                                <div class="panel-body ">

                                    <div class="col-sm-12   mgbt-xs-15" style="display: none">
                                        <label class="control-label">Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpCategory" runat="server">
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6 col-sm-12  mgbt-xs-9" id="divEmail" runat="server" visible="false">
                                        <label class="control-label">To Email&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtToEmail" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-6 col-sm-12   mgbt-xs-9" id="divMobile" runat="server" visible="false">
                                        <label class="control-label">To Mobile No.&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtMobileno" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>

                                        </div>
                                    </div>
                                    <script type="text/javascript">
                                    function Check() {
                                        var chkPassport = document.getElementById("chkAll2");
                                        if (chkPassport.checked) {
                                            alert("CheckBox checked.");
                                        } else {
                                            alert("CheckBox not checked.");
                                        }
                                    }
                                </script>
                                    <asp:UpdatePanel runat="server" ID="chkupdate">
                                        <ContentTemplate>
                                             
                                            <div class="col-sm-12   mgbt-xs-15" runat="server" id="forGuardianShop" visible="false">
                                        <label class="control-label">For&nbsp;<span class="vd_red">*</span></label>
                                        <div class="controls txt-middle">
                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" class="vd_checkbox checkbox-success" TextAlign="Right" RepeatDirection="Horizontal" RepeatLayout="Flow" onclick="Checked();" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Class Teacher" Value="CT"></asp:ListItem>
                                                        <asp:ListItem Text="Managing Director" Value="MD"></asp:ListItem>
                                                        <asp:ListItem Text="Director" Value="Director"></asp:ListItem>
                                                        <asp:ListItem Text="Manager" Value="Manager"></asp:ListItem>
                                                        <asp:ListItem Text="Principal" Value="Principal"></asp:ListItem>
                                                        <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                            </asp:CheckBoxList>
                                              <asp:CheckBox ID="chkAll2" runat="server" class="vd_checkbox checkbox-success" AutoPostBack="true" OnCheckedChanged="chkAll2_CheckedChanged" Text="Select All" onclick="SelectAll(this);" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <div class="col-sm-12   mgbt-xs-15">
                                        <label class="control-label">Conversation Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                   
                                    <div class="col-sm-12   mgbt-xs-15">
                                        <label class="control-label">Describe Your Conversation</label>
                                        <div class=" ">
                                           <%-- (Text Editor Class) summernote_email--%>
                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="5"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                      <script type="text/javascript">
                                        function validateFileSize() {
                                            var uploadControl = document.getElementById('<%= fuImage.ClientID %>');
                                            if (uploadControl.files[0].size > 100000) {
                                                document.getElementById('dvMsg').style.display = "block";
                                                return false;
                                            }
                                            else {
                                                document.getElementById('dvMsg').style.display = "none";
                                                return true;
                                            }
                                        }
                                    </script>
                                    <div class="col-md-6 col-sm-12   mgbt-xs-15">
                                        <label class=" control-label">Attachment&nbsp;<span class="vd_red"></span></label>
                                        <div class="col-sm-12  no-padding">
                                            <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control-blue"
                                                onChange="checksFileSizeandFileTypeinupdatePanel_fordoc(this,'pdf|doc|docx|xls|xlsx|txt|jpg|jpeg|png|gif|JPG|PNG|JPEG|GIF',
                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_hidFile',
                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_hidFileExt');validateFileSize();"></asp:FileUpload>
                                            <div class="text-box-msg">
                                               <div id="dvMsg" style="background-color:Red; color:White; width:190px; padding:3px; display:none;" >
                                                Maximum size allowed is 100 KB
                                                </div>   
                                                <asp:HiddenField ID="hidFile" runat="server" />
                                                <asp:HiddenField ID="hidFileExt" runat="server" />
                                            </div>
                                            <div class="btn-set-r-t">
                                                <asp:LinkButton ID="lnkcatta" runat="server" OnClick="lnkcatta_Click"  CssClass="form-control-blue button"><i class="icon-cross"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6 col-sm-12  btn-a-devices-1-p4-p2  mgbt-xs-15">

                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="form-control-blue button"
                                            OnClientClick="noteeditable();return ValidateTextBox('.validatetxt'); Check();"  OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px"></div>
                                        <script>
                                            function noteeditable() {
                                                var editable = document.querySelector(".note-editable");
                                                var txtMessage = document.getElementById('<%= txtDescription.ClientID %>');
                                                txtMessage.value = editable.innerHTML;
                                            }
                                        </script>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 " id="div2" runat="server" visible="false">

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
                                                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Eval("RecordDate") %>'></asp:Label>
                                                            | Updated: 
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("UpdatedDate") %>'></asp:Label>
                                                        </div>

                                                    </div>
                                                    <div class="reply-box">
                                                        <div class="mail-send-title-box">
                                                              Complain Id - &nbsp;
                                                        "<asp:Label ID="lblREFNO" runat="server" Text='<%# Eval("ConversationID") %>' Font-Bold="true">vioni</asp:Label>"&nbsp;&nbsp;
                                                        Created By - &nbsp;
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("username") %>'></asp:Label>(<asp:Label ID="lblSendto" runat="server" Text='<%# Eval("UserType") %>'></asp:Label>)
                                                          <%--  <asp:Label ID="lblCaption" runat="server" Text='<%# Eval("caption") %>'></asp:Label>--%>
                                                           <%-- <asp:Label ID="lblSendto0" runat="server" Text='<%# Eval("SentTo") %>'></asp:Label>--%>
                                                        </div>
                                                        <div class="mail-reply-box">
                                                            <asp:LinkButton ID="lnkReply" runat="server" title="Reply" OnClick="lnkReply_Click"  data-placement="bottom" class="btn-reply" Visible='<%# Eval("IsActive").ToString()=="True"?true:false %>'>
                                                            <span><i class="icon-reply"></i></span> Reply</asp:LinkButton>
                                                            
                                                            <asp:Label runat="server" ID="lblclose" style="border:red 1px solid;padding: 2px 4px; color:red;" Text='<%# Eval("IsActive").ToString()=="True"?"":"Closed" %>' Visible='<%# Eval("IsActive").ToString()=="True"?false:true %>'></asp:Label>
                                                            <asp:Label ID="lblid" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                                             <asp:Label ID="Label2" runat="server" Text='<%# Eval("ConversationID") %>' Visible="false"></asp:Label>
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
                                                                <%-- (Text Editor Class) summernote_email--%>
                                                                <asp:TextBox ID="txtReply" runat="server" TextMode="MultiLine" CssClass="form-control-blue " Rows="5"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-6   mgbt-xs-15">
                                                            <label class="control-label">Attachment&nbsp;<span class="vd_red"></span></label>
                                                            <div class="col-sm-12  no-padding">
                                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control-blue"
                                                                    onChange="checksFileSizeandFileTypeinupdatePanel_witin_Repeater(this,'100000','pdf|doc|docx|xls|xlsx|txt|jpg|jpeg|png|gif|JPG|PNG|JPEG|GIF',
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
                                                             <asp:HiddenField ID="hfid1" runat="server" />
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
                                                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                                                            <span class="mail-user-sub-title">(<asp:Label ID="lblUserType" runat="server" Text='<%# Eval("UserType") %>'></asp:Label>)</span>
                                                                        </div>


                                                                        <div class="mail-date-title">
                                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("RecordDate") %>'></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-12  hr-5">
                                                                            <br />
                                                                        </div>
                                                                    </div>
                                                                    <div class="attechment-box">
                                                                        <asp:Label ID="lblReplyAttechmentPath" runat="server" Text='<%# Eval("AttechmentPath") %>' Visible="false"></asp:Label>
                                                                        <asp:LinkButton ID="lnkReplyAttechment" runat="server" title="Attechment" OnClick="lnkReplyAttechment_Click"
                                                                            data-placement="bottom" class="btn-attechment"
                                                                            Visible='<%# Eval("AttechmentPath").ToString()==string.Empty?false:true %>'>
                                                                            <span><i class="fa fa-paperclip rotate-50"></i></span></asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-12  no-padding">
                                                                    <div class="col-sm-12  no-padding">

                                                                        <asp:Label ID="lblReplyDiscription" runat="server" Text='<%# Eval("Discription") %>'></asp:Label>


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
                                                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                                                    <span class="mail-user-sub-title">(<asp:Label ID="lblUserType" runat="server" Text='<%# Eval("UserType") %>'></asp:Label>)</span>
                                                                </div>
                                                                <div class="mail-date-title">
                                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("RecordDate") %>'></asp:Label>
                                                                </div>
                                                                <div class="col-sm-12  hr-5">
                                                                    <br />
                                                                </div>
                                                            </div>
                                                            <div class="attechment-box">
                                                                <asp:Label ID="lblPath" runat="server" Text='<%# Eval("AttechmentPath") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="lnkAttechment" runat="server" title="Attechment" OnClick="lnkAttechment_Click"
                                                                    data-placement="bottom" class="btn-attechment"
                                                                    Visible='<%# Eval("AttechmentPath").ToString()==string.Empty?false:true %>'>
                                                            <span><i class="fa fa-paperclip rotate-50"></i></span></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12   no-padding ">

                                                            <div class="col-sm-12  no-padding">
                                                                <asp:Label ID="lblDiscription" runat="server" Text='<%# Eval("Discription") %>'></asp:Label>
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

     <script>
        function SelectAll(chkAll2) {
            var checkBoxList1 = document.getElementById("<%= CheckBoxList1.ClientID %>");
            var option = checkBoxList1.getElementsByTagName('input');
            var i;
            if (chkAll2.checked) {
                for (i = 0; i < option.length; i++) {
                    option[i].checked = true;
                }
            }
            else {
                for (i = 0; i < option.length; i++) {
                    option[i].checked = false;
                }
            }
        }

        function Checked() {
            var chkAll = document.getElementById("<%= chkAll2.ClientID %>");
            var checkBoxList1 = document.getElementById("<%= CheckBoxList1.ClientID %>");
            var option = checkBoxList1.getElementsByTagName('input');

            for (var i = 0; i < option.length; i++) {
                if (option[i].checked === false) {
                    chkAll.checked = false;
                    break;
                }
            }

        }

    </script>
</asp:Content>

