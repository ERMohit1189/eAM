<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="VehicleDropLocationMaster.aspx.cs" Inherits="admin_VehicleDropLocationMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <style type="text/css">
        .selected {
            background-color: #666;
            color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding" runat="server" id="table1">

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Route Name&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="DrpRouteName" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="DrpRouteName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Route" ControlToValidate="DrpRouteName" CssClass="imp" Display="Dynamic"
                                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">

                                            <asp:DropDownList ID="DrpVehicleType" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="DrpVehicleType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Vehicle Type" ControlToValidate="DrpVehicleType" CssClass="imp" Display="Dynamic"
                                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Vehicle No.&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="Drpvehicleno" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="Drpvehicleno_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Vehicle No." ControlToValidate="Drpvehicleno" CssClass="imp" Display="Dynamic"
                                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Total No. of Stoppage&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtDropStopage" runat="server" CssClass="form-control-blue" AutoPostBack="True" MaxLength="2" onBlur = "ChecktenDigitNumber(this);" Placeholder="Number here" OnTextChanged="txtDropStopage_TextChanged"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDropStopage" ErrorMessage="Please Total No. of Stoppage"
                                                    ValidationGroup="a" Display="Dynamic" CssClass="imp" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark</label>
                                        <div class="">

                                            <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" CssClass="form-control-blue" Rows="1"></asp:TextBox>

                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12  mgbt-xs-5">
                                    <div class=" table-responsive  table-responsive2">

                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsrno" runat="server" Text='<%# Bind("sr") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Drop Stoppage Name">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDropName" placeholder="Enter Stoppage Name" runat="server" CssClass="form-control-blue" OnTextChanged="txtDropName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="imp"
                                                            ControlToValidate="txtDropName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="a">
                                                        </asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Arrival Time">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAHH" placeholder="HH" runat="server" CssClass="form-control-blue text-center" MaxLength="2" Width="50px" onBlur = "ChecktenDigitNumber(this);"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                            ErrorMessage="*" ControlToValidate="txtAHH" Display="Dynamic" ValidationExpression="^(([01])?([0-9])|([2][0-3]))$" ValidationGroup="a" SetFocusOnError="True" CssClass="imp"></asp:RegularExpressionValidator>
                                                        <asp:TextBox ID="txtAColon" runat="server" CssClass="textbox" Text=":" Width="4px" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                        <asp:TextBox ID="txtAMM" placeholder="MM" runat="server" CssClass="form-control-blue text-center" MaxLength="2" Width="50px" onBlur = "ChecktenDigitNumber(this);"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                            ErrorMessage="*" ControlToValidate="txtAMM" Display="Dynamic" ValidationExpression="^(([012345])?([0-9]))$" ValidationGroup="a" SetFocusOnError="True" CssClass="imp"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="160px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Departure Timing">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDHH" placeholder="HH" runat="server" CssClass="form-control-blue text-center" MaxLength="2" Width="50px" onBlur = "ChecktenDigitNumber(this);"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                            ErrorMessage="*" ControlToValidate="txtDHH" Display="Dynamic" ValidationExpression="^(([01])?([0-9])|([2][0-3]))$" ValidationGroup="a" SetFocusOnError="True" CssClass="imp"></asp:RegularExpressionValidator>
                                                        <asp:TextBox ID="txtDColon" runat="server" CssClass="textbox" Text=":" Width="4px" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                        <asp:TextBox ID="txtDMM" placeholder="MM" runat="server" CssClass="form-control-blue text-center" MaxLength="2" Width="50px" onBlur = "ChecktenDigitNumber(this);"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                                            ErrorMessage="*" ControlToValidate="txtDMM" Display="Dynamic" ValidationExpression="^(([012345])?([0-9]))$" ValidationGroup="a" SetFocusOnError="True" CssClass="imp"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="160px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Distance">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDistance" placeholder="K.M." runat="server" CssClass="form-control-blue " MaxLength="2" Width="50px"  onBlur = "ChecktenDigitNumber(this);"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                            ErrorMessage="*" ControlToValidate="txtDistance" Display="Dynamic" ValidationExpression="^[0-9]\d*$" ValidationGroup="a" SetFocusOnError="True" CssClass="imp"></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Display Order">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDisplayOrder" ReadOnly="true" runat="server" CssClass="form-control-blue text-center" Width="50px" Text='<%# Bind("srno") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Stoppage Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStopageCode" runat="server" Text="Label"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>

                                <div class="col-sm-12  mgbt-xs-15">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="LinkButton1" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div id="msgbox" runat="server" style="left: 75px;"></div>
                                </div>

                                <div class="col-sm-12  ">
                                    <div class=" table-responsive table-responsive2">

                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover no-bm no-head-border table-bordered text-center" ShowFooter="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vehicle Type" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DropPointId" Visible="false" runat="server" Text='<%# Bind("DropPointId") %>'></asp:Label>
                                                        <asp:Label ID="lblVehicleType" runat="server" Text='<%# Bind("VehicleType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vehicle No." ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleNo" runat="server" Text='<%# Bind("VehicleNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Route Name" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("RouteName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Drop Stoppage Name" ItemStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("DropPointName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Distance" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("DropDistance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Arrival Time" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ArrivalTime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Departure Time" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("DepartureTime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Display Order" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("DisplayOrder") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" title="Delete" data-toggle="tooltip"
                                                            data-placement="top" class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup ">
                        <tr>
                            <td>Route Name <span class="vd_red">*</span>
                            </td>
                            <td>

                                <asp:DropDownList ID="DrpRouteName0" AutoPostBack="true" OnSelectedIndexChanged="DrpRouteName0_SelectedIndexChanged" runat="server" CssClass="form-control-blue">
                                </asp:DropDownList>


                            </td>
                        </tr>
                        <tr>
                            <td>Vehicle <span class="vd_red">*</span>
                            </td>
                            <td>

                                <asp:DropDownList ID="DrpVehicleType0" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                    OnSelectedIndexChanged="DrpVehicleType0_SelectedIndexChanged">
                                </asp:DropDownList>


                            </td>
                        </tr>
                        <tr>

                            <td>Vehicle No. <span class="vd_red">*</span>
                            </td>
                            <td>

                                <asp:DropDownList ID="Drpvehicleno0" runat="server" CssClass=" form-control-blue">
                                </asp:DropDownList>


                            </td>
                        </tr>
                        <tr>
                            <td>Drop Stoppage Name<span class="vd_red">*</span>
                            </td>
                            <td>

                                <asp:TextBox ID="txtDropStopageName" runat="server" CssClass=" form-control-blue"></asp:TextBox>


                            </td>
                        </tr>
                        <tr>
                            <td>Arrival Time <span class="vd_red">*</span>
                            </td>
                            <td>

                                <asp:TextBox ID="txtAHH0" runat="server" CssClass="form-control-blue text-center" MaxLength="2" Width="50px" onBlur = "ChecktenDigitNumber(this);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server"
                                    ErrorMessage="*" ControlToValidate="txtAHH0" Display="Dynamic" ValidationExpression="^(([01])?([0-9])|([2][0-3]))$" ValidationGroup="b" SetFocusOnError="True" CssClass="imp"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtAColon0" runat="server" CssClass="form-control-blue" Text=":" Width="4px" Enabled="false" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox ID="txtAMM0" runat="server" CssClass="form-control-blue text-center" MaxLength="2" Width="50px" onBlur = "ChecktenDigitNumber(this);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                    ErrorMessage="*" ControlToValidate="txtAMM0" Display="Dynamic" ValidationExpression="^(([012345])?([0-9]))$" ValidationGroup="b" SetFocusOnError="True" CssClass="imp"></asp:RegularExpressionValidator>

                            </td>
                        </tr>
                        <tr>
                            <td>Departure Time<span class="vd_red">*</span>
                            </td>
                            <td>

                                <asp:TextBox ID="txtDHH0" runat="server" CssClass="form-control-blue text-center" MaxLength="2" Width="50px" onBlur = "ChecktenDigitNumber(this);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                                    ErrorMessage="*" ControlToValidate="txtDHH0" Display="Dynamic" ValidationExpression="^(([01])?([0-9])|([2][0-3]))$" ValidationGroup="b" SetFocusOnError="True" CssClass="imp"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtDColon0" runat="server" CssClass="textbox" Text=":" Width="4px" Enabled="false" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox ID="txtDMM0" runat="server" CssClass="form-control-blue text-center" MaxLength="2" Width="50px" onBlur = "ChecktenDigitNumber(this);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server"
                                    ErrorMessage="*" ControlToValidate="txtDMM0" Display="Dynamic" ValidationExpression="^(([012345])?([0-9]))$" ValidationGroup="b" SetFocusOnError="True" CssClass="imp"></asp:RegularExpressionValidator>


                            </td>

                        </tr>
                        <tr>

                            <td>Distance <span class="vd_red">*</span>
                            </td>
                            <td colspan="3">

                                <asp:TextBox ID="txtDropDistance" runat="server" CssClass="form-control-blue" MaxLength="2" onBlur = "ChecktenDigitNumber(this);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtDropDistance" ErrorMessage="Enter numeric/decimal value."
                                    Style="color: #CC0000" ValidationExpression="\d*\b.{1}\d*" ValidationGroup="b" Display="Dynamic"></asp:RegularExpressionValidator>


                            </td>

                        </tr>
                        <tr style="display: none">
                            <td style="text-align:right;">Display Order <span class="vd_red">*</span>
                            </td>
                            <td colspan="3">

                                <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="form-control-blue text-center"></asp:TextBox>


                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;" colspan="4">
                                <%-- <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                   <ContentTemplate>--%>
                                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click" ValidationGroup="b" CssClass="button-y">Update</asp:LinkButton>
                                &nbsp;&nbsp;
                                                <asp:LinkButton ID="LinkButton5" runat="server" CssClass="button-n">Cancel</asp:LinkButton>
                                <%--</ContentTemplate>
               </asp:UpdatePanel>--%>
                            </td>

                        </tr>
                    </table>
                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                    <asp:Button ID="Button9" runat="server" Style="display: none" />
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button9" PopupControlID="Panel1"
                    CancelControlID="LinkButton5" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close">
                </asp:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td style="text-align:center;" height="50">
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" /></h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;" height="50">
                                <asp:Button ID="Button8" CssClass="button-n" runat="server" Text="No" />
                                &nbsp;&nbsp;
                                            <asp:Button ID="btnDelete" CssClass="button-y" runat="server" OnClick="btnDelete_Click" Text="Yes" />
                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="Button8" DynamicServicePath=""
                        Enabled="True" PopupControlID="Panel2" TargetControlID="Button7" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                    </asp:ModalPopupExtender>
                </asp:Panel>
            </div>



            <script type="text/javascript" src="../js/jquery.min_1.js"></script>
            <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
            <script type="text/javascript">
                $(function () {
                    $("[id*=GridView1]").sortable({
                        items: 'tr:not(tr:first-child,tr:last-child)',
                        cursor: 'move',
                        axis: 'y',
                        dropOnEmpty: false,
                        start: function (event, ui) {
                            ui.item.addClass("selected");
                        },
                        stop: function (event, ui) {
                            ui.item.removeClass("selected");
                            var gridview = document.getElementById("ContentPlaceHolder1_GridView1");

                            smoke.confirm('Do you want to reorder Drop point!', function (e) {
                                if (e) {
                                    for (var i = 1; i < gridview.rows.length - 1; i++) {
                                        gridview.rows[i].cells[0].childNodes[1].innerHTML = i.toString();
                                        var id = gridview.rows[i].cells[9].childNodes[1].innerHTML;
                                        $.ajax({
                                            type: "POST",
                                            contentType: "application/json; charset=utf-8",
                                            url: "VehicleDropLocationMaster.aspx/updateGridViewReorder",
                                            data: "{'DisplayOrder':'" + i + "','id':'" + id + "'}",
                                            dataType: "json"
                                        });
                                    }

                                    smoke.alert("Updated successfully.", function () {
                                        //try{
                                        window.location.href = "VehicleDropLocationMaster.aspx";
                                        //}
                                        //catch (err) {
                                        //    alert(err.message);
                                        //}
                                    });


                                }
                                else {
                                    window.location.href = "VehicleDropLocationMaster.aspx";
                                }
                            });

                        }
                    });
                });

                function refreshPage() { location.reload(); }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


