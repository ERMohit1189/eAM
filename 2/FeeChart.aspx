<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="FeeChart.aspx.cs" Inherits="admin_FeeChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
 

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                                <div class="col-lg-12 no-padding">

                                    <div class="col-sm-3">
                                        <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                             <asp:DropDownList ID="drpFeeGroup" runat="server" CssClass="form-control-blue validatedrp">
                                                </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>   

                                    <div class="col-sm-3">
                                        <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpMedium" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>   
                                    <div class="col-sm-3">
                                        <label class="control-label">Type of Admission</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpAdmissionType" runat="server" CssClass="form-control-blue">
                                                    <asp:ListItem Text="<--All-->"></asp:ListItem>
                                                    <asp:ListItem>New</asp:ListItem>
                                                    <asp:ListItem>Old</asp:ListItem>
                                                </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    

                                   <div class="col-sm-3" style="padding-top:23px;">

                                        <asp:LinkButton ID="lnkShow" OnClientClick="return ValidateDropdown('.validatedrp');"
                                            runat="server" class="btn vd_bg-blue vd_white" OnClick="lnkShow_Click">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 70px;"></div>

                                    </div>

                                </div>

                                <div class="col-sm-12  mgbt-xs-5 " id="divEx" runat="server" visible="false">
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


                                <div class="col-lg-12 ">
                                    <div class=" table-responsive  table-responsive2 " runat="server" id="divExport" visible="false">
                                        <div class="table no-bm no-p-b-table table-nb">
                                            <div class="col-lg-12 no-padding">
                                                <div  class="col-lg-12 no-padding p-pad-2 ">
                                                    <div id="header" runat="server"></div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 no-padding">
                                                <div  class="col-lg-12 no-padding p-pad-1 text-left p-h-titel-box">

                                                    <asp:Repeater ID="rptClasswithBranch" runat="server">
                                                        <HeaderTemplate>
                                                            <div class="col-lg-12 no-padding table no-bm no-p-b-table table-nb">
                                                               
                                                                    <div class="col-lg-12 no-padding txt-rep-title-13 text-center p-pad-2">

                                                                        <asp:Label ID="lblheader" runat="server" Text=""></asp:Label><br />
                                                                        Medium: <asp:Label ID="lblmedium" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;
                                                                        Type of Admission: <asp:Label ID="lblAdmitionType" runat="server" Text=""></asp:Label>
                                                    
                                                                
                                                                    </div>
                                                                
                                                            </div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <div class="col-lg-12 no-padding table no-bm no-p-b-table table-nb">

                                                               
                                                                    <div class="col-lg-12 no-padding p-pad-0 p-tot-tit" style="border: 1px solid #ccc; ">
                                                                        <div class="txt-rep-title-11-b text-left" style="background-color: #ccc !important;">
                                                                            Class : <asp:Label ID="lblClasswithBranch" runat="server" style="font-size: 16px;" Text='<%# Eval("class") %>'></asp:Label>
                                                                        </div>
                                                                        <asp:Label ID="lblclassname" runat="server" Text='<%# Eval("classname") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblclassid" runat="server" Text='<%# Eval("classid") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblbranchid" runat="server" Text='<%# Eval("branchid") %>' Visible="false"></asp:Label><br />
                                                                        <asp:Repeater ID="rptInsttalment" runat="server">
                                                                            <ItemTemplate>
                                                                                <div class="col-lg-12 no-padding table mp-table lbn-rbn-table no-tb table-nb no-bm  p-table-bordered table-bordered">
                                                                                  
                                                                                        <div class="col-lg-12 no-padding p-pad-0" style="padding-left:5px !important; padding-right:5px !important;">
                                                                                            <div class="txt-rep-title-11-b text-left p-table-bordered   table-bordered" style="background: #edeef2;">
                                                                                                <asp:Label ID="Monthid" runat="server" Visible="false" Text='<%# Eval("Monthid") %>'></asp:Label>
                                                                                                Installment : <asp:Label ID="lblMonthName" runat="server" Text='<%# Eval("MonthName") %>'></asp:Label>
                                                                                            </div>
                                                                                            <table class="table col-lg-12 no-padding table mp-table no-bm p-table-bordered   table-bordered">
                                                                                                <tr style="background: #edeef2;">
                                                                                                    <th style="font-weight:bold !important;text-align:left">Fee Head</th>
                                                                                                    <th style="font-weight:bold !important;text-align:left">Type of Admission</th>
                                                                                                    <th style="font-weight:bold !important;text-align:right">Amount</th>
                                                                                                </tr>
                                                                                            <asp:Repeater ID="rptFeeHeadWithAmount" runat="server">
                                                                                                <ItemTemplate>
                                                                                                    <tr>
                                                                                                        <td style="width:40%; text-align:left">
                                                                                                            <asp:Label ID="lblFeehead" runat="server" Text='<%# Eval("FeeType") %>'></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="width:30%;">
                                                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("AdmissionType") %>' Style="float: left"></asp:Label>
                                                                                                        </td>
                                                                                                         <td style="width:30%;">
                                                                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("FeePayment") %>' Style="float: right"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                                <FooterTemplate>
                                                                                                     <tr>
                                                                                                     <td colspan="3" style="text-align:right;">
                                                                                                        <asp:Label ID="lblTotalFee" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                         </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" style="border:0px !important; outline:none;" ><br /></td>
                                                                                                    </tr>
                                                                                                </FooterTemplate>
                                                                                            </asp:Repeater>
                                                                                                </table>
                                                                                        </div>
                                                                                   
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <div style="font-family: 'Arial Rounded MT'" class="txt-rep-title-13 p-pad-5 text-right">
                                                                                    <asp:Label ID="lblGrandTotalFee" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                        
                                                                    </div>
                                                                <div width="100%" class="page-cut-style">
                                                                                    &nbsp;
                                                                                </div>
                                                            </div>
                                                        </ItemTemplate>

                                                    </asp:Repeater>

                                                </div>
                                            </div>
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

