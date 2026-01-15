<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true"
    CodeFile="TicketReAllotmentorChangeStatus.aspx.cs" Inherits="admin_TicketReAllotmentorChangeStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top ">
                    <div class="panel-body ">
                        <table class="table p-table p-table-bordered table-hover no-bm table-striped table-bordered pro-table">
                            <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <th>Ticket ID</th>
                                        <th>Created BY</th>
                                        <th>Contact No.</th>
                                        <th>Email</th>
                                        <th>Subject</th>
                                        <th>Assign To</th>
                                        <th>Re-Assign To</th>
                                        <th>User's List</th>
                                        <th>Status</th>
                                        <th>Update</th>
                                        <th>View</th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblrefno" runat="server" Text='<%# Eval("REFNO") %>'></asp:Label>
                                        </td>
                                        <td><%# Eval("Name") %> (<%# Eval("LoginType") %>)</td>
                                        <td><%# Eval("ContactNo") %></td>
                                        <td><%# Eval("Email") %></td>
                                        <td><%# Eval("Subject") %></td>
                                        <td>
                                            <%# Eval("AssignTo") %>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpAssignTo" runat="server" OnSelectedIndexChanged="drpAssignTo_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="2">Admin</asp:ListItem>
                                                <asp:ListItem Value="3">Staff</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="drpUserList" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("isClosed") %>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="drpStatus" runat="server">
                                                <asp:ListItem Value="False">Open</asp:ListItem>
                                                <asp:ListItem Value="True">Closed</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click">Update</asp:LinkButton></td>
                                        <td>
                                            <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click">View</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

