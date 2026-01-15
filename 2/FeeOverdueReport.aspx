<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="FeeOverdueReport.aspx.cs" Inherits="FeeOverdueReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="loader" runat="server"></div>
            <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding ">
                                    <div class="col-sm-12 no-padding " style="border-bottom: 1px solid #ccc;">
                                        <div class="col-sm-4">
                                            <asp:RadioButtonList ID="reportType" runat="server" class="form-control-blue vd_radio radio-success" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="reportType_SelectedIndexChanged">
                                                <asp:ListItem Value="Fee" Selected="True">Fee</asp:ListItem>
                                                <asp:ListItem Value="Arrear">Arrear</asp:ListItem>
                                                <asp:ListItem Value="Other Fee">Other Fee</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="col-sm-4" id="divBranch" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                                <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-2" id="divSession" runat="server">
                                        <label class="control-label">Session</label>
                                            <asp:DropDownList runat="server" ID="DrpSessionName"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2" runat="server" id="divFeeCategory">
                                        <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="ddlFeeCategory" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlFeeCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2" runat="server" id="divType">
                                        <label class="control-label">Type</label>
                                            <asp:DropDownList ID="drpFeeGroup" runat="server" class="form-control-blue ">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Defaulter</asp:ListItem>
                                            </asp:DropDownList>
                                            
                                    </div>
                                    <div class="col-sm-2" runat="server" id="divClass">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="DrpClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2" runat="server" id="divSection">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2" runat="server" id="divStream">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                    </div>


                                    <div class="col-sm-2" runat="server" id="divInstallment">
                                        <label class="control-label">Installment&nbsp;<span class="vd_red">*</span></label>
                                            <asp:DropDownList ID="drpInstallment" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                    </div>
                                     <div class="col-sm-2 half-width-50">
                                <label class="control-label">Status&nbsp;<span class="vd_red"></span></label>
                                <div class="">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" CssClass="form-control-blue">
                                        <asp:ListItem Value="0"><--Select--></asp:ListItem>
                                        <asp:ListItem Value="" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                    <div class="col-sm-8  mgbt-xs-15">
                                        <div style="margin-top: 25px;">
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue ">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 60px; color:red;"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server" visible="false">

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
                                <div class="col-sm-12">
                                    <div class="table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div id="gdv1" runat="server">
                                                    <table align="center" id="abc" runat="server" visible="false" width="100%" class="table no-p-b-table">
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

                                                                    <%--<asp:GridView ID="GridViewFeeInstallmentwiseReport" runat="server" class="table table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:GridView>--%>
                                                                    <asp:GridView ID="GridViewFeeInstallmentwiseReport" runat="server" AutoGenerateColumns="false"
                                                                        CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center"
                                                                        ShowFooter="true" Width="100%" OnRowDataBound="GridViewFeeInstallmentwiseReport_RowDataBound">

                                                                    </asp:GridView>
                                                                 <%--   <asp:GridView ID="GridViewFeeOverdueArrear" runat="server" AutoGenerateColumns="false"
                                                                        CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center"
                                                                        ShowFooter="true" Width="100%" OnRowDataBound="GridViewFeeOverdueArrear_RowDataBound">
   
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="GridViewOtherFeeOverDue" runat="server" AutoGenerateColumns="false"
                                                                        CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center"
                                                                        ShowFooter="true" Width="100%" OnRowDataBound="GridViewOtherFeeOverDue_RowDataBound">
   
                                                                    </asp:GridView>--%>

                                                                    
                                                                    <asp:GridView ID="GridViewFeeOverdueArrear" runat="server" class="table table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="false">
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
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true"   />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Father's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="FatherName" runat="server"  Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true"   />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Contact No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="FamilyContactNo" runat="server"  Text='<%# Bind("ContactNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="TotalH" runat="server"  Text="Total"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true"   />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  Font-Bold="true" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Balance">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="BalanceAmount" runat="server"  Text='<%# Bind("BalanceAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="ClassBalanceAmount" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true"   />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  Font-Bold="true" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="GridViewOtherFeeOverDue" runat="server"  ShowFooter="true" class="table table-striped table-hover no-head-border table-bordered" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1ds" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="S. R. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SrNo" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="FatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Contact No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="FamilyContactNo" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Class">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CombineClassName" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="totalh" runat="server" Font-Bold="true">Total</asp:Label>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Amount" runat="server"  Font-Bold="true"  Text='<%# Bind("Amount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="totalM" runat="server" Font-Bold="true"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="grid_heading_default" />
                                                                        <RowStyle CssClass="grid_details_default" />
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
            <style>
                .removeBorder {
                    border-right: 0px !important;
                }

                .removeBorder1 {
                    border-right: 0px !important;
                    border-left: 0px !important;
                }
            </style>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

