<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StudentDocsReport.aspx.cs" Inherits="AdminStudentDocsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                
            </script>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="" id="div_drpclass">
                                            <asp:DropDownList runat="server" ID="DrpClass" class="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="" id="div_section">
                                            <asp:DropDownList runat="server" ID="drpSection" class="form-control-blue validatedrp"></asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Doc Type&nbsp;<span class="vd_red hide">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpDocType" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem>Both</asp:ListItem>
                                                        <asp:ListItem>Hard Copy</asp:ListItem>
                                                        <asp:ListItem>Soft Copy</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Type of Admission &nbsp;<span class="vd_red hide">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpAdmissioncType" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem>All</asp:ListItem>
                                                        <asp:ListItem>NEW</asp:ListItem>
                                                        <asp:ListItem>OLD</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Type of Documents &nbsp;<span class="vd_red hide">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBoxList runat="server" ID="chk_doc_type" CssClass="vd_checkbox checkbox-success" RepeatDirection="Horizontal" RepeatLayout="flow"></asp:CheckBoxList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6  mgbt-xs-15 hide">
                                        <label class="control-label">List Of &nbsp;<span class="vd_red hide">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddllistOf" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Value="SubmitAnything">Submited DOC</asp:ListItem>
                                                        <asp:ListItem Value="SubmitNothing">Not Submited</asp:ListItem>
                                                        <asp:ListItem Value="AllStudents">All Students</asp:ListItem>

                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                            <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                            <div class="">
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue ">View</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 75px;"></div>
                                                <div id="headerDiv" runat="server" class="hide"></div>

                                            </div>
                                        </div>

                                </div>
                                <div class="col-sm-12  no-padding" runat="server" visible="false" id="icons">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                        title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                        title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color hide"
                                                        title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                        title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

                                                    <script>
                                                        
                                                    </script>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="ImageButton1" />
                                                <asp:PostBackTrigger ControlID="ImageButton2" />
                                                <asp:PostBackTrigger ControlID="ImageButton3" />
                                                <asp:PostBackTrigger ControlID="ImageButton4" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                <div class="col-sm-12 ">
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div id="gdv1" runat="server" visible="false">
                                                        <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table">
                                                            <tr>
                                                                <td>
                                                                    <div id="header" runat="server" class="col-md-12 no-padding"></div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div id="divExport" runat="server"></div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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

