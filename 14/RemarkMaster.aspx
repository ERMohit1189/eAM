<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="RemarkMaster.aspx.cs" Inherits="RemarkMaster" %>

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

                                <div class="col-sm-12  no-padding" runat="server">
                                    <div class="col-sm-8">
                                        <label class="control-label">Remark&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox CssClass="form-control-blue validatetxt" ID="txtRemark" runat="server" onkeyup="CopyTextBox(this,'#ContentPlaceHolder1_ContentPlaceHolderMainBox_txtSubjectCode');"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="control-label">Status&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList CssClass="form-control-blue validatedrp" ID="drpStatus" runat="server">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="Active" Selected="True">Active</asp:ListItem>
                                                <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2" style="padding-top:26px;">
                                        <asp:Button ID="btnInserts" runat="server" OnClick="btnInserts_Click" OnClientClick="ValidateDropdown('.validatedrp');ValidateTextBox('.validatetxt');return validationReturn();" CssClass="button form-control-blue " Text="Submit"  />
                                        <div id="msgbox" runat="server" style="left:155px;"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <br />
                                    <div class="table-responsive table-responsive2">
                                        <asp:GridView ID="Grid" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-head-border table-bordered">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3dd" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Remark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle"/>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblIsLock" Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="Status" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LoginName" runat="server" Text='<%#Eval("LoginName") %>'></asp:Label><br />
                                                        (<asp:Label ID="recordDate" runat="server" Text='<%#Eval("recordDate") %>'></asp:Label>)
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="180px"/>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" title="Edit"  OnClick="lbtnEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>                                                  
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" title="Delete"  class="btn menu-icon vd_bd-red vd_red">
                                                            <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="40px" />
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
                                <asp:Label ID="llll" runat="server" Text="Remark *"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEditIsLock" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblEditValueId" Visible="false" runat="server"></asp:Label>
                                <asp:TextBox ID="txtEditRemark" CssClass="textbox validatetxt1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="Labeld1" runat="server" Text="Status *"></asp:Label>
                            </td>
                             <td>
                                 <asp:DropDownList CssClass="form-control-blue validatedrp1" ID="drpEditStatus" runat="server">
                                     <asp:ListItem Value=""><--Select--></asp:ListItem>
                                     <asp:ListItem Value="Active">Active</asp:ListItem>
                                     <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                 </asp:DropDownList>
                             </td> 
                        </tr>
                        <tr>
                            <td></td>
                            <td>

                                <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" OnClientClick="ValidateDropdown('.validatedrp1');ValidateTextBox('.validatetxt1');return validationReturn();" Text="Update" />
                                &nbsp; &nbsp;
                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button5" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="pnlDelete" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblClassids" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                                <asp:Button ID="btnNo" runat="server" CssClass="button-n" CausesValidation="False" Text="No" OnClick="btnNo_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnYes" runat="server" CssClass="button-y" CausesValidation="False" Text="Yes" OnClick="btnYes_Click" />

                            </td>
                        </tr>
                    </table>
                    <ajaxToolkit:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                        Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

