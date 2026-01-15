<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="NoticeRemarkforAdmitCard.aspx.cs" Inherits="admin_NoticeRemarkforAdmitCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
                                <div class="col-sm-12   no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From Class</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpFromClass" runat="server" CssClass="form-control-blue"
                                                OnSelectedIndexChanged="drpFromClass_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To Class</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpToClass" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Notice</label>
                                        <div class="">
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2  mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" OnClick="LinkButton1_Click">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>

                                </div>
                                <div class="col-sm-12   ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table" AutoGenerateColumns="false" Width="100%">
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
                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Notice">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="link_Edit" runat="server" title="Edit" 
                                                            OnClick="link_Edit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="link_Delete" runat="server" OnClick="link_Delete_Click" title="Delete" data-toggle="tooltip"
                                                            data-placement="top" class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="link_Edit" runat="server" CssClass="edit" Font-Size="0pt"
                                                            Text='<%# Bind("Id") %>' Height="20px" Width="20px" OnClick="link_Edit_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="link_Delete" runat="server" CssClass="delete" Font-Size="0pt"
                                                            Text='<%# Bind("Id") %>' Height="20px"
                                                            OnClick="link_Delete_Click" Width="20px"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>
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
                            <td>From Class
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFromClass1" runat="server" CssClass="form-control-blue"
                                    OnSelectedIndexChanged="drpFromClass1_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Section
                            </td>
                            <td>
                                <asp:DropDownList ID="drpSection1" runat="server" CssClass="form-control-blue">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Notice/Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemark1" runat="server" CssClass="form-control-blue"></asp:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td></td>
                            <td >
                               
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="button-y" OnClick="LinkButton2_Click">Update</asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="button-n">Cancel</asp:LinkButton>
                                 <asp:Label ID="Label5" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>

                </asp:Panel>

                <asp:Button ID="Button1" runat="server" Text="Button" Style="Display: none" />
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" BackgroundCssClass="popup_bg"
                    TargetControlID="Button1" PopupControlID="Panel1" PopupDragHandleControlID="Panel1" CancelControlID="LinkButton3">
                </asp:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">

                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Do you really want to Delete this record?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button8" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>

                        <tr>
                            <td >
                                <asp:Button ID="Button9" runat="server" CausesValidation="False" Text="No" CssClass="button-n" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" CssClass="button-y" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button2" runat="server" Text="Button" Style="Display: none" />
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" BackgroundCssClass="popup_bg"
                    TargetControlID="Button2" PopupControlID="Panel2" PopupDragHandleControlID="Panel2" CancelControlID="Button9">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

