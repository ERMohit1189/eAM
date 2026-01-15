<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="room_master.aspx.cs" Inherits="room_master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <script>
        
    </script>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top">
                            <div class="panel-body">
                                <div class="col-sm-12 no-padding" runat="server">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Category <span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DrpCategory" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                   

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Building <span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DrpbuildingLocation" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Room Type <span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DrpRoomType" runat="server" CssClass="form-control-blue">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Room No. Type <span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DrpRoomnoType" runat="server" CssClass="form-control-blue" OnSelectedIndexChanged="DrpRoomnoType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="Alphanumeric">Alphanumeric</asp:ListItem>
                                                <asp:ListItem Value="Numeric">Numeric</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label" id="lblRoomNoType" runat="server">Room No. <span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtroomnoAlpha" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>
                                            <asp:TextBox ID="txtroomnoNum" runat="server" CssClass="form-control-blue validatetxt" Visible="false" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Room Status <span class="vd_red">*</span></label>
                                        <div class=" ">
                                            <asp:DropDownList ID="DrpRoomStatus" runat="server" CssClass="form-control-blue ">
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-8  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Remark</label>
                                        <div class=" ">
                                            <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" CssClass="textbox" Height="35px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 btn-a-devices-2-p2  mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server"></div>
                                    </div>
                                </div>

                                <div class="col-sm-12 ">
                                    <div class="table-responsive2 table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered pro-table table-header-group">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Room No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("RoomNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Room Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("RoomType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Building">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("BuildingLocation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Room Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" data-toggle="tooltip"
                                                            data-placement="top" OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CausesValidation="False"
                                                            title="Delete"  class="btn menu-icon vd_bd-red vd_red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
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



            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">

                    <table class="tab-popup text-center">
                        <tr>
                            <td>Are you sure you want to delete this?
                            </td>
                            <td>
                                <asp:Label ID="lblvalue" runat="server" CssClass="form-control-blue" Visible="False"></asp:Label>
                                <asp:Button ID="Button7" runat="server" Style="display: none" />
                                <asp:HiddenField ID="hdnBuildingName" runat="server" />
                                <asp:HiddenField ID="checkRoomName" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" OnClick="Button8_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CssClass="button-y" OnClick="btnDelete_Click"
                                    Text="Yes" CausesValidation="False" />
                            </td>
                        </tr>

                    </table>

                </asp:Panel>


                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server"
                    DynamicServicePath="" Enabled="True" TargetControlID="Button7" PopupControlID="Panel2" CancelControlID="Button8">
                </asp:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 2px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup">
                        <tr>
                            <td>Category
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpCategory0" runat="server" CssClass="form-control-blue" SkinID="ddDefault">
                                </asp:DropDownList>
                                <asp:Label ID="lblid" runat="server" CssClass="hide"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Room type
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpRoomType0" runat="server" SkinID="ddDefault" CssClass="form-control-blue">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Building/Location
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpbuildingLocation0" CssClass="form-control-blue" runat="server" SkinID="ddDefault">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Room No. Type
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpRoomnoType0" CssClass="form-control-blue" runat="server" SkinID="ddDefault">
                                    <asp:ListItem Value="Alphanumeric">Alphanumeric</asp:ListItem>
                                    <asp:ListItem Value="Numeric">Numeric</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Room No.
                            </td>
                            <td>
                                <asp:TextBox ID="txtroomnoAlpha0" CssClass="form-control-blue" runat="server" SkinID="TxtBoxDef"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="txtroomnoAlpha0" ErrorMessage="*" SetFocusOnError="True"
                                    Style="color: #990033" ValidationGroup="b"></asp:RequiredFieldValidator>
                                 <asp:TextBox ID="txtroomnoNum0" CssClass="form-control-blue" runat="server" SkinID="TxtBoxDef" Visible="false" onkeypress="return isNumber(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="txtroomnoNum0" ErrorMessage="*" SetFocusOnError="True"
                                    Style="color: #990033" ValidationGroup="b"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Room Status
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpRoomStatus0" CssClass="form-control-blue" runat="server" SkinID="ddDefault">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txtremark0" CssClass="form-control-blue" runat="server" SkinID="txtmulti"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="button-y" OnClick="LinkButton4_Click" ValidationGroup="b">Update</asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:Button ID="Button1" runat="server" Style="display: none" />
                                <asp:LinkButton ID="LinkButton5" runat="server" CssClass="button-n">Cancel</asp:LinkButton>
                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button1" PopupControlID="Panel1"
                    CancelControlID="LinkButton5" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
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

