<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ListOfpublisher.aspx.cs"
    Inherits="ListOfpublisher" %>

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
                                <div class="col-sm-12">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drppublisherCategory" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-8  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" SkinID="save" OnClick="LinkButton1_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12" runat="server" id="divPrinttools" visible="false">

                                    <div class="col-sm-12  mgbt-xs-5">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float: right; font-size: 19px;" id="Panel2" runat="server">


                                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" Visible="false" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

                                                    <script>
                                                        
                                                    </script>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="ImageButton1" />
                                                <asp:PostBackTrigger ControlID="ImageButton2" />
                                                <asp:PostBackTrigger ControlID="ImageButton3" />
                                                <asp:PostBackTrigger ControlID="ImageButton4" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div id="abc" runat="server" class="col-sm-12  ">
                                        <div class=" table-responsive  text-center table-responsive2">
                                            <div id="header" runat="server"></div>
                                            <asp:Label runat="server" ID="lblheadername" Font-Bold="true"></asp:Label>
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered ">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" Width="50px" />
                                                        <ItemStyle CssClass="text-center " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                        <ItemStyle CssClass="text-center " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name of Publisher">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("PublisherName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="vd_bg-blue text-left vd_white" />
                                                        <ItemStyle CssClass="text-left " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                        <ItemStyle CssClass="text-center " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Phone No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Phone1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                        <ItemStyle CssClass="text-center " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Mobile" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                        <ItemStyle CssClass="text-center " />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="vd_bg-blue text-center vd_white" />
                                                        <ItemStyle CssClass="text-center " />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
