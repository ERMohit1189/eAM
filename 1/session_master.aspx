<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="session_master.aspx.cs" Inherits="_1.AdminSessionMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-4   mgbt-xs-15" id="divBranch" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                        <asp:DropDownList runat="server" ID="ddlBranch" CssClass="validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpYear_SelectedIndexChanged"
                                                         CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMonth_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDate" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpYear1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpYear1_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpMonth1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMonth1_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDate1" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Session&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtSesion" runat="server" ReadOnly="True" placeholder="e.g. : 2016-2017" ValidationGroup="a" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSesion" ErrorMessage="Can't leave blank!"
                                                                            Style="color: #CC0000" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9 hide">
                                        <label class="control-label">Remark </label>
                                        <div class="">
                                            <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine"  Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton" runat="server" OnClick="LinkButton_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>

                                </div>

                                <div class="col-sm-12  ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="Grd" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" AutoGenerateColumns="False">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Session">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SessionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FromDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("ToDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("SessionId") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("SessionId") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="grid_heading_default" />
                                            <RowStyle CssClass="grid_details_default" />
                                        </asp:GridView>
                                    </div>

                                </div>
                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup">
                                            <tr>
                                                <td>Session<span class="vd_red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="txtSessionNamePanel" Enabled="False" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>From<span class="vd_red">*</span></td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DrpYYFromPanel" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="DrpYYFromPanel_SelectedIndexChanged" Enabled="false" CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DrpFromMMPanel" runat="server"
                                                                OnSelectedIndexChanged="DrpFromMMPanel_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DrpFromDDPanel" runat="server"
                                                                OnSelectedIndexChanged="DrpFromDDPanel_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>To<span class="vd_red">*</span></td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="DrpYYToPanel" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="DrpYYToPanel_SelectedIndexChanged" Enabled="false" CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DrpPaneltoMM" runat="server"
                                                                OnSelectedIndexChanged="DrpPaneltoMM_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DrpToPanelDD" runat="server"
                                                                OnSelectedIndexChanged="DrpToPanelDD_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr class="hide">
                                                <td>Remark</td>
                                                <td>
                                                    <asp:TextBox ID="txtremark0" runat="server" CssClass="form-control-blue" Rows="2"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="Button3" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                                    &nbsp; &nbsp;
                                               <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="Button4" PopupControlID="Panel1"
                                            TargetControlID="Button5" BackgroundCssClass="popup_bg">
                                        </asp:ModalPopupExtender>
                                    </asp:Panel>
                                </div>
                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup text-center">

                                            <tr>
                                                <td>
                                                    <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                                            </h4>
                                            </td>
                                        </tr>
                                
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" OnClick="Button8_Click" CausesValidation="False" />
                                                &nbsp; &nbsp;
                                                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="button-y" Text="Yes" CausesValidation="False" />


                                            </td>
                                        </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                                        PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                                    </asp:ModalPopupExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
