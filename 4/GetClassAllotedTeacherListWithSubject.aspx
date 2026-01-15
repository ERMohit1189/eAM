<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="GetClassAllotedTeacherListWithSubject.aspx.cs" Inherits="admin_GetClassAllotedTeacherListWithSubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>




    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <script>
                Sys.Application.add_load(tooltip);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" OnSelectedIndexChanged="drpClass_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control-blue validatedrp">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSection" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Stream</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-3-p6  mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue">View</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px;"></div>

                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="col-sm-12  mgbt-xs-10">
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <div id="th" runat="server">
                                        <div style="float: right; font-size: 19px;" id="Panel2" runat="server">


                                            <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color" title="Export to Word" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                            <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color" title="Export to Excel" data-toggle="tooltip" data-placement="top"><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                            <%--<asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color" title="Export to PDF" data-toggle="tooltip" data-placement="top"><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>--%>
                                            <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color" title="Print" data-toggle="tooltip" data-placement="top"><i class="fa fa-print "></i></asp:LinkButton>


                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="ImageButton1" />
                                    <asp:PostBackTrigger ControlID="ImageButton2" />
                                    <%--<asp:PostBackTrigger ControlID="ImageButton3" />--%>
                                    <asp:PostBackTrigger ControlID="ImageButton4" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>




                        <div id="maindiv" runat="server" class="col-sm-12  no-padding">
                            <div class=" table-responsive  table-responsive2 " id="divExport" runat="server">
                                <table runat="server" id="abc" class="table no-bm table-nb">
                                    <tr>
                                        <td style="border: 2px solid #fff;">
                                            <div id="header" runat="server" class="col-sm-12 print-row no-padding">
                                            </div>
                                            <div class="col-lg-12 print-row no-padding">
                                                <ul class="vd_timeline">
                                                    <li class="tl-item tl-item-year">
                                                        <div class="tl-year">
                                                            <asp:Label ID="lblSessionName" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </li>
                                                    <asp:Repeater ID="Repeater1" runat="server">
                                                        <ItemTemplate>
                                                            <li class="tl-item tl-item-date">
                                                                <div class="tl-date">
                                                                    <p class="class-txt">
                                                                        <asp:Label ID="lblClass" runat="server" Text='<%# Eval("ClassSectionBranch") %>'></asp:Label>
                                                                        <asp:Label ID="lblclassId" runat="server" Text='<%# Eval("classid") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblSection" runat="server" Text='<%# Eval("SectionName") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblBranchid" runat="server" Text='<%# Eval("branchid") %>' Visible="false"></asp:Label>
                                                                    </p>
                                                                </div>
                                                            </li>
                                                            <asp:Repeater ID="Repeater2" runat="server">
                                                                <ItemTemplate>


                                                                    <li class="tl-item">
                                                                        <div class="tl-icon success">
                                                                            <p class="staff-id">
                                                                                <asp:Label ID="lblEmpid" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label>
                                                                            </p>
                                                                        </div>
                                                                        <div class="tl-label panel widget light-widget bg-color-none">

                                                                            <div class="panel-body bg-color-none">
                                                                                <div class="col-sm-6 p-tech-box pro-box-w3 panel-bd-left">
                                                                                    <img alt="example image" class="tl-img img-right img-circle  mgtp-5" src="../img/user-pic/user-pic.jpg">
                                                                                    <h3 class="mgtp-10 p-tech-name mgbt-xs-5">
                                                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                                    </h3>

                                                                                    <div class="clearfix p-tech-f-name mgbt-xs-10">
                                                                                        <asp:Label ID="lblEmpcode" runat="server" Text='<%# Eval("Ecode") %>'></asp:Label>
                                                                                        <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("FatherName") %>'></asp:Label>
                                                                                        <asp:Label ID="lblisClassTeacher" runat="server" Text='<%# Eval("isClassTeacher") %>'></asp:Label>
                                                                                    </div>

                                                                                    <div class="col-sm-12 no-padding">
                                                                                    </div>

                                                                                </div>
                                                                                <!-- comments -->
                                                                                <div class="col-sm-6 p-tech-sub  no-padding">
                                                                                    <ul class="vd_timeline">
                                                                                        <asp:Repeater ID="Repeater3" runat="server">
                                                                                            <ItemTemplate>
                                                                                                <li class="tl-item">
                                                                                                    <div class="tl-icon success">
                                                                                                        <p class="staff-id">S</p>
                                                                                                    </div>
                                                                                                    <div class="tl-label p-marg-l-12 panel widget light-widget bg-color-none">
                                                                                                        <div class="panel-body bg-color-none">
                                                                                                            <div class="col-sm-12 pro-box-w3 panel-bd-left">

                                                                                                                <h4 class="p-sub-main-t mgbt-xs-5"><i class=" fa fa-book mgr-10 "></i>
                                                                                                                    <asp:Label ID="lblSubjectName" runat="server" Text='<%# Eval("SubjectGroup") %>'></asp:Label>
                                                                                                                    <asp:Label ID="lblsubjectgroupid" runat="server" Text='<%# Eval("subjectgroupid") %>' Visible="false"></asp:Label>
                                                                                                                </h4>

                                                                                                                <div class="clearfix mgbt-xs-10"></div>

                                                                                                                <table class="table tab-bg-color no-bm p-sub-main-st">
                                                                                                                    <asp:Repeater ID="Repeater4" runat="server">
                                                                                                                        <ItemTemplate>
                                                                                                                            <tr>
                                                                                                                                <th class="text-left vd_black" style="width: 40px"><i class=" fa fa-hand-o-right "></i>
                                                                                                                                </th>
                                                                                                                                <td class="text-left ">
                                                                                                                                    <asp:Label ID="lblActivity" runat="server" Text='<%# Eval("SubjectName") %>'></asp:Label>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:Repeater>


                                                                                                                </table>


                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </li>
                                                                                            </ItemTemplate>

                                                                                        </asp:Repeater>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                            <!-- panel-body -->
                                                                        </div>
                                                                        <!-- panel -->
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                </ul>
                                            </div>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

