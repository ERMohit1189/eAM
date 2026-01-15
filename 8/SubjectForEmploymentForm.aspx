<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SubjectForEmploymentForm.aspx.cs" Inherits="admin_SubjectForEmploymentForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <script>
        
    </script>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="div1" runat="server">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Education Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpEducationType" runat="server" AutoPostBack="True" CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="drpEducationType_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkSubmit" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" runat="server" CssClass="form-control-blue button" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div>
                                            <div id="msgbox" runat="server"></div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-12  ">
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:Repeater ID="Repeater1" runat="server" Visible="false">
                                                        <HeaderTemplate>
                                                            <table class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                                <tr>

                                                                    <th class="vd_bg-blue vd_white" style="width: 50px;">#</th>
                                                                    <th class="vd_bg-blue vd_white text-left">Education Type</th>
                                                                    <th class="vd_bg-blue vd_white">Subject</th>
                                                                    <th class="vd_bg-blue vd_white" style="width: 40px;">Edit</th>
                                                                    <th class="vd_bg-blue vd_white" style="width: 40px;">Delete</th>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'
                                                                        Visible="false"></asp:Label><%# Container.ItemIndex+1 %></td>
                                                                <td class=" text-left"><%# Eval("EducationType") %></td>
                                                                <td><%# Eval("Subject") %></td>
                                                                <td class="text-center" style="width: 40px">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="lnkEdit" EventName="Click" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" title="Edit "  CausesValidation="False" OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow" Style="padding: 0px 5px; margin: 2px;"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td class="text-center" style="width: 40px">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:LinkButton ID="lnkDelete" runat="server" Visible='<%# Eval("sessionname").ToString()==Session["SessionName"].ToString()? true :false %>' 
                                                                                title="Delete"  class="btn menu-icon vd_bd-red vd_red"
                                                                                 OnClick="lnkDelete_Click" Style="padding: 0px 5px; margin: 2px;"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                        <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always auto-set-height">
                                            <div class="col-sm-12 ">
                                                <table class="tab-popup">
                                                    <tr>
                                                        <td>Education Type <span class="vd_red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drpEducationTypeUpdate" runat="server" CssClass="form-control-blue validatedrp1"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr class="">
                                                        <td>Subject <span class="vd_red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSubjectUpdate" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Button ID="lnkUpdate" runat="server" CssClass="button-y" OnClick="lnkUpdate_Click" OnClientClick="ValidateDropdown('.validatedrp1');ValidateTextBox('.validatetxt1');return validationReturn();" Text="Update" />
                                                            &nbsp;&nbsp;
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                                            <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button4" PopupControlID="Panel1"
                                        CancelControlID="Button4" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                                    </asp:ModalPopupExtender>
                                </div>
                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup text-center">

                                            <tr>
                                                <td>
                                                    <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                                        <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" OnClick="Button8_Click" CausesValidation="False" />
                                                    &nbsp;&nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />


                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                                        PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
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

