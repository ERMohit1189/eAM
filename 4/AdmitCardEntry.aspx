<%@ Page Title="" Language="C#" MasterPageFile="~/Master/admin_root-manager.master" AutoEventWireup="true" CodeFile="AdmitCardEntry.aspx.cs" Inherits="admin_AdmitCardEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (charCode === 46) {
                var inputValue = $("#inputfield").val();
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode !== 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <div id="loader" runat="server"></div>
    <asp:UpdatePanel ID="tyu" runat="server">
        <ContentTemplate>

            <div class="vd_content-section clearfix">
                <div class="row">
                    <div class="col-sm-12 ">
                        <div class="panel widget light-widget panel-bd-top ">
                            <div class="panel-body ">
                                <div class="col-sm-12  no-padding">
                                    <div class="col-md-6 col-sm-8   no-padding  mgbt-xs-15">
                                        <label class="col-md-4 col-sm-4   control-label">Subject Mode&nbsp;<span class="vd_red">*</span></label>
                                        <div class="col-md-8  col-sm-8 ">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" CssClass="vd_radio radio-success" RepeatLayout="Flow"
                                                OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem>Subject Group wise</asp:ListItem>
                                                <asp:ListItem>Subject wise</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12  no-padding">
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Class&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpClass" runat="server" CssClass="form-control-blue" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpClass_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Section</label>
                                        <div class="">
                                            <asp:DropDownList ID="drpsection" runat="server" OnSelectedIndexChanged="drpsection_SelectedIndexChanged" CssClass="form-control-blue" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Evaluation&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpEval" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpEval_SelectedIndexChanged" CssClass="form-control-blue">
                                                <asp:ListItem>FA1</asp:ListItem>
                                                <asp:ListItem>FA2</asp:ListItem>
                                                <asp:ListItem>SA1</asp:ListItem>
                                                <asp:ListItem>FA3</asp:ListItem>
                                                <asp:ListItem>FA4</asp:ListItem>
                                                <asp:ListItem>SA2</asp:ListItem>
                                                <asp:ListItem>PRE BOARD</asp:ListItem>
                                                <asp:ListItem>BOARD</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Subject&nbsp;<span class="vd_red">*</span></label>
                                        <div class="">
                                            <asp:DropDownList ID="drpSubject" runat="server" CssClass="textbox">
                                            </asp:DropDownList>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4  half-width-50 mgbt-xs-15 no-padding">
                                        <div class="col-sm-6 col-xs-6 half-width-50 ">
                                            <label class="control-label">From Time</label>
                                            <div class="">
                                                <asp:TextBox ID="txtFromHour" runat="server" CssClass="form-control-blue text-center validatetxt" Width="55px" placeholder="HH"
                                                    onkeypress="return isNumberKey(this);" AutoPostBack="True" MaxLength="2" OnTextChanged="txtFromHour_TextChanged"></asp:TextBox>
                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-blue" Text=":" Width="18px" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                <asp:TextBox ID="txtFromMinute" runat="server" CssClass="form-control-blue text-center" Width="55px" placeholder="MM"
                                                    onkeypress="return isNumberKey(this);" AutoPostBack="True" MaxLength="2"
                                                    OnTextChanged="txtFromMinute_TextChanged"></asp:TextBox>
                                                <div class="text-box-msg">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFromHour" runat="server"
                                                        ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFromMinute" runat="server"
                                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-6 col-xs-6 half-width-50 ">
                                            <label class="control-label">To Time</label>
                                            <div class="">
                                                <asp:TextBox ID="txtToHour" runat="server" CssClass="form-control-blue text-center validatetxt" Width="55px" placeholder="HH" onkeypress="return isNumberKey(this);"
                                                    AutoPostBack="True" MaxLength="2" OnTextChanged="txtToHour_TextChanged"></asp:TextBox>
                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-blue" Width="18px" Text=":" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                <asp:TextBox ID="txtToMinute" runat="server" CssClass="form-control-blue text-center" Width="55px" placeholder="MM"
                                                    onkeypress="return isNumberKey(this);" AutoPostBack="True" MaxLength="2" OnTextChanged="txtToMinute_TextChanged"></asp:TextBox>
                                                <div class="text-box-msg">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtToHour" runat="server"
                                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtToMinute" runat="server"
                                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50 mgbt-xs-15">
                                        <label class="control-label">Exam Date</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DDYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDYear_SelectedIndexChanged"
                                                        CssClass="col-sm-4 col-xs-4 form-control-blue">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDMonth_SelectedIndexChanged"
                                                        CssClass="col-sm-4 col-xs-4 form-control-blue">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDDate" runat="server" AutoPostBack="True" CssClass="col-sm-4 col-xs-4 form-control-blue">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-box-msg">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4  half-width-50  btn-a-devices-6-p6 mgbt-xs-15">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" OnClientClick="ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp');return validationReturn();" CssClass="button form-control-blue" OnClick="lnkSubmit_Click" ValidationGroup="a">Submit</asp:LinkButton>
                                        <div id="msgbox" runat="server"></div>
                                    </div>

                                </div>



                                <div class="col-sm-12 ">
                                    <div class=" table-responsive  table-responsive2">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover no-bm no-head-border table-bordered ">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Day">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Day") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Timings">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Duration">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("TotalTime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="vd_bg-blue vd_white" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <%-- <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" CssClass="delete" Height="16px" Width="16px" Font-Size="0px" Text='<%# Bind("Id") %>'>Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" title="Delete" CausesValidation="False"
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
                    <table class="tab-popup text-center">

                        <tr>
                            <td>
                                <h4>Do you really want to delete this record?</h4>
                                <asp:Label ID="lblvalue"
                                    runat="server" Visible="False"></asp:Label></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="button-n" />
                                &nbsp;&nbsp;
                                 <asp:Button ID="btnDelete" runat="server" CausesValidation="False"
                                     OnClick="btnDelete_Click" Text="Yes" CssClass="button-y " />



                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:Button ID="Button10" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
                    BackgroundCssClass="popup_bg" DynamicServicePath="" Enabled="True" CancelControlID="Button8"
                    PopupControlID="Panel1" TargetControlID="Button10" PopupDragHandleControlID="Panel2">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

