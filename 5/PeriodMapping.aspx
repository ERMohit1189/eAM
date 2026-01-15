<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PeriodMapping.aspx.cs" Inherits="PeriodMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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

                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSection" CssClass="form-control-blue validatedrp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">

                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Periods&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlPeriod" CssClass="form-control-blue validatedrp" runat="server">

                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlFromH" EnableTheming="false" CssClass="form-control-blue col-xs-4 validatedrp" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromM" runat="server" EnableTheming="false" CssClass="form-control-blue col-xs-4 validatedrp">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromType" runat="server" EnableTheming="false" CssClass="form-control-blue col-xs-4">
                                                <asp:ListItem Value="AM">AM</asp:ListItem>
                                                <asp:ListItem Value="PM">PM</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlToH" EnableTheming="false" CssClass="form-control-blue col-xs-4 validatedrp" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlToM" runat="server" EnableTheming="false" CssClass="form-control-blue col-xs-4 validatedrp">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlToType" runat="server" EnableTheming="false" CssClass="form-control-blue col-xs-4">
                                                <asp:ListItem Value="AM">AM</asp:ListItem>
                                                <asp:ListItem Value="PM">PM</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:Button ID="btnInsert" runat="server" CssClass="button form-control-blue"  OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();"  Text="Submit" OnClick="btnInsert_Click" />
                                        <div id="msgbox" runat="server"></div>
                                    </div>

                                    <div class="col-sm-12 ">
                                        <div class="table-responsive table-responsive2">
                                            <asp:GridView ID="gvTimeTableRule" runat="server" CssClass="table table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="ClassName" HeaderText="Class" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                    <asp:BoundField DataField="SectionName" HeaderText="Section" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                    <asp:BoundField DataField="Period" HeaderText="Period" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                    <asp:BoundField DataField="StartFrom" HeaderText="From" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                    <asp:BoundField DataField="EndTo" HeaderText="To" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                     
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ClassID" runat="server" Text='<%# Eval("ClassID") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="SectionID" runat="server" Text='<%# Eval("SectionID") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="Label37" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="PeriodID" runat="server" Text='<%# Eval("PeriodID") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" title="Delete" data-toggle="tooltip" data-placement="top" class="btn menu-icon vd_bd-red vd_red">
                                                                <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    

                                    <div style="overflow: auto; width: 2px; height: 1px">
                                        <asp:Panel ID="pnlDelete" runat="server" CssClass="popup animated2 fadeInDown">
                                            <table class="tab-popup text-center">
                                                <tr>
                                                    <td style="text-align:center;" height="50">
                                                        <h4>Do you really want to delete this record?<asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                                            <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                                        </h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:center;">
                                                        <asp:Button ID="btnNo" runat="server" CausesValidation="False" CssClass="button-n" Text="No" OnClick="btnNo_Click" />
                                                        &nbsp; &nbsp;
                                                        <asp:Button ID="btnYes" runat="server" CausesValidation="False" CssClass="button-y" Text="Yes" OnClick="btnYes_Click" />


                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                                                Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                                            </asp:ModalPopupExtender>
                                        </asp:Panel>


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

