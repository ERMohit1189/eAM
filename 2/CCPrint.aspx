<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="CCPrint.aspx.cs" Inherits="_2.AdminCCPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <%--  <div style="padding-right: 15px; text-align: right; padding-bottom: 4px;">
        <asp:Panel ID="Panel1" runat="server">
            <asp:ImageButton ID="imgPrint" runat="server" ImageUrl="~/images/print_icon.gif" OnClick="imgPrint_Click" title="Print"
                Style="height: 16px;" />
        </asp:Panel>
    </div>--%>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-lg-6 no-padding">
                                    <asp:Label ID="lblMedium" runat="server" Text="Reference No. is : " class="  no-padding txt-bold  "></asp:Label>
                                    &nbsp;
                            <asp:Label ID="Label1" runat="server" Style="color: #CC0000; font-weight: 700" Text="Label"></asp:Label>
                                </div>
                                <div class="col-lg-6 no-padding text-right menu-action">
                                    <%--  <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/admin/CCCollection.aspx" Style="color: #CC0000">Go back to C.C. Fee Deposit</asp:LinkButton>--%>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn-print-box" PostBackUrl="~/2/CCCollection.aspx" Style="color: #CC0000"
                                        title="Go back to C.C. Fee Deposit" data-placement="left"><i class="fa fa-reply"></i></asp:LinkButton>
                                    &nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn-print-box" title="With Header Print" data-placement="left"><i class="icon-printer"></i></asp:LinkButton>

                                    &nbsp; &nbsp;
                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CssClass="btn-print-box" title="Without Header Print" data-placement="left"><i class="icon-printer"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30">
        <div class="col-sm-12 no-padding print-row marg-bot-30">

            <div class="col-sm-12 print-row fee-d-box-nhl-a4" runat="server" id="divExport">

                <div class="box-border">
                    <div class="col-sm-12 no-padding print-row">
                        <table  cellpadding="0" cellspacing="0" style="margin-right: 10px; width: 100%;">
                            <tr>
                                <td id="header1" runat="server">
                                    <div id="header" runat="server"></div>
                                </td>

                            </tr>

                        </table>
                    </div>


                    <div class="col-sm-12 no-padding print-row " id="divExport2" runat="server">
                        <div class="col-sm-12 no-padding print-row" style="margin-top: 25px">
                            <table width="100%">
                                <tr>
                                    <td style="width: 80%; padding-left: 45px;">
                                        <h3 class="sub-adds-l">Reference No. :
                                        <asp:Label ID="lblRefno" runat="server" Text=""></asp:Label></h3>
                                    </td>
                                    <td style="width: 20%; padding-right: 10px">
                                        <h3 class="sub-adds-l text-center">Date :
                                        <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></h3>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="col-sm-12" style="margin-top: 50px">

                            <h3 class="text-center" style="padding: 2px 0px; margin: 5px 0px 40px 0px;"><span style="border-bottom: 1px solid #292929;font-weight:bold">Character Certificate</span> </h3>
                            <p style="font-size: 18px; margin-left: 30px; margin-right: 10px; text-align: justify; text-justify: inter-word;">
                                It is certified that 
                            <asp:Label ID="lblstudentname" runat="server" Text="" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblRollNotxt" runat="server" Text="Roll No.-" Font-Bold="true"></asp:Label>                                
                                 <asp:Label ID="lbl_rollnumber" runat="server" Text="" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblSonordauter" runat="server" Text=""></asp:Label>
                                of 
                            <asp:Label ID="lblfathername" runat="server" Text="" Font-Bold="true"></asp:Label>
                                has
                            <asp:Label ID="lblstatus" runat="server" Text="" Font-Bold="true"></asp:Label>
                                class
                            <asp:Label ID="lblclassname" runat="server" Text="" Font-Bold="true"></asp:Label>
                                in the session
                            <asp:Label ID="lblyear" runat="server" Text="" Font-Bold="true"></asp:Label>. 
                                As per our records 
                                  <asp:Label ID="lblrecordsdob" runat="server" Text="" ></asp:Label>
                                Date of Birth is 
                                 <asp:Label ID="lbldob" runat="server" Text="" Font-Bold="true"></asp:Label>.
                            <asp:Label ID="lblheshe1" runat="server" Text="Label"></asp:Label>
                                bears a good moral character.
                            </p>
                            <p style="font-size: 18px; margin-bottom: 40px; margin-left: 30px; margin-right: 10px;">
                                <asp:Label ID="lblheshe" runat="server" Text="Label"></asp:Label>
                                has earned our good wishes for success in 
                            <asp:Label ID="lblheher" runat="server" Text=""></asp:Label>
                                life.
                            </p>
                        </div>
                        <div></div>
                        <div class="col-sm-12 ">
                            <table style="width: 100%;">
                                <tr>
                                    <th class="text-left" style="width: 40%; font-size: 14px;">&nbsp; &nbsp;
                                    </th>

                                    <th class="text-right" style="width: 60%; font-size: 14px; padding-right: 10px;">

                                        <strong>
                                            <asp:Label ID="Label19" runat="server" Text="Sign. of Principal"></asp:Label>
                                            </strong>
                                    </th>
                                </tr>
                            </table>
                        </div>


                    </div>
                </div>
        </div>
        </div>
    </div>




</asp:Content>

