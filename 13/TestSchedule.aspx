<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="TestSchedule.aspx.cs"
    Inherits="TestSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <%-- ==== in aspx file   --%>
    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        // ReSharper disable once UnusedParameter
        function EndRequestHandler(sender, args) {
            scrollTo(0, 0);
        }
    </script>
    <style>
        .table tr th, td input[type=text] {
            text-transform: uppercase;
        }
    </style>
    <script>
        function changeOptionType(tis) {
            str = $(tis).val();
            var counts = $("[id*=Grd] tbody tr").length;
            for (var i = 1; i < counts; i++) {
                $("[id*=Grd] tbody tr:eq(" + i + ") td:eq(8)").find("select").val(str);
            }
        }
        function changeOptionAll(tis, col) {
            str = $(tis).val().replace(/\s/g, '');
            $(tis).val(str.toUpperCase());
            var counts = $("[id*=Grd] tbody tr").length;
            for (var i = 1; i < counts; i++) {
                $("[id*=Grd] tbody tr:eq(" + i + ") td:eq(" + col + ")").find("input[type=text]").val(str);
            }
        }

        function changeOption(tis) {
            str = $(tis).val().replace(/\s/g, '');
            $(tis).val(str.toUpperCase());
        }


    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
                function timeClick() {
                    Sys.Application.add_load(time);
                }
            </script>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3 no-padding">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-9  half-width-50  btn-a-devices-3-p6 mgbt-xs-15" style="padding-top:25px;">
                                        <asp:LinkButton ID="btnView" runat="server" OnClick="btnView_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 75px;"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12   no-padding" id="listdisplay" runat="server" visible="false">
                                            <div class="col-sm-12  no-padding">
                                                <div style="float: right; font-size: 19px;">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                                title="Export to Word"  Visible="false"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                                title="Export to Excel"  Visible="false"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                                title="Export to PDF"  Visible="false"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                            <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                                title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="col-sm-12   no-padding" id="gdv" runat="server">
                                                <div runat="server" id="header1"></div>
                                                <div class=" table-responsive  table-responsive2 text-center" id="abc" runat="server">
                                                    <asp:Label ID="title" runat="server" style="font-weight:bold; text-align:center;"></asp:Label>
                                                    <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                                        <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Term">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="TermName" runat="server" Text='<%# Bind("TermName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="130" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Subject">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Subject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="130" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Paper">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Paper" runat="server" Text='<%# Bind("Paper") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="130" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Test">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ExamName" runat="server" Text='<%# Bind("ExamName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="290" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Test Start From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ExamStart" runat="server" Text='<%# Bind("ExamStart") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="170" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ExamEnd" runat="server" Text='<%# Bind("ExamEnd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="170" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Show Result From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ResultShow" runat="server" Text='<%# Bind("ResultShow") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="170" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ResultHide" runat="server" Text='<%# Bind("ResultHide") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="170" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                    
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
