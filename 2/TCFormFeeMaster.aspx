<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TCFormFeeMaster.aspx.cs" Inherits="TCFormFeeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                         <label class="control-label">Copy&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlCopyType" runat="server" class="form-control-blue">
                                                <asp:ListItem Value="Original">Original</asp:ListItem>
                                                <asp:ListItem Value="Duplicate">Duplicate</asp:ListItem>
                                                <asp:ListItem Value="Triplicate">Triplicate</asp:ListItem>
                                                <asp:ListItem Value="Quadruplicate">Quadruplicate</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Amount&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                        </div>
                                    </div>

                                    

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px;"></div>

                                    </div>
                                </div>



                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stream">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Branch") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Copy">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabeCopy" runat="server" Text='<%# Bind("CopyType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" title="Edit" 
                                                            OnClick="lnkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                       
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
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
                        <%--<tr>
                            <td>Class
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>Amount
                            </td>
                            <td>
                                <asp:TextBox ID="txtAmount0" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="imp"
                                    ControlToValidate="txtAmount0" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>

                                <asp:LinkButton ID="lnkUpdate" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CssClass="button-y" OnClick="lnkUpdate_Click" ValidationGroup="b">Update</asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lnkCancel" runat="server" CssClass="button-n">Cancel</asp:LinkButton>
                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
                <%-- ReSharper disable once Asp.InvalidControlType --%>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="lnkCancel"
                    BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True"
                    PopupControlID="Panel1" TargetControlID="Button1">
                </asp:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?</h4>
                                <asp:Label ID="lblvalue"
                                    runat="server" Visible="False"></asp:Label></td>
                        </tr>

                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="Button8" runat="server" CausesValidation="False" Text="No" CssClass="button-n" />
                                &nbsp;&nbsp;  
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False"
                                    OnClick="btnDelete_Click" Text="Yes" CssClass="button-y" />



                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
                <%-- ReSharper disable once Asp.InvalidControlType --%>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button8"
                    BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True"
                    PopupControlID="Panel2" TargetControlID="Button2">
                </asp:ModalPopupExtender>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

