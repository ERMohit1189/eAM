<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PunchDownloadTime.aspx.cs" Inherits="_8.PunchDownloadTime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label2" runat="server" class="control-label" Text="Time for"></asp:Label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rbTimeFor" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbTimeFor_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">Student</asp:ListItem>
                                                <asp:ListItem>Employee</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label16" runat="server" class="control-label" Text="Start Time for Downloading Punch"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlFromHour" CssClass="form-control-blue col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromMinute" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromType" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label1" runat="server" class="control-label" Text="Time Interval for Downloading Punch"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlIntervalHour" CssClass="form-control-blue col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlIntervalMinute" runat="server" CssClass="form-control-blue col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" OnClick="lnkSubmit_OnClick">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="lnkClear" runat="server" CssClass="button" OnClick="lnkClear_OnClick">Clear</asp:LinkButton>
                                            <div class="text-box-msg">
                                                <div id="msgbox" runat="server"></div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <div id="divExport" runat="server">
                                            <asp:GridView ID="grdDailyPunchDownloadTime" runat="server" AutoGenerateColumns="False" 
                                                CssClass="table table-striped table-hover no-bm no-head-border table-bordered table-header-group text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Time Interval">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDownloadStartTime" runat="server" Text='<%# Eval("IimeInterval") %>'></asp:Label>
                                                        </ItemTemplate>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

