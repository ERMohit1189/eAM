<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="sr-no-management.aspx.cs" Inherits="_1.AdminSrNoManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-12   ">
                                        <label class="control-label">If Manual Selected then user can input SR No. manually in from & If Selected Automated.</label>
                                        <div class="">
                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem Value="M" Selected="True">Manual</asp:ListItem>
                                                <asp:ListItem Value="A">Automated</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <fieldset>
                                            <legend>Numeric</legend>

                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Input Starting Number &nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox1" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <label class="control-label">First SR No. Will Be</label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox2" CssClass="form-control-blue"  runat="server"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <fieldset>
                                            <legend>Alphanumeric</legend>

                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Input Starting Alphabet&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox3" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Alphabet Will Change After&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox4" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Input Last Number &nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox5" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <label class="control-label">First SR No. Will Be</label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox6" CssClass="form-control-blue"  runat="server"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <fieldset>
                                            <legend>Customized</legend>

                                            <div class="col-sm-12  no-padding">
                                                <div class="col-sm-6  ">
                                                    <fieldset>
                                                        <legend>Register</legend>
                                                        <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">First No.&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:TextBox ID="TextBox7" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Last No.&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:TextBox ID="TextBox8" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="col-sm-6  ">
                                                    <fieldset>
                                                        <legend>Page No.</legend>
                                                        <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">First No.&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:TextBox ID="TextBox11" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                            <label class="control-label">Last No.&nbsp;<span class="vd_red">*</span></label>
                                                            <div class="">
                                                                <asp:TextBox ID="TextBox12" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                                                <div class="text-box-msg">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>


                                           
                                            <div class="col-sm-6  half-width-50 mgbt-xs-15">
                                                <label class="control-label">First SR No. Will Be</label>
                                                <div class="">
                                                    <asp:TextBox ID="TextBox10" CssClass="form-control-blue"  runat="server"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
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

