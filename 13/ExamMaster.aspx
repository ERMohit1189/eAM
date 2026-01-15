<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ExamMaster.aspx.cs"
    Inherits="ExamMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
    <style>
        .vd_checkbox label {
            margin-bottom:0px !important;
        }
        #ContentPlaceHolder1_ContentPlaceHolderMainBox_Panel1 {
            overflow: auto !important;
    max-height: 550px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        // ReSharper disable once UnusedParameter
        //function EndRequestHandler(sender, args) {
        //    scrollTo(0, 0);
        //}
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(datetime);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
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
                                        <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlPaper" runat="server" CssClass="form-control-blue validatedrps" AutoPostBack="true" OnSelectedIndexChanged="ddlPaper_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Term Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlTerm" runat="server" CssClass="form-control-blue validatedrps" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Test Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtExamName" runat="server" CssClass="form-control-blue validatetxts" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">File Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlFileType" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlFileType_SelectedIndexChanged">
                                                <asp:ListItem Value="Manual">Manual</asp:ListItem>
                                                <asp:ListItem Value="GoogleDrivePath" Selected="True">Google Drive Path</asp:ListItem>
                                                <asp:ListItem Value="FileUpload">File Upload</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divFileUpload" visible="false">
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
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Duration (Minutes)&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control-blue validatetxts" MaxLength="3" onBlur="ChecktenDigitNumber(this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        
                                        <div class="col-sm-5" style="padding:0!important;">
                                            <label class="control-label">Start From&nbsp;<span class="vd_red">*</span></label>
                                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control-blue validatetxts datepicker-normal"></asp:TextBox>
                                        </div>
                                         <div class="col-sm-7" style="padding:0!important;padding-top: 27px !important;">
                                            <asp:DropDownList ID="ddlFromHour" CssClass="form-control-blue validatedrps col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromMinute" runat="server" CssClass="form-control-blue validatedrps col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromType" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <div class="col-sm-5" style="padding:0!important;">
                                            <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control-blue validatetxts datepicker-normal"></asp:TextBox>
                                        </div>
                                         <div class="col-sm-7" style="padding:0!important;padding-top: 27px !important;">
                                             <asp:DropDownList ID="ddlToHour" CssClass="form-control-blue validatedrps col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlToMinute" runat="server" CssClass="form-control-blue validatedrps col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlToType" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Result Show&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:CheckBox ID="ddlDontshow" runat="server" Text="Do not show result" CssClass="vd_checkbox checkbox-success" Checked="true" style="margin-bottom:0px!important;" AutoPostBack="true" OnCheckedChanged="ddlDontshow_CheckedChanged"></asp:CheckBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15" runat="server" id="divResultFrom" visible="false">

                                        <div class="col-sm-5" style="padding: 0!important;">
                                            <label class="control-label">Result From&nbsp;<span class="vd_red">*</span></label>
                                            <asp:TextBox ID="txtShowResultFromDate" runat="server" CssClass="form-control-blue validatetxts datepicker-normal"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-7" style="padding: 0!important; padding-top: 27px !important;">
                                            <asp:DropDownList ID="ddlShowResultFromHH" CssClass="form-control-blue validatedrps col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlShowResultFromMM" runat="server" CssClass="form-control-blue validatedrps col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlShowResultFromTT" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15" runat="server" id="divResultTo" visible="false">

                                        <div class="col-sm-5" style="padding: 0!important;">
                                            <label class="control-label">Result To&nbsp;<span class="vd_red">*</span></label>
                                            <asp:TextBox ID="txtShowResultToDate" runat="server" CssClass="form-control-blue validatetxts datepicker-normal"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-7" style="padding: 0!important; padding-top: 27px !important;">
                                            <asp:DropDownList ID="ddlShowResultToHH" CssClass="form-control-blue validatedrps col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlShowResultToMM" runat="server" CssClass="form-control-blue validatedrps col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlShowResultToTT" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"  Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divResSetrting" visible="false">
                                        <label class="control-label">Result Setting&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:CheckBox ID="chkInstantResult" runat="server" Text="Show instant result" CssClass="vd_checkbox checkbox-success" style="margin-bottom:0px!important;"></asp:CheckBox>
                                        </div>
                                        <label class="control-label" style="margin-bottom:0px!important;">Note:-&nbsp;<span class="vd_red">If Checked, student can see the result just after submitting the test.</span></label>
                                    </div>
                                   
                                    <div class="col-sm-10  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Test Setting&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:CheckBox ID="chkAutoAttempt" runat="server" Text="Allow multiple logins" CssClass="vd_checkbox checkbox-success"></asp:CheckBox>
                                        </div>
                                        <label class="control-label">Note:-&nbsp;<span class="vd_red">If Checked, students can attempt multiple times within the allotted slot. If Unchecked, students can attempt only once within the allotted slot.
                                                                                 </span></label>
                                    </div>
                                    
                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkSubmit" runat="server" OnClientClick="ValidateTextBox('.validatetxts');ValidateDropdown('.validatedrps');return validationReturn();" OnClick="LinkSubmit_Click" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>

                                    </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Class" runat="server" Text='<%# Bind("classname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Subject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paper">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Paper" runat="server" Text='<%# Bind("Paper") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Term Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TermName" runat="server" Text='<%# Bind("TermName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Test Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExamName" runat="server" Text='<%# Bind("ExamName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Duration">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Duration" runat="server" Text='<%# Bind("Duration") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="File">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" ID="nnn" NavigateUrl='<%# Bind("FilePath") %>' download="download" Target="_blank" style="text-decoration:underline;">Test File</asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                
                                                
                                                <asp:TemplateField HeaderText="Exam Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExamDate" runat="server" Text='<%# Eval("Examdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Result Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResultDate" runat="server" Text='<%# Eval("Resultdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Show instant result">
                                                    <ItemTemplate>
                                                        <asp:Label ID="InstantResult" runat="server" Text='<%# Eval("ResultStting").ToString()=="False"?"No":"Yes" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Allow multiple logins">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Allowmultiplelogins" runat="server" Text='<%# Eval("TestSetting").ToString()=="False"?"No":"Yes" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="status" runat="server" Text='<%# Eval("status").ToString()=="False"?"De-Active":"Active" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                    <div data-rel="scroll" data-scrollheight="250" class="scroll-show-always auto-set-height">
                        <div class="col-sm-12 ">
                            <table class="tab-popup">
                                <tr>
                                    <td>Class <span class="vd_red">*</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlClassPanel" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Subject <span class="vd_red">*</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlSubjectPanel" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Paper <span class="vd_red">*</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlPaperPanel" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Term Name&nbsp; <span class="vd_red">*</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlTermPanel" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Test Name <span class="vd_red">*</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtExamNamePanel" runat="server" CssClass="form-control-blue validatetxt1" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnSigName" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Duration (Minutes) <span class="vd_red">*</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtDurationPanel" runat="server" CssClass="form-control-blue validatetxt1" MaxLength="3" onBlur="ChecktenDigitNumber(this)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>File Type <span class="vd_red">*</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlFileTypePanel" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="ddlFileTypePanel_SelectedIndexChanged">
                                                <asp:ListItem Value="Manual">Manual</asp:ListItem>
                                                <asp:ListItem Value="GoogleDrivePath">Google Drive Path</asp:ListItem>
                                            <asp:ListItem Value="FileUpload">File Upload</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" id="divdivFileUploadPanel" visible="false">
                                    <td>Upload File <span class="vd_red">*</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:FileUpload ID="fileUploadPanel" runat="server"  
                                            onchange="checksFileSizeandFileTypeinupdatePanels(this, 5000000, 'jpg|png|jpeg|gif|pdf|JPG|PNG|JPEG|GIF|PDF','Avatars1', 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnFilePanel', 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hdnExtentionPanel');"
                                            type="file" CssClass="form-control-blue"></asp:FileUpload>
                                        <asp:Image alt="" ID="Avatars1" class="Avatars1 hide" runat="server" ImageUrl="../img/user-pic/student-pic.png" Style="width: 103px; height: 123px;" />
                                        <asp:HiddenField ID="hdnFilePanel" runat="server" />
                                        <asp:HiddenField ID="hdnExtentionPanel" runat="server" />

                                    </td>
                                </tr>
                                <tr runat="server" id="divGoogleDrivePathPanel">
                                    <td>Google Drive Path<span class="vd_red">*</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtGoogleDrivePathPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td>Start From <span class="vd_red"></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateFromPanel" runat="server" CssClass="form-control-blue validatetxt1 datepicker-normal"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:DropDownList ID="ddlFromHour0" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlFromMinute0" runat="server" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlFromType0" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                            <asp:ListItem Text="PM" Value="PM" ></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>End Test <span class="vd_red"></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateToPanel" runat="server" CssClass="form-control-blue validatetxt1 datepicker-normal"></asp:TextBox>
                                    </td>
                                     <td>
                                       <asp:DropDownList ID="ddlToHour0" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlToMinute0" runat="server" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlToType0" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                            <asp:ListItem Text="PM" Value="PM" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Result Show </td>
                                    <td colspan="2"><div class="">
                                            <asp:CheckBox ID="ddlDontshowPanel" runat="server" Text="Do not show result" AutoPostBack="true" OnCheckedChanged="ddlDontshowPanel_CheckedChanged" CssClass="vd_checkbox checkbox-success"></asp:CheckBox>
                                        </div>
                                </tr>
                                 <tr runat="server" id="TrResultFrom"  visible="false">
                                    <td>Show Result From <span class="vd_red"></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShowResultFromDate0" runat="server" CssClass="form-control-blue validatetxt1 datepicker-normal"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:DropDownList ID="ddlShowResultFromHH0" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlShowResultFromMM0" runat="server" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlShowResultFromTT0" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                            <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" id="TrResultTo"  visible="false">
                                    <td>Show Result To <span class="vd_red"></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShowResultToDate0" runat="server" CssClass="form-control-blue validatetxt1 datepicker-normal"></asp:TextBox>
                                    </td>
                                     <td>
                                       <asp:DropDownList ID="ddlShowResultToHH0" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlShowResultToMM0" runat="server" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlShowResultToTT0" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                            <asp:ListItem Text="PM" Value="PM" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                 <tr class="hide">
                                    <td>Status <span class="vd_red"></span>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlStatusPanel" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Deactive </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" id="TrResSetrting"  visible="false">
                                    <td>Result Setting</td>
                                    <td colspan="2"><div class="">
                                            <asp:CheckBox ID="chkInstantResult0" runat="server" Text="Show instant result" CssClass="vd_checkbox checkbox-success"></asp:CheckBox>
                                        </div>
                                        <label class="control-label">Note:-&nbsp;<span class="vd_red">If Checked, student can see the result just after submitting the test.</span></label></td>
                                </tr>
                                <tr>
                                    <td>Test Setting</td>
                                    <td colspan="2"><div class="">
                                            <asp:CheckBox ID="chkAutoAttempt0" runat="server" Text="Allow multiple logins" CssClass="vd_checkbox checkbox-success"></asp:CheckBox>
                                        </div>
                                        <label class="control-label">Note:-&nbsp;<span class="vd_red">If Checked, students can attempt multiple times within the allotted slot.<br /> If Unchecked, students can attempt only once within the allotted slot.
                                                                                 </span></label></td>
                                </tr>
                                
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="Button3" runat="server" CssClass="button-y" OnClick="Button3_Click" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();" Text="Update"
                                            ValidationGroup="b" />
                                        &nbsp;&nbsp;
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                        <div runat="server" id="msgbox2"></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button4" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" OnClick="Button8_Click" CausesValidation="False" />
                                &nbsp;&nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
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
