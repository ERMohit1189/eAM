<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="CoscholasticAllotment.aspx.cs" Inherits="admin_CoscholasticAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top ">
                    <div class="panel-body ">
                        <div class="col-sm-12 no-padding ">
                            <div class="col-sm-5 no-padding mgbt-xs-20">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" class="col-sm-4  txt-middle-l txt-bold " Text="Select *"></asp:Label>
                                    <div class="col-lg-8 control">
                                        <asp:DropDownList ID="drpEnter" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem Value="EmpId">Emp Id</asp:ListItem>
                                            <asp:ListItem Value="ECode">Emp Code</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5 no-padding mgbt-xs-20">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" class="col-sm-4  txt-middle-l txt-bold " Text="Enter *"></asp:Label>
                                    <div class="col-lg-8 control">
                                        <asp:TextBox ID="txtEnter" runat="server" CssClass="form-control-blue" onKeyUp=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 no-padding text-center  mgbt-xs-20">
                                <asp:LinkButton ID="lnkShow" runat="server" OnClick="lnkShow_Click" CssClass="button2 form-control-blue">Submit</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-12 no-padding " runat="server" id="div1">
                            <div class="col-sm-12 no-padding ">
                                <div class=" table-responsive  table-responsive2">
                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40px" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Emp Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEcode" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Emp Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Father Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesi" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-sm-12 no-padding ">
                                <div class="col-sm-4 no-padding mgbt-xs-20">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" class="col-sm-4  txt-middle-l txt-bold " Text="Class"></asp:Label>
                                        <div class="col-lg-8 control">
                                            <asp:DropDownList ID="drpclass" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpclass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 no-padding mgbt-xs-20">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" class="col-sm-4  txt-middle-l txt-bold " Text="Medium"></asp:Label>
                                        <div class="col-lg-8 control">
                                            <asp:DropDownList ID="drpmedium" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpmedium_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 no-padding mgbt-xs-20">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" class="col-sm-4  txt-middle-l txt-bold " Text="Co-Scholastic"></asp:Label>
                                        <div class="col-lg-8 control">
                                            <asp:DropDownList ID="drpCoscholastic" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 no-padding mgbt-xs-20 text-center">
                                    <asp:LinkButton ID="btnSubmit" runat="server" CssClass="button2 " OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                </div>

                                <div class="col-sm-12 no-padding ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd1" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center"
                                            OnPreRender="Grd1_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEcode1" runat="server" Text='<%# Eval("Ecode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpId1" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpName1" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Medium">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMedium" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ClassName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClassName" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Co-Scholastic Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCoscholasticName" runat="server" Text='<%# Bind("CoscholasticName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                               <%-- <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkEdit" runat="server" Text='<%# Bind("Id") %>' Font-Size="0"
                                                            Width="16" Height="16" CssClass="edit" OnClick="linkEdit_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkDelete" runat="server" Text='<%# Bind("Id") %>' Font-Size="0"
                                                            Width="16" Height="16" CssClass="delete" OnClick="linkDelete_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                  <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="linkEdit" runat="server" title="Edit" 
                                                                OnClick="linkEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="linkDelete" runat="server" OnClick="linkDelete_Click" title="Delete" CausesValidation="False"
                                                                data-placement="top" class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
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
        </div>
    </div>


    <div style="overflow: auto; width: 1px; height: 1px">
        <asp:Panel ID="Panel1" runat="server" CssClass="popup">
            <table class="table">
                <tr>
                    <td>Class :
                    </td>
                    <td>

                        <asp:DropDownList ID="drpclass1" runat="server" CssClass="textbox" Width="200" AutoPostBack="True"
                            OnSelectedIndexChanged="drpclass1_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="lblid" runat="server" Text="" Visible="false"></asp:Label>


                    </td>
                </tr>
                <tr>
                    <td>Medium :
                    </td>
                    <td>

                        <asp:DropDownList ID="drpmedium1" runat="server" CssClass="textbox" Width="200" AutoPostBack="True"
                            OnSelectedIndexChanged="drpmedium1_SelectedIndexChanged">
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td>Co-Scholastic :
                    </td>
                    <td>

                        <asp:DropDownList ID="drpCoscholastic1" runat="server" CssClass="textbox" Width="200">
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                <td>&nbsp;
                </td>
                <td>

                    <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button" OnClick="lnkUpdate_Click">Update</asp:LinkButton>
                    <asp:LinkButton ID="lnkCancle" runat="server" CssClass="button">Cancle</asp:LinkButton>

                </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />
        <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
            BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True"
            PopupControlID="Panel1" TargetControlID="Button2" CancelControlID="lnkCancle">
        </asp:ModalPopupExtender>
    </div>

    <div style="overflow: auto; width: 1px; height: 1px">
        <asp:Panel ID="Panel2" runat="server" CssClass="popup">
            <table width="100%">
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <h4>Do you really want to delete this record?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                        </h4>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" CssClass="button" CausesValidation="False" Text="Yes" />
                        &nbsp;
                        <asp:Button ID="Button8" runat="server" CssClass="button" Text="No" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
            PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
        </asp:ModalPopupExtender>
    </div>
</asp:Content>

