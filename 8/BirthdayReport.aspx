<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="BirthdayReport.aspx.cs" Inherits="_8.AdminBirthdayReport" %>

<%@ Register Src="~/admin/usercontrol/loader.ascx" TagPrefix="uc1" TagName="loader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Birthday Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server"> 
    <uc1:loader runat="server" ID="loader" />
    <asp:UpdatePanel ID="upMaun" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  ">
                                    <div class="table-responsive2  table-responsive">
                                        <asp:Label runat="server" ID="lblNorecord" Visible="false" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="gvEmployee" AutoGenerateColumns="false" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Emp. ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Bind("EmpId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Bind("name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Bind("EFatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Bind("EMobileNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                       <asp:Label ID="Label1" runat="server" Text='<% #Bind("DepartmentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                       <asp:Label ID="Label1" runat="server" Text='<% #Bind("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="gvStudent" AutoGenerateColumns="false" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Student's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Bind("name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Bind("FamilyContactNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gender">
                                                    <ItemTemplate>
                                                       <asp:Label ID="Label1" runat="server" Text='<% #Bind("Gender") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                       <asp:Label ID="Label1" runat="server" Text='<% #Bind("class") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-center"></HeaderStyle>
                                                    <ItemStyle CssClass="text-center"></ItemStyle>
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



