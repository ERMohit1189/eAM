<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StdAttendanceAlert.aspx.cs" Inherits="_1.StdAttendanceAlert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div runat="server" id="loader"></div>
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <script>
                
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="ShiftTime" runat="server" class="control-label" Text="Class"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label1" runat="server" class="control-label" Text="Section"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label2" runat="server" class="control-label" Text="Stream"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="drpBranch" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label3" runat="server" class="control-label" Text="Attendance"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="drpAttendance" runat="server">
                                                <asp:ListItem Text="<--Select-->" Value="-1"></asp:ListItem>
                                                <asp:ListItem Value="P">Present</asp:ListItem>
                                                <asp:ListItem Value="A">Absent</asp:ListItem>
                                                <asp:ListItem Value="Lt">Late</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label4" runat="server" class="control-label" Text="Date"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="datepicker-normal"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <div class="">
                                            <asp:LinkButton ID="lnkView" runat="server" CssClass="button form-control-blue " OnClick="lnkView_Click">View</asp:LinkButton>
                                            &nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button form-control-blue " OnClick="lnkSubmit_Click">Send</asp:LinkButton>
                                            <div class="text-box-msg">
                                                <div id="divmsg" runat="server" style="left: 150px;"></div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-sm-12  no-padding">
                                    <asp:GridView ID="grdAlertData" runat="server" AutoGenerateColumns="false"
                                        class="table table-striped table-hover no-bm no-head-border table-bordered">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Checked="true" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSr" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S.R. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Eval("srno") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Class">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Eval("class") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSection" runat="server" Text='<%# Eval("section") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Attendance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttendanceValue" runat="server" Text='<%# Eval("AttendanceValue") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="In">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInTime" runat="server" Text='<%# Eval("InTime") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Contact No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFamilyContactNo" runat="server" Text='<%# Eval("FamilyContactNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="SMS">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSMS" TextMode="MultiLine" runat="server" Text='<%# Eval("sms") %>'></asp:TextBox>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="400px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

