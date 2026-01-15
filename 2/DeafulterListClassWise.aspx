<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DeafulterListClassWise.aspx.cs"
    Inherits="_2.AdminDeafulterListClassWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top ">
                    <div class="panel-body ">
                        <asp:UpdatePanel ID="tyu" runat="server">
                            <ContentTemplate>


                                <table style="text-align: center;">
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="LnkShow" runat="server" OnClick="LnkShow_Click" CssClass="button">View</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 60px;"></div>

                                        </td>
                                    </tr>
                                </table>

                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel1" runat="server">


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
                                <div id="divExport" runat="server">
                                    <table id="abc" runat="server" width="100%" cssclass="table no-bm p-table p-table-bordered table-hover table-striped table-bordered pro-table">
                                        <tr>
                                            <td colspan="2">
                                                <div class="col-sm-12 no-padding" id="header" runat="server"></div>
                                            </td>
                                        </tr>
                                        <tr style="text-align: center;">
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Balance List (For Arrears)"></asp:Label>
                                                <asp:Label ID="lblDate" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="GrdClass" runat="server" AutoGenerateColumns="False" ShowFooter="True" 
                                                    CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center pro-table"
                                                    Width="100%" OnSelectedIndexChanged="GrdClass_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <th class="text-left">
                                                                            <b>&nbsp;
                                                                                <asp:Label ID="lblClass" runat="server" Style="text-align: left" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                            </b>
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="GrdDiscountDetails" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                                                Width="100%" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                                                                <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="#">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label34" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="S.R. No.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Enrollment No" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label35" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Student's Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Class">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblClass0" runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Section">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSection" runat="server" Text='<%# Bind("Section") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Arrears Session">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("ArrierSession") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Arrears Amount">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ArrearAmt") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <asp:Label ID="lblArrierTotalAmount" runat="server" ForeColor="Red"></asp:Label>
                                                                                        </FooterTemplate>
                                                                                        <HeaderStyle HorizontalAlign="right" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                        <FooterStyle HorizontalAlign="right" VerticalAlign="Middle" />
                                                                                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Remark"  Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="Label37" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
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
</asp:Content>
