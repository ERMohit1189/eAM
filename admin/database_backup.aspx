<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="database_backup.aspx.cs" Inherits="admin_database_backup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
  <div id="loader" runat="server"></div>  <%-- ==== in aspx file   --%>                            

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">

                                    <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Drive &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">

                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:TextBox ID="TextBox1" runat="server" SkinID="TxtBoxDef" Visible="false"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8 half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button">Get Backup</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left:102px !important;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12 no-padding" style="color: #f91c18;">
                                    <b>Note:</b> <br />o Database backup feature works only on Server machine.
                                    <br />
                                        o Please save the data on server drive only and ignore other drives.

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

