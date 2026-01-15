<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="ProductOrServices.aspx.cs" Inherits="ProductOrServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script>
        var validNumber = new RegExp(/^\d*\.?\d*$/);
        

        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode !== 32 && keyCode !== 46)

                return false;
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(datetime);
            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" runat="server">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Category &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpartclcatagory" CssClass="form-control-blue" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpartclcatagory_SelectedIndexChanged">
                                                <asp:ListItem Value="Product">Product</asp:ListItem>
                                                <asp:ListItem Value="Services">Services</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divProType">
                                        <label class="control-label">Product Type &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:RadioButtonList ID="rdoProductType" CssClass="vd_radio radio-success" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem Value="NonConsumable" Selected="True">Non-Consumable</asp:ListItem>
                                                <asp:ListItem Value="Consumable">Consumable</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Name &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtname" CssClass="form-control-blue validatetxt" MaxLength="100" onKeyPress="return ValidateAlpha(event);" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divHSNCode">
                                        <label class="control-label">HSN Code&nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtHSNCode" CssClass="form-control-blue validatetxt" runat="server"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" runat="server" id="divUnit">
                                        <label class="control-label">Select Unit &nbsp;<span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="drpunit" CssClass="form-control-blue validatedrp" runat="server"></asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark &nbsp;<span class="vd_red"></span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtremark" CssClass="form-control-blue" MaxLength="100" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 btn-a-devices-3-p6  mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" OnClick="lnkSubmit_OnClick">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 76px"></div>
                                    </div>
                                </div>
                                <div class="col-sm-12  mgbt-xs-10 hide" runat="server" id="divExport" visible="false">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div style="float: right; font-size: 19px;" id="Div1" runat="server">

                                                    <asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="icon-word-color"
                                                        title="Export to Word" ><i class="fa fa-file-word-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CssClass="icon-excel-color"
                                                        title="Export to Excel" ><i class="fa fa-file-excel-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" CssClass="icon-pdf-color"
                                                        title="Export to PDF" ><i class="fa  fa-file-pdf-o "></i></asp:LinkButton>
                                                    <asp:LinkButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" CssClass="icon-print-color"
                                                        title="Print" ><i class="fa fa-print "></i></asp:LinkButton>

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

                                    <div class="col-sm-12  no-padding" runat="server" id="pnlcontrols">
                                        <div class="col-lg-12  no-padding" runat="server">

                                            <div class=" table-responsive  table-responsive2">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div id="gdv1" runat="server">
                                                            <table align="center" id="abc" runat="server" width="100%" class="table no-p-b-table  no-padding">
                                                                <tr class="hide">
                                                                    <td>
                                                                        <div id="header" runat="server" class="col-md-12 no-padding"></div>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div id="gdv" runat="server" class="text-center" style="width: 100%;">
                                                                            <asp:Label ID="heading" class="hide" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label><br />
                                                                            <asp:Label ID="lblRegister" class="hide" runat="server" Style="font-weight: 700; font-size: 14px;"></asp:Label>
                                <div class="col-sm-12 "  runat="server" id="divlistshow" style="padding: 0 10px;">
                                                <div class="table-responsive2 table-responsive">
                                                    <table class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group text-center">
                                                        <tbody>
                                                            <asp:Repeater ID="Repeater1" runat="server">
                                                                <HeaderTemplate>
                                                                    <tr>
                                                                        <th class="vd_bg-blue-np vd_white-np">#</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">Category</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">Product Type</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">Name</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">HSN Code</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">Unit Name</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">Remark</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">Edit</th>
                                                                        <th class="vd_bg-blue-np vd_white-np">Delete</th>
                                                                    </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label12" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Caregory") %>'></asp:Label>
                                                                        </td>
                                                                         <td>
                                                                            <asp:Label ID="Label6s" runat="server" Text='<%# Eval("ProductType").ToString()=="NonConsumable"?"Non-Consumable": Eval("ProductType").ToString()==""?"":Eval("ProductType").ToString() %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                        </td>
                                                                         <td>
                                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("HSNCode") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("unitName") %>'></asp:Label>
                                                                        </td>
                                                                       
                                                                        
                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                                        </td>

                                                                        <td class="menu-action" style="width: 40px;">
                                                                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("Id")%>' Visible="false"></asp:Label>
                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:LinkButton ID="LinkButton1" CausesValidation="False" runat="server" title="Edit"  CssClass="btn menu-icon vd_bd-green vd_green" OnClick="LinkButton1_OnClick"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>

                                                                        </td>
                                                                        <td class="menu-action" style="width: 40px;">
                                                                            <asp:LinkButton ID="lnkDelete" runat="server"
                                                                                CausesValidation="False" title="Delete" 
                                                                                class="btn menu-icon vd_bd-red vd_red" OnClick="lnkDelete_OnClick"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>

                                                </div>
                                            </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                        <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                            <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                                <div class="col-sm-12 ">
                                    <table class="tab-popup">
                                       
                                        <tr>
                                            <td>Category</td>
                                            <td>
                                                <asp:Button ID="Button5" runat="server" Style="display: none" />
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:DropDownList ID="drpeditarticlecategory" runat="server" class="form-control-blue" Enabled="false">
                                                        <asp:ListItem Value="Product">Product</asp:ListItem>
                                                        <asp:ListItem Value="Services">Services</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trProType">
                                            <td>Product Type</td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoeditProductType" CssClass="vd_radio radio-success" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                    <asp:ListItem Value="NonConsumable" Selected="True">Non-Consumable</asp:ListItem>
                                                    <asp:ListItem Value="Consumable">Consumable</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Name</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox ID="txteditname" CssClass="form-control-blue validatetxtt" MaxLength="100"  runat="server"></asp:TextBox>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trHSNCode">
                                            <td>HSN Code</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox ID="txteditHSNCode" CssClass="form-control-blue validatetxtt" MaxLength="100" runat="server"></asp:TextBox>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trUnit">
                                            <td>Select Unit</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:DropDownList ID="ddleditunit" CssClass="form-control-blue validatedrpp" runat="server"></asp:DropDownList>

                                                </div>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Remark</td>
                                            <td>
                                                <div class="input-group input-group-margin">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox ID="txtEditRemark" runat="server" MaxLength="100" class="form-control-blue"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div style="text-align: center;">
                                                    <asp:Button ID="btnupdate" CssClass="button-y" runat="server" Text="Update" TabIndex="3" OnClick="btnupdate_OnClick" OnClientClick="ValidateTextBox('.validatetxtt');ValidateDropdown('.validatedrpp');return validationReturn();" />
                                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" Text="Cancel" />
                                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" CancelControlID="Button4" PopupControlID="Panel1"
                            TargetControlID="Button5" BackgroundCssClass="popup_bg" BehaviorID="Panel1_ModalPopupExtender_Close" PopupDragHandleControlID="Panel1">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>

                    <div style="overflow: auto; width: 1px; height: 1px">
                        <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                            <table class="tab-popup text-center">

                                <tr>
                                    <td style="text-align: center">
                                        <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                            <asp:Button ID="Button7" runat="server" Style="display: none" />
                                        </h4>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="Button8" runat="server" CssClass="button-n" Text="No" />
                                        &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button-y" Text="Yes" OnClick="btnDelete_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <%-- ReSharper disable once Asp.InvalidControlType --%>
                        <ajaxToolkit:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                            PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg" BehaviorID="Panel2_ModalPopupExtender_Close">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>


                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

