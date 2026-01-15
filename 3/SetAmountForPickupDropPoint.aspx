<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SetAmountForPickupDropPoint.aspx.cs" Inherits="SetAmountForPickupDropPoint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <script type="text/javascript" language="javascript">
        function onRadioButtonClick(radiobutton) {
            try {
                var table1 = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_table1");
                var label1 = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_Label1");
                var hiddenField1 = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_HiddenField1");

                if (radiobutton.checked) {
                    table1.style.display = 'block';
                    label1.innerText = radiobutton.value;
                    hiddenField1.value = "1";
                }
            }
            catch (err) {
                alert(err.message);
            }
        }
    </script>


    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div id="loader" runat="server"></div>

    <div class="vd_content-section clearfix">
        <div class="row">
            <div class="col-sm-12 ">
                <div class="panel widget light-widget panel-bd-top ">
                    <div class="panel-body ">

                        <div class="col-sm-12  no-padding">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-4  no-padding half-width-50 mgbt-xs-15">
                                        <div class="col-md-7 col-sm-6  h-width-64-48">
                                            <asp:Label runat="server" ID="Label7">Select Transport Way</asp:Label><br />
                                            <asp:RadioButtonList ID="rdoWay" runat="server" RepeatDirection="Horizontal" class="vd_radio radio-success" RepeatLayout="Flow" AutoPostBack="True">
                                                <asp:ListItem Value="Oneway" Selected="True">Oneway</asp:ListItem>
                                                <asp:ListItem Value="Twoway">Twoway</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  no-padding half-width-50 mgbt-xs-15">
                                        <div class="col-md-7 col-sm-6  h-width-64-48">
                                            <asp:Label runat="server" ID="lbl1">Select Transport Type</asp:Label><br />
                                            <asp:RadioButtonList ID="rbl1" runat="server" RepeatDirection="Horizontal" class="vd_radio radio-success" RepeatLayout="Flow" OnSelectedIndexChanged="rbl1_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Value="Pickup" onclick="onRadioButtonClick(this);"> Pickup </asp:ListItem>
                                                <asp:ListItem Value="Drop" onclick="onRadioButtonClick(this);"> Drop </asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="col-sm-12  no-padding" id="table1" runat="server">

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Class Name&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DrpClass" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="DrpClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Select Class" ControlToValidate="DrpClass" CssClass="imp" Display="Dynamic"
                                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Stream&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="form-control-blue" AutoPostBack="true" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Select Branch" ControlToValidate="drpBranch" CssClass="imp" Display="Dynamic"
                                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Route Name&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DrpRouteName" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="DrpRouteName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Route" ControlToValidate="DrpRouteName" CssClass="imp" Display="Dynamic"
                                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Vehicle Type&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DrpVehicleType" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="DrpVehicleType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Vehicle Type" ControlToValidate="DrpVehicleType" CssClass="imp" Display="Dynamic"
                                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Vehicle No.&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="Drpvehicleno" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="Drpvehicleno_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Vehicle No." ControlToValidate="Drpvehicleno" CssClass="imp" Display="Dynamic"
                                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <label class="control-label">Fee Category&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="drpFeeGroup" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Select FeeGroup" ControlToValidate="drpFeeGroup" CssClass="imp" Display="Dynamic"
                                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <asp:Label ID="Label1" runat="server" class="control-label" Text=" "></asp:Label>
                                <label class="control-label">Location 1 (From)&nbsp;<span class="vd_red">*</span></label>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="Drplocation1" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="Drplocation1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Vehicle Location" ControlToValidate="Drplocation1" CssClass="imp" Display="Dynamic"
                                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="Label2" runat="server" class="control-label" Text=" "></asp:Label>
                                        <label class="control-label">Location 2 (To)&nbsp;<span class="vd_red">*</span></label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="Drplocation2" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="Drplocation2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select Vehicle Location" ControlToValidate="Drplocation2" CssClass="imp" Display="Dynamic"
                                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>

                            <div class="col-sm-12  mgbt-xs-5" id="ac1" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <div class=" table-responsive  table-responsive2">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInsttalment" runat="server" Text='<%# Eval("MonthName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtHeadAmount" runat="server" CssClass="form-control-blue"
                                                                onkeyup="Copy_TextBox1_Text_to_all_TextBox(this);" placeholder="Set Amount"></asp:TextBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control-blue" placeholder="Amount"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Amount Should be numeric" CssClass="imp" Display="Dynamic" ControlToValidate="txtAmount"
                                                                ValidationExpression="^[0-9]\d*$" ValidationGroup="a" SetFocusOnError="True"></asp:RegularExpressionValidator>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue tab-in" Width="150px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tab-in" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-sm-12  mgbt-xs-15">
                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                 <div id="msgbox" runat="server" style="left: 75px"></div>
                            </div>
                        </div>

                        <div class="col-sm-12  ">
                            <div class=" table-responsive  table-responsive2">
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView2" runat="server" class="table table-striped table-hover no-bm no-head-border table-bordered text-center"
                                            AutoGenerateColumns="false" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClassId1s" runat="server" Visible="false" Text='<%# Bind("ClassId") %>'></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Vehicle Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LocationCode" Visible="false" runat="server" Text='<%# Bind("LocationCode") %>'></asp:Label>
                                                        <asp:Label ID="lblVehicleType" runat="server" Text='<%# Bind("VehicleType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Vehicle No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleNo" runat="server" Text='<%# Bind("VehicleNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Route Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("RouteName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pickup Stoppage Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("PickupPointName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Installment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Insttalment") %>'></asp:Label>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="col-sm-12 ">
                            <div class=" table-responsive  table-responsive2">
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center" ShowFooter="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClassId2s" runat="server" Visible="false" Text='<%# Bind("ClassId") %>'></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Vehicle Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LocationCode" Visible="false" runat="server" Text='<%# Bind("LocationCode") %>'></asp:Label>
                                                        <asp:Label ID="lblVehicleType" runat="server" Text='<%# Bind("VehicleType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Vehicle No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleNo" runat="server" Text='<%# Bind("VehicleNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Route Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("RouteName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pickup Stoppage Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("DropPointName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Installment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Insttalment") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label38" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton4" runat="server" title="Edit" 
                                                            OnClick="LinkButton4_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label39" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" title="Delete" data-toggle="tooltip"
                                                            data-placement="top" class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>





    <div style="overflow: auto; width: 2px; height: 1px">
        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
            <table class="tab-popup">
                <tr>
                    <td>Route Name <span class="vd_red">*</span>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drpRouteNamePanel" runat="server" CssClass="form-control-blue">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Route" ControlToValidate="drpRouteNamePanel" CssClass="imp" Display="Dynamic"
                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>Vehicle Type <span class="vd_red">*</span>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drpVehicleTypePanel" runat="server" CssClass="form-control-blue">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Select Vehicle Type" ControlToValidate="drpVehicleTypePanel" CssClass="imp" Display="Dynamic"
                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                </tr>
                <tr>
                    <td>Vehicle No. <span class="vd_red">*</span>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drpVehicleNoPanel" runat="server" CssClass="form-control-blue" AutoPostBack="True" OnSelectedIndexChanged="drpVehicleNoPanel_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Select Vehicle No." ControlToValidate="drpVehicleNoPanel" CssClass="imp" Display="Dynamic"
                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                        Location<span class="vd_red">*</span>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drpLocationPanel" runat="server" CssClass="form-control-blue">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Select Vehicle Location" ControlToValidate="drpLocationPanel" CssClass="imp" Display="Dynamic"
                                    InitialValue="<--Select-->" SetFocusOnError="True" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>

                </tr>
                <tr>
                    <td>Insttalment
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblInsttalmentPanel" runat="server" Text="Label"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                </tr>
                <tr>
                    <td>Amount
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtAmountPanel" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Amount Should be numeric" CssClass="imp" Display="Dynamic" ControlToValidate="txtAmountPanel"
                                    ValidationExpression="^[0-9]\d*$" ValidationGroup="b" SetFocusOnError="True"></asp:RegularExpressionValidator>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>

                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="LinkButton6" runat="server" CssClass="button-y" OnClick="LinkButton6_Click" ValidationGroup="b">Update</asp:LinkButton>
                        &nbsp; &nbsp;
                        <asp:LinkButton ID="LinkButton7" runat="server" CssClass="button-n">Cancel</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

    <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none" />
    <asp:Label runat="server" ID="lblValue" Style="display: none"></asp:Label>
    <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
        BackgroundCssClass="popup_bg" Enabled="True" CancelControlID="LinkButton7"
        PopupControlID="Panel1" TargetControlID="btn1" PopupDragHandleControlID="Panel1">
    </ajaxToolkit:ModalPopupExtender>




    <div style="overflow: auto; width: 2px; height: 1px">
        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
            <table class="tab-popup text-center">
                <tr>
                    <td>
                        <h4>Are you sure you want to delete this?</h4>
                        <asp:Label ID="lblid" runat="server" Style="display: none"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButton9" runat="server" CssClass="button-n">No</asp:LinkButton>
                        &nbsp; &nbsp;
                        <asp:LinkButton ID="LinkButton8" runat="server" CssClass="button-y" OnClick="LinkButton8_Click">Yes</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

    <asp:Button ID="Button1" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" CancelControlID="LinkButton9"
        TargetControlID="Button1" PopupDragHandleControlID="Panel2" BackgroundCssClass="popup_bg" Enabled="true"
        PopupControlID="Panel2">
    </ajaxToolkit:ModalPopupExtender>




    <%--</div>--%>
    <script type="text/javascript" language="javascript">
        function Copy_TextBox1_Text_to_all_TextBox(textbox) {
            try {
                var gridView1 = document.getElementById("ContentPlaceHolder1_ContentPlaceHolderMainBox_GridView1").getElementsByTagName("input");
                var amount = textbox.value;
                for (var i = 1; i < gridView1.length; i++) {
                    gridView1[i].value = amount;
                }

            }
            catch (err) {
                alert(err.message);
            }
        }
    </script>

</asp:Content>

