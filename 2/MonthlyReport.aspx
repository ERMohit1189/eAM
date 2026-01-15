
<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MonthlyReport.aspx.cs" Inherits="MonthlyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
     <script src="../js/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
    
    <script>
        Sys.Application.add_load(getStudentsList);
        
    </script>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12  no-padding">
                        <div runat="server" id="msg1" class="text-danger"></div></div>
                        <div class="col-sm-12  no-padding " runat="server" id="divMain">
                            

                            <div class="col-sm-4  " id="divBranch" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                        <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-2  " id="divSession" runat="server">
                                <label class="control-label">Session</label>
                                <div class="">
                                    <asp:DropDownList runat="server" ID="DrpSessionName"></asp:DropDownList>
                                </div>
                            </div>
                           <div class="col-sm-2   mgbt-xs-15">
                                <label class="control-label">Months</label>
                                <div class="">
                                    <asp:DropDownList ID="ddlMonths" runat="server" CssClass="vd_radio radio-success">
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2   mgbt-xs-15">
                                <label class="control-label">Mode of Payment</label>
                                <div class="">
                                    <asp:DropDownList ID="DdlpaymentMode" runat="server" CssClass="vd_radio radio-success">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>DD</asp:ListItem>
                                        <asp:ListItem>Card</asp:ListItem>
                                        <asp:ListItem>Online Transfer</asp:ListItem>
                                        <asp:ListItem>Other</asp:ListItem>
                                        <asp:ListItem Value="Online">Online</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2   mgbt-xs-15">
                                <label class="control-label">Status</label>
                                <div class="">
                                    <asp:DropDownList ID="drpStatus" runat="server" class="form-control-blue ">
                                        <asp:ListItem>Paid</asp:ListItem>
                                        <asp:ListItem>Pending</asp:ListItem>
                                        <asp:ListItem>Cancelled</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="" style="margin-top: 25px;">
                                  <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 mgbt-xs-10">
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

                                <div class="col-sm-12">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                 <div id="gdv1" runat="server">
                                                <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table" visible="false">
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
                                                                <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Class">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("classname") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="1">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s1" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="f1" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="2">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s2" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f2" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="3">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s3" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f3" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="4">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s4" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f4" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="5">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s5" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f5" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="6">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s6" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f6" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="7">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s7" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f7" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="8">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s8" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f8" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="9">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s9" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f9" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="10">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s10" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f10" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="11">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s11" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f11" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s12" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f12" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="13">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s13" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f13" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="14">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s14" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f14" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="15">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s15" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f15" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="16">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s16" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f16" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="17">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s17" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f17" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="18">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s18" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f18" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="19">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s19" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f19" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="20">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s20" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f20" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="21">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s21" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f21" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="22">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s22" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f22" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="23">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s23" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f23" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="24">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s24" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f24" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="25">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s25" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f25" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="26">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s26" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f26" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="27">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s27" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f27" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="28">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s28" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f28" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="29">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s29" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f29" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="30">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s30" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f30" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="31">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="s31" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <FooterTemplate>
                                                                                <asp:Label ID="f31" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Total">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGtotal" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                              <FooterTemplate>
                                                                                <asp:Label ID="ftotal" runat="server"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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

