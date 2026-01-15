<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="fee_card.aspx.cs" Inherits="_2.FeeCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <div id="divExport" runat="server">
                                            <div style="page-break-after: always;">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td id="divCustom" runat="server">

                                                            <table class="table">
                                                                <tr>
                                                                    <td colspan="4" class="p-pad-20 text-right text-bold">
                                                                        <div class="print-row col-lg-12 ">
                                                                            <div id="header" runat="server" style="width: 100%"></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td colspan="4" class="p-pad-20 p-h-titel-box">
                                                                        <div class="set-date-rt" style="display: none">
                                                                            <asp:Label ID="Label5" runat="server" Text="Date :" CssClass="sub-adds customtext" Font-Bold="true"></asp:Label>
                                                                            <asp:Label ID="lblDate" runat="server" Text="Label" CssClass="sub-adds customtext" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <table class="table no-bm mp-table p-table-bordered table-bordered v-align-t">

                                                                            <tr class="font-f-Courier">
                                                                                <td class="p-pad-25  tab-b-30">
                                                                                    <asp:Label ID="Label20" runat="server" CssClass="txt-rep-title-12-b customtext" Text="S.R. No. :"></asp:Label>
                                                                                    <asp:Label ID="lblsrno" runat="server" CssClass="txt-rep-title-12-b customtext" Font-Names="Courier New"
                                                                                        Text='SrNo'></asp:Label>
                                                                                </td>
                                                                                <td class="p-pad-25 tab-b-35">
                                                                                    <asp:Label ID="Label3" runat="server" CssClass="txt-rep-title-12-b customtext"
                                                                                        Text="Student's Name :"></asp:Label>
                                                                                    <asp:Label ID="lblStudentName" runat="server" Font-Names="Courier New" CssClass="txt-rep-title-12-b customtext"
                                                                                        Text='StudentName'></asp:Label>
                                                                                </td>
                                                                                <td class="p-pad-25 tab-b-35">
                                                                                    <asp:Label ID="Label4" runat="server" CssClass="txt-rep-title-12-b customtext" Text="Class :"></asp:Label>
                                                                                    &nbsp;<asp:Label ID="lblClass" runat="server" Font-Names="Courier New"
                                                                                        CssClass="txt-rep-title-12-b customtext" Text='ClassName'></asp:Label>
                                                                                    (<asp:Label ID="lblSection" runat="server" Font-Names="Courier New"
                                                                                        CssClass="txt-rep-title-12-b customtext" Text='SectionName'></asp:Label>)</td>


                                                                            </tr>
                                                                            <tr class="font-f-Courier">
                                                                                <td class="p-pad-25 ">
                                                                                    <asp:Label ID="Label28" runat="server" CssClass="txt-rep-title-12-b customtext" Text="DOB :"></asp:Label>
                                                                                    <asp:Label ID="lblDOB" runat="server" Font-Names="Courier New"
                                                                                        CssClass="txt-rep-title-12-b customtext" Text='DOB'></asp:Label>
                                                                                </td>

                                                                                <td class="p-pad-25 ">
                                                                                    <asp:Label ID="Label22" runat="server" CssClass="txt-rep-title-12-b customtext" Text="Father's Name :"></asp:Label>
                                                                                    <asp:Label ID="lblFatherName" runat="server" Font-Names="Courier New"
                                                                                        CssClass="txt-rep-title-12-b customtext" Text='FatherName'></asp:Label>
                                                                                </td>
                                                                                <td class="p-pad-25 ">
                                                                                    <asp:Label ID="Label30" runat="server" CssClass="txt-rep-title-12-b customtext" Text="Mother's Name :"></asp:Label>
                                                                                    <asp:Label ID="lblMotherName" runat="server" Font-Names="Courier New"
                                                                                        CssClass="txt-rep-title-12-b customtext" Text='MotherName'></asp:Label>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                            <td colspan="4" class="p-pad-20 p-h-titel-box">
                                                                                <table class="table no-bm mp-table p-table-bordered" style="height: 135px;">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <th class="p-pad-n text-left ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">Installment No.</h5>
                                                                                            </th>
                                                                                            <th class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">Month</h5>
                                                                                            </th>
                                                                                            <th class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">Particulars</h5>
                                                                                            </th>
                                                                                            <th class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">Reciept No.</h5>
                                                                                            </th>
                                                                                            <th class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">Amount Paid</h5>
                                                                                            </th>
                                                                                            <th class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">Balance(if any)</h5>
                                                                                            </th>
                                                                                            <th class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">Sign & Date</h5>
                                                                                            </th>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="p-pad-n text-left ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">I</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">April</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">Annual Maintenance + Medical Fee + April Fee</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>

                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="p-pad-n text-left ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">II</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">July</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">July</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="p-pad-n text-left ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">III</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">August</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">August + June Fee</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                        </tr>
                                                                                         <tr>
                                                                                            <td class="p-pad-n text-left ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">IV</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">September</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">September + Half Yearly Exam Fee</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="p-pad-n text-left ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">V</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">October</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">October + May Fee</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="p-pad-n text-left ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">VI</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">November</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">November + March Fee</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="p-pad-n text-left ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">VII</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">December</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">December</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="p-pad-n text-left ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">VIII</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">January</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">January + Anual Exam Fee</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="p-pad-n text-left ">
                                                                                                <h5 class="txt-rep-title-12-b customtext">IX</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">February</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-12 customtext">February</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                            <td class="p-pad-n text-center ">
                                                                                                <h5 class="txt-rep-title-11 customtext"> &nbsp;</h5>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                            
                                                                        </table>
                                                                    </td>
                                                                </tr>


                                                            </table>

                                                        </td>
                                                    </tr>
                                                </table>
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

