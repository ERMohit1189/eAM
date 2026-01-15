<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SetStudentAttendenceRange_ng.aspx.cs" Inherits="admin_SetStudentAttendenceRange_ng" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
        <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <script>
                Sys.Application.add_load(datetime);
            </script>
<div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                        <div class="col-lg-6 no-padding">
                            <div class="form-group ">
                                <asp:Label ID="lblClass" runat="server" class="col-sm-2  txt-middle-l txt-bold no-padding" Text="Class"></asp:Label>
                                <div class="col-sm-10 controls  mgbt-xs-20">
                                    <asp:DropDownList ID="drpClass" runat="server" OnSelectedIndexChanged="drpClass_SelectedIndexChanged" CssClass="form-control-blue validatedrp"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 no-padding">
                            <div class="form-group ">
                                <asp:Label ID="Label1" runat="server" class="col-sm-2  txt-middle-l txt-bold no-padding" Text="Term"></asp:Label>
                                <div class="col-sm-10 controls  mgbt-xs-20">
                                    <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drpEval_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 no-padding">
                            <div class="form-group ">
                                <asp:Label ID="Label2" runat="server" class="col-sm-2  txt-middle-l txt-bold no-padding" Text="Start Date"></asp:Label>
                                <div class="col-sm-10 controls  mgbt-xs-20">
                                    <asp:TextBox ID="txtDate1" runat="server" CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 no-padding">
                            <div class="form-group ">
                                <asp:Label ID="Label3" runat="server" class="col-sm-2  txt-middle-l txt-bold no-padding" Text="End Date"></asp:Label>
                                <div class="col-sm-10 controls  mgbt-xs-20">
                                    <asp:TextBox ID="txtDate2" runat="server" CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 no-padding">
                            <div class="form-group ">
                                <asp:Label ID="Label4" runat="server" class="col-sm-2  txt-middle-l txt-bold no-padding" Text="Attendence count start from"></asp:Label>
                                <div class="col-sm-5 controls no-padding">
                                    <asp:RadioButtonList ID="rbcountStart" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_radio radio-success validaterb"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbcountStart_SelectedIndexChanged">
                                        <asp:ListItem Value="1">School Session Date</asp:ListItem>
                                        <asp:ListItem Value="2">After Last Exam End Date*</asp:ListItem>
                                        <asp:ListItem Value="3">Manualy</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <div class="no-padding txt-rep-title-11">*attendence count start after one day from last exam date.</div>
                                </div>
                                <div class="col-sm-2 controls  mgbt-xs-20">
                                    <asp:TextBox ID="txtCountStartDate" runat="server" CssClass="form-control-blue validatetxt" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 no-padding">
                            <div class="form-group ">
                                <asp:Label ID="Label5" runat="server" class="col-sm-4  txt-middle-l txt-bold no-padding" Text="Attendence count at last"></asp:Label>
                                <div class="col-sm-8 controls  no-padding no-padding">
                                    <asp:RadioButtonList ID="rbcountEnd" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_radio radio-success validaterb">
                                        <asp:ListItem Value="1">Before Start Date*</asp:ListItem>
                                        <asp:ListItem Value="2">End Date</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <div class="no-padding txt-rep-title-11">*attendence count at last before one day from exam start date.</div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 no-padding mgbt-xs-20">
                            <div class="form-group ">
                                <asp:Label ID="Label6" runat="server" class="col-sm-4  txt-middle-l txt-bold no-padding" Text="Exclude days before DOA*"></asp:Label>
                                <div class="col-sm-8 controls  no-padding no-padding">
                                    <asp:RadioButtonList ID="rbExcludedays" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_radio radio-success validaterb" Enabled="false">
                                        <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-lg-12 no-padding txt-rep-title-11">*DOA(Date of Addmission),Exclude days before DOA if DOA is less then Attendence count at last</div>
                        </div>
                        <div class="col-lg-12 no-padding text-center">
                            <div class="form-group ">
                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" OnClick="lnkSubmit_Click"
                                    OnClientClick="ValidateRadiobuttonList('.validaterb');ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();">Submit</asp:LinkButton>
                           <div id="msgbox" runat="server" style="left: 120px;"></div>

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

