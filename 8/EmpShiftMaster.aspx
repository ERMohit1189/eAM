<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="EmpShiftMaster.aspx.cs" Inherits="_8.AdminEmpShiftMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Timing Group Master</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div runat="server" id="loader"></div>
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label16" runat="server" class="control-label" Text="Timing From"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlFromHour" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFromHour_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromMinute" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlFromMinute_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromSecond" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlFromSecond_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromType" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlFromType_SelectedIndexChanged">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label1" runat="server" class="control-label" Text="Timing To"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlToHour" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlToHour_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlToMinute" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlToMinute_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlToSecond" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlToSecond_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlToType" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlToType_SelectedIndexChanged">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label2" runat="server" class="control-label" Text="Shift Duration"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtShiftTime" ReadOnly="true" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="ShiftTime" runat="server" class="control-label" Text="Shift Name"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtShiftName" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <%--<div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label3" runat="server" class="control-label" Text="Lunch From Time"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlLunchFromH" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchFromH_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlLunchFromM" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchFromM_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlLunchFromS" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchFromS_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlLunchFromT" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchFromT_SelectedIndexChanged">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="lbl4" runat="server" class="control-label" Text="Lunch To Time"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlLunchToH" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchToH_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlLunchToM" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchToM_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlLunchToS" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchToS_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlLunchToT" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchToT_SelectedIndexChanged">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Grace Time IN</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlGraceTimeInH" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlGraceTimeInM" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlGraceTimeInS" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Grace Time OUT</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlGraceTimeOutH" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlGraceTimeOutM" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlGraceTimeOutS" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="row">
                                        <div class="col-sm-4 half-width-50 mgbt-xs-15">
                                            <label class="control-label">SL After</label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlSLTimeH" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" runat="server">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSLTimeM" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSLTimeS" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSLTimeT" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3">
                                                    <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                    <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">SL Before Out</label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlSLTimeHO" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" runat="server">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSLTimeMO" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSLTimeSO" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSLTimeTO" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3">
                                                    <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                    <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                            <label class="control-label">HD After</label>
                                            <div class="">
                                                <asp:DropDownList ID="ddlHDTimeH" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3" runat="server">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlHDTimeM" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlHDTimeS" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlHDTimeT" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-3 col-xs-3">
                                                    <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                    <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="text-box-msg">
                                                </div>
                                            </div>
                                        </div>
                                            </div>
                                    </div>


                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <asp:Label ID="Label56" runat="server" class="control-label" Text="Shift Category"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlDesignation" EnableTheming="false" CssClass="form-control-blue " runat="server">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:Button ID="btnInsert" runat="server" CssClass="button form-control-blue " Text="Submit" OnClick="btnInsert_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnReset" runat="server" CssClass="button form-control-blue " Text="Reset" OnClick="btnReset_Click" />
                                    </div>

                                </div>


                                <div class="col-sm-12  ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="gvShiftMaster" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <Columns>

                                                <asp:BoundField DataField="SrNo" HeaderText="#" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" HeaderStyle-Width="50px" />
                                                <asp:BoundField DataField="ShiftName" HeaderText="Shift Name" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField DataField="ShiftTime" HeaderText="Shift Duration" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField DataField="FromTime" HeaderText="From Time" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField DataField="ToTime" HeaderText="ToTime" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />

                                                <asp:BoundField DataField="DesName" HeaderText="Shift Category" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                <%--<asp:BoundField DataField="Lunch" HeaderText="Lunch" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />--%>
                                                <asp:BoundField DataField="GraceTime" HeaderText="Grace Time IN" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />

                                                <asp:BoundField DataField="GraceTimeOut" HeaderText="Grace Time OUT" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField DataField="SLTime" HeaderText="SL After" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField DataField="SLTimeO" HeaderText="SL Before Out" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField DataField="HDTime" HeaderText="HD After" HeaderStyle-CssClass="vd_bg-blue vd_white" ItemStyle-CssClass="text-center" />

                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FromHour" Visible="false" runat="server" Text='<%# Eval("FromHour") %>'></asp:Label>
                                                        <asp:Label ID="FromMinute" Visible="false" runat="server" Text='<%# Eval("FromMinute") %>'></asp:Label>
                                                        <asp:Label ID="FromSecond" Visible="false" runat="server" Text='<%# Eval("FromSecond") %>'></asp:Label>
                                                        <asp:Label ID="FromType" runat="server" Visible="false" Text='<%# Eval("FromType") %>'></asp:Label>
                                                        <asp:Label ID="ToHour" Visible="false" runat="server" Text='<%# Eval("ToHour") %>'></asp:Label>
                                                        <asp:Label ID="ToMinute" Visible="false" runat="server" Text='<%# Eval("ToMinute") %>'></asp:Label>
                                                        <asp:Label ID="ToSecond" Visible="false" runat="server" Text='<%# Eval("ToSecond") %>'></asp:Label>
                                                        <asp:Label ID="ToType" runat="server" Visible="false" Text='<%# Eval("ToType") %>'></asp:Label>
                                                        <asp:Label ID="A02ID" runat="server" Visible="false" Text='<%# Eval("A02ID") %>'></asp:Label>


                                                        <%--<asp:Label ID="lblLunchFromH" Visible="false" runat="server" Text='<%# Eval("LunchFromHour") %>'></asp:Label>
                                                        <asp:Label ID="lblLunchFromM" Visible="false" runat="server" Text='<%# Eval("LunchFromMinute") %>'></asp:Label>
                                                        <asp:Label ID="lblLunchFromT" runat="server" Visible="false" Text='<%# Eval("LunchFromType") %>'></asp:Label>

                                                        <asp:Label ID="lblLunchToH" Visible="false" runat="server" Text='<%# Eval("LunchToHour") %>'></asp:Label>
                                                        <asp:Label ID="lblLunchToM" Visible="false" runat="server" Text='<%# Eval("LunchToMinute") %>'></asp:Label>
                                                        <asp:Label ID="lblLunchToT" runat="server" Visible="false" Text='<%# Eval("LunchToType") %>'></asp:Label>--%>

                                                        <asp:Label ID="lblGraceTimeH" Visible="false" runat="server" Text='<%# Eval("GraceToHour") %>'></asp:Label>
                                                        <asp:Label ID="lblGraceTimeM" runat="server" Visible="false" Text='<%# Eval("GraceToMinute") %>'></asp:Label>
                                                        <asp:Label ID="lblGraceTimeS" runat="server" Visible="false" Text='<%# Eval("GraceToSecond") %>'></asp:Label>

                                                        <asp:Label ID="lblGraceTimeOutH" Visible="false" runat="server" Text='<%# Eval("GraceToHourOut") %>'></asp:Label>
                                                        <asp:Label ID="lblGraceTimeOutM" runat="server" Visible="false" Text='<%# Eval("GraceToMinuteOut") %>'></asp:Label>
                                                        <asp:Label ID="lblGraceTimeOutS" runat="server" Visible="false" Text='<%# Eval("GraceToSecondOut") %>'></asp:Label>

                                                        <asp:Label ID="Label4" Visible="false" runat="server" Text='<%# Eval("GraceToHour") %>'></asp:Label>
                                                        <asp:Label ID="Label6" runat="server" Visible="false" Text='<%# Eval("GraceToMinute") %>'></asp:Label>
                                                        <asp:Label ID="Label8" runat="server" Visible="false" Text='<%# Eval("GraceToSecond") %>'></asp:Label>

                                                        <asp:Label ID="lblDesID" Visible="false" runat="server" Text='<%# Eval("EmpDegId") %>'></asp:Label>


                                                        <asp:Label ID="lblSLTimeH" Visible="false" runat="server" Text='<%# Eval("SLTime_H") %>'></asp:Label>
                                                        <asp:Label ID="lblSLTimeM" Visible="false" runat="server" Text='<%# Eval("SLTime_M") %>'></asp:Label>
                                                        <asp:Label ID="lblSLTimeS" Visible="false" runat="server" Text='<%# Eval("SLTime_S") %>'></asp:Label>
                                                        <asp:Label ID="lblSLTimeT" Visible="false" runat="server" Text='<%# Eval("SLTime_T") %>'></asp:Label>

                                                        <asp:Label ID="lblSLTimeHO" Visible="false" runat="server" Text='<%# Eval("SLTime_HO") %>'></asp:Label>
                                                        <asp:Label ID="lblSLTimeMO" Visible="false" runat="server" Text='<%# Eval("SLTime_MO") %>'></asp:Label>
                                                        <asp:Label ID="lblSLTimeSO" Visible="false" runat="server" Text='<%# Eval("SLTime_SO") %>'></asp:Label>
                                                        <asp:Label ID="lblSLTimeTO" Visible="false" runat="server" Text='<%# Eval("SLTime_TO") %>'></asp:Label>


                                                        <asp:Label ID="lblHDTimeH" Visible="false" runat="server" Text='<%# Eval("HDTime_H") %>'></asp:Label>
                                                        <asp:Label ID="lblHDTimeM" Visible="false" runat="server" Text='<%# Eval("HDTime_M") %>'></asp:Label>
                                                        <asp:Label ID="lblHDTimeS" Visible="false" runat="server" Text='<%# Eval("HDTime_S") %>'></asp:Label>
                                                        <asp:Label ID="lblHDTimeT" Visible="false" runat="server" Text='<%# Eval("HDTime_T") %>'></asp:Label>

                                                        <%--<asp:LinkButton ID="lbtnEdit" runat="server" Text='<%# Eval("T01ID") %>' CssClass="edit" Font-Size="0pt" Height="16px" Width="16px" OnClick="lbtnEdit_Click"></asp:LinkButton>--%>
                                                        <%--<asp:Label ID="Label36" runat="server" Text='<%# Eval("T01ID") %>' Visible="false"></asp:Label>--%>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" title="Edit" OnClick="lbtnEdit_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lbtnDelete" runat="server" Text='<%# Eval("T01ID") %>' CssClass="delete" Font-Size="0pt" Height="16px" Width="16px" OnClick="lbtnDelete_Click"></asp:LinkButton>--%>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Eval("A02ID") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" title="Delete" class="btn menu-icon vd_bd-red vd_red">
                                                            <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>



                                <div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                        <table class="tab-popup">
                                            <div id="tblInsert0" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label160" runat="server" Text="Timing From"></asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFromHour0" EnableTheming="false" CssClass="form-control-blue col-sm-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFromHour0_SelectedIndexChanged">
                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddlFromMinute0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlFromMinute0_SelectedIndexChanged">
                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddlFromSecond0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlFromSecond0_SelectedIndexChanged">
                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddlFromType0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlFromType0_SelectedIndexChanged">
                                                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                            <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" Text="Timing To"></asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlToHour0" EnableTheming="false" CssClass="form-control-blue col-sm-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlToHour0_SelectedIndexChanged">
                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddlToMinute0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlToMinute0_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlToSecond0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlToSecond0_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlToType0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlToType0_SelectedIndexChanged">
                                                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                            <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>

                                                <%--<tr>
                                                    <td>
                                                        <asp:Label ID="Label30" runat="server" Text="Lunch From Time"></asp:Label></td>

                                                    <td>
                                                        <asp:DropDownList ID="ddlLunchFromH0" EnableTheming="false" CssClass="form-control-blue col-sm-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchFromH0_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlLunchFromM0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchFromM0_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlLunchFromS0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchFromS0_SelectedIndexChanged">
