<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="StdPunchReport.aspx.cs" Inherits="_1.StdPunchReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                try {
                    Sys.Application.add_load(datetime);
                    
                }
                catch (err) {

                }

            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" id="table1" runat="server">



                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpClass_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpSection_SelectedIndexChanged"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Attendance&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:DropDownList ID="drpAttendance" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpAttendance_SelectedIndexChanged">
                                                <asp:ListItem Text="<--Select-->" Value="-1"></asp:ListItem>
                                                <asp:ListItem Value="Present">Present</asp:ListItem>
                                                <asp:ListItem Value="Absent">Absent</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Date&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <%-- ReSharper disable once UnknownCssClass --%>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control-blue datepicker-normal validatetxt"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                        <div class="">
                                            <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="return ValidateTextBox('.validatetxt');"
                                                CssClass="button" OnClick="lnkSubmit_OnClick">Submit</asp:LinkButton>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <div class="col-sm-12  mgbt-xs-5 no-padding">

                                    <div style="float: right; font-size: 19px;">

                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkword" runat="server" OnClick="lnkword_OnClick" CssClass="icon-word-color"
                                                        title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkexcel" runat="server" OnClick="lnkexcel_OnClick" CssClass="icon-excel-color"
                                                        title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkpdf" runat="server" OnClick="lnkpdf_OnClick" CssClass="icon-pdf-color"
                                                        title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkprint" runat="server" OnClick="lnkprint_OnClick" CssClass="icon-print-color"
                                                        title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkword" />
                                                    <asp:PostBackTrigger ControlID="lnkexcel" />
                                                    <asp:PostBackTrigger ControlID="lnkpdf" />
                                                    <asp:PostBackTrigger ControlID="lnkprint" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </asp:Panel>

                                    </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <div id="divExport" runat="server">
                                            <div id="header" runat="server" style="width: 85%"></div>
                                            <h4 class="col-sm-12  text-center" style="font-weight: bold">
                                                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h4>
                                            <asp:GridView ID="grdStdPunchdata" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdStdPunchdata_OnRowDataBound"
                                                CssClass="table table-striped table-hover no-bm no-head-border table-bordered table-header-group" ShowFooter="true">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblsr" runat="server" Text="#"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsrnos" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClassName" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Section">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSectionName" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Roll No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInstituteRollNo" runat="server" Text='<%# Bind("InstituteRollNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Card No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCardNo" runat="server" Text='<%# Bind("CardNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Student's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="In Time">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIntime" runat="server" Text='<%# Bind("Intime") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Out Time">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOuttime" runat="server" Text='<%# Bind("Outtime") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Attendance">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAttendance" runat="server" Text='<%# Bind("Attendance") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <HeaderStyle CssClass="grid_heading_default" />
                                                <RowStyle CssClass="grid_details_default" />
                                            </asp:GridView>
                                            <p style="font-family: Courier New; font-size: 12px; /*background-color: #393a3e; */ /*color: white; */" class="text-right">
                                                Generated by eAM&reg; &nbsp;
                                            </p>
                                        </div>
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

