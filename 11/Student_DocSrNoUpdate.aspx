<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="Student_DocSrNoUpdate.aspx.cs" Inherits="Student_DocSrNoUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="thy" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpAtteClass" runat="server" OnSelectedIndexChanged="DrpAtteClass_SelectedIndexChanged"
                                                        AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                   
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpAttenSection" runat="server" 
                                                        CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="btnShow" runat="server" OnClick="btnShow_Click" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 64px"></div>

                                    </div>

                                </div>

                                <div class="col-sm-12  ">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False"
                                                        class="table mp-table no-tb no-bm p-table-bordered table-bordered vd_bg-green form-control-blue vd_white">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label10" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" Width="25px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Bind("admissionNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-3" Width="55px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="p-pad-3" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Old SrNo.">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtOldSrno" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ClassName" runat="server" Text='<%# Bind("class_section") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np p-pad-n" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="p-pad-n" />
                                                            </asp:TemplateField>
                                                           
                                                        </Columns>

                                                    </asp:GridView>
                                                </div>
                                                 <asp:LinkButton ID="LinkSubmit" runat="server" OnClick="LinkSubmit_Click" Visible="false" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                            </asp:Panel>
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

