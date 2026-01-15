<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="GroupMaster.aspx.cs" Inherits="_1.GroupMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
  <div id="loader" runat="server"></div>  <%-- ==== in aspx file   --%>                            

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
            <div id="heads" runat="server"></div>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="vd_content-section clearfix">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel widget light-widget">

                                    <div class="panel-body">
                                        <div class="col-sm-12 no-padding ">



                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true"
                                                        OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true"
                                                        OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Group&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtStream" CssClass="form-control-blue validatetxt" runat="server"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Remark</label>
                                                <div class="">
                                                    <asp:TextBox ID="txtRemark" CssClass="form-control-blue" Rows="1" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                                <asp:LinkButton ClientIDMode="Static" ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click"
                                                    OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"
                                                    CssClass="form-control-blue button">Submit</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 74px;"></div>

                                            </div>


                                        </div>

                                        <div class="col-sm-12  ">
                                            <div class="table-responsive2  table-responsive">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped no-bm table-hover no-head-border table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Class">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClass" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Stream">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Group">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStream" runat="server" Text='<%# Eval("Stream") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remark">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEditid" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click"
                                                                    title="Edit" 
                                                                    class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeleteId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"
                                                                    title="Delete" 
                                                                    class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
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
                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">

                            <table class="tab-popup">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" class="col-sm-4 txt-bold" Text="Class"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="drpClassPanel" runat="server" Enabled="false" CssClass="form-control-blue validatedrp1" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpClassPanel_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" class="col-sm-4 txt-bold" Text="Stream"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="drpBranchPanel" Enabled="false" runat="server" CssClass="form-control-blue validatedrp1"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" class="col-sm-4 txt-bold" Text="Group"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtStreamPanel" CssClass="form-control-blue validatetxt1" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" class="col-sm-4 txt-bold" Text="Remark"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtRemarkPanel" CssClass="form-control-blue" Rows="1" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:LinkButton ID="lnkUpdate" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();"
                                             OnClick="lnkUpdate_Click" CssClass="button-y">Update</asp:LinkButton>
                                        &nbsp;&nbsp;
                                                <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n">Cancel</asp:LinkButton>

                                        <asp:Button ID="Button1" runat="server" Style="display: none" />
                                        <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="lnkCancel" PopupControlID="Panel1"
                                            TargetControlID="Button1" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close" PopupDragHandleControlID="Panel1">
                                        </ajaxToolkit:ModalPopupExtender>
                                </tr>

                            </table>

                        </asp:Panel>
                    </div>
                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                            <table class="tab-popup text-center">
                                <tr>
                                    <td>
                                        <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                        </h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button8" runat="server" CausesValidation="False" Text="No" CssClass="button-n" />
                                         &nbsp; &nbsp;
                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" CssClass="button-y" />
                                       
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                            <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button8" DynamicServicePath=""
                                Enabled="True" PopupControlID="Panel2" TargetControlID="Button7" BackgroundCssClass="popup_bg">
                            </ajaxToolkit:ModalPopupExtender>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

