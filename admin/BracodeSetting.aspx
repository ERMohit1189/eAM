<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="BracodeSetting.aspx.cs" Inherits="Admin_BracodeSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4   mgbt-xs-15">
                                        <label class="control-label">Automated Fee Deposit Option&nbsp;<span class="vd_red">*</span></label>
                                         <div class="txt-middle">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_radio radio-success">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4    btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" OnClick="LinkButton1_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
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

