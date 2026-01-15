<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AddSubjectDisplayOrder.aspx.cs" Inherits="admin_temp_AddDisplayOrder" %>

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
                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpclass" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True" OnSelectedIndexChanged="drpclass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" SkinID="ddDefault">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject Group&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSubjectGroup" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drpSubjectGroup_SelectedIndexChanged" SkinID="ddDefault">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered ">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" Width="40px" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Display Order">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drpDisplayNo" runat="server" AutoPostBack="true" CssClass="form-control-blue" OnSelectedIndexChanged="drpDisplayNo_SelectedIndexChanged"></asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tab-in" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                               <div class="col-sm-12 ">
                                    <asp:LinkButton ID="LinkButton1" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" runat="server" OnClick="LinkButton1_Click">Submit</asp:LinkButton>
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

