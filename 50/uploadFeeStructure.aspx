<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="uploadFeeStructure.aspx.cs" Inherits="uploadFeeStructure" %>

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
                                    
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Upload File&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:FileUpload ID="fu1" runat="server" CssClass="form-control-blue validatetxt"
                                                onchange="checksFileSizeandFileTypeinupdatePanel_fordoc(this,'pdf', 'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfSllabusFile',
                                        'ContentPlaceHolder1_ContentPlaceHolderMainBox_hfSllabusFileext');" />
                                            <asp:HiddenField ID="hfSllabusFile" runat="server" />
                                            <asp:HiddenField ID="hfSllabusFileext" runat="server" />
                                            <div class="text-box-msg">
                                                <asp:Label ID="lblErrormsg" CssClass="form-control-blue " runat="server" ForeColor="#DA4448"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server"
                                            OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"
                                            CssClass="button" OnClick="lnkSubmit_Click" ValidationGroup="a">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px"></div>
                                    </div>

                                </div>

                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="grdDocList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped no-bm table-hover no-head-border table-bordered pro-table">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Path">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" ID="hy" NavigateUrl='<%# Bind("PDFurl") %>' Text="Download PDF" download="download"></asp:HyperLink>
                                                        <asp:Label ID="lblDocPath" runat="server" Visible="false" Text='<%# Bind("PDFurl") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelete" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" title="Delete" 
                                                            OnClick="lnkDelete_Click" CausesValidation="False" class="btn menu-icon vd_bd-red vd_red"> 
                                                        <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">

                        <tr>
                            <td class="text-center">
                                <h4>Do you really want to Delete this record?</h4>
                                <asp:Label ID="lblvalue"
                                    runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblpath" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="text-center">
                                <asp:LinkButton ID="lnkNo" runat="server" CssClass="button-n">No</asp:LinkButton>
                                &nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkYes" runat="server" CssClass="button-y" OnClick="lnkYes_Click">Yes</asp:LinkButton>

                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:LinkButton ID="Button10" runat="server" Style="display: none"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server"
                    BackgroundCssClass="popup_bg" Enabled="True"
                    PopupControlID="Panel2" TargetControlID="Button10" CancelControlID="lnkNo">
                </ajaxToolkit:ModalPopupExtender>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

