<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="SalaryComponent.aspx.cs" Inherits="admin_SalaryComponent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
            <script>
                try {
                    Sys.Application.add_load(datetime);
                    
                }
                catch (ex) {
                }
            </script>

    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding" id="tblInsert" runat="server">

                                    <asp:UpdatePanel ID="upMain" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-12  no-padding">
                                                <div class="col-sm-4">
                                                    <label class="control-label">Component Type&nbsp;<span class="vd_red">*</span></label><br />
                                                    <asp:RadioButtonList ID="rdoComponentType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdoComponentType_SelectedIndexChanged" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                        <asp:ListItem Text="Earning" Value="Earning" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Deduction" Value="Deduction"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>

                                            </div>
                                            <div class="col-sm-12 well" id="divEarningCtrl" runat="server">
                                                <div class="col-sm-4">
                                                    <label class="control-label">Component Name&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtComponentName_E" runat="server" CssClass="form-control-blue validatetxtE"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label class="control-label">Earning Component Category&nbsp;<span class="vd_red">*</span></label><br />
                                                    <asp:DropDownList ID="ddlCategoryE" runat="server" CssClass="form-control-blue validatedrpE">
                                                        <asp:ListItem Text="<--Select-->" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Basic Salary" Value="Basic Salary"></asp:ListItem>
                                                        <asp:ListItem Text="House Rent Allowances" Value="House Rent Allowances"></asp:ListItem>
                                                        <asp:ListItem Text="Dearness Allowances" Value="Dearness Allowances"></asp:ListItem>
                                                        <asp:ListItem Text="Conveyance Allowances" Value="Conveyance Allowances"></asp:ListItem>
                                                        <asp:ListItem Text="Medical Allowances" Value="Medical Allowances"></asp:ListItem>
                                                        <asp:ListItem Text="Special Allowances" Value="Special Allowances"></asp:ListItem>
                                                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label class="control-label">Depends On&nbsp;<span class="vd_red">*</span></label><br />
                                                    <asp:DropDownList ID="ddlDependsOnE" runat="server" CssClass="form-control-blue validatedrpE">
                                                        <asp:ListItem Text="<--Select-->" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="CTC" Value="CTC"></asp:ListItem>
                                                        <asp:ListItem Text="Basic Salary" Value="Basic Salary"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                
                                            </div>
                                            <div class="col-sm-12 well" id="divDeductionCtrl" runat="server" visible="false">
                                                
                                                <div class="col-sm-3">
                                                    <label class="control-label">Component Name&nbsp;<span class="vd_red">*</span></label>
                                                    <div class="">
                                                        <asp:TextBox ID="txtComponentName_D" runat="server" CssClass="form-control-blue validatetxtD"></asp:TextBox>
                                                        <div class="text-box-msg">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label class="control-label">Earning Component Category&nbsp;<span class="vd_red">*</span></label><br />
                                                    <asp:DropDownList ID="ddlCategoryD" runat="server" CssClass="form-control-blue validatedrpD" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoryD_SelectedIndexChanged">
                                                        <asp:ListItem Text="<--Select-->" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Employee Provident Fund (EPF)" Value="Employee Provident Fund (EPF)"></asp:ListItem>
                                                        <asp:ListItem Text="Health Insurance (ESIC)" Value="Health Insurance (ESIC)"></asp:ListItem>
                                                        <asp:ListItem Text="Professional Tax" Value="Professional Tax"></asp:ListItem>
                                                        <asp:ListItem Text="TDS" Value="TDS"></asp:ListItem>
                                                        <asp:ListItem Text="Other Deduction" Value="Other Deduction"></asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-3" runat="server" id="divDependsOn">
                                                    <label class="control-label">Depends On&nbsp;<span class="vd_red">*</span></label><br />
                                                    <asp:DropDownList ID="ddlDependsOnD" runat="server" CssClass="form-control-blue">
                                                        <asp:ListItem Text="<--Select-->" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="CTC" Value="CTC"></asp:ListItem>
                                                        <asp:ListItem Text="Basic Salary" Value="Basic Salary"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-3" id="divAmuntDeductionType" runat="server">
                                                    <label class="control-label">Amount Deduction Type</label><br />
                                                    <asp:RadioButtonList ID="rdoAmuntDeductionType" runat="server" CssClass="vd_radio radio-success" RepeatDirection="Horizontal" RepeatLayout="flow">
                                                        <asp:ListItem Text="Flexible" Value="Flexible" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Fixed" Value="Fixed"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                
                                            </div>


                                            <div class="col-sm-4  half-width-50   mgbt-xs-15">
                                                <asp:LinkButton ID="btnInsertE" runat="server" CssClass="button form-control-blue" OnClick="btnInsertE_Click" OnClientClick="ValidateTextBox('.validatetxtE');ValidateDropdown('.validatedrpE');return validationReturn();">Submit</asp:LinkButton>
                                                <asp:LinkButton ID="btnInsertD" runat="server" CssClass="button form-control-blue" Visible="false" OnClick="btnInsertD_Click" OnClientClick="ValidateTextBox('.validatetxtD');ValidateDropdown('.validatedrpD');return validationReturn();">Submit</asp:LinkButton>
                                                <div id="msgbox" runat="server" style="left: 140px;"></div>
                                            </div>

                                            <div class="col-sm-12  ">
                                                <div class=" table-responsive  table-responsive2">
                                                    <asp:GridView ID="Grd" runat="server" CssClass="table table-striped no-bm table-hover no-head-border table-bordered" AutoGenerateColumns="False">
                                                        <AlternatingRowStyle CssClass="grid_alt_details_default" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Component Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelComponentType" runat="server" Text='<%# Bind("ComponentType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Component Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelComponentName" runat="server" Text='<%# Bind("ComponentName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Component Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelComponentCategory" runat="server" Text='<%# Bind("ComponentCategory") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Depends On">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelDependsOn" runat="server" Text='<%# Bind("DependsOn") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount Deduction Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelAmountDeductionType" runat="server" Text='<%# Bind("AmountDeductionType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelLoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label><br />
                                                                    (<asp:Label ID="LabelRecordedDate" runat="server" Text='<%# Bind("RecordedDate") %>'></asp:Label>)
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CausesValidation="False"
                                                                        title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" OnClick="Button8_Click" CausesValidation="False" />
                                &nbsp;&nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:scroll(0,0);" Text="Yes" CssClass="button-y" CausesValidation="False" />


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

