
<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DueDepositBalance.aspx.cs" Inherits="DueDepositBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
     <script src="../js/jquery.min.js"></script>
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
                            <div class="col-sm-12  no-padding " style="border-bottom:1px solid #ccc;" runat="server" id="divtp">
                                 <div class="col-sm-4 ">
                                    <asp:RadioButtonList ID="reportType" runat="server" class="form-control-blue vd_radio radio-success" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="reportType_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="True">Consolidated</asp:ListItem>
                                        <asp:ListItem Value="1">Class wise</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                            <div class="col-sm-4 " id="divBranch" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <label class="control-label">Institute Branch&nbsp;<span class="vd_red">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlBranch" CssClass="validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-2 " id="divSession" runat="server">
                                <label class="control-label">Session&nbsp;<span class="vd_red">*</span></label>
                                    <asp:DropDownList runat="server" ID="DrpSessionName"  AutoPostBack="true" CssClass="validatedrp" OnSelectedIndexChanged="DrpSessionName_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                           
                            
                            <div class="col-sm-3 " id="divClass" runat="server" visible="false">
                                <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                            <asp:DropDownList ID="DrpClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                </div>
                            </div>
                             <div class="col-sm-2 " id="divSection" runat="server" visible="false">
                                <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                </div>
                            </div>
                             <div class="col-sm-3 " id="divStream" runat="server" visible="false">
                                <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                <div class="">
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-2 half-width-50"  id="divStatus" runat="server">
                                <label class="control-label">Status&nbsp;<span class="vd_red"></span></label>
                                <div class="">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" CssClass="form-control-blue">
                                        <asp:ListItem Value="0"><--Select--></asp:ListItem>
                                        <asp:ListItem Value="" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                           
                            
                            <div class="col-sm-6 ">
                                <div class="" style="margin-top: 25px;">
                                  <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue ">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 60px; color:red;"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12  mgbt-xs-10" runat="server" id="divExports" visible="false">
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
                                                <table align="center" id="abc" runat="server" visible="false" width="100%" class="table no-p-b-table">
                                                    <tr>
                                                        <td>
                                                            <div id="header" runat="server" class="col-md-12 no-padding"></div>
                                                            <div id="Div1" runat="server" class="col-md-12 no-padding text-center">
                                                                <asp:Label ID="lblOrganization" runat="server" Style="font-weight: 700; font-size: 16px;"></asp:Label><br />
                                                                    <asp:Label ID="lblAddress" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                <asp:GridView ID="ConsolidatedGrid" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="40px" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Class">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="CombineClassName" runat="server"  Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="CTotal" runat="server"  Text="Total"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true"   />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  Font-Bold="true" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Due">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="cDue" runat="server"  Text='<%# Bind("PayableAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="CDueTotal" runat="server" ></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true"   />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  Font-Bold="true" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Deposit">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="cDeposit" runat="server"  Text='<%# Bind("PaidAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="CDepositTotal" runat="server" ></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true"   />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  Font-Bold="true" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Balance">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="cBalance" runat="server"  Text='<%# Bind("BalanceAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="CBalanceTotal" runat="server" ></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true"   />
                                                                           <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  Font-Bold="true" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <asp:GridView ID="ClasswiseGrid" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group  ">
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="40px" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S. R. No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="srno" runat="server"  Text='<%# Bind("srno") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Student's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="name" runat="server"  Text='<%# Bind("name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="ClassTotal" runat="server"  Text="Total"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true"   />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  Font-Bold="true" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Due">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ClassDue" runat="server"  Text='<%# Bind("PayableAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="ClassDueTotal" runat="server" ></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true"   />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  Font-Bold="true" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Deposit">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ClassDeposit" runat="server"  Text='<%# Bind("PaidAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="ClassDepositTotal" runat="server" ></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true"   />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  Font-Bold="true" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Balance">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ClassBalance" runat="server"  Text='<%# Bind("BalanceAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="ClassBalanceTotal" runat="server"  Text="Total"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true"   />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  Font-Bold="true" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
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

