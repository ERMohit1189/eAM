<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true"CodeFile="driver_registration.aspx.cs" Inherits="admin_driver_registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
 <div class="maincontent">
			<div class="codepart">
				<div class="hedingbg">
                    <h3 class="h3txt">Transportation</h3>
                </div>
				<div class="hedingline">
					<h4 class="h4txt">Driver Registration</h4>
				</div>
				<div class="contentbox">

                    <table align="center" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table align="center" cellpadding="0" cellspacing="0" width="40%">
                                    <tr>
                                        <td align="right">
                                            Category :</td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownList1" runat="server" SkinID="ddDefault">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server">
                                    <table cellpadding="0" cellspacing="0" width="80%">
                                        <tr>
                                            <td colspan="3">
                                                <table align="right" cellpadding="0" cellspacing="0" width="67%">
                                                    <tr>
                                                        <td align="right" width="125px">
                                                            Employee&nbsp; Id :</td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox1" runat="server" SkinID="TxtBoxDef"></asp:TextBox><span class="vd_red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" SkinID="Show"></asp:LinkButton>
                                                        </td>
                                                        <td width="100px">
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="GridView1" runat="server" Width="100%">
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server">
                                    <table cellpadding="0" cellspacing="0" align="center" width="90%">
                                        <tr>
                                            <td height="10px" colspan="6">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Name :</td>
                                            <td align="left">
                                                <asp:TextBox ID="TextBox2" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td align="left">
                                                &nbsp;</td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td align="left">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="6">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Country :</td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropDownList2" runat="server" SkinID="ddDefault">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                State :</td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropDownList3" runat="server" SkinID="ddDefault">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                City :</td>
                                            <td align="left">
                                                <asp:DropDownList ID="DropDownList4" runat="server" SkinID="ddDefault">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="6">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Address :</td>
                                            <td align="left">
                                                <asp:TextBox ID="TextBox3" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                Mobile No. :</td>
                                            <td align="left">
                                                <asp:TextBox ID="TextBox4" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                &nbsp;E-mail :&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="TextBox5" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px" colspan="6">
                                                </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="80%">
                                    <tr>
                                        <td><h4 class="h4txt">
                                            License Details</h4></td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td height="10px" colspan="4">
                                                </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            License No.: </td>
                                        <td align="left">
                                                <asp:TextBox ID="TextBox6" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td height="10px" colspan="4">
                                                </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Issued On :</td>
                                        <td align="left" style="padding-left: 5px">
                                            <asp:DropDownList ID="DropDownList5" runat="server" SkinID="ddlSize2">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList6" runat="server" SkinID="ddlSize1">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList7" runat="server" SkinID="ddlSize0">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            Valid Upto:</td>
                                        <td align="left" style="padding-left: 5px">
                                            <asp:DropDownList ID="DropDownList8" runat="server" SkinID="ddlSize2">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList9" runat="server" SkinID="ddlSize1">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownList10" runat="server" SkinID="ddlSize0">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                       <td height="10px" colspan="4">
                                                </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Catrgory :</td>
                                        <td align="left">
                                                <asp:TextBox ID="TextBox7" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                        <td align="right">
                                                Issuing Authority :
                                        </td>
                                        <td>
                                                <asp:TextBox ID="TextBox8" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td height="10px" colspan="4">
                                                </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;</td>
                                        <td align="left" style="padding-left: 5px">
                                            <asp:LinkButton ID="LinkButton2" runat="server" SkinID="save"></asp:LinkButton>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>

                </div>
                </div>
                </div>


</asp:Content>

