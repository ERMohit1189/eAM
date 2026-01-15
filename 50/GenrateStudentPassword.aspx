<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="GenrateStudentPassword.aspx.cs" Inherits="admin_GenrateStudentPassword" %>

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
                                        <label class="control-label">Institute Branch</label>
                                        <div class="">
                                                <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Session</label>
                                        <div class="">
                                                <asp:DropDownList runat="server" ID="DrpSessionName" AutoPostBack="true" OnSelectedIndexChanged="DrpSessionName_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="true" 
                                                OnSelectedIndexChanged="drpClass_SelectedIndexChanged" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Section&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Stream&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlBranchs" runat="server" AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlBranchs_SelectedIndexChanged" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click" CssClass="button">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="1000" CssClass="table table-striped no-bm table-hover no-head-border table-bordered pro-table">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClassName" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Password">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPassword" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

