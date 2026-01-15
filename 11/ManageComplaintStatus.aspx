<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="ManageComplaintStatus.aspx.cs" Inherits="admin_ManageComplaintStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script>
        function onenter(e) {
            if (e.keyCode === 13) {
                document.getElementById('<%# lnkSubmit.ClientID %>').focus();
                document.getElementById('<%# lnkSubmit.ClientID %>').click();
            }
        }
    </script>
    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 60px;
            height: 34px;
        }

            .switch input {
                display: none;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 26px;
                width: 26px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- <script>
                
                Sys.Application.add_load(scrollbar);
            </script>--%>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server">
                                    <label class="control-label">Enter Conversation ID</label>
                                    <div class="">
                                        <asp:TextBox ID="txtRefNo" onkeypress="onenter(event);" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                    </div>
                                </div>

                                <%--     <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');"
                                            CssClass="button form-control-blue" OnClick="lnkSubmit_Click" ValidationGroup="a">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px"></div>
                                    </div>--%>
                                <div class="col-sm-4  half-width-50  btn-a-devices-1-p1-p2 mgbt-xs-15" runat="server">
                                    <label class="control-label">&nbsp;</label>
                                    <div class="txt-middle">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');"
                                            CssClass="button form-control-blue" OnClick="lnkSubmit_Click" ValidationGroup="a">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px"></div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server">
                                    <label class="control-label">Status </label>
                                    <div class="">
                                        <asp:DropDownList runat="server" ID="ddlstatus" CssClass="form-control-blue validatetxt" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Selected="True" Text="Active" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Closed" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Both" Value="Both"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">

                                        <table class="table table-striped table-hover no-bm no-head-border table-bordered table-header-group">
                                            <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                                                <HeaderTemplate>
                                                    <tr>
                                                        <th class="text-left">#</th>
                                                        <th class="text-left">Conversation ID</th>
                                                        <th class="text-left">Created By (User Type)</th>
                                                        <th class="text-left">Subject</th>
                                                        <th>Status</th>

                                                        <%--<th>Edit</th>--%>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex+1 %></td>
                                                        <td>
                                                            <asp:Label ID="lblrefno" runat="server" Text='<%# Eval("ConversationID") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("CreatedBy") %>'></asp:Label>
                                                            <%--   <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>' Visible="false"></asp:Label>--%>
                                                            <%--   <asp:Label ID="lblLoginType" runat="server" Text='<%# Eval("LoginType") %>' Visible="false"></asp:Label></td>--%>
                                                            <td><%# Eval("Subject") %></td>

                                                            <td class="text-center">
                                                                <asp:DropDownList ID="drpClose" runat="server" Width="100px" Visible="False">
                                                                    <asp:ListItem Text="Active" Value="Active" style="background-color: #1fae66 !important"></asp:ListItem>
                                                                    <asp:ListItem Text="Closed" Value="Closed" style="background-color: #DA4448 !important"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("IsActive") %>' Visible="False"></asp:Label>
                                                                <label class="switch">
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("IsActive").ToString() != "False" %>' OnCheckedChanged="CheckBox1_OnCheckedChanged" AutoPostBack="True" />
                                                                    <span class="slider round"></span>
                                                                </label>

                                                            </td>
                                                            <td class="text-center" style="display: none">
                                                                <asp:LinkButton ID="lnkUpdate" OnClick="lnkUpdate_Click" runat="server" class="btn-reply"
                                                                    OnClientClick="return confirmticketClosing();">Update</asp:LinkButton>
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
            if (confirm("do you want to close this Complaint!")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>

