<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ShiftForStudent.aspx.cs" Inherits="_1.ShiftForStudent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div runat="server" id="loader"></div>
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <style>
                input[type=checkbox] {
                    width: 15px !important;
                    height: 15px !important;
                    margin: 0px 8px !important;
                }
            </style>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <asp:Label ID="ShiftTime" runat="server" class="control-label" Text="Shift Name"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtShiftName" CssClass="form-control-blue validatetxt" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <asp:Label ID="Label16" runat="server" class="control-label" Text="In"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlFromHour" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-4 col-xs-4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFromHour_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromMinute" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-4 col-xs-4" AutoPostBack="True" OnSelectedIndexChanged="ddlFromMinute_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlFromType" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4 col-xs-4" AutoPostBack="True" OnSelectedIndexChanged="ddlFromType_SelectedIndexChanged">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <asp:Label ID="Label1" runat="server" class="control-label" Text="Out"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlToHour" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-4 col-xs-4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlToHour_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlToMinute" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-4 col-xs-4" AutoPostBack="True" OnSelectedIndexChanged="ddlToMinute_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlToType" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4 col-xs-4" AutoPostBack="True" OnSelectedIndexChanged="ddlToType_SelectedIndexChanged">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <asp:Label ID="Label2" runat="server" class="control-label" Text="Shift Duration"></asp:Label>
                                        <div class="">
                                            <asp:TextBox ID="txtShiftTime" ReadOnly="true" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <label class="control-label">Grace Time IN</label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlGraceTimeInH" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-6 col-xs-6" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlGraceTimeInM" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-6 col-xs-6">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <asp:Label ID="Label6" runat="server" class="control-label" Text="Absent On"></asp:Label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlAbsentOnH" EnableTheming="false" AutoPostBack="true"
                                                CssClass="form-control-blue validatedrp col-sm-4 col-xs-4" runat="server" OnSelectedIndexChanged="ddlAbsentOnH_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlAbsentOnM" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlAbsentOnType" runat="server" AutoPostBack="true"
                                                EnableTheming="false" CssClass="form-control-blue col-sm-4 col-xs-4" OnSelectedIndexChanged="ddlAbsentOnType_SelectedIndexChanged">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15">
                                        <asp:Label ID="Label4" runat="server" class="control-label" Text="Send SMS In"></asp:Label>
                                        <asp:CheckBox ID="chkSendSMSIn" runat="server" AutoPostBack="true" OnCheckedChanged="chkSendSMSIn_CheckedChanged"></asp:CheckBox>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSendSMSInH" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlSendSMSInM" runat="server" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlSendSMSInType" runat="server" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 mgbt-xs-15" runat="server" visible="false">
                                        <asp:Label ID="Label5" runat="server" class="control-label" Text="Send SMS Out"></asp:Label>
                                        <asp:CheckBox ID="chkSendSMSOut" runat="server" AutoPostBack="true" OnCheckedChanged="chkSendSMSOut_CheckedChanged"></asp:CheckBox>
                                        <div class="">
                                            <asp:DropDownList ID="ddlSendSMSOutH" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlSendSMSOutM" runat="server" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlSendSMSOutType" runat="server" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  btn-a-devices-2-p2 mgbt-xs-15">
                                        <asp:Button ID="btnInsert" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue " Text="Submit" OnClick="btnInsert_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnReset" runat="server" CssClass="button form-control-blue " Text="Cancel" OnClick="btnReset_Click" />
                                        <div id="msgbox" runat="server" style="left: 150px"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12  ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="gvShiftMaster" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Shift Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftName" runat="server" Text='<%# Eval("ShiftName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="In">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFTime" runat="server" Text='<%# Eval("ShiftIn") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Out">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblToTime" runat="server" Text='<%# Eval("ShiftOut") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Shift Duration">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTime" runat="server" Text='<%# Eval("ShiftDuration") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grace Time (Min.)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGraceTime" runat="server" Text='<%# Eval("GraceIn") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Absent On">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAbsentOn" runat="server" Text='<%# Eval("AbsenOn") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Send SMS In/ Enable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSendSMSIn" runat="server" Text='<%# Eval("SendSMSIn") %>'></asp:Label>/ 
                                                        <asp:Label ID="EnableIn" runat="server" Text='<%# Eval("EnableIn") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Send SMS Out/ Enable" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSendSMSOut" runat="server" Text='<%# Eval("SendSMSOut") %>'></asp:Label>/ 
                                                        <asp:Label ID="EnableOut" runat="server" Text='<%# Eval("EnableOut") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkButton2_Click" CausesValidation="False" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftId" runat="server" CssClass="hide" Text='<%# Eval("Id") %>'></asp:Label>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" title="Delete Shift" class="btn menu-icon vd_bd-red vd_red">
                                                            <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="vd_bg-blue vd_white" VerticalAlign="Middle" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                            <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always auto-set-height"  style="width: 874px;">
                                    <div class="col-sm-12  no-padding">
                                         <div class="col-sm-4">
                                             <asp:HiddenField ID="hfShiftID" runat="server" />
                                             <asp:Label ID="Label3" runat="server" class="control-label" Text="Shift Name"></asp:Label>
                                             <div class="">
                                                 <asp:TextBox ID="TextBox1" CssClass="form-control-blue validatetxt1" runat="server" Enabled="false"></asp:TextBox>
                                                 <div class="text-box-msg">
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="col-sm-4">
                                             <asp:Label ID="Label7" runat="server" class="control-label" Text="In"></asp:Label>
                                             <div class="">
                                                 <asp:DropDownList ID="DropDownList1" EnableTheming="false" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                 </asp:DropDownList>
                                                 <asp:DropDownList ID="DropDownList2" EnableTheming="false" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                                 </asp:DropDownList>
                                                 <asp:DropDownList ID="DropDownList3" EnableTheming="false" CssClass="form-control-blue col-sm-4 col-xs-4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                                     <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                     <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                 </asp:DropDownList>
                                                 <div class="text-box-msg">
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="col-sm-4 mgbt-xs-15">
                                             <asp:Label ID="Label8" runat="server" class="control-label" Text="Out"></asp:Label>
                                             <div class="">
                                                 <asp:DropDownList ID="DropDownList4" EnableTheming="false" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                                                 </asp:DropDownList>
                                                 <asp:DropDownList ID="DropDownList5" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" AutoPostBack="True" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">
                                                 </asp:DropDownList>
                                                 <asp:DropDownList ID="DropDownList6" runat="server" EnableTheming="false" CssClass="form-control-blue col-sm-4 col-xs-4" AutoPostBack="True" OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                                                     <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                     <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                 </asp:DropDownList>
                                                 <div class="text-box-msg">
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="col-sm-4 ">
                                             <asp:Label ID="Label9" runat="server" class="control-label" Text="Shift Duration"></asp:Label>
                                             <div class="">
                                                 <asp:TextBox ID="TextBox2" ReadOnly="true" CssClass="form-control-blue" runat="server"></asp:TextBox>
                                                 <div class="text-box-msg">
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="col-sm-4 ">
                                             <label class="control-label">Grace Time IN</label>
                                             <div class="">
                                                 <asp:DropDownList ID="DropDownList7" EnableTheming="false" CssClass="form-control-blue validatedrp1 col-sm-6 col-xs-6" runat="server">
                                                 </asp:DropDownList>
                                                 <asp:DropDownList ID="DropDownList8" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp1 col-sm-6 col-xs-6">
                                                 </asp:DropDownList>
                                                 <div class="text-box-msg">
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="col-sm-4 mgbt-xs-15">
                                             <asp:Label ID="Label10" runat="server" class="control-label" Text="Absent On"></asp:Label>
                                             <div class="">
                                                 <asp:DropDownList ID="DropDownList9" EnableTheming="false" AutoPostBack="true"
                                                     CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4" runat="server" OnSelectedIndexChanged="DropDownList9_SelectedIndexChanged">
                                                 </asp:DropDownList>
                                                 <asp:DropDownList ID="DropDownList10" runat="server" EnableTheming="false" CssClass="form-control-blue validatedrp1 col-sm-4 col-xs-4">
                                                 </asp:DropDownList>
                                                 <asp:DropDownList ID="DropDownList11" runat="server" AutoPostBack="true"
                                                     EnableTheming="false" CssClass="form-control-blue col-sm-4 col-xs-4" OnSelectedIndexChanged="DropDownList11_SelectedIndexChanged">
                                                     <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                     <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                 </asp:DropDownList>
                                                 <div class="text-box-msg">
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="col-sm-4 ">
                                             <asp:Label ID="Label11" runat="server" class="control-label" Text="Send SMS In"></asp:Label>
                                             <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged"></asp:CheckBox>
                                             <div class="">
                                                 <asp:DropDownList ID="DropDownList12" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4" runat="server">
                                                 </asp:DropDownList>
                                                 <asp:DropDownList ID="DropDownList13" runat="server" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                 </asp:DropDownList>
                                                 <asp:DropDownList ID="DropDownList14" runat="server" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                     <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                     <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                 </asp:DropDownList>
                                                 <div class="text-box-msg">
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="col-sm-4 " runat="server" visible="false">
                                             <asp:Label ID="Label12" runat="server" class="control-label" Text="Send SMS Out"></asp:Label>
                                             <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="true"></asp:CheckBox>
                                             <div class="">
                                                 <asp:DropDownList ID="DropDownList15" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4" runat="server">
                                                 </asp:DropDownList>
                                                 <asp:DropDownList ID="DropDownList16" runat="server" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                 </asp:DropDownList>
                                                 <asp:DropDownList ID="DropDownList17" runat="server" EnableTheming="false" Enabled="false" CssClass="form-control-blue col-sm-4 col-xs-4">
                                                     <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                     <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                 </asp:DropDownList>
                                                 <div class="text-box-msg">
                                                 </div>
                                             </div>
                                         </div>
                                         <div class="col-sm-4  btn-a-devices-2-p2 mgbt-xs-15">
                                             <asp:Button ID="Button1" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');ValidateDropdown('.validatedrp1');return validationReturn();" CssClass="button form-control-blue " Text="Update" OnClick="Button1_Click1" />
                                             &nbsp;
                                             <asp:Button ID="Button2" runat="server" CssClass="button form-control-blue " Text="Cancel" OnClick="Button2_Click" />
                                             <div id="Div1" runat="server" style="left: 150px"></div>
                                         </div>
                                     </div>
                            </div>
                        </asp:Panel>
                        <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button2" PopupControlID="Panel1"
                            CancelControlID="Button2" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                        </asp:ModalPopupExtender>
                    </div>

                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                            <table class="tab-popup text-center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblIdD" runat="server" CssClass="hide"></asp:Label>
                                        <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label></h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lnkDeleteNo" runat="server" CssClass="button-n" CausesValidation="false">No</asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="lnkDeleteYes" runat="server" CssClass="button-y" CausesValidation="false" OnClick="lnkDeleteYes_Click">Yes</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:LinkButton ID="lnkTargetControl" runat="server" Style="display: none"></asp:LinkButton>
                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" BackgroundCssClass="popup_bg" runat="server" Enabled="true"
                            CancelControlID="lnkDeleteNo" PopupControlID="Panel2" TargetControlID="lnkTargetControl">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

