<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ChangeFeeDepositDate.aspx.cs"
    Inherits="ChangeFeeDepositDate" %>

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
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" SkinID="TxtBoxDef" placeholder="Enter Receipt Number" CssClass="form-control-blue validatetxt"
                                                OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4   mgbt-xs-15">
                                        <asp:LinkButton ID="Show" runat="server" OnClick="Show_Click" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px"></div>
                                    </div>


                                </div>
                                <div class="col-sm-12  no-padding" runat="server" id="divDate" visible="false">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Old Deposit Date</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                        Enabled="False" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                        Enabled="False" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDDate" runat="server" AutoPostBack="True" Enabled="False" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">New Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DDYearC" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYearC_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDMonthC" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonthC_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDDateC" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="btnUpate" runat="server" OnClick="btnUpate_Click" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" CssClass="button form-control-blue">Update</asp:LinkButton>
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
