<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="MonthwisePunchReport.aspx.cs" Inherits="MonthwisePunchReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <style>
        #ContentPlaceHolder1_ContentPlaceHolderMainBox_GridConsolidated tbody tr th:nth-child(1n+1) {
            text-align:left !important;
        }
        
    </style>
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" id="table1" runat="server">
                                    <div class="col-sm-3">
                                        <label class="control-label">Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:RadioButtonList ID="rdoType" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem Value="Descriptive" Selected="True">Descriptive</asp:ListItem>
                                                <asp:ListItem Value="Consolidated">Consolidated</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Designation</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpDesignation" CssClass="form-control-blue" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpDesignation_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="control-label">Year&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlYear" runat="server">
                                                
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <label class="control-label">Month&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlMonth" runat="server">
                                                <asp:ListItem Value="Jan">Jan</asp:ListItem>
                                                <asp:ListItem Value="Feb">Feb</asp:ListItem>
                                                <asp:ListItem Value="Mar">Mar</asp:ListItem>
                                                <asp:ListItem Value="Apr">Apr</asp:ListItem>
                                                <asp:ListItem Value="May">May</asp:ListItem>
                                                <asp:ListItem Value="Jun">Jun</asp:ListItem>
                                                <asp:ListItem Value="Jul">Jul</asp:ListItem>
                                                <asp:ListItem Value="Aug">Aug</asp:ListItem>
                                                <asp:ListItem Value="Sep">Sep</asp:ListItem>
                                                <asp:ListItem Value="Oct">Oct</asp:ListItem>
                                                <asp:ListItem Value="Nov">Nov</asp:ListItem>
                                                <asp:ListItem Value="Dec">Dec</asp:ListItem>

                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 mgbt-xs-15">
                                        <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" OnClick="lnkSubmit_OnClick">View</asp:LinkButton>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <div class="col-sm-12  mgbt-xs-5 no-padding" runat="server" id="divExport1">

                                    <div style="float: right; font-size: 19px;">

                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <span id="btnPrint" runat="server" class="btn-print-box hide" onclick="printDiv();" ><a><i class="icon-printer"></i>Print</a></span>
                                                    <asp:LinkButton ID="lnkword" runat="server" OnClick="lnkword_OnClick" CssClass="icon-word-color"
                                                        title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkexcel" runat="server" OnClick="lnkexcel_OnClick" CssClass="icon-excel-color"
                                                        title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkpdf" runat="server" OnClick="lnkpdf_OnClick" CssClass="icon-pdf-color"
                                                        title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkprint" runat="server" OnClick="lnkprint_OnClick" CssClass="icon-print-color"
                                                        title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                                    
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkword" />
                                                    <asp:PostBackTrigger ControlID="lnkexcel" />
                                                    <asp:PostBackTrigger ControlID="lnkpdf" />
                                                    <asp:PostBackTrigger ControlID="lnkprint" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </asp:Panel>

                                    </div>
                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <div id="divExport" runat="server">
                                            <div id="header" runat="server" style="width: 100%"></div>
                                            <h4 class="col-sm-12  text-center" style="font-weight: bold">
                                                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h4>
                                            <asp:GridView ID="grdMonthlyPunch" runat="server" CssClass="table pro-table p-table p-table-bordered no-bm table-hover table-striped table-bordered"
                                                OnRowDataBound="grdMonthlyPunch_OnRowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="GridConsolidated" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered" AutoGenerateColumns="false">
                                                <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Emp. Id">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="EmpId" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Designation" runat="server" Text='<%# Bind("DesNameNew") %>'></asp:Label>
                                                                </ItemTemplate>
                                                               <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Total In Month">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="TotalDaysOfMonth" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Present">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Present" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Half Day">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="HalfDay" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Short Leave">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ShortLeave" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Late">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Late" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Absent">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Absent" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sandwitch">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Sandwitch" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Holiday">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Holiday" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="AchievDays" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="LWP">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Leaves" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Paid Days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PaidDays" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:GridView>
                                            <p style="font-family: Courier New; font-size: 12px; /*background-color: #393a3e; */ /*color: white; */" class="col-sm-12  text-right">
                                                Generated by eAM&reg; &nbsp;
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
              <script src="../js/jquery-1.4.3.min.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script>

        function printDiv() {
            var monthName = $('select[Id*="ddlMonth"]').find(":selected").text();
            var yearName = $('select[Id*="ddlYear"]').find(":selected").text();

            var tds = $('table[Id*="grdMonthlyPunch"] tbody').children('tr').children('th').length;
            //alert(tds);
            var tfHdr = [], tfBody = [];
            $('table[Id*="grdMonthlyPunch"] tbody tr th').each(function () {
                tfHdr.push($(this).html());
            });

            $('table[Id*="grdMonthlyPunch"] tbody tr td').each(function () {
                tfBody.push($(this).html());
            });

            //alert(tfHdr); alert(tfBody);

            /*===================================================*/

            var tutionDiv = '<b>Attendance</b><hr><table class="table table-bordered"><thead><tr>';
            for (var i = 0; i < tfHdr.length; i++) {
                tutionDiv += '<th>' + tfHdr[i] + '</th>';
            }
            tutionDiv += '</tr></thead><tbody>';
            var j = 0;
            for (var i = 0; i < tfBody.length; i = i + tds) {
                tutionDiv += '<tr>';
                for (var j = 0; j < tds; j++) {
                    tutionDiv += '<td>' + tfBody[i + j].replace('-', '').replace('AM', '').replace('PM', '') + '</td>';
                }
                tutionDiv += '</tr>';
            }
            tutionDiv += '</tbody></table>';
            //alert(tutionDiv);
            /*===================================================*/
            var newWin = window.open('', 'Print-Window');
            newWin.document.open();
            newWin.document.write('<html><head><title>Attendance ' + monthName + '-' + yearName + ' </title><link rel="stylesheet" href="../css/bootstrap.min.css">' +
                '<style>hr { margin-top: 2px; margin-bottom: 2px;}.table{font-size: 10px;} .table>tbody>tr>td, .table>tbody>tr>th, .table>tfoot>tr>td, .table>tfoot>tr>th, .table>thead>tr>td, .table>thead>tr>th {'
                + 'padding: 2px;}  @media print {.col-lg-6 { width: 50%;float:left;font-size:12px;} } </style> </head><body onload="window.print()"><div class="ontainer"><div class="row">'
                //+ '<div class="col-lg-12"><b>Student Details </b><hr>' + studentDiv + '</div>'
                + '<div class="col-lg-12">' + tutionDiv + '</div>'
                //+ '<div class="col-lg-6">' + (transportDiv.length < 60 ? '' : transportDiv) + '<div>'
                + '<div><div></body></html>');
            newWin.document.close();
            setTimeout(function () { newWin.close(); }, 5);
        }

    </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

