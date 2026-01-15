<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="uploadSyllabus.aspx.cs" Inherits="admin_UploadSyllabus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
     <script>
         

            </script>
    <style>
        .link:hover{
            text-decoration:underline;
        }
    </style>
    <asp:UpdatePanel ID="tyu" runat="server">
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
                                            <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue validatedrp"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpSection_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue validatedrp"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Title&nbsp;<span class="vd_red">*</span>&nbsp;<span id="charactercount">Max. 100 Character(s)</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtTitle" runat="server" MaxLength="102" CssClass="form-control-blue textchange validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                            
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Upload File&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:FileUpload ID="fu1" runat="server" CssClass="form-control-blue "
                                                onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this,'pdf|xlx|xlsx|doc|docx|txt|jpg|jpeg|png|gif|PDF|XLX|XLSX|DOC|DOCX|TXT|JPG|JPEG|PNG|GIF', 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfSllabusFile',
                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfSllabusFileext');" />
                                            <asp:HiddenField ID="hfSllabusFile" runat="server" />
                                            <asp:HiddenField ID="hfSllabusFileext" runat="server" />
                                            <div class="text-box-msg">
                                                <asp:Label ID="lblErrormsg" CssClass="form-control-blue " runat="server" ForeColor="#DA4448"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server"
                                            OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"
                                            CssClass="button" OnClick="lnkSubmit_Click" ValidationGroup="a">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px"></div>
                                    </div>

                                </div>

                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="grdDocList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped no-bm table-hover no-head-border table-bordered pro-table">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np"  Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" ID="idd" CssClass="link" NavigateUrl='<%# Bind("DocPath") %>' download="Syllabus" style="color:blue;">
                                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np"  Width="350px" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Path" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocPath" runat="server" Text='<%# Bind("DocPath") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClass" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                        <asp:Label ID="lblBranchName" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                        (<asp:Label ID="lblSection" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>)
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np"  Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DateWithLoginName" runat="server" Text='<%# Bind("DateWithLoginName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np"  Width="250px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEdit" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" title="Edit" 
                                                            OnClick="lnkEdit_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelete" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" title="Delete" 
                                                            OnClick="lnkDelete_Click" CausesValidation="False" class="btn menu-icon vd_bd-red vd_red"> 
                                                        <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>Upload File</td>
                            <td>
                                <asp:FileUpload ID="fu1panel" runat="server"
                                    onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this,'pdf|doc|docx|txt|jpg|jpeg|png|gif',
                                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfSllabusFilepanel',
                                                                      'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfSllabusFileextpanel');" />
                                <asp:HiddenField ID="hfSllabusFilepanel" runat="server" />
                                <asp:HiddenField ID="hfSllabusFileextpanel" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>Title</td>
                            <td>
                                <asp:TextBox ID="txtTitlepanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Class</td>
                            <td>
                                <asp:DropDownList ID="drpClasspanel" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpClasspanel_SelectedIndexChanged" CssClass="form-control-blue validatedrp1">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="drpSectionpanel" runat="server" CssClass="form-control-blue validatedrp1"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Stream</td>
                            <td>
                                <asp:DropDownList ID="drpBranchpanel" runat="server" CssClass="form-control-blue validatedrp1"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="lnkUpdate" runat="server" class="button-y"
                                    OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();"
                                    type="button" OnClick="lnkUpdate_Click">Update</asp:LinkButton>
                                &nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkCancel" runat="server" class="button-n" type="button">Cancel</asp:LinkButton>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label></td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:LinkButton ID="Button2" runat="server" Style="display: none"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                    BackgroundCssClass="popup_bg" Enabled="True" CancelControlID="lnkCancel"
                    PopupControlID="Panel1" TargetControlID="Button2">
                </ajaxToolkit:ModalPopupExtender>
            </div>

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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

