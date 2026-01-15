<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="TCForCbseBoard_hindi.aspx.cs" Inherits="admin_TCForCbseBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <div class="vd_content-section clearfix" id="Panel1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-lg-6 no-padding">
                            <asp:Label ID="lblMedium" runat="server" Text="Your Affiliation No. is : " class="  no-padding txt-bold  "></asp:Label>
                            &nbsp;
                                                    <asp:Label ID="lblaffno" runat="server" Style="color: #CC0000; font-weight: 700" Text="Label"></asp:Label>
                        </div>
                        <div class="col-lg-6 no-padding text-right menu-action">
                            <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/admin/TCCollection_New.aspx" Style="color: #CC0000">Go Back TC Collection</asp:LinkButton>
                            &nbsp;
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn-print-box" title="T.C Print" data-placement="left" OnClick="LinkButton1_Click"><i class="icon-printer"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="margin: 0 auto; width: 900px; padding: 10px; font-size: 12px;" class="marg-bot-30" id="divexport" runat="server">
        <div class="col-sm-12 no-padding print-row " runat="server" id="abc">
            <div class="col-sm-12 fee-d-box-nhl">
                <table id="Table1" runat="server" width="100%" style="border: none; font-family: Courier New;">
                    <tr>
                        <td colspan="2" class="tcb-font-style"><span class="txt-hindi">सम्बद्धता सं०</span>/Affiliation No. :&nbsp;<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2" class="tcb-font-style text-right"><span class="txt-hindi">विद्यालय सं०</span>/School No. :&nbsp;<asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table style="width: 85%;">
                                <tr>
                                    <td runat="server" id="header1"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td class="text-center" colspan="4"><span class="form-box-border2 tcb-font-style"><span class="txt-hindi">स्थानान्तरण प्रमाण पत्र</span>/Transfer Certificate </span>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tcb-font-style"><span class="txt-hindi">पुस्तक सं०</span>/Book No. :&nbsp;<asp:Label ID="Label32" runat="server"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <span class="txt-hindi">क्रम सं०</span>/Sl. No. :&nbsp;<asp:Label ID="Label30" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2" class="tcb-font-style text-right"><span class="txt-hindi">छात्र पंजिका सं०</span>/Admission No. :&nbsp;<asp:Label ID="Label31" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table class="tc-tab-h">
                                <tr>
                                    <td style="width: 4%;">1.
                                    </td>
                                    <td style="width: 59%;"><span class="txt-hindi">विद्यार्थी का नाम</span>/Name of Pupil
                                    </td>
                                    <td></td>
                                    <td style="width: 40%;">
                                        <asp:Label ID="Label3" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>2.
                                    </td>
                                    <td><span class="txt-hindi">पिता/माता/अभिभावक का नाम</span>/Father's/Mother's/Guardian's Name
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label4" Font-Bold="true" runat="server" Text=""></asp:Label>&nbsp;/&nbsp;<asp:Label ID="Label5" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>3.
                                    </td>
                                    <td><span class="txt-hindi">राष्ट्रीयता</span>/Nationality
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label6" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>4.
                                    </td>
                                    <td><span class="txt-hindi">क्या विद्यार्थी अनु० जाति/जन जाति या पिछड़ा वर्ग से सम्बन्धित है</span>/Whether the candidate belongs to Schedule Caste or Schedule tribe or Backward Caste
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label7" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>5.
                                    </td>
                                    <td><span class="txt-hindi">प्रथम प्रवेश का दिनांक व कक्षा</span>/Date of first admission in the School with class (in figures)
                                    </td>
                                    <td></td>
                                    <td>(<asp:Label ID="Label8" Font-Bold="true" runat="server" Text=""></asp:Label>)&nbsp;
                                                        (<asp:Label ID="Label9" Font-Bold="true" runat="server" Text=""></asp:Label>)
                                    </td>
                                </tr>
                                <tr>
                                    <td>6.
                                    </td>
                                    <td><span class="txt-hindi">प्रवेश पुस्तिका के अनुसार जन्म तिथि (अंको/शब्दों में)</span>/Date of birth according to Admission Register (in figures/words)
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label10" Font-Bold="true" runat="server" Text=""></asp:Label>
                                        /
                                                        <asp:Label ID="Label11" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>7.
                                    </td>
                                    <td><span class="txt-hindi">पिछली कक्षा जिसमे विद्यार्थी अध्ययनरत था (अंको/शब्दों में)</span>/Class in which the pupil last Studied (in figures/words)
                                    </td>
                                    <td></td>
                                    <td>(<asp:Label ID="Label12" Font-Bold="true" runat="server" Text=""></asp:Label>)
                                        /
                                                        <asp:Label ID="Label19" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>8.
                                    </td>
                                    <td><span class="txt-hindi">पिछले विद्यालय/बोर्ड परीक्षा व परिणाम</span>/School/Board Annual examination last taken with result
                                    </td>
                                    <td></td>
                                    <td>(<asp:Label ID="Label13" Font-Bold="true" runat="server" Text=""></asp:Label>)&nbsp;
                                                        (<asp:Label ID="Label14" Font-Bold="true" runat="server" Text=""></asp:Label>)
                                    </td>
                                </tr>
                                <tr>
                                    <td>9.
                                    </td>
                                    <td><span class="txt-hindi">यदि अनुत्तीर्ण है तो एक ही कक्षा में एक/दो बार</span>/Whether failed, if so once/twice in the same class
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label15" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>10.
                                    </td>
                                    <td><span class="txt-hindi">पठित विषय</span>/Subject Studied</td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label16" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>11.
                                    </td>
                                    <td><span class="txt-hindi">क्या उच्च कक्षा में पदोन्नत का अधिकारी है</span>/Whether qualified for promotion to the next higher class
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label17" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td><span class="txt-hindi">यदि हाँ, तो किस कक्षा के लिये</span>/If so, to which class
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label18" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>12.
                                    </td>
                                    <td><span class="txt-hindi">क्या विद्यार्थी ने विद्यालय की सभी देय राशि का भुगतान कर दिया है</span>/Month upto which the (pupil has paid) school dues paid
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label20" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>13.
                                    </td>
                                    <td><span class="txt-hindi">क्या विद्यार्थी को कोई शुल्क रियायत प्रदान की गयी थी, यदि हाँ तो उसकी प्रकृति</span>/Any fee concession availed of: if so, the nature of such concession 
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label21" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>14.
                                    </td>
                                    <td><span class="txt-hindi">विद्यालय दिवसों की कुल सं०</span>/Total No. of working days
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label22" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>15.
                                    </td>
                                    <td><span class="txt-hindi">उपस्थितियों की कुल सं०</span>/Total No. of working days present
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label23" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>16.
                                    </td>
                                    <td><span class="txt-hindi">क्या विद्यार्थी एन सी सी कैडेट/स्काउट है? (विवरण दें)</span>/Whether NCC Cadet/Boy Scout/Girl Guide (details may be given)
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label24" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>17.
                                    </td>
                                    <td><span class="txt-hindi">विद्यार्थी की खेल/पाठ्येतर गतिविधियाँ (उपलब्धि स्तर का विवरण दें)</span>/Games played or extra curricular activities in which the pupil usually took part. 
                                                    
                                                        (mention achievement level therein)
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label25" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>18.
                                    </td>
                                    <td><span class="txt-hindi">सामान्य आचरण</span>/General conduct
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label26" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>19.
                                    </td>
                                    <td><span class="txt-hindi">प्रमाण पत्र के लिए आवेदन की तिथि</span>/Date of application for certificate
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label27" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>20.
                                    </td>
                                    <td><span class="txt-hindi">प्रमाण पत्र  जारी करने की तिथि</span>/Date of issue of certificate
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label28" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>21.
                                    </td>
                                    <td><span class="txt-hindi">विद्यालय छोड़ने का कारण</span>/Reason for leaving the school
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label29" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>22.
                                    </td>
                                    <td><span class="txt-hindi">अन्य टिप्पणियाँ</span>/Any other remarks
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label33" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table class="tc-tab-ft">
                                <tr>
                                    <td><span class="txt-hindi">कक्षाध्यापक के हस्ताक्षर</span>
                                        <br />
                                        Sign. of Class Teacher</td>
                                    <td class="text-center"><span class="txt-hindi">तैयारकर्ता</span>
                                        <br />
                                        Prepared By</td>
                                    <td class="text-right"><span class="txt-hindi"></span>प्राचार्य के हस्ताक्षर
                                        <br />
                                        Signature Principal</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>




</asp:Content>

