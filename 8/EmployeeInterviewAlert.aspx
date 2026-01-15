<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmployeeInterviewAlert.aspx.cs" Inherits="SuperAdmin_Customized_Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">


    <script type="text/javascript">
        function count(clientId) {
            var txtInput = document.getElementById(clientId);
            var spanDisplay = document.getElementById('spanDisplay');
            spanDisplay.innerHTML = txtInput.value.length;
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body ">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-12  no-padding">
                                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                                <label class="control-label">Designation&nbsp;<span class="vd_red">*</span></label>
                                                <div class="">

                                                    <asp:DropDownList ID="drpdes" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpdes_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                    <div class="text-box-msg">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4  half-width-50 mgbt-xs-9" runat="server" id="table3">
                                                <label class="control-label">Message &nbsp;<span class="vd_red">*</span></label>
                                                <div class="">
                                                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="1"
                                                        CssClass="form-control-blue" onkeyup="GetCount(this.value);"></asp:TextBox>
                                                    <div class="text-box-msg">
                                                        <span style="text-align: left">Total No. of words :- 
                                                    <asp:Label ID="Label8" runat="server" Text="0"></asp:Label></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                                <div id="msgbox" runat="server" style="left: 74px;"></div>

                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" OnClick="LinkButton1_Click">Send</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="button" OnClick="LinkButton3_Click" Visible="false">Stop</asp:LinkButton>

                                            </div>
                                        </div>
                                        <div class="col-sm-12  mgtp-10">
                                            <div class=" table-responsive  table-responsive2">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Repeater ID="Repeater1" runat="server">
                                                            <HeaderTemplate>
                                                                <table id="table1" class="table pro-table mp-table no-tb no-bm p-table-bordered table-hover table-striped table-bordered text-center">
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th class="text-left">Name</th>
                                                                        <th class="text-left">Father's Name</th>
                                                                        <th>Gender</th>
                                                                        <th>Contact No.</th>
                                                                        <th>Designation</th>
                                                                        <th>
                                                                            <asp:CheckBox ID="ChkAll" AutoPostBack="true" OnCheckedChanged="ChkAll_CheckedChanged" runat="server" /></th>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="width: 50px"><%# Container.ItemIndex+1 %></td>
                                                                    <td><%# Eval("EmpName") %></td>
                                                                    <td><%# Eval("EmpFather") %></td>
                                                                    <td><%# Eval("EmpGender") %></td>
                                                                    <td>
                                                                        <asp:Label ID="lblContactNo" runat="server" Text='<%# Eval("EmpContactNo") %>'></asp:Label></td>
                                                                    <td><%# Eval("EmpDesName") %></td>
                                                                    <td>
                                                                        <asp:CheckBox ID="Chk" runat="server" /></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

