<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/administrato_root-manager.master" AutoEventWireup="true" CodeFile="database_restore.aspx.cs" Inherits="admin.Restore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">

                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Enter Path of Backup File (with File Name)&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">


                                            <asp:TextBox ID="txtDataBasePath" runat="server" CssClass="form-control-blue" PlaceHolder="E:\eambackup\eAMdbBranchName30Jun2012193207.bak" Text=""></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click" CssClass="button">Restore Backup</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left:130px !important;"></div>
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

