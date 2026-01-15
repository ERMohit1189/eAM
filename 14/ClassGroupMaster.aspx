<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ClassGroupMaster.aspx.cs" Inherits="ClassGroupMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12  ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Group&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:RadioButtonList ID="rdoGroup" runat="server" RepeatDirection="Horizontal" class="vd_radio radio-success" RepeatLayout="Flow"
                                                        OnSelectedIndexChanged="rdoGroup_SelectedIndexChanged" AutoPostBack="True">
                                                        <asp:ListItem Value="G1">Group1 (Pre Primary)</asp:ListItem>
                                                        <asp:ListItem Value="G2">Group2 (Primary & Junior)</asp:ListItem>
                                                        <asp:ListItem Value="G3">Group3 (Secondary & Senior Secondary)</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  half-width-50 mgbt-xs-15" id="clasbox" runat="server" visible="false">
                                        <label class="control-label">Select Classs&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:CheckBoxList ID="ChkClass" runat="server" RepeatDirection="Horizontal" class="vd_checkbox checkbox-success" RepeatLayout="Flow" />
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12   mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" CssClass="button" OnClick="LinkButton1_Click">Submit</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 75px;"></div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

