<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ViewTicket.aspx.cs" Inherits="common_ViewTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script>
        function onenter(e) {
            if (e.keyCode === 13) {
                document.getElementById('<%# lnkSubmit.ClientID %>').focus();
                document.getElementById('<%# lnkSubmit.ClientID %>').click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Enter Ticket ID</label>
                                    <div class="txt-middle">
                                        <asp:TextBox ID="txtRefNo" onkeypress="onenter(event);" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                    <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');"
                                        CssClass="button form-control-blue" OnClick="lnkSubmit_Click" ValidationGroup="a">Submit</asp:LinkButton>
                                    <div id="msgbox" runat="server" style="left: 76px"></div>
                                </div>
                                <div class="col-md-12">
                                    <table class="table p-table p-table-bordered table-hover no-bm table-striped table-bordered pro-table">
                                        <asp:Repeater ID="rpt" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th>Ticket ID</th>
                                                    <th>Created BY</th>
                                                    <th>Contact No.</th>
                                                    <th>Email</th>
                                                    <th>Subject</th>
                                                    <th>Assign By</th>
                                                    <th>View</th>
                                                    <th>Status</th>
                                                    <th>Update</th>
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
                                                        <asp:Label ID="lblAssignby" runat="server" Text='<%# Eval("Assignby") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click">View</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <div id="status" runat="server" visible='<%# Eval("isClosed").ToString()=="True"?false:true %>'>
                                                            <asp:DropDownList ID="drpClose" runat="server" OnSelectedIndexChanged="drpClose_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="True">Open</asp:ListItem>
                                                                <asp:ListItem Value="False">Close</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click"
                                                            OnClientClick="return confirmticketClosing();" Visible="false">Update</asp:LinkButton>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function confirmticketClosing() {
            if (confirm("do you want to close this ticket!")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>

