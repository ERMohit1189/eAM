<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="NewTicket.aspx.cs" Inherits="common_NewTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%--  <script>
        function setFocus() {
            var textSubject = document.getElementById('<%= txtSubject.ClientID %>');
            if (textSubject != null) {
                textSubject.focus();
            }
        }
    </script>--%>
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <%--   <script>
                Sys.Application.add_load(setFocus);
                
                Sys.Application.add_load(scrollbar);
            </script>--%>
    <!-- PAGE CONTENT WRAPPER -->
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top ">
                    <div class="panel-body " id="divDropdown" runat="server">
                        <div class="col-sm-12  no-padding">
                            <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                <label class="control-label">Ticket Type</label>
                                <div class="mgtp-5">
                                    <asp:RadioButtonList ID="rblist1" CssClass="vd_radio radio-success" runat="server" AutoPostBack="true"
                                        RepeatDirection="Horizontal" OnSelectedIndexChanged="rblist1_SelectedIndexChanged">
                                        <asp:ListItem Selected="True">New</asp:ListItem>
                                        <asp:ListItem>Old</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-sm-10  half-width-50 mgbt-xs-15">
                                <label class="control-label">Select Category&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:DropDownList ID="drpCategory" runat="server">
                                        <asp:ListItem>Other</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-sm-12  no-padding" id="div1" runat="server">
                <div class="col-md-12 col-sm-6 ">
                    <div class="panel widget light-widget panel-bd-top ">
                        <div class="panel-body ">
                            <div class="col-sm-12   mgbt-xs-15">
                                <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:TextBox ID="txtSubject" PlaceHolder="Max 160 Char" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12   mgbt-xs-15">
                                <label class="control-label">Description</label>
                                <div class=" ">
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control-blue summernote_email" Rows="1"></asp:TextBox>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12   mgbt-xs-15">
                                <label class=" control-label">Attachment&nbsp;<span class="vd_red"></span></label>
                                <div class="col-sm-12  no-padding">
                                    <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control-blue"
                                        onChange="checksFileSizeandFileTypeinupdatePanel_fordoc(this,'pdf|doc|docx|xls|xlsx|txt|jpg|jpeg|png|gif|JPG|PNG|JPEG|GIF',
                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_hidFile',
                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_hidFileExt');"></asp:FileUpload>
                                    <div class="text-box-msg">
                                        <asp:HiddenField ID="hidFile" runat="server" />
                                        <asp:HiddenField ID="hidFileExt" runat="server" />
                                    </div>
                                    <div class="btn-set-r-t">
                                        <asp:LinkButton ID="lnkcatta" runat="server" OnClick="lnkcatta_Click" CssClass="form-control-blue button"><i class="icon-cross"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12  btn-a-devices-1-p4-p2  mgbt-xs-15">

                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="form-control-blue button" OnClientClick="noteeditable();return ValidateTextBox('.validatetxt');"
                                    OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
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
                                <table class="table p-table p-table-bordered table-hover no-bm table-striped table-bordered pro-table">
                                    <asp:Repeater ID="rpt" runat="server">
                                        <HeaderTemplate>
                                            <tr>
                                                <th>Ticket ID</th>
                                                <th>Creation Date</th>
                                                <th>Subject</th>
                                                <th>Status</th>
                                                <th>View</th>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblrefno" runat="server" Text='<%# Eval("REFNO") %>' Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="lnkrefno" runat="server" OnClick="lnkView_Click"><%# Eval("REFNO") %></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkInsertDate" runat="server" OnClick="lnkView_Click"><%# Eval("InsertDate") %></asp:LinkButton></td>
                                                <td>
                                                    <asp:LinkButton ID="lnkSubject" runat="server" OnClick="lnkView_Click"><%# Eval("Subject") %></asp:LinkButton></td>
                                                <td>
                                                    <asp:LinkButton ID="lnkisclosed" runat="server" OnClick="lnkView_Click"><%# Eval("IsClosed").ToString()=="True"?"Closed":"Open" %></asp:LinkButton></td>
                                                <td>
                                                    <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click">View</asp:LinkButton>
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
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