</asp:DropDownList>
                                                        <asp:DropDownList ID="ddlLunchFromT0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchFromT0_SelectedIndexChanged">
                                                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                            <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl40" runat="server" Text="Lunch To Time"></asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLunchToH0" EnableTheming="false" CssClass="form-control-blue col-sm-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchToH0_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlLunchToM0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchToM0_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlLunchToS0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchToS0_SelectedIndexChanged">
</asp:DropDownList>
                                                        <asp:DropDownList ID="ddlLunchToT0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchToT0_SelectedIndexChanged">
                                                            <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                            <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label20" runat="server" Text="Shift Duration"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtShiftTime0" ReadOnly="true" CssClass="form-control-blue" runat="server"></asp:TextBox></td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="ShiftTime0" runat="server" Text="Shift Name"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtShiftName0" CssClass="form-control-blue" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Grace Time</label></td>
                                                    <td>
                                                        <div class="">
                                                            <asp:DropDownList ID="ddlGraceTimeH0" EnableTheming="false" CssClass="form-control-blue col-xs-4" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlGraceTimeM0" runat="server" EnableTheming="false" CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlGraceTimeS0" runat="server" EnableTheming="false" CssClass="form-control-blue col-xs-4">
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Grace Time OUT</td>
                                                    <td>
                                                        <div class="">
                                                            <asp:DropDownList ID="ddlGraceTimeOutH0" EnableTheming="false" CssClass="form-control-blue col-sm-4 col-xs-3" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlGraceTimeOutM0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4 col-xs-3">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlGraceTimeOutS0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4 col-xs-3">
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>SL After</td>
                                                    <td>
                                                        <div class="">
                                                            <asp:DropDownList ID="ddlSLTimeH0" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSLTimeM0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSLTimeS0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSLTimeT0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3">
                                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>SL Before OUT</td>
                                                    <td>
                                                        <div class="">
                                                            <asp:DropDownList ID="ddlSLTimeHO0" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSLTimeMO0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSLTimeSO0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSLTimeTO0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3">
                                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>HD After</td>
                                                    <td>
                                                        <div class="">
                                                            <asp:DropDownList ID="ddlHDTimeH0" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlHDTimeM0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlHDTimeS0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlHDTimeT0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-3 col-xs-3">
                                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="Shift Category"></asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDesignation0" EnableTheming="false" runat="server">
                                                        </asp:DropDownList></td>

                                                </tr>
                                            </div>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="button-y" Text="Update" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btnClose" runat="server" CssClass="button-n" Text="Close" OnClick="btnClose_Click" />
                                                </td>
                                            </tr>
                                        </table>


                                        <asp:Button ID="Button5" runat="server" Style="display: none" />
                                        <%-- ReSharper disable once Asp.InvalidControlType --%>
                                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                                            CancelControlID="btnClose" BackgroundCssClass="popup_bg">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </asp:Panel>
                                </div>

                                <%--<div style="overflow: auto; width: 1px; height: 1px">
                                    <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                                        <div class="col-sm-12  no-padding " id="tblInsert0" runat="server">
                                            <div class="col-sm-12 no-padding ">
                                                <div class="col-sm-6 no-padding ">
                                                    <div class="form-group ">
                                                        <asp:Label ID="Label160" runat="server" class="control-label" Text="Timing From"></asp:Label>
                                                        <div class="col-sm-8 controls mgbt-xs-15">
                                                            <asp:DropDownList ID="ddlFromHour0" EnableTheming="false" CssClass="form-control-blue col-sm-4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFromHour0_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlFromMinute0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4" AutoPostBack="True" OnSelectedIndexChanged="ddlFromMinute0_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlFromType0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4" AutoPostBack="True" OnSelectedIndexChanged="ddlFromType0_SelectedIndexChanged">
                                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 no-padding ">
                                                    <div class="form-group ">
                                                        <asp:Label ID="Label10" runat="server" class="control-label" Text="Timing To"></asp:Label>
                                                        <div class="col-sm-8 controls mgbt-xs-15">
                                                            <asp:DropDownList ID="ddlToHour0" EnableTheming="false" CssClass="form-control-blue col-sm-4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlToHour0_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlToMinute0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4" AutoPostBack="True" OnSelectedIndexChanged="ddlToMinute0_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlToType0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4" AutoPostBack="True" OnSelectedIndexChanged="ddlToType0_SelectedIndexChanged">
                                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 no-padding ">
                                                <div class="col-sm-6 no-padding ">
                                                    <div class="form-group ">
                                                        <asp:Label ID="Label30" runat="server" class="control-label" Text="Lunch From Time"></asp:Label>
                                                        <div class="col-sm-8 controls mgbt-xs-15">
                                                            <asp:DropDownList ID="ddlLunchFromH0" EnableTheming="false" CssClass="form-control-blue col-sm-4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchFromH0_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlLunchFromM0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchFromM0_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlLunchFromT0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchFromT0_SelectedIndexChanged">
                                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 no-padding ">
                                                    <div class="form-group ">
                                                        <asp:Label ID="lbl40" runat="server" class="control-label" Text="Lunch To Time"></asp:Label>
                                                        <div class="col-sm-8 controls mgbt-xs-15">
                                                            <asp:DropDownList ID="ddlLunchToH0" EnableTheming="false" CssClass="form-control-blue col-sm-4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchToH0_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlLunchToM0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchToM0_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlLunchToT0" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4" AutoPostBack="True" OnSelectedIndexChanged="ddlLunchToT0_SelectedIndexChanged">
                                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 no-padding ">
                                                <div class="col-sm-6 no-padding ">
                                                    <div class="form-group ">
                                                        <asp:Label ID="Label20" runat="server" class="control-label" Text="Shift Duration"></asp:Label>
                                                        <div class="col-sm-8 controls mgbt-xs-15">
                                                            <asp:TextBox ID="txtShiftTime0" ReadOnly="true" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 no-padding ">
                                                    <div class="form-group ">
                                                        <asp:Label ID="ShiftTime0" runat="server" class="control-label" Text="Shift Name"></asp:Label>
                                                        <div class="col-sm-8 controls mgbt-xs-15">
                                                            <asp:TextBox ID="txtShiftName0" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 no-padding ">
                                                <div class="col-sm-6 no-padding ">
                                                    <div class="form-group ">
                                                        <label class="control-label">Grace Time</label>
                                                        <div class="">
                                                            <asp:DropDownList ID="ddlGraceTimeH0" EnableTheming="false" CssClass="form-control-blue col-xs-6" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlGraceTimeM0" runat="server" EnableTheming="false" CssClass="form-control-blue col-xs-6">
                                                            </asp:DropDownList>
                                                            <div class="text-box-msg">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 no-padding ">
                                                    <div class="form-group ">
                                                        <asp:Label ID="Label5" runat="server" class="control-label" Text="Designation"></asp:Label>
                                                        <div class="col-sm-8 controls mgbt-xs-15">
                                                            <asp:DropDownList ID="ddlDesignation0" EnableTheming="false" CssClass="form-control-blue col-sm-4" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center mgbt-xs-15">
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
                                        </div>
                                    </asp:Panel>

                                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                                    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                                        CancelControlID="btnClose" BackgroundCssClass="popup_bg">
                                    </ajaxToolkit:ModalPopupExtender>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="overflow: hidden; width: 1px; height: 1px">
                <asp:Panel runat="server" ID="pnlDelete" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td style="text-align: center" height="50">
                                <h4>Are you sure you want to delete this?
                                    <asp:Label ID="lblValue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="btnNone" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnNo" runat="server" CausesValidation="False" CssClass="button-n" Text="No" OnClick="btnNo_Click" />
                                &nbsp;&nbsp;
                                                    <asp:Button ID="btnYes" runat="server" CausesValidation="False" CssClass="button-y" Text="Yes" OnClick="btnYes_Click" />

                            </td>
                        </tr>
                    </table>
                    <%-- ReSharper disable once Asp.InvalidControlType --%>
                    <asp:ModalPopupExtender ID="mpeDelete" runat="server" CancelControlID="btnNo"
                        Enabled="True" PopupControlID="pnlDelete" TargetControlID="btnNone" BackgroundCssClass="popup_bg">
                    </asp:ModalPopupExtender>
                </asp:Panel>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>





