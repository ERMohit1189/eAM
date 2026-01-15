<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/administrato_root-manager.master" AutoEventWireup="true" CodeFile="Setting.aspx.cs" Inherits="admin_RulesForlibrary" %>

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
                                <div class="col-sm-4 col-xs-4">
                                    <label>Education Type</label>
                                    <asp:RadioButtonList runat="server" ID="rdoK12" RepeatDirection="Horizontal" CssClass="underlined text-center input-border-btm vd_bd-red" Style="background-color: rgb(250, 255, 189);">
                                        <asp:ListItem Value="1" Selected="True">K12 Education</asp:ListItem>
                                        <asp:ListItem Value="0">Higher Education</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-sm-8 col-xs-8">
                                    <label>Identity Type</label><br />
                                    <asp:DropDownList runat="server" ID="ddlAadhaar" RepeatDirection="Horizontal" CssClass="underlined text-center input-border-btm width-30 vd_bd-red" Style="background-color: rgb(250, 255, 189);">
                                        <asp:ListItem Value="Aadhaar No.">Aadhaar No.</asp:ListItem>
                                        <asp:ListItem Value="National Identification Number (NIN)">National Identification Number (NIN)</asp:ListItem>
                                        <asp:ListItem Value="Social Security number (SSN) ">Social Security Number (SSN) </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-12  mgtp-5">
                                    <hr />
                                    <h3><b>System Rules</b></h3>
                                </div>

                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-12 ">
                                        <span class="txt-rep-title-13">RULE 1 </span>
                                        <span class="txt-rep-title-12 tab-in-05">Decimal precession count
                                          <asp:TextBox ID="txtR1" runat="server" onkeyup="CheckIntegerValueonKeyUp(event,this);" CssClass="underlined text-center input-border-btm width-10 vd_bd-red validatetxt" style="padding: 4px 5px !important;" Text="2"></asp:TextBox></span>
                                    </div>

                                    <div class="col-sm-12  mgtp-5">
                                        <span class="txt-rep-title-13">RULE 2 </span>
                                        <span class="txt-rep-title-12 tab-in-05">Currency
                                            <asp:DropDownList runat="server" ID="ddlCurrencySymbol" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrencySymbol_SelectedIndexChanged" CssClass="underlined text-center input-border-btm  width-20 vd_bd-red validatetxt" Style="background-color: rgb(250, 255, 189);">
                                                <asp:ListItem Value="8377">INR - Indian Rupee (&#8377;)</asp:ListItem>
                                                <asp:ListItem Value="36">USD - US Dollar (&#36;)</asp:ListItem>
                                                <asp:ListItem Value="8358">NGN - Nigerian Niara (&#8358;)</asp:ListItem>
                                                 <asp:ListItem Value="65020">SAR - Saudi Arabian Riyal (&#65020;)</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox runat="server" ID="txtCurrencyName" placeholder="Currency Name" CssClass="underlined text-center input-border-btm width-20 vd_bd-red validatetxt" style="padding: 4px 5px !important;" Text="Rs."></asp:TextBox>
                                        </span>
                                    </div>

                                    <div class="col-sm-12  mgtp-5">
                                        <span class="txt-rep-title-13">RULE 3 </span>
                                        <span class="txt-rep-title-12 tab-in-05">Date Format
                                            <asp:DropDownList runat="server" ID="ddlDateFormat" CssClass="underlined text-center input-border-btm width-10 vd_bd-red validatetxt" Style="background-color: rgb(250, 255, 189);">
                                                <asp:ListItem Value="DD MM YYYY">DD MM YYYY</asp:ListItem>
                                                <asp:ListItem Value="DD-MM-YYYY">DD-MM-YYYY</asp:ListItem>
                                                <asp:ListItem Value="DD/MM/YYYY">DD/MM/YYYY</asp:ListItem>
                                                <asp:ListItem Value="DD MMM YYYY">DD MMM YYYY</asp:ListItem>
                                                <asp:ListItem Value="DD-MMM-YYYY">DD-MMM-YYYY</asp:ListItem>
                                                <asp:ListItem Value="DD/MMM/YYYY">DD/MMM/YYYY</asp:ListItem>
                                            </asp:DropDownList>
                                        </span>
                                    </div>

                                    <div class="col-sm-12  mgtp-5">
                                        <span class="txt-rep-title-13">RULE 4 </span>
                                        <span class="txt-rep-title-12 tab-in-05">Language
                                            <asp:DropDownList runat="server" ID="ddlLanguage" CssClass="underlined text-center input-border-btm width-10 vd_bd-red validatetxt" Style="background-color: rgb(250, 255, 189);">
                                                <asp:ListItem Value="English">English</asp:ListItem>
                                                <asp:ListItem Value="Hindi">Hindi</asp:ListItem>
                                            </asp:DropDownList>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-12  mgtp-5">
                                    <hr />
                                </div>
                                <div class="col-sm-12  mgtp-5 text-center">
                                    <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                    <div id="msgbox" runat="server" style="left: 76px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

