<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TCICSE.aspx.cs" Inherits="TCICSE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script>
        try {
            function setFont(fontsize) { var customtext = document.querySelectorAll(".customtext"); for (var i = 0; i < customtext.length; i++) { customtext[i].style.fontSize = fontsize; } }
        }
        catch (err) {
            alert(err.message);
        }
    </script>
    <style>
        .inderlineTD {
            border-bottom: 1px solid;
            width: 100% !important;
        }
        .innertable tbody tr td {
            padding-top:15px !important;
        }
        .innertable tbody tr td span {
           text-align:center !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div class="vd_content-section clearfix" id="Panel1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-6 no-padding">
                            <asp:Label ID="lblaf" runat="server" Text="Your Affiliation No. is : " class="  no-padding txt-bold  "></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblaffno" runat="server" Style="color: #CC0000; font-weight: 700" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-6 no-padding text-right menu-action">
                            <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click" CssClass="btn-print-box"
                                title="Print T.C." data-placement="left"><i class="icon-printer"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30" id="divexport" runat="server">
        <div class="col-sm-12 no-padding print-row " runat="server" id="abc" style="padding-bottom:30px !important;">
            <div class="col-sm-12 fee-d-box-nhl">
                <table id="Table1" runat="server" width="100%" style="border: none; font-family: Courier New;">

                    <tr>
                        <td colspan="4">
                            <table style="width: 100%;">
                                <tr>
                                    <td runat="server" id="header1"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td class="text-center" colspan="4"><span class="form-box-border2 tcb-font-style" style="border: none;">TRANSFER CERTIFICATE</span>
                                                        (<span class="form-box-border2 tcb-font-style" style="border: none;"><asp:Label ID="tcCopy" runat="server" Style="border: none;"></asp:Label>&nbsp;Copy</span>)

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tcb-font-style">Sr. No. :&nbsp;<asp:Label ID="Label30" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2" class="tcb-font-style text-right">Admission No. :&nbsp;<asp:Label ID="Label31" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table class="tc-tab innertable">
                                <tr>
                                    <td class="customtext">UDISE PEN </td>
                                    <td colspan="2" class="inderlineTD">
                                        <asp:Label ID="lblUdisePen" Font-Bold="true" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="customtext">APAAR ID </td>
                                    <td colspan="2" class="inderlineTD">
                                        <asp:Label ID="lblApaarID" Font-Bold="true" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="customtext">This is to certify that </td>
                                    <td colspan="2" class="inderlineTD">
                                        <asp:Label ID="Label3" Font-Bold="true" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>

                                    <td class="customtext">Son of </td>
                                    <td class="inderlineTD">
                                        <asp:Label ID="lblMotherName" Font-Bold="true" runat="server" ></asp:Label> &
                                        <asp:Label ID="Label5" Font-Bold="true" runat="server" ></asp:Label>
                                    </td>
                                    <td>was admitted</td>

                                </tr>
                                <tr>
                                    <td class="customtext">into this school on </td>
                                    <td class="inderlineTD">
                                        <asp:Label ID="Label4" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td>on a transfer from
                                        <asp:Label ID="lblLastSchool" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" id="tr">
                                    <td colspan="3" class="customtext inderlineTD text-center">
                                        <asp:Label ID="Label6" Font-Bold="true" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="customtext">at left on
                                        <asp:Label ID="Label1" Font-Bold="true" runat="server" class="inderlineTD"></asp:Label></td>
                                    <td>with a
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" Font-Bold="true" runat="server" class="inderlineTD"></asp:Label>
                                        character.</td>

                                </tr>
                                <tr>
                                    <td class="customtext">He was then studying in the 
                                   
                                        <asp:Label ID="Label2" Font-Bold="true" runat="server" class="inderlineTD"></asp:Label></td>
                                    <td>class of the
                                    </td>
                                    <td>
                                        <asp:Label ID="Label13" Font-Bold="true" runat="server"  class="inderlineTD"></asp:Label>
                                        stream,</td>

                                </tr>
                                <tr>
                                    <td class="customtext" style="white-space: nowrap;">the school year being from
                                   
                                        <asp:Label ID="Label8" Font-Bold="true" runat="server"  class="inderlineTD"></asp:Label></td>
                                    <td>to
                                    </td>
                                    <td>
                                        <asp:Label ID="Label9" Font-Bold="true" runat="server" class="inderlineTD"></asp:Label>.
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="customtext">All sums due to this institution on his account has been remitted or satisfactorily arranged for.
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="2" class="customtext">His date of birth, according to the Admission Register is (in figures)
                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" Font-Bold="true" runat="server"  class="inderlineTD"></asp:Label>.
                                    </td>
                                </tr>
                                <tr>
                                    <td class="customtext">(in words)
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="Label11" Font-Bold="true" runat="server"  class="inderlineTD"></asp:Label>.
                                    </td>
                                </tr>
                                <tr>
                                    <td class="customtext">Promotion has been
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="Label12" Font-Bold="true" runat="server"  class="inderlineTD"></asp:Label>.
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="customtext">Date_____________________________</td>
                                    <td></td>
                                    <td class="customtext">Signature________________________________<br />
                                        (Head of the School)</td>

                                </tr>
                                <tr>
                                    <td colspan="3"></td>
                                </tr>
                                <tr>

                                </tr>

                            </table>
                        </td>
                    </tr>

                </table>
            </div>
        </div>
    </div>



</asp:Content>

