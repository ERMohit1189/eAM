<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="CoscholasticAreas.aspx.cs" Inherits="CoscholasticAreas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">

                                <div class="col-sm-12  no-padding">

                                    <div class="col-sm-12  half-width-50 mgbt-xs-15" id="Tr1" runat="server" visible="false">
                                        <label class="control-label">Mode of Entry&nbsp;<span class="vd_red">*</span></label>
                                        <div class="mgtp-6">
                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow"
                                                OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Selected="True">Single Class</asp:ListItem>
                                                <asp:ListItem>Multiple Class</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpclass" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15" id="toclass" runat="server" visible="false">
                                        <label class="control-label">To Class</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpclass0" runat="server" CssClass="form-control-blue" AutoPostBack="True" Visible="False">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Medium</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpmedium" runat="server"
                                                SkinID="ddDefault" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpmedium_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Group Name</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpGroupName" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpGroupName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Coscholastic&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue validatetxt"></asp:TextBox>

                                            <div class="text-box-msg">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Can't leave blank!"
                                                    Style="color: #CC0000" SetFocusOnError="True" ValidationGroup="a" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Coscholastic Code</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Max. Marks</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox4" runat="server" OnTextChanged="TextBox4_TextChanged" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 hide">
                                        <label class="control-label">Mode of Paper</label>
                                        <div class="">
                                            <asp:DropDownList ID="DropDownList2" runat="server" SkinID="ddDefault" CssClass="form-control-blue">
                                                <asp:ListItem Selected="True">Theory</asp:ListItem>
                                                <asp:ListItem>Practical</asp:ListItem>
                                                <asp:ListItem>Verbal</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-9 hide">
                                        <label class="control-label">Remark</label>
                                        <div class="">
                                            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15" style="display: none">
                                        <label class="control-label">IsShow</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpOptional" runat="server" CssClass="form-control-blue">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-2-p2  mgbt-xs-15">
                                        <asp:LinkButton ID="LinkButton1" OnClientClick="ValidateTextBox('.validatetxt');return validationReturn();" runat="server" OnClick="LinkButton1_Click" ValidationGroup="a" CssClass="button form-control-blue">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server" style="left: 120px;"></div>
                                    </div>


                                </div>

                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover no-bm no-head-border table-bordered text-center">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Medium">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Medium") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Co-Scholastic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("CoscholasticName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paper Mode" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("ModeOfPaper") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MaxMarks" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("MaxMarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" title="Edit" 
                                                            OnClick="LinkButton2_Click" class="btn menu-icon vd_bd-yellow vd_yellow"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="menu-action" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" title="Delete" CausesValidation="False"
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



            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel1" runat="server" CssClass="popup animated2 fadeInDown">
                    <div data-rel="scroll" data-scrollheight="450" class="scroll-show-always">
                        <div class="col-sm-12 ">
                            <table class="tab-popup">
                                <tr>
                                    <td>Class <span class="vd_red">*</span>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button5" runat="server" Style="display: none" />

                                        <asp:DropDownList ID="drpClassPanel" runat="server" CssClass="form-control-blue" Enabled="false">
                                        </asp:DropDownList>

                                    </td>
                                </tr>

                               <%-- <tr>
                                    <td>Medium <span class="vd_red">*</span>
                                    </td>
                                    <td>

                                        <asp:DropDownList ID="DropMediumPanel" runat="server" SkinID="ddDefault"
                                            CssClass="form-control-blue">
                                         
                                        </asp:DropDownList>

                                    </td>
                                </tr>--%>

                                <tr>
                                    <td>Group Name
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpGroupNamePanel" runat="server" CssClass="form-control-blue">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Coscholastic <span class="vd_red">*</span>
                                    </td>
                                    <td>

                                        <asp:TextBox ID="txtCoscholasticPanel" runat="server" CssClass="form-control-blue validatetxt1"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr class="hide">
                                    <td>Coscholastic Code
                                    </td>
                                    <td>

                                        <asp:TextBox ID="txtCoscholasticCodePanel" runat="server" CssClass="form-control-blue"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr class="hide">
                                    <td>Max. Marks
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPanelMaxMarks" runat="server" CssClass="form-control-blue"></asp:TextBox>
                                    </td>

                                </tr>
                                <tr class="hide">

                                    <td>Mode of Paper <span class="vd_red">*</span>
                                    </td>
                                    <td>

                                        <asp:DropDownList ID="drpModePaperPanel" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem Selected="True">Theory</asp:ListItem>
                                            <asp:ListItem>Practical</asp:ListItem>
                                            <asp:ListItem>Verbal</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                </tr>

                                <tr class="hide">
                                    <td>Remark
                                    </td>
                                    <td>

                                        <asp:TextBox ID="txtRemarkPanel" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control-blue"></asp:TextBox>

                                    </td>

                                </tr>
                                <tr style="display: none">

                                    <td>IsShow
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="drpOptionalPanel" runat="server" CssClass="form-control-blue">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="Button3" runat="server" OnClientClick="ValidateTextBox('.validatetxt1');return validationReturn();" CausesValidation="False" CssClass="button-y" OnClick="Button3_Click" Text="Update" />
                                        &nbsp; &nbsp;
                                <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="button-n" OnClick="Button4_Click" Text="Cancel" />
                                        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" TargetControlID="Button5" PopupControlID="Panel1"
                    CancelControlID="Button4" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
            </div>

            <div style="overflow: auto; width: 1px; height: 1px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popup animated2 fadeInDown">
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Are you sure you want to delete this?<asp:Label ID="lblvalue" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button7" runat="server" Style="display: none" />
                                </h4>
                            </td>
                        </tr>

                        <tr>
                            <td align="center">
                                <asp:Button ID="Button8" CssClass="button-n" runat="server" Text="No" OnClick="Button8_Click" />
                                &nbsp;&nbsp;  
                                <asp:Button ID="btnDelete" CssClass="button-y" runat="server" OnClick="btnDelete_Click" Text="Yes" CausesValidation="False" />

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="Panel2_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="Button7"
                    PopupControlID="Panel2" CancelControlID="Button8" BackgroundCssClass="popup_bg">
                </asp:ModalPopupExtender>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

