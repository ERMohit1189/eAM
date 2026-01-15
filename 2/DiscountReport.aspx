<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="DiscountReport.aspx.cs" Inherits="DiscountReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <style>
        table.discTable tbody tr td:nth-child(-n+9) {
            font-size:16px;
            text-align:right;
        }
        table.discTable tbody tr td:nth-child(3), table.discTable tbody tr td:nth-child(4) 
        {text-align:left; }
        table.discTable tbody tr td:nth-child(1) {text-align:center; }
        input {font-size: 16px; }
    </style>
    <div id="loader" runat="server"></div>


    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding " style="border-bottom: 1px solid #ccc;">
                                    <div class="col-sm-6  mgbt-xs-15">
                                        <asp:RadioButtonList ID="reportType" runat="server" class="form-control-blue vd_radio radio-success" AutoPostBack="true" OnSelectedIndexChanged="reportType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Selected="True">Consolidated</asp:ListItem>
                                            <asp:ListItem Value="1">Descriptive</asp:ListItem>
                                            <asp:ListItem Value="2">Manual Discount</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div id="filterSection" runat="server" class="col-sm-12  no-padding">
                                    <div class="col-sm-4   mgbt-xs-15" id="divBranch" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                                <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-2   mgbt-xs-15" id="divSession" runat="server">
                                        <label class="control-label">Session</label>
                                        <div class="">
                                            <asp:DropDownList runat="server" ID="DrpSessionName"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="classSec" class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="DrpClass" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div id="discForSec" runat="server" class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Discount For&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDiscountFor" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Composit Fee</asp:ListItem>
                                                <asp:ListItem>Other Fee</asp:ListItem>
                                                <asp:ListItem>Other Fee</asp:ListItem>
                                                <asp:ListItem>TC Fee</asp:ListItem>
                                                <asp:ListItem>CC Fee</asp:ListItem>
                                                <asp:ListItem>Admission Fee</asp:ListItem>
                                                <asp:ListItem>Additional Fee</asp:ListItem>
                                                <asp:ListItem>Product Fee</asp:ListItem>
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div id="discTypeSec"  runat="server" visible="false" class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Applied From&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpApplyFrom" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Special Discount</asp:ListItem>
                                                <asp:ListItem>Sibling Discount</asp:ListItem>
                                                <asp:ListItem>Complete Fee Discount</asp:ListItem>
                                                <asp:ListItem>Manual Discount</asp:ListItem>
                                                <asp:ListItem>On Page Discount</asp:ListItem>
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div id="fromDate" class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">From&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                           <script>
                                               Sys.Application.add_load(datetime);
                                           </script>
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                      <div id="toDate" class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">To&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                           <script>
                                               Sys.Application.add_load(datetime);
                                           </script>
                                                        <asp:TextBox ID="txtDate2" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-3  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue ">View</asp:LinkButton>
                                        <div id="msgbox" runat="server"></div>
                                    </div>
                                    <div id="divNote" runat="server" class="col-sm-12">
<span><b>Note:- </b><span style="color:red;">Showing report is paid discount report only.</span></span>
                                    </div>
                                    <div id="divNote2" runat="server" visible="false" class="col-sm-12">
<span><b>Note:- </b><span style="color:red;">Showing report is submitted discount report only.</span></span>
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

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                    <asp:Label ID="heading" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                    <asp:Label ID="lblRegister" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                                                    <asp:GridView ID="ConsolidatedGrid" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SrNo" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Class">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CombineClassName" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Discount For">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="DiscountFor" runat="server" Text='<%# Bind("DiscountFor") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="CAmountTotal" runat="server"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="StudentwiseGrid" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group ">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SrNo" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Class">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CombineClassName" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Discount For">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="DiscountFor" runat="server" Text='<%# Bind("DiscountFor") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Applied From">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ApplyFrom" runat="server" Text='<%# Bind("ApplyFrom") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            

                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="CAmountTotal" runat="server"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Username">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br />
                                                                                    <asp:Label ID="RecordedDate" runat="server" Text='<%# Eval("RecordedDate").ToString()==""?"":"("+Eval("RecordedDate").ToString()+")" %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Narration">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Narration" runat="server" Text='<%# Bind("Narration") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                
                                                                    <asp:GridView ID="gvDiscounts" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SrNo" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Student's Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Class">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CombineClassName" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Installment">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="MonthName" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Fee Head">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="FeeHead" runat="server" Text='<%# Bind("FeeHead") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Discount Head">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="DiscHeadName" runat="server" Text='<%# Bind("DiscHeadName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Amount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="CAmountTotal" runat="server"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Username">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br />
                                                                                    <asp:Label ID="RecordedDate" runat="server" Text='<%# Eval("RecordedDate").ToString()==""?"":"("+Eval("RecordedDate").ToString()+")" %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Narration">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Narration" runat="server" Text='<%# Bind("Narration") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
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

    <script>
        function hideSection(param) {
            if (param == 0) {
                $('#fromDate').addClass('hide');
                $('#toDate').addClass('hide');

                $('#classSec').removeClass('hide');
                $('#discTypeSec').removeClass('hide');
                $('#discForSec').removeClass('hide');
            } else {
                $('#fromDate').removeClass('hide');
                $('#toDate').removeClass('hide');

                $('#classSec').addClass('hide');
                $('#discTypeSec').addClass('hide');
                $('#discForSec').addClass('hide');
            }
        }
    </script>
</asp:Content>

