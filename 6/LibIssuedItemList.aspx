<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="LibIssuedItemList.aspx.cs" Inherits="admin_LibIssuedItemList" %>

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
                                     <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <div class="">
                                            <asp:DropDownList ID="rdotype" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="Student" Selected="True">Student</asp:ListItem>
                                                <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                       <div class="">
                                    <asp:DropDownList runat="server" ID="ddlIssuedItemReport" CssClass="form-control-blue">
                                        <asp:ListItem Selected="True" Text="Not Returned Item" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Returned Item" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="Both" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                           </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15 btn-a-devices-1-p2-p2">
                                       <asp:Button runat="server" ID="btnsearch" Text="View" CssClass="button" OnClick="btnsearch_Click" />
                                        <div id="msgbox" runat="server" style="left: 90px !important;"></div>
                                   </div>
                                </div>

                                <div class="col-sm-12  mgbt-xs-10">
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
                                <div class="col-sm-12  text-center table-responsive" id="divExport" runat="server" >
                                    <table style="width:100% !important;">
                                        <tr>
                                            <td><div id="header" runat="server" style="width:100% !important;"></div></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label runat="server" ID="lblheadername" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    
                                    
                                     <tr>
                                            <td>
                                    <asp:GridView ID="grd1" runat="server" OnRowDataBound="grd1_RowDataBound"  CssClass="table table-striped no-bm table-hover no-head-border table-bordered pro-table text-left">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td></tr>
                                        </table>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

