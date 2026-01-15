<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ConcessionReport.aspx.cs" Inherits="_2.AdminSetConcession" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpclass" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpclass1_SelectedIndexChanged" ValidationGroup="b">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        &nbsp;&nbsp;
                                        <div id="msgbox" runat="server"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">


                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
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

                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <div id="divExport" runat="server">
                                            <table id="abc" runat="server" width="100%">
                                                <tr style="text-align: center;">
                                                    <td>
                                                        <asp:Image ID="Image1" runat="server" Height="71px" Width="73px" />
                                                        <asp:Label ID="lblCollegeName" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="text-align: center;">
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text=" Fee Concession Report"></asp:Label>
                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="Grd0" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" ShowFooter="true">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsrnos" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label32" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Enrollment No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label33" runat="server" Text='<%# Bind("StEnCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label34" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                        <asp:Label ID="Label35" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Class">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label38" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Section">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label39" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Medium">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label40" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Fee Category" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label41" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Admission Date ">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label42" runat="server" Text='<%# Bind("DateOfAdmiission") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Insttalment">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label45" runat="server" Text='<%# Bind("Insttalment") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Table Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label43" runat="server" Text='<%# Bind("TableName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Concession">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label44" runat="server" Text='<%# Bind("Concession") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="150px" />
                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="grid_heading_default" />
                                                            <RowStyle CssClass="grid_details_default" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
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

