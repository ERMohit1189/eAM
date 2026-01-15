<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="CustomizeMessageforStaff.aspx.cs" Inherits="admin_CustomizeMessageforStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <script>
        function GetCount(txtStr) {

            document.getElementById("<%= Label8.ClientID %>").innerHTML = txtStr.length;

        }
        function alertmsg() {
            alert(
                "It looks like you are not connected to the Internet.\nPlease check your Internet connection and try again.");
        }
    </script>
    <div aling="center" id="show" runat="server">
    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-4  col-md-4 half-width-50">
                                    <label class="control-label">Department&nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <asp:DropDownList ID="DrpDepartment" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="DrpDepartment_SelectedIndexChanged" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4  col-md-4 half-width-50 mgbt-xs-15">
                                    <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                    <div class="">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button form-control-blue" OnClick="LinkButton1_Click" ValidationGroup="a">Send</asp:LinkButton>
                                        <div class="text-box-msg">
                                            <div id="msgbox1" runat="server" style="left: 64px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12  col-md-12 col-lg-12 mgbt-xs-20" runat="server" id="table3">
                                    <label class="control-label">Message &nbsp;<span class="vd_red">*</span></label>
                                    <div class="">
                                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="4" Font-Size="12"
                                            CssClass="form-control-blue" onkeyup="GetCount(this.value);"></asp:TextBox>
                                        <div class="text-box-msg">
                                            <span style="text-align: left">
                                                <asp:Label ID="Label11" runat="server" CssClass="control-label " Text="Entered Characters:"></asp:Label>
                                                <asp:Label ID="Label8" runat="server" Text="0"></asp:Label></span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage=""
                                                ControlToValidate="txtMessage" ForeColor="Red" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Label3" runat="server" CssClass="control-label " Text=" (For Unicode SMS: No. of characters will be extra according to content.)"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12  mgbt-xs-15" id="Panel2" runat="server">
                                    <div class=" table-responsive  table-responsive2">

                                        <asp:GridView ID="Grd1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered table-header-group">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Ecode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("EFirstName") %>'></asp:Label>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("EMiddleName") %>'></asp:Label>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("ELastName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label19" runat="server" Text='<%# Bind("EMobileNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delay">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="ChkAll_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk" runat="server" Checked="true" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

