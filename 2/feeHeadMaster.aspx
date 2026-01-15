<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="feeHeadMaster.aspx.cs"
    Inherits="feeHeadMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding ">
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Type&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="ddlFeeType" runat="server" class="form-control-blue validatedrp" OnSelectedIndexChanged="ddlFeeType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value=""><--Select--></asp:ListItem>
                                                <asp:ListItem Value="Tuition Fee">Tuition Fee</asp:ListItem>
                                                <asp:ListItem Value="Tuition Fee (Optional)">Tuition Fee (Optional)</asp:ListItem>
                                                <asp:ListItem Value="Transport Fee">Transport Fee</asp:ListItem>
                                                <asp:ListItem Value="Hostel Fee">Hostel Fee</asp:ListItem>
                                                <asp:ListItem Value="Fine (Late Fee)">Fine (Late Fee)</asp:ListItem>
                                                <asp:ListItem Value="Cheque Bounce Charge">Cheque Bounce Charge</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Fee Head&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtFeeHead" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3  half-width-50 mgbt-xs-15" id="divPriority" runat="server">
                                        <label class="control-label">Priority (Max 2 Digits)&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtPriority" runat="server" CssClass="form-control-blue validatetxt" onchange="checkMaxLength(this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 half-width-50  btn-a-devices-2-p2  mgbt-xs-15">
                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="button form-control-blue" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();">Submit</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-12  mgbt-xs-15">
                                        <div id="msgbox" runat="server" style="left: 75px"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grds" runat="server" AutoGenerateColumns="False" class="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1s" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fee Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFeeType" runat="server" Text='<%# Bind("FeeType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fee Head">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFeeHead" runat="server" Text='<%# Bind("FeeHead") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Priority">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="EditId" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click"
                                                            title="Edit"  class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="deleteId" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="grid_heading_default" />
                                            <RowStyle CssClass="grid_details_default" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>




            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>Fee Type</td>
                            <td>
                                <asp:DropDownList ID="ddlFeeTypePanel" runat="server" class="form-control-blue">
                                    <asp:ListItem Value="Tuition Fee">Tuition Fee</asp:ListItem>
                                    <asp:ListItem Value="Tuition Fee (Optional)">Tuition Fee (Optional)</asp:ListItem>
                                    <asp:ListItem Value="Transport Fee">Transport Fee</asp:ListItem>
                                    <asp:ListItem Value="Hostel Fee">Hostel Fee</asp:ListItem>
                                    <asp:ListItem Value="Fine (Late Fee)">Fine (Late Fee)</asp:ListItem>
                                    <asp:ListItem Value="Cheque Bounce Charge">Cheque Bounce Charge</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Fee Head <span class="vd_red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFeeHeadPanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>

                            </td>

                        </tr>
                        <tr id="trPriority" runat="server">
                            <td>Priority (Max 2 Digits) <span class="vd_red">*</span></td>
                            <td>
                                <asp:TextBox ID="txtPriorityPanel" runat="server" CssClass="form-control-blue validatetxt1"  onchange="checkMaxLength(this)"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button ID="btnUpdate" runat="server" CausesValidation="False" CssClass="button-y " OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" OnClick="btnUpdate_Click" Text="Update" />
                                &nbsp; &nbsp;
                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n " OnClick="Button4_Click" Text="Cancel" />
                                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                <div id="msgbox2" runat="server" style="left: 75px"></div>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="Button5" runat="server" Style="display: none" />
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button5"
                    PopupControlID="Panel1" CancelControlID="Button4" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td style="text-align: center;">
                                <h4>Do you really want to delete this record?
                            <asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: center;">

                                <asp:Button ID="Button8" runat="server" Text="No" OnClick="Button8_Click" CausesValidation="False"
                                    CssClass="button-n" />
                                &nbsp; &nbsp;
                                <asp:Button ID="btnDeleteYes" runat="server" OnClick="btnDeleteYes_Click" Text="Yes" CausesValidation="False"
                                    CssClass="button-y" />

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                </ajaxToolkit:ModalPopupExtender>
            </div>
            <script>
                
                function checkMaxLength(inputtxt) {
                    var len = $(inputtxt).val().length;
                    var phoneno = /^\d+$/;
                    if (inputtxt.value.match(phoneno) && inputtxt != null && len <= 2) {
                        inputtxt.style.border = "1px solid #D5D5D5";
                        return true;
                    }
                    else {
                        inputtxt.style.border = "1px solid Red";
                        inputtxt.value = "";
                        inputtxt.focus();
                        return false;
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
