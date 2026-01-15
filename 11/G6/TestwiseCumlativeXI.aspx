<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true"
    CodeFile="TestwiseCumlativeXI.aspx.cs" Inherits="TestwiseCumlativeXI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <script>
                
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue ">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue ">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                      <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpBranch" runat="server" AutoPostBack="True" CssClass="form-control-blue " OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Evaluation&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="drpEval_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Value="TERM1">TERM 1</asp:ListItem>
                                                <asp:ListItem Value="TERM2">TERM 2</asp:ListItem>
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Test&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drptest" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drptest_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Test Month&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged" AutoPostBack="true">
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
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Select Order&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="ddlOrder" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="Alphabetical">Alphabetical</asp:ListItem>
                                                <asp:ListItem Value="Sequential">Sequential</asp:ListItem>
                                                <asp:ListItem Value="RollNoWise">Roll No. Wise</asp:ListItem>
                                                
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="msgbox" runat="server" style="left: 0;"></div>
                                
                                <div class="col-sm-12  " runat="server" id="divList" visible="false">
                                     <div style="float: right; font-size: 19px;">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkWord" runat="server" CssClass="icon-word-color" OnClick="lnkWord_Click" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkExcel" runat="server" CssClass="icon-excel-color" OnClick="lnkExcel_Click" Visible="false" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <a onclick="exceller();" class="icon-excel-color"><i class="fa fa-file-excel-o " title="Export to Excel" ></i></a>
                                                <asp:LinkButton ID="lnkPdf" Visible="false" runat="server" CssClass="icon-pdf-color" OnClick="lnkPdf_Click" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkPrint" runat="server" CssClass="icon-print-color" OnClick="lnkPrint_Click" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                  <asp:PostBackTrigger ControlID="lnkWord" />
                                                <asp:PostBackTrigger ControlID="lnkExcel" />
                                                <asp:PostBackTrigger ControlID="lnkPdf" />
                                                <asp:PostBackTrigger ControlID="lnkPrint" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                </div>
                                     <div class="col-sm-12  " id="divExport" runat="server">
                                        <div class=" table-responsive  table-responsive2 " id="abc" runat="server">
                                            <div id="header" runat="server" class="col-sm-12" style="width: 100%"></div>

                                                            <div class="col-sm-12 text-center"  style="font-size: 14px">
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="14px" Text="Marksheet"></asp:Label>
                                                                (<asp:Label ID="lblMonthss" runat="server" Text=""></asp:Label>-<asp:Label ID="lblTest" runat="server" Text=""></asp:Label>)
                                                            </div>
                                                            <div class="col-sm-12 text-center">

                                                                <span style="font-weight: bold; font-family: Verdana; font-size: 14px">Class:</span><asp:Label ID="lblClass" runat="server" Text="" style="font-size: 14px"></asp:Label>
                                                                (<asp:Label ID="lblSection" runat="server" Text="" style="font-size: 14px"></asp:Label>)
                                                                <span style="font-weight: bold; font-family: Verdana; font-size: 14px"> Eval.:</span><asp:Label ID="lblEval" runat="server" Text="" style="font-size: 14px"></asp:Label>
                                                            </div>
                                            <table class="table table-striped no-bm no-head-border table-bordered pro-table table-header-group">
                                               
                                                <tbody>
                                                    <tr>
                                                        <th class="text-center" style="vertical-align: top; width: 40px; text-transform:uppercase;">#</th>
                                                       <th class="text-center" style="vertical-align: top; width: 80px; text-transform:uppercase;">S.R. No.</th>
                                                       <th class="text-left" style="vertical-align: top; width: 180px; text-transform:uppercase;">Student's Name</th>
                                                        <asp:Repeater ID="rptSubject" runat="server">
                                                            <ItemTemplate>
                                                                 
                                                                <th class="text-center" style="vertical-align: top; width: 180px; text-transform:uppercase;">
                                                                    <asp:Label ID="lblpaperid" runat="server" Visible="false" Text='<%# Eval("PaperId")  %>'></asp:Label>
                                                                    <asp:Label ID="lblSubjectId" runat="server" Visible="false" Text='<%# Eval("SubjectId")  %>'></asp:Label>
                                                                    <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("SubjectName")  %>'></asp:Label>
                                                                        (<asp:Label ID="lblMM1" runat="server"></asp:Label>)
                                                                </th>

                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <td class="p-pad-3 p-tot-tit hide">Month</td>
                                                       
                                                    </tr>
                                                    <asp:Repeater ID="rptStudents" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.ItemIndex+1  %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Eval("srno")  %>'></asp:Label>
                                                                </td>
                                                                <td style="text-transform:uppercase;">
                                                                    <%# Eval("StudentName")  %>
                                                                </td>
                                                                <asp:Repeater ID="rptMarks" runat="server">
                                                                    <ItemTemplate>
                                                                        <td class="text-center">
                                                                            <%# Eval("Marks")  %>
                                                                            <asp:Label ID="lblM1" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>

                                                                <td class="text-center hide">
                                                                     <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                   <tr><th colspan="12" style="height: 30px;">Teacher's Sign</th></tr>
                                                    <tr><td style="height: 70px;"></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
                                                    <tr><td style="height: 70px;" colspan="12">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                       <th style="border-right:1px solid  #e0e0e0; height: 70px; padding-top: 50px; text-align: center; font-weight: bold;"><span>Class Teacher's Sign</span></th>
                                                        <th style="border-right:1px solid  #e0e0e0; height: 70px; padding-top: 50px; text-align: center; font-weight: bold;"><span>Principal's Sign</span></th>
                                                        <th style="height: 70px; padding-top: 50px; text-align: center; font-weight: bold;"><span>Academic Director's Sign</span></th>
                                                        </tr></table>
                                                            </td></tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>



                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script>
  function exceller() {
    var uri = 'data:application/vnd.ms-excel;base64,',
      template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
      base64 = function(s) {
        return window.btoa(unescape(encodeURIComponent(s)))
      },
      format = function(s, c) {
        return s.replace(/{(\w+)}/g, function(m, p) {
          return c[p];
        })
      }
    var toExcel = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_divExport").innerHTML;
    var ctx = {
      worksheet: name || '',
      table: toExcel
    };
    var link = document.createElement("a");
    link.download = "TestwiseCumulativeXI.xls";
    link.href = uri + base64(format(template, ctx))
    link.click();
  }
</script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

