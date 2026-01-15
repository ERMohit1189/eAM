<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="BulkPromotion.aspx.cs" Inherits="BulkPromotion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12  ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding ">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Current Session&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList runat="server" ID="drpSession" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpFeeGroup" runat="server" CssClass="form-control-blue validatedrp">
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
                                                    <asp:DropDownList ID="DrpClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged"
                                                        CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropBranch" runat="server"
                                                        CssClass="form-control-blue validatedrp" OnSelectedIndexChanged="DropBranch_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue validatedrp">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                       <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Medium</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Group</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpStream" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Education Act</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Gender</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Value="-1" Selected="True">All</asp:ListItem>
                                                        <asp:ListItem Value="Male">Male</asp:ListItem>
                                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                                        <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Display Order</label>
                                    <div class="controls">
                                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                            <asp:ListItem Value="Name"  Selected="True">Alphabetical</asp:ListItem>
                                            <asp:ListItem Value="Id">Sequential</asp:ListItem>
                                            <asp:ListItem Value="InstituteRollNo">Roll No. Wise</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2  mgbt-xs-15">
                                        <div class="pull-left">
                                            <asp:LinkButton runat="server" ID="LinkShow" OnClick="LinkShow_Click" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>

                                            <div id="msgbox" runat="server" style="left: 64px"></div>
                                        </div>
                                        <div class="pull-left btn-txt-side">
                                            <asp:Label ID="lblRedIndicate" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lblNoRecord" runat="server" ForeColor="Red"></asp:Label>

                                        </div>
                                    </div>

                                </div>

                                <div class="col-sm-12" id="div_grid" runat="server" visible="false">
                                    <div class=" table-responsive  table-responsive2">
                                        <table id="tablemain" class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                            <tr>
                                                <th class="text-center">#</th>
                                                <th class="text-left">S.R. No.</th>
                                                <th class="text-left">Student's Name</th>
                                                <th class="text-left">Father's Name</th>
                                                <th class="text-center">Class</th>
                                                <th class="text-center">Education Act</th>
                                                <th class="text-center">Promotion<br />
                                                    <asp:DropDownList ID="drpstatusAll" runat="server"  onchange="changeStatus(this)">
                                                        <asp:ListItem Value="Pass">Pass</asp:ListItem>
                                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                    </asp:DropDownList>
                                                </th>
                                                <th class="text-right">Total Arrear</th>
                                            </tr>
                                            <asp:Repeater runat="server" ID="mainGrid">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-center">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSrNO" runat="server" Text='<%# Bind("SrNO") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFatherName" runat="server" Text='<%# Bind("FatherName") %>'></asp:Label>
                                                        </td>
                                                        <td class="text-center">
                                                            <asp:Label ID="lblClass" runat="server" Text='<%# Bind("CombineClassName") %>'></asp:Label>
                                                        </td>
                                                         <td class="text-center">
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Actname") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drpstatus" runat="server" CssClass="form-control-blue">
                                                                <asp:ListItem Value="Pass">Pass</asp:ListItem>
                                                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label ID="lblTotalArrear" runat="server" style="text-align:right;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater runat="server" ID="childGrid" Visible="false">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <asp:Label ID="lblHeadId" runat="server" Text='<%# Bind("FeeheadId") %>'></asp:Label>
                                                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <th colspan="7" style="text-align:right;">Total</th>
                                                <th style="text-align:right;">
                                                            <asp:Label ID="lblFTotal" runat="server" style="text-align:right;"></asp:Label>
                                                </th>
                                            </tr>
                                        </table>

                                    </div>
                                </div>
                                <div class="col-sm-12  no-padding" id="Panel2" runat="server">
                                    <div class="col-sm-12  ">
                                        <h4 class="form-heading">Promote To</h4>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Next Session&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSessionNew" runat="server" Enabled="False" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Course&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropCourseNew" runat="server" CssClass="form-control-blue validatedrpS" AutoPostBack="true" OnSelectedIndexChanged="DropCourseNew_SelectedIndexChanged">
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
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpClassNew" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpClassNew_SelectedIndexChanged"
                                                        CssClass="form-control-blue validatedrpS">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DrpBranchNew" runat="server"
                                                        CssClass="form-control-blue validatedrpS" AutoPostBack="true" OnSelectedIndexChanged="DrpBranchNew_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpSectionNew" runat="server"
                                                        CssClass="form-control-blue validatedrpS">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Medium&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropMediumNew" runat="server" CssClass="form-control-blue validatedrpS">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Group</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropStreamNew" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Board/University&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropBoardNew" runat="server"
                                                        CssClass="form-control-blue validatedrpS">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">House&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropHouseNew" runat="server"
                                                        CssClass="form-control-blue validatedrpS">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>--%>
                                      <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Education Act</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropFeeGroup" runat="server"
                                                        CssClass="form-control-blue validatedrpS">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6 mgbt-xs-15" style="padding-top: 24px;">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="LinkSubmit" runat="server" OnClientClick="ValidateTextBox('.validatetxtS');ValidateDropdown('.validatedrpS');return validationReturn();" OnClick="LinkSubmit_Click" CssClass="button form-control-blue">Promote</asp:LinkButton>
                                                <div id="msgbox1" runat="server" style="left: 75px"></div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script>
                function changeStatus(tis)
                {
                    var len = $("#tablemain tbody tr").length;
                    for (var i = 1; i < len; i++) {
                        if (!$(tis).closest('tbody').find('tr:eq(' + i + ') td:eq(6) select').hasClass('aspNetDisabled'))
                        {
                            $(tis).closest('tbody').find('tr:eq(' + i + ') td:eq(6) select').val($(tis).val());
                        }
                    }
                }
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
