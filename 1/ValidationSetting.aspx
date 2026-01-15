<%@ Page Language="C#"  MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ValidationSetting.aspx.cs" Inherits="administrator_ValidationSetting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
   
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                 <div class="col-sm-12 no-padding">
                                <div class="col-sm-4 col-xs-4">
                                    <label>Control Name</label>
                                    <asp:DropDownList runat="server" ID="ddlControlType" CssClass="form-control-blue"></asp:DropDownList>
                                </div>
                                 <div class="col-sm-4 col-xs-4"><label>Is Apply</label>
                           <asp:DropDownList runat="server" ID="ddlStatus" 
                              CssClass="form-control-blue"
                              >
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                              </asp:DropDownList>
                                </div>
                               
                                <div class="col-sm-4  col-xs-4 mgbt-xs-15">
                                    <label class="control-label">&nbsp;</label>
                                    <div>
                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px;"></div>
                                    </div>
                                    
                                </div>
                                     </div>
                                <div class="col-sm-12  ">
                                    <div class="table-responsive2 table-responsive">

                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Controls" HeaderText="Control Name" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                                                <asp:BoundField DataField="Status" HeaderText="Is Apply" HeaderStyle-CssClass="vd_bg-blue vd_white" />
                          
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" title="Edit" 
                                                            OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                              <%--  <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkConfirmDelete" runat="server" OnClick="lnkConfirmDelete_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>


                                    </div>
                                </div>
                            </div>
                        </div>
            
                         <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown" >
                            <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always auto-set-height">
                                <div class="col-sm-12 ">
                                    <table class="tab-popup">
                                        <tr>
                                            <td>Control Name <span class="vd_red">*</span></td>
                                            <td>

                           <asp:DropDownList runat="server" ID="DropDownList1" 
                               CssClass="form-control-blue">
                                
                              </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Is Apply
                                            </td>
                                            <td>
                                                
                           <asp:DropDownList runat="server" ID="DropDownList2" 
                              CssClass="form-control-blue">
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                              </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="Button3" runat="server" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                  <asp:Label ID="lblbranchID" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button4" PopupControlID="Panel1"
                            CancelControlID="Button4" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close"></asp:ModalPopupExtender>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
