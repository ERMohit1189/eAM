<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="HostelBedMaster.aspx.cs" Inherits="HostelBedMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
    <script type="text/javascript">
        function GetHostelRoom() {
            $(function () {
                $("[id$=txtSearch]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Admin/webservices/WebService.asmx/GetHostelRoom") %>',
                            data: "{ 'SearchText': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {

                                response($.map(data.d,
                                    function (item) {
                                        return {
                                            label: item.split('@')[0],
                                            val: item.split('@')[1]
                                        }
                                    }));
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hfRoomId]").val(i.item.val);
                    },
                    minLength: 1
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>
            <script>
                try {
                    Sys.Application.add_load(GetHostelRoom);
                }
                catch (ex) {

                }

            </script>
            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Room No.&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtSearch" placeholder="Room No." runat="server" AutoPostBack="True" CssClass="form-control-blue validatetxt"
                                            OnTextChanged="txtSearch_TextChanged" onkeyup="onchangetxt();" onpaste="onchangeatcopyandpaste()" />
                                            <asp:HiddenField ID="hfRoomId" runat="server" />
                                        </div>
                                        <script>
                                            function onchangetxt() {
                                                if (document.getElementById('<%= txtSearch.ClientID %>').value.length === 0) {
                                                document.getElementById('<%= hfRoomId.ClientID %>').value = "";
                                                }
                                             }
                                            function onchangeatcopyandpaste() {
                                                document.getElementById('<%= hfRoomId.ClientID %>').value = "";
                                            }
                                        </script>
                                        
                                    </div>
                                    <div class="col-sm-8  half-width-50 mgbt-xs-15"><div id="msgbox3" runat="server" style="left: 85px;"></div></div>
                                </div>
                                <div class="col-sm-12  no-padding" runat="server" id="divNoOfRoom" visible="false">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">No. of Beds&nbsp;<span class="vd_red">* </span></label>
                                        <div class="">
                                            <asp:TextBox ID="txtNoofBeds" runat="server" CssClass="form-control-blue validatetxt" OnTextChanged="txtNoofBeds_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNoofBeds" ErrorMessage="Please Enter Total Stoppage."
                                                    ValidationGroup="a" Display="Dynamic" CssClass="imp" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Stoppage Should be numeric" CssClass="imp" Display="Dynamic" ControlToValidate="txtNoofBeds"
                                                    ValidationExpression="^[1-9]\d*$" ValidationGroup="a" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="col-sm-12  mgbt-xs-5">
                                    <div class=" table-responsive  table-responsive2">

                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped table-hover  no-bm no-head-border table-bordered text-center" Visible="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Bed No.">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtBedNo" Text='<%# Bind("BedNo") %>' CssClass="validatetxt"  ValidationGroup="a"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Payment Type" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="drpPaymentType" Enabled="false">
                                                            <asp:ListItem value="Installment">Installment</asp:ListItem>
                                                            <asp:ListItem value="Monthly">Monthly</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Bed Charges">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="chkAll" Text="Bed Charges" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtPrice" Text='<%# Bind("BedCharge") %>' CssClass="validatetxt" placeholder="Bed Charges" ValidationGroup="a"  onkeypress="return isNumber(event)"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtremark" Text='<%# Bind("remark") %>' ></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="260px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" ShowFooter="true" class="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group" Visible="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Bed No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBedNo" runat="server" Text='<%# Bind("BedNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" ID="txtBedNo"  CssClass="validatetxt"  ValidationGroup="b" placeholder="Bed No."></asp:TextBox>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Payment Type" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="drpPaymentType"  Enabled="false">
                                                            <asp:ListItem value="Installment">Installment</asp:ListItem>
                                                            <asp:ListItem value="Monthly">Monthly</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList runat="server" ID="drpPaymentTypeFooter" Enabled="false">
                                                            <asp:ListItem value="Installment">Installment</asp:ListItem>
                                                            <asp:ListItem value="Monthly">Monthly</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bed Charges">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("BedCharge") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" ID="txtBedCharge"  CssClass="validatetxt"  ValidationGroup="b" placeholder="Bed Charges"  onkeypress="return isNumber(event)"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblremark" runat="server" Text='<%# Bind("remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" ID="txtremark" placeholder="Remark"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="260px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bed Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbedStatus" runat="server" Text='<%# Bind("bedStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <FooterTemplate>
                                                    <asp:LinkButton ID="btnAdd" runat="server" title="Edit" 
                                                            OnClick="btnAdd_Click" class="btn menu-icon vd_bd-green vd_green"> <i class="fa fa-plus"></i></asp:LinkButton>
                                                         </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Booking Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBookedStatus" runat="server" Text='<%# Bind("BookedStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBedIdEdit" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBedId" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div id="msgbox2" runat="server" style="left: 85px;"></div>
                                    </div>
                                </div>
                                  <div class="col-sm-12  half-width-50  btn-a-devices-2-p2 mgbt-xs-15 text-center">
                                        <asp:LinkButton ID="BtnSubmit" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" runat="server" OnClick="BtnSubmit_Click" ValidationGroup="a" CssClass="button" Visible="false">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 85px;"></div>
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
                            <td><h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="Button7" runat="server" Style="display: none" />
                                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server"
                                    CancelControlID="Button8" DynamicServicePath="" Enabled="True"
                                    PopupControlID="Panel2" TargetControlID="Button7"></asp:ModalPopupExtender>
                                </h4>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <asp:Button ID="Button8" CssClass="button-n" runat="server" CausesValidation="False"
                                    OnClick="Button8_Click" Text="No" />
                                &nbsp;&nbsp; 
                                <asp:Button ID="btnDelete" runat="server" CssClass="button-y" CausesValidation="False"
                                    OnClick="btnDelete_Click" Text="Yes" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel3" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">
                        <tr>
                            <td><asp:DropDownList runat="server" ID="ddlBedStatus0">
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Damaged</asp:ListItem>
                                </asp:DropDownList></td>
                            <td><asp:DropDownList runat="server" ID="ddlBookedStatus0">
                                <asp:ListItem Value="1">Occupied</asp:ListItem>
                                <asp:ListItem Value="0">Vacant</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                         <tr>
                            <td colspan="2"><asp:TextBox runat="server" ID="txtremark0" placeholder="Remark"></asp:TextBox></td>
                             </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblvalueEdit" runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="Button1" runat="server" Style="display: none" />
                                <asp:ModalPopupExtender ID="Panel3_ModalPopupExtender" runat="server"
                                    CancelControlID="Button3" DynamicServicePath="" Enabled="True"
                                    PopupControlID="Panel3" TargetControlID="Button1"></asp:ModalPopupExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="Button3" CssClass="button-n" runat="server" CausesValidation="False"
                                    OnClick="Button3_Click" Text="No" />
                                &nbsp;&nbsp; 
                                <asp:Button ID="btnUpdate" runat="server" CssClass="button-y" CausesValidation="False"
                                    OnClick="btnUpdate_Click" Text="Update" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <script>
                function isNumber(evt) {
                    evt = (evt) ? evt : window.event;
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    return true;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

