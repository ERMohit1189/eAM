<%@ Page Title="" Language="C#" MasterPageFile="~/staff/staff_root-manager.master" AutoEventWireup="true" CodeFile="MonthwiseStudentAttendenceReport_Newoo.aspx.cs" Inherits="admin_MonthwiseStudentAttendenceReport_Newoo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>

    <asp:UpdatePanel ID="dfg" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding mgbt-xs-20">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow"
                                        OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" Visible="false">
                                        <asp:ListItem Selected="True">Classwise</asp:ListItem>
                                        <asp:ListItem>Studentwise</asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>
                                <div runat="server" id="table1" class="col-sm-12 no-padding">
                                    <div class="col-sm-6 no-padding mgbt-xs-20">
                                        <div class="form-group ">
                                            <asp:Label ID="Label10" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Select *"></asp:Label>

                                            <div class="col-sm-8  ">
                                                <asp:DropDownList ID="DrpEnter" runat="server" CssClass="form-control-blue ">
                                                    <asp:ListItem Value="srno">S.R. No.</asp:ListItem>
                                                    <asp:ListItem Value="StEnRCode">Enrollment  No.</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 no-padding mgbt-xs-20">
                                        <div class="form-group ">
                                            <asp:Label ID="Label7" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Enter *"></asp:Label>

                                            <div class="col-sm-8  ">
                                                <asp:TextBox ID="TxtEnter" runat="server" CssClass="form-control-blue "></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 no-padding mgbt-xs-20">
                                        <div class="form-group ">
                                            <div class="col-sm-8  ">
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CssClass="button">Show</asp:LinkButton>
                                                <asp:Label ID="lblFee" runat="server" Style="color: #FF0000"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div runat="server" class="col-sm-6 no-padding mgbt-xs-20" id="table3">
                                    <div class="form-group ">
                                        <asp:Label ID="Label13" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Month"></asp:Label>

                                        <div class="col-sm-8  ">
                                            <asp:DropDownList ID="drpMonth1" runat="server" AutoPostBack="True" CssClass="form-control-blue "
                                                OnSelectedIndexChanged="drpMonth1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div runat="server" id="table4" class="col-sm-12 no-padding">
                                    <div class="col-sm-12 no-padding ">
                                        <div class="col-sm-6 no-padding mgbt-xs-20">
                                            <div class="form-group ">

                                                <asp:Label ID="Label12" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Class"></asp:Label>
                                                <div class="col-sm-8  ">
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="drpClass" runat="server" AutoPostBack="True" CssClass="form-control-blue"
                                                                OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-6  no-padding mgbt-xs-20">
                                            <div class="form-group ">
                                                <asp:Label ID="Label11" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Stream"></asp:Label>
                                                <div class="col-sm-8  ">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="drpStream" runat="server" CssClass="form-control-blue ">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 no-padding ">

                                        <div class="col-sm-6 no-padding mgbt-xs-20">
                                            <div class="form-group ">

                                                <asp:Label ID="Label9" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Section"></asp:Label>
                                                <div class="col-sm-8  ">
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="drpSection" runat="server" AutoPostBack="True" CssClass="form-control-blue ">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-6 no-padding mgbt-xs-20">
                                            <div class="form-group ">

                                                <asp:Label ID="Label8" runat="server" class="col-sm-4  txt-bold txt-middle-l" Text="Month"></asp:Label>
                                                <div class="col-sm-8  ">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="drpMonth" runat="server" AutoPostBack="True" CssClass="form-control-blue ">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 no-padding mgbt-xs-20">
                                        <div class="form-group ">
                                            <div class="col-sm-8 ">
                                                <asp:LinkButton ID="lnkSubmit" CssClass="button" runat="server" OnClick="lnkSubmit_Click" ValidationGroup="a">Submit</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12  mgbt-xs-10">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div style="float: right; font-size: 19px;" id="Panel2" runat="server">

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

                                <%-- <div class="col-sm-12 no-padding ">
                            <div style="float: right">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/export_word_icon.gif" OnClick="ImageButton1_Click"
                                        title="Export to Word" Style="height: 16px" CausesValidation="False" ValidationGroup="b" />
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/export_excel_icon.gif" OnClick="ImageButton2_Click"
                                        title="Export to Excel" ValidationGroup="b" />
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/export_pdf_icon.gif" OnClick="ImageButton3_Click"
                                        Style="width: 16px" title="Export to PDF" ValidationGroup="b" />
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/print_icon.gif" OnClick="ImageButton4_Click" title="Print"
                                        Style="height: 16px;" ValidationGroup="b" />
                                </asp:Panel>
                            </div>
                        </div>--%>

                                <div class="col-sm-12 no-padding ">
                                    <div class=" table-responsive  table-responsive2">
                                        <table width="100%" runat="server" id="table2">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table p-table p-table-bordered table-hover table-striped table-bordered">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.R. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enrollment No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("StEnRCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Class">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Stream">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label29" runat="server" Text='<%# Bind("BranchName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Section">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Medium">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label27" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fee Group" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label28" runat="server" Text='<%# Bind("Card") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>

                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div class="col-sm-12 no-padding ">
                                    <div id="divExport" runat="server">
                                        <div class=" table-responsive  table-responsive2">
                                            <table id="abc" runat="server" width="100%">
                                                <tr>
                                                    <td>
                                                        <div id="header" runat="server" style="width: 80%"></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="text-center">
                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table p-table p-table-bordered table-hover table-striped table-bordered">
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="S.R.NO.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Style="font-weight: 700" Text='<%# Bind("srno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Style="font-weight: 700" Text='<%# Bind("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl1" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl2" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl3" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl4" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl5" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl6" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl7" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl8" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl9" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl10" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl11" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl12" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl13" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl14" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl15" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl16" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl17" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl18" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl19" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl20" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl21" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl22" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl23" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl24" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl25" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl26" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl27" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl28" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl29" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl30" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl31" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Working Days">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalWorkingDays" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Present">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalPresent" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Absent">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalAbsent" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
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

