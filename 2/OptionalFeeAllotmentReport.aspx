<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="OptionalFeeAllotmentReport.aspx.cs" Inherits="OptionalFeeAllotmentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
    <div id="loader" runat="server"></div>
    <script>
        Sys.Application.add_load(getStudentsList);
        
    </script>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12  no-padding ">
                            <div class="col-sm-2" runat="server" id="divFeeCategory">
                                        <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlFeeCategory" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlFeeCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                    </div>
                            
                            <div class="col-sm-2   mgbt-xs-15">
                                <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                            <asp:DropDownList ID="DrpClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                </div>
                            </div>
                             <div class="col-sm-2   mgbt-xs-15">
                                <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-2   mgbt-xs-15">
                                <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                <div class="">
                                    <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-2" runat="server" id="divInstallment">
                                        <label class="control-label">Installment&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="drpInstallment" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                    </div>
                            <div class="col-sm-2   mgbt-xs-15">
                                <div class="" style="margin-top: 25px;">
                                  <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"  OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn(this);" CssClass="button form-control-blue ">View</asp:LinkButton>
                                    <div id="msgbox" runat="server" style="left: 75px;"></div>
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
                                                            <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;">Optional Fee Allotment Report</asp:Label><br />
                                                                <asp:Label ID="lblInstallment" runat="server" Style="font-weight: 600; font-size: 13px;"></asp:Label>
                                                                <asp:GridView ID="GrdDisplay" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group  ">
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="40px" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S.R. No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="SrNo" runat="server"  Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Student's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Name" runat="server"  Text='<%# Bind("Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Class">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="CombineClassName" runat="server"  Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField HeaderText="Optional Fee Heads">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="OptFeeHeads" runat="server"  Text='<%# Bind("OptFeeHeads") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="FooterTotal" runat="server" Style="font-weight: bold"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
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

