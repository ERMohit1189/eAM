<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="subjectwise_attendance_Update.aspx.cs" Inherits="_1.AdminSubjectwiseAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
  <div id="loader" runat="server"></div>  <%-- ==== in aspx file   --%>                            

    <asp:UpdatePanel ID="hgy" runat="server">
        <ContentTemplate>
    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 mgbt-xs-20">
                <div class="panel widget light-widget panel-bd-top">
                    <div class="panel-body">
                        <div class="col-sm-12  no-padding">
                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="dd" runat="server" class="control-label" Text="Branch"></asp:Label>
                                        &nbsp;<span class="vd_red">*</span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DrpSaal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpSaal_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DrpMahina" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpMahina_SelectedIndexChanged" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DrpDin" runat="server" AutoPostBack="True" CssClass="form-control-blue col-xs-4">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DrpAtteClass" runat="server" OnSelectedIndexChanged="DrpAtteClass_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Section/Branch&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DrpAttenSection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpAttenSection_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Select Subject&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" CssClass="form-control-blue" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="text-box-msg">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                <asp:LinkButton ID="btnShow" runat="server" OnClick="btnShow_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                            <div id="msgbox" runat="server" style="left:64px"></div>
                            </div>

                             <div class="col-sm-12 ">
                                <div class=" table-responsive  table-responsive2">
                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table no-bm p-table p-table-bordered table-hover table-striped table-bordered">

                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S.R. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Enrollment No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Student_Name") %>'></asp:Label>                                                 
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attendance">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="col-sm-12  mgbt-xs-10  btn-a-devices-6-p6">
                                <asp:Button ID="Button1" runat="server" CssClass="button form-control-blue" Text="Submit Attendance" OnClick="Button1_Click" />
                                <div id="msgbox1" runat="server" style="left:152px"></div>
                                
                                <asp:Label ID="lblmess" runat="server" Style="font-weight: 700; color: #CC0000"></asp:Label>
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

