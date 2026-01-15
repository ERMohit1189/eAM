<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="FineSetMaster.aspx.cs" Inherits="FineSetMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label14" runat="server" class="control-label" Text="Fine (Late Fee) Type"></asp:Label>&nbsp;<span class="vd_red">*</span>
                                        <div class="mgtp-6">
                                            <asp:RadioButtonList ID="rdoBasis" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoBasis_SelectedIndexChanged"
                                                RepeatDirection="Horizontal" class="vd_radio radio-success" RepeatLayout="Flow">
                                                <asp:ListItem Selected="True">Range Basis</asp:ListItem>
                                                <asp:ListItem>Daily Basis</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-7 " id="Panel1" runat="server">
                                    <fieldset>
                                        <legend>
                                            <span class="font-s-17">Range Basis</span>
                                        </legend>
                                        <div class="col-sm-12  no-padding ">
                                            <div class="col-sm-4   mgbt-xs-15 hide">
                                                <label class="control-label">Select</label>
                                                <div class="">
                                                    <asp:DropDownList ID="ddlFineBaseType1" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Value="Between Dates">Between Dates</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">From Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpFromDate1" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">To Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpToDate1" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Amount</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtrangeAmount1" runat="server" CssClass="form-control-blue" onKeyup="CheckDecimalNumber(this);"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="col-sm-12  no-padding " style="background: #edeef2;">
                                            <div class="col-sm-4   mgbt-xs-15 hide">
                                                <label class="control-label">Select</label>
                                                <div class="">
                                                    <asp:DropDownList ID="ddlFineBaseType2" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Value="Between Dates">Between Dates</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">From Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpFromDate2" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">To Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpToDate2" runat="server" SkinID="ddlSize2" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Amount</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtrangeAmount2" runat="server" CssClass="form-control-blue" onKeyup="CheckDecimalNumber(this);"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12  no-padding ">
                                            <div class="col-sm-4   mgbt-xs-15 hide">
                                                <label class="control-label">Select</label>
                                                <div class="">
                                                    <asp:DropDownList ID="ddlFineBaseType3" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Value="Between Dates">Between Dates</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">From Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpFromDate3" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">To Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpToDate3" runat="server" SkinID="ddlSize2" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Amount</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtrangeAmount3" runat="server" CssClass="form-control-blue" onKeyup="CheckDecimalNumber(this);"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12  no-padding " style="background: #edeef2;">
                                            <div class="col-sm-4   mgbt-xs-15 hide">
                                                <label class="control-label">Select</label>
                                                <div class="">
                                                    <asp:DropDownList ID="ddlFineBaseType4" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Value="Between Dates">Between Dates</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">From Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpFromDate4" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">To Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpToDate4" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Amount</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtrangeAmount4" runat="server" CssClass="form-control-blue" onKeyup="CheckDecimalNumber(this);"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12  no-padding ">
                                            <div class="col-sm-4   mgbt-xs-15 hide">
                                                <label class="control-label">Select</label>
                                                <div class="">
                                                    <asp:DropDownList ID="ddlFineBaseType5" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Value="Between Dates">Between Dates</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">From Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpFromDate5" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">To Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpToDate5" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Amount</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtrangeAmount5" runat="server" CssClass="form-control-blue" onKeyup="CheckDecimalNumber(this);"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12  no-padding " style="background: #edeef2;">
                                            <div class="col-sm-4   mgbt-xs-15 hide">
                                                <label class="control-label">Select</label>
                                                <div class="">
                                                    <asp:DropDownList ID="ddlFineBaseType6" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Value="Between Dates">Between Dates</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">From Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpFromDate6" runat="server" SkinID="ddlSize2" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">To Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpToDate6" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Amount</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtrangeAmount6" runat="server" CssClass="form-control-blue" onKeyup="CheckDecimalNumber(this);"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12  no-padding ">

                                            <div class="col-sm-4   mgbt-xs-15">
                                                <label class="control-label">Select</label>
                                                <div class="">
                                                    <asp:DropDownList ID="ddlFineBaseType7" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Value="Every Month">Every Month (Recurring)</asp:ListItem>
                                                        <asp:ListItem Value="Completion Month">Completion of Month</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15 hide">
                                                <label class="control-label">From Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpFromDate7" runat="server" SkinID="ddlSize2" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15 hide">
                                                <label class="control-label">To Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="DrpToDate7" runat="server" SkinID="ddlSize2" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15">


                                                <label class="control-label">Amount</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtrangeAmount7" runat="server" CssClass="form-control-blue" onKeyup="CheckDecimalNumber(this);"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-sm-7 " id="Panel2" runat="server">

                                    <fieldset>
                                        <legend>
                                            <span class="font-s-17">Daily Basis</span>
                                        </legend>
                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <asp:Label ID="Label7" runat="server" class="control-label" Text="From"></asp:Label>&nbsp;<span class="vd_red">* </span>
                                            <div class="">
                                                <asp:DropDownList ID="DrpDate" runat="server" CssClass=" form-control-blue">
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <asp:Label ID="Label2" runat="server" class="control-label" Text="Amount"></asp:Label>&nbsp;<span class="vd_red">* </span>
                                            <div class="">
                                                <asp:TextBox ID="txtdailAmount" runat="server" CssClass=" form-control-blue" onKeyup="CheckDecimalNumber(this);"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <asp:Label ID="Label3" runat="server" class="control-label" Text="Daily Increment"></asp:Label>&nbsp;<span class="vd_red">* </span>
                                            <div class="">
                                                <asp:TextBox ID="txtdaIncre" runat="server" CssClass=" form-control-blue"></asp:TextBox>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12  no-padding ">

                                            <div class="col-sm-6   mgbt-xs-15">
                                                <label class="control-label">Select</label>
                                                <div class="">
                                                    <asp:DropDownList ID="ddlFineBaseType7forDailyBasis" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Value="Every Month">Every Month (Recurring)</asp:ListItem>
                                                        <asp:ListItem Value="Completion Month">Completion of Month</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15 hide">
                                                <label class="control-label">From Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="ddlFromDate22" runat="server" SkinID="ddlSize2" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4   mgbt-xs-15 hide">
                                                <label class="control-label">To Date</label>
                                                <div class="">
                                                    <asp:DropDownList ID="ddlToDate22" runat="server" SkinID="ddlSize2" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6   mgbt-xs-15">


                                                <label class="control-label">Amount</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtrangeAmount7forDailyBasis" runat="server" CssClass="form-control-blue" onKeyup="CheckDecimalNumber(this);"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>

                                </div>
                                <div class="col-sm-5 ">
                                    <fieldset>
                                        <legend>
                                            <span class="font-s-17">Fine Rules</span>
                                        </legend>
                                        <div class="col-sm-12  half-width-50">
                                            <label class="control-label">&nbsp;&nbsp;</label>
                                            <div class="">
                                                <asp:CheckBox ID="chkExemptionAccordingDoA" runat="server" CssClass="vd_checkbox checkbox-success " RepeatDirection="Horizontal"
                                                    RepeatLayout="flow" Text="Late Fee Exemption According to Date of Admission" />
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12  half-width-50">
                                            <label class="control-label">&nbsp;&nbsp;</label>
                                            <div class="">
                                                <asp:CheckBox ID="chkExemptionforOnline" runat="server" CssClass="vd_checkbox checkbox-success " RepeatDirection="Horizontal"
                                                    RepeatLayout="flow" Text="Late Fee Exemption for Online Payment" />
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div class="col-sm-12 ">
                                        <div class="col-sm-6 no-padding">
                                            <label class="control-label">Cheque Bounce Charge</label>
                                            <div class="">
                                                <asp:TextBox ID="txtChequeBounceCharge" runat="server" Text="0.00" onKeyup="CheckDecimalNumber(this);"></asp:TextBox>
                                            </div>
                                            <br />
                                        </div>
                                        <div class="col-sm-6" style="padding-top: 24px;">
                                            <asp:LinkButton ID="LinkSubmit" runat="server" OnClick="LinkSubmit_Click" CssClass="button-y" Text="Submit"></asp:LinkButton>
                                            <asp:LinkButton ID="LinkReset" runat="server" OnClick="LinkReset_Click" CssClass="button-n form-control-blue" Text="Reset All"></asp:LinkButton>
                                        </div>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                        <div id="msgbox" runat="server" style="left: 155px;"></div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <h4>Are you sure you want to delete
                                    <asp:Label ID="Label4" runat="server"></asp:Label>
                                    ?
                                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="Button4" runat="server" Text="No" OnClick="Button4_Click" CssClass="button-n" CausesValidation="False" />
                                &nbsp;&nbsp; 
                                <asp:Button ID="DeleteYes" runat="server" OnClick="DeleteYes_Click" CssClass="button-y" Text="Yes" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel3_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True"
                    TargetControlID="Button7" PopupControlID="Panel3"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>



            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
