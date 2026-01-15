
<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="BalanceFeeReminder.aspx.cs" Inherits="BalanceFeeReminder" %>

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
                        <div class="col-sm-12  no-padding">
                        <div runat="server" id="msg1" class="text-danger"></div></div>
                        <div class="col-sm-12  no-padding " runat="server" id="divMain">
                            

                            <div class="col-sm-3  " id="divBranch" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <label class="control-label">Institute Branch&nbsp;<span class="vd_red"></span></label>
                                        <asp:DropDownList runat="server" ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3  " id="divSession" runat="server">
                                <label class="control-label">Session</label>
                                <div class="">
                                    <asp:DropDownList runat="server" ID="DrpSessionName"></asp:DropDownList>
                                </div>
                            </div>
                           
                            <div class="col-sm-3">
                                <label class="control-label">Fee Group</label>
                                <div class="">
                                    <asp:DropDownList ID="drpFeeGroup" runat="server" class="form-control-blue">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Tuition Fee</asp:ListItem>
                                        <asp:ListItem>Transport Fee</asp:ListItem>
                                        <asp:ListItem>Hostel Fee</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                            <asp:DropDownList ID="DrpClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                </div>
                            </div>
                             <div class="col-sm-3">
                                <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                <div class="">
                                    <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 half-width-50">
                                <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:DropDownList ID="ddlFeeCategory" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlFeeCategory_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 half-width-50">
                                <label class="control-label">Installment&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:DropDownList ID="drpInstallment" runat="server" CssClass="form-control-blue validatedrp">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 half-width-50">
                                <label class="control-label">Status&nbsp;<span class="vd_red"></span></label>
                                <div class="">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control-blue">
                                        <asp:ListItem Value="0"><--Select--></asp:ListItem>
                                        <asp:ListItem Value="" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="W">Withdrawal</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="" style="margin-top: 25px;">
                                  <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server" visible="false">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color hide"
                                                    title="Export to Word" ><i class="fa fa-file-word-o"></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color hide"
                                                    title="Export to Excel" ><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color hide"
                                                    title="Export to PDF" ><i class="fa  fa-file-pdf-o"></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                    title="Print" ><i class="fa fa-print"></i></asp:LinkButton>

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
                                                <table align="center" id="abc" runat="server" visible="false" width="100%" class="table no-p-b-table" style="margin-bottom:0px !important">
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
                                                                <asp:GridView ID="GridView1" runat="server" class="table table-striped table-hover no-head-border table-bordered" style="margin-bottom:0px !important">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                             <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged"></asp:CheckBox>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
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
                                        <div class="col-sm-6 half-width-50  mgbt-xs-9">
                                            <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" OnClick="LinkButton1_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" ValidationGroup="a">Send SMS</asp:LinkButton>
                                            <div id="msgbox" runat="server" style="left: 75px;"></div>
                                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
            <style>
                .removeBorder {
                    border-right:0px !important;
                }
                .removeBorder1 {
                    border-right:0px !important;
                    border-left:0px !important;
                }
            </style>
            <script>
                function makeGrid() {
                    var len = $('#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr').length - 1;
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(0)").html("");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(1)").html("");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(0)").addClass("removeBorder");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(1)").addClass("removeBorder1");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(1)").addClass("removeBorder1");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(2)").addClass("removeBorder1");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(2)").addClass("removeBorder1");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(3)").addClass("removeBorder1");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(3)").addClass("removeBorder1");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(4)").addClass("removeBorder1");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(4)").addClass("removeBorder1");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(5)").addClass("removeBorder1");
                    $("#ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1 tbody tr:eq(" + len + ")").find("td:eq(5)").addClass("removeBorder1");

                }

            </script>
     </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>

