<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EditExam.aspx.cs"
    Inherits="EditExam" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(datetime);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 " style="padding-left: 0px;">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control-blue validatedrps" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control-blue validatedrps" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Paper Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlPaper" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlPaper_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Term Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlTerm" runat="server" CssClass="form-control-blue validatedrps" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Test Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlExam" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkSubmit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" OnClick="LinkSubmit_Click" ValidationGroup="a" CssClass="button form-control-blue" Style="margin-top: 26px;">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>

                                    </div>
                                </div>

                                <div class="col-sm-12 " runat="server" id="divUpdate" visible="false">
                                    <hr />
                                    <div class="col-sm-12  mgbt-xs-15" style="padding-left: 0;">
                                        <div class="col-sm-6 " style="padding-left: 0;">
                                            <label class="control-label">Test Start From&nbsp;<span class="vd_red">*</span></label>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="control-label">Test End&nbsp;<span class="vd_red">*</span></label>
                                        </div>
                                        <div class="col-sm-3" style="padding-left: 0;">
                                            <asp:TextBox ID="txtDateFromPanel" runat="server" CssClass="form-control-blue datepicker-normal validatetxt1"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlFromHour0" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromMinute0" runat="server" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromType0" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtDateToPanel" runat="server" CssClass="form-control-blue datepicker-normal validatetxt1"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlToHour0" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlToMinute0" runat="server" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlToType0" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  mgbt-xs-15" style="padding-left: 0;">
                                        <div class="col-sm-6" style="padding-left: 0;">
                                            <label class="control-label">Show Result From&nbsp;<span class="vd_red">*</span></label>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="control-label">Show Result To&nbsp;<span class="vd_red">*</span></label>
                                        </div>
                                        <div class="col-sm-3" style="padding-left: 0;">
                                            <asp:TextBox ID="txtShowResultFromDate0" runat="server" CssClass="form-control-blue datepicker-normal validatetxt1"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlShowResultFromHH0" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlShowResultFromMM0" runat="server" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlShowResultFromTT0" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>


                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtShowResultToDate0" runat="server" CssClass="form-control-blue datepicker-normal validatetxt1"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlShowResultToHH0" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlShowResultToMM0" runat="server" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlShowResultToTT0" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" style="padding-left: 0;">
                                        <label class="control-label">File Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlFileType" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlFileType_SelectedIndexChanged">
                                                <asp:ListItem Value="Manual">Manual</asp:ListItem>
                                                <asp:ListItem Value="GoogleDrivePath" Selected="True">Google Drive Path</asp:ListItem>
                                                <asp:ListItem Value="FileUpload">File Upload</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6" runat="server" id="divFileUpload" visible="false">
                                        <label class="control-label">Upload File&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:FileUpload ID="fileUpload" runat="server"  
                                            onchange="checksFileSizeandFileTypeinupdatePanels(this, 100000000, 'pdf|PDF','Avatars', 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnFile', 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnExtention');"
                                            type="file" CssClass="form-control-blue"></asp:FileUpload>
                                            <asp:Image alt="" ID="Avatar" class="Avatars hide" runat="server" ImageUrl="../img/user-pic/student-pic.png" Style="width: 103px; height: 123px;" />
                                            <asp:HiddenField ID="hdnExtention" runat="server" />
                                            <asp:HiddenField ID="hdnFile" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divGoogleDrivePath">
                                        <label class="control-label">Google Drive Path&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtGoogleDrivePath" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  mgbt-xs-15 hide" style="padding-left: 0;">

                                        <div class="col-sm-3" style="padding-left: 0;">
                                            <label class="control-label">Status&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlStatusPanel" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Deactive </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  mgbt-xs-15" style="padding-left: 0;">
                                        <div class="col-sm-12" style="padding-left: 0;">
                                            <label class="control-label">Result Setting&nbsp;<span class="vd_red">*</span></label>
                                        </div>
                                        <asp:CheckBox ID="chkInstantResult0" runat="server" Text="Show instant result" CssClass="vd_checkbox checkbox-success"></asp:CheckBox>
                                        <br /><label class="control-label">Note:-&nbsp;<span class="vd_red">If Checked, student can see the result just after submitting the test.</span></label></td>
                                    </div>
                                    <div class="col-sm-12  mgbt-xs-15" style="padding-left: 0;">
                                        <div class="col-sm-12" style="padding-left: 0;">
                                            <label class="control-label">Test Setting&nbsp;<span class="vd_red">*</span></label>
                                        </div>
                                        <asp:CheckBox ID="chkAutoAttempt0" runat="server" Text="Allow multiple logins" CssClass="vd_checkbox checkbox-success"></asp:CheckBox>
                                        <br /><label class="control-label">
                                            Note:-&nbsp;<span class="vd_red"> If Checked, students can attempt multiple times within the allotted slot. If Unchecked, students can attempt only once within the allotted slot.
                                            </span>
                                        </label>
                                    </div>
                                    

                                    <div class="col-sm-12  mgbt-xs-15 text-center" style="padding-left: 0;">
                                        <asp:Button ID="Button3" runat="server" CssClass="button form-control-blue" OnClick="Button3_Click" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();" Text="Update" />
                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                        <div runat="server" id="msgbox2"></div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function checksFileSizeandFileTypeinupdatePanels(fileupload, size, filetype, imageClass, hiddenfield, hiddenfieldExtention) {
            var img = document.querySelector('.' + imageClass);
            if (fileupload.files.length > 0) {
                var filename = fileupload.files[0].name;
                var filesize = fileupload.files[0].size;
                if (filesize <= size) {
                    var extSplit = filename.split('.');
                    var extReverse = extSplit.reverse();
                    var ext = extReverse[0];
                    document.getElementById(hiddenfieldExtention).value = ext;
                    var splitfileext = filetype.split('|');
                    var flag = false;

                    for (var i = 0; i < splitfileext.length; i++) {
                        if (ext === splitfileext[i]) {
                            flag = true;
                            break;
                        }

                    }
                    if (flag === false) {
                        alert('Only ' + filetype + ' files are allowed!');
                        fileupload.value = "";
                    }
                }
                else {
                    var s = (size / 1000);
                    s = s / 1000;
                    alert('File size should not more than ' + (s) + ' MB');
                    fileupload.value = "";
                }

                var reader = new FileReader();
                reader.onloadend = function () {
                    img.src = reader.result;
                    var base64Url = reader.result.split(',');
                    document.getElementById(hiddenfield).value = base64Url[base64Url.length - 1];
                };
                if (fileupload.files[0]) {
                    reader.readAsDataURL(fileupload.files[0]);
                }
                else {
                    img.src = "";
                }
            }
            else {
                img.src = "";
            }

        }
    </script>

</asp:Content>
