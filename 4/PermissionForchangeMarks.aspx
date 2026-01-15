<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="PermissionForchangeMarks.aspx.cs" Inherits="SuperAdmin_PermissionForchangeMarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
       <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding " runat="server" id="divTools">

                                    <div class="col-sm-4  no-padding half-width-50 mgbt-xs-15">
                                        <label class="col-sm-5  control-label">Permission For&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" col-sm-7 ">
                                            <asp:RadioButtonList ID="RadioButtonList1" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vd_radio radio-success" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                    <asp:ListItem>Admin</asp:ListItem>
                                                    <asp:ListItem Selected="True">Staff</asp:ListItem>
                                                </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>   

                                    <div class="col-sm-4  no-padding half-width-50 mgbt-xs-15" id="staffdiv" runat="server">
                                        <label class="control-label col-sm-4 ">Department &nbsp;<span class="vd_red">*</span></label>
                                        <div class="col-sm-8 ">
                                            <asp:DropDownList ID="DrpDepartment" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DrpDepartment_SelectedIndexChanged" CssClass="form-control-blue">
                                                </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>   
                                </div>

                                <div class="col-sm-12 " runat="server" id="divGrid">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered ">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40px" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Login Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("LoginId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label19" runat="server" Text='<%# Bind("MobileNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delay">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="true" OnCheckedChanged="ChkAll_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </div>
                                 <div class="col-sm-12 ">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" OnClick="LinkButton1_Click">Submit</asp:LinkButton>
                                <div id="msgbox" runat="server" style="left: 75px;"></div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

