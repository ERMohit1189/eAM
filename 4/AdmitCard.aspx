<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AdmitCard.aspx.cs" Inherits="AdmitCard" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Course&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpCourse" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpCourse_SelectedIndexChanged" CssClass="form-control-blue ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue ">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpsection" runat="server" CssClass="form-control-blue " AutoPostBack="true" OnSelectedIndexChanged="drpsection_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Examination&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpExamination" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpExamination_SelectedIndexChanged">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem>Periodic Test 1</asp:ListItem>
                                                <asp:ListItem>Periodic Test 2</asp:ListItem>
                                                <asp:ListItem>Periodic Test 3</asp:ListItem>
                                                <asp:ListItem>Semester-I Examination</asp:ListItem>
                                                <asp:ListItem>Periodic Test 4</asp:ListItem>
                                                <asp:ListItem>Periodic Test 5</asp:ListItem>
                                                <asp:ListItem>Periodic Test 6</asp:ListItem>
                                                <asp:ListItem>Pre-Board-I</asp:ListItem>
                                                <asp:ListItem>Pre-Board-II</asp:ListItem>
                                                <asp:ListItem>Pre-Board-III</asp:ListItem>
                                                <asp:ListItem>Annual Examination</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 mgbt-xs-10" runat="server" id="divshow" visible="False">
                                        <div style="float: right; font-size: 19px;">
                                           <%-- <asp:LinkButton ID="lnkWord" runat="server" CssClass="icon-word-color" title="Export to Word" data-toggle="tooltip"
                                                data-placement="Bottom" OnClick="lnkWord_Click"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkExcel" runat="server" CssClass="icon-excel-color" title="Export to Excel" data-toggle="tooltip"
                                                data-placement="Bottom" OnClick="lnkExcel_Click"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click" CssClass="icon-print-color" title="Print"
                                                ><i class="fa fa-print "></i></asp:LinkButton>--%>
                                            <span onclick="PrintDiv()" class="btn btn-sm btn-default" title="Print" style="cursor:pointer;"><i class="fa fa-print "></i>&nbsp;Print</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding">
                                    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30">
                                        <div id="divExport"  class="col-sm-12 no-padding print-row marg-bot-30">
                                            <div class=" table-responsive  table-responsive2">
                                                <asp:Repeater ID="rpStudentDetails" runat="server" OnItemCreated="rpStudentDetails_ItemCreated">
                                                    <ItemTemplate>
                                                        <div runat="server" style="padding: 9px; width:50%; float:left;">
                                                        <div class="col-sm-12 print-row fee-d-box-nhl" runat="server"  style="padding: 3px;">
                                                            <div class="col-sm-12 print-row"  style="padding: 0px;">
                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 100%">
                                                                            <div id="">
                                                                                <div class="text-left col-lg-2 col-md-2 col-xs-2 col-sm-2 no-padding" style="padding-left: 10px !important">
                                                                                    <div id="" class="mgbt-xs-5 p-mgbt-xs-5">
                                                                                        <asp:Image runat="server" ID="imgLogo" Style="width: 50px;" />
                                                                                    </div>
                                                                                </div>
                                                                                <div id="" class="text-center col-lg-10 col-md-10 col-xs-10 col-sm-10 no-padding ">
                                                                                    <div class="main-titel-box">
                                                                                        <h1 id="" class="main-name">
                                                                                            <asp:Label runat="server" ID="collegeName" Style="font-weight: bold; text-transform:uppercase; font-size: 16px !important;"></asp:Label>
                                                                                        </h1>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <h3 class="main-name-l text-center" style="margin: 0 15px 0 0; font-weight: 600; line-height: 18px; text-transform: uppercase;font-size: 13px !important;">
                                                                                <asp:Label ID="Label4" runat="server" Text="Admit Card "></asp:Label></h3>
                                                                            <h3 style="font-size: 13px; font-weight: 600; margin: 0px 15px 5px 0; text-transform: uppercase;" class="sub-adds-l text-center">
                                                                                <asp:Label runat="server" ID="lblEvaluation"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblSession"></asp:Label>
                                                                            </h3>
                                                                        </td>
                                                                    </tr>

                                                                </table>
                                                            </div>

                                                            <div class="col-sm-12">
                                                                <hr style="margin: 0px 0; padding: 0;" />
                                                            </div>

                                                            <div class="col-sm-12"  style="padding: 0px;">
                                                                <div class="col-sm-9">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td class="text-left" style="width: 33%;">
                                                                                <asp:Label ID="Label5" runat="server" Text="S.R. No. " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                            </td>
                                                                            <td class="text-left">: &nbsp;
                                                     <asp:Label ID="lblRecieptNo" runat="server" Font-Bold="True" Text='<%# Bind("srno") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td class="text-left">
                                                                                <asp:Label ID="Label9" runat="server" Text="Student's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                            </td>
                                                                            <td class="text-left">: &nbsp;
                                                    <asp:Label ID="lblStudentName" runat="server" Font-Bold="True" Text='<%# Bind("Name") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td class="text-left">
                                                                                <asp:Label ID="Label11" runat="server" Text="Father's Name " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                            </td>
                                                                            <td class="text-left">: &nbsp;
                                                   <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" Text='<%# Bind("FatherName") %>' Font-Names="Courier New" Font-Size="12px"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                       
                                                                    </table>
                                                                </div>
                                                                <div class="col-sm-3" style="vertical-align: top; text-align:right !important;">
                                                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("PhotoPath")+"?Date="+DateTime.Now.ToString(CultureInfo.InvariantCulture) %>' style="height: 65px !important;"  />
                                                                </div>
                                                                <div class="col-sm-12" style="margin-bottom:7px;">
                                                                    <table style="width:100%;">
                                                                        <tr>
                                                                    <td class="text-left"  style="width: 24%;">
                                                                                <asp:Label ID="Label10" runat="server" Text="Class " Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                            </td>
                                                                            <td class="text-left">: &nbsp;
                                                    <asp:Label ID="lblClass" runat="server" Font-Names="Courier New" Font-Bold="True" Text='<%# Bind("CombineClassName") %>' Font-Size="12px"></asp:Label>
                                                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                <asp:Label ID="Label14" runat="server" Text="Roll No." Font-Bold="False" Font-Size="12px"></asp:Label>
                                                                            : &nbsp;
                                               <asp:Label ID="lblMode" runat="server" Font-Names="Courier New" Font-Bold="True" Text='<%# Bind("RollNo") %>' Font-Size="12px"></asp:Label>
                                                                            </td>
                                                                            </tr>
                                                                        </table>
                                                                        </div>
                                                                <table style="width:100%;">
                                                                    <tr>
                                                                        <td class="text-left" style="width: 50%;">
                                                                            <div style="padding-left:15px;">
                                                                                &nbsp;
                                                                            <asp:Image ID="AccountantImg" runat="server"  style="height: 27px !important;" Visible="false"  />
                                                                                </div>
                                                                            <div style="padding-left:15px;">Accountant</div>
                                                                        </td>
                                                                        <td class="text-right" style="padding-right:20px;">
                                                                            <div>
                                                                            <asp:Image ID="Principalimg" runat="server"  style="height: 27px !important;"  />
                                                                                </div>
                                                                            <div>Principal</div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>


                                                        </div>
                                                        </div>
                                                        <div id="pagebreak" runat="server"></div>
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


        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function PrintDiv() {
            
            var headContent = document.getElementsByTagName('head')[0].innerHTML;

            var divContents = document.getElementById("divExport").innerHTML;
            var printWindow = window.open('', '', 'height=700,width=1000, class="tbls"');
            printWindow.document.write('<html><head><title>Examination Admit Card</title>' + headContent + '</head>');
            var TermNmae = $("[id*=drpEval]").val();
            if (TermNmae == 'Term1') {
                printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');
            }
            else {
                printWindow.document.write('<body id="tbls">' + divContents + '</body></html>');
            }

            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 1500);
            return false;
            printWindow.close();

        }
    </script>
</asp:Content>

