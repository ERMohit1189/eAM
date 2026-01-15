<%@ Page Title="" Language="C#" MasterPageFile="~/website/MasterPage.master" AutoEventWireup="true" CodeFile="CAREER.aspx.cs" Inherits="website_CAREER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-danger">
        <div class="panel-heading">CAREER</div>
        <div class="row">
            <div class="main-content col-md-8 col-md-offset-2 col-sm-12  my-mar-top2">

                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>

                        <div class="main-career">
                            <h3><%# Eval("JobTitle") %></h3>

                            <div class=" career">
                                <table class="table ">
                                    <tbody>
                                        <tr>
                                            <th>Department</th>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("DepartMent") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <th>Post</th>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Post") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <th>Experience</th>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Experience") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <th>Qualification</th>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Qualification") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <th>Salary</th>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Salary") %>'></asp:Label></td>

                                        </tr>

                                        <tr>
                                            <th>No. of Position(s)</th>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("NoofPosition") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <th>Key Skills</th>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("JobDescription") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <th>Date of Posting</th>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("Fdate") %>'></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <asp:LinkButton ID="LinkButton1" runat="server" ToolTip='<%# Bind("JobId") %>' class="webform-submit button-primary btn-primary btn form-submit" OnClick="LinkButton1_Click">Apply</asp:LinkButton>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>


            </div>

        </div>
    </div>

</asp:Content>

