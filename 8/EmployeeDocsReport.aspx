<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmployeeDocsReport.aspx.cs" Inherits="_8.AdminStudentDocsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="ds" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 mgbt-xs-20">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">

                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Department&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="txtDepartmentName" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Designation&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                                <asp:DropDownList ID="drpdes" runat="server" CssClass="form-control-blue"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Doc Type&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpDocType" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem>Both</asp:ListItem>
                                                        <asp:ListItem>Only Hardcopy</asp:ListItem>
                                                        <asp:ListItem>Only Softcopy</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6  mgbt-xs-15">
                                        <%-- <label class="control-label">&nbsp;<span class="vd_red"></span></label>--%>

                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return ValidateDropdown('.valiadte');"
                                            CssClass="button" OnClick="LinkButton1_Click">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 63px"></div>

                                    </div>


                                </div>




                                <div class="col-sm-12  mgbt-xs-5 no-padding">

                                    <div style="float: right; font-size: 19px;">

                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                        title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                        title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                        title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                        title="Print" ><i class="fa fa-print "></i></asp:LinkButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="ImageButton1" />
                                                    <asp:PostBackTrigger ControlID="ImageButton2" />
                                                    <asp:PostBackTrigger ControlID="ImageButton3" />
                                                    <asp:PostBackTrigger ControlID="ImageButton4" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </asp:Panel>

                                    </div>
                                </div>

                                <div class="col-sm-12  " id="divExport" runat="server" visible="false">
                                    <div class=" table-responsive  table-responsive2">
                                        <table runat="server" id="abc" style="width:100%;">
                                            <tr>
                                                <td class="text-center">
                                                    <div id="header1" runat="server" style="width: 100%"></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-text-left table-header-group"
                                                        OnRowDataBound="GridView1_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblid" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Left" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

