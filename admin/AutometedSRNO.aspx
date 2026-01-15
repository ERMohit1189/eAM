<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AutometedSRNO.aspx.cs"
    Inherits="AutometedSRNO" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" runat="server" id="divSavePanel">
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                        <label class="control-label">S.R. No. Type &nbsp;<span class="vd_red"></span></label>
                                        <asp:RadioButtonList runat="server" ID="rdoSrNoType" AutoPostBack="true" CssClass="vd_radio radio-success" OnSelectedIndexChanged="rdoSrNoType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Manual" Selected="True">Manual</asp:ListItem>
                                            <asp:ListItem Value="Automatic">Automatic</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15" runat="server" id="divManual">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Pre-String &nbsp;<span class="vd_red"></span></label>
                                            <div class=" ">
                                                <asp:TextBox ID="txtPrestringManual" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Separator &nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:DropDownList runat="server" ID="ddlSeparaterManual">
                                                        <asp:ListItem Value="/" Selected="True">/</asp:ListItem>
                                                        <asp:ListItem Value="-">-</asp:ListItem>
                                                        <asp:ListItem Value="_">_</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Final S.R. No. Format &nbsp;<span class="vd_red"></span></label><br />
                                            <asp:Label runat="server" class="control-label" ForeColor="Red" ID="lblManualSRFormate"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  half-width-50 mgbt-xs-15" runat="server" id="divAutomatic">
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">Automatic S.R. No. Type &nbsp;<span class="vd_red"></span></label>
                                            <asp:RadioButtonList runat="server" ID="rdoAutomaticSrNoType" AutoPostBack="true"  CssClass="vd_radio radio-success" OnSelectedIndexChanged="rdoAutomaticSrNoType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Numeric" Selected="True">Numeric</asp:ListItem>
                                                <asp:ListItem Value="AlfaNumeric">Alfa-Numeric</asp:ListItem>
                                                <asp:ListItem Value="Customized">Customized</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-8  half-width-50 mgbt-xs-15" runat="server" id="divNumeric">
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Initial No. &nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:TextBox ID="txtInitialNoNumeric" runat="server" CssClass="form-control-blue" onblur="CheckDigitMobileNumber(this)" MaxLength="5"></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                        </div>
                                        <div class="col-sm-8  half-width-50 mgbt-xs-15" runat="server" id="divAlfaNumeric">
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Pre-String &nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:TextBox ID="txtPrestringAlfa" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Separator &nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:DropDownList runat="server" ID="ddlSeparaterAlfa">
                                                        <asp:ListItem Value="/" Selected="True">/</asp:ListItem>
                                                        <asp:ListItem Value="-">-</asp:ListItem>
                                                        <asp:ListItem Value="_">_</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Initial No. &nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:TextBox ID="txtInitialAlfa" runat="server" CssClass="form-control-blue" onblur="CheckDigitMobileNumber(this)" MaxLength="5"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Final S.R. No. Format &nbsp;<span class="vd_red"></span></label><br />
                                                <asp:Label runat="server" class="control-label" ForeColor="Red" ID="lblAlfaSRFormate"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-8  half-width-50 mgbt-xs-15" runat="server" id="divCustomized">
                                            <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Pre-String &nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:TextBox ID="txtPrestringCustomized" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Separator &nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:DropDownList runat="server" ID="ddlSeparaterCustomized">
                                                        <asp:ListItem Value="/" Selected="True">/</asp:ListItem>
                                                        <asp:ListItem Value="-">-</asp:ListItem>
                                                        <asp:ListItem Value="_">_</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Serial No. &nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:TextBox ID="txtSerialCustomized" runat="server" CssClass="form-control-blue" onblur="CheckDigitMobileNumber(this)" MaxLength="5"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Separator &nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:DropDownList runat="server" ID="ddlSeparaterCustomized2">
                                                        <asp:ListItem Value="/" Selected="True">/</asp:ListItem>
                                                        <asp:ListItem Value="-">-</asp:ListItem>
                                                        <asp:ListItem Value="_">_</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Register No. &nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:TextBox ID="txtRegisterNo" runat="server" CssClass="form-control-blue" onblur="CheckDigitMobileNumber(this)" MaxLength="5"></asp:TextBox>
                                                </div>
                                            </div>
                                             <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                                <label class="control-label">No. of Pages &nbsp;<span class="vd_red"></span></label>
                                                <div class=" ">
                                                    <asp:TextBox ID="txtPages" runat="server" CssClass="form-control-blue" onblur="CheckDigitMobileNumber(this)" MaxLength="5"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Final S.R. No. Format &nbsp;<span class="vd_red"></span></label><br />
                                                <asp:Label runat="server" class="control-label" ForeColor="Red" ID="lblAutomaticCustomizedSRFormate"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12  text-center">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click" CssClass="button">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 78px;"></div>
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
