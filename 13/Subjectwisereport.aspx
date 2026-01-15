<%@ Page Title="Subject wise Result | eAM&#174;" Language="C#" MasterPageFile="stuRootManager.master" AutoEventWireup="true" CodeFile="Subjectwisereport.aspx.cs"
    Inherits="Subjectwisereport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-3">
                                        <label class="control-label">Term Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlTerm" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-9 btn-a-devices-3-p6 mgbt-xs-15" style="padding-top:25px;">
                                        <asp:LinkButton ID="btnView" runat="server" OnClick="btnView_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12  ">
                                    <div class=" table-responsive  table-responsive2 text-center">
                                        <asp:Repeater ID="rpter" runat="server">
                                            <ItemTemplate>
                                                <table class="table table-striped no-bm table-hover no-head-border table-bordered">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Bind("id") %>'></asp:Label>
                                                            <asp:Label ID="lblTerm" runat="server" Text='<%# Bind("TermName") %>'></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                                                <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsl" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Subject">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Paper") %>'></asp:Label>
                                                                            <asp:Label ID="SubjectId" runat="server" Visible="false" Text='<%# Bind("id") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="50" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Max. Marks">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMax" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="170" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Obtaind Marks">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblObtaind" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="170" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        <asp:Chart ID="Chart1" runat="server" BorderlineWidth="0">
                                                        <Titles>
                                                            <asp:Title Docking="Bottom" Name="Title1" Text="" />
                                                        </Titles>
                                                        <Legends>
                                                            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                                        </Legends>
                                                        <Series>
                                                            <asp:Series Name="Default" XValueMember="Marks" YValueMembers="Subjects" IsValueShownAsLabel="false" />
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                            </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
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
