<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ListOfAlumni.aspx.cs" Inherits="ListOfAlumni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>


    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix" style="min-height:600px;">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding">
                                     <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Year of Passing</label>
                                        <div class="">
                                                <asp:DropDownList runat="server" ID="drpLastYearAttended"></asp:DropDownList>
                                            </div>
                                    </div>
                                    
                                    <div class="col-sm-2">
                                        <label class="control-label">Status</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpStatus" runat="server" class="form-control-blue">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                <asp:ListItem Value="Active">Active</asp:ListItem>
                                                <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <label class="control-label">Name/ Contact No./ Email</label>
                                        <div class="">
                                            <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control-blue">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="" style="margin-top: 25px;">
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="button form-control-blue">View</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  mgbt-xs-10" runat="server" id="divExport" visible="false">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                    title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                    title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                    title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

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

                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div id="gdv1" runat="server">
                                                    <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table">
                                                        <tr>
                                                            <td>
                                                                <div id="header" runat="server" class="col-md-12 no-padding"></div>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                    <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                    <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                    
                                                        <asp:GridView ID="GrdDiscountDetails" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" ShowFooter="True">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label34" runat="server" Text='<%# Container.DataItemIndex+1  %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="90px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Contact No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="90px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Email">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label35" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Current Occupation">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("CurrentOccupation") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Year of passing">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblClass" runat="server" Text='<%# Bind("LastAttendedYear") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="90px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                               
                                                                <asp:TemplateField HeaderText="Current City">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CityName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="190px" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Photo">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="Label3" runat="server" ImageUrl='<%# Bind("RecentPhoto") %>' style="    height: 70px;"></asp:Image>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                
                                                            </Columns>
                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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

