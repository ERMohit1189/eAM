<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TestPermissionCBSE.aspx.cs" Inherits="TestPermissionCBSE" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <style>
                .table > tbody > tr > td {
                    padding: 2px 5px !important;
                }
            </style>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                         <div class="col-sm-12   mgbt-xs-15">
                                           <span class="txt-bold txt-middle-l text-primary">Note: </span><span class="txt-bold txt-middle-l text-danger blink">Select Yes for Display and No for Don't Display.</span></div>
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
                                                                    <asp:Label ID="ClassId" Visible="false" runat="server" Text='<%# Eval("ClassId") %>'></asp:Label>
                                                                    <asp:Label ID="BranchId" Visible="false" runat="server" Text='<%# Eval("BranchId") %>'></asp:Label>
                                                                    <asp:Label ID="Class" runat="server" Text='<%# Eval("CombineClass") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Test1">
                                                                <HeaderTemplate>
                                                                    <asp:DropDownList runat="server" ID="ddlTest1H" OnSelectedIndexChanged="ddlTest1H_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="">Display Test1</asp:ListItem>
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList runat="server" ID="rdoTest1" CssClass="vd_radio radio-success" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                                
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Test2">
                                                                <HeaderTemplate>
                                                                    
                                                                    <asp:DropDownList runat="server" ID="ddlTest2H" OnSelectedIndexChanged="ddlTest2H_SelectedIndexChanged" AutoPostBack="true">
                                                                       <asp:ListItem Value="">Display Test2</asp:ListItem>
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                   <asp:RadioButtonList runat="server" ID="rdoTest2" CssClass="vd_radio radio-success" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Test3">
                                                                <HeaderTemplate>
                                                                   
                                                                    <asp:DropDownList runat="server" ID="ddlTest3H" OnSelectedIndexChanged="ddlTest3H_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="">Display Test3</asp:ListItem>
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList runat="server" ID="rdoTest3" CssClass="vd_radio radio-success" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Test4">
                                                                <HeaderTemplate>
                                                                    
                                                                    <asp:DropDownList runat="server" ID="ddlTest4H" OnSelectedIndexChanged="ddlTest4H_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="">Display Test4</asp:ListItem>
                                                                       <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList runat="server" ID="rdoTest4" CssClass="vd_radio radio-success" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Test5">
                                                                <HeaderTemplate>
                                                                    
                                                                    <asp:DropDownList runat="server" ID="ddlTest5H" OnSelectedIndexChanged="ddlTest5H_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="">Display Test5</asp:ListItem>
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList runat="server" ID="rdoTest5" CssClass="vd_radio radio-success" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Test6">
                                                                <HeaderTemplate>
                                                                    
                                                                    <asp:DropDownList runat="server" ID="ddlTest6H" OnSelectedIndexChanged="ddlTest6H_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="">Display Test6</asp:ListItem>
                                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList runat="server" ID="rdoTest6" CssClass="vd_radio radio-success" RepeatDirection="Horizontal">
                                                                         <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Test6" Visible="false">
                                                                <HeaderTemplate>
                                                                    
                                                                    <asp:TextBox runat="server" ID="txtPassingMarkH" OnTextChanged="txtPassingMark_TextChanged" AutoPostBack="true" placeholder="Passing Marks(%)" style="width: 123px;"></asp:TextBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <asp:TextBox runat="server" ID="txtPassingMark" style="width: 123px;"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-sm-12  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="button form-control-blue" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                            </div>
                                            <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                                <div id="msgbox" runat="server"></div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

