<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="CreateDocumentName.aspx.cs" Inherits="_1.AdminFrmCreateDocumentName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
   
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
             <div id="loader" runat="server"></div>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" id="table1" runat="server">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Document Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDocumentName" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 74px;"></div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>


                                </div>

                                <div class="col-sm-12  ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdDetails" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblsrno" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Name">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDocument" Text='<%# Bind("DocumentType") %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblId" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                    <ContentTemplate>

                                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" title="Edit Document Name" 
                                                                            OnClick="lnkEdit_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                    <ContentTemplate>

                                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" CausesValidation="False"
                                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div style="overflow: hidden; width: 1px; height: 1px">
                <asp:Panel runat="server" ID="Panel1" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup">
                        <tr>
                            <td>Document Name
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDocumentTypePanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button-y" OnClick="lnkUpdate_Click" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();">Update</asp:LinkButton>
                                &nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n">Cancel</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button1" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                    CancelControlID="lnkCancel" TargetControlID="Button1" BackgroundCssClass="popup_bg"
                    PopupControlID="Panel1" PopupDragHandleControlID="Panel1">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?</h4>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkNo" runat="server" CssClass="button-n">No</asp:LinkButton>
                                &nbsp;&nbsp; 
                                                        <asp:LinkButton ID="lnkYes" runat="server" CssClass="button-y" OnClick="lnkYes_Click">Yes</asp:LinkButton>


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server"
                    CancelControlID="lnkNo" TargetControlID="Button2" BackgroundCssClass="popup_bg"
                    PopupControlID="Panel2" PopupDragHandleControlID="Panel2">
                </ajaxToolkit:ModalPopupExtender>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

