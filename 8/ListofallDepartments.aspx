<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ListofallDepartments.aspx.cs"
    Inherits="admin_ListofallDepartments" %>

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

                                    <div class="col-sm-12  mgbt-xs-10">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float:left;">
                                                    <div id="msgbox" runat="server"></div>
                                                </div>
                                                <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                    <asp:LinkButton ID="ImageButton1" runat="server" CssClass="icon-word-color" OnClick="ImageButton1_Click" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" CssClass="icon-excel-color" OnClick="ImageButton2_Click" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" CssClass="icon-pdf-color" OnClick="ImageButton3_Click" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton4" runat="server" CssClass="icon-print-color" OnClick="ImageButton4_Click" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
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


                                    <div class="col-sm-12  ">
                                        <div class=" table-responsive  table-responsive2" id="abc" runat="server">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table pro-table table-striped table-hover no-bm no-head-border table-bordered">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("EmpDepName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <SelectedRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
