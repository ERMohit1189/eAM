<%@ Page Title="" Language="C#" MasterPageFile="~/50/sadminRootManager.master" AutoEventWireup="true" CodeFile="AttendanceMaster.aspx.cs" Inherits="_1.AdminAttendanceMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <script>
                
            </script>
            <div id="loader" runat="server"></div>


            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Abbreviation&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlAbrevation" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="P">P (Present)</asp:ListItem>
                                                <asp:ListItem Value="LC">LC (Latecomers)</asp:ListItem>
                                                <asp:ListItem Value="LT">LT (Late)</asp:ListItem>
                                                <asp:ListItem Value="SL">SL (Short Leave)</asp:ListItem>
                                                <asp:ListItem Value="HD">HD (Half Day)</asp:ListItem>
                                                 <asp:ListItem Value="A">A (Absent)</asp:ListItem>
                                                <asp:ListItem Value="L">L (Leave)</asp:ListItem>
                                                <asp:ListItem Value="NAD">NAD (New Admission)</asp:ListItem>
                                                <asp:ListItem Value="ML">ML (Medical Leave)</asp:ListItem>
                                                <asp:ListItem Value="NE">NE (Not Exam)</asp:ListItem>
                                                <asp:ListItem Value="RS">RS (Resticate)</asp:ListItem>
                                                <asp:ListItem Value="NM">NM (Not Mark)</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9 hide">
                                        <label class="control-label">Valid For</label>
                                        <div class="mgtp-6">
                                            <asp:RadioButtonList ID="rblValidFor" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                <asp:ListItem Text="Student" Value="S"></asp:ListItem>
                                                <asp:ListItem Text="Employee" Value="E"></asp:ListItem>
                                                <asp:ListItem Text="Both" Value="B" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-1-p4-p2 mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="form-control-blue button">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 74px"></div>

                                    </div>

                                </div>

                                <div class="col-sm-12   ">

                                    <div class="table-responsive2  table-responsive ">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Srno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Attendance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("AttendanceName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Abbreviation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("AbbreviationName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Valid For">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValidFor" runat="server" Text='<%# Bind("ValidFor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue-np vd_white-np" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BorderStyle="None" />
                                            <RowStyle BorderStyle="None" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-sm-12 ">

                                    <ul class="vd_li" style="padding: 0 !important;">
                                        <li class="vd_green"><i class="fa  fa-hand-o-right fa-fw append-icon"></i>P &nbsp; <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>Present
                                        </li>
                                         <li class="vd_red"><i class="fa  fa-hand-o-right append-icon "></i>A &nbsp; <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>Absent
                                        </li>
                                         <li class="vd_yellow"><i class="fa  fa-hand-o-right fa-fw append-icon "></i>L &nbsp; <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>Leave
                                        </li>
                                        <li class="vd_blue"><i class="fa  fa-hand-o-right fa-fw append-icon "></i>LT <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>Late
                                        </li>
                                        <li class="vd_blue"><i class="fa  fa-hand-o-right fa-fw append-icon"></i>LC <i class="fa  fa-long-arrow-right fa-fw append-icon "></i>Latecomers (In this Textbox will be enabled.)
                                        </li>
                                        
                                    </ul>



                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" CssClass="button-n" OnClick="Button8_Click" Text="No" />
                                &nbsp;&nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" CssClass="button-y" CausesValidation="False" OnClick="btnDelete_Click" Text="Yes" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>

            
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

