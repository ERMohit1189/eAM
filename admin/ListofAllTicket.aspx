<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ListofAllTicket.aspx.cs" Inherits="common_ViewTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
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
                                        <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
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
                                                    <th>Assign To</th>
                                                    <th>Status</th>
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
                                                        <asp:Label ID="lblAssignby" runat="server" Text='<%# Eval("Assignby") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAssignto" runat="server" Text='<%# Eval("Assignto") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("isClosed").ToString()=="True"?"Closed":"Open" %>'></asp:Label>
                                                    </td>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

