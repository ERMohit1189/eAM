<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AttendanceShift.aspx.cs" Inherits="admin_AttendanceShift" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Attendance Shift
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <table class="table" align="center" width="100%" id="tblInsert" runat="server">
                <tr>
                    <th>Attendance Shift</th>
                </tr>
                <tr>
                    <td align="left">
                        <br />
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAttendanceShift" runat="server" Text="Class"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAttendanceShift" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblShortName" runat="server" Text="Short Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtShortName" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblShiftTime" runat="server" Text="Shift Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtShiftTime" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFromTimeShift" runat="server" Text="From Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromTimeShift" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblToTimeShift" runat="server" Text="To Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToTimeShift" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblGraceTime" runat="server" Text="Grace Time For 1st/2nd Half"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGraceTime" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblLunchTime" runat="server" Text="Lunch"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLunchTime" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblFromTimeLunch" runat="server" Text="From Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromTimeLunch" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblToTimeLunch" runat="server" Text="To Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToTimeLunch" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsEarlyPunch" runat="server" Text="Is Early Punch Allowed"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblIsEarlyPunch" runat="server">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Label ID="lblNotifikationType" runat="server" Text="From Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbAll" runat="server" Text="All" />
                                    <asp:CheckBoxList ID="cblNotificationType" RepeatDirection="Horizontal" runat="server">
                                        <asp:ListItem Text="Present" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Absent" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="Leave" Value="L"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td>
                                    <asp:Label ID="lblNotification" runat="server" Text="Notification Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNotificationTime" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="7">
                        <br />
                        <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Enter" />
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click1" Text="Update" />
                        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" />
                    </td>
                </tr>
            </table>
            </td>
            </tr>
            <tr>
                <th>
                    <br />
                    Subject Paper List </th>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvAttendanceShift" runat="server" AutoGenerateColumns="false" CssClass="Grid" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="#" />
                            <asp:BoundField DataField="SubjectPaperName" HeaderText="Subject Paper Name" />
                            <asp:BoundField DataField="SubjectGroup" HeaderText="Subject Group" />
                            <asp:BoundField DataField="WeekPeriodCount" HeaderText="Week Period Count" />
                            <asp:BoundField DataField="IsForExam_txt" HeaderText="Is For Exam" />
                            <asp:BoundField DataField="ClassName" HeaderText="Class" />
                            <asp:BoundField DataField="BranchName" HeaderText="Stream" />
                            <asp:BoundField DataField="SectionName" HeaderText="Section" />

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:Label ID="lblClassID" runat="server" Text='<%# Eval("ClassID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblSectionID" runat="server" Text='<%# Eval("SectionID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblBranchID" runat="server" Text='<%# Eval("BranchID") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="edit" Font-Size="0pt" Height="16px" OnClick="lbtnEdit_Click" Text='<%# Eval("S02ID") %>' Width="16px"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete" Font-Size="0pt" Height="16px" OnClick="lbtnDelete_Click" Text='<%# Eval("S02ID") %>' Width="16px"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            </table>

                <div style="overflow: auto; width: 1px; height: 1px">
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup">
                        <table width="100%" class="table">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAttendanceShift0" runat="server" Text="Class"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAttendanceShift0" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblShortName0" runat="server" Text="Short Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtShortName0" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblShiftTime0" runat="server" Text="Shift Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtShiftTime0" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFromTimeShift0" runat="server" Text="From Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromTimeShift0" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblToTimeShift0" runat="server" Text="To Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToTimeShift0" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblGraceTime0" runat="server" Text="Grace Time For 1st/2nd Half"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGraceTime0" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblLunchTime0" runat="server" Text="Lunch"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLunchTime0" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblFromTimeLunch0" runat="server" Text="From Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromTimeLunch0" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblToTimeLunch0" runat="server" Text="To Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToTimeLunch0" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIsEarlyPunch0" runat="server" Text="Is Early Punch Allowed"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblIsEarlyPunch0" runat="server">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Label ID="lblNotifikationType0" runat="server" Text="From Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chAll0" runat="server" Text="All" />
                                    <asp:CheckBoxList ID="cblIsEarlyPunch0" RepeatDirection="Horizontal" runat="server">
                                        <asp:ListItem Text="Present" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Absent" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="Leave" Value="L"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td>
                                    <asp:Label ID="lblNotification0" runat="server" Text="Notification Time"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNotificationTime0" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                        CancelControlID="Button4" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="pnlDelete" runat="server" CssClass="popup">
                    <table width="100%">
                        <tr>
                            <td align="center" height="50">
                                <h4>Do you really want to delete this record?<asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="50">
                                <asp:Button ID="btnYes" runat="server" CausesValidation="False" Text="Yes" OnClick="btnYes_Click" />
                                <asp:Button ID="btnNo" runat="server" CausesValidation="False" Text="No" OnClick="btnNo_Click" />
                            </td>
                        </tr>
                    </table>
                    <ajaxToolkit:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                        Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                    </ajaxToolkit:ModalPopupExtender>
                </asp:Panel>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

