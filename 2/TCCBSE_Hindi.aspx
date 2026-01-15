<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="TCCBSE_Hindi.aspx.cs" Inherits="_2.TCCBSE_Hindi" %>

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
        table tbody tr {
            line-height: 19px !important;
        }
        .tc-tab-ft > tbody > tr > td {
            padding: 25px 10px 0px !important;
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
                            <asp:Label ID="lblaf" runat="server" Text="Affiliation No. is : " class="  no-padding txt-bold  "></asp:Label>
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
                    <tr class="hide">
                        <td colspan="2" class="tcb-font-style">Affiliation No. :&nbsp;<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2" class="tcb-font-style text-right">School No. :&nbsp;<asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>--%>
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
                        <td class="text-center" colspan="4"><span class="form-box-border2 tcb-font-style" style="border:none;">स्थानान्तरण प्रमाण पत्र/TRANSFER CERTIFICATE</span>
                            (<span class="form-box-border2 tcb-font-style" style="border:none;"><asp:Label ID="tcCopy" runat="server" style="border:none;"></asp:Label>&nbsp;Copy</span>)

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tcb-font-style">Book No. :&nbsp;<asp:Label ID="Label32" runat="server"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Sl. No. :&nbsp;<asp:Label ID="Label30" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2" class="tcb-font-style text-right">Admission No. :&nbsp;<asp:Label ID="Label31" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="4">
                            <table class="tc-tab">
                                <tr>
                                    <td style="width: 4%;">1.
                                    </td>
                                    <td style="width: 59%;" class="customtext">विद्यार्थी का नाम/Name of Pupil
                                    </td>
                                    <td></td>
                                    <td style="width: 40%;">
                                        <asp:Label ID="Label3" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            <tr>
                                <td>2.
                                </td>
                                <td>माता का नाम/Mother's Name
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label5" Font-Bold="true" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                                <tr>
                                    <td>3.
                                    </td>
                                    <td>पिता का नाम/Father's Name
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label4" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            
                                <tr>
                                    <td>4.
                                    </td>
                                    <td>राष्ट्रीयता/Nationality
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label6" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>5.
                                    </td>
                                    <td>क्या विद्यार्थी अनु० जाति/जन जाति या पिछड़ा वर्ग से सम्बन्धित है/Whether the Pupil belongs to Schedule Caste or Schedule tribe or Backward Caste
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label7" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>6.
                                    </td>
                                    <td>प्रथम प्रवेश का दिनांक व कक्षा/Date of admission in the School with class
                                    </td>
                                    <td></td>
                                    <td><asp:Label ID="Label8" Font-Bold="true" runat="server" Text=""></asp:Label>&nbsp;
                                                        (<asp:Label ID="Label9" Font-Bold="true" runat="server" Text=""></asp:Label>)
                                    </td>
                                </tr>
                                <tr>
                                    <td>7.
                                    </td>
                                    <td>प्रवेश पुस्तिका के अनुसार जन्म तिथि (अंको में)/Date of birth according to Admission Register (in figures)
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label10" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            <tr>
                                <td>8.
                                </td>
                                <td>प्रवेश पुस्तिका के अनुसार जन्म तिथि (शब्दों में)/Date of birth according to Admission Register (in words)
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label11" Font-Bold="true" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                                <tr>
                                    <td>9.
                                    </td>
                                    <td>पिछली कक्षा जिसमे विद्यार्थी अध्ययनरत था (अंको/शब्दों में)/Class in which the pupil last Studied (in figures/words)
                                    </td>
                                    <td></td>
                                    <td><asp:TextBox ID="Label12" Font-Bold="true" runat="server" Text="" Width="250" style="border:0 !important; outline: none !important; padding: 0 !important;" OnTextChanged="Label12_TextChanged" AutoPostBack="true"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>10.
                                    </td>
                                    <td>पिछले विद्यालय/बोर्ड परीक्षा व परिणाम/Name of the School/Board Examination last taken with result
                                    </td>
                                    <td></td>
                                    <td><asp:Label ID="Label13" Font-Bold="true" runat="server" Text=""></asp:Label>&nbsp;
                                                        <asp:Label ID="Label14" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>11.
                                    </td>
                                    <td>क्या विद्यार्थी एक ही कक्षा में एक या अधिक बार अनुत्तीर्ण हुआ है/Whether the student is failed once or more in the same class
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label15" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>12.
                                    </td>
                                    <td>पठित विषय/Subjects Studied</td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label16" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>13.
                                    </td>
                                    <td>क्या उच्च कक्षा में पदोन्नत का अधिकारी है/Whether qualified for promotion to the next higher class
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label17" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>यदि हाँ, तो किस कक्षा के लिये/If so, to which class
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label18" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>14.
                                    </td>
                                    <td>क्या विद्यार्थी ने विद्यालय की सभी देय राशि का भुगतान कर दिया है/Whether the pupil has paid all dues to the school
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label20" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>15.
                                    </td>
                                    <td>क्या कोई शुल्क रियायत प्रदान की गयी थी, यदि हाँ तो उसकी प्रकृति/Whether any fee concession was availed; if so, specify the nature of the concession 
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label21" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>16.
                                    </td>
                                    <td>विद्यालय दिवसों की कुल सं०/Total No. of working days upto date
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label22" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>17.
                                    </td>
                                    <td>उपस्थितियों की कुल सं०/Total No. of working days present
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label23" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>18.
                                    </td>
                                    <td>क्या विद्यार्थी एन सी सी कैडेट/स्काउट है? (विवरण दें)/Whether the pupil is NCC Cadet/Boy Scout/Girl Guide (Give details)
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label24" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>19.
                                    </td>
                                    <td>विद्यार्थी की खेल/पाठ्येतर गतिविधियाँ (उपलब्धि स्तर का विवरण दें)/Games played or extra curricular activities in which the pupil usually took part. 
                                                    
                                                        (mention achievement level therein)
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label25" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>20.
                                    </td>
                                    <td>सामान्य आचरण/General conduct
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label26" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>21.
                                    </td>
                                    <td>प्रमाण पत्र के लिए आवेदन की तिथि/Date of application for certificate
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label27" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            <tr>
                                <td>22.
                                </td>
                                <td> विद्यालय की नामावली से विद्यार्थी का नाम हटाये जाने की तिथि/Date on which Pupil's name was struck off the rolls of the school
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblpupilsname" Font-Bold="true" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                                <tr>
                                    <td>23.
                                    </td>
                                    <td>प्रमाण पत्र जारी करने की तिथि/Date of issue of certificate
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label28" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>24.
                                    </td>
                                    <td>विद्यालय छोड़ने का कारण/Reason for leaving the school
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label29" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>25.
                                    </td>
                                    <td>यूडीआईएसई पेन/UDISE PEN
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblPen" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>26.
                                    </td>
                                    <td>अपार आईडी/APAAR ID
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblApaarID" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>27.
                                    </td>
                                    <td>अन्य टिप्पणियाँ/Any other remarks
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label33" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height:35px;"><td colspan="4"></td></tr>
                    <tr>
                        <td colspan="4">
                            <table class="tc-tab-ft">
                                <tr>
                                    <td>कक्षाध्यापक के हस्ताक्षर<br />(Sign. of Class Teacher)</td>
                                    <td class="text-center">तैयारकर्ता<br />(Prepared By)</td>
                                    <td class="text-right">प्राचार्य के हस्ताक्षर<br />(Sign. of Principal)</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>




</asp:Content>

