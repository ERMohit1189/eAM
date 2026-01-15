<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="AttendanceReport.aspx.cs" Inherits="admin_AttendanceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <title>Attendance Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="upMaun" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <asp:HiddenField ID="hidSQL" runat="server" />
                                   <div class="col-sm-12  half-width-50 mgbt-xs-15">
                                <asp:RadioButtonList ID="rptType" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow" AutoPostBack="true" OnSelectedIndexChanged="rptType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Class wise Attendance</asp:ListItem>
                                    <asp:ListItem>Batch wise Attendance</asp:ListItem>
                                </asp:RadioButtonList>
                                    </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Date</label>
                                    <div class="">
                                        <asp:TextBox ID="txtFromDate" placeholder="" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15" id="pnl2" runat="server" visible="false">
                                    <label class="control-label">To Date</label>
                                    <div class="">
                                        <asp:TextBox ID="txtToDate" placeholder="" runat="server" CssClass="form-control-blue datepicker-normal"></asp:TextBox>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                    <label class="control-label">Attendance</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlAttendance" AutoPostBack="true" OnSelectedIndexChanged="ddlAttendance_SelectedIndexChanged" runat="server" CssClass="form-control-blue validatedrp">
                                            <asp:ListItem Text="--All--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Present" Value="P"></asp:ListItem>
                                            <asp:ListItem Text="Not Mark" Value="N"></asp:ListItem>
                                            <asp:ListItem Text="Absent" Value="A"></asp:ListItem>
                                            <asp:ListItem Text="Late" Value="Lt"></asp:ListItem>
                                            <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divClass" visible="false">
                                    <label class="control-label">Class</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlClass" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divBatch" visible="false">
                                    <label class="control-label">Batch</label>
                                    <div class="">
                                        <asp:DropDownList ID="DropDownList1" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" runat="server" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divBranch" visible="false">
                                    <label class="control-label">Stream</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlBranch" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divSection" visible="false">
                                    <label class="control-label">Section</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control-blue validatedrp"></asp:DropDownList>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4  half-width-50 mgbt-xs-15" id="pnlHide1" runat="server" visible="false">
                                    <label class="control-label">Sr No.</label>
                                    <div class="">
                                        <asp:TextBox ID="txtSrNo" placeholder="" runat="server" CssClass="form-control-blue validatetxt" ToolTip="Nationality"></asp:TextBox>
                                        <div class="text-box-msg">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15" style="display: none;">
                                    <asp:LinkButton ID="lbtnSearch" Visible="false" OnClick="lbtnSearch_Click" ValidationGroup="A" CssClass="btn vd_btn vd_bg-blue" runat="server">  <i class="fa fa-paper-plane"></i> Search </asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 59px"></div>
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


                                <div class="col-sm-12  " id="divExport" runat="server">
                                    <h4 class="form-heading">
                                        <asp:Label ID="lblHeading" runat="server"></asp:Label></h4>
                                    <div class="table-responsive2  table-responsive">
                                        <asp:GridView ID="gvAttendance" AutoGenerateColumns="false" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCount" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="vd_bg-blue-np vd_white-np text-left" />
                                                    <ItemStyle CssClass="text-left" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="S.R. No." DataField="SrNo" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />
                                                <asp:BoundField HeaderText="Student's Name" DataField="StudentName" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />
                                                <asp:BoundField HeaderText="Father's Name" DataField="FatherName" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-left" ItemStyle-CssClass="text-left" />

                                                <asp:BoundField HeaderText="Class" DataField="ClassName" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-center" ItemStyle-CssClass="text-center" />
                                               
                                                <asp:BoundField HeaderText="Machine No." DataField="CardNo" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-center" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField HeaderText="Machine ID" DataField="MachineNo" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-center" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField HeaderText="Shift" DataField="ShiftName" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-center" ItemStyle-CssClass="text-center" />
                                                
                                                <asp:BoundField HeaderText="Attendance" DataField="AttendanceValue" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-center" ItemStyle-CssClass="text-center" />

                                                <asp:BoundField HeaderText="In" DataField="Intime" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-center" ItemStyle-CssClass="text-center" />

                                                <asp:BoundField HeaderText="Out" DataField="Outtime" HeaderStyle-CssClass="vd_bg-blue-np vd_white-np text-center" ItemStyle-CssClass="text-center" />

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

