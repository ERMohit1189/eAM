<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="StudenceAttendanceDayWiseReport.aspx.cs" Inherits="common_StudenceAttendanceDayWiseReport" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="uhg" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue validatedrp"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="FromYY" runat="server" OnSelectedIndexChanged="FromYY_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromMM" runat="server" OnSelectedIndexChanged="FromMM_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="FromDD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FromDD_SelectedIndexChanged"
                                                        CssClass="form-control-blue col-xs-4">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide ">
                                        <label class="control-label">To&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ToYY" runat="server" OnSelectedIndexChanged="ToYY_SelectedIndexChanged" CssClass="form-control-blue col-xs-4"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToMM" runat="server" OnSelectedIndexChanged="ToMM_SelectedIndexChanged" CssClass="form-control-blue col-xs-4"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ToDD" runat="server" CssClass="form-control-blue col-xs-4" AutoPostBack="True" OnSelectedIndexChanged="ToDD_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton5" runat="server" OnClientClick="return ValidateDropdown('.validatedrp');"
                                             OnClick="LinkButton5_Click" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 59px"></div>

                                    </div>

                                </div>


                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel1" runat="server">

                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

                                                <script>
                                                    
                                                </script>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ImageButton1" />
                                            <asp:PostBackTrigger ControlID="ImageButton2" />
                                            <asp:PostBackTrigger ControlID="ImageButton3" />
                                            <asp:PostBackTrigger ControlID="ImageButton4" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>



                                <div class="col-sm-12 " id="gdv" runat="server">

                                    <div class=" table-responsive  table-responsive2" id="abc" runat="server">
                                        <div id="header" runat="server"></div>
                                        <div class="col-sm-12  text-center">
                                            <asp:Label ID="Label14" runat="server"></asp:Label>
                                        </div>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="False" class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.R. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mother's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("MotherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("AttendanceDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Attendance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("AttendanceValue") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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

