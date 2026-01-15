<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ShowreportcardtoparentsICSE.aspx.cs" Inherits="ShowreportcardtoparentsICSE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <script>
                
            </script>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpclass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpclass_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpsection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label"></label>
                                        <div class="">
                                            <asp:LinkButton ID="LinkView" OnClick="LinkView_Click" OnClientClick="ValidateDropdown('.validatedrp'); return validationReturn();" CssClass="btn vd_btn vd_bg-blue" runat="server">View </asp:LinkButton>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div runat="server" id="divExport" visible="false">
                                    <div class=" table-responsive  table-responsive2 ">
                                        <table class="table table-striped no-bm table-hover no-head-border table-bordered pro-table">
                                            <tbody>
                                                <tr>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 3%">#</th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 10%; text-align:left !important;">S.R. No.</th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 9%; text-align:left !important;">Student's Name</th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 9%; text-align:left !important;">Father's Name</th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 7%; text-align:left !important;">Class</th>
                                                    <th class="p-pad-3 p-tot-tit" style="width: 4%"><asp:CheckBox ID="chkAll" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" runat="server"></asp:CheckBox>Display</th>
                                                </tr>
                                                <asp:Repeater ID="rptStudents" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-center"><%# Container.ItemIndex+1  %></td>
                                                            <td>
                                                                <asp:Label ID="LblSrno" runat="server" Text='<%# Eval("srno")  %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtName" runat="server" Text='<%# Eval("Name")  %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="TextFatherName" runat="server" Text='<%# Eval("FatherName")  %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LabelClass" runat="server" Text='<%# Eval("CombineClassName")  %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align:center;">
                                                                <asp:CheckBox ID="chk" runat="server"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-sm-12  half-width-50 mgbt-xs-15 text-center">
                                    <label class="control-label"></label>
                                    <div class="">
                                        <asp:UpdatePanel runat="server" ID="dssd">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkUpdate" OnClick="LinkUpdate_Click" Visible="false" CssClass="btn vd_btn vd_bg-blue" runat="server"> Submit</asp:LinkButton>
                                                <div class="text-box-msg">
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                    <div id="msgbox" runat="server" style="left: 0;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

