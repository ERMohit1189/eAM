<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SmsForLatecomers.aspx.cs" Inherits="admin_SmsForLatecomers" %>

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
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpDDEmpYY" runat="server" OnSelectedIndexChanged="DrpDDEmpYY_SelectedIndexChanged" Enabled="False"
                                                        CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDDEmpMM" runat="server" OnSelectedIndexChanged="DrpDDEmpMM_SelectedIndexChanged" Enabled="False"
                                                        CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DrpDDEmpDD" runat="server" Enabled="False"
                                                        CssClass="form-control-blue col-sm-4 col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Department&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpDepartment" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DrpDepartment_SelectedIndexChanged" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                                                                                <div id="msgbox1" runat="server" style="left: 64px"></div>

                                    </div>
                                </div>
                                <div class="col-sm-12 " id="divExport" runat="server">
                                    <div class=" table-responsive  table-responsive2" id="abc" runat="server">
                                        <asp:GridView ID="Grd1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered">
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
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
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
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("FName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("EMobileNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delay">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtHours" runat="server" placeholder="HH" CssClass="form-control-blue" Width="35" MaxLength="2"></asp:TextBox>
                                                        <%--<asp:TextBox ID="txtColon" runat="server" Text=":" CssClass="textbox" Width="6" Enabled="false"></asp:TextBox>--%>
                                                        <span class="animated infinite pulse">: </span>
                                                        <asp:TextBox ID="txtMinutes" runat="server" placeholder="MM" CssClass="form-control-blue" Width="35" MaxLength="2"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="ChkAll" runat="server" OnCheckedChanged="ChkAll_CheckedChanged" AutoPostBack="true" Checked="true"></asp:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk" runat="server" Checked="true"></asp:CheckBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </div>


                                <div class="col-sm-12  text-center" id="table1" runat="server">
                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="button form-control-blue" OnClick="LinkButton2_Click">Submit</asp:LinkButton>
                                    <div id="msgbox" runat="server" style="left:70px"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>






        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


