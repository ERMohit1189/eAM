<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="SubjectwiseCumlativeIXtoX.aspx.cs" Inherits="SubjectwiseCumlativeIXtoX" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" id="div1" runat="server">

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>

                                            <div class="col-sm-12  no-padding ">

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpsection" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="drpsection_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="ddlMedium" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="true" OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Evaluation&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpEval" runat="server" CssClass="form-control-blue validatedrp" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpEval_SelectedIndexChanged">
                                                            <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                            <asp:ListItem Value="TERM1">TERM 1</asp:ListItem>
                                                            <asp:ListItem Value="TERM2">TERM 2</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control-blue validatedrp"
                                                            AutoPostBack="true" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                                    <label class="control-label">Paper&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:DropDownList ID="drpPaper" runat="server" CssClass="form-control-blue validatedrp">
                                                        </asp:DropDownList>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3  half-width-50 mgbt-xs-15" style="display: none">
                                                    <label class="control-label">Maximum Marks&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtMax" runat="server" CssClass="form-control-blue" Width="50px" Enabled="false"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3" runat="server" id="divHideForGardian8">
                                                    <label class="control-label">&nbsp;<span class="vd_red"></span></label>
                                                    <div class="">
                                                        <asp:LinkButton runat="server" ID="lnkView" class="button" Text="View" OnClick="lnkView_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();"></asp:LinkButton>
                                                        <div class="text-box-msg"></div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12   mgbt-xs-15">
                                                <asp:Label ID="Label33" runat="server" class="txt-bold txt-middle-l text-danger" Text="Note:- ML=>Medical Leave,  NAD=>New Admission, AB=>Absent."></asp:Label>

                                            </div>



                                            <div class="col-sm-12  " id="divExport" runat="server" visible="false">
                                                <div class="col-sm-12 no-padding">

                                                    <div style="float: right; font-size: 19px;">

                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                                    title="Export to Word"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                                    title="Export to Excel"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                                    title="Export to PDF"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                                <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                                    title="Print"><i class="fa fa-print "></i></asp:LinkButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="ImageButton1" />
                                                                <asp:PostBackTrigger ControlID="ImageButton2" />
                                                                <asp:PostBackTrigger ControlID="ImageButton3" />
                                                                <asp:PostBackTrigger ControlID="ImageButton4" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>


                                                    </div>
                                                </div>
                                                <div class="col-sm-12" id="table1" runat="server">
                                                    <div class="table-responsive2 table-responsive">
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table  table-header-group ">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label15" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="S.R. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="SrNo" runat="server" Text='<%# Bind("srno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Student's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="MonthlyTesth" runat="server" Text="Monthly Test"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="MonthlyTest" runat="server" Text='<%# Bind("MonthlyTest") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="ut1" runat="server" Text="UT-1"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtut1" runat="server" Text='<%# Bind("UT1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="ut2" runat="server" Text="UT-2"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtut2" runat="server" Text='<%# Bind("UT2") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                          <%--       <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="ut3" runat="server" Text="UT-3"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtut3" runat="server" Text='<%# Bind("UT2") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="hy" runat="server" Text="HY"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txthy" runat="server" Text='<%# Bind("HY") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="HYt" runat="server" Text="TOTAL (150)"></asp:Label>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="hytotal" runat="server" Text='<%# Bind("HYTotal") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../../js/jquery.min.js"></script>
    <script>

        function Showhide(evel) {

            if (evel == "1") {
                $("#Term1Mark").removeClass('hide');
                $("#Term2Mark").addClass('hide');
            }
            if (evel == "2") {
                $("#Term1Mark").addClass('hide');
                $("#Term2Mark").removeClass('hide');
            }
        }
    </script>
   
</asp:Content>

