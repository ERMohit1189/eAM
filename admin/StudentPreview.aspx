<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StudentPreview.aspx.cs"
    Inherits="admin_StudentPreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <%--Content starts--%>
    <table>
        <tr>
            <td>S.R. No. <span class="vd_red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtSrNo" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button">View Details</asp:LinkButton>
            </td>
        </tr>
    </table>
    <div align="right">
        <asp:Panel ID="Panel2" runat="server">
            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/print_icon.gif" OnClick="ImageButton4_Click" title="Print"
                Style="height: 16px;" />
        </asp:Panel>
    </div>
    <asp:Panel ID="Panel1" runat="server" BackColor="White">
        <table width="100%">
            <tr>
                <td align="center">
                   <div id="header" runat="server" style="width:80%"></div>
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                    S.R. No. :
                    <asp:Label ID="lblSrno" runat="server" Text="lblSrno" Style="font-weight: 700"></asp:Label>
                </td>
                <td align="right">Date :
                    <asp:Label ID="lblDate" runat="server" Text="lblDate" Style="font-weight: 700"></asp:Label>
                </td>
            </tr>
        </table>
        <hr />
        <h4>GENERAL DETAILS & FAMILY DETAILS</h4>
        <div style="border-bottom: 1px solid #ccc; margin-bottom: 4px; margin-top: 3px;">
        </div>
        <table class="rct_table" width="100%">
            <tr>
                <td width="170">
                    <strong>Student&#39;s Name</strong>
                </td>
                <td>
                    <asp:Label ID="lblStuName" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Father&#39;s Name</strong>&nbsp;</td>
                <td>
                    <asp:Label ID="lblFaName" runat="server"></asp:Label>
                </td>
                <td rowspan="9" align="right" valign="top" width="150">
                    <asp:Image ID="Image1" runat="server" Height="150px" Width="125px" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Mother&#39;s Name </strong>
                </td>
                <td>
                    <asp:Label ID="lblMothName" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Date of Birth </strong>
                </td>
                <td>
                    <asp:Label ID="lblDob" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Gender </strong>
                </td>
                <td>
                    <asp:Label ID="lblGender" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Blood Group</strong></td>
                <td>
                    <asp:Label ID="lblBloodGroup" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <strong>Nationality</strong></td>
                <td>
                    <asp:Label ID="lblNationality" runat="server"></asp:Label></td>
                <td>
                    <strong>Religion</strong></td>
                <td>
                    <asp:Label ID="lblReligion" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <strong>Categeory</strong></td>
                <td>
                    <asp:Label ID="lblCategeory" runat="server"></asp:Label></td>
                <td>
                    <strong>Caste</strong></td>
                <td>
                    <asp:Label ID="lblCaste" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <strong>Mobile No.</strong></td>
                <td>
                    <asp:Label ID="lblMobile" runat="server"></asp:Label></td>
                <td>
                    <strong>E-mail</strong></td>
                <td>
                    <asp:Label ID="lblmail" runat="server"></asp:Label></td>
            </tr>
            <%--<tr>
                <td>
                    <strong>Sibling Studying</strong></td>
                <td>
                    <asp:Label ID="lblSibling" runat="server"></asp:Label></td>
                <td>
                    <strong>Physically Disable</strong></td>
                <td>
                    <asp:Label ID="lblPhysically" runat="server"></asp:Label></td>
            </tr>--%>
            <tr>
                <td>
                    <strong>Father's Occupation</strong>
                </td>
                <td>
                    <asp:Label ID="lblFatherOccu" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Mother's Occupation</strong>
                </td>
                <td>
                    <asp:Label ID="lblMotherOccu" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Father's Designation</strong>
                </td>
                <td>
                    <asp:Label ID="lblFatherDesi" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Mother's Designation</strong>
                </td>
                <td>
                    <asp:Label ID="lblMotherDesi" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Father's Qualification</strong>
                </td>
                <td>
                    <asp:Label ID="lblFatherQuali" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Mother's Qualification</strong>
                </td>
                <td>
                    <asp:Label ID="lblMotherQuali" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Father's Income (Monthly)</strong>
                </td>
                <td>
                    <asp:Label ID="lblFatherInc" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Mother's Income (Monthly)</strong>
                </td>
                <td>
                    <asp:Label ID="lblMotherInc" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Family Income (Monthly)</strong>
                </td>
                <td>
                    <asp:Label ID="lblFamilyIncMon" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Father's Office Address</strong>
                </td>
                <td>
                    <asp:Label ID="lblFatherOffAdd" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Mother's Office Address</strong>
                </td>
                <td>
                    <asp:Label ID="lblMotherOffAdd" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Father's Contact No.</strong>
                </td>
                <td>
                    <asp:Label ID="lblFatherConNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Mother's Contact No.</strong>
                </td>
                <td>
                    <asp:Label ID="lblMotherConNo" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Father's E-mail</strong>
                </td>
                <td>
                    <asp:Label ID="lblFathermail" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Mother's E-mail</strong>
                </td>
                <td>
                    <asp:Label ID="lblMothermail" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Guardian name</strong>
                </td>
                <td>
                    <asp:Label ID="lblGuardianname" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Guardian Relationship</strong>
                </td>
                <td>
                    <asp:Label ID="lblGuardianRel" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Guardian's Contact No.</strong>
                </td>
                <td>
                    <asp:Label ID="lblGuardianContactNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Guardian E-mail</strong>
                </td>
                <td>
                    <asp:Label ID="lblGuardianmail" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>&nbsp;</strong>
                </td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
        <h4>SIBLING DETAILS</h4>
        <div style="border-bottom: 1px solid #ccc; margin-bottom: 4px; margin-top: 3px;">
        </div>
        <table class="rct_table" width="100%">
            <tr>
                <td width="120">
                    <strong>Sibling Studying</strong>
                </td>
                <td colspan="1">
                    <asp:Label ID="Label8" runat="server"></asp:Label>
                </td>
                <td width="120">
                    <strong>S.R. No.</strong>
                </td>
                <td>
                    <asp:Label ID="lblSiblingSrNo" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <strong>Name</strong>
                </td>
                <td width="37%">
                    <asp:Label ID="lblSiblingName" runat="server"></asp:Label>
                </td>
                <td width="120">
                    <strong>Father's Name</strong>
                </td>
                <td>
                    <asp:Label ID="lblSiblingFName" runat="server"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Class</strong>
                </td>
                <td width="37%">
                    <asp:Label ID="lblSiblingClass" runat="server"></asp:Label>
                </td>
                <td width="120">Section/Branch
                </td>
                <td>
                    <asp:Label ID="lblSiblingSection" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <h4>PHYSICALLY DISABLE DETAILS</h4>
        <div style="border-bottom: 1px solid #ccc; margin-bottom: 4px; margin-top: 3px;">
        </div>
        <table class="rct_table" width="100%">
            <tr>
                <td width="130">
                    <strong>Physically Disabled</strong>
                </td>
                <td colspan="1">
                    <asp:Label ID="lblPhysicallyDisabled" runat="server"></asp:Label>
                </td>
                <td width="120">
                    <strong>NAME</strong>
                </td>
                <td>
                    <asp:Label ID="lblPhysicallyName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Details</strong>
                </td>
                <td width="37%">
                    <asp:Label ID="lblPhysicallyDetails" runat="server"></asp:Label>
                </td>
                <td width="120">
                    <strong>&nbsp;</strong>
                </td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
        <h4>PRESENT ADDRESS</h4>
        <div style="border-bottom: 1px solid #ccc; margin-bottom: 4px; margin-top: 3px;">
        </div>
        <table class="rct_table" width="100%">
            <tr>
                <td width="120">
                    <strong>Address</strong>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblStuTempAdd" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>State </strong>
                </td>
                <td width="37%">
                    <asp:Label ID="lblStateTemp" runat="server"></asp:Label>
                </td>
                <td width="120">
                    <strong>City </strong>
                </td>
                <td>
                    <asp:Label ID="lblCityTemp" runat="server"></asp:Label>
                    &nbsp;
                    
                </td>
            </tr>

            <tr>
                <td>
                    <strong>Pin</strong></td>
                <td width="37%">
                    <asp:Label ID="lblPin" runat="server"></asp:Label></td>
                <td width="120">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

        </table>
        <h4>PERMANENT ADDRESS</h4>
        <div style="border-bottom: 1px solid #ccc; margin-bottom: 4px; margin-top: 3px;">
        </div>
        <table class="rct_table" width="100%">
            <tr>
                <td width="120">
                    <strong>Address </strong>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblStuPermanentAdd" runat="server"></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                    <strong>State</strong>
                </td>
                <td width="37%">
                    <asp:Label ID="lblStatePermanent" runat="server"></asp:Label>
                </td>
                <td width="120">
                    <strong>City </strong>
                </td>
                <td>
                    <asp:Label ID="lblCityPermanent" runat="server"></asp:Label>
                    &nbsp;
                    
                </td>
            </tr>

            <tr>
                <td>
                    <strong>Pin</strong></td>
                <td width="37%">
                    <asp:Label ID="lblPin0" runat="server"></asp:Label></td>
                <td width="120">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

        </table>
        <h4>PREVIOUS INSTITUTION DETAILS</h4>
        <div style="border-bottom: 1px solid #ccc; margin-bottom: 4px; margin-top: 3px;">
        </div>
        <table class="rct_table" width="100%">
            <tr>
                <td width="120">
                    <strong>Institute Name</strong>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblSchoolName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="120">
                    <strong>Institute Address</strong>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblAddPrev" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="120">
                    <strong>Institute State</strong>
                </td>
                <td width="37%">
                    <asp:Label ID="lblSchoolStatePrev" runat="server"></asp:Label>
                </td>

                <td width="120">
                    <strong>Institute City</strong>
                </td>
                <td>
                    <asp:Label ID="lblSchoolCityPrev" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="120">
                    <strong>From Date</strong>
                </td>
                <td>
                    <asp:Label ID="lblFormDD" runat="server"></asp:Label>
                </td>

                <td width="120">
                    <strong>To Date</strong>
                </td>
                <td>
                    <asp:Label ID="lblToDD" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="120">
                    <strong>Last Class</strong>
                </td>
                <td>
                    <asp:Label ID="lblStuLastClass" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="120">
                    <strong>Country</strong>
                </td>
                <td width="37%">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                <td width="120">
                    <strong>Board/University</strong>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="120">
                    <strong>Medium</strong>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                </td>
                <td width="120">
                    <strong>Marks</strong>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="120">
                    <strong>Percentage</strong>
                </td>
                <td width="37%">
                    <asp:Label ID="Label5" runat="server"></asp:Label>
                </td>
                <td width="120">&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>

        </table>
        <h4>OFFICIAL DETAILS</h4>
        <div style="border-bottom: 1px solid #ccc; margin-bottom: 4px; margin-top: 3px;">
        </div>
        <table class="rct_table" width="100%">
            <tr>
                <td width="140">
                    <b>S.R. No. </b>
                </td>
                <td width="35%">
                    <asp:Label ID="lblStuSrNo" runat="server"></asp:Label>
                </td>
                <td width="140">
                    <b>Class </b>
                </td>
                <td width="35%">
                    <asp:Label ID="lblStuClass" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Section/Branch </b>
                </td>
                <td>
                    <asp:Label ID="lblSection" runat="server"></asp:Label>
                </td>
                <td>
                    <b>Date of Admission </b>
                </td>
                <td>
                    <asp:Label ID="lblStuAdmission" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Fee Category </b>
                </td>
                <td>
                    <asp:Label ID="lblStuGroup" runat="server"></asp:Label>
                </td>
                <td>
                    <b>Medium </b>
                </td>
                <td>
                    <asp:Label ID="lblStuMedium" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Hostel </b>
                </td>
                <td>
                    <asp:Label ID="lblStuHostel" runat="server"></asp:Label>
                </td>
                <td>
                    <b>Transport </b>
                </td>
                <td>
                    <asp:Label ID="lblTransport" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>House </b>
                </td>
                <td>
                    <asp:Label ID="lblHouse" runat="server"></asp:Label>
                </td>
                <td>
                    <b>Admission Type </b>
                </td>
                <td>
                    <asp:Label ID="lblAddTypeStu" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Board/University</b></td>
                <td>
                    <asp:Label ID="lblStuBoard" runat="server"></asp:Label>
                </td>
                <td>
                    <strong>Library</strong></td>
                <td>
                    <asp:Label ID="Label6" runat="server"></asp:Label>

                </td>
            </tr>
            <tr>
                <td width="220">
                    <strong>Board/University Roll No.</strong>
                </td>
                <td width="37%">
                    <asp:Label ID="Label7" runat="server"></asp:Label>
                </td>
                <td width="50">&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td width="120">
                    <strong>Enquiry No.</strong>
                </td>
                <td width="37%">
                    <asp:Label ID="Label9" runat="server"></asp:Label>
                </td>
                <td width="160">
                    <strong>Subject Group</strong>
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="120">
                    <strong>Institute Roll No.</strong>
                </td>
                <td width="37%">
                    <asp:Label ID="Label11" runat="server"></asp:Label>
                </td>
                <td width="120">
                    <strong>File No.</strong>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="120">
                    <strong>Reference</strong>
                </td>
                <td width="37%">
                    <asp:Label ID="Label13" runat="server"></asp:Label>
                </td>
                <td width="120">
                    <strong>Remark</strong>
                </td>
                <td>
                    <asp:Label ID="Label14" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />

        <asp:Label ID="Label15" runat="server" Text="Signature of Guardian"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;
       <asp:Label ID="Label16" runat="server" Text="Signature of Admission In Charge"></asp:Label>
    </asp:Panel>
</asp:Content>
