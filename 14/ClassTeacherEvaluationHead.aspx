<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ClassTeacherEvaluationHead.aspx.cs" Inherits="ClassTeacherEvaluationHead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                             <div class="col-sm-12   mgbt-xs-15">
                                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Class &nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <asp:DropDownList ID="drpclass" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                                OnSelectedIndexChanged="drpclass_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                        <label class="control-label">Stream &nbsp;<span class="vd_red">*</span></label>
                                                        <div class="">
                                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True" 
                                                                OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>

                                                 <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                     <label class="control-label">Medium &nbsp;<span class="vd_red">*</span></label>
                                                     <div class="">
                                                         <asp:DropDownList ID="drpmedium" runat="server" AutoPostBack="True"
                                                             OnSelectedIndexChanged="drpmedium_SelectedIndexChanged"
                                                             CssClass="form-control-blue validatedrp">
                                                         </asp:DropDownList>
                                                         <div class="text-box-msg">
                                                         </div>
                                                     </div>
                                                 </div>
                                                 <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                     <label class="control-label">Evaluation Head &nbsp;<span class="vd_red">*</span></label>
                                                     <div class="">
                                                         <asp:TextBox ID="txtEvaluationHead" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                         <div class="text-box-msg">
                                                         </div>
                                                     </div>
                                                 </div>
                                                 <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                                     <asp:LinkButton ID="btnSubmit" runat="server" Visible="false" CssClass="button form-control-blue"
                                                         OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                                 </div>
                                                 <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                                     <div id="divMsg" runat="server"></div>
                                                 </div>

                                             </div>
                                            <div class="col-sm-12 ">

                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClassId" Visible="false" runat="server" Text='<%# Eval("ClassId") %>'></asp:Label>
                                                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Stream">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBranchId" Visible="false" runat="server" Text='<%# Eval("BranchId") %>'></asp:Label>
                                                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Medium">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMedium" runat="server" Text='<%# Eval("Medium") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Evaluation Head">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEvaluationHead" runat="server" Text='<%# Eval("EvaluationHead") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                                        OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CausesValidation="False"
                                                                        title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
             <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>Evaluation Head <span class="vd_red">*</span></td>
                            <td>
                                <asp:Label ID="lblBranchIds" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblMediums" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblClassIds" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                <asp:TextBox ID="txtEvaluationHead0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                <asp:Button ID="Button9" runat="server" Style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="LinkButton4" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CausesValidation="False" OnClick="LinkButton4_Click" CssClass="button-y">Update</asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" OnClick="LinkButton5_Click" CssClass="button-n">Cancel</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="LinkButton5" PopupControlID="Panel1"
                        TargetControlID="Button9" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <asp:Label ID="lblBranchIdss" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblMediumss" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblClassIdss" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                <h4>Are you sure you want to delete this?</h4>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" CssClass="button-n" CausesValidation="False" OnClick="Button8_Click" Text="No" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CssClass="button-y" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" />
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button8" 
                        Enabled="True" PopupControlID="Panel2" TargetControlID="Button7" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

