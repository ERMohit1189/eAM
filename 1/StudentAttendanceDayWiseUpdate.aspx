<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StudentAttendanceDayWiseUpdate.aspx.cs" Inherits="_1.StudentAttendanceDayWiseUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="jfg" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label8" runat="server" class="control-label" Text="Date"></asp:Label>&nbsp;
                                        <span class="vd_red">*</span>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>

                                                    <asp:DropDownList ID="DrpSaal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpSaal_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpMahina" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMahina_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDin" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpDin_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpAtteClass" runat="server" OnSelectedIndexChanged="DrpAtteClass_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpAttenSection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpAttenSection_SelectedIndexChanged" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Display Order</label>
                                        <div class="controls">
                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem Value="A" Selected="True">Alphabetical</asp:ListItem>
                                                <asp:ListItem Value="S">Sequential</asp:ListItem>
                                                <asp:ListItem Value="Rn">Roll No. Wise</asp:ListItem>
                                                <asp:ListItem Value="doa">Date of Admission</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="btnShow" runat="server" OnClick="btnShow_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>


                                    </div>

                                    <div class="col-sm-12   mgbt-xs-15">
                                        <span class="txt-bold txt-middle-l text-danger">Note:- </span><span class="txt-bold txt-middle-l text-danger blink">This page updates records only and does not send any SMS.</span>
                                    </div>

                                    <div class="col-sm-12   no-padding ">
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="cc" runat="server" Style="font-weight: 700; color: #CC0000"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-sm-12  ">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <div class=" table-responsive  table-responsive2">
                                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="Grd_SelectedIndexChanged" class="table no-bm no-head-border table-bordered">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label10" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" Width="25px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsrno" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-3" Width="85px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Enrollment No." Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStEnRCode" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Student's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Contact No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFatherContactNo" runat="server" Text='<%# Bind("FatherContactNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Attendance">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control-blue vd_bg-green vd_white" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" Width="75px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-2 form-group" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-12   no-padding ">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <div style="display: flex;align-items: flex-end;">
                                                    <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CausesValidation="False" CssClass="button form-control-blue">Submit</asp:LinkButton>                                               
                                                    <div id="msgbox" runat="server" style="left: 90px"></div></div>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel3" runat="server" CssClass="popup">

                        <table>

                            <tr>
                                <td style="text-align: center;">
                                    <strong>Updated successfully.</strong><asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 5px; text-align: center;">
                                    <asp:LinkButton ID="lnkOK" CssClass="button form-control-blue" runat="server">OK</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>

                    </asp:Panel>
                    <asp:ModalPopupExtender ID="Panel3_ModalPopupExtender" runat="server" CancelControlID="lnkOK" DynamicServicePath="" Enabled="True" PopupControlID="Panel3" TargetControlID="Button7" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
