<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TC_Print.aspx.cs" Inherits="admin_TC_Print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
<div>

          <div align="right"><asp:LinkButton ID="LinkButton6" runat="server" onclick="LinkButton6_Click" CssClass="print" 
                  Height="16px" ToolTip="Print" Width="16px"></asp:LinkButton></div>
                  <br />
    <%--<asp:DataList ID="DataList1" runat="server">
    <ItemTemplate>--%>
        <table class="table" id="abc" runat="server" width="100%">
        <tr>
                                    <td colspan="8">
                                        <div id="header" runat="server" style="width:80%"></div>
                                    </td>
                                </tr>
        <tr>
                                    <td colspan="8">
                                        <hr />
                                    </td>
                                </tr>
        <tr>
            <td style="text-align: center; font-weight: 700">
                &nbsp;</td>
            <td colspan="4" style="text-align: center; font-weight: 700">
                स्कालर रजिस्टर तथा टी. सी. फार्म S.Register No. ....................</td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;</td>
            <td style="text-align: center" colspan="3">
                Scholar&#39;s Register &amp; Transfer Certificate</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;</td>
            <td style="text-align: center">
               Adm. No/प्रवेश फाइल सं. .................</td>
            <td style="text-align: center">
               Withdrwl No./विथड्राल फाइल सं...........</td>
            <td style="text-align: center">
               T.C. No/टी. सी. फाइल सं. ............. </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <table class="style1">
                    <tr>
                        <td>
                            छात्र/छात्रा का नाम तथा धर्म
                        </td>
                        <td rowspan="5">
                            <table class="style1">
                                <tr>
                                    <td>
                                        Father&#39;s Name</td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mother&#39;s Name</td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Occupation
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Address</td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td rowspan="5">
                            <table class="style1">
                                <tr>
                                    <td align="center">
                                        अंतिम संस्थान जहाँ कि विद्यार्थी 
                                        <br />
                                        ने इसके पहले शिक्षा पाई हो
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Name of the Scholar with Caste,</td>
                    </tr>
                    <tr>
                        <td>
                            If Hindu,Otherwise Religion</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                जन्म तिथि (अंको में ) 
                <asp:Label ID="Label12" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="3">
                &nbsp;जन्म तिथि (शब्दों में )
                <asp:Label ID="Label13" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <%--<table class="style1">
                    <tr>
                        <td>
                           कक्षा/Class</td>
                        <td>
                            प्रवेश  तिथि Date Of Admission</td>
                        <td>
                           उत्तीर्ण  तिथि Date of Promotion</td>
                        <td>
                           त्याग  तिथि Date Of Removal</td>
                        <td>
                          अलग होने का कारण Cause of removal i.e. Nonpayment of Dues removal of Family,expulsions etc.</td>
                        <td>
                           वर्ष/Year</td>
                        <td>
                           चरित्र  और  कार्य /Conduct &amp; Work</td>
                    </tr>
                    <tr>
                        <td>
                            Nursery</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            K.G.</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            I</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; II</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; III</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; IV</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            5&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; V</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            6&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; VI</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            7&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; VII</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            8&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; VIII</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            9&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; IX</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            10&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; X</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            11&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; XI</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            12&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; XII</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>--%>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="कक्षा Class">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Class" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="प्रवेश  तिथि Date of Admission">
                            <ItemTemplate>
                                <asp:Label ID="lbl_date" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="उत्तीर्ण  तिथि Date of Promotion">
                            <ItemTemplate>
                                <asp:Label ID="lbl_promotion" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="त्याग  तिथि Date of Removal">
                            <ItemTemplate>
                                <asp:Label ID="lbl_relieve" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="अलग होने का कारण Cause of Removal i.e. Nonpayment of Dues, Removal of Family & Expulsions etc.">
                            <ItemTemplate>
                                <asp:Label ID="lbl_reason" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="वर्ष Year">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Year" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="चरित्र  और  कार्य Conduct &amp; Work">
                            <ItemTemplate>
                                <asp:Label ID="lbl_character" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>       
        <tr>
            <td align="center">
                &nbsp;</td>
            <td colspan="3" align="center">
                यह प्रमाणित किया जाता है कि उपर्युक्त छात्र लेखा पत्रक शिक्षा विभाग के 
                नियमानुसार त्याग तिथि तक यथोचित लिखा गया है। </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;</td>
            <td colspan="3" align="center">
                Certified the above Scholar&#39;s Register has been posted upto date Scholar&#39;s 
                leaving as required by the Department Rules.</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;</td>
            <td colspan="3" align="center">
                टिप्पणी :- यदि छात्र कक्षा में प्रथम पांच छात्रों में योग्यतानुसार हो तो कार्य 
                कोष्ठ में विवरण दे देना चाहिए। कक्षा IX से XII के बीच अगर विद्यार्थी संस्था 
                छोड़ता है तो उसकी उपस्थिति भी इस आदेश पत्र पर अंकित कर देना चाहिए।</td>
            <td>
             
                &nbsp;</td>
        </tr>      
        <tr>

        <td colspan="6" align="right">
             प्रधानाचार्य
             </td>
            
             </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
   <%-- </ItemTemplate>
    </asp:DataList>--%>
    
</div>
</asp:Content>

